using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using Trizbort.Extensions;
using static System.String;

namespace Trizbort.Export
{
  class ZilExporter : CodeExporter
  {
    const char SINGLE_QUOTE = '\'';
    const char DOUBLE_QUOTE = '"';

    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
    {
      new KeyValuePair<string, string>("ZIL Source File", ".zil"),
      new KeyValuePair<string, string>("Text Files", ".txt")
    };

    public override string FileDialogTitle => "Export ZIL Source Code";

    protected override IEnumerable<string> ReservedWords => new[] {"object", "objects"};


    protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history)
    {
      var startingRoom = LocationsInExportOrder.First();

      writer.WriteLine($"{DOUBLE_QUOTE}{title} main file{DOUBLE_QUOTE}");
      writer.WriteLine();
      writer.WriteLine("<VERSION ZIP>");
      writer.WriteLine("<CONSTANT RELEASEID 1>");
      writer.WriteLine();
      writer.WriteLine($"{DOUBLE_QUOTE}Main Loop{DOUBLE_QUOTE}");
      writer.WriteLine();
      writer.WriteLine($"<CONSTANT GAME-BANNER {DOUBLE_QUOTE}{title}|An interactive fiction by {author}{DOUBLE_QUOTE}>");
      writer.WriteLine($"<ROUTINE GO()");
      writer.WriteLine($"    <CRLF> <CRLF>");
      writer.WriteLine($"    <TELL {DOUBLE_QUOTE}{description}{DOUBLE_QUOTE} CR CR>");
      writer.WriteLine($"    <V-VERSION> <CRLF>");
      writer.WriteLine($"    <SETG HERE ,{startingRoom.ExportName}>");
      writer.WriteLine($"    <MOVE ,PLAYER ,HERE>");
      writer.WriteLine($"    <V-LOOK>");
      writer.WriteLine($"    <REPEAT()");
      writer.WriteLine($"        <COND (<PARSER>");
      writer.WriteLine($"               <PERFORM, PRSA, PRSO, PRSI>");
      writer.WriteLine($"               <APPLY <GETP, HERE, P?ACTION> ,M-END>");
      writer.WriteLine($"               <OR <GAME-VERB?> <CLOCKER>>)>");
      writer.WriteLine($"        <SETG HERE <LOC ,WINNER>>>>");
      writer.WriteLine();
      writer.WriteLine($"<INSERT-FILE {DOUBLE_QUOTE}parser{DOUBLE_QUOTE}>");
      writer.WriteLine();
      writer.WriteLine($"{DOUBLE_QUOTE}Objects{DOUBLE_QUOTE}");
    }

    protected override void ExportContent(TextWriter writer)
    {
      // export location
      foreach (var location in LocationsInExportOrder)
      {
        location.Exported = true;

        writer.WriteLine();
        writer.WriteLine($"<ROOM {location.ExportName}");
        writer.WriteLine($"    (DESC {DOUBLE_QUOTE}{location.Room.Name}{DOUBLE_QUOTE})");
        writer.WriteLine($"    (IN ROOMS)");
        writer.WriteLine($"    (LDESC {DOUBLE_QUOTE}{location.Room.PrimaryDescription}{DOUBLE_QUOTE})");

        foreach (var direction in AllDirections)
        {
          var exit = location.GetBestExit(direction);
          if (exit != null && exit.Conditional)
          {
            writer.WriteLine($"    ({toZILPropertyName(direction)} SORRY {DOUBLE_QUOTE}An export nymph appears on your keyboard. She says, 'You can't go that way, as that exit was marked as conditional, you know, a dotted line, in Trizbort. Obviously in your game you'll have a better rationale for this than, er, me.' She looks embarrassed. 'Bye!'{DOUBLE_QUOTE})");
          }
          else if (exit != null)
          {
            writer.WriteLine($"    ({toZILPropertyName(direction)} TO {exit.Target.ExportName})");
            var oppositeDirection = CompassPointHelper.GetOpposite(direction);
            if (Exit.IsReciprocated(location, direction, exit.Target))
            {
              var reciprocal = exit.Target.GetBestExit(oppositeDirection);
              reciprocal.Exported = true;
            }
          }
        }


        if (!location.Room.IsDark)
        {
          writer.WriteLine("    (FLAGS LIGHTBIT)");
        }
        writer.WriteLine(">");
        writer.WriteLine();
        exportThings(writer, location.Things, null, 1);
      }
    }

