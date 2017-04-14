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
using System.Diagnostics;
using System.IO;
using System.Text;
using Trizbort.Automap;

namespace Trizbort.Export
{
  internal class Inform6Exporter : CodeExporter
  {
    private const char SINGLE_QUOTE = '\'';
    private const char DOUBLE_QUOTE = '"';

    public override string FileDialogTitle => "Export Inform 6 Source Code";

    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
    {
      new KeyValuePair<string, string>("Inform 6 Source Files", ".inf"),
      new KeyValuePair<string, string>("Text Files", ".txt")
    };

    protected override IEnumerable<string> ReservedWords => new[] {"Constant", "Story", "Headline", "Include", "Object", "with", "has", "hasnt", "not", "and", "or", "n_to", "s_to", "e_to", "w_to", "nw_to", "ne_to", "sw_to", "se_to", "u_to", "d_to", "in_to", "out_to", "before", "after", "if", "else", "print", "player", "location", "description"};

//    protected override Encoding Encoding => Encoding.ASCII;

    protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history)
    {
      writer.WriteLine("Constant Story {0};", toI6String(title, DOUBLE_QUOTE));
      writer.WriteLine("Constant Headline {0};", toI6String($"^By {author}^{description}^^", DOUBLE_QUOTE));
      writer.WriteLine();
      writer.WriteLine("Include \"Parser\";");
      writer.WriteLine("Include \"VerbLib\";");
      writer.WriteLine();
    }

    protected override void ExportContent(TextWriter writer)
    {
      if (RegionsInExportOrder.Count > 0)
      {
      foreach (var region in RegionsInExportOrder)
      {
      writer.WriteLine("Class {0};", region.ExportName);
      }
      writer.WriteLine();
      }

      foreach (var location in LocationsInExportOrder)
      {
        // export the location object
        writeLocation(writer, location);

        // export the doors from this location.
        foreach (var direction in AllDirections)
        {
          var exit = location.GetBestExit(direction);
          if ((exit != null) && (exit.Door != null) && (exit.Exported == false))
          {
            // remember we've exported this exit
            exit.Exported = true;
            writeDoor(writer, location, direction, exit);
          }
        }

        // export the objects in this location
        ExportThings(writer, location.Things, null, 1);
      }

      writer.WriteLine("[ Initialise;");
      if (LocationsInExportOrder.Count > 0)
      {
        bool foundStart = false;
        foreach (var location in LocationsInExportOrder)
        {
          if (location.Room.IsStartRoom)
          {
            if (foundStart)
            {
               writer.WriteLine("! {0} is a second start-room according to Trizbort.", location.ExportName);
            }
            else
            {
              writer.WriteLine("    location = {0};", location.ExportName);
              foundStart = true;
            }
          }
        }
        if (!foundStart)
          writer.WriteLine("    location = {0};", LocationsInExportOrder[0].ExportName);
      }
            else
      {
        writer.WriteLine("    ! location = ...;");
      }
      writer.WriteLine("    ! \"^^Your opening paragraph here...^^\";");
      writer.WriteLine("];");
      writer.WriteLine();
      writer.WriteLine("Include \"Grammar\";");
      writer.WriteLine();
      if (!string.IsNullOrEmpty(Project.Current.History))
      {
          writer.WriteLine("Verb meta 'about' * -> About;");
          writer.WriteLine();
          writer.WriteLine("[ AboutSub ;");
          writer.WriteLine("  print({0});", toI6String(Project.Current.History, DOUBLE_QUOTE));
          writer.WriteLine("];");
        writer.WriteLine();
      }
    }

    private void ExportThings(TextWriter writer, List<Thing> things, Thing container, int indent)
    {
      foreach (var thing in things)
      {
        if (thing.Container != container)
        {
          // match only the container we're given, or lack thereof
          continue;
        }

        writeOneThing(writer, thing, indent);

        ExportThings(writer, thing.Contents, thing, indent + 1);
      }
    }

    private void writeOneThing(TextWriter writer, Thing thing, int indent)
    {
        writer.WriteLine("Object {0} {1} {2}", repeat("-> ", indent), thing.ExportName, toI6String(stripUnaccentedCharacters(thing.DisplayName).Trim(), DOUBLE_QUOTE));

        writer.Write("  with  name {0}", toI6Words(deaccent(stripUnaccentedCharacters(thing.DisplayName))));
        writer.Write("        description {0}", toI6String(thing.DisplayName, DOUBLE_QUOTE));
        if (thing.Contents.Count > 0)
        {
          writer.WriteLine(",");
          writer.WriteLine("   has open container;");
        }
        else
        {
          writer.WriteLine(";");
        }
        writer.WriteLine();
    }

