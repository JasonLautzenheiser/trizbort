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
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using Trizbort.Domain.Application;
using Trizbort.Domain.AppSettings;
using Trizbort.Domain.Misc;
using Trizbort.Properties;
using Trizbort.UI;
using Trizbort.Util;
using Settings = Trizbort.Setup.Settings;

namespace Trizbort.Domain.Elements {
  /// <summary>
  ///   A connection between elements or points in space.
  /// </summary>
  /// <remarks>
  ///   Connections are multi-segment lines between vertices.
  ///   Each vertex is fixed either to a point in space or
  ///   to an element's port.
  /// </remarks>
  [SuppressMessage("ReSharper", "CanBeReplacedWithTryCastAndCheckForNull")]
  public class Connection : Element {
    public const string Up = "up";
    public const string Down = "down";
    public const string In = "in";
    public const string Out = "out";
    private const ConnectionStyle DEFAULT_STYLE = ConnectionStyle.Solid;
    private const ConnectionFlow DEFAULT_FLOW = ConnectionFlow.TwoWay;
    private readonly TextBlock mEndText = new TextBlock();
    private readonly TextBlock mMidText = new TextBlock();
    private readonly List<LineSegment> mSmartSegments = new List<LineSegment>();
    private readonly TextBlock mStartText = new TextBlock();
    private string description = string.Empty;
    private Door door;
    private Color mConnectionColor = Color.Transparent;
    private ConnectionFlow mFlow = DEFAULT_FLOW;
    private ConnectionStyle mStyle = DEFAULT_STYLE;
    private string name = string.Empty;

    public Connection() { }

    public Connection(Project project) : base(project) {
      initEvents();
    }

    // Added this second constructor to be used when loading a room
    // This constructor is significantly faster as it doesn't look for gap in the element IDs
    public Connection(Project project, int totalIDs) : base(project, totalIDs) {
      initEvents();
    }

    public Connection(Project project, Vertex a, Vertex b)
      : this(project) {
      VertexList.Add(a);
      VertexList.Add(b);
    }

    // Added to ignore ID gaps
    public Connection(Project project, Vertex a, Vertex b, int totalIDs)
      : this(project, totalIDs) {
      VertexList.Add(a);
      VertexList.Add(b);
    }

    public Connection(Project project, Vertex a, Vertex b, params Vertex[] args)
      : this(project, a, b) {
      foreach (var vertex in args)
        VertexList.Add(vertex);
    }

    // Added to ignore ID gaps
    public Connection(Project project, Vertex a, Vertex b, int totalIDs, params Vertex[] args)
      : this(project, a, b, totalIDs) {
      foreach (var vertex in args)
        VertexList.Add(vertex);
    }

    public Color ConnectionColor {
      get => mConnectionColor;
      set {
        if (mConnectionColor != value) {
          mConnectionColor = value;
          RaiseChanged();
        }
      }
    }

    public override Depth Depth => Depth.High;

    public string Description {
      get => description;
      set {
        if (description != value) {
          description = value;
          RaiseChanged();
        }
      }
    }

    public Door Door {
      get => door;
      set {
        if (door != value) {
          door = value;
          RaiseChanged();
        }
      }
    }

    public string EndText {
      get => mEndText.Text;
      set {
        if (mEndText.Text != value) {
          mEndText.Text = value;
          RaiseChanged();
        }
      }
    }

    public ConnectionFlow Flow {
      get => mFlow;
      set {
        if (mFlow != value) {
          mFlow = value;
          RaiseChanged();
        }
      }
    }

    public override bool HasDialog => true;

    public string MidText {
      get => mMidText.Text;
      set {
        if (mMidText.Text != value) {
          mMidText.Text = value;
          RaiseChanged();
        }
      }
    }

    public override string Name {
      get => name;
      set {
        if (name != value) {
          name = value;
          RaiseChanged();
        }
      }
    }

    public string StartText {
      get => mStartText.Text;
      set {
        if (mStartText.Text != value) {
          mStartText.Text = value;
          RaiseChanged();
        }
      }
    }

