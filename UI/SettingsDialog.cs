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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Trizbort.Domain.Application;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Misc;
using Trizbort.Setup;
using Trizbort.Util;
using Region = Trizbort.Domain.Misc.Region;

namespace Trizbort.UI {
  public partial class SettingsDialog : Form {
    private const int HORIZONTAL_MARGIN = 2;
    private const int VERTICAL_MARGIN = 2;
    private const int WIDTH = 24;
    private readonly TextBox editBox;
    private bool bUpdatingRegionText;
    private int itemSelected;
    private Region mCurrentRegion;
    private Font mLargeFont;
    private Font mLineFont;
    private Font mSmallFont;
    private Font mSubtitleFont;

    public SettingsDialog() {
      ElementColors = new Color[Colors.Count];
      Regions = Settings.Regions;
      InitializeComponent();

      editBox = new TextBox {Location = new Point(0, 0), Size = new Size(0, 0), Font = new Font("Tahoma", 8.25f), AcceptsReturn = true};
      editBox.Hide();

      m_RegionListing.Controls.AddRange(new Control[] {editBox});
      editBox.Text = string.Empty;
      editBox.BorderStyle = BorderStyle.FixedSingle;

      editBox.KeyPress += editBoxKeyPress;
      editBox.LostFocus += focusOver;
      editBox.Enter += editBoxEnter;
      editBox.Leave += editBoxLeave;

      m_colorListBox.DrawMode = DrawMode.OwnerDrawFixed;
      m_colorListBox.DrawItem += ColorListBox_DrawItem;
      m_colorListBox.SelectedIndex = 0;

      addRegionsToListbox();

      m_RegionListing.DrawMode = DrawMode.OwnerDrawFixed;
      m_RegionListing.DrawItem += RegionListBox_DrawItem;
      m_RegionListing.SelectedIndex = 0;

      m_documentVerticalMargins.Enabled = m_documentHorizontalMargins.Enabled = m_documentSpecificMargins.Checked;
    }

    public string Author { get => m_authorTextBox.Text; set => m_authorTextBox.Text = value; }

    public float ConnectionArrowSize { get => (float) m_arrowSizeUpDown.Value; set => m_arrowSizeUpDown.Value = (decimal) value; }

    public float ConnectionStalkLength { get => (float) m_connectionStalkLengthUpDown.Value; set => m_connectionStalkLengthUpDown.Value = (decimal) value; }

    public float DarknessStripeSize { get => (float) m_darknessStripeSizeNumericUpDown.Value; set => m_darknessStripeSizeNumericUpDown.Value = (decimal) value; }

    public string DefaultRoomName { get => txtDefaultRoomName.Text; set => txtDefaultRoomName.Text = value; }

    public RoomShape DefaultRoomShape { get => (RoomShape) cboRoomShape.SelectedIndex; set => cboRoomShape.SelectedIndex = (int) value; }

    public string Description { get => m_descriptionTextBox.Text; set => m_descriptionTextBox.Text = value; }

    public float DocHorizontalMargin { get => (float) m_documentHorizontalMargins.Value; set => m_documentHorizontalMargins.Value = (decimal) value; }

    public bool DocumentSpecificMargins { get => m_documentSpecificMargins.Checked; set => m_documentSpecificMargins.Checked = value; }

    public float DocVerticalMargin { get => (float) m_documentVerticalMargins.Value; set => m_documentVerticalMargins.Value = (decimal) value; }

    public Color[] ElementColors { get; }

    public float GridSize { get => (float) m_gridSizeUpDown.Value; set => m_gridSizeUpDown.Value = (decimal) value; }

    public bool HandDrawnDoc { get => m_handDrawnCheckBox.Checked; set => m_handDrawnCheckBox.Checked = value; }

    public float HandleSize { get => (float) m_handleSizeUpDown.Value; set => m_handleSizeUpDown.Value = (decimal) value; }

    public string History { get => m_historyTextBox.Text; set => m_historyTextBox.Text = value; }

    public bool IsGridVisible { get => m_showGridCheckBox.Checked; set => m_showGridCheckBox.Checked = value; }

