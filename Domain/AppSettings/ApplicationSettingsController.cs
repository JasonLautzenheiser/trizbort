using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using Trizbort.Automap;
using Trizbort.Domain.Misc;
using Trizbort.UI;
using Trizbort.Util;
using Formatting = Newtonsoft.Json.Formatting;

namespace Trizbort.Domain.AppSettings {
  public static class ApplicationSettingsController {
    public const int RECENT_PROJECTS_MAX_COUNT = 4;
    private const string AppSettingsFileName = @".\appsettings.json";
    private static readonly string legacyAppSettingsPath = Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Genstein"), "Trizbort"), "Settings.xml");

    private static ApplicationSettings settings;

    public static ApplicationSettings AppSettings => settings;

    static ApplicationSettingsController() {
      settings = new ApplicationSettings();
      LoadSettings();
    }

    public static void LoadSettings() {
      // if app settings don't exist, create a default one
      if (!File.Exists(AppSettingsFileName)) {
        if (File.Exists(legacyAppSettingsPath)) {
          loadLegacyAppSettings();
        } else {
          ResetSettings();
        }
        SaveSettings();
      }
      else {
        settings = JsonConvert.DeserializeObject<ApplicationSettings>(File.ReadAllText(AppSettingsFileName));
      }
    }

    public static void ResetSettings() {
      settings.DontCareAboutVersion = new Version(0, 0, 0, 0);
      settings.Automap = AutomapSettings.Default;
      settings.InfiniteScrollBounds = false;
      settings.ShowMiniMap = true;
      settings.SaveAt100 = true;
      settings.SaveToImage = true;
      settings.SaveToPDF = true;
      settings.SaveTadstoAdv3Lite = true;
      settings.RecentProjects.Clear();
      settings.ShowToolTipsOnObjects = true;
    }

    public static void SaveSettings() {
      var serializeObject = JsonConvert.SerializeObject(settings,Formatting.Indented);
      File.WriteAllText(AppSettingsFileName, serializeObject);
    }

    public static void ShowAppDialog() {
      using (var dialog = new AppSettingsDialog()) {
        dialog.InvertMouseWheel = settings.InvertMouseWheel;
        dialog.ShowFullPathInTitleBar = settings.ShowFullPathInTitleBar;
        dialog.DefaultFontName = settings.DefaultFontName;
        dialog.DefaultImageType = settings.DefaultImageType;
        dialog.PortAdjustDetail = settings.PortAdjustDetail;
        dialog.SaveToImage = settings.SaveToImage;
        dialog.SaveToPDF = settings.SaveToPDF;
        dialog.SaveTADSToADV3Lite = settings.SaveTadstoAdv3Lite;
        dialog.SaveAt100 = settings.SaveAt100;
        dialog.SpecifyGenMargins = settings.SpecifyGenMargins;
        dialog.GenHorizontalMargin = settings.GenHorizontalMargin;
        dialog.GenVerticalMargin = settings.GenVerticalMargin;
        dialog.LoadLastProjectOnStart = settings.LoadLastProjectOnStart;
        dialog.HandDrawnGlobal = settings.HandDrawnGlobal;
        dialog.ShowToolTipsOnObjects = settings.ShowToolTipsOnObjects;

        if (dialog.ShowDialog() == DialogResult.OK) {
          settings.InvertMouseWheel = dialog.InvertMouseWheel;
          settings.ShowFullPathInTitleBar = dialog.ShowFullPathInTitleBar;
          settings.DefaultFontName = dialog.DefaultFontName;
          settings.DefaultImageType = dialog.DefaultImageType;
          settings.PortAdjustDetail = dialog.PortAdjustDetail;
          settings.SaveAt100 = dialog.SaveAt100;
          settings.SaveToImage = dialog.SaveToImage;
          settings.SaveToPDF = dialog.SaveToPDF;
          settings.SaveTadstoAdv3Lite = dialog.SaveTADSToADV3Lite;
          settings.SpecifyGenMargins = dialog.SpecifyGenMargins;
          settings.GenHorizontalMargin = (int) dialog.GenHorizontalMargin;
          settings.GenVerticalMargin = (int) dialog.GenVerticalMargin;
          settings.LoadLastProjectOnStart = dialog.LoadLastProjectOnStart;
          settings.HandDrawnGlobal = dialog.HandDrawnGlobal;
          settings.ShowToolTipsOnObjects = dialog.ShowToolTipsOnObjects;
          SaveSettings();
        }
      }
    }

