using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using Trizbort.Domain.Elements;
using Trizbort.Properties;
using Trizbort.UI.Controls;
using Trizbort.Util;
using Settings = Trizbort.Setup.Settings;

namespace Trizbort.Domain.Misc; 

internal static class Drawing {
  private static readonly Cursor m_drawLineCursor;
  private static readonly Cursor m_drawLineInvertedCursor;
  private static readonly Cursor m_moveLineCursor;
  private static readonly Cursor m_moveLineInvertedCursor;
  private static GraphicsPath m_chevronPath;

  static Drawing() {
    m_drawLineCursor = LoadCursor(Resources.DrawLineCursor);
    m_drawLineInvertedCursor = LoadCursor(Resources.DrawLineInvertedCursor);
    m_moveLineCursor = LoadCursor(Resources.MoveLineCursor);
    m_moveLineInvertedCursor = LoadCursor(Resources.MoveLineInvertedCursor);
  }

  public static Cursor DrawLineCursor => IsDark(Settings.Color[Colors.Canvas]) ? m_drawLineInvertedCursor : m_drawLineCursor;

  public static Cursor MoveLineCursor => IsDark(Settings.Color[Colors.Canvas]) ? m_moveLineInvertedCursor : m_moveLineCursor;

  public static void AddLine(GraphicsPath path, LineSegment segment, Random random, bool straightEdges) {
    //      if (Settings.HandDrawnDoc && !straightEdges)
    if (!straightEdges) {
      var dx = segment.End.X - segment.Start.X;
      var dy = segment.End.Y - segment.Start.Y;
      var distance = (float) Math.Sqrt(dx * dx + dy * dy);
      var points = random.Next(Math.Max(3, (int) (distance / 15)), Math.Max(6, (int) (distance / 8)));
      var lines = points - 1;
      var last = segment.Start;
      for (var line = 0; line < lines; ++line) {
        Vector next;
        if (line == 0) {
          next = last;
        } else if (line == lines - 1) {
          next = segment.End;
        } else {
          var fraction = line / (float) (lines - 1);
          var x = segment.Start.X + (segment.End.X - segment.Start.X) * fraction;
          var y = segment.Start.Y + (segment.End.Y - segment.Start.Y) * fraction;

          x += random.Next(-1, 2);
          y += random.Next(-1, 2);
          next = new Vector(x, y);
        }

        path.AddLine(last.ToPointF(), next.ToPointF());
        last = next;
      }
    } else {
      path.AddLine(segment.Start.ToPointF(), segment.End.ToPointF());
    }
  }

  public static PointF Divide(PointF pos, float scalar) {
    return new PointF(pos.X / scalar, pos.Y / scalar);
  }

  public static void DrawChevron(Graphics graphics, PointF pos, float angle, float size, Brush fillBrush) {
    if (m_chevronPath == null) {
      var apex = new PointF(0.5f, 0);
      var leftCorner = new PointF(-0.5f, 0.5f);
      var rightCorner = new PointF(-0.5f, -0.5f);
      m_chevronPath = new GraphicsPath();
      m_chevronPath.AddLine(apex, rightCorner);
      m_chevronPath.AddLine(rightCorner, leftCorner);
      m_chevronPath.AddLine(leftCorner, apex);
    }

    var state = graphics.Save();
    graphics.TranslateTransform(pos.X, pos.Y);
    graphics.RotateTransform(angle);
    graphics.ScaleTransform(size, size);
    graphics.DrawPath(new Pen(fillBrush), m_chevronPath);
    graphics.Restore(state);
  }

  public static void DrawHandle(Canvas canvas, Graphics graphics, Palette palette, Rect bounds, DrawingContext context, bool alwaysAlpha, bool round) {
    if (bounds.Width <= 0 || bounds.Height <= 0) return;

    using (var quality = new Smoothing(graphics, SmoothingMode.Default)) {
      Brush brush;
      Pen pen;
      var alpha = 180;

      if (context.Selected) {
        if (!alwaysAlpha) alpha = 255;
        brush = palette.Gradient(bounds, Color.FromArgb(alpha, Color.LemonChiffon), Color.FromArgb(alpha, Color.DarkOrange));
        pen = palette.Pen(Color.FromArgb(alpha, Color.Chocolate), 0);
      } else {
        brush = palette.Gradient(bounds, Color.FromArgb(alpha, Color.LightCyan), Color.FromArgb(alpha, Color.SteelBlue));
        pen = palette.Pen(Color.FromArgb(alpha, Color.Navy), 0);
      }

      if (round) {
        graphics.DrawEllipse(new Pen(brush), bounds.ToRectangleF());
        //          graphics.DrawRectangle(new XPen(Color.Red), bounds.ToRectangleF() );
        graphics.DrawEllipse(pen, bounds.ToRectangleF());
      } else {
        graphics.DrawRectangle(new Pen(brush), bounds.ToRectangle());
        graphics.DrawRectangle(pen, bounds.ToRectangle());
      }
    }
  }

  public static string FontName(Font font) {
    if (!string.IsNullOrEmpty(font.OriginalFontName)) return font.OriginalFontName;
    return font.Name;
  }

  public static bool IsDark(Color color) {
    return Math.Max(color.R, Math.Max(color.G, color.B)) < 128;
  }

  public static Color Mix(Color a, Color b, int propA, int propB) {
    return Color.FromArgb(
      (byte) ((a.R * propA + b.R * propB) / (propA + propB)),
      (byte) ((a.G * propA + b.G * propB) / (propA + propB)),
      (byte) ((a.B * propA + b.B * propB) / (propA + propB)));
  }

  public static bool SetAlignmentFromCardinalOrOrdinalDirection(StringFormat format, CompassPoint compassPoint, RoomShape? rs = null) {
    switch (compassPoint) {
      case CompassPoint.North:
      case CompassPoint.NorthEast:
        format.LineAlignment = StringAlignment.Far;
        format.Alignment = StringAlignment.Near;
        break;
      case CompassPoint.East:
      case CompassPoint.SouthEast:
      case CompassPoint.South:
        format.LineAlignment = StringAlignment.Near;
        format.Alignment = StringAlignment.Near;
        break;
      case CompassPoint.West:
      case CompassPoint.SouthWest:
        format.LineAlignment = StringAlignment.Near;
        format.Alignment = StringAlignment.Far;
        break;
      case CompassPoint.NorthWest:
      case CompassPoint.NorthNorthWest:
        format.LineAlignment = StringAlignment.Far;
        format.Alignment = StringAlignment.Far;
        break;
      default:
        return false;
    }

    return true;
  }

  public static PointF Subtract(PointF a, PointF b) {
    return new PointF(a.X - b.X, a.Y - b.Y);
  }

  public static Rectangle ToRectangle(RectangleF rect) {
    return new Rectangle((int) rect.X, (int) rect.Y, (int) rect.Width, (int) rect.Height);
  }

  private static Cursor LoadCursor(byte[] bytes) {
    using (var stream = new MemoryStream(bytes)) {
      return new Cursor(stream);
    }
  }
}