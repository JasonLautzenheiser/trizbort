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
using System.Diagnostics;
using Trizbort.Automap;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Domain.Misc;

namespace Trizbort.Export.Domain; 

public class Exit {
  // The priority of the this exit's primary direction, compared to other exits which may go in the same direction from
  // the same room.
  // 
  // Since multiple exits may lead the same way from the same room, priorities are
  // used to decide which exit is the "best" exit in any direction.
  // For example, a northerly exit which is docked to the N compass point and which
  // does not go up, down, in or out is a higher priority than a northerly exit
  // docked to the NNE compass point and which also goes up.
  private int mPrimaryPriority;

  public Exit(Location source, Location target, CompassPoint visualCompassPoint, string connectionText, Connection connection) {
    Source = source;
    Target = target;
    VisualCompassPoint = visualCompassPoint;
    Door = connection.Door;
    ConnectionName = connection.Name;
    ConnectionDescription = connection.Description;
    Conditional = connection.Style == ConnectionStyle.Dashed;

    assignPrimaryPriority();
    assignSecondaryDirection(connectionText);
    if (SecondaryDirection != null)
      PrimaryDirection = (MappableDirection) SecondaryDirection;
    else
      assignPrimaryDirection();
  }

  //   True if this exit requires some in-game action from the player to be used; false otherwise.
  public bool Conditional { get; }
  public string ConnectionDescription { get; }
  public string ConnectionName { get; }

  public Door Door { get; }

  //   True if this exit has been exported; false otherwise.
  public bool Exported { get; set; }

  //   The primary direction of this exit: N, S, E, W, NE, NW, SE, SW.
  //   Deduced from VisualCompassPoint.
  public MappableDirection PrimaryDirection { get; private set; }

  //   The secondary direction of this exit, if any: either up, down, in or out.
  public MappableDirection? SecondaryDirection { get; private set; }

  //  The room from which this exit leads.
  public Location Source { get; }

  //  The room to which this exit leads.
  public Location Target { get; }

  //  The compass point in Trizbort at which this exit is docked to the starting room.
  //  Naturally this may include compass points such as SouthSouthWest need to be
  //  translated into an exportable direction; see PrimaryDirection and SecondaryDirection.
  public CompassPoint VisualCompassPoint { get; }

  //   Get the priority of the exit, in the given direction, with respect to other exits.
  //   Higher priorities indicate more suitable exits.
  public int GetPriority(MappableDirection direction) {
    if (direction == PrimaryDirection) return mPrimaryPriority;
    if (direction == SecondaryDirection) return 1;
    return -1;
  }

  //  Test whether an exit is reciprocated in the other direction; i.e. is there a bidirectional connection.
  public static bool IsReciprocated(Location source, MappableDirection direction, Location target) {
    if (target != null) {
      var oppositeDirection = CompassPointHelper.GetOpposite(direction);
      var reciprocal = target.GetBestExit(oppositeDirection);
      if (reciprocal != null) {
        Debug.Assert(reciprocal.PrimaryDirection == oppositeDirection || reciprocal.SecondaryDirection == oppositeDirection, "Alleged opposite direction appears to lead somewhere else. Something went wrong whilst building the set of exits from each room.");
        return reciprocal.Target == source;
      }
    }

    return false;
  }

  private void assignPrimaryDirection() {
    switch (VisualCompassPoint) {
      case CompassPoint.NorthNorthWest:
      case CompassPoint.North:
      case CompassPoint.NorthNorthEast:
        PrimaryDirection = MappableDirection.North;
        break;
      case CompassPoint.NorthEast:
        PrimaryDirection = MappableDirection.NorthEast;
        break;
      case CompassPoint.EastNorthEast:
      case CompassPoint.East:
      case CompassPoint.EastSouthEast:
        PrimaryDirection = MappableDirection.East;
        break;
      case CompassPoint.SouthEast:
        PrimaryDirection = MappableDirection.SouthEast;
        break;
      case CompassPoint.SouthSouthEast:
      case CompassPoint.South:
      case CompassPoint.SouthSouthWest:
        PrimaryDirection = MappableDirection.South;
        break;
      case CompassPoint.SouthWest:
        PrimaryDirection = MappableDirection.SouthWest;
        break;
      case CompassPoint.WestSouthWest:
      case CompassPoint.West:
      case CompassPoint.WestNorthWest:
        PrimaryDirection = MappableDirection.West;
        break;
      case CompassPoint.NorthWest:
        PrimaryDirection = MappableDirection.NorthWest;
        break;
      default:
        throw new InvalidOperationException("Unexpected compass point found on ");
    }
  }

  private void assignPrimaryPriority() {
    mPrimaryPriority = 0;

    switch (VisualCompassPoint) {
      case CompassPoint.North:
      case CompassPoint.South:
      case CompassPoint.East:
      case CompassPoint.West:
      case CompassPoint.NorthEast:
      case CompassPoint.SouthEast:
      case CompassPoint.SouthWest:
      case CompassPoint.NorthWest:
        if (SecondaryDirection == null)
          mPrimaryPriority += 4;
        else
          mPrimaryPriority -= 2;
        break;
      default:
        if (SecondaryDirection == null)
          mPrimaryPriority += 3;
        else
          mPrimaryPriority -= 1;
        break;
    }
  }

  private void assignSecondaryDirection(string connectionText) {
    switch (connectionText) {
      case Connection.Up:
        SecondaryDirection = MappableDirection.Up;
        break;
      case Connection.Down:
        SecondaryDirection = MappableDirection.Down;
        break;
      case Connection.In:
        SecondaryDirection = MappableDirection.In;
        break;
      case Connection.Out:
        SecondaryDirection = MappableDirection.Out;
        break;
      default:
        SecondaryDirection = null;
        break;
    }
  }
}