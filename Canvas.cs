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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using PdfSharp.Drawing;

namespace Trizbort
{
    internal partial class Canvas : UserControl, IAutomapCanvas
    {
        public Canvas()
        {
            InitializeComponent();

            SetStyle(ControlStyles.Selectable, true);
            TabStop = true;
            TabIndex = 0;

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            m_cornerPanel.BackColor = SystemColors.Control;

            PreviewKeyDown += OnPreviewKeyDown;

            m_recomputeTimer = new System.Threading.Timer(OnRecomputeTimerTick);

            Project.ProjectChanged += OnProjectChanged;
            OnProjectChanged(this, new ProjectChangedEventArgs(null, Project.Current));

            Settings.Changed += OnSettingsChanged;
            OnSettingsChanged(this, EventArgs.Empty);

            m_threadSafeAutomapCanvas = new MultithreadedAutomapCanvas(this);
            m_minimap.Canvas = this;
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                StopAutomapping();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0007: // WM_SETFOCUS
                    // do not pass focus to our child controls
                    m.Result = IntPtr.Zero;
                    return;
            }
            base.WndProc(ref m);
        }

        private void RequestRecomputeSmartSegments()
        {
            m_smartLineSegmentsUpToDate = false;
            m_recomputeTimer.Change(RecomputeNMillisecondsAfterChange, RecomputeNMillisecondsAfterChange);
        }

        private void OnRecomputeTimerTick(object state)
        {
            m_recomputeTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            var context = new DrawingContext(ZoomFactor);
            var elements = DepthSortElements();

            foreach (var element in elements)
            {
                element.PreDraw(context);
            }

            foreach (var element in elements)
            {
                element.RecomputeSmartLineSegments(context);
            }

            m_smartLineSegmentsUpToDate = true;
            Invalidate();
        }

        private void OnProjectChanged(object sender, ProjectChangedEventArgs e)
        {
            if (e.OldProject != null)
            {
                e.OldProject.Elements.Added -= OnElementAdded;
                e.OldProject.Elements.Removed -= OnElementRemoved;

                foreach (var element in e.OldProject.Elements)
                {
                    element.Changed -= OnElementChanged;
                }
            }
            if (e.NewProject != null)
            {
                e.NewProject.Elements.Added += OnElementAdded;
                e.NewProject.Elements.Removed += OnElementRemoved;

                foreach (var element in e.NewProject.Elements)
                {
                    element.Changed += OnElementChanged;
                }
            }

            Reset();
            ZoomToFit();
        }

        private void OnSettingsChanged(object sender, EventArgs e)
        {
            RequestRecomputeSmartSegments();
            BackColor = Settings.Color[Colors.Canvas];
            Invalidate();
        }

        private void OnElementAdded(object sender, ItemEventArgs<Element> e)
        {
            if (e.Item is Room)
            {
                var room = (Room)e.Item;
                room.Size = m_newRoomSize;
                room.IsDark = m_newRoomIsDark;
                room.ObjectsPosition = m_newRoomObjectsPosition;
            }
            e.Item.Changed += OnElementChanged;
            Project.Current.IsDirty = true;
            RequestRecomputeSmartSegments();
            Invalidate();
        }

        private void OnElementRemoved(object sender, ItemEventArgs<Element> e)
        {
            m_selectedElements.Remove(e.Item);
            UpdateSelection();
            EndDrag();
            UpdateDragHover(PointToClient(Control.MousePosition));

            Project.Current.IsDirty = true;
            e.Item.Changed -= OnElementChanged;
            RequestRecomputeSmartSegments();
            Invalidate();
        }

