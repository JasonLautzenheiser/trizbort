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

using Trizbort.Automap;

namespace Trizbort
{
  internal interface IAutomapCanvas
  {
    /// <summary>
    ///   Find a room matching the given name and, if the given description isn't null, the given description.
    /// </summary>
    Room FindRoom(string roomName, string roomDescription, string line, RoomMatcher matcher);

    Room CreateRoom(Room existing, string name);

    Room CreateRoom(Room existing, AutomapDirection directionFromExisting, string roomName, string line);

    void Connect(Room source, AutomapDirection directionFromSource, Room target, bool assumeTwoWayConnections);

    void AddExitStub(Room room, AutomapDirection direction);

    void RemoveExitStub(Room room, AutomapDirection direction);

    void SelectRoom(Room room);
    void RemoveRoom(Room mOtherRoom);
  }

  internal delegate bool? RoomMatcher(string roomName, string roomDescription, Room room);
}