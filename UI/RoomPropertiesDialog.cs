using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Domain.Misc;
using Trizbort.Setup;
using Trizbort.Util;

namespace Trizbort.UI {
  internal partial class RoomPropertiesDialog : Form {
    private const int HORIZONTAL_MARGIN = 2;
    private const int VERTICAL_MARGIN = 2;
    private const int WIDTH = 24;
    private const string NO_COLOR_SET = "No Color Set";
    private static int mLastViewedTab = (int)Tab.Description;
    private readonly int roomID;
    private bool mAdjustingPosition;

    public RoomPropertiesDialog(PropertiesStartType start, int id) {
      InitializeComponent();

      roomID = id;

      // load regions control
      cboRegion.Items.Clear();
      KeyPreview = true;
      foreach (var region in Settings.Regions.OrderBy(p => p.RegionName != Domain.Misc.Region.DefaultRegion)
        .ThenBy(p => p.RegionName)) cboRegion.Items.Add(region.RegionName);

      cboRegion.DrawMode = DrawMode.OwnerDrawFixed;
      cboRegion.DrawItem += RegionListBox_DrawItem;

      cboReference.Items.Add("");
      foreach (var room in Project.Current.Elements.OfType<Room>().Where(p => p.ID != roomID).OrderBy(p => p.Name))
        cboReference.Items.Add(room);

      if (Settings.Regions.Count > 0)
        cboRegion.SelectedIndex = 0;

      if (start == PropertiesStartType.Region) {
        m_tabControl.SelectedTab = tabRegions;
        cboRegion.Select();
      } else {
        m_tabControl.SelectedTab = mLastViewedTab switch {
          (int) Tab.Objects => tabObjects,
          (int) Tab.Description => tabDescription,
          (int) Tab.Colors => tabColors,
          (int) Tab.Regions => tabRegions,
          (int) Tab.RoomShapes => tabRoomShapes,
          _ => m_tabControl.SelectedTab
        };

        if (start == PropertiesStartType.RoomName)
          txtName.Focus();
      }
    }

    public bool AllCornersEqual {
      get => chkCornersSame.Checked;
      set => chkCornersSame.Checked = value;
    }

    public BorderDashStyle BorderStyle {
      get => (BorderDashStyle) Enum.Parse(typeof(BorderDashStyle), cboBorderStyle.SelectedItem.ToString());
      set => cboBorderStyle.SelectedItem = value.ToString();
    }

    public CornerRadii Corners {
      get => new CornerRadii {
        BottomLeft = (double) txtBottomLeft.Value,
        BottomRight = (double) txtBottomRight.Value,
        TopRight = (double) txtTopRight.Value,
        TopLeft = (double) txtTopLeft.Value
      };
      set {
        txtBottomRight.Value = new decimal(value.BottomRight);
        txtTopLeft.Value = new decimal(value.TopLeft);
        txtBottomLeft.Value = new decimal(value.BottomLeft);
        txtTopRight.Value = new decimal(value.TopRight);
      }
    }

    public string Description {
      get => m_descriptionTextBox.Text;
      set => m_descriptionTextBox.Text = value;
    }

    public bool Ellipse {
      get => cboDrawType.SelectedItem.ToString() == "Ellipse";
      set {
        if (value) cboDrawType.SelectedItem = "Ellipse";
      }
    }

    public bool HandDrawnEdges {
      get => chkHandDrawnRoom.Checked;
      set => chkHandDrawnRoom.Checked = value;
    }

    public bool IsDark {
      get => m_isDarkCheckBox.Checked;
      set => m_isDarkCheckBox.Checked = value;
    }

    public bool IsEndRoom {
      get => chkEndRoom.Checked;
      set => chkEndRoom.Checked = value;
    }

    public bool IsReference => cboReference.SelectedItem?.ToString() != string.Empty;

    public bool IsStartRoom {
      get => chkStartRoom.Checked;
      set => chkStartRoom.Checked = value;
    }

    public string Objects {
      get => txtObjects.Text;
      set => txtObjects.Text = value;
    }

    public bool ObjectsCustomPosition {
      get => chkCustomPosition.Checked;
      set => chkCustomPosition.Checked = value;
    }

