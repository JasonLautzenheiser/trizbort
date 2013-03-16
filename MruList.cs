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
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Trizbort
{
    class MruList : IEnumerable<string>
    {
        public const int MaxItems = 5;

        public void Add(string item)
        {
            Insert(0, item);
        }

        public void Append(string item)
        {
            Insert(m_list.Count, item);
        }

        public void Insert(int index, string item)
        {
            if (string.IsNullOrEmpty(item))
            {
                return;
            }

            foreach (var existing in m_list)
            {
                if (StringComparer.InvariantCultureIgnoreCase.Compare(existing, item) == 0)
                {
                    // already have it; prepare to bump it to the top
                    m_list.Remove(existing);
                    break;
                }
            }

            m_list.Insert(index, item);
            while (m_list.Count > MaxItems)
            {
                m_list.RemoveAt(m_list.Count - 1);
            }
        }

        public void Remove(string item)
        {
            m_list.Remove(item);
        }

        public void Clear()
        {
            m_list.Clear();
        }

        public int Count
        {
            get { return m_list.Count; }
        }

        public string this[int index]
        {
            get { return m_list[index]; }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return m_list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_list.GetEnumerator();
        }

        private List<string> m_list = new List<string>();
    }
}
