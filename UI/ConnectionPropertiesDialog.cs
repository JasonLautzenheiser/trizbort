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
using System.Drawing;
using System.Windows.Forms;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;

namespace Trizbort.UI; 

public partial class ConnectionPropertiesDialog : Form {
  private const string NO_COLOR_SET = "No Color Set";


    
  public ConnectionPropertiesDialog() {
    InitializeComponent();
  }

  public Color ConnectionColor {
    get => connectionColorBox.Text == NO_COLOR_SET ? Color.Transparent : connectionColorBox.BackColor;
    set {
      if (value == Color.Transparent) {
        connectionColorBox.BackColor = Color.White;
        connectionColorBox.Text = NO_COLOR_SET;
      } else {
        connectionColorBox.BackColor = value;
        connectionColorBox.Text = string.Empty;
      }
    }
  }

  public string ConnectionDescription {
    get => txtDescription.Text;
    set {
      txtDescription.Text = value;
      updateControls();
    }
  }

  public string ConnectionName {
    get => txtName.Text;
    set {
      txtName.Text = value;
      updateControls();
    }
  }

  public Door Door {
    get => chkDoor.Checked ? new Door {Lockable = chkLockable.Checked, Locked = chkLocked.Checked, Open = chkOpen.Checked, Openable = chkOpenable.Checked} : null;
    set {
      if (value != null) {
        chkDoor.Checked = true;
        chkLockable.Checked = value.Lockable;
        chkLocked.Checked = value.Locked;
        chkOpen.Checked = value.Open;
        chkOpenable.Checked = value.Openable;
      }
    }
  }

  public string EndText {
    get => m_endTextBox.Text;
    set {
      m_endTextBox.Text = value;
      updateControls();
    }
  }


  public bool IsDirectional { get => m_oneWayCheckBox.Checked; set => m_oneWayCheckBox.Checked = value; }

  public bool IsDotted { get => m_dottedCheckBox.Checked; set => m_dottedCheckBox.Checked = value; }

  public string MidText {
    get => m_middleTextBox.Text;
    set {
      m_middleTextBox.Text = value;
      updateControls();
    }
  }

  public string StartText {
    get => m_startTextBox.Text;
    set {
      m_startTextBox.Text = value;
      updateControls();
    }
  }

  private void changeConnectionColor() {
    ConnectionColor = Colors.ShowColorDialog(ConnectionColor, this);
  }

  private void chkDoor_CheckedChanged(object sender, EventArgs e) {
    chkOpen.Enabled = chkDoor.Checked;
    chkLockable.Enabled = chkDoor.Checked;
    chkLocked.Enabled = chkDoor.Checked;
    chkOpenable.Enabled = chkDoor.Checked;
  }


  private void connectionColorBox_DoubleClick(object sender, EventArgs e) {
    changeConnectionColor();
  }

  private void connectionColorChange_Click(object sender, EventArgs e) {
    changeConnectionColor();
  }

  private bool matchText(ConnectionLabel label) {
    Connection.GetText(label, out var start, out var end);
    return StartText == start && EndText == end && string.IsNullOrEmpty(MidText);
  }

  private void onRadioButtonCheckedChanged(object sender, EventArgs e) {
    if (m_udRadioButton.Checked)
      setText(ConnectionLabel.Up);
    else if (m_duRadioButton.Checked)
      setText(ConnectionLabel.Down);
    else if (m_ioRadioButton.Checked)
      setText(ConnectionLabel.In);
    else if (m_oiRadioButton.Checked) setText(ConnectionLabel.Out);
  }

  private void setText(ConnectionLabel label) {
    Connection.GetText(label, out var start, out var end);
    StartText = start;
    EndText = end;
  }

  private void updateControls() {
    if (matchText(ConnectionLabel.Up))
      m_udRadioButton.Checked = true;
    else if (matchText(ConnectionLabel.Down))
      m_duRadioButton.Checked = true;
    else if (matchText(ConnectionLabel.In))
      m_ioRadioButton.Checked = true;
    else if (matchText(ConnectionLabel.Out))
      m_oiRadioButton.Checked = true;
    else
      m_customRadioButton.Checked = true;
  }

  private void connectionColorClear_Click(object sender, EventArgs e)
  {
    ConnectionColor = Color.Transparent;
  }

  private void connectionColorBox_Enter(object sender, EventArgs e) {
    connectionColorChange.Focus();
  }
}