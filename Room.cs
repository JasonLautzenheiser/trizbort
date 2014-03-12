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
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PdfSharp.Drawing;

namespace Trizbort
{
    /// <summary>
    /// A room in the project.
    /// </summary>
    internal class Room : Element, IMoveable, ISizeable
    {

        public Room(Project project)
            : base(project)
        {
            Name = "Cave";
            Size = new Vector(3 * Settings.GridSize, 2 * Settings.GridSize);
            Position = new Vector(-Size.X / 2, -Size.Y / 2);

            // connections may connect to any of our "corners"
            PortList.Add(new CompassPort(CompassPoint.North, this));
            PortList.Add(new CompassPort(CompassPoint.NorthNorthEast, this));
            PortList.Add(new CompassPort(CompassPoint.NorthEast, this));
            PortList.Add(new CompassPort(CompassPoint.EastNorthEast, this));
            PortList.Add(new CompassPort(CompassPoint.East, this));
            PortList.Add(new CompassPort(CompassPoint.EastSouthEast, this));
            PortList.Add(new CompassPort(CompassPoint.SouthEast, this));
            PortList.Add(new CompassPort(CompassPoint.SouthSouthEast, this));
            PortList.Add(new CompassPort(CompassPoint.South, this));
            PortList.Add(new CompassPort(CompassPoint.SouthSouthWest, this));
            PortList.Add(new CompassPort(CompassPoint.SouthWest, this));
            PortList.Add(new CompassPort(CompassPoint.WestSouthWest, this));
            PortList.Add(new CompassPort(CompassPoint.West, this));
            PortList.Add(new CompassPort(CompassPoint.WestNorthWest, this));
            PortList.Add(new CompassPort(CompassPoint.NorthWest, this));
            PortList.Add(new CompassPort(CompassPoint.NorthNorthWest, this));
        }

        // Added this second constructor to be used when loading a room
        // This constructor is significantly faster as it doesn't look for gap in the element IDs
        public Room(Project project, int TotalIDs)
            : base(project, TotalIDs)
        {
            Name = "Cave";
            Size = new Vector(3 * Settings.GridSize, 2 * Settings.GridSize);
            Position = new Vector(-Size.X / 2, -Size.Y / 2);

            // connections may connect to any of our "corners"
            PortList.Add(new CompassPort(CompassPoint.North, this));
            PortList.Add(new CompassPort(CompassPoint.NorthNorthEast, this));
            PortList.Add(new CompassPort(CompassPoint.NorthEast, this));
            PortList.Add(new CompassPort(CompassPoint.EastNorthEast, this));
            PortList.Add(new CompassPort(CompassPoint.East, this));
            PortList.Add(new CompassPort(CompassPoint.EastSouthEast, this));
            PortList.Add(new CompassPort(CompassPoint.SouthEast, this));
            PortList.Add(new CompassPort(CompassPoint.SouthSouthEast, this));
            PortList.Add(new CompassPort(CompassPoint.South, this));
            PortList.Add(new CompassPort(CompassPoint.SouthSouthWest, this));
            PortList.Add(new CompassPort(CompassPoint.SouthWest, this));
            PortList.Add(new CompassPort(CompassPoint.WestSouthWest, this));
            PortList.Add(new CompassPort(CompassPoint.West, this));
            PortList.Add(new CompassPort(CompassPoint.WestNorthWest, this));
            PortList.Add(new CompassPort(CompassPoint.NorthWest, this));
            PortList.Add(new CompassPort(CompassPoint.NorthNorthWest, this));
        }

        public Vector Position
        {
            get { return m_position; }
            set
            {
                if (m_position != value)
                {
                    m_position = value;
                    ArbitraryAutomappedPosition = false;
                    RaiseChanged();
                }
            }
        }

        public float X
        {
            get { return m_position.X; }
        }

        public float Y
        {
            get { return m_position.Y; }
        }

        public Vector Size
        {
            get { return m_size; }
            set
            {
                if (m_size != value)
                {
                    m_size = value;
                    RaiseChanged();
                }
            }
        }

        public float Width
        {
            get { return m_size.X; }
        }

        public float Height
        {
            get { return m_size.Y; }
        }

        public Rect InnerBounds
        {
            get { return new Rect(Position, Size); }
        }

        public override float Distance(Vector pos, bool includeMargins)
        {
            var bounds = UnionBoundsWith(Rect.Empty, includeMargins);
            return pos.DistanceFromRect(bounds);
        }

        public override bool Intersects(Rect rect)
        {
            return InnerBounds.IntersectsWith(rect);
        }

