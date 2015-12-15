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

namespace Trizbort
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
      this.m_okButton = new System.Windows.Forms.Button();
      this.m_cancelButton = new System.Windows.Forms.Button();
      this.m_handDrawnCheckBox = new System.Windows.Forms.CheckBox();
      this.m_lineWidthUpDown = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.m_linesGroupBox = new System.Windows.Forms.GroupBox();
      this.label18 = new System.Windows.Forms.Label();
      this.m_preferredDistanceBetweenRoomsUpDown = new System.Windows.Forms.NumericUpDown();
      this.label8 = new System.Windows.Forms.Label();
      this.m_textOffsetFromLineUpDown = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.m_connectionStalkLengthUpDown = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.m_arrowSizeUpDown = new System.Windows.Forms.NumericUpDown();
      this.m_gridGroupBox = new System.Windows.Forms.GroupBox();
      this.m_showOriginCheckBox = new System.Windows.Forms.CheckBox();
      this.m_showGridCheckBox = new System.Windows.Forms.CheckBox();
      this.m_snapToGridCheckBox = new System.Windows.Forms.CheckBox();
      this.label6 = new System.Windows.Forms.Label();
      this.m_gridSizeUpDown = new System.Windows.Forms.NumericUpDown();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtDefaultRoomName = new System.Windows.Forms.TextBox();
      this.label20 = new System.Windows.Forms.Label();
      this.label34 = new System.Windows.Forms.Label();
      this.m_objectListOffsetFromRoomNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.label13 = new System.Windows.Forms.Label();
      this.m_darknessStripeSizeNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.panel1 = new System.Windows.Forms.Panel();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.label17 = new System.Windows.Forms.Label();
      this.label16 = new System.Windows.Forms.Label();
      this.m_historyTextBox = new System.Windows.Forms.TextBox();
      this.label15 = new System.Windows.Forms.Label();
      this.label14 = new System.Windows.Forms.Label();
      this.m_descriptionTextBox = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.m_authorTextBox = new System.Windows.Forms.TextBox();
      this.m_titleTextBox = new System.Windows.Forms.TextBox();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.m_colorsGroupBox = new System.Windows.Forms.GroupBox();
      this.m_colorListBox = new System.Windows.Forms.ListBox();
      this.m_changeColorButton = new System.Windows.Forms.Button();
      this.m_fontsGroupBox = new System.Windows.Forms.GroupBox();
      this.label9 = new System.Windows.Forms.Label();
      this.m_lineFontSizeTextBox = new System.Windows.Forms.TextBox();
      this.m_changeLineFontButton = new System.Windows.Forms.Button();
      this.m_lineFontNameTextBox = new System.Windows.Forms.TextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.m_smallFontSizeTextBox = new System.Windows.Forms.TextBox();
      this.m_changeSmallFontButton = new System.Windows.Forms.Button();
      this.m_smallFontNameTextBox = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.m_largeFontSizeTextBox = new System.Windows.Forms.TextBox();
      this.m_changeLargeFontButton = new System.Windows.Forms.Button();
      this.m_largeFontNameTextBox = new System.Windows.Forms.TextBox();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.tabRegions = new System.Windows.Forms.TabPage();
      this.label19 = new System.Windows.Forms.Label();
      this.btnDeleteRegion = new System.Windows.Forms.Button();
      this.btnAddRegion = new System.Windows.Forms.Button();
      this.btnChange = new System.Windows.Forms.Button();
      this.m_RegionListing = new System.Windows.Forms.ListBox();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label7 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.m_snapToElementDistanceUpDown = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.m_handleSizeUpDown = new System.Windows.Forms.NumericUpDown();
      this.groupBox4 = new System.Windows.Forms.GroupBox();
      this.label4b = new System.Windows.Forms.Label();
      this.label4c = new System.Windows.Forms.Label();
      this.m_documentSpecificMargins = new System.Windows.Forms.CheckBox();
      this.m_documentHorizontalMargins = new System.Windows.Forms.NumericUpDown();
      this.m_documentVerticalMargins = new System.Windows.Forms.NumericUpDown();
      this.propertySettings1 = new DevComponents.DotNetBar.PropertySettings();
      this.superTooltip1 = new DevComponents.DotNetBar.SuperTooltip();
      ((System.ComponentModel.ISupportInitialize)(this.m_lineWidthUpDown)).BeginInit();
      this.m_linesGroupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_preferredDistanceBetweenRoomsUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_textOffsetFromLineUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_connectionStalkLengthUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_arrowSizeUpDown)).BeginInit();
      this.m_gridGroupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_gridSizeUpDown)).BeginInit();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_objectListOffsetFromRoomNumericUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_darknessStripeSizeNumericUpDown)).BeginInit();
      this.panel1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.m_colorsGroupBox.SuspendLayout();
      this.m_fontsGroupBox.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabRegions.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_snapToElementDistanceUpDown)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_handleSizeUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // m_okButton
      // 
      this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_okButton.Location = new System.Drawing.Point(207, 12);
      this.m_okButton.Name = "m_okButton";
      this.m_okButton.Size = new System.Drawing.Size(75, 23);
      this.m_okButton.TabIndex = 0;
      this.m_okButton.Text = "OK";
      this.m_okButton.UseVisualStyleBackColor = true;
      this.m_okButton.Click += new System.EventHandler(this.m_okButton_Click);
      // 
      // m_cancelButton
      // 
      this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_cancelButton.CausesValidation = false;
      this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_cancelButton.Location = new System.Drawing.Point(288, 12);
      this.m_cancelButton.Name = "m_cancelButton";
      this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
      this.m_cancelButton.TabIndex = 1;
      this.m_cancelButton.Text = "Cancel";
      this.m_cancelButton.UseVisualStyleBackColor = true;
      // 
      // m_handDrawnCheckBox
      // 
      this.m_handDrawnCheckBox.AutoSize = true;
      this.m_handDrawnCheckBox.Cursor = System.Windows.Forms.Cursors.Help;
      this.m_handDrawnCheckBox.Location = new System.Drawing.Point(48, 20);
      this.m_handDrawnCheckBox.Name = "m_handDrawnCheckBox";
      this.m_handDrawnCheckBox.Size = new System.Drawing.Size(93, 17);
      this.superTooltip1.SetSuperTooltip(this.m_handDrawnCheckBox, new DevComponents.DotNetBar.SuperTooltipInfo("Hand-drawn", "", "By default Trizbort gives lines a \"hand drawn\" appearance. If you prefer straight" +
            " lines, you can uncheck this box. This applies to connections\' lines, the border" +
            " of a room, and dividing fill lines.", null, null, DevComponents.DotNetBar.eTooltipColor.Orange));
      this.m_handDrawnCheckBox.TabIndex = 0;
      this.m_handDrawnCheckBox.Text = "\"&Hand-drawn\"";
      this.m_handDrawnCheckBox.UseVisualStyleBackColor = true;
      // 
      // m_lineWidthUpDown
      // 
      this.m_lineWidthUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_lineWidthUpDown.DecimalPlaces = 1;
      this.m_lineWidthUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
      this.m_lineWidthUpDown.Location = new System.Drawing.Point(242, 19);
      this.m_lineWidthUpDown.Name = "m_lineWidthUpDown";
      this.m_lineWidthUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_lineWidthUpDown.TabIndex = 2;
      this.m_lineWidthUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_lineWidthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.AutoSize = true;
      this.label5.Cursor = System.Windows.Forms.Cursors.Help;
      this.label5.Location = new System.Drawing.Point(201, 21);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(35, 13);
      this.superTooltip1.SetSuperTooltip(this.label5, new DevComponents.DotNetBar.SuperTooltipInfo("Line Width", "", "The width of lines, in pixels. This applies to both connections\' lines and the bo" +
            "rder of a room.", null, null, DevComponents.DotNetBar.eTooltipColor.Orange));
      this.label5.TabIndex = 1;
      this.label5.Text = "&Width";
      // 
      // m_linesGroupBox
      // 
      this.m_linesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_linesGroupBox.Controls.Add(this.label18);
      this.m_linesGroupBox.Controls.Add(this.m_preferredDistanceBetweenRoomsUpDown);
      this.m_linesGroupBox.Controls.Add(this.label8);
      this.m_linesGroupBox.Controls.Add(this.m_textOffsetFromLineUpDown);
      this.m_linesGroupBox.Controls.Add(this.label1);
      this.m_linesGroupBox.Controls.Add(this.m_connectionStalkLengthUpDown);
      this.m_linesGroupBox.Controls.Add(this.label4);
      this.m_linesGroupBox.Controls.Add(this.m_arrowSizeUpDown);
      this.m_linesGroupBox.Controls.Add(this.m_handDrawnCheckBox);
      this.m_linesGroupBox.Controls.Add(this.label5);
      this.m_linesGroupBox.Controls.Add(this.m_lineWidthUpDown);
      this.m_linesGroupBox.Location = new System.Drawing.Point(5, 6);
      this.m_linesGroupBox.Name = "m_linesGroupBox";
      this.m_linesGroupBox.Size = new System.Drawing.Size(343, 163);
      this.m_linesGroupBox.TabIndex = 0;
      this.m_linesGroupBox.TabStop = false;
      this.m_linesGroupBox.Text = "&Lines";
      // 
      // label18
      // 
      this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label18.AutoSize = true;
      this.label18.Cursor = System.Windows.Forms.Cursors.Help;
      this.label18.Location = new System.Drawing.Point(62, 100);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(177, 13);
      this.superTooltip1.SetSuperTooltip(this.label18, new DevComponents.DotNetBar.SuperTooltipInfo("Preferred Distance Between Rooms", "", resources.GetString("label18.SuperTooltip"), null, null, DevComponents.DotNetBar.eTooltipColor.Orange));
      this.label18.TabIndex = 7;
      this.label18.Text = "&Preferred Distance Between Rooms";
      // 
      // m_preferredDistanceBetweenRoomsUpDown
      // 
      this.m_preferredDistanceBetweenRoomsUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_preferredDistanceBetweenRoomsUpDown.DecimalPlaces = 1;
      this.m_preferredDistanceBetweenRoomsUpDown.Location = new System.Drawing.Point(242, 98);
      this.m_preferredDistanceBetweenRoomsUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_preferredDistanceBetweenRoomsUpDown.Name = "m_preferredDistanceBetweenRoomsUpDown";
      this.m_preferredDistanceBetweenRoomsUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_preferredDistanceBetweenRoomsUpDown.TabIndex = 8;
      this.m_preferredDistanceBetweenRoomsUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_preferredDistanceBetweenRoomsUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label8
      // 
      this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label8.AutoSize = true;
      this.label8.Cursor = System.Windows.Forms.Cursors.Help;
      this.label8.Location = new System.Drawing.Point(127, 127);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(112, 13);
      this.superTooltip1.SetSuperTooltip(this.label8, new DevComponents.DotNetBar.SuperTooltipInfo("Text Offset From Line", "", "Trizbort automatically adds a gap between a connection\'s line and any labels on t" +
            "hat line. This setting controls how large that gap is.", null, null, DevComponents.DotNetBar.eTooltipColor.Orange));
      this.label8.TabIndex = 9;
      this.label8.Text = "&Text Offset From Line";
      // 
      // m_textOffsetFromLineUpDown
      // 
      this.m_textOffsetFromLineUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_textOffsetFromLineUpDown.DecimalPlaces = 1;
      this.m_textOffsetFromLineUpDown.Location = new System.Drawing.Point(242, 125);
      this.m_textOffsetFromLineUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_textOffsetFromLineUpDown.Name = "m_textOffsetFromLineUpDown";
      this.m_textOffsetFromLineUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_textOffsetFromLineUpDown.TabIndex = 10;
      this.m_textOffsetFromLineUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_textOffsetFromLineUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Cursor = System.Windows.Forms.Cursors.Help;
      this.label1.Location = new System.Drawing.Point(111, 73);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(128, 13);
      this.superTooltip1.SetSuperTooltip(this.label1, new DevComponents.DotNetBar.SuperTooltipInfo("Room Arrow Stalk Length", "", resources.GetString("label1.SuperTooltip"), null, null, DevComponents.DotNetBar.eTooltipColor.Orange));
      this.label1.TabIndex = 5;
      this.label1.Text = "&Room Arrow Stalk Length";
      // 
      // m_connectionStalkLengthUpDown
      // 
      this.m_connectionStalkLengthUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_connectionStalkLengthUpDown.DecimalPlaces = 1;
      this.m_connectionStalkLengthUpDown.Location = new System.Drawing.Point(242, 71);
      this.m_connectionStalkLengthUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_connectionStalkLengthUpDown.Name = "m_connectionStalkLengthUpDown";
      this.m_connectionStalkLengthUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_connectionStalkLengthUpDown.TabIndex = 6;
      this.m_connectionStalkLengthUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_connectionStalkLengthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Cursor = System.Windows.Forms.Cursors.Help;
      this.label4.Location = new System.Drawing.Point(179, 47);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(58, 13);
      this.superTooltip1.SetSuperTooltip(this.label4, new DevComponents.DotNetBar.SuperTooltipInfo("Arrow Size", "", "The pixel size of the base of arrows drawn on one way connections.", null, null, DevComponents.DotNetBar.eTooltipColor.Orange));
      this.label4.TabIndex = 3;
      this.label4.Text = "&Arrow Size";
      // 
      // m_arrowSizeUpDown
      // 
      this.m_arrowSizeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_arrowSizeUpDown.DecimalPlaces = 1;
      this.m_arrowSizeUpDown.Location = new System.Drawing.Point(242, 45);
      this.m_arrowSizeUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_arrowSizeUpDown.Name = "m_arrowSizeUpDown";
      this.m_arrowSizeUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_arrowSizeUpDown.TabIndex = 4;
      this.m_arrowSizeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_arrowSizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // m_gridGroupBox
      // 
      this.m_gridGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_gridGroupBox.Controls.Add(this.m_showOriginCheckBox);
      this.m_gridGroupBox.Controls.Add(this.m_showGridCheckBox);
      this.m_gridGroupBox.Controls.Add(this.m_snapToGridCheckBox);
      this.m_gridGroupBox.Controls.Add(this.label6);
      this.m_gridGroupBox.Controls.Add(this.m_gridSizeUpDown);
      this.m_gridGroupBox.Location = new System.Drawing.Point(5, 175);
      this.m_gridGroupBox.Name = "m_gridGroupBox";
      this.m_gridGroupBox.Size = new System.Drawing.Size(343, 72);
      this.m_gridGroupBox.TabIndex = 1;
      this.m_gridGroupBox.TabStop = false;
      this.m_gridGroupBox.Text = "&Grid";
      // 
      // m_showOriginCheckBox
      // 
      this.m_showOriginCheckBox.AutoSize = true;
      this.m_showOriginCheckBox.Location = new System.Drawing.Point(114, 20);
      this.m_showOriginCheckBox.Name = "m_showOriginCheckBox";
      this.m_showOriginCheckBox.Size = new System.Drawing.Size(83, 17);
      this.m_showOriginCheckBox.TabIndex = 2;
      this.m_showOriginCheckBox.Text = "Show &Origin";
      this.m_showOriginCheckBox.UseVisualStyleBackColor = true;
      // 
      // m_showGridCheckBox
      // 
      this.m_showGridCheckBox.AutoSize = true;
      this.m_showGridCheckBox.Location = new System.Drawing.Point(45, 20);
      this.m_showGridCheckBox.Name = "m_showGridCheckBox";
      this.m_showGridCheckBox.Size = new System.Drawing.Size(55, 17);
      this.m_showGridCheckBox.TabIndex = 0;
      this.m_showGridCheckBox.Text = "&Visible";
      this.m_showGridCheckBox.UseVisualStyleBackColor = true;
      // 
      // m_snapToGridCheckBox
      // 
      this.m_snapToGridCheckBox.AutoSize = true;
      this.m_snapToGridCheckBox.Location = new System.Drawing.Point(45, 43);
      this.m_snapToGridCheckBox.Name = "m_snapToGridCheckBox";
      this.m_snapToGridCheckBox.Size = new System.Drawing.Size(65, 17);
      this.m_snapToGridCheckBox.TabIndex = 1;
      this.m_snapToGridCheckBox.Text = "S&nap To";
      this.m_snapToGridCheckBox.UseVisualStyleBackColor = true;
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(212, 20);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(26, 13);
      this.label6.TabIndex = 3;
      this.label6.Text = "&Size";
      // 
      // m_gridSizeUpDown
      // 
      this.m_gridSizeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_gridSizeUpDown.DecimalPlaces = 1;
      this.m_gridSizeUpDown.Location = new System.Drawing.Point(242, 16);
      this.m_gridSizeUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_gridSizeUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
      this.m_gridSizeUpDown.Name = "m_gridSizeUpDown";
      this.m_gridSizeUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_gridSizeUpDown.TabIndex = 4;
      this.m_gridSizeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_gridSizeUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.txtDefaultRoomName);
      this.groupBox1.Controls.Add(this.label20);
      this.groupBox1.Controls.Add(this.label34);
      this.groupBox1.Controls.Add(this.m_objectListOffsetFromRoomNumericUpDown);
      this.groupBox1.Controls.Add(this.label13);
      this.groupBox1.Controls.Add(this.m_darknessStripeSizeNumericUpDown);
      this.groupBox1.Location = new System.Drawing.Point(5, 6);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(343, 96);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "&Room";
      // 
      // txtDefaultRoomName
      // 
      this.txtDefaultRoomName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDefaultRoomName.BackColor = System.Drawing.SystemColors.Window;
      this.txtDefaultRoomName.CausesValidation = false;
      this.txtDefaultRoomName.Location = new System.Drawing.Point(242, 64);
      this.txtDefaultRoomName.Name = "txtDefaultRoomName";
      this.txtDefaultRoomName.Size = new System.Drawing.Size(95, 21);
      this.txtDefaultRoomName.TabIndex = 5;
      // 
      // label20
      // 
      this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label20.AutoSize = true;
      this.label20.Location = new System.Drawing.Point(143, 68);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(97, 13);
      this.label20.TabIndex = 4;
      this.label20.Text = "Default Room &Text";
      // 
      // label34
      // 
      this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label34.AutoSize = true;
      this.label34.Location = new System.Drawing.Point(148, 43);
      this.label34.Name = "label34";
      this.label34.Size = new System.Drawing.Size(92, 13);
      this.label34.TabIndex = 2;
      this.label34.Text = "&Object List Offset";
      // 
      // m_objectListOffsetFromRoomNumericUpDown
      // 
      this.m_objectListOffsetFromRoomNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_objectListOffsetFromRoomNumericUpDown.DecimalPlaces = 1;
      this.m_objectListOffsetFromRoomNumericUpDown.Location = new System.Drawing.Point(242, 39);
      this.m_objectListOffsetFromRoomNumericUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_objectListOffsetFromRoomNumericUpDown.Name = "m_objectListOffsetFromRoomNumericUpDown";
      this.m_objectListOffsetFromRoomNumericUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_objectListOffsetFromRoomNumericUpDown.TabIndex = 3;
      this.m_objectListOffsetFromRoomNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_objectListOffsetFromRoomNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label13
      // 
      this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(133, 17);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(104, 13);
      this.label13.TabIndex = 0;
      this.label13.Text = "&Darkness Stripe Size";
      // 
      // m_darknessStripeSizeNumericUpDown
      // 
      this.m_darknessStripeSizeNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_darknessStripeSizeNumericUpDown.DecimalPlaces = 1;
      this.m_darknessStripeSizeNumericUpDown.Location = new System.Drawing.Point(242, 13);
      this.m_darknessStripeSizeNumericUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_darknessStripeSizeNumericUpDown.Name = "m_darknessStripeSizeNumericUpDown";
      this.m_darknessStripeSizeNumericUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_darknessStripeSizeNumericUpDown.TabIndex = 1;
      this.m_darknessStripeSizeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_darknessStripeSizeNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.m_okButton);
      this.panel1.Controls.Add(this.m_cancelButton);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(10, 327);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(363, 35);
      this.panel1.TabIndex = 1;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabRegions);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
      this.tabControl1.Location = new System.Drawing.Point(10, 10);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(363, 317);
      this.tabControl1.TabIndex = 0;
      this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
      // 
      // tabPage4
      // 
      this.tabPage4.Controls.Add(this.groupBox3);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Size = new System.Drawing.Size(355, 291);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "About";
      this.tabPage4.UseVisualStyleBackColor = true;
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.label17);
      this.groupBox3.Controls.Add(this.label16);
      this.groupBox3.Controls.Add(this.m_historyTextBox);
      this.groupBox3.Controls.Add(this.label15);
      this.groupBox3.Controls.Add(this.label14);
      this.groupBox3.Controls.Add(this.m_descriptionTextBox);
      this.groupBox3.Controls.Add(this.label10);
      this.groupBox3.Controls.Add(this.m_authorTextBox);
      this.groupBox3.Controls.Add(this.m_titleTextBox);
      this.groupBox3.Location = new System.Drawing.Point(5, 6);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(343, 274);
      this.groupBox3.TabIndex = 0;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "&Map";
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Location = new System.Drawing.Point(24, 186);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(41, 13);
      this.label17.TabIndex = 7;
      this.label17.Text = "&History";
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.ForeColor = System.Drawing.SystemColors.GrayText;
      this.label16.Location = new System.Drawing.Point(6, 19);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(306, 13);
      this.label16.TabIndex = 0;
      this.label16.Text = "These details help identify your map to others who may see it.";
      // 
      // m_historyTextBox
      // 
      this.m_historyTextBox.AcceptsReturn = true;
      this.m_historyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_historyTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_historyTextBox.Location = new System.Drawing.Point(69, 183);
      this.m_historyTextBox.Multiline = true;
      this.m_historyTextBox.Name = "m_historyTextBox";
      this.m_historyTextBox.Size = new System.Drawing.Size(265, 82);
      this.m_historyTextBox.TabIndex = 8;
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(3, 98);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(60, 13);
      this.label15.TabIndex = 5;
      this.label15.Text = "&Description";
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(25, 72);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(40, 13);
      this.label14.TabIndex = 3;
      this.label14.Text = "&Author";
      // 
      // m_descriptionTextBox
      // 
      this.m_descriptionTextBox.AcceptsReturn = true;
      this.m_descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_descriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_descriptionTextBox.Location = new System.Drawing.Point(69, 95);
      this.m_descriptionTextBox.Multiline = true;
      this.m_descriptionTextBox.Name = "m_descriptionTextBox";
      this.m_descriptionTextBox.Size = new System.Drawing.Size(265, 82);
      this.m_descriptionTextBox.TabIndex = 6;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(36, 46);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(27, 13);
      this.label10.TabIndex = 1;
      this.label10.Text = "&Title";
      // 
      // m_authorTextBox
      // 
      this.m_authorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_authorTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_authorTextBox.Location = new System.Drawing.Point(69, 69);
      this.m_authorTextBox.Name = "m_authorTextBox";
      this.m_authorTextBox.Size = new System.Drawing.Size(265, 21);
      this.m_authorTextBox.TabIndex = 4;
      // 
      // m_titleTextBox
      // 
      this.m_titleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_titleTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_titleTextBox.Location = new System.Drawing.Point(69, 43);
      this.m_titleTextBox.Name = "m_titleTextBox";
      this.m_titleTextBox.Size = new System.Drawing.Size(265, 21);
      this.m_titleTextBox.TabIndex = 2;
      // 
      // tabPage1
      // 
      this.tabPage1.Controls.Add(this.m_colorsGroupBox);
      this.tabPage1.Controls.Add(this.m_fontsGroupBox);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(355, 291);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Colors and Fonts";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // m_colorsGroupBox
      // 
      this.m_colorsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_colorsGroupBox.Controls.Add(this.m_colorListBox);
      this.m_colorsGroupBox.Controls.Add(this.m_changeColorButton);
      this.m_colorsGroupBox.Location = new System.Drawing.Point(5, 6);
      this.m_colorsGroupBox.Name = "m_colorsGroupBox";
      this.m_colorsGroupBox.Size = new System.Drawing.Size(343, 172);
      this.m_colorsGroupBox.TabIndex = 0;
      this.m_colorsGroupBox.TabStop = false;
      this.m_colorsGroupBox.Text = "&Colors";
      // 
      // m_colorListBox
      // 
      this.m_colorListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_colorListBox.FormattingEnabled = true;
      this.m_colorListBox.Items.AddRange(new object[] {
            "Canvas",
            "Fill",
            "Border",
            "Connection",
            "Connection (selected)",
            "Connection (hover)",
            "Room Text",
            "Object Text",
            "Connection Text",
            "Grid",
            "Start Room"});
      this.m_colorListBox.Location = new System.Drawing.Point(82, 19);
      this.m_colorListBox.Name = "m_colorListBox";
      this.m_colorListBox.Size = new System.Drawing.Size(174, 147);
      this.m_colorListBox.TabIndex = 0;
      this.m_colorListBox.DoubleClick += new System.EventHandler(this.onChangeColor);
      // 
      // m_changeColorButton
      // 
      this.m_changeColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeColorButton.Location = new System.Drawing.Point(262, 19);
      this.m_changeColorButton.Name = "m_changeColorButton";
      this.m_changeColorButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeColorButton.TabIndex = 1;
      this.m_changeColorButton.Text = "C&hange...";
      this.m_changeColorButton.UseVisualStyleBackColor = true;
      this.m_changeColorButton.Click += new System.EventHandler(this.onChangeColor);
      // 
      // m_fontsGroupBox
      // 
      this.m_fontsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_fontsGroupBox.Controls.Add(this.label9);
      this.m_fontsGroupBox.Controls.Add(this.m_lineFontSizeTextBox);
      this.m_fontsGroupBox.Controls.Add(this.m_changeLineFontButton);
      this.m_fontsGroupBox.Controls.Add(this.m_lineFontNameTextBox);
      this.m_fontsGroupBox.Controls.Add(this.label12);
      this.m_fontsGroupBox.Controls.Add(this.m_smallFontSizeTextBox);
      this.m_fontsGroupBox.Controls.Add(this.m_changeSmallFontButton);
      this.m_fontsGroupBox.Controls.Add(this.m_smallFontNameTextBox);
      this.m_fontsGroupBox.Controls.Add(this.label11);
      this.m_fontsGroupBox.Controls.Add(this.m_largeFontSizeTextBox);
      this.m_fontsGroupBox.Controls.Add(this.m_changeLargeFontButton);
      this.m_fontsGroupBox.Controls.Add(this.m_largeFontNameTextBox);
      this.m_fontsGroupBox.Location = new System.Drawing.Point(5, 184);
      this.m_fontsGroupBox.Name = "m_fontsGroupBox";
      this.m_fontsGroupBox.Size = new System.Drawing.Size(343, 99);
      this.m_fontsGroupBox.TabIndex = 1;
      this.m_fontsGroupBox.TabStop = false;
      this.m_fontsGroupBox.Text = "&Fonts";
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(39, 73);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(26, 13);
      this.label9.TabIndex = 8;
      this.label9.Text = "&Line";
      // 
      // m_lineFontSizeTextBox
      // 
      this.m_lineFontSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_lineFontSizeTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_lineFontSizeTextBox.Location = new System.Drawing.Point(225, 70);
      this.m_lineFontSizeTextBox.Name = "m_lineFontSizeTextBox";
      this.m_lineFontSizeTextBox.ReadOnly = true;
      this.m_lineFontSizeTextBox.Size = new System.Drawing.Size(31, 21);
      this.m_lineFontSizeTextBox.TabIndex = 10;
      // 
      // m_changeLineFontButton
      // 
      this.m_changeLineFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeLineFontButton.Location = new System.Drawing.Point(262, 68);
      this.m_changeLineFontButton.Name = "m_changeLineFontButton";
      this.m_changeLineFontButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeLineFontButton.TabIndex = 11;
      this.m_changeLineFontButton.Text = "Cha&nge...";
      this.m_changeLineFontButton.UseVisualStyleBackColor = true;
      this.m_changeLineFontButton.Click += new System.EventHandler(this.ChangeLineFontButton_Click);
      // 
      // m_lineFontNameTextBox
      // 
      this.m_lineFontNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_lineFontNameTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_lineFontNameTextBox.Location = new System.Drawing.Point(82, 70);
      this.m_lineFontNameTextBox.Name = "m_lineFontNameTextBox";
      this.m_lineFontNameTextBox.ReadOnly = true;
      this.m_lineFontNameTextBox.Size = new System.Drawing.Size(137, 21);
      this.m_lineFontNameTextBox.TabIndex = 9;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(39, 47);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(39, 13);
      this.label12.TabIndex = 4;
      this.label12.Text = "&Object";
      // 
      // m_smallFontSizeTextBox
      // 
      this.m_smallFontSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_smallFontSizeTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_smallFontSizeTextBox.Location = new System.Drawing.Point(225, 44);
      this.m_smallFontSizeTextBox.Name = "m_smallFontSizeTextBox";
      this.m_smallFontSizeTextBox.ReadOnly = true;
      this.m_smallFontSizeTextBox.Size = new System.Drawing.Size(31, 21);
      this.m_smallFontSizeTextBox.TabIndex = 6;
      // 
      // m_changeSmallFontButton
      // 
      this.m_changeSmallFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeSmallFontButton.Location = new System.Drawing.Point(262, 42);
      this.m_changeSmallFontButton.Name = "m_changeSmallFontButton";
      this.m_changeSmallFontButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeSmallFontButton.TabIndex = 7;
      this.m_changeSmallFontButton.Text = "Cha&nge...";
      this.m_changeSmallFontButton.UseVisualStyleBackColor = true;
      this.m_changeSmallFontButton.Click += new System.EventHandler(this.ChangeSmallFontButton_Click);
      // 
      // m_smallFontNameTextBox
      // 
      this.m_smallFontNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_smallFontNameTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_smallFontNameTextBox.Location = new System.Drawing.Point(82, 44);
      this.m_smallFontNameTextBox.Name = "m_smallFontNameTextBox";
      this.m_smallFontNameTextBox.ReadOnly = true;
      this.m_smallFontNameTextBox.Size = new System.Drawing.Size(137, 21);
      this.m_smallFontNameTextBox.TabIndex = 5;
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(41, 22);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(34, 13);
      this.label11.TabIndex = 0;
      this.label11.Text = "&Room";
      // 
      // m_largeFontSizeTextBox
      // 
      this.m_largeFontSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_largeFontSizeTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_largeFontSizeTextBox.Location = new System.Drawing.Point(225, 19);
      this.m_largeFontSizeTextBox.Name = "m_largeFontSizeTextBox";
      this.m_largeFontSizeTextBox.ReadOnly = true;
      this.m_largeFontSizeTextBox.Size = new System.Drawing.Size(31, 21);
      this.m_largeFontSizeTextBox.TabIndex = 2;
      // 
      // m_changeLargeFontButton
      // 
      this.m_changeLargeFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeLargeFontButton.Location = new System.Drawing.Point(262, 16);
      this.m_changeLargeFontButton.Name = "m_changeLargeFontButton";
      this.m_changeLargeFontButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeLargeFontButton.TabIndex = 3;
      this.m_changeLargeFontButton.Text = "Ch&ange...";
      this.m_changeLargeFontButton.UseVisualStyleBackColor = true;
      this.m_changeLargeFontButton.Click += new System.EventHandler(this.ChangeLargeFontButton_Click);
      // 
      // m_largeFontNameTextBox
      // 
      this.m_largeFontNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_largeFontNameTextBox.BackColor = System.Drawing.SystemColors.Window;
      this.m_largeFontNameTextBox.Location = new System.Drawing.Point(82, 18);
      this.m_largeFontNameTextBox.Name = "m_largeFontNameTextBox";
      this.m_largeFontNameTextBox.ReadOnly = true;
      this.m_largeFontNameTextBox.Size = new System.Drawing.Size(137, 21);
      this.m_largeFontNameTextBox.TabIndex = 1;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.m_linesGroupBox);
      this.tabPage2.Controls.Add(this.m_gridGroupBox);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(355, 291);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Lines and Grid";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // tabRegions
      // 
      this.tabRegions.Controls.Add(this.label19);
      this.tabRegions.Controls.Add(this.btnDeleteRegion);
      this.tabRegions.Controls.Add(this.btnAddRegion);
      this.tabRegions.Controls.Add(this.btnChange);
      this.tabRegions.Controls.Add(this.m_RegionListing);
      this.tabRegions.Location = new System.Drawing.Point(4, 22);
      this.tabRegions.Name = "tabRegions";
      this.tabRegions.Padding = new System.Windows.Forms.Padding(3);
      this.tabRegions.Size = new System.Drawing.Size(355, 291);
      this.tabRegions.TabIndex = 4;
      this.tabRegions.Text = "Regions";
      this.tabRegions.UseVisualStyleBackColor = true;
      // 
      // label19
      // 
      this.label19.AutoSize = true;
      this.label19.ForeColor = System.Drawing.Color.Blue;
      this.label19.Location = new System.Drawing.Point(7, 147);
      this.label19.Name = "label19";
      this.label19.Size = new System.Drawing.Size(151, 13);
      this.label19.TabIndex = 5;
      this.label19.Text = "Note:  F2 to edit Region name";
      // 
      // btnDeleteRegion
      // 
      this.btnDeleteRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDeleteRegion.Location = new System.Drawing.Point(257, 117);
      this.btnDeleteRegion.Name = "btnDeleteRegion";
      this.btnDeleteRegion.Size = new System.Drawing.Size(92, 23);
      this.btnDeleteRegion.TabIndex = 4;
      this.btnDeleteRegion.Text = "&Delete Region";
      this.btnDeleteRegion.UseVisualStyleBackColor = true;
      this.btnDeleteRegion.Click += new System.EventHandler(this.btnDeleteRegion_Click);
      // 
      // btnAddRegion
      // 
      this.btnAddRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddRegion.Location = new System.Drawing.Point(257, 6);
      this.btnAddRegion.Name = "btnAddRegion";
      this.btnAddRegion.Size = new System.Drawing.Size(92, 23);
      this.btnAddRegion.TabIndex = 2;
      this.btnAddRegion.Text = "&Add Region";
      this.btnAddRegion.UseVisualStyleBackColor = true;
      this.btnAddRegion.Click += new System.EventHandler(this.btnAddRegion_Click);
      // 
      // btnChange
      // 
      this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnChange.Location = new System.Drawing.Point(257, 34);
      this.btnChange.Name = "btnChange";
      this.btnChange.Size = new System.Drawing.Size(92, 23);
      this.btnChange.TabIndex = 3;
      this.btnChange.Text = "C&hange...";
      this.btnChange.UseVisualStyleBackColor = true;
      this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
      // 
      // m_RegionListing
      // 
      this.m_RegionListing.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_RegionListing.FormattingEnabled = true;
      this.m_RegionListing.HorizontalScrollbar = true;
      this.m_RegionListing.Location = new System.Drawing.Point(6, 6);
      this.m_RegionListing.Name = "m_RegionListing";
      this.m_RegionListing.Size = new System.Drawing.Size(245, 134);
      this.m_RegionListing.TabIndex = 1;
      this.m_RegionListing.SelectedIndexChanged += new System.EventHandler(this.m_RegionListing_SelectedIndexChanged);
      this.m_RegionListing.DoubleClick += new System.EventHandler(this.onChangeRegionColor);
      this.m_RegionListing.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_RegionListing_KeyDown);
      this.m_RegionListing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.m_RegionListing_KeyPress);
      this.m_RegionListing.KeyUp += new System.Windows.Forms.KeyEventHandler(this.m_RegionListing_KeyUp);
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.groupBox4);
      this.tabPage3.Controls.Add(this.groupBox2);
      this.tabPage3.Controls.Add(this.groupBox1);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(355, 291);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Other";
      this.tabPage3.UseVisualStyleBackColor = true;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.m_snapToElementDistanceUpDown);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Controls.Add(this.m_handleSizeUpDown);
      this.groupBox2.Location = new System.Drawing.Point(5, 108);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(343, 101);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "&Advanced";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
      this.label7.Location = new System.Drawing.Point(7, 20);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(320, 13);
      this.label7.TabIndex = 0;
      this.label7.Text = "These settings affect the ease with which you can click and drag.";
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(106, 73);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(129, 13);
      this.label3.TabIndex = 3;
      this.label3.Text = "&Snap to Element Distance";
      // 
      // m_snapToElementDistanceUpDown
      // 
      this.m_snapToElementDistanceUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_snapToElementDistanceUpDown.DecimalPlaces = 1;
      this.m_snapToElementDistanceUpDown.Location = new System.Drawing.Point(242, 71);
      this.m_snapToElementDistanceUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_snapToElementDistanceUpDown.Name = "m_snapToElementDistanceUpDown";
      this.m_snapToElementDistanceUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_snapToElementDistanceUpDown.TabIndex = 4;
      this.m_snapToElementDistanceUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_snapToElementDistanceUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(112, 47);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(123, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Resize/Drag &Handle Size";
      // 
      // m_handleSizeUpDown
      // 
      this.m_handleSizeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_handleSizeUpDown.DecimalPlaces = 1;
      this.m_handleSizeUpDown.Location = new System.Drawing.Point(242, 45);
      this.m_handleSizeUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_handleSizeUpDown.Name = "m_handleSizeUpDown";
      this.m_handleSizeUpDown.Size = new System.Drawing.Size(95, 21);
      this.m_handleSizeUpDown.TabIndex = 2;
      this.m_handleSizeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_handleSizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // groupBox4
      // 
      this.groupBox4.Location = new System.Drawing.Point(5, 214);
      this.groupBox4.Controls.Add(this.label4b);
      this.groupBox4.Controls.Add(this.label4c);
      this.groupBox4.Controls.Add(this.m_documentSpecificMargins);
      this.groupBox4.Controls.Add(this.m_documentHorizontalMargins);
      this.groupBox4.Controls.Add(this.m_documentVerticalMargins);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new System.Drawing.Size(343, 80);
      this.groupBox4.TabIndex = 1;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "&Margins";
      // 
      // label4b
      // 
      this.label4b.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label4b.AutoSize = true;
      this.label4b.Location = new System.Drawing.Point(112, 29);
      this.label4b.Name = "label4b";
      this.label4b.Size = new System.Drawing.Size(123, 13);
      this.label4b.TabIndex = 1;
      this.label4b.Text = "&Horizontal margins";
      // 
      // m_documentSpecificMargins
      // 
      this.m_documentSpecificMargins.AutoSize = true;
      this.m_documentSpecificMargins.Cursor = System.Windows.Forms.Cursors.Help;
      this.m_documentSpecificMargins.Location = new System.Drawing.Point(112,9);
      this.m_documentSpecificMargins.Name = "m_documentSpecificMargins";
      this.m_documentSpecificMargins.Size = new System.Drawing.Size(200, 17);
      this.m_documentSpecificMargins.TabIndex = 0;
      this.m_documentSpecificMargins.Text = "Document-Specific Margins";
      this.m_documentSpecificMargins.UseVisualStyleBackColor = true;
      // 
      // m_documentHorizontalMargins
      // 
      this.m_documentHorizontalMargins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_documentHorizontalMargins.DecimalPlaces = 1;
      this.m_documentHorizontalMargins.Location = new System.Drawing.Point(242, 29);
      this.m_documentHorizontalMargins.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_documentHorizontalMargins.Name = "m_documentHorizontalMargins";
      this.m_documentHorizontalMargins.Size = new System.Drawing.Size(95, 20);
      this.m_documentHorizontalMargins.TabIndex = 4;
      this.m_documentHorizontalMargins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_documentHorizontalMargins.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // label4c
      // 
      this.label4c.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label4c.AutoSize = true;
      this.label4c.Location = new System.Drawing.Point(112, 49);
      this.label4c.Name = "label4c";
      this.label4c.Size = new System.Drawing.Size(123, 13);
      this.label4c.TabIndex = 1;
      this.label4c.Text = "&Vertical margins";
      // 
      // m_documentVerticalMargins
      // 
      this.m_documentVerticalMargins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_documentVerticalMargins.DecimalPlaces = 1;
      this.m_documentVerticalMargins.Location = new System.Drawing.Point(242, 49);
      this.m_documentVerticalMargins.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_documentVerticalMargins.Name = "m_documentVerticalMargins";
      this.m_documentVerticalMargins.Size = new System.Drawing.Size(95, 20);
      this.m_documentVerticalMargins.TabIndex = 4;
      this.m_documentVerticalMargins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_documentVerticalMargins.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // propertySettings1
      // 
      this.propertySettings1.DisplayName = "Region1";
      this.propertySettings1.PropertyName = "";
      // 
      // superTooltip1
      // 
      this.superTooltip1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
      // 
      // SettingsDialog
      // 
      this.AcceptButton = this.m_okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_cancelButton;
      this.ClientSize = new System.Drawing.Size(383, 374);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.tabControl1);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SettingsDialog";
      this.Padding = new System.Windows.Forms.Padding(10);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Map Settings";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsDialog_FormClosing);
      this.Load += new System.EventHandler(this.SettingsDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.m_lineWidthUpDown)).EndInit();
      this.m_linesGroupBox.ResumeLayout(false);
      this.m_linesGroupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_preferredDistanceBetweenRoomsUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_textOffsetFromLineUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_connectionStalkLengthUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_arrowSizeUpDown)).EndInit();
      this.m_gridGroupBox.ResumeLayout(false);
      this.m_gridGroupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_gridSizeUpDown)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_objectListOffsetFromRoomNumericUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_darknessStripeSizeNumericUpDown)).EndInit();
      this.panel1.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.m_colorsGroupBox.ResumeLayout(false);
      this.m_fontsGroupBox.ResumeLayout(false);
      this.m_fontsGroupBox.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabRegions.ResumeLayout(false);
      this.tabRegions.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_snapToElementDistanceUpDown)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_handleSizeUpDown)).EndInit();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.CheckBox m_handDrawnCheckBox;
        private System.Windows.Forms.NumericUpDown m_lineWidthUpDown;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox m_linesGroupBox;
        private System.Windows.Forms.GroupBox m_gridGroupBox;
        private System.Windows.Forms.CheckBox m_snapToGridCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown m_gridSizeUpDown;
        private System.Windows.Forms.CheckBox m_showGridCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown m_darknessStripeSizeNumericUpDown;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.NumericUpDown m_objectListOffsetFromRoomNumericUpDown;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox m_colorsGroupBox;
        private System.Windows.Forms.ListBox m_colorListBox;
        private System.Windows.Forms.Button m_changeColorButton;
        private System.Windows.Forms.GroupBox m_fontsGroupBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox m_smallFontSizeTextBox;
        private System.Windows.Forms.Button m_changeSmallFontButton;
        private System.Windows.Forms.TextBox m_smallFontNameTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox m_largeFontSizeTextBox;
        private System.Windows.Forms.Button m_changeLargeFontButton;
        private System.Windows.Forms.TextBox m_largeFontNameTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown m_arrowSizeUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown m_connectionStalkLengthUpDown;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown m_snapToElementDistanceUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown m_handleSizeUpDown;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4b;
        private System.Windows.Forms.Label label4c;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox m_documentSpecificMargins;
        private System.Windows.Forms.NumericUpDown m_documentHorizontalMargins;
        private System.Windows.Forms.NumericUpDown m_documentVerticalMargins;
        private System.Windows.Forms.NumericUpDown m_textOffsetFromLineUpDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox m_lineFontSizeTextBox;
        private System.Windows.Forms.Button m_changeLineFontButton;
        private System.Windows.Forms.TextBox m_lineFontNameTextBox;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox m_descriptionTextBox;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox m_authorTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_titleTextBox;
        private System.Windows.Forms.TextBox m_historyTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox m_showOriginCheckBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown m_preferredDistanceBetweenRoomsUpDown;
        private System.Windows.Forms.TabPage tabRegions;
        private System.Windows.Forms.ListBox m_RegionListing;
        private System.Windows.Forms.Button btnAddRegion;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnDeleteRegion;
        private System.Windows.Forms.Label label19;
        private DevComponents.DotNetBar.PropertySettings propertySettings1;
        private DevComponents.DotNetBar.SuperTooltip superTooltip1;
    private System.Windows.Forms.TextBox txtDefaultRoomName;
    private System.Windows.Forms.Label label20;
  }
}
