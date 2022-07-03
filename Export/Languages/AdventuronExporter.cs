using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Trizbort.Domain;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;

namespace Trizbort.Export.Languages; 

internal sealed class AdventuronExporter : CodeExporter
{
  // Adventuron limits headers to 25 characters
  private const int MaximumHeaderLength = 25;

  public override List<KeyValuePair<string, string>> FileDialogFilters => 
    new List<KeyValuePair<string, string>> {
      new KeyValuePair<string, string>("Adventuron Source Files", ".adv"), 
      new KeyValuePair<string, string>("Text Files", ".txt")
    };

  public override string FileDialogTitle => "Adventuron Source Code (rooms only)";

  protected override IEnumerable<string> ReservedWords => new[] { "ether", "objects", "inventory", "root", "player" };

  protected override void ExportContent(TextWriter writer)
  {

    StringBuilder headerSB = new StringBuilder();
    StringBuilder locationsSB = new StringBuilder();
    StringBuilder connectionsSB = new StringBuilder();

    locationsSB.Append("\nlocations {\n");
    connectionsSB.Append("\nconnections {\n");
    connectionsSB.Append("   from, direction, to = [\n");

    StringBuilder footerSB = new StringBuilder();
    string startRoom = null;
    bool isFirst = true;

    int maxLen = -1;

    foreach (var location in LocationsInExportOrder.Where(location => location.ExportName.Length > maxLen)) {
      maxLen = escapeAdventuronId(location.ExportName).Length;
    }

    foreach (var location in LocationsInExportOrder)
    {
      if (isFirst || location.Room.IsStartRoom)
      {
        startRoom = location.ExportName;
      }

      //String subtitle = string.IsNullOrEmpty(location.Room.SubTitle) ? null : location.Room.SubTitle;

      string roomDescription = string.IsNullOrEmpty(location.Room.PrimaryDescription) ? "" : escapeAdventuronText(location.Room.PrimaryDescription);
      string locationRoomName = string.IsNullOrEmpty(location.Room.Name) ? "" : location.Room.Name;

      if (locationRoomName.Length > MaximumHeaderLength)
      {
        // Limit to 'MaximumHeaderLength' characters before escaping !
        locationRoomName = locationRoomName.Substring(0, MaximumHeaderLength);
      }

      string headerDescNormalized = escapeAdventuronText(locationRoomName);
      string headerDescription = (" header = \""+ headerDescNormalized + "\"");
      locationsSB.Append("   " + padRight(escapeAdventuronId(location.ExportName), maxLen) + " : location \""+ roomDescription + "\"" + headerDescription + ";\n");
      foreach (var direction in Directions.AllDirections)
      {
        var exit = location.GetBestExit(direction);
        if (exit != null)
        {
          connectionsSB.Append("      " + escapeAdventuronId(location.ExportName) + ", " + toAdventuronDirectionName(direction) + ", " + escapeAdventuronId(exit.Target.ExportName) + ",\n");
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
  private static string padRight (string inputString, int maxLen)
  {
    if (maxLen == inputString.Length) {
      return inputString;
    }

    StringBuilder sb = new StringBuilder();
    sb.Append(inputString);
    for (int i=0; i < maxLen - inputString.Length; i++)
    {
      sb.Append(" ");
    }
    return sb.ToString();
  }

  private static string escapeAdventuronId(string input)
  {
    StringBuilder sb = new StringBuilder();
    foreach (var c in input) {
      switch (c) {
        case ' ':
        case '_': {
          if (sb.Length > 0 && sb[^1] != '_')
          {
            sb.Append('_');
          }

          break;
        }
        case '\'':
          break;
        default: {
          if (
            (c >= 'a' && c <= 'z') ||
            (c >= 'A' && c <= 'Z') ||
            (c >= '0' && c <= '9') ||
            (c >= 160 && c <= 8231) ||
            (c >= 8234 && c <= 55295) ||
            (c >= 57344 && c <= 65533)
          ) {
            sb.Append(c);
          }

          break;
        }
      }
    }

    return sb.ToString();
  }

  private static string escapeAdventuronText(string input) {
    StringBuilder sb = new StringBuilder();
    foreach (var c in input) {
      switch (c) {
        case '\n':
          sb.Append("\\n");
          break;
        case '\r':
          // Ignore
          break;
        case '$':
          sb.Append("$$");
          break;
        case '\\':
          sb.Append("\\\\");
          break;
        case '[':
          sb.Append("[[");
          break;
        case ']':
          sb.Append("]]");
          break;
        case '{':
          sb.Append("{{");
          break;
        case '}':
          sb.Append("}}");
          break;
        case '\"':
          sb.Append("\\\"");
          break;
        case '~':
          sb.Append("~~");
          break;
        case '<':
          sb.Append("<<");
          break;
        case '>':
          sb.Append(">>");
          break;
        default:
          sb.Append(c);
          break;
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
      case MappableDirection.Out: return "leave_oneway";
      default:
        throw new InvalidOperationException("Cannot convert a direction to its Adventuron version.");
    }
  }
}