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
using System.IO;
using System.Windows.Forms;
using PdfSharp.Drawing;
using Trizbort.Properties;

namespace Trizbort
{
  internal static class Drawing
  {
    private static readonly Cursor m_drawLineCursor;
    private static readonly Cursor m_drawLineInvertedCursor;
    private static readonly Cursor m_moveLineCursor;
    private static readonly Cursor m_moveLineInvertedCursor;
    private static XGraphicsPath m_chevronPath;

    static Drawing()
    {
      m_drawLineCursor = LoadCursor(Resources.DrawLineCursor);
      m_drawLineInvertedCursor = LoadCursor(Resources.DrawLineInvertedCursor);
      m_moveLineCursor = LoadCursor(Resources.MoveLineCursor);
      m_moveLineInvertedCursor = LoadCursor(Resources.MoveLineInvertedCursor);
    }

    public static Cursor DrawLineCursor { get { return IsDark(Settings.Color[Colors.Canvas]) ? m_drawLineInvertedCursor : m_drawLineCursor; } }

    public static Cursor MoveLineCursor { get { return IsDark(Settings.Color[Colors.Canvas]) ? m_moveLineInvertedCursor : m_moveLineCursor; } }

    private static Cursor LoadCursor(byte[] bytes)
    {
      using (var stream = new MemoryStream(bytes))
      {
        return new Cursor(stream);
      }
    }

    public static Rectangle ToRectangle(RectangleF rect)
    {
      return new Rectangle((int) rect.X, (int) rect.Y, (int) rect.Width, (int) rect.Height);
    }

    public static void AddLine(XGraphicsPath path, LineSegment segment, Random random, bool straightEdges)
    {
      if (Settings.HandDrawn && !straightEdges)
      {
        var dx = segment.End.X - segment.Start.X;
        var dy = segment.End.Y - segment.Start.Y;
        var distance = (float) Math.Sqrt(dx*dx + dy*dy);
        var points = random.Next(Math.Max(3, (int) (distance/15)), Math.Max(6, (int) (distance/8)));
        var lines = points - 1;
        var last = segment.Start;
        for (var line = 0; line < lines; ++line)
        {
          Vector next;
          if (line == 0)
          {
            next = last;
          }
          else if (line == lines - 1)
          {
            next = segment.End;
          }
          else
          {
            var fraction = line/(float) (lines - 1);
            var x = segment.Start.X + (segment.End.X - segment.Start.X)*fraction;
            var y = segment.Start.Y + (segment.End.Y - segment.Start.Y)*fraction;

            x += random.Next(-1, 2);
            y += random.Next(-1, 2);
            next = new Vector(x, y);
          }

          path.AddLine(last.ToPointF(), next.ToPointF());
          last = next;
        }
      }
      else
      {
        path.AddLine(segment.Start.ToPointF(), segment.End.ToPointF());
      }
    }

    public static void DrawHandle(Canvas canvas, XGraphics graphics, Palette palette, Rect bounds, DrawingContext context, bool alwaysAlpha, bool round)
    {
      if (bounds.Width <= 0 || bounds.Height <= 0)
      {
        return;
      }

      using (var quality = new Smoothing(graphics, XSmoothingMode.Default))
      {
        XBrush brush;
        Pen pen;
        var alpha = 180;

        if (context.Selected)
        {
          if (!alwaysAlpha)
          {
            alpha = 255;
          }
          brush = palette.Gradient(bounds, Color.FromArgb(alpha, Color.LemonChiffon), Color.FromArgb(alpha, Color.DarkOrange));
          pen = palette.Pen(Color.FromArgb(alpha, Color.Chocolate), 0);
        }
        else
        {
          brush = palette.Gradient(bounds, Color.FromArgb(alpha, Color.LightCyan), Color.FromArgb(alpha, Color.SteelBlue));
          pen = palette.Pen(Color.FromArgb(alpha, Color.Navy), 0);
        }

        if (round)
        {
          graphics.DrawEllipse(brush, bounds.ToRectangleF());
//          graphics.DrawRectangle(new XPen(Color.Red), bounds.ToRectangleF() );
          graphics.DrawEllipse(pen, bounds.ToRectangleF());
        }
        else
        {
          graphics.DrawRectangle(brush, bounds.ToRectangleF());
          graphics.DrawRectangle(pen, bounds.ToRectangleF());
        }
      }
    }

    public static Color Mix(Color a, Color b, int propA, int propB)
    {
      return Color.FromArgb(
                            (byte) (((a.R*propA) + (b.R*propB))/(propA + propB)),
                            (byte) (((a.G*propA) + (b.G*propB))/(propA + propB)),
                            (byte) (((a.B*propA) + (b.B*propB))/(propA + propB)));
    }

    public static bool IsDark(Color color)
    {
      return Math.Max(color.R, Math.Max(color.G, color.B)) < 128;
    }

    public static PointF Subtract(PointF a, PointF b)
    {
      return new PointF(a.X - b.X, a.Y - b.Y);
    }

    public static PointF Divide(PointF pos, float scalar)
    {
      return new PointF(pos.X/scalar, pos.Y/scalar);
    }

    public static void DrawChevron(XGraphics graphics, PointF pos, float angle, float size, Brush fillBrush)
    {
      if (m_chevronPath == null)
      {
        var apex = new PointF(0.5f, 0);
        var leftCorner = new PointF(-0.5f, 0.5f);
        var rightCorner = new PointF(-0.5f, -0.5f);
        m_chevronPath = new XGraphicsPath();
        m_chevronPath.AddLine(apex, rightCorner);
        m_chevronPath.AddLine(rightCorner, leftCorner);
        m_chevronPath.AddLine(leftCorner, apex);
      }
      var state = graphics.Save();
      graphics.TranslateTransform(pos.X, pos.Y);
      graphics.RotateTransform(angle);
      graphics.ScaleTransform(size, size);
      graphics.DrawPath(fillBrush, m_chevronPath);
      graphics.Restore(state);
    }

    public static bool SetAlignmentFromCardinalOrOrdinalDirection(XStringFormat format, CompassPoint compassPoint)
    {
      switch (compassPoint)
      {
        case CompassPoint.North:
        case CompassPoint.NorthEast:
          format.LineAlignment = XLineAlignment.Far;
          format.Alignment = XStringAlignment.Near;
          break;
        case CompassPoint.East:
        case CompassPoint.SouthEast:
        case CompassPoint.South:
          format.LineAlignment = XLineAlignment.Near;
          format.Alignment = XStringAlignment.Near;
          break;
        case CompassPoint.West:
        case CompassPoint.SouthWest:
          format.LineAlignment = XLineAlignment.Near;
          format.Alignment = XStringAlignment.Far;
          break;
        case CompassPoint.NorthWest:
          format.LineAlignment = XLineAlignment.Far;
          format.Alignment = XStringAlignment.Far;
          break;
        default:
          return false;
      }
      return true;
    }

    public static string FontName(Font font)
    {
      if (!string.IsNullOrEmpty(font.OriginalFontName))
      {
        return font.OriginalFontName;
      }
      return font.Name;
    }
  }
}