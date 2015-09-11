using System.Collections.Generic;
using System.Linq;

namespace Trizbort
{
  public static class MapStatistics
  {
    public static int NumberOfRooms => Project.Current.Elements.OfType<Room>().Count();

    public static int NumberOfFloatingRooms
    {
      get { return Project.Current.Elements.OfType<Room>().Count(p=>p.GetConnections().Count==0); }
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
        return Project.Current.Elements.OfType<Room>().Count(p =>
        {
            var x = p.GetConnections();

            if (x.Count == 0) // if a room is isolated, it can't be a dead end
                return false;

            foreach (var y in x)
            {
                if (y.Flow == ConnectionFlow.TwoWay)
                    if (y.VertexList[0].Port.Owner != y.VertexList[1].Port.Owner) // make sure it doesn't loop on itself
                        return false;
                if (y.VertexList[0].Port.Owner == p) //if our room starts the one-way line, it's not a dead end
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