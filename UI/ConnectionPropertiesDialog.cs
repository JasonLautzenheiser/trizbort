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
using System.Drawing;
using System.Windows.Forms;
using Trizbort.Domain;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;

namespace Trizbort.UI
{
  public partial class ConnectionPropertiesDialog : Form
  {
    private const string NO_COLOR_SET = "No Color Set";

    public ConnectionPropertiesDialog()
    {
      InitializeComponent();
    }

    public Color ConnectionColor
    {
      get { return connectionColorBox.WatermarkText == NO_COLOR_SET ? Color.Transparent : connectionColorBox.BackColor; }
      set
      {
        if (value == Color.Transparent)
        {
          connectionColorBox.BackColor = Color.White;
          connectionColorBox.WatermarkText = NO_COLOR_SET;
        }
        else
        {
          connectionColorBox.BackColor = value;
          connectionColorBox.WatermarkText = string.Empty;
        }
      }
    }


    public bool IsDirectional { get { return m_oneWayCheckBox.Checked; } set { m_oneWayCheckBox.Checked = value; } }

    public bool IsDotted { get { return m_dottedCheckBox.Checked; } set { m_dottedCheckBox.Checked = value; } }

    public string ConnectionName
    {
      get { return txtName.Text; }
      set { txtName.Text = value;updateControls(); }
    }

    public string ConnectionDescription
    {
      get { return txtDescription.Text; }
      set { txtDescription.Text = value; updateControls(); }
    }

    public string StartText
    {
      get { return m_startTextBox.Text; }
      set
      {
        m_startTextBox.Text = value;
        updateControls();
      }
    }

    public string MidText
    {
      get { return m_middleTextBox.Text; }
      set
      {
        m_middleTextBox.Text = value;
        updateControls();
      }
    }

    public string EndText
    {
      get { return m_endTextBox.Text; }
      set
      {
        m_endTextBox.Text = value;
        updateControls();
      }
    }

    public Door Door
    {
      get {
        return chkDoor.Checked ? new Door {Lockable = chkLockable.Checked, Locked = chkLocked.Checked, Open = chkOpen.Checked, Openable = chkOpenable.Checked} : null;
      }
      set
      {
        if (value != null)
        {
          chkDoor.Checked = true;
          chkLockable.Checked = value.Lockable;
          chkLocked.Checked = value.Locked;
          chkOpen.Checked = value.Open;
          chkOpenable.Checked = value.Openable;
        }
      }
    }

    private void updateControls()
    {
      if (matchText(ConnectionLabel.Up))
      {
        m_udRadioButton.Checked = true;
      }
      else if (matchText(ConnectionLabel.Down))
      {
        m_duRadioButton.Checked = true;
      }
      else if (matchText(ConnectionLabel.In))
      {
        m_ioRadioButton.Checked = true;
      }
      else if (matchText(ConnectionLabel.Out))
      {
        m_oiRadioButton.Checked = true;
      }
      else
      {
        m_customRadioButton.Checked = true;
      }
    }

    private bool matchText(ConnectionLabel label)
    {
      string start, end;
      Connection.GetText(label, out start, out end);
      return StartText == start && EndText == end && string.IsNullOrEmpty(MidText);
    }

    private void onRadioButtonCheckedChanged(object sender, EventArgs e)
    {
      if (m_udRadioButton.Checked)
      {
        setText(ConnectionLabel.Up);
      }
      else if (m_duRadioButton.Checked)
      {
        setText(ConnectionLabel.Down);
      }
      else if (m_ioRadioButton.Checked)
      {
        setText(ConnectionLabel.In);
      }
      else if (m_oiRadioButton.Checked)
      {
        setText(ConnectionLabel.Out);
      }
    }

    private void setText(ConnectionLabel label)
    {
      string start, end;
      Connection.GetText(label, out start, out end);
      StartText = start;
      EndText = end;
    }

    private void connectionColorChange_Click(object sender, EventArgs e)
    {
      changeConnectionColor();
    }

    private void changeConnectionColor()
    {
      ConnectionColor = Colors.ShowColorDialog(ConnectionColor, this);
    }

    private void connectionColorBox_ButtonCustomClick(object sender, EventArgs e)
    {
      ConnectionColor = Color.Transparent;
      connectionColorChange.Focus();
    }

    private void connectionColorBox_DoubleClick(object sender, EventArgs e)
    {
      changeConnectionColor();
    }

    private void chkDoor_CheckedChanged(object sender, EventArgs e)
    {
      chkOpen.Enabled = chkDoor.Checked;
      chkLockable.Enabled = chkDoor.Checked;
      chkLocked.Enabled = chkDoor.Checked;
      chkOpenable.Enabled = chkDoor.Checked;
    }
  }
}