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

using System.Collections.Generic;

namespace Trizbort.Domain.Misc
{
    /// <summary>
    /// The context in which an object's drawing is taking place.
    /// </summary>
    public class DrawingContext
    {
        public DrawingContext(float zoomFactor)
        {
            ZoomFactor = zoomFactor;
        }

        /// <summary>
        /// Get/set whether the object should be drawn as if selected.
        /// </summary>
        public bool Selected
        {
            get;
            set;
        }

        /// <summary>
        /// Get/set whether the object should be drawn as if hovered over.
        /// </summary>
        public bool Hover
        {
            get;
            set;
        }

        /// <summary>
        /// Get/set the zoom factor at which drawing is taking place.
        /// </summary>
        public float ZoomFactor
        {
            get;
            set;
        }

        /// <summary>
        /// Get/set whether to draw smart line segments, as opposed to simple ones.
        /// </summary>
        public bool UseSmartLineSegments
        {
            get;
            set;
        }

        /// <summary>
        /// Get the list of lines drawn so far.
        /// </summary>
        public List<LineSegment> LinesDrawn
        {
            get { return m_linesDrawn; }
        }

        private List<LineSegment> m_linesDrawn = new List<LineSegment>();
    }
}
