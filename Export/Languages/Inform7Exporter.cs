/*
    Copyright (c) 2010-2018 by Genstein and Jason Lautzenheiser.

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
using Trizbort.Domain;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Domain.Misc;
using Trizbort.Export.Domain;

namespace Trizbort.Export.Languages {
  internal class Inform7Exporter : CodeExporter {
    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>> {
      new KeyValuePair<string, string>("Inform 7 Projects", ".inform"),
      new KeyValuePair<string, string>("Inform 7 Source Files", ".ni"),
      new KeyValuePair<string, string>("Text Files", ".txt")
    };

    public override string FileDialogTitle => "Export Inform 7 Source Code";

    protected override IEnumerable<string> ReservedWords => new[]
      {"object", "objects", "thing", "things", "door", "doors", "is", "are", "in", "on", "and", "outside", "inside"};

    protected override StreamWriter Create(string fileName) {
      if (Path.GetExtension(fileName) == ".inform") {
        var directoryName = Path.Combine(fileName, "Source");
        Directory.CreateDirectory(directoryName);
        fileName = Path.Combine(directoryName, "Story.ni");
      }

      return base.Create(fileName);
    }

    protected override void ExportContent(TextWriter writer) {
      var anyConditionalExits = false;

      if (MapStatistics.NumberOfRoomsWithoutRegion() > 0) {
        writer.WriteLine("book Regionless Rooms");
        writer.WriteLine();
      }

      foreach (var location in LocationsInExportOrder) {
        if (location.Room.Region != "NoRegion") continue;
        anyConditionalExits |= printThisLoc(writer, location);
      }

      // export regions
      foreach (var region in RegionsInExportOrder) {
        writer.WriteLine("book {0}", getExportName(region.ExportName, null));
        writer.WriteLine();
        writer.WriteLine("There is a region called {0}.", getExportName(region.ExportName, null));
        writer.WriteLine();
        // export each location
        foreach (var location in LocationsInExportOrder) {
          if ((location.Room.Region == region.Region.RegionName) ||(location.Room.Region == region.ExportName))
            anyConditionalExits |= printThisLoc(writer, location);
        }
      }

      if (anyConditionalExits) {
        writer.WriteLine("book conditional exit warning");
        writer.WriteLine();
        writer.WriteLine("To block conditional exits:");
        writer.WriteLine(
          "\tsay \"An export nymph appears on your keyboard. She says, 'You can't go that way, as that exit was marked as conditional, you know, a dotted line, in Trizbort. Obviously in your game you'll have a better rationale for this than, er, me.' She looks embarrassed. 'Bye!'\"");
        writer.WriteLine();
      }
    }

    protected override void ExportHeader(TextWriter writer, string title, string author, string description,
                                         string history) {
      writer.WriteLine("\"{0}\" by \"{1}\"", title, author);
      writer.WriteLine();

      if (!string.IsNullOrEmpty(description)) {
        writer.WriteLine("The story description is {0}{1}", toInform7PrintableString(description),
          description.EndsWith(".") ? string.Empty : ".");
        writer.WriteLine();
      }

      writer.WriteLine("Volume Trizbort generated map");
      writer.WriteLine();

      if (!string.IsNullOrWhiteSpace(history))
        exportHistory(writer, history);
    }

    protected override string GetExportName(Room room, int? suffix) {
      return getExportName(room.Name, suffix);
    }

    protected override string GetExportName(string displayName, int? suffix) {
      return getExportName(displayName, suffix);
    }

    private static bool containsOddCharacters(string text) {
      return text.Any(c => c != ' ' && c != '-' && !char.IsLetterOrDigit(c));
    }

    private static bool containsWord(string text, IEnumerable<string> words) {
      return words.Any(word => containsWord(text, word));
    }

    private static bool containsWord(string text, string word) {
      if (string.IsNullOrEmpty(text)) return string.IsNullOrEmpty(word);
      var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
      return words.Any(wordFound => StringComparer.InvariantCultureIgnoreCase.Compare(word, wordFound) == 0);
    }

    private static void exportHistory(TextWriter writer, string history) {
      var historyCoded = history.Replace("\r\n", "\r\n[line break]");
      writer.WriteLine("chapter about");
      writer.WriteLine("");
      writer.WriteLine("abouting is an action out of world.");
      writer.WriteLine("understand the command \"about\" as something new.");
      writer.WriteLine("understand \"about\" as abouting.");
      writer.WriteLine($"carry out abouting: say \"{historyCoded}\".");
      writer.WriteLine("");
    }

    private string getArticle(Thing myThing) {
      var noun = myThing.ExportName;

      if (myThing.ProperNamed) return "";

      if (string.IsNullOrEmpty(noun) || isPlural(noun) || myThing.Forceplural == Thing.Amounts.Plural) {
        if (!string.IsNullOrEmpty(noun) && char.IsUpper(noun[0])) return string.Empty;

        // e.g. "Some canvas", "Some leaves"
        return "Some ";
      }

      if ("aeiou".IndexOf(char.ToLowerInvariant(noun[0])) >= 0) return "An ";

      // e.g. "A trapdoor", "A mailbox", "A xylophone"
      return "A ";
    }

    private string getExportName(string name, int? suffix) {
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

    private static string getInform7Name(MappableDirection direction) {
      switch (direction) {
        case MappableDirection.North:
          return "North";
        case MappableDirection.South:
          return "South";
        case MappableDirection.East:
          return "East";
        case MappableDirection.West:
          return "West";
        case MappableDirection.NorthEast:
          return "Northeast";
        case MappableDirection.SouthEast:
          return "Southeast";
        case MappableDirection.NorthWest:
          return "Northwest";
        case MappableDirection.SouthWest:
          return "Southwest";
        case MappableDirection.Up:
          return "Up";
        case MappableDirection.Down:
          return "Down";
        case MappableDirection.In:
          return "Inside";
        case MappableDirection.Out:
          return "Outside";
        default:
          throw new InvalidOperationException("Cannot convert a direction to its Inform 7 equivalent.");
      }
    }

    private static string isAre(Thing myThing) {
      if (myThing.Forceplural == Thing.Amounts.Plural) return "are";
      return "is";
    }

    private static bool isPlural(string noun) {
      return !string.IsNullOrEmpty(noun) && !char.IsUpper(noun[0]) && noun.EndsWith("s") && !noun.EndsWith("ss");
    }

    private bool printThisLoc(TextWriter writer, Location location) {
      // remember we've exported this location

      writer.WriteLine("part {0}", location.ExportName);
      writer.WriteLine();

      // tiresome format, avoids I7 errors:
      // these can occur with rooms called "West of House", or one room called "Cave" and one called "Damp Cave", etc.
      writer.Write("There is a room called {0}.", location.ExportName);
      if (location.ExportName != location.Room.Name)
        writer.Write(" The printed name of it is {0}.", toInform7PrintableString(location.Room.Name));
      var description = location.Room.PrimaryDescription;
      if (!string.IsNullOrEmpty(description))
        writer.Write(" {0}{1}", toInform7PrintableString(description), description.EndsWith(".") ? string.Empty : ".");
      if (location.Room.IsDark) writer.Write(" It is dark.");

      if (!string.IsNullOrEmpty(location.Room.Region) && !location.Room.Region.Equals(Region.DefaultRegion))
        writer.WriteLine(" It is in {0}.",
          RegionsInExportOrder.Find(p => p.Region.RegionName == location.Room.Region).ExportName);
      else
        writer.WriteLine();

      writer.WriteLine(); // extra blank line for formatting and so utterly blank rooms don't throw an error in Inform IDE

      if (location.Room.IsStartRoom) {
        writer.Write("The player is in {0}.", location.ExportName);
        writer.WriteLine();
      }

      var exportedThings = false;

      foreach (var thing in location.Things) {
        exportedThings = true;

        var thingText = string.Empty;

        if (!thing.IsPerson) {
          thingText += $"{getArticle(thing)}{thing.ExportName} ";
        } 
        thingText += $"{whatItIs(thing)}";
        if (thing.Container == null) {
          thingText += $" in {thing.Location.ExportName}.";
        } else {
          if (thing.Container.IsPerson) {
            if (thing.Worn) {
              thingText += $" worn by {thing.Container.ExportName}.";
            } else {
              thingText += $" carried by {thing.Container.ExportName}.";
            }
          } else {
            if (thing.PartOf) {
              thingText += $" part of {thing.Container.ExportName}.";
            } else {
              thingText += $" in {thing.Container.ExportName}.";
            }
          }
        }

        writer.Write(thingText);
        
        
        
        if (thing.DisplayName != thing.ExportName)
          writer.Write(" It is privately-named. The printed name of it is {0}{1} Understand {2} as {3}.",
            toInform7PrintableString(thing.DisplayName), thing.DisplayName.EndsWith(".") ? string.Empty : ".",
            toInform7UnderstandWords(thing.DisplayName), thing.ExportName);
        writer.WriteLine();
        if (!string.IsNullOrWhiteSpace(thing.WarningText))
          writer.WriteLine($"[Note: there were errors with your bracketed definitions.\n{thing.WarningText}]");
      }

      if (exportedThings) writer.WriteLine();

      var exportedExits = false;
      // export the chosen exits from this location.
      foreach (var direction in Directions.AllDirections) {
        var exit = location.GetBestExit(direction);

        if (exit != null && exit.Exported == false) {
          // remember we've exported this exit
          exit.Exported = true;
          exportedExits = true;

          if (exit.Door == null)
            writeNormalExit(writer, location, direction, exit);
          else
            writeDoor(writer, location, direction, exit);
        }
      }

      if (exportedExits) writer.WriteLine();

      var wroteConditionalExit = false;
      foreach (var direction in Directions.AllDirections) {
        var exit = location.GetBestExit(direction);
        if (exit != null && exit.Conditional) {
          wroteConditionalExit = true;
          writer.WriteLine("Instead of going {0} from {1}, block conditional exits.",
            getInform7Name(direction).ToLowerInvariant(), location.ExportName);
        }
      }

      if (wroteConditionalExit)
        writer.WriteLine();
      return wroteConditionalExit;
    }

    private static string stripOddCharacters(string text, params char[] exceptChars) {
      var exceptCharsList = new List<char>(exceptChars);
      var newText = text.Where(c => c == ' ' || c == '-' || char.IsLetterOrDigit(c) || exceptCharsList.Contains(c))
        .Aggregate(string.Empty, (current, c) => current + c);
      return string.IsNullOrEmpty(newText) ? "object" : newText;
    }

    private static string toInform7PrintableString(string text) {
      return $"\"{text.Replace("'", "[']").Replace("\"", "'")}\"";
    }

    private static string toInform7UnderstandString(string text) {
      return $"\"{stripOddCharacters(text, '\'')}\"";
    }

    private static string toInform7UnderstandWords(string text) {
      // "battery-powered brass lantern" -> { "battery-powered", "brass", "lantern" }
      var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
      text = string.Empty;
      foreach (var word in words) {
        // "battery-powered"
        if (text.Length > 0) text += " and ";
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

    private static string whatItIs(Thing thing) {
      var whatString = string.Empty;
      if (thing.IsPerson) {
        whatString += $"{thing.ExportName} is a " + Enum.GetName(typeof(Thing.ThingGender), thing.Gender) + " person";
      }
      else if (thing.PartOf) {
        whatString += " is ";
      }
      else if (thing.IsScenery) whatString += $". {thing.ExportName} is scenery";
      else if (thing.IsContainer) whatString += $". {thing.ExportName} is a container";
      else if (thing.IsSupporter) whatString += $". {thing.ExportName} is a supporter";
      else
        whatString = "is a " + (thing.Forceplural == Thing.Amounts.Plural ? "plural-named " : "") +
                     (thing.ProperNamed ? "proper-named " : "") + "thing";

      return whatString;
    }

    private void writeDoor(TextWriter writer, Location location, MappableDirection direction, Exit exit) {
      var oppositeDirection = CompassPointHelper.GetOpposite(direction);
      var reciprocal = exit.Target.GetBestExit(oppositeDirection);
      writer.WriteLine(
        $"{exit.ConnectionName} is a door. {exit.ConnectionName} is {direction.ToString().ToLower()} of {location.ExportName} and {oppositeDirection.ToString().ToLower()} of {exit.Target.ExportName}.  ");
      writer.WriteLine(
        $"{exit.ConnectionName} is {(exit.Door.Open ? "open" : "closed")} and {(exit.Door.Openable ? "openable" : "not openable")}.");
      writer.WriteLine(
        $"{exit.ConnectionName} is {(exit.Door.Locked ? "locked" : "unlocked")} and {(exit.Door.Lockable ? "lockable" : "not lockable")}.");
      writer.WriteLine(
        $"The description of {exit.ConnectionName} is {toInform7PrintableString(exit.ConnectionDescription)}.");
      reciprocal.Exported = true;
      writer.WriteLine();
    }

    private static void writeNormalExit(TextWriter writer, Location location, MappableDirection direction, Exit exit) {
      writer.Write($"{getInform7Name(direction)} of {location.ExportName} is {exit.Target.ExportName}.");
      var oppositeDirection = CompassPointHelper.GetOpposite(direction);
      if (Exit.IsReciprocated(location, direction, exit.Target)) {
        // only export each connection once, if reciprocated;
        // I7 will infer that the direction is two way unless contradicted.
        var reciprocal = exit.Target.GetBestExit(oppositeDirection);
        reciprocal.Exported = true;
      }
      else if (exit.Target.GetBestExit(oppositeDirection) == null) {
        // if we aren't laying down a contradiction which I7 will pick up,
        // then be clear about one way connections.
        writer.Write($" {getInform7Name(oppositeDirection)} of {exit.Target.ExportName} is nowhere.");
      }

      writer.WriteLine();
    }
  }
}