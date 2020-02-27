using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Trizbort.Automap;
using Trizbort.Domain;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Export.Domain;

namespace Trizbort.Export.Languages
{
    internal class A3Exporter : CodeExporter
    {
        public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("JSON Files", ".json")
        };

        public override string FileDialogTitle => "Export in A3 JSON schema";

        protected override IEnumerable<string> ReservedWords =>
            new[] {"rooms", "id", "description", "exits", "name", "room_id"};

        protected override void ExportContent(TextWriter writer)
        {
            var rooms = new List<Dictionary<string, object>>();
            var items = new List<Dictionary<string, object>>();
            var doors = new List<Dictionary<string, object>>();
            var og = new Dictionary<string, object>
            {
                {"rooms", rooms},
                {"items", items},
                {"doors", doors},
            };

            doors.AddRange(from exit in ExitsInExportOrder
                where exit.Door != null
                select new Dictionary<string, object>
                {
                    {"room1", exit.Source.ExportName}, {"room2", exit.Target.ExportName},
                    {"keys", new List<string> {exit.ConnectionDescription}}
                });

            foreach (var location in LocationsInExportOrder)
            {
                var exits = (from direction in Directions.AllDirections
                    let exit = location.GetBestExit(direction)
                    where exit != null
                    select new Dictionary<string, object>
                        {{"name", toQuestPropertyName(direction)}, {"room id", exit.Target.ExportName}}).ToList();

                var room = new Dictionary<string, object>
                {
                    {"id", location.ExportName},
                    {"description", location.Room.HasDescription ? location.Room.PrimaryDescription : ""},
                    {"exits", exits},
                    {"points", int.TryParse(location.Room.SubTitle, out var roomPoints) ? roomPoints : 0}
                };

                rooms.Add(room);

                if (location.Room.IsStartRoom)
                {
                    og.Add("start room", location.ExportName);
                }

                if (location.Room.IsEndRoom)
                {
                    og.Add("treasure room", location.ExportName);
                }

                items.AddRange(location.Things.Select(thing => new Dictionary<string, object>
                {
                    {"id", thing.ExportName},
                    {"room", location.ExportName},
                    {"points", thing.Points},
                    {"is_torch", thing.ProperNamed},
                }));
            }

            og.Add("win message", "Congratulations! You have summoned Happy Dave.");
            og.Add("dark message", "It's really dark here... Might want to find a light source.");

            writer.WriteLine(JToken.Parse(JsonConvert.SerializeObject(og)));
        }

        protected override void ExportHeader(TextWriter writer, string title, string author, string description,
            string history)
        {
        }

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
            var name = stripOddCharacters(displayName);

            return stripOddCharacters(displayName, new char[] {' '});
        }

        private static string stripOddCharacters(string text, params char[] exclude)
        {
            var exclusions = new List<char>(exclude);
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var result = string.Empty;
            foreach (var c in text)
                if (c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z' || c >= '0' && c <= '9' || c == '_' ||
                    exclusions.Contains(c))
                    result += c;
            return result;
        }


        private static string toQuestPropertyName(MappableDirection direction)
        {
            switch (direction)
            {
                case MappableDirection.North:
                    return "north";
                case MappableDirection.South:
                    return "south";
                case MappableDirection.East:
                    return "east";
                case MappableDirection.West:
                    return "west";
                case MappableDirection.NorthEast:
                    return "northeast";
                case MappableDirection.NorthWest:
                    return "northwest";
                case MappableDirection.SouthEast:
                    return "southeast";
                case MappableDirection.SouthWest:
                    return "southwest";
                case MappableDirection.Up:
                    return "up";
                case MappableDirection.Down:
                    return "down";
                case MappableDirection.In:
                    return "in";
                case MappableDirection.Out:
                    return "out";
                default:
                    Debug.Assert(false, "Unrecognised automap direction.");
                    return "north";
            }
        }
    }
}