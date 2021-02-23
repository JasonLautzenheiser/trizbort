/*
    Copyright (c) 2010-2018 by Genstein and Jason Lautzenheiser.

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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Trizbort.Domain.Application;
using Trizbort.Domain.AppSettings;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;
using Trizbort.UI;
using Trizbort.Util;
using Region = Trizbort.Domain.Misc.Region;

namespace Trizbort.Setup {
  [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
  public static class Settings {
    private const float MinFontSize = 2;

    private const float MaxFontSize = 256;

    // per-map settings, saved with the map
    private static readonly Color[] sColor = new Color[Colors.Count];
    private static Font sRoomNameFont;
    private static Font sObjectFont;
    private static Font sSubtitleFont;
    private static Font sLineFont;
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
    private static bool sWrapTextAtDashes;
    private static bool sWrappingChanged;
    private static float sDragDistanceToInitiateNewConnection;
    private static float sConnectionArrowSize;
    private static Keys sKeypadNavigationCreationModifier;

    private static Keys sKeypadNavigationUnexploredModifier;

    // application settings, saved for the user
    // TODO: private static AutomapSettings sAutomap;

    static Settings() {
      Color = new ColorSettings();
      Regions = new List<Region> {
        new Region {RegionName = Region.DefaultRegion, RColor = System.Drawing.Color.White}
      };
      Reset();
    }


    public static ColorSettings Color { get; }

    public static float ConnectionArrowSize {
      get => sConnectionArrowSize;
      set {
        if (sConnectionArrowSize != value) {
          sConnectionArrowSize = value;
          raiseChanged();
        }
      }
    }

    public static float ConnectionStalkLength {
      get => sConnectionStalkLength;
      set {
        if (sConnectionStalkLength != value) {
          sConnectionStalkLength = value;
          raiseChanged();
        }
      }
    }

    public static float DarknessStripeSize {
      get => sDarknessStripeSize;
      set {
        if (sDarknessStripeSize != value) {
          sDarknessStripeSize = value;
          raiseChanged();
        }
      }
    }

    public static string DefaultRoomName { get; set; } = "Cave";
    public static RoomShape DefaultRoomShape { get; set; }

    public static float DocHorizontalMargin {
      get => sDocHorizontalMargin;
      set {
        if (sDocHorizontalMargin != value) {
          sDocHorizontalMargin = value;
          raiseChanged();
        }
      }
    }

    public static bool DocumentSpecificMargins {
      get => sDocSpecificMargins;
      set {
        if (sDocSpecificMargins == value) return;
        sDocSpecificMargins = value;
        raiseChanged();
      }
    }

    public static float DocVerticalMargin {
      get => sDocVerticalMargin;
      set {
        if (sDocVerticalMargin != value) {
          sDocVerticalMargin = value;
          raiseChanged();
        }
      }
    }

    public static bool WrapTextAtDashes {
      get => sWrapTextAtDashes;
      set {
        if (sWrapTextAtDashes == value) return;
        sWrapTextAtDashes = value;
        sWrappingChanged = true;
        raiseChanged();
      }
    }

    public static bool WrappingChanged {
      get => sWrappingChanged;
      set => sWrappingChanged = value;  
    }
    public static float DragDistanceToInitiateNewConnection {
      get => sDragDistanceToInitiateNewConnection;
      set {
        if (sDragDistanceToInitiateNewConnection != value) {
          sDragDistanceToInitiateNewConnection = value;
          raiseChanged();
        }
      }
    }

    public static bool EndRoomLoaded { get; set; }

    public static float GridSize {
      get => sGridSize;
      set {
        if (sGridSize != value) {
          sGridSize = value;
          raiseChanged();
        }
      }
    }

    public static float HandleSize {
      get => sHandleSize;
      set {
        if (sHandleSize != value) {
          sHandleSize = value;
          raiseChanged();
        }
      }
    }

    public static bool IsGridVisible {
      get => sIsGridVisible;
      set {
        if (sIsGridVisible != value) {
          sIsGridVisible = value;
          raiseChanged();
        }
      }
    }

    /// <summary>
    ///   Get/set the modifier keys required, along with a numeric keypad key,
    ///   to create new rooms from the currently selected room.
    /// </summary>
    public static Keys KeypadNavigationCreationModifier {
      get => sKeypadNavigationCreationModifier;
      set {
        if (sKeypadNavigationCreationModifier != value) {
          sKeypadNavigationCreationModifier = value;
          raiseChanged();
        }
      }
    }

    /// <summary>
    ///   Get/set the modifier keys required, along with a numeric keypad key,
    ///   to mark "unexplored" connections from the currently selected room.
    /// </summary>
    public static Keys KeypadNavigationUnexploredModifier {
      get => sKeypadNavigationUnexploredModifier;
      set {
        if (sKeypadNavigationUnexploredModifier != value) {
          sKeypadNavigationUnexploredModifier = value;
          raiseChanged();
        }
      }
    }

    public static Font LineFont {
      get => sLineFont;
      set {
        if (!Equals(sLineFont, value)) {
          sLineFont = value;
          raiseChanged();
        }
      }
    }


    public static float LineWidth {
      get => sLineWidth;
      set {
        if (sLineWidth != value) {
          sLineWidth = value;
          raiseChanged();
        }
      }
    }

    public static Font ObjectFont {
      get => sObjectFont;
      set {
        if (!Equals(sObjectFont, value)) {
          sObjectFont = value;
          raiseChanged();
        }
      }
    }

    public static float ObjectListOffsetFromRoom {
      get => sObjectListOffsetFromRoom;
      set {
        if (sObjectListOffsetFromRoom != value) {
          sObjectListOffsetFromRoom = value;
          raiseChanged();
        }
      }
    }

    public static float PreferredDistanceBetweenRooms {
      get => sPreferredDistanceBetweenRooms;
      set {
        if (sPreferredDistanceBetweenRooms != value) {
          sPreferredDistanceBetweenRooms = value;
          raiseChanged();
        }
      }
    }

    public static List<Region> Regions { get; private set; }

    public static Font RoomNameFont {
      get => sRoomNameFont;
      set {
        if (!Equals(sRoomNameFont, value)) {
          sRoomNameFont = value;
          raiseChanged();
        }
      }
    }

    public static bool ShowOrigin {
      get => sShowOrigin;
      set {
        if (sShowOrigin != value) {
          sShowOrigin = value;
          raiseChanged();
        }
      }
    }

    public static float SnapToElementSize {
      get => sSnapToElementSize;
      set {
        if (sSnapToElementSize != value) {
          sSnapToElementSize = value;
          raiseChanged();
        }
      }
    }

    public static bool SnapToGrid {
      get => sSnapToGrid;
      set {
        if (sSnapToGrid != value) {
          sSnapToGrid = value;
          raiseChanged();
        }
      }
    }

    public static bool StartRoomLoaded { get; set; }

    public static Font SubtitleFont {
      get => sSubtitleFont;
      set {
        if (!Equals(sSubtitleFont, value)) {
          sSubtitleFont = value;
          raiseChanged();
        }
      }
    }

    public static float TextOffsetFromConnection {
      get => sTextOffsetFromConnection;
      set {
        if (sTextOffsetFromConnection != value) {
          sTextOffsetFromConnection = value;
          raiseChanged();
        }
      }
    }

    public static List<CustomAttributeDefinition> CustomAttributeDefinitions { get; set; } = new List<CustomAttributeDefinition>();

    public static event EventHandler Changed;

    public static void Load(XmlElementReader element) {
      var colors = element["colors"];
      foreach (var color in colors.Children)
        if (Colors.FromName(color.Name, out var index))
          Color[index] = color.ToColor(Color[index]);

      var regions = element["regions"];
      Regions = new List<Region>();

      if (regions.Children.Count <= 0)
        Regions.Add(new Region {RColor = System.Drawing.Color.White, TextColor = System.Drawing.Color.Blue, RegionName = Region.DefaultRegion});
      else
        foreach (var region in regions.Children) {
          var tRegion = new Region {TextColor = region.Attribute("TextColor").Text == string.Empty ? System.Drawing.Color.Blue : ColorTranslator.FromHtml(region.Attribute("TextColor").Text)};

          var node = region.Attribute("name");
          tRegion.RegionName = node != null && node.Text != string.Empty ? node.Text : region.Name;

          tRegion.RegionName = tRegion.ClearRegionNameObfuscation();
          tRegion.RColor = region.ToColor(System.Drawing.Color.White);
          Regions.Add(tRegion);
        }

      var fonts = element["fonts"];
      foreach (var font in fonts.Children) {
        var style = FontStyle.Regular;
        if (font.Attribute("bold").ToBool()) style |= FontStyle.Bold;
        if (font.Attribute("italic").ToBool()) style |= FontStyle.Italic;
        if (font.Attribute("underline").ToBool()) style |= FontStyle.Underline;
        if (font.Attribute("strikeout").ToBool()) style |= FontStyle.Strikeout;
        if (font.Name == "room")
          RoomNameFont = new Font(font.ToText(RoomNameFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(RoomNameFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        else if (font.Name == "object")
          ObjectFont = new Font(font.ToText(ObjectFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(ObjectFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        else if (font.Name == "subTitle")
          SubtitleFont = new Font(font.ToText(SubtitleFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(SubtitleFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
        else if (font.Name == "line") LineFont = new Font(font.ToText(LineFont.Name), Numeric.Clamp(font.Attribute("size").ToFloat(LineFont.Size), MinFontSize, MaxFontSize), style, GraphicsUnit.World);
      }

      SnapToGrid = element["grid"]["snapTo"].ToBool(sSnapToGrid);
      IsGridVisible = element["grid"]["visible"].ToBool(sIsGridVisible);
      GridSize = element["grid"]["size"].ToFloat(sGridSize);
      ShowOrigin = element["grid"]["showOrigin"].ToBool(sShowOrigin);

      LineWidth = element["lines"]["width"].ToFloat(sLineWidth);
      ConnectionArrowSize = element["lines"]["arrowSize"].ToFloat(sConnectionArrowSize);
      TextOffsetFromConnection = element["lines"]["textOffset"].ToFloat(sTextOffsetFromConnection);

      DarknessStripeSize = element["rooms"]["darknessStripeSize"].ToFloat(sDarknessStripeSize);
      ObjectListOffsetFromRoom = element["rooms"]["objectListOffset"].ToFloat(sObjectListOffsetFromRoom);
      ConnectionStalkLength = element["rooms"]["connectionStalkLength"].ToFloat(sConnectionStalkLength);
      PreferredDistanceBetweenRooms = element["rooms"]["preferredDistanceBetweenRooms"].ToFloat(sConnectionStalkLength * 2); // introduced in v1.2, hence default based on existing setting

      DefaultRoomShape = (RoomShape) element["rooms"]["defaultRoomShape"].ToInt();
      DefaultRoomName = element["rooms"]["defaultRoomName"].Text;
      if (string.IsNullOrEmpty(DefaultRoomName))
        DefaultRoomName = "Cave";

      HandleSize = element["ui"]["handleSize"].ToFloat(sHandleSize);
      SnapToElementSize = element["ui"]["snapToElementSize"].ToFloat(sSnapToElementSize);

      DocumentSpecificMargins = element["margins"]["documentSpecific"].ToBool(sDocSpecificMargins);
      DocHorizontalMargin = element["margins"]["horizontal"].ToFloat(sDocHorizontalMargin);
      DocVerticalMargin = element["margins"]["vertical"].ToFloat(sDocVerticalMargin);
      WrapTextAtDashes = element["margins"]["wrapDashes"].ToBool(true); // maybe should be somewhere else

      KeypadNavigationCreationModifier = stringToModifierKeys(element["keypadNavigation"]["creationModifier"].Text, sKeypadNavigationCreationModifier);
      KeypadNavigationUnexploredModifier = stringToModifierKeys(element["keypadNavigation"]["unexploredModifier"].Text, sKeypadNavigationUnexploredModifier);

      var attributeDefinitions = element["customAttributeDefinitions"];
      if (attributeDefinitions != null)
      {
        CustomAttributeDefinitions.Clear();

        foreach (var childElement in attributeDefinitions.Children)
        {
          var definition = new CustomAttributeDefinition();
          definition.Load(childElement);
          CustomAttributeDefinitions.Add(definition);
        }
      }
    }


    public static void Reset() {
      Color[Colors.Canvas] = System.Drawing.Color.White;
      //Color[Colors.Fill] = System.Drawing.Color.White;
      Color[Colors.Border] = System.Drawing.Color.MidnightBlue;
      Color[Colors.Line] = System.Drawing.Color.MidnightBlue;
      Color[Colors.HoverLine] = System.Drawing.Color.DarkOrange;
      Color[Colors.SelectedLine] = System.Drawing.Color.Gold;
      Color[Colors.Subtitle] = System.Drawing.Color.MidnightBlue;
      Color[Colors.SmallText] = System.Drawing.Color.MidnightBlue;
      Color[Colors.LineText] = System.Drawing.Color.MidnightBlue;
      Color[Colors.Grid] = Drawing.Mix(System.Drawing.Color.White, System.Drawing.Color.Black, 10, 1);
      Color[Colors.StartRoom] = System.Drawing.Color.GreenYellow;
      Color[Colors.EndRoom] = System.Drawing.Color.Red;

      Project.Current.Title = Project.Current.Author = Project.Current.History = Project.Current.Description = "";

      RoomNameFont = new Font(ApplicationSettingsController.AppSettings.DefaultFontName, 13.0f, FontStyle.Regular, GraphicsUnit.World);
      ObjectFont = new Font(ApplicationSettingsController.AppSettings.DefaultFontName, 11.0f, FontStyle.Regular, GraphicsUnit.World);
      SubtitleFont = new Font(ApplicationSettingsController.AppSettings.DefaultFontName, 9.0f, FontStyle.Regular, GraphicsUnit.World);
      LineFont = new Font(ApplicationSettingsController.AppSettings.DefaultFontName, 9.0f, FontStyle.Regular, GraphicsUnit.World);

      DocumentSpecificMargins = ApplicationSettingsController.AppSettings.SpecifyGenMargins;

      if (ApplicationSettingsController.AppSettings.SpecifyGenMargins) {
        DocHorizontalMargin = ApplicationSettingsController.AppSettings.GenHorizontalMargin;
        DocVerticalMargin = ApplicationSettingsController.AppSettings.GenVerticalMargin;
      } else {
        DocHorizontalMargin = DocVerticalMargin = 0;
      }

      WrapTextAtDashes = ApplicationSettingsController.AppSettings.SpecifyWrapping;

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
      PreferredDistanceBetweenRooms = ConnectionStalkLength * 2;
      TextOffsetFromConnection = 4.0f;
      HandleSize = 12.0f;
      SnapToElementSize = 16.0f;
      DragDistanceToInitiateNewConnection = 32f;
      ConnectionArrowSize = 12.0f;

      KeypadNavigationCreationModifier = Keys.Control;
      KeypadNavigationUnexploredModifier = Keys.Alt;

      Regions = new List<Region> {
        new Region {RegionName = Region.DefaultRegion, RColor = System.Drawing.Color.White}
      };
    }

    public static void Save(XmlScribe scribe) {
      // save colors
      scribe.StartElement("colors");
      for (var index = 0; index < Colors.Count; ++index)
        if (Colors.ToName(index, out var colorName))
          scribe.Element(colorName, Color[index]);
      scribe.EndElement();

      scribe.StartElement("regions");
      foreach (var region in Regions.OrderBy(p => p.RegionName)) {
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
      saveFont(scribe, sSubtitleFont, "subTitle");
      saveFont(scribe, sLineFont, "line");
      scribe.EndElement();

      scribe.StartElement("grid");
      scribe.Element("snapTo", sSnapToGrid);
      scribe.Element("visible", sIsGridVisible);
      scribe.Element("showOrigin", sShowOrigin);
      scribe.Element("size", sGridSize);
      scribe.EndElement();

      scribe.StartElement("lines");
      scribe.Element("width", sLineWidth);
      scribe.Element("arrowSize", sConnectionArrowSize);
      scribe.Element("textOffset", sTextOffsetFromConnection);
      scribe.EndElement();

      scribe.StartElement("rooms");
      scribe.Element("darknessStripeSize", sDarknessStripeSize);
      scribe.Element("objectListOffset", sObjectListOffsetFromRoom);
      scribe.Element("connectionStalkLength", sConnectionStalkLength);
      scribe.Element("preferredDistanceBetweenRooms", sPreferredDistanceBetweenRooms);
      scribe.Element("defaultRoomName", DefaultRoomName);
      scribe.Element("defaultRoomShape", (int) DefaultRoomShape);
      scribe.EndElement();

      scribe.StartElement("ui");
      scribe.Element("handleSize", sHandleSize);
      scribe.Element("snapToElementSize", sSnapToElementSize);
      scribe.EndElement();

      scribe.StartElement("margins");
      scribe.Element("documentSpecific", sDocSpecificMargins);
      scribe.Element("horizontal", sDocHorizontalMargin);
      scribe.Element("vertical", sDocVerticalMargin);
      scribe.Element("wrapDashes", sWrapTextAtDashes); // maybe should be somewhere else
      scribe.EndElement();

      scribe.StartElement("keypadNavigation");
      scribe.Element("creationModifier", modifierKeysToString(sKeypadNavigationCreationModifier));
      scribe.Element("unexploredModifier", modifierKeysToString(sKeypadNavigationUnexploredModifier));
      scribe.EndElement();

      scribe.StartElement("customAttributeDefinitions");
      foreach (var cad in CustomAttributeDefinitions) {
        cad.Save(scribe);
      }
      scribe.EndElement();
    }

    public static void ShowMapDialog() {
      using (var dialog = new SettingsDialog()) {
        for (var index = 0; index < Colors.Count; ++index) dialog.ElementColors[index] = Color[index];

        //below is code for pulling the region names, text color and background color from Settings.Regions.
        //it is set up so that Trizbort can check after we click OK or Cancel, and we can see if anything changed.
        //Currently Trizbort only can check for region names of individual rooms changing.
        //After talking with Jason, We'll need to refactor code to make this run smoother, but this is the best for now.

        var regCount = Regions.Count;
        var regNameList = Regions.Select(p => p.RegionName).ToList();
        var regTextColorList = Regions.Select(p => p.TextColor).ToList();
        var regBkgdColorList = Regions.Select(p => p.RColor).ToList();

        dialog.Title = Project.Current.Title;
        dialog.Author = Project.Current.Author;
        dialog.Description = Project.Current.Description;
        dialog.History = Project.Current.History;
        dialog.DefaultRoomName = DefaultRoomName;
        dialog.LargeFont = RoomNameFont;
        dialog.SmallFont = ObjectFont;
        dialog.LineFont = LineFont;
        dialog.SubtitleFont = SubtitleFont;
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
        dialog.WrapTextAtDashes = WrapTextAtDashes;
        dialog.ConnectionArrowSize = ConnectionArrowSize;
        dialog.DefaultRoomShape = DefaultRoomShape;
        dialog.CustomAttributeDefinitions = CustomAttributeDefinitions;
        if (dialog.ShowDialog() == DialogResult.OK) {
          for (var index = 0; index < Colors.Count; ++index) {
            if (Color[index] != dialog.ElementColors[index]) Project.Current.IsDirty = true;
            Color[index] = dialog.ElementColors[index];
          }

          if (Project.Current.Title != dialog.Title) Project.Current.IsDirty = true;
          Project.Current.Title = dialog.Title;
          if (Project.Current.Author != dialog.Author) Project.Current.IsDirty = true;
          Project.Current.Author = dialog.Author;
          if (Project.Current.Description != dialog.Description) Project.Current.IsDirty = true;
          Project.Current.Description = dialog.Description;
          if (Project.Current.History != dialog.History) Project.Current.IsDirty = true;
          Project.Current.History = dialog.History;
          if (DefaultRoomName != dialog.DefaultRoomName) Project.Current.IsDirty = true;
          DefaultRoomName = dialog.DefaultRoomName;
          if (!Equals(RoomNameFont, dialog.LargeFont)) Project.Current.IsDirty = true;
          RoomNameFont = dialog.LargeFont;
          if (!Equals(ObjectFont, dialog.SmallFont)) Project.Current.IsDirty = true;
          ObjectFont = dialog.SmallFont;
          if (!Equals(SubtitleFont, dialog.SubtitleFont)) Project.Current.IsDirty = true;
          SubtitleFont = dialog.SubtitleFont;
          if (!Equals(LineFont, dialog.LineFont)) Project.Current.IsDirty = true;
          LineFont = dialog.LineFont;
          if (LineWidth != dialog.LineWidth) Project.Current.IsDirty = true;
          LineWidth = dialog.LineWidth;
          if (SnapToGrid != dialog.SnapToGrid) Project.Current.IsDirty = true;
          SnapToGrid = dialog.SnapToGrid;
          if (GridSize != dialog.GridSize) Project.Current.IsDirty = true;
          GridSize = dialog.GridSize;
          if (IsGridVisible != dialog.IsGridVisible) Project.Current.IsDirty = true;
          IsGridVisible = dialog.IsGridVisible;
          if (ShowOrigin != dialog.ShowOrigin) Project.Current.IsDirty = true;
          ShowOrigin = dialog.ShowOrigin;
          if (DarknessStripeSize != dialog.DarknessStripeSize) Project.Current.IsDirty = true;
          DarknessStripeSize = dialog.DarknessStripeSize;
          if (ObjectListOffsetFromRoom != dialog.ObjectListOffsetFromRoom) Project.Current.IsDirty = true;
          ObjectListOffsetFromRoom = dialog.ObjectListOffsetFromRoom;
          if (ConnectionStalkLength != dialog.ConnectionStalkLength) Project.Current.IsDirty = true;
          ConnectionStalkLength = dialog.ConnectionStalkLength;
          if (PreferredDistanceBetweenRooms != dialog.PreferredDistanceBetweenRooms) Project.Current.IsDirty = true;
          PreferredDistanceBetweenRooms = dialog.PreferredDistanceBetweenRooms;
          if (TextOffsetFromConnection != dialog.TextOffsetFromConnection) Project.Current.IsDirty = true;
          TextOffsetFromConnection = dialog.TextOffsetFromConnection;
          if (HandleSize != dialog.HandleSize) Project.Current.IsDirty = true;
          HandleSize = dialog.HandleSize;
          if (SnapToElementSize != dialog.SnapToElementSize) Project.Current.IsDirty = true;
          SnapToElementSize = dialog.SnapToElementSize;
          if (ConnectionArrowSize != dialog.ConnectionArrowSize) Project.Current.IsDirty = true;
          ConnectionArrowSize = dialog.ConnectionArrowSize;
          if (DocumentSpecificMargins != dialog.DocumentSpecificMargins) Project.Current.IsDirty = true;
          DocumentSpecificMargins = dialog.DocumentSpecificMargins;
          if (DocHorizontalMargin != dialog.DocHorizontalMargin) Project.Current.IsDirty = true;
          DocHorizontalMargin = dialog.DocHorizontalMargin;
          if (DocVerticalMargin != dialog.DocVerticalMargin) Project.Current.IsDirty = true;
          DocVerticalMargin = dialog.DocVerticalMargin;
          if (WrapTextAtDashes != dialog.WrapTextAtDashes) Project.Current.IsDirty = true;
          WrapTextAtDashes = dialog.WrapTextAtDashes;
          if (DefaultRoomShape != dialog.DefaultRoomShape) Project.Current.IsDirty = true;
          DefaultRoomShape = dialog.DefaultRoomShape;
        }

        //Note this needs to be done outside of the "if OK button is clicked" loop for now.
        //Trizbort makes changes to regions immediately. So we can change a region and hit cancel.
        //We need to account for that.
        var newRegCount = Regions.Count;
        var newReg = Regions.ToArray();
        if (newRegCount != regCount)
          Project.Current.IsDirty = true;
        else
          for (var index = 0; index < newRegCount; index++) {
            if (regBkgdColorList[index] != newReg[index].RColor)
              Project.Current.IsDirty = true;
            if (regTextColorList[index] != newReg[index].TextColor)
              Project.Current.IsDirty = true;
            if (regNameList[index] != newReg[index].RegionName)
              Project.Current.IsDirty = true;
          }

        // Custom Attribute Definitions take effect even if we Cancel out since elements are updated immediately.
        if (!Enumerable.SequenceEqual(CustomAttributeDefinitions, dialog.CustomAttributeDefinitions)) Project.Current.IsDirty = true;
        CustomAttributeDefinitions = dialog.CustomAttributeDefinitions;
      }
    }

    public static float Snap(float value) {
      float offset = 0;
      while (value < GridSize) {
        value += GridSize;
        offset += GridSize;
      }

      var mod = value % GridSize;
      if (SnapToGrid && mod != 0)
        if (mod < GridSize / 2)
          value -= mod;
        else
          value = value + GridSize - mod;

      return value - offset;
    }

    public static Vector Snap(Vector pos) {
      if (SnapToGrid) {
        pos.X = Snap(pos.X);
        pos.Y = Snap(pos.Y);
      }

      return pos;
    }

    private static string modifierKeysToString(Keys key) {
      var builder = new StringBuilder();
      if ((key & Keys.Shift) == Keys.Shift) {
        if (builder.Length != 0)
          builder.Append("|");
        builder.Append("shift");
      }

      if ((key & Keys.Control) == Keys.Control) {
        if (builder.Length != 0)
          builder.Append("|");
        builder.Append("control");
      }

      if ((key & Keys.Alt) == Keys.Alt) {
        if (builder.Length != 0)
          builder.Append("|");
        builder.Append("alt");
      }

      if (builder.Length == 0) builder.Append("none");
      return builder.ToString();
    }

    private static void raiseChanged() {
      var changed = Changed;
      changed?.Invoke(null, EventArgs.Empty);
    }

    private static void saveFont(XmlScribe scribe, Font font, string name) {
      scribe.StartElement(name);
      scribe.Attribute("size", font.Size);
      if ((font.Style & FontStyle.Bold) == FontStyle.Bold) scribe.Attribute("bold", true);
      if ((font.Style & FontStyle.Italic) == FontStyle.Italic) scribe.Attribute("italic", true);
      if ((font.Style & FontStyle.Underline) == FontStyle.Underline) scribe.Attribute("underline", true);
      if ((font.Style & FontStyle.Strikeout) == FontStyle.Strikeout) scribe.Attribute("strikeout", true);
      scribe.Value(Drawing.FontName(font));
      scribe.EndElement();
    }

    private static Keys stringToModifierKeys(string text, Keys defaultValue) {
      if (string.IsNullOrEmpty(text)) return defaultValue;

      var value = Keys.None;
      foreach (var part in text.Split('|'))
        if (StringComparer.InvariantCultureIgnoreCase.Compare(part, "shift") == 0)
          value |= Keys.Shift;
        else if (StringComparer.InvariantCultureIgnoreCase.Compare(part, "control") == 0)
          value |= Keys.Control;
        else if (StringComparer.InvariantCultureIgnoreCase.Compare(part, "alt") == 0) value |= Keys.Alt;
      // Note that "none" is also an allowed value.
      return value;
    }


    public class ColorSettings {
      public Color this[int index] {
        get => sColor[index];
        set {
          if (sColor[index] != value) {
            sColor[index] = value;
            raiseChanged();
          }
        }
      }
    }
  }
}