/*
    Copyright (c) 2010-2015 by Genstein and Jason Lautzenheiser.

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
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trizbort
{
  public sealed partial class Canvas
  {
    private readonly Automap m_automap = Automap.Instance;
    private readonly MultithreadedAutomapCanvas m_threadSafeAutomapCanvas;
    private bool m_dontAskAboutAmbiguities;


    public bool IsAutomapping
    {
      get { return m_automap.Running; }
    }

    public string AutomappingStatus
    {
      get { return m_automap.Status; }
    }

    Room IAutomapCanvas.FindRoom(string roomName, string roomDescription, string line, RoomMatcher matcher)
    {
      var list = new List<Room>();
      foreach (var element in Project.Current.Elements)
      {
        if (element is Room)
        {
          var room = (Room) element;
          var matches = matcher(roomName, roomDescription, room);
          if (matches.HasValue && matches.Value)
          {
            // it's definitely this room
            return room;
          }
          if (!matches.HasValue)
          {
            // it's ambiguous
            list.Add(room);
          }
        }
      }

      if (list.Count == 0)
      {
        return null;
      }
      if (m_dontAskAboutAmbiguities)
      {
        // the user has long given up on this process;
        // use the first ambiguous room in the list.
        return list[0];
      }
      if ((string.IsNullOrEmpty(roomDescription) || !list[0].HasDescription) && list.Count == 1)
      {
        // technically it's ambiguous, but we don't two descriptions for the user to compare;
        // there's only one option, so use it. They probably didn't have VERBOSE on for the whole transcript.
        return list[0];
      }

      using (var dialog = new DisambiguateRoomsDialog())
      {
        dialog.SetTranscriptContext(roomName, roomDescription, line);
        dialog.AddAmbiguousRooms(list);
        dialog.ShowDialog();
        if (dialog.UserDoesntCareAnyMore)
        {
          // The user has given up on this process! Can't say I blame them.
          // Use the first ambiguous room on the list, as above.
          m_dontAskAboutAmbiguities = true;
          return list[0];
        }

        // Either the user picked a room, or they said "New Room" in which case we don't match, returning null.
        return dialog.Disambiguation;
      }
    }

    Room IAutomapCanvas.CreateRoom(Room existing, string name)
    {
      // start by placing the room at the origin
      var room = new Room(Project.Current);
      room.Name = name;

      if (existing != null)
      {
        // if we know which room we were just in, start at that instead
        room.Position = existing.Position;
      }
      room.Position = Settings.Snap(room.Position);

      // while we can't place the room, try to the left, then the right,
      // expanding our distance as we go, until we find a blank space.
      var tryOtherSideNext = false;
      var tryLeft = true;
      var distance = 0;
      var initialPosition = room.Position;
      while (AnyRoomsIntersect(room))
      {
        if (tryOtherSideNext)
        {
          tryLeft = !tryLeft;
          tryOtherSideNext = false;
        }
        else
        {
          tryOtherSideNext = true;
          ++distance;
        }
        if (tryLeft)
        {
          room.Position = Settings.Snap(new Vector(initialPosition.X - distance*(Settings.PreferredDistanceBetweenRooms + room.Width), room.Position.Y));
        }
        else
        {
          room.Position = Settings.Snap(new Vector(initialPosition.X + distance*(Settings.PreferredDistanceBetweenRooms + room.Width), room.Position.Y));
        }

        Debug.WriteLine(string.Format("Try again, more to the {0}.", tryLeft ? "left" : "right"));
      }

      // after we set the position, set this flag,
      // since setting the position clears this flag if set.
      room.ArbitraryAutomappedPosition = true;

      Project.Current.Elements.Add(room);
      return room;
    }

    Room IAutomapCanvas.CreateRoom(Room existing, AutomapDirection directionFromExisting, string roomName, string line)
    {
      if (!Project.Current.Elements.Contains(existing))
      {
        // avoid issues if the user has just deleted the existing room
        return null;
      }

      var room = new Room(Project.Current) {Name = roomName};
      if (line != roomName)
      {
        room.SubTitle = line.Replace(roomName, "");
      }

      Vector delta;
      PositionRelativeTo(room, existing, CompassPointHelper.GetCompassDirection(directionFromExisting), out delta);

      if (AnyRoomsIntersect(room))
      {
        ShiftMap(room.InnerBounds, delta);
        Debug.WriteLine("Shift map.");
      }

      Project.Current.Elements.Add(room);

      return room;
    }

    void IAutomapCanvas.AddExitStub(Room room, AutomapDirection direction)
    {
      if (!Project.Current.Elements.Contains(room))
      {
        // avoid issues if the user has just deleted one of these rooms
        return;
      }

      var sourceCompassPoint = CompassPointHelper.GetCompassDirection(direction);
      var connection = addConnection(room, sourceCompassPoint, room, sourceCompassPoint);
      switch (direction)
      {
        case AutomapDirection.Up:
          connection.StartText = Connection.Up;
          break;
        case AutomapDirection.Down:
          connection.StartText = Connection.Down;
          break;
      }
    }

    void IAutomapCanvas.RemoveExitStub(Room room, AutomapDirection direction)
    {
      if (!Project.Current.Elements.Contains(room))
      {
        // avoid issues if the user has just deleted one of these rooms
        return;
      }

      var compassPoint = CompassPointHelper.GetCompassDirection(direction);
      foreach (var connection in room.GetConnections(compassPoint))
      {
        CompassPoint sourceCompassPoint, targetCompassPoint;
        var source = connection.GetSourceRoom(out sourceCompassPoint);
        var target = connection.GetTargetRoom(out targetCompassPoint);
        if (source == room && target == room && sourceCompassPoint == compassPoint && targetCompassPoint == compassPoint)
        {
          Project.Current.Elements.Remove(connection);
        }
      }
    }

    void IAutomapCanvas.Connect(Room source, AutomapDirection directionFromSource, Room target)
    {
      if (!Project.Current.Elements.Contains(source) || !Project.Current.Elements.Contains(target))
      {
        // avoid issues if the user has just deleted one of these rooms
        return;
      }

      // work out the correct compass point to use for the given direction

      // look for existing connections:
      // if the given direction is up/down/in/out, any existing connection will suffice;
      // otherwise, only match an existing connection if it's pretty close to the one we want.
      var sourceCompassPoint = CompassPointHelper.GetCompassDirection(directionFromSource);
      CompassPoint? acceptableSourceCompassPoint;
      switch (directionFromSource)
      {
        case AutomapDirection.Up:
        case AutomapDirection.Down:
        case AutomapDirection.In:
        case AutomapDirection.Out:
          acceptableSourceCompassPoint = null; // any existing connection will do
          break;
        default:
          acceptableSourceCompassPoint = sourceCompassPoint;
          break;
      }
      bool wrongWay;
      var connection = FindConnection(source, target, acceptableSourceCompassPoint, out wrongWay);

      if (connection == null)
      {
        // there is no suitable connection between these rooms:
        var targetCompassPoint = CompassPointHelper.GetAutomapOpposite(sourceCompassPoint);

        // check whether we can move one of the rooms to make the connection tidier;
        // we won't need this very often, but it can be useful especially if the user teleports into a room
        // and then steps out into an existing one (this can appear to happen if the user moves into a 
        // dark room, turns on the light, then leaves).
        TryMoveRoomsForTidyConnection(source, sourceCompassPoint, target, targetCompassPoint);

        // add a new connection
        connection = addConnection(source, sourceCompassPoint, target, targetCompassPoint);

        if (m_automap.UseDottedConnection)
        {
          connection.Style = ConnectionStyle.Dashed;
          m_automap.UseDottedConnection = false;
        }
        else
        {
          connection.Style = ConnectionStyle.Solid;
        }

        connection.Flow = ConnectionFlow.OneWay;
      }
      else if (wrongWay)
      {
        // there is a suitable connection between these rooms, but it goes the wrong way;
        // make it bidirectional since we can now go both ways.
        connection.Flow = ConnectionFlow.TwoWay;
      }

      // if this is an up/down/in/out connection, mark it as such;
      // but don't override any existing text.
      switch (directionFromSource)
      {
        case AutomapDirection.Up:
        case AutomapDirection.Down:
        case AutomapDirection.In:
        case AutomapDirection.Out:
          if (string.IsNullOrEmpty(connection.StartText) && string.IsNullOrEmpty(connection.EndText))
          {
            switch (directionFromSource)
            {
              case AutomapDirection.Up:
                connection.SetText(wrongWay ? ConnectionLabel.Down : ConnectionLabel.Up);
                break;
              case AutomapDirection.Down:
                connection.SetText(wrongWay ? ConnectionLabel.Up : ConnectionLabel.Down);
                break;
              case AutomapDirection.In:
                connection.SetText(wrongWay ? ConnectionLabel.Out : ConnectionLabel.In);
                break;
              case AutomapDirection.Out:
                connection.SetText(wrongWay ? ConnectionLabel.In : ConnectionLabel.Out);
                break;
            }
          }
          break;
      }
    }

    void IAutomapCanvas.SelectRoom(Room room)
    {
      if (!Project.Current.Elements.Contains(room))
      {
        // avoid issues if the user has just deleted this room
        return;
      }

      SelectedElement = room;
      commandController.MakeVisible(room);
    }

    public Task StartAutomapping(AutomapSettings settings, bool justParseFile = false)
    {
      StopAutomapping();

      var task = justParseFile ? m_automap.StartCL(m_threadSafeAutomapCanvas, settings) : m_automap.Start(m_threadSafeAutomapCanvas, settings);

      m_dontAskAboutAmbiguities = false;

      return task;
    }

    public void StopAutomapping()
    {
      m_automap.Stop();
    }

    private bool AnyRoomsIntersect(Room room)
    {
      var bounds = room.InnerBounds;
      foreach (var element in Project.Current.Elements)
      {
        if (element is Room && element != room && element.Intersects(bounds))
        {
          Debug.WriteLine("{0} is blocking {1}.", (element as Room).Name, room.Name);
          return true;
        }
      }
      return false;
    }

    private void PositionRelativeTo(Room room, Room existing, CompassPoint existingCompassPoint, out Vector delta)
    {
      delta = CompassPointHelper.GetAutomapDirectionVector(existingCompassPoint);
      delta.X *= Settings.PreferredDistanceBetweenRooms + room.Width;
      delta.Y *= Settings.PreferredDistanceBetweenRooms + room.Height;

      var newRoomCenter = existing.InnerBounds.Center + delta;
      room.Position = Settings.Snap(new Vector(newRoomCenter.X - room.Width/2, newRoomCenter.Y - room.Height/2));
    }

    private void ShiftMap(Rect deltaOrigin, Vector delta)
    {
      // move all elements to the left/right of the origin left/right by the given delta
      foreach (var element in Project.Current.Elements)
      {
        if (element is Room)
        {
          var room = (Room) element;
          var bounds = room.InnerBounds;
          if (delta.X < 0)
          {
            if (bounds.Center.X < deltaOrigin.Right)
            {
              room.Position = new Vector(room.Position.X + delta.X, room.Position.Y);
            }
          }
          else if (delta.X > 0)
          {
            if (bounds.Center.X > deltaOrigin.Left)
            {
              room.Position = new Vector(room.Position.X + delta.X, room.Position.Y);
            }
          }
        }
      }

      // move all elements above/below the origin up/down by the given delta
      foreach (var element in Project.Current.Elements)
      {
        if (element is Room)
        {
          var room = (Room) element;
          var bounds = room.InnerBounds;
          if (delta.Y < 0)
          {
            if (bounds.Center.Y < deltaOrigin.Bottom)
            {
              room.Position = new Vector(room.Position.X, room.Position.Y + delta.Y);
            }
          }
          else if (bounds.Center.Y > deltaOrigin.Top)
          {
            if (bounds.Bottom > deltaOrigin.Y)
            {
              room.Position = new Vector(room.Position.X, room.Position.Y + delta.Y);
            }
          }
        }
      }
    }

    /// <summary>
    ///   Approximately match two directions, allowing for aesthetic rearrangement by the user.
    /// </summary>
    /// <remarks>
    ///   Two compass points match if they are on the same side of a box representing the room.
    /// </remarks>
    private bool ApproximateDirectionMatch(CompassPoint one, CompassPoint two)
    {
      return CompassPointHelper.GetAutomapDirectionVector(one) == CompassPointHelper.GetAutomapDirectionVector(two);
    }

    private Connection FindConnection(Room source, Room target, CompassPoint? directionFromSource, out bool wrongWay)
    {
      foreach (var element in Project.Current.Elements)
      {
        if (element is Connection)
        {
          var connection = (Connection) element;
          CompassPoint fromDirection, toDirection;
          var fromRoom = connection.GetSourceRoom(out fromDirection);
          var toRoom = connection.GetTargetRoom(out toDirection);
          if (fromRoom == source && toRoom == target && (directionFromSource == null || ApproximateDirectionMatch(directionFromSource.Value, fromDirection)))
          {
            // the two rooms are connected already in the given direction, A to B or both ways.
            wrongWay = false;
            return connection;
          }
          if (fromRoom == target && toRoom == source && (directionFromSource == null || ApproximateDirectionMatch(directionFromSource.Value, toDirection)))
          {
            // the two rooms are connected already in the given direction, B to A or both ways.
            wrongWay = connection.Flow == ConnectionFlow.OneWay;
            return connection;
          }
          if (fromRoom == target && toRoom == source)
          {
            var r1 = (Room.CompassPort)connection.VertexList[1].Port;
            r1.CompassPoint = (CompassPoint)directionFromSource;
            wrongWay = connection.Flow == ConnectionFlow.OneWay;
            return connection;
          }
          if (fromRoom == source && toRoom == target)
          {
            var r1 = (Room.CompassPort)connection.VertexList[0].Port;
            r1.CompassPoint = (CompassPoint)directionFromSource;
            wrongWay = false;
            return connection;
          }
        }
      }
      wrongWay = false;
      return null;
    }

    private void TryMoveRoomsForTidyConnection(Room source, CompassPoint sourceCompassPoint, Room target, CompassPoint targetCompassPoint)
    {
      if (source.ArbitraryAutomappedPosition && !source.IsConnected)
      {
        if (TryMoveRoomForTidyConnection(source, targetCompassPoint, target))
        {
          return;
        }
      }
      if (target.ArbitraryAutomappedPosition && !target.IsConnected)
      {
        TryMoveRoomForTidyConnection(target, sourceCompassPoint, source);
      }
    }

    private bool TryMoveRoomForTidyConnection(Room source, CompassPoint targetCompassPoint, Room target)
    {
      var sourceArbitrary = source.ArbitraryAutomappedPosition;
      var sourcePosition = source.Position;

      Vector delta;
      PositionRelativeTo(source, target, targetCompassPoint, out delta);
      if (AnyRoomsIntersect(source))
      {
        // didn't work; restore previous position
        source.Position = sourcePosition;
        source.ArbitraryAutomappedPosition = sourceArbitrary;
        return false;
      }

      // that's better
      return true;
    }

    /// <summary>
    ///   A proxy class which implements IAutomapCanvas, marshalling calls to the real canvas on the main thread.
    /// </summary>
    private class MultithreadedAutomapCanvas : IAutomapCanvas
    {
      private readonly IAutomapCanvas m_canvas;
      private readonly Control m_control;

      public MultithreadedAutomapCanvas(Canvas canvas)
      {
        m_control = canvas;
        m_canvas = canvas;
      }

      public Room FindRoom(string roomName, string roomDescription, string line, RoomMatcher matcher)
      {
        Room room = null;
        try { m_control.Invoke((MethodInvoker) delegate { room = m_canvas.FindRoom(roomName, roomDescription, line, matcher); }); }
        catch (Exception)
        {
        }
        return room;
      }

      public Room CreateRoom(Room existing, string name)
      {
        Room room = null;
        try { m_control.Invoke((MethodInvoker) delegate { room = m_canvas.CreateRoom(existing, name); }); }
        catch (Exception)
        {
        }
        return room;
      }

      public Room CreateRoom(Room existing, AutomapDirection directionFromExisting, string roomName, string line)
      {
        Room room = null;
        try { m_control.Invoke((MethodInvoker) delegate { room = m_canvas.CreateRoom(existing, directionFromExisting, roomName,line); }); }
        catch (Exception)
        {
        }
        return room;
      }

      public void Connect(Room source, AutomapDirection directionFromSource, Room target)
      {
        try { m_control.Invoke((MethodInvoker) delegate { m_canvas.Connect(source, directionFromSource, target); }); }
        catch (Exception)
        {
        }
      }

      public void AddExitStub(Room room, AutomapDirection direction)
      {
        try { m_control.Invoke((MethodInvoker) delegate { m_canvas.AddExitStub(room, direction); }); }
        catch (Exception)
        {
        }
      }

      public void RemoveExitStub(Room room, AutomapDirection direction)
      {
        try { m_control.Invoke((MethodInvoker) delegate { m_canvas.RemoveExitStub(room, direction); }); }
        catch (Exception)
        {
        }
      }

      public void SelectRoom(Room room)
      {
        try { m_control.Invoke((MethodInvoker) delegate { m_canvas.SelectRoom(room); }); }
        catch (Exception)
        {
        }
      }
    }
  }
}