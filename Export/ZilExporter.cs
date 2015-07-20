using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
          if (exit != null)
          {
            writer.WriteLine($"    ({direction.ToString().ToUpper()} TO {exit.Target.ExportName})");
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

    private static void exportThings(TextWriter writer, List<Thing> things, Thing container, int indent)
    {
      foreach (var thing in things.Where(thing => thing.Container == container))
      {
        writer.WriteLine();
        writer.WriteLine($"<OBJECT {thing.ExportName}");
        writer.WriteLine($"    (IN {thing.Location.ExportName})");
        writer.WriteLine($"    (DESC {DOUBLE_QUOTE}{thing.DisplayName}{DOUBLE_QUOTE})");
        writer.WriteLine(">");
        writer.WriteLine();
      }
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
      if (string.IsNullOrEmpty(text))
      {
        return string.IsNullOrEmpty(word);
      }
      var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      return words.Any(wordFound => StringComparer.InvariantCultureIgnoreCase.Compare(word, wordFound) == 0);
    }

    private static bool containsOddCharacters(string text)
    {
      foreach (var c in text)
      {
        if (c != ' ' && c != '-' && !char.IsLetterOrDigit(c)) return true;
      }
      return false;
    }

    private static string stripOddCharacters(string text, params char[] exceptChars)
    {
      var exceptCharsList = new List<char>(exceptChars);
      var newText = text.Where(c => c == ' ' || c == '-' || char.IsLetterOrDigit(c) || exceptCharsList.Contains(c)).Aggregate(string.Empty, (current, c) => current + c);
      return string.IsNullOrEmpty(newText) ? "object" : newText;
    }

    protected override string GetExportNameForObject(string displayName, int? suffix)
    {
      var name = stripOddCharacters(displayName);

      name = name.ToUpper().Replace(' ', '-');

      if (string.IsNullOrEmpty(name))
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