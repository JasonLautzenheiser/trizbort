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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Trizbort
{
  public class BoundList<T> : Collection<T>
    {
        public void AddRange<U>(IEnumerable<U> list) where U : T
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public void RemoveRange<U>(IEnumerable<U> list) where U : T
        {
            foreach (var item in list)
            {
                Remove(item);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            RaiseAdded(item);
        }

        protected override void RemoveItem(int index)
        {
            var element = Items[index];
            base.RemoveItem(index);
            RaiseRemoved(element);
        }

        public void Reverse()
        {
            var list = new List<T>(this);
            list.Reverse();
            base.Clear();
            foreach (var item in list)
            {
                base.Add(item);
            }
        }

        public event ItemEventHandler<T> Added;
        public event ItemEventHandler<T> Removed;

        private void RaiseAdded(T item)
        {
            var added = Added;
            if (added != null)
            {
                added(this, new ItemEventArgs<T>(item));
            }
        }

        private void RaiseRemoved(T item)
        {
            var removed = Removed;
            if (removed != null)
            {
                removed(this, new ItemEventArgs<T>(item));
            }
        }
    }
}
