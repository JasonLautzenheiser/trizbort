using System;
using System.Collections.Generic;
using System.Linq;
using Trizbort.Domain;

namespace Trizbort
{
  public static class MapStatistics
  {
    public static int NumberOfRooms => Project.Current.Elements.OfType<Room>().Count();

    public static int NumberOfDarkRooms => Project.Current.Elements.OfType<Room>().Count(p => p.IsDark);

    public static int NumberOfStartRooms => Project.Current.Elements.OfType<Room>().Count(p => p.IsStartRoom);

    public static int NumberOfEndRooms => Project.Current.Elements.OfType<Room>().Count(p => p.IsEndRoom);

    public static int NumberOfRoomsWithSubtitles => Project.Current.Elements.OfType<Room>().Count(p => p.SubTitle != "");

    public static int NumberOfDescribedRooms => Project.Current.Elements.OfType<Room>().Count(p => p.HasDescription);

    public static int NumberOfOneWayConnections => Project.Current.Elements.OfType<Connection>().Count(p => p.Flow == ConnectionFlow.OneWay);

    public static int NumberOfDottedConnections => Project.Current.Elements.OfType<Connection>().Count(p => p.Style == ConnectionStyle.Dashed);

    public static int NumberOfDoors => Project.Current.Elements.OfType<Connection>().Count(p => p.Door != null);

    public static int NumberOfLockedDoors => Project.Current.Elements.OfType<Connection>().Count(p => p.Door != null && p.Door.Locked);

    public static int NumberOfLockableDoors => Project.Current.Elements.OfType<Connection>().Count(p => p.Door != null && p.Door.Lockable);

    public static int NumberOfOpenDoors => Project.Current.Elements.OfType<Connection>().Count(p => p.Door != null && p.Door.Open);

    public static int NumberOfOpenableDoors => Project.Current.Elements.OfType<Connection>().Count(p => p.Door != null && p.Door.Openable);

    public static int NumberOfEllipticalRooms => Project.Current.Elements.OfType<Room>().Count(p => p.Shape == RoomShape.Ellipse);

    public static int NumberOfRectangularRooms => Project.Current.Elements.OfType<Room>().Count(p => p.Shape == RoomShape.SquareCorners);

    public static int NumberOfRoundCornerRooms => Project.Current.Elements.OfType<Room>().Count(p => p.Shape == RoomShape.RoundedCorners);

    public static int NumberOfOctagonalRooms => Project.Current.Elements.OfType<Room>().Count(p => p.Shape == RoomShape.Octagonal);

    public static string StartRoomName
    {
      get
      {
        var room = Project.Current.Elements.OfType<Room>().First(p => p.IsStartRoom);
        return room != null ? room.Name : "(None)";
      }
    }

    public static string EndRoomName
    {
      get
      {
        var endRoomName = Project.Current.Elements.OfType<Room>().Where(p => p.IsEndRoom).Select(p=>p.Name).Aggregate((i, j) => i + ", " + j);
        return endRoomName;
      }
    }

    public static int NumberOfFloatingRooms
    {
      get { return Project.Current.Elements.OfType<Room>().Count(p =>
        {
            var x = p.GetConnections();

            if (x.Count == 0)
                return true;
            foreach (var y in x)
            {
                if ((y.GetSourceRoom() != null) && (y.GetTargetRoom() != null)) //first, ignore dangling connections
                {
                    if (y.GetSourceRoom() != y.GetTargetRoom()) //next, ignore looping connections
                        return false;
                }
            }
            return true;
        });
      }
    }

    public static int NumberOfRoomsWithObjects
    {
      get { return Project.Current.Elements.OfType<Room>().Count(p => p.ListOfObjects().Count > 0); }
    }

    public static int NumberOfRoomsWithXObjects(int objectNum, bool XOrMore)
    {
      return XOrMore ? Project.Current.Elements.OfType<Room>().Count(p => p.ListOfObjects().Count >= objectNum) :
        Project.Current.Elements.OfType<Room>().Count(p => p.ListOfObjects().Count == objectNum);
    }

    public static int NumberOfTotalObjectsInRooms
    {
      get { return Project.Current.Elements.OfType<Room>().ToList().Sum(p => p.ListOfObjects().Count); }
    }

