using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trizbort
{
  public partial class MapStatisticsView : Form
  {
    public MapStatisticsView()
    {
      InitializeComponent();
    }

    private void MapStatisticsView_Load(object sender, EventArgs e)
    {
      string stats = string.Empty;

      stats += $"# of Rooms: {MapStatistics.NumberOfRooms}{Environment.NewLine}";
      stats += $"# of Dark Rooms: {MapStatistics.NumberOfDarkRooms}{Environment.NewLine}";
      stats += $"# of Unconnected Rooms: {MapStatistics.NumberOfFloatingRooms}{Environment.NewLine}";
      
      stats += $"{Environment.NewLine}";
      stats += $"# of Connections: {MapStatistics.NumberOfConnections}{Environment.NewLine}";
      stats += $"# of Dangling Connections: {MapStatistics.NumberOfDanglingConnections}{Environment.NewLine}";
      stats += $"# of Self Looping Connections: {MapStatistics.NumberOfLoopingConnections}{Environment.NewLine}";

      stats += $"# of Dead Ends: {MapStatistics.NumberOfDeadEnds}{Environment.NewLine}";

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


      txtStats.Text = stats;
      txtStats.SelectionStart = 1;
      txtStats.SelectionLength = 0;
    }
  }
}
