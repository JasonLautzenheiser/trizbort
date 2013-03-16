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
using System.Drawing;
using System.Windows.Forms;

namespace Trizbort
{
    internal partial class RoomPropertiesDialog : Form
    {
        public RoomPropertiesDialog()
        {
            InitializeComponent();

            switch (m_lastViewedTab)
            {
                case Tab.Objects:
                    m_tabControl.SelectedIndex = 0;
                    break;
                case Tab.Description:
                    m_tabControl.SelectedIndex = 1;
                    break;
            }
        }

        public string RoomName
        {
            get { return m_nameTextBox.Text; }
            set { m_nameTextBox.Text = value; }
        }

        public string Description
        {
            get { return m_descriptionTextBox.Text; }
            set { m_descriptionTextBox.Text = value; }
        }

        public bool IsDark
        {
            get { return m_isDarkCheckBox.Checked; }
            set { m_isDarkCheckBox.Checked = value; }
        }

        public string Objects
        {
            get { return m_objectsTextBox.Text; }
            set { m_objectsTextBox.Text = value; }
        }

        public CompassPoint ObjectsPosition
        {
            get
            {
                if (m_nCheckBox.Checked) return CompassPoint.North;
                else if (m_sCheckBox.Checked) return CompassPoint.South;
                else if (m_eCheckBox.Checked) return CompassPoint.East;
                else if (m_wCheckBox.Checked) return CompassPoint.West;
                else if (m_neCheckBox.Checked) return CompassPoint.NorthEast;
                else if (m_nwCheckBox.Checked) return CompassPoint.NorthWest;
                else if (m_seCheckBox.Checked) return CompassPoint.SouthEast;
                else if (m_swCheckBox.Checked) return CompassPoint.SouthWest;
                else return CompassPoint.WestSouthWest;
            }
            set
            {
                switch (value)
                {
                    case CompassPoint.North:
                        m_nCheckBox.Checked = true;
                        break;
                    case CompassPoint.South:
                        m_sCheckBox.Checked = true;
                        break;
                    case CompassPoint.East:
                        m_eCheckBox.Checked = true;
                        break;
                    case CompassPoint.West:
                        m_wCheckBox.Checked = true;
                        break;
                    case CompassPoint.NorthEast:
                        m_neCheckBox.Checked = true;
                        break;
                    case CompassPoint.NorthWest:
                        m_nwCheckBox.Checked = true;
                        break;
                    case CompassPoint.SouthEast:
                        m_seCheckBox.Checked = true;
                        break;
                    case CompassPoint.SouthWest:
                        m_swCheckBox.Checked = true;
                        break;
                    default:
                        m_cCheckBox.Checked = true;
                        break;
                }
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (m_tabControl.SelectedIndex)
            {
                case 0:
                default:
                    m_lastViewedTab = Tab.Objects;
                    break;
                case 1:
                    m_lastViewedTab = Tab.Description;
                    break;
            }
        }

        private void PositionCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (m_adjustingPosition)
                return;

            m_adjustingPosition = true;
            try
            {
                var checkBox = (CheckBox)sender;
                if (checkBox.Checked)
                {
                    foreach (Control other in checkBox.Parent.Controls)
                    {
                        if (other is CheckBox && other != checkBox)
                        {
                            ((CheckBox)other).Checked = false;
                        }
                    }
                }
                else
                {
                    m_sCheckBox.Checked = true;
                }
            }
            finally
            {
                m_adjustingPosition = false;
            }
        }

        private bool m_adjustingPosition = false;

        enum Tab
        {
            Objects,
            Description
        }

        private static Tab m_lastViewedTab = Tab.Objects;
    }
}