    public static int NumberOfConnections => Project.Current.Elements.OfType<Connection>().Count();

    public static int NumberOfDanglingConnections
    {
      get { return Project.Current.Elements.OfType<Connection>().Count(p => p.GetSourceRoom() == null || p.GetTargetRoom() == null); }
    }

    public static int NumberOfLoopingConnections
    {
      get
      {
        return Project.Current.Elements.OfType<Connection>().Count(p =>
        {
          var sourceRoom = p.GetSourceRoom();
          var targetRoom = p.GetTargetRoom();
          return ((sourceRoom != null && targetRoom != null) && (sourceRoom == targetRoom));
        });
      }
    }

    public static int NumberOfDeadEnds
    {
      get
      {
        if (Project.Current.Elements.OfType<Room>().Count() == 1)
          return 1; // if there's only one room, it must be where you start, and so it is a dead end.

        return Project.Current.Elements.OfType<Room>().Count(p =>
        {
            var x = p.GetConnections();


            if (x.Count == 0) // if a room is isolated, it (usually) can't be a dead end...
            {
                if (p.IsStartRoom) // unless it's the the start room, since there is a way to get there...the game puts you there.
                    return true;
                return false;
            }

            // we are looking for dead ends and not isolated rooms, so we need to make sure there is a way into a room before calling it a dead end
            var waysIn = 0;

            foreach (var y in x)
            {
                if ((y.GetSourceRoom() == null) || (y.GetTargetRoom() == null))
                    continue; // in other words, a dangling connection does not allow a way in.
                if (y.Flow == ConnectionFlow.TwoWay)
                {
                    if (y.GetSourceRoom() != y.GetTargetRoom()) // make sure it doesn't loop on itself
                        return false;
                }
                else if (y.GetSourceRoom() == p) //if our room starts the one-way line, it's not a dead end
                    return false;
                else
                    waysIn++;
            }
            return waysIn > 0;
        });
      }
    }

    public static int UpDown
    {
        get {
        return Project.Current.Elements.OfType<Connection>().Count(p =>
        {
          if ((p.EndText == $"up") && (p.StartText == $"down"))
            return true;
          if ((p.EndText == $"down") && (p.StartText == $"up"))
            return true;
          return false;
        }
        );
      }
    }

    public static int CustomConnections
    {
      get
      {
        var labeled = Project.Current.Elements.OfType<Connection>().Count(p =>
        {
          if ((p.EndText != string.Empty) || (p.StartText != string.Empty))
            return true;
          return false;
        }
        );
        return labeled - InOut - UpDown;
      }
    }

    public static int HasMiddleText
    {
      get
      {
        return Project.Current.Elements.OfType<Connection>().Count(p =>
        {
          if (p.MidText != string.Empty)
            return true;
          return false;
        }
        );
      }
    }

    public static int UnlabeledConnections
    {
      get
      {
        return Project.Current.Elements.OfType<Connection>().Count(p =>
        {
        if ((p.VertexList[0].Connection.StartText != "") || (p.VertexList[1].Connection.EndText != ""))
          return true;
        return false;
        }
        );
      }
    }

    public static int DiagonalConnections(int checkval)
    {
        return Project.Current.Elements.OfType<Connection>().Count(p =>
        {
        var port1 = (Room.CompassPort)p.VertexList[0].Port;
        var port2 = (Room.CompassPort)p.VertexList[1].Port;

        var firstRoomConnectionDir = (port1 == null) ? 0 : (int)port1?.CompassPoint;
        var secondRoomConnectionDir = (port2 == null) ? 0 : (int)port2?.CompassPoint;

        var diags = 0;

        if ((firstRoomConnectionDir % 4 == 2) && (p.VertexList[0].Connection.StartText == ""))
          diags++;
        if ((secondRoomConnectionDir % 4 == 2) && (p.VertexList[1].Connection.EndText == ""))
          diags++;
        return (diags == checkval);
        }
        );

    }

