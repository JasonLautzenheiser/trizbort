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
using System.Drawing;

namespace Trizbort
{
  public struct Rect
  {
    public static readonly Rect Empty = new Rect(Vector.Zero, Vector.Zero);
    public float Height;
    public float Width;
    public float X;
    public float Y;

    public Rect(float x, float y, float width, float height)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }

    public Rect(Vector pos, Vector size) : this(pos.X, pos.Y, size.X, size.Y)
    {
    }

    public Rect(RectangleF r) : this(r.X, r.Y, r.Width, r.Height)
    {
    }

    public float Left => X;

    public float Top => Y;

    public float Right => X + Width;

    public float Bottom => Y + Height;

    public Vector Position => new Vector(X, Y);

    public Vector Center => new Vector(X + Width/2, Y + Height/2);

    public Vector Size => new Vector(Width, Height);

    public RectangleF ToRectangleF()
    {
      return new RectangleF(X, Y, Width, Height);
    }

    public Rectangle ToRectangle()
    {
      return new Rectangle((int) X, (int) Y, (int) Width, (int) Height);
    }

    public bool Contains(Vector pos)
    {
      return pos.X >= Left && pos.X <= Right && pos.Y >= Top && pos.Y <= Bottom;
    }

    public bool Contains(Rect r)
    {
      return r.X >= Left && r.Right <= Right && r.Top >= Top && r.Bottom <= Bottom;
    }

    public Vector GetCorner(CompassPoint point, bool ellipse = false, CornerRadii corners = null)
    {
      if (ellipse)
      {
        return new Vector(Center.X + (float)((Width/2.0) * Math.Cos(CompassPointHelper.GetAngleInRadians(point))), Center.Y + (float)((Height / 2.0) * Math.Sin(CompassPointHelper.GetAngleInRadians(point))));
      }

      if (corners != null)
      {
        if (point != CompassPoint.East && point != CompassPoint.West && point != CompassPoint.North && point != CompassPoint.South)
        {
          if (corners.TopRight > 10.0 && (point == CompassPoint.EastNorthEast || point == CompassPoint.NorthEast || point == CompassPoint.NorthNorthEast))
            return new Vector(Center.X + (float) ((Width/2.0)*Math.Cos(CompassPointHelper.GetAngleInRadians(point))), Center.Y + (float) ((Height/2.0)*Math.Sin(CompassPointHelper.GetAngleInRadians(point))));

          if (corners.TopLeft > 10.0 && (point == CompassPoint.WestNorthWest || point == CompassPoint.NorthWest || point == CompassPoint.NorthNorthWest))
            return new Vector(Center.X + (float)((Width / 2.0) * Math.Cos(CompassPointHelper.GetAngleInRadians(point))), Center.Y + (float)((Height / 2.0) * Math.Sin(CompassPointHelper.GetAngleInRadians(point))));

          if (corners.BottomLeft > 10.0 && (point == CompassPoint.WestSouthWest || point == CompassPoint.SouthWest || point == CompassPoint.SouthSouthWest))
            return new Vector(Center.X + (float)((Width / 2.0) * Math.Cos(CompassPointHelper.GetAngleInRadians(point))), Center.Y + (float)((Height / 2.0) * Math.Sin(CompassPointHelper.GetAngleInRadians(point))));

          if (corners.BottomRight > 10.0 && (point == CompassPoint.SouthSouthEast || point == CompassPoint.SouthEast || point == CompassPoint.EastSouthEast))
            return new Vector(Center.X + (float)((Width / 2.0) * Math.Cos(CompassPointHelper.GetAngleInRadians(point))), Center.Y + (float)((Height / 2.0) * Math.Sin(CompassPointHelper.GetAngleInRadians(point))));

        }
      }

      switch (point)
      {
        case CompassPoint.North:
          return new Vector(X + Width/2, Y);
        case CompassPoint.NorthNorthEast:
          return new Vector(X + Width*3/4, Y);
        case CompassPoint.NorthEast:
          return new Vector(X + Width, Y);
        case CompassPoint.EastNorthEast:
          return new Vector(X + Width, Y + Height/4);
        case CompassPoint.East:
          return new Vector(X + Width, Y + Height/2);
        case CompassPoint.EastSouthEast:
          return new Vector(X + Width, Y + Height*3/4);
        case CompassPoint.SouthEast:
          return new Vector(X + Width, Y + Height);
        case CompassPoint.SouthSouthEast:
          return new Vector(X + Width*3/4, Y + Height);
        case CompassPoint.South:
          return new Vector(X + Width/2, Y + Height);
        case CompassPoint.SouthSouthWest:
          return new Vector(X + Width/4, Y + Height);
        case CompassPoint.SouthWest:
          return new Vector(X, Y + Height);
        case CompassPoint.WestSouthWest:
          return new Vector(X, Y + Height*3/4);
        case CompassPoint.West:
          return new Vector(X, Y + Height/2);
        case CompassPoint.WestNorthWest:
          return new Vector(X, Y + Height/4);
        case CompassPoint.NorthWest:
          return new Vector(X, Y);
        case CompassPoint.NorthNorthWest:
          return new Vector(X + Width/4, Y);
        default:
          throw new InvalidOperationException();
      }
    }

