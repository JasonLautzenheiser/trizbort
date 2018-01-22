
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Trizbort.Automap;
using Trizbort.Domain.AppSettings;

namespace Trizbort.Export
{
  internal class QuestExporter : CodeExporter
  {
    private const char SINGLE_QUOTE = '\'';
    private const char DOUBLE_QUOTE = '"';

    public override string FileDialogTitle => "Export Quest Source Code";

    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
    {
      new KeyValuePair<string, string>("Quest Source Files", ".aslx"),
      new KeyValuePair<string, string>("Text Files", ".txt")
    };

    protected override IEnumerable<string> ReservedWords => new[] {"object", "game", "turnscript"};
//    protected override Encoding Encoding => Encoding.ASCII;

    protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history)
    {
      writer.WriteLine("<asl version=\"550\">");
      writer.WriteLine();
      writer.WriteLine("  <include ref=\"English.aslx\"/>");
      writer.WriteLine("  <include ref=\"Core.aslx\"/>");
      writer.WriteLine("  <game name=\"{0}\">", title);
      writer.WriteLine("    <gameid>{0}</gameid>", System.Guid.NewGuid().ToString());
      writer.WriteLine("    <version>1.0</version>");
      writer.WriteLine("    <firstpublished>{0}</firstpublished>", DateTime.Now.Year.ToString());
      writer.WriteLine("    <description>{0}</description>", description);
      writer.WriteLine("  </game>");
      writer.WriteLine();
    }

    protected override void ExportContent(TextWriter writer)
    {
      foreach (var location in LocationsInExportOrder)
      {
        writer.WriteLine("  <object name=\"{0}\">", GetExportName(location.Room, null));
        writer.WriteLine("    <inherit name=\"editor_room\" />");
        writer.WriteLine("    <alias>{0}</alias>", location.Room.Name);
        if (location.Room.IsDark)
        {
          writer.WriteLine("    <dark />");
        }
        if (!string.IsNullOrEmpty(location.Room.PrimaryDescription))
        {
          writer.WriteLine("    <description>{0}</description>", location.Room.PrimaryDescription);
        }
        
        foreach (var direction in AllDirections)
        {
          var exit = location.GetBestExit(direction);
          if (exit != null)
          {
            writer.WriteLine("    <exit alias=\"{0}\" to=\"{1}\">", toQuestPropertyName(direction), exit.Target.ExportName);
            writer.WriteLine("      <inherit name=\"{0}direction\" />", toQuestPropertyName(direction));
            writer.WriteLine("    </exit>");
          }
        }

        foreach (var thing in location.Things) {
          writer.WriteLine("    <object name=\"{0}\">", thing.ExportName);
          writer.WriteLine("      <inherit name=\"editor_object\" />");
          writer.WriteLine("      <alias>{0}</alias>", thing.DisplayName);
          writer.WriteLine("    </object>");
        }
        
        // TODO need to add player if here
        writer.WriteLine("  </object>");
        writer.WriteLine();

      }

      writer.WriteLine("</asl>");
      writer.WriteLine();
    }



    private static string toQuestPropertyName(AutomapDirection direction)
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