        public override Vector GetPortPosition(Port port)
        {
            // map the compass points onto our bounding rectangle
            var compass = (CompassPort)port;
            return InnerBounds.GetCorner(compass.CompassPoint);
        }

        public override Vector GetPortStalkPosition(Port port)
        {
            var outerBounds = InnerBounds;
            outerBounds.Inflate(Settings.ConnectionStalkLength);
            var compass = (CompassPort)port;
            Vector inner = InnerBounds.GetCorner(compass.CompassPoint);
            Vector outer = outerBounds.GetCorner(compass.CompassPoint);
            switch (compass.CompassPoint)
            {
                case CompassPoint.EastNorthEast:
                case CompassPoint.EastSouthEast:
                case CompassPoint.WestNorthWest:
                case CompassPoint.WestSouthWest:
                    return new Vector(outer.X, inner.Y);
                case CompassPoint.NorthNorthEast:
                case CompassPoint.NorthNorthWest:
                case CompassPoint.SouthSouthEast:
                case CompassPoint.SouthSouthWest:
                    return new Vector(inner.X, outer.Y);
                default:
                    return outer;
            }
        }

        public Port PortAt(CompassPoint compassPoint)
        {
            foreach (CompassPort port in PortList)
            {
                if (port.CompassPoint == compassPoint)
                {
                    return port;
                }
            }
            return null;
        }

        /// <summary>
        /// Get/set the name of the room.
        /// </summary>
        public string Name
        {
            get { return m_name.Text; }
            set
            {
                value = value ?? string.Empty;
                if (m_name.Text != value)
                {
                    m_name.Text = value;
                    RaiseChanged();
                }
            }
        }

        /// <summary>
        /// Get/set whether the room is dark or lit.
        /// </summary>
        public bool IsDark
        {
            get { return m_isDark; }
            set
            {
                if (m_isDark != value)
                {
                    m_isDark = value;
                    RaiseChanged();
                }
            }
        }

        /// <summary>
        /// Get/set the list of objects in the room.
        /// </summary>
        public string Objects
        {
            get { return m_objects.Text; }
            set
            {
                value = value ?? string.Empty;
                if (m_objects.Text != value)
                {
                    m_objects.Text = value;
                    RaiseChanged();
                }
            }
        }

        /// <summary>
        /// Get/set the position, relative to the room,
        /// at which the object list is drawn on the map.
        /// </summary>
        public CompassPoint ObjectsPosition
        {
            get { return m_objectsPosition; }
            set
            {
                if (m_objectsPosition != value)
                {
                    m_objectsPosition = value;
                    RaiseChanged();
                }
            }
        }

        public override Depth Depth
        {
            get { return Depth.Medium; }
        }

        public override void PreDraw(DrawingContext context)
        {
            var topLeft = InnerBounds.GetCorner(CompassPoint.NorthWest);
            var topRight = InnerBounds.GetCorner(CompassPoint.NorthEast);
            var bottomLeft = InnerBounds.GetCorner(CompassPoint.SouthWest);
            var bottomRight = InnerBounds.GetCorner(CompassPoint.SouthEast);

            var topCenter = InnerBounds.GetCorner(CompassPoint.North);
            var rightCenter = InnerBounds.GetCorner(CompassPoint.East);
            var bottomCenter = InnerBounds.GetCorner(CompassPoint.South);
            var leftCenter = InnerBounds.GetCorner(CompassPoint.West);

            var top = new LineSegment(topLeft, topRight);
            var right = new LineSegment(topRight, bottomRight);
            var bottom = new LineSegment(bottomRight, bottomLeft);
            var left = new LineSegment(bottomLeft, topLeft);

            var halfTopRight = new LineSegment(topCenter, topRight);
            var halfBottomRight = new LineSegment(bottomRight, bottomCenter);
            var centerVertical = new LineSegment(bottomCenter, topCenter);

            var centerHorizontal = new LineSegment(leftCenter, rightCenter);
            var halfRightBottom = new LineSegment(rightCenter, bottomRight);
            var halfLeftBottom = new LineSegment(bottomLeft, leftCenter);

            var slantUp = new LineSegment(bottomLeft, topRight);
            var slantDown = new LineSegment(bottomRight, topLeft);

            context.LinesDrawn.Add(top);
            context.LinesDrawn.Add(right);
            context.LinesDrawn.Add(bottom);
            context.LinesDrawn.Add(left);
        }

