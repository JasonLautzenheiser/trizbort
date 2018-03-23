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
using PdfSharp.Drawing;

namespace Trizbort.Domain.Misc {
  public class LineSegment {
    public Vector End;

    public ConnectionIconBlock IconBlock1;
    public ConnectionIconBlock IconBlock2;

    public Vector Start;

    public LineSegment(Vector start, Vector end) {
      Start = start;
      End = end;
      IconBlock1 = new ConnectionIconBlock(this, 0);
      IconBlock2 = new ConnectionIconBlock(this, 1);
    }

    public Vector Delta => End - Start;

    public float Length => Delta.Length;
    public Vector Mid => (Start + End) / 2;

    public void DrawIcons(XGraphics graphics) {
      IconBlock1.DrawBlock(graphics);
      IconBlock2.DrawBlock(graphics);
    }

    //   Shorten this line segment, moving the start point towards the end point.
    public bool Forshorten(float amount) {
      Reverse();
      var result = Shorten(amount);
      Reverse();
      return result;
    }

    public bool Intersect(LineSegment other, bool ignoreEndPointIntersects, out List<LineSegmentIntersect> intersects) {
      return Intersect(this, other, ignoreEndPointIntersects, out intersects);
    }

    public bool Intersect(LineSegment a, LineSegment b, bool ignoreEndPointIntersects, out List<LineSegmentIntersect> intersects) {
      var ua = (b.End.X - b.Start.X) * (a.Start.Y - b.Start.Y) - (b.End.Y - b.Start.Y) * (a.Start.X - b.Start.X);
      var ub = (a.End.X - a.Start.X) * (a.Start.Y - b.Start.Y) - (a.End.Y - a.Start.Y) * (a.Start.X - b.Start.X);
      var denominator = (b.End.Y - b.Start.Y) * (a.End.X - a.Start.X) - (b.End.X - b.Start.X) * (a.End.Y - a.Start.Y);
      intersects = null;
      const float SMALL = 0.01f;

      if (Math.Abs(denominator) <= SMALL) {
        if (Math.Abs(ua) <= SMALL && Math.Abs(ub) <= SMALL) {
          // lines are coincident:
          // lacking other algorithms which actually work,
          // roll some expensive distance tests to find intersection points
          if (a.Start.DistanceFromLineSegment(b) <= SMALL) {
            intersects = new List<LineSegmentIntersect>();
            intersects.Add(new LineSegmentIntersect(LineSegmentIntersectType.StartA, a.Start));
          }

          if (a.End.DistanceFromLineSegment(b) <= SMALL) {
            if (intersects == null) intersects = new List<LineSegmentIntersect>();
            intersects.Add(new LineSegmentIntersect(LineSegmentIntersectType.EndA, a.End));
          }

          if (b.Start.DistanceFromLineSegment(a) <= SMALL) {
            if (intersects == null) intersects = new List<LineSegmentIntersect>();
            intersects.Add(new LineSegmentIntersect(LineSegmentIntersectType.MidPointA, b.Start));
          }

          if (b.End.DistanceFromLineSegment(a) <= SMALL) {
            if (intersects == null) intersects = new List<LineSegmentIntersect>();
            intersects.Add(new LineSegmentIntersect(LineSegmentIntersectType.MidPointA, b.End));
          }
        }
      } else {
        ua /= denominator;
        ub /= denominator;

        if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1) {
          var type = LineSegmentIntersectType.MidPointA;
          if (ua <= SMALL)
            type = LineSegmentIntersectType.StartA;
          else if (1 - ua <= SMALL) type = LineSegmentIntersectType.EndA;
          intersects = new List<LineSegmentIntersect>();
          intersects.Add(new LineSegmentIntersect(type, a.Start + new Vector(ua * (a.End.X - a.Start.X), ua * (a.End.Y - a.Start.Y))));
        }
      }

      if (intersects != null && ignoreEndPointIntersects)
        for (var index = 0; index < intersects.Count; ++index) {
          var intersect = intersects[index];
          if (intersect.Position.Distance(a.Start) <= SMALL ||
              intersect.Position.Distance(b.Start) <= SMALL ||
              intersect.Position.Distance(a.End) <= SMALL ||
              intersect.Position.Distance(b.End) <= SMALL) {
            intersects.RemoveAt(index);
            --index;
          }
        }

      return intersects != null;
    }

    public bool IntersectsWith(Rect rect) {
      if (rect.Contains(Start) || rect.Contains(End)) return true;
      var a = new LineSegment(new Vector(rect.Left, rect.Top), new Vector(rect.Right, rect.Top));
      var b = new LineSegment(new Vector(rect.Right, rect.Top), new Vector(rect.Right, rect.Bottom));
      var c = new LineSegment(new Vector(rect.Right, rect.Bottom), new Vector(rect.Left, rect.Bottom));
      var d = new LineSegment(new Vector(rect.Left, rect.Bottom), new Vector(rect.Left, rect.Top));
      List<LineSegmentIntersect> intersects;
      if (Intersect(a, false, out intersects) || Intersect(b, false, out intersects) || Intersect(c, false, out intersects) || Intersect(d, false, out intersects)) return true;
      return false;
    }

    public void Reverse() {
      var temp = End;
      End = Start;
      Start = temp;
    }

    //   Shorten this line segment, moving the end point towards the start point.
    public bool Shorten(float amount) {
      var delta = Delta;
      var length = delta.Length;
      delta.Normalize();

      if (length > amount) {
        length -= amount;
        End = Start + delta * length;
        return true;
      }

      // don't shorten past zero length
      End = Start;
      return false;
    }
  }

  /// <summary>
  ///   The type of a line segment intersection.
  /// </summary>
  public enum LineSegmentIntersectType {
    MidPointA,
    StartA,
    EndA
  }

  /// <summary>
  ///   A line segment intersection.
  /// </summary>
  public struct LineSegmentIntersect {
    public LineSegmentIntersect(LineSegmentIntersectType type, Vector pos) {
      Type = type;
      Position = pos;
    }

    public LineSegmentIntersectType Type;
    public Vector Position;
  }
}