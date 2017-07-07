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
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Trizbort.UI;

namespace Trizbort
{
  [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
  public static class Settings
  {
    private const float MinFontSize = 2;
    private const float MaxFontSize = 256;
    // per-map settings, saved with the map
    private static readonly Color[]  sColor = new Color[Colors.Count];
    private static Font sRoomNameFont;
    private static Font sObjectFont;
    private static Font sSubtitleFont;
    private static float sLineWidth;
    private static bool sSnapToGrid;
    private static bool sIsGridVisible;
    private static bool sShowOrigin;
    private static float sGridSize;
    private static float sDarknessStripeSize;
    private static float sObjectListOffsetFromRoom;
    private static float sConnectionStalkLength;
    private static float sPreferredDistanceBetweenRooms;
    private static float sTextOffsetFromConnection;
    private static float sHandleSize;
    private static float sSnapToElementSize;
    private static bool sDocSpecificMargins;
    private static float sDocHorizontalMargin;
    private static float sDocVerticalMargin;
    private static float sDragDistanceToInitiateNewConnection;
    private static float sConnectionArrowSize;
    private static Keys sKeypadNavigationCreationModifier;
    private static Keys sKeypadNavigationUnexploredModifier;
    // application settings, saved for the user
    private static AutomapSettings sAutomap;

    static Settings()
    {
      Color = new ColorSettings();
      Regions = new List<Region>
      {
        new Region {RegionName = Region.DefaultRegion, RColor = System.Drawing.Color.White}
      };
      RecentProjects = new MruList();
      resetApplicationSettings();
      loadApplicationSettings();
      Reset();
    }

    private static string applicationSettingsPath => Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Genstein"), "Trizbort"), "Settings.xml");

    public static ColorSettings Color { get; }
    public static List<Region> Regions { get; private set; }

    public static string DefaultRoomName { get; set; } = "Cave";

    public static Font RoomNameFont
    {
      get { return sRoomNameFont; }
      set
      {
        if (!Equals(sRoomNameFont, value))
        {
          sRoomNameFont = value;
          raiseChanged();
        }
      }
    }

    public static Font ObjectFont
    {
      get { return sObjectFont; }
      set
      {
        if (!Equals(sObjectFont, value))
        {
          sObjectFont = value;
          raiseChanged();
        }
      }
    }

    public static Font SubtitleFont
    {
      get { return sSubtitleFont; }
      set
      {
        if (!Equals(sSubtitleFont, value))
        {
          sSubtitleFont = value;
          raiseChanged();
        }
      }
    }

//    public static bool HandDrawnDoc
//    {
//      get { return HandDrawnDocUnchecked; }
//      set
//      {
//        if (HandDrawnDocUnchecked != value)
//        {
//          HandDrawnDocUnchecked = value;
//          raiseChanged();
//        }
//      }
//    }
//
//    public static bool HandDrawnDocUnchecked { get; set; }

    public static float LineWidth
    {
      get { return sLineWidth; }
      set
      {
        if (sLineWidth != value)
        {
          sLineWidth = value;
          raiseChanged();
        }
      }
    }

    public static bool SnapToGrid
    {
      get { return sSnapToGrid; }
      set
      {
        if (sSnapToGrid != value)
        {
          sSnapToGrid = value;
          raiseChanged();
        }
      }
    }

    public static float GridSize
    {
      get { return sGridSize; }
      set
      {
        if (sGridSize != value)
        {
          sGridSize = value;
          raiseChanged();
        }
      }
    }

    public static bool IsGridVisible
    {
      get { return sIsGridVisible; }
      set
      {
        if (sIsGridVisible != value)
        {
          sIsGridVisible = value;
          raiseChanged();
        }
      }
    }

    public static bool ShowOrigin
    {
      get { return sShowOrigin; }
      set
      {
        if (sShowOrigin != value)
        {
          sShowOrigin = value;
          raiseChanged();
        }
      }
    }

    public static float DarknessStripeSize
    {
      get { return sDarknessStripeSize; }
      set
      {
        if (sDarknessStripeSize != value)
        {
          sDarknessStripeSize = value;
          raiseChanged();
        }
      }
    }

    public static float ObjectListOffsetFromRoom
    {
      get { return sObjectListOffsetFromRoom; }
      set
      {
        if (sObjectListOffsetFromRoom != value)
        {
          sObjectListOffsetFromRoom = value;
          raiseChanged();
        }
      }
    }

    public static float HandleSize
    {
      get { return sHandleSize; }
      set
      {
        if (sHandleSize != value)
        {
          sHandleSize = value;
          raiseChanged();
        }
      }
    }

    public static float SnapToElementSize
    {
      get { return sSnapToElementSize; }
      set
      {
        if (sSnapToElementSize != value)
        {
          sSnapToElementSize = value;
          raiseChanged();
        }
      }
    }

    public static bool DocumentSpecificMargins
    {
      get => sDocSpecificMargins;
      set
      {
        if (sDocSpecificMargins == value) return;
        sDocSpecificMargins = value;
        raiseChanged();
      }
    }

    public static float DocHorizontalMargin
    {
      get { return sDocHorizontalMargin; }
      set
      {
        if (sDocHorizontalMargin != value)
        {
          sDocHorizontalMargin = value;
          raiseChanged();
        }
      }
    }

    public static float DocVerticalMargin
    {
      get { return sDocVerticalMargin; }
      set
      {
        if (sDocVerticalMargin != value)
        {
          sDocVerticalMargin = value;
          raiseChanged();
        }
      }
    }

    public static float DragDistanceToInitiateNewConnection
    {
      get { return sDragDistanceToInitiateNewConnection; }
      set
      {
        if (sDragDistanceToInitiateNewConnection != value)
        {
          sDragDistanceToInitiateNewConnection = value;
          raiseChanged();
        }
      }
    }

    public static float ConnectionStalkLength
    {
      get { return sConnectionStalkLength; }
      set
      {
        if (sConnectionStalkLength != value)
        {
          sConnectionStalkLength = value;
          raiseChanged();
        }
      }
    }

    public static float PreferredDistanceBetweenRooms
    {
      get { return sPreferredDistanceBetweenRooms; }
      set
      {
        if (sPreferredDistanceBetweenRooms != value)
        {
          sPreferredDistanceBetweenRooms = value;
          raiseChanged();
        }
      }
    }

    public static float TextOffsetFromConnection
    {
      get { return sTextOffsetFromConnection; }
      set
      {
        if (sTextOffsetFromConnection != value)
        {
          sTextOffsetFromConnection = value;
          raiseChanged();
        }
      }
    }

    public static float ConnectionArrowSize
    {
      get { return sConnectionArrowSize; }
      set
      {
        if (sConnectionArrowSize != value)
        {
          sConnectionArrowSize = value;
          raiseChanged();
        }
      }
    }

    /// <summary>
    ///   Get/set the modifier keys required, along with a numeric keypad key,
    ///   to create new rooms from the currently selected room.
    /// </summary>
    public static Keys KeypadNavigationCreationModifier
    {
      get { return sKeypadNavigationCreationModifier; }
      set
      {
        if (sKeypadNavigationCreationModifier != value)
        {
          sKeypadNavigationCreationModifier = value;
          raiseChanged();
        }
      }
    }

    /// <summary>
    ///   Get/set the modifier keys required, along with a numeric keypad key,
    ///   to mark "unexplored" connections from the currently selected room.
    /// </summary>
    public static Keys KeypadNavigationUnexploredModifier
    {
      get { return sKeypadNavigationUnexploredModifier; }
      set
      {
        if (sKeypadNavigationUnexploredModifier != value)
        {
          sKeypadNavigationUnexploredModifier = value;
          raiseChanged();
        }
      }
    }

    public static bool StartRoomLoaded { get; set; }
    public static bool EndRoomLoaded { get; set; }

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
    public static bool SaveTadstoAdv3Lite { get; set; }
    public static bool SpecifyGenMargins { get; set; }
    public static int GenHorizontalMargin { get; set; }
    public static int GenVerticalMargin { get; set; }
    public static int CanvasWidth { get; set; }
    public static int CanvasHeight { get; set; }
    public static int PortAdjustDetail { get; set; }
    public static int DefaultImageType { get; set; }
    public static string DefaultFontName { get; set; }
    public static bool HandDrawnGlobal { get; set; }
    public static bool ShowToolTipsOnObjects { get; set; } = true;
    public static bool InvertMouseWheel { get; set; }
    public static Version DontCareAboutVersion { get; set; }

    public static AutomapSettings Automap
    {
      get { return sAutomap; }
      set { sAutomap = value; }
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
    public static string LastExportZilFileName { get; set; }
    public static string LastExportAlanFileName { get; set; }
    public static MruList RecentProjects { get; }

    private static void resetApplicationSettings()
    {
      DontCareAboutVersion = new Version(0, 0, 0, 0);
      sAutomap = AutomapSettings.Default;
      InfiniteScrollBounds = false;
      ShowMiniMap = true;
      SaveAt100 = true;
      SaveToImage = true;
      SaveToPDF = true;
      SaveTadstoAdv3Lite = true;
      RecentProjects.Clear();
      ShowToolTipsOnObjects = true;
    }

    private static void loadApplicationSettings()
    {
      try
      {
        if (File.Exists(applicationSettingsPath))
        {
          var doc = new XmlDocument();
          doc.Load(applicationSettingsPath);
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
            LastExportHugoFileName = root["lastExportedHugoFileName"].Text;
            LastExportZilFileName = root["lastExportedZilFileName"].Text;

            InvertMouseWheel = root["invertMouseWheel"].ToBool(InvertMouseWheel);
            PortAdjustDetail = root["portAdjustDetail"].ToInt(PortAdjustDetail);
            DefaultFontName = root["defaultFontName"].Text;

            if (DefaultFontName.Length == 0) DefaultFontName = "Arial"; // important for compatibility with 1.5.9.3 and before. Otherwise it's set to MS Sans Serif

            DefaultImageType = root["defaultImageType"].ToInt(DefaultImageType);
            SaveToImage = root["saveToImage"].ToBool(SaveToImage);
            SaveToPDF = root["saveToPDF"].ToBool(SaveToPDF);
            SaveTadstoAdv3Lite = root["saveTADSToADV3Lite"].ToBool(SaveTadstoAdv3Lite);
            SaveAt100 = root["saveAt100"].ToBool(SaveAt100);
            SpecifyGenMargins = root["specifyMargins"].ToBool(SpecifyGenMargins);
            GenHorizontalMargin = root["horizontalMargin"].ToInt(GenHorizontalMargin);
            GenVerticalMargin = root["verticalMargin"].ToInt(GenVerticalMargin);
            HandDrawnGlobal = root["handDrawnDefault"].ToBool(HandDrawnGlobal);
            ShowToolTipsOnObjects = root["showToolTipsOnObjects"].ToBool(true);

            CanvasWidth = root["canvasWidth"].ToInt(CanvasWidth);
            CanvasHeight = root["canvasHeight"].ToInt(CanvasHeight);
            if (CanvasWidth == 0) { CanvasWidth = 624; }
            if (CanvasHeight == 0) { CanvasHeight = 450; }

            var recentProjects = root["recentProjects"];
            string fileName;
            var index = 0;
            do
            {
              fileName = recentProjects[$"fileName{index++}"].Text;
              if (!string.IsNullOrEmpty(fileName))
              {
                RecentProjects.Append(fileName);
              }
            } while (!string.IsNullOrEmpty(fileName));

            var automap = root["automap"];
            sAutomap.FileName = automap["transcriptFileName"].ToText(sAutomap.FileName);
            sAutomap.VerboseTranscript = automap["verboseTranscript"].ToBool(sAutomap.VerboseTranscript);
            sAutomap.AssumeRoomsWithSameNameAreSameRoom = automap["assumeRoomsWithSameNameAreSameRoom"].ToBool(sAutomap.AssumeRoomsWithSameNameAreSameRoom);
            sAutomap.GuessExits = automap["guessExits"].ToBool(sAutomap.GuessExits);
            sAutomap.AddObjectCommand = automap["addObjectCommand"].ToText(sAutomap.AddObjectCommand);
            sAutomap.AddRegionCommand = automap["addRegionCommand"].ToText(sAutomap.AddRegionCommand);
          }
        }
      }
      catch (Exception)
      {
        // ignored
      }
    }

    public static void SaveApplicationSettings()
    {
      try
      {
        var directoryName = Path.GetDirectoryName(applicationSettingsPath);
        if (directoryName != null) Directory.CreateDirectory(directoryName);
        using (var scribe = XmlScribe.Create(applicationSettingsPath))
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
          scribe.Element("saveTADSToADV3Lite", SaveTadstoAdv3Lite);
          scribe.Element("verticalMargin", GenVerticalMargin);
          scribe.Element("horizontalMargin", GenHorizontalMargin);
          scribe.Element("specifyMargins", SpecifyGenMargins);
          scribe.Element("handDrawnDefault", HandDrawnGlobal);
          scribe.Element("showToolTipsOnObjects", ShowToolTipsOnObjects);

          scribe.Element("canvasHeight", CanvasHeight);
          scribe.Element("canvasWidth", CanvasWidth);

          scribe.Element("loadLastProjectOnStart", LoadLastProjectOnStart);
          scribe.Element("lastProjectFileName", LastProjectFileName);
          scribe.Element("lastExportedImageFileName", LastExportImageFileName);
          scribe.Element("lastExportedInform7FileName", LastExportInform7FileName);
          scribe.Element("lastExportedInform6FileName", LastExportInform6FileName);
          scribe.Element("lastExportedTadsFileName", LastExportTadsFileName);
          scribe.Element("lastExportedHugoFileName", LastExportHugoFileName);
          scribe.Element("lastExportedZilFileName", LastExportZilFileName);

          scribe.StartElement("recentProjects");
          var index = 0;
          foreach (var fileName in RecentProjects)
          {
            scribe.Element($"fileName{index++}", fileName);
          }
          scribe.EndElement();

          scribe.StartElement("automap");
          scribe.Element("transcriptFileName", sAutomap.FileName);
          scribe.Element("verboseTranscript", sAutomap.VerboseTranscript);
          scribe.Element("assumeRoomsWithSameNameAreSameRoom", sAutomap.AssumeRoomsWithSameNameAreSameRoom);
          scribe.Element("guessExits", sAutomap.GuessExits);
          scribe.Element("addObjectCommand", sAutomap.AddObjectCommand);
          scribe.Element("addRegionCommand", sAutomap.AddRegionCommand);
          scribe.EndElement();
        }
      }
      catch (Exception)
      {
        // ignored
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

    private static void raiseChanged()
    {
      var changed = Changed;
      changed?.Invoke(null, EventArgs.Empty);
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

        var regCount = Regions.Count;
        var regNameList = Regions.Select(p=>p.RegionName).ToList();
        var regTextColorList = Regions.Select(p=>p.TextColor).ToList();
        var regBkgdColorList = Regions.Select(p=>p.RColor).ToList();

        dialog.Title = Project.Current.Title;
        dialog.Author = Project.Current.Author;
        dialog.Description = Project.Current.Description;
        dialog.History = Project.Current.History;
        dialog.DefaultRoomName = DefaultRoomName;
        dialog.LargeFont = RoomNameFont;
        dialog.SmallFont = ObjectFont;
        dialog.LineFont = SubtitleFont;
//        dialog.HandDrawnDoc = HandDrawnDoc;
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
          if (!Equals(RoomNameFont, dialog.LargeFont)) { Project.Current.IsDirty = true; }
          RoomNameFont = dialog.LargeFont;
          if (!Equals(ObjectFont, dialog.SmallFont)) { Project.Current.IsDirty = true; }
          ObjectFont = dialog.SmallFont;
          if (!Equals(SubtitleFont, dialog.LineFont)) { Project.Current.IsDirty = true; }
          SubtitleFont = dialog.LineFont;
//          if (HandDrawnDoc != dialog.HandDrawnDoc) { Project.Current.IsDirty = true; }
//          HandDrawnDoc = dialog.HandDrawnDoc;
          if (LineWidth != dialog.LineWidth) { Project.Current.IsDirty = true; }
          LineWidth = dialog.LineWidth;
          if (SnapToGrid != dialog.SnapToGrid) { Project.Current.IsDirty = true; }
          SnapToGrid = dialog.SnapToGrid;
          if (GridSize != dialog.GridSize) { Project.Current.IsDirty = true; }
          GridSize = dialog.GridSize;
          if (IsGridVisible != dialog.IsGridVisible) { Project.Current.IsDirty = true; }
          IsGridVisible = dialog.IsGridVisible;
          if (ShowOrigin != dialog.ShowOrigin) { Project.Current.IsDirty = true; }
          ShowOrigin = dialog.ShowOrigin;
          if (DarknessStripeSize != dialog.DarknessStripeSize) { Project.Current.IsDirty = true; }
          DarknessStripeSize = dialog.DarknessStripeSize;
          if (ObjectListOffsetFromRoom != dialog.ObjectListOffsetFromRoom) { Project.Current.IsDirty = true; }
          ObjectListOffsetFromRoom = dialog.ObjectListOffsetFromRoom;
          if (ConnectionStalkLength != dialog.ConnectionStalkLength) { Project.Current.IsDirty = true; }
          ConnectionStalkLength = dialog.ConnectionStalkLength;
          if (PreferredDistanceBetweenRooms != dialog.PreferredDistanceBetweenRooms) { Project.Current.IsDirty = true; }
          PreferredDistanceBetweenRooms = dialog.PreferredDistanceBetweenRooms;
          if (TextOffsetFromConnection != dialog.TextOffsetFromConnection) { Project.Current.IsDirty = true; }
          TextOffsetFromConnection = dialog.TextOffsetFromConnection;
          if (HandleSize != dialog.HandleSize) { Project.Current.IsDirty = true; }
          HandleSize = dialog.HandleSize;
          if (SnapToElementSize != dialog.SnapToElementSize) { Project.Current.IsDirty = true; }
          SnapToElementSize = dialog.SnapToElementSize;
          if (ConnectionArrowSize != dialog.ConnectionArrowSize) { Project.Current.IsDirty = true; }
          ConnectionArrowSize = dialog.ConnectionArrowSize;
          if (DocumentSpecificMargins != dialog.DocumentSpecificMargins) { Project.Current.IsDirty = true; }
          DocumentSpecificMargins = dialog.DocumentSpecificMargins;
          if (DocHorizontalMargin != dialog.DocHorizontalMargin) { Project.Current.IsDirty = true; }
          DocHorizontalMargin = dialog.DocHorizontalMargin;
          if (DocVerticalMargin != dialog.DocVerticalMargin) { Project.Current.IsDirty = true; }
          DocVerticalMargin = dialog.DocVerticalMargin;
        }

        //Note this needs to be done outside of the "if OK button is clicked" loop for now.
        //Trizbort makes changes to regions immediately. So we can change a region and hit cancel.
        //We need to account for that.
        var newRegCount = Regions.Count;
        var newReg = Regions.ToArray();
        if (newRegCount != regCount)
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

    public static void Reset()
    {
      Color[Colors.Canvas] = System.Drawing.Color.White;
      //Color[Colors.Fill] = System.Drawing.Color.White;
      Color[Colors.Border] = System.Drawing.Color.MidnightBlue;
      Color[Colors.Line] = System.Drawing.Color.MidnightBlue;
      Color[Colors.HoverLine] = System.Drawing.Color.DarkOrange;
      Color[Colors.SelectedLine] = System.Drawing.Color.Gold;
      Color[Colors.LargeText] = System.Drawing.Color.MidnightBlue;
      Color[Colors.SmallText] = System.Drawing.Color.MidnightBlue;
      Color[Colors.LineText] = System.Drawing.Color.MidnightBlue;
      Color[Colors.Grid] = Drawing.Mix(System.Drawing.Color.White, System.Drawing.Color.Black, 25, 1);
      Color[Colors.StartRoom] = System.Drawing.Color.GreenYellow;
      Color[Colors.EndRoom] = System.Drawing.Color.Red;

      Project.Current.Title = Project.Current.Author = Project.Current.History = Project.Current.Description = "";

      RoomNameFont = new Font(DefaultFontName, 13.0f, FontStyle.Regular, GraphicsUnit.World);
      ObjectFont = new Font(DefaultFontName, 11.0f, FontStyle.Regular, GraphicsUnit.World);
      SubtitleFont = new Font(DefaultFontName, 9.0f, FontStyle.Regular, GraphicsUnit.World);

//      HandDrawnDoc = HandDrawnGlobal;

      DocumentSpecificMargins = SpecifyGenMargins;

      if (SpecifyGenMargins)
      {
        DocHorizontalMargin = GenHorizontalMargin;
        DocVerticalMargin = GenVerticalMargin;
      }
      else
      {
        DocHorizontalMargin = DocVerticalMargin = 0;
      }

      LineWidth = 2.0f;

      SnapToGrid = true;
      IsGridVisible = true;
      GridSize = 32.0f;
      ShowOrigin = true;

      StartRoomLoaded = false;
      EndRoomLoaded = false;

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
        if (Colors.ToName(index, out string colorName))
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
      saveFont(scribe, sRoomNameFont, "room");
      saveFont(scribe, sObjectFont, "object");
      saveFont(scribe, sSubtitleFont, "line");
      scribe.EndElement();

      scribe.StartElement("grid");
      scribe.Element("snapTo", sSnapToGrid);
      scribe.Element("visible", sIsGridVisible);
      scribe.Element("showOrigin", sShowOrigin);
      scribe.Element("size", sGridSize);
      scribe.EndElement();

      scribe.StartElement("lines");
      scribe.Element("width", sLineWidth);
//      scribe.Element("handDrawn", HandDrawnDocUnchecked);
      scribe.Element("arrowSize", sConnectionArrowSize);
      scribe.Element("textOffset", sTextOffsetFromConnection);
      scribe.EndElement();

      scribe.StartElement("rooms");
      scribe.Element("darknessStripeSize", sDarknessStripeSize);
      scribe.Element("objectListOffset", sObjectListOffsetFromRoom);
      scribe.Element("connectionStalkLength", sConnectionStalkLength);
      scribe.Element("preferredDistanceBetweenRooms", sPreferredDistanceBetweenRooms);
      scribe.Element("defaultRoomName", DefaultRoomName);
      scribe.EndElement();

      scribe.StartElement("ui");
      scribe.Element("handleSize", sHandleSize);
      scribe.Element("snapToElementSize", sSnapToElementSize);
      scribe.EndElement();

      scribe.StartElement("margins");
      scribe.Element("documentSpecific", sDocSpecificMargins);
      scribe.Element("horizontal", sDocHorizontalMargin);
      scribe.Element("vertical", sDocVerticalMargin);
      scribe.EndElement();

      scribe.StartElement("keypadNavigation");
      scribe.Element("creationModifier", modifierKeysToString(sKeypadNavigationCreationModifier));
      scribe.Element("unexploredModifier", modifierKeysToString(sKeypadNavigationUnexploredModifier));
      scribe.EndElement();

      SaveApplicationSettings();
    }

    private static string modifierKeysToString(Keys key)
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

    private static Keys stringToModifierKeys(string text, Keys defaultValue)
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

    private static void saveFont(XmlScribe scribe, Font font, string name)
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
        if (Colors.FromName(color.Name, out int index))
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
          var tRegion = new Region {TextColor = region.Attribute("TextColor").Text == string.Empty ? System.Drawing.Color.Blue : ColorTranslator.FromHtml(region.Attribute("TextColor").Text)};

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
          RoomNameFont = new Font(font.ToText(RoomNameFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(RoomNameFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        }
        else if (font.Name == "object")
        {
          ObjectFont = new Font(font.ToText(ObjectFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(ObjectFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        }
        else if (font.Name == "line")
        {
          SubtitleFont = new Font(font.ToText(ObjectFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(SubtitleFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        }
      }

      SnapToGrid = element["grid"]["snapTo"].ToBool(sSnapToGrid);
      IsGridVisible = element["grid"]["visible"].ToBool(sIsGridVisible);
      GridSize = element["grid"]["size"].ToFloat(sGridSize);
      ShowOrigin = element["grid"]["showOrigin"].ToBool(sShowOrigin);

      LineWidth = element["lines"]["width"].ToFloat(sLineWidth);
//      HandDrawnDoc = element["lines"]["handDrawn"].ToBool(HandDrawnDocUnchecked);
      ConnectionArrowSize = element["lines"]["arrowSize"].ToFloat(sConnectionArrowSize);
      TextOffsetFromConnection = element["lines"]["textOffset"].ToFloat(sTextOffsetFromConnection);

      DarknessStripeSize = element["rooms"]["darknessStripeSize"].ToFloat(sDarknessStripeSize);
      ObjectListOffsetFromRoom = element["rooms"]["objectListOffset"].ToFloat(sObjectListOffsetFromRoom);
      ConnectionStalkLength = element["rooms"]["connectionStalkLength"].ToFloat(sConnectionStalkLength);
      PreferredDistanceBetweenRooms = element["rooms"]["preferredDistanceBetweenRooms"].ToFloat(sConnectionStalkLength*2); // introduced in v1.2, hence default based on existing setting

      DefaultRoomName = element["rooms"]["defaultRoomName"].Text;
      if (string.IsNullOrEmpty(DefaultRoomName)) 
          DefaultRoomName = "Cave";

      HandleSize = element["ui"]["handleSize"].ToFloat(sHandleSize);
      SnapToElementSize = element["ui"]["snapToElementSize"].ToFloat(sSnapToElementSize);

      DocumentSpecificMargins = element["margins"]["documentSpecific"].ToBool(sDocSpecificMargins);
      DocHorizontalMargin = element["margins"]["horizontal"].ToFloat(sDocHorizontalMargin);
      DocVerticalMargin = element["margins"]["vertical"].ToFloat(sDocVerticalMargin);

      KeypadNavigationCreationModifier = stringToModifierKeys(element["keypadNavigation"]["creationModifier"].Text, sKeypadNavigationCreationModifier);
      KeypadNavigationUnexploredModifier = stringToModifierKeys(element["keypadNavigation"]["unexploredModifier"].Text, sKeypadNavigationUnexploredModifier);
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
        dialog.SaveTADSToADV3Lite = SaveTadstoAdv3Lite;
        dialog.SaveAt100 = SaveAt100;
        dialog.SpecifyGenMargins = SpecifyGenMargins;
        dialog.GenHorizontalMargin = GenHorizontalMargin;
        dialog.GenVerticalMargin = GenVerticalMargin;
        dialog.LoadLastProjectOnStart = LoadLastProjectOnStart;
        dialog.HandDrawnGlobal = HandDrawnGlobal;
        dialog.ShowToolTipsOnObjects = ShowToolTipsOnObjects;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          InvertMouseWheel = dialog.InvertMouseWheel;
          DefaultFontName = dialog.DefaultFontName;
          DefaultImageType = dialog.DefaultImageType;
          PortAdjustDetail = dialog.PortAdjustDetail;
          SaveAt100 = dialog.SaveAt100;
          SaveToImage = dialog.SaveToImage;
          SaveToPDF = dialog.SaveToPDF;
          SaveTadstoAdv3Lite = dialog.SaveTADSToADV3Lite;
          SpecifyGenMargins = dialog.SpecifyGenMargins;
          GenHorizontalMargin = (int)dialog.GenHorizontalMargin;
          GenVerticalMargin = (int)dialog.GenVerticalMargin;
          LoadLastProjectOnStart = dialog.LoadLastProjectOnStart;
          HandDrawnGlobal = dialog.HandDrawnGlobal;
          ShowToolTipsOnObjects = dialog.ShowToolTipsOnObjects;
        }
      }
    }

    public class ColorSettings
    {
      public Color this[int index]
      {
        get { return sColor[index]; }
        set
        {
          if (sColor[index] != value)
          {
            sColor[index] = value;
            raiseChanged();
          }
        }
      }
    }
  }
}
