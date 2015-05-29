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

      stats += string.Format("# of Rooms: {0}{1}", MapStatistics.NumberOfRooms, Environment.NewLine);
      stats += string.Format("# of Unconnected Rooms: {0}{1}", MapStatistics.NumberOfFloatingRooms, Environment.NewLine);
      
      stats += string.Format("{0}", Environment.NewLine);
      stats += string.Format("# of Connections: {0}{1}", MapStatistics.NumberOfConnections, Environment.NewLine);
      stats += string.Format("# of Dangling Connections: {0}{1}", MapStatistics.NumberOfDanglingConnections, Environment.NewLine);
      stats += string.Format("# of Self Looping Connections: {0}{1}", MapStatistics.NumberOfLoopingConnections, Environment.NewLine);

      stats += string.Format("{0}", Environment.NewLine);
      stats += string.Format("# of Regions: {0}{1}", MapStatistics.NumberOfRegions, Environment.NewLine);

      if (MapStatistics.NumberOfRegions > 0)
      {
        stats += string.Format("Regions:{0}", Environment.NewLine);
        foreach (var region in Settings.Regions.OrderBy(p=>p.RegionName).Where(p=>p.RegionName != Trizbort.Region.DefaultRegion))
        {
          stats += string.Format("{2}: Rooms:{0} {1}", MapStatistics.NumberOfRoomsInRegion(region.RegionName), Environment.NewLine, region.RegionName);
        }
      }

      stats += string.Format("{0}", Environment.NewLine);
      stats += string.Format("Total # Objects in All Rooms: {0}{1}", MapStatistics.NumberOfTotalObjectsInRooms, Environment.NewLine);
      stats += string.Format("Total # Rooms with Objects: {0}{1}", MapStatistics.NumberOfRoomsWithObjects, Environment.NewLine);


      txtStats.Text = stats;
      txtStats.SelectionStart = 1;
      txtStats.SelectionLength = 0;
    }
  }
}
