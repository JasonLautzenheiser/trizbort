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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using Trizbort.Domain.Application;
using Trizbort.Domain.AppSettings;
using Trizbort.Domain.Enums;
using Trizbort.Domain.Misc;
using Trizbort.Extensions;
using Trizbort.Setup;
using Trizbort.UI;
using Trizbort.Util;

namespace Trizbort.Domain.Elements {
  /// <summary>
  ///   A room in the project.
  /// </summary>
  public class Room : Element, ISizeable {
    private const CompassPoint DEFAULT_OBJECTS_POSITION = CompassPoint.South;
    private readonly List<string> mDescriptions = new List<string>();
    private readonly TextBlock mName = new TextBlock();
    private readonly TextBlock mObjects = new TextBlock();
    private readonly TextBlock mSubTitle = new TextBlock();
    private bool mAllCornersEqual = true;
    private BorderDashStyle mBorderStyle = BorderDashStyle.Solid;
    private CornerRadii mCorners;
    private bool mEllipse;
    private bool mHandDrawnEdges;
    private bool mIsDark;
    private bool mIsEndRoom;
    private bool mIsStartRoom;
    private CompassPoint mObjectsPosition = DEFAULT_OBJECTS_POSITION;

    private bool mOctagonal;

    // Added for linking connections when pasting

    private Vector mPosition;

    // Added for Room specific colors (White shows global color)
    private Color mRoomborder = Color.Transparent;

    private Color mRoomfill = Color.Transparent;
    private Color mRoomlargetext = Color.Transparent;
    private string mRoomRegion;
    private Color mRoomsmalltext = Color.Transparent;
    private Color mRoomSubtitleColor = Color.Transparent;
    private bool mRoundedCorners;
    private Color mSecondfill = Color.Transparent;
    private string mSecondfilllocation = "Bottom";
    private Vector mSize;
    private bool mStraightEdges;


    private RoomShape shape;

    public Room() {
      addPortsToRoom();
    }

    public Room(Project project) : base(project) {
      Name = Settings.DefaultRoomName;
      HandDrawnEdges = ApplicationSettingsController.AppSettings.HandDrawnGlobal;
      Region = Misc.Region.DefaultRegion;
      Size = new Vector(3 * Settings.GridSize, 2 * Settings.GridSize);
      Position = new Vector(-Size.X / 2, -Size.Y / 2);
      Corners = new CornerRadii();
      Shape = Settings.DefaultRoomShape;

      // connections may connect to any of our "corners"
      addPortsToRoom();
    }

    // Added this second constructor to be used when loading a room
    // This constructor is significantly faster as it doesn't look for gap in the element IDs
    public Room(Project project, int totalIDs) : base(project, totalIDs) {
      Name = Settings.DefaultRoomName;
      Region = Misc.Region.DefaultRegion;
      Size = new Vector(3 * Settings.GridSize, 2 * Settings.GridSize);
      Position = new Vector(-Size.X / 2, -Size.Y / 2);
      Corners = new CornerRadii();
      Shape = RoomShape.SquareCorners; //would be nice to make an app default later: issue #93

      // connections may connect to any of our "corners"
      addPortsToRoom();
    }

    public bool AllCornersEqual {
      get => mAllCornersEqual;
      set {
        if (mAllCornersEqual != value) {
          mAllCornersEqual = value;
          RaiseChanged();
        }
      }
    }

    public bool ArbitraryAutomappedPosition { get; set; }

    public BorderDashStyle BorderStyle {
      get => mBorderStyle;
      set {
        if (mBorderStyle == value) return;
        mBorderStyle = value;
        RaiseChanged();
      }
    }

    public CornerRadii Corners {
      get => mCorners;
      set {
        if (mCorners == null) {
          mCorners = value;
          return;
        }

        if (mCorners.BottomLeft != value.BottomLeft) {
          mCorners.BottomLeft = value.BottomLeft;
          RaiseChanged();
        } //mCorners never equals value...

        if (mCorners.BottomRight != value.BottomRight) {
          mCorners.BottomRight = value.BottomRight;
          RaiseChanged();
        }

        if (mCorners.TopLeft != value.TopLeft) {
          mCorners.TopLeft = value.TopLeft;
          RaiseChanged();
        }

        if (mCorners.TopRight != value.TopRight) {
          mCorners.TopRight = value.TopRight;
          RaiseChanged();
        }
      }
    }

    public override Depth Depth => Depth.Medium;

    public bool Ellipse {
      get => mEllipse;
      set {
        if (mEllipse != value) {
          mEllipse = value;
          RaiseChanged();
        }
      }
    }

    public bool HandDrawnEdges {
      get => mHandDrawnEdges;
      set {
        if (mHandDrawnEdges != value) {
          mHandDrawnEdges = value;
          RaiseChanged();
        }
      }
    }

    public bool HasDescription => mDescriptions.Count > 0;
    public override bool HasDialog => true;

    [JsonIgnore]
    public bool IsConnected {
      get {
        return Project.Current.Elements.OfType<Connection>()
                      .Any(element => element
                                      .VertexList.Select(vertex => vertex.Port)
                                      .Any(port => port != null && port.Owner == this));
      }
    }

    /// <summary>
    ///   Get/set whether the room is dark or lit.
    /// </summary>
    public bool IsDark {
      get => mIsDark;
      set {
        if (mIsDark == value) return;
        mIsDark = value;
        RaiseChanged();
      }
    }

    public bool IsEndRoom {
      get => mIsEndRoom;
      set {
        if (mIsEndRoom != value) {
          mIsEndRoom = value;
          RaiseChanged();
        }
      }
    }

    public bool IsReference => ReferenceRoom != null;

