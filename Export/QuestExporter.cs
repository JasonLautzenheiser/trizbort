
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Trizbort.Automap;
using Trizbort.Domain.AppSettings;
using Trizbort.Domain.Elements;

namespace Trizbort.Export
{
  internal class QuestRoomsExporter : CodeExporter
  {
    public override string FileDialogTitle => "Export Quest Source Code (rooms only)";

    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
    {
      new KeyValuePair<string, string>("Quest Source Files", ".aslx"),
      new KeyValuePair<string, string>("Text Files", ".txt")
    };

    protected override IEnumerable<string> ReservedWords => new[] {"object", "game", "turnscript"};

    protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history)
    {
    }

    protected override void ExportContent(TextWriter writer)
    {
      foreach (var location in LocationsInExportOrder)
      {
        writer.WriteLine("  <object name=\"{0}\">", location.ExportName);
        writer.WriteLine("    <inherit name=\"editor_room\" />");
        writer.WriteLine("    <alias>{0}</alias>", location.Room.Name);
        if (location.Room.IsDark)
        {
          writer.WriteLine("    <dark />");
        }
        writer.WriteLine("    <attr name=\"grid_width\" type=\"int\">{0}</attr>", location.Room.Width / 32);
        writer.WriteLine("    <attr name=\"grid_length\" type=\"int\">{0}</attr>", location.Room.Height / 32);
        writer.WriteLine("    <attr name=\"grid_fill\">{0}</attr>", System.Drawing.ColorTranslator.ToHtml(location.Room.RoomFillColor));
        writer.WriteLine("    <attr name=\"grid_border\">{0}</attr>", System.Drawing.ColorTranslator.ToHtml(location.Room.RoomBorderColor));
        writer.WriteLine("    <attr name=\"implementation_notes\">{0}</attr>", location.Room.GetToolTipText());
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

        if (location.Room.IsStartRoom)
        {
          writer.WriteLine("    <object name=\"player\">");
          writer.WriteLine("      <inherit name=\"editor_object\" />");
          writer.WriteLine("      <inherit name=\"editor_player\" />");
          writer.WriteLine("    </object>");
        }
        foreach (var thing in location.Things) {
          writer.WriteLine("    <object name=\"{0}\">", thing.ExportName);
          writer.WriteLine("      <inherit name=\"editor_object\" />");
          if (thing.isScenery)
          {
            writer.WriteLine("      <scenery />");
          }
          if (thing.isContainer)
          {
            writer.WriteLine("      <feature_container />");
            writer.WriteLine("      <inherit name=\"container_closed\" />");
          }
          if (thing.forceplural == Thing.Amounts.plural)
          {
            writer.WriteLine("      <inherit name=\"plural\" />");
          }
          if (thing.isPerson)
          {
            if (thing.gender == Thing.ThingGender.female)
            {
              if (thing.properNamed)
              {
                writer.WriteLine("      <inherit name=\"namedfemale\" />");
              }
              else
              {
                writer.WriteLine("      <inherit name=\"female\" />");
              }
            }
            else if (thing.gender == Thing.ThingGender.male)
                        {
              if (thing.properNamed)
              {
                writer.WriteLine("      <inherit name=\"namedmale\" />");
              }
              else
              {
                writer.WriteLine("      <inherit name=\"male\" />");
              }
            }
          }
          writer.WriteLine("      <alias>{0}</alias>", thing.DisplayName);
          writer.WriteLine("    </object>");
        }
        
        // TODO need to add player if here
        writer.WriteLine("  </object>");
        writer.WriteLine();

      }

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

    protected override string GetExportName(Room room, int? suffix) {
      return getExportName(room.Name, suffix);
    }

    protected override string GetExportName(string displayName, int? suffix) {
      return getExportName(displayName, suffix);
    }

    private static string getExportName(string displayName, int? suffix) {
      var name = stripOddCharacters(displayName);
      if (string.IsNullOrEmpty(name)) {
        name = "item";
      }

      if (suffix != null) {
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


  internal class QuestExporter : QuestRoomsExporter
    {
    public override string FileDialogTitle => "Export Quest Source Code";

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
      writer.WriteLine("    <author>{0}</author>", author);
      writer.WriteLine("    <description>{0}</description>", description);
      writer.WriteLine("  </game>");
      writer.WriteLine();
    }

    protected override void ExportContent(TextWriter writer)
    {
      base.ExportContent(writer);

      writer.WriteLine("</asl>");
      writer.WriteLine();
    }


  }
}