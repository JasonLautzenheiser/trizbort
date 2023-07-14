using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Trizbort.Automap;
using Trizbort.Domain;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Export.Domain;

namespace Trizbort.Export.Languages {
  internal class TXTExporter : CodeExporter {
    static class IdCounter {
      public static int Door;
    };

    public override List<KeyValuePair<string, string>> FileDialogFilters => new List<KeyValuePair<string, string>>
    {
            new KeyValuePair<string, string>("TXT Source Files", ".dat"),
            new KeyValuePair<string, string>("Text Files", ".txt")
        };

    public override string FileDialogTitle => "Export TXT Source Code";

    protected override IEnumerable<string> ReservedWords => new[] { "#roomId", "#objectId", "#description", "#name", "#note" };

    private void writeDoor(TextWriter writer, string strFrom, string strDirection, string strTo, Exit exit) {
      writer.WriteLine("{");
      var tmpObjId = "door_" + IdCounter.Door;
      writer.WriteLine("#objectId=obj_{0};", tmpObjId);
      writer.WriteLine("#preset=preset_decoration;");
      writer.WriteLine("\t#location={0};", strFrom);
      writer.WriteLine("\t#name=dvere;");
      writer.WriteLine("\t#door={0},#{1},{2};", strFrom, strDirection, strTo);

      if (exit.Door.Openable) {
        writer.WriteLine("\t\t#isOpen={0};", exit.Door.Open ? "#true" : "#false");
        writer.WriteLine("\t\t\t#open={};");
        writer.WriteLine("\t\t\t#close={};");
      }

      if (exit.Door.Lockable) {
        writer.WriteLine("\t\t#isLocked={0};", exit.Door.Locked ? "#true" : "#false");
        writer.WriteLine("\t\t\t#lock={};");
        writer.WriteLine("\t\t\t#unlock={};");
        writer.WriteLine("\t\t\t#key=#null;");
      }
      writer.WriteLine("}");
      writer.WriteLine();

      exit.Exported = true;
      IdCounter.Door++;
    }

    protected override void ExportContent(TextWriter writer) {
      IdCounter.Door = 1;

      foreach (var location in LocationsInExportOrder) {
        writer.WriteLine("#note ---------- ---------- ---------- ---------- ----------;");
        //Room
        writer.WriteLine();
        writer.WriteLine("{");
        var tmpRoomId = convertToId(location.Room.Name);
        writer.WriteLine("#roomId=rm_{0};", tmpRoomId);

        if (location.Room.IsDark)
          writer.WriteLine("#preset=preset_dark_room;");
        else
          writer.WriteLine("#preset=preset_room;");

        writer.WriteLine("\t#name={0};", convertNewline(location.Room.Name));
        if (location.Room.SubTitle.Length > 0) writer.WriteLine("\t#note {0};", location.Room.SubTitle);
        writer.WriteLine();
        if (!string.IsNullOrEmpty(location.Room.PrimaryDescription)) writer.WriteLine("\t#description={0};", convertNewline(location.Room.PrimaryDescription));

        foreach (var direction in Directions.AllDirections) {
          var exit = location.GetBestExit(direction);
          if (exit != null) {
            string tmp = DirectionToTXTPropertyName(direction);
            if (tmp == "unknownPath") {
              Debug.Assert(false, "Unknown path.");
              writer.WriteLine();
              writer.WriteLine("#note BUG: ERROR - unknown path;");
            }

            bool export = true;
            if (exit.Door != null && exit.Door.Openable && exit.Door.Open == false) {
              export = false;
            }
            if (export) {
              writer.WriteLine("\t#{0}=rm_{1};", tmp, exit.Target.ExportName);
            }

            if (tmp == "unknownPath") {
              writer.WriteLine();
            }
          }
        }
        writer.WriteLine("}");
        writer.WriteLine();

        //Doors
        foreach (var direction in Directions.AllDirections) {
          var exit = location.GetBestExit(direction);
          if (exit != null && exit.Door != null && exit.Exported == false) {
            string strDirection = DirectionToTXTPropertyName(direction);
            if (strDirection == "unknownPath") {
              Debug.Assert(false, "Unknown path.");
              writer.WriteLine("#note BUG: ERROR - unknown path;");
            }
            string strTo = "rm_" + exit.Target.ExportName;
            string strFrom = "rm_" + location.ExportName;
            writeDoor(writer, strFrom, strDirection, strTo, exit);
          }
        }

        //Objects
        string backupLocation = "";
        foreach (var thing in location.Things) {
          writer.WriteLine("{");

          string keyName = "";
          string valueDescription = "";
          SplitKeyValue(thing.DisplayName, ref keyName, ref valueDescription);

          var tmpObjId = convertToId(keyName);
          writer.WriteLine("#objectId=obj_{0};", tmpObjId);
          if (thing.IsPerson == true) {
            writer.WriteLine("#preset=preset_person;");
          }
          else if (thing.IsScenery == true) {
            writer.WriteLine("#preset=preset_decoration;");
          }
          else {
            writer.WriteLine("#preset=preset_object;");
          }

          writer.WriteLine("\t#name={0};", convertNewline(keyName));
          writer.WriteLine("\t#description={0};", convertNewline(valueDescription));

          if (thing.IsContainer == true) {
            writer.WriteLine("\t#isContainer=#true;");
          }

          if (thing.PartOf == true || thing.Worn == true) {
            if (backupLocation != "")
              writer.WriteLine("\t#location={0};", backupLocation);
            else {
              Debug.Assert(false, "Unrecognised location.");
              writer.WriteLine();
              writer.WriteLine("#note BUG: ERROR - unrecognised location;");
              writer.WriteLine("#location=;");
              writer.WriteLine();
            }
          }
          else {
            writer.WriteLine("\t#location=rm_{0};", tmpRoomId);
          }

          writer.WriteLine("}");
          writer.WriteLine();

          backupLocation = tmpObjId;
        }
      }

      writer.WriteLine();
    }

