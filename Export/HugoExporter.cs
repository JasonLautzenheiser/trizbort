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
using System.Linq;
using System.Text;

namespace Trizbort.Export
{
    internal class HugoExporter : CodeExporter
    {
    private const char SINGLE_QUOTE = '\'';
    private const char DOUBLE_QUOTE = '"';

    public override string FileDialogTitle => "Export Hugo Source Code";

    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
    {
      new KeyValuePair<string, string>("Hugo Source Files", ".hug"),
      new KeyValuePair<string, string>("Text Files", ".txt")
    };

    protected override IEnumerable<string> ReservedWords => new[] {"Room", "Actor", "Thing", "Object", "Door", "Chair", "Heavy", "Fixture", "OpenableContainer", "Food", "GameMainDef", "if", "else", "me"};
    protected override Encoding Encoding => Encoding.ASCII;

    protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history)
    {
      writer.WriteLine("# include \"verblib.g\" ! grammar must come first");
      writer.WriteLine();
      writer.WriteLine("# ifset PRECOMPILED_LIBRARY");
      writer.WriteLine("# link \"hugolib.hlb\"");
      writer.WriteLine("#else");
      writer.WriteLine("# include \"hugolib.h\"");
      writer.WriteLine("#endif");
      writer.WriteLine();
      writer.WriteLine("routine init");
      writer.WriteLine("{");
      if (!string.IsNullOrWhiteSpace(title))
      {
        writer.WriteLine("\tFont(BOLD_ON)");
        writer.WriteLine("\t\"{0}\"", title);
        writer.WriteLine("\tFont(BOLD_OFF)");
      }
      if (!string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(author))
        writer.WriteLine("\t\"{0}, by {1}\"", description, author);
      else if (!string.IsNullOrWhiteSpace(description))
        writer.WriteLine("\t\"{0}\"", description);
      else if (!string.IsNullOrWhiteSpace(author))
        writer.WriteLine("\t\"by {0}\"", author);

      if (LocationsInExportOrder.Count > 0)
      {
        bool foundStart = false;
        foreach (var location in LocationsInExportOrder)
        {
          if (location.Room.IsStartRoom)
          {
            if (foundStart)
            {
              writer.WriteLine("! {0} is an extra StartRoom. ", location.ExportName);
            }
            writer.WriteLine("\tlocation = {0}", location.ExportName);
            foundStart = true;
          }
        }
        if (!foundStart)
            writer.WriteLine("\tlocation = {0}", LocationsInExportOrder[0].ExportName);
      }
      else
      {
        writer.WriteLine("\t! location = ... ");
      }

      if (!string.IsNullOrWhiteSpace(history))
      {
        exportHistory(writer, history);
      }
      writer.WriteLine(";");
      writer.WriteLine();
    }

    private void exportHistory(TextWriter writer, string history)
    {
      writer.WriteLine();
      writer.WriteLine("    showAbout()");
      writer.WriteLine("    {");
      writer.WriteLine($"    {DOUBLE_QUOTE}{history}{DOUBLE_QUOTE};");
      writer.WriteLine("    }");
    }

    protected override void ExportContent(TextWriter writer)
    {
      foreach (var location in LocationsInExportOrder)
      {
                if (!string.IsNullOrEmpty(location.Room.PrimaryDescription))
                {
                    writer.WriteLine("\tlong_desc");
                    writer.WriteLine("\t{");
                    writer.WriteLine("\t\t{0}", location.Room.PrimaryDescription);
                    writer.WriteLine("\t}");
                }
                writer.WriteLine("room {0}", location.ExportName);
                writer.WriteLine("{");
                foreach (var direction in AllDirections)
                {
                    var exit = location.GetBestExit(direction);
                    if (exit != null)
                    {
                        writer.WriteLine("\t{0} {1}", toHugoPropertyName(direction), exit.Target.ExportName);
                    }
                }
                writer.WriteLine("}");
                writer.WriteLine();

                exportThings(writer, location.Things, null, 1);
            }

            writer.WriteLine("player_character you \"you\"");
      writer.WriteLine("{");
      writer.WriteLine("}");
            writer.WriteLine();

    }

    private static void exportThings(TextWriter writer, List<Thing> things, Thing container, int indent)
    {
      foreach (var thing in things.Where(thing => thing.Container == container)) {
                writer.WriteLine("object {0}", thing.ExportName);
                writer.WriteLine("{");
                writer.WriteLine("\tin {0}", thing.Location.ExportName);
                writer.WriteLine("}");
                writer.WriteLine();

                exportThings(writer, thing.Contents, thing, indent + 1);
      }
    }

    private static string repeat(char c, int times)
    {
      var text = string.Empty;
      for (var index = 0; index < times; ++index)
      {
        text += c;
      }
      return text;
    }

    private static string toHugoString(string text, char quote)
    {
      if (text == null)
      {
        text = string.Empty;
      }
      return string.Format("{1}{0}{1}", text.Replace(quote.ToString(), $@"\{quote}"), quote);
    }

    private static string toHugoPropertyName(AutomapDirection direction)
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
          return "n_to";
      }
    }

    protected override string GetExportName(Room room, int? suffix)
    {
      var name = stripOddCharacters(room.Name);
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
      var name = stripOddCharacters(displayName);
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