    public static int BentConnections(bool ignoreAnnos)
    {
        return Project.Current.Elements.OfType<Connection>().Count(p =>
        {
        var port1 = (Room.CompassPort)p.VertexList[0].Port;
        var port2 = (Room.CompassPort)p.VertexList[1].Port;

        if (port1 == null || port2 == null)
          return false;

        var firstRoomConnectionDir = port1.CompassPoint;
        var secondRoomConnectionDir = port2.CompassPoint;

        if (!ignoreAnnos) // To ignore annotations means we count a bend no matter what, even if it has special start/end text
        {
          if ((p.VertexList[0].Connection.StartText != "") || (p.VertexList[1].Connection.EndText != ""))
          {
            return false;
          }
        }

        if (!Project.Current.Canvas.EqualEnough(firstRoomConnectionDir, CompassPointHelper.GetOpposite(secondRoomConnectionDir)))
        {
          var x = 1;
          x++;
        }
        // if the port directions are not (roughly) opposite, then we have a bent connection. Note NN(EW) = N, WW(NS) = W, etc.
        return (!Project.Current.Canvas.EqualEnough(firstRoomConnectionDir, CompassPointHelper.GetOpposite(secondRoomConnectionDir)));
        }
        );

        }

    public static bool roomHasDupConnection(Room rm, string dupString)
    {
      var dupes = 0;
      foreach (var element in rm.GetConnections())
      {
        if ((element.GetTargetRoom() == rm) && (element.EndText == dupString)) { dupes++; }
        if ((element.GetSourceRoom() == rm) && (element.StartText == dupString)) { dupes++; }
      }
      return (dupes > 1);
    }

    public static string dupConnectionList(string dupString)
    {
      var myList = Project.Current.Elements.OfType<Room>().ToArray().OrderBy(p=>p.Name).Where(p=>roomHasDupConnection(p, dupString));
      if (myList.Count() == 0) { return "No rooms with duplicate " + dupString + " exits."; }
      return "Rooms with duplicate " + dupString + " exits: " + string.Join(", ", myList.Select(x => x.Name).ToArray()) + ".";
    }

    public static int InOut
    {
      get
      {
        return Project.Current.Elements.OfType<Connection>().Count(p =>
        {
          if ((p.EndText == $"in") && (p.StartText == $"out"))
            return true;
          if ((p.EndText == $"out") && (p.StartText == $"in"))
            return true;
          return false;
        }
        );
      }
    }

    public static string DuplicateNamedRooms
    {
      get
      {
        var roomNameDictionary = new Dictionary<string, long>(StringComparer.InvariantCultureIgnoreCase);
        foreach (var rm in Project.Current.Elements.OfType<Room>())
          if (roomNameDictionary.ContainsKey(rm.Name))
            roomNameDictionary[rm.Name]++;
          else
            roomNameDictionary[rm.Name] = 1;

        var keysAndNums = roomNameDictionary.Where(p => p.Value > 1);
        if (keysAndNums.Count() == 0)
          return "None";

        var totalDupes = String.Join(", ", keysAndNums.Select(x => x.Key + "(" + x.Value + ")"));

        return totalDupes;
      }
    }

    public static bool RegionsLinked(Region r1, Region r2)
    {
      if (r1 == r2)
        return false;
      foreach (var room in Project.Current.Elements.OfType<Room>().ToArray().OrderBy(p => p.Name).Where(p => p.Region == r1.RegionName))
      {
        var roomConnections = room.GetConnections();

        foreach (var thisConnection in roomConnections)
        {
          if ((thisConnection.GetSourceRoom().Region == r2.RegionName) || (thisConnection.GetTargetRoom().Region == r2.RegionName))
            return true;
        }
      }
      return false;
    }

    public static int NumberOfRegions
    {
      get { return Settings.Regions.Count(p=>p.RegionName != Region.DefaultRegion && p.RegionName != string.Empty); }
    }

    public static int NumberOfRoomsInRegion(string pRegion)
    {
      return Project.Current.Elements.OfType<Room>().Count(p => p.Region == pRegion);
    }

    public static int NumberOfRoomsWithoutRegion()
    {
      return Project.Current.Elements.OfType<Room>().Count(p => p.Region == Region.DefaultRegion || p.Region == string.Empty);
    }

  }
}