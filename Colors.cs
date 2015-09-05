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
using System.Drawing;
using System.Windows.Forms;
using Trizbort.Extensions;

namespace Trizbort
{
  internal static class Colors
  {
    public static readonly int Canvas = 0;
    public static readonly int Fill = 1;
    public static readonly int Border = 2;
    public static readonly int Line = 3;
    public static readonly int SelectedLine = 4;
    public static readonly int HoverLine = 5;
    public static readonly int LargeText = 6;
    public static readonly int SmallText = 7;
    public static readonly int LineText = 8;
    public static readonly int Grid = 9;
    public static readonly int Count = 10;

    private static readonly string[] Names =
    {
      "canvas",
      "fill",
      "border",
      "line",
      "selectedLine",
      "hoverLine",
      "largeText",
      "smallText",
      "lineText",
      "grid"
    };

    public static bool ToName(int color, out string name)
    {
      if (color >= 0 && color < Names.Length)
      {
        name = Names[color];
        return true;
      }
      name = string.Empty;
      return false;
    }

    public static bool FromName(string name, out int color)
    {
      for (int index = 0; index < Names.Length; ++index)
      {
        if (StringComparer.InvariantCultureIgnoreCase.Compare(name ?? string.Empty, Names[index]) == 0)
        {
          color = index;
          return true;
        }
      }
      color = -1;
      return false;
    }

    public static Color ShowColorDialog(Color color, Form parent)
    {

      using (var dialog = new ColorDialog())
      {
        dialog.Color = color == Color.Transparent ? Color.White : color;

        if (dialog.ShowDialog(parent) == DialogResult.OK)
        {
          return dialog.Color;
        }
      }
      return color;
    }

    public static string SaveColor(Color colorAttribute)
    {
      if (colorAttribute == Color.Transparent)
        return string.Empty;

      var colorValue = colorAttribute.ToHex();
      return colorValue;
    }
  }
}