        private void OnElementChanged(object sender, EventArgs e)
        {
            if (sender is Room)
            {
                SetRoomDefaultsFrom((Room)sender);
            }
            if (sender is Connection)
            {
                SetConnectionDefaultsFrom((Connection)sender);
            }
            Invalidate();
            Project.Current.IsDirty = true;
            RequestRecomputeSmartSegments();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float ZoomFactor
        {
            get { return m_zoomFactor; }
            set
            {
                if (m_zoomFactor != value)
                {
                    m_zoomFactor = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Vector Origin
        {
            get { return m_origin; }
            set
            {
                if (m_origin != value)
                {
                    m_origin = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rect Viewport
        {
            get
            {
                var origin = Origin;
                var size = ClientToCanvas(new SizeF(Width, Height));
                return new Rect(origin.X - size.Width / 2, origin.Y - size.Height / 2, size.Width, size.Height);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                e.Graphics.Clear(Settings.Color[Colors.Canvas]);
                return;
            }

            using (var nativeGraphics = Graphics.FromHdc(e.Graphics.GetHdc()))
            {
                using (var graphics = XGraphics.FromGraphics(nativeGraphics, new XSize(Width, Height)))
                {
                    Draw(graphics, false, Width, Height);
                }
            }
            e.Graphics.ReleaseHdc();

            // update our scroll bars, unless this paint event was caused by the scroll bars,
            // in which case messing with them may cause the scroll bars to throw exceptions.
            if (!m_doNotUpdateScrollBarsNextPaint)
            {
                UpdateScrollBars();
            }
            m_doNotUpdateScrollBarsNextPaint = false;

            // update the minimap
            m_minimap.Invalidate();
            m_minimap.Update();
        }

        public Rect ComputeCanvasBounds(bool includePadding)
        {
            var bounds = Rect.Empty;
            foreach (var element in Project.Current.Elements)
            {
                bounds = element.UnionBoundsWith(bounds, true);
            }

            if (includePadding)
            {
                // HACK: fudge the canvas size to allow for overhanging line/object text
                bounds.Inflate(Math.Max(Settings.LineFont.GetHeight(), Settings.SmallFont.GetHeight()) * 24);
            }
            return bounds;
        }

        /// <summary>
        /// Draw the current project.
        /// </summary>
        /// <param name="graphics">The graphics with which to draw.</param>
        /// <param name="finalRender">True if rendering to PDF, an image, etc.; false if rendering to a window.</param>
        /// <param name="width">The width of the drawing area.</param>
        /// <param name="height">The height of the drawing area.</param>
        public void Draw(XGraphics graphics, bool finalRender, float width, float height)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var zoomFactor = ZoomFactor;
            var origin = Origin;
            if (finalRender)
            {
                // zoom to fit (0,0)-(width,height)
                var canvasBounds = ComputeCanvasBounds(true);
                ZoomFactor = Math.Min(canvasBounds.Width > 0 ? width / canvasBounds.Width : 1.0f, canvasBounds.Height > 0 ? height / canvasBounds.Height : 1.0f);
                Origin = new Vector(canvasBounds.X + canvasBounds.Width / 2, canvasBounds.Y + canvasBounds.Height / 2);
            }

            using (var palette = new Palette())
            {
                if (finalRender)
                {
                    graphics.Clear(Settings.Color[Colors.Canvas]);
                }

                if (!finalRender)
                {
                    DrawGrid(graphics, palette);
                }

                graphics.TranslateTransform(width / 2, height / 2);
                graphics.ScaleTransform(ZoomFactor, ZoomFactor);
                graphics.TranslateTransform(-Origin.X, -Origin.Y);

                if (Settings.DebugShowFPS && !finalRender)
                {
                    var canvasBounds = ComputeCanvasBounds(true);
                    graphics.DrawRectangle(XPens.Purple, canvasBounds.ToRectangleF());
                }

                if (Settings.ShowOrigin && !finalRender)
                {
                    var pen = palette.Pen(Drawing.Mix(Settings.Color[Colors.Canvas], Settings.Color[Colors.SmallText], 3, 1));
                    var n = Settings.GridSize;
                    graphics.DrawLine(pen, -n, 0, n, 0);
                    graphics.DrawLine(pen, 0, -n, 0, n);
                }

                graphics.SmoothingMode = XSmoothingMode.AntiAlias;

                DrawElements(graphics, palette, finalRender);
                if (!finalRender)
                {
                    DrawHandles(graphics, palette);
                    DrawPorts(graphics, palette);
                    DrawMarquee(graphics, palette);
                }

                stopwatch.Stop();
                if (Settings.DebugShowFPS && !finalRender)
                {
                    var fps = 1.0f / (float)(stopwatch.Elapsed.TotalSeconds);
                    graphics.Graphics.Transform = new Matrix();
                    graphics.DrawString(string.Format("{0} ms ({1} fps) {2} rebuilds", stopwatch.Elapsed.TotalMilliseconds, fps, TextBlock.RebuildCount), Settings.LargeFont, Brushes.Red, new PointF(10, 10 + Settings.LargeFont.GetHeight()));
                }
            }

            ZoomFactor = zoomFactor;
            Origin = origin;
        }

        private void DrawGrid(XGraphics graphics, Palette palette)
        {
            if (Settings.IsGridVisible && Settings.GridSize * ZoomFactor > 10)
            {
                var topLeft = Settings.Snap(ClientToCanvas(new PointF(-Settings.GridSize * ZoomFactor, -Settings.GridSize * ZoomFactor)));
                var bottomRight = Settings.Snap(ClientToCanvas(new PointF(Width + Settings.GridSize * ZoomFactor, Height + Settings.GridSize * ZoomFactor)));
                var points = new List<PointF>();
                var even = true;
                for (float x = topLeft.X; x <= bottomRight.X; x += Settings.GridSize)
                {
                    var start = CanvasToClient(new Vector(x, topLeft.Y));
                    var end = CanvasToClient(new Vector(x, bottomRight.Y));
                    if (even)
                    {
                        points.Add(start);
                        points.Add(end);
                    }
                    else
                    {
                        points.Add(end);
                        points.Add(start);
                    }
                    even = !even;
                    if (Settings.DebugDisableGridPolyline)
                    {
                        graphics.DrawLine(palette.GridPen, start, end);
                    }
                }
                if (!Settings.DebugDisableGridPolyline)
                {
                    graphics.DrawLines(palette.GridPen, points.ToArray());
                }
                points = new List<PointF>();
                for (float y = topLeft.Y; y <= bottomRight.Y; y += Settings.GridSize)
                {
                    var start = CanvasToClient(new Vector(topLeft.X, y));
                    var end = CanvasToClient(new Vector(bottomRight.X, y));
                    if (even)
                    {
                        points.Add(start);
                        points.Add(end);
                    }
                    else
                    {
                        points.Add(end);
                        points.Add(start);
                    }
                    even = !even;
                    if (Settings.DebugDisableGridPolyline)
                    {
                        graphics.DrawLine(palette.GridPen, start, end);
                    }
                }
                if (!Settings.DebugDisableGridPolyline)
                {
                    graphics.DrawLines(palette.GridPen, points.ToArray());
                }
            }
        }

        private List<Element> DepthSortElements()
        {
            var elements = new List<Element>();
            elements.AddRange(Project.Current.Elements);
            elements.Sort();
            return elements;
        }

        private void DrawElements(XGraphics graphics, Palette palette, bool finalRender)
        {
            if (Settings.DebugDisableElementRendering)
                return;

            bool disabledHandDrawLinesForSpeed = false;
            if (!finalRender && ZoomFactor < 0.75f && Settings.HandDrawn)
            {
                disabledHandDrawLinesForSpeed = true;
                Settings.HandDrawnUnchecked = false;
            }

            var context = new DrawingContext(ZoomFactor);
            context.UseSmartLineSegments = m_smartLineSegmentsUpToDate;
            var elements = DepthSortElements();

            if (!context.UseSmartLineSegments)
            {
                foreach (var element in elements)
                {
                    element.PreDraw(context);
                    element.Flagged = false;
                }
            }
            else
            {
                foreach (var element in elements)
                {
                    element.Flagged = false;
                }
            }

            foreach (var element in m_selectedElements)
            {
                element.Flagged = true;
            }

            var clipToScreen = new RectangleF(Origin.X - Width / 2 / ZoomFactor, Origin.Y - Height / 2 / ZoomFactor, Width / ZoomFactor, Height / ZoomFactor);

            foreach (var element in elements)
            {
                context.Selected = element.Flagged && !finalRender;
                context.Hover = !context.Selected && element == HoverElement && !finalRender;
                if (context.Hover && DragMode == DragModes.MovePort)
                {
                    // special case: when we're creating or moving a line, don't highlight elements we hover over;
                    // we don't want it to look like these elements can dock with the line, since they can't.
                    context.Hover = false;
                }

                try
                {
                    var elementBounds = element.UnionBoundsWith(Rect.Empty, true).ToRectangleF();
                    if (finalRender || clipToScreen.IntersectsWith(elementBounds))
                    {
                        element.Draw(graphics, palette, context);
                    }
                }
                catch (Exception)
                {
                    // avoid GDI+ exceptions (vast shapes, etc.) taking down the canvas
                }
            }

            if (disabledHandDrawLinesForSpeed)
            {
                Settings.HandDrawnUnchecked = true;
            }
        }

        private void DrawHandles(XGraphics graphics, Palette palette)
        {
            if (m_handles.Count == 0)
            {
                return;
            }

            var context = new DrawingContext(ZoomFactor);

            if (m_handles.Count > 1)
            {
                var bounds = Rect.Empty;

                foreach (var handle in m_handles)
                {
                    if (bounds == Rect.Empty)
                    {
                        bounds = new Rect(handle.Position, Vector.Zero);
                    }
                    else
                    {
                        bounds = bounds.Union(handle.Position);
                    }
                }
                bounds.X += Settings.HandleSize / 2f;
                bounds.Y += Settings.HandleSize / 2f;
                graphics.DrawRectangle(palette.ResizeBorderPen, bounds.ToRectangleF());
            }
            

            foreach (var handle in m_handles)
            {
                context.Selected = handle == HoverHandle;
                handle.Draw(this, graphics, palette, context);
            }
        }

        private void DrawPorts(XGraphics graphics, Palette palette)
        {
            var context = new DrawingContext(ZoomFactor);

            // draw all non-selected ports
            foreach (var port in m_ports)
            {
                if (HoverPort == port)
                {
                    // we'll draw this port last
                    continue;
                }

                context.Selected = false;
                port.Draw(this, graphics, palette, context);
            }

            if (HoverPort != null)
            {
                // lastly, always the port under the mouse, if any
                context.Selected = true;
                HoverPort.Draw(this, graphics, palette, context);
            }
        }

        private void DrawMarquee(XGraphics graphics, Palette palette)
        {
            var marqueeRect = GetMarqueeCanvasBounds();
            if (marqueeRect.Width > 0 && marqueeRect.Height > 0)
            {
                //var topLeft = CanvasToClient(new Vector(marqueeRect.X, marqueeRect.Y));
                //var bottomRight = CanvasToClient(new Vector(marqueeRect.X + marqueeRect.Width, marqueeRect.Y + marqueeRect.Height));
                //var rect = new RectangleF(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
                graphics.DrawRectangle(palette.MarqueeFillBrush, marqueeRect.ToRectangleF());
                var topLeft = new PointF(marqueeRect.Left, marqueeRect.Top);
                var topRight = new PointF(marqueeRect.Right, marqueeRect.Top);
                var bottomLeft = new PointF(marqueeRect.Left, marqueeRect.Bottom);
                var bottomRight = new PointF(marqueeRect.Right, marqueeRect.Bottom);
                graphics.DrawLine(palette.MarqueeBorderPen, topLeft, topRight);
                graphics.DrawLine(palette.MarqueeBorderPen, topRight, bottomRight);
                graphics.DrawLine(palette.MarqueeBorderPen, bottomLeft, bottomRight);
                graphics.DrawLine(palette.MarqueeBorderPen, topLeft, bottomLeft);
            }
        }

        public PointF CanvasToClient(Vector v)
        {
            v.X -= Origin.X;
            v.X *= ZoomFactor;
            v.X += Width / 2;
            v.Y -= Origin.Y;
            v.Y *= ZoomFactor;
            v.Y += Height / 2;
            return new PointF(v.X, v.Y);
        }

        public Vector ClientToCanvas(PointF p)
        {
            p.X -= Width / 2;
            p.X /= ZoomFactor;
            p.X += Origin.X;
            p.Y -= Height / 2;
            p.Y /= ZoomFactor;
            p.Y += Origin.Y;
            return new Vector(p.X, p.Y);
        }

        public SizeF CanvasToClient(SizeF s)
        {
            s.Width *= ZoomFactor;
            s.Height *= ZoomFactor;
            return s;
        }

        public SizeF ClientToCanvas(SizeF s)
        {
            s.Width /= ZoomFactor;
            s.Height /= ZoomFactor;
            return s;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.X < 0 || e.X > Width || e.Y < 0 || e.Y > Width)
                return;

            var pos = ClientToCanvas(new PointF(e.X, e.Y));

            if (e.Delta < 0)
            {
                ZoomIn();
            }
            else if (e.Delta > 0 && ZoomFactor > 1/100.0f)
            {
                ZoomOut();
            }

            Vector newPos = ClientToCanvas(new PointF(e.X, e.Y));
            Origin = Origin - (newPos - pos);

            Invalidate();
            UpdateDragHover(e.Location);

            base.OnMouseWheel(e);
        }

        private bool IsMiddleButton(MouseEventArgs e)
        {
            return e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Right && Control.ModifierKeys == Keys.Shift);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var clientPos = new PointF(e.X, e.Y);
            var canvasPos = ClientToCanvas(clientPos);
            m_lastMouseDownPosition = e.Location;

            if (DragMode != DragModes.None)
                return;

            if (IsMiddleButton(e))
            {
                BeginDragPan(clientPos, canvasPos);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (CanSelectElements)
                {
                    BeginDragMove(clientPos, canvasPos);
                }
                if (DragMode == DragModes.None)
                {
                    if (HoverPort != null && CanDrawLine)
                    {
                        BeginDragDrawLine();
                    }
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            EndDrag();
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // ignore spurious mouse move events
            if (m_lastKnownMousePosition == e.Location)
            {
                return;

            }
            m_lastKnownMousePosition = e.Location;

            UpdateDragHover(e.Location);
            base.OnMouseMove(e);
        }

        private void UpdateDragHover(Point mousePosition)
        {
            m_lastKnownMousePosition = mousePosition;

            var clientPos = new PointF(mousePosition.X, mousePosition.Y);
            var canvasPos = ClientToCanvas(clientPos);

            switch (DragMode)
            {
                case DragModes.Pan:
                    DoDragPan(clientPos, canvasPos);
                    break;
                case DragModes.MoveElement:
                    DoDragMoveElement(clientPos, canvasPos);
                    break;
                case DragModes.MoveResizeHandle:
                    DoDragMoveResizeHandle(clientPos, canvasPos);
                    break;
                case DragModes.MovePort:
                    HoverElement = HitTestElement(canvasPos, true);
                    HoverPort = HitTestPort(canvasPos);
                    DoDragMovePort(clientPos, canvasPos);
                    break;
                case DragModes.None:
                    HoverHandle = HitTestHandle(canvasPos); // set first; it will RecreatePorts() if the value changes
                    HoverPort = HitTestPort(canvasPos);
                    HoverElement = HitTestElement(canvasPos, false);
                    break;
                case DragModes.DrawLine:
                    if (new Vector(m_lastMouseDownPosition).Distance(new Vector(mousePosition)) > Settings.DragDistanceToInitiateNewConnection)
                    {
                        var startPos = new PointF(m_lastMouseDownPosition.X, m_lastMouseDownPosition.Y);
                        BeginDrawConnection(startPos, ClientToCanvas(startPos));
                    }
                    break;
                case DragModes.Marquee:
                    if (m_dragMarqueeLastPosition != canvasPos)
                    {
                        m_dragMarqueeLastPosition = canvasPos;
                        Invalidate();
                    }
                    break;
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CanSelectElements && HasSingleSelectedElement && SelectedElement.HasDialog)
                {
                    SelectedElement.ShowDialog();
                }
            }
            base.OnMouseDoubleClick(e);
        }

        void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SelectedElement == null)
                {
                    // select the room closest to the center of the viewport
                    var viewportCenter = Viewport.Center;
                    Room closestRoom = null;
                    var closestDistance = float.MaxValue;
                    foreach (var element in Project.Current.Elements)
                    {
                        if (element is Room)
                        {
                            var room = (Room)element;
                            var roomCenter = room.InnerBounds.Center;
                            var distance = roomCenter.Distance(viewportCenter);
                            if (distance < closestDistance)
                            {
                                closestRoom = room;
                                closestDistance = distance;
                            }
                        }
                    }
                    SelectedElement = closestRoom;
                    EnsureVisible(SelectedElement);
                }
                else if (HasSingleSelectedElement && SelectedElement.HasDialog)
                {
                    SelectedElement.ShowDialog();
                }
            }
            else if (e.KeyCode == Keys.Escape && SelectedElement != null)
            {
                // clear selection
                SelectedElement = null;
            }
            else if (e.KeyCode == Keys.A && e.Control)
            {
                // select all
                SelectAll();
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
            {
                ZoomIn();
            }
            else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
            {
                ZoomOut();
            }
            else if (e.KeyCode == Keys.Home)
            {
                if (e.Control)
                {
                    ZoomToFit();
                }
                else
                {
                    ResetZoomOrigin();
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                Origin += new Vector(0, (e.KeyCode == Keys.Down ? 1 : -1) * Viewport.Height / (e.Shift ? 5 : 10));
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                Origin += new Vector((e.KeyCode == Keys.Right ? 1 : -1) * Viewport.Width / (e.Shift ? 5 : 10), 0);
            }
            else if (e.KeyCode == Keys.R)
            {
                AddRoom(true);
            }
            else if (e.KeyCode == Keys.T)
            {
                if (NewConnectionStyle == ConnectionStyle.Solid)
                {
                    NewConnectionStyle = ConnectionStyle.Dashed;
                }
                else
                {
                    NewConnectionStyle = ConnectionStyle.Solid;
                }
                ApplyConnectionStyle(NewConnectionStyle);
            }
            else if (e.KeyCode == Keys.A)
            {
                if (NewConnectionFlow == ConnectionFlow.TwoWay)
                {
                    NewConnectionFlow = ConnectionFlow.OneWay;
                }
                else
                {
                    NewConnectionFlow = ConnectionFlow.TwoWay;
                }
                ApplyConnectionFlow(NewConnectionFlow);
            }
            else if (e.KeyCode == Keys.P)
            {
                ApplyNewPlainConnectionSettings();
            }
            else if (e.KeyCode == Keys.U)
            {
                NewConnectionLabel = ConnectionLabel.Up;
                ApplyConnectionLabel(NewConnectionLabel);
            }
            else if (e.KeyCode == Keys.D)
            {
                NewConnectionLabel = ConnectionLabel.Down;
                ApplyConnectionLabel(NewConnectionLabel);
            }
            else if (e.KeyCode == Keys.I)
            {
                NewConnectionLabel = ConnectionLabel.In;
                ApplyConnectionLabel(NewConnectionLabel);
            }
            else if (e.KeyCode == Keys.O)
            {
                NewConnectionLabel = ConnectionLabel.Out;
                ApplyConnectionLabel(NewConnectionLabel);
            }
            else if (e.KeyCode == Keys.V)
            {
                ReverseLineDirection();
            }
            else if (e.KeyCode == Keys.K)
            {
                foreach (var element in m_selectedElements)
                {
                    if (element is Room)
                    {
                        var room = (Room)element;
                        room.IsDark = !room.IsDark;
                    }
                }
            }
            else if (e.KeyCode == Keys.F1 && Control.ModifierKeys == Keys.Control)
            {
                Settings.DebugShowFPS = !Settings.DebugShowFPS;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F2 && Control.ModifierKeys == Keys.Control)
            {
                Settings.DebugDisableElementRendering = !Settings.DebugDisableElementRendering;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F3 && Control.ModifierKeys == Keys.Control)
            {
                Settings.DebugDisableLineRendering = !Settings.DebugDisableLineRendering;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F4 && Control.ModifierKeys == Keys.Control)
            {
                Settings.DebugDisableTextRendering = !Settings.DebugDisableTextRendering;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F5 && Control.ModifierKeys == Keys.Control)
            {
                Settings.DebugDisableGridPolyline = !Settings.DebugDisableGridPolyline;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F5)
            {
                // for diagnostic purposes, cancel single stepping
                if (IsAutomapping)
                {
                    m_automap.RunToCompletion();
                }
            }
            else if (e.KeyCode == Keys.F11)
            {
                // for diagnostic purposes, allow single stepping
                m_automap.Step();
            }
            else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                // the numeric keypad keys allow rooms to be quickly selected or added.
                CompassPoint? compassPoint = null;
                switch (e.KeyCode)
                {
                    case Keys.NumPad8:
                        compassPoint = CompassPoint.North;
                        break;
                    case Keys.NumPad9:
                        compassPoint = CompassPoint.NorthEast;
                        break;
                    case Keys.NumPad6:
                        compassPoint = CompassPoint.East;
                        break;
                    case Keys.NumPad3:
                        compassPoint = CompassPoint.SouthEast;
                        break;
                    case Keys.NumPad2:
                        compassPoint = CompassPoint.South;
                        break;
                    case Keys.NumPad1:
                        compassPoint = CompassPoint.SouthWest;
                        break;
                    case Keys.NumPad4:
                        compassPoint = CompassPoint.West;
                        break;
                    case Keys.NumPad7:
                        compassPoint = CompassPoint.NorthWest;
                        break;
                }
                if (compassPoint.HasValue)
                {
                    if (!SelectRoomRelativeToSelectedRoom(compassPoint.Value))
                    {
                        if (Control.ModifierKeys == Settings.KeypadNavigationCreationModifier)
                        {
                            AddOrConnectRoomRelativeToSelectedRoom(compassPoint.Value);
                        }
                        else if (Control.ModifierKeys == Settings.KeypadNavigationUnexploredModifier)
                        {
                            AddUnexploredConnectionToSelectedRoom(compassPoint.Value);
                        }
                    }
                }
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Select the room in the given direction from the selected room;
        /// </summary>
        /// <param name="compassPoint">The direction to consider.</param>
        /// <returns>True if a new room was found and selected; false otherwise.</returns>
        private bool SelectRoomRelativeToSelectedRoom(CompassPoint compassPoint)
        {
            if (SelectedElement is Room)
            {
                var room = (Room)SelectedElement;
                var nextRoom = GetRoomInApproximateDirectionFromRoom(room, compassPoint);
                if (nextRoom != null)
                {
                    SelectedElement = nextRoom;
                    EnsureVisible(SelectedElement);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find a room adjacent to the selected room in the given direction;
        /// if found, connect the rooms. If not, create a new room in that direction.
        /// </summary>
        /// <param name="compassPoint">The direction to consider.</param>
        /// <returns>True if a new connection/room was made; false otherwise.</returns>
        private bool AddOrConnectRoomRelativeToSelectedRoom(CompassPoint compassPoint)
        {
            if (SelectedElement is Room)
            {
                var room = (Room)SelectedElement;
                var rect = room.InnerBounds;
                rect.Inflate(Settings.PreferredDistanceBetweenRooms + room.Width / 2, Settings.PreferredDistanceBetweenRooms + room.Height / 2);
                var centerOfNewRoom = rect.GetCorner(compassPoint);

                var existing = HitTestElement(centerOfNewRoom, false);
                if (existing is Room)
                {
                    // just connect the rooms together
                    AddConnection(room, compassPoint, (Room)existing, CompassPointHelper.GetOpposite(compassPoint));
                    SelectedElement = existing;
                    EnsureVisible(SelectedElement);
                }
                else
                {
                    // new room entirely
                    var newRoom = new Room(Project.Current);

                    newRoom.Position = new Vector(centerOfNewRoom.X - room.Width / 2, centerOfNewRoom.Y - room.Height / 2); ;
                    newRoom.Size = room.Size;

                    Project.Current.Elements.Add(newRoom);
                    AddConnection(room, compassPoint, newRoom, CompassPointHelper.GetOpposite(compassPoint));
                    SelectedElement = newRoom;
                    EnsureVisible(SelectedElement);
                    Refresh();
                    newRoom.ShowDialog();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add an "unexplored" (loopback) connection from 
        /// </summary>
        /// <param name="room"></param>
        /// <param name="compassPoint"></param>
        private bool AddUnexploredConnectionToSelectedRoom(CompassPoint compassPoint)
        {
            if (SelectedElement is Room)
            {
                var room = (Room)SelectedElement;
                AddConnection(room, compassPoint, room, compassPoint);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add a new connection between the given rooms.
        /// </summary>
        /// <param name="roomOne">The first room.</param>
        /// <param name="compassPointOne">The direction of the connection in the first room.</param>
        /// <param name="roomTwo">The second room.</param>
        /// <param name="compassPointTwo">The direction of the connection in the second room.</param>
        private Connection AddConnection(Room roomOne, CompassPoint compassPointOne, Room roomTwo, CompassPoint compassPointTwo)
        {
            var vertexOne = new Vertex(roomOne.PortAt(compassPointOne));
            var vertexTwo = new Vertex(roomTwo.PortAt(compassPointTwo));
            var connection = new Connection(Project.Current, vertexOne, vertexTwo);
            Project.Current.Elements.Add(connection);
            return connection;
        }

        /// <summary>
        /// Get a room which can be found in the given direction from the given room.
        /// </summary>
        /// <param name="room">The initial room.</param>
        /// <param name="compassPoint">The direction to consider.</param>
        /// <returns>The room which can be found in that direction, or null if none.</returns>
        /// <remarks>
        /// If no room can be found exactly in that direction, then consider the directions
        /// either side. For example, after checking east and finding nothing, check
        /// east-north-east and east-south-east.
        /// </remarks>
        private Room GetRoomInApproximateDirectionFromRoom(Room room, CompassPoint compassPoint)
        {
            var nextRoom = GetRoomInExactDirectionFromRoom(room, compassPoint);
            if (nextRoom == null)
            {
                nextRoom = GetRoomInExactDirectionFromRoom(room, CompassPointHelper.RotateAntiClockwise(compassPoint));
            }
            if (nextRoom == null)
            {
                nextRoom = GetRoomInExactDirectionFromRoom(room, CompassPointHelper.RotateClockwise(compassPoint));
            }
            return nextRoom;
        }

        /// <summary>
        /// Get a room which can be found in the given direction from the given room.
        /// </summary>
        /// <param name="room">The initial room.</param>
        /// <param name="compassPoint">The direction to consider.</param>
        /// <returns>The room which can be found in that direction, or null if none.</returns>
        private Room GetRoomInExactDirectionFromRoom(Room room, CompassPoint compassPoint)
        {
            var connections = room.GetConnections(compassPoint);
            foreach (var connection in connections)
            {
                foreach (var vertex in connection.VertexList)
                {
                    var port = vertex.Port;
                    if (port != null && port.Owner != room && port.Owner is Room)
                    {
                        return (Room)port.Owner;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Ensure the given element is visible, without changing the zoom factor.
        /// </summary>
        /// <param name="element">The element to make visible.</param>
        private void EnsureVisible(Element element)
        {
            Rect rect = Rect.Empty;
            rect = element.UnionBoundsWith(rect, false);
            if (rect != Rect.Empty)
            {
                Origin = rect.Center;
            }
        }

        /// <summary>
        /// Ensure the given point is visible.
        /// </summary>
        /// <param name="canvasPos">The canvas position to make visible.</param>
        private void EnsureVisible(Vector canvasPos)
        {
            var topLeft = ClientToCanvas(PointF.Empty);
            var bottomRight = ClientToCanvas(new PointF(Width, Height));
            var dx = 0.0f;
            var dy = 0.0f;
            if (canvasPos.X < topLeft.X)
            {
                dx -= topLeft.X - canvasPos.X;
            }
            if (canvasPos.Y < topLeft.Y)
            {
                dy -= topLeft.Y - canvasPos.Y;
            }
            if (canvasPos.X > bottomRight.X)
            {
                dx += canvasPos.X - bottomRight.X;
            }
            if (canvasPos.Y > bottomRight.Y)
            {
                dy += canvasPos.Y - bottomRight.Y;
            }
            if (dx != 0 || dy != 0)
            {
                var origin = Origin;
                Origin = new Vector(origin.X + dx, origin.Y + dy);
            }
        }

        private void BeginDragPan(PointF clientPos, Vector canvasPos)
        {
            DragMode = DragModes.Pan;
            m_panPosition = clientPos;
            Cursor = Cursors.NoMove2D;
            Capture = true;
        }

        private void BeginDragMove(PointF clientPos, Vector canvasPos)
        {
            if (HoverHandle != null)
            {
                DragMode = DragModes.MoveResizeHandle;
                m_dragResizeHandleLastPosition = canvasPos; // unsnapped
                Capture = true;
            }
            else if (HoverPort != null)
            {
                if (HoverPort is MoveablePort)
                {
                    m_dragMovePort = (MoveablePort)HoverPort;
                    m_dragOffsetCanvas = Settings.Snap(canvasPos - HoverPort.Position);
                    DragMode = DragModes.MovePort;
                    Capture = true;
                }
            }
            else
            {
                var hitElement = HitTestElement(canvasPos, false);

                bool alreadySelected = m_selectedElements.Contains(hitElement);
                if (!alreadySelected && (Control.ModifierKeys & (Keys.Control|Keys.Shift)) == Keys.None)
                {
                    // if clicking on empty space, or an unselected element, without holding Control/Shift, clear the current selection.
                    m_selectedElements.Clear();
                }
                else if (hitElement != null)
                {
                    // if clicking on a selected element, remove it from the selection;
                    // we do this since element's size handles etc. belong to the SelectedElement
                    // which is the last one in the list, so we want to add this one back to the end
                    // of the list as the user seems more interested in it and may want handles.
                    m_selectedElements.Remove(hitElement);
                }
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    if (!alreadySelected && hitElement != null)
                    {
                        // if we're holding shift and we clicked an element which wasn't already selected, select it.
                        m_selectedElements.Add(hitElement);
                    }
                }
                else if (hitElement != null)
                {
                    // if we're not holding shift, ensure the current element is selected.
                    // we're safe to re-add it since it will definitely have been removed already
                    // if it was selected, by the above logic.
                    m_selectedElements.Add(hitElement);
                }

                // now we've finished messing with the set of selected elements,
                // update handles, ports, and take defaults for new elements from the most recently selected element.
                UpdateSelection();

                if (hitElement != null && m_selectedElements.Contains(hitElement))
                {
                    // if we ended up with the hit element being selected, initiate a drag move.
                    DragMode = DragModes.MoveElement;
                    canvasPos = Settings.Snap(canvasPos);
                    m_dragOffsetCanvas = canvasPos;
                    Capture = true;
                }
                else if (hitElement == null)
                {
                    // if we didn't hit anything at all, begin a new marquee selection.
                    DragMode = DragModes.Marquee;
                    m_dragOffsetCanvas = canvasPos;
                    m_dragMarqueeLastPosition = canvasPos;
                    Capture = true;
                }
            }
            Invalidate();
        }

        private void BeginDragDrawLine()
        {
            DragMode = DragModes.DrawLine;
            Capture = true;
        }

        private void BeginDrawConnection(PointF clientPos, Vector canvasPos)
        {
            Connection connection;
            HoverPort = HitTestPort(canvasPos);
            if (HoverPort != null && !(HoverPort is MoveablePort))
            {
                // Only from non-moveable ports, until we fix docking.
                // See also DoDragMovePort().
                connection = new Connection(Project.Current, new Vertex(HoverPort), new Vertex(HoverPort));
            }
            else
            {
                var pos = Settings.Snap(canvasPos);
                connection = new Connection(Project.Current, new Vertex(pos), new Vertex(pos));
            }
            connection.Style = NewConnectionStyle;
            connection.Flow = NewConnectionFlow;
            connection.SetText(NewConnectionLabel);
            Project.Current.Elements.Add(connection);
            SelectedElement = connection;
            m_dragMovePort = (MoveablePort)connection.Ports[1];
            m_dragOffsetCanvas = Settings.Snap(canvasPos - connection.VertexList[0].Position);
            HoverPort = null;
            DragMode = DragModes.MovePort;
            Capture = true;
        }

        public void AddRoom(bool atCursor)
        {
            var room = new Room(Project.Current);
            Vector pos;
            if (atCursor && ClientRectangle.Contains(PointToClient(Control.MousePosition)))
            {
                // center on the mouse cursor
                pos = ClientToCanvas(PointToClient(Control.MousePosition));
            }
            else
            {
                // center on the origin
                pos = new Vector(Origin.X - room.Size.X / 2, Origin.Y - room.Size.Y / 2);
            }
            
            // rooms' origins are in the top left corner
            pos -= room.Size / 2;

            // snap to the grid, if required
            pos = Settings.Snap(pos);

            bool clash = true;
            while (clash)
            {
                clash = false;
                foreach (var element in Project.Current.Elements)
                {
                    if (element is IMoveable && ((IMoveable)element).Position == pos)
                    {
                        pos.X += Math.Max(2, Settings.GridSize);
                        pos.Y += Math.Max(2, Settings.GridSize);
                        clash = true;
                    }
                }
            }
            room.Position = pos;
            Project.Current.Elements.Add(room);
            SelectedElement = room;
        }

        private void DoDragPan(PointF clientPos, Vector canvasPos)
        {
            var delta = Drawing.Subtract(m_panPosition, clientPos);
            delta = Drawing.Divide(delta, ZoomFactor);
            Origin = new Vector(Origin.X + delta.X, Origin.Y + delta.Y);
            m_panPosition = clientPos;
        }

        private void DoDragMoveElement(PointF clientPos, Vector canvasPos)
        {
            canvasPos = Settings.Snap(canvasPos);
            foreach (var element in m_selectedElements)
            {
                MoveElementBy(element, canvasPos - m_dragOffsetCanvas);
            }
            m_dragOffsetCanvas = canvasPos;
        }

        private void MoveElementBy(Element element, Vector delta)
        {
            if (element is IMoveable)
            {
                // move any selected moveable elements
                var moveable = (IMoveable)element;
                moveable.Position += delta;
            }

            if (element is Connection)
            {
                // move any free floating points on selected connections
                var connection = (Connection)element;
                foreach (var vertex in connection.VertexList)
                {
                    if (vertex.Port == null)
                    {
                        vertex.Position += delta;
                    }
                }
            }
        }

        private void DoDragMoveResizeHandle(PointF clientPos, Vector canvasPos)
        {
            // the mouse has moved this much on the canvas since we last successfully resized the element
            var delta = canvasPos - m_dragResizeHandleLastPosition;

            if (HoverHandle != null)
            {
                // work out to whether and where we'd like to move the element's corner/edge
                var newPosition = HoverHandle.OwnerPosition + delta;
                if (newPosition != HoverHandle.OwnerPosition)
                {
                    // we'd like to move the element's corner/edge;
                    // try to do so
                    var oldPosition = HoverHandle.OwnerPosition;
                    HoverHandle.OwnerPosition = Settings.Snap(newPosition);

                    // *NOTE 1: *IN THEORY* you'd imagine we could just set the corner/edge position to
                    // a grid-snapped version of the mouse position on the canvas. This would work if
                    // our handles were directly over the corner/edge we're resizing, but they may not
                    // be since we may want to display both resize handles and "draw a new connection"
                    // ports for a corner/edge and so move the resize handles outward so both will fit.
                    // Hence this mucking about with delta values instead.
                    //
                    // *NOTE 2: That said, *IN THEORY* you'd imagine we would just set m_dragResizeHandleLastPosition
                    // to canvasPos here, regardless of whether we actually resized the element.
                    // This is true but for a couple of subtle issues:
                    //
                    // i) Elements have a minimum size (even if it's a width and height of 0). If we're
                    // dragging the buttom right corner of an element up/left, we want the element to stop
                    // at said minimum size, and this is handled by the ResizeHandle. However, our mouse
                    // cursor may keep moving up/left in the meantime; when it eventually moves down/right
                    // again, we want the element NOT to resize until the mouse cursor is actually over a
                    // position such that if we moved the element's corner/edge that way it would grow in size.
                    // (Try resizing a window in Windows and see what I mean.) We achieve this by not changing
                    // m_dragResizeHandleLastPosition unless we actually effect a change.
                    //
                    // ii) Snap to grid. If we just set the last position to the canvas mouse position, then
                    // when resizing you'll observe that the mouse cursor "desyncs" with the element's
                    // corner edge the larger we make the element. This is because of accumulated error
                    // in m_dragResizeHandleLastPosition due to the snap. An easy way to resolve this is
                    // to apply the delta by which we actually resized the element instead of using
                    // the canvas mouse position.
                    if (HoverHandle.OwnerPosition.X != oldPosition.X)
                    {
                        // we managed to move the element's corner/edge horizontally, on the X axis;
                        // on this axis, apply the effective delta to our the basis for future movement.
                        m_dragResizeHandleLastPosition.X += HoverHandle.OwnerPosition.X - oldPosition.X;
                    }
                    if (HoverHandle.OwnerPosition.Y != oldPosition.Y)
                    {
                        // likewise for the vertical/Y axis
                        m_dragResizeHandleLastPosition.Y += HoverHandle.OwnerPosition.Y - oldPosition.Y;
                    }
                }
            }
        }

        private void DoDragMovePort(PointF clientPos, Vector canvasPos)
        {
            if (HoverPort != null && HoverPort != m_dragMovePort)
            {
                if (m_dragMovePort.DockedAt != HoverPort && (!(HoverPort is MoveablePort) || ((MoveablePort)HoverPort).DockedAt != m_dragMovePort))
                {

                    // TODO: Docking disabled until a decent mechanism is worked out.
                    // Currently can cause infinite loops which can crash the program;
                    // eg. dock A to B, B to C, C to A.
                    // Also after docking A to B, moving B brings A but moving A doesn't bring B.
                    // Perhaps some form of "weld" to make the vertices actually the same?
                    // And a corresponding "unweld" option?
                    // Or simply detect circular references and refuse to make them,
                    // always move them all together, etc.

                    // m_dragMovePort.DockAt(HoverPort);

                    // Until docking re-enabled, treat as positional move;
                    // but do dock to rooms etc. as that's safe.
                    // See also BeginDrawConnection().
                    if (!(HoverPort is MoveablePort))
                    {
                        m_dragMovePort.DockAt(HoverPort);
                    }
                    else
                    {
                        canvasPos = Settings.Snap(canvasPos);
                        m_dragMovePort.SetPosition(canvasPos - m_dragOffsetCanvas);
                    }
                }
                else
                {
                    // leave docking alone, and don't snap to grid
                }
            }
            else
            {
                canvasPos = Settings.Snap(canvasPos);
                m_dragMovePort.SetPosition(canvasPos - m_dragOffsetCanvas);
            }
        }

        private void EndDrag()
        {
            if (DragMode == DragModes.MovePort)
            {
                // clear the selection now the line is drawn
                SelectedElement = null;

                if (m_dragMovePort.Owner is Connection)
                {
                    // remove dead connections
                    var connection = (Connection)m_dragMovePort.Owner;
                    bool same = true;
                    if (connection.VertexList.Count > 0)
                    {
                        var pos = connection.VertexList[0].Position;
                        foreach (Vertex v in connection.VertexList)
                        {
                            if (v.Port != null && v.Port.Owner is Room)
                            {
                                // keep connections attached to rooms;
                                // if they don't go anywhere, they
                                // go back to the room, which is significant.
                                same = false;
                            }

                            var distance = v.Position.Distance(pos);
                            if (distance > Numeric.Small)
                            {
                                // keep connections which visibly go anywhere
                                same = false;
                            }
                        }
                    }
                    if (same)
                    {
                        // remove connections which don't go anywhere useful
                        Project.Current.Elements.Remove(connection);
                    }
                }
            }
            else if (DragMode == DragModes.Marquee)
            {
                var marqueeRect = GetMarqueeCanvasBounds();
                if ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) == Keys.None)
                {
                    m_selectedElements.Clear();
                }
                foreach (var element in HitTest(marqueeRect, false))
                {
                    if (!m_selectedElements.Contains(element))
                    {
                        m_selectedElements.Add(element);
                    }
                    else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        if (m_selectedElements.Contains(element))
                        {
                            m_selectedElements.Remove(element);
                        }
                    }
                }
                UpdateSelection();
            }
            DragMode = DragModes.None;
            HoverHandle = null;
            Capture = false;
            Cursor = null;
            Invalidate();
        }

        private List<Element> HitTest(Rect rect, bool roomsOnly)
        {
            var list = new List<Element>();
            foreach (var element in Project.Current.Elements)
            {
                if ((!roomsOnly || element is Room) && element.Intersects(rect))
                {
                    list.Add(element);
                }
            }
            return list;
        }

        private Rect GetMarqueeCanvasBounds()
        {
            if (DragMode != DragModes.Marquee)
            {
                return Rect.Empty;
            }
            var topLeft = m_dragOffsetCanvas;
            var bottomRight = ClientToCanvas(PointToClient(Control.MousePosition));
            if (bottomRight.X < topLeft.X)
            {
                Numeric.Swap(ref bottomRight.X, ref topLeft.X);
            }
            if (bottomRight.Y < topLeft.Y)
            {
                Numeric.Swap(ref bottomRight.Y, ref topLeft.Y);
            }
            return new Rect(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
        }

        private float SnapToElementSizeAtCurrentZoomFactor
        {
            get { return Settings.SnapToElementSize; }
        }

        private Element HitTestElement(Vector canvasPos, bool includeMargins)
        {
            List<Element> closest = new List<Element>();
            float closestDistance = float.MaxValue;
            foreach (var element in DepthSortElements()) // sort into drawing order
            {
                if (DragMode == DragModes.MovePort && m_dragMovePort.Owner == element)
                {
                    // when moving a port on an element, don't try to dock it to that element itself
                    continue;
                }

                float distance = element.Distance(canvasPos, includeMargins);
                if (distance <= SnapToElementSizeAtCurrentZoomFactor)
                {
                    if (Numeric.ApproxEqual(distance, closestDistance))
                    {
                        closest.Add(element);
                    }
                    else if (distance < closestDistance)
                    {
                        closest.Clear();
                        closest.Add(element);
                        closestDistance = distance;
                    }
                }
            }

            if (closest.Count == 0)
            {
                return null;
            }
            return closest[closest.Count - 1]; // choose the topmost element
        }

        private ResizeHandle HitTestHandle(Vector canvasPos)
        {
            // examine handles, topmost (drawn) to lowermost
            for (int index = m_handles.Count - 1; index >= 0; --index)
            {
                var handle = m_handles[index];
                if (handle.HitTest(canvasPos))
                {
                    return handle;
                }
            }
            return null;
        }

        private Port HitTestPort(Vector canvasPos)
        {
            Port closest = null;
            float closestDistance = float.MaxValue;

            foreach (var port in m_ports)
            {
                if (DragMode == DragModes.MovePort && port == m_dragMovePort)
                {
                    // when dragging a port, don't try to dock it with itself
                    continue;
                }

                float distance = port.Distance(canvasPos);

                var snapDistance = SnapToElementSizeAtCurrentZoomFactor;

                var bounds = port.Owner.UnionBoundsWith(Rect.Empty, true);
                if (bounds.Contains(canvasPos))
                {
                    if (DragMode == DragModes.MovePort)
                    {
                        // if we're dragging a line to set its end point and are over a room, ALWAYS snap to ports:
                        // we do this to avoid the user accidentally making a connection that "nearly" goes to a room.
                        snapDistance = float.MaxValue;
                    }
                    else
                    {
                        // if we'starting a new line and are over a room, NEVER snap to ports:
                        // we do this if hit testing inside so we can more easily select small rooms and not their ports
                        snapDistance = 0;
                    }
                }

                if (distance <= snapDistance && distance < closestDistance)
                {
                    closest = port;
                    closestDistance = distance;
                }
            }
            return closest;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Element SelectedElement
        {
            get { return m_selectedElements.Count > 0 ? m_selectedElements[m_selectedElements.Count - 1] : null; }
            set
            {
                var selectedElement = m_selectedElements.Count > 0 ? m_selectedElements[m_selectedElements.Count - 1] : null;
                if (selectedElement != value)
                {
                    m_selectedElements.Clear();
                    if (value != null)
                    {
                        m_selectedElements.Add(value);
                    }
                    UpdateSelection();
                }
            }
        }

        public int SelectedElementCount
        {
            get { return m_selectedElements.Count; }
        }

        public bool HasSingleSelectedElement
        {
            get { return SelectedElementCount == 1; }
        }

        public bool HasSelectedElement<T>() where T : Element
        {
            foreach (var element in m_selectedElements)
            {
                if (element is T)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Element> SelectedElements
        {
            get { return m_selectedElements; }
        }

        void UpdateSelection()
        {
            RecreateHandles();
            RecreatePorts();
            // only if we have a single element selected;
            // otherwise selecting multiple items will cause one to override the others' settings!
            var selectedElement = SelectedElement;
            if (selectedElement is Connection)
            {
                SetConnectionDefaultsFrom((Connection)selectedElement);
            }
            else if (selectedElement is Room)
            {
                SetRoomDefaultsFrom((Room)selectedElement);
            }
            Invalidate();
        }

        public void SelectAll()
        {
            m_selectedElements.Clear();
            m_selectedElements.AddRange(Project.Current.Elements);
            UpdateSelection();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private ResizeHandle HoverHandle
        {
            get { return m_hoverHandle; }
            set
            {
                if (m_hoverHandle != value)
                {
                    m_hoverHandle = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private Port HoverPort
        {
            get { return m_hoverPort; }
            set
            {
                if (m_hoverPort != value)
                {
                    m_hoverPort = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Element HoverElement
        {
            get
            {
                return m_hoverElement;
            }
            set
            {
                if (m_hoverElement != value)
                {
                    m_hoverElement = value;
                    RecreatePorts();
                }
            }
        }

        private void RecreateHandles()
        {
            HoverHandle = null;
            m_handles.Clear();
            var element = SelectedElement;
            if (CanSelectElements && element is ISizeable && HasSingleSelectedElement)
            {
                var sizeable = (ISizeable)element;
                m_handles.Add(new ResizeHandle(CompassPoint.North, sizeable));
                m_handles.Add(new ResizeHandle(CompassPoint.South, sizeable));
                m_handles.Add(new ResizeHandle(CompassPoint.East, sizeable));
                m_handles.Add(new ResizeHandle(CompassPoint.West, sizeable));
                m_handles.Add(new ResizeHandle(CompassPoint.NorthWest, sizeable));
                m_handles.Add(new ResizeHandle(CompassPoint.NorthEast, sizeable));
                m_handles.Add(new ResizeHandle(CompassPoint.SouthWest, sizeable));
                m_handles.Add(new ResizeHandle(CompassPoint.SouthEast, sizeable));
            }

            Invalidate();
        }

        private void RecreatePorts()
        {
            HoverPort = null;
            m_ports.Clear();

            // decide if we want ports on the element under the mouse cursor; if so, add them
            if (HoverElement is Room && !m_selectedElements.Contains(HoverElement))
            {
                // we're hovering over a non-selected room;
                // (we don't show ports on selected rooms since they get handles already and it's too confusing;
                // we don't show ports on lines we're hovering over, since we don't allow line-line connections
                // right now as the algorithm's borked somewhere and can end up with nastiness)
                if (DragMode == DragModes.MovePort || (CanDrawLine && SelectedElement == null))
                {
                    // when we're either:
                    //    i) currently dragging a new or existing line's end point, OR
                    //   ii) can draw a new line, and haven't got anything selelected
                    // then we're safe to show ports on the room we're overing over
                    // from which new connections can be drawn.
                    // We're trying to avoid visual clashes between (stacking of), and
                    // associated selection problems with, a selected connection and the
                    // ports of the room joined to one end of the connection.
                    // Rather than special casing this which might confuse the user as to
                    // when they can draw lines and when they can't we make the general rule
                    // that if you've got something selected you can't draw a line.
                    m_ports.AddRange(HoverElement.Ports);
                }
            }

            // decide if we want movable ports on the selected element; if so, add them
            // (currently movable ports only apply to connections, and if we want to be able
            // to move a connection we must show them.)
            var needMovablePortsOnSelectedElement = CanSelectElements;
            if (needMovablePortsOnSelectedElement && HasSingleSelectedElement)
            {
                foreach (var port in SelectedElement.Ports)
                {
                    if (port is MoveablePort)
                    {
                        m_ports.Add(port);
                    }
                }
            }

            Invalidate();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private DragModes DragMode
        {
            get { return m_dragMode; }
            set
            {
                m_dragMode = value;
                RecreatePorts();
            }
        }

        public bool CanSelectElements
        {
            get { return true; }
        }

        public bool CanDrawLine
        {
            get { return true; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ConnectionStyle NewConnectionStyle
        {
            get { return m_newConnectionStyle; }
            set
            {
                if (m_newConnectionStyle != value)
                {
                    m_newConnectionStyle = value;
                    RaiseNewConnectionStyleChanged();
                }
            }
        }

        public void ApplyConnectionStyle(ConnectionStyle connectionStyle)
        {
            foreach (var element in m_selectedElements)
            {
                if (element is Connection)
                {
                    var connection = (Connection)element;
                    connection.Style = connectionStyle;
                }
            }
            Invalidate();
        }

        public event EventHandler NewConnectionStyleChanged;

        private void RaiseNewConnectionStyleChanged()
        {
            var changed = NewConnectionStyleChanged;
            if (changed != null)
            {
                changed(this, EventArgs.Empty);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ConnectionFlow NewConnectionFlow
        {
            get { return m_newConnectionFlow; }
            set 
            {
                if (m_newConnectionFlow != value)
                {
                    m_newConnectionFlow = value;
                    RaiseNewConnectionFlowChanged();
                }
            }
        }

        public void ApplyConnectionFlow(ConnectionFlow connectionFlow)
        {
            foreach (var element in m_selectedElements)
            {
                if (element is Connection)
                {
                    var connection = (Connection)element;
                    connection.Flow = connectionFlow;
                }
            }
            Invalidate();
        }

        public event EventHandler NewConnectionFlowChanged;

        private void RaiseNewConnectionFlowChanged()
        {
            var changed = NewConnectionFlowChanged;
            if (changed != null)
            {
                changed(this, EventArgs.Empty);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ConnectionLabel NewConnectionLabel
        {
            get { return m_newConnectionLabel; }
            set
            {
                if (m_newConnectionLabel != value)
                {
                    m_newConnectionLabel = value;
                    RaiseNewConnectionLabelChanged();
                }
            }
        }

        public void ApplyConnectionLabel(ConnectionLabel connectionLabel)
        {
            foreach (var element in m_selectedElements)
            {
                if (element is Connection)
                {
                    var connection = (Connection)element;
                    connection.SetText(connectionLabel);
                }
            }
            Invalidate();
        }

        public event EventHandler NewConnectionLabelChanged;

        private void RaiseNewConnectionLabelChanged()
        {
            var changed = NewConnectionLabelChanged;
            if (changed != null)
            {
                changed(this, EventArgs.Empty);
            }
        }

        public override Cursor Cursor
        {
            get
            {
                if (CanDrawLine && ((HoverPort != null && !(HoverPort is MoveablePort)) || DragMode == DragModes.MovePort))
                {
                    return Drawing.DrawLineCursor;
                }

                if (HoverPort != null && HoverPort is MoveablePort)
                {
                    return Drawing.MoveLineCursor;
                }

                if (HoverHandle != null)
                {
                    var cursor = HoverHandle.Cursor;
                    if (cursor != null)
                    {
                        return cursor;
                    }
                }

                if (HoverElement != null && HoverElement is IMoveable && m_selectedElements.Contains(HoverElement))
                {
                    return Cursors.SizeAll;
                }
                return base.Cursor;
            }
            set
            {
                base.Cursor = value;
            }
        }

        public void ResetZoomOrigin()
        {
            Origin = ComputeCanvasBounds(false).Center;
            ZoomFactor = 1.0f;
        }

        public void ZoomIn()
        {
            if (ZoomFactor < 100.0f)
            {
                ZoomFactor *= 1.25f;
            }
        }

        public void ZoomOut()
        {
            if (ZoomFactor > 1 / 100.0f)
            {
                ZoomFactor /= 1.25f;
            }
        }

        public void ZoomToFit()
        {
            ResetZoomOrigin();
            var canvasBounds = ComputeCanvasBounds(false);
            while (!Viewport.Contains(canvasBounds))
            {
                ZoomOut();

                // Added an escape clause in the case the map is too large to shrink to the screen
                if (ZoomFactor <= 1 / 100.0f)
                {
                    return;
                }
            }
        }

        private void SetRoomDefaultsFrom(Room room)
        {
            m_newRoomSize = room.Size;
            m_newRoomIsDark = room.IsDark;
            m_newRoomObjectsPosition = room.ObjectsPosition;
        }

        private void SetConnectionDefaultsFrom(Connection connection)
        {
            NewConnectionFlow = connection.Flow;
            NewConnectionStyle = connection.Style;
        }

        public void ReverseLineDirection()
        {
            foreach (var element in m_selectedElements)
            {
                if (element is Connection)
                {
                    var connection = (Connection)element;
                    connection.Reverse();
                }
            }
        }

        public void DeleteSelection()
        {
            var doomedElements = new List<Element>(m_selectedElements);
            foreach (var element in doomedElements)
            {
                Project.Current.Elements.Remove(element);
            }
            m_selectedElements.Clear();
            UpdateSelection();
        }

        public void ApplyNewPlainConnectionSettings()
        {
            // apply sequentially as each change will affect our defaults,
            // so setting the style will cause us to take the existing flow and label, etc.
            NewConnectionStyle = ConnectionStyle.Solid;
            ApplyConnectionStyle(NewConnectionStyle);

            NewConnectionFlow = ConnectionFlow.TwoWay;
            ApplyConnectionFlow(NewConnectionFlow);

            NewConnectionLabel = ConnectionLabel.None;
            ApplyConnectionLabel(NewConnectionLabel);
        }

        public void UpdateScrollBars()
        {
            m_updatingScrollBars = true;

            var topLeft = PointF.Empty;
            var displaySize = new PointF(Math.Max(0, Width - m_vScrollBar.Width), Math.Max(0,Height - m_hScrollBar.Height));

            Rect clientBounds;
            if (Project.Current.Elements.Count > 0)
            {
                Rect canvasBounds = Rect.Empty;
                foreach (var element in Project.Current.Elements)
                {
                    canvasBounds = element.UnionBoundsWith(canvasBounds, true);
                }

                var tl = CanvasToClient(canvasBounds.Position);
                var br = CanvasToClient(canvasBounds.GetCorner(CompassPoint.SouthEast));
                clientBounds = new Rect(tl.X, tl.Y, br.X - tl.X, br.Y - tl.Y);
            }
            else
            {
                // if there's nothing on the canvas, don't include the origin (0,0) as a "thing" to scroll to
                clientBounds = new Rect(topLeft.X, topLeft.Y, displaySize.X, displaySize.Y);
            }

            if (!Settings.InfiniteScrollBounds && topLeft.Y <= clientBounds.Top && topLeft.Y + displaySize.Y >= clientBounds.Bottom)
            {
                m_vScrollBar.Enabled = false;
            }
            else
            {
                m_vScrollBar.Enabled = true;
                m_vScrollBar.Minimum = (int)Math.Min(topLeft.Y, clientBounds.Top);
                m_vScrollBar.Maximum = (int)Math.Max(topLeft.Y + displaySize.Y, clientBounds.Bottom) - 1; // -1 since Maximum is actually maximum value + 1; see MSDN.
                m_vScrollBar.Value = (int)Math.Max(m_vScrollBar.Minimum, Math.Min(m_vScrollBar.Maximum, topLeft.Y));
                m_vScrollBar.LargeChange = (int)displaySize.Y;
                m_vScrollBar.SmallChange = (int)(displaySize.Y / 10);
            }

            if (!Settings.InfiniteScrollBounds && topLeft.X <= clientBounds.Left && topLeft.X + displaySize.X >= clientBounds.Right)
            {
                m_hScrollBar.Enabled = false;
            }
            else
            {
                m_hScrollBar.Enabled = true;
                m_hScrollBar.Minimum = (int)Math.Min(topLeft.X, clientBounds.Left);
                m_hScrollBar.Maximum = (int)Math.Max(topLeft.X + displaySize.X, clientBounds.Right) - 1; // -1 since Maximum is actually maximum value + 1; see MSDN.
                m_hScrollBar.Value = (int)Math.Max(m_hScrollBar.Minimum, Math.Min(m_hScrollBar.Maximum, topLeft.X));
                m_hScrollBar.LargeChange = (int)displaySize.X;
                m_hScrollBar.SmallChange = (int)(displaySize.X / 10);
            }

            m_updatingScrollBars = false;
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (m_updatingScrollBars)
            {
                return;
            }

            // the scroll bar will Invalidate() and Update() us; avoid exceptions
            m_doNotUpdateScrollBarsNextPaint = true;

            var clientDelta = e.NewValue - e.OldValue;
            if (Settings.InfiniteScrollBounds && e.Type == ScrollEventType.SmallIncrement || e.Type == ScrollEventType.SmallDecrement)
            {
                // since our canvas is infinite, allow the user to use the
                // scroll bar arrows to keep scrolling past the bounds.
                if (Math.Abs(clientDelta) != m_vScrollBar.SmallChange)
                {
                    clientDelta = m_vScrollBar.SmallChange * (e.Type == ScrollEventType.SmallIncrement ? 1 : -1);
                }
            }
            if (clientDelta != 0)
            {
                if (sender == m_vScrollBar)
                {
                    Origin += new Vector(ClientToCanvas(new SizeF(0, clientDelta)));
                }
                else
                {
                    Origin += new Vector(ClientToCanvas(new SizeF(clientDelta, 0)));
                }
            }
        }

        public bool MinimapVisible
        {
            get { return m_minimap.Visible; }
            set
            {
                m_minimap.Visible = value;
                if (!m_minimap.Visible)
                {
                    m_vScrollBar.Top = 0;
                    m_vScrollBar.Height = Height - m_cornerPanel.Height;
                }
                else
                {
                    m_vScrollBar.Top = m_minimap.Bottom;
                    m_vScrollBar.Height = Height - m_cornerPanel.Height - m_minimap.Height;
                }
            }
        }

        private enum DragModes
        {
            None,
            Pan,
            MoveElement,
            MoveResizeHandle,
            MovePort,
            Marquee,
            DrawLine,
        }

        private void Reset()
        {
            ZoomFactor = 1;
            Origin = Vector.Zero;
            SelectedElement = null;
            HoverElement = null;
            HoverHandle = null;
            HoverPort = null;
            DragMode = DragModes.None;
            NewConnectionStyle = ConnectionStyle.Solid;
            NewConnectionFlow = ConnectionFlow.TwoWay;
            m_newRoomSize = new Vector(Settings.GridSize * 3, Settings.GridSize * 2);
            m_newRoomIsDark = false;
            m_newRoomObjectsPosition = CompassPoint.South;
            RequestRecomputeSmartSegments();
            StopAutomapping();
        }

        private float m_zoomFactor;
        private Vector m_origin;

        private List<Element> m_selectedElements = new List<Element>();
        private Element m_hoverElement;
        private ResizeHandle m_hoverHandle;
        private Port m_hoverPort;

        private Point m_lastKnownMousePosition;
        private Point m_lastMouseDownPosition;
        private DragModes m_dragMode;
        private PointF m_panPosition;
        private Vector m_dragOffsetCanvas;
        private MoveablePort m_dragMovePort;
        private Vector m_dragMarqueeLastPosition;
        private Vector m_dragResizeHandleLastPosition;

        private List<ResizeHandle> m_handles = new List<ResizeHandle>();
        private List<Port> m_ports = new List<Port>();

        private ConnectionStyle m_newConnectionStyle;
        private ConnectionFlow m_newConnectionFlow;
        private ConnectionLabel m_newConnectionLabel;
        private Vector m_newRoomSize;
        private bool m_newRoomIsDark;
        private CompassPoint m_newRoomObjectsPosition;
        private System.Threading.Timer m_recomputeTimer;
        private bool m_updatingScrollBars;
        private bool m_doNotUpdateScrollBarsNextPaint;

        private static readonly int RecomputeNMillisecondsAfterChange = 500;
        private static bool m_smartLineSegmentsUpToDate = false;
    }
}
