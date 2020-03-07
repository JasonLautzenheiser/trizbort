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

using System.Collections.Generic;
using Trizbort.Automap;
using Trizbort.Domain;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;

namespace Trizbort.Export.Domain
{
    public class Location {
      private readonly List<Exit> mExits = new List<Exit>();
      private readonly Dictionary<MappableDirection, Exit> mMapDirectionToBestExit = new Dictionary<MappableDirection, Exit>();

      public Location(Room room, string exportName) {
        Room = room;
        ExportName = exportName;
      }

      public string ExportName { get; }

      public Room Room { get; }

      public List<Thing> Things { get; } = new List<Thing>();

      public void AddExit(Exit exit) {
        mExits.Add(exit);
      }

      public Exit GetBestExit(MappableDirection direction) {
        return mMapDirectionToBestExit.TryGetValue(direction, out var exit) ? exit : null;
      }

      public void PickBestExits() {
        mMapDirectionToBestExit.Clear();
        foreach (var direction in Directions.AllDirections) {
          var exit = pickBestExit(direction);
          if (exit != null) mMapDirectionToBestExit.Add(direction, exit);
        }
      }

      private Exit pickBestExit(MappableDirection direction) {
        // sort exits by priority for this direction only
        mExits.Sort((a, b) => {
          var one = a.GetPriority(direction);
          var two = b.GetPriority(direction);
          return two - one;
        });

        // pick the highest priority exit if its direction matches;
        // if the highest priority exit's direction doesn't match,
        // there's no exit in this direction.
        if (mExits.Count > 0) {
          var exit = mExits[0];
          if (exit.PrimaryDirection == direction || exit.SecondaryDirection == direction) return exit;
        }

        return null;
      }
    }
  }
