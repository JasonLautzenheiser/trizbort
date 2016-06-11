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

using System.Windows.Forms;
using PdfSharp.Drawing;

namespace Trizbort
{
  /// <summary>
  ///   A visual handle by which an element may be resized.
  /// </summary>
  internal class ResizeHandle
  {
    private readonly CompassPoint mCompassPoint;
    private readonly ISizeable mOwner;

    public ResizeHandle(CompassPoint compassPoint, ISizeable owner)
    {
      mCompassPoint = compassPoint;
      mOwner = owner;
    }

    public Vector Position
    {
      get
      {
        var tBounds = mOwner.InnerBounds;
        
        var pos = tBounds.GetCorner(mCompassPoint);
        if (mOwner is Room)
        {
           pos = tBounds.GetCorner(mCompassPoint, ((Room)mOwner).Shape, ((Room)mOwner).Corners);
        }
        pos.X -= size.X/2;
        pos.Y -= size.Y/2;
        return pos;
      }
    }

    public Vector OwnerPosition
    {
      get
      {
        var pos = mOwner.InnerBounds.GetCorner(mCompassPoint);
        return pos;
      }
      set
      {
        setX(value.X);
        setY(value.Y);
      }
    }

    private static Vector size => new Vector(Settings.HandleSize);

    private Rect  bounds => new Rect(Position, size);

    public Cursor Cursor
    {
      get
      {
        switch (mCompassPoint)
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

    private void setX(float value)
    {
      switch (mCompassPoint)
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
          if (mOwner.Width - (value - mOwner.X) >= 1)
          {
            var old = mOwner.X;
            mOwner.Position = new Vector(value, mOwner.Position.Y);
            mOwner.Size = new Vector(mOwner.Size.X - (mOwner.X - old), mOwner.Size.Y);
          }
          break;
        case CompassPoint.NorthEast:
        case CompassPoint.EastNorthEast:
        case CompassPoint.East:
        case CompassPoint.EastSouthEast:
        case CompassPoint.SouthEast:
          if (value - mOwner.X >= 1)
          {
            mOwner.Size = new Vector(value - mOwner.X, mOwner.Size.Y);
          }
          break;
      }
    }

    private void setY(float value)
    {
      switch (mCompassPoint)
      {
        case CompassPoint.East:
        case CompassPoint.West:
          break;
        case CompassPoint.NorthWest:
        case CompassPoint.NorthNorthWest:
        case CompassPoint.North:
        case CompassPoint.NorthNorthEast:
        case CompassPoint.NorthEast:
          if (mOwner.Height - (value - mOwner.Y) >= 1)
          {
            var old = mOwner.Y;
            mOwner.Position = new Vector(mOwner.Position.X, value);
            mOwner.Size = new Vector(mOwner.Size.X, mOwner.Size.Y - (mOwner.Y - old));
          }
          break;
        case CompassPoint.SouthWest:
        case CompassPoint.SouthSouthWest:
        case CompassPoint.South:
        case CompassPoint.SouthSouthEast:
        case CompassPoint.SouthEast:
          if (value - mOwner.Y >= 1)
          {
            mOwner.Size = new Vector(mOwner.Size.X, value - mOwner.Y);
          }
          break;
      }
    }

    public bool HitTest(Vector pos)
    {
      return bounds.Contains(pos);
    }

    public void Draw(UI.Controls.Canvas canvas, XGraphics graphics, Palette palette, DrawingContext context)
    {
      Drawing.DrawHandle(canvas, graphics, palette, bounds, context, false, false);
    }
  }
}