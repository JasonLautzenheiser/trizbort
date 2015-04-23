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
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Trizbort.Export
{
    class Inform6Exporter : CodeExporter
    {
        const char SingleQuote = '\'';
        const char DoubleQuote = '"';

        public override string FileDialogTitle
        {
            get { return "Export Inform 6 Source Code"; }
        }

        public override List<KeyValuePair<string, string>> FileDialogFilters
        {
            get
            {
                return new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("Inform 6 Source Files", ".inf"),
                    new KeyValuePair<string, string>("Text Files", ".txt"),
                };
            }
        }

        protected override IEnumerable<string> ReservedWords
        {
            get { return new string[] { "Constant", "Story", "Headline", "Include", "Object", "with", "has", "hasnt", "not", "and", "or", "n_to", "s_to", "e_to", "w_to", "nw_to", "ne_to", "sw_to", "se_to", "u_to", "d_to", "in_to", "out_to", "before", "after", "if", "else", "print", "player", "location", "description" }; }
        }

        protected override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }

      protected override void ExportHeader(TextWriter writer, string title, string author, string description)
        {
            writer.WriteLine("Constant Story {0};", ToI6String(title, DoubleQuote));
            writer.WriteLine("Constant Headline {0};", ToI6String(string.Format("^By {0}^{1}^^", author, description), DoubleQuote));
            writer.WriteLine();
            writer.WriteLine("Include \"Parser\";");
            writer.WriteLine("Include \"VerbLib\";");
            writer.WriteLine();
        }

 
      protected override void ExportContent(TextWriter writer)
        {
            foreach (var location in LocationsInExportOrder)
            {
                writer.WriteLine("Object  {0} {1}", location.ExportName, ToI6String(location.Room.Name, DoubleQuote));
                writer.WriteLine("  with  description");
                writer.WriteLine("            {0},", ToI6String(location.Room.PrimaryDescription, DoubleQuote));
                foreach (var direction in AllDirections)
                {
                    var exit = location.GetBestExit(direction);
                    if (exit != null)
                    {
                        writer.WriteLine("        {0} {1},", ToI6PropertyName(direction), exit.Target.ExportName);
                    }
                }
                writer.WriteLine("   has  {0}light;", location.Room.IsDark ? "~" : string.Empty);
                writer.WriteLine();

                ExportThings(writer, location.Things, null, 1);
            }

            writer.WriteLine("[ Initialise;");
            if (LocationsInExportOrder.Count > 0)
            {
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

                writer.WriteLine("Object {0} {1} {2}", Repeat("-> ", indent), thing.ExportName, ToI6String(StripOddCharacters(thing.DisplayName, ' ', '-').Trim(), DoubleQuote));
                writer.Write("  with  name {0}", ToI6Words(StripOddCharacters(thing.DisplayName, ' ', '-')));
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

                ExportThings(writer, thing.Contents, thing, indent + 1);
            }
        }

        private static string ToI6Words(string text)
        {
            var words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length == 0)
            {
                return ToI6String("thing", SingleQuote);
            }
            string output = string.Empty;
            foreach (var word in words)
            {
                if (output.Length > 0)
                {
                    output += ' ';
                }
                output += ToI6String(word, SingleQuote);
            }
            return output;
        }

        private static string Repeat(string s, int times)
        {
            var text = string.Empty;
            for (var index = 0; index < times; ++index)
            {
                text += s;
            }
            return text;
        }

        private static string ToI6String(string text, char quote)
        {
            if (text == null)
            {
                text = string.Empty;
            }
            return string.Format("{1}{0}{1}", text.Replace('\"', '~').Replace("\r", string.Empty).Replace('\n', '^'), quote);
        }

        private static string ToI6PropertyName(AutomapDirection direction)
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
            var name = StripOddCharacters(room.Name);
            if (string.IsNullOrEmpty(name))
            {
                name = "room";
            }
            if (suffix != null)
            {
                name = string.Format("{0}{1}", name, suffix);
            }
            return name;
        }

        protected override string GetExportNameForObject(string displayName, int? suffix)
        {
            var name = StripOddCharacters(displayName);
            if (string.IsNullOrEmpty(name))
            {
                name = "item";
            }
            if (suffix != null)
            {
                name = string.Format("{0}{1}", name, suffix);
            }
            return name;
        }

        private static string StripOddCharacters(string text, params char[] exclude)
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
