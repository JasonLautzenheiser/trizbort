using System.Linq;

namespace Trizbort
{
  public static class MapStatistics
  {
    public static int NumberOfRooms
    {
      get { return Project.Current.Elements.OfType<Room>().Count(); }
    }

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

    public static int NumberOfConnections
    {
      get { return Project.Current.Elements.OfType<Connection>().Count(); }
    }

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

    public static int NumberOfRegions
    {
      get { return Settings.Regions.Count(p=>p.RegionName != Region.DefaultRegion); }
    }
    public static int NumberOfRoomsInRegion(string pRegion)
    {
      return Project.Current.Elements.OfType<Room>().Count(p => p.Region == pRegion);
    }
  }
}