/*
    Copyright (c) 2010-2015 by Genstein and Jason Lautzenheiser.

    This file is (or was originally) part of Trizbort, the Interactive Fiction Mapper.

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Trizbort.Export
{
  internal class AlanExporter : CodeExporter
  {
    public override string FileDialogTitle => "Export Alan Source Code";

    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
    {
      new KeyValuePair<string, string>("Alan Source Files", ".i"),
      new KeyValuePair<string, string>("Text Files", ".txt")
    };

    protected override IEnumerable<string> ReservedWords => new[] {"object", "objects", "thing", "things", "door", "doors", "is", "are", "in", "on", "and", "outside", "inside"};

    protected override StreamWriter Create(string fileName)
    {
      if (Path.GetExtension(fileName) == ".inform")
      {
        var directoryName = Path.Combine(fileName, "Source");
        Directory.CreateDirectory(directoryName);
        fileName = Path.Combine(directoryName, "Story.ni");
      }

      return base.Create(fileName);
    }

    protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history)
    {
      writer.WriteLine("-- Alan currently does not process metadata such as {0}, so those will be in the metadata below.",
        string.IsNullOrWhiteSpace(description) ? "title and author" : "title, author, or description");
      writer.WriteLine("-- Trizbort exports History to the 'about' verb.");
      writer.WriteLine("-- All other metadata will be in comments below.");
      writer.WriteLine();
      writer.WriteLine("-- \"{0}\" by {1}", title, author);

      if (!string.IsNullOrWhiteSpace(description))
      {
        writer.WriteLine("-- description: {0}{1}", description, description.EndsWith(".") ? string.Empty : ".");
        writer.WriteLine();
      }

      if (!string.IsNullOrWhiteSpace(history))
      {
        exportHistory(writer, history);
      }

    }

    private static void exportHistory(TextWriter writer, string history)
    {
      string historyCoded = history.Replace("\r\n", "\"\r\n    \"");
      writer.WriteLine("Verb about");
      writer.WriteLine("    \"{0}\"", historyCoded);
      writer.WriteLine("End Verb about.");
      writer.WriteLine("");
    }

    private void printThisLoc(TextWriter writer, Location location)
    {
        // remember we've exported this location
        location.Exported = true;

        writer.WriteLine("The {0} isa location Name '{1}'", location.ExportName, location.Room.Name);

        string nowhereExits = "";

        var description = location.Room.PrimaryDescription;
        if (!string.IsNullOrWhiteSpace(description))
        {
          writer.WriteLine("  Description");
          writer.WriteLine("  \"{0}\"", description);
        }
        else
          writer.WriteLine("  Description \"\"");

        foreach (var direction in AllDirections)
        {
          var exit = location.GetBestExit(direction);

          if ((exit != null) && (exit.Exported == false))
          {
            // remember we've exported this exit
            exit.Exported = true;

            writer.WriteLine("  Exit {0} to {1}.", getAlanName(direction), exit.Target.ExportName);
            if (exit.Conditional)
            {
              writer.WriteLine("    Check");
              writer.WriteLine("      \"This was marked as a conditional exit in Trizbort, so you'll want to change it.\"");
            }
            writer.WriteLine("  End exit.");
          }
          else 
          {
            if (string.IsNullOrWhiteSpace(nowhereExits))
              nowhereExits = getAlanName(direction);
            else
              nowhereExits += " " + getAlanName(direction);
          }
        }
        if (!string.IsNullOrWhiteSpace(nowhereExits))
        {
          writer.WriteLine();
          writer.WriteLine("  Exit {0} to nowhere", nowhereExits);
          writer.WriteLine("    Check");
          writer.WriteLine("      \"You can't go that way.\"");
          writer.WriteLine("  End exit.");
        }

        if (location.Room.IsDark)
        {
          writer.WriteLine("  Is Not lit.");
        }

        writer.WriteLine("end The {0}.", location.ExportName);
        writer.WriteLine("");

        if (location.Room.IsStartRoom)
        {
          writer.WriteLine("The hero Isa actor at {0}", location.ExportName);
          writer.WriteLine("End The Hero.");
          writer.WriteLine();
        }

        foreach (var thing in location.Things)
        {
          writer.WriteLine("The {0} isa {1} at {2}.", thing.ExportName, thing.isPerson ? "actor" : "thing", location.ExportName);
          writer.WriteLine("  IsDisplayedAs {0}.", thing.DisplayName);
          writer.WriteLine("End The {0}.", thing.ExportName);
          /*if (!string.IsNullOrWhiteSpace(thing.WarningText))
          {
            string warningCode = thing.WarningText.TrimEnd();
            warningCode = warningCode.Replace("\n", "\n-- ");
            writer.WriteLine("-- {0}", warningCode);
          }*/
          writer.WriteLine();
        }

    }

    protected override void ExportContent(TextWriter writer)
    {
      foreach (var location in LocationsInExportOrder)
      {
        printThisLoc(writer, location);
      }
    }

    protected override string GetExportName(Room room, int? suffix)
    {
      return getExportName(room.Name, suffix);
    }

    protected override string GetExportName(string displayName, int? suffix)
    {
      return getExportName(displayName, suffix);
    }

    private string getExportName(string name, int? suffix)
    {
      var spaceless = true;

      if (containsOddCharacters(name))
        name = stripOddCharacters(name);

      if (containsWord(name, ReservedWords))
        if (suffix == null)
          suffix = 1;

      if (suffix != null)
        name = $"{name}{(spaceless ? string.Empty : " ")}{suffix}";

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
      var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
      return words.Any(wordFound => StringComparer.InvariantCultureIgnoreCase.Compare(word, wordFound) == 0);
    }

    private static bool containsOddCharacters(string text)
    {
      return text.Any(c => c != ' ' && c != '-' && !char.IsLetterOrDigit(c));
    }

    private static string stripOddCharacters(string text, params char[] exceptChars)
    {
      var exceptCharsList = new List<char>(exceptChars);
      var newText = text.Where(c => c == ' ' || c == '-' || char.IsLetterOrDigit(c) || exceptCharsList.Contains(c)).Aggregate(string.Empty, (current, c) => current + c);
      return string.IsNullOrEmpty(newText) ? "object" : newText;
    }

    private static string getAlanName(AutomapDirection direction)
    {
      switch (direction)
      {
        case AutomapDirection.North:
          return "North";
        case AutomapDirection.South:
          return "South";
        case AutomapDirection.East:
          return "East";
        case AutomapDirection.West:
          return "West";
        case AutomapDirection.NorthEast:
          return "Northeast";
        case AutomapDirection.SouthEast:
          return "Southeast";
        case AutomapDirection.NorthWest:
          return "Northwest";
        case AutomapDirection.SouthWest:
          return "Southwest";
        case AutomapDirection.Up:
          return "Up";
        case AutomapDirection.Down:
          return "Down";
        case AutomapDirection.In:
          return "Inside";
        case AutomapDirection.Out:
          return "Outside";
        default:
          return "";
          throw new InvalidOperationException("Cannot convert a direction to its Inform 7 equivalent.");
      }
    }
  }
}