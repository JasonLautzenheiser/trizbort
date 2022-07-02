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
using System.Windows.Forms;
using Trizbort.Domain.Elements;

namespace Trizbort.UI;

sealed partial class DisambiguateRoomsDialog : Form
{
  public DisambiguateRoomsDialog()
  {
    InitializeComponent();

    m_thisRoomButton.Enabled = false;
  }

  public void SetTranscriptContext(string roomName, string roomDescription, string line)
  {
    m_transcriptContextTextBox.Text = string.Format("{0}\n{1}", line, roomDescription).Replace("\r", string.Empty).Replace("\n", "\r\n");
  }

  protected override void OnLoad(EventArgs e)
  {
    if (m_roomNamesListBox.Items.Count == 1)
    {
      // if there's only one option, select it
      m_roomNamesListBox.SelectedIndex = 0;
    }

    m_transcriptContextTextBox.Focus();
    m_transcriptContextTextBox.Select(0, 0);

    base.OnLoad(e);
  }

  public void AddAmbiguousRoom(Room room)
  {
    m_roomNamesListBox.Items.Add(new AmbiguousRoom(room));
  }

  public void AddAmbiguousRooms(IEnumerable<Room> rooms)
  {
    foreach (var room in rooms)
    {
      AddAmbiguousRoom(room);
    }
  }

  private void RoomNamesListBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (m_roomNamesListBox.SelectedIndex != -1)
    {
      m_thisRoomButton.Enabled = true;
      var room = (m_roomNamesListBox.SelectedItem as AmbiguousRoom).Room;
      m_roomDescriptionTextBox.Text = room.PrimaryDescription;
    }
    else
    {
      m_thisRoomButton.Enabled = false;
      m_roomDescriptionTextBox.Text = string.Empty;
    }
  }

  public Room Disambiguation
  {
    get
    {
      if (DialogResult == DialogResult.Yes && m_roomNamesListBox.SelectedIndex != -1)
      {
        return (m_roomNamesListBox.SelectedItem as AmbiguousRoom).Room;
      }
      return null;
    }
  }

  public bool UserDoesntCareAnyMore
  {
    get { return DialogResult == DialogResult.Abort; }
  }

  /// <summary>
  /// A Room reference which will ToString() as the room name.
  /// For use in the room names list box.
  /// </summary>
  class AmbiguousRoom
  {
    public AmbiguousRoom(Room room)
    {
      Room = room;
    }

    public override string ToString()
    {
      return Room.Name;
    }

    public Room Room { get; private set; }
  }
}