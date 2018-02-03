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
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Trizbort.UI
{
  internal partial class RoomPropertiesDialog : Form
  {
    private const int HORIZONTAL_MARGIN = 2;
    private const int VERTICAL_MARGIN = 2;
    private const int WIDTH = 24;
    private static Tab mLastViewedTab = Tab.Objects;
    private bool mAdjustingPosition;
    private const string NO_COLOR_SET = "No Color Set";
    private int roomID;

    public RoomPropertiesDialog(PropertiesStartType start, int id)
    {
      InitializeComponent();

      roomID = id;
        
      // load regions control
      cboRegion.Items.Clear();
      foreach (var region in Settings.Regions.OrderBy(p => p.RegionName != Trizbort.Region.DefaultRegion).ThenBy(p => p.RegionName))
      {
        cboRegion.Items.Add(region.RegionName);
      }

      cboRegion.DrawMode = DrawMode.OwnerDrawFixed;
      cboRegion.DrawItem += RegionListBox_DrawItem;

      cboReference.Items.Add("");
      foreach (var room in Project.Current.Elements.OfType<Room>().Where(p => p.ID != roomID).OrderBy(p=>p.Name))
        cboReference.Items.Add(room);

      if (Settings.Regions.Count > 0)
        cboRegion.SelectedIndex = 0;

      if (start == PropertiesStartType.Region)
      {
        m_tabControl.SelectedTabIndex = (int) Tab.Regions;
        cboRegion.Select();
      }
      else
      {
        switch (mLastViewedTab)
        {
          case Tab.Objects:
            m_tabControl.SelectedTabIndex = 0;
            break;
          case Tab.Description:
            m_tabControl.SelectedTabIndex = 1;
            break;
          case Tab.Colors:
            m_tabControl.SelectedTabIndex = 2;
            break;
          case Tab.Regions:
            m_tabControl.SelectedTabIndex = 3;
            break;
        }
        
        if (start == PropertiesStartType.RoomName)
          txtName.Focus();
      }


    }

    public bool RoundedCorners { get => cboDrawType.SelectedItem == itemRoundedCorners; set { if (value) cboDrawType.SelectedItem = itemRoundedCorners; } }
    public bool Ellipse { get => cboDrawType.SelectedItem == itemEllipse; set { if (value) cboDrawType.SelectedItem = itemEllipse; } }
    public bool StraightEdges { get => cboDrawType.SelectedItem == itemStraightEdges; set { if (value) cboDrawType.SelectedItem = itemStraightEdges; } }
    public bool Octagonal { get => cboDrawType.SelectedItem == itemOctagonal; set { if (value) cboDrawType.SelectedItem = itemOctagonal; } }

    public CornerRadii Corners
    {
      get => new CornerRadii()
      {
        BottomLeft = txtBottomLeft.Value,
        BottomRight = txtBottomRight.Value,
        TopRight = txtTopRight.Value,
        TopLeft = txtTopLeft.Value
      };
      set
      {
        txtBottomRight.Value = value.BottomRight;
        txtTopLeft.Value = value.TopLeft;
        txtBottomLeft.Value = value.BottomLeft;
        txtTopRight.Value = value.TopRight;
      }
    }

    public RoomShape Shape
    {
      get => (RoomShape)cboDrawType.SelectedIndex;
      set => cboDrawType.SelectedIndex = (int)value;
    }

    public string RoomName
    {
      get => txtName.Text.Trim();
      set => txtName.Text = value;
    }

    public BorderDashStyle BorderStyle
    {
      get => (BorderDashStyle)Enum.Parse(typeof(BorderDashStyle), cboBorderStyle.SelectedItem.ToString());
      set => cboBorderStyle.SelectedItem = value.ToString();
    }
    public string RoomSubTitle
    {
      get => txtSubTitle.Text;
      set => txtSubTitle.Text = value;
    }

    public string Description
    {
      get => m_descriptionTextBox.Text;
      set => m_descriptionTextBox.Text = value;
    }

    public bool HandDrawnEdges
    {
      get => chkHandDrawnRoom.Checked;
      set => chkHandDrawnRoom.Checked = value;
    }

    public bool IsStartRoom
    {
      get => chkStartRoom.Checked;
      set => chkStartRoom.Checked = value;
    }

    public bool IsEndRoom
    {
      get => chkEndRoom.Checked;
      set => chkEndRoom.Checked = value;
    }

    public bool IsDark
    {
      get => m_isDarkCheckBox.Checked;
      set => m_isDarkCheckBox.Checked = value;
    }

    public string Objects
    {
      get => txtObjects.Text;
      set => txtObjects.Text = value;
    }

    public string RoomRegion
    {
      get => cboRegion.SelectedItem?.ToString() ?? string.Empty;
      set => cboRegion.SelectedItem = value;
    }

    public bool IsReference => cboReference.SelectedItem?.ToString() != string.Empty;
    public Room ReferenceRoom {
      get {
        if (cboReference.SelectedItem != null && cboReference.SelectedItem.ToString() != "") return (Room) cboReference.SelectedItem;
        return null;
      }
      set => cboReference.SelectedItem = value;
    }

    public CompassPoint ObjectsPosition
    {
      get
      {
        if (m_nCheckBox.Checked) return CompassPoint.North;
        if (m_sCheckBox.Checked) return CompassPoint.South;
        if (m_eCheckBox.Checked) return CompassPoint.East;
        if (m_wCheckBox.Checked) return CompassPoint.West;
        if (m_neCheckBox.Checked) return CompassPoint.NorthEast;
        if (m_nwCheckBox.Checked) return CompassPoint.NorthWest;
        if (m_seCheckBox.Checked) return CompassPoint.SouthEast;
        if (m_swCheckBox.Checked) return CompassPoint.SouthWest;
        return CompassPoint.WestSouthWest;
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

    // Added for Room specific colors
    public Color RoomFillColor
    {
      get => m_roomFillTextBox.WatermarkText == NO_COLOR_SET ? Color.Transparent : m_roomFillTextBox.BackColor;
      set
      {
        if (value == Color.Transparent)
        {
          m_roomFillTextBox.BackColor = Color.White;
          m_roomFillTextBox.WatermarkText = NO_COLOR_SET;
        }
        else
        {
          m_roomFillTextBox.BackColor = value;
          m_roomFillTextBox.WatermarkText = string.Empty;
        }
      }
    }

    // Added for Room specific colors
    public Color SecondFillColor
    {
      get
      {
        return m_secondFillTextBox.WatermarkText == NO_COLOR_SET ? Color.Transparent : m_secondFillTextBox.BackColor;
      }
      set
      {
        if (value == Color.Transparent)
        {
          m_secondFillTextBox.BackColor = Color.White;
          m_secondFillTextBox.WatermarkText = NO_COLOR_SET;
        }
        else
        {
          m_secondFillTextBox.BackColor = value;
          m_secondFillTextBox.WatermarkText = string.Empty;
        }
      }
    }

    // Added for Room specific colors
    public string SecondFillLocation
    {
      get
      {
        switch (comboBox1.SelectedIndex)
        {
          case 0:
            return "Bottom";
          case 1:
            return "BottomRight";
          case 2:
            return "BottomLeft";
          case 3:
            return "Left";
          case 4:
            return "Right";
          case 5:
            return "TopRight";
          case 6:
            return "TopLeft";
          case 7:
            return "Top";
          default:
            return "Bottom";
        }
      }
      set
      {
        switch (value)
        {
          case "Bottom":
            comboBox1.SelectedIndex = 0;
            break;
          case "BottomRight":
            comboBox1.SelectedIndex = 1;
            break;
          case "BottomLeft":
            comboBox1.SelectedIndex = 2;
            break;
          case "Left":
            comboBox1.SelectedIndex = 3;
            break;
          case "Right":
            comboBox1.SelectedIndex = 4;
            break;
          case "TopRight":
            comboBox1.SelectedIndex = 5;
            break;
          case "TopLeft":
            comboBox1.SelectedIndex = 6;
            break;
          case "Top":
            comboBox1.SelectedIndex = 7;
            break;
          default:
            comboBox1.SelectedIndex = 0;
            break;
        }
      }
    }

    // Added for Room specific colors
    public Color RoomBorderColor
    {
      get
      {
        return m_roomBorderTextBox.WatermarkText == NO_COLOR_SET ? Color.Transparent : m_roomBorderTextBox.BackColor;
      }
      set
      {
        if (value == Color.Transparent)
        {
          m_roomBorderTextBox.BackColor = Color.White;
          m_roomBorderTextBox.WatermarkText = NO_COLOR_SET;
        }
        else
        {
          m_roomBorderTextBox.BackColor = value;
          m_roomBorderTextBox.WatermarkText = string.Empty;
        }
      }
    }

    // Added for Room specific colors
    public Color RoomTextColor
    {
      get
      {
        return m_roomTextTextBox.WatermarkText == NO_COLOR_SET ? Color.Transparent : m_roomTextTextBox.BackColor;
      }
      set
      {
        if (value == Color.Transparent)
        {
          m_roomTextTextBox.BackColor = Color.White;
          m_roomTextTextBox.WatermarkText = NO_COLOR_SET;
        }
        else
        {
          m_roomTextTextBox.BackColor = value;
          m_roomTextTextBox.WatermarkText = string.Empty;
        }
      }
    }

    // Added for Room specific colors
    public Color ObjectTextColor
    {
      get
      {
        return m_objectTextTextBox.WatermarkText == NO_COLOR_SET ? Color.Transparent : m_objectTextTextBox.BackColor;
      }
      set
      {
        if (value == Color.Transparent)
        {
          m_objectTextTextBox.BackColor = Color.White;
          m_objectTextTextBox.WatermarkText = NO_COLOR_SET;
        }
        else
        {
          m_objectTextTextBox.BackColor = value;
          m_objectTextTextBox.WatermarkText = string.Empty;
        }
      }
    }

    public bool AllCornersEqual { get { return chkCornersSame.Checked; } set { chkCornersSame.Checked = value; } }
    public bool ObjectsCustomPosition { get { return chkCustomPosition.Checked; } set { chkCustomPosition.Checked = value; } }
    public int ObjectsCustomPositionDown { get { return txtDown.Value; } set { txtDown.Value = value; } }
    public int ObjectsCustomPositionRight { get { return txtRight.Value; } set { txtRight.Value = value; } }

    private void RegionListBox_DrawItem(object sender, DrawItemEventArgs e)
    {
      using (var palette = new Palette())
      {
        e.DrawBackground();

        var colorBounds = new Rectangle(e.Bounds.Left + HORIZONTAL_MARGIN, e.Bounds.Top + VERTICAL_MARGIN, WIDTH, e.Bounds.Height - VERTICAL_MARGIN*2);
        var textBounds = new Rectangle(colorBounds.Right + HORIZONTAL_MARGIN, e.Bounds.Top, e.Bounds.Width - colorBounds.Width - HORIZONTAL_MARGIN*2, e.Bounds.Height);
        var firstOrDefault = Settings.Regions.FirstOrDefault(p => p.RegionName == cboRegion.Items[e.Index].ToString());
        if (firstOrDefault != null) e.Graphics.FillRectangle(palette.Brush(firstOrDefault.RColor), colorBounds);
        e.Graphics.DrawRectangle(palette.Pen(e.ForeColor, 0), colorBounds);
        e.Graphics.DrawString(cboRegion.Items[e.Index].ToString(), e.Font, palette.Brush(e.ForeColor), textBounds, StringFormats.Left);
      }
    }

    private void PositionCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (mAdjustingPosition)
        return;

      mAdjustingPosition = true;
      try
      {
        var checkBox = (CheckBox) sender;
        if (checkBox.Checked)
        {
          foreach (Control other in checkBox.Parent.Controls)
          {
            var box = other as CheckBox;
            if (box != null && other != checkBox)
            {
              box.Checked = false;
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
        mAdjustingPosition = false;
      }
    }

    private void changeRoomFillColor()
    {
      if (tabColors.IsSelected) RoomFillColor = Colors.ShowColorDialog(RoomFillColor, this);
    }

    // Added for Room specific colors
    private void changeSecondFillColor()
    {
      if (tabColors.IsSelected) SecondFillColor = Colors.ShowColorDialog(SecondFillColor, this);
    }

    private void changeRoomTextColor()
    {
      if (tabColors.IsSelected) RoomTextColor = Colors.ShowColorDialog(RoomTextColor, this);
    }

    private void changeRoomBorderColor()
    {
      if (tabColors.IsSelected) RoomBorderColor = Colors.ShowColorDialog(RoomBorderColor, this);
    }

    private void changeObjectTextColor()
    {
      if (tabColors.IsSelected) ObjectTextColor = Colors.ShowColorDialog(ObjectTextColor, this);
    }

    private void m_changeLargeFontButton_Click(object sender, EventArgs e)
    {
      changeRoomFillColor();
    }

    // Added for Room specific colors
    private void m_changeSecondFillButton_Click(object sender, EventArgs e)
    {
      changeSecondFillColor();
    }

    // Added for Room specific colors
    private void button1_Click(object sender, EventArgs e)
    {
      changeRoomBorderColor();
    }

    // Added for Room specific colors
    private void button2_Click(object sender, EventArgs e)
    {
      changeRoomTextColor();
    }

    // Added for Room specific colors
    private void button3_Click(object sender, EventArgs e)
    {
      changeObjectTextColor();
    }

    private enum Tab
    {
      Objects, 
      Description,
      Colors,
      Regions
    }

    private void RoomPropertiesDialog_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Alt)
      {
        switch (e.KeyCode)
        {
          case Keys.Y:
            cboBorderStyle.Focus();
            break;

          case Keys.F:
            changeRoomFillColor();
            break;

          case Keys.S:
            changeSecondFillColor();
            break;

          case Keys.B:
            changeRoomBorderColor();
            break;

          case Keys.T:
            changeRoomTextColor();
            break;

          case Keys.J:
            changeObjectTextColor();
            break;

          case Keys.O:
            m_tabControl.SelectedTabIndex = (int) Tab.Objects;
            setObjectsTabFocus();
            break;

          case Keys.E:
            m_tabControl.SelectedTabIndex = (int) Tab.Description;
            setDescriptionTabFocus();
            break;

          case Keys.G:
            m_tabControl.SelectedTabIndex = (int) Tab.Regions;
            setRegionsTabFocus();
            break;

          case Keys.C:
            m_tabControl.SelectedTabIndex = (int) Tab.Colors;
            setColorsTabFocus();
            break;
        }
      }
    }

    private void setColorsTabFocus()
    {
      m_changeRoomFillButton.Focus();
    }

    private void setRegionsTabFocus()
    {
      cboRegion.Focus();
    }

    private void setDescriptionTabFocus()
    {
      m_descriptionTextBox.Focus();
      m_descriptionTextBox.SelectAll();
    }

    private void setObjectsTabFocus()
    {
      txtObjects.Focus();
    }


    private void m_tabControl_Enter(object sender, EventArgs e)
    {
      switch (m_tabControl.SelectedTabIndex)
      {
        case (int)Tab.Objects:
          setObjectsTabFocus();
          break;
        case (int)Tab.Description:
          setDescriptionTabFocus();
          break;
        case (int)Tab.Regions:
          setRegionsTabFocus();
          break;
        case (int)Tab.Colors:
          setColorsTabFocus();
          break;
      }
    }



    private void m_roomFillTextBox_ButtonCustomClick(object sender, EventArgs e)
    {
      RoomFillColor = Color.Transparent;
      m_changeRoomFillButton.Focus();
    }

    private void m_secondFillTextBox_ButtonCustomClick(object sender, EventArgs e)
    {
      SecondFillColor = Color.Transparent;
      m_changeSecondFillButton.Focus();
    }

    private void m_roomBorderTextBox_ButtonCustomClick(object sender, EventArgs e)
    {
      RoomBorderColor = Color.Transparent;
      m_changeRoomBorderButton.Focus();
    }

    private void m_roomTextTextBox_ButtonCustomClick(object sender, EventArgs e)
    {
      RoomTextColor = Color.Transparent;
      m_changeRoomTextButton.Focus();
    }

    private void m_objectTextTextBox_ButtonCustomClick(object sender, EventArgs e)
    {
      ObjectTextColor = Color.Transparent;
      m_changeObjectTextButton.Focus();
    }

    private void m_roomFillTextBox_DoubleClick(object sender, EventArgs e)
    {
      changeRoomFillColor();
    }

    private void m_secondFillTextBox_DoubleClick(object sender, EventArgs e)
    {
      changeSecondFillColor();
    }

    private void m_roomBorderTextBox_DoubleClick(object sender, EventArgs e)
    {
      changeRoomBorderColor();
    }

    private void m_roomTextTextBox_DoubleClick(object sender, EventArgs e)
    {
      changeRoomTextColor();
    }

    private void m_objectTextTextBox_DoubleClick(object sender, EventArgs e)
    {
      changeObjectTextColor();
    }

    private void m_roomFillTextBox_Enter(object sender, EventArgs e)
    {
      m_changeRoomFillButton.Focus();
    }

    private void m_secondFillTextBox_Enter(object sender, EventArgs e)
    {
      m_changeSecondFillButton.Focus();
    }

    private void m_roomTextTextBox_Enter(object sender, EventArgs e)
    {
      m_changeRoomTextButton.Focus();
    }

    private void m_objectTextTextBox_Enter(object sender, EventArgs e)
    {
      m_changeObjectTextButton.Focus();
    }

    private void m_roomBorderTextBox_Enter(object sender, EventArgs e)
    {
      m_changeRoomBorderButton.Focus();
    }

    private void pnlSampleRoomShape_Paint(object sender, PaintEventArgs e)
    {
      var graph = pnlSampleRoomShape.CreateGraphics();
      var path = new GraphicsPath();
      var pen = new Pen(Color.Black,2.0f);

      var rect = new RectangleF(10,10, 3 * Settings.GridSize, 2 * Settings.GridSize);

      if (cboDrawType.SelectedItem == itemRoundedCorners)
      {
        var corners = new CornerRadii()
        {
          BottomLeft = txtBottomLeft.Value,
          BottomRight = txtBottomRight.Value,
          TopRight = txtTopRight.Value,
          TopLeft = txtTopLeft.Value
        };

        path.AddArc(rect.X + rect.Width - ((float)corners.TopRight * 2), rect.Y, (float)corners.TopRight * 2, (float)corners.TopRight * 2, 270, 90);
        path.AddArc(rect.X + rect.Width - ((float)corners.BottomRight * 2), rect.Y + rect.Height - ((float)corners.BottomRight * 2), (float)corners.BottomRight * 2, (float)corners.BottomRight * 2, 0, 90);
        path.AddArc(rect.X, rect.Y + rect.Height - ((float)corners.BottomLeft * 2), (float)corners.BottomLeft * 2, (float)corners.BottomLeft * 2, 90, 90);
        path.AddArc(rect.X, rect.Y, (float)corners.TopLeft * 2, (float)corners.TopLeft * 2, 180, 90);
        path.CloseFigure();
      }
      else if (cboDrawType.SelectedItem == itemEllipse)
      {
        path.AddEllipse(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
      }
      else if (cboDrawType.SelectedItem == itemOctagonal)
      {
        path.AddLine(rect.X, rect.Y + rect.Height/4, rect.X, rect.Y + 3*rect.Height/4);
        path.AddLine(rect.X, rect.Y + 3*rect.Height/4, rect.X + rect.Width/4, rect.Y + rect.Height);
        path.AddLine(rect.X + rect.Width / 4, rect.Y + rect.Height, rect.X + 3*rect.Width/4, rect.Y + rect.Height);
        path.AddLine(rect.X + 3*rect.Width/4, rect.Y + rect.Height, rect.X+rect.Width, rect.Y + 3*rect.Height/4);
        path.AddLine(rect.X + rect.Width, rect.Y + rect.Height/4, rect.X + rect.Width, rect.Y + 3*rect.Height/4);
        path.AddLine(rect.X + rect.Width, rect.Y + rect.Height / 4, rect.X + 3*rect.Width/4, rect.Y);
        path.AddLine(rect.X + 3*rect.Width/4, rect.Y, rect.X + rect.Width/4, rect.Y);
        path.AddLine(rect.X + rect.Width/4, rect.Y, rect.X, rect.Y + rect.Height/4);
      }
      else if (cboDrawType.SelectedItem == itemStraightEdges)
      {
        path.AddRectangle(rect);
      }
      graph.DrawPath(pen,path);
    }


    private void redrawSampleOnChange(object sender, EventArgs e)
    {
      if ((sender == txtTopLeft) && (chkCornersSame.Checked))
      {
        txtBottomLeft.Value = txtTopLeft.Value;
        txtBottomRight.Value = txtTopLeft.Value;
        txtTopRight.Value = txtTopLeft.Value;
      }
      pnlSampleRoomShape.Invalidate();
    }

    private void cboDrawType_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cboDrawType.SelectedItem == itemEllipse)
      {
        groupRoundedCorners.Visible = false;
      }
      else if (cboDrawType.SelectedItem == itemRoundedCorners)
      {
        groupRoundedCorners.Location = new Point(8, 44);
        groupRoundedCorners.Visible = true;
      }
      else
      {
        groupRoundedCorners.Visible = false;
      }

      pnlSampleRoomShape.Invalidate();
    }

    private void chkCornersSame_CheckedChanged(object sender, EventArgs e)
    {
      txtBottomLeft.Enabled = !chkCornersSame.Checked;
      txtBottomRight.Enabled = !chkCornersSame.Checked;
      txtTopRight.Enabled = !chkCornersSame.Checked;
      if (chkCornersSame.Checked)
      {
        txtBottomLeft.Value = txtTopLeft.Value;
        txtBottomRight.Value = txtTopLeft.Value;
        txtTopRight.Value = txtTopLeft.Value;
      }
    }

    private void chkStartRoom_CheckedChanged(object sender, EventArgs e)
    {
      if (chkStartRoom.Checked)
      {
        var list = Project.Current.Elements.OfType<Room>().Where(p => p.IsStartRoom && p.ID != roomID).ToList();

        if (list.Count <= 0) return;

        if (MessageBox.Show($"The room '{list.First().Name}' is set as the starting room.  Do you want to change it to this room?", "Change Starting Room", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          Project.Current.Elements.OfType<Room>().ToList().ForEach(p => p.IsStartRoom = false);
        else
          chkStartRoom.Checked = false;
      }
    }

    private void m_okButton_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(txtName.Text))
      {
        MessageBox.Show("The room name can't be empty. Please put something in there.", "Empty name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtName.Focus();
        DialogResult = DialogResult.None;
      }
      if (!txtName.Text.Any(Char.IsLetter))
      {
        MessageBox.Show("The room name must contain one letter.", "Non-alphabetic name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtName.Focus();
        DialogResult = DialogResult.None;
      }

    }

    private void m_tabControl_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
    {
      switch (m_tabControl.SelectedTabIndex)
      {
        default:
          mLastViewedTab = Tab.Objects;
          break;
        case 1:
          mLastViewedTab = Tab.Description;
          break;
        case 2:
          mLastViewedTab = Tab.Colors;
          break;
        case 3:
          mLastViewedTab = Tab.Regions;
          break;
      }

    }

    private void lblObjectSyntaxHelp_Click(object sender, EventArgs e)
    {
      pnlObjectSyntaxHelp.Visible = !pnlObjectSyntaxHelp.Visible;
    }

    private void chkHandDrawnRoom_CheckedChanged(object sender, EventArgs e)
    {

    }



    private void txtObjects_KeyDown(object sender, KeyEventArgs e)
    {
      SelectAllHandler(sender, e);
    }

    private static void SelectAllHandler(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.A)
      {
        ((TextBox) sender).SelectAll();
        e.Handled = true;
      }
    }

    private void m_descriptionTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      SelectAllHandler(sender,e);
    }

    private void toolTip_BeforeTooltipDisplay(object sender, DevComponents.DotNetBar.SuperTooltipEventArgs e)
    {
      e.TooltipInfo.BodyText = e.TooltipInfo.BodyText.Replace("{gridsize}", Settings.GridSize.ToString(CultureInfo.CurrentCulture));
    }

    private void chkEndRoom_CheckedChanged(object sender, EventArgs e)
    {
      if (chkEndRoom.Checked)
      {
        var list = Project.Current.Elements.OfType<Room>().Where(p => p.IsEndRoom && p.ID != roomID).ToList();

        if (list.Count <= 0) return;

        if (MessageBox.Show($"The room '{list.First().Name}' is set as the ending room.  Do you want to change it to this room?", "Change Ending Room", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          Project.Current.Elements.OfType<Room>().ToList().ForEach(p => p.IsEndRoom = false);
        else
          chkEndRoom.Checked = false;
      }
    }
  }
}