    protected override StreamWriter Create(string fileName) {
      return new StreamWriter(fileName, false, Encoding.UTF8, 2 ^ 16);
    }

    protected override void ExportHeader(TextWriter writer, string title, string author, string description, string history) {

      bool descriptionIsPreset = false;
      if (description.Length > 0) {
        DialogResult result = MessageBox.Show("Description is defined, should it be used as default presets?", "Presets...", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
        if (result == DialogResult.Yes) {
          descriptionIsPreset = true;
        }
      }

      writer.WriteLine("#launcher=TXT;");
      writer.WriteLine("#version=current;");

      writer.WriteLine();
      writer.WriteLine("{");
      writer.WriteLine("#gameSetup;");
      writer.WriteLine("\t#appName={0};", title);
      writer.WriteLine("\t#author={0};", author);
      writer.WriteLine("\t#appVersion=1;");

      foreach (var location in LocationsInExportOrder) {
        if (location.Room.IsStartRoom) {
          var tmpYourId = convertToId(location.Room.Name);
          writer.WriteLine("\t#yourPosition=rm_{0};", tmpYourId);
        }
      }
      writer.WriteLine("}");
      writer.WriteLine();

      writer.WriteLine("#note default data;");
      writer.WriteLine("#note ---------- ---------- ---------- ---------- ----------;");
      writer.WriteLine();

      if (descriptionIsPreset) {
        writer.WriteLine(description);
      }
      else {
        writer.WriteLine("{");
        writer.WriteLine("#roomId=preset_room;");
        writer.WriteLine("\t#name=PRESET Room;");
        writer.WriteLine("\t$dark=#false;");
        writer.WriteLine("}");
        writer.WriteLine();

        writer.WriteLine("{");
        writer.WriteLine("#roomId=preset_dark_room;");
        writer.WriteLine("\t#name=PRESET Dark Room;");
        writer.WriteLine("\t$dark=#true;");
        writer.WriteLine("}");
        writer.WriteLine();

        writer.WriteLine("{");
        writer.WriteLine("#objectId=preset_decoration;");
        writer.WriteLine("\t#name=PRESET Decoration;");
        writer.WriteLine("\t#nonPortable=#true;");
        writer.WriteLine("\t#showAsSeparateObject=#false;");
        writer.WriteLine("\t#visibility=#true;");
        writer.WriteLine("\t#found=#true;");
        writer.WriteLine("}");
        writer.WriteLine();

        writer.WriteLine("{");
        writer.WriteLine("#objectId=preset_decoration_not_found;");
        writer.WriteLine("#preset=preset_decoration;");
        writer.WriteLine("\t#name=PRESET Decoration Not Found;");
        writer.WriteLine("\t#found=#false;");
        writer.WriteLine("}");
        writer.WriteLine();

        writer.WriteLine("{");
        writer.WriteLine("#objectId=preset_object;");
        writer.WriteLine("\t#name=PRESET Object;");
        writer.WriteLine("\t#nonPortable=#false;");
        writer.WriteLine("\t#showAsSeparateObject=#true;");
        writer.WriteLine("\t#visibility=#true;");
        writer.WriteLine("\t#found=#true;");
        writer.WriteLine("}");
        writer.WriteLine();

        writer.WriteLine("{");
        writer.WriteLine("#objectId=preset_object_not_found;");
        writer.WriteLine("#preset=preset_object;");
        writer.WriteLine("\t#name=PRESET Object Not Found;");
        writer.WriteLine("\t#found=#false;");
        writer.WriteLine("}");
        writer.WriteLine();

        writer.WriteLine("{");
        writer.WriteLine("#objectId=preset_person;");
        writer.WriteLine("\t#name=PRESET Person;");
        writer.WriteLine("\t#nonPortable=#true;");
        writer.WriteLine("\t#showAsSeparateObject=#true;");
        writer.WriteLine("\t#visibility=#true;");
        writer.WriteLine("\t#found=#true;");
        writer.WriteLine("\t#isContainer=#true;");
        writer.WriteLine("}");
        writer.WriteLine();

        writer.WriteLine("{");
        writer.WriteLine("#objectId=preset_person_not_found;");
        writer.WriteLine("#preset=preset_person;");
        writer.WriteLine("\t#name=PRESET Person Not Found;");
        writer.WriteLine("\t#found=#false;");
        writer.WriteLine("}");
        writer.WriteLine();
      }
      writer.WriteLine("#note {0};", history);
      writer.WriteLine();
    }

    protected override string GetExportName(Room room, int? suffix) {
      return getExportName(room.Name, suffix);
    }

    protected override string GetExportName(string displayName, int? suffix) {
      return getExportName(displayName, suffix);
    }

    private static bool SplitKeyValue(string text, ref string key, ref string value) {
      if (text.Contains("=") == false) {
        key = text;
        value = "";
        return false;
      }

      var buffer = text.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
      key = buffer[0];
      value = buffer[1];

      return true;
    }

    private static string getExportName(string displayName, int? suffix) {
      var name = convertToId(displayName);
      if (string.IsNullOrEmpty(name)) name = "item";

      if (suffix != null) name = $"{name}{suffix}";

      return name;
    }

    private static string convertToId(string text, params char[] exclude) {
      var exclusions = new List<char>(exclude);
      if (string.IsNullOrEmpty(text)) return string.Empty;
      var result = string.Empty;
      foreach (var c in text) {
        if (c != ';' && c != ' ' && c != '\t' && c != ' ' && c != '\n')
          result += c;
        else
          result += '_';
      }
      return result;
    }

    private static string convertNewline(string text) {
      if (string.IsNullOrEmpty(text)) return string.Empty;
      var result = string.Empty;
      foreach (var c in text) {
        if (c == '\n')
          result += "<br>";
        else if (c == '\r')
          ;
        else
          result += c;
      }
      return result;
    }

    private static string DirectionToTXTPropertyName(MappableDirection direction) {
      switch (direction) {
        case MappableDirection.North:
          return "north";
        case MappableDirection.South:
          return "south";
        case MappableDirection.East:
          return "east";
        case MappableDirection.West:
          return "west";
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
          return "unknownPath";
      }
    }
  }
}