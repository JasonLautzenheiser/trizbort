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
using System.Linq;
using System.Windows.Forms;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;
using Trizbort.Setup;

namespace Trizbort.UI.Controls {
  internal sealed partial class Minimap : UserControl {
    private const int OUTER_BORDER_SIZE = 2;
    private const int OUTER_PADDING = 3;
    private const int INNER_BORDER_SIZE = 2;
    private const int INNER_PADDING = 3;
    private const int TOTAL_PADDING = OUTER_BORDER_SIZE + OUTER_PADDING + INNER_BORDER_SIZE + INNER_PADDING;
    private bool mDraggingViewport;

    private Point mLastMousePosition;

    public Minimap() {
      InitializeComponent();

      // we cannot aquire the focus; we want keyboard input to go to the canvas.
      SetStyle(ControlStyles.Selectable, false);
      TabStop = false;

      SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
      DoubleBuffered = true;
    }

    public Canvas Canvas {  get; set; }

    public bool IsMouseOverMe() {
      return ClientRectangle.Contains(PointToClient(MousePosition));
    }

    protected override void OnMouseDown(MouseEventArgs e) {
      if (e.Button == MouseButtons.Left) {
        setCanvasOrigin(e.Location);
        mLastMousePosition = e.Location;
        Capture = true;
        mDraggingViewport = true;
        Invalidate();
      }

      base.OnMouseDown(e);
    }

    protected override void OnMouseMove(MouseEventArgs e) {
      if (mDraggingViewport && e.Location != mLastMousePosition) {
        setCanvasOrigin(e.Location);
        Invalidate();
      }

      mLastMousePosition = e.Location;

      base.OnMouseMove(e);
    }

    protected override void OnMouseUp(MouseEventArgs e) {
      if (mDraggingViewport) {
        mDraggingViewport = false;
        Capture = false;
        Invalidate();
      }


      base.OnMouseUp(e);
    }

    protected override void OnPaint(PaintEventArgs e) {
      if (DesignMode) {
        e.Graphics.Clear(Settings.Color[Colors.Canvas]);
        return;
      }

      foreach (var element in Project.Current.Elements)
        element.Flagged = false;
      foreach (var element in Canvas.SelectedElements)
        element.Flagged = true;

      using (var graphics = Graphics.FromHdc(e.Graphics.GetHdc())) {
        using (var palette = new Palette()) {
          var clientArea = new Rectangle(0, 0, Width, Height);

          ControlPaint.DrawBorder3D(graphics, clientArea, Border3DStyle.Raised);
          clientArea.Inflate(-OUTER_BORDER_SIZE, -OUTER_BORDER_SIZE);
          graphics.FillRectangle(SystemBrushes.Control, clientArea);
          clientArea.Inflate(-OUTER_PADDING, -OUTER_PADDING);
          ControlPaint.DrawBorder3D(graphics, clientArea, Border3DStyle.SunkenOuter);
          clientArea.Inflate(-INNER_BORDER_SIZE, -INNER_BORDER_SIZE);

          graphics.FillRectangle(palette.CanvasBrush, clientArea);
          clientArea.Inflate(-INNER_PADDING, -INNER_PADDING);

          //nativeGraphics.FillRectangle(Brushes.Cyan, clientArea);

          var canvasBounds = (Rect) Canvas?.ComputeCanvasBounds(false);

          foreach (var room in Project.Current.Elements.OfType<Room>()) {
            var roomBounds = canvasToClient(room.InnerBounds.ToRectangle(), canvasBounds, clientArea);

            var borderPen = room.Flagged
              ? palette.Pen(Settings.Color[Colors.SelectedLine], 0)
              : palette.Pen(Settings.Color[Colors.Border], 0);
            var paletteBorderBrush =
              room.Flagged ? new SolidBrush(Settings.Color[Colors.SelectedLine]) : palette.FillBrush;
            graphics.DrawRectangle(borderPen, Rectangle.Round(roomBounds));
          }

          if (Canvas != null) {
            // draw the viewport area as a selectable "handle"
            var viewportBounds = canvasToClient(Canvas.Viewport.ToRectangleF(), canvasBounds, clientArea);
            viewportBounds.Intersect(clientArea);
            if (Project.Current.Elements.Count > 0) {
              var context = new DrawingContext(1f) {Selected = mDraggingViewport};
              Drawing.DrawHandle(Canvas, graphics, palette, new Rect(viewportBounds), context, true, false);
            }
          }
        }
      }

      e.Graphics.ReleaseHdc();

      base.OnPaint(e);
    }

    protected override void WndProc(ref Message m) {
      switch (m.Msg) {
        case 0x0007: // WM_SETFOCUS
          // return focus to the canvas
          Canvas.Focus();
          m.Result = IntPtr.Zero;
          return;
      }

      base.WndProc(ref m);
    }

    private RectangleF canvasToClient(RectangleF bounds, Rect canvasBounds, Rectangle clientArea) {
      bounds.X = (bounds.X - canvasBounds.Left) / Math.Max(1, canvasBounds.Width) * clientArea.Width + TOTAL_PADDING;
      bounds.Y = (bounds.Y - canvasBounds.Top) / Math.Max(1, canvasBounds.Height) * clientArea.Height + TOTAL_PADDING;
      bounds.Width = bounds.Width / Math.Max(1, canvasBounds.Width) * clientArea.Width;
      bounds.Height = bounds.Height / Math.Max(1, canvasBounds.Height) * clientArea.Height;
      return bounds;
    }

    private void setCanvasOrigin(Point clientPosition) {
      if (Canvas == null)
        return;

      // get the minimap client area, in pixels, without borders and padding
      var clientArea = new Rect(0, 0, Width, Height);
      clientArea.Inflate(-TOTAL_PADDING, -TOTAL_PADDING);

      // clamp the mouse within the client area
      clientPosition = clientArea.Clamp(new Vector(clientPosition)).ToPoint();

      // get the mouse position as a percentage of the client area size
      var x = (clientPosition.X - TOTAL_PADDING) / clientArea.Width;
      var y = (clientPosition.Y - TOTAL_PADDING) / clientArea.Height;

      // get the visible area on the canvas, in canvas coordinates
      var viewport = Canvas.Viewport;
      var canvasBounds = Canvas.ComputeCanvasBounds(false);

      // limit it to the rectangle in which the center of the viewport may be placed whilst rendering only the occupied portions of the canvas visible
      var restrictedBounds = canvasBounds;
      if (restrictedBounds.Width > viewport.Width) {
        restrictedBounds.Inflate(-viewport.Width / 2, 0);
      } else {
        restrictedBounds.X += restrictedBounds.Width / 2;
        restrictedBounds.Width = 0;
      }

      if (restrictedBounds.Height > viewport.Height) {
        restrictedBounds.Inflate(0, -viewport.Height / 2);
      } else {
        restrictedBounds.Y += restrictedBounds.Height / 2;
        restrictedBounds.Height = 0;
      }

      // center the canvas on the mouse position in canvas coordinates, limited to the above rectangle
      Canvas.Origin = restrictedBounds.Clamp(new Vector(canvasBounds.Left + canvasBounds.Width * x, canvasBounds.Top + canvasBounds.Height * y));
    }
  }
}