    public Font LargeFont {
      get => mLargeFont;
      set {
        mLargeFont = value;
        m_largeFontNameTextBox.Text = Drawing.FontName(mLargeFont);
        m_largeFontSizeTextBox.Text = ((int) Math.Round(mLargeFont.Size)).ToString();
      }
    }

    public Font LineFont {
      get => mLineFont;
      set {
        mLineFont = value;
        m_lineFontNameTextBox.Text = Drawing.FontName(mLineFont);
        m_lineFontSizeTextBox.Text = ((int) Math.Round(mLineFont.Size)).ToString();
      }
    }

    public float LineWidth { get => (float) m_lineWidthUpDown.Value; set => m_lineWidthUpDown.Value = (decimal) value; }

    public float ObjectListOffsetFromRoom { get => (float) m_objectListOffsetFromRoomNumericUpDown.Value; set => m_objectListOffsetFromRoomNumericUpDown.Value = (decimal) value; }

    public float PreferredDistanceBetweenRooms { get => (float) m_preferredDistanceBetweenRoomsUpDown.Value; set => m_preferredDistanceBetweenRoomsUpDown.Value = (decimal) value; }
    public List<Region> Regions { get; }

    public bool ShowOrigin { get => m_showOriginCheckBox.Checked; set => m_showOriginCheckBox.Checked = value; }

    public Font SmallFont {
      get => mSmallFont;
      set {
        mSmallFont = value;
        m_smallFontNameTextBox.Text = Drawing.FontName(mSmallFont);
        m_smallFontSizeTextBox.Text = ((int) Math.Round(mSmallFont.Size)).ToString();
      }
    }

    public float SnapToElementSize { get => (float) m_snapToElementDistanceUpDown.Value; set => m_snapToElementDistanceUpDown.Value = (decimal) value; }

    public bool SnapToGrid { get => m_snapToGridCheckBox.Checked; set => m_snapToGridCheckBox.Checked = value; }

    public Font SubtitleFont {
      get => mSubtitleFont;
      set {
        mSubtitleFont = value;
        m_subtitleFontNameTextBox.Text = Drawing.FontName(mSubtitleFont);
        m_subtitleFontSizeTextBox.Text = ((int) Math.Round(mSubtitleFont.Size)).ToString();
      }
    }

    public float TextOffsetFromConnection { get => (float) m_textOffsetFromLineUpDown.Value; set => m_textOffsetFromLineUpDown.Value = (decimal) value; }

    public string Title { get => m_titleTextBox.Text; set => m_titleTextBox.Text = value; }

    private void addRegionsToListbox() {
      m_RegionListing.Items.Clear();
      foreach (var region in Regions.OrderBy(p => p.RegionName != Domain.Misc.Region.DefaultRegion).ThenBy(p => p.RegionName)) m_RegionListing.Items.Add(region.RegionName);
    }

    private void btnAddRegion_Click(object sender, EventArgs e) {
      var region = new Region {RegionName = nextAvailableRegionName(), RColor = Color.White, TextColor = Settings.Color[Colors.Subtitle]};
      Regions.Add(region);
      addRegionsToListbox();
      m_colorListBox.Invalidate();

      var newOne = m_RegionListing.FindString(region.RegionName);

      m_RegionListing.SelectedIndex = newOne;
      m_RegionListing.Focus();
    }

    private void btnChange_Click(object sender, EventArgs e) {
      changeRegionColor();
      m_RegionListing.Focus();
    }

    private void btnDeleteRegion_Click(object sender, EventArgs e) {
      deleteRegion();
    }

    private void ChangeLargeFontButton_Click(object sender, EventArgs e) {
      LargeFont = showFontDialog(LargeFont);
    }

    private void ChangeLineFontButton_Click(object sender, EventArgs e) {
      LineFont = showFontDialog(LineFont);
    }

