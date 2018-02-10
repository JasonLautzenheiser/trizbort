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
using Trizbort.Domain.Elements;

namespace Trizbort.Domain.Misc
{
    /// <summary>
    /// A vertex on a connection.
    /// </summary>
    /// <remarks>
    /// Connections are multi-segment lines between vertices.
    /// Each vertex is fixed either to a point in space or 
    /// to an element's port.
    /// </remarks>
    public class Vertex
    {
        public Vertex()
        {
        }

        public Vertex(Port port)
        {
            Port = port;
        }

        public Vertex(Vector position)
        {
            Position = position;
        }

        public Connection Connection
        {
            get; set;
        }

        public Vector Position
        {
            get
            {
                if (m_port != null)
                {
                    return m_port.Position;
                }
                return m_position; 
            }
            set
            {
                if (m_position != value)
                {
                    m_position = value;
                    m_port = null;
                    RaiseChanged();
                }
            }
        }

        public Port Port
        {
            get { return m_port; }
            set
            {
                if (m_port != value)
                {
                    m_position = Vector.Zero;
                    m_port = value;
                    RaiseChanged();
                }
            }
        }

        public event EventHandler Changed;

        private void RaiseChanged()
        {
            var changed = Changed;
            if (changed != null)
            {
                changed(this, EventArgs.Empty);
            }
        }

        private Vector m_position;
        private Port m_port;
    }
}
