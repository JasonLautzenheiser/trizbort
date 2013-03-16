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
using System.Windows.Forms;
using PdfSharp.Drawing;

namespace Trizbort
{
    /// <summary>
    /// A visual handle by which an element may be resized.
    /// </summary>
    internal class ResizeHandle
    {
        public ResizeHandle(CompassPoint compassPoint, ISizeable owner)
        {
            m_compassPoint = compassPoint;
            m_owner = owner;
        }

        public Vector Position
        {
            get
            {
                var bounds = m_owner.InnerBounds;
                //bounds.Inflate(Settings.HandleSize * 1.5f, Settings.HandleSize * 1.5f);
                var pos = bounds.GetCorner(m_compassPoint);
                pos.X -= Size.X / 2;
                pos.Y -= Size.Y / 2;
                return pos;
            }
        }

        public Vector OwnerPosition
        {
            get
            {
                var pos = m_owner.InnerBounds.GetCorner(m_compassPoint);
                return pos;
            }
            set
            {
                SetX(value.X);
                SetY(value.Y);
            }
        }

        private void SetX(float value)
        {
            float old;
            switch (m_compassPoint)
            {
                case CompassPoint.North:
                case CompassPoint.South:
                    break;
                case CompassPoint.NorthWest:
                case CompassPoint.WestNorthWest:
                case CompassPoint.West:
                case CompassPoint.WestSouthWest:
                case CompassPoint.SouthWest:
                default:
                    if (m_owner.Width - (value - m_owner.X) >= 1)
                    {
                        old = m_owner.X;
                        m_owner.Position = new Vector(value, m_owner.Position.Y);
                        m_owner.Size = new Vector(m_owner.Size.X - (m_owner.X - old), m_owner.Size.Y);
                    }
                    break;
                case CompassPoint.NorthEast:
                case CompassPoint.EastNorthEast:
                case CompassPoint.East:
                case CompassPoint.EastSouthEast:
                case CompassPoint.SouthEast:
                    if (value - m_owner.X >= 1)
                    {
                        m_owner.Size = new Vector(value - m_owner.X, m_owner.Size.Y);
                    }
                    break;
            }
        }

        private void SetY(float value)
        {
            float old;
            switch (m_compassPoint)
            {
                case CompassPoint.East:
                case CompassPoint.West:
                    break;
                case CompassPoint.NorthWest:
                case CompassPoint.NorthNorthWest:
                case CompassPoint.North:
                case CompassPoint.NorthNorthEast:
                case CompassPoint.NorthEast:
                    if (m_owner.Height - (value - m_owner.Y) >= 1)
                    {
                        old = m_owner.Y;
                        m_owner.Position = new Vector(m_owner.Position.X, value);
                        m_owner.Size = new Vector(m_owner.Size.X, m_owner.Size.Y - (m_owner.Y - old));
                    }
                    break;
                case CompassPoint.SouthWest:
                case CompassPoint.SouthSouthWest:
                case CompassPoint.South:
                case CompassPoint.SouthSouthEast:
                case CompassPoint.SouthEast:
                    if (value - m_owner.Y >= 1)
                    {
                        m_owner.Size = new Vector(m_owner.Size.X, value - m_owner.Y);
                    }
                    break;
            }
        }

        private Vector Size
        {
            get
            {
                return new Vector(Settings.HandleSize);
            }
        }

        private Rect Bounds
        {
            get { return new Rect(Position, Size); }
        }

        public bool HitTest(Vector pos)
        {
            return Bounds.Contains(pos);
        }

        public Cursor Cursor
        {
            get
            {
                switch (m_compassPoint)
                {
                    case CompassPoint.NorthWest:
                    case CompassPoint.SouthEast:
                        return Cursors.SizeNWSE;
                    case CompassPoint.NorthEast:
                    case CompassPoint.SouthWest:
                        return Cursors.SizeNESW;
                    case CompassPoint.North:
                    case CompassPoint.South:
                        return Cursors.SizeNS;
                    case CompassPoint.East:
                    case CompassPoint.West:
                        return Cursors.SizeWE;
                    default:
                        return null;
                }
            }
        }

        public void Draw(Canvas canvas, XGraphics graphics, Palette palette, DrawingContext context)
        {
            Drawing.DrawHandle(canvas, graphics, palette, Bounds, context, false, false);
        }

        private ISizeable m_owner;
        private CompassPoint m_compassPoint;
    }
}
