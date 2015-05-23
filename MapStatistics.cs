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

    public static int NumberOfTotalObjectsInRooms
    {
      get
      {
        int tCount = 0;

        foreach (var room in Project.Current.Elements.OfType<Room>())
        {
          var tObjects = room.Objects.Replace("\r", string.Empty).Replace("|", "\\|").Replace("\n", "|");
          var objects = tObjects.Split('|');
          tCount += objects.Count();
        }
        return tCount;
      }
    }

    public static int NumberOfConnections
    {
      get { return Project.Current.Elements.OfType<Connection>().Count(); }
    }

    public static int NumberOfDanglingConnections
    {
      get { return Project.Current.Elements.OfType<Connection>().Count(p => p.GetSourceRoom() == null || p.GetTargetRoom() == null); }
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