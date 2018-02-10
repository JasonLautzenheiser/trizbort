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

namespace Trizbort.Domain.Misc
{
    public struct Vector
    {
        public Vector(float scalar)
        {
            X = scalar;
            Y = scalar;
        }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector(PointF point)
        {
            X = point.X;
            Y = point.Y;
        }

        public Vector(SizeF size)
        {
            X = size.Width;
            Y = size.Height;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static Vector operator -(Vector v, float scalar)
        {
            return new Vector(v.X - scalar, v.Y - scalar);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator +(Vector v, float scalar)
        {
            return new Vector(v.X + scalar, v.Y + scalar);
        }

        public static Vector operator *(Vector v, float scalar)
        {
            return new Vector(v.X * scalar, v.Y * scalar);
        }

        public static Vector operator *(float scalar, Vector v)
        {
            return new Vector(v.X * scalar, v.Y * scalar);
        }

        public static Vector operator /(Vector v, float scalar)
        {
            return new Vector(v.X / scalar, v.Y / scalar);
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector))
                return false;

            Vector other = (Vector)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }

        public SizeF ToSizeF()
        {
            return new SizeF(X, Y);
        }

        public Size ToSize()
        {
            return new Size((int)X, (int)Y);
        }

        public float Dot(Vector other)
        {
            return Dot(this, other);
        }

        public static float Dot(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public float LengthSquared
        {
            get { return X * X + Y * Y; }
        }

        public float Length
        {
            get
            {
                return (float)Math.Sqrt(LengthSquared);
            }
        }

        public Vector Abs()
        {
            return new Vector(Math.Abs(X), Math.Abs(Y));
        }

        public static float Distance(Vector a, Vector b)
        {
            return Math.Abs((b - a).Length);
        }

        public float Distance(Vector other)
        {
            return Distance(this, other);
        }

        public static Vector Normalize(Vector v)
        {
            Vector n = v;
            n.Normalize();
            return n;
        }

        public void Normalize()
        {
            var length = Length;
            if (length == 0)
            {
                // avoid division by zero (NaN)
                X = 0;
                Y = 0;
            }
            else
            {
                X /= length;
                Y /= length;
            }
        }

        public void Negate()
        {
            X = -X;
            Y = -Y;
        }

        public static float DistanceFromLineSegment(LineSegment line, Vector pos)
        {
            var delta = line.End - line.Start;
            var direction = Vector.Normalize(delta);

            float distanceAlongSegment = Vector.Dot(pos, direction) - Vector.Dot(line.Start, direction);
            Vector nearest;
            if (distanceAlongSegment < 0)
            {
                nearest = line.Start;
            }
            else if (distanceAlongSegment > delta.Length)
            {
                nearest = line.End;
            }
            else
            {
                nearest = line.Start + distanceAlongSegment * direction;
            }

            return Vector.Distance(nearest, pos);
        }

        public static float DistanceFromRect(Rect rect, Vector pos)
        {
            if (rect.Contains(pos))
            {
                return 0;
            }
            var nw = rect.GetCorner(CompassPoint.NorthWest);
            var ne = rect.GetCorner(CompassPoint.NorthEast);
            var sw = rect.GetCorner(CompassPoint.SouthWest);
            var se = rect.GetCorner(CompassPoint.SouthEast);
            
            var distanceFromTop = DistanceFromLineSegment(new LineSegment(nw, ne), pos);
            var distanceFromRight = DistanceFromLineSegment(new LineSegment(ne, se), pos);
            var distanceFromBottom = DistanceFromLineSegment(new LineSegment(se, sw), pos);
            var distanceFromLeft = DistanceFromLineSegment(new LineSegment(sw, nw), pos);

            return Math.Min(distanceFromTop, Math.Min(distanceFromLeft, Math.Min(distanceFromBottom, distanceFromRight)));
        }

        public float DistanceFromRect(Rect rect)
        {
            return DistanceFromRect(rect, this);
        }

        public float DistanceFromLineSegment(LineSegment line)
        {
            return DistanceFromLineSegment(line, this);
        }

        public static readonly Vector Zero = new Vector();

        public float X;
        public float Y;
    }
}
