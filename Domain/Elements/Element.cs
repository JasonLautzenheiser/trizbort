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
using DevComponents.DotNetBar;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using Trizbort.Domain.Application;
using Trizbort.Domain.Misc;

namespace Trizbort.Domain.Elements {
  public class Element : IComparable<Element> {
    private int mID;

    private int zOrder;

    public Element() {
      initElement(null);
    }

    public Element(Project project) {
      initElement(project);
      var id = GetNextID();
      ID = id;
    }

    // Added this second constructor to be used when loading a room
    // This constructor is significantly faster as it doesn't look for gap in the element IDs
    public Element(Project project, int TotalIDs) {
      initElement(project);
      ID = TotalIDs;
    }

    /// <summary>
    ///   Get the drawing priority of this element.
    /// </summary>
    /// <remarks>
    ///   Elements with a higher drawing priority are drawn last.
    /// </remarks>
    public virtual Depth Depth => Depth.Low;

    /// <summary>
    ///   Get/set whether this element is flagged. For temporary use by the Canvas when traversing elements.
    /// </summary>
    public bool Flagged { get; set; }

    /// <summary>
    ///   Get whether the element has a properties dialog which may be displayed.
    /// </summary>
    public virtual bool HasDialog => false;

    /// <summary>
    ///   Get the unique identifier of this element.
    /// </summary>
    public int ID {
      get => mID;
      set {
        if (Project != null)
          if (!Project.IsElementIDInUse(value))
            mID = value;
      }
    }

    public virtual string Name { get; set; }

    /// <summary>
    ///   Get the collection of ports on the element.
    /// </summary>
    [JsonIgnore]
    public List<Port> PortList { get; set; } = new List<Port>();

    public virtual Vector Position { get; set; }

    [JsonIgnore]
    public Project Project { get; set; }

    public virtual int ZOrder {
      get => zOrder;
      set {
        zOrder = value;
        RaiseChanged();
      }
    }

    /// <summary>
    ///   Get/set whether to raise change events.
    /// </summary>
    protected bool RaiseChangedEvents { get; set; } = true;

    /// <summary>
    ///   Compare this element to another.
    /// </summary>
    /// <param name="element">The other element.</param>
    /// <remarks>
    ///   This method is used to sort into drawing order. Depth is the
    ///   primary criterion; after that a deterministic sort order is
    ///   guaranteed by using a unique, monotonically increasing sort
    ///   identifier for each element.
    /// </remarks>
    public int CompareTo(Element element) {
      var delta = Depth.CompareTo(element.Depth);
      if (delta == 0) {
        delta = ZOrder.CompareTo(element.ZOrder);
        if (delta == 0)
          delta = ID.CompareTo(element.ID);
      }

      return delta;
    }

    /// <summary>
    ///   Event raised when the element changes.
    /// </summary>
    public event EventHandler Changed;

    public virtual void Dispose() {
      //There is nothing to clean. 
      Disposed?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler Disposed;

    /// <summary>
    ///   Get the distance from this element to the given point.
    /// </summary>
    /// <param name="includeMargins">True to include suitable margins around elements which need them; false otherwise.</param>
    public virtual float Distance(Vector pos, bool includeMargins) {
      return 0.0f;
    }

    /// <summary>
    ///   Draw the element.
    /// </summary>
    /// <param name="graphics">The graphics with which to draw.</param>
    /// <param name="palette">The palette from which to obtain drawing tools.</param>
    /// <param name="context">The context in which drawing is taking place.</param>
    public virtual void Draw(XGraphics graphics, Palette palette, DrawingContext context) { }

    public int GetNextID() {
      var id = 1;
      while (Project.IsElementIDInUse(id))
        ++id;
      return id;
    }

    /// <summary>
    ///   Get the position of a given port on the element.
    /// </summary>
    /// <param name="port">The port in question.</param>
    /// <returns>The position of the port.</returns>
    public virtual Vector GetPortPosition(Port port) {
      return new Vector(0, 0);
    }

    /// <summary>
    ///   Get the position of the end of the "stalk" on the given port; or the port position if none.
    /// </summary>
    /// <param name="port">The port in question.</param>
    /// <returns>The position of the end of the stalk, or the port position.</returns>
    public virtual Vector GetPortStalkPosition(Port port) {
      return new Vector(0, 0);
    }

    public RoomShape GetRoomType() {
      if (this is Room xx) return xx.Shape;
      return RoomShape.NotARoom;
    }

    public virtual eTooltipColor GetToolTipColor() {
      return eTooltipColor.Blue;
    }

    public virtual string GetToolTipFooter() {
      return string.Empty;
    }

    public virtual string GetToolTipHeader() {
      return string.Empty;
    }

    public virtual string GetToolTipText() {
      return string.Empty;
    }

    public virtual bool HasTooltip() {
      return false;
    }

    /// <summary>
    ///   Get whether this element intersects the given rectangle.
    /// </summary>
    public virtual bool Intersects(Rect rect) {
      return false;
    }

    /// <summary>
    ///   Perform a pre-drawing pass for this element.
    /// </summary>
    /// <param name="context">The context in which drawing is taking place.</param>
    /// <remarks>
    ///   Elements which wish to add to context.LinesDrawn such that
    ///   Connections lines will detect them may do so here.
    /// </remarks>
    public virtual void PreDraw(DrawingContext context) { }

    /// <summary>
    ///   Recompute any "smart" line segments we use when drawing.
    /// </summary>
    public virtual void RecomputeSmartLineSegments(DrawingContext context) { }

    /// <summary>
    ///   Display the element's properties dialog.
    /// </summary>
    public virtual void ShowDialog() { }

    /// <summary>
    ///   Enlarge the given rectangle so as to fully incorporate this element.
    /// </summary>
    /// <param name="rect">The rectangle to enlarge.</param>
    /// <param name="includeMargins">True to include suitable margins around elements which need them; false otherwise.</param>
    /// <returns>The new rectangle which incorporates this element.</returns>
    /// <remarks>
    ///   This method is used to determine the bounds of the canvas on which
    ///   elements are drawn when exporting as an image or PDF.
    ///   For that usage, includeMargins should be set to true.
    ///   For more exacting bounds tests, includeMargins should be set to false.
    /// </remarks>
    public virtual Rect UnionBoundsWith(Rect rect, bool includeMargins) {
      return new Rect();
    }

    /// <summary>
    ///   Raise the Changed event.
    /// </summary>
    protected void RaiseChanged() {
      if (!RaiseChangedEvents) return;
      var changed = Changed;
      changed?.Invoke(this, EventArgs.Empty);
    }

    private void initElement(Project project) {
      Project = project;
    }
  }
}