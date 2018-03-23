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
using System.Drawing;
using Trizbort.Domain.Elements;

namespace Trizbort.Domain.Misc {
  public struct Rect {
    public static readonly Rect Empty = new Rect(Vector.Zero, Vector.Zero);
    public float Height;
    public float Width;
    public float X;
    public float Y;

    public Rect(float x, float y, float width, float height) {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    public Rect(Vector pos, Vector size) : this(pos.X, pos.Y, size.X, size.Y) { }

    public Rect(RectangleF r) : this(r.X, r.Y, r.Width, r.Height) { }

    public float Left => X;

    public float Top => Y;

    public float Right => X + Width;

    public float Bottom => Y + Height;

    public Vector Position => new Vector(X, Y);

    public Vector Center => new Vector(X + Width / 2, Y + Height / 2);

    public Vector Size => new Vector(Width, Height);

    public RectangleF ToRectangleF() {
      return new RectangleF(X, Y, Width, Height);
    }

    public Rectangle ToRectangle() {
      return new Rectangle((int) X, (int) Y, (int) Width, (int) Height);
    }

    public bool Contains(Vector pos) {
      return pos.X >= Left && pos.X <= Right && pos.Y >= Top && pos.Y <= Bottom;
    }

    public bool Contains(Rect r) {
      return r.X >= Left && r.Right <= Right && r.Top >= Top && r.Bottom <= Bottom;
    }

    public Vector GetCorner(CompassPoint point, RoomShape myRmShape = RoomShape.SquareCorners, CornerRadii corners = null) {
      var isOctagonal = myRmShape == RoomShape.Octagonal;
      var isRounded = myRmShape == RoomShape.RoundedCorners;
      var isEllipse = myRmShape == RoomShape.Ellipse;
      double angleInRadians;
      if (myRmShape == RoomShape.Ellipse) {
        angleInRadians = CompassPointHelper.CalcRadianForEllipse(point, this);
        return new Vector(Center.X + (float) (Width / 2.0 * Math.Cos(angleInRadians)), Center.Y + (float) (Height / 2.0 * Math.Sin(angleInRadians)));
      }

      if (myRmShape == RoomShape.Ellipse)
        if (point == CompassPoint.NorthEast || point == CompassPoint.NorthWest || point == CompassPoint.SouthWest || point == CompassPoint.SouthEast) {
          angleInRadians = CompassPointHelper.CalcRadianForEllipse(point, this);

          if (point == CompassPoint.NorthEast) {
            var rect = new Rect(X + Width, Y, (float) corners.TopRight, (float) corners.TopRight);
            return new Vector(rect.Center.X - (float) (corners.TopRight / 2.0) - (float) (corners.TopRight / 2.0 * Math.Cos(angleInRadians)), rect.Center.Y + (float) (corners.TopLeft / 4.0) + (float) (corners.TopRight / 2.0 * Math.Sin(angleInRadians)));
          }

          if (point == CompassPoint.NorthWest) {
            var rect = new Rect(X, Y, (float) corners.TopLeft, (float) corners.TopLeft);
            return new Vector(rect.Center.X - (float) (corners.TopLeft / 2.0) - (float) (corners.TopLeft / 2.0 * Math.Cos(angleInRadians)), rect.Center.Y + (float) (corners.TopLeft / 4.0) + (float) (corners.TopLeft / 2.0 * Math.Sin(angleInRadians)));
          }

          if (point == CompassPoint.SouthWest) {
            var rect = new Rect(X, Y + Height, (float) corners.BottomLeft, (float) corners.BottomLeft);
            return new Vector(rect.Center.X - (float) (corners.BottomLeft / 2.0) - (float) (corners.BottomLeft / 2.0 * Math.Cos(angleInRadians)), rect.Center.Y - (float) (corners.BottomLeft / 2.0) - (float) (corners.BottomLeft / 2.0 * Math.Sin(angleInRadians)));
          }

          if (point == CompassPoint.SouthEast) {
            var rect = new Rect(X + Width, Y + Height, (float) corners.BottomRight, (float) corners.BottomRight);
            return new Vector(rect.Center.X - (float) (corners.BottomRight / 2.0) - (float) (corners.BottomRight / 2.0 * Math.Cos(angleInRadians)), rect.Center.Y - (float) (corners.BottomRight / 2.0) - (float) (corners.BottomRight / 2.0 * Math.Sin(angleInRadians)));
          }
        }

      switch (point) {
        case CompassPoint.North:
          return new Vector(X + Width / 2, Y);
        case CompassPoint.NorthNorthEast:
          return new Vector(X + Width * 3 / 4, Y);
        case CompassPoint.NorthEast:
          if (isOctagonal)
            return new Vector(X + Width * 7 / 8, Y + Height * 1 / 8);
          if (isRounded)
            return new Vector((float) (X + Width - 0.25 * corners.TopLeft), (float) (Y + 0.25 * corners.TopLeft));
          return new Vector(X + Width, Y);
        case CompassPoint.EastNorthEast:
          return new Vector(X + Width, Y + Height / 4);
        case CompassPoint.East:
          return new Vector(X + Width, Y + Height / 2);
        case CompassPoint.EastSouthEast:
          return new Vector(X + Width, Y + Height * 3 / 4);
        case CompassPoint.SouthEast:
          if (isOctagonal)
            return new Vector(X + Width * 7 / 8, Y + Height * 7 / 8);
          if (isRounded)
            return new Vector((float) (X + Width - 0.25 * corners.TopLeft), (float) (Y + Height - 0.25 * corners.TopLeft));
          return new Vector(X + Width, Y + Height);
        case CompassPoint.SouthSouthEast:
          return new Vector(X + Width * 3 / 4, Y + Height);
        case CompassPoint.South:
          return new Vector(X + Width / 2, Y + Height);
        case CompassPoint.SouthSouthWest:
          return new Vector(X + Width / 4, Y + Height);
        case CompassPoint.SouthWest:
          if (isOctagonal)
            return new Vector(X + Width * 1 / 8, Y + Height * 7 / 8);
          if (isRounded)
            return new Vector((float) (X + 0.3 * corners.TopLeft), (float) (Y + Height - 0.3 * corners.TopLeft));
          return new Vector(X, Y + Height);
        case CompassPoint.WestSouthWest:
          return new Vector(X, Y + Height * 3 / 4);
        case CompassPoint.West:
          return new Vector(X, Y + Height / 2);
        case CompassPoint.WestNorthWest:
          return new Vector(X, Y + Height / 4);
        case CompassPoint.NorthWest:
          if (isOctagonal)
            return new Vector(X + Width * 1 / 8, Y + Height * 1 / 8);
          if (isRounded)
            return new Vector((float) (X + 0.3 * corners.TopLeft), (float) (Y + 0.3 * corners.TopLeft));
          return new Vector(X, Y);
        case CompassPoint.NorthNorthWest:
          return new Vector(X + Width / 4, Y);
        default:
          throw new InvalidOperationException();
      }
    }

    public void Inflate(float dx, float dy) {
      X -= dx;
      Y -= dy;
      Width += dx * 2;
      Height += dy * 2;
    }

    public void Inflate(float delta) {
      Inflate(delta, delta);
    }

    /// <summary>
    ///   Get the union of this rectangle with another rectangle.
    /// </summary>
    /// <param name="rect">A rectangle.</param>
    /// <returns>The union of the two rectangles.</returns>
    public Rect Union(Rect rect) {
      if (rect == Empty) return this;
      if (this == Empty) return rect;

      var newRect = rect;
      newRect.X = Math.Min(rect.X, X);
      newRect.Y = Math.Min(rect.Y, Y);
      newRect.Width = Math.Max(rect.X + rect.Width, X + Width) - newRect.X;
      newRect.Height = Math.Max(rect.Y + rect.Height, Y + Height) - newRect.Y;
      return newRect;
    }

    public Rect Union(Vector v) {
      if (this == Empty) return new Rect(v, Vector.Zero);

      var newRect = this;
      newRect.X = Math.Min(v.X, X);
      newRect.Y = Math.Min(v.Y, Y);
      newRect.Width = Math.Max(v.X, X + Width) - newRect.X;
      newRect.Height = Math.Max(v.Y, Y + Height) - newRect.Y;
      return newRect;
    }

    public bool IntersectsWith(Rect rect) {
      var a = new RectangleF(X, Y, Width, Height);
      var b = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
      return a.IntersectsWith(b);
    }

    /// <summary>
    ///   Clamp a vector so that it falls within the rectangle.
    /// </summary>
    public Vector Clamp(Vector v) {
      v.X = Math.Max(X, Math.Min(X + Width, v.X));
      v.Y = Math.Max(Y, Math.Min(Y + Height, v.Y));
      return v;
    }

    public static bool operator ==(Rect a, Rect b) {
      return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
    }

    public static bool operator !=(Rect a, Rect b) {
      return a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
    }

    public override bool Equals(object obj) {
      if (!(obj is Rect))
        return false;

      var other = (Rect) obj;
      return this == other;
    }

    public override int GetHashCode() {
      return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
    }

    public override string ToString() {
      return string.Format("({0}, {1}) - ({2}, {3}) ({4} by {5})", Left, Top, Right, Bottom, Width, Height);
    }
  }
}