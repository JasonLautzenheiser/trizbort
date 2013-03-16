/*
    Copyright (c) 2010 by Genstein

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
using PdfSharp.Drawing;

namespace Trizbort
{
    /// <summary>
    /// A palette of drawing tools such as brushes, pens and fonts.
    /// </summary>
    /// <remarks>
    /// Centralising such tools in one place avoids tedious management
    /// of their lifetimes and avoids creating them until necessary,
    /// and then only once.
    /// </remarks>
    internal class Palette : IDisposable
    {
        public void Dispose()
        {
            foreach (var item in m_items)
            {
                item.Dispose();
            }
        }

        public Pen Pen(Color color)
        {
            return Pen(color, Settings.LineWidth);
        }

        public Pen Pen(Color color, float width)
        {
            var pen = new Pen(color, width);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            m_items.Add(pen);
            return pen;
        }

        public Brush Brush(Color color)
        {
            var brush = new SolidBrush(color);
            m_items.Add(brush);
            return brush;
        }

        public XBrush Gradient(Rect rect, Color color1, Color color2)
        {
            var brush = new XLinearGradientBrush(rect.ToRectangleF(), color1, color2, XLinearGradientMode.ForwardDiagonal);
            //m_items.Add(brush);
            return brush;
        }

        public Font Font(string familyName, float emSize)
        {
            var font = new Font(familyName, emSize, FontStyle.Regular, GraphicsUnit.World);
            m_items.Add(font);
            return font;
        }

        public Font Font(Font prototype, FontStyle newStyle)
        {
            var font = new Font(prototype, newStyle);
            m_items.Add(font);
            return font;
        }

        public XGraphicsPath Path()
        {
            var path = new XGraphicsPath();
            //m_items.Add(path);
            return path;
        }

        public Pen LinePen
        {
            get
            {
                if (m_linePen == null)
                {
                    m_linePen = Pen(Settings.Color[Colors.Line]);
                }
                return m_linePen;
            }
        }

        public Pen DashedLinePen
        {
            get
            {
                if (m_dashedLinePen == null)
                {
                    m_dashedLinePen = Pen(Settings.Color[Colors.Line]);
                    m_dashedLinePen.DashStyle = DashStyle.Dot;
                }
                return m_dashedLinePen;
            }
        }

        public Pen SelectedLinePen
        {
            get
            {
                if (m_selectedLinePen == null)
                {
                    m_selectedLinePen = Pen(Settings.Color[Colors.SelectedLine]);
                }
                return m_selectedLinePen;
            }
        }

        public Pen SelectedDashedLinePen
        {
            get
            {
                if (m_selectedDashedLinePen == null)
                {
                    m_selectedDashedLinePen = Pen(Settings.Color[Colors.SelectedLine]);
                    m_selectedDashedLinePen.DashStyle = DashStyle.Dot;
                }
                return m_selectedDashedLinePen;
            }
        }

        public Pen HoverLinePen
        {
            get
            {
                if (m_hoverLinePen == null)
                {
                    m_hoverLinePen = Pen(Settings.Color[Colors.HoverLine]);
                }
                return m_hoverLinePen;
            }
        }

        public Pen HoverDashedLinePen
        {
            get
            {
                if (m_hoverDashedLinePen == null)
                {
                    m_hoverDashedLinePen = Pen(Settings.Color[Colors.HoverLine]);
                    m_hoverDashedLinePen.DashStyle = DashStyle.Dot;
                }
                return m_hoverDashedLinePen;
            }
        }

        public Pen GetLinePen(bool selected, bool hover, bool dashed)
        {
            if (selected)
            {
                return dashed ? SelectedDashedLinePen : SelectedLinePen;
            }
            else if (hover)
            {
                return dashed ? HoverDashedLinePen : HoverLinePen;
            }
            return dashed ? DashedLinePen : LinePen;
        }

        public Brush GetLineBrush(bool selected, bool hover)
        {
            if (selected)
            {
                return SelectedLineBrush;
            }
            else if (hover)
            {
                return HoverLineBrush;
            }
            return LineBrush;
        }

        public Brush LineBrush
        {
            get
            {
                if (m_lineBrush == null)
                {
                    m_lineBrush = Brush(Settings.Color[Colors.Line]);
                }
                return m_lineBrush;
            }
        }

        public Brush SelectedLineBrush
        {
            get
            {
                if (m_selectedLineBrush == null)
                {
                    m_selectedLineBrush = Brush(Settings.Color[Colors.SelectedLine]);
                }
                return m_selectedLineBrush;
            }
        }

        public Brush HoverLineBrush
        {
            get
            {
                if (m_hoverLineBrush == null)
                {
                    m_hoverLineBrush = Brush(Settings.Color[Colors.HoverLine]);
                }
                return m_hoverLineBrush;
            }
        }

        public Pen BorderPen
        {
            get
            {
                if (m_borderPen == null)
                {
                    m_borderPen = Pen(Settings.Color[Colors.Border]);
                }
                return m_borderPen;
            }
        }

        public Pen LargeTextPen
        {
            get
            {
                if (m_largeTextPen == null)
                {
                    m_largeTextPen = Pen(Settings.Color[Colors.LargeText]);
                }
                return m_largeTextPen;
            }
        }

        public Pen FillPen
        {
            get
            {
                if (m_fillPen == null)
                {
                    m_fillPen = Pen(Settings.Color[Colors.Fill]);
                }
                return m_fillPen;
            }
        }

        public Pen GridPen
        {
            get
            {
                if (m_gridPen == null)
                {
                    m_gridPen = Pen(Settings.Color[Colors.Grid], 0);
                }
                return m_gridPen;
            }
        }

        public Brush CanvasBrush
        {
            get
            {
                if (m_canvasBrush == null)
                {
                    m_canvasBrush = Brush(Settings.Color[Colors.Canvas]);
                }
                return m_canvasBrush;
            }
        }

        public Brush BorderBrush
        {
            get
            {
                if (m_borderBrush == null)
                {
                    m_borderBrush = Brush(Settings.Color[Colors.Border]);
                }
                return m_borderBrush;
            }
        }

        public Brush FillBrush
        {
            get
            {
                if (m_fillBrush == null)
                {
                    m_fillBrush = Brush(Settings.Color[Colors.Fill]);
                }
                return m_fillBrush;
            }
        }

        public Brush LargeTextBrush
        {
            get
            {
                if (m_largeTextBrush == null)
                {
                    m_largeTextBrush = Brush(Settings.Color[Colors.LargeText]);
                }
                return m_largeTextBrush;
            }
        }

        public Brush SmallTextBrush
        {
            get
            {
                if (m_smallTextBrush == null)
                {
                    m_smallTextBrush = Brush(Settings.Color[Colors.SmallText]);
                }
                return m_smallTextBrush;
            }
        }

        public Brush LineTextBrush
        {
            get
            {
                if (m_lineTextBrush == null)
                {
                    m_lineTextBrush = Brush(Settings.Color[Colors.LineText]);
                }
                return m_lineTextBrush;
            }
        }

        public Brush MarqueeFillBrush
        {
            get
            {
                if (m_marqueeFillBrush == null)
                {
                    m_marqueeFillBrush = Brush(Color.FromArgb(80, Settings.Color[Colors.Border]));
                }
                return m_marqueeFillBrush;
            }
        }

        public Pen MarqueeBorderPen
        {
            get
            {
                if (m_marqueeBorderPen == null)
                {
                    m_marqueeBorderPen = Pen(Color.FromArgb(120, Settings.Color[Colors.Border]), 0);
                }
                return m_marqueeBorderPen;
            }
        }

        public Pen ResizeBorderPen
        {
            get
            {
                if (m_resizeBorderPen == null)
                {
                    m_resizeBorderPen = Pen(Color.FromArgb(64, Color.SteelBlue), 6);
                    //m_resizeBorderPen.DashStyle = DashStyle.Dot;
                }
                return m_resizeBorderPen;
            }
        }

        private List<IDisposable> m_items = new List<IDisposable>();

        private Pen m_linePen;
        private Pen m_dashedLinePen;
        private Pen m_selectedLinePen;
        private Pen m_selectedDashedLinePen;
        private Pen m_hoverLinePen;
        private Pen m_hoverDashedLinePen;
        private Brush m_lineBrush;
        private Brush m_selectedLineBrush;
        private Brush m_hoverLineBrush;
        private Pen m_borderPen;
        private Pen m_largeTextPen;
        private Pen m_fillPen;
        private Pen m_gridPen;
        private Brush m_borderBrush;
        private Brush m_fillBrush;
        private Brush m_canvasBrush;
        private Brush m_largeTextBrush;
        private Brush m_smallTextBrush;
        private Brush m_lineTextBrush;
        private Brush m_marqueeFillBrush;
        private Pen m_marqueeBorderPen;
        private Pen m_resizeBorderPen;
    }
}
