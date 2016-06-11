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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DevComponents.DotNetBar;
using PdfSharp.Drawing;
using Trizbort.Domain;
using Trizbort.Extensions;
using Trizbort.UI;

namespace Trizbort
{
  /// <summary>
  ///   A room in the project.
  /// </summary>
  public class Room : Element, ISizeable
  {
    private const CompassPoint DEFAULT_OBJECTS_POSITION = CompassPoint.South;
    private readonly List<string> mDescriptions = new List<string>();
    private readonly TextBlock mName = new TextBlock();
    private readonly TextBlock mSubTitle = new TextBlock();
    private readonly TextBlock mObjects = new TextBlock();
    private bool mIsDark = false;
    private bool mRoundedCorners = false;
    private bool mOctagonal = false;
    private bool mEllipse = false;
    private bool mStraightEdges = false;
    private bool mAllCornersEqual = true;
    private bool mIsStartRoom = false;
    private CornerRadii mCorners;
    private CompassPoint mObjectsPosition = DEFAULT_OBJECTS_POSITION;
    // Added for linking connections when pasting
    private int mOldID;
    private Vector mPosition;
    // Added for Room specific colors (White shows global color)
    private Color mRoomborder = Color.Transparent;
    private Color mRoomfill = Color.Transparent;
    private Color mRoomlargetext = Color.Transparent;
    private string mRoomRegion;
    private Color mRoomsmalltext = Color.Transparent;
    private Color mSecondfill = Color.Transparent;
    private string mSecondfilllocation = "Bottom";
    private BorderDashStyle mBorderStyle = BorderDashStyle.Solid;
    private Vector mSize;
    private const int MAX_OBJECTS=1000;

