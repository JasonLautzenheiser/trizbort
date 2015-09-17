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
using System.Diagnostics;

namespace Trizbort
{
  /// <summary>
  ///   A abstract point on the compass rose.
  /// </summary>
  public enum CompassPoint
  {
    North,
    NorthNorthEast,
    NorthEast,
    EastNorthEast,
    East,
    EastSouthEast,
    SouthEast,
    SouthSouthEast,
    South,
    SouthSouthWest,
    SouthWest,
    WestSouthWest,
    West,
    WestNorthWest,
    NorthWest,
    NorthNorthWest,


    Min = North,
    Max = NorthNorthWest
  }

  internal static class CompassPointHelper
  {
    private static readonly string[] Names =
    {
      "n",
      "nne",
      "ne",
      "ene",
      "e",
      "ese",
      "se",
      "sse",
      "s",
      "ssw",
      "sw",
      "wsw",
      "w",
      "wnw",
      "nw",
      "nnw"
    };

    public static bool ToName(CompassPoint point, out string name)
    {
      var index = (int) point;
      if (index >= 0 && index < Names.Length)
      {
        name = Names[index];
        return true;
      }
      name = string.Empty;
      return false;
    }

    public static bool FromName(string name, out CompassPoint point)
    {
      for (var index = 0; index < Names.Length; ++index)
      {
        if (StringComparer.InvariantCultureIgnoreCase.Compare(name ?? string.Empty, Names[index]) == 0)
        {
          point = (CompassPoint) index;
          return true;
        }
      }
      point = CompassPoint.North;
      return false;
    }

    /// <summary>
    ///   Rotate a point clockwise to find its neighbour on that side.
    /// </summary>
    public static CompassPoint RotateClockwise(CompassPoint point)
    {
      point = (CompassPoint) ((int) point + 1);
      if (point > CompassPoint.Max)
      {
        point = CompassPoint.Min;
      }
      return point;
    }

    /// <summary>
    ///   Rotate a point anti-clockwise to find its neighbour on that side.
    /// </summary>
    public static CompassPoint RotateAntiClockwise(CompassPoint point)
    {
      point = (CompassPoint) ((int) point - 1);
      if (point < CompassPoint.Min)
      {
        point = CompassPoint.Max;
      }
      return point;
    }

    /// <summary>
    ///   Get the geometric opposite of a compass point on the compass rose.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static CompassPoint GetOpposite(CompassPoint point)
    {
      for (var index = 0; index < (CompassPoint.Max - CompassPoint.Min + 1)/2; ++index)
      {
        point = RotateClockwise(point);
      }
      return point;
    }

    /// <summary>
    ///   Get the compass point which is the opposite of the given point,
    ///   for the purpose of connecting rooms during automapping.
    /// </summary>
    /// <remarks>
    ///   Treating the room as a box, and mapping compass points onto it,
    ///   the automap opposite is the corresponding point on the opposite
    ///   side of the box.
    /// </remarks>
    public static CompassPoint GetAutomapOpposite(CompassPoint point)
    {
      switch (point)
      {
        case CompassPoint.North:
          return CompassPoint.South;
        case CompassPoint.NorthNorthEast:
          return CompassPoint.SouthSouthEast; // vertical
        case CompassPoint.NorthEast:
          return CompassPoint.SouthWest;
        case CompassPoint.EastNorthEast:
          return CompassPoint.WestNorthWest; // horizontal
        case CompassPoint.East:
          return CompassPoint.West;
        case CompassPoint.EastSouthEast:
          return CompassPoint.WestSouthWest; // horizontal
        case CompassPoint.SouthEast:
          return CompassPoint.NorthWest;
        case CompassPoint.SouthSouthEast:
          return CompassPoint.NorthNorthEast; // vertical
        case CompassPoint.South:
          return CompassPoint.North;
        case CompassPoint.SouthSouthWest:
          return CompassPoint.NorthNorthWest; // vertical
        case CompassPoint.SouthWest:
          return CompassPoint.NorthEast;
        case CompassPoint.WestSouthWest:
          return CompassPoint.EastSouthEast; // horizontal
        case CompassPoint.West:
          return CompassPoint.East;
        case CompassPoint.WestNorthWest:
          return CompassPoint.EastNorthEast; // horizontal
        case CompassPoint.NorthWest:
          return CompassPoint.SouthEast;
        case CompassPoint.NorthNorthWest:
          return CompassPoint.SouthSouthWest; // vertical
        default:
          Debug.Assert(false, "Opposite compass point not found.");
          return CompassPoint.North;
      }
    }

    /// <summary>
    ///   Get the direction delta (where x and y are range -1 to 1) in which
    ///   we would place a new room given a connection in the given direction.
    /// </summary>
    /// <remarks>
    ///   Two compass points have the same direction vector if they are mapped
    ///   onto the the same side of a box drawn to represent the room.
    /// </remarks>
    public static Vector GetAutomapDirectionVector(CompassPoint compassPoint)
    {
      switch (compassPoint)
      {
        case CompassPoint.NorthNorthWest:
        case CompassPoint.North:
        case CompassPoint.NorthNorthEast:
          return new Vector(0, -1);
        case CompassPoint.NorthEast:
          return new Vector(1, -1);
        case CompassPoint.EastNorthEast:
        case CompassPoint.East:
        case CompassPoint.EastSouthEast:
          return new Vector(1, 0);
        case CompassPoint.SouthEast:
          return new Vector(1, 1);
        case CompassPoint.SouthSouthEast:
        case CompassPoint.South:
        case CompassPoint.SouthSouthWest:
          return new Vector(0, 1);
        case CompassPoint.SouthWest:
          return new Vector(-1, 1);
        case CompassPoint.WestSouthWest:
        case CompassPoint.West:
        case CompassPoint.WestNorthWest:
          return new Vector(-1, 0);
        case CompassPoint.NorthWest:
          return new Vector(-1, -1);
        default:
          Debug.Assert(false, "Direction vector not found.");
          return new Vector(0, -1);
      }
    }