    private static string toZILPropertyName(AutomapDirection direction)
    {
      switch (direction)
      {
        case AutomapDirection.North:
          return "NORTH";
        case AutomapDirection.South:
          return "SOUTH";
        case AutomapDirection.East:
          return "EAST";
        case AutomapDirection.West:
          return "WEST";
        case AutomapDirection.NorthEast:
          return "NE";
        case AutomapDirection.SouthEast:
          return "SE";
        case AutomapDirection.SouthWest:
          return "SW";
        case AutomapDirection.NorthWest:
          return "NW";
        case AutomapDirection.Up:
          return "UP";
        case AutomapDirection.Down:
          return "DOWN";
        case AutomapDirection.In:
          return "IN";
        case AutomapDirection.Out:
          return "OUT";
        default:
          throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
      }
    }

    private static void exportThings(TextWriter writer, List<Thing> things, Thing container, int indent)
    {
      foreach (var thing in things.Where(p=>p.Container == container))
      {
        writer.WriteLine();
        writer.WriteLine($"<OBJECT {thing.ExportName}");

        if (thing.Container == null)
          writer.WriteLine($"    (LOC {thing.Location.ExportName})");
        else
          writer.WriteLine($"    (LOC {thing.Container.ExportName})");

        writer.WriteLine($"    (DESC {DOUBLE_QUOTE}{thing.DisplayName}{DOUBLE_QUOTE})");
        writer.WriteLine($"    (SYNONYM {getSynonyms(thing)})");
        writer.WriteLine($"    (FLAGS {getFlags(thing)})");
        writer.WriteLine(">");
        writer.WriteLine();

        if (thing.Contents.Any())
          exportThings(writer,thing.Contents,thing,indent++);
      }
    }

    private static string getSynonyms(Thing thing)
    {
      string synonyms = Empty;
      var list = new List<string>();

      var words = thing.DisplayName.Split(' ').ToList();

      words.ForEach(p=>list.Add(stripOddCharacters(p)));

      synonyms += Join(" ", list).ToUpper();
      return synonyms;
    }

    private static string getFlags(Thing thing)
    {
      string flags = Empty;

      if (thing.DisplayName.StartsWithVowel())
      {
        flags += "VOWELBIT ";
      }

      if (thing.Contents.Any())
      {
        flags += "CONTBIT ";
      }


      flags += "TAKEBIT ";
      return flags;
    }


    protected override string GetExportName(Room room, int? suffix)
    {
      var name = room.Name.ToUpper().Replace(' ','-'); 
      if (suffix != null || containsWord(name,ReservedWords) || containsOddCharacters(name))
      {
        name = stripOddCharacters(name.Replace(" ", "-"));
      }
      if (suffix != null)
      {
        name = $"{name}-{suffix}";
      }

      return name;
    }

    private static bool containsWord(string text, IEnumerable<string> words)
    {
      return words.Any(word => containsWord(text, word));
    }

    private static bool containsWord(string text, string word)
    {
      if (IsNullOrEmpty(text))
      {
        return IsNullOrEmpty(word);
      }
      var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      return words.Any(wordFound => StringComparer.InvariantCultureIgnoreCase.Compare(word, wordFound) == 0);
    }

    private static bool containsOddCharacters(string text)
    {
      return text.Any(c => c != ' ' && c != '-' && !char.IsLetterOrDigit(c));
    }

    private static string stripOddCharacters(string text, params char[] exceptChars)
    {
      var exceptCharsList = new List<char>(exceptChars);
      var newText = text.Where(c => c == ' ' || c == '-' || char.IsLetterOrDigit(c) || exceptCharsList.Contains(c)).Aggregate(Empty, (current, c) => current + c);
      return IsNullOrEmpty(newText) ? "object" : newText;
    }

    protected override string GetExportNameForObject(string displayName, int? suffix)
    {
      var name = stripOddCharacters(displayName);

      name = name.ToUpper().Replace(' ', '-');

      if (IsNullOrEmpty(name))
      {
        name = "item";
      }
      if (suffix != null)
      {
        name = $"{name}{suffix}";
      }
      return name;
    }



  }
}