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
using System.Windows.Forms;
using Trizbort.Automap;
using Trizbort.Domain.AppSettings;
using Trizbort.Util;

namespace Trizbort.UI
{
  internal partial class AutomapDialog : Form
  {
    public AutomapDialog()
    {
      InitializeComponent();
#if DEBUG
      m_singleStepCheckBox.Visible = true;
#else
            m_singleStepCheckBox.Visible = false;
#endif
      Data = ApplicationSettingsController.AppSettings.Automap;
    }

    public AutomapSettings Data
    {
      get
      {
        var settings = new AutomapSettings
        {
          FileName = m_textBox.Text,
          SingleStep = m_singleStepCheckBox.Checked,
          VerboseTranscript = m_verboseTranscriptCheckBox.Checked,
          AssumeRoomsWithSameNameAreSameRoom = !m_verboseTranscriptCheckBox.Checked || m_roomsWithSameNameAreSameRoomCheckBox.Checked,
          GuessExits = m_guessExitsCheckBox.Checked,
          AddObjectCommand = m_addObjectCommandTextBox.Text,
          AddRegionCommand = m_addRegionCommandTextBox.Text,
          ContinueTranscript = m_startFromEndCheckBox.Checked,
          AssumeTwoWayConnections = chkAssumeTwoWayConnections.Checked
        };
        return settings;
      }
      set
      {
        m_textBox.Text = value.FileName;
        m_singleStepCheckBox.Checked = value.SingleStep;
        m_verboseTranscriptCheckBox.Checked = value.VerboseTranscript;
        m_roomsWithSameNameAreSameRoomCheckBox.Enabled = m_verboseTranscriptCheckBox.Enabled;
        m_roomsWithSameNameAreSameRoomCheckBox.Checked = m_roomsWithSameNameAreSameRoomCheckBox.Enabled && value.AssumeRoomsWithSameNameAreSameRoom;
        m_guessExitsCheckBox.Checked = value.GuessExits;
        m_addObjectCommandTextBox.Text = value.AddObjectCommand;
        m_addRegionCommandTextBox.Text = value.AddRegionCommand;
        m_startFromEndCheckBox.Checked = value.ContinueTranscript;
        chkAssumeTwoWayConnections.Checked = value.AssumeTwoWayConnections;
      }
    }

    protected override void OnClosed(EventArgs e)
    {
      if (DialogResult == DialogResult.OK)
        ApplicationSettingsController.AppSettings.Automap = Data;
      base.OnClosed(e);
    }

    private void BrowseButton_Click(object sender, EventArgs e)
    {
      using (var dialog = new OpenFileDialog())
      {
        dialog.Filter = "Text and Log Files(*.txt, *.log)|*.txt;*.log|All Files|*.*||";
        dialog.Title = "Open Transcript";
        dialog.FileName = m_textBox.Text;
        dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(m_textBox.Text);
        if (dialog.ShowDialog() == DialogResult.OK)
          m_textBox.Text = dialog.FileName;
      }
    }

    private void VerboseTranscriptCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      m_roomsWithSameNameAreSameRoomCheckBox.Enabled = m_verboseTranscriptCheckBox.Checked;
      m_roomsWithSameNameAreSameRoomCheckBox.Checked = !m_verboseTranscriptCheckBox.Checked;
    }
  }
}