        public override void Draw(XGraphics graphics, Palette palette, DrawingContext context)
        {
            Random random = new Random(Name.GetHashCode());

            var topLeft = InnerBounds.GetCorner(CompassPoint.NorthWest);
            var topRight = InnerBounds.GetCorner(CompassPoint.NorthEast);
            var bottomLeft = InnerBounds.GetCorner(CompassPoint.SouthWest);
            var bottomRight = InnerBounds.GetCorner(CompassPoint.SouthEast);

            var topCenter = InnerBounds.GetCorner(CompassPoint.North);
            var rightCenter = InnerBounds.GetCorner(CompassPoint.East);
            var bottomCenter = InnerBounds.GetCorner(CompassPoint.South);
            var leftCenter = InnerBounds.GetCorner(CompassPoint.West);

            var top = new LineSegment(topLeft, topRight);
            var right = new LineSegment(topRight, bottomRight);
            var bottom = new LineSegment(bottomRight, bottomLeft);
            var left = new LineSegment(bottomLeft, topLeft);

            var halfTopRight = new LineSegment(topCenter, topRight);
            var halfBottomRight = new LineSegment(bottomRight, bottomCenter);
            var centerVertical = new LineSegment(bottomCenter, topCenter);

            var centerHorizontal = new LineSegment(leftCenter, rightCenter);
            var halfRightBottom = new LineSegment(rightCenter, bottomRight);
            var halfLeftBottom = new LineSegment(bottomLeft, leftCenter);

            var slantUp = new LineSegment(bottomLeft, topRight);
            var slantDown = new LineSegment(bottomRight, topLeft);

            context.LinesDrawn.Add(top);
            context.LinesDrawn.Add(right);
            context.LinesDrawn.Add(bottom);
            context.LinesDrawn.Add(left);

            var brush = context.Selected ? palette.BorderBrush : palette.FillBrush;
            // Room specific fill brush (White shows global color)
            if (RoomFill != ColorTranslator.FromHtml("White") && RoomFill != ColorTranslator.FromHtml("#FFFFFF")) { brush = new SolidBrush(RoomFill); }

            if (!Settings.DebugDisableLineRendering)
            {

                var path = palette.Path();
                Drawing.AddLine(path, top, random);
                Drawing.AddLine(path, right, random);
                Drawing.AddLine(path, bottom, random);
                Drawing.AddLine(path, left, random);
                graphics.DrawPath(brush, path);

                // Second fill for room specific colors with a split option
                if (SecondFill != ColorTranslator.FromHtml("White") && SecondFill != ColorTranslator.FromHtml("#FFFFFF"))
                {
                    // Set the second fill color
                    brush = new SolidBrush(SecondFill);

                    // Define the second path based on the second fill location
                    var secondPath = palette.Path();
                    switch (SecondFillLocation)
                    {
                        case "Bottom":
                            Drawing.AddLine(secondPath, centerHorizontal, random);
                            Drawing.AddLine(secondPath, halfRightBottom, random);
                            Drawing.AddLine(secondPath, bottom, random);
                            Drawing.AddLine(secondPath, halfLeftBottom, random);
                            break;
                        case "BottomRight":
                            Drawing.AddLine(secondPath, slantUp, random);
                            Drawing.AddLine(secondPath, right, random);
                            Drawing.AddLine(secondPath, bottom, random);
                            break;
                        case "Right":
                            Drawing.AddLine(secondPath, halfTopRight, random);
                            Drawing.AddLine(secondPath, right, random);
                            Drawing.AddLine(secondPath, halfBottomRight, random);
                            Drawing.AddLine(secondPath, centerVertical, random);
                            break;
                        case "TopRight":
                            Drawing.AddLine(secondPath, top, random);
                            Drawing.AddLine(secondPath, right, random);
                            Drawing.AddLine(secondPath, slantDown, random);
                            break;
                        default:
                            break;
                    }
                    // Draw the second fill over the first
                    graphics.DrawPath(brush, secondPath);
                }

                if (IsDark)
                {
                    var state = graphics.Save();
                    graphics.IntersectClip(path);
                    brush = context.Selected ? palette.FillBrush : palette.BorderBrush;
                    // Room specific fill brush (White shows global color)
                    if (RoomBorder != ColorTranslator.FromHtml("White") && RoomBorder != ColorTranslator.FromHtml("#FFFFFF")) { brush = new SolidBrush(RoomBorder); }
                    graphics.DrawPolygon(brush, new PointF[] { topRight.ToPointF(), new PointF(topRight.X - Settings.DarknessStripeSize, topRight.Y), new PointF(topRight.X, topRight.Y + Settings.DarknessStripeSize) }, XFillMode.Alternate);
                    graphics.Restore(state);
                }

                if (RoomBorder == ColorTranslator.FromHtml("White") || RoomBorder == ColorTranslator.FromHtml("#FFFFFF"))
                {
                    graphics.DrawPath(palette.BorderPen, path);
                }
                else
                {
                    var RoomBorderPen = new Pen(RoomBorder, Settings.LineWidth);
                    RoomBorderPen.StartCap = LineCap.Round;
                    RoomBorderPen.EndCap = LineCap.Round;
                    graphics.DrawPath(RoomBorderPen, path);
                }
            }

            var font = Settings.LargeFont;
            brush = context.Selected ? palette.FillBrush : palette.LargeTextBrush;
            // Room specific fill brush (White shows global color)
            if (RoomLargeText != ColorTranslator.FromHtml("White") && RoomLargeText != ColorTranslator.FromHtml("#FFFFFF")) { brush = new SolidBrush(RoomLargeText); }

            Rect textBounds = InnerBounds;
            textBounds.Inflate(-5, -5);

            if (textBounds.Width > 0 && textBounds.Height > 0)
            {
                m_name.Draw(graphics, font, brush, textBounds.Position, textBounds.Size, XStringFormats.Center);
            }

            var expandedBounds = InnerBounds;
            expandedBounds.Inflate(Settings.ObjectListOffsetFromRoom, Settings.ObjectListOffsetFromRoom);
            var drawnObjectList = false;

            font = Settings.SmallFont;
            brush = palette.SmallTextBrush;
            // Room specific fill brush (White shows global color)
            if (RoomSmallText != ColorTranslator.FromHtml("White") && RoomSmallText != ColorTranslator.FromHtml("#FFFFFF")) { brush = new SolidBrush(RoomSmallText); }

            if (!string.IsNullOrEmpty(Objects))
            {
                XStringFormat format = new XStringFormat();
                Vector pos = expandedBounds.GetCorner(m_objectsPosition);
                if (!Drawing.SetAlignmentFromCardinalOrOrdinalDirection(format, m_objectsPosition))
                {
                    // object list appears inside the room below its name
                    format.LineAlignment = XLineAlignment.Far;
                    format.Alignment = XStringAlignment.Near;
                    //format.Trimming = StringTrimming.EllipsisCharacter;
                    //format.FormatFlags = StringFormatFlags.LineLimit;
                    var height = InnerBounds.Height / 2 - font.Height / 2;
                    var bounds = new Rect(InnerBounds.Left + Settings.ObjectListOffsetFromRoom, InnerBounds.Bottom - height, InnerBounds.Width - Settings.ObjectListOffsetFromRoom, height - Settings.ObjectListOffsetFromRoom);
                    brush = context.Selected ? palette.FillBrush : brush;
                    if (bounds.Width > 0 && bounds.Height > 0)
                    {
                        m_objects.Draw(graphics, font, brush, bounds.Position, bounds.Size, format);
                    }
                    drawnObjectList = true;
                }
                else if (m_objectsPosition == CompassPoint.North || m_objectsPosition == CompassPoint.South)
                {
                    pos.X += Settings.ObjectListOffsetFromRoom;
                }

                if (!drawnObjectList)
                {
                    m_objects.Draw(graphics, font, brush, pos, Vector.Zero, format);
                }
            }
        }

