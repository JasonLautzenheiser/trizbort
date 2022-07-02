using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Setup;

namespace Trizbort.UI; 

public partial class MapStatisticsView : Form {
  public MapStatisticsView() {
    InitializeComponent();
  }

  private string mapStatisticsString() {
    var stats = string.Empty;

    if (MapStatistics.NumberOfRooms == 0) return "No rooms to take stats of.";

    stats += $"# of Rooms: {MapStatistics.NumberOfRooms}{Environment.NewLine}";
    stats += $"# of Dark Rooms: {MapStatistics.NumberOfDarkRooms}{Environment.NewLine}";
    stats += $"# of Unconnected Rooms: {MapStatistics.NumberOfFloatingRooms}{Environment.NewLine}";

    stats += $"{Environment.NewLine}";

    if (MapStatistics.NumberOfStartRooms == 1)
      stats += $"Start room = {MapStatistics.StartRoomName}";
    else if (MapStatistics.NumberOfStartRooms > 1)
      stats += "More than one start room.";
    else
      stats += "No start room.";
    stats += $"{Environment.NewLine}";

    if (MapStatistics.NumberOfEndRooms == 1)
      stats += $"End room = {MapStatistics.EndRoomName}";
    else if (MapStatistics.NumberOfEndRooms > 1)
      stats += $"End rooms = ({MapStatistics.EndRoomName})";
    else
      stats += "No end room.";
    stats += $"{Environment.NewLine}";

    var canvasBounds = Project.Current.Canvas.ComputeCanvasBounds(true);
    stats += $"{Environment.NewLine}Dimensions with margins: height {canvasBounds.Bottom - canvasBounds.Top}, width {canvasBounds.Right - canvasBounds.Left}{Environment.NewLine}";

    canvasBounds = Project.Current.Canvas.ComputeCanvasBounds(false);
    stats += $"Dimensions without margins: height {canvasBounds.Bottom - canvasBounds.Top}, width {canvasBounds.Right - canvasBounds.Left}{Environment.NewLine}";

    stats += $"{Environment.NewLine}";
    stats += $"# of Connections: {MapStatistics.NumberOfConnections} total, {MapStatistics.UnlabeledConnections} unlabeled, " +
             $"{MapStatistics.NumberOfOneWayConnections} one-way, {MapStatistics.NumberOfDottedConnections} dashed/dotted, " +
             $"{MapStatistics.UpDown} up/down, {MapStatistics.InOut} in/out.{Environment.NewLine}";
    stats += $"# of Dangling Connections: {MapStatistics.NumberOfDanglingConnections}{Environment.NewLine}";
    stats += $"# of Self Looping Connections: {MapStatistics.NumberOfLoopingConnections}{Environment.NewLine}";

    stats += $"# of Dead Ends: {MapStatistics.NumberOfDeadEnds}{Environment.NewLine}";

    stats += $"{Environment.NewLine}";
    stats += $"{MapStatistics.NumberOfDoors} door{plur(MapStatistics.NumberOfDoors)}, {MapStatistics.NumberOfLockedDoors} locked, {MapStatistics.NumberOfLockableDoors} lockable, {MapStatistics.NumberOfOpenDoors} open, {MapStatistics.NumberOfOpenableDoors} openable{Environment.NewLine}";

    stats += $"{Environment.NewLine}";
    stats += $"# of Regions: {MapStatistics.NumberOfRegions}{Environment.NewLine}";
    stats += $"# of Rooms without a region: {MapStatistics.NumberOfRoomsWithoutRegion()}{Environment.NewLine}";

    if (MapStatistics.NumberOfRegions > 0) {
      stats += $"Regions:{Environment.NewLine}";
      foreach (var region in Settings.Regions.OrderBy(p => p.RegionName).Where(p => p.RegionName != Domain.Misc.Region.DefaultRegion)) {
        var numberOfRoomsInRegion = MapStatistics.NumberOfRoomsInRegion(region.RegionName);
        stats += string.Format("{2} ({0} {3}){1}", numberOfRoomsInRegion, Environment.NewLine, region.RegionName, numberOfRoomsInRegion == 1 ? "room" : "rooms");
      }
    }

    stats += $"{Environment.NewLine}";
    stats += $"Total # Objects in All Rooms: {MapStatistics.NumberOfTotalObjectsInRooms}{Environment.NewLine}";
    stats += $"Total # Rooms with Objects: {MapStatistics.NumberOfRoomsWithObjects}, {MapStatistics.NumberOfRoomsWithXObjects(1, false)} with 1, " +
             $"{MapStatistics.NumberOfRoomsWithXObjects(2, false)} with 2, {MapStatistics.NumberOfRoomsWithXObjects(3, true)} with 3+{Environment.NewLine}";

    stats += $"{Environment.NewLine}";
    stats += $"Total # of Rooms with subtitles: {MapStatistics.NumberOfRoomsWithSubtitles}{Environment.NewLine}";
    stats += $"Total # of Rooms with descriptions: {MapStatistics.NumberOfDescribedRooms}{Environment.NewLine}";

    stats += $"{Environment.NewLine}{new string('=', 30)}Odd stuff below here{Environment.NewLine}";

    stats += $"{Environment.NewLine}Duplicate rooms: {MapStatistics.DuplicateNamedRooms}.{Environment.NewLine}";

    stats += $"{Environment.NewLine}Room shapes: {MapStatistics.NumberOfRectangularRooms} rectangular, {MapStatistics.NumberOfEllipticalRooms} elliptical, " +
             $"{MapStatistics.NumberOfRoundCornerRooms} round cornered, {MapStatistics.NumberOfOctagonalRooms} octagonal.{Environment.NewLine}";

    if (MapStatistics.NumberOfRegions > 0) {
      stats += $"{Environment.NewLine}";
      foreach (var region in Settings.Regions.OrderBy(p => p.RegionName).Where(p => p.RegionName != Domain.Misc.Region.DefaultRegion)) {
        if (MapStatistics.NumberOfRoomsInRegion(region.RegionName) == 0)
          stats += $"{region.RegionName} has no rooms.";
        else if (MapStatistics.NumberOfRoomsInRegion(region.RegionName) == 1)
          stats += $"Only room in {region.RegionName}: ";
        else
          stats += $"List of rooms in {region.RegionName}: ";

        var roomsInRegion = Project.Current.Elements.OfType<Room>().ToArray().OrderBy(p => p.Name).Where(p => p.Region == region.RegionName);
        stats += string.Join(", ", roomsInRegion.Select(x => x.Name).ToArray()) + Environment.NewLine;
      }

      stats += $"{Environment.NewLine}";
    } else if (MapStatistics.NumberOfRegions > 1) {
      stats += $"{Environment.NewLine}Region Links:{Environment.NewLine}";
      var allRegions = Settings.Regions.OrderBy(p => p.RegionName).Where(p => p.RegionName != Domain.Misc.Region.DefaultRegion);
      foreach (var region1 in allRegions) {
        var linkedRegions = string.Join(", ", Settings.Regions.OrderBy(p => p.RegionName).ToArray().Where(p => MapStatistics.RegionsLinked(region1, p)).Select(x => x.RegionName));

        stats += $"{Environment.NewLine}";

        if (linkedRegions == string.Empty)
          stats += $"{region1.RegionName} is not linked to any other regions.";
        else
          stats += $"{region1.RegionName} is linked to {linkedRegions}.";
      }

      stats += $"{Environment.NewLine}";
    } else {
      stats += $"{Environment.NewLine}";
    }

    stats += $"{MapStatistics.UpDown} up-down connection{plur(MapStatistics.UpDown)}{Environment.NewLine}";
    stats += $"{MapStatistics.InOut} in-out connection{plur(MapStatistics.InOut)}{Environment.NewLine}";
    stats += $"{MapStatistics.CustomConnections} custom connection{plur(MapStatistics.CustomConnections)}{Environment.NewLine}";
    stats += $"{MapStatistics.DiagonalConnections(2)} diagonal connection{plur(MapStatistics.DiagonalConnections(2))} (2-way){Environment.NewLine}";
    stats += $"{MapStatistics.DiagonalConnections(1)} diagonal connection{plur(MapStatistics.DiagonalConnections(1))} (1-way){Environment.NewLine}";
    stats += $"{MapStatistics.HasMiddleText} connection{plur(MapStatistics.HasMiddleText)} with middle text{Environment.NewLine}";
    stats += $"{MapStatistics.BentConnections(true)} bent connection{plur(MapStatistics.BentConnections(true))}, {MapStatistics.BentConnections(false)} with no text{Environment.NewLine}";
    stats += Environment.NewLine;

    stats += MapStatistics.DupConnectionList("in") + Environment.NewLine;
    stats += MapStatistics.DupConnectionList("out") + Environment.NewLine;
    stats += MapStatistics.DupConnectionList("up") + Environment.NewLine;
    stats += MapStatistics.DupConnectionList("down") + Environment.NewLine;

    return stats;
  }

  public void MapStatisticsView_Export(object sender, EventArgs e) {
    var stats = mapStatisticsString();

    var curFile = Project.Current.FileName;
    string outFile;

    if (string.IsNullOrEmpty(curFile)) {
      outFile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\trizbort.log";
    } else {
      if (curFile.Contains(".trizbort"))
        outFile = curFile.Replace(".trizbort", ".log");
      else
        outFile = curFile + ".trizbort";
    }

    var writer = new StreamWriter(outFile, false, Encoding.UTF8, 2 ^ 16);
    writer.Write(stats);
    writer.Close();
    MessageBox.Show("Wrote log to " + outFile, "Log file written");
  }

  private string plur(int x) {
    return x == 1 ? "" : "s";
  }

  private void MapStatisticsView_Load(object sender, EventArgs e) {
    txtStats.Text = mapStatisticsString();
    txtStats.SelectionStart = 1;
    txtStats.SelectionLength = 0;
  }
}