    private void changeRegionColor() {
      var selectedIndex = m_RegionListing.SelectedIndex;
      if (selectedIndex == -1) return;
      var region = Regions.FirstOrDefault(p => p.RegionName == m_RegionListing.Items[selectedIndex].ToString());
      if (region != null) {
        var originalRegionName = region.RegionName;
        var frm = new RegionSettings(region, Regions);
        if (frm.ShowDialog() == DialogResult.OK) {
          region.RColor = frm.RegionToChange.RColor;
          region.TextColor = frm.RegionToChange.TextColor;
          region.RegionName = frm.RegionToChange.RegionName;
          updateExistingRoomRegions(frm.RegionToChange.RegionName, originalRegionName);
          addRegionsToListbox();
          m_RegionListing.Invalidate();
          m_RegionListing.SelectedIndex = selectedIndex;
        }
      }
    }

    private void ChangeSmallFontButton_Click(object sender, EventArgs e) {
      SmallFont = showFontDialog(SmallFont);
    }

    private void ChangeSubtitleFontButton_Click(object sender, EventArgs e) {
      SubtitleFont = showFontDialog(SubtitleFont);
    }

    private void ColorListBox_DrawItem(object sender, DrawItemEventArgs e) {
      using (var palette = new Palette()) {
        e.DrawBackground();

        const int horizontalMargin = 2;
        const int verticalMargin = 2;
        const int width = 24;
        var colorBounds = new Rectangle(e.Bounds.Left + horizontalMargin, e.Bounds.Top + verticalMargin, width, e.Bounds.Height - verticalMargin * 2);
        var textBounds = new Rectangle(colorBounds.Right + horizontalMargin, e.Bounds.Top, e.Bounds.Width - colorBounds.Width - horizontalMargin * 2, e.Bounds.Height);
        e.Graphics.FillRectangle(palette.Brush(ElementColors[e.Index]), colorBounds);
        e.Graphics.DrawRectangle(palette.Pen(e.ForeColor, 0), colorBounds);
        var format = new StringFormat {Trimming = StringTrimming.EllipsisCharacter};
        e.Graphics.DrawString(m_colorListBox.Items[e.Index].ToString(), e.Font, palette.Brush(e.ForeColor), textBounds, format);
      }
    }

    private void createEditBox() {
      itemSelected = m_RegionListing.SelectedIndex;

      if (m_RegionListing.Items[itemSelected].ToString() == Domain.Misc.Region.DefaultRegion) return;

      var r = m_RegionListing.GetItemRectangle(itemSelected);
      var itemText = m_RegionListing.Items[itemSelected].ToString();

      var colorBounds = new Rectangle(r.Left + HORIZONTAL_MARGIN, r.Top + VERTICAL_MARGIN, WIDTH, r.Height - VERTICAL_MARGIN * 2);
      var textBounds = new Rectangle(colorBounds.Right + HORIZONTAL_MARGIN, r.Top, r.Width - colorBounds.Width - HORIZONTAL_MARGIN * 2, r.Height);

      editBox.Location = new Point(textBounds.X + 1, textBounds.Y + 1);
      editBox.AutoSize = false;
      editBox.Size = new Size(textBounds.Width, r.Height + 2);
      editBox.Show();
      editBox.Text = itemText;
      editBox.Focus();
      editBox.SelectAll();
    }

    private void deleteRegion() {
      itemSelected = m_RegionListing.SelectedIndex;
      if (m_RegionListing.Items[itemSelected].ToString() == Domain.Misc.Region.DefaultRegion) return;
      foreach (var tRoom in Project.Current.Elements.OfType<Room>().Where(tRoom => tRoom.Region == m_RegionListing.Items[itemSelected].ToString())) tRoom.Region = Domain.Misc.Region.DefaultRegion;
      Regions.RemoveAll(p => p.RegionName == m_RegionListing.Items[itemSelected].ToString());
      addRegionsToListbox();

      m_RegionListing.SelectedIndex = itemSelected == 0 ? 0 : itemSelected + 1 >= m_RegionListing.Items.Count ? m_RegionListing.Items.Count - 1 : itemSelected;
      m_RegionListing.Focus();
    }