    public void Inflate(float dx, float dy)
    {
      X -= dx;
      Y -= dy;
      Width += dx*2;
      Height += dy*2;
    }

    public void Inflate(float delta)
    {
      Inflate(delta, delta);
    }

    /// <summary>
    ///   Get the union of this rectangle with another rectangle.
    /// </summary>
    /// <param name="rect">A rectangle.</param>
    /// <returns>The union of the two rectangles.</returns>
    public Rect Union(Rect rect)
    {
      if (rect == Empty)
      {
        // the union of this rectangle and the empty rectangle is this rectangle
        return this;
      }
      if (this == Empty)
      {
        // the union of this empty rectangle and another rectangle is the other rectangle
        return rect;
      }

      var newRect = rect;
      newRect.X = Math.Min(rect.X, X);
      newRect.Y = Math.Min(rect.Y, Y);
      newRect.Width = Math.Max(rect.X + rect.Width, X + Width) - newRect.X;
      newRect.Height = Math.Max(rect.Y + rect.Height, Y + Height) - newRect.Y;
      return newRect;
    }

    public Rect Union(Vector v)
    {
      if (this == Empty)
      {
        // the union of this empty rectangle and a vector is that vector
        return new Rect(v, Vector.Zero);
      }

      var newRect = this;
      newRect.X = Math.Min(v.X, X);
      newRect.Y = Math.Min(v.Y, Y);
      newRect.Width = Math.Max(v.X, X + Width) - newRect.X;
      newRect.Height = Math.Max(v.Y, Y + Height) - newRect.Y;
      return newRect;
    }

    public bool IntersectsWith(Rect rect)
    {
      var a = new RectangleF(X, Y, Width, Height);
      var b = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
      return a.IntersectsWith(b);
    }

    /// <summary>
    ///   Clamp a vector so that it falls within the rectangle.
    /// </summary>
    public Vector Clamp(Vector v)
    {
      v.X = Math.Max(X, Math.Min(X + Width, v.X));
      v.Y = Math.Max(Y, Math.Min(Y + Height, v.Y));
      return v;
    }

    public static bool operator ==(Rect a, Rect b)
    {
      return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
    }

    public static bool operator !=(Rect a, Rect b)
    {
      return a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
    }

    public override bool Equals(object obj)
    {
      if (!(obj is Rect))
        return false;

      var other = (Rect) obj;
      return this == other;
    }

    public override int GetHashCode()
    {
      return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format("({0}, {1}) - ({2}, {3}) ({4} by {5})", Left, Top, Right, Bottom, Width, Height);
    }
  }
}