    public static CompassPoint GetCompassPointFromAutomapDirectionVector(Vector vector)
    {
      if (vector.X < 0)
      {
        if (vector.Y < 0)
        {
          return CompassPoint.NorthWest;
        }
        if (vector.Y > 0)
        {
          return CompassPoint.SouthWest;
        }
        return CompassPoint.West;
      }
      if (vector.X > 0)
      {
        if (vector.Y < 0)
        {
          return CompassPoint.NorthEast;
        }
        if (vector.Y > 0)
        {
          return CompassPoint.SouthEast;
        }
        return CompassPoint.East;
      }
      if (vector.Y < 0)
      {
        return CompassPoint.North;
      }
      if (vector.Y > 0)
      {
        return CompassPoint.South;
      }

      Debug.Assert(false, "Automap direction vector should not be zero.");
      return CompassPoint.North;
    }

    /// <summary>
    ///   Convert an automap direction into a compass point.
    ///   Compass directions map directly; up/down/in/out are assigned specific other diretions.
    /// </summary>
    public static CompassPoint GetCompassDirection(AutomapDirection direction)
    {
      switch (direction)
      {
        case AutomapDirection.Up:
          return CompassPoint.NorthNorthWest;
        case AutomapDirection.Down:
          return CompassPoint.SouthSouthWest;
        case AutomapDirection.In:
          return CompassPoint.EastNorthEast;
        case AutomapDirection.Out:
          return CompassPoint.WestNorthWest;
        case AutomapDirection.North:
          return CompassPoint.North;
        case AutomapDirection.South:
          return CompassPoint.South;
        case AutomapDirection.East:
          return CompassPoint.East;
        case AutomapDirection.West:
          return CompassPoint.West;
        case AutomapDirection.NorthEast:
          return CompassPoint.NorthEast;
        case AutomapDirection.NorthWest:
          return CompassPoint.NorthWest;
        case AutomapDirection.SouthEast:
          return CompassPoint.SouthEast;
        case AutomapDirection.SouthWest:
          return CompassPoint.SouthWest;
        default:
          Debug.Assert(false, "Couldn't work out the compass direction for the given automap direction.");
          return CompassPoint.North;
      }
    }

    /// <summary>
    ///   "Round" the compass point to the nearest cardinal or ordinal direction.
    /// </summary>
    public static CompassPoint GetNearestCardinalOrOrdinal(CompassPoint compassPoint)
    {
      return GetCompassPointFromAutomapDirectionVector(GetAutomapDirectionVector(compassPoint));
    }


    public static double CalcRadianForEllipse(CompassPoint point, Rect rect)
    {
      var angleIncrement = 360.0/16.0;
      var i = getPointIntegerValue(point);
      return (i * angleIncrement) * (Math.PI / 180);
    }



    private static int getPointIntegerValue(CompassPoint point)
    {
      switch (point)
      {
        case CompassPoint.North:
          return 12;
        case CompassPoint.NorthNorthEast:
          return 13;
        case CompassPoint.NorthEast:
          return 14;
        case CompassPoint.EastNorthEast:
          return 15;
        case CompassPoint.East:
          return 0;
        case CompassPoint.EastSouthEast:
          return 1;
        case CompassPoint.SouthEast:
          return 2;
        case CompassPoint.SouthSouthEast:
          return 3;
        case CompassPoint.South:
          return 4;
        case CompassPoint.SouthSouthWest:
          return 5;
        case CompassPoint.SouthWest:
          return 6;
        case CompassPoint.WestSouthWest:
          return 7;
        case CompassPoint.West:
          return 8;
        case CompassPoint.WestNorthWest:
          return 9;
        case CompassPoint.NorthWest:
          return 10;
        case CompassPoint.NorthNorthWest:
          return 11;
        default:
          throw new ArgumentOutOfRangeException(nameof(point), point, null);
      }
    }

    /// <summary>
    ///   Get the literal opposite of any direction.
    /// </summary>
    public static AutomapDirection GetOpposite(AutomapDirection direction)
    {
      switch (direction)
      {
        case AutomapDirection.North:
          return AutomapDirection.South;
        case AutomapDirection.South:
          return AutomapDirection.North;
        case AutomapDirection.East:
          return AutomapDirection.West;
        case AutomapDirection.West:
          return AutomapDirection.East;
        case AutomapDirection.NorthEast:
          return AutomapDirection.SouthWest;
        case AutomapDirection.NorthWest:
          return AutomapDirection.SouthEast;
        case AutomapDirection.SouthEast:
          return AutomapDirection.NorthWest;
        case AutomapDirection.SouthWest:
          return AutomapDirection.NorthEast;
        case AutomapDirection.Up:
          return AutomapDirection.Down;
        case AutomapDirection.Down:
          return AutomapDirection.Up;
        case AutomapDirection.In:
          return AutomapDirection.Out;
        case AutomapDirection.Out:
          return AutomapDirection.In;
        default:
          Debug.Assert(false, "Couldn't work out the opposite of the given direction.");
          return AutomapDirection.North;
      }
    }
  }
}