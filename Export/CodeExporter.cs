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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;
using Trizbort.Export.Domain;
using Trizbort.Setup;
using Trizbort.Util;

namespace Trizbort.Export; 

public abstract partial class CodeExporter : IDisposable {
  private readonly Dictionary<Room, Location> mMapRoomToLocation = new Dictionary<Room, Location>();
  protected Enum? Dialect = null;
  protected CodeExporter() {
    LocationsInExportOrder = new List<Location>();
    RegionsInExportOrder = new List<ExportRegion>();
  }

  public abstract List<KeyValuePair<string, string>> FileDialogFilters { get; }

  public abstract string FileDialogTitle { get; }

  protected List<Location> LocationsInExportOrder { get; }

  protected List<ExportRegion> RegionsInExportOrder { get; }

  protected abstract IEnumerable<string> ReservedWords { get; }

  public void Dispose() {
    Dispose(true);
  }

  public string Export(Enum? dialect = default) {
    this.Dialect = dialect;
    using var writer = new StringWriter();
    var title = Project.Current.Title;
    if (string.IsNullOrEmpty(title)) {
      title = PathHelper.SafeGetFilenameWithoutExtension(Project.Current.FileName);
      if (string.IsNullOrEmpty(title)) title = "A Trizbort Map";
    }

    var author = Project.Current.Author;
    if (string.IsNullOrEmpty(author)) author = "A Trizbort User";
    var history = Project.Current.History;

    prepareContent();
    ExportHeader(writer, title, author, Project.Current.Description ?? string.Empty, history);
    ExportContent(writer);

    var ss = writer.ToString();

    return ss;
  }

  

  public void Export(string fileName, Enum? dialect = default) {
    this.Dialect = dialect;
    using var writer = Create(fileName);
    var title = Project.Current.Title;
    if (string.IsNullOrEmpty(title)) {
      title = PathHelper.SafeGetFilenameWithoutExtension(Project.Current.FileName);
      if (string.IsNullOrEmpty(title)) title = "A Trizbort Map";
    }

    var author = Project.Current.Author;
    if (string.IsNullOrEmpty(author)) author = "A Trizbort User";

    var history = Project.Current.History;
    prepareContent();
    ExportHeader(writer, title, author, Project.Current.Description ?? string.Empty, history);
    ExportContent(writer);
  }

  protected virtual StreamWriter Create(string fileName) {
    return new StreamWriter(fileName, false, Encoding.ASCII, 2 ^ 16);
  }

  protected virtual void Dispose(bool disposing) { }

  protected abstract void ExportContent(TextWriter writer);

  protected abstract void ExportHeader(TextWriter writer, string title, string author, string description,
                                       string history);

  protected abstract string GetExportName(Room room, int? suffix);
  protected abstract string GetExportName(string displayName, int? suffix);

  private void findExits() {
    // find the exits from each room,
    // file them by room, and assign them priorities.
    // don't decide yet which exit is "the" from a room in a particular direction,
    // since we need to compare all a room's exits for that.
    foreach (var connection in Project.Current.Elements.OfType<Connection>()) {
      var sourceRoom = connection.GetSourceRoom(out var sourceCompassPoint);
      var targetRoom = connection.GetTargetRoom(out var targetCompassPoint);

      if (sourceRoom == null || targetRoom == null) continue;

      if (sourceRoom == targetRoom && sourceCompassPoint == targetCompassPoint) continue;

      if (mMapRoomToLocation.TryGetValue(sourceRoom, out var sourceLocation) &&
          mMapRoomToLocation.TryGetValue(targetRoom, out var targetLocation)) {
        sourceLocation.AddExit(new Exit(sourceLocation, targetLocation, sourceCompassPoint, connection.StartText,
          connection));

        if (connection.Flow == ConnectionFlow.TwoWay)
          targetLocation.AddExit(new Exit(targetLocation, sourceLocation, targetCompassPoint, connection.EndText,
            connection));
      }
    }
  }