    public Room(Project project): base(project)
    {
      Name = Settings.DefaultRoomName;
      HandDrawnEdges = Settings.HandDrawnGlobal;
      Region = Trizbort.Region.DefaultRegion;
      Size = new Vector(3*Settings.GridSize, 2*Settings.GridSize);
      Position = new Vector(-Size.X/2, -Size.Y/2);
      Corners = new CornerRadii();
      Shape = RoomShape.SquareCorners; //would be nice to make an app default later: issue #93
//      StraightEdges = true;

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
    public Room(Project project, int totalIDs) : base(project, totalIDs)
    {
      Name = Settings.DefaultRoomName;
      Region = Trizbort.Region.DefaultRegion;
      Size = new Vector(3*Settings.GridSize, 2*Settings.GridSize);
      Position = new Vector(-Size.X/2, -Size.Y/2);
      Corners = new CornerRadii();
      Shape = RoomShape.SquareCorners; //would be nice to make an app default later: issue #93

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

    /// <summary>
    ///   Get/set the name of the room.
    /// </summary>
    public string Name
    {
      get { return mName.Text; }
      set
      {
        value = value ?? string.Empty;
        if (mName.Text == value) return;
        mName.Text = value;
        RaiseChanged();
      }
    }

    
    private RoomShape shape;
    private bool mHandDrawnEdges;

    public RoomShape Shape
    {
      get { return shape; }
      set
      {
        if (shape != value)
        {
        shape = value;
        setRoomShape(value);
        RaiseChanged();
        }
      }
    }

    /// <summary>
    ///   Get/set the subtitle of the room.
    /// </summary>
    public string SubTitle
    {
      get { return mSubTitle.Text; }
      set
      {
        value = value ?? string.Empty;
        if (mSubTitle.Text == value) return;
        mSubTitle.Text = value;

        RaiseChanged();
      }
    }

    private void setRoomShape(RoomShape pShape)
    {
      switch (pShape)
      {
        case RoomShape.SquareCorners:
          StraightEdges = !StraightEdges;
          Ellipse = false;
          RoundedCorners = false;
          Octagonal = false;
          break;
        case RoomShape.RoundedCorners:
          RoundedCorners = true;
          Ellipse = false;
          StraightEdges = false;
          if (Corners.TopRight == 0.0 && Corners.TopLeft == 0.0 && Corners.BottomRight == 0.0  && Corners.BottomLeft == 0.0)
            Corners = new CornerRadii();
          Octagonal = false;
          break;
        case RoomShape.Ellipse:
          Ellipse = true;
          RoundedCorners = false;
          StraightEdges = false;
          Octagonal = false;
          break;
        case RoomShape.Octagonal:
          Octagonal = true;
          Ellipse = false;
          StraightEdges = false;
          RoundedCorners = false;
                    break;
        default:
          throw new ArgumentOutOfRangeException(nameof(pShape), pShape, null);
      }
    }

    /// <summary>
    ///   Get/set whether the room is dark or lit.
    /// </summary>
    public bool IsDark
    {
      get { return mIsDark; }
      set
      {
        if (mIsDark == value) return;
        mIsDark = value;
        RaiseChanged();
      }
    }

    /// <summary>
    ///   Get/set the list of objects in the room.
    /// </summary>
    public string Objects
    {
      get { return mObjects.Text; }
      set
      {
        value = value ?? string.Empty;
        if (mObjects.Text == value) return;
        mObjects.Text = value;
        RaiseChanged();
      }
    }

    /// <summary>
    ///   Get/set the position, relative to the room,
    ///   at which the object list is drawn on the map.
    /// </summary>
    public CompassPoint ObjectsPosition
    {
      get { return mObjectsPosition; }
      set
      {
        if (mObjectsPosition == value) return;
        mObjectsPosition = value;
        RaiseChanged();
      }
    }

    public BorderDashStyle BorderStyle
    {
      get { return mBorderStyle; }
      set
      {
        if (mBorderStyle == value) return;
        mBorderStyle = value;
        RaiseChanged();
      }
    }

    public string Region
    {
      get { return mRoomRegion; }
      set
      {
        if (mRoomRegion != value)
        {
          mRoomRegion = value;
          RaiseChanged();
        }
      }
    }

    public override Depth Depth => Depth.Medium;
    public override bool HasDialog => true;

    public CornerRadii Corners
    {
      get { return mCorners; }
      set
      {
        if (mCorners == null) { mCorners = value; return; }
        if (mCorners.BottomLeft != value.BottomLeft) { mCorners.BottomLeft = value.BottomLeft; RaiseChanged(); } //mCorners never equals value...
        if (mCorners.BottomRight != value.BottomRight) { mCorners.BottomRight = value.BottomRight; RaiseChanged(); }
        if (mCorners.TopLeft != value.TopLeft) { mCorners.TopLeft = value.TopLeft; RaiseChanged(); }
        if (mCorners.TopRight != value.TopRight) { mCorners.TopRight = value.TopRight; RaiseChanged(); }
        return;
      }
    }

    public bool RoundedCorners
    {
      get { return mRoundedCorners; }
      set { if (mRoundedCorners != value) { mRoundedCorners = value; RaiseChanged(); } }
    }

    public bool Octagonal
    {
      get { return mOctagonal; }
      set { if (mOctagonal != value) { mOctagonal = value; RaiseChanged(); } }
    }

    public bool Ellipse
    {
      get { return mEllipse; }
      set { if (mEllipse != value) { mEllipse = value; RaiseChanged(); } }
    }

    public bool StraightEdges
    {
      get { return mStraightEdges; }
      set { if (mStraightEdges != value) { mStraightEdges = value; //RaiseChanged(); This is disabled because it gives false positives
      } }
    }

    public bool AllCornersEqual
    {
      get { return mAllCornersEqual; }
      set { if (mAllCornersEqual != value) { mAllCornersEqual = value; RaiseChanged(); } }
    }

    public bool HandDrawnEdges
    {
      get { return mHandDrawnEdges; }
      set { if (mHandDrawnEdges != value) { mHandDrawnEdges = value;RaiseChanged(); } }
    }

    public bool IsStartRoom
    {
      get { return mIsStartRoom; }
      set { if (mIsStartRoom != value) { mIsStartRoom = value; RaiseChanged(); } }
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

          var connection = (Connection) element;
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

    public bool ArbitraryAutomappedPosition { get; set; }

    public string PrimaryDescription
    {
      get
      {
        if (mDescriptions.Count > 0)
        {
          return mDescriptions[0];
        }
        return null;
      }
    }

    public bool HasDescription => mDescriptions.Count > 0;

    // Added for Room specific colors
    public Color RoomFill
    {
      get { return mRoomfill; }
      set
      {
        if (mRoomfill != value)
        {
          mRoomfill = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color SecondFill
    {
      get { return mSecondfill; }
      set
      {
        if (mSecondfill != value)
        {
          mSecondfill = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public String SecondFillLocation
    {
      get { return mSecondfilllocation; }
      set
      {
        if (mSecondfilllocation != value)
        {
          mSecondfilllocation = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color RoomBorder
    {
      get { return mRoomborder; }
      set
      {
        if (mRoomborder != value)
        {
          mRoomborder = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color RoomLargeText
    {
      get { return mRoomlargetext; }
      set
      {
        if (mRoomlargetext != value)
        {
          mRoomlargetext = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color RoomSmallText
    {
      get { return mRoomsmalltext; }
      set
      {
        if (mRoomsmalltext != value)
        {
          mRoomsmalltext = value;
          RaiseChanged();
        }
      }
    }

    // Added for linking connections when pasting
    public int OldID
    {
      get { return mOldID; }
      set { mOldID = value; }
    }

    public override sealed Vector Position
    {
      get { return mPosition; }
      set
      {
        if (mPosition != value)
        {
          mPosition = value;
          ArbitraryAutomappedPosition = false;
          RaiseChanged();
        }
      }
    }

    public float X => mPosition.X;
    public float Y => mPosition.Y;

    public Vector Size
    {
      get { return mSize; }
      set
      {
        if (mSize != value)
        {
          mSize = value;
          RaiseChanged();
        }
      }
    }

    public float Width => mSize.X;
    public float Height => mSize.Y;

    public Rect InnerBounds => new Rect(Position, Size);

    public override string ToString()
    {
      return $"Room: {Name}";
    }

    public override string GetToolTipFooter()
    {
      return Objects;
    }

    private bool isDefaultRegion()
    {
      return Region == Trizbort.Region.DefaultRegion || string.IsNullOrEmpty(Region);
    }

    public override string GetToolTipHeader()
    {
      return $"{Name}{(!isDefaultRegion() ? $" ({Region})" : string.Empty)}";
    }

    public override bool HasTooltip()
    {
      return true;
    }



    public override eTooltipColor GetToolTipColor()
    {
      return eTooltipColor.BlueMist;
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
      var compass = (CompassPort) port;

      if (Ellipse)
        return InnerBounds.GetCorner(compass.CompassPoint, RoomShape.Ellipse);

      if (RoundedCorners)
        return InnerBounds.GetCorner(compass.CompassPoint, RoomShape.RoundedCorners, Corners);
      
      if (Octagonal)
        return InnerBounds.GetCorner(compass.CompassPoint, RoomShape.Octagonal, Corners);
      
      return InnerBounds.GetCorner(compass.CompassPoint, RoomShape.SquareCorners, Corners);
    }


    public override Vector GetPortStalkPosition(Port port)
    {
      var outerBounds = InnerBounds;
      outerBounds.Inflate(Settings.ConnectionStalkLength);
      var compass = (CompassPort) port;
      var inner = InnerBounds.GetCorner(compass.CompassPoint);
      var outer = outerBounds.GetCorner(compass.CompassPoint);
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

    public override string GetToolTipText()
    {
      var sText = $"{PrimaryDescription}";

      return sText;
    }

    public Port PortAt(CompassPoint compassPoint)
    {
      return PortList.Cast<CompassPort>().FirstOrDefault(port => port.CompassPoint == compassPoint);
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

    public Vector quarterPoint(Vector frompoint, Vector topoint)
    {
            var retVector = new Vector();
            retVector.X = (frompoint.X * 3 + topoint.X) / 4;
            retVector.Y = (frompoint.Y * 3 + topoint.Y) / 4;
            return retVector;
    }

    public override void Draw(XGraphics graphics, Palette palette, DrawingContext context)
    {
      //if we are drawing a new room, we need to edit the code at three points:
      //1. if (IsStartRoom) => start room outline
      //2. if (context.Selected) => selected room outline
      //3. if (!Settings.DebugDisableLineRendering => main default outline

      StraightEdges = !HandDrawnEdges;

      var random = new Random(Name.GetHashCode());

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
      var halfTopLeft = new LineSegment(topCenter, topLeft);
      var halfBottomRight = new LineSegment(bottomRight, bottomCenter);
      var halfBottomLeft = new LineSegment(bottomLeft, bottomCenter);
      var centerVertical = new LineSegment(bottomCenter, topCenter);

      var centerHorizontal = new LineSegment(leftCenter, rightCenter);
      var halfRightBottom = new LineSegment(rightCenter, bottomRight);
      var halfLeftBottom = new LineSegment(bottomLeft, leftCenter);
      var halfRightTop = new LineSegment(rightCenter, topRight);
      var halfLeftTop = new LineSegment(topLeft, leftCenter);

      var slantUp = new LineSegment(bottomLeft, topRight);
      var slantDown = new LineSegment(bottomRight, topLeft);

      context.LinesDrawn.Add(top);
      context.LinesDrawn.Add(right);
      context.LinesDrawn.Add(bottom);
      context.LinesDrawn.Add(left);

      // if starting room: this is the code to draw a yellow-green boundary around the start room
      if (IsStartRoom)
      {
        var tBounds = InnerBounds;
        tBounds.Inflate(5);

        var q = tBounds.Left;

        var topLeftSelect = tBounds.GetCorner(CompassPoint.NorthWest);
        var topRightSelect = tBounds.GetCorner(CompassPoint.NorthEast);
        var bottomLeftSelect = tBounds.GetCorner(CompassPoint.SouthWest);
        var bottomRightSelect = tBounds.GetCorner(CompassPoint.SouthEast);

        var topSelect = new LineSegment(topLeftSelect, topRightSelect);
        var rightSelect = new LineSegment(topRightSelect, bottomRightSelect);
        var bottomSelect = new LineSegment(bottomRightSelect, bottomLeftSelect);
        var leftSelect = new LineSegment(bottomLeftSelect, topLeftSelect);

        var pathSelected = palette.Path();
        if (RoundedCorners)
        {
          createRoomPath(pathSelected, topSelect, leftSelect);
        }
        else if (Ellipse)
        {
          pathSelected.AddEllipse(new RectangleF(topSelect.Start.X, topSelect.Start.Y, topSelect.Length, leftSelect.Length));
        }
        else if (Octagonal)
        {
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(bottomLeftSelect, topLeftSelect), quarterPoint(topLeftSelect, bottomLeftSelect)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(topLeftSelect, bottomLeft), quarterPoint(topLeftSelect, topRight)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(topLeftSelect, topRight), quarterPoint(topRightSelect, topLeft)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(topRightSelect, topLeft), quarterPoint(topRightSelect, bottomRight)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(topRightSelect, bottomRight), quarterPoint(bottomRightSelect, topRight)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(bottomRightSelect, topRight), quarterPoint(bottomRightSelect, bottomLeft)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(bottomRightSelect, bottomLeft), quarterPoint(bottomLeftSelect, bottomRight)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(bottomLeftSelect, bottomRight), quarterPoint(bottomLeftSelect, topLeft)),
               random, StraightEdges);
        }
        else
        {
          Drawing.AddLine(pathSelected, topSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, rightSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, bottomSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, leftSelect, random, StraightEdges);
        }
        var brushSelected = new SolidBrush(Settings.Color[Colors.StartRoom]);
        graphics.DrawPath(brushSelected, pathSelected);
      }

      //this is the code to draw the yellow boundary around a selected room
      if (context.Selected)
      {
        var tBounds = InnerBounds;
        tBounds.Inflate(5);

        var topLeftSelect = tBounds.GetCorner(CompassPoint.NorthWest);
        var topRightSelect = tBounds.GetCorner(CompassPoint.NorthEast);
        var bottomLeftSelect = tBounds.GetCorner(CompassPoint.SouthWest);
        var bottomRightSelect = tBounds.GetCorner(CompassPoint.SouthEast);

        var topSelect = new LineSegment(topLeftSelect, topRightSelect);
        var rightSelect = new LineSegment(topRightSelect, bottomRightSelect);
        var bottomSelect = new LineSegment(bottomRightSelect, bottomLeftSelect);
        var leftSelect = new LineSegment(bottomLeftSelect, topLeftSelect);

        var pathSelected = palette.Path();
        if (RoundedCorners)
        {
          createRoomPath(pathSelected, topSelect, leftSelect);
        }
        else if (Ellipse)
        {
          pathSelected.AddEllipse(new RectangleF(topSelect.Start.X, topSelect.Start.Y, topSelect.Length, leftSelect.Length));
        }
        else if (Octagonal)
        {
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(bottomLeftSelect, topLeftSelect), quarterPoint(topLeftSelect, bottomLeftSelect)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(topLeftSelect, bottomLeftSelect), quarterPoint(topLeftSelect, topRightSelect)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(topLeftSelect, topRightSelect), quarterPoint(topRightSelect, topLeftSelect)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(topRightSelect, topLeftSelect), quarterPoint(topRightSelect, bottomRightSelect)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(topRightSelect, bottomRightSelect), quarterPoint(bottomRightSelect, topRightSelect)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(bottomRightSelect, topRightSelect), quarterPoint(bottomRightSelect, bottomLeftSelect)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(bottomRightSelect, bottomLeftSelect), quarterPoint(bottomLeftSelect, bottomRightSelect)),
               random, StraightEdges);
           Drawing.AddLine(pathSelected, new LineSegment(quarterPoint(bottomLeftSelect, bottomRightSelect), quarterPoint(bottomLeftSelect, topLeftSelect)),
               random, StraightEdges);
        }
        else
        {
          Drawing.AddLine(pathSelected, topSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, rightSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, bottomSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, leftSelect, random, StraightEdges);
        }
        var brushSelected = new SolidBrush(Color.Gold);
        graphics.DrawPath(brushSelected, pathSelected);
      }

      // get region color
      var regionColor = Settings.Regions.FirstOrDefault(p => p.RegionName.Equals(Region, StringComparison.OrdinalIgnoreCase)) ?? Settings.Regions.FirstOrDefault(p => p.RegionName.Equals(Trizbort.Region.DefaultRegion, StringComparison.OrdinalIgnoreCase));
      Brush brush = new SolidBrush(regionColor.RColor);

      // Room specific fill brush (White shows global color)
      if (RoomFill != Color.Transparent) { brush = new SolidBrush(RoomFill); }

      // this is the main drawing routine for the actual room borders
      if (!Settings.DebugDisableLineRendering && BorderStyle != BorderDashStyle.None)
      {
        var path = palette.Path();

        if (RoundedCorners)
        {
          createRoomPath(path, top, left);
        }
        else if (Ellipse)
        {
          path.AddEllipse(new RectangleF(top.Start.X, top.Start.Y, top.Length, left.Length));
          
        }
        else if (Octagonal)
        {
           Drawing.AddLine(path, new LineSegment(quarterPoint(bottomLeft, topLeft), quarterPoint(topLeft, bottomLeft)),
               random, StraightEdges);
           Drawing.AddLine(path, new LineSegment(quarterPoint(topLeft, bottomLeft), quarterPoint(topLeft, topRight)),
               random, StraightEdges);
           Drawing.AddLine(path, new LineSegment(quarterPoint(topLeft, topRight), quarterPoint(topRight, topLeft)),
               random, StraightEdges);
           Drawing.AddLine(path, new LineSegment(quarterPoint(topRight, topLeft), quarterPoint(topRight, bottomRight)),
               random, StraightEdges);
           Drawing.AddLine(path, new LineSegment(quarterPoint(topRight, bottomRight), quarterPoint(bottomRight, topRight)),
               random, StraightEdges);
           Drawing.AddLine(path, new LineSegment(quarterPoint(bottomRight, topRight), quarterPoint(bottomRight, bottomLeft)),
               random, StraightEdges);
           Drawing.AddLine(path, new LineSegment(quarterPoint(bottomRight, bottomLeft), quarterPoint(bottomLeft, bottomRight)),
               random, StraightEdges);
           Drawing.AddLine(path, new LineSegment(quarterPoint(bottomLeft, bottomRight), quarterPoint(bottomLeft, topLeft)),
               random, StraightEdges);
        }
        else
        {
          Drawing.AddLine(path, top, random, StraightEdges);
          Drawing.AddLine(path, right, random, StraightEdges);
          Drawing.AddLine(path, bottom, random, StraightEdges);
          Drawing.AddLine(path, left, random, StraightEdges);
        }
        graphics.DrawPath(brush, path);

        // Second fill for room specific colors with a split option
        if (SecondFill != Color.Transparent)
        {
          var state = graphics.Save();
          graphics.IntersectClip(path);

          // Set the second fill color
          brush = new SolidBrush(SecondFill);

          // Define the second path based on the second fill location
          var secondPath = palette.Path();
          switch (SecondFillLocation)
          {
            case "Bottom":
              Drawing.AddLine(secondPath, centerHorizontal, random, StraightEdges);
              Drawing.AddLine(secondPath, halfRightBottom, random, StraightEdges);
              Drawing.AddLine(secondPath, bottom, random, StraightEdges);
              Drawing.AddLine(secondPath, halfLeftBottom, random, StraightEdges);
              break;
            case "BottomRight":
              Drawing.AddLine(secondPath, slantUp, random, StraightEdges);
              Drawing.AddLine(secondPath, right, random, StraightEdges);
              Drawing.AddLine(secondPath, bottom, random, StraightEdges);
              break;
            case "BottomLeft":
              Drawing.AddLine(secondPath, slantDown, random, StraightEdges);
              Drawing.AddLine(secondPath, bottom, random, StraightEdges);
              Drawing.AddLine(secondPath, left, random, StraightEdges);
              break;
            case "Left":
              Drawing.AddLine(secondPath, halfTopLeft, random, StraightEdges);
              Drawing.AddLine(secondPath, left, random, StraightEdges);
              Drawing.AddLine(secondPath, halfBottomLeft, random, StraightEdges);
              Drawing.AddLine(secondPath, centerVertical, random, StraightEdges);
              break;
            case "Right":
              Drawing.AddLine(secondPath, halfTopRight, random, StraightEdges);
              Drawing.AddLine(secondPath, right, random, StraightEdges);
              Drawing.AddLine(secondPath, halfBottomRight, random, StraightEdges);
              Drawing.AddLine(secondPath, centerVertical, random, StraightEdges);
              break;
            case "TopRight":
              Drawing.AddLine(secondPath, top, random, StraightEdges);
              Drawing.AddLine(secondPath, right, random, StraightEdges);
              Drawing.AddLine(secondPath, slantDown, random, StraightEdges);
              break;
            case "TopLeft":
              Drawing.AddLine(secondPath, top, random, StraightEdges);
              Drawing.AddLine(secondPath, slantUp, random, StraightEdges);
              Drawing.AddLine(secondPath, left, random, StraightEdges);
              break;
            case "Top":
              Drawing.AddLine(secondPath, centerHorizontal, random, StraightEdges);
              Drawing.AddLine(secondPath, halfRightTop, random, StraightEdges);
              Drawing.AddLine(secondPath, top, random, StraightEdges);
              Drawing.AddLine(secondPath, halfLeftTop, random, StraightEdges);
              break;
            default:
              break;
          }
          // Draw the second fill over the first
          graphics.DrawPath(brush, secondPath);
          graphics.Restore(state);
        }
        if (IsDark)
        {
          var state = graphics.Save();
          var solidbrush = (SolidBrush)palette.BorderBrush;
          float darknessXDistance = Settings.DarknessStripeSize;
          float darknessYDistance = Settings.DarknessStripeSize;
          graphics.IntersectClip(path);
          if (Ellipse)
          {
            darknessYDistance = 2 * this.Height / 5;
            darknessXDistance = 2 * this.Width / 5;
          }
          else if (RoundedCorners)
          {
            if (Corners.TopRight > 2 * Settings.DarknessStripeSize)
              darknessYDistance = darknessXDistance = (float)Corners.TopRight / 2;
          }
          else if (Octagonal)
          {
            darknessXDistance = this.Width * 7 / 20;
            darknessYDistance = this.Height * 7 / 20;
          }

          graphics.DrawPolygon(solidbrush, new[] { topRight.ToPointF(), new PointF(topRight.X - (darknessXDistance), topRight.Y), new PointF(topRight.X, topRight.Y + darknessYDistance) }, XFillMode.Alternate);
          graphics.Restore(state);
        }

        if (RoomBorder == Color.Transparent)
        {
          var pen = palette.BorderPen;
          pen.DashStyle = BorderStyle.ConvertToDashStyle();
          graphics.DrawPath(pen, path);
        }
        else
        {
          var roomBorderPen = new Pen(RoomBorder, Settings.LineWidth) {StartCap = LineCap.Round, EndCap = LineCap.Round, DashStyle = BorderStyle.ConvertToDashStyle()};
          graphics.DrawPath(roomBorderPen, path);
        }
      }

      var font = Settings.LargeFont;
      var roombrush = new SolidBrush(regionColor.TextColor);
      // Room specific fill brush (White shows global color)
      if (RoomLargeText != Color.Transparent) { roombrush = new SolidBrush(RoomLargeText); }

      var textBounds = InnerBounds;
      if (Ellipse)
        textBounds.Inflate(-11.5f);
      else
        textBounds.Inflate(-5, -5);

      if (textBounds.Width > 0 && textBounds.Height > 0)
      {
        if (!Settings.DebugDisableTextRendering)
        {
          var RoomTextRect = mName.Draw(graphics, font, roombrush, textBounds.Position, textBounds.Size, XStringFormats.Center);
          var SubtitleTextRect = new Rect(RoomTextRect.Left, RoomTextRect.Bottom, RoomTextRect.Right - RoomTextRect.Left, textBounds.Bottom - RoomTextRect.Bottom);
          mSubTitle.Draw(graphics, Settings.LineFont, roombrush, SubtitleTextRect.Position, SubtitleTextRect.Size, XStringFormats.Center);
        }
      }

      var expandedBounds = InnerBounds;
      expandedBounds.Inflate(Settings.ObjectListOffsetFromRoom, Settings.ObjectListOffsetFromRoom);
      var drawnObjectList = false;

      font = Settings.SmallFont;
      brush = palette.SmallTextBrush;
      // Room specific fill brush (White shows global color)
      var bUseObjectRoomBrush = false;
      if (RoomSmallText != Color.Transparent)
      {
        bUseObjectRoomBrush = true;
        brush = new SolidBrush(RoomSmallText);
      }

      if (!string.IsNullOrEmpty(Objects))
      {
        var format = new XStringFormat();
        var pos = expandedBounds.GetCorner(mObjectsPosition);

        string tempStr = mObjects.Text;
        Regex rgx = new Regex(@"\[[^\]\[]*\]");
        mObjects.Text = rgx.Replace(mObjects.Text, "");

        if (!Drawing.SetAlignmentFromCardinalOrOrdinalDirection(format, mObjectsPosition))
        {
          // object list appears inside the room below its name
          format.LineAlignment = XLineAlignment.Far;
          format.Alignment = XStringAlignment.Near;
          var height = InnerBounds.Height/2 - font.Height/2;
          var bounds = new Rect(InnerBounds.Left + Settings.ObjectListOffsetFromRoom, InnerBounds.Bottom - height, InnerBounds.Width - Settings.ObjectListOffsetFromRoom, height - Settings.ObjectListOffsetFromRoom);
          brush = (bUseObjectRoomBrush ? new SolidBrush(RoomSmallText) : roombrush);
          if (bounds.Width > 0 && bounds.Height > 0)
          {
            mObjects.Draw(graphics, font, brush, bounds.Position, bounds.Size, format);
          }
          drawnObjectList = true;
        }
        else if (mObjectsPosition == CompassPoint.North || mObjectsPosition == CompassPoint.South)
        {
          pos.X += Settings.ObjectListOffsetFromRoom;
        }

        if (!drawnObjectList)
        {
          if (!Settings.DebugDisableTextRendering)
          {
            var tString = mObjects.Text;
            var displayObjects = new TextBlock() {Text = tString};

            var block = displayObjects.Draw(graphics, font, brush, pos, Vector.Zero, format);

          }
        }
        mObjects.Text = tempStr;
      }
    }

    private void createRoomPath(XGraphicsPath path, LineSegment top, LineSegment left)
    {
      path.AddArc(top.Start.X + top.Length - (Corners.TopRight*2), top.Start.Y, Corners.TopRight*2, Corners.TopRight*2, 270, 90);
      path.AddArc(top.Start.X + top.Length - (Corners.BottomRight*2), top.Start.Y + left.Length - (Corners.BottomRight*2), Corners.BottomRight*2, Corners.BottomRight*2, 0, 90);
      path.AddArc(top.Start.X, top.Start.Y + left.Length - (Corners.BottomLeft*2), Corners.BottomLeft*2, Corners.BottomLeft*2, 90, 90);
      path.AddArc(top.Start.X, top.Start.Y, Corners.TopLeft*2, Corners.TopLeft * 2, 180, 90);
      path.CloseFigure();
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

    public void ShowDialog(PropertiesStartType startPoint)
    {
      showRoomDialog(startPoint);
    }


    public override void ShowDialog()
    {
      showRoomDialog();
    }

    private void showRoomDialog(PropertiesStartType start = PropertiesStartType.RoomName)
    {
      using (var dialog = new RoomPropertiesDialog(start, this.ID))
      {
        dialog.RoomName = Name;
        dialog.Description = PrimaryDescription;
        dialog.RoomSubTitle = SubTitle;
        dialog.IsDark = IsDark;
        dialog.IsStartRoom = IsStartRoom;
        dialog.HandDrawnEdges = HandDrawnEdges;
        dialog.Objects = Objects;
        dialog.ObjectsPosition = ObjectsPosition;
        dialog.BorderStyle = BorderStyle;

        dialog.RoomFillColor = RoomFill;
        dialog.SecondFillColor = SecondFill;
        dialog.SecondFillLocation = SecondFillLocation;
        dialog.RoomBorderColor = RoomBorder;
        dialog.RoomTextColor = RoomLargeText;
        dialog.ObjectTextColor = RoomSmallText;
        dialog.RoomRegion = Region;
        dialog.Corners = Corners;
        dialog.RoundedCorners = RoundedCorners;
        //dialog.Octagonal = Octagonal;
        dialog.Ellipse = Ellipse;
        dialog.StraightEdges = StraightEdges;
        dialog.AllCornersEqual = AllCornersEqual;
        dialog.Shape = Shape;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Name = dialog.RoomName;
          SubTitle = dialog.RoomSubTitle;
          if (PrimaryDescription != dialog.Description)
          {
            ClearDescriptions();
            AddDescription(dialog.Description);
          }
          IsDark = dialog.IsDark;
          IsStartRoom = dialog.IsStartRoom;
          HandDrawnEdges = dialog.HandDrawnEdges;
          Objects = dialog.Objects;
          BorderStyle = dialog.BorderStyle;
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
          Region = dialog.RoomRegion;
          Corners = dialog.Corners;
          RoundedCorners = dialog.RoundedCorners;
          Shape = dialog.Shape;
          //Octagonal = dialog.Octagonal;
          Ellipse = dialog.Ellipse;
          StraightEdges = dialog.StraightEdges;
          AllCornersEqual = dialog.AllCornersEqual;
        }
      }
    }

    public void Save(XmlScribe scribe)
    {
      scribe.Attribute("name", Name);
      scribe.Attribute("subtitle", SubTitle);
      scribe.Attribute("x", Position.X);
      scribe.Attribute("y", Position.Y);
      scribe.Attribute("w", Size.X);
      scribe.Attribute("h", Size.Y);
      scribe.Attribute("region", string.IsNullOrEmpty(Region) ? Trizbort.Region.DefaultRegion : Region);
      scribe.Attribute("handDrawn", StraightEdges);
      scribe.Attribute("allcornersequal", AllCornersEqual);
      scribe.Attribute("ellipse", Ellipse);
      scribe.Attribute("roundedCorners", RoundedCorners);
      scribe.Attribute("octagonal", Octagonal);
      scribe.Attribute("cornerTopLeft",(float) Corners.TopLeft);
      scribe.Attribute("cornerTopRight",(float) Corners.TopRight);
      scribe.Attribute("cornerBottomLeft",(float) Corners.BottomLeft);
      scribe.Attribute("cornerBottomRight",(float) Corners.BottomRight);

      scribe.Attribute("borderstyle",BorderStyle.ToString());
      if (IsDark)
      {
        scribe.Attribute("isDark", IsDark);
      }

      if (IsStartRoom)
      {
        scribe.Attribute("isStartRoom", IsStartRoom);
      }

      scribe.Attribute("description", PrimaryDescription);

      var colorValue = Colors.SaveColor(RoomFill);
      scribe.Attribute("roomFill", colorValue);

      colorValue = Colors.SaveColor(SecondFill);
      scribe.Attribute("secondFill", colorValue);
      scribe.Attribute("secondFillLocation", SecondFillLocation);

      colorValue = Colors.SaveColor(RoomBorder);
      scribe.Attribute("roomBorder", colorValue);

      colorValue = Colors.SaveColor(RoomLargeText);
      scribe.Attribute("roomLargeText", colorValue);

      colorValue = Colors.SaveColor(RoomSmallText);
      scribe.Attribute("roomSmallText", colorValue);

      // Up to this point was added to turn colors to Hex code for xmpl saving/loading

      if (!string.IsNullOrEmpty(Objects) || ObjectsPosition != DEFAULT_OBJECTS_POSITION)
      {
        scribe.StartElement("objects");
        if (ObjectsPosition != DEFAULT_OBJECTS_POSITION)
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
      SubTitle = element.Attribute("subtitle").Text;
      ClearDescriptions();
      AddDescription(element.Attribute("description").Text);
      Position = new Vector(element.Attribute("x").ToFloat(), element.Attribute("y").ToFloat());
      Size = new Vector(element.Attribute("w").ToFloat(), element.Attribute("h").ToFloat());
      Region = element.Attribute("region").Text;
      IsDark = element.Attribute("isDark").ToBool();
      IsStartRoom = element.Attribute("isStartRoom").ToBool();
      if (IsStartRoom)
      {
          if (Settings.startRoomLoaded)
            MessageBox.Show($"{Name} is a duplicate start room. You may need to erase \"isStartRoom=YES\" from the XML.", "Duplicate start room warning");
          else
            Settings.startRoomLoaded = true;
      }
      //Note: long term, we probably want an app default for this, but for now, let's force a room shape. #93 should fix this code along with #149.
      //We also should not have two of these at once.
      if (element.Attribute("roundedCorners").ToBool())
        this.Shape = RoomShape.RoundedCorners;
      else if (element.Attribute("octagonal").ToBool())
        this.Shape = RoomShape.Octagonal;
      else if (element.Attribute("ellipse").ToBool())
        this.Shape = RoomShape.Ellipse;
      else
        this.Shape = RoomShape.SquareCorners;

      StraightEdges = element.Attribute("handDrawn").ToBool();
      AllCornersEqual = element.Attribute("allcornersequal").ToBool();

      Corners = new CornerRadii();
      Corners.TopLeft = element.Attribute("cornerTopLeft").ToFloat();
      Corners.TopRight = element.Attribute("cornerTopRight").ToFloat();
      Corners.BottomLeft = element.Attribute("cornerBottomLeft").ToFloat();
      Corners.BottomRight = element.Attribute("cornerBottomRight").ToFloat();


      if (element.Attribute("borderstyle").Text != "")
        BorderStyle = (BorderDashStyle)Enum.Parse(typeof(BorderDashStyle), element.Attribute("borderstyle").Text);

      if (Project.Version.CompareTo(new Version(1, 5, 8, 3)) < 0)
      {
        if (element.Attribute("roomFill").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") { RoomFill = ColorTranslator.FromHtml(element.Attribute("roomFill").Text); }
        if (element.Attribute("secondFill").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") { SecondFill = ColorTranslator.FromHtml(element.Attribute("secondFill").Text); }
        if (element.Attribute("secondFillLocation").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") { SecondFillLocation = element.Attribute("secondFillLocation").Text; }
        if (element.Attribute("roomBorder").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") { RoomBorder = ColorTranslator.FromHtml(element.Attribute("roomBorder").Text); }
        if (element.Attribute("roomLargeText").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") { RoomLargeText = ColorTranslator.FromHtml(element.Attribute("roomLargeText").Text); }
        if (element.Attribute("roomSmallText").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") { RoomSmallText = ColorTranslator.FromHtml(element.Attribute("roomSmallText").Text); }
      }
      else
      {
        if (element.Attribute("roomFill").Text != "") { RoomFill = ColorTranslator.FromHtml(element.Attribute("roomFill").Text); }
        if (element.Attribute("secondFill").Text != "") { SecondFill = ColorTranslator.FromHtml(element.Attribute("secondFill").Text); }
        if (element.Attribute("secondFillLocation").Text != "") { SecondFillLocation = element.Attribute("secondFillLocation").Text; }
        if (element.Attribute("roomBorder").Text != "") { RoomBorder = ColorTranslator.FromHtml(element.Attribute("roomBorder").Text); }
        if (element.Attribute("roomLargeText").Text != "") { RoomLargeText = ColorTranslator.FromHtml(element.Attribute("roomLargeText").Text); }
        if (element.Attribute("roomSmallText").Text != "") { RoomSmallText = ColorTranslator.FromHtml(element.Attribute("roomSmallText").Text); }
      }
      Objects = element["objects"].Text.Replace("|", "\r\n").Replace("\\\r\n", "|");
      ObjectsPosition = element["objects"].Attribute("at").ToCompassPoint(ObjectsPosition);
    }

    public List<Connection> GetConnections()
    {
      return GetConnections(null);
    }
    
    public List<Connection> GetConnections(CompassPoint? compassPoint)
    {
      var connections = new List<Connection>();

      // TODO: This is needlessly expensive, traversing as it does the entire project's element list.
      foreach (var element in Project.Current.Elements.OfType<Connection>())
      {
        var connection = element;
        foreach (var vertex in connection.VertexList)
        {
          var port = vertex.Port;
          if (port == null || port.Owner != this || !(port is CompassPort))
            continue;

          var compassPort = (CompassPort) vertex.Port;
          
          if (compassPoint == null)
            connections.Add(connection);
          else
            if (compassPort.CompassPoint == compassPoint)
            {
              // found a connection with a vertex joined to our room's port at the given compass point
              connections.Add(connection);
            }
        }
      }
      return connections;
    }

    public IList<string> ListOfObjects()
    {
      var tObjects = Objects.Replace("\r", string.Empty).Replace("|", "\\|").Replace("\n", "|");
      var objects = tObjects.Split('|').Where(p => p != string.Empty).ToList();
      return objects;
    }

    public void AdjustAllRoomConnections()
    {
        bool somethingChanged = false;
        foreach (var element in this.GetConnections())
        {
            if (element.VertexList[0].Port.Owner == element.VertexList[1].Port.Owner) { continue; }

            CompassPoint cp = CompassPoint.Min;

            float xDelta = element.VertexList[0].Port.Owner.Position.X - element.VertexList[1].Port.Owner.Position.X;
            float yDelta = element.VertexList[0].Port.Owner.Position.Y - element.VertexList[1].Port.Owner.Position.Y;

            if ((xDelta == 0) && (yDelta == 0)) { continue; }
            if (xDelta == 0) { cp = CompassPoint.North; }
            else
            {
                float slope = (yDelta / xDelta);
                float abSlope = Math.Abs(slope);
                bool isNeg = (slope > 0);
                bool isLeft = (xDelta > 0);
                switch (Settings.PortAdjustDetail)
                {  //These numbers are decided as follows: tangent of 45 degrees, then 22.5/67.5, then 11.25/33.75/56.25/78.75
                    case 0: //fourths
                        if (abSlope > 1) { cp = CompassPoint.North; }
                        else { cp = CompassPoint.East; if (isLeft) { cp = CompassPoint.West; } }
                        break;
                    case 1: //eighths
                        if (abSlope > 2.414) { cp = CompassPoint.North; } //incidentally tan (pi/8) = sqrt 2 - 1. Angle bisector theorem/trig identities prove it.
                        else if (abSlope > 0.414) { cp = CompassPoint.NorthEast; if (isNeg) { cp = CompassPoint.NorthWest; } }
                        else { cp = CompassPoint.East; if (isLeft) { cp = CompassPoint.West; } }
                        break;
                    case 2: //sixteenths
                        if (abSlope > 5.03) { cp = CompassPoint.North; }
                        else if (abSlope > 1.49) { cp = CompassPoint.NorthNorthEast; if (isNeg) { cp = CompassPoint.NorthNorthWest; } }
                        else if (abSlope > 0.668) { cp = CompassPoint.NorthEast; if (isNeg) { cp = CompassPoint.NorthWest; } }
                        else if (abSlope > 0.197) { cp = CompassPoint.EastNorthEast; if (isNeg) { cp = CompassPoint.WestNorthWest; } }
                        else { cp = CompassPoint.East; if (isLeft) { cp = CompassPoint.West; } }
                        break;
                }
            }
            //we need to check we're not totally backwards here. This code appears correct, but I'm defining the boolean in case there's
            //a special case I forgot.
            bool backwards = (yDelta < 0);
            if (backwards)
            {
            cp = CompassPointHelper.GetOpposite(cp);
            }
            int cpInt = (int)cp;
            if (element.VertexList[0].Port != element.VertexList[0].Port.Owner.Ports[cpInt])
            {
                somethingChanged = true;
                element.VertexList[0].Port = element.VertexList[0].Port.Owner.Ports[cpInt];
            }
            int cpIntOpposite = (int)(CompassPointHelper.GetOpposite(cp));
            if (element.VertexList[1].Port != element.VertexList[1].Port.Owner.Ports[cpIntOpposite])
            {
                element.VertexList[1].Port = element.VertexList[1].Port.Owner.Ports[cpIntOpposite];
                somethingChanged = true;
            }
        }
        if (somethingChanged) RaiseChanged();
    }

    public void DeleteAllRoomConnections()
    {
      bool zappedOne = false;
      foreach (var element in this.GetConnections())
      {
      Project.Current.Elements.Remove(element); zappedOne = true;
      }
      if (zappedOne) { RaiseChanged(); } else { MessageBox.Show("No connections were deleted.", "Nothing to delete");  }

/*      foreach (var b in this.L)
      {
        Project.Current.Elements.Remove(b.);
      }*/

    }

    public void ClearDescriptions()
    {
      if (mDescriptions.Count > 0)
      {
        mDescriptions.Clear();
        RaiseChanged();
      }
    }

    public void AddDescription(string description)
    {
      if (string.IsNullOrEmpty(description))
      {
        // never add an empty description
        return;
      }

      if (mDescriptions.Any(existing => existing == description))
      {
        return;
      }

      // we don't have this (non-empty) description already; add it
      mDescriptions.Add(description);
      RaiseChanged();
    }

    public bool MatchDescription(string description)
    {
      if (string.IsNullOrEmpty(description))
      {
        // match a lack of description if we have no descriptions
        return mDescriptions.Count == 0;
      }

      return mDescriptions.Any(existing => existing == description);

      // no match
    }

    public string ClipboardPrint()
    {
      var clipboardText = "";
      clipboardText += Name + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Position.X + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Position.Y + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Size.X + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Size.Y + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += IsDark + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += PrimaryDescription + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Region + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += BorderStyle + UI.Controls.Canvas.CopyDelimiter;

      clipboardText += StraightEdges + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Ellipse + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += RoundedCorners + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Octagonal + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Corners.TopRight + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Corners.TopLeft + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Corners.BottomRight + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += Corners.BottomLeft + UI.Controls.Canvas.CopyDelimiter;

      var colorValue = Colors.SaveColor(RoomFill);
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(SecondFill);
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += SecondFillLocation + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(RoomBorder);
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(RoomLargeText); 
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(RoomSmallText);
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(RoomBorder);
      clipboardText += colorValue;


      if (!string.IsNullOrEmpty(Objects) || ObjectsPosition != DEFAULT_OBJECTS_POSITION)
      {
        var objectsDirection = "";
        CompassPointHelper.ToName(ObjectsPosition, out objectsDirection);
        clipboardText += UI.Controls.Canvas.CopyDelimiter + objectsDirection + UI.Controls.Canvas.CopyDelimiter;
        if (!string.IsNullOrEmpty(Objects))
        {
          clipboardText += (Objects.Replace("\r\n", UI.Controls.Canvas.CopyDelimiter));
        }
      }

      return clipboardText;
    }


    public string ClipboardColorPrint()
    {
      var clipboardText = string.Empty;

      var colorValue = Colors.SaveColor(RoomFill);
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(SecondFill); 
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;
      clipboardText += SecondFillLocation + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(RoomBorder);
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(RoomLargeText);
      clipboardText += colorValue + UI.Controls.Canvas.CopyDelimiter;

      colorValue = Colors.SaveColor(RoomSmallText);
      clipboardText += colorValue;

      return clipboardText;
    }

    internal class CompassPort : Port
    {
      public CompassPort(CompassPoint compassPoint, Room room) : base(room)
      {
        CompassPoint = compassPoint;
        Room = room;
      }

      public CompassPoint CompassPoint { get; set; }

      public override string ID
      {
        get
        {
          string name;
          return CompassPointHelper.ToName(CompassPoint, out name) ? name : string.Empty;
        }
      }

      public Room Room { get; private set; }
    }
  }

}

public class CornerRadii
{
  public double TopRight { get; set; } = 15.0;
  public double BottomRight { get; set; } = 15.0;
  public double TopLeft { get; set; } = 15.0;
  public double BottomLeft { get; set; } = 15.0;
}

public enum RoomShape
{
  SquareCorners,
  RoundedCorners,
  Ellipse,
  Octagonal
}

public enum PropertiesStartType
{
  RoomName, 
  Region
}