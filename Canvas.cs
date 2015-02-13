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
using System.Globalization;
using System.Linq;
using DevComponents.DotNetBar;
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

            PreviewKeyDown += onPreviewKeyDown;

            mRecomputeTimer = new System.Threading.Timer(onRecomputeTimerTick);

            Project.ProjectChanged += onProjectChanged;
            onProjectChanged(this, new ProjectChangedEventArgs(null, Project.Current));

            Settings.Changed += onSettingsChanged;
            onSettingsChanged(this, EventArgs.Empty);

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

        private void requestRecomputeSmartSegments()
        {
            mSmartLineSegmentsUpToDate = false;
            mRecomputeTimer.Change(RecomputeNMillisecondsAfterChange, RecomputeNMillisecondsAfterChange);
        }

        private void onRecomputeTimerTick(object state)
        {
            mRecomputeTimer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);

            var context = new DrawingContext(ZoomFactor);
            var elements = depthSortElements();

            foreach (var element in elements)
            {
                element.PreDraw(context);
            }

            foreach (var element in elements)
            {
                element.RecomputeSmartLineSegments(context);
            }

            mSmartLineSegmentsUpToDate = true;
            Invalidate();
        }

        private void onProjectChanged(object sender, ProjectChangedEventArgs e)
        {
            if (e.OldProject != null)
            {
                e.OldProject.Elements.Added -= onElementAdded;
                e.OldProject.Elements.Removed -= onElementRemoved;

                foreach (var element in e.OldProject.Elements)
                {
                    element.Changed -= onElementChanged;
                }
            }
            if (e.NewProject != null)
            {
                e.NewProject.Elements.Added += onElementAdded;
                e.NewProject.Elements.Removed += onElementRemoved;

                foreach (var element in e.NewProject.Elements)
                {
                    element.Changed += onElementChanged;
                }
            }

            reset();
            ZoomToFit();
        }

        private void onSettingsChanged(object sender, EventArgs e)
        {
            requestRecomputeSmartSegments();
            BackColor = Settings.Color[Colors.Canvas];
            Invalidate();
        }

        private void onElementAdded(object sender, ItemEventArgs<Element> e)
        {
            var item = e.Item as Room;
            if (item != null)
            {
                var room = item;
                room.Size = mNewRoomSize;
                room.IsDark = mNewRoomIsDark;
                room.ObjectsPosition = mNewRoomObjectsPosition;
            }
            e.Item.Changed += onElementChanged;
            Project.Current.IsDirty = true;
            requestRecomputeSmartSegments();
            Invalidate();
        }

        private void onElementRemoved(object sender, ItemEventArgs<Element> e)
        {
            mSelectedElements.Remove(e.Item);
            updateSelection();
            endDrag();
            updateDragHover(PointToClient(MousePosition));

            Project.Current.IsDirty = true;
            e.Item.Changed -= onElementChanged;
            requestRecomputeSmartSegments();
            Invalidate();
        }

        private void onElementChanged(object sender, EventArgs e)
        {
            var room = sender as Room;
            if (room != null)
            {
                setRoomDefaultsFrom(room);
            }
            var connection = sender as Connection;
            if (connection != null)
            {
                setConnectionDefaultsFrom(connection);
            }
            Invalidate();
            Project.Current.IsDirty = true;
            requestRecomputeSmartSegments();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float ZoomFactor
        {
            get { return mZoomFactor; }
            set
            {
                if (mZoomFactor != value)
                {
                    mZoomFactor = value;
                    lblZoom.Text = mZoomFactor.ToString("p0");
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Vector Origin
        {
            get { return mOrigin; }
            set
            {
                if (mOrigin != value)
                {
                    mOrigin = value;
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
            if (!mDoNotUpdateScrollBarsNextPaint)
            {
                UpdateScrollBars();
            }
            mDoNotUpdateScrollBarsNextPaint = false;

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
                    drawGrid(graphics, palette);
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

                drawElements(graphics, palette, finalRender);
                if (!finalRender)
                {
                    drawHandles(graphics, palette);
                    drawPorts(graphics, palette);
                    drawMarquee(graphics, palette);
                }

                stopwatch.Stop();
                if (Settings.DebugShowFPS && !finalRender)
                {
                    var fps = 1.0f / (float)(stopwatch.Elapsed.TotalSeconds);
                    graphics.Graphics.Transform = new Matrix();
                    graphics.DrawString(string.Format("{0} ms ({1} fps) {2} rebuilds", stopwatch.Elapsed.TotalMilliseconds, fps, TextBlock.RebuildCount), Settings.LargeFont, Brushes.Red, new PointF(10, 20 + Settings.LargeFont.GetHeight()));
                }
                if (Settings.DebugShowMouseCoordinates && !finalRender)
                {
                    var mouseCoord = MousePosition;
                    graphics.Graphics.Transform = new Matrix();
                    graphics.DrawString(string.Format("X:{0}  Y:{1}", mouseCoord.X, mouseCoord.Y), Settings.LargeFont, Brushes.Green, new PointF(10, 40 + Settings.LargeFont.GetHeight()));
                    if (HoverElement == null)
                        graphics.DrawString(new Point(0, 0).ToString(), Settings.LargeFont, new SolidBrush(Color.YellowGreen), new PointF(10, 60 + Settings.LargeFont.GetHeight()));
                    else
                        graphics.DrawString(PointToScreen(HoverElement.Position.ToPoint()).ToString(), Settings.LargeFont, new SolidBrush(Color.YellowGreen), new PointF(10, 60 + Settings.LargeFont.GetHeight()));

                }
            }

            ZoomFactor = zoomFactor;
            Origin = origin;
        }

        private void drawGrid(XGraphics graphics, Palette palette)
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

        private List<Element> depthSortElements()
        {
            var elements = new List<Element>();
            elements.AddRange(Project.Current.Elements);
            elements.Sort();
            return elements;
        }

        private void drawElements(XGraphics graphics, Palette palette, bool finalRender)
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
            context.UseSmartLineSegments = mSmartLineSegmentsUpToDate;
            var elements = depthSortElements();

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

            foreach (var element in mSelectedElements)
            {
                element.Flagged = true;
            }

            var clipToScreen = new RectangleF(Origin.X - Width / 2 / ZoomFactor, Origin.Y - Height / 2 / ZoomFactor, Width / ZoomFactor, Height / ZoomFactor);

            foreach (var element in elements)
            {
                context.Selected = element.Flagged && !finalRender;
                context.Hover = !context.Selected && element == HoverElement && !finalRender;
                if (context.Hover && dragMode == DragModes.MovePort)
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

        private void drawHandles(XGraphics graphics, Palette palette)
        {
            if (mHandles.Count == 0)
            {
                return;
            }

            var context = new DrawingContext(ZoomFactor);

            if (mHandles.Count > 1)
            {
                var bounds = Rect.Empty;

                foreach (var handle in mHandles)
                {
                    bounds = bounds == Rect.Empty ? new Rect(handle.Position, Vector.Zero) : bounds.Union(handle.Position);
                }
                bounds.X += Settings.HandleSize / 2f;
                bounds.Y += Settings.HandleSize / 2f;
                graphics.DrawRectangle(palette.ResizeBorderPen, bounds.ToRectangleF());
            }
            

            foreach (var handle in mHandles)
            {
                context.Selected = handle == hoverHandle;
                handle.Draw(this, graphics, palette, context);
            }
        }

        private void drawPorts(XGraphics graphics, Palette palette)
        {
            var context = new DrawingContext(ZoomFactor);

            // draw all non-selected ports
            foreach (var port in mPorts)
            {
                if (hoverPort == port)
                {
                    // we'll draw this port last
                    continue;
                }

                context.Selected = false;
                port.Draw(this, graphics, palette, context);
            }

            if (hoverPort != null)
            {
                // lastly, always the port under the mouse, if any
                context.Selected = true;
                hoverPort.Draw(this, graphics, palette, context);
            }
        }

        private void drawMarquee(XGraphics graphics, Palette palette)
        {
            var marqueeRect = getMarqueeCanvasBounds();
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

        private bool isZoomIn(int delta)
        {
            return (!Settings.InvertMouseWheel && delta < 0) || (Settings.InvertMouseWheel && delta > 0);
        }

        private bool isZoomOut(int delta)
        {
            return (!Settings.InvertMouseWheel && delta > 0) || (Settings.InvertMouseWheel && delta < 0);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.X < 0 || e.X > Width || e.Y < 0 || e.Y > Width)
                return;

            var pos = ClientToCanvas(new PointF(e.X, e.Y));

            if (isZoomIn(e.Delta))
            {
                ZoomIn();
            }
            else if (isZoomOut(e.Delta)  && ZoomFactor > 1/100.0f)
            {
                ZoomOut();
            }

            Vector newPos = ClientToCanvas(new PointF(e.X, e.Y));
            Origin = Origin - (newPos - pos);

            Invalidate();
            updateDragHover(e.Location);

            base.OnMouseWheel(e);
        }


        private bool isDragButton(MouseEventArgs e)
        {
            return (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Right && ModifierKeys == Keys.Shift));
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var clientPos = new PointF(e.X, e.Y);
            var canvasPos = ClientToCanvas(clientPos);
            mLastMouseDownPosition = e.Location;

            if (dragMode != DragModes.None)
                return;

            if (isDragButton(e))
            {
                beginDragPan(clientPos, canvasPos);
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (CanSelectElements)
                {
                    beginDragMove(clientPos, canvasPos);
                }
                if (dragMode == DragModes.None)
                {
                    if (hoverPort != null && CanDrawLine)
                    {
                        beginDragDrawLine();
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (CanSelectElements)
                    beginDragMove(clientPos,canvasPos);

            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            endDrag();

            base.OnMouseUp(e);
        }



        protected override void OnMouseMove(MouseEventArgs e)
        {
            // ignore spurious mouse move events
            if (mLastKnownMousePosition == e.Location)
            {
                return;

            }
            mLastKnownMousePosition = e.Location;

            updateDragHover(e.Location);
            base.OnMouseMove(e);
        }

        private void updateDragHover(Point mousePosition)
        {
            mLastKnownMousePosition = mousePosition;

            var clientPos = new PointF(mousePosition.X, mousePosition.Y);
            var canvasPos = ClientToCanvas(clientPos);

            switch (dragMode)
            {
                case DragModes.Pan:
                    doDragPan(clientPos, canvasPos);
                    break;
                case DragModes.MoveElement:
                    doDragMoveElement(clientPos, canvasPos);
                    break;
                case DragModes.MoveResizeHandle:
                    doDragMoveResizeHandle(clientPos, canvasPos);
                    break;
                case DragModes.MovePort:
                    HoverElement = hitTestElement(canvasPos, true);
                    hoverPort = hitTestPort(canvasPos);
                    doDragMovePort(clientPos, canvasPos);
                    break;
                case DragModes.None:
                    hoverHandle = hitTestHandle(canvasPos); // set first; it will RecreatePorts() if the value changes
                    hoverPort = hitTestPort(canvasPos);
                    var hoverElement = hitTestElement(canvasPos, false);
                    bool sameElement = HoverElement == hoverElement;
                    HoverElement = hoverElement;

                        if (HoverElement == null)
                            roomTooltip.HideTooltip();
                        else
                            if (HoverElement.HasTooltip())
                            {
                                {
                                    if ((roomTooltip.IsTooltipVisible) && (sameElement)) return;

                                    var toolTip = new SuperTooltipInfo(HoverElement.GetToolTipHeader(), HoverElement.GetToolTipFooter(), HoverElement.GetToolTipText(), null, null, HoverElement.GetToolTipColor());
                                    roomTooltip.SetSuperTooltip(this, toolTip);
                                    var tPoint = MousePosition;
                                    tPoint.Offset(20, 20);
                                    roomTooltip.ShowTooltip(this, tPoint);
                                }
                            }
                    break;
                case DragModes.DrawLine:
                    if (new Vector(mLastMouseDownPosition).Distance(new Vector(mousePosition)) > Settings.DragDistanceToInitiateNewConnection)
                    {
                        var startPos = new PointF(mLastMouseDownPosition.X, mLastMouseDownPosition.Y);
                        beginDrawConnection(startPos, ClientToCanvas(startPos));
                    }
                    break;
                case DragModes.Marquee:
                    if (mDragMarqueeLastPosition != canvasPos)
                    {
                        mDragMarqueeLastPosition = canvasPos;
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

        void onPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                        var room1 = element as Room;
                        if (room1 != null)
                        {
                            var room = room1;
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
            else if (e.KeyCode == Keys.Up && ModifierKeys == Keys.Control)
            {
                if (!selectRoomRelativeToSelectedRoom(CompassPoint.North)) addOrConnectRoomRelativeToSelectedRoom(CompassPoint.North);
            }
            else if (e.KeyCode == Keys.Down && ModifierKeys == Keys.Control)
            {
                if (!selectRoomRelativeToSelectedRoom(CompassPoint.South)) addOrConnectRoomRelativeToSelectedRoom(CompassPoint.South);
            }
            else if (e.KeyCode == Keys.Left && ModifierKeys == Keys.Control)
            {
                if (!selectRoomRelativeToSelectedRoom(CompassPoint.West)) addOrConnectRoomRelativeToSelectedRoom(CompassPoint.West);
            }
            else if (e.KeyCode == Keys.Right && ModifierKeys == Keys.Control)
            {
                if (!selectRoomRelativeToSelectedRoom(CompassPoint.East)) addOrConnectRoomRelativeToSelectedRoom(CompassPoint.East);
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
                NewConnectionStyle = NewConnectionStyle == ConnectionStyle.Solid ? ConnectionStyle.Dashed : ConnectionStyle.Solid;
                ApplyConnectionStyle(NewConnectionStyle);
            }
            else if (e.KeyCode == Keys.A)
            {
                NewConnectionFlow = NewConnectionFlow == ConnectionFlow.TwoWay ? ConnectionFlow.OneWay : ConnectionFlow.TwoWay;
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
            else if (e.KeyCode == Keys.V && ModifierKeys == Keys.Control)
            {
                Paste(true);
            }
            else if (e.KeyCode == Keys.J)
            {
                var selectedRooms = mSelectedElements.Where(p=>p.GetType() == typeof(Room)).ToList();
                if (selectedRooms.Count() == 2)
                {
                    joinSelectedRooms((Room)selectedRooms[0], (Room)selectedRooms[1]);
                }
            }
            else if (e.KeyCode == Keys.V)
            {
                ReverseLineDirection();
            }
            else if (e.KeyCode == Keys.K)
            {
                foreach (var element in mSelectedElements)
                {
                    var room1 = element as Room;
                    if (room1 != null)
                    {
                        var room = room1;
                        room.IsDark = !room.IsDark;
                    }
                }
            }
            else if (e.KeyCode == Keys.F1 && ModifierKeys == Keys.Control)
            {
                Settings.DebugShowFPS = !Settings.DebugShowFPS;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F1 && ModifierKeys == Keys.Shift)
            {
                Settings.DebugShowMouseCoordinates = !Settings.DebugShowMouseCoordinates;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F2 && ModifierKeys == Keys.Control)
            {
                Settings.DebugDisableElementRendering = !Settings.DebugDisableElementRendering;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F3 && ModifierKeys == Keys.Control)
            {
                Settings.DebugDisableLineRendering = !Settings.DebugDisableLineRendering;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F4 && ModifierKeys == Keys.Control)
            {
                Settings.DebugDisableTextRendering = !Settings.DebugDisableTextRendering;
                Invalidate();
            }
            else if (e.KeyCode == Keys.F5 && ModifierKeys == Keys.Control)
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
                    if (!selectRoomRelativeToSelectedRoom(compassPoint.Value))
                    {
                        if (ModifierKeys == Settings.KeypadNavigationCreationModifier)
                        {
                            addOrConnectRoomRelativeToSelectedRoom(compassPoint.Value);
                        }
                        else if (ModifierKeys == Settings.KeypadNavigationUnexploredModifier)
                        {
                            addUnexploredConnectionToSelectedRoom(compassPoint.Value);
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
        private bool selectRoomRelativeToSelectedRoom(CompassPoint compassPoint)
        {
            var element = SelectedElement as Room;
            if (element != null)
            {
                var room = element;
                var nextRoom = getRoomInApproximateDirectionFromRoom(room, compassPoint);
                if (nextRoom != null)
                {
                    SelectedElement = nextRoom;
                    EnsureVisible(SelectedElement);
                    return true;
                }
            }
            return false;
        }

        private void joinSelectedRooms(Room room1, Room room2)
        {
            var rect1 = room1.InnerBounds;
            var rect2 = room2.InnerBounds;

            var dx = rect1.X - rect2.X;
            var dy = rect1.Y - rect2.Y;

            if (dy == 0 && dx != 0)
            {
                if (dx > 0)
                    addConnection(room1, CompassPoint.West, room2, CompassPoint.East);
                else
                    addConnection(room1, CompassPoint.East, room2, CompassPoint.West);
            }

            else if (dy != 0 && dx == 0)
            {
                if (dy > 0)
                    addConnection(room1, CompassPoint.North, room2, CompassPoint.South);
                else
                    addConnection(room1, CompassPoint.South, room2, CompassPoint.North);
            }

            else
            {
                if (Math.Abs(dy) >= Math.Abs(dx))
                    if (dy > 0)
                        addConnection(room1, CompassPoint.North, room2, CompassPoint.South);
                    else
                        addConnection(room1, CompassPoint.South, room2, CompassPoint.North);
                else
                    if (dx > 0)
                        addConnection(room1, CompassPoint.West, room2, CompassPoint.East);
                    else
                        addConnection(room1, CompassPoint.East, room2, CompassPoint.West);
                    
            }
        }

        /// <summary>
        /// Find a room adjacent to the selected room in the given direction;
        /// if found, connect the rooms. If not, create a new room in that direction.
        /// </summary>
        /// <param name="compassPoint">The direction to consider.</param>
        /// <returns>True if a new connection/room was made; false otherwise.</returns>
        private void addOrConnectRoomRelativeToSelectedRoom(CompassPoint compassPoint)
        {
            var element = SelectedElement as Room;
            if (element != null)
            {
                var room = element;
                var rect = room.InnerBounds;
                rect.Inflate(Settings.PreferredDistanceBetweenRooms + room.Width / 2, Settings.PreferredDistanceBetweenRooms + room.Height / 2);
                var centerOfNewRoom = rect.GetCorner(compassPoint);

                var existing = hitTestElement(centerOfNewRoom, false);
                var two = existing as Room;
                if (two != null)
                {
                    // just connect the rooms together
                    addConnection(room, compassPoint, two, CompassPointHelper.GetOpposite(compassPoint));
                    SelectedElement = existing;
                    EnsureVisible(SelectedElement);
                }
                else
                {
                    // new room entirely
                    var newRoom = new Room(Project.Current) {Position = new Vector(centerOfNewRoom.X - room.Width/2, centerOfNewRoom.Y - room.Height/2),Region = room.Region};

                    newRoom.Size = room.Size;

                    Project.Current.Elements.Add(newRoom);
                    addConnection(room, compassPoint, newRoom, CompassPointHelper.GetOpposite(compassPoint));
                    SelectedElement = newRoom;
                    EnsureVisible(SelectedElement);
                    Refresh();
                    newRoom.ShowDialog();
                }
            }
        }

        /// <summary>
        /// Add an "unexplored" (loopback) connection from 
        /// </summary>
        /// <param name="compassPoint"></param>
        private void addUnexploredConnectionToSelectedRoom(CompassPoint compassPoint)
        {
            var element = SelectedElement as Room;
            if (element != null)
            {
                var room = element;
                addConnection(room, compassPoint, room, compassPoint);
            }
        }

        /// <summary>
        /// Add a new connection between the given rooms.
        /// </summary>
        /// <param name="roomOne">The first room.</param>
        /// <param name="compassPointOne">The direction of the connection in the first room.</param>
        /// <param name="roomTwo">The second room.</param>
        /// <param name="compassPointTwo">The direction of the connection in the second room.</param>
        private Connection addConnection(Room roomOne, CompassPoint compassPointOne, Room roomTwo, CompassPoint compassPointTwo)
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
        private Room getRoomInApproximateDirectionFromRoom(Room room, CompassPoint compassPoint)
        {
            var nextRoom = getRoomInExactDirectionFromRoom(room, compassPoint) ?? getRoomInExactDirectionFromRoom(room, CompassPointHelper.RotateAntiClockwise(compassPoint));

            return nextRoom ?? (getRoomInExactDirectionFromRoom(room, CompassPointHelper.RotateClockwise(compassPoint)));
        }

        /// <summary>
        /// Get a room which can be found in the given direction from the given room.
        /// </summary>
        /// <param name="room">The initial room.</param>
        /// <param name="compassPoint">The direction to consider.</param>
        /// <returns>The room which can be found in that direction, or null if none.</returns>
        private Room getRoomInExactDirectionFromRoom(Room room, CompassPoint compassPoint)
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

        private void beginDragPan(PointF clientPos, Vector canvasPos)
        {
            dragMode = DragModes.Pan;
            mPanPosition = clientPos;
            Cursor = Cursors.NoMove2D;
            Capture = true;
        }

        private void beginDragMove(PointF clientPos, Vector canvasPos)
        {
            if (hoverHandle != null)
            {
                dragMode = DragModes.MoveResizeHandle;
                mDragResizeHandleLastPosition = canvasPos; // unsnapped
                Capture = true;
            }
            else if (hoverPort != null)
            {
                if (hoverPort is MoveablePort)
                {
                    mDragMovePort = (MoveablePort)hoverPort;
                    mDragOffsetCanvas = Settings.Snap(canvasPos - hoverPort.Position);
                    dragMode = DragModes.MovePort;
                    Capture = true;
                }
            }
            else
            {
                var hitElement = hitTestElement(canvasPos, false);

                bool alreadySelected = mSelectedElements.Contains(hitElement);
                if (!alreadySelected && (Control.ModifierKeys & (Keys.Control|Keys.Shift)) == Keys.None)
                {
                    // if clicking on empty space, or an unselected element, without holding Control/Shift, clear the current selection.
                    mSelectedElements.Clear();
                }
                else if (hitElement != null)
                {
                    // if clicking on a selected element, remove it from the selection;
                    // we do this since element's size handles etc. belong to the SelectedElement
                    // which is the last one in the list, so we want to add this one back to the end
                    // of the list as the user seems more interested in it and may want handles.
                    mSelectedElements.Remove(hitElement);
                }
                if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                {
                    if (!alreadySelected && hitElement != null)
                    {
                        // if we're holding shift and we clicked an element which wasn't already selected, select it.
                        mSelectedElements.Add(hitElement);
                    }
                }
                else if (hitElement != null)
                {
                    // if we're not holding shift, ensure the current element is selected.
                    // we're safe to re-add it since it will definitely have been removed already
                    // if it was selected, by the above logic.
                    mSelectedElements.Add(hitElement);
                }

                // now we've finished messing with the set of selected elements,
                // update handles, ports, and take defaults for new elements from the most recently selected element.
                updateSelection();

                if (hitElement != null && mSelectedElements.Contains(hitElement))
                {
                    // if we ended up with the hit element being selected, initiate a drag move.
                    dragMode = DragModes.MoveElement;
                    canvasPos = Settings.Snap(canvasPos);
                    mDragOffsetCanvas = canvasPos;
                    Capture = true;
                }
                else if (hitElement == null)
                {
                    // if we didn't hit anything at all, begin a new marquee selection.
                    dragMode = DragModes.Marquee;
                    mDragOffsetCanvas = canvasPos;
                    mDragMarqueeLastPosition = canvasPos;
                    Capture = true;
                }
            }
            Invalidate();
        }

        private void beginDragDrawLine()
        {
            dragMode = DragModes.DrawLine;
            Capture = true;
        }

        private void beginDrawConnection(PointF clientPos, Vector canvasPos)
        {
            Connection connection;
            hoverPort = hitTestPort(canvasPos);
            if (hoverPort != null && !(hoverPort is MoveablePort))
            {
                // Only from non-moveable ports, until we fix docking.
                // See also DoDragMovePort().
                // Updated to ignore ID gaps. ID gaps are resolved on load
                connection = new Connection(Project.Current, new Vertex(hoverPort), new Vertex(hoverPort));
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
            mDragMovePort = (MoveablePort)connection.Ports[1];
            mDragOffsetCanvas = Settings.Snap(canvasPos - connection.VertexList[0].Position);
            hoverPort = null;
            dragMode = DragModes.MovePort;
            Capture = true;
        }

        public void AddRoom(bool atCursor)
        {
            // Changed this to ignore ID gaps. ID gaps are resolved on load
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
            Refresh();
        }

        private void doDragPan(PointF clientPos, Vector canvasPos)
        {
            var delta = Drawing.Subtract(mPanPosition, clientPos);
            delta = Drawing.Divide(delta, ZoomFactor);
            Origin = new Vector(Origin.X + delta.X, Origin.Y + delta.Y);
            mPanPosition = clientPos;
        }

        private void doDragMoveElement(PointF clientPos, Vector canvasPos)
        {
            canvasPos = Settings.Snap(canvasPos);
            foreach (var element in mSelectedElements)
            {
                moveElementBy(element, canvasPos - mDragOffsetCanvas);
            }
            mDragOffsetCanvas = canvasPos;
        }

        private void moveElementBy(Element element, Vector delta)
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

        private void doDragMoveResizeHandle(PointF clientPos, Vector canvasPos)
        {
            // the mouse has moved this much on the canvas since we last successfully resized the element
            var delta = canvasPos - mDragResizeHandleLastPosition;

            if (hoverHandle != null)
            {
                // work out to whether and where we'd like to move the element's corner/edge
                var newPosition = hoverHandle.OwnerPosition + delta;
                if (newPosition != hoverHandle.OwnerPosition)
                {
                    // we'd like to move the element's corner/edge;
                    // try to do so
                    var oldPosition = hoverHandle.OwnerPosition;
                    hoverHandle.OwnerPosition = Settings.Snap(newPosition);

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
                    if (hoverHandle.OwnerPosition.X != oldPosition.X)
                    {
                        // we managed to move the element's corner/edge horizontally, on the X axis;
                        // on this axis, apply the effective delta to our the basis for future movement.
                        mDragResizeHandleLastPosition.X += hoverHandle.OwnerPosition.X - oldPosition.X;
                    }
                    if (hoverHandle.OwnerPosition.Y != oldPosition.Y)
                    {
                        // likewise for the vertical/Y axis
                        mDragResizeHandleLastPosition.Y += hoverHandle.OwnerPosition.Y - oldPosition.Y;
                    }
                }
            }
        }

        private void doDragMovePort(PointF clientPos, Vector canvasPos)
        {
            if (hoverPort != null && hoverPort != mDragMovePort)
            {
                if (mDragMovePort.DockedAt != hoverPort && (!(hoverPort is MoveablePort) || ((MoveablePort)hoverPort).DockedAt != mDragMovePort))
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
                    if (!(hoverPort is MoveablePort))
                    {
                        mDragMovePort.DockAt(hoverPort);
                    }
                    else
                    {
                        canvasPos = Settings.Snap(canvasPos);
                        mDragMovePort.SetPosition(canvasPos - mDragOffsetCanvas);
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
                mDragMovePort.SetPosition(canvasPos - mDragOffsetCanvas);
            }
        }

        private void endDrag()
        {
            if (dragMode == DragModes.MovePort)
            {
                // clear the selection now the line is drawn
                SelectedElement = null;

                if (mDragMovePort.Owner is Connection)
                {
                    // remove dead connections
                    var connection = (Connection)mDragMovePort.Owner;
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
            else if (dragMode == DragModes.Marquee)
            {
                var marqueeRect = getMarqueeCanvasBounds();
                if ((Control.ModifierKeys & (Keys.Shift | Keys.Control)) == Keys.None)
                {
                    mSelectedElements.Clear();
                }
                foreach (var element in hitTest(marqueeRect, false))
                {
                    if (!mSelectedElements.Contains(element))
                    {
                        mSelectedElements.Add(element);
                    }
                    else if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        if (mSelectedElements.Contains(element))
                        {
                            mSelectedElements.Remove(element);
                        }
                    }
                }
                updateSelection();
            }
            dragMode = DragModes.None;
            hoverHandle = null;
            Capture = false;
            Cursor = null;
            Invalidate();
        }

        private List<Element> hitTest(Rect rect, bool roomsOnly)
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

        private Rect getMarqueeCanvasBounds()
        {
            if (dragMode != DragModes.Marquee)
            {
                return Rect.Empty;
            }
            var topLeft = mDragOffsetCanvas;
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

        private float snapToElementSizeAtCurrentZoomFactor
        {
            get { return Settings.SnapToElementSize; }
        }



        private Element hitTestElement(Vector canvasPos, bool includeMargins)
        {
            List<Element> closest = new List<Element>();
            float closestDistance = float.MaxValue;
            foreach (var element in depthSortElements()) // sort into drawing order
            {
                if (dragMode == DragModes.MovePort && mDragMovePort.Owner == element)
                {
                    // when moving a port on an element, don't try to dock it to that element itself
                    continue;
                }

                float distance = element.Distance(canvasPos, includeMargins);
                if (distance <= snapToElementSizeAtCurrentZoomFactor)
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

        private ResizeHandle hitTestHandle(Vector canvasPos)
        {
            // examine handles, topmost (drawn) to lowermost
            for (int index = mHandles.Count - 1; index >= 0; --index)
            {
                var handle = mHandles[index];
                if (handle.HitTest(canvasPos))
                {
                    return handle;
                }
            }
            return null;
        }

        private Port hitTestPort(Vector canvasPos)
        {
            Port closest = null;
            float closestDistance = float.MaxValue;

            foreach (var port in mPorts)
            {
                if (dragMode == DragModes.MovePort && port == mDragMovePort)
                {
                    // when dragging a port, don't try to dock it with itself
                    continue;
                }

                float distance = port.Distance(canvasPos);

                var snapDistance = snapToElementSizeAtCurrentZoomFactor;

                var bounds = port.Owner.UnionBoundsWith(Rect.Empty, true);
                if (bounds.Contains(canvasPos))
                {
                    if (dragMode == DragModes.MovePort)
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
            get { return mSelectedElements.Count > 0 ? mSelectedElements[mSelectedElements.Count - 1] : null; }
            set
            {
                var selectedElement = mSelectedElements.Count > 0 ? mSelectedElements[mSelectedElements.Count - 1] : null;
                if (selectedElement != value)
                {
                    mSelectedElements.Clear();
                    if (value != null)
                    {
                        mSelectedElements.Add(value);
                    }
                    updateSelection();
                }
            }
        }

        public int SelectedElementCount
        {
            get { return mSelectedElements.Count; }
        }

        public bool HasSingleSelectedElement
        {
            get { return SelectedElementCount == 1; }
        }

        public bool HasSelectedElement<T>() where T : Element
        {
            foreach (var element in mSelectedElements)
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
            get { return mSelectedElements; }
        }

        void updateSelection()
        {
            recreateHandles();
            recreatePorts();
            // only if we have a single element selected;
            // otherwise selecting multiple items will cause one to override the others' settings!
            var selectedElement = SelectedElement;
            if (selectedElement is Connection)
            {
                setConnectionDefaultsFrom((Connection)selectedElement);
            }
            else if (selectedElement is Room)
            {
                setRoomDefaultsFrom((Room)selectedElement);
            }
            Invalidate();
        }

        public void SelectAll()
        {
            mSelectedElements.Clear();
            mSelectedElements.AddRange(Project.Current.Elements);
            updateSelection();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private ResizeHandle hoverHandle
        {
            get { return mHoverHandle; }
            set
            {
                if (mHoverHandle != value)
                {
                    mHoverHandle = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private Port hoverPort
        {
            get { return mHoverPort; }
            set
            {
                if (mHoverPort != value)
                {
                    mHoverPort = value;
                    Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Element HoverElement
        {
            get
            {
                return mHoverElement;
            }
            set
            {
                if (mHoverElement != value)
                {
                    mHoverElement = value;
                    recreatePorts();
                }
            }
        }

        private void recreateHandles()
        {
            hoverHandle = null;
            mHandles.Clear();
            var element = SelectedElement;
            if (CanSelectElements && element is ISizeable && HasSingleSelectedElement)
            {
                var sizeable = (ISizeable)element;
                mHandles.Add(new ResizeHandle(CompassPoint.North, sizeable));
                mHandles.Add(new ResizeHandle(CompassPoint.South, sizeable));
                mHandles.Add(new ResizeHandle(CompassPoint.East, sizeable));
                mHandles.Add(new ResizeHandle(CompassPoint.West, sizeable));
                mHandles.Add(new ResizeHandle(CompassPoint.NorthWest, sizeable));
                mHandles.Add(new ResizeHandle(CompassPoint.NorthEast, sizeable));
                mHandles.Add(new ResizeHandle(CompassPoint.SouthWest, sizeable));
                mHandles.Add(new ResizeHandle(CompassPoint.SouthEast, sizeable));
            }

            Invalidate();
        }

        private void recreatePorts()
        {
            hoverPort = null;
            mPorts.Clear();

            // decide if we want ports on the element under the mouse cursor; if so, add them
            if (HoverElement is Room && !mSelectedElements.Contains(HoverElement))
            {
                // we're hovering over a non-selected room;
                // (we don't show ports on selected rooms since they get handles already and it's too confusing;
                // we don't show ports on lines we're hovering over, since we don't allow line-line connections
                // right now as the algorithm's borked somewhere and can end up with nastiness)
                if (dragMode == DragModes.MovePort || (CanDrawLine && SelectedElement == null))
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
                    mPorts.AddRange(HoverElement.Ports);
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
                        mPorts.Add(port);
                    }
                }
            }

            Invalidate();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private DragModes dragMode
        {
            get { return mDragMode; }
            set
            {
                mDragMode = value;
                recreatePorts();
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
            get { return mNewConnectionStyle; }
            set
            {
                if (mNewConnectionStyle != value)
                {
                    mNewConnectionStyle = value;
                    raiseNewConnectionStyleChanged();
                }
            }
        }

        public void ApplyConnectionStyle(ConnectionStyle connectionStyle)
        {
            foreach (var element in mSelectedElements)
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

        private void raiseNewConnectionStyleChanged()
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
            get { return mNewConnectionFlow; }
            set 
            {
                if (mNewConnectionFlow != value)
                {
                    mNewConnectionFlow = value;
                    raiseNewConnectionFlowChanged();
                }
            }
        }

        public void ApplyConnectionFlow(ConnectionFlow connectionFlow)
        {
            foreach (var element in mSelectedElements)
            {
                var element1 = element as Connection;
                if (element1 != null)
                {
                    var connection = element1;
                    connection.Flow = connectionFlow;
                }
            }
            Invalidate();
        }

        public event EventHandler NewConnectionFlowChanged;

        private void raiseNewConnectionFlowChanged()
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
            get { return mNewConnectionLabel; }
            set
            {
                if (mNewConnectionLabel != value)
                {
                    mNewConnectionLabel = value;
                    raiseNewConnectionLabelChanged();
                }
            }
        }

        public void ApplyConnectionLabel(ConnectionLabel connectionLabel)
        {
            foreach (var element in mSelectedElements)
            {
                var element1 = element as Connection;
                if (element1 != null)
                {
                    var connection = element1;
                    connection.SetText(connectionLabel);
                }
            }
            Invalidate();
        }

        public event EventHandler NewConnectionLabelChanged;

        private void raiseNewConnectionLabelChanged()
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
                if (CanDrawLine && ((hoverPort != null && !(hoverPort is MoveablePort)) || dragMode == DragModes.MovePort))
                {
                    return Drawing.DrawLineCursor;
                }

                if (hoverPort != null && hoverPort is MoveablePort)
                {
                    return Drawing.MoveLineCursor;
                }

                if (hoverHandle != null)
                {
                    var cursor = hoverHandle.Cursor;
                    if (cursor != null)
                    {
                        return cursor;
                    }
                }

                if (HoverElement != null && HoverElement is IMoveable && mSelectedElements.Contains(HoverElement))
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

        private void setRoomDefaultsFrom(Room room)
        {
            mNewRoomSize = room.Size;
            mNewRoomIsDark = room.IsDark;
            mNewRoomObjectsPosition = room.ObjectsPosition;
        }

        private void setConnectionDefaultsFrom(Connection connection)
        {
            NewConnectionFlow = connection.Flow;
            NewConnectionStyle = connection.Style;
        }

        public void ReverseLineDirection()
        {
            foreach (var element in mSelectedElements)
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
            var doomedElements = new List<Element>(mSelectedElements);
            foreach (var element in doomedElements)
            {
                Project.Current.Elements.Remove(element);
            }
            mSelectedElements.Clear();
            updateSelection();
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
            mUpdatingScrollBars = true;

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

            mUpdatingScrollBars = false;
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (mUpdatingScrollBars)
            {
                return;
            }

            // the scroll bar will Invalidate() and Update() us; avoid exceptions
            mDoNotUpdateScrollBarsNextPaint = true;

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

        private void reset()
        {
            ZoomFactor = 1;
            Origin = Vector.Zero;
            SelectedElement = null;
            HoverElement = null;
            hoverHandle = null;
            hoverPort = null;
            dragMode = DragModes.None;
            NewConnectionStyle = ConnectionStyle.Solid;
            NewConnectionFlow = ConnectionFlow.TwoWay;
            mNewRoomSize = new Vector(Settings.GridSize * 3, Settings.GridSize * 2);
            mNewRoomIsDark = false;
            mNewRoomObjectsPosition = CompassPoint.South;
            requestRecomputeSmartSegments();
            StopAutomapping();
        }

        public void CopySelectedElements()
        {
            String clipboardText = "Elements";
            foreach (var element in mSelectedElements)
            {
                clipboardText += "\r\n";
                if (element is Room)
                {
                    clipboardText += "room:";
                    clipboardText += element.ID + ":";
                    clipboardText += ((Room)element).ClipboardPrint();
                }
                else if (element is Connection)
                {
                    clipboardText += "line:";
                    clipboardText += element.ID + ":";
                    clipboardText += ((Connection)element).ClipboardPrint();
                }
            }

            Clipboard.SetText(clipboardText);
        }

        public void CopySelectedColor()
        {
            String clipboardText = "Colors";

            if (SelectedElement is Room)
            {
                clipboardText += "\r\n";
                clipboardText += ((Room)SelectedElement).ClipboardColorPrint();
            }

            Clipboard.SetText(clipboardText);
        }

        public void Paste(bool atCursor)
        {
            String clipboardText = Clipboard.GetText();

            if (!string.IsNullOrEmpty(clipboardText))
            {
                String[] elements = clipboardText.Replace("\r\n", "|").Split('|');

                if (elements.Length > 0)
                {
                    if (elements[0] == "Elements")
                    {
                        int index = 1;

                        List<Element> newElements = new List<Element>();

                        bool firstElement = true;
                        float firstX = 0;
                        float firstY = 0;
                        float newFirstX = 0;
                        float newFirstY = 0;
                        bool removeElement = false;

                        newElements.Clear();

                        while (index < elements.Length)
                        {
                            String[] elementProperties = elements[index].Split(':');

                            // Rooms and Lines both copy 15 items for their base attributes
                            if (elementProperties.Length > 14)
                            {
                                // Only load rooms on the first pass
                                if (elementProperties[0] == "room")
                                {
                                    AddRoom(atCursor); // Create the room

                                    Room currentRoom = (Room)Project.Current.Elements[Project.Current.Elements.Count - 1]; // Link to the new room

                                    currentRoom.OldID = Convert.ToInt32(elementProperties[1]); // Keep a record of the old ID
                                    currentRoom.Name = elementProperties[2]; // Set the room's name

                                    // Check if it's the first element in the paste and record the old and new locations for reference
                                    if (firstElement)
                                    {
                                        firstX = Convert.ToSingle(elementProperties[3]);
                                        firstY = Convert.ToSingle(elementProperties[4]);
                                        newFirstX = currentRoom.X;
                                        newFirstY = currentRoom.Y;
                                        firstElement = false;
                                    }
                                    else // If it's not the first element then paste it relative to the new element
                                    {
                                        currentRoom.Position = new Vector(newFirstX + (Convert.ToSingle(elementProperties[3]) - firstX), newFirstY + (Convert.ToSingle(elementProperties[4]) - firstY));
                                    }

                                    // Set the remaining attributes in the order they were copied
                                    currentRoom.Size = new Vector(Convert.ToSingle(elementProperties[5]), Convert.ToSingle(elementProperties[6]));
                                    currentRoom.IsDark = Convert.ToBoolean(elementProperties[7]);
                                    currentRoom.AddDescription(elementProperties[8]);
                                    currentRoom.Region = elementProperties[9];
                                    currentRoom.RoomFill = ColorTranslator.FromHtml(elementProperties[10]);
                                    currentRoom.SecondFill = ColorTranslator.FromHtml(elementProperties[11]);
                                    currentRoom.SecondFillLocation = elementProperties[12];
                                    currentRoom.RoomBorder = ColorTranslator.FromHtml(elementProperties[13]);
                                    currentRoom.RoomLargeText = ColorTranslator.FromHtml(elementProperties[14]);
                                    currentRoom.RoomSmallText = ColorTranslator.FromHtml(elementProperties[15]);

                                    // Get ready to check for objects in the room (small text)
                                    int index2 = 16;
                                    String newObjects = "";

                                    // Cycle through all the objects
                                    while (index2 < elementProperties.Length)
                                    {
                                        // First attribute will be which direction the objects are written
                                        if (index2 == 16)
                                        {
                                            CompassPoint point;
                                            CompassPointHelper.FromName(elementProperties[16], out point);
                                            currentRoom.ObjectsPosition = point;
                                        }
                                        // It's it's the last object then don't add \r\n on the end
                                        else if (index2 == elementProperties.Length - 1)
                                        {
                                            newObjects += elementProperties[index2];
                                        }
                                        // Add all other objects with \r\n
                                        else
                                        {
                                            newObjects += elementProperties[index2] + "\r\n";
                                        }
                                        ++index2;
                                    }
                                    // Ass the objects to the room
                                    currentRoom.Objects = newObjects;
                                    // Keep a record of the elements that are pasted
                                    newElements.Add(currentRoom);
                                }
                            }

                            ++index;
                        }

                        // Reset the index for a second pass
                        index = 1;

                        while (index < elements.Length)
                        {
                            String[] elementProperties = elements[index].Split(':');

                            // Rooms and Lines both copy 15 items for their base attributes
                            if (elementProperties.Length > 14)
                            {
                                // Only load line on the second pass
                                if (elementProperties[0] == "line")
                                {
                                    // Create the new connection
                                    var currentConnection = new Connection(Project.Current);
                                    Project.Current.Elements.Add(currentConnection);

                                    // Set the connection style
                                    switch (elementProperties[2])
                                    {
                                        case "solid":
                                            currentConnection.Style = ConnectionStyle.Solid;
                                            break;
                                        case "dashed":
                                            currentConnection.Style = ConnectionStyle.Dashed;
                                            break;
                                        default:
                                            currentConnection.Style = ConnectionStyle.Solid;
                                            break;
                                    }

                                    // Set the connection Flow
                                    switch (elementProperties[3])
                                    {
                                        case "oneWay":
                                            currentConnection.Flow = ConnectionFlow.OneWay;
                                            break;
                                        case "twoWay":
                                            currentConnection.Flow = ConnectionFlow.TwoWay;
                                            break;
                                        default:
                                            currentConnection.Flow = ConnectionFlow.OneWay;
                                            break;
                                    }

                                    // Set the texts on the connection
                                    currentConnection.StartText = elementProperties[4];
                                    currentConnection.MidText = elementProperties[5];
                                    currentConnection.EndText = elementProperties[6];

                                    // Used to determine if the first vertex needs to be redone
                                    bool firstVertexFound = true;

                                    // Check if the first vertex is a dock or a point
                                    if (elementProperties[7] == "dock")
                                    {
                                        // If this is the first element to be pasted
                                        if (firstElement)
                                        {
                                            // Check if the other vertex is a point
                                            if (elementProperties[11] == "point")
                                            {
                                                // record the location of the line both old and new
                                                firstX = Convert.ToSingle(elementProperties[13]);
                                                firstY = Convert.ToSingle(elementProperties[14]);
                                                newFirstX = 0;
                                                newFirstY = 0;
                                                firstElement = false;
                                            }
                                            else if (elementProperties[11] == "dock")
                                            {
                                                // The first element can not be a line with 2 docked ends. Mark this for removal
                                                removeElement = true;
                                            }
                                        }

                                        // Make sure that co-ordinates were able to be pasted
                                        if (!firstElement)
                                        {
                                            bool foundDock = false;

                                            // Check all the previous elements that have been pasted
                                            foreach (var element in newElements)
                                            {
                                                // Check for rooms
                                                if (element is Room)
                                                {
                                                    // See if it was the room that this used to be docked to
                                                    if (((Room)element).OldID == Convert.ToInt32(elementProperties[9]))
                                                    {
                                                        // Determine the compass point for the dock
                                                        CompassPoint point;
                                                        CompassPointHelper.FromName(elementProperties[10], out point);

                                                        // Add the first Vertex
                                                        Vertex vertexOne = new Vertex(((Room)element).PortAt(point));
                                                        currentConnection.VertexList.Add(vertexOne);

                                                        foundDock = true;
                                                    }
                                                }
                                            }

                                            if (!foundDock)
                                            {
                                                firstVertexFound = false;
                                            }
                                        }
                                    }
                                    else if (elementProperties[7] == "point")
                                    {
                                        // If this is the first element then record the location of the line both old and new
                                        if (firstElement)
                                        {
                                            firstX = Convert.ToSingle(elementProperties[9]);
                                            firstY = Convert.ToSingle(elementProperties[10]);
                                            newFirstX = 0;
                                            newFirstY = 0;
                                            firstElement = false;
                                        }

                                        // Add the first Vertex
                                        Vector vectorOne = new Vector(newFirstX + (Convert.ToSingle(elementProperties[9]) - firstX), newFirstY + (Convert.ToSingle(elementProperties[10]) - firstY));
                                        Vertex vertexOne = new Vertex(vectorOne);
                                        currentConnection.VertexList.Add(vertexOne);
                                    }

                                    // Check if the second vertex is a dock or a point
                                    if (elementProperties[11] == "dock")
                                    {
                                        // If this is the first element to be pasted
                                        if (firstElement)
                                        {
                                            removeElement = true;
                                        }

                                        // Make sure that co-ordinates were able to be pasted
                                        if (!firstElement)
                                        {
                                            bool foundDock = false;

                                            // Check all the previous elements that have been pasted
                                            foreach (var element in newElements)
                                            {
                                                // Check for rooms
                                                if (element is Room)
                                                {
                                                    // See if it was the room that this used to be docked to
                                                    if (((Room)element).OldID == Convert.ToInt32(elementProperties[13]))
                                                    {
                                                        // Determine the compass point for the dock
                                                        CompassPoint point;
                                                        CompassPointHelper.FromName(elementProperties[14], out point);

                                                        // Add the first Vertex
                                                        Vertex vertexOne = new Vertex(((Room)element).PortAt(point));
                                                        currentConnection.VertexList.Add(vertexOne);

                                                        foundDock = true;
                                                    }
                                                }
                                            }

                                            if (!foundDock)
                                            {
                                                Vector vectorOne = new Vector(currentConnection.VertexList[0].Position.X + 1, currentConnection.VertexList[0].Position.Y + 1);
                                                Vertex vertexOne = new Vertex(vectorOne);
                                                currentConnection.VertexList.Add(vertexOne);
                                            }
                                        }
                                    }
                                    else if (elementProperties[11] == "point")
                                    {
                                        // If this is the first element then record the location of the line both old and new
                                        if (firstElement)
                                        {
                                            removeElement = true;
                                        }

                                        // Add the first Vertex
                                        Vector vectorOne = new Vector(newFirstX + (Convert.ToSingle(elementProperties[13]) - firstX), newFirstY + (Convert.ToSingle(elementProperties[14]) - firstY));
                                        Vertex vertexOne = new Vertex(vectorOne);
                                        currentConnection.VertexList.Add(vertexOne);
                                    }

                                    if (!firstVertexFound)
                                    {
                                        Vector vectorOne = new Vector(currentConnection.VertexList[0].Position.X + 1, currentConnection.VertexList[0].Position.Y + 1);
                                        Vertex vertexOne = new Vertex(vectorOne);
                                        currentConnection.VertexList.Add(vertexOne);
                                    }

                                    // Add the new connection to the list of pasted elements
                                    newElements.Add(currentConnection);
                                }
                            }

                            // If the connection was not pastable it will get flagged and removed
                            if (removeElement)
                            {
                                Project.Current.Elements.Remove(Project.Current.Elements[Project.Current.Elements.Count - 1]);
                                removeElement = false;
                            }


                            ++index;
                        }

                        mSelectedElements = newElements;
                    }

                    if (elements[0] == "Colors")
                    {
                        if (elements.Length > 1)
                        {
                            String[] pasteColors = elements[1].Split(':');

                            if (pasteColors.Length > 5)
                            {
                                String fillColor = pasteColors[0];
                                String secondFillColor = pasteColors[1];
                                String secondFillLocation = pasteColors[2];
                                String borderColor = pasteColors[3];
                                String largeTextColor = pasteColors[4];
                                String smallTextColor = pasteColors[5];

                                foreach (var element in SelectedElements)
                                {
                                    if (element is Room)
                                    {
                                        ((Room)element).RoomFill = ColorTranslator.FromHtml(fillColor);
                                        ((Room)element).SecondFill = ColorTranslator.FromHtml(secondFillColor);
                                        ((Room)element).SecondFillLocation = secondFillLocation;
                                        ((Room)element).RoomBorder = ColorTranslator.FromHtml(borderColor);
                                        ((Room)element).RoomLargeText = ColorTranslator.FromHtml(largeTextColor);
                                        ((Room)element).RoomSmallText = ColorTranslator.FromHtml(smallTextColor);
                                    }
                                }
                            }
                        }
                    }
                }                
            }
        }

        private float mZoomFactor;
        private Vector mOrigin;

        private List<Element> mSelectedElements = new List<Element>();
        private Element mHoverElement;
        private ResizeHandle mHoverHandle;
        private Port mHoverPort;

        private Point mLastKnownMousePosition;
        private Point mLastMouseDownPosition;
        private DragModes mDragMode;
        private PointF mPanPosition;
        private Vector mDragOffsetCanvas;
        private MoveablePort mDragMovePort;
        private Vector mDragMarqueeLastPosition;
        private Vector mDragResizeHandleLastPosition;

        private List<ResizeHandle> mHandles = new List<ResizeHandle>();
        private List<Port> mPorts = new List<Port>();

        private ConnectionStyle mNewConnectionStyle;
        private ConnectionFlow mNewConnectionFlow;
        private ConnectionLabel mNewConnectionLabel;
        private Vector mNewRoomSize;
        private bool mNewRoomIsDark;
        private CompassPoint mNewRoomObjectsPosition;
        private System.Threading.Timer mRecomputeTimer;
        private bool mUpdatingScrollBars;
        private bool mDoNotUpdateScrollBarsNextPaint;

        private static readonly int RecomputeNMillisecondsAfterChange = 500;
        private static bool mSmartLineSegmentsUpToDate = false;
        private Room lastSelectedRoom;

        private void ctxCanvasMenu_Opening(object sender, CancelEventArgs e)
        {
            if (m_minimap.IsMouseOverMe())
                e.Cancel = true;

            var clientPos = new PointF(mLastMouseDownPosition.X, mLastMouseDownPosition.Y);
            var canvasPos = ClientToCanvas(clientPos);
            var hitElement = hitTestElement(canvasPos, false);
            var regionMenu = regionToolStripMenuItem;

            darkToolStripMenuItem.Visible = false;

            regionMenu.Enabled = false;
            if (hitElement != null)
            {
                if (hitElement.GetType() == typeof (Room))
                {
                    regionMenu.Enabled = true;
                    lastSelectedRoom = (Room)hitElement;

                    regionMenu.DropDownItems.Clear();

                    //OrderBy(i => i != "first").ThenBy(i => i)
                  foreach (var region in Settings.Regions.OrderBy(p => p.RegionName != Trizbort.Region.DefaultRegion).ThenBy(p=>p.RegionName))
                    {
                        var item = regionMenu.DropDownItems.Add(region.RegionName, null, regionContextClick);
                        item.Image = generateRegionImage(region);
                        if (region.RegionName == lastSelectedRoom.Region)
                            ((ToolStripMenuItem) item).Checked = true;
                    }

                    darkToolStripMenuItem.Visible = true;
                    darkToolStripMenuItem.Click += darkToolStripMenuItem_Click;
                    darkToolStripMenuItem.Checked = lastSelectedRoom.IsDark;
                }
            }
        }

        private Image generateRegionImage(Region region)
        {
            var image = new Bitmap(24,20);
            Graphics g = Graphics.FromImage(image);
            Font txtColorFont = new Font("Arial", 6);
            using (var palette = new Palette())
            {
                g.FillRectangle(palette.Brush(region.RColor),0,0,24,20);
            }

            return image;

        }

        void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedRooms = mSelectedElements.Where(p => p.GetType() == typeof(Room)).ToList();

            if (!selectedRooms.Any())
                selectedRooms.Add(lastSelectedRoom);

            var darkMenu = ((ToolStripMenuItem) sender);

            foreach (var selectedRoom in selectedRooms.Cast<Room>())
            {
                selectedRoom.IsDark = !darkMenu.Checked;
            }
        }

        // context menu event to change region of room(s)
        private void regionContextClick(object sender, EventArgs e)
        {
            var selectedRooms = mSelectedElements.Where(p => p.GetType() == typeof (Room)).ToList();

            if (!selectedRooms.Any())
                selectedRooms.Add(lastSelectedRoom);
            
            var regionSelected = ((ToolStripMenuItem) sender);

            foreach (var selectedRoom in selectedRooms.Cast<Room>()) 
            {
                selectedRoom.Region = regionSelected.Text;
            }
            
       }
    }
}