    private void writeLocation(TextWriter writer, Location location)
    {
        writer.WriteLine("{0}  {1} {2}", location.Room.Region == Region.DefaultRegion ? "Object" : GetExportName(location.Room.Region, null), location.ExportName, toI6String(location.Room.Name, DOUBLE_QUOTE));
        writer.WriteLine("  with  description");
        writer.WriteLine("            {0},", toI6String(location.Room.PrimaryDescription, DOUBLE_QUOTE));
        foreach (var direction in AllDirections)
        {
          var exit = location.GetBestExit(direction);
          if (exit != null)
          {
            if (exit.Door != null)
              writer.WriteLine("        {0} {1},", toI6PropertyName(direction), GetExportName(exit.ConnectionName, null));
            else
              writer.WriteLine("        {0} {1},", toI6PropertyName(direction), exit.Target.ExportName);
          }
        }
        writer.WriteLine("   has  {0}light;", location.Room.IsDark ? "~" : string.Empty);
        writer.WriteLine();
    }

    private void writeDoor(TextWriter writer, Location location, AutomapDirection direction, Exit exit)
    {
      var oppositeDirection = CompassPointHelper.GetOpposite(direction);
      var reciprocal = exit.Target.GetBestExit(oppositeDirection);
      writer.WriteLine("Object {0} {1}", GetExportName(exit.ConnectionName, null), exit.ConnectionDescription);
      writer.WriteLine("  with  name {0},", toI6Words(deaccent(stripUnaccentedCharacters(exit.ConnectionName))));
      writer.WriteLine("        description {0},", toI6String(exit.ConnectionDescription, DOUBLE_QUOTE));
      writer.WriteLine("        found_in {0} {1},", location.ExportName, exit.Target.ExportName);
      writer.WriteLine("        door_to [; if (self in {0}) return {1}; return {0};],", location.ExportName, exit.Target.ExportName);
      writer.WriteLine("        door_dir [; if (self in {0}) return {1}; return {2}; ],", location.ExportName, toI6PropertyName(direction), toI6PropertyName(oppositeDirection));
      writer.WriteLine("  has   door {0} {1} {2} {3} ;", (exit.Door.Openable ? "openable" : string.Empty), (exit.Door.Open ? "open" : "~open"), (exit.Door.Lockable ? "lockable" : string.Empty), (exit.Door.Locked ? "locked" : "~locked"));
      reciprocal.Exported = true;
      writer.WriteLine();
    }

    private static string toI6Words(string text)
    {
      var words = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
      if (words.Length == 0)
      {
        return toI6String("thing", SINGLE_QUOTE);
      }
      var output = string.Empty;
      foreach (var word in words)
      {
        if (output.Length > 0)
        {
          output += ' ';
        }
        output += toI6String(deaccent(word), SINGLE_QUOTE);
      }
      return output;
    }

    private static string repeat(string s, int times)
    {
      var text = string.Empty;
      for (var index = 0; index < times; ++index)
      {
        text += s;
      }
      return text;
    }

    private static string toI6String(string text, char quote)
    {
      if (text == null)
      {
        text = string.Empty;
      }
      return string.Format("{1}{0}{1}", text.Replace('\"', '~').Replace("\r", string.Empty).Replace('\n', '^'), quote);
    }

    private static string toI6PropertyName(AutomapDirection direction)
    {
      switch (direction)
      {
        case AutomapDirection.North:
          return "n_to";
        case AutomapDirection.South:
          return "s_to";
        case AutomapDirection.East:
          return "e_to";
        case AutomapDirection.West:
          return "w_to";
        case AutomapDirection.NorthEast:
          return "ne_to";
        case AutomapDirection.NorthWest:
          return "nw_to";
        case AutomapDirection.SouthEast:
          return "se_to";
        case AutomapDirection.SouthWest:
          return "sw_to";
        case AutomapDirection.Up:
          return "u_to";
        case AutomapDirection.Down:
          return "d_to";
        case AutomapDirection.In:
          return "in_to";
        case AutomapDirection.Out:
          return "out_to";
        default:
          Debug.Assert(false, "Unrecognised automap direction.");
          return "north";
      }
    }

    protected override string GetExportName(Room room, int? suffix)
    {
      var name = deaccent(stripUnaccentedCharacters(room.Name)).Replace(" ", "");
      if (string.IsNullOrEmpty(name))
      {
        name = "room";
      }
      if (suffix != null)
      {
        name = $"{name}{suffix}";
      }
      return name;
    }

    protected override string GetExportName(string displayName, int? suffix)
    {
      var name = deaccent(stripUnaccentedCharacters(displayName)).Replace(" ", "");
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

    private static string stripUnaccentedCharacters(string text)
    {
      return stripOddCharacters(text, 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç', 'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï', 'Ñ', 'Ò', 'Ó', 'Ô', 'Õ', 'Ö', 'Ù', 'Ú', 'Û', 'Ü', 'ß', 'à', 'á', 'â', 'ã', 'ä', 'å', 'æ', 'ç', 'è', 'é', 'ê', 'ë', 'ì', 'í', 'î', 'ï', 'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', 'ù', 'ú', 'û', 'ü', ' ', '-');
    }

    private static string stripOddCharacters(string text, params char[] exclude)
    {
      var exclusions = new List<char>(exclude);
      if (string.IsNullOrEmpty(text))
      {
        return string.Empty;
      }
      var result = string.Empty;
      foreach (var c in text)
      {
        if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || c == '_' || exclusions.Contains(c))
        {
          result += c;
        }
      }
      return result;
    }
  }
}
