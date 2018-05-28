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

namespace Trizbort.Domain.Misc {
  /// <summary>
  ///   Numeric functions.
  /// </summary>
  internal static class Numeric {
    /// <summary>
    ///   A "small" number.
    /// </summary>
    public static readonly float Small = 0.001f;

    /// <summary>
    ///   Test whether two numbers are approximately equal.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>True if approximately equal; false otherwise.</returns>
    public static bool ApproxEqual(float a, float b) {
      return Distance(a, b) <= Small;
    }

    /// <summary>
    ///   Clamp a value within a specific (inclusive) range.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="min">The inclusive lower bound of the range.</param>
    /// <param name="max">The inclusive upper bound of the range.</param>
    /// <returns>The value, or the lower/upper bound of the range.</returns>
    public static float Clamp(float value, float min, float max) {
      if (min > max) return Math.Max(max, Math.Min(min, value));
      return Math.Max(min, Math.Min(max, value));
    }


    /// <summary>
    ///   Get the absolute distance between two numbers.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    /// <returns>The absolute (positive) distance between the two.</returns>
    public static float Distance(float a, float b) {
      return Math.Abs(b - a);
    }

    /// <summary>
    ///   Test whether a value is within an (inclusive) range.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="min">The inclusive lower bound of the range.</param>
    /// <param name="max">The inclusive upper bound of the range.</param>
    /// <returns>True if the value is in range; false otherwise.</returns>
    public static bool InRange(float value, float min, float max) {
      if (min > max) return value >= max && value <= min;
      return value >= min && value <= max;
    }

    /// <summary>
    ///   Swap two numbers.
    /// </summary>
    /// <param name="a">The first number.</param>
    /// <param name="b">The second number.</param>
    public static void Swap(ref float a, ref float b) {
      var temp = a;
      a = b;
      b = temp;
    }
  }
}