        public override Rect UnionBoundsWith(Rect rect, bool includeMargins)
        {
            var bounds = InnerBounds;
            if (includeMargins)
            {
                bounds.Inflate(Settings.LineWidth + Settings.ConnectionStalkLength);
            }
            return rect.Union(bounds);
        }

        public override bool HasDialog
        {
            get { return true; }
        }

        public override void ShowDialog()
        {
            using (var dialog = new RoomPropertiesDialog())
            {
                dialog.RoomName = Name;
                dialog.Description = PrimaryDescription;
                dialog.IsDark = IsDark;
                dialog.Objects = Objects;
                dialog.ObjectsPosition = ObjectsPosition;
                // Added for Room specific colors
                dialog.RoomFillColor = RoomFill;
                // Added for Room specific colors
                dialog.SecondFillColor = SecondFill;
                // Added for Room specific colors
                dialog.SecondFillLocation = SecondFillLocation;
                // Added for Room specific colors
                dialog.RoomBorderColor = RoomBorder;
                // Added for Room specific colors
                dialog.RoomTextColor = RoomLargeText;
                // Added for Room specific colors
                dialog.ObjectTextColor = RoomSmallText;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Name = dialog.RoomName;
                    if (PrimaryDescription != dialog.Description)
                    {
                        ClearDescriptions();
                        AddDescription(dialog.Description);
                    }
                    IsDark = dialog.IsDark;
                    Objects = dialog.Objects;
                    ObjectsPosition = dialog.ObjectsPosition;
                    // Added for Room specific colors
                    RoomFill = dialog.RoomFillColor;
                    // Added for Room specific colors
                    SecondFill = dialog.SecondFillColor;
                    // Added for Room specific colors
                    SecondFillLocation = dialog.SecondFillLocation;
                    // Added for Room specific colors
                    RoomBorder = dialog.RoomBorderColor;
                    // Added for Room specific colors
                    RoomLargeText = dialog.RoomTextColor;
                    // Added for Room specific colors
                    RoomSmallText = dialog.ObjectTextColor;
                }
            }
        }