  private void findRegions() {
    var mapExportNameToRegion = new Dictionary<string, Region>(StringComparer.InvariantCultureIgnoreCase);

    foreach (var reservedWord in ReservedWords)
      mapExportNameToRegion.Add(reservedWord, null);

    foreach (var region in Settings.Regions.Where(p => p.RegionName != Region.DefaultRegion)) {
      var exportName = GetExportName(region.RegionName, null);
      if (exportName == string.Empty)
        exportName = "region";

      var index = 2;
      while (mapExportNameToRegion.ContainsKey(exportName))
        exportName = GetExportName(region.RegionName, index++);

      mapExportNameToRegion[exportName] = region;
      RegionsInExportOrder.Add(new ExportRegion(region, exportName));
    }
  }

  private void findRooms() {
    var mapExportNameToRoom = new Dictionary<string, Room>(StringComparer.InvariantCultureIgnoreCase);

    // prevent use of reserved words
    foreach (var reservedWord in ReservedWords) mapExportNameToRoom.Add(reservedWord, null);

    foreach (var region in RegionsInExportOrder) mapExportNameToRoom.Add(region.ExportName, null);

    foreach (var element in Project.Current.Elements.OfType<Room>()) {
      var room = element;

      // assign each room a unique export name.
      var exportName = GetExportName(room, null);
      if (exportName == string.Empty)
        exportName = "object";
      var index = 2;
      while (mapExportNameToRoom.ContainsKey(exportName)) exportName = GetExportName(room, index++);

      mapExportNameToRoom[exportName] = room;
      var location = new Location(room, exportName);
      LocationsInExportOrder.Add(location);
      mMapRoomToLocation[room] = location;
    }
  }

  private void findThings() {
    var mapExportNameToThing = new Dictionary<string, Thing>(StringComparer.InvariantCultureIgnoreCase);

    // prevent use of reserved words
    foreach (var reservedWord in ReservedWords) mapExportNameToThing.Add(reservedWord, null);

    foreach (var rooms in LocationsInExportOrder) mapExportNameToThing.Add(rooms.ExportName, null);

    foreach (var region in RegionsInExportOrder) mapExportNameToThing.Add(region.ExportName, null);

    foreach (var location in LocationsInExportOrder) {
      var objectsText = location.Room.Objects;
      if (string.IsNullOrEmpty(objectsText)) continue;

      var objectNames = objectsText.Replace("\r", string.Empty)
                                   .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
      foreach (var objectName in objectNames) {
        // the display name is simply the object name without indentation and without and trailing []
        var displayName = objectName.Trim();

        var propString = "";

        var rgx = new Regex(@"\[[^\]\[]*\]");

        var match = rgx.Match(displayName);

        if (match.Success) {
          propString = displayName;
          displayName = rgx.Replace(displayName, "");
          var rgx2 = new Regex(@".*\[");
          propString = rgx2.Replace(propString, "");
          rgx2 = new Regex(@"\].*");
          propString = rgx2.Replace(propString, "");
        }

        if (string.IsNullOrEmpty(displayName)) continue;

        // assign each thing a unique export name.
        var exportName = GetExportName(displayName, null);
        var index = 2;
        while (mapExportNameToThing.ContainsKey(exportName)) exportName = GetExportName(displayName, index++);

        // on each line, indentation denotes containment;
        // work out how much indentation there is
        var indent = 0;
        while (indent < objectName.Length && objectName[indent] == ' ') ++indent;

        // compare indentations to deduce containment
        Thing container = null;
        for (var thingIndex = location.Things.Count - 1; thingIndex >= 0; --thingIndex) {
          var priorThing = location.Things[thingIndex];
          if (indent > priorThing.Indent) {
            container = priorThing;
            break;
          }
        }

        var thing = new Thing(displayName, exportName, location, container, indent, propString);
        mapExportNameToThing.Add(exportName, thing);
        location.Things.Add(thing);
      }
    }
  }

  private void pickBestExits() {
    // for every direction from every room, if there are one or more exits
    // in said direction, pick the best one.
    foreach (var location in LocationsInExportOrder) location.PickBestExits();
  }

  private void prepareContent() {
    findRegions();
    findRooms();
    findExits();
    pickBestExits();
    findThings();
  }
}