    private void editBoxEnter(object sender, EventArgs e) {
      AcceptButton = null;
      CancelButton = null;
    }

    private void editBoxKeyPress(object sender, KeyPressEventArgs e) {
      if (e.KeyChar.ToString() == "_" || e.KeyChar.ToString() == ":") {
        e.Handled = true;
        return;
      }

      if (e.KeyChar == (char) Keys.Enter || e.KeyChar == (char) Keys.Return) updateHideRegionTextBox();

      if (e.KeyChar == (char) Keys.Escape) {
        bUpdatingRegionText = true;
        editBox.Hide();
        bUpdatingRegionText = false;
      }
    }

    private void editBoxLeave(object sender, EventArgs e) {
      AcceptButton = m_okButton;
      CancelButton = m_cancelButton;
    }

    private void focusOver(object sender, EventArgs e) {
      if (editBox.Visible) {
        updateHideRegionTextBox();
        m_RegionListing.Focus();
      }
    }

    private void m_documentSpecificMargins_CheckedChanged(object sender, EventArgs e) {
      m_documentHorizontalMargins.Enabled = m_documentVerticalMargins.Enabled = m_documentSpecificMargins.Checked;
    }

    private void m_okButton_Click(object sender, EventArgs e) {
      if (string.IsNullOrWhiteSpace(txtDefaultRoomName.Text)) {
        MessageBox.Show("The default room name can't be empty. Please put something in there.", "Empty default name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtDefaultRoomName.Focus();
        DialogResult = DialogResult.None;
      }

      else if (!txtDefaultRoomName.Text.Any(char.IsLetter)) {
        MessageBox.Show("The default room name must contain one letter. Please include a letter.", "Invalid default name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        txtDefaultRoomName.Focus();
        DialogResult = DialogResult.None;
      }
    }

    private void m_RegionListing_KeyDown(object sender, KeyEventArgs e) {
      if (e.KeyData == Keys.F2) createEditBox();
    }

    private void m_RegionListing_KeyPress(object sender, KeyPressEventArgs e) { }

    private void m_RegionListing_KeyUp(object sender, KeyEventArgs e) {
      if (e.KeyCode == Keys.Delete) deleteRegion();
    }

    private void m_RegionListing_SelectedIndexChanged(object sender, EventArgs e) {
      mCurrentRegion = m_RegionListing.SelectedItem == null ? null : Regions.Find(p => p.RegionName == m_RegionListing.SelectedItem.ToString());

      if (m_RegionListing.SelectedItem == null || m_RegionListing.SelectedItem.ToString() == Domain.Misc.Region.DefaultRegion)
        btnDeleteRegion.Enabled = false;
      else
        btnDeleteRegion.Enabled = true;
    }

    private string nextAvailableRegionName() {
      var num = 1;
      var newRegionName = "Region1";

      while (Regions.Exists(p => p.RegionName.Equals(newRegionName, StringComparison.OrdinalIgnoreCase))) {
        num++;
        newRegionName = $"Region{num}";
      }

      return newRegionName;
    }

    private void onChangeColor(object sender, EventArgs e) {
      if (m_colorListBox.SelectedItems.Count == 1) {
        var color = Colors.ShowColorDialog(ElementColors[m_colorListBox.SelectedIndex], this);
        if (color != Color.Empty)
          ElementColors[m_colorListBox.SelectedIndex] = color;
      } else {
        var color = Colors.ShowColorDialog(Color.Empty, this);
        if (color != Color.Empty)
          foreach (int selectedIndex in m_colorListBox.SelectedIndices)
            ElementColors[selectedIndex] = color;
      }

      m_colorListBox.Invalidate();
    }

    private void onChangeRegionColor(object sender, EventArgs e) {
      changeRegionColor();
    }

    private bool regionAlreadyExists(string pNew) {
      if (Regions.Any(p => p != mCurrentRegion && p.RegionName.Equals(pNew, StringComparison.OrdinalIgnoreCase))) {
        MessageBox.Show($"A Region already exists with the name '{pNew}'");
        return true;
      }

      return false;
    }

    private void RegionListBox_DrawItem(object sender, DrawItemEventArgs e) {
      if (e.Index < 0) return;
      var txtColorFont = new Font("Arial", 6);
      using (var palette = new Palette()) {
        e.DrawBackground();

        var colorBounds = new Rectangle(e.Bounds.Left + HORIZONTAL_MARGIN, e.Bounds.Top + VERTICAL_MARGIN, WIDTH, e.Bounds.Height - VERTICAL_MARGIN * 2);
        var textBounds = new Rectangle(colorBounds.Right + HORIZONTAL_MARGIN, e.Bounds.Top, e.Bounds.Width - colorBounds.Width - HORIZONTAL_MARGIN * 2, e.Bounds.Height);
        var foundRegion = Regions.FirstOrDefault(p => p.RegionName == m_RegionListing.Items[e.Index].ToString());
        if (foundRegion != null) {
          e.Graphics.FillRectangle(palette.Brush(foundRegion.RColor), colorBounds);
          e.Graphics.DrawRectangle(palette.Pen(e.ForeColor, 0), colorBounds);
          e.Graphics.DrawString(m_RegionListing.Items[e.Index].ToString(), e.Font, palette.Brush(e.ForeColor), textBounds, StringFormats.Left);
          e.Graphics.DrawString("123", txtColorFont, palette.Brush(foundRegion.TextColor), colorBounds, StringFormats.Center);
        }
      }
    }

    private void SettingsDialog_FormClosing(object sender, FormClosingEventArgs e) {
      Properties.Settings.Default.SettingsLastTabIndex = tabControl1.SelectedIndex;
      Properties.Settings.Default.Save();
    }

    private void SettingsDialog_Load(object sender, EventArgs e) {
      try {
        var tab = Properties.Settings.Default.SettingsLastTabIndex;

        tabControl1.SelectedIndex = Convert.ToInt32(tab);
      }
      catch {
        // ignored
      }
    }

    private Font showFontDialog(Font font) {
      using (var dialog = new FontDialog()) {
        if (font != null)
          dialog.Font = new Font(font.Name, font.Size, font.Style);
        if (dialog.ShowDialog(this) == DialogResult.OK) return new Font(dialog.Font.Name, dialog.Font.Size, dialog.Font.Style, GraphicsUnit.World);
      }

      return font;
    }

    private void tabControl1_Selected(object sender, TabControlEventArgs e) {
      switch (e.TabPage.Name) {
        case "tabRegions":
          m_RegionListing.Focus();
          break;
      }
    }

    private void updateExistingRoomRegions(string pNew, string pOld) {
      var original = pOld;
      var newname = pNew;

      foreach (var tRoom in Project.Current.Elements.OfType<Room>().Where(tRoom => tRoom.Region == original)) tRoom.Region = newname;
    }

    private void updateHideRegionTextBox() {
      if (!bUpdatingRegionText) {
        bUpdatingRegionText = true;
        editBox.Text = editBox.Text.Trim().Replace("\"", "'");
        if (Domain.Misc.Region.ValidRegionName(editBox.Text))
          if (updateRegionName(editBox.Text, m_RegionListing.Items[itemSelected].ToString())) {
            editBox.Hide();
            addRegionsToListbox();
            m_RegionListing.SelectedIndex = itemSelected == 0 ? 0 : itemSelected + 1 >= m_RegionListing.Items.Count ? m_RegionListing.Items.Count - 1 : itemSelected;
            m_RegionListing.Focus();
          } else {
            editBox.Focus();
            editBox.SelectAll();
          }
        else
          editBox.Hide();

        bUpdatingRegionText = false;
      }
    }

    private bool updateRegionName(string pNew, string pOld) {
      pNew = pNew.Trim();

      if (regionAlreadyExists(pNew)) return false;

      updateExistingRoomRegions(pNew, pOld);

      Regions.First(p => p.RegionName == m_RegionListing.Items[itemSelected].ToString()).RegionName = pNew;
      m_RegionListing.Items[itemSelected] = pNew;
      return true;
    }
  }
}