    public ConnectionStyle Style {
      get => mStyle;
      set {
        if (mStyle != value) {
          mStyle = value;
          RaiseChanged();
        }
      }
    }

    [JsonIgnore]
    public BoundList<Vertex> VertexList { get; set; } = new BoundList<Vertex>();

    public object BeginLoad(XmlElementReader element) {
      if (element.Attribute("door").Text == "yes")
        Door = new Door {
          Lockable = element.Attribute("lockable").Text == "yes",
          Locked = element.Attribute("locked").Text == "yes",
          Open = element.Attribute("open").Text == "yes",
          Openable = element.Attribute("openable").Text == "yes"
        };

      switch (element.Attribute("style").Text) {
        default:
          Style = ConnectionStyle.Solid;
          break;
        case "dashed":
          Style = ConnectionStyle.Dashed;
          break;
      }

      switch (element.Attribute("flow").Text) {
        default:
          Flow = ConnectionFlow.TwoWay;
          break;
        case "oneWay":
          Flow = ConnectionFlow.OneWay;
          break;
      }

      Name = element.Attribute("name").Text;
      Description = element.Attribute("description").Text;
      StartText = element.Attribute("startText").Text;
      MidText = element.Attribute("midText").Text;
      EndText = element.Attribute("endText").Text;
      if (element.Attribute("color").Text != "") ConnectionColor = ColorTranslator.FromHtml(element.Attribute("color").Text);

      var vertexElementList = new List<XmlElementReader>();
      vertexElementList.AddRange(element.Children);
      vertexElementList.Sort((a, b) => a.Attribute("index").ToInt().CompareTo(b.Attribute("index").ToInt()));

      foreach (var vertexElement in vertexElementList)
        if (vertexElement.HasName("point")) {
          var vertex = new Vertex {Position = new Vector(vertexElement.Attribute("x").ToFloat(), vertexElement.Attribute("y").ToFloat())};
          VertexList.Add(vertex);
        } else if (vertexElement.HasName("dock")) {
          var vertex = new Vertex();
          // temporarily leave this vertex as a positional vertex;
          // we can't safely dock it to a port until EndLoad().
          VertexList.Add(vertex);
        }

      return vertexElementList;
    }

    public int ConnectedRoomToRotate(bool whichRoom) {
      //first, let's take care of cases where the right room is forced, if there is one
      if (VertexList[0].Port == null && VertexList[0].Port == null) return -1;
      if (VertexList[1].Port == null) return 0;
      if (VertexList[0].Port == null) return 1;

      var firstRoom = (Room) VertexList[0].Port.Owner;
      var secondRoom = (Room) VertexList[1].Port.Owner;

      var firstCenterY = firstRoom.Y + firstRoom.Height / 2;
      var secondCenterY = secondRoom.Y + secondRoom.Height / 2;

      if (firstCenterY < secondCenterY) return whichRoom ? 0 : 1;
      if (firstCenterY > secondCenterY) return whichRoom ? 1 : 0;

      var firstCenterX = firstRoom.Position.X + firstRoom.Height / 2;
      var secondCenterX = secondRoom.Position.X + secondRoom.Height / 2;

      if (firstCenterX < secondCenterX) return whichRoom ? 0 : 1;
      if (firstCenterX > secondCenterX) return whichRoom ? 1 : 0;

      return 1;
    }


    public override float Distance(Vector pos, bool includeMargins) {
      var distance = float.MaxValue;
      foreach (var segment in getSegments())
        distance = Math.Min(distance, pos.DistanceFromLineSegment(segment));
      return distance;
    }

