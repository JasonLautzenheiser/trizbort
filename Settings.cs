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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Trizbort
{
  public static class Settings
  {
    private static readonly float MinFontSize = 2;
    private static readonly float MaxFontSize = 256;
    // per-map settings, saved with the map
    private static readonly Color[] s_color = new Color[Colors.Count];
    private static List<Region> s_regionColor = new List<Region>();
    private static Font s_largeFont;
    private static Font s_smallFont;
    private static Font s_lineFont;
    private static float s_lineWidth;
    private static bool s_snapToGrid;
    private static bool s_isGridVisible;
    private static bool s_showOrigin;
    private static float s_gridSize;
    private static float s_darknessStripeSize;
    private static float s_objectListOffsetFromRoom;
    private static float s_connectionStalkLength;
    private static float s_preferredDistanceBetweenRooms;
    private static float s_textOffsetFromConnection;
    private static float s_handleSize;
    private static float s_snapToElementSize;
    private static bool s_docSpecificMargins;
    private static float s_docHorizontalMargin;
    private static float s_docVerticalMargin;
    private static float s_dragDistanceToInitiateNewConnection;
    private static float s_connectionArrowSize;
    private static Keys s_keypadNavigationCreationModifier;
    private static Keys s_keypadNavigationUnexploredModifier;
    // application settings, saved for the user
    private static AutomapSettings s_automap;

    static Settings()
    {
      Color = new ColorSettings();
      Regions = new List<Region>
      {
        new Region {RegionName = Region.DefaultRegion, RColor = System.Drawing.Color.White}
      };
      RecentProjects = new MruList();
      ResetApplicationSettings();
      LoadApplicationSettings();
      ResetProjectSettings();
    }

    private static string ApplicationSettingsPath
    {
      get { return Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Genstein"), "Trizbort"), "Settings.xml"); }
    }

    public static ColorSettings Color { get; private set; }
    public static List<Region> Regions { get; private set; }

    public static string DefaultRoomName { get; set; } = "Cave";

    public static Font LargeFont
    {
      get { return s_largeFont; }
      set
      {
        if (s_largeFont != value)
        {
          s_largeFont = value;
          RaiseChanged();
        }
      }
    }

    public static Font SmallFont
    {
      get { return s_smallFont; }
      set
      {
        if (s_smallFont != value)
        {
          s_smallFont = value;
          RaiseChanged();
        }
      }
    }

    public static Font LineFont
    {
      get { return s_lineFont; }
      set
      {
        if (s_lineFont != value)
        {
          s_lineFont = value;
          RaiseChanged();
        }
      }
    }

    public static bool HandDrawn
    {
      get { return HandDrawnUnchecked; }
      set
      {
        if (HandDrawnUnchecked != value)
        {
          HandDrawnUnchecked = value;
          RaiseChanged();
        }
      }
    }

    public static bool HandDrawnUnchecked { get; set; }

    public static float LineWidth
    {
      get { return s_lineWidth; }
      set
      {
        if (s_lineWidth != value)
        {
          s_lineWidth = value;
          RaiseChanged();
        }
      }
    }

    public static bool SnapToGrid
    {
      get { return s_snapToGrid; }
      set
      {
        if (s_snapToGrid != value)
        {
          s_snapToGrid = value;
          RaiseChanged();
        }
      }
    }

    public static float GridSize
    {
      get { return s_gridSize; }
      set
      {
        if (s_gridSize != value)
        {
          s_gridSize = value;
          RaiseChanged();
        }
      }
    }

    public static bool IsGridVisible
    {
      get { return s_isGridVisible; }
      set
      {
        if (s_isGridVisible != value)
        {
          s_isGridVisible = value;
          RaiseChanged();
        }
      }
    }

    public static bool ShowOrigin
    {
      get { return s_showOrigin; }
      set
      {
        if (s_showOrigin != value)
        {
          s_showOrigin = value;
          RaiseChanged();
        }
      }
    }

    public static float DarknessStripeSize
    {
      get { return s_darknessStripeSize; }
      set
      {
        if (s_darknessStripeSize != value)
        {
          s_darknessStripeSize = value;
          RaiseChanged();
        }
      }
    }

    public static float ObjectListOffsetFromRoom
    {
      get { return s_objectListOffsetFromRoom; }
      set
      {
        if (s_objectListOffsetFromRoom != value)
        {
          s_objectListOffsetFromRoom = value;
          RaiseChanged();
        }
      }
    }

    public static float HandleSize
    {
      get { return s_handleSize; }
      set
      {
        if (s_handleSize != value)
        {
          s_handleSize = value;
          RaiseChanged();
        }
      }
    }

    public static float SnapToElementSize
    {
      get { return s_snapToElementSize; }
      set
      {
        if (s_snapToElementSize != value)
        {
          s_snapToElementSize = value;
          RaiseChanged();
        }
      }
    }

    public static bool DocumentSpecificMargins
    {
      get { return s_docSpecificMargins; }
      set
      {
        if (s_docSpecificMargins != value)
        {
          s_docSpecificMargins = value;
          RaiseChanged();
        }
      }
    }

    public static float DocHorizontalMargin
    {
      get { return s_docHorizontalMargin; }
      set
      {
        if (s_docHorizontalMargin != value)
        {
          s_docHorizontalMargin = value;
          RaiseChanged();
        }
      }
    }

    public static float DocVerticalMargin
    {
      get { return s_docVerticalMargin; }
      set
      {
        if (s_docVerticalMargin != value)
        {
          s_docVerticalMargin = value;
          RaiseChanged();
        }
      }
    }

    public static float DragDistanceToInitiateNewConnection
    {
      get { return s_dragDistanceToInitiateNewConnection; }
      set
      {
        if (s_dragDistanceToInitiateNewConnection != value)
        {
          s_dragDistanceToInitiateNewConnection = value;
          RaiseChanged();
        }
      }
    }

    public static float ConnectionStalkLength
    {
      get { return s_connectionStalkLength; }
      set
      {
        if (s_connectionStalkLength != value)
        {
          s_connectionStalkLength = value;
          RaiseChanged();
        }
      }
    }

    public static float PreferredDistanceBetweenRooms
    {
      get { return s_preferredDistanceBetweenRooms; }
      set
      {
        if (s_preferredDistanceBetweenRooms != value)
        {
          s_preferredDistanceBetweenRooms = value;
          RaiseChanged();
        }
      }
    }

    public static float TextOffsetFromConnection
    {
      get { return s_textOffsetFromConnection; }
      set
      {
        if (s_textOffsetFromConnection != value)
        {
          s_textOffsetFromConnection = value;
          RaiseChanged();
        }
      }
    }

    public static float ConnectionArrowSize
    {
      get { return s_connectionArrowSize; }
      set
      {
        if (s_connectionArrowSize != value)
        {
          s_connectionArrowSize = value;
          RaiseChanged();
        }
      }
    }

    /// <summary>
    ///   Get/set the modifier keys required, along with a numeric keypad key,
    ///   to create new rooms from the currently selected room.
    /// </summary>
    public static Keys KeypadNavigationCreationModifier
    {
      get { return s_keypadNavigationCreationModifier; }
      set
      {
        if (s_keypadNavigationCreationModifier != value)
        {
          s_keypadNavigationCreationModifier = value;
          RaiseChanged();
        }
      }
    }

    /// <summary>
    ///   Get/set the modifier keys required, along with a numeric keypad key,
    ///   to mark "unexplored" connections from the currently selected room.
    /// </summary>
    public static Keys KeypadNavigationUnexploredModifier
    {
      get { return s_keypadNavigationUnexploredModifier; }
      set
      {
        if (s_keypadNavigationUnexploredModifier != value)
        {
          s_keypadNavigationUnexploredModifier = value;
          RaiseChanged();
        }
      }
    }

    public static bool startRoomLoaded { get; set; }

    public static bool DebugShowFPS { get; set; }
    public static bool DebugShowMouseCoordinates { get; set; }
    public static bool DebugDisableTextRendering { get; set; }
    public static bool DebugDisableLineRendering { get; set; }
    public static bool DebugDisableElementRendering { get; set; }
    public static bool DebugDisableGridPolyline { get; set; }
    public static bool SaveAt100 { get; set; }
    public static int MouseDragButton { get; set; }

    public static bool SaveToPDF { get; set; }
    public static bool SaveToImage { get; set; }
    public static bool SaveTADSToADV3Lite { get; set; }
    public static bool SpecifyGenMargins { get; set; }
    public static int GenHorizontalMargin { get; set; }
    public static int GenVerticalMargin { get; set; }
    public static int CanvasWidth { get; set; }
    public static int CanvasHeight { get; set; }
    public static int PortAdjustDetail { get; set; }
    public static int DefaultImageType { get; set; }
    public static string DefaultFontName { get; set; }
    public static bool InvertMouseWheel { get; set; }
    public static Version DontCareAboutVersion { get; set; }

    public static AutomapSettings Automap
    {
      get { return s_automap; }
      set { s_automap = value; }
    }

    public static bool InfiniteScrollBounds { get; set; }
    public static bool ShowMiniMap { get; set; }
    public static bool LoadLastProjectOnStart { get; set; }
    public static string LastProjectFileName { get; set; }
    public static string LastExportImageFileName { get; set; }
    public static string LastExportInform7FileName { get; set; }
    public static string LastExportInform6FileName { get; set; }
    public static string LastExportTadsFileName { get; set; }
    public static string LastExportHugoFileName { get; set; }
    public static MruList RecentProjects { get; private set; }

    private static void ResetApplicationSettings()
    {
      DontCareAboutVersion = new Version(0, 0, 0, 0);
      s_automap = AutomapSettings.Default;
      InfiniteScrollBounds = false;
      ShowMiniMap = true;
      SaveAt100 = true;
      SaveToImage = true;
      SaveToPDF = true;
      SaveTADSToADV3Lite = true;
      RecentProjects.Clear();
      // TODO: add other application settings here
    }

    private static void LoadApplicationSettings()
    {
      try
      {
        if (File.Exists(ApplicationSettingsPath))
        {
          var doc = new XmlDocument();
          doc.Load(ApplicationSettingsPath);
          var root = new XmlElementReader(doc.DocumentElement);
          if (root.Name == "settings")
          {
            var versionText = root["dontCareAboutVersion"].Text;
            if (!string.IsNullOrEmpty(versionText))
            {
              DontCareAboutVersion = new Version(versionText);
            }
            InfiniteScrollBounds = root["infiniteScrollBounds"].ToBool(InfiniteScrollBounds);
            ShowMiniMap = root["showMiniMap"].ToBool(ShowMiniMap);

            LoadLastProjectOnStart = root["loadLastProjectOnStart"].ToBool(LoadLastProjectOnStart);
            LastProjectFileName = root["lastProjectFileName"].Text;
            LastExportImageFileName = root["lastExportedImageFileName"].Text;
            LastExportInform7FileName = root["lastExportedInform7FileName"].Text;
            LastExportInform6FileName = root["lastExportedInform6FileName"].Text;
            LastExportTadsFileName = root["lastExportedTadsFileName"].Text;

            InvertMouseWheel = root["invertMouseWheel"].ToBool(InvertMouseWheel);
            PortAdjustDetail = root["portAdjustDetail"].ToInt(PortAdjustDetail);
            DefaultFontName = root["defaultFontName"].Text;
            DefaultImageType = root["defaultImageType"].ToInt(DefaultImageType);
            SaveToImage = root["saveToImage"].ToBool(SaveToImage);
            SaveToPDF = root["saveToPDF"].ToBool(SaveToPDF);
            SaveTADSToADV3Lite = root["saveTADSToADV3Lite"].ToBool(SaveTADSToADV3Lite);
            SaveAt100 = root["saveAt100"].ToBool(SaveAt100);
            SpecifyGenMargins = root["specifyMargins"].ToBool(SpecifyGenMargins);
            GenHorizontalMargin = root["horizontalMargin"].ToInt(GenHorizontalMargin);
            GenVerticalMargin = root["verticalMargin"].ToInt(GenVerticalMargin);

            CanvasWidth = root["canvasWidth"].ToInt(CanvasWidth);
            CanvasHeight = root["canvasHeight"].ToInt(CanvasHeight);
            if (CanvasWidth == 0) { CanvasWidth = 624; }
            if (CanvasHeight == 0) { CanvasHeight = 450; }

            var recentProjects = root["recentProjects"];
            var fileName = string.Empty;
            var index = 0;
            do
            {
              fileName = recentProjects[string.Format("fileName{0}", index++)].Text;
              if (!string.IsNullOrEmpty(fileName))
              {
                RecentProjects.Append(fileName);
              }
            } while (!string.IsNullOrEmpty(fileName));

            var automap = root["automap"];
            s_automap.FileName = automap["transcriptFileName"].ToText(s_automap.FileName);
            s_automap.VerboseTranscript = automap["verboseTranscript"].ToBool(s_automap.VerboseTranscript);
            s_automap.AssumeRoomsWithSameNameAreSameRoom = automap["assumeRoomsWithSameNameAreSameRoom"].ToBool(s_automap.AssumeRoomsWithSameNameAreSameRoom);
            s_automap.GuessExits = automap["guessExits"].ToBool(s_automap.GuessExits);
            s_automap.AddObjectCommand = automap["addObjectCommand"].ToText(s_automap.AddObjectCommand);
            s_automap.AddRegionCommand = automap["addRegionCommand"].ToText(s_automap.AddRegionCommand);
          }
        }
      }
      catch (Exception)
      {
      }
    }

    public static void SaveApplicationSettings()
    {
      try
      {
        Directory.CreateDirectory(Path.GetDirectoryName(ApplicationSettingsPath));
        using (var scribe = XmlScribe.Create(ApplicationSettingsPath))
        {
          scribe.StartElement("settings");
          scribe.Element("dontCareAboutVersion", DontCareAboutVersion.ToString());
          scribe.Element("infiniteScrollBounds", InfiniteScrollBounds);
          scribe.Element("showMiniMap", ShowMiniMap);
          scribe.Element("invertMouseWheel", InvertMouseWheel);
          scribe.Element("defaultFontName", DefaultFontName);
          scribe.Element("defaultImageType", DefaultImageType);
          scribe.Element("portAdjustDetail", PortAdjustDetail);
          scribe.Element("saveAt100", SaveAt100);
          scribe.Element("saveToPDF", SaveToPDF);
          scribe.Element("saveToImage", SaveToImage);
          scribe.Element("saveTADSToADV3Lite", SaveTADSToADV3Lite);
          scribe.Element("verticalMargin", GenVerticalMargin);
          scribe.Element("horizontalMargin", GenHorizontalMargin);
          scribe.Element("specifyMargins", SpecifyGenMargins);
          scribe.Element("canvasHeight", CanvasHeight);
          scribe.Element("canvasWidth", CanvasWidth);

          scribe.Element("loadLastProjectOnStart", LoadLastProjectOnStart);
          scribe.Element("lastProjectFileName", LastProjectFileName);
          scribe.Element("lastExportedImageFileName", LastExportImageFileName);
          scribe.Element("lastExportedInform7FileName", LastExportInform7FileName);
          scribe.Element("lastExportedInform6FileName", LastExportInform6FileName);
          scribe.Element("lastExportedTadsFileName", LastExportTadsFileName);
          scribe.Element("lastExportedHugoFileName", LastExportHugoFileName);

          scribe.StartElement("recentProjects");
          var index = 0;
          foreach (var fileName in RecentProjects)
          {
            scribe.Element(string.Format("fileName{0}", index++), fileName);
          }
          scribe.EndElement();

          scribe.StartElement("automap");
          scribe.Element("transcriptFileName", s_automap.FileName);
          scribe.Element("verboseTranscript", s_automap.VerboseTranscript);
          scribe.Element("assumeRoomsWithSameNameAreSameRoom", s_automap.AssumeRoomsWithSameNameAreSameRoom);
          scribe.Element("guessExits", s_automap.GuessExits);
          scribe.Element("addObjectCommand", s_automap.AddObjectCommand);
          scribe.Element("addRegionCommand", s_automap.AddRegionCommand);
          scribe.EndElement();
        }
      }
      catch (Exception)
      {
      }
    }

    public static float Snap(float value)
    {
      float offset = 0;
      while (value < GridSize)
      {
        value += GridSize;
        offset += GridSize;
      }

      var mod = value%GridSize;
      if (SnapToGrid && mod != 0)
      {
        if (mod < GridSize/2)
        {
          value -= mod;
        }
        else
        {
          value = value + GridSize - mod;
        }
      }

      return value - offset;
    }

    public static Vector Snap(Vector pos)
    {
      if (SnapToGrid)
      {
        pos.X = Snap(pos.X);
        pos.Y = Snap(pos.Y);
      }
      return pos;
    }

    public static event EventHandler Changed;

    private static void RaiseChanged()
    {
      var changed = Changed;
      if (changed != null)
      {
        changed(null, EventArgs.Empty);
      }
    }

    public static void ShowMapDialog()
    {
      using (var dialog = new SettingsDialog())
      {
        for (var index = 0; index < Colors.Count; ++index)
        {
          dialog.Color[index] = Color[index];
        }

        //below is code for pulling the region names, text color and background color from Settings.Regions.
        //it is set up so that Trizbort can check after we click OK or Cancel, and we can see if anything changed.
        //Currently Trizbort only can check for region names of individual rooms changing.
        //After talking with Jason, We'll need to refactor code to make this run smoother, but this is the best for now.

        var RegCount = Settings.Regions.Count;
        var regNameList = Settings.Regions.Select(p=>p.RegionName).ToList();
        var regTextColorList = Settings.Regions.Select(p=>p.TextColor).ToList();
        var regBkgdColorList = Settings.Regions.Select(p=>p.RColor).ToList();

        dialog.Title = Project.Current.Title;
        dialog.Author = Project.Current.Author;
        dialog.Description = Project.Current.Description;
        dialog.History = Project.Current.History;
        dialog.DefaultRoomName = DefaultRoomName;
        dialog.LargeFont = LargeFont;
        dialog.SmallFont = SmallFont;
        dialog.LineFont = LineFont;
        dialog.HandDrawn = HandDrawn;
        dialog.LineWidth = LineWidth;
        dialog.SnapToGrid = SnapToGrid;
        dialog.GridSize = GridSize;
        dialog.IsGridVisible = IsGridVisible;
        dialog.ShowOrigin = ShowOrigin;
        dialog.DarknessStripeSize = DarknessStripeSize;
        dialog.ObjectListOffsetFromRoom = ObjectListOffsetFromRoom;
        dialog.ConnectionStalkLength = ConnectionStalkLength;
        dialog.PreferredDistanceBetweenRooms = PreferredDistanceBetweenRooms;
        dialog.TextOffsetFromConnection = TextOffsetFromConnection;
        dialog.HandleSize = HandleSize;
        dialog.SnapToElementSize = SnapToElementSize;
        dialog.DocumentSpecificMargins = DocumentSpecificMargins;
        dialog.DocHorizontalMargin = DocHorizontalMargin;
        dialog.DocVerticalMargin = DocVerticalMargin;
        dialog.ConnectionArrowSize = ConnectionArrowSize;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          for (var index = 0; index < Colors.Count; ++index)
          {
            if (Color[index] != dialog.Color[index]) { Project.Current.IsDirty = true; }
            Color[index] = dialog.Color[index];
          }

          if (Project.Current.Title != dialog.Title) { Project.Current.IsDirty = true; }
          Project.Current.Title = dialog.Title;
          if (Project.Current.Author != dialog.Author) { Project.Current.IsDirty = true; }
          Project.Current.Author = dialog.Author;
          if (Project.Current.Description != dialog.Description) { Project.Current.IsDirty = true; }
          Project.Current.Description = dialog.Description;
          if (Project.Current.History != dialog.History) { Project.Current.IsDirty = true; }
          Project.Current.History = dialog.History;
          if (DefaultRoomName != dialog.DefaultRoomName) { Project.Current.IsDirty = true; }
          DefaultRoomName = dialog.DefaultRoomName;
          if (LargeFont != dialog.LargeFont) { Project.Current.IsDirty = true; }
          LargeFont = dialog.LargeFont;
          if (SmallFont != dialog.SmallFont) { Project.Current.IsDirty = true; }
          SmallFont = dialog.SmallFont;
          if (LineFont != dialog.LineFont) { Project.Current.IsDirty = true; }
          LineFont = dialog.LineFont;
          if (HandDrawn != dialog.HandDrawn) { Project.Current.IsDirty = true; }
          HandDrawn = dialog.HandDrawn;
          if (LineWidth != dialog.LineWidth) { Project.Current.IsDirty = true; };
          LineWidth = dialog.LineWidth;
          if (SnapToGrid != dialog.SnapToGrid) { Project.Current.IsDirty = true; };
          SnapToGrid = dialog.SnapToGrid;
          if (GridSize != dialog.GridSize) { Project.Current.IsDirty = true; }
          GridSize = dialog.GridSize;
          if (IsGridVisible != dialog.IsGridVisible) { Project.Current.IsDirty = true; };
          IsGridVisible = dialog.IsGridVisible;
          if (ShowOrigin != dialog.ShowOrigin) { Project.Current.IsDirty = true; };
          ShowOrigin = dialog.ShowOrigin;
          if (DarknessStripeSize != dialog.DarknessStripeSize) { Project.Current.IsDirty = true; };
          DarknessStripeSize = dialog.DarknessStripeSize;
          if (ObjectListOffsetFromRoom != dialog.ObjectListOffsetFromRoom) { Project.Current.IsDirty = true; };
          ObjectListOffsetFromRoom = dialog.ObjectListOffsetFromRoom;
          if (ConnectionStalkLength != dialog.ConnectionStalkLength) { Project.Current.IsDirty = true; };
          ConnectionStalkLength = dialog.ConnectionStalkLength;
          if (PreferredDistanceBetweenRooms != dialog.PreferredDistanceBetweenRooms) { Project.Current.IsDirty = true; };
          PreferredDistanceBetweenRooms = dialog.PreferredDistanceBetweenRooms;
          if (TextOffsetFromConnection != dialog.TextOffsetFromConnection) { Project.Current.IsDirty = true; };
          TextOffsetFromConnection = dialog.TextOffsetFromConnection;
          if (HandleSize != dialog.HandleSize) { Project.Current.IsDirty = true; };
          HandleSize = dialog.HandleSize;
          if (SnapToElementSize != dialog.SnapToElementSize) { Project.Current.IsDirty = true; };
          SnapToElementSize = dialog.SnapToElementSize;
          if (ConnectionArrowSize != dialog.ConnectionArrowSize) { Project.Current.IsDirty = true; };
          ConnectionArrowSize = dialog.ConnectionArrowSize;
          if (DocumentSpecificMargins != dialog.DocumentSpecificMargins) { Project.Current.IsDirty = true; };
          DocumentSpecificMargins = dialog.DocumentSpecificMargins;
          if (DocHorizontalMargin != dialog.DocHorizontalMargin) { Project.Current.IsDirty = true; };
          DocHorizontalMargin = dialog.DocHorizontalMargin;
          if (DocVerticalMargin != dialog.DocVerticalMargin) { Project.Current.IsDirty = true; };
          DocVerticalMargin = dialog.DocVerticalMargin;
        }

        //Note this needs to be done outside of the "if OK button is clicked" loop for now.
        //Trizbort makes changes to regions immediately. So we can change a region and hit cancel.
        //We need to account for that.
        var newRegCount = Settings.Regions.Count;
        var newReg = Settings.Regions.ToArray();
        if (newRegCount != RegCount)
        {
            Project.Current.IsDirty = true;
        }
        else
        {
            for (var index = 0; index < newRegCount; index++)
            {
                if (regBkgdColorList[index] != newReg[index].RColor)
                    Project.Current.IsDirty = true;
                if (regTextColorList[index] != newReg[index].TextColor)
                    Project.Current.IsDirty = true;
                if (regNameList[index] != newReg[index].RegionName)
                    Project.Current.IsDirty = true;
            }
        }

      }
    }

    public static void ResetProjectSettings()
    {
      Color[Colors.Canvas] = System.Drawing.Color.White;
      Color[Colors.Fill] = System.Drawing.Color.White;
      Color[Colors.Border] = System.Drawing.Color.MidnightBlue;
      Color[Colors.Line] = System.Drawing.Color.MidnightBlue;
      Color[Colors.HoverLine] = System.Drawing.Color.DarkOrange;
      Color[Colors.SelectedLine] = System.Drawing.Color.Gold;
      Color[Colors.LargeText] = System.Drawing.Color.MidnightBlue;
      Color[Colors.SmallText] = System.Drawing.Color.MidnightBlue;
      Color[Colors.LineText] = System.Drawing.Color.MidnightBlue;
      Color[Colors.Grid] = Drawing.Mix(System.Drawing.Color.White, System.Drawing.Color.Black, 25, 1);
      Color[Colors.StartRoom] = System.Drawing.Color.GreenYellow;

      LargeFont = new Font(DefaultFontName, 13.0f, FontStyle.Regular, GraphicsUnit.World);
      SmallFont = new Font(DefaultFontName, 11.0f, FontStyle.Regular, GraphicsUnit.World);
      LineFont = new Font(DefaultFontName, 9.0f, FontStyle.Regular, GraphicsUnit.World);

      Settings.DocumentSpecificMargins = Settings.SpecifyGenMargins;

      if (Settings.SpecifyGenMargins)
      {
        Settings.DocHorizontalMargin = Settings.GenHorizontalMargin;
        Settings.DocVerticalMargin = Settings.GenVerticalMargin;
      }
      else
      {
        Settings.DocHorizontalMargin = Settings.DocVerticalMargin = 0;
      }

      LineWidth = 2.0f;
      HandDrawn = true;

      SnapToGrid = true;
      IsGridVisible = true;
      GridSize = 32.0f;
      ShowOrigin = true;

      startRoomLoaded = false;

      DarknessStripeSize = 24.0f;
      ObjectListOffsetFromRoom = 4.0f;

      ConnectionStalkLength = 32.0f;
      PreferredDistanceBetweenRooms = ConnectionStalkLength*2;
      TextOffsetFromConnection = 4.0f;
      HandleSize = 12.0f;
      SnapToElementSize = 16.0f;
      DragDistanceToInitiateNewConnection = 32f;
      ConnectionArrowSize = 12.0f;

      KeypadNavigationCreationModifier = Keys.Control;
      KeypadNavigationUnexploredModifier = Keys.Alt;

      Regions = new List<Region>
      {
        new Region {RegionName = Region.DefaultRegion, RColor = System.Drawing.Color.White}
      };

    }

    public static void Save(XmlScribe scribe)
    {
      // save colors
      scribe.StartElement("colors");
      for (var index = 0; index < Colors.Count; ++index)
      {
        string colorName;
        if (Colors.ToName(index, out colorName))
        {
          scribe.Element(colorName, Color[index]);
        }
      }
      scribe.EndElement();

      scribe.StartElement("regions");
      foreach (var region in Regions.OrderBy(p=>p.RegionName))
      {
        scribe.StartElement(region.FixupRegionNameForSave());
        scribe.Attribute("Name", region.RegionName);
        scribe.Attribute("TextColor", region.TextColor);
        scribe.Value(region.RColor);
        scribe.EndElement();
      }
      scribe.EndElement();



      // save fonts
      scribe.StartElement("fonts");
      SaveFont(scribe, s_largeFont, "room");
      SaveFont(scribe, s_smallFont, "object");
      SaveFont(scribe, s_lineFont, "line");
      scribe.EndElement();

      scribe.StartElement("grid");
      scribe.Element("snapTo", s_snapToGrid);
      scribe.Element("visible", s_isGridVisible);
      scribe.Element("showOrigin", s_showOrigin);
      scribe.Element("size", s_gridSize);
      scribe.EndElement();

      scribe.StartElement("lines");
      scribe.Element("width", s_lineWidth);
      scribe.Element("handDrawn", HandDrawnUnchecked);
      scribe.Element("arrowSize", s_connectionArrowSize);
      scribe.Element("textOffset", s_textOffsetFromConnection);
      scribe.EndElement();

      scribe.StartElement("rooms");
      scribe.Element("darknessStripeSize", s_darknessStripeSize);
      scribe.Element("objectListOffset", s_objectListOffsetFromRoom);
      scribe.Element("connectionStalkLength", s_connectionStalkLength);
      scribe.Element("preferredDistanceBetweenRooms", s_preferredDistanceBetweenRooms);
      scribe.Element("defaultRoomName", DefaultRoomName);
      scribe.EndElement();

      scribe.StartElement("ui");
      scribe.Element("handleSize", s_handleSize);
      scribe.Element("snapToElementSize", s_snapToElementSize);
      scribe.EndElement();

      scribe.StartElement("margins");
      scribe.Element("documentSpecific", s_docSpecificMargins);
      scribe.Element("horizontal", s_docHorizontalMargin);
      scribe.Element("vertical", s_docVerticalMargin);
      scribe.EndElement();

      scribe.StartElement("keypadNavigation");
      scribe.Element("creationModifier", ModifierKeysToString(s_keypadNavigationCreationModifier));
      scribe.Element("unexploredModifier", ModifierKeysToString(s_keypadNavigationUnexploredModifier));
      scribe.EndElement();

      SaveApplicationSettings();
    }

    private static string ModifierKeysToString(Keys key)
    {
      var builder = new StringBuilder();
      if ((key & Keys.Shift) == Keys.Shift)
      {
        if (builder.Length != 0)
          builder.Append("|");
        builder.Append("shift");
      }
      if ((key & Keys.Control) == Keys.Control)
      {
        if (builder.Length != 0)
          builder.Append("|");
        builder.Append("control");
      }
      if ((key & Keys.Alt) == Keys.Alt)
      {
        if (builder.Length != 0)
          builder.Append("|");
        builder.Append("alt");
      }
      if (builder.Length == 0)
      {
        builder.Append("none");
      }
      return builder.ToString();
    }

    private static Keys StringToModifierKeys(string text, Keys defaultValue)
    {
      if (string.IsNullOrEmpty(text))
      {
        return defaultValue;
      }

      var value = Keys.None;
      foreach (var part in text.Split('|'))
      {
        if (StringComparer.InvariantCultureIgnoreCase.Compare(part, "shift") == 0)
        {
          value |= Keys.Shift;
        }
        else if (StringComparer.InvariantCultureIgnoreCase.Compare(part, "control") == 0)
        {
          value |= Keys.Control;
        }
        else if (StringComparer.InvariantCultureIgnoreCase.Compare(part, "alt") == 0)
        {
          value |= Keys.Alt;
        }
        // Note that "none" is also an allowed value.
      }
      return value;
    }

    private static void SaveFont(XmlScribe scribe, Font font, string name)
    {
      scribe.StartElement(name);
      scribe.Attribute("size", font.Size);
      if ((font.Style & FontStyle.Bold) == FontStyle.Bold)
      {
        scribe.Attribute("bold", true);
      }
      if ((font.Style & FontStyle.Italic) == FontStyle.Italic)
      {
        scribe.Attribute("italic", true);
      }
      if ((font.Style & FontStyle.Underline) == FontStyle.Underline)
      {
        scribe.Attribute("underline", true);
      }
      if ((font.Style & FontStyle.Strikeout) == FontStyle.Strikeout)
      {
        scribe.Attribute("strikeout", true);
      }
      scribe.Value(Drawing.FontName(font));
      scribe.EndElement();
    }

    public static void Load(XmlElementReader element)
    {
      var colors = element["colors"];
      foreach (var color in colors.Children)
      {
        int index;
        if (Colors.FromName(color.Name, out index))
        {
          Color[index] = color.ToColor(Color[index]);
        }
      }

      var regions = element["regions"];
      Regions = new List<Region>();

      if (regions.Children.Count <= 0)
        Regions.Add(new Region {RColor = System.Drawing.Color.White, TextColor = System.Drawing.Color.Blue, RegionName = Region.DefaultRegion});
      else
      {
        foreach (var region in regions.Children)
        {
          var tRegion = new Region();
          tRegion.TextColor = region.Attribute("TextColor").Text == string.Empty ? System.Drawing.Color.Blue : ColorTranslator.FromHtml(region.Attribute("TextColor").Text);

          var node = region.Attribute("name");
          tRegion.RegionName = node != null && node.Text != string.Empty ? node.Text : region.Name;
          
          tRegion.RegionName = tRegion.ClearRegionNameObfuscation();
          tRegion.RColor = region.ToColor(System.Drawing.Color.White);
          Regions.Add(tRegion);
        }
      }
      var fonts = element["fonts"];
      foreach (var font in fonts.Children)
      {
        var style = FontStyle.Regular;
        if (font.Attribute("bold").ToBool())
        {
          style |= FontStyle.Bold;
        }
        if (font.Attribute("italic").ToBool())
        {
          style |= FontStyle.Italic;
        }
        if (font.Attribute("underline").ToBool())
        {
          style |= FontStyle.Underline;
        }
        if (font.Attribute("strikeout").ToBool())
        {
          style |= FontStyle.Strikeout;
        }
        if (font.Name == "room")
        {
          LargeFont = new Font(font.ToText(LargeFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(LargeFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        }
        else if (font.Name == "object")
        {
          SmallFont = new Font(font.ToText(SmallFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(SmallFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        }
        else if (font.Name == "line")
        {
          LineFont = new Font(font.ToText(SmallFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(LineFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        }
      }

      SnapToGrid = element["grid"]["snapTo"].ToBool(s_snapToGrid);
      IsGridVisible = element["grid"]["visible"].ToBool(s_isGridVisible);
      GridSize = element["grid"]["size"].ToFloat(s_gridSize);
      ShowOrigin = element["grid"]["showOrigin"].ToBool(s_showOrigin);

      LineWidth = element["lines"]["width"].ToFloat(s_lineWidth);
      HandDrawn = element["lines"]["handDrawn"].ToBool(HandDrawnUnchecked);
      ConnectionArrowSize = element["lines"]["arrowSize"].ToFloat(s_connectionArrowSize);
      TextOffsetFromConnection = element["lines"]["textOffset"].ToFloat(s_textOffsetFromConnection);

      DarknessStripeSize = element["rooms"]["darknessStripeSize"].ToFloat(s_darknessStripeSize);
      ObjectListOffsetFromRoom = element["rooms"]["objectListOffset"].ToFloat(s_objectListOffsetFromRoom);
      ConnectionStalkLength = element["rooms"]["connectionStalkLength"].ToFloat(s_connectionStalkLength);
      PreferredDistanceBetweenRooms = element["rooms"]["preferredDistanceBetweenRooms"].ToFloat(s_connectionStalkLength*2); // introduced in v1.2, hence default based on existing setting

      DefaultRoomName = element["rooms"]["defaultRoomName"].Text;
      if (string.IsNullOrEmpty(DefaultRoomName)) //Fix for Bug#132: Trizbort needs a non-empty default
          DefaultRoomName = "Cave";

      HandleSize = element["ui"]["handleSize"].ToFloat(s_handleSize);
      SnapToElementSize = element["ui"]["snapToElementSize"].ToFloat(s_snapToElementSize);

      DocumentSpecificMargins = element["margins"]["documentSpecific"].ToBool(s_docSpecificMargins);
      DocHorizontalMargin = element["margins"]["horizontal"].ToFloat(s_docHorizontalMargin);
      DocVerticalMargin = element["margins"]["vertical"].ToFloat(s_docVerticalMargin);

      KeypadNavigationCreationModifier = StringToModifierKeys(element["keypadNavigation"]["creationModifier"].Text, s_keypadNavigationCreationModifier);
      KeypadNavigationUnexploredModifier = StringToModifierKeys(element["keypadNavigation"]["unexploredModifier"].Text, s_keypadNavigationUnexploredModifier);
    }

    public static void ShowAppDialog()
    {
      using (var dialog = new AppSettingsDialog())
      {
        dialog.InvertMouseWheel = InvertMouseWheel;
        dialog.DefaultFontName = DefaultFontName;
        dialog.DefaultImageType = DefaultImageType;
        dialog.PortAdjustDetail = PortAdjustDetail;
        dialog.SaveToImage = SaveToImage;
        dialog.SaveToPDF = SaveToPDF;
        dialog.SaveTADSToADV3Lite = SaveTADSToADV3Lite;
        dialog.SaveAt100 = SaveAt100;
        dialog.SpecifyGenMargins = SpecifyGenMargins;
        dialog.GenHorizontalMargin = GenHorizontalMargin;
        dialog.GenVerticalMargin = GenVerticalMargin;
        dialog.LoadLastProjectOnStart = LoadLastProjectOnStart;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          InvertMouseWheel = dialog.InvertMouseWheel;
          DefaultFontName = dialog.DefaultFontName;
          DefaultImageType = dialog.DefaultImageType;
          PortAdjustDetail = dialog.PortAdjustDetail;
          SaveAt100 = dialog.SaveAt100;
          SaveToImage = dialog.SaveToImage;
          SaveToPDF = dialog.SaveToPDF;
          SaveTADSToADV3Lite = dialog.SaveTADSToADV3Lite;
          SpecifyGenMargins = dialog.SpecifyGenMargins;
          GenHorizontalMargin = (int)dialog.GenHorizontalMargin;
          GenVerticalMargin = (int)dialog.GenVerticalMargin;
          LoadLastProjectOnStart = dialog.LoadLastProjectOnStart;
        }
      }
    }

    public class ColorSettings
    {
      public Color this[int index]
      {
        get { return s_color[index]; }
        set
        {
          if (s_color[index] != value)
          {
            s_color[index] = value;
            RaiseChanged();
          }
        }
      }
    }
  }
}