    public bool IsStartRoom {
      get => mIsStartRoom;
      set {
        if (mIsStartRoom != value) {
          mIsStartRoom = value;
          RaiseChanged();
        }
      }
    }

    /// <summary>
    ///   Get/set the name of the room.
    /// </summary>
    public override string Name {
      get => mName.Text;
      set {
        value = value ?? string.Empty;
        if (mName.Text == value) return;
        mName.Text = value;
        RaiseChanged();
      }
    }

    /// <summary>
    ///   Get/set the list of objects in the room.
    /// </summary>
    public string Objects {
      get => mObjects.Text;
      set {
        value = value ?? string.Empty;
        if (mObjects.Text == value) return;
        mObjects.Text = value;
        RaiseChanged();
      }
    }

    public bool ObjectsCustomPosition { get; set; }

    public int ObjectsCustomPositionDown { get; set; }
    public int ObjectsCustomPositionRight { get; set; }

    /// <summary>
    ///   Get/set the position, relative to the room,
    ///   at which the object list is drawn on the map.
    /// </summary>
    public CompassPoint ObjectsPosition {
      get => mObjectsPosition;
      set {
        if (mObjectsPosition == value) return;
        mObjectsPosition = value;
        RaiseChanged();
      }
    }

    public bool Octagonal {
      get => mOctagonal;
      set {
        if (mOctagonal != value) {
          mOctagonal = value;
          RaiseChanged();
        }
      }
    }

    // Added for linking connections when pasting
    public int OldID { get; set; }

    public string PrimaryDescription {
      get {
        if (mDescriptions.Count > 0)
          return mDescriptions[0];
        return null;
      }
    }

    public Room ReferenceRoom => Project.Current.Elements.OfType<Room>().FirstOrDefault(p => p.ID == ReferenceRoomId);
    public int ReferenceRoomId { get; set; } = -1;

