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

namespace Trizbort.Automap; 

public struct AutomapSettings
{
  public static AutomapSettings Default
  {
    get
    {
      var settings = new AutomapSettings
      {
        FileName = string.Empty,
        SingleStep = false,
        VerboseTranscript = true,
        AssumeRoomsWithSameNameAreSameRoom = false,
        GuessExits = true,
        AddObjectCommand = "tb see",
        AddRegionCommand = "tb region",
        ContinueTranscript = false,
        AssumeTwoWayConnections = true
      };
      return settings;
    }
  }

  public string FileName;
  public bool SingleStep;
  public bool VerboseTranscript;
  public bool AssumeRoomsWithSameNameAreSameRoom;
  public bool GuessExits;
  public string AddObjectCommand;
  public string AddRegionCommand;
  public bool ContinueTranscript;
  public bool AssumeTwoWayConnections;
}