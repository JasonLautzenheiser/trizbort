using System.Collections.Generic;
using System.Linq;

namespace Trizbort
{
  public static class MapStatistics
  {
    public static int NumberOfRooms => Project.Current.Elements.OfType<Room>().Count();

    public static int NumberOfDarkRooms => Project.Current.Elements.OfType<Room>().Count(p => p.IsDark);

    public static int NumberOfStartRooms => Project.Current.Elements.OfType<Room>().Count(p => p.IsStartRoom);

    public static string StartRoomName
    {
      get { foreach (var x in Project.Current.Elements.OfType<Room>()) { if (x.IsStartRoom) { return x.Name; } } return "(None)"; }
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

            foreach (var y in x)
            {
                if ((y.GetSourceRoom() == null) || (y.GetTargetRoom() == null))
                    continue;
                if (y.Flow == ConnectionFlow.TwoWay)
                    if (y.GetSourceRoom() != y.GetTargetRoom()) // make sure it doesn't loop on itself
                        return false;
                else if (y.GetSourceRoom() == p) //if our room starts the one-way line, it's not a dead end
                    return false;
            }
            return true;
        });
      }
    }

    public static int NumberOfRegions
    {
      get { return Settings.Regions.Count(p=>p.RegionName != Region.DefaultRegion); }
    }

    public static int NumberOfRoomsInRegion(string pRegion)
    {
      return Project.Current.Elements.OfType<Room>().Count(p => p.Region == pRegion);
    }

    public static int NumberOfRoomsWithoutRegion()
    {
      return Project.Current.Elements.OfType<Room>().Count(p => p.Region == Region.DefaultRegion);
    }

  }
}