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
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();

            m_colorListBox.DrawMode = DrawMode.OwnerDrawFixed;
            m_colorListBox.DrawItem += ColorListBox_DrawItem;
            m_colorListBox.SelectedIndex = 0;
        }

        private void ColorListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            using (var palette = new Palette())
            {
                e.DrawBackground();

                var horizontalMargin = 2;
                var verticalMargin = 2;
                var width = 24;
                var colorBounds = new Rectangle(e.Bounds.Left + horizontalMargin, e.Bounds.Top + verticalMargin, width, e.Bounds.Height - verticalMargin * 2);
                var textBounds = new Rectangle(colorBounds.Right + horizontalMargin, e.Bounds.Top, e.Bounds.Width - colorBounds.Width - horizontalMargin * 2, e.Bounds.Height);
                e.Graphics.FillRectangle(palette.Brush(m_color[e.Index]), colorBounds);
                e.Graphics.DrawRectangle(palette.Pen(e.ForeColor, 0), colorBounds);
                e.Graphics.DrawString(m_colorListBox.Items[e.Index].ToString(), e.Font, palette.Brush(e.ForeColor), textBounds, StringFormats.Left);
            }
        }

        private void OnChangeColor(object sender, EventArgs e)
        {
            Color[m_colorListBox.SelectedIndex] = ShowColorDialog(Color[m_colorListBox.SelectedIndex]);
            m_colorListBox.Invalidate();
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

        public Color[] Color
        {
            get { return m_color; }
        }

        public Font LargeFont
        {
            get { return m_largeFont; }
            set
            { 
                m_largeFont = value;
                m_largeFontNameTextBox.Text = Drawing.FontName(m_largeFont);
                m_largeFontSizeTextBox.Text = ((int)Math.Round(m_largeFont.Size)).ToString();
            }
        }

        public Font SmallFont
        {
            get { return m_smallFont; }
            set
            {
                m_smallFont = value;
                m_smallFontNameTextBox.Text = Drawing.FontName(m_smallFont);
                m_smallFontSizeTextBox.Text = ((int)Math.Round(m_smallFont.Size)).ToString();
            }
        }

        public Font LineFont
        {
            get { return m_lineFont; }
            set
            {
                m_lineFont = value;
                m_lineFontNameTextBox.Text = Drawing.FontName(m_lineFont);
                m_lineFontSizeTextBox.Text = ((int)Math.Round(m_lineFont.Size)).ToString();
            }
        }

        public bool HandDrawn
        {
            get { return m_handDrawnCheckBox.Checked; }
            set { m_handDrawnCheckBox.Checked = value; }
        }

        public float LineWidth
        {
            get { return (float)m_lineWidthUpDown.Value; }
            set { m_lineWidthUpDown.Value = (decimal)value; }
        }

        public bool SnapToGrid
        {
            get { return m_snapToGridCheckBox.Checked; }
            set { m_snapToGridCheckBox.Checked = value; }
        }

        public float GridSize
        {
            get { return (float)m_gridSizeUpDown.Value; }
            set { m_gridSizeUpDown.Value = (decimal)value; }
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
            get { return (float)m_darknessStripeSizeNumericUpDown.Value; }
            set { m_darknessStripeSizeNumericUpDown.Value = (decimal)value; }
        }

        public float ObjectListOffsetFromRoom
        {
            get { return (float)m_objectListOffsetFromRoomNumericUpDown.Value; }
            set { m_objectListOffsetFromRoomNumericUpDown.Value = (decimal)value; }
        }

        public float ConnectionStalkLength
        {
            get { return (float)m_connectionStalkLengthUpDown.Value; }
            set { m_connectionStalkLengthUpDown.Value = (decimal)value; }
        }

        public float PreferredDistanceBetweenRooms
        {
            get { return (float)m_preferredDistanceBetweenRoomsUpDown.Value; }
            set { m_preferredDistanceBetweenRoomsUpDown.Value = (decimal)value; }
        }

        public float TextOffsetFromConnection
        {
            get { return (float)m_textOffsetFromLineUpDown.Value; }
            set { m_textOffsetFromLineUpDown.Value = (decimal)value; }
        }

        public float HandleSize
        {
            get { return (float)m_handleSizeUpDown.Value; }
            set { m_handleSizeUpDown.Value = (decimal)value; }
        }

        public float SnapToElementSize
        {
            get { return (float)m_snapToElementDistanceUpDown.Value; }
            set { m_snapToElementDistanceUpDown.Value = (decimal)value; }
        }

        public float ConnectionArrowSize
        {
            get { return (float)m_arrowSizeUpDown.Value; }
            set { m_arrowSizeUpDown.Value = (decimal)value; }
        }

        private void ChangeLargeFontButton_Click(object sender, EventArgs e)
        {
            LargeFont = ShowFontDialog(LargeFont);
        }

        private void ChangeSmallFontButton_Click(object sender, EventArgs e)
        {
            SmallFont = ShowFontDialog(SmallFont);
        }

        private void ChangeLineFontButton_Click(object sender, EventArgs e)
        {
            LineFont = ShowFontDialog(LineFont);
        }

        private Color ShowColorDialog(Color color)
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

        private Font ShowFontDialog(Font font)
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

        private Color[] m_color = new Color[Colors.Count];
        private Font m_largeFont;
        private Font m_smallFont;
        private Font m_lineFont;
    }
}