    private static void loadLegacyAppSettings() {
        try
        {
          if (File.Exists(legacyAppSettingsPath))
          {
            var doc = new XmlDocument();
            doc.Load(legacyAppSettingsPath);
            var root = new XmlElementReader(doc.DocumentElement);
            if (root.Name == "settings")
            {
              var versionText = root["dontCareAboutVersion"].Text;
              if (!string.IsNullOrEmpty(versionText))
              {
                settings.DontCareAboutVersion = new Version(versionText);
              }
              settings.InfiniteScrollBounds = root["infiniteScrollBounds"].ToBool(settings.InfiniteScrollBounds);
              settings.ShowMiniMap = root["showMiniMap"].ToBool(settings.ShowMiniMap);

             settings.LoadLastProjectOnStart = root["loadLastProjectOnStart"].ToBool(settings.LoadLastProjectOnStart);
             settings.LastProjectFileName = root["lastProjectFileName"].Text;
             settings.LastExportImageFileName = root["lastExportedImageFileName"].Text;
             settings.LastExportInform7FileName = root["lastExportedInform7FileName"].Text;
             settings.LastExportInform6FileName = root["lastExportedInform6FileName"].Text;
             settings.LastExportTadsFileName = root["lastExportedTadsFileName"].Text;
             settings.LastExportHugoFileName = root["lastExportedHugoFileName"].Text;
             settings.LastExportZilFileName = root["lastExportedZilFileName"].Text;
             settings.LastExportQuestFileName = root["lastExportedQuestFileName"].Text;

              settings.InvertMouseWheel = root["invertMouseWheel"].ToBool(settings.InvertMouseWheel);
              settings.PortAdjustDetail = root["portAdjustDetail"].ToInt(settings.PortAdjustDetail);
              settings.DefaultFontName = root["defaultFontName"].Text;

              if (settings.DefaultFontName.Length == 0) settings.DefaultFontName = "Arial"; // important for compatibility with 1.5.9.3 and before. Otherwise it's set to MS Sans Serif

              settings.DefaultImageType = root["defaultImageType"].ToInt(settings.DefaultImageType);
              settings.SaveToImage = root["saveToImage"].ToBool(settings.SaveToImage);
              settings.SaveToPDF = root["saveToPDF"].ToBool(settings.SaveToPDF);
              settings.SaveTadstoAdv3Lite = root["saveTADSToADV3Lite"].ToBool(settings.SaveTadstoAdv3Lite);
              settings.SaveAt100 = root["saveAt100"].ToBool(settings.SaveAt100);
              settings.SpecifyGenMargins = root["specifyMargins"].ToBool(settings.SpecifyGenMargins);
              settings.GenHorizontalMargin = root["horizontalMargin"].ToInt(settings.GenHorizontalMargin);
              settings.GenVerticalMargin = root["verticalMargin"].ToInt(settings.GenVerticalMargin);
              settings.HandDrawnGlobal = root["handDrawnDefault"].ToBool(settings.HandDrawnGlobal);
              settings.ShowToolTipsOnObjects = root["showToolTipsOnObjects"].ToBool(true);

              settings.CanvasWidth = root["canvasWidth"].ToInt(settings.CanvasWidth);
              settings.CanvasHeight = root["canvasHeight"].ToInt(settings.CanvasHeight);
              if (settings.CanvasWidth == 0) { settings.CanvasWidth = 624; }
              if (settings.CanvasHeight == 0) { settings.CanvasHeight = 450; }

              var recentProjects = root["recentProjects"];
              string fileName;
              var index = 0;
              do
              {
                fileName = recentProjects[$"fileName{index++}"].Text;
                if (!string.IsNullOrEmpty(fileName))
                {
                  settings.RecentProjects.Add(fileName);
                }
              } while (!string.IsNullOrEmpty(fileName));

              var automap = root["automap"];
              var settingsAutomap = settings.Automap;
              settingsAutomap.FileName = automap["transcriptFileName"].ToText(settings.Automap.FileName);
              settingsAutomap.VerboseTranscript = automap["verboseTranscript"].ToBool(settings.Automap.VerboseTranscript);
              settingsAutomap.AssumeRoomsWithSameNameAreSameRoom = automap["assumeRoomsWithSameNameAreSameRoom"].ToBool(settings.Automap.AssumeRoomsWithSameNameAreSameRoom);
              settingsAutomap.GuessExits = automap["guessExits"].ToBool(settings.Automap.GuessExits);
              settingsAutomap.AddObjectCommand = automap["addObjectCommand"].ToText(settings.Automap.AddObjectCommand);
              settingsAutomap.AddRegionCommand = automap["addRegionCommand"].ToText(settings.Automap.AddRegionCommand);
              settings.Automap = settingsAutomap;
            }
          }
        }
        catch (Exception)
        {
          // ignored
        }
      }

    public static void OpenProject(string fileName) {
      AppSettings.LastProjectFileName = fileName;
      if (AppSettings.RecentProjects.Contains(fileName)) {
        AppSettings.RecentProjects.Remove(fileName);
      }
      AppSettings.RecentProjects.Insert(0, fileName);

      if (AppSettings.RecentProjects.Count > ApplicationSettingsController.RECENT_PROJECTS_MAX_COUNT) {
        AppSettings.RecentProjects.RemoveRange(ApplicationSettingsController.RECENT_PROJECTS_MAX_COUNT, AppSettings.RecentProjects.Count - ApplicationSettingsController.RECENT_PROJECTS_MAX_COUNT);
      }
      SaveSettings();
    }
  }
}