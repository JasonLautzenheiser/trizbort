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
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Trizbort
{
    public partial class SettingsDialog : Form
    {
        private const int HORIZONTAL_MARGIN = 2;
        private const int VERTICAL_MARGIN = 2;
        private const int WIDTH = 24;
        private readonly TextBox editBox;
        private int itemSelected;
        private ListBox listBox;
        private Font m_largeFont;
        private Font m_lineFont;
        private Font m_smallFont;
        private bool bUpdatingRegionText = false;

        public SettingsDialog()
        {
            Color = new Color[Colors.Count];
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
        }

        public string Title
        {
            get { return m_titleTextBox.Text; }
            set { m_titleTextBox.Text = value; }
        }

        public string Author
        {
            get { return m_authorTextBox.Text; }
            set { m_authorTextBox.Text = value; }
        }

        public string Description
        {
            get { return m_descriptionTextBox.Text; }
            set { m_descriptionTextBox.Text = value; }
        }

        public string History
        {
            get { return m_historyTextBox.Text; }
            set { m_historyTextBox.Text = value; }
        }

        public Color[] Color { get; private set; }
        public List<Settings.Region> Regions { get; private set; }

        public Font LargeFont
        {
            get { return m_largeFont; }
            set
            {
                m_largeFont = value;
                m_largeFontNameTextBox.Text = Drawing.FontName(m_largeFont);
                m_largeFontSizeTextBox.Text = ((int) Math.Round(m_largeFont.Size)).ToString();
            }
        }

        public Font SmallFont
        {
            get { return m_smallFont; }
            set
            {
                m_smallFont = value;
                m_smallFontNameTextBox.Text = Drawing.FontName(m_smallFont);
                m_smallFontSizeTextBox.Text = ((int) Math.Round(m_smallFont.Size)).ToString();
            }
        }

        public Font LineFont
        {
            get { return m_lineFont; }
            set
            {
                m_lineFont = value;
                m_lineFontNameTextBox.Text = Drawing.FontName(m_lineFont);
                m_lineFontSizeTextBox.Text = ((int) Math.Round(m_lineFont.Size)).ToString();
            }
        }

        public bool HandDrawn
        {
            get { return m_handDrawnCheckBox.Checked; }
            set { m_handDrawnCheckBox.Checked = value; }
        }

        public float LineWidth
        {
            get { return (float) m_lineWidthUpDown.Value; }
            set { m_lineWidthUpDown.Value = (decimal) value; }
        }

        public bool SnapToGrid
        {
            get { return m_snapToGridCheckBox.Checked; }
            set { m_snapToGridCheckBox.Checked = value; }
        }

        public float GridSize
        {
            get { return (float) m_gridSizeUpDown.Value; }
            set { m_gridSizeUpDown.Value = (decimal) value; }
        }

        public bool IsGridVisible
        {
            get { return m_showGridCheckBox.Checked; }
            set { m_showGridCheckBox.Checked = value; }
        }

        public bool ShowOrigin
        {
            get { return m_showOriginCheckBox.Checked; }
            set { m_showOriginCheckBox.Checked = value; }
        }

        public float DarknessStripeSize
        {
            get { return (float) m_darknessStripeSizeNumericUpDown.Value; }
            set { m_darknessStripeSizeNumericUpDown.Value = (decimal) value; }
        }

        public float ObjectListOffsetFromRoom
        {
            get { return (float) m_objectListOffsetFromRoomNumericUpDown.Value; }
            set { m_objectListOffsetFromRoomNumericUpDown.Value = (decimal) value; }
        }

        public float ConnectionStalkLength
        {
            get { return (float) m_connectionStalkLengthUpDown.Value; }
            set { m_connectionStalkLengthUpDown.Value = (decimal) value; }
        }

        public float PreferredDistanceBetweenRooms
        {
            get { return (float) m_preferredDistanceBetweenRoomsUpDown.Value; }
            set { m_preferredDistanceBetweenRoomsUpDown.Value = (decimal) value; }
        }

        public float TextOffsetFromConnection
        {
            get { return (float) m_textOffsetFromLineUpDown.Value; }
            set { m_textOffsetFromLineUpDown.Value = (decimal) value; }
        }

        public float HandleSize
        {
            get { return (float) m_handleSizeUpDown.Value; }
            set { m_handleSizeUpDown.Value = (decimal) value; }
        }

        public float SnapToElementSize
        {
            get { return (float) m_snapToElementDistanceUpDown.Value; }
            set { m_snapToElementDistanceUpDown.Value = (decimal) value; }
        }

        public float ConnectionArrowSize
        {
            get { return (float) m_arrowSizeUpDown.Value; }
            set { m_arrowSizeUpDown.Value = (decimal) value; }
        }

        private void editBoxLeave(object sender, EventArgs e)
        {
            AcceptButton = m_okButton;
            CancelButton = m_cancelButton;
        }

        private void editBoxEnter(object sender, EventArgs e)
        {
            AcceptButton = null;
            CancelButton = null;
        }

        private void addRegionsToListbox()
        {
            m_RegionListing.Items.Clear();
            foreach (var region in Regions)
            {
                m_RegionListing.Items.Add(region.RegionName);
            }
        }

        private void RegionListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            using (var palette = new Palette())
            {
                e.DrawBackground();

                var colorBounds = new Rectangle(e.Bounds.Left + HORIZONTAL_MARGIN, e.Bounds.Top + VERTICAL_MARGIN, WIDTH, e.Bounds.Height - VERTICAL_MARGIN*2);
                var textBounds = new Rectangle(colorBounds.Right + HORIZONTAL_MARGIN, e.Bounds.Top, e.Bounds.Width - colorBounds.Width - HORIZONTAL_MARGIN*2, e.Bounds.Height);
                e.Graphics.FillRectangle(palette.Brush(Regions.FirstOrDefault(p => p.RegionName == m_RegionListing.Items[e.Index].ToString()).RColor), colorBounds);
                e.Graphics.DrawRectangle(palette.Pen(e.ForeColor, 0), colorBounds);
                e.Graphics.DrawString(m_RegionListing.Items[e.Index].ToString(), e.Font, palette.Brush(e.ForeColor), textBounds, StringFormats.Left);
            }
        }

        private void ColorListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            using (var palette = new Palette())
            {
                e.DrawBackground();

                const int horizontalMargin = 2;
                const int verticalMargin = 2;
                const int width = 24;
                var colorBounds = new Rectangle(e.Bounds.Left + horizontalMargin, e.Bounds.Top + verticalMargin, width,e.Bounds.Height - verticalMargin*2);
                var textBounds = new Rectangle(colorBounds.Right + horizontalMargin, e.Bounds.Top,e.Bounds.Width - colorBounds.Width - horizontalMargin*2, e.Bounds.Height);
                e.Graphics.FillRectangle(palette.Brush(Color[e.Index]), colorBounds);
                e.Graphics.DrawRectangle(palette.Pen(e.ForeColor, 0), colorBounds);
                e.Graphics.DrawString(m_colorListBox.Items[e.Index].ToString(), e.Font, palette.Brush(e.ForeColor),textBounds, StringFormats.Left);
            }
        }

        private void onChangeColor(object sender, EventArgs e)
        {
            Color[m_colorListBox.SelectedIndex] = showColorDialog(Color[m_colorListBox.SelectedIndex]);
            m_colorListBox.Invalidate();
        }

        private void onChangeRegionColor(object sender, EventArgs e)
        {
            changeRegionColor();
        }

        private void changeRegionColor()
        {
            var region =
                Regions.FirstOrDefault(
                    p => p.RegionName == m_RegionListing.Items[m_RegionListing.SelectedIndex].ToString());
            if (region != null) region.RColor = showColorDialog(region.RColor);
            m_RegionListing.Invalidate();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            changeRegionColor();
        }

        private void btnAddRegion_Click(object sender, EventArgs e)
        {
            var region = new Settings.Region {RegionName = nextAvailableRegionName(), RColor = System.Drawing.Color.White};
            Regions.Add(region);
            addRegionsToListbox();
            m_colorListBox.Invalidate();
            m_RegionListing.SelectedIndex = m_RegionListing.Items.Count - 1;
//            createEditBox(m_colorListBox);
        }

        private string nextAvailableRegionName()
        {
            Regex rgx = new Regex("^Region[0-9]+", RegexOptions.IgnoreCase);
            var defaults = Regions.Where(p => rgx.IsMatch(p.RegionName)).OrderByDescending(p=>p.RegionName).ToList();

            int num=0;
            if (defaults.Any())
            {
                Regex rgx2 = new Regex("^Region([0-9]+)$", RegexOptions.IgnoreCase);
                num = Convert.ToInt32(rgx2.Match(defaults[0].RegionName).Groups[1].Value);
            }
            string sReturn = string.Format("Region{0}", num + 1);

            return sReturn;
        }

        private void ChangeLargeFontButton_Click(object sender, EventArgs e)
        {
            LargeFont = showFontDialog(LargeFont);
        }

        private void ChangeSmallFontButton_Click(object sender, EventArgs e)
        {
            SmallFont = showFontDialog(SmallFont);
        }

        private void ChangeLineFontButton_Click(object sender, EventArgs e)
        {
            LineFont = showFontDialog(LineFont);
        }

        private Color showColorDialog(Color color)
        {
            using (var dialog = new ColorDialog())
            {
                dialog.Color = color;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    return dialog.Color;
                }
            }
            return color;
        }

        private Font showFontDialog(Font font)
        {
            using (var dialog = new FontDialog())
            {
                dialog.Font = new Font(font.Name, font.Size, font.Style);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    return new Font(dialog.Font.Name, dialog.Font.Size, dialog.Font.Style, GraphicsUnit.World);
                }
            }
            return font;
        }

        private void m_RegionListing_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                createEditBox(sender);
        }

        private void m_RegionListing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
                createEditBox(sender);
        }

        private void createEditBox(object sender)
        {
            listBox = (ListBox) sender;
            itemSelected = m_RegionListing.SelectedIndex;

            if (m_RegionListing.Items[itemSelected].ToString() == Settings.Region.DefaultRegion) return;

            var r = m_RegionListing.GetItemRectangle(itemSelected);
            var itemText = m_RegionListing.Items[itemSelected].ToString();

            var colorBounds = new Rectangle(r.Left + HORIZONTAL_MARGIN, r.Top + VERTICAL_MARGIN, WIDTH,r.Height - VERTICAL_MARGIN*2);
            var textBounds = new Rectangle(colorBounds.Right + HORIZONTAL_MARGIN, r.Top,r.Width - colorBounds.Width - HORIZONTAL_MARGIN*2, r.Height);

            editBox.Location = new Point(textBounds.X + 1, textBounds.Y + 1);
            editBox.AutoSize = false;
            editBox.Size = new Size(textBounds.Width, r.Height + 2);
            editBox.Show();
            editBox.Text = itemText;
            editBox.Focus();
            editBox.SelectAll();
        }

        private void focusOver(object sender, EventArgs e)
        {
            if (editBox.Visible)
            {
                updateHideRegionTextBox();
            }
        }

        private void updateHideRegionTextBox()
        {
            if (!bUpdatingRegionText)
            {
                bUpdatingRegionText = true;
                if (updateRegionName())
                    editBox.Hide();
                else
                {
                    editBox.Focus();
                    editBox.SelectAll();
                }
                bUpdatingRegionText = false;
            }
        }

        private bool updateRegionName()
        {
            if (Regions.Any(p => p.RegionName == editBox.Text))
            {
                MessageBox.Show(string.Format("A Region already exists with the name '{0}'", editBox.Text));
                return false;
            }
            
            var original = m_RegionListing.Items[itemSelected].ToString();
            var newname = editBox.Text;

            foreach (var tRoom in Project.Current.Elements.OfType<Room>().Where(tRoom => tRoom.Region == original)) {
                tRoom.Region = newname;
            }

            Regions.First(p => p.RegionName == m_RegionListing.Items[itemSelected].ToString()).RegionName = editBox.Text;
            m_RegionListing.Items[itemSelected] = editBox.Text;
            return true;
        }

        private void editBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter || e.KeyChar == (char) Keys.Return)
            {
                updateHideRegionTextBox();
            }

            if (e.KeyChar == (char) Keys.Escape)
            {
                bUpdatingRegionText = true;
                editBox.Hide();
                bUpdatingRegionText = false;
            }
        }

        private void btnDeleteRegion_Click(object sender, EventArgs e)
        {
            itemSelected = m_RegionListing.SelectedIndex;
            if (m_RegionListing.Items[itemSelected].ToString() == Settings.Region.DefaultRegion) return;
            Regions.RemoveAll(p => p.RegionName == m_RegionListing.Items[itemSelected].ToString());
            addRegionsToListbox();

            m_RegionListing.SelectedIndex = itemSelected == 0 ? 0 : itemSelected - 1;
        }
    }
}