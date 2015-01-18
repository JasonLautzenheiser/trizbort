/*
    Copyright (c) 2010 by Genstein

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
using System.Windows.Forms;
using System.Globalization;
using System.Text;
using System.IO;
using System.Xml;

namespace Trizbort
{
    public static class Settings
    {
        static Settings()
        {
            Color = new ColorSettings();
            Regions = new List<Region>
            {
                new Region() {RegionName = Region.DefaultRegion, RColor = System.Drawing.Color.White}
            };
            RecentProjects = new MruList();
            Reset();
            ResetApplicationSettings();
            LoadApplicationSettings();
        }

        static void ResetApplicationSettings()
        {
            DontCareAboutVersion = new Version(0, 0, 0, 0);
            s_automap = AutomapSettings.Default;
            InfiniteScrollBounds = false;
            ShowMiniMap = true;
            SaveAt100 = true;
            RecentProjects.Clear();
            // TODO: add other application settings here
        }

        static void LoadApplicationSettings()
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

                        LastProjectFileName = root["lastProjectFileName"].Text;
                        LastExportImageFileName = root["lastExportedImageFileName"].Text;
                        LastExportInform7FileName = root["lastExportedInform7FileName"].Text;
                        LastExportInform6FileName = root["lastExportedInform6FileName"].Text;
                        LastExportTadsFileName = root["lastExportedTadsFileName"].Text;

                        InvertMouseWheel = root["invertMouseWheel"].ToBool(InvertMouseWheel);
                        DefaultImageType = root["defaultImageType"].ToInt(DefaultImageType);
                        SaveAt100 = root["saveAt100"].ToBool(SaveAt100);

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
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        static string ApplicationSettingsPath
        {
            get
            {
                return Path.Combine(Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Genstein"), "Trizbort"), "Settings.xml");
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
                    scribe.Element("invertMouseWheel",InvertMouseWheel);
                    scribe.Element("defaultImageType", DefaultImageType);
                    scribe.Element("saveAt100",SaveAt100);

                    scribe.Element("lastProjectFileName", LastProjectFileName);
                    scribe.Element("lastExportedImageFileName", LastExportImageFileName);
                    scribe.Element("lastExportedInform7FileName", LastExportInform7FileName);
                    scribe.Element("lastExportedInform6FileName", LastExportInform6FileName);
                    scribe.Element("lastExportedTadsFileName", LastExportTadsFileName);

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
                    scribe.EndElement();
                }
            }
            catch (Exception)
            {
            }
        }

        public static ColorSettings Color { get; private set; }
        public static List<Region> Regions { get; private set; }

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
            get { return s_handDrawn; }
            set
            {
                if (s_handDrawn != value)
                {
                    s_handDrawn = value;
                    RaiseChanged();
                }
            }
        }

        public static bool HandDrawnUnchecked
        {
            get { return s_handDrawn; }
            set { s_handDrawn = value; }
        }

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
        /// Get/set the modifier keys required, along with a numeric keypad key, 
        /// to create new rooms from the currently selected room.
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
        /// Get/set the modifier keys required, along with a numeric keypad key, 
        /// to mark "unexplored" connections from the currently selected room.
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

        public static float Snap(float value)
        {
            float offset = 0;
            while (value < GridSize)
            {
                value += GridSize;
                offset += GridSize;
            }

            var mod = value % GridSize;
            if (SnapToGrid && mod != 0)
            {
                if (mod < GridSize / 2)
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
                for (int index = 0; index < Colors.Count; ++index)
                {
                    dialog.Color[index] = Color[index];
                }

                dialog.Title = Project.Current.Title;
                dialog.Author = Project.Current.Author;
                dialog.Description = Project.Current.Description;
                dialog.History = Project.Current.History;
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
                dialog.ConnectionArrowSize = ConnectionArrowSize;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    for (int index = 0; index < Colors.Count; ++index)
                    {
                        Color[index] = dialog.Color[index];
                    }
                    Project.Current.Title = dialog.Title;
                    Project.Current.Author = dialog.Author;
                    Project.Current.Description = dialog.Description;
                    Project.Current.History = dialog.History;
                    LargeFont = dialog.LargeFont;
                    SmallFont = dialog.SmallFont;
                    LineFont = dialog.LineFont;
                    HandDrawn = dialog.HandDrawn;
                    LineWidth = dialog.LineWidth;
                    SnapToGrid = dialog.SnapToGrid;
                    GridSize = dialog.GridSize;
                    IsGridVisible = dialog.IsGridVisible;
                    ShowOrigin = dialog.ShowOrigin;
                    DarknessStripeSize = dialog.DarknessStripeSize;
                    ObjectListOffsetFromRoom = dialog.ObjectListOffsetFromRoom;
                    ConnectionStalkLength = dialog.ConnectionStalkLength;
                    PreferredDistanceBetweenRooms = dialog.PreferredDistanceBetweenRooms;
                    TextOffsetFromConnection = dialog.TextOffsetFromConnection;
                    HandleSize = dialog.HandleSize;
                    SnapToElementSize = dialog.SnapToElementSize;
                    ConnectionArrowSize = dialog.ConnectionArrowSize;
                }
            }
        }

        public static void Reset()
        {
            Color[Colors.Canvas] = System.Drawing.Color.White;
            Color[Colors.Fill] = System.Drawing.Color.White;
            Color[Colors.Border] = System.Drawing.Color.MidnightBlue;
            Color[Colors.Line] = System.Drawing.Color.MidnightBlue;
            Color[Colors.HoverLine] = System.Drawing.Color.DarkOrange;
            Color[Colors.SelectedLine] = System.Drawing.Color.SteelBlue;
            Color[Colors.LargeText] = System.Drawing.Color.MidnightBlue;
            Color[Colors.SmallText] = System.Drawing.Color.MidnightBlue;
            Color[Colors.LineText] = System.Drawing.Color.MidnightBlue;
            Color[Colors.Grid] = Drawing.Mix(System.Drawing.Color.White, System.Drawing.Color.Black, 25, 1);

            LargeFont = new Font("Comic Sans MS", 13.0f, FontStyle.Regular, GraphicsUnit.World);
            SmallFont = new Font("Comic Sans MS", 11.0f, FontStyle.Regular, GraphicsUnit.World);
            LineFont = new Font("Comic Sans MS", 9.0f, FontStyle.Regular, GraphicsUnit.World);

            LineWidth = 2.0f;
            HandDrawn = true;

            SnapToGrid = true;
            IsGridVisible = true;
            GridSize = 32.0f;
            ShowOrigin = true;

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

            Regions = new List<Region>
            {
                new Region() {RegionName = Region.DefaultRegion, RColor = System.Drawing.Color.White}
            };
        }

        public static void Save(XmlScribe scribe)
        {
            // save colors
            scribe.StartElement("colors");
            for (int index = 0; index < Colors.Count; ++index)
            {
                string colorName;
                if (Colors.ToName(index, out colorName))
                {
                    scribe.Element(colorName, Color[index]);
                }
            }
            scribe.EndElement();

            scribe.StartElement("regions");
            foreach (var region in Regions)
            {
                scribe.StartElement(region.FixupRegionNameForSave());
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
            scribe.Element("handDrawn", s_handDrawn);
            scribe.Element("arrowSize", s_connectionArrowSize);
            scribe.Element("textOffset", s_textOffsetFromConnection);
            scribe.EndElement();

            scribe.StartElement("rooms");
            scribe.Element("darknessStripeSize", s_darknessStripeSize);
            scribe.Element("objectListOffset", s_objectListOffsetFromRoom);
            scribe.Element("connectionStalkLength", s_connectionStalkLength);
            scribe.Element("preferredDistanceBetweenRooms", s_preferredDistanceBetweenRooms);
            scribe.EndElement();

            scribe.StartElement("ui");
            scribe.Element("handleSize", s_handleSize);
            scribe.Element("snapToElementSize", s_snapToElementSize);
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

            Keys value = Keys.None;
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
                Regions.Add(new Region(){RColor = System.Drawing.Color.White, TextColor = System.Drawing.Color.Blue, RegionName = Region.DefaultRegion});
            else
            {
                foreach (var region in regions.Children)
                {
                    var tRegion = new Region();
                    tRegion.TextColor = region.Attribute("TextColor").Text == string.Empty ? System.Drawing.Color.Blue : ColorTranslator.FromHtml(region.Attribute("TextColor").Text);
                    tRegion.RegionName = region.Name;
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
            HandDrawn = element["lines"]["handDrawn"].ToBool(s_handDrawn);
            ConnectionArrowSize = element["lines"]["arrowSize"].ToFloat(s_connectionArrowSize);
            TextOffsetFromConnection = element["lines"]["textOffset"].ToFloat(s_textOffsetFromConnection);

            DarknessStripeSize = element["rooms"]["darknessStripeSize"].ToFloat(s_darknessStripeSize);
            ObjectListOffsetFromRoom = element["rooms"]["objectListOffset"].ToFloat(s_objectListOffsetFromRoom);
            ConnectionStalkLength = element["rooms"]["connectionStalkLength"].ToFloat(s_connectionStalkLength);
            PreferredDistanceBetweenRooms = element["rooms"]["preferredDistanceBetweenRooms"].ToFloat(s_connectionStalkLength * 2); // introduced in v1.2, hence default based on existing setting

            HandleSize = element["ui"]["handleSize"].ToFloat(s_handleSize);
            SnapToElementSize = element["ui"]["snapToElementSize"].ToFloat(s_snapToElementSize);

            KeypadNavigationCreationModifier = StringToModifierKeys(element["keypadNavigation"]["creationModifier"].Text, s_keypadNavigationCreationModifier);
            KeypadNavigationUnexploredModifier = StringToModifierKeys(element["keypadNavigation"]["unexploredModifier"].Text, s_keypadNavigationUnexploredModifier);
        }

        
        public class ColorSettings
        {
            public Color this[int index]
            {
                get { return Settings.s_color[index]; }
                set
                {
                    if (Settings.s_color[index] != value)
                    {
                        Settings.s_color[index] = value;
                        Settings.RaiseChanged();
                    }
                }
            }
        }


        public static bool DebugShowFPS {get; set;}
        public static bool DebugShowMouseCoordinates { get; set; }
        public static bool DebugDisableTextRendering { get; set; }
        public static bool DebugDisableLineRendering { get; set; }
        public static bool DebugDisableElementRendering { get; set; }
        public static bool DebugDisableGridPolyline { get; set; }

        public static bool SaveAt100 { get; set; }
        public static int MouseDragButton { get; set; }
        public static int DefaultImageType { get; set; }
        public static bool InvertMouseWheel { get; set; }

        public static Version DontCareAboutVersion { get; set; }

        public static AutomapSettings Automap
        {
            get { return s_automap; }
            set { s_automap = value; }
        }

        public static bool InfiniteScrollBounds { get; set; }
        public static bool ShowMiniMap { get; set; }
        public static string LastProjectFileName { get; set; }
        public static string LastExportImageFileName { get; set; }
        public static string LastExportInform7FileName { get; set; }
        public static string LastExportInform6FileName { get; set; }
        public static string LastExportTadsFileName { get; set; }
        public static MruList RecentProjects { get; private set; }

        private static readonly float MinFontSize = 2;
        private static readonly float MaxFontSize = 256;

        // per-map settings, saved with the map
        private static Color[] s_color = new Color[Colors.Count];
        private static List<Region> s_regionColor = new List<Region>();
        private static Font s_largeFont;
        private static Font s_smallFont;
        private static Font s_lineFont;
        private static float s_lineWidth;
        private static bool s_handDrawn;
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
        private static float s_dragDistanceToInitiateNewConnection;
        private static float s_connectionArrowSize;
        private static Keys s_keypadNavigationCreationModifier;
        private static Keys s_keypadNavigationUnexploredModifier;

        // application settings, saved for the user
        private static AutomapSettings s_automap;

        public static void ShowAppDialog()
        {
            using (var dialog = new AppSettingsDialog())
            {
                dialog.InvertMouseWheel = InvertMouseWheel;
                dialog.DefaultImageType = DefaultImageType;
                dialog.SaveAt100 = SaveAt100;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    InvertMouseWheel = dialog.InvertMouseWheel;
                    DefaultImageType = dialog.DefaultImageType;
                    SaveAt100 = dialog.SaveAt100;
                }
            }
        }
    }
}
