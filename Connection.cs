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
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using PdfSharp.Drawing;
using Trizbort.Extensions;

namespace Trizbort
{
  /// <summary>
  ///   A connection between elements or points in space.
  /// </summary>
  /// <remarks>
  ///   Connections are multi-segment lines between vertices.
  ///   Each vertex is fixed either to a point in space or
  ///   to an element's port.
  /// </remarks>
  internal class Connection : Element
  {
    public const string Up = "up";
    public const string Down = "down";
    public const string In = "in";
    public const string Out = "out";
    private static readonly ConnectionStyle DefaultStyle = ConnectionStyle.Solid;
    private static readonly ConnectionFlow DefaultFlow = ConnectionFlow.TwoWay;
    private readonly TextBlock m_endText = new TextBlock();
    private readonly TextBlock m_midText = new TextBlock();
    private readonly List<LineSegment> m_smartSegments = new List<LineSegment>();
    private readonly TextBlock m_startText = new TextBlock();
    private readonly BoundList<Vertex> m_vertexList = new BoundList<Vertex>();
    private ConnectionFlow m_flow = DefaultFlow;
    private ConnectionStyle m_style = DefaultStyle;
    private Color m_ConnectionColor = Color.Transparent;

    public Connection(Project project)
      : base(project)
    {
      m_vertexList.Added += OnVertexAdded;
      m_vertexList.Removed += OnVertexRemoved;
    }

    // Added this second constructor to be used when loading a room
    // This constructor is significantly faster as it doesn't look for gap in the element IDs
    public Connection(Project project, int TotalIDs)
      : base(project, TotalIDs)
    {
      m_vertexList.Added += OnVertexAdded;
      m_vertexList.Removed += OnVertexRemoved;
    }

    public Connection(Project project, Vertex a, Vertex b)
      : this(project)
    {
      VertexList.Add(a);
      VertexList.Add(b);
    }

    // Added to ignore ID gaps
    public Connection(Project project, Vertex a, Vertex b, int TotalIDs)
      : this(project, TotalIDs)
    {
      VertexList.Add(a);
      VertexList.Add(b);
    }

    public Connection(Project project, Vertex a, Vertex b, params Vertex[] args)
      : this(project, a, b)
    {
      foreach (var vertex in args)
      {
        VertexList.Add(vertex);
      }
    }

    // Added to ignore ID gaps
    public Connection(Project project, Vertex a, Vertex b, int TotalIDs, params Vertex[] args)
      : this(project, a, b, TotalIDs)
    {
      foreach (var vertex in args)
      {
        VertexList.Add(vertex);
      }
    }

    public Color ConnectionColor
    {
      get { return m_ConnectionColor; }
      set
      {
        if (m_ConnectionColor != value)
        {
          m_ConnectionColor = value;
          RaiseChanged();
        }
      }
    }
    public ConnectionStyle Style
    {
      get { return m_style; }
      set
      {
        if (m_style != value)
        {
          m_style = value;
          RaiseChanged();
        }
      }
    }

    public ConnectionFlow Flow
    {
      get { return m_flow; }
      set
      {
        if (m_flow != value)
        {
          m_flow = value;
          RaiseChanged();
        }
      }
    }

    public string StartText
    {
      get { return m_startText.Text; }
      set
      {
        if (m_startText.Text != value)
        {
          m_startText.Text = value;
          RaiseChanged();
        }
      }
    }

    public string MidText
    {
      get { return m_midText.Text; }
      set
      {
        if (m_midText.Text != value)
        {
          m_midText.Text = value;
          RaiseChanged();
        }
      }
    }

    public string EndText
    {
      get { return m_endText.Text; }
      set
      {
        if (m_endText.Text != value)
        {
          m_endText.Text = value;
          RaiseChanged();
        }
      }
    }

    public BoundList<Vertex> VertexList
    {
      get { return m_vertexList; }
    }

    public override Depth Depth
    {
      get { return Depth.Low; }
    }

    public override bool HasDialog
    {
      get { return true; }
    }

    private void OnVertexAdded(object sender, ItemEventArgs<Vertex> e)
    {
      e.Item.Connection = this;
      e.Item.Changed += OnVertexChanged;
      PortList.Add(new VertexPort(e.Item, this));
    }

    private void OnVertexRemoved(object sender, ItemEventArgs<Vertex> e)
    {
      e.Item.Connection = null;
      e.Item.Changed -= OnVertexChanged;
      foreach (VertexPort port in PortList)
      {
        if (port.Vertex == e.Item)
        {
          PortList.Remove(port);
          break;
        }
      }
    }