    public int ObjectsCustomPositionDown {
      get => (int) txtDown.Value;
      set => txtDown.Value = value;
    }

    public int ObjectsCustomPositionRight {
      get => (int) txtRight.Value;
      set => txtRight.Value = value;
    }

    public CompassPoint ObjectsPosition {
      get {
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
      set {
        switch (value) {
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
    public Color ObjectTextColor {
      get => m_objectTextTextBox.Watermark == NO_COLOR_SET ? Color.Transparent : m_objectTextTextBox.BackColor;
      set {
        if (value == Color.Transparent) {
          m_objectTextTextBox.BackColor = Color.White;
          m_objectTextTextBox.Watermark = NO_COLOR_SET;
        } else {
          m_objectTextTextBox.BackColor = value;
          m_objectTextTextBox.Watermark = string.Empty;
        }
      }
    }

    public bool Octagonal {
      get => cboDrawType.SelectedItem.ToString() == "Octagonal";
      set {
        if (value) cboDrawType.SelectedItem = "Octagonal";
      }
    }

    public Room ReferenceRoom {
      get {
        if (cboReference.SelectedItem != null && cboReference.SelectedItem.ToString() != "")
          return (Room) cboReference.SelectedItem;
        return null;
      }
      set => cboReference.SelectedItem = value;
    }

    // Added for Room specific colors
    public Color RoomBorderColor {
      get => m_roomBorderTextBox.Watermark == NO_COLOR_SET ? Color.Transparent : m_roomBorderTextBox.BackColor;
      set {
        if (value == Color.Transparent) {
          m_roomBorderTextBox.BackColor = Color.White;
          m_roomBorderTextBox.Watermark = NO_COLOR_SET;
        } else {
          m_roomBorderTextBox.BackColor = value;
          m_roomBorderTextBox.Watermark = string.Empty;
        }
      }
    }

    // Added for Room specific colors
    public Color RoomFillColor {
      get => m_roomFillTextBox.Watermark == NO_COLOR_SET ? Color.Transparent : m_roomFillTextBox.BackColor;
      set {
        if (value == Color.Transparent) {
          m_roomFillTextBox.BackColor = Color.White;
          m_roomFillTextBox.Watermark = NO_COLOR_SET;
        } else {
          m_roomFillTextBox.BackColor = value;
          m_roomFillTextBox.Watermark = string.Empty;
        }
      }
    }

    public string RoomName {
      get => txtName.Text.Trim();
      set => txtName.Text = value;
    }

    // Added for Room specific colors
    public Color RoomNameColor {
      get => m_roomTextTextBox.Watermark == NO_COLOR_SET ? Color.Transparent : m_roomTextTextBox.BackColor;
      set {
        if (value == Color.Transparent) {
          m_roomTextTextBox.BackColor = Color.White;
          m_roomTextTextBox.Watermark = NO_COLOR_SET;
        } else {
          m_roomTextTextBox.BackColor = value;
          m_roomTextTextBox.Watermark = string.Empty;
        }
      }
    }

    public string RoomRegion {
      get => cboRegion.SelectedItem?.ToString() ?? string.Empty;
      set => cboRegion.SelectedItem = value;
    }

    public string RoomSubTitle {
      get => txtSubTitle.Text;
      set => txtSubTitle.Text = value;
    }

    public Color RoomSubtitleColor {
      get => m_subTitleTextTextBox.Watermark == NO_COLOR_SET ? Color.Transparent : m_subTitleTextTextBox.BackColor;
      set {
        if (value == Color.Transparent) {
          m_subTitleTextTextBox.BackColor = Color.White;
          m_subTitleTextTextBox.Watermark = NO_COLOR_SET;
        } else {
          m_subTitleTextTextBox.BackColor = value;
          m_subTitleTextTextBox.Watermark = string.Empty;
        }
      }
    }

    public bool RoundedCorners {
      get => cboDrawType.SelectedItem.ToString() == "Rounded Corners";
      set {
        if (value) cboDrawType.SelectedItem = "Rounded Corners";
      }
    }

    // Added for Room specific colors
    public Color SecondFillColor {
      get => m_secondFillTextBox.Watermark == NO_COLOR_SET ? Color.Transparent : m_secondFillTextBox.BackColor;
      set {
        if (value == Color.Transparent) {
          m_secondFillTextBox.BackColor = Color.White;
          m_secondFillTextBox.Watermark = NO_COLOR_SET;
        } else {
          m_secondFillTextBox.BackColor = value;
          m_secondFillTextBox.Watermark = string.Empty;
        }
      }
    }

    // Added for Room specific colors
    public string SecondFillLocation {
      get {
        switch (comboBox1.SelectedIndex) {
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
      set {
        switch (value) {
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

    public RoomShape Shape {
      get => (RoomShape) cboDrawType.SelectedIndex;
      set => cboDrawType.SelectedIndex = (int) value;
    }

    public bool StraightEdges {
      get => cboDrawType.SelectedItem.ToString() == "Straight Edges";
      set {
        if (value) cboDrawType.SelectedItem = "Straight Edges";
      }
    }

    // Added for Room specific colors
    private void button1_Click(object sender, EventArgs e) {
      changeRoomBorderColor();
    }

    // Added for Room specific colors
    private void button2_Click(object sender, EventArgs e) {
      changeRoomTextColor();
    }

    // Added for Room specific colors
    private void button3_Click(object sender, EventArgs e) {
      changeObjectTextColor();
    }

    private void cboDrawType_SelectedIndexChanged(object sender, EventArgs e) {
      if (cboDrawType.SelectedItem.ToString() == "Ellipse") {
        groupRoundedCorners.Visible = false;
      } else if (cboDrawType.SelectedItem.ToString() == "Rounded Corners") {
        groupRoundedCorners.Location = new Point(cboDrawType.Left, cboDrawType.Bottom + 20);
        groupRoundedCorners.Visible = true;
      } else {
        groupRoundedCorners.Visible = false;
      }

      // Hand drawn style is currently only implemented for "Straight Edges" line style.
      chkHandDrawnRoom.Enabled = (cboDrawType.SelectedItem.ToString() == "Straight Edges");

      pnlSampleRoomShape.Invalidate();
    }

    private void changeObjectTextColor() {
      if (m_tabControl.SelectedTab == tabColors) ObjectTextColor = Colors.ShowColorDialog(ObjectTextColor, this);
    }

    private void changeRoomBorderColor() {
      if (m_tabControl.SelectedTab == tabColors) RoomBorderColor = Colors.ShowColorDialog(RoomBorderColor, this);
    }

    private void changeRoomFillColor() {
      if (m_tabControl.SelectedTab == tabColors) RoomFillColor = Colors.ShowColorDialog(RoomFillColor, this);
    }

    private void changeRoomTextColor() {
      if (m_tabControl.SelectedTab == tabColors) RoomNameColor = Colors.ShowColorDialog(RoomNameColor, this);
    }

    // Added for Room specific colors
    private void changeSecondFillColor() {
      if (m_tabControl.SelectedTab == tabColors) SecondFillColor = Colors.ShowColorDialog(SecondFillColor, this);
    }

    private void changeSubtitleColor() {
      if (m_tabControl.SelectedTab == tabColors) RoomSubtitleColor = Colors.ShowColorDialog(RoomSubtitleColor, this);
    }

    private void chkCornersSame_CheckedChanged(object sender, EventArgs e) {
      txtBottomLeft.Enabled = !chkCornersSame.Checked;
      txtBottomRight.Enabled = !chkCornersSame.Checked;
      txtTopRight.Enabled = !chkCornersSame.Checked;
      if (chkCornersSame.Checked) {
        txtBottomLeft.Value = txtTopLeft.Value;
        txtBottomRight.Value = txtTopLeft.Value;
        txtTopRight.Value = txtTopLeft.Value;
      }
    }

    private void chkHandDrawnRoom_CheckedChanged(object sender, EventArgs e) { }

    private void chkStartRoom_CheckedChanged(object sender, EventArgs e) {
      if (chkStartRoom.Checked) {
        var list = Project.Current.Elements.OfType<Room>().Where(p => p.IsStartRoom && p.ID != roomID).ToList();

        if (list.Count <= 0) return;

        if (MessageBox.Show(
              $"The room '{list.First().Name}' is set as the starting room.  Do you want to change it to this room?",
              "Change Starting Room", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
          Project.Current.Elements.OfType<Room>().ToList().ForEach(p => p.IsStartRoom = false);
        else
          chkStartRoom.Checked = false;
      }
    }

    private void lblObjectSyntaxHelp_Click(object sender, EventArgs e) {
      pnlObjectSyntaxHelp.Visible = !pnlObjectSyntaxHelp.Visible;
    }

    private void m_changeLargeFontButton_Click(object sender, EventArgs e) {
      changeRoomFillColor();
    }

    // Added for Room specific colors
    private void m_changeSecondFillButton_Click(object sender, EventArgs e) {
      changeSecondFillColor();
    }

    private void m_changeSubtitleTextButton_Click(object sender, EventArgs e) {
      changeSubtitleColor();
    }

    private void m_descriptionTextBox_KeyDown(object sender, KeyEventArgs e) {
      SelectAllHandler(sender, e);
    }

    private void m_objectTextTextBox_ButtonCustomClick(object sender, EventArgs e) {
      ObjectTextColor = Color.Transparent;
      m_changeObjectTextButton.Focus();
    }

    private void m_objectTextTextBox_DoubleClick(object sender, EventArgs e) {
      changeObjectTextColor();
    }

    private void m_objectTextTextBox_Enter(object sender, EventArgs e) {
      m_changeObjectTextButton.Focus();
    }

    private void m_okButton_Click(object sender, EventArgs e) {
      if (string.IsNullOrWhiteSpace(txtName.Text)) {
        MessageBox.Show("The room name can't be empty. Please put something in there.", "Empty name",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtName.Focus();
        DialogResult = DialogResult.None;
      } else if (!txtName.Text.Any(char.IsLetter)) {
        MessageBox.Show("The room name must contain one letter.", "Non-alphabetic name", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        txtName.Focus();
        DialogResult = DialogResult.None;
      }
    }

    private void m_roomBorderTextBox_ButtonCustomClick(object sender, EventArgs e) {
      RoomBorderColor = Color.Transparent;
      m_changeRoomBorderButton.Focus();
    }

    private void m_roomBorderTextBox_DoubleClick(object sender, EventArgs e) {
      changeRoomBorderColor();
    }

    private void m_roomBorderTextBox_Enter(object sender, EventArgs e) {
      m_changeRoomBorderButton.Focus();
    }


    private void m_roomFillTextBox_ButtonCustomClick(object sender, EventArgs e) {
      RoomFillColor = Color.Transparent;
      m_changeRoomFillButton.Focus();
    }

    private void m_roomFillTextBox_DoubleClick(object sender, EventArgs e) {
      changeRoomFillColor();
    }

    private void m_roomFillTextBox_Enter(object sender, EventArgs e) {
      m_changeRoomFillButton.Focus();
    }

    private void m_roomTextTextBox_ButtonCustomClick(object sender, EventArgs e) {
      RoomNameColor = Color.Transparent;
      m_changeRoomTextButton.Focus();
    }

    private void m_roomTextTextBox_DoubleClick(object sender, EventArgs e) {
      changeRoomTextColor();
    }

    private void m_roomTextTextBox_Enter(object sender, EventArgs e) {
      m_changeRoomTextButton.Focus();
    }

    private void m_secondFillTextBox_ButtonCustomClick(object sender, EventArgs e) {
      SecondFillColor = Color.Transparent;
      m_changeSecondFillButton.Focus();
    }

    private void m_secondFillTextBox_DoubleClick(object sender, EventArgs e) {
      changeSecondFillColor();
    }

    private void m_secondFillTextBox_Enter(object sender, EventArgs e) {
      m_changeSecondFillButton.Focus();
    }

    private void m_subTitleTextTextBox_ButtonCustomClick(object sender, EventArgs e) {
      RoomSubtitleColor = Color.Transparent;
      m_changeSubtitleTextButton.Focus();
    }

    private void m_subTitleTextTextBox_DoubleClick(object sender, EventArgs e) {
      changeSubtitleColor();
    }

    private void m_subTitleTextTextBox_Enter(object sender, EventArgs e) {
      m_changeSubtitleTextButton.Focus();
    }


    private void m_tabControl_Enter(object sender, EventArgs e) {
      switch (m_tabControl.SelectedIndex) {
        case (int) Tab.Objects:
          setObjectsTabFocus();
          break;
        case (int) Tab.Description:
          setDescriptionTabFocus();
          break;
        case (int) Tab.Regions:
          setRegionsTabFocus();
          break;
        case (int) Tab.Colors:
          setColorsTabFocus();
          break;
      }
    }


    private void pnlSampleRoomShape_Paint(object sender, PaintEventArgs e) {
      var graph = pnlSampleRoomShape.CreateGraphics();
      var path = new GraphicsPath();
      var pen = new Pen(Color.Black, 2.0f);

      var rect = new RectangleF(10, 10, 3 * Settings.GridSize, 2 * Settings.GridSize);

      if (cboDrawType.SelectedItem.ToString() == "Rounded Corners") {
        var corners = new CornerRadii {
          BottomLeft = (double) txtBottomLeft.Value,
          BottomRight = (double) txtBottomRight.Value,
          TopRight = (double) txtTopRight.Value,
          TopLeft = (double) txtTopLeft.Value
        };

        path.AddArc(rect.X + rect.Width - (float) corners.TopRight * 2, rect.Y, (float) corners.TopRight * 2,
          (float) corners.TopRight * 2, 270, 90);
        path.AddArc(rect.X + rect.Width - (float) corners.BottomRight * 2,
          rect.Y + rect.Height - (float) corners.BottomRight * 2, (float) corners.BottomRight * 2,
          (float) corners.BottomRight * 2, 0, 90);
        path.AddArc(rect.X, rect.Y + rect.Height - (float) corners.BottomLeft * 2, (float) corners.BottomLeft * 2,
          (float) corners.BottomLeft * 2, 90, 90);
        path.AddArc(rect.X, rect.Y, (float) corners.TopLeft * 2, (float) corners.TopLeft * 2, 180, 90);
        path.CloseFigure();
      } else if (cboDrawType.SelectedItem.ToString() == "Ellipse") {
        path.AddEllipse(new RectangleF(rect.X, rect.Y, rect.Width, rect.Height));
      } else if (cboDrawType.SelectedItem.ToString() == "Octagonal") {
        path.AddLine(rect.X, rect.Y + rect.Height / 4, rect.X, rect.Y + 3 * rect.Height / 4);
        path.AddLine(rect.X, rect.Y + 3 * rect.Height / 4, rect.X + rect.Width / 4, rect.Y + rect.Height);
        path.AddLine(rect.X + rect.Width / 4, rect.Y + rect.Height, rect.X + 3 * rect.Width / 4, rect.Y + rect.Height);
        path.AddLine(rect.X + 3 * rect.Width / 4, rect.Y + rect.Height, rect.X + rect.Width,
          rect.Y + 3 * rect.Height / 4);
        path.AddLine(rect.X + rect.Width, rect.Y + rect.Height / 4, rect.X + rect.Width, rect.Y + 3 * rect.Height / 4);
        path.AddLine(rect.X + rect.Width, rect.Y + rect.Height / 4, rect.X + 3 * rect.Width / 4, rect.Y);
        path.AddLine(rect.X + 3 * rect.Width / 4, rect.Y, rect.X + rect.Width / 4, rect.Y);
        path.AddLine(rect.X + rect.Width / 4, rect.Y, rect.X, rect.Y + rect.Height / 4);
      } else if (cboDrawType.SelectedItem.ToString() == "Straight Edges") {
        path.AddRectangle(rect);
      }

      graph.DrawPath(pen, path);
    }

    private void PositionCheckBox_CheckedChanged(object sender, EventArgs e) {
      if (mAdjustingPosition)
        return;

      mAdjustingPosition = true;
      try {
        var checkBox = (CheckBox) sender;
        if (checkBox.Checked)
          foreach (Control other in checkBox.Parent.Controls) {
            var box = other as CheckBox;
            if (box != null && other != checkBox) box.Checked = false;
          }
        else
          m_sCheckBox.Checked = true;
      }
      finally {
        mAdjustingPosition = false;
      }
    }


    private void redrawSampleOnChange(object sender, EventArgs e) {
      if (sender == txtTopLeft && chkCornersSame.Checked) {
        txtBottomLeft.Value = txtTopLeft.Value;
        txtBottomRight.Value = txtTopLeft.Value;
        txtTopRight.Value = txtTopLeft.Value;
      }

      pnlSampleRoomShape.Invalidate();
    }

    private void RegionListBox_DrawItem(object sender, DrawItemEventArgs e) {
      using (var palette = new Palette()) {
        e.DrawBackground();

        var colorBounds = new Rectangle(e.Bounds.Left + HORIZONTAL_MARGIN, e.Bounds.Top + VERTICAL_MARGIN, WIDTH,
          e.Bounds.Height - VERTICAL_MARGIN * 2);
        var textBounds = new Rectangle(colorBounds.Right + HORIZONTAL_MARGIN, e.Bounds.Top,
          e.Bounds.Width - colorBounds.Width - HORIZONTAL_MARGIN * 2, e.Bounds.Height);
        var firstOrDefault = Settings.Regions.FirstOrDefault(p => p.RegionName == cboRegion.Items[e.Index].ToString());
        if (firstOrDefault != null) e.Graphics.FillRectangle(palette.Brush(firstOrDefault.RColor), colorBounds);
        e.Graphics.DrawRectangle(palette.Pen(e.ForeColor, 0), colorBounds);
        e.Graphics.DrawString(cboRegion.Items[e.Index].ToString(), e.Font, palette.Brush(e.ForeColor), textBounds,
          StringFormats.Left);
      }
    }

    private void RoomPropertiesDialog_KeyUp(object sender, KeyEventArgs e) {
      if (e.Alt)
        switch (e.KeyCode) {
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
            m_tabControl.SelectedIndex = (int) Tab.Objects;
            setObjectsTabFocus();
            break;

          case Keys.E:
            m_tabControl.SelectedIndex = (int) Tab.Description;
            setDescriptionTabFocus();
            break;

          case Keys.G:
            m_tabControl.SelectedIndex = (int) Tab.Regions;
            setRegionsTabFocus();
            break;

          case Keys.C:
            m_tabControl.SelectedIndex = (int) Tab.Colors;
            setColorsTabFocus();
            break;
        }
    }

    private static void SelectAllHandler(object sender, KeyEventArgs e) {
      if (e.Control && e.KeyCode == Keys.A) {
        ((TextBox) sender).SelectAll();
        e.Handled = true;
      }
    }

    private void setColorsTabFocus() {
      m_changeRoomFillButton.Focus();
    }

    private void setDescriptionTabFocus() {
      m_descriptionTextBox.Focus();
      m_descriptionTextBox.SelectAll();
    }

    private void setObjectsTabFocus() {
      txtObjects.Focus();
    }

    private void setRegionsTabFocus() {
      cboRegion.Focus();
    }

    private void txtObjects_KeyDown(object sender, KeyEventArgs e) {
      SelectAllHandler(sender, e);
    }

    private void m_tabControl_SelectedIndexChanged(object sender, EventArgs e) {

      mLastViewedTab = m_tabControl.SelectedIndex;
      
      // switch (m_tabControl.SelectedIndex) {
      //   case (int)Tab.Description:
      //     mLastViewedTab = Tab.Description;
      //     break;
      //   case (int)Tab.Colors:
      //     mLastViewedTab = Tab.Colors;
      //     break;
      //   case (int)Tab.Regions:
      //     mLastViewedTab = Tab.Regions;
      //     break;
      //   case (int)Tab.RoomShapes:
      //     mLastViewedTab = Tab.RoomShapes;
      //     break;
      //   default:
      //     mLastViewedTab = Tab.Objects;
      //     break;
      // }
    }

    private enum Tab {
      Description,
      Objects,
      Colors,
      Regions,
      RoomShapes
    }
  }
}