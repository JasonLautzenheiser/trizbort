/*
    Copyright (c) 2010 by Genstein

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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Trizbort
{
    public partial class ConnectionPropertiesDialog : Form
    {
        public ConnectionPropertiesDialog()
        {
            InitializeComponent();
        }

        public bool IsDirectional
        {
            get { return m_oneWayCheckBox.Checked; }
            set { m_oneWayCheckBox.Checked = value; }
        }

        public bool IsDotted
        {
            get { return m_dottedCheckBox.Checked; }
            set { m_dottedCheckBox.Checked = value; }
        }

        public string StartText
        {
            get { return m_startTextBox.Text; }
            set
            {
                m_startTextBox.Text = value;
                UpdateControls();
            }
        }

        public string MidText
        {
            get { return m_middleTextBox.Text; }
            set
            {
                m_middleTextBox.Text = value;
                UpdateControls();
            }
        }

        public string EndText
        {
            get { return m_endTextBox.Text; }
            set
            {
                m_endTextBox.Text = value;
                UpdateControls();
            }
        }

        private void UpdateControls()
        {
            if (MatchText(ConnectionLabel.Up))
            {
                m_udRadioButton.Checked = true;
            }
            else if (MatchText(ConnectionLabel.Down))
            {
                m_duRadioButton.Checked = true;
            }
            else if (MatchText(ConnectionLabel.In))
            {
                m_ioRadioButton.Checked = true;
            }
            else if (MatchText(ConnectionLabel.Out))
            {
                m_oiRadioButton.Checked = true;
            }
            else
            {
                m_customRadioButton.Checked = true;
            }
        }

        private bool MatchText(ConnectionLabel label)
        {
            string start, end;
            Connection.GetText(label, out start, out end);
            return StartText == start && EndText == end && string.IsNullOrEmpty(MidText);
        }

        private void OnRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            if (m_udRadioButton.Checked)
            {
                SetText(ConnectionLabel.Up);
            }
            else if (m_duRadioButton.Checked)
            {
                SetText(ConnectionLabel.Down);
            }
            else if (m_ioRadioButton.Checked)
            {
                SetText(ConnectionLabel.In);
            }
            else if (m_oiRadioButton.Checked)
            {
                SetText(ConnectionLabel.Out);
            }
        }

        private void SetText(ConnectionLabel label)
        {
            string start, end;
            Connection.GetText(label, out start, out end);
            StartText = start;
            EndText = end;
        }
    }
}
