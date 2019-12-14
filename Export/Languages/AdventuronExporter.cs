using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using Trizbort.Automap;
using Trizbort.Domain;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Export.Domain;

namespace Trizbort.Export.Languages
{
    internal class AdventuronExporter : CodeExporter
    {

        public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>> {
      new KeyValuePair<string, string>("Adventuron Source Files", ".adv"),
      new KeyValuePair<string, string>("Text Files",              ".txt")
    };

        public override string FileDialogTitle => "Adventuron Source Code (rooms only)";

        protected override IEnumerable<string> ReservedWords => new[] { "ether", "objects", "inventory", "root" };

        protected override void ExportContent(TextWriter writer)
        {

            StringBuilder headerSB = new StringBuilder();
            StringBuilder locationsSB = new StringBuilder();
            StringBuilder connectionsSB = new StringBuilder();

            locationsSB.Append("\nlocations {\n");
            connectionsSB.Append("\nconnections {\n");
            connectionsSB.Append("   from, direction, to = [\n");

            StringBuilder footerSB = new StringBuilder();
            String startRoom = null;
            Boolean isFirst = true;

            int maxLen = -1;

            foreach (var location in LocationsInExportOrder)
            {
                if  (location.ExportName.Length > maxLen)
                {
                    maxLen = location.ExportName.Length;
                }
            }

            foreach (var location in LocationsInExportOrder)
            {
                if (isFirst || location.Room.IsStartRoom)
                {
                    startRoom = location.ExportName;
                }

                String subtitle = string.IsNullOrEmpty(location.Room.SubTitle) ? null : location.Room.SubTitle;

                String roomDescription = string.IsNullOrEmpty(location.Room.PrimaryDescription) ? "" : escapeAdventuronText(location.Room.PrimaryDescription);
                locationsSB.Append("   " + padRight(location.ExportName, maxLen) + " : location \""+ roomDescription + "\" "+(subtitle == null ? "" : ("header = \""+ escapeAdventuronText(subtitle) + "\" ")) +"; // " + location.Room.Name  + "\n");
                foreach (var direction in Directions.AllDirections)
                {
                    var exit = location.GetBestExit(direction);
                    if (exit != null)
                    {
                        connectionsSB.Append("      " + location.ExportName + ", " + toAdventuronDirectionName(direction) + ", " + exit.Target.ExportName + ",\n");
                    }
                }
                isFirst = false;
            }
            writer.WriteLine();
            headerSB.Append("start_at = " + startRoom);
            connectionsSB.Append("   ]\n");
            connectionsSB.Append("}\n");
            locationsSB.Append("}\n");
            writer.WriteLine(headerSB.ToString());
            writer.WriteLine(locationsSB.ToString());
            writer.WriteLine(connectionsSB.ToString());
            writer.WriteLine(footerSB.ToString());
        }
        private static String padRight (String inputString, int maxLen)
        {
            if (maxLen == inputString.Length) {
                return inputString;
            } else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(inputString);
                for (int i=0; i < maxLen - inputString.Length; i++)
                {
                    sb.Append(" ");
                }
                return sb.ToString();
            }
        }
        private static String escapeAdventuronText(String input) {
            StringBuilder sb = new StringBuilder();
            foreach(var c in input.ToCharArray()) {
                if (c == '\n') {
                    sb.Append("\\n");
                } else if (c == '$') {
                    sb.Append("$$");
                }
                else if (c == '\\')
                {
                    sb.Append("\\\\");

                }
                else if (c == '[')
                {
                    sb.Append("[[");

                }
                else if (c == ']')
                {
                    sb.Append("]]");

                }
                else if (c == '{')
                {
                    sb.Append("{{");

                }
                else if (c == '}')
                {
                    sb.Append("}}");

                }
                else if (c == '\"')
                {
                    sb.Append("\\\"");

                }
                else if (c == '~')
                {
                    sb.Append("~~");
                }
                else if (c == '<')
                {
                    sb.Append("<<");
                }
                else if (c == '>')
                {
                    sb.Append(">>");
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history) { }

        protected override string GetExportName(Room room, int? suffix)
        {
            return getExportName(room.Name, suffix);
        }

        protected override string GetExportName(string displayName, int? suffix)
        {
            return getExportName(displayName, suffix);
        }

        private static string getExportName(string displayName, int? suffix)
        {
            var name = displayName;
            name = name.ToLower().Replace(" ", "_").Replace(".", "_").Replace("$", "_");
            if (string.IsNullOrEmpty(name)) name = "location";
            if (suffix != null) name = $"{name}_{suffix}";
            return name;
        }


        private static string toAdventuronDirectionName(MappableDirection direction)
        {
            switch (direction)
            {
                case MappableDirection.North: return "north_oneway";
                case MappableDirection.South: return "south_oneway";
                case MappableDirection.East: return "east_oneway";
                case MappableDirection.West: return "west_oneway";
                case MappableDirection.NorthEast: return "northeast_oneway";
                case MappableDirection.NorthWest: return "northwest_oneway";
                case MappableDirection.SouthEast: return "southeast_oneway";
                case MappableDirection.SouthWest: return "southwest_oneway";
                case MappableDirection.Up: return "up_oneway";
                case MappableDirection.Down: return "down_oneway";
                case MappableDirection.In: return "enter_oneway";
                case MappableDirection.Out: return "exit_oneway";
                default:
                    throw new InvalidOperationException("Cannot convert a direction to its Adventuron version.");
            }
        }
    }


}