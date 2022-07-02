using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Trizbort.Automap;
using Trizbort.Domain;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Domain.Misc;
using Trizbort.Export.Domain;
using Trizbort.Extensions;

namespace Trizbort.Export.Languages; 

internal class ZilExporter : CodeExporter {
  private const char SINGLE_QUOTE = '\'';
  private const char DOUBLE_QUOTE = '"';
  private const char SPACE = ' ';

  public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>> {
    new KeyValuePair<string, string>("ZIL Source File", ".zil"),
    new KeyValuePair<string, string>("Text Files", ".txt")
  };

  public override string FileDialogTitle => "Export ZIL Source Code";

  protected override IEnumerable<string> ReservedWords => new[] {"object", "objects"};

  protected override void ExportContent(TextWriter writer) {
    // export location
    bool needConditionalFunction = false, wroteConditionalFunction = false;
    foreach (var location in LocationsInExportOrder) {
      writer.WriteLine();
      writer.WriteLine($"<ROOM {location.ExportName}");
      writer.WriteLine($"    (DESC {toZILString(location.Room.Name)})");
      writer.Write($"    (IN ROOMS)");

      if (!String.IsNullOrWhiteSpace(location.Room.PrimaryDescription)) {
        writer.WriteLine();
        writer.Write($"    (LDESC {toZILString(location.Room.PrimaryDescription)})");
      }

      foreach (var direction in Directions.AllDirections) {
        var exit = location.GetBestExit(direction);
        if (exit != null && exit.Conditional) {
          writer.WriteLine();
          writer.Write($"    ({toZILPropertyName(direction)} PER TRIZBORT-CONDITIONAL-EXIT)");
          needConditionalFunction = true;
        } else if (exit != null) {
          writer.WriteLine();
          writer.Write($"    ({toZILPropertyName(direction)} TO {exit.Target.ExportName})");
          var oppositeDirection = CompassPointHelper.GetOpposite(direction);
          if (Exit.IsReciprocated(location, direction, exit.Target)) {
            var reciprocal = exit.Target.GetBestExit(oppositeDirection);
            reciprocal.Exported = true;
          }
        }
      }

      if (!location.Room.IsDark) {
        writer.WriteLine();
        writer.Write("    (FLAGS LIGHTBIT)");
      }

      writer.WriteLine(">");
      writer.WriteLine();

      if (needConditionalFunction && !wroteConditionalFunction) {
        writer.WriteLine();
        writer.WriteLine("<ROUTINE TRIZBORT-CONDITIONAL-EXIT ()");
        writer.WriteLine($"    <TELL {DOUBLE_QUOTE}An export nymph appears on your keyboard. She says, 'You can't go that way, as that exit was marked as conditional, you know, a dotted line, in Trizbort. Obviously in your game you'll have a better rationale for this than, er, me.' She looks embarrassed. 'Bye!'{DOUBLE_QUOTE} CR>");
        writer.WriteLine("    <RFALSE>>");
        writer.WriteLine();
        wroteConditionalFunction = true;
      }

      exportThings(writer, location.Things, null, 1);
    }
  }


  protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history) {
    var list = Project.Current.Elements.OfType<Room>().Where(p => p.IsStartRoom).ToList();
    var startingRoom = list.Count == 0 ? LocationsInExportOrder.First() : LocationsInExportOrder.Find(p => p.Room.ID == list.First().ID);

    writer.WriteLine($"{DOUBLE_QUOTE}{title} main file{DOUBLE_QUOTE}");
    writer.WriteLine();
    writer.WriteLine("<VERSION ZIP>");
    writer.WriteLine("<CONSTANT RELEASEID 1>");
    writer.WriteLine();
    writer.WriteLine($"{DOUBLE_QUOTE}Main Loop{DOUBLE_QUOTE}");
    writer.WriteLine();
    writer.WriteLine($"<CONSTANT GAME-BANNER {DOUBLE_QUOTE}{title}|An interactive fiction by {author}{DOUBLE_QUOTE}>");
    writer.WriteLine();
    writer.WriteLine($"<ROUTINE GO ()");
    writer.WriteLine($"    <CRLF> <CRLF>");
    writer.WriteLine($"    <TELL {toZILString(description)} CR CR>");
    writer.WriteLine($"    <V-VERSION> <CRLF>");
    writer.WriteLine($"    <SETG HERE ,{startingRoom.ExportName}>");
    writer.WriteLine($"    <MOVE ,PLAYER ,HERE>");
    writer.WriteLine($"    <V-LOOK>");
    writer.WriteLine($"    <REPEAT ()");
    writer.WriteLine($"        <COND (<PARSER>");
    writer.WriteLine($"               <PERFORM ,PRSA ,PRSO ,PRSI>");
    writer.WriteLine($"               <COND (<NOT <GAME-VERB?>>");
    writer.WriteLine($"                      <APPLY <GETP ,HERE ,P?ACTION> ,M-END>");
    writer.WriteLine($"                      <CLOCKER>)>)>");
    writer.WriteLine($"        <SETG HERE <LOC ,WINNER>>>>");
    writer.WriteLine();
    writer.WriteLine($"<INSERT-FILE {DOUBLE_QUOTE}parser{DOUBLE_QUOTE}>");
    writer.WriteLine();

    if (!String.IsNullOrWhiteSpace(history)) exportHistory(writer, history);

    writer.WriteLine($"{DOUBLE_QUOTE}Objects{DOUBLE_QUOTE}");
  }


  protected override string GetExportName(Room room, int? suffix) {
    var name = room.Name.ToUpper().Replace(' ', '-');
    if (suffix != null || containsWord(name, ReservedWords) || containsOddCharacters(name)) name = stripOddCharacters(name.Replace(" ", "-"));
    if (suffix != null) name = $"{name}-{suffix}";

    return name;
  }

  protected override string GetExportName(string displayName, int? suffix) {
    var name = stripOddCharacters(displayName);

    name = name.ToUpper().Replace(' ', '-');

    if (String.IsNullOrEmpty(name)) name = "item";
    if (suffix != null) name = $"{name}{suffix}";
    return name;
  }

  private static bool containsOddCharacters(string text) {
    return text.Any(c => c != ' ' && c != '-' && !char.IsLetterOrDigit(c));
  }

  private static bool containsWord(string text, IEnumerable<string> words) {
    return words.Any(word => containsWord(text, word));
  }

  private static bool containsWord(string text, string word) {
    if (String.IsNullOrEmpty(text)) return String.IsNullOrEmpty(word);
    var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
    return words.Any(wordFound => StringComparer.InvariantCultureIgnoreCase.Compare(word, wordFound) == 0);
  }

  private void exportHistory(TextWriter writer, string history) {
    writer.WriteLine("<SYNTAX ABOUT = V-ABOUT>");
    writer.WriteLine();
    writer.WriteLine("<ROUTINE V-ABOUT ()");
    writer.WriteLine($"    <TELL {toZILString(history)} CR>>");
    writer.WriteLine();
  }

  private static void exportThings(TextWriter writer, List<Thing> things, Thing container, int indent) {
    foreach (var thing in things.Where(p => p.Container == container)) {
      writer.WriteLine();
      writer.WriteLine($"<OBJECT {thing.ExportName}");

      if (thing.Container == null)
        writer.WriteLine($"    (IN {thing.Location.ExportName})");
      else
        writer.WriteLine($"    (IN {thing.Container.ExportName})");

      writer.WriteLine($"    (DESC {toZILString(thing.DisplayName)})");

      var words = getObjectWords(thing);
      if (words.Count > 0) writer.WriteLine($"    (SYNONYM {words[words.Count - 1]})");
      if (words.Count > 1) writer.WriteLine($"    (ADJECTIVE {String.Join($"{SPACE}", words.Take(words.Count - 1))})");

      writer.WriteLine($"    (FLAGS {getFlags(thing)})>");
      writer.WriteLine();

      if (thing.Contents.Any())
        exportThings(writer, thing.Contents, thing, indent++);
    }
  }

  private static string getFlags(Thing thing) {
    var flags = new StringBuilder("TAKEBIT");

    if (thing.DisplayName.StartsWithVowel()) flags.Append(" VOWELBIT");

    if (thing.Contents.Any()) flags.Append(" CONTBIT");

    return flags.ToString();
  }

  private static IList<string> getObjectWords(Thing thing) {
    var synonyms = String.Empty;
    var list = new List<string>();

    var words = thing.DisplayName.Split(' ').ToList();

    words.ForEach(p => list.Add(stripOddCharacters(p).ToUpper()));

    return list;
  }

  private static string stripOddCharacters(string text, params char[] exceptChars) {
    var exceptCharsList = new List<char>(exceptChars);
    var newText = text.Where(c => c == ' ' || c == '-' || char.IsLetterOrDigit(c) || exceptCharsList.Contains(c)).Aggregate(String.Empty, (current, c) => current + c);
    return String.IsNullOrEmpty(newText) ? "object" : newText;
  }

  private static string toZILPropertyName(MappableDirection direction) {
    switch (direction) {
      case MappableDirection.North:
        return "NORTH";
      case MappableDirection.South:
        return "SOUTH";
      case MappableDirection.East:
        return "EAST";
      case MappableDirection.West:
        return "WEST";
      case MappableDirection.NorthEast:
        return "NE";
      case MappableDirection.SouthEast:
        return "SE";
      case MappableDirection.SouthWest:
        return "SW";
      case MappableDirection.NorthWest:
        return "NW";
      case MappableDirection.Up:
        return "UP";
      case MappableDirection.Down:
        return "DOWN";
      case MappableDirection.In:
        return "IN";
      case MappableDirection.Out:
        return "OUT";
      default:
        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
    }
  }

  private static string toZILString(string str) {
    if (str == null) str = String.Empty;
    return DOUBLE_QUOTE + str.Replace('\n', '|').Replace($"{DOUBLE_QUOTE}", $"\\{DOUBLE_QUOTE}") + DOUBLE_QUOTE;
  }
}