    public override void Draw(XGraphics graphics, Palette palette, DrawingContext context) {
      var lineSegments = context.UseSmartLineSegments ? mSmartSegments : getSegments();

      foreach (var lineSegment in lineSegments) {
        var pen = palette.GetLinePen(context.Selected, context.Hover, Style == ConnectionStyle.Dashed);
        Pen specialPen = null;

        if (!context.Hover)
          if (ConnectionColor != Color.Transparent && !context.Selected) {
            specialPen = (Pen) pen.Clone();
            specialPen.Color = ConnectionColor;
          }

        if (!ApplicationSettingsController.AppSettings.DebugDisableLineRendering)
          graphics.DrawLine(specialPen ?? pen, lineSegment.Start.ToPointF(), lineSegment.End.ToPointF());
        var delta = lineSegment.Delta;
        if (Flow == ConnectionFlow.OneWay && delta.Length > Settings.ConnectionArrowSize) {
          var brush = (SolidBrush) palette.GetLineBrush(context.Selected, context.Hover);
          SolidBrush specialBrush = null;

          if (!context.Hover)
            if (ConnectionColor != Color.Transparent && !context.Selected) {
              specialBrush = (SolidBrush) brush.Clone();
              specialBrush.Color = ConnectionColor;
            }

          Drawing.DrawChevron(graphics, lineSegment.Mid.ToPointF(), (float) (Math.Atan2(delta.Y, delta.X) / Math.PI * 180), Settings.ConnectionArrowSize, specialBrush ?? brush);
        }

        context.LinesDrawn.Add(lineSegment);
      }

      if (door != null)
        showDoorIcons(graphics, lineSegments[0]);

      annotate(graphics, palette, lineSegments);
    }

    public void EndLoad(object state) {
      var elements = (List<XmlElementReader>) state;
      for (var index = 0; index < elements.Count; ++index) {
        var element = elements[index];
        if (element.HasName("dock"))
          if (Project.FindElement(element.Attribute("id").ToInt(), out var target)) {
            var portID = element.Attribute("port").Text;
            foreach (var port in target.PortList)
              if (StringComparer.InvariantCultureIgnoreCase.Compare(portID, port.ID) == 0) {
                var vertex = VertexList[index];
                vertex.Port = port;
                break;
              }
          }
      }
    }

    public override Vector GetPortPosition(Port port) {
      var vertexPort = (VertexPort) port;
      return vertexPort.Vertex.Position;
    }

    public override Vector GetPortStalkPosition(Port port) {
      return GetPortPosition(port);
    }

    public Room GetSourceRoom() {
      return GetSourceRoom(out var t);
    }

    public Room GetSourceRoom(out CompassPoint sourceCompassPoint) {
      if (VertexList.Count > 0) {
        var port = VertexList[0].Port;
        if (port is Room.CompassPort) {
          var compassPort = (Room.CompassPort) port;
          sourceCompassPoint = compassPort.CompassPoint;
          return port.Owner as Room;
        }
      }

      sourceCompassPoint = CompassPoint.North;
      return null;
    }

    public Room GetTargetRoom() {
      CompassPoint t;
      return GetTargetRoom(out t);
    }

    public Room GetTargetRoom(out CompassPoint targetCompassPoint) {
      if (VertexList.Count > 1) {
        var port = VertexList[VertexList.Count - 1].Port;
        if (port is Room.CompassPort) {
          var compassPort = (Room.CompassPort) port;
          targetCompassPoint = compassPort.CompassPoint;
          return compassPort.Owner as Room;
        }
      }

      targetCompassPoint = CompassPoint.North;
      return null;
    }

    public static void GetText(ConnectionLabel label, out string start, out string end) {
      start = string.Empty;
      end = string.Empty;
      switch (label) {
        case ConnectionLabel.None:
          start = string.Empty;
          end = string.Empty;
          break;
        case ConnectionLabel.Up:
          start = Up;
          end = Down;
          break;
        case ConnectionLabel.Down:
          start = Down;
          end = Up;
          break;
        case ConnectionLabel.In:
          start = In;
          end = Out;
          break;
        case ConnectionLabel.Out:
          start = Out;
          end = In;
          break;
      }
    }

    public override Color GetToolTipColor() {
      return Door == null ? Color.Yellow : Color.GreenYellow;
    }

    public override string GetToolTipFooter() {
      return string.Empty;
    }

    public override string GetToolTipHeader() {
      return $"{Name}{(Door != null ? " (Door)" : string.Empty)}";
    }

