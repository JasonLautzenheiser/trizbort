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
using PdfSharp.Drawing;

namespace Trizbort
{
	/// <summary>
	/// A docking point at which a connection's vertex may join to an element.
	/// </summary>
	internal abstract class Port
	{
		public Port(Element owner)
		{
			Owner = owner;
		}

		public Element Owner
		{
			get;
			private set;
		}

		/// <summary>
		/// Get the unique identifier for this port amongst all of the owner's ports.
		/// </summary>
		public abstract string ID
		{
			get;
		}

		public virtual Vector Position
		{
			get { return Owner.GetPortPosition(this); }
		}

		public virtual bool HasStalk
		{
			get { return StalkPosition != Position; }
		}

		public virtual Vector StalkPosition
		{
			get { return Owner.GetPortStalkPosition(this); }
		}

		public virtual float X
		{
			get { return Position.X; }
		}

		public virtual float Y
		{
			get { return Position.Y; }
		}

		private Vector Size
		{
			get { return new Vector(Settings.HandleSize); }
		}

        private Vector VisualPosition
        {
            get { return Position; }
        }

		private Rect VisualBounds
		{
			get { return new Rect(VisualPosition - Size / 2, Size); }
		}

		public virtual void Draw(Canvas canvas, XGraphics graphics, Palette palette, DrawingContext context)
		{
            Drawing.DrawHandle(canvas, graphics, palette, VisualBounds, context, false, true);
		}

		public float Distance(Vector pos)
		{
			return pos.DistanceFromRect(VisualBounds);
		}
	}
}
