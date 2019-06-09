/*
    Copyright (c) 2010-2018 by Genstein and Jason Lautzenheiser.

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

namespace Trizbort.UI
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
            this.label21 = new System.Windows.Forms.Label();
            this.cboRoomShape = new System.Windows.Forms.ComboBox();
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
            this.label22 = new System.Windows.Forms.Label();
            this.m_subtitleFontSizeTextBox = new System.Windows.Forms.TextBox();
            this.m_changeSubtitleFontButton = new System.Windows.Forms.Button();
            this.m_subtitleFontNameTextBox = new System.Windows.Forms.TextBox();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.m_wrapTextAtDashes = new System.Windows.Forms.CheckBox();
            this.label4b = new System.Windows.Forms.Label();
            this.label4c = new System.Windows.Forms.Label();
            this.m_documentSpecificMargins = new System.Windows.Forms.CheckBox();
            this.m_documentHorizontalMargins = new System.Windows.Forms.NumericUpDown();
            this.m_documentVerticalMargins = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_snapToElementDistanceUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.m_handleSizeUpDown = new System.Windows.Forms.NumericUpDown();
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
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_documentHorizontalMargins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_documentVerticalMargins)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_snapToElementDistanceUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_handleSizeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_okButton.Location = new System.Drawing.Point(414, 24);
            this.m_okButton.Margin = new System.Windows.Forms.Padding(6);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(150, 46);
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
            this.m_cancelButton.Location = new System.Drawing.Point(576, 24);
            this.m_cancelButton.Margin = new System.Windows.Forms.Padding(6);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(150, 46);
            this.m_cancelButton.TabIndex = 1;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // m_handDrawnCheckBox
            // 
            this.m_handDrawnCheckBox.AutoSize = true;
            this.m_handDrawnCheckBox.Cursor = System.Windows.Forms.Cursors.Help;
            this.m_handDrawnCheckBox.Location = new System.Drawing.Point(96, 40);
            this.m_handDrawnCheckBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_handDrawnCheckBox.Name = "m_handDrawnCheckBox";
            this.m_handDrawnCheckBox.Size = new System.Drawing.Size(197, 36);
            this.m_handDrawnCheckBox.TabIndex = 0;
            this.m_handDrawnCheckBox.Text = "\"&Hand-drawn\"";
            this.m_handDrawnCheckBox.UseVisualStyleBackColor = true;
            this.m_handDrawnCheckBox.Visible = false;
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
            this.m_lineWidthUpDown.Location = new System.Drawing.Point(484, 38);
            this.m_lineWidthUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_lineWidthUpDown.Name = "m_lineWidthUpDown";
            this.m_lineWidthUpDown.Size = new System.Drawing.Size(190, 39);
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
            this.label5.Location = new System.Drawing.Point(402, 41);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 32);
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
            this.m_linesGroupBox.Location = new System.Drawing.Point(10, 12);
            this.m_linesGroupBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_linesGroupBox.Name = "m_linesGroupBox";
            this.m_linesGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.m_linesGroupBox.Size = new System.Drawing.Size(686, 326);
            this.m_linesGroupBox.TabIndex = 0;
            this.m_linesGroupBox.TabStop = false;
            this.m_linesGroupBox.Text = "&Lines";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Cursor = System.Windows.Forms.Cursors.Help;
            this.label18.Location = new System.Drawing.Point(92, 199);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(389, 32);
            this.label18.TabIndex = 7;
            this.label18.Text = "&Preferred Distance Between Rooms";
            // 
            // m_preferredDistanceBetweenRoomsUpDown
            // 
            this.m_preferredDistanceBetweenRoomsUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_preferredDistanceBetweenRoomsUpDown.DecimalPlaces = 1;
            this.m_preferredDistanceBetweenRoomsUpDown.Location = new System.Drawing.Point(484, 196);
            this.m_preferredDistanceBetweenRoomsUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_preferredDistanceBetweenRoomsUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_preferredDistanceBetweenRoomsUpDown.Name = "m_preferredDistanceBetweenRoomsUpDown";
            this.m_preferredDistanceBetweenRoomsUpDown.Size = new System.Drawing.Size(190, 39);
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
            this.label8.Location = new System.Drawing.Point(238, 253);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(243, 32);
            this.label8.TabIndex = 9;
            this.label8.Text = "&Text Offset From Line";
            // 
            // m_textOffsetFromLineUpDown
            // 
            this.m_textOffsetFromLineUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textOffsetFromLineUpDown.DecimalPlaces = 1;
            this.m_textOffsetFromLineUpDown.Location = new System.Drawing.Point(484, 250);
            this.m_textOffsetFromLineUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_textOffsetFromLineUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_textOffsetFromLineUpDown.Name = "m_textOffsetFromLineUpDown";
            this.m_textOffsetFromLineUpDown.Size = new System.Drawing.Size(190, 39);
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
            this.label1.Location = new System.Drawing.Point(197, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(284, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "&Room Arrow Stalk Length";
            // 
            // m_connectionStalkLengthUpDown
            // 
            this.m_connectionStalkLengthUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_connectionStalkLengthUpDown.DecimalPlaces = 1;
            this.m_connectionStalkLengthUpDown.Location = new System.Drawing.Point(484, 142);
            this.m_connectionStalkLengthUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_connectionStalkLengthUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_connectionStalkLengthUpDown.Name = "m_connectionStalkLengthUpDown";
            this.m_connectionStalkLengthUpDown.Size = new System.Drawing.Size(190, 39);
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
            this.label4.Location = new System.Drawing.Point(354, 93);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "&Arrow Size";
            // 
            // m_arrowSizeUpDown
            // 
            this.m_arrowSizeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_arrowSizeUpDown.DecimalPlaces = 1;
            this.m_arrowSizeUpDown.Location = new System.Drawing.Point(484, 90);
            this.m_arrowSizeUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_arrowSizeUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_arrowSizeUpDown.Name = "m_arrowSizeUpDown";
            this.m_arrowSizeUpDown.Size = new System.Drawing.Size(190, 39);
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
            this.m_gridGroupBox.Location = new System.Drawing.Point(10, 350);
            this.m_gridGroupBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_gridGroupBox.Name = "m_gridGroupBox";
            this.m_gridGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.m_gridGroupBox.Size = new System.Drawing.Size(686, 144);
            this.m_gridGroupBox.TabIndex = 1;
            this.m_gridGroupBox.TabStop = false;
            this.m_gridGroupBox.Text = "&Grid";
            // 
            // m_showOriginCheckBox
            // 
            this.m_showOriginCheckBox.AutoSize = true;
            this.m_showOriginCheckBox.Location = new System.Drawing.Point(228, 33);
            this.m_showOriginCheckBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_showOriginCheckBox.Name = "m_showOriginCheckBox";
            this.m_showOriginCheckBox.Size = new System.Drawing.Size(178, 36);
            this.m_showOriginCheckBox.TabIndex = 2;
            this.m_showOriginCheckBox.Text = "Show &Origin";
            this.m_showOriginCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_showGridCheckBox
            // 
            this.m_showGridCheckBox.AutoSize = true;
            this.m_showGridCheckBox.Location = new System.Drawing.Point(90, 33);
            this.m_showGridCheckBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_showGridCheckBox.Name = "m_showGridCheckBox";
            this.m_showGridCheckBox.Size = new System.Drawing.Size(117, 36);
            this.m_showGridCheckBox.TabIndex = 0;
            this.m_showGridCheckBox.Text = "&Visible";
            this.m_showGridCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_snapToGridCheckBox
            // 
            this.m_snapToGridCheckBox.AutoSize = true;
            this.m_snapToGridCheckBox.Location = new System.Drawing.Point(90, 86);
            this.m_snapToGridCheckBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_snapToGridCheckBox.Name = "m_snapToGridCheckBox";
            this.m_snapToGridCheckBox.Size = new System.Drawing.Size(132, 36);
            this.m_snapToGridCheckBox.TabIndex = 1;
            this.m_snapToGridCheckBox.Text = "S&nap To";
            this.m_snapToGridCheckBox.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(423, 35);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 32);
            this.label6.TabIndex = 3;
            this.label6.Text = "&Size";
            // 
            // m_gridSizeUpDown
            // 
            this.m_gridSizeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_gridSizeUpDown.DecimalPlaces = 1;
            this.m_gridSizeUpDown.Location = new System.Drawing.Point(484, 32);
            this.m_gridSizeUpDown.Margin = new System.Windows.Forms.Padding(6);
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
            this.m_gridSizeUpDown.Size = new System.Drawing.Size(190, 39);
            this.m_gridSizeUpDown.TabIndex = 4;
            this.m_gridSizeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_gridSizeUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.cboRoomShape);
            this.groupBox1.Controls.Add(this.txtDefaultRoomName);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.m_objectListOffsetFromRoomNumericUpDown);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.m_darknessStripeSizeNumericUpDown);
            this.groupBox1.Location = new System.Drawing.Point(10, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox1.Size = new System.Drawing.Size(686, 234);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "&Room";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(169, 27);
            this.label21.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(235, 32);
            this.label21.TabIndex = 9;
            this.label21.Text = "Default Room Shape";
            // 
            // cboRoomShape
            // 
            this.cboRoomShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoomShape.FormattingEnabled = true;
            this.cboRoomShape.Items.AddRange(new object[] {
            "Straight Edges",
            "Rounded Corners",
            "Ellipse",
            "Octagonal"});
            this.cboRoomShape.Location = new System.Drawing.Point(416, 24);
            this.cboRoomShape.Margin = new System.Windows.Forms.Padding(6);
            this.cboRoomShape.Name = "cboRoomShape";
            this.cboRoomShape.Size = new System.Drawing.Size(258, 40);
            this.cboRoomShape.TabIndex = 8;
            // 
            // txtDefaultRoomName
            // 
            this.txtDefaultRoomName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefaultRoomName.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultRoomName.CausesValidation = false;
            this.txtDefaultRoomName.Location = new System.Drawing.Point(416, 178);
            this.txtDefaultRoomName.Margin = new System.Windows.Forms.Padding(6);
            this.txtDefaultRoomName.Name = "txtDefaultRoomName";
            this.txtDefaultRoomName.Size = new System.Drawing.Size(258, 39);
            this.txtDefaultRoomName.TabIndex = 5;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(192, 181);
            this.label20.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(212, 32);
            this.label20.TabIndex = 4;
            this.label20.Text = "Default Room &Text";
            // 
            // label34
            // 
            this.label34.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(205, 131);
            this.label34.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(199, 32);
            this.label34.TabIndex = 2;
            this.label34.Text = "&Object List Offset";
            // 
            // m_objectListOffsetFromRoomNumericUpDown
            // 
            this.m_objectListOffsetFromRoomNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_objectListOffsetFromRoomNumericUpDown.DecimalPlaces = 1;
            this.m_objectListOffsetFromRoomNumericUpDown.Location = new System.Drawing.Point(416, 127);
            this.m_objectListOffsetFromRoomNumericUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_objectListOffsetFromRoomNumericUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_objectListOffsetFromRoomNumericUpDown.Name = "m_objectListOffsetFromRoomNumericUpDown";
            this.m_objectListOffsetFromRoomNumericUpDown.Size = new System.Drawing.Size(258, 39);
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
            this.label13.Location = new System.Drawing.Point(175, 79);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(229, 32);
            this.label13.TabIndex = 0;
            this.label13.Text = "&Darkness Stripe Size";
            // 
            // m_darknessStripeSizeNumericUpDown
            // 
            this.m_darknessStripeSizeNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_darknessStripeSizeNumericUpDown.DecimalPlaces = 1;
            this.m_darknessStripeSizeNumericUpDown.Location = new System.Drawing.Point(416, 76);
            this.m_darknessStripeSizeNumericUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_darknessStripeSizeNumericUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_darknessStripeSizeNumericUpDown.Name = "m_darknessStripeSizeNumericUpDown";
            this.m_darknessStripeSizeNumericUpDown.Size = new System.Drawing.Size(258, 39);
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
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 754);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 70);
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
            this.tabControl1.Location = new System.Drawing.Point(20, 20);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(726, 722);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Location = new System.Drawing.Point(8, 46);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(710, 668);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "About";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.m_historyTextBox);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.m_descriptionTextBox);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.m_authorTextBox);
            this.groupBox3.Controls.Add(this.m_titleTextBox);
            this.groupBox3.Location = new System.Drawing.Point(10, 12);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox3.Size = new System.Drawing.Size(694, 651);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "&Map";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(48, 372);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 32);
            this.label17.TabIndex = 7;
            this.label17.Text = "&History";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label16.Location = new System.Drawing.Point(100, 562);
            this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(513, 32);
            this.label16.TabIndex = 0;
            this.label16.Text = "These details help identify your map to others.";
            // 
            // m_historyTextBox
            // 
            this.m_historyTextBox.AcceptsReturn = true;
            this.m_historyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_historyTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_historyTextBox.Location = new System.Drawing.Point(154, 366);
            this.m_historyTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_historyTextBox.Multiline = true;
            this.m_historyTextBox.Name = "m_historyTextBox";
            this.m_historyTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_historyTextBox.Size = new System.Drawing.Size(502, 146);
            this.m_historyTextBox.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 204);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 32);
            this.label15.TabIndex = 5;
            this.label15.Text = "&Description";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(50, 148);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 32);
            this.label14.TabIndex = 3;
            this.label14.Text = "&Author";
            // 
            // m_descriptionTextBox
            // 
            this.m_descriptionTextBox.AcceptsReturn = true;
            this.m_descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_descriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_descriptionTextBox.Location = new System.Drawing.Point(154, 198);
            this.m_descriptionTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_descriptionTextBox.Multiline = true;
            this.m_descriptionTextBox.Name = "m_descriptionTextBox";
            this.m_descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_descriptionTextBox.Size = new System.Drawing.Size(502, 146);
            this.m_descriptionTextBox.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(72, 92);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 32);
            this.label10.TabIndex = 1;
            this.label10.Text = "&Title";
            // 
            // m_authorTextBox
            // 
            this.m_authorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_authorTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_authorTextBox.Location = new System.Drawing.Point(154, 142);
            this.m_authorTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_authorTextBox.Name = "m_authorTextBox";
            this.m_authorTextBox.Size = new System.Drawing.Size(502, 39);
            this.m_authorTextBox.TabIndex = 4;
            // 
            // m_titleTextBox
            // 
            this.m_titleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_titleTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_titleTextBox.Location = new System.Drawing.Point(154, 86);
            this.m_titleTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_titleTextBox.Name = "m_titleTextBox";
            this.m_titleTextBox.Size = new System.Drawing.Size(502, 39);
            this.m_titleTextBox.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_colorsGroupBox);
            this.tabPage1.Controls.Add(this.m_fontsGroupBox);
            this.tabPage1.Location = new System.Drawing.Point(8, 46);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage1.Size = new System.Drawing.Size(710, 668);
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
            this.m_colorsGroupBox.Location = new System.Drawing.Point(10, 12);
            this.m_colorsGroupBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_colorsGroupBox.Name = "m_colorsGroupBox";
            this.m_colorsGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.m_colorsGroupBox.Size = new System.Drawing.Size(686, 384);
            this.m_colorsGroupBox.TabIndex = 0;
            this.m_colorsGroupBox.TabStop = false;
            this.m_colorsGroupBox.Text = "&Colors";
            // 
            // m_colorListBox
            // 
            this.m_colorListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_colorListBox.FormattingEnabled = true;
            this.m_colorListBox.ItemHeight = 32;
            this.m_colorListBox.Items.AddRange(new object[] {
            "Canvas",
            "Border",
            "Connection",
            "Connection (selected)",
            "Connection (hover)",
            "Subtitle Text",
            "Object Text",
            "Connection Text",
            "Grid",
            "Start Room",
            "End Room"});
            this.m_colorListBox.Location = new System.Drawing.Point(164, 38);
            this.m_colorListBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_colorListBox.Name = "m_colorListBox";
            this.m_colorListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.m_colorListBox.Size = new System.Drawing.Size(344, 292);
            this.m_colorListBox.TabIndex = 0;
            this.m_colorListBox.DoubleClick += new System.EventHandler(this.onChangeColor);
            // 
            // m_changeColorButton
            // 
            this.m_changeColorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeColorButton.Location = new System.Drawing.Point(524, 38);
            this.m_changeColorButton.Margin = new System.Windows.Forms.Padding(6);
            this.m_changeColorButton.Name = "m_changeColorButton";
            this.m_changeColorButton.Size = new System.Drawing.Size(150, 46);
            this.m_changeColorButton.TabIndex = 1;
            this.m_changeColorButton.Text = "C&hange...";
            this.m_changeColorButton.UseVisualStyleBackColor = true;
            this.m_changeColorButton.Click += new System.EventHandler(this.onChangeColor);
            // 
            // m_fontsGroupBox
            // 
            this.m_fontsGroupBox.Controls.Add(this.label22);
            this.m_fontsGroupBox.Controls.Add(this.m_subtitleFontSizeTextBox);
            this.m_fontsGroupBox.Controls.Add(this.m_changeSubtitleFontButton);
            this.m_fontsGroupBox.Controls.Add(this.m_subtitleFontNameTextBox);
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
            this.m_fontsGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.m_fontsGroupBox.Location = new System.Drawing.Point(6, 406);
            this.m_fontsGroupBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_fontsGroupBox.Name = "m_fontsGroupBox";
            this.m_fontsGroupBox.Padding = new System.Windows.Forms.Padding(6);
            this.m_fontsGroupBox.Size = new System.Drawing.Size(698, 256);
            this.m_fontsGroupBox.TabIndex = 1;
            this.m_fontsGroupBox.TabStop = false;
            this.m_fontsGroupBox.Text = "&Fonts";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(54, 197);
            this.label22.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(102, 32);
            this.label22.TabIndex = 12;
            this.label22.Text = "&Subtitle:";
            // 
            // m_subtitleFontSizeTextBox
            // 
            this.m_subtitleFontSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_subtitleFontSizeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_subtitleFontSizeTextBox.Location = new System.Drawing.Point(462, 194);
            this.m_subtitleFontSizeTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_subtitleFontSizeTextBox.Name = "m_subtitleFontSizeTextBox";
            this.m_subtitleFontSizeTextBox.ReadOnly = true;
            this.m_subtitleFontSizeTextBox.Size = new System.Drawing.Size(58, 39);
            this.m_subtitleFontSizeTextBox.TabIndex = 14;
            // 
            // m_changeSubtitleFontButton
            // 
            this.m_changeSubtitleFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeSubtitleFontButton.Location = new System.Drawing.Point(536, 190);
            this.m_changeSubtitleFontButton.Margin = new System.Windows.Forms.Padding(6);
            this.m_changeSubtitleFontButton.Name = "m_changeSubtitleFontButton";
            this.m_changeSubtitleFontButton.Size = new System.Drawing.Size(150, 46);
            this.m_changeSubtitleFontButton.TabIndex = 15;
            this.m_changeSubtitleFontButton.Text = "Cha&nge...";
            this.m_changeSubtitleFontButton.UseVisualStyleBackColor = true;
            this.m_changeSubtitleFontButton.Click += new System.EventHandler(this.ChangeSubtitleFontButton_Click);
            // 
            // m_subtitleFontNameTextBox
            // 
            this.m_subtitleFontNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_subtitleFontNameTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_subtitleFontNameTextBox.Location = new System.Drawing.Point(164, 194);
            this.m_subtitleFontNameTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_subtitleFontNameTextBox.Name = "m_subtitleFontNameTextBox";
            this.m_subtitleFontNameTextBox.ReadOnly = true;
            this.m_subtitleFontNameTextBox.Size = new System.Drawing.Size(282, 39);
            this.m_subtitleFontNameTextBox.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(92, 143);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 32);
            this.label9.TabIndex = 8;
            this.label9.Text = "&Line:";
            // 
            // m_lineFontSizeTextBox
            // 
            this.m_lineFontSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_lineFontSizeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_lineFontSizeTextBox.Location = new System.Drawing.Point(462, 140);
            this.m_lineFontSizeTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_lineFontSizeTextBox.Name = "m_lineFontSizeTextBox";
            this.m_lineFontSizeTextBox.ReadOnly = true;
            this.m_lineFontSizeTextBox.Size = new System.Drawing.Size(58, 39);
            this.m_lineFontSizeTextBox.TabIndex = 10;
            // 
            // m_changeLineFontButton
            // 
            this.m_changeLineFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeLineFontButton.Location = new System.Drawing.Point(536, 136);
            this.m_changeLineFontButton.Margin = new System.Windows.Forms.Padding(6);
            this.m_changeLineFontButton.Name = "m_changeLineFontButton";
            this.m_changeLineFontButton.Size = new System.Drawing.Size(150, 46);
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
            this.m_lineFontNameTextBox.Location = new System.Drawing.Point(164, 140);
            this.m_lineFontNameTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_lineFontNameTextBox.Name = "m_lineFontNameTextBox";
            this.m_lineFontNameTextBox.ReadOnly = true;
            this.m_lineFontNameTextBox.Size = new System.Drawing.Size(282, 39);
            this.m_lineFontNameTextBox.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(56, 91);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 32);
            this.label12.TabIndex = 4;
            this.label12.Text = "&Objects:";
            // 
            // m_smallFontSizeTextBox
            // 
            this.m_smallFontSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_smallFontSizeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_smallFontSizeTextBox.Location = new System.Drawing.Point(462, 88);
            this.m_smallFontSizeTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_smallFontSizeTextBox.Name = "m_smallFontSizeTextBox";
            this.m_smallFontSizeTextBox.ReadOnly = true;
            this.m_smallFontSizeTextBox.Size = new System.Drawing.Size(58, 39);
            this.m_smallFontSizeTextBox.TabIndex = 6;
            // 
            // m_changeSmallFontButton
            // 
            this.m_changeSmallFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeSmallFontButton.Location = new System.Drawing.Point(536, 84);
            this.m_changeSmallFontButton.Margin = new System.Windows.Forms.Padding(6);
            this.m_changeSmallFontButton.Name = "m_changeSmallFontButton";
            this.m_changeSmallFontButton.Size = new System.Drawing.Size(150, 46);
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
            this.m_smallFontNameTextBox.Location = new System.Drawing.Point(164, 88);
            this.m_smallFontNameTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_smallFontNameTextBox.Name = "m_smallFontNameTextBox";
            this.m_smallFontNameTextBox.ReadOnly = true;
            this.m_smallFontNameTextBox.Size = new System.Drawing.Size(282, 39);
            this.m_smallFontNameTextBox.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 39);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(153, 32);
            this.label11.TabIndex = 0;
            this.label11.Text = "&Room Name:";
            // 
            // m_largeFontSizeTextBox
            // 
            this.m_largeFontSizeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_largeFontSizeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_largeFontSizeTextBox.Location = new System.Drawing.Point(462, 36);
            this.m_largeFontSizeTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_largeFontSizeTextBox.Name = "m_largeFontSizeTextBox";
            this.m_largeFontSizeTextBox.ReadOnly = true;
            this.m_largeFontSizeTextBox.Size = new System.Drawing.Size(58, 39);
            this.m_largeFontSizeTextBox.TabIndex = 2;
            // 
            // m_changeLargeFontButton
            // 
            this.m_changeLargeFontButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeLargeFontButton.Location = new System.Drawing.Point(536, 32);
            this.m_changeLargeFontButton.Margin = new System.Windows.Forms.Padding(6);
            this.m_changeLargeFontButton.Name = "m_changeLargeFontButton";
            this.m_changeLargeFontButton.Size = new System.Drawing.Size(150, 46);
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
            this.m_largeFontNameTextBox.Location = new System.Drawing.Point(164, 36);
            this.m_largeFontNameTextBox.Margin = new System.Windows.Forms.Padding(6);
            this.m_largeFontNameTextBox.Name = "m_largeFontNameTextBox";
            this.m_largeFontNameTextBox.ReadOnly = true;
            this.m_largeFontNameTextBox.Size = new System.Drawing.Size(282, 39);
            this.m_largeFontNameTextBox.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_linesGroupBox);
            this.tabPage2.Controls.Add(this.m_gridGroupBox);
            this.tabPage2.Location = new System.Drawing.Point(8, 46);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6);
            this.tabPage2.Size = new System.Drawing.Size(710, 668);
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
            this.tabRegions.Location = new System.Drawing.Point(8, 46);
            this.tabRegions.Margin = new System.Windows.Forms.Padding(6);
            this.tabRegions.Name = "tabRegions";
            this.tabRegions.Padding = new System.Windows.Forms.Padding(6);
            this.tabRegions.Size = new System.Drawing.Size(710, 668);
            this.tabRegions.TabIndex = 4;
            this.tabRegions.Text = "Regions";
            this.tabRegions.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(12, 246);
            this.label19.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(337, 32);
            this.label19.TabIndex = 5;
            this.label19.Text = "Note:  F2 to edit Region name";
            // 
            // btnDeleteRegion
            // 
            this.btnDeleteRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteRegion.Location = new System.Drawing.Point(514, 194);
            this.btnDeleteRegion.Margin = new System.Windows.Forms.Padding(6);
            this.btnDeleteRegion.Name = "btnDeleteRegion";
            this.btnDeleteRegion.Size = new System.Drawing.Size(184, 46);
            this.btnDeleteRegion.TabIndex = 4;
            this.btnDeleteRegion.Text = "&Delete Region";
            this.btnDeleteRegion.UseVisualStyleBackColor = true;
            this.btnDeleteRegion.Click += new System.EventHandler(this.btnDeleteRegion_Click);
            // 
            // btnAddRegion
            // 
            this.btnAddRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRegion.Location = new System.Drawing.Point(514, 12);
            this.btnAddRegion.Margin = new System.Windows.Forms.Padding(6);
            this.btnAddRegion.Name = "btnAddRegion";
            this.btnAddRegion.Size = new System.Drawing.Size(184, 46);
            this.btnAddRegion.TabIndex = 2;
            this.btnAddRegion.Text = "&Add Region";
            this.btnAddRegion.UseVisualStyleBackColor = true;
            this.btnAddRegion.Click += new System.EventHandler(this.btnAddRegion_Click);
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChange.Location = new System.Drawing.Point(514, 68);
            this.btnChange.Margin = new System.Windows.Forms.Padding(6);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(184, 46);
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
            this.m_RegionListing.ItemHeight = 32;
            this.m_RegionListing.Location = new System.Drawing.Point(12, 12);
            this.m_RegionListing.Margin = new System.Windows.Forms.Padding(6);
            this.m_RegionListing.Name = "m_RegionListing";
            this.m_RegionListing.Size = new System.Drawing.Size(486, 228);
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
            this.tabPage3.Location = new System.Drawing.Point(8, 46);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(6);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(710, 668);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Other";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.m_wrapTextAtDashes);
            this.groupBox4.Controls.Add(this.label4b);
            this.groupBox4.Controls.Add(this.label4c);
            this.groupBox4.Controls.Add(this.m_documentSpecificMargins);
            this.groupBox4.Controls.Add(this.m_documentHorizontalMargins);
            this.groupBox4.Controls.Add(this.m_documentVerticalMargins);
            this.groupBox4.Location = new System.Drawing.Point(10, 456);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox4.Size = new System.Drawing.Size(686, 212);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "&Margins";
            // 
            // m_wrapTextAtDashes
            // 
            this.m_wrapTextAtDashes.AutoSize = true;
            this.m_wrapTextAtDashes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.m_wrapTextAtDashes.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_wrapTextAtDashes.Location = new System.Drawing.Point(22, 46);
            this.m_wrapTextAtDashes.Margin = new System.Windows.Forms.Padding(6);
            this.m_wrapTextAtDashes.Name = "m_wrapTextAtDashes";
            this.m_wrapTextAtDashes.Size = new System.Drawing.Size(263, 36);
            this.m_wrapTextAtDashes.TabIndex = 2;
            this.m_wrapTextAtDashes.Text = "Wrap Text at Dashes";
            this.m_wrapTextAtDashes.UseVisualStyleBackColor = true;
            // 
            // label4b
            // 
            this.label4b.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4b.AutoSize = true;
            this.label4b.Location = new System.Drawing.Point(259, 107);
            this.label4b.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4b.Name = "label4b";
            this.label4b.Size = new System.Drawing.Size(217, 32);
            this.label4b.TabIndex = 1;
            this.label4b.Text = "&Horizontal margins";
            // 
            // label4c
            // 
            this.label4c.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4c.AutoSize = true;
            this.label4c.Location = new System.Drawing.Point(292, 154);
            this.label4c.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4c.Name = "label4c";
            this.label4c.Size = new System.Drawing.Size(184, 32);
            this.label4c.TabIndex = 1;
            this.label4c.Text = "&Vertical margins";
            // 
            // m_documentSpecificMargins
            // 
            this.m_documentSpecificMargins.AutoSize = true;
            this.m_documentSpecificMargins.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.m_documentSpecificMargins.Location = new System.Drawing.Point(330, 46);
            this.m_documentSpecificMargins.Margin = new System.Windows.Forms.Padding(6);
            this.m_documentSpecificMargins.Name = "m_documentSpecificMargins";
            this.m_documentSpecificMargins.Size = new System.Drawing.Size(344, 36);
            this.m_documentSpecificMargins.TabIndex = 0;
            this.m_documentSpecificMargins.Text = "Document-Specific Margins";
            this.m_documentSpecificMargins.UseVisualStyleBackColor = true;
            this.m_documentSpecificMargins.CheckedChanged += new System.EventHandler(this.m_documentSpecificMargins_CheckedChanged);
            // 
            // m_documentHorizontalMargins
            // 
            this.m_documentHorizontalMargins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_documentHorizontalMargins.DecimalPlaces = 1;
            this.m_documentHorizontalMargins.Location = new System.Drawing.Point(484, 103);
            this.m_documentHorizontalMargins.Margin = new System.Windows.Forms.Padding(6);
            this.m_documentHorizontalMargins.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_documentHorizontalMargins.Name = "m_documentHorizontalMargins";
            this.m_documentHorizontalMargins.Size = new System.Drawing.Size(190, 39);
            this.m_documentHorizontalMargins.TabIndex = 4;
            this.m_documentHorizontalMargins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_documentHorizontalMargins.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // m_documentVerticalMargins
            // 
            this.m_documentVerticalMargins.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_documentVerticalMargins.DecimalPlaces = 1;
            this.m_documentVerticalMargins.Location = new System.Drawing.Point(484, 154);
            this.m_documentVerticalMargins.Margin = new System.Windows.Forms.Padding(6);
            this.m_documentVerticalMargins.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_documentVerticalMargins.Name = "m_documentVerticalMargins";
            this.m_documentVerticalMargins.Size = new System.Drawing.Size(190, 39);
            this.m_documentVerticalMargins.TabIndex = 4;
            this.m_documentVerticalMargins.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_documentVerticalMargins.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.m_snapToElementDistanceUpDown);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.m_handleSizeUpDown);
            this.groupBox2.Location = new System.Drawing.Point(10, 247);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6);
            this.groupBox2.Size = new System.Drawing.Size(686, 202);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Advanced";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(50, 40);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(607, 32);
            this.label7.TabIndex = 0;
            this.label7.Text = "These settings adjust the ease of clicking and dragging.";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 146);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(289, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "&Snap to Element Distance";
            // 
            // m_snapToElementDistanceUpDown
            // 
            this.m_snapToElementDistanceUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_snapToElementDistanceUpDown.DecimalPlaces = 1;
            this.m_snapToElementDistanceUpDown.Location = new System.Drawing.Point(484, 142);
            this.m_snapToElementDistanceUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_snapToElementDistanceUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_snapToElementDistanceUpDown.Name = "m_snapToElementDistanceUpDown";
            this.m_snapToElementDistanceUpDown.Size = new System.Drawing.Size(190, 39);
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
            this.label2.Location = new System.Drawing.Point(198, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Resize/Drag &Handle Size";
            // 
            // m_handleSizeUpDown
            // 
            this.m_handleSizeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_handleSizeUpDown.DecimalPlaces = 1;
            this.m_handleSizeUpDown.Location = new System.Drawing.Point(484, 91);
            this.m_handleSizeUpDown.Margin = new System.Windows.Forms.Padding(6);
            this.m_handleSizeUpDown.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_handleSizeUpDown.Name = "m_handleSizeUpDown";
            this.m_handleSizeUpDown.Size = new System.Drawing.Size(190, 39);
            this.m_handleSizeUpDown.TabIndex = 2;
            this.m_handleSizeUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_handleSizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(766, 844);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(20);
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
            this.tabPage4.PerformLayout();
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_documentHorizontalMargins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_documentVerticalMargins)).EndInit();
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
    private System.Windows.Forms.TextBox txtDefaultRoomName;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.ComboBox cboRoomShape;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.TextBox m_subtitleFontSizeTextBox;
    private System.Windows.Forms.Button m_changeSubtitleFontButton;
    private System.Windows.Forms.TextBox m_subtitleFontNameTextBox;
        private System.Windows.Forms.CheckBox m_wrapTextAtDashes;
  }
}
