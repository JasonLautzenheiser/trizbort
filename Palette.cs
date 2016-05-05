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
    public class Palette : IDisposable
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
            var pen = new Pen(color, width) {StartCap = LineCap.Round, EndCap = LineCap.Round};
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
            get { return m_linePen ?? (m_linePen = Pen(Settings.Color[Colors.Line])); }
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
            get { return m_hoverLinePen ?? (m_hoverLinePen = Pen(Settings.Color[Colors.HoverLine])); }
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
            get { return m_lineBrush ?? (m_lineBrush = Brush(Settings.Color[Colors.Line])); }
        }

        public Brush SelectedLineBrush
        {
            get { return m_selectedLineBrush ?? (m_selectedLineBrush = Brush(Settings.Color[Colors.SelectedLine])); }
        }

        public Brush HoverLineBrush
        {
            get { return m_hoverLineBrush ?? (m_hoverLineBrush = Brush(Settings.Color[Colors.HoverLine])); }
        }

        public Pen BorderPen
        {
            get { return m_borderPen ?? (m_borderPen = Pen(Settings.Color[Colors.Border])); }
        }

        public Pen LargeTextPen
        {
            get { return m_largeTextPen ?? (m_largeTextPen = Pen(Settings.Color[Colors.LargeText])); }
        }

        public Pen FillPen
        {
            get { return m_fillPen ?? (m_fillPen = Pen(Settings.Color[Colors.Fill])); }
        }

        public Pen GridPen
        {
            get { return m_gridPen ?? (m_gridPen = Pen(Settings.Color[Colors.Grid], 0)); }
        }

        public Brush CanvasBrush
        {
            get { return m_canvasBrush ?? (m_canvasBrush = Brush(Settings.Color[Colors.Canvas])); }
        }

        public Brush BorderBrush
        {
            get { return m_borderBrush ?? (m_borderBrush = Brush(Settings.Color[Colors.Border])); }
        }

        public Brush FillBrush
        {
            get { return m_fillBrush ?? (m_fillBrush = Brush(Settings.Color[Colors.Fill])); }
        }

        public Brush LargeTextBrush
        {
            get { return m_largeTextBrush ?? (m_largeTextBrush = Brush(Settings.Color[Colors.LargeText])); }
        }

        public Brush SmallTextBrush
        {
            get { return m_smallTextBrush ?? (m_smallTextBrush = Brush(Settings.Color[Colors.SmallText])); }
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