    private void OnVertexChanged(object sender, EventArgs e)
    {
      RaiseChanged();
    }

    public void SetText(string start, string mid, string end)
    {
      StartText = start;
      MidText = mid ?? MidText;
      EndText = end;
    }

    public static void GetText(ConnectionLabel label, out string start, out string end)
    {
      start = string.Empty;
      end = string.Empty;
      switch (label)
      {
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

    public void SetText(ConnectionLabel label)
    {
      string start, end;
      GetText(label, out start, out end);
      SetText(start, null, end);
    }

    private List<LineSegment> GetSegments()
    {
      var list = new List<LineSegment>();
      if (VertexList.Count > 0)
      {
        var first = VertexList[0];
        var last = VertexList[VertexList.Count - 1];

        var index = 0;
        var a = VertexList[index++].Position;

        if (first.Port != null && first.Port.HasStalk)
        {
          var stalkPos = first.Port.StalkPosition;
          list.Add(new LineSegment(a, stalkPos));
          a = stalkPos;
        }

        while (index < VertexList.Count)
        {
          var v = VertexList[index++];
          var b = v.Position;

          if (index == VertexList.Count && v.Port != null && v.Port.HasStalk)
          {
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
    private bool Split(LineSegment lineSegment, DrawingContext context, ref List<LineSegment> newSegments)
    {
      foreach (var previousSegment in context.LinesDrawn)
      {
        var amount = Math.Max(1, Settings.LineWidth)*3;
        List<LineSegmentIntersect> intersects;
        if (lineSegment.Intersect(previousSegment, true, out intersects))
        {
          foreach (var intersect in intersects)
          {
            switch (intersect.Type)
            {
              case LineSegmentIntersectType.MidPointA:
                var one = new LineSegment(lineSegment.Start, intersect.Position);
                if (one.Shorten(amount))
                {
                  if (!Split(one, context, ref newSegments))
                  {
                    if (newSegments == null)
                    {
                      newSegments = new List<LineSegment>();
                    }
                    newSegments.Add(one);
                  }
                }
                var two = new LineSegment(intersect.Position, lineSegment.End);
                if (two.Forshorten(amount))
                {
                  if (!Split(two, context, ref newSegments))
                  {
                    if (newSegments == null)
                    {
                      newSegments = new List<LineSegment>();
                    }
                    newSegments.Add(two);
                  }
                }
                break;

              case LineSegmentIntersectType.StartA:
                if (lineSegment.Forshorten(amount))
                {
                  if (!Split(lineSegment, context, ref newSegments))
                  {
                    if (newSegments == null)
                    {
                      newSegments = new List<LineSegment>();
                    }
                    newSegments.Add(lineSegment);
                  }
                }
                break;

              case LineSegmentIntersectType.EndA:
                if (lineSegment.Shorten(amount))
                {
                  if (!Split(lineSegment, context, ref newSegments))
                  {
                    if (newSegments == null)
                    {
                      newSegments = new List<LineSegment>();
                    }
                    newSegments.Add(lineSegment);
                  }
                }
                break;
            }

            // don't check other intersects;
            // we've already split this line, and tested the parts for further intersects.
            return newSegments != null;
          }
        }
      }
      return false;
    }

    public override void RecomputeSmartLineSegments(DrawingContext context)
    {
      m_smartSegments.Clear();
      foreach (var lineSegment in GetSegments())
      {
        List<LineSegment> newSegments = null;
        if (Split(lineSegment, context, ref newSegments))
        {
          foreach (var newSegment in newSegments)
          {
            m_smartSegments.Add(newSegment);
          }
        }
        else
        {
          m_smartSegments.Add(lineSegment);
        }
      }

      foreach (var segment in m_smartSegments)
      {
        context.LinesDrawn.Add(segment);
      }
    }

    public override void Draw(XGraphics graphics, Palette palette, DrawingContext context)
    {
      var lineSegments = context.UseSmartLineSegments ? m_smartSegments : GetSegments();

      foreach (var lineSegment in lineSegments)
      {
        var pen = palette.GetLinePen(context.Selected, context.Hover, Style == ConnectionStyle.Dashed);
        Pen specialPen = null;

        if (!context.Hover)
          if ((ConnectionColor != Color.Transparent) && !context.Selected)
          {
            specialPen = (Pen) pen.Clone();
            specialPen.Color = ConnectionColor;
          }

        if (!Settings.DebugDisableLineRendering)
        {
          graphics.DrawLine(specialPen ?? pen, lineSegment.Start.ToPointF(), lineSegment.End.ToPointF());
        }
        var delta = lineSegment.Delta;
        if (Flow == ConnectionFlow.OneWay && delta.Length > Settings.ConnectionArrowSize)
        {
          SolidBrush brush = (SolidBrush) palette.GetLineBrush(context.Selected, context.Hover);
          SolidBrush specialBrush = null;

          if (!context.Hover)
            if ((ConnectionColor != Color.Transparent) && !context.Selected)
            {
              specialBrush = (SolidBrush) brush.Clone();
              specialBrush.Color = ConnectionColor;
            }

          Drawing.DrawChevron(graphics, lineSegment.Mid.ToPointF(), (float) (Math.Atan2(delta.Y, delta.X)/Math.PI*180), Settings.ConnectionArrowSize, specialBrush ?? brush);
        }
        context.LinesDrawn.Add(lineSegment);
      }

      Annotate(graphics, palette, lineSegments);
    }

    private void Annotate(XGraphics graphics, Palette palette, List<LineSegment> lineSegments)
    {
      if (lineSegments.Count == 0)
        return;

      if (!string.IsNullOrEmpty(StartText))
      {
        Annotate(graphics, palette, lineSegments[0], m_startText, StringAlignment.Near, GetSourceRoom()?.Shape);
      }

      if (!string.IsNullOrEmpty(EndText))
      {
        Annotate(graphics, palette, lineSegments[lineSegments.Count - 1], m_endText, StringAlignment.Far, GetTargetRoom()?.Shape);
      }

      if (!string.IsNullOrEmpty(MidText))
      {
        var totalLength = lineSegments.Sum(lineSegment => lineSegment.Length);
        var middle = totalLength/2;
        foreach (var lineSegment in lineSegments)
        {
          var length = lineSegment.Length;
          if (middle > length)
          {
            middle -= length;
          }
          else
          {
            middle /= length;
            var pos = lineSegment.Start + lineSegment.Delta*middle;
            var fakeSegment = new LineSegment(pos - lineSegment.Delta*Numeric.Small, pos + lineSegment.Delta*Numeric.Small);
            Annotate(graphics, palette, fakeSegment, m_midText, StringAlignment.Center);
            break;
          }
        }
      }
    }

    private static void Annotate(XGraphics graphics, Palette palette, LineSegment lineSegment, TextBlock text, StringAlignment alignment, RoomShape? shape = RoomShape.SquareCorners)
    {
      Vector point;
      var delta = lineSegment.Delta;
      switch (alignment)
      {
        case StringAlignment.Near:
        default:
          point = lineSegment.Start;
          delta.Negate();
          break;
        case StringAlignment.Center:
          point = lineSegment.Mid;
          break;
        case StringAlignment.Far:
          point = lineSegment.End;
          break;
      }

      var bounds = new Rect(point, Vector.Zero);
      bounds.Inflate(Settings.TextOffsetFromConnection);

      var angle = (float) -(Math.Atan2(delta.Y, delta.X)/Math.PI*180.0);
      var compassPoint = CompassPoint.East;
      if (Numeric.InRange(angle, 0, 45))
      {
        compassPoint = CompassPoint.NorthWest;
      }
      else if (Numeric.InRange(angle, 45, 90))
      {
        compassPoint = CompassPoint.SouthEast;
      }
      else if (Numeric.InRange(angle, 90, 135))
      {
        compassPoint = CompassPoint.SouthWest;
      }
      else if (Numeric.InRange(angle, 135, 180))
      {
        compassPoint = CompassPoint.NorthEast;
      }
      else if (Numeric.InRange(angle, 0, -45))
      {
        compassPoint = CompassPoint.NorthEast;
      }
      else if (Numeric.InRange(angle, -45, -90))
      {
        compassPoint = CompassPoint.NorthEast;
      }
      else if (Numeric.InRange(angle, -90, -135))
      {
        compassPoint = CompassPoint.NorthWest;
      }
      else if (Numeric.InRange(angle, -135, -180))
      {
        compassPoint = CompassPoint.SouthEast;
      }

      var pos = bounds.GetCorner(compassPoint);
      var format = new XStringFormat();
      Drawing.SetAlignmentFromCardinalOrOrdinalDirection(format, compassPoint);
      if (alignment == StringAlignment.Center && Numeric.InRange(angle, -10, 10))
      {
        // HACK: if the line segment is pretty horizontal and we're drawing mid-line text,
        // move text below the line to get it out of the way of any labels at the ends,
        // and center the text so it fits onto a line between two proximal rooms.
        pos = bounds.GetCorner(CompassPoint.South);
        format.Alignment = XStringAlignment.Center;
        format.LineAlignment = XLineAlignment.Near;
      }

      
      if (!Settings.DebugDisableTextRendering)
        text.Draw(graphics, Settings.LineFont, palette.LineTextBrush, pos, Vector.Zero, format);
    }

    public override Rect UnionBoundsWith(Rect rect, bool includeMargins)
    {
      foreach (var vertex in VertexList)
      {
        rect = rect.Union(vertex.Position);
      }

      return rect;
    }

    public override Vector GetPortPosition(Port port)
    {
      var vertexPort = (VertexPort) port;
      return vertexPort.Vertex.Position;
    }

    public override Vector GetPortStalkPosition(Port port)
    {
      return GetPortPosition(port);
    }

    public override string GetToolTipText()
    {
      return "Connection ToolTip Text";
    }

    public override eTooltipColor GetToolTipColor()
    {
      return eTooltipColor.Apple;
    }

    public override string GetToolTipFooter()
    {
      return string.Empty;
    }

    public override string GetToolTipHeader()
    {
      return string.Empty;
    }

    public override bool HasTooltip()
    {
      return false;
    }


    public override float Distance(Vector pos, bool includeMargins)
    {
      var distance = float.MaxValue;
      foreach (var segment in GetSegments())
      {
        distance = Math.Min(distance, pos.DistanceFromLineSegment(segment));
      }
      return distance;
    }

    public override bool Intersects(Rect rect)
    {
      foreach (var segment in GetSegments())
      {
        if (segment.IntersectsWith(rect))
        {
          return true;
        }
      }
      return false;
    }

    public override void ShowDialog()
    {
      using (var dialog = new ConnectionPropertiesDialog())
      {
        dialog.IsDotted = Style == ConnectionStyle.Dashed;
        dialog.IsDirectional = Flow == ConnectionFlow.OneWay;
        dialog.StartText = StartText;
        dialog.MidText = MidText;
        dialog.EndText = EndText;
        dialog.ConnectionColor = ConnectionColor;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Style = dialog.IsDotted ? ConnectionStyle.Dashed : ConnectionStyle.Solid;
          Flow = dialog.IsDirectional ? ConnectionFlow.OneWay : ConnectionFlow.TwoWay;
          ConnectionColor = dialog.ConnectionColor;
          StartText = dialog.StartText;
          MidText = dialog.MidText;
          EndText = dialog.EndText;
        }
      }
    }

    public void Save(XmlScribe scribe)
    {
      if (ConnectionColor != Color.Transparent)
        scribe.Attribute("color", Colors.SaveColor(ConnectionColor));

      if (Style != DefaultStyle)
      {
        switch (Style)
        {
          case ConnectionStyle.Solid:
            scribe.Attribute("style", "solid");
            break;
          case ConnectionStyle.Dashed:
            scribe.Attribute("style", "dashed");
            break;
        }
      }
      if (Flow != DefaultFlow)
      {
        switch (Flow)
        {
          case ConnectionFlow.OneWay:
            scribe.Attribute("flow", "oneWay");
            break;
          case ConnectionFlow.TwoWay:
            scribe.Attribute("flow", "twoWay");
            break;
        }
      }

      if (!string.IsNullOrEmpty(StartText))
      {
        scribe.Attribute("startText", StartText);
      }
      if (!string.IsNullOrEmpty(MidText))
      {
        scribe.Attribute("midText", MidText);
      }
      if (!string.IsNullOrEmpty(EndText))
      {
        scribe.Attribute("endText", EndText);
      }

      var index = 0;
      foreach (var vertex in VertexList)
      {
        if (vertex.Port != null)
        {
          scribe.StartElement("dock");
          scribe.Attribute("index", index);
          scribe.Attribute("id", vertex.Port.Owner.ID);
          scribe.Attribute("port", vertex.Port.ID);
          scribe.EndElement();
        }
        else
        {
          scribe.StartElement("point");
          scribe.Attribute("index", index);
          scribe.Attribute("x", vertex.Position.X);
          scribe.Attribute("y", vertex.Position.Y);
          scribe.EndElement();
        }
        ++index;
      }
    }

    public object BeginLoad(XmlElementReader element)
    {
      switch (element.Attribute("style").Text)
      {
        case "solid":
        default:
          Style = ConnectionStyle.Solid;
          break;
        case "dashed":
          Style = ConnectionStyle.Dashed;
          break;
      }
      switch (element.Attribute("flow").Text)
      {
        case "twoWay":
        default:
          Flow = ConnectionFlow.TwoWay;
          break;
        case "oneWay":
          Flow = ConnectionFlow.OneWay;
          break;
      }
      StartText = element.Attribute("startText").Text;
      MidText = element.Attribute("midText").Text;
      EndText = element.Attribute("endText").Text;
      if (element.Attribute("color").Text != "") { ConnectionColor = ColorTranslator.FromHtml(element.Attribute("color").Text); }

      var vertexElementList = new List<XmlElementReader>();
      vertexElementList.AddRange(element.Children);
      vertexElementList.Sort((a, b) => a.Attribute("index").ToInt().CompareTo(b.Attribute("index").ToInt()));

      foreach (var vertexElement in vertexElementList)
      {
        if (vertexElement.HasName("point"))
        {
          var vertex = new Vertex();
          vertex.Position = new Vector(vertexElement.Attribute("x").ToFloat(), vertexElement.Attribute("y").ToFloat());
          VertexList.Add(vertex);
        }
        else if (vertexElement.HasName("dock"))
        {
          var vertex = new Vertex();
          // temporarily leave this vertex as a positional vertex;
          // we can't safely dock it to a port until EndLoad().
          VertexList.Add(vertex);
        }
      }

      return vertexElementList;
    }

    public void EndLoad(object state)
    {
      var elements = (List<XmlElementReader>) (state);
      for (var index = 0; index < elements.Count; ++index)
      {
        var element = elements[index];
        if (element.HasName("dock"))
        {
          Element target;
          if (Project.FindElement(element.Attribute("id").ToInt(), out target))
          {
            var portID = element.Attribute("port").Text;
            foreach (var port in target.Ports)
            {
              if (StringComparer.InvariantCultureIgnoreCase.Compare(portID, port.ID) == 0)
              {
                var vertex = VertexList[index];
                vertex.Port = port;
                break;
              }
            }
          }
        }
      }
    }

    public void Reverse()
    {
      VertexList.Reverse();
      RaiseChanged();
    }

    public Room GetSourceRoom()
    {
      CompassPoint t;
      return GetSourceRoom(out t);
    }

    public Room GetTargetRoom()
    {
      CompassPoint t;
      return GetTargetRoom(out t);
    }

    public Room GetSourceRoom(out CompassPoint sourceCompassPoint)
    {
      if (m_vertexList.Count > 0)
      {
        var port = m_vertexList[0].Port;
        if (port is Room.CompassPort)
        {
          var compassPort = (Room.CompassPort) port;
          sourceCompassPoint = compassPort.CompassPoint;
          return port.Owner as Room;
        }
      }
      sourceCompassPoint = CompassPoint.North;
      return null;
    }

    public int ConnectedRoomToRotate(bool whichRoom)
    {
    //first, let's take care of cases where the right room is forced, if there is one
      if ((this.VertexList[0].Port == null) && (this.VertexList[0].Port == null)) return -1;
      if (this.VertexList[1].Port == null) return 0;
      if (this.VertexList[0].Port == null) return 1;

      var firstRoom = (Room)this.VertexList[0].Port.Owner;
      var secondRoom = (Room)this.VertexList[1].Port.Owner;

      var firstCenterY = firstRoom.Y + firstRoom.Height / 2;
      var secondCenterY = secondRoom.Y + secondRoom.Height / 2;

      if (firstCenterY < secondCenterY) { return whichRoom ? 0 : 1; }
      if (firstCenterY > secondCenterY) { return whichRoom ? 1 : 0; }

      var firstCenterX = firstRoom.Position.X + firstRoom.Height / 2;
      var secondCenterX = secondRoom.Position.X + secondRoom.Height / 2;

      if (firstCenterX < secondCenterX) { return whichRoom ? 0 : 1; }
      if (firstCenterX > secondCenterX) { return whichRoom ? 1 : 0; }

      return 1;
    }

    public void RotateConnector(bool upperRoom, bool whichWay)
    {
      var upEnd = ConnectedRoomToRotate(upperRoom);
      if (upEnd == -1) { return; }
      var pointToChange = (Room.CompassPort)VertexList[ConnectedRoomToRotate(upperRoom)].Port;
      var connRoom = (Room)pointToChange.Owner;
      var dirToChange = pointToChange.CompassPoint;
      var startDir = dirToChange;
      do
      {
        if (whichWay)
          dirToChange--;
        else
          dirToChange++;
        if (dirToChange < CompassPoint.Min) { dirToChange = CompassPoint.Max; }
        if (dirToChange > CompassPoint.Max) { dirToChange = CompassPoint.Min; }
      }
      while ((dirToChange != startDir) && (connRoom.GetConnections((CompassPoint)dirToChange).Count > 0));

      if (startDir == dirToChange)
      {
        MessageBox.Show($"There are no free ports in room {connRoom.Name}", "Connector rotate failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }
      if (this.VertexList[upEnd].Port != connRoom.Ports[(int) dirToChange])
      {
        //this should always be different, but just in case...
        this.VertexList[upEnd].Port = connRoom.Ports[(int) dirToChange];
        RaiseChanged();
      }
    }

    public Room GetTargetRoom(out CompassPoint targetCompassPoint)
    {
      if (m_vertexList.Count > 1)
      {
        var port = m_vertexList[m_vertexList.Count - 1].Port;
        if (port is Room.CompassPort)
        {
          var compassPort = (Room.CompassPort) port;
          targetCompassPoint = compassPort.CompassPoint;
          return compassPort.Owner as Room;
        }
      }
      targetCompassPoint = CompassPoint.North;
      return null;
    }

    public String ClipboardPrint()
    {
      var clipboardText = "";
      
      switch (Style)
      {
        case ConnectionStyle.Solid:
          clipboardText += "solid" + Canvas.CopyDelimiter;
          break;
        case ConnectionStyle.Dashed:
          clipboardText += "dashed" + Canvas.CopyDelimiter;
          break;
        default:
          clipboardText += "default" + Canvas.CopyDelimiter;
          break;
      }

      switch (Flow)
      {
        case ConnectionFlow.OneWay:
          clipboardText += "oneWay" + Canvas.CopyDelimiter;
          break;
        case ConnectionFlow.TwoWay:
          clipboardText += "twoWay" + Canvas.CopyDelimiter;
          break;
        default:
          clipboardText += "default" + Canvas.CopyDelimiter;
          break;
      }

      clipboardText += Colors.SaveColor(ConnectionColor) + Canvas.CopyDelimiter;

      clipboardText += StartText + Canvas.CopyDelimiter;
      clipboardText += MidText + Canvas.CopyDelimiter;
      clipboardText += EndText;

      var index = 0;
      foreach (var vertex in VertexList)
      {
        clipboardText += Canvas.CopyDelimiter;
        if (vertex.Port != null)
        {
          clipboardText += "dock" + Canvas.CopyDelimiter;
          clipboardText += index + Canvas.CopyDelimiter;
          clipboardText += vertex.Port.Owner.ID + Canvas.CopyDelimiter;
          clipboardText += vertex.Port.ID;
        }
        else
        {
          clipboardText += "point" + Canvas.CopyDelimiter;
          clipboardText += index + Canvas.CopyDelimiter;
          clipboardText += vertex.Position.X + Canvas.CopyDelimiter;
          clipboardText += vertex.Position.Y;
        }
        ++index;
      }

      clipboardText += "";
      return clipboardText;
    }

    internal class VertexPort : MoveablePort
    {
      public VertexPort(Vertex vertex, Connection connection) : base(connection)
      {
        Vertex = vertex;
        Connection = connection;
      }

      public override string ID { get { return Connection.VertexList.IndexOf(Vertex).ToString(CultureInfo.InvariantCulture); } }

      public override Port DockedAt { get { return Vertex.Port; } }

      public Vertex Vertex { get; private set; }
      public Connection Connection { get; private set; }

      public override void SetPosition(Vector pos)
      {
        Vertex.Position = pos;
        Connection.RaiseChanged();
      }

      public override void DockAt(Port port)
      {
        Vertex.Port = port;
        Connection.RaiseChanged();
      }
    }
  }

  /// <summary>
  ///   The visual style of a connection.
  /// </summary>
  internal enum ConnectionStyle
  {
    Solid,
    Dashed
  }

  /// <summary>
  ///   The direction in which a connection flows.
  /// </summary>
  internal enum ConnectionFlow
  {
    TwoWay,
    OneWay
  }

  /// <summary>
  ///   The style of label to display on a line.
  ///   This is a simple set of defaults; lines may have entirely custom labels.
  /// </summary>
  internal enum ConnectionLabel
  {
    None,
    Up,
    Down,
    In,
    Out
  }
}