        public void Save(XmlScribe scribe)
        {
            scribe.Attribute("name", Name);
            scribe.Attribute("x", Position.X);
            scribe.Attribute("y", Position.Y);
            scribe.Attribute("w", Size.X);
            scribe.Attribute("h", Size.Y);
            if (IsDark)
            {
                scribe.Attribute("isDark", IsDark);
            }
            scribe.Attribute("description", PrimaryDescription);
            // Added for room specific fill color, down to the next comment
            string rValue = "";
            string bValue = "";
            string gValue = "";
            if (RoomFill.R < 16)
            { rValue = "0" + RoomFill.R.ToString("X"); }
            else
            { rValue = RoomFill.R.ToString("X"); }
            if (RoomFill.G < 16)
            { gValue = "0" + RoomFill.G.ToString("X"); }
            else
            { gValue = RoomFill.G.ToString("X"); }
            if (RoomFill.B < 16)
            { bValue = "0" + RoomFill.B.ToString("X"); }
            else
            { bValue = RoomFill.B.ToString("X"); }

            string colorValue = "#" + rValue + "" + gValue + "" + bValue;
            scribe.Attribute("roomFill", colorValue);

            if (SecondFill.R < 16)
            { rValue = "0" + SecondFill.R.ToString("X"); }
            else
            { rValue = SecondFill.R.ToString("X"); }
            if (SecondFill.G < 16)
            { gValue = "0" + SecondFill.G.ToString("X"); }
            else
            { gValue = SecondFill.G.ToString("X"); }
            if (SecondFill.B < 16)
            { bValue = "0" + SecondFill.B.ToString("X"); }
            else
            { bValue = SecondFill.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            scribe.Attribute("secondFill", colorValue);
            scribe.Attribute("secondFillLocation", SecondFillLocation);

            if (RoomBorder.R < 16)
            { rValue = "0" + RoomBorder.R.ToString("X"); }
            else
            { rValue = RoomBorder.R.ToString("X"); }
            if (RoomBorder.G < 16)
            { gValue = "0" + RoomBorder.G.ToString("X"); }
            else
            { gValue = RoomBorder.G.ToString("X"); }
            if (RoomBorder.B < 16)
            { bValue = "0" + RoomBorder.B.ToString("X"); }
            else
            { bValue = RoomBorder.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            scribe.Attribute("roomBorder", colorValue);

            if (RoomLargeText.R < 16)
            { rValue = "0" + RoomLargeText.R.ToString("X"); }
            else
            { rValue = RoomLargeText.R.ToString("X"); }
            if (RoomLargeText.G < 16)
            { gValue = "0" + RoomLargeText.G.ToString("X"); }
            else
            { gValue = RoomLargeText.G.ToString("X"); }
            if (RoomLargeText.B < 16)
            { bValue = "0" + RoomLargeText.B.ToString("X"); }
            else
            { bValue = RoomLargeText.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            scribe.Attribute("roomLargeText", colorValue);

            if (RoomSmallText.R < 16)
            { rValue = "0" + RoomSmallText.R.ToString("X"); }
            else
            { rValue = RoomSmallText.R.ToString("X"); }
            if (RoomSmallText.G < 16)
            { gValue = "0" + RoomSmallText.G.ToString("X"); }
            else
            { gValue = RoomSmallText.G.ToString("X"); }
            if (RoomSmallText.B < 16)
            { bValue = "0" + RoomSmallText.B.ToString("X"); }
            else
            { bValue = RoomSmallText.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            scribe.Attribute("roomSmallText", colorValue);
            // Up to this point was added to turn colors to Hex code for xmpl saving/loading

            if (!string.IsNullOrEmpty(Objects) || ObjectsPosition != DefaultObjectsPosition)
            {
                scribe.StartElement("objects");
                if (ObjectsPosition != DefaultObjectsPosition)
                {
                    scribe.Attribute("at", ObjectsPosition);
                }
                if (!string.IsNullOrEmpty(Objects))
                {
                    scribe.Value(Objects.Replace("\r", string.Empty).Replace("|", "\\|").Replace("\n", "|"));
                }
                scribe.EndElement();
            }
        }

        public void Load(XmlElementReader element)
        {
            Name = element.Attribute("name").Text;
            ClearDescriptions();
            AddDescription(element.Attribute("description").Text);
            Position = new Vector(element.Attribute("x").ToFloat(), element.Attribute("y").ToFloat());
            Size = new Vector(element.Attribute("w").ToFloat(), element.Attribute("h").ToFloat());
            IsDark = element.Attribute("isDark").ToBool();
            if (element.Attribute("roomFill").Text != "") { RoomFill = ColorTranslator.FromHtml(element.Attribute("roomFill").Text); }
            if (element.Attribute("secondFill").Text != "") { SecondFill = ColorTranslator.FromHtml(element.Attribute("secondFill").Text); }
            if (element.Attribute("secondFillLocation").Text != "") { SecondFillLocation = element.Attribute("secondFillLocation").Text; }
            if (element.Attribute("roomBorder").Text != "") { RoomBorder = ColorTranslator.FromHtml(element.Attribute("roomBorder").Text); }
            if (element.Attribute("roomLargeText").Text != "") { RoomLargeText = ColorTranslator.FromHtml(element.Attribute("roomLargeText").Text); }
            if (element.Attribute("roomSmallText").Text != "") { RoomSmallText = ColorTranslator.FromHtml(element.Attribute("roomSmallText").Text); }
            Objects = element["objects"].Text.Replace("|", "\r\n").Replace("\\\r\n", "|");
            ObjectsPosition = element["objects"].Attribute("at").ToCompassPoint(ObjectsPosition);
        }

        public bool IsConnected
        {
            get
            {
                // TODO: This is needlessly expensive
                foreach (var element in Project.Current.Elements)
                {
                    if (!(element is Connection))
                    {
                        continue;
                    }

                    var connection = (Connection)element;
                    foreach (var vertex in connection.VertexList)
                    {
                        var port = vertex.Port;
                        if (port != null && port.Owner == this)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public List<Connection> GetConnections(CompassPoint compassPoint)
        {
            var connections = new List<Connection>();

            // TODO: This is needlessly expensive, traversing as it does the entire project's element list.
            foreach (var element in Project.Current.Elements)
            {
                if (!(element is Connection))
                    continue;

                var connection = (Connection)element;
                foreach (var vertex in connection.VertexList)
                {
                    var port = vertex.Port;
                    if (port == null || port.Owner != this || !(port is CompassPort))
                        continue;

                    var compassPort = (CompassPort)vertex.Port;
                    if (compassPort.CompassPoint == compassPoint)
                    {
                        // found a connection with a vertex joined to our room's port at the given compass point
                        connections.Add(connection);
                    }
                }
            }
            return connections;
        }

        internal class CompassPort : Port
        {
            public CompassPort(CompassPoint compassPoint, Room room)
                : base(room)
            {
                CompassPoint = compassPoint;
                Room = room;
            }

            public CompassPoint CompassPoint
            {
                get;
                private set;
            }

            public override string ID
            {
                get
                {
                    string name;
                    if (CompassPointHelper.ToName(CompassPoint, out name))
                    {
                        return name;
                    }
                    return string.Empty;
                }
            }

            public Room Room
            {
                get;
                private set;
            }
        }

        public bool ArbitraryAutomappedPosition
        {
            get;
            set;
        }

        public string PrimaryDescription
        {
            get
            {
                if (m_descriptions.Count > 0)
                {
                    return m_descriptions[0];
                }
                return null;
            }
        }

        public void ClearDescriptions()
        {
            if (m_descriptions.Count > 0)
            {
                m_descriptions.Clear();
                RaiseChanged();
            }
        }

        public bool HasDescription
        {
            get { return m_descriptions.Count > 0; }
        }

        public void AddDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                // never add an empty description
                return;
            }

            foreach (var existing in m_descriptions)
            {
                if (existing == description)
                {
                    // we already have this description
                    return;
                }
            }

            // we don't have this (non-empty) description already; add it
            m_descriptions.Add(description);
            RaiseChanged();
        }

        public bool MatchDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                // match a lack of description if we have no descriptions
                return m_descriptions.Count == 0;
            }

            foreach (var existing in m_descriptions)
            {
                if (existing == description)
                {
                    // match a description if we have it
                    return true;
                }
            }

            // no match
            return false;
        }

        public String ClipboardPrint()
        {
            String clipboardText = "";
            clipboardText += Name + ":";
            clipboardText += Position.X + ":";
            clipboardText += Position.Y + ":";
            clipboardText += Size.X + ":";
            clipboardText += Size.Y + ":";
            clipboardText += IsDark + ":";
            clipboardText += PrimaryDescription + ":";

            string rValue = "";
            string bValue = "";
            string gValue = "";
            if (RoomFill.R < 16)
            { rValue = "0" + RoomFill.R.ToString("X"); }
            else
            { rValue = RoomFill.R.ToString("X"); }
            if (RoomFill.G < 16)
            { gValue = "0" + RoomFill.G.ToString("X"); }
            else
            { gValue = RoomFill.G.ToString("X"); }
            if (RoomFill.B < 16)
            { bValue = "0" + RoomFill.B.ToString("X"); }
            else
            { bValue = RoomFill.B.ToString("X"); }

            string colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue + ":";

            if (SecondFill.R < 16)
            { rValue = "0" + SecondFill.R.ToString("X"); }
            else
            { rValue = SecondFill.R.ToString("X"); }
            if (SecondFill.G < 16)
            { gValue = "0" + SecondFill.G.ToString("X"); }
            else
            { gValue = SecondFill.G.ToString("X"); }
            if (SecondFill.B < 16)
            { bValue = "0" + SecondFill.B.ToString("X"); }
            else
            { bValue = SecondFill.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue + ":";
            clipboardText += SecondFillLocation + ":";

            if (RoomBorder.R < 16)
            { rValue = "0" + RoomBorder.R.ToString("X"); }
            else
            { rValue = RoomBorder.R.ToString("X"); }
            if (RoomBorder.G < 16)
            { gValue = "0" + RoomBorder.G.ToString("X"); }
            else
            { gValue = RoomBorder.G.ToString("X"); }
            if (RoomBorder.B < 16)
            { bValue = "0" + RoomBorder.B.ToString("X"); }
            else
            { bValue = RoomBorder.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue + ":";

            if (RoomLargeText.R < 16)
            { rValue = "0" + RoomLargeText.R.ToString("X"); }
            else
            { rValue = RoomLargeText.R.ToString("X"); }
            if (RoomLargeText.G < 16)
            { gValue = "0" + RoomLargeText.G.ToString("X"); }
            else
            { gValue = RoomLargeText.G.ToString("X"); }
            if (RoomLargeText.B < 16)
            { bValue = "0" + RoomLargeText.B.ToString("X"); }
            else
            { bValue = RoomLargeText.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue + ":";

            if (RoomSmallText.R < 16)
            { rValue = "0" + RoomSmallText.R.ToString("X"); }
            else
            { rValue = RoomSmallText.R.ToString("X"); }
            if (RoomSmallText.G < 16)
            { gValue = "0" + RoomSmallText.G.ToString("X"); }
            else
            { gValue = RoomSmallText.G.ToString("X"); }
            if (RoomSmallText.B < 16)
            { bValue = "0" + RoomSmallText.B.ToString("X"); }
            else
            { bValue = RoomSmallText.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue;

            if (!string.IsNullOrEmpty(Objects) || ObjectsPosition != DefaultObjectsPosition)
            {
                clipboardText += ":" + ObjectsPosition + ":";
                if (!string.IsNullOrEmpty(Objects))
                {
                    clipboardText += (Objects.Replace("\r\n", ":"));
                }
            }

            return clipboardText;
        }

        public String ClipboardColorPrint()
        {
            String clipboardText = "";

            string rValue = "";
            string bValue = "";
            string gValue = "";
            if (RoomFill.R < 16)
            { rValue = "0" + RoomFill.R.ToString("X"); }
            else
            { rValue = RoomFill.R.ToString("X"); }
            if (RoomFill.G < 16)
            { gValue = "0" + RoomFill.G.ToString("X"); }
            else
            { gValue = RoomFill.G.ToString("X"); }
            if (RoomFill.B < 16)
            { bValue = "0" + RoomFill.B.ToString("X"); }
            else
            { bValue = RoomFill.B.ToString("X"); }

            string colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue + ":";

            if (SecondFill.R < 16)
            { rValue = "0" + SecondFill.R.ToString("X"); }
            else
            { rValue = SecondFill.R.ToString("X"); }
            if (SecondFill.G < 16)
            { gValue = "0" + SecondFill.G.ToString("X"); }
            else
            { gValue = SecondFill.G.ToString("X"); }
            if (SecondFill.B < 16)
            { bValue = "0" + SecondFill.B.ToString("X"); }
            else
            { bValue = SecondFill.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue + ":";
            clipboardText += SecondFillLocation + ":";

            if (RoomBorder.R < 16)
            { rValue = "0" + RoomBorder.R.ToString("X"); }
            else
            { rValue = RoomBorder.R.ToString("X"); }
            if (RoomBorder.G < 16)
            { gValue = "0" + RoomBorder.G.ToString("X"); }
            else
            { gValue = RoomBorder.G.ToString("X"); }
            if (RoomBorder.B < 16)
            { bValue = "0" + RoomBorder.B.ToString("X"); }
            else
            { bValue = RoomBorder.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue + ":";

            if (RoomLargeText.R < 16)
            { rValue = "0" + RoomLargeText.R.ToString("X"); }
            else
            { rValue = RoomLargeText.R.ToString("X"); }
            if (RoomLargeText.G < 16)
            { gValue = "0" + RoomLargeText.G.ToString("X"); }
            else
            { gValue = RoomLargeText.G.ToString("X"); }
            if (RoomLargeText.B < 16)
            { bValue = "0" + RoomLargeText.B.ToString("X"); }
            else
            { bValue = RoomLargeText.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue + ":";

            if (RoomSmallText.R < 16)
            { rValue = "0" + RoomSmallText.R.ToString("X"); }
            else
            { rValue = RoomSmallText.R.ToString("X"); }
            if (RoomSmallText.G < 16)
            { gValue = "0" + RoomSmallText.G.ToString("X"); }
            else
            { gValue = RoomSmallText.G.ToString("X"); }
            if (RoomSmallText.B < 16)
            { bValue = "0" + RoomSmallText.B.ToString("X"); }
            else
            { bValue = RoomSmallText.B.ToString("X"); }

            colorValue = "#" + rValue + "" + gValue + "" + bValue;
            clipboardText += colorValue;

            return clipboardText;
        }

        // Added for Room specific colors
        public Color RoomFill
        {
            get { return m_roomfill; }
            set
            {
                if (m_roomfill != value)
                {
                    m_roomfill = value;
                    RaiseChanged();
                }
            }
        }

        // Added for Room specific colors
        public Color SecondFill
        {
            get { return m_secondfill; }
            set
            {
                if (m_secondfill != value)
                {
                    m_secondfill = value;
                    RaiseChanged();
                }
            }
        }

        // Added for Room specific colors
        public String SecondFillLocation
        {
            get { return m_secondfilllocation; }
            set
            {
                if (m_secondfilllocation != value)
                {
                    m_secondfilllocation = value;
                    RaiseChanged();
                }
            }
        }

        // Added for Room specific colors
        public Color RoomBorder
        {
            get { return m_roomborder; }
            set
            {
                if (m_roomborder != value)
                {
                    m_roomborder = value;
                    RaiseChanged();
                }
            }
        }

        // Added for Room specific colors
        public Color RoomLargeText
        {
            get { return m_roomlargetext; }
            set
            {
                if (m_roomlargetext != value)
                {
                    m_roomlargetext = value;
                    RaiseChanged();
                }
            }
        }

        // Added for Room specific colors
        public Color RoomSmallText
        {
            get { return m_roomsmalltext; }
            set
            {
                if (m_roomsmalltext != value)
                {
                    m_roomsmalltext = value;
                    RaiseChanged();
                }
            }
        }

        private Vector m_position;
        private Vector m_size;
        private TextBlock m_name = new TextBlock();
        private bool m_isDark = false;
        private TextBlock m_objects = new TextBlock();
        private static readonly CompassPoint DefaultObjectsPosition = CompassPoint.South;
        private CompassPoint m_objectsPosition = DefaultObjectsPosition;
        private List<string> m_descriptions = new List<string>();
        // Added for Room specific colors (White shows global color)
        private Color m_roomfill = ColorTranslator.FromHtml("White");
        private Color m_secondfill = ColorTranslator.FromHtml("White");
        private String m_secondfilllocation = "Bottom";
        private Color m_roomborder = ColorTranslator.FromHtml("White");
        private Color m_roomlargetext = ColorTranslator.FromHtml("White");
        private Color m_roomsmalltext = ColorTranslator.FromHtml("White");
    }
}