    public string Region {
      get => mRoomRegion;
      set {
        if (mRoomRegion != value) {
          mRoomRegion = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color RoomBorderColor {
      get => mRoomborder;
      set {
        if (mRoomborder != value) {
          mRoomborder = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color RoomFillColor {
      get => mRoomfill;
      set {
        if (mRoomfill != value) {
          mRoomfill = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color RoomNameColor {
      get => mRoomlargetext;
      set {
        if (mRoomlargetext != value) {
          mRoomlargetext = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color RoomObjectTextColor {
      get => mRoomsmalltext;
      set {
        if (mRoomsmalltext != value) {
          mRoomsmalltext = value;
          RaiseChanged();
        }
      }
    }

    public Color RoomSubtitleColor {
      get => mRoomSubtitleColor;
      set {
        if (mRoomSubtitleColor != value) {
          mRoomSubtitleColor = value;
          RaiseChanged();
        }
      }
    }

    public bool RoundedCorners {
      get => mRoundedCorners;
      set {
        if (mRoundedCorners != value) {
          mRoundedCorners = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public Color SecondFillColor {
      get => mSecondfill;
      set {
        if (mSecondfill != value) {
          mSecondfill = value;
          RaiseChanged();
        }
      }
    }

    // Added for Room specific colors
    public string SecondFillLocation {
      get => mSecondfilllocation;
      set {
        if (mSecondfilllocation != value) {
          mSecondfilllocation = value;
          RaiseChanged();
        }
      }
    }

    public RoomShape Shape {
      get => shape;
      set {
        if (shape != value) {
          shape = value;
          setRoomShape(value);
          RaiseChanged();
        }
      }
    }

    public bool StraightEdges {
      get => mStraightEdges;
      set {
        if (mStraightEdges != value) mStraightEdges = value; //RaiseChanged(); This is disabled because it gives false positives
      }
    }

    /// <summary>
    ///   Get/set the subtitle of the room.
    /// </summary>
    public string SubTitle {
      get => mSubTitle.Text;
      set {
        value = value ?? string.Empty;
        if (mSubTitle.Text == value) return;
        mSubTitle.Text = value;

        RaiseChanged();
      }
    }

    public List<RoomValidationState> ValidationState { get; set; } = new List<RoomValidationState>();

    public sealed override Vector Position {
      get => mPosition;
      set {
        if (mPosition != value) {
          mPosition = value;
          ArbitraryAutomappedPosition = false;
          RaiseChanged();
        }
      }
    }

    public float X => mPosition.X;
    public float Y => mPosition.Y;
    public float Height => mSize.Y;

    public Rect InnerBounds => new Rect(Position, Size);

    public Vector Size {
      get => mSize;
      set {
        if (mSize != value) {
          mSize = value;
          RaiseChanged();
        }
      }
    }

    public float Width => mSize.X;

    public void AddDescription(string description) {
      if (string.IsNullOrEmpty(description))
        return;

      if (mDescriptions.Any(existing => existing == description))
        return;

      // we don't have this (non-empty) description already; add it
      mDescriptions.Add(description);
      RaiseChanged();
    }

    public void AdjustAllRoomConnections() {
      var somethingChanged = false;
      foreach (var element in GetConnections()) {
        if (element.VertexList[0].Port.Owner == element.VertexList[1].Port.Owner) continue;

        var cp = CompassPoint.Min;

        var xDelta = element.VertexList[0].Port.Owner.Position.X - element.VertexList[1].Port.Owner.Position.X;
        var yDelta = element.VertexList[0].Port.Owner.Position.Y - element.VertexList[1].Port.Owner.Position.Y;

        if (xDelta == 0 && yDelta == 0) continue;
        if (xDelta == 0) { cp = CompassPoint.North; } else {
          var slope = yDelta / xDelta;
          var abSlope = Math.Abs(slope);
          var isNeg = slope > 0;
          var isLeft = xDelta > 0;
          switch (ApplicationSettingsController.AppSettings.PortAdjustDetail) {
            //These numbers are decided as follows: tangent of 45 degrees, then 22.5/67.5, then 11.25/33.75/56.25/78.75
            case 0: //fourths
              if (abSlope > 1) { cp = CompassPoint.North; } else {
                cp = CompassPoint.East;
                if (isLeft) cp = CompassPoint.West;
              }

              break;
            case 1: //eighths
              if (abSlope > 2.414) { cp = CompassPoint.North; } //incidentally tan (pi/8) = sqrt 2 - 1. Angle bisector theorem/trig identities prove it.
              else if (abSlope > 0.414) {
                cp = CompassPoint.NorthEast;
                if (isNeg) cp = CompassPoint.NorthWest;
              } else {
                cp = CompassPoint.East;
                if (isLeft) cp = CompassPoint.West;
              }

              break;
            case 2: //sixteenths
              if (abSlope > 5.03) { cp = CompassPoint.North; } else if (abSlope > 1.49) {
                cp = CompassPoint.NorthNorthEast;
                if (isNeg) cp = CompassPoint.NorthNorthWest;
              } else if (abSlope > 0.668) {
                cp = CompassPoint.NorthEast;
                if (isNeg) cp = CompassPoint.NorthWest;
              } else if (abSlope > 0.197) {
                cp = CompassPoint.EastNorthEast;
                if (isNeg) cp = CompassPoint.WestNorthWest;
              } else {
                cp = CompassPoint.East;
                if (isLeft) cp = CompassPoint.West;
              }

              break;
          }
        }

        //we need to check we're not totally backwards here. This code appears correct, but I'm defining the boolean in case there's
        //a special case I forgot.
        var backwards = yDelta < 0;
        if (backwards)
          cp = CompassPointHelper.GetOpposite(cp);
        var cpInt = (int) cp;
        if (element.VertexList[0].Port != element.VertexList[0].Port.Owner.PortList[cpInt]) {
          somethingChanged = true;
          element.VertexList[0].Port = element.VertexList[0].Port.Owner.PortList[cpInt];
        }

        var cpIntOpposite = (int) CompassPointHelper.GetOpposite(cp);
        if (element.VertexList[1].Port != element.VertexList[1].Port.Owner.PortList[cpIntOpposite]) {
          element.VertexList[1].Port = element.VertexList[1].Port.Owner.PortList[cpIntOpposite];
          somethingChanged = true;
        }
      }

      if (somethingChanged) RaiseChanged();
    }

    public void CheckValidation() {
      RoomValidationState state;
      ValidationState.Clear();

      if (Project.Current.MustHaveDescription && !HasDescription) {
        state = new RoomValidationState {
          Message = "There is no description for this room.",
          Status = RoomValidationStatus.Invalid,
          Type = ValidationType.RoomDescription
        };
        ValidationState.Add(state);
      }

      if (Project.Current.MustHaveUniqueNames)
        if (Project.Current.Elements.OfType<Room>().Count(p => p.Name == Name) > 1) {
          state = new RoomValidationState {
            Message = "The room name is not unique.",
            Status = RoomValidationStatus.Invalid,
            Type = ValidationType.RoomUniqueName
          };
          ValidationState.Add(state);
        }

      if (Project.Current.MustHaveNoDanglingConnectors)
        if (Project.Current.Elements.OfType<Connection>().Count(p => p.GetSourceRoom() == this && p.GetTargetRoom() == null) > 0) {
          state = new RoomValidationState {
            Message = "Room has dangling connectors.",
            Status = RoomValidationStatus.Invalid,
            Type = ValidationType.RoomUniqueName
          };
          ValidationState.Add(state);
        }

      if (Project.Current.MustHaveSubtitle)
        if (string.IsNullOrWhiteSpace(SubTitle)) {
          state = new RoomValidationState {
            Message = "Room must have a subtitle.",
            Status = RoomValidationStatus.Invalid,
            Type = ValidationType.RoomUniqueName
          };
          ValidationState.Add(state);
        }
    }

    public void ClearDescriptions() {
      if (mDescriptions.Count > 0) {
        mDescriptions.Clear();
        RaiseChanged();
      }
    }


    public void DeleteAllRoomConnections() {
      var zappedOne = false;
      foreach (var element in GetConnections()) {
        Project.Current.Elements.Remove(element);
        zappedOne = true;
      }

      if (zappedOne) RaiseChanged();
      else MessageBox.Show("No connections were deleted.", "Nothing to delete");

/*      foreach (var b in this.L)
      {
        Project.Current.Elements.Remove(b.);
      }*/
    }


    public override float Distance(Vector pos, bool includeMargins) {
      var bounds = UnionBoundsWith(Rect.Empty, includeMargins);
      return pos.DistanceFromRect(bounds);
    }

    public override void Draw(XGraphics graphics, Palette palette, DrawingContext context) {
      CheckValidation();
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
      if (IsStartRoom || IsEndRoom || IsReference) {
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
        if (RoundedCorners) {
          createRoomPath(pathSelected, topSelect, leftSelect);
        } else if (Ellipse) {
          pathSelected.AddEllipse(new RectangleF(topSelect.Start.X, topSelect.Start.Y, topSelect.Length, leftSelect.Length));
        } else if (Octagonal) {
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
        } else {
          Drawing.AddLine(pathSelected, topSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, rightSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, bottomSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, leftSelect, random, StraightEdges);
        }

        SolidBrush brushSelected;
        if (IsReference)
          brushSelected = new SolidBrush(Color.LightBlue);
        else
          brushSelected = new SolidBrush(Settings.Color[IsStartRoom ? Colors.StartRoom : Colors.EndRoom]);
        graphics.DrawPath(brushSelected, pathSelected);
      }

      //this is the code to draw the yellow boundary around a selected room
      if (context.Selected) {
        var tBounds = InnerBounds;
        tBounds.Inflate(Project.Current.ActiveSelectedElement?.ID == ID ? 10 : 5);

        var topLeftSelect = tBounds.GetCorner(CompassPoint.NorthWest);
        var topRightSelect = tBounds.GetCorner(CompassPoint.NorthEast);
        var bottomLeftSelect = tBounds.GetCorner(CompassPoint.SouthWest);
        var bottomRightSelect = tBounds.GetCorner(CompassPoint.SouthEast);

        var topSelect = new LineSegment(topLeftSelect, topRightSelect);
        var rightSelect = new LineSegment(topRightSelect, bottomRightSelect);
        var bottomSelect = new LineSegment(bottomRightSelect, bottomLeftSelect);
        var leftSelect = new LineSegment(bottomLeftSelect, topLeftSelect);

        var pathSelected = palette.Path();
        if (RoundedCorners) {
          createRoomPath(pathSelected, topSelect, leftSelect);
        } else if (Ellipse) {
          pathSelected.AddEllipse(new RectangleF(topSelect.Start.X, topSelect.Start.Y, topSelect.Length, leftSelect.Length));
        } else if (Octagonal) {
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
        } else {
          Drawing.AddLine(pathSelected, topSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, rightSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, bottomSelect, random, StraightEdges);
          Drawing.AddLine(pathSelected, leftSelect, random, StraightEdges);
        }

        var brushSelected = Project.Current.ActiveSelectedElement?.ID == ID ? new SolidBrush(Color.Gold) : new SolidBrush(Color.Gold);
        graphics.DrawPath(brushSelected, pathSelected);
      }

      // get region color
      var regionColor = Settings.Regions.FirstOrDefault(p => p.RegionName.Equals(Region, StringComparison.OrdinalIgnoreCase)) ?? Settings.Regions.FirstOrDefault(p => p.RegionName.Equals(Misc.Region.DefaultRegion, StringComparison.OrdinalIgnoreCase));
      Brush brush = new SolidBrush(regionColor.RColor);

      // Room specific fill brush (White shows global color)
      if (RoomFillColor != Color.Transparent) brush = new SolidBrush(RoomFillColor);

      // this is the main drawing routine for the actual room borders
      if (!ApplicationSettingsController.AppSettings.DebugDisableLineRendering && BorderStyle != BorderDashStyle.None) {
        var path = palette.Path();

        if (RoundedCorners) {
          createRoomPath(path, top, left);
        } else if (Ellipse) {
          path.AddEllipse(new RectangleF(top.Start.X, top.Start.Y, top.Length, left.Length));
        } else if (Octagonal) {
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
        } else {
          Drawing.AddLine(path, top, random, StraightEdges);
          Drawing.AddLine(path, right, random, StraightEdges);
          Drawing.AddLine(path, bottom, random, StraightEdges);
          Drawing.AddLine(path, left, random, StraightEdges);
        }

        graphics.DrawPath(brush, path);

        // Second fill for room specific colors with a split option
        if (SecondFillColor != Color.Transparent) {
          var state = graphics.Save();
          graphics.IntersectClip(path);

          // Set the second fill color
          brush = new SolidBrush(SecondFillColor);

          // Define the second path based on the second fill location
          var secondPath = palette.Path();
          switch (SecondFillLocation) {
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

        if (IsDark) {
          var state = graphics.Save();
          var solidbrush = (SolidBrush) palette.BorderBrush;
          var darknessXDistance = Settings.DarknessStripeSize;
          var darknessYDistance = Settings.DarknessStripeSize;
          graphics.IntersectClip(path);
          if (Ellipse) {
            darknessYDistance = 2 * Height / 5;
            darknessXDistance = 2 * Width / 5;
          } else if (RoundedCorners) {
            if (Corners.TopRight > 2 * Settings.DarknessStripeSize)
              darknessYDistance = darknessXDistance = (float) Corners.TopRight / 2;
          } else if (Octagonal) {
            darknessXDistance = Width * 7 / 20;
            darknessYDistance = Height * 7 / 20;
          }

          graphics.DrawPolygon(solidbrush, new[] {topRight.ToPointF(), new PointF(topRight.X - darknessXDistance, topRight.Y), new PointF(topRight.X, topRight.Y + darknessYDistance)}, XFillMode.Alternate);
          graphics.Restore(state);
        }

        if (RoomBorderColor == Color.Transparent) {
          var pen = palette.BorderPen;
          pen.DashStyle = IsReference ? BorderDashStyle.Dot.ConvertToDashStyle() : BorderStyle.ConvertToDashStyle();
          graphics.DrawPath(pen, path);
        } else {
          var roomBorderPen = new Pen(RoomBorderColor, Settings.LineWidth) {StartCap = LineCap.Round, EndCap = LineCap.Round, DashStyle = IsReference ? BorderDashStyle.Dot.ConvertToDashStyle() : BorderStyle.ConvertToDashStyle()};
          graphics.DrawPath(roomBorderPen, path);
        }
      }

      var font = Settings.RoomNameFont;
      var roombrush = new SolidBrush(regionColor.TextColor);
      // Room specific fill brush (White shows global color)

      if (RoomNameColor != Color.Transparent) roombrush = new SolidBrush(RoomNameColor);

      var textBounds = InnerBounds;
      if (Ellipse)
        textBounds.Inflate(-11.5f);
      else
        textBounds.Inflate(-5, -5);

      if (textBounds.Width > 0 && textBounds.Height > 0)
        if (!ApplicationSettingsController.AppSettings.DebugDisableTextRendering) {
          var tName = IsReference ? new TextBlock {Text = "To"} : mName;
          var tSubtitle = IsReference ? new TextBlock {Text = ReferenceRoom.Name} : mSubTitle;
          var RoomTextRect = tName.Draw(graphics, font, roombrush, textBounds.Position, textBounds.Size, XStringFormats.Center);

          // draw subtitle text
          var subTitleBrush = IsReference ? roombrush : (RoomSubtitleColor != Color.Transparent ? new SolidBrush(RoomSubtitleColor) : palette.SubtitleTextBrush);
          var SubtitleTextRect = new Rect(RoomTextRect.Left, RoomTextRect.Bottom, RoomTextRect.Right - RoomTextRect.Left, textBounds.Bottom - RoomTextRect.Bottom);
          tSubtitle.Draw(graphics, Settings.SubtitleFont, subTitleBrush, SubtitleTextRect.Position, SubtitleTextRect.Size, XStringFormats.Center);
        }

      var expandedBounds = InnerBounds;
      expandedBounds.Inflate(Settings.ObjectListOffsetFromRoom, Settings.ObjectListOffsetFromRoom);
      var drawnObjectList = false;

      font = Settings.ObjectFont;
      brush = palette.SmallTextBrush;
      // Room specific fill brush (White shows global color)
      var bUseObjectRoomBrush = false;
      if (RoomObjectTextColor != Color.Transparent) {
        bUseObjectRoomBrush = true;
        brush = new SolidBrush(RoomObjectTextColor);
      }

      if (!string.IsNullOrEmpty(Objects)) {
        var format = new XStringFormat();
        var pos = expandedBounds.GetCorner(mObjectsPosition);

        var tempStr = mObjects.Text;
        var rgx = new Regex(@"\[[^\]\[]*\]");
        mObjects.Text = rgx.Replace(mObjects.Text, "");

        if (!Drawing.SetAlignmentFromCardinalOrOrdinalDirection(format, mObjectsPosition)) {
          // object list appears inside the room below its name
          format.LineAlignment = XLineAlignment.Far;
          format.Alignment = XStringAlignment.Near;
          var height = InnerBounds.Height / 2 - font.Height / 2;
          var bounds = new Rect(InnerBounds.Left + Settings.ObjectListOffsetFromRoom, InnerBounds.Bottom - height, InnerBounds.Width - Settings.ObjectListOffsetFromRoom, height - Settings.ObjectListOffsetFromRoom);
          if (bUseObjectRoomBrush)
            brush = new SolidBrush(RoomObjectTextColor);
          pos = bounds.Position;
          pos.X += ObjectsCustomPosition ? ObjectsCustomPositionRight : 0;
          pos.Y += ObjectsCustomPosition ? ObjectsCustomPositionDown : 0;
          if (bounds.Width > 0 && bounds.Height > 0)
            mObjects.Draw(graphics, font, brush, pos, bounds.Size, format);
          drawnObjectList = true;
        } else if (mObjectsPosition == CompassPoint.North || mObjectsPosition == CompassPoint.South) {
          pos.X += Settings.ObjectListOffsetFromRoom + (ObjectsCustomPosition ? ObjectsCustomPositionRight : 0);
          pos.Y += ObjectsCustomPosition ? ObjectsCustomPositionDown : 0;
        } else {
          pos.X += ObjectsCustomPosition ? ObjectsCustomPositionRight : 0;
          pos.Y += ObjectsCustomPosition ? ObjectsCustomPositionDown : 0;
        }

        if (!drawnObjectList)
          if (!ApplicationSettingsController.AppSettings.DebugDisableTextRendering) {
            var tString = mObjects.Text;
            var displayObjects = new TextBlock {Text = tString};

            var block = displayObjects.Draw(graphics, font, brush, pos, Vector.Zero, format);
          }

        mObjects.Text = tempStr;
      }

      if (!valid()) {
        var path = palette.Path();
        var pen = new Pen(Color.Red, 2.0f);
        pen.DashStyle = DashStyle.Solid;

        var xx = new PointF[2];
        xx[0] = new PointF(InnerBounds.Left, InnerBounds.Top);
        xx[1] = new PointF(InnerBounds.Right, InnerBounds.Bottom);

        path.AddLines(xx);
        graphics.DrawPath(pen, path);

        var yy = new PointF[2];
        yy[0] = new PointF(InnerBounds.Right, InnerBounds.Top);
        yy[1] = new PointF(InnerBounds.Left, InnerBounds.Bottom);

        path = palette.Path();
        path.AddLines(yy);

        graphics.DrawPath(pen, path);
      }
    }

    public List<Connection> GetConnections() {
      return GetConnections(null);
    }

    public List<Connection> GetConnections(CompassPoint? compassPoint) {
      var connections = new List<Connection>();

      // TODO: This is needlessly expensive, traversing as it does the entire project's element list.
      foreach (var element in Project.Current.Elements.OfType<Connection>()) {
        var connection = element;
        foreach (var vertex in connection.VertexList) {
          var port = vertex.Port;
          if (port == null || port.Owner != this || !(port is CompassPort))
            continue;

          var compassPort = (CompassPort) vertex.Port;

          if (compassPoint == null)
            connections.Add(connection);
          else if (compassPort.CompassPoint == compassPoint)
            connections.Add(connection);
        }
      }

      return connections;
    }

    public override Vector GetPortPosition(Port port) {
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


    public override Vector GetPortStalkPosition(Port port) {
      var outerBounds = InnerBounds;
      outerBounds.Inflate(Settings.ConnectionStalkLength);
      var compass = (CompassPort) port;
      var inner = InnerBounds.GetCorner(compass.CompassPoint);
      var outer = outerBounds.GetCorner(compass.CompassPoint);
      switch (compass.CompassPoint) {
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


    public override Color GetToolTipColor() {
      return !valid() ? Color.Red : Color.LightBlue;
    }

    public override string GetToolTipFooter() {
      return Objects;
    }

    public override string GetToolTipHeader() {
      var sText = $"{Name}{(!isDefaultRegion() ? $" ({Region})" : string.Empty)}";
      if (!valid()) {
        if (sText.Length > 0) sText += " - ";
        sText += "Room validation issues:";
      }

      return sText;
    }

    public override string GetToolTipText() {
      if (!valid()) {
        var sText = "";
        sText = ValidationState.Aggregate(sText, (current, roomValidationState) => current + roomValidationState.Message + Environment.NewLine);
        return sText;
      }

      var sDesc = $"{PrimaryDescription}";

      return sDesc;
    }

    public override bool HasTooltip() {
      return true;
    }

    public override bool Intersects(Rect rect) {
      return InnerBounds.IntersectsWith(rect);
    }

    public IList<string> ListOfObjects() {
      var tObjects = Objects.Replace("\r", string.Empty).Replace("|", "\\|").Replace("\n", "|");
      var objects = tObjects.Split('|').Where(p => p != string.Empty).ToList();
      return objects;
    }


    public void Load(XmlElementReader element) {
      Name = element.Attribute("name").Text;
      SubTitle = element.Attribute("subtitle").Text;
      ClearDescriptions();
      AddDescription(element.Attribute("description").Text);
      Position = new Vector(element.Attribute("x").ToFloat(), element.Attribute("y").ToFloat());
      Size = new Vector(element.Attribute("w").ToFloat(), element.Attribute("h").ToFloat());
      Region = element.Attribute("region").Text;
      ReferenceRoomId = element.Attribute("referenceRoom").ToInt();
      IsDark = element.Attribute("isDark").ToBool();
      IsStartRoom = element.Attribute("isStartRoom").ToBool();
      IsEndRoom = element.Attribute("isEndRoom").ToBool();
      ZOrder = element.Attribute("ZOrder").ToInt();
      if (IsStartRoom)
        if (Settings.StartRoomLoaded)
          MessageBox.Show($"{Name} is a duplicate start room. You may need to erase \"isStartRoom=YES\" from the XML.", "Duplicate start room warning");
        else
          Settings.StartRoomLoaded = true;
      if (IsEndRoom)
        if (Settings.EndRoomLoaded)
          MessageBox.Show($"{Name} is a duplicate end room. You may need to erase \"isEndRoom=YES\" from the XML.", "Duplicate end room warning");
        else
          Settings.EndRoomLoaded = true;
      //Note: long term, we probably want an app default for this, but for now, let's force a room shape. #93 should fix this code along with #149.
      //We also should not have two of these at once.
      if (element.Attribute("roundedCorners").ToBool())
        Shape = RoomShape.RoundedCorners;
      else if (element.Attribute("octagonal").ToBool())
        Shape = RoomShape.Octagonal;
      else if (element.Attribute("ellipse").ToBool())
        Shape = RoomShape.Ellipse;
      else
        Shape = RoomShape.SquareCorners;

      HandDrawnEdges = element.Attribute("handDrawn").ToBool();
      AllCornersEqual = element.Attribute("allcornersequal").ToBool();

      Corners = new CornerRadii();
      Corners.TopLeft = element.Attribute("cornerTopLeft").ToFloat();
      Corners.TopRight = element.Attribute("cornerTopRight").ToFloat();
      Corners.BottomLeft = element.Attribute("cornerBottomLeft").ToFloat();
      Corners.BottomRight = element.Attribute("cornerBottomRight").ToFloat();


      if (element.Attribute("borderstyle").Text != "")
        BorderStyle = (BorderDashStyle) Enum.Parse(typeof(BorderDashStyle), element.Attribute("borderstyle").Text);

      if (Project.Version.CompareTo(new Version(1, 5, 8, 3)) < 0) {
        if (element.Attribute("roomFill").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") RoomFillColor = ColorTranslator.FromHtml(element.Attribute("roomFill").Text);
        if (element.Attribute("secondFill").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") SecondFillColor = ColorTranslator.FromHtml(element.Attribute("secondFill").Text);
        if (element.Attribute("secondFillLocation").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") SecondFillLocation = element.Attribute("secondFillLocation").Text;
        if (element.Attribute("roomBorder").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") RoomBorderColor = ColorTranslator.FromHtml(element.Attribute("roomBorder").Text);
        if (element.Attribute("roomLargeText").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") RoomNameColor = ColorTranslator.FromHtml(element.Attribute("roomLargeText").Text);
        if (element.Attribute("roomSmallText").Text != "" && element.Attribute("roomFill").Text != "#FFFFFF") RoomObjectTextColor = ColorTranslator.FromHtml(element.Attribute("roomSmallText").Text);
      } else {
        if (element.Attribute("roomFill").Text != "") RoomFillColor = ColorTranslator.FromHtml(element.Attribute("roomFill").Text);
        if (element.Attribute("secondFill").Text != "") SecondFillColor = ColorTranslator.FromHtml(element.Attribute("secondFill").Text);
        if (element.Attribute("secondFillLocation").Text != "") SecondFillLocation = element.Attribute("secondFillLocation").Text;
        if (element.Attribute("roomBorder").Text != "") RoomBorderColor = ColorTranslator.FromHtml(element.Attribute("roomBorder").Text);
        if (element.Attribute("roomLargeText").Text != "") RoomNameColor = ColorTranslator.FromHtml(element.Attribute("roomLargeText").Text);
        if (element.Attribute("roomSmallText").Text != "") RoomObjectTextColor = ColorTranslator.FromHtml(element.Attribute("roomSmallText").Text);
        if (element.Attribute("roomSubtitleColor").Text != "") RoomSubtitleColor = ColorTranslator.FromHtml(element.Attribute("roomSubtitleColor").Text);
      }

      Objects = element["objects"].Text.Replace("|", "\r\n").Replace("\\\r\n", "|");
      ObjectsPosition = element["objects"].Attribute("at").ToCompassPoint(ObjectsPosition);
      ObjectsCustomPosition = element["objects"].Attribute("custom").ToBool();
      ObjectsCustomPositionRight = element["objects"].Attribute("customRight").ToInt();
      ObjectsCustomPositionDown = element["objects"].Attribute("customDown").ToInt();
    }

    public bool MatchDescription(string description) {
      if (string.IsNullOrEmpty(description))
        return mDescriptions.Count == 0;

      return mDescriptions.Any(existing => existing == description);

      // no match
    }

    public void MarkNameInvalid() {
      // it might be a better idea to make that change via public method
      System.Reflection.FieldInfo fi = mName.GetType().GetField("m_invalidLayout", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
      fi.SetValue(mName, true);
    }

    public Port PortAt(CompassPoint compassPoint) {
      return PortList.Cast<CompassPort>().FirstOrDefault(port => port.CompassPoint == compassPoint);
    }

    public override void PreDraw(DrawingContext context) {
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

    public Vector quarterPoint(Vector frompoint, Vector topoint) {
      var retVector = new Vector();
      retVector.X = (frompoint.X * 3 + topoint.X) / 4;
      retVector.Y = (frompoint.Y * 3 + topoint.Y) / 4;
      return retVector;
    }

    public void Save(XmlScribe scribe) {
      scribe.Attribute("name", Name);
      scribe.Attribute("subtitle", SubTitle);
      scribe.Attribute("x", Position.X);
      scribe.Attribute("y", Position.Y);
      scribe.Attribute("w", Size.X);
      scribe.Attribute("h", Size.Y);
      scribe.Attribute("region", string.IsNullOrEmpty(Region) ? Misc.Region.DefaultRegion : Region);
      if (ReferenceRoom != null)
        scribe.Attribute("referenceRoom", ReferenceRoom.ID);

      scribe.Attribute("handDrawn", HandDrawnEdges);
      scribe.Attribute("allcornersequal", AllCornersEqual);
      scribe.Attribute("ellipse", Ellipse);
      scribe.Attribute("roundedCorners", RoundedCorners);
      scribe.Attribute("octagonal", Octagonal);
      scribe.Attribute("cornerTopLeft", (float) Corners.TopLeft);
      scribe.Attribute("cornerTopRight", (float) Corners.TopRight);
      scribe.Attribute("cornerBottomLeft", (float) Corners.BottomLeft);
      scribe.Attribute("cornerBottomRight", (float) Corners.BottomRight);

      scribe.Attribute("borderstyle", BorderStyle.ToString());
      if (IsDark)
        scribe.Attribute("isDark", IsDark);

      if (IsStartRoom)
        scribe.Attribute("isStartRoom", IsStartRoom);
      if (IsEndRoom)
        scribe.Attribute("isEndRoom", IsEndRoom);

      scribe.Attribute("description", PrimaryDescription);

      var colorValue = Colors.SaveColor(RoomFillColor);
      scribe.Attribute("roomFill", colorValue);

      colorValue = Colors.SaveColor(SecondFillColor);
      scribe.Attribute("secondFill", colorValue);
      scribe.Attribute("secondFillLocation", SecondFillLocation);

      colorValue = Colors.SaveColor(RoomBorderColor);
      scribe.Attribute("roomBorder", colorValue);

      colorValue = Colors.SaveColor(RoomNameColor);
      scribe.Attribute("roomLargeText", colorValue);

      colorValue = Colors.SaveColor(RoomSubtitleColor);
      scribe.Attribute("roomSubtitleColor", colorValue);

      colorValue = Colors.SaveColor(RoomObjectTextColor);
      scribe.Attribute("roomSmallText", colorValue);

      scribe.Attribute("ZOrder", ZOrder);

      // Up to this point was added to turn colors to Hex code for xmpl saving/loading

      if (!string.IsNullOrEmpty(Objects) || ObjectsPosition != DEFAULT_OBJECTS_POSITION) {
        scribe.StartElement("objects");

        if (ObjectsPosition != DEFAULT_OBJECTS_POSITION)
          scribe.Attribute("at", ObjectsPosition);

        if (ObjectsCustomPosition) {
          scribe.Attribute("custom", ObjectsCustomPosition);
          scribe.Attribute("customRight", ObjectsCustomPositionRight);
          scribe.Attribute("customDown", ObjectsCustomPositionDown);
        }

        if (!string.IsNullOrEmpty(Objects))
          scribe.Value(Objects.Replace("\r", string.Empty).Replace("|", "\\|").Replace("\n", "|"));
        scribe.EndElement();
      }
    }

    public void ShowDialog(PropertiesStartType startPoint) {
      showRoomDialog(startPoint);
    }


    public override void ShowDialog() {
      showRoomDialog();
    }

    public override string ToString() {
      var sText = $"Room: {Name}";
      return sText;
    }


    public override Rect UnionBoundsWith(Rect rect, bool includeMargins) {
      var bounds = InnerBounds;
      if (includeMargins)
        bounds.Inflate(Settings.LineWidth + Settings.ConnectionStalkLength);
      return rect.Union(bounds);
    }

    private void addPortsToRoom() {
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

    private void createRoomPath(XGraphicsPath path, LineSegment top, LineSegment left) {
      path.AddArc(top.Start.X + top.Length - Corners.TopRight * 2, top.Start.Y, Corners.TopRight * 2, Corners.TopRight * 2, 270, 90);
      path.AddArc(top.Start.X + top.Length - Corners.BottomRight * 2, top.Start.Y + left.Length - Corners.BottomRight * 2, Corners.BottomRight * 2, Corners.BottomRight * 2, 0, 90);
      path.AddArc(top.Start.X, top.Start.Y + left.Length - Corners.BottomLeft * 2, Corners.BottomLeft * 2, Corners.BottomLeft * 2, 90, 90);
      path.AddArc(top.Start.X, top.Start.Y, Corners.TopLeft * 2, Corners.TopLeft * 2, 180, 90);
      path.CloseFigure();
    }

    private bool isDefaultRegion() {
      return Region == Misc.Region.DefaultRegion || string.IsNullOrEmpty(Region);
    }

    private void setRoomShape(RoomShape pShape) {
      switch (pShape) {
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
          if (Corners.TopRight == 0.0 && Corners.TopLeft == 0.0 && Corners.BottomRight == 0.0 && Corners.BottomLeft == 0.0)
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

    private void showRoomDialog(PropertiesStartType start = PropertiesStartType.RoomName) {
      using (var dialog = new RoomPropertiesDialog(start, ID)) {
        dialog.RoomName = Name;
        dialog.Description = PrimaryDescription;
        dialog.RoomSubTitle = SubTitle;
        dialog.IsDark = IsDark;
        dialog.IsStartRoom = IsStartRoom;
        dialog.IsEndRoom = IsEndRoom;
        dialog.HandDrawnEdges = HandDrawnEdges;
        dialog.Objects = Objects;
        dialog.ObjectsPosition = ObjectsPosition;
        dialog.ObjectsCustomPosition = ObjectsCustomPosition;
        dialog.ObjectsCustomPositionDown = ObjectsCustomPositionDown;
        dialog.ObjectsCustomPositionRight = ObjectsCustomPositionRight;
        dialog.BorderStyle = BorderStyle;

        dialog.RoomFillColor = RoomFillColor;
        dialog.SecondFillColor = SecondFillColor;
        dialog.SecondFillLocation = SecondFillLocation;
        dialog.RoomBorderColor = RoomBorderColor;
        dialog.RoomNameColor = RoomNameColor;
        dialog.ObjectTextColor = RoomObjectTextColor;
        dialog.RoomSubtitleColor = RoomSubtitleColor;
        dialog.RoomRegion = Region;
        dialog.ReferenceRoom = ReferenceRoom;
        dialog.Corners = Corners;
        dialog.RoundedCorners = RoundedCorners;
        //dialog.Octagonal = Octagonal;
        dialog.Ellipse = Ellipse;
        dialog.StraightEdges = StraightEdges;
        dialog.AllCornersEqual = AllCornersEqual;
        dialog.Shape = Shape;

        if (dialog.ShowDialog(Project.Canvas) == DialogResult.OK) {
          Name = dialog.RoomName;
          SubTitle = dialog.RoomSubTitle;
          if (PrimaryDescription != dialog.Description) {
            ClearDescriptions();
            AddDescription(dialog.Description);
          }

          IsDark = dialog.IsDark;
          IsStartRoom = dialog.IsStartRoom;
          IsEndRoom = dialog.IsEndRoom;
          HandDrawnEdges = dialog.HandDrawnEdges;
          Objects = dialog.Objects;
          BorderStyle = dialog.BorderStyle;
          ObjectsPosition = dialog.ObjectsPosition;
          ObjectsCustomPosition = dialog.ObjectsCustomPosition;
          ObjectsCustomPositionDown = dialog.ObjectsCustomPositionDown;
          ObjectsCustomPositionRight = dialog.ObjectsCustomPositionRight;
          // Added for Room specific colors
          RoomFillColor = dialog.RoomFillColor;
          RoomSubtitleColor = dialog.RoomSubtitleColor;
          SecondFillColor = dialog.SecondFillColor;
          SecondFillLocation = dialog.SecondFillLocation;
          RoomBorderColor = dialog.RoomBorderColor;
          RoomNameColor = dialog.RoomNameColor;
          RoomObjectTextColor = dialog.ObjectTextColor;


          Region = dialog.RoomRegion;

          ReferenceRoomId = dialog.ReferenceRoom?.ID ?? -1;
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

    private bool valid() {
      return ValidationState == null || ValidationState.Count == 0;
    }

    internal class CompassPort : Port {
      public CompassPort(CompassPoint compassPoint, Room room) : base(room) {
        CompassPoint = compassPoint;
        Room = room;
      }

      public CompassPoint CompassPoint { get; set; }

      public override string ID {
        get {
          string name;
          return CompassPointHelper.ToName(CompassPoint, out name) ? name : string.Empty;
        }
      }

      public Room Room { get; }
    }
  }

  public class CornerRadii {
    private double bottomLeft = 15.0;
    private double bottomRight = 15.0;
    private double topLeft = 15.0;
    private double topRight = 15.0;

    public double BottomLeft {
      get => bottomLeft;
      set {
        if (value < 1) bottomLeft = 1;
        else if (value > 30) bottomLeft = 30;
        else bottomLeft = value;
      }
    }

    public double BottomRight {
      get => bottomRight;
      set {
        if (value < 1) bottomRight = 1;
        else if (value > 30) bottomRight = 30;
        else bottomRight = value;
      }
    }

    public double TopLeft {
      get => topLeft;
      set {
        if (value < 1) topLeft = 1;
        else if (value > 30) topLeft = 30;
        else topLeft = value;
      }
    }

    public double TopRight {
      get => topRight;
      set {
        if (value < 1) topRight = 1;
        else if (value > 30) topRight = 30;
        else topRight = value;
      }
    }
  }

  public enum RoomShape {
    SquareCorners,
    RoundedCorners,
    Ellipse,
    Octagonal,
    NotARoom
  }

  public enum PropertiesStartType {
    RoomName,
    Region
  }
}