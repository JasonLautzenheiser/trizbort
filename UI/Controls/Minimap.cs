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
using System.Windows.Forms;
using PdfSharp.Drawing;

namespace Trizbort.UI.Controls
{
    partial class Minimap : UserControl
    {
        const int OuterBorderSize = 2;
        const int OuterPadding = 3;
        const int InnerBorderSize = 2;
        const int InnerPadding = 3;
        const int TotalPadding = OuterBorderSize + OuterPadding + InnerBorderSize + InnerPadding;

        public Minimap()
        {
            InitializeComponent();

            // we cannot aquire the focus; we want keyboard input to go to the canvas.
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;            

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0007: // WM_SETFOCUS
                    // return focus to the canvas
                    Canvas.Focus();
                    m.Result = IntPtr.Zero;
                    return;
            }

            base.WndProc(ref m);
        }

        public UI.Controls.Canvas Canvas
        {
            get;
            set;
        }

        RectangleF CanvasToClient(RectangleF bounds, Rect canvasBounds, Rectangle clientArea)
        {
            bounds.X = (bounds.X - canvasBounds.Left) / Math.Max(1, canvasBounds.Width) * clientArea.Width + TotalPadding;
            bounds.Y = (bounds.Y - canvasBounds.Top) / Math.Max(1, canvasBounds.Height) * clientArea.Height + TotalPadding;
            bounds.Width = bounds.Width / Math.Max(1, canvasBounds.Width) * clientArea.Width;
            bounds.Height = bounds.Height / Math.Max(1, canvasBounds.Height) * clientArea.Height;
            return bounds;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                e.Graphics.Clear(Settings.Color[Colors.Canvas]);
                return;
            }

            foreach (var element in Project.Current.Elements)
            {
                element.Flagged = false;
            }
            foreach (var element in Canvas.SelectedElements)
            {
                element.Flagged = true;
            }

            using (var nativeGraphics = Graphics.FromHdc(e.Graphics.GetHdc()))
            {
                using (var graphics = XGraphics.FromGraphics(nativeGraphics, new XSize(Width, Height)))
                {
                    using (var palette = new Palette())
                    {
                        var clientArea = new Rectangle(0, 0, Width, Height);

                        ControlPaint.DrawBorder3D(nativeGraphics, clientArea, Border3DStyle.Raised);
                        clientArea.Inflate(-OuterBorderSize, -OuterBorderSize);
                        nativeGraphics.FillRectangle(SystemBrushes.Control, clientArea);
                        clientArea.Inflate(-OuterPadding, -OuterPadding);
                        ControlPaint.DrawBorder3D(nativeGraphics, clientArea, Border3DStyle.SunkenOuter);
                        clientArea.Inflate(-InnerBorderSize, -InnerBorderSize);

                        nativeGraphics.FillRectangle(palette.CanvasBrush, clientArea);
                        clientArea.Inflate(-InnerPadding, -InnerPadding);

                        //nativeGraphics.FillRectangle(Brushes.Cyan, clientArea);

                        var canvasBounds = Canvas != null ? Canvas.ComputeCanvasBounds(false) : Rect.Empty;

                        var borderPen = palette.Pen(Settings.Color[Colors.Border], 0);
                        foreach (var element in Project.Current.Elements)
                        {
                            if (element is Room)
                            {
                                var room = (Room)element;
                                var roomBounds = CanvasToClient(room.InnerBounds.ToRectangleF(), canvasBounds, clientArea);
                                graphics.DrawRectangle(borderPen, room.Flagged ? palette.BorderBrush : palette.FillBrush, roomBounds);
                            }
                        }

                        if (Canvas != null)
                        {
                            // draw the viewport area as a selectable "handle"
                            var viewportBounds = CanvasToClient(Canvas.Viewport.ToRectangleF(), canvasBounds, clientArea);
                            viewportBounds.Intersect(clientArea);
                            if (Project.Current.Elements.Count > 0)
                            {
                                var context = new DrawingContext(1f);
                                context.Selected = m_draggingViewport;
                                Drawing.DrawHandle(Canvas, graphics, palette, new Rect(viewportBounds), context, true, false);
                            }
                        }
                    }
                }
            }
            e.Graphics.ReleaseHdc();

            base.OnPaint(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SetCanvasOrigin(e.Location);
                m_lastMousePosition = e.Location;
                Capture = true;
                m_draggingViewport = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (m_draggingViewport && e.Location != m_lastMousePosition)
            {
                SetCanvasOrigin(e.Location);
                Invalidate();
            }
            m_lastMousePosition = e.Location;

            base.OnMouseMove(e);
        }

        public bool IsMouseOverMe()
        {
            return  ClientRectangle.Contains(PointToClient(Control.MousePosition));
        }

        void SetCanvasOrigin(Point clientPosition)
        {
            if (Canvas == null)
            {
                return;
            }

            // get the minimap client area, in pixels, without borders and padding
            var clientArea = new Rect(0, 0, Width, Height);
            clientArea.Inflate(-TotalPadding, -TotalPadding);

            // clamp the mouse within the client area
            clientPosition = clientArea.Clamp(new Vector(clientPosition)).ToPoint();

            // get the mouse position as a percentage of the client area size
            var x = (clientPosition.X - TotalPadding) / clientArea.Width;
            var y = (clientPosition.Y - TotalPadding) / clientArea.Height;

            // get the visible area on the canvas, in canvas coordinates
            var viewport = Canvas.Viewport;
            var canvasBounds = Canvas.ComputeCanvasBounds(false);

            // limit it to the rectangle in which the center of the viewport may be placed whilst rendering only the occupied portions of the canvas visible
            var restrictedBounds = canvasBounds;
            if (restrictedBounds.Width > viewport.Width)
            {
                restrictedBounds.Inflate(-viewport.Width / 2, 0);
            }
            else
            {
                restrictedBounds.X += restrictedBounds.Width / 2;
                restrictedBounds.Width = 0;
            }
            if (restrictedBounds.Height > viewport.Height)
            {
                restrictedBounds.Inflate(0, -viewport.Height / 2);
            }
            else
            {
                restrictedBounds.Y += restrictedBounds.Height / 2;
                restrictedBounds.Height = 0;
            }

            // center the canvas on the mouse position in canvas coordinates, limited to the above rectangle
            Canvas.Origin = restrictedBounds.Clamp(new Vector(canvasBounds.Left + canvasBounds.Width * x, canvasBounds.Top + canvasBounds.Height * y));
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
           
            if (m_draggingViewport)
            {
                m_draggingViewport = false;
                Capture = false;
                Invalidate();
            }


            base.OnMouseUp(e);
        }

        Point m_lastMousePosition;
        bool m_draggingViewport;
    }
}