    public override string GetToolTipText() {
      return Description;
    }

    public override bool HasTooltip() {
      return true;
    }

    public override bool Intersects(Rect rect) {
      foreach (var segment in getSegments())
        if (segment.IntersectsWith(rect))
          return true;
      return false;
    }

    public override void RecomputeSmartLineSegments(DrawingContext context) {
      mSmartSegments.Clear();
      foreach (var lineSegment in getSegments()) {
        List<LineSegment> newSegments = null;
        if (split(lineSegment, context, ref newSegments))
          foreach (var newSegment in newSegments)
            mSmartSegments.Add(newSegment);
        else
          mSmartSegments.Add(lineSegment);
      }

      foreach (var segment in mSmartSegments)
        context.LinesDrawn.Add(segment);
    }

    public void Reverse() {
      VertexList.Reverse();
      RaiseChanged();
    }

    public void RotateConnector(bool upperRoom, bool whichWay) {
      var upEnd = ConnectedRoomToRotate(upperRoom);
      if (upEnd == -1) return;
      var pointToChange = (Room.CompassPort) VertexList[ConnectedRoomToRotate(upperRoom)].Port;
      var connRoom = (Room) pointToChange.Owner;
      var dirToChange = pointToChange.CompassPoint;
      var startDir = dirToChange;
      do {
        if (whichWay)
          dirToChange--;
        else
          dirToChange++;
        if (dirToChange < CompassPoint.Min) dirToChange = CompassPoint.Max;
        if (dirToChange > CompassPoint.Max) dirToChange = CompassPoint.Min;
      } while (dirToChange != startDir && connRoom.GetConnections(dirToChange).Count > 0);

      if (startDir == dirToChange) {
        MessageBox.Show($"There are no free ports in room {connRoom.Name}", "Connector rotate failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      if (VertexList[upEnd].Port != connRoom.PortList[(int) dirToChange]) {
        //this should always be different, but just in case...
        VertexList[upEnd].Port = connRoom.PortList[(int) dirToChange];
        RaiseChanged();
      }
    }

    public void Save(XmlScribe scribe) {
      scribe.Attribute("name", Name);
      scribe.Attribute("description", Description);
      if (Door != null) {
        scribe.Attribute("door", true);
        scribe.Attribute("lockable", door.Lockable);
        scribe.Attribute("openable", door.Openable);
        scribe.Attribute("locked", door.Locked);
        scribe.Attribute("open", door.Open);
      }

      if (ConnectionColor != Color.Transparent)
        scribe.Attribute("color", Colors.SaveColor(ConnectionColor));


      if (Style != DEFAULT_STYLE)
        switch (Style) {
          case ConnectionStyle.Solid:
            scribe.Attribute("style", "solid");
            break;
          case ConnectionStyle.Dashed:
            scribe.Attribute("style", "dashed");
            break;
        }
      if (Flow != DEFAULT_FLOW)
        switch (Flow) {
          case ConnectionFlow.OneWay:
            scribe.Attribute("flow", "oneWay");
            break;
          case ConnectionFlow.TwoWay:
            scribe.Attribute("flow", "twoWay");
            break;
        }

      if (!string.IsNullOrEmpty(StartText))
        scribe.Attribute("startText", StartText);
      if (!string.IsNullOrEmpty(MidText))
        scribe.Attribute("midText", MidText);
      if (!string.IsNullOrEmpty(EndText))
        scribe.Attribute("endText", EndText);

      var index = 0;
      foreach (var vertex in VertexList) {
        if (vertex.Port != null) {
          scribe.StartElement("dock");
          scribe.Attribute("index", index);
          scribe.Attribute("id", vertex.Port.Owner.ID);
          scribe.Attribute("port", vertex.Port.ID);
          scribe.EndElement();
        } else {
          scribe.StartElement("point");
          scribe.Attribute("index", index);
          scribe.Attribute("x", vertex.Position.X);
          scribe.Attribute("y", vertex.Position.Y);
          scribe.EndElement();
        }

        ++index;
      }
    }

    public void SetText(string start, string mid, string end) {
      StartText = start;
      MidText = mid ?? MidText;
      EndText = end;
    }

    public void SetText(ConnectionLabel label) {
      GetText(label, out var start, out var end);
      SetText(start, null, end);
    }

    public override void ShowDialog() {
      using (var dialog = new ConnectionPropertiesDialog()) {
        dialog.ConnectionName = Name;
        dialog.ConnectionDescription = Description;
        dialog.IsDotted = Style == ConnectionStyle.Dashed;
        dialog.IsDirectional = Flow == ConnectionFlow.OneWay;
        dialog.StartText = StartText;
        dialog.MidText = MidText;
        dialog.EndText = EndText;
        dialog.ConnectionColor = ConnectionColor;
        dialog.Door = Door;
        if (dialog.ShowDialog(Project.Canvas) == DialogResult.OK) {
          Name = dialog.ConnectionName;
          Description = dialog.ConnectionDescription;
          Style = dialog.IsDotted ? ConnectionStyle.Dashed : ConnectionStyle.Solid;
          Flow = dialog.IsDirectional ? ConnectionFlow.OneWay : ConnectionFlow.TwoWay;
          ConnectionColor = dialog.ConnectionColor;
          StartText = dialog.StartText;
          MidText = dialog.MidText;
          EndText = dialog.EndText;
          Door = dialog.Door;
        }
      }
    }


    public override Rect UnionBoundsWith(Rect rect, bool includeMargins) {
      foreach (var vertex in VertexList)
        rect = rect.Union(vertex.Position);

      return rect;
    }

    private void annotate(XGraphics graphics, Palette palette, List<LineSegment> lineSegments) {
      if (lineSegments.Count == 0)
        return;

      if (!string.IsNullOrEmpty(StartText))
        annotate(graphics, palette, lineSegments[0], mStartText, StringAlignment.Near);

      if (!string.IsNullOrEmpty(EndText))
        annotate(graphics, palette, lineSegments[lineSegments.Count - 1], mEndText, StringAlignment.Far);

      if (!string.IsNullOrEmpty(MidText)) {
        var totalLength = lineSegments.Sum(lineSegment => lineSegment.Length);
        var middle = totalLength / 2;
        if (lineSegments.Count % 2 == 1) // with default values, middle text is horizontally but not vertically centered
          middle += 4.0f * Settings.LineFont.Height / 5;
        foreach (var lineSegment in lineSegments) {
          var length = lineSegment.Length;
          if (middle > length) {
            middle -= length;
          } else {
            middle /= length;
            var pos = lineSegment.Start + lineSegment.Delta * middle;
            var fakeSegment = new LineSegment(pos - lineSegment.Delta * Numeric.Small, pos + lineSegment.Delta * Numeric.Small);
            annotate(graphics, palette, fakeSegment, mMidText, StringAlignment.Center);
            break;
          }
        }
      }
    }

    private void annotate(XGraphics graphics, Palette palette, LineSegment lineSegment, TextBlock text, StringAlignment alignment) {
      Vector point;
      var delta = lineSegment.Delta;
      var roomTypeAdjustments = Vector.Zero;

      RoomShape roomType;
      switch (alignment) {
        default:
          // detached vertex has no port, so need to check if it exists
          if (VertexList[0].Port != null) {
            roomType = VertexList[0].Port.Owner.GetRoomType();
            if (roomType == RoomShape.Ellipse || roomType == RoomShape.Octagonal)
              roomTypeAdjustments = this.roomTypeAdjustments(VertexList[0]);
          }
          point = lineSegment.Start + roomTypeAdjustments;
          delta.Negate();
          break;
        case StringAlignment.Center:
          point = lineSegment.Mid;
          break;
        case StringAlignment.Far:
          // detached vertex has no port, so need to check if it exists
          if (VertexList[1].Port != null) {
            roomType = VertexList[1].Port.Owner.GetRoomType();
            if (roomType == RoomShape.Ellipse || roomType == RoomShape.Octagonal)
              roomTypeAdjustments = this.roomTypeAdjustments(VertexList[1]);
          }
          point = lineSegment.End + roomTypeAdjustments;
          break;
      }

      var bounds = new Rect(point, Vector.Zero);
      bounds.Inflate(Settings.TextOffsetFromConnection);

      var compassPoint = CompassPointHelper.DirectionFromAngle(out var angle, delta);

      var pos = bounds.GetCorner(compassPoint);
      var format = new XStringFormat();
      Drawing.SetAlignmentFromCardinalOrOrdinalDirection(format, compassPoint);
      if (alignment == StringAlignment.Center && Numeric.InRange(angle, -10, 10)) {
        // HACK: if the line segment is pretty horizontal and we're drawing mid-line text,
        // move text below the line to get it out of the way of any labels at the ends,
        // and center the text so it fits onto a line between two proximal rooms.
        pos = bounds.GetCorner(CompassPoint.South);
        format.Alignment = XStringAlignment.Center;
        format.LineAlignment = XLineAlignment.Near;
      }


      if (!ApplicationSettingsController.AppSettings.DebugDisableTextRendering)
        text.Draw(graphics, Settings.LineFont, palette.LineTextBrush, pos, Vector.Zero, format);
    }

    private List<LineSegment> getSegments() {
      var list = new List<LineSegment>();
      if (VertexList.Count > 0) {
        var first = VertexList[0];

        var index = 0;
        var a = VertexList[index++].Position;

        if (first.Port != null && first.Port.HasStalk) {
          var stalkPos = first.Port.StalkPosition;
          list.Add(new LineSegment(a, stalkPos));
          a = stalkPos;
        }

        while (index < VertexList.Count) {
          var v = VertexList[index++];
          var b = v.Position;

          if (index == VertexList.Count && v.Port != null && v.Port.HasStalk) {
            var stalkPos = v.Port.StalkPosition;
            list.Add(new LineSegment(a, stalkPos));
            a = stalkPos;
          }

          list.Add(new LineSegment(a, b));
          a = b;
        }
      }

      return list;
    }

    private void initEvents() {
      VertexList.Added += onVertexAdded;
      VertexList.Removed += onVertexRemoved;
    }

    private void onVertexAdded(object sender, ItemEventArgs<Vertex> e) {
      e.Item.Connection = this;
      e.Item.Changed += onVertexChanged;
      PortList.Add(new VertexPort(e.Item, this));
    }

    private void onVertexChanged(object sender, EventArgs e) {
      RaiseChanged();
    }

    private void onVertexRemoved(object sender, ItemEventArgs<Vertex> e) {
      e.Item.Connection = null;
      e.Item.Changed -= onVertexChanged;
      foreach (var port1 in PortList) {
        var port = (VertexPort) port1;
        if (port.Vertex == e.Item) {
          PortList.Remove(port);
          break;
        }
      }
    }

    private Vector roomTypeAdjustments(Vertex vertex) {
      var roomTypeAdjustments = Vector.Zero;
      CompassPointHelper.FromName(vertex.Port.ID, out var dir);
      switch (dir) {
        case CompassPoint.SouthEast:
          roomTypeAdjustments = new Vector(8, 6);
          break;
        case CompassPoint.NorthEast:
          roomTypeAdjustments = new Vector(10, -6);
          break;
        case CompassPoint.EastNorthEast:
          roomTypeAdjustments = new Vector(4, 0);
          break;
        case CompassPoint.EastSouthEast:
          roomTypeAdjustments = new Vector(4, 0);
          break;
        case CompassPoint.NorthWest:
          roomTypeAdjustments = new Vector(-10, -4);
          break;
        case CompassPoint.SouthWest:
          roomTypeAdjustments = new Vector(-10, 4);
          break;
        case CompassPoint.WestSouthWest:
          roomTypeAdjustments = new Vector(-10, 0);
          break;
        case CompassPoint.WestNorthWest:
          roomTypeAdjustments = new Vector(-10, -4);
          break;
      }

      return roomTypeAdjustments;
    }

    private void showDoorIcons(XGraphics graphics, LineSegment lineSegment) {
      var doorIcon = door.Open ? new Bitmap(Resources.Door_Open) : new Bitmap(Resources.Door);
      var doorLock = door.Locked ? new Bitmap(Resources.Lock) : new Bitmap(Resources.Unlocked);
      lineSegment.IconBlock1.Image = doorIcon;
      lineSegment.IconBlock2.Image = doorLock;

      lineSegment.DrawIcons(graphics);
    }

    /// <summary>
    ///   Split the given line segment if it crosses line segments we've already drawn.
    /// </summary>
    /// <param name="lineSegment">The line segment to consider.</param>
    /// <param name="context">The context in which we've been drawing line segments.</param>
    /// <param name="newSegments">
    ///   The results of splitting the given line segment, if any. Call with a reference to a null
    ///   list.
    /// </param>
    /// <returns>True if the line segment was split and newSegments now exists and contains line segments; false otherwise.</returns>
    private bool split(LineSegment lineSegment, DrawingContext context, ref List<LineSegment> newSegments) {
      foreach (var previousSegment in context.LinesDrawn) {
        var amount = Math.Max(1, Settings.LineWidth) * 3;
        if (lineSegment.Intersect(previousSegment, true, out var intersects))
          foreach (var intersect in intersects) {
            switch (intersect.Type) {
              case LineSegmentIntersectType.MidPointA:
                var one = new LineSegment(lineSegment.Start, intersect.Position);
                if (one.Shorten(amount))
                  if (!split(one, context, ref newSegments)) {
                    if (newSegments == null)
                      newSegments = new List<LineSegment>();
                    newSegments.Add(one);
                  }

                var two = new LineSegment(intersect.Position, lineSegment.End);
                if (two.Forshorten(amount))
                  if (!split(two, context, ref newSegments)) {
                    if (newSegments == null)
                      newSegments = new List<LineSegment>();
                    newSegments.Add(two);
                  }

                break;

              case LineSegmentIntersectType.StartA:
                if (lineSegment.Forshorten(amount))
                  if (!split(lineSegment, context, ref newSegments)) {
                    if (newSegments == null)
                      newSegments = new List<LineSegment>();
                    newSegments.Add(lineSegment);
                  }

                break;

              case LineSegmentIntersectType.EndA:
                if (lineSegment.Shorten(amount))
                  if (!split(lineSegment, context, ref newSegments)) {
                    if (newSegments == null)
                      newSegments = new List<LineSegment>();
                    newSegments.Add(lineSegment);
                  }

                break;
            }

            // don't check other intersects;
            // we've already split this line, and tested the parts for further intersects.
            return newSegments != null;
          }
      }

      return false;
    }

    public class VertexPort : MoveablePort {
      public VertexPort(Vertex vertex, Connection connection) : base(connection) {
        Vertex = vertex;
        Connection = connection;
      }

      public Connection Connection { get; }

      public override Port DockedAt => Vertex.Port;

      public override string ID => Connection.VertexList.IndexOf(Vertex).ToString(CultureInfo.InvariantCulture);

      public Vertex Vertex { get; }

      public override void DockAt(Port port) {
        Vertex.Port = port;
        Connection.RaiseChanged();
      }

      public override void SetPosition(Vector pos) {
        Vertex.Position = pos;
        Connection.RaiseChanged();
      }
    }
  }

  /// <summary>
  ///   The visual style of a connection.
  /// </summary>
  public enum ConnectionStyle {
    Solid,
    Dashed
  }

  /// <summary>
  ///   The direction in which a connection flows.
  /// </summary>
  public enum ConnectionFlow {
    TwoWay,
    OneWay
  }

  /// <summary>
  ///   The style of label to display on a line.
  ///   This is a simple set of defaults; lines may have entirely custom labels.
  /// </summary>
  public enum ConnectionLabel {
    None,
    Up,
    Down,
    In,
    Out
  }
}