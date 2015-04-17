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
    class TadsExporter : CodeExporter
    {
        const char SingleQuote = '\'';
        const char DoubleQuote = '"';

        public override string FileDialogTitle
        {
            get { return "Export TADS Source Code"; }
        }

        public override List<KeyValuePair<string, string>> FileDialogFilters
        {
            get
            {
                return new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("TADS Source Files", ".t"),
                    new KeyValuePair<string, string>("Text Files", ".txt"),
                };
            }
        }

        protected override IEnumerable<string> ReservedWords
        {
            get { return new string[] { "Room", "Actor", "Thing", "Object", "Door", "Chair", "Heavy", "Fixture", "OpenableContainer", "Food", "GameMainDef", "if", "else", "me" }; }
        }

        protected override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }

        protected override void ExportHeader(StreamWriter writer, string title, string author, string description)
        {
            writer.WriteLine("#charset \"us-ascii\"");
            writer.WriteLine();
            writer.WriteLine("#include <adv3.h>");
            writer.WriteLine("#include <en_us.h>");
            writer.WriteLine();
            writer.WriteLine("versionInfo : GameID");
            writer.WriteLine("    name = {0}", ToTadsString(title, SingleQuote));
            writer.WriteLine("    byline = {0}", ToTadsString(string.Format("By {0}", author), SingleQuote));
            writer.WriteLine("    version = '1'");
            writer.WriteLine("    desc = {0}", ToTadsString(description, SingleQuote));
            writer.WriteLine(";");
            writer.WriteLine();
        }

        protected override void ExportContent(StreamWriter writer)
        {
            foreach (var location in LocationsInExportOrder)
            {
                writer.WriteLine("{0}: {1} {2}", location.ExportName, location.Room.IsDark ? "DarkRoom" : "Room", ToTadsString(location.Room.Name, SingleQuote));
                if (!string.IsNullOrEmpty(location.Room.PrimaryDescription))
                {
                    writer.WriteLine("    {0}", ToTadsString(location.Room.PrimaryDescription, DoubleQuote));
                }
                bool anyExits = false;
                foreach (var direction in AllDirections)
                {
                    var exit = location.GetBestExit(direction);
                    if (exit != null)
                    {
                        if (!anyExits)
                        {
                            writer.WriteLine();
                            anyExits = true;
                        }
                        writer.WriteLine("    {0} = {1}", ToTadsPropertyName(direction), exit.Target.ExportName);
                    }
                }
                writer.WriteLine(";");
                writer.WriteLine();

                ExportThings(writer, location.Things, null, 1);
            }

            writer.WriteLine("me: Actor");
            if (LocationsInExportOrder.Count > 0)
            {
                writer.WriteLine("    location = {0}", LocationsInExportOrder[0].ExportName);
            }
            else
            {
                writer.WriteLine("    /* location = ... */");
            }
            writer.WriteLine(";");
            writer.WriteLine();

            writer.WriteLine("gameMain: GameMainDef");
            writer.WriteLine("    initialPlayerChar = me");
            writer.WriteLine(";");
            writer.WriteLine();
        }

        private void ExportThings(StreamWriter writer, List<Thing> things, Thing container, int indent)
        {
            foreach (var thing in things)
            {
                if (thing.Container != container)
                {
                    // match only the container we're given, or lack thereof
                    continue;
                }

                writer.WriteLine("{0} {1}: {3} {2} {2}", Repeat('+', indent), thing.ExportName, ToTadsString(StripOddCharacters(thing.DisplayName, ' ', '-').Trim(), SingleQuote), thing.Contents.Count > 0 ? "Container" : "Thing");
                writer.WriteLine(";");
                writer.WriteLine();

                ExportThings(writer, thing.Contents, thing, indent + 1);
            }
        }

        private static string Repeat(char c, int times)
        {
            var text = string.Empty;
            for (var index = 0; index < times; ++index)
            {
                text += c;
            }
            return text;
        }

        private static string ToTadsString(string text, char quote)
        {
            if (text == null)
            {
                text = string.Empty;
            }
            return string.Format("{1}{0}{1}", text.Replace(quote.ToString(), string.Format(@"\{0}", quote)), quote);
        }

        private static string ToTadsPropertyName(AutomapDirection direction)
        {
            switch (direction)
            {
                case AutomapDirection.North:
                    return "north";
                case AutomapDirection.South:
                    return "south";
                case AutomapDirection.East:
                    return "east";
                case AutomapDirection.West:
                    return "west";
                case AutomapDirection.NorthEast:
                    return "northeast";
                case AutomapDirection.NorthWest:
                    return "northwest";
                case AutomapDirection.SouthEast:
                    return "southeast";
                case AutomapDirection.SouthWest:
                    return "southwest";
                case AutomapDirection.Up:
                    return "up";
                case AutomapDirection.Down:
                    return "down";
                case AutomapDirection.In:
                    return "in";
                case AutomapDirection.Out:
                    return "out";
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
