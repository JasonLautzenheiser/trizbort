using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Trizbort.UI
{
  public partial class MapStatisticsView : Form
  {
    public MapStatisticsView()
    {
      InitializeComponent();
    }

    public string MapStatisticsString()
    {
      string stats = string.Empty;

      stats += $"# of Rooms: {MapStatistics.NumberOfRooms}{Environment.NewLine}";
      stats += $"# of Dark Rooms: {MapStatistics.NumberOfDarkRooms}{Environment.NewLine}";
      stats += $"# of Unconnected Rooms: {MapStatistics.NumberOfFloatingRooms}{Environment.NewLine}";

      stats += $"{Environment.NewLine}";

      if (MapStatistics.NumberOfStartRooms == 1)
        stats += $"Start room = {MapStatistics.StartRoomName}";
      else if (MapStatistics.NumberOfStartRooms == 2)
        stats += $"More than one start room.";
      else
        stats += $"No start room.";
      stats += $"{Environment.NewLine}";

      if (MapStatistics.NumberOfEndRooms == 1)
        stats += $"End room = {MapStatistics.EndRoomName}";
      else if (MapStatistics.NumberOfEndRooms == 2)
        stats += $"More than one end room.";
      else
        stats += $"No end room.";
      stats += $"{Environment.NewLine}";

      stats += $"{Environment.NewLine}";
      stats += $"# of Connections: {MapStatistics.NumberOfConnections}, {MapStatistics.NumberOfOneWayConnections} one-way, {MapStatistics.NumberOfDottedConnections} dashed/dotted," +
        $"{MapStatistics.UpDown} up/down, {MapStatistics.InOut} in/out.{Environment.NewLine}";
      stats += $"# of Dangling Connections: {MapStatistics.NumberOfDanglingConnections}{Environment.NewLine}";
      stats += $"# of Self Looping Connections: {MapStatistics.NumberOfLoopingConnections}{Environment.NewLine}";

      stats += $"# of Dead Ends: {MapStatistics.NumberOfDeadEnds}{Environment.NewLine}";

      stats += $"{Environment.NewLine}";
      stats += string.Format("{0} door{1}, {2} locked, {3} open, {4} openable{5}",
        MapStatistics.NumberOfDoors, plur(MapStatistics.NumberOfDoors),
        MapStatistics.NumberOfLockedDoors, MapStatistics.NumberOfOpenDoors, MapStatistics.NumberOfOpenableDoors,
        Environment.NewLine);

      stats += $"{Environment.NewLine}";
      stats += $"# of Regions: {MapStatistics.NumberOfRegions}{Environment.NewLine}";
      stats += $"# of Rooms without a region: {MapStatistics.NumberOfRoomsWithoutRegion()}{Environment.NewLine}";

      if (MapStatistics.NumberOfRegions > 0)
      {
        stats += $"Regions:{Environment.NewLine}";
        foreach (var region in Settings.Regions.OrderBy(p=>p.RegionName).Where(p=>p.RegionName != Trizbort.Region.DefaultRegion))
        {
          var numberOfRoomsInRegion = MapStatistics.NumberOfRoomsInRegion(region.RegionName);
          stats += string.Format("{2} ({0} {3}){1}", numberOfRoomsInRegion, Environment.NewLine, region.RegionName,numberOfRoomsInRegion==1 ? "room" : "rooms");
        }
      }

      stats += $"{Environment.NewLine}";
      stats += $"Total # Objects in All Rooms: {MapStatistics.NumberOfTotalObjectsInRooms}{Environment.NewLine}";
      stats += $"Total # Rooms with Objects: {MapStatistics.NumberOfRoomsWithObjects}{Environment.NewLine}";

      return stats;
    }

    public string plur (int x)
    {
      return (x == 1 ? "" : "s");
    }

    public void MapStatisticsView_Export(object sender, EventArgs e)
    {
      var stats = MapStatisticsString();

      var curFile = Project.Current.FileName;
      string outFile = "";

      if (string.IsNullOrEmpty(curFile))
      {
        outFile = "c:\\temp\\default-log.txt";
      }
      else
      {
        if (curFile.Contains(".trizbort"))
          outFile = curFile.Replace(".trizbort", ".log");
        else
          outFile = curFile + ".trizbort";
      }
      var writer = new StreamWriter(outFile, false, Encoding.UTF8, 2 ^ 16);
      if (writer == null) { MessageBox.Show("Could not open" + outFile, "Write error"); }
      writer.Write(stats);
      writer.Close();
      MessageBox.Show("Wrote log to " + outFile, "Log file written");
    }

    private void MapStatisticsView_Load(object sender, EventArgs e)
    {
      txtStats.Text = MapStatisticsString();
      txtStats.SelectionStart = 1;
      txtStats.SelectionLength = 0;
    }
  }
}
