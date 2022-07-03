using System.Drawing;
using System.Windows.Forms;
using Trizbort.Domain.Elements;
using Trizbort.Setup;
using Trizbort.UI.Controls;

namespace Trizbort.Domain.Misc; 

/// <summary>
///   A visual handle by which an element may be resized.
/// </summary>
internal sealed class ResizeHandle {
  private readonly CompassPoint mCompassPoint;
  private readonly ISizeable mOwner;

  public ResizeHandle(CompassPoint compassPoint, ISizeable owner) {
    mCompassPoint = compassPoint;
    mOwner = owner;
  }

  public Cursor Cursor {
    get {
      switch (mCompassPoint) {
        case CompassPoint.NorthWest:
        case CompassPoint.SouthEast:
          return Cursors.SizeNWSE;
        case CompassPoint.NorthEast:
        case CompassPoint.SouthWest:
          return Cursors.SizeNESW;
        case CompassPoint.North:
        case CompassPoint.South:
          return Cursors.SizeNS;
        case CompassPoint.East:
        case CompassPoint.West:
          return Cursors.SizeWE;
        default:
          return null;
      }
    }
  }

  public Vector OwnerPosition {
    get {
      var pos = mOwner.InnerBounds.GetCorner(mCompassPoint);
      return pos;
    }
    set {
      setX(value.X);
      setY(value.Y);
    }
  }

  public Vector Position {
    get {
      var tBounds = mOwner.InnerBounds;

      var pos = tBounds.GetCorner(mCompassPoint);
      if (mOwner is Room room) pos = tBounds.GetCorner(mCompassPoint, room.Shape, room.Corners);
      pos.X -= size.X / 2;
      pos.Y -= size.Y / 2;
      return pos;
    }
  }

  private Rect bounds => new Rect(Position, size);

  private static Vector size => new Vector(Settings.HandleSize);

  public void Draw(Canvas canvas, Graphics graphics, Palette palette, DrawingContext context) {
    Drawing.DrawHandle(canvas, graphics, palette, bounds, context, false, false);
  }

  public bool HitTest(Vector pos) {
    return bounds.Contains(pos);
  }

  private void setX(float value) {
    switch (mCompassPoint) {
      case CompassPoint.North:
      case CompassPoint.South:
        break;
      case CompassPoint.NorthWest:
      case CompassPoint.WestNorthWest:
      case CompassPoint.West:
      case CompassPoint.WestSouthWest:
      case CompassPoint.SouthWest:
        if (mOwner.Width - (value - mOwner.X) >= 1) {
          var old = mOwner.X;
          mOwner.Position = new Vector(value, mOwner.Position.Y);
          mOwner.Size = new Vector(mOwner.Size.X - (mOwner.X - old), mOwner.Size.Y);
        }
        break;
      case CompassPoint.NorthEast:
      case CompassPoint.EastNorthEast:
      case CompassPoint.East:
      case CompassPoint.EastSouthEast:
      case CompassPoint.SouthEast:
        if (value - mOwner.X >= 1) mOwner.Size = new Vector(value - mOwner.X, mOwner.Size.Y);
        break;
    }
  }

  private void setY(float value) {
    switch (mCompassPoint) {
      case CompassPoint.East:
      case CompassPoint.West:
        break;
      case CompassPoint.NorthWest:
      case CompassPoint.NorthNorthWest:
      case CompassPoint.North:
      case CompassPoint.NorthNorthEast:
      case CompassPoint.NorthEast:
        if (mOwner.Height - (value - mOwner.Y) >= 1) {
          var old = mOwner.Y;
          mOwner.Position = new Vector(mOwner.Position.X, value);
          mOwner.Size = new Vector(mOwner.Size.X, mOwner.Size.Y - (mOwner.Y - old));
        }

        break;
      case CompassPoint.SouthWest:
      case CompassPoint.SouthSouthWest:
      case CompassPoint.South:
      case CompassPoint.SouthSouthEast:
      case CompassPoint.SouthEast:
        if (value - mOwner.Y >= 1) mOwner.Size = new Vector(mOwner.Size.X, value - mOwner.Y);
        break;
    }
  }
}