using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using Trizbort.Setup;

namespace Trizbort.Domain.Misc; 

/// <summary>
///   A palette of drawing tools such as brushes, pens and fonts.
/// </summary>
/// <remarks>
///   Centralising such tools in one place avoids tedious management
///   of their lifetimes and avoids creating them until necessary,
///   and then only once.
/// </remarks>
public sealed class Palette : IDisposable {
  private readonly List<IDisposable> m_items = new List<IDisposable>();
  private Brush m_borderBrush;
  private Pen m_borderPen;
  private Brush m_canvasBrush;
  private Pen m_dashedLinePen;
  private Brush m_fillBrush;
  // TODO: private Pen m_fillPen;
  private Pen m_gridPen;
  private Pen m_hoverDashedLinePen;
  private Brush m_hoverLineBrush;
  private Pen m_hoverLinePen;

  private Brush m_lineBrush;

  private Pen m_linePen;
  private Brush m_lineTextBrush;
  private Pen m_marqueeBorderPen;
  private Brush m_marqueeFillBrush;
  private Pen m_resizeBorderPen;
  private Pen m_selectedDashedLinePen;
  private Brush m_selectedLineBrush;
  private Pen m_selectedLinePen;
  private Brush m_smallTextBrush;
  private Brush m_subTitleTextBrush;
  // TODO: private Pen m_subTitleTextPen;

  public Brush BorderBrush => m_borderBrush ?? (m_borderBrush = Brush(Settings.Color[Colors.Border]));

  public Pen BorderPen => m_borderPen ?? (m_borderPen = Pen(Settings.Color[Colors.Border]));

  public Brush CanvasBrush => m_canvasBrush ?? (m_canvasBrush = Brush(Settings.Color[Colors.Canvas]));

  public Pen DashedLinePen {
    get {
      if (m_dashedLinePen == null) {
        m_dashedLinePen = Pen(Settings.Color[Colors.Line]);
        m_dashedLinePen.DashStyle = DashStyle.Dot;
      }

      return m_dashedLinePen;
    }
  }

  public Brush FillBrush => m_fillBrush ?? (m_fillBrush = Brush(Color.White));

  //public Pen FillPen
  //{
  //    get { return m_fillPen ?? (m_fillPen = Pen(Settings.Color[Colors.Fill])); }
  //}

  public Pen GridPen => m_gridPen ?? (m_gridPen = Pen(Settings.Color[Colors.Grid], 0));

  public Pen HoverDashedLinePen {
    get {
      if (m_hoverDashedLinePen == null) {
        m_hoverDashedLinePen = Pen(Settings.Color[Colors.HoverLine]);
        m_hoverDashedLinePen.DashStyle = DashStyle.Dot;
      }

      return m_hoverDashedLinePen;
    }
  }

  public Brush HoverLineBrush => m_hoverLineBrush ?? (m_hoverLineBrush = Brush(Settings.Color[Colors.HoverLine]));
  public Pen HoverLinePen => m_hoverLinePen ?? (m_hoverLinePen = Pen(Settings.Color[Colors.HoverLine]));

  public Brush LineBrush => m_lineBrush ?? (m_lineBrush = Brush(Settings.Color[Colors.Line]));
  public Pen LinePen => m_linePen ?? (m_linePen = Pen(Settings.Color[Colors.Line]));
  public Brush LineTextBrush => m_lineTextBrush ?? (m_lineTextBrush = Brush(Settings.Color[Colors.LineText]));

  public Pen MarqueeBorderPen => m_marqueeBorderPen ?? (m_marqueeBorderPen = Pen(Color.FromArgb(120, Settings.Color[Colors.Border]), 0));
  public Brush MarqueeFillBrush => m_marqueeFillBrush ?? (m_marqueeFillBrush = Brush(Color.FromArgb(80, Settings.Color[Colors.Border])));

  public Pen ResizeBorderPen => m_resizeBorderPen ?? (m_resizeBorderPen = Pen(Color.FromArgb(64, Color.SteelBlue), 6));

  public Pen SelectedDashedLinePen {
    get {
      if (m_selectedDashedLinePen == null) {
        m_selectedDashedLinePen = Pen(Settings.Color[Colors.SelectedLine]);
        m_selectedDashedLinePen.DashStyle = DashStyle.Dot;
      }

      return m_selectedDashedLinePen;
    }
  }

  public Brush SelectedLineBrush => m_selectedLineBrush ?? (m_selectedLineBrush = Brush(Settings.Color[Colors.SelectedLine]));
  public Pen SelectedLinePen => m_selectedLinePen ?? (m_selectedLinePen = Pen(Settings.Color[Colors.SelectedLine]));

  public Brush SmallTextBrush => m_smallTextBrush ?? (m_smallTextBrush = Brush(Settings.Color[Colors.SmallText]));
  public Brush SubtitleTextBrush => m_subTitleTextBrush ?? (m_subTitleTextBrush = Brush(Settings.Color[Colors.Subtitle]));

  public void Dispose() {
    foreach (var item in m_items) item.Dispose();
  }

  public Brush Brush(Color color) {
    var brush = new SolidBrush(color);
    m_items.Add(brush);
    return brush;
  }

  public Font Font(string familyName, float emSize) {
    var font = new Font(familyName, emSize, FontStyle.Regular, GraphicsUnit.World);
    m_items.Add(font);
    return font;
  }

  public Font Font(Font prototype, FontStyle newStyle) {
    var font = new Font(prototype, newStyle);
    m_items.Add(font);
    return font;
  }

  public Brush GetLineBrush(bool selected, bool hover) {
    if (selected)
      return SelectedLineBrush;
    if (hover) return HoverLineBrush;
    return LineBrush;
  }

  public Pen GetLinePen(bool selected, bool hover, bool dashed) {
    if (selected)
      return dashed ? SelectedDashedLinePen : SelectedLinePen;
    if (hover) return dashed ? HoverDashedLinePen : HoverLinePen;
    return dashed ? DashedLinePen : LinePen;
  }

  public Brush Gradient(Rect rect, Color color1, Color color2) {
    var brush = new LinearGradientBrush(rect.ToRectangleF(), color1, color2, LinearGradientMode.ForwardDiagonal);
    return brush;
  }

  public GraphicsPath Path() {
    var path = new GraphicsPath();
    //m_items.Add(path);
    return path;
  }

  public Pen Pen(Color color) {
    return Pen(color, Settings.LineWidth);
  }

  public Pen Pen(Color color, float width) {
    var pen = new Pen(color, width) {StartCap = LineCap.Round, EndCap = LineCap.Round};
    m_items.Add(pen);
    return pen;
  }
}