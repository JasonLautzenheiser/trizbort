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
  internal class Inform7Exporter : CodeExporter
  {
    public override string FileDialogTitle => "Export Inform 7 Source Code";

    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
    {
      new KeyValuePair<string, string>("Inform 7 Projects", ".inform"),
      new KeyValuePair<string, string>("Inform 7 Source Files", ".ni"),
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
      writer.WriteLine("\"{0}\" by {1}", title, author);
      writer.WriteLine();

      if (!string.IsNullOrEmpty(description))
      {
        writer.WriteLine("The story description is {0}{1}", toInform7PrintableString(description), description.EndsWith(".") ? string.Empty : ".");
        writer.WriteLine();
      }

      if (!string.IsNullOrWhiteSpace(history))
        exportHistory(writer, history);
    }

    private static void exportHistory(TextWriter writer, string history)
    {
      writer.WriteLine("chapter about");
      writer.WriteLine("");
      writer.WriteLine("abouting is an action out of world.");
      writer.WriteLine("understand the command \"about\" as something new.");
      writer.WriteLine("understand \"about\" as abouting.");
      writer.WriteLine($"carry out abouting: say \"{history}\".");
    }

    protected override void ExportContent(TextWriter writer)
    {
      // export regions
      foreach (var region in RegionsInExportOrder)
      {
        writer.WriteLine("There is a region called {0}.", getExportName(region.ExportName, null));
        writer.WriteLine();
      }

      // export each location
      foreach (var location in LocationsInExportOrder)
      {
        // remember we've exported this location
        location.Exported = true;

        // tiresome format, avoids I7 errors:
        // these can occur with rooms called "West of House", or one room called "Cave" and one called "Damp Cave", etc.
        writer.Write("There is a room called {0}.", location.ExportName);
        if (location.ExportName != location.Room.Name)
        {
          writer.Write(" The printed name of it is {0}.", toInform7PrintableString(location.Room.Name));
        }
        var description = location.Room.PrimaryDescription;
        if (!string.IsNullOrEmpty(description))
        {
          writer.Write(" {0}{1}", toInform7PrintableString(description), description.EndsWith(".") ? string.Empty : ".");
        }
        if (location.Room.IsDark)
        {
          writer.Write(" It is dark.");
        }

        if (!string.IsNullOrEmpty(location.Room.Region) && !location.Room.Region.Equals(Region.DefaultRegion))
          writer.Write(" It is in {0}.",  RegionsInExportOrder.Find(p=>p.Region.RegionName == location.Room.Region).ExportName);

        writer.WriteLine(); // end the previous line
        writer.WriteLine(); // blank line

        if (location.Room.IsStartRoom)
        {
          writer.Write("The player is in {0}.", location.ExportName);
          writer.WriteLine();
        }
      }

      // export the chosen exits from each location.
      foreach (var location in LocationsInExportOrder)
      {
        // export the chosen exits from this location.
        foreach (var direction in AllDirections)
        {
          var exit = location.GetBestExit(direction);
          if (exit != null && !exit.Exported)
          {
            // remember we've exported this exit
            exit.Exported = true;

            writer.Write("{0} of {1} is {2}.", getInform7Name(direction), location.ExportName, exit.Target.ExportName);
            var oppositeDirection = CompassPointHelper.GetOpposite(direction);
            if (Exit.IsReciprocated(location, direction, exit.Target))
            {
              // only export each connection once, if reciprocated;
              // I7 will infer that the direction is two way unless contradicted.
              var reciprocal = exit.Target.GetBestExit(oppositeDirection);
              reciprocal.Exported = true;
            }
            else if (exit.Target.GetBestExit(oppositeDirection) == null)
            {
              // if we aren't laying down a contridiction which I7 will pick up,
              // then be clear about one way connections.
              writer.Write(" {0} of {1} is nowhere.", getInform7Name(oppositeDirection), exit.Target.ExportName);
            }
            writer.WriteLine();
          }
        }
      }

      writer.WriteLine();

      // if an exit is conditional, block it.
      var wroteConditionalFunction = false;
      foreach (var location in LocationsInExportOrder)
      {
        foreach (var direction in AllDirections)
        {
          var exit = location.GetBestExit(direction);
          if (exit != null && exit.Conditional)
          {
            if (!wroteConditionalFunction)
            {
              writer.WriteLine("To block conditional exits:");
              writer.WriteLine("\tsay \"An export nymph appears on your keyboard. She says, 'You can't go that way, as that exit was marked as conditional, you know, a dotted line, in Trizbort. Obviously in your game you'll have a better rationale for this than, er, me.' She looks embarrassed. 'Bye!'\"");
              writer.WriteLine();
              wroteConditionalFunction = true;
            }

            writer.WriteLine("Instead of going {0} from {1}, block conditional exits.", getInform7Name(direction).ToLowerInvariant(), location.ExportName);
          }
        }
      }

      if (wroteConditionalFunction)
      {
        // add a blank line if we need one
        writer.WriteLine();
      }

      var exportedThings = false;
      foreach (var location in LocationsInExportOrder)
      {
        foreach (var thing in location.Things)
        {
          exportedThings = true;
          if (thing.Container == null)
          {
            writer.Write("{0}{1} {2} in {3}.", getArticle(thing.ExportName), thing.ExportName, isAre(thing.ExportName), thing.Location.ExportName);
          }
          else
          {
            writer.Write("{0}{1} {2} in the {3}.", getArticle(thing.ExportName), thing.ExportName, isAre(thing.ExportName), thing.Container.ExportName);
          }
          if (thing.DisplayName != thing.ExportName)
          {
            writer.Write(" It is privately-named. The printed name of it is {0}{1} Understand {2} as {3}.", toInform7PrintableString(thing.DisplayName), thing.DisplayName.EndsWith(".") ? string.Empty : ".", toInform7UnderstandWords(thing.DisplayName), thing.ExportName);
          }
          writer.WriteLine();
        }
      }

      if (exportedThings)
      {
        // add a blank line if we need one
        writer.WriteLine();
      }
    }

    private string getArticle(string noun)
    {
      if (string.IsNullOrEmpty(noun) || isPlural(noun))
      {
        if (!string.IsNullOrEmpty(noun) && char.IsUpper(noun[0]))
        {
          // the empty string or e.g. "Mr Jones" or "Athena"
          return string.Empty;
        }

        // e.g. "Some canvas", "Some leaves"
        return "Some ";
      }

      if ("aeiouh".IndexOf(char.ToLowerInvariant(noun[0])) >= 0)
      {
        // e.g. "An aardvark", "An igloo", "An hotel"
        return "An ";
      }

      // e.g. "A trapdoor", "A mailbox", "A xylophone"
      return "A ";
    }

    private static string isAre(string noun)
    {
      return "is";
    }

    private static bool isPlural(string noun)
    {
      return !string.IsNullOrEmpty(noun) && !char.IsUpper(noun[0]) && noun.EndsWith("s") && !noun.EndsWith("ss");
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

    private static string toInform7PrintableString(string text)
    {
      return $"\"{text.Replace("'", "[']").Replace("\"", "'")}\"";
    }

    private static string toInform7UnderstandString(string text)
    {
      return $"\"{stripOddCharacters(text, '\'')}\"";
    }

    private static string toInform7UnderstandWords(string text)
    {
      // "battery-powered brass lantern" -> { "battery-powered", "brass", "lantern" }
      var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
      text = string.Empty;
      foreach (var word in words)
      {
        // "battery-powered"
        if (text.Length > 0)
        {
          text += " and ";
        }
        text += toInform7UnderstandString(word);

        //// "battery-powered" -> { "battery", "powered" }
        //var parts = word.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
        //if (parts.Length > 1)
        //{
        //    foreach (var part in parts)
        //    {
        //        if (text.Length > 0)
        //        {
        //            text += " and ";
        //        }
        //        text += ToInform7UnderstandString(part);
        //    }
        //}
      }
      return text;
    }

    private static string getInform7Name(AutomapDirection direction)
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
          throw new InvalidOperationException("Cannot convert a direction to its Inform 7 equivalent.");
      }
    }
  }
}