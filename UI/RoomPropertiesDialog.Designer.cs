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

using Trizbort.UI.Controls;

namespace Trizbort.UI
{
    partial class RoomPropertiesDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomPropertiesDialog));
      this.m_okButton = new System.Windows.Forms.Button();
      this.m_cancelButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.m_isDarkCheckBox = new System.Windows.Forms.CheckBox();
      this.m_descriptionTextBox = new System.Windows.Forms.TextBox();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.m_changeSecondFillButton = new System.Windows.Forms.Button();
      this.label4 = new System.Windows.Forms.Label();
      this.m_changeObjectTextButton = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.m_changeRoomTextButton = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.m_changeRoomBorderButton = new System.Windows.Forms.Button();
      this.label11 = new System.Windows.Forms.Label();
      this.m_changeRoomFillButton = new System.Windows.Forms.Button();
      this.cboRegion = new System.Windows.Forms.ComboBox();
      this.label6 = new System.Windows.Forms.Label();
      this.subtitleColorClear = new System.Windows.Forms.Button();
      this.objectColorClear = new System.Windows.Forms.Button();
      this.roomTextColorClear = new System.Windows.Forms.Button();
      this.roomBorderColorClear = new System.Windows.Forms.Button();
      this.secondColorClear = new System.Windows.Forms.Button();
      this.roomColorClear = new System.Windows.Forms.Button();
      this.m_subTitleTextTextBox = new Trizbort.UI.Controls.TrizbortTextBox();
      this.label14 = new System.Windows.Forms.Label();
      this.m_changeSubtitleTextButton = new System.Windows.Forms.Button();
      this.m_secondFillTextBox = new Trizbort.UI.Controls.TrizbortTextBox();
      this.m_objectTextTextBox = new Trizbort.UI.Controls.TrizbortTextBox();
      this.m_roomTextTextBox = new Trizbort.UI.Controls.TrizbortTextBox();
      this.m_roomBorderTextBox = new Trizbort.UI.Controls.TrizbortTextBox();
      this.m_roomFillTextBox = new Trizbort.UI.Controls.TrizbortTextBox();
      this.txtRight = new System.Windows.Forms.NumericUpDown();
      this.txtDown = new System.Windows.Forms.NumericUpDown();
      this.label12 = new System.Windows.Forms.Label();
      this.label10 = new System.Windows.Forms.Label();
      this.chkCustomPosition = new System.Windows.Forms.CheckBox();
      this.pnlObjectSyntaxHelp = new System.Windows.Forms.Panel();
      this.label9 = new System.Windows.Forms.Label();
      this.lblObjectSyntaxHelp = new System.Windows.Forms.Label();
      this.txtObjects = new Trizbort.UI.Controls.TrizbortTextBox();
      this.m_objectsPositionGroupBox = new System.Windows.Forms.GroupBox();
      this.m_cCheckBox = new System.Windows.Forms.CheckBox();
      this.m_nwCheckBox = new System.Windows.Forms.CheckBox();
      this.m_seCheckBox = new System.Windows.Forms.CheckBox();
      this.m_nCheckBox = new System.Windows.Forms.CheckBox();
      this.m_sCheckBox = new System.Windows.Forms.CheckBox();
      this.m_neCheckBox = new System.Windows.Forms.CheckBox();
      this.m_swCheckBox = new System.Windows.Forms.CheckBox();
      this.m_wCheckBox = new System.Windows.Forms.CheckBox();
      this.m_eCheckBox = new System.Windows.Forms.CheckBox();
      this.chkHandDrawnRoom = new System.Windows.Forms.CheckBox();
      this.groupRoundedCorners = new System.Windows.Forms.Panel();
      this.chkCornersSame = new System.Windows.Forms.CheckBox();
      this.txtBottomRight = new System.Windows.Forms.NumericUpDown();
      this.txtTopLeft = new System.Windows.Forms.NumericUpDown();
      this.txtBottomLeft = new System.Windows.Forms.NumericUpDown();
      this.txtTopRight = new System.Windows.Forms.NumericUpDown();
      this.cboDrawType = new System.Windows.Forms.ComboBox();
      this.pnlSampleRoomShape = new System.Windows.Forms.Panel();
      this.txtName = new Trizbort.UI.Controls.TrizbortTextBox();
      this.txtSubTitle = new Trizbort.UI.Controls.TrizbortTextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.cboBorderStyle = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.chkStartRoom = new System.Windows.Forms.CheckBox();
      this.toolTip = new Trizbort.UI.Controls.TrizbortToolTip();
      this.chkEndRoom = new System.Windows.Forms.CheckBox();
      this.cboReference = new System.Windows.Forms.ComboBox();
      this.label13 = new System.Windows.Forms.Label();
      this.m_tabControl = new System.Windows.Forms.TabControl();
      this.tabDescription = new System.Windows.Forms.TabPage();
      this.tabObjects = new System.Windows.Forms.TabPage();
      this.tabColors = new System.Windows.Forms.TabPage();
      this.tabRegions = new System.Windows.Forms.TabPage();
      this.tabRoomShapes = new System.Windows.Forms.TabPage();
      ((System.ComponentModel.ISupportInitialize)(this.txtRight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtDown)).BeginInit();
      this.pnlObjectSyntaxHelp.SuspendLayout();
      this.m_objectsPositionGroupBox.SuspendLayout();
      this.groupRoundedCorners.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtBottomRight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTopLeft)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtBottomLeft)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTopRight)).BeginInit();
      this.m_tabControl.SuspendLayout();
      this.tabDescription.SuspendLayout();
      this.tabObjects.SuspendLayout();
      this.tabColors.SuspendLayout();
      this.tabRegions.SuspendLayout();
      this.tabRoomShapes.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_okButton
      // 
      this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_okButton.Location = new System.Drawing.Point(344, 389);
      this.m_okButton.Name = "m_okButton";
      this.m_okButton.Size = new System.Drawing.Size(75, 23);
      this.m_okButton.TabIndex = 97;
      this.m_okButton.Text = "O&K";
      this.m_okButton.UseVisualStyleBackColor = true;
      this.m_okButton.Click += new System.EventHandler(this.m_okButton_Click);
      // 
      // m_cancelButton
      // 
      this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_cancelButton.Location = new System.Drawing.Point(425, 389);
      this.m_cancelButton.Name = "m_cancelButton";
      this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
      this.m_cancelButton.TabIndex = 98;
      this.m_cancelButton.Text = "Cancel";
      this.m_cancelButton.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(34, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "&Name";
      // 
      // m_isDarkCheckBox
      // 
      this.m_isDarkCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_isDarkCheckBox.AutoSize = true;
      this.m_isDarkCheckBox.Location = new System.Drawing.Point(439, 99);
      this.m_isDarkCheckBox.Name = "m_isDarkCheckBox";
      this.m_isDarkCheckBox.Size = new System.Drawing.Size(48, 17);
      this.m_isDarkCheckBox.TabIndex = 13;
      this.m_isDarkCheckBox.Text = "&Dark";
      this.m_isDarkCheckBox.UseVisualStyleBackColor = true;
      // 
      // m_descriptionTextBox
      // 
      this.m_descriptionTextBox.AcceptsReturn = true;
      this.m_descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_descriptionTextBox.Location = new System.Drawing.Point(3, 3);
      this.m_descriptionTextBox.Multiline = true;
      this.m_descriptionTextBox.Name = "m_descriptionTextBox";
      this.m_descriptionTextBox.Size = new System.Drawing.Size(474, 184);
      this.m_descriptionTextBox.TabIndex = 4;
      this.m_descriptionTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_descriptionTextBox_KeyDown);
      // 
      // comboBox1
      // 
      this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
      this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] {
            "Bottom",
            "BottomRight",
            "BottomLeft",
            "Left",
            "Right",
            "TopRight",
            "TopLeft",
            "Top"});
      this.comboBox1.Location = new System.Drawing.Point(82, 35);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(121, 21);
      this.comboBox1.TabIndex = 15;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.BackColor = System.Drawing.Color.Transparent;
      this.label5.Location = new System.Drawing.Point(7, 38);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(57, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "&Second Fill";
      // 
      // m_changeSecondFillButton
      // 
      this.m_changeSecondFillButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeSecondFillButton.Location = new System.Drawing.Point(397, 35);
      this.m_changeSecondFillButton.Name = "m_changeSecondFillButton";
      this.m_changeSecondFillButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeSecondFillButton.TabIndex = 16;
      this.m_changeSecondFillButton.Text = "Change...";
      this.m_changeSecondFillButton.UseVisualStyleBackColor = true;
      this.m_changeSecondFillButton.Click += new System.EventHandler(this.m_changeSecondFillButton_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.BackColor = System.Drawing.Color.Transparent;
      this.label4.Location = new System.Drawing.Point(7, 125);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(64, 13);
      this.label4.TabIndex = 19;
      this.label4.Text = "Ob&ject Text";
      // 
      // m_changeObjectTextButton
      // 
      this.m_changeObjectTextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeObjectTextButton.Location = new System.Drawing.Point(397, 122);
      this.m_changeObjectTextButton.Name = "m_changeObjectTextButton";
      this.m_changeObjectTextButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeObjectTextButton.TabIndex = 19;
      this.m_changeObjectTextButton.Text = "Change...";
      this.m_changeObjectTextButton.UseVisualStyleBackColor = true;
      this.m_changeObjectTextButton.Click += new System.EventHandler(this.button3_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.BackColor = System.Drawing.Color.Transparent;
      this.label3.Location = new System.Drawing.Point(7, 96);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(59, 13);
      this.label3.TabIndex = 16;
      this.label3.Text = "Room &Text";
      // 
      // m_changeRoomTextButton
      // 
      this.m_changeRoomTextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeRoomTextButton.Location = new System.Drawing.Point(397, 93);
      this.m_changeRoomTextButton.Name = "m_changeRoomTextButton";
      this.m_changeRoomTextButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeRoomTextButton.TabIndex = 18;
      this.m_changeRoomTextButton.Text = "Change...";
      this.m_changeRoomTextButton.UseVisualStyleBackColor = true;
      this.m_changeRoomTextButton.Click += new System.EventHandler(this.button2_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.BackColor = System.Drawing.Color.Transparent;
      this.label2.Location = new System.Drawing.Point(7, 67);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(69, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "Room &Border";
      // 
      // m_changeRoomBorderButton
      // 
      this.m_changeRoomBorderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeRoomBorderButton.Location = new System.Drawing.Point(397, 64);
      this.m_changeRoomBorderButton.Name = "m_changeRoomBorderButton";
      this.m_changeRoomBorderButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeRoomBorderButton.TabIndex = 17;
      this.m_changeRoomBorderButton.Text = "Change...";
      this.m_changeRoomBorderButton.UseVisualStyleBackColor = true;
      this.m_changeRoomBorderButton.Click += new System.EventHandler(this.button1_Click);
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.BackColor = System.Drawing.Color.Transparent;
      this.label11.Location = new System.Drawing.Point(7, 9);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(49, 13);
      this.label11.TabIndex = 10;
      this.label11.Text = "Room &Fill";
      // 
      // m_changeRoomFillButton
      // 
      this.m_changeRoomFillButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeRoomFillButton.Location = new System.Drawing.Point(397, 6);
      this.m_changeRoomFillButton.Name = "m_changeRoomFillButton";
      this.m_changeRoomFillButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeRoomFillButton.TabIndex = 14;
      this.m_changeRoomFillButton.Text = "Change...";
      this.m_changeRoomFillButton.UseVisualStyleBackColor = true;
      this.m_changeRoomFillButton.Click += new System.EventHandler(this.m_changeLargeFontButton_Click);
      // 
      // cboRegion
      // 
      this.cboRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboRegion.FormattingEnabled = true;
      this.cboRegion.Items.AddRange(new object[] {
            "Bottom",
            "BottomRight",
            "Right",
            "TopRight"});
      this.cboRegion.Location = new System.Drawing.Point(69, 17);
      this.cboRegion.Name = "cboRegion";
      this.cboRegion.Size = new System.Drawing.Size(186, 21);
      this.cboRegion.TabIndex = 7;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.BackColor = System.Drawing.Color.Transparent;
      this.label6.Location = new System.Drawing.Point(16, 20);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(47, 13);
      this.label6.TabIndex = 21;
      this.label6.Text = "Region: ";
      // 
      // subtitleColorClear
      // 
      this.subtitleColorClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.subtitleColorClear.Image = ((System.Drawing.Image)(resources.GetObject("subtitleColorClear.Image")));
      this.subtitleColorClear.Location = new System.Drawing.Point(366, 151);
      this.subtitleColorClear.Name = "subtitleColorClear";
      this.subtitleColorClear.Size = new System.Drawing.Size(28, 23);
      this.subtitleColorClear.TabIndex = 34;
      this.subtitleColorClear.Text = "...";
      this.subtitleColorClear.UseVisualStyleBackColor = true;
      this.subtitleColorClear.Click += new System.EventHandler(this.m_subTitleTextTextBox_ButtonCustomClick);
      // 
      // objectColorClear
      // 
      this.objectColorClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.objectColorClear.Image = ((System.Drawing.Image)(resources.GetObject("objectColorClear.Image")));
      this.objectColorClear.Location = new System.Drawing.Point(366, 122);
      this.objectColorClear.Name = "objectColorClear";
      this.objectColorClear.Size = new System.Drawing.Size(28, 23);
      this.objectColorClear.TabIndex = 33;
      this.objectColorClear.Text = "...";
      this.objectColorClear.UseVisualStyleBackColor = true;
      this.objectColorClear.Click += new System.EventHandler(this.m_objectTextTextBox_ButtonCustomClick);
      // 
      // roomTextColorClear
      // 
      this.roomTextColorClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.roomTextColorClear.Image = ((System.Drawing.Image)(resources.GetObject("roomTextColorClear.Image")));
      this.roomTextColorClear.Location = new System.Drawing.Point(366, 93);
      this.roomTextColorClear.Name = "roomTextColorClear";
      this.roomTextColorClear.Size = new System.Drawing.Size(28, 23);
      this.roomTextColorClear.TabIndex = 32;
      this.roomTextColorClear.Text = "...";
      this.roomTextColorClear.UseVisualStyleBackColor = true;
      this.roomTextColorClear.Click += new System.EventHandler(this.m_roomTextTextBox_ButtonCustomClick);
      // 
      // roomBorderColorClear
      // 
      this.roomBorderColorClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.roomBorderColorClear.Image = ((System.Drawing.Image)(resources.GetObject("roomBorderColorClear.Image")));
      this.roomBorderColorClear.Location = new System.Drawing.Point(366, 64);
      this.roomBorderColorClear.Name = "roomBorderColorClear";
      this.roomBorderColorClear.Size = new System.Drawing.Size(28, 23);
      this.roomBorderColorClear.TabIndex = 31;
      this.roomBorderColorClear.Text = "...";
      this.roomBorderColorClear.UseVisualStyleBackColor = true;
      this.roomBorderColorClear.Click += new System.EventHandler(this.m_roomBorderTextBox_ButtonCustomClick);
      // 
      // secondColorClear
      // 
      this.secondColorClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.secondColorClear.Image = ((System.Drawing.Image)(resources.GetObject("secondColorClear.Image")));
      this.secondColorClear.Location = new System.Drawing.Point(366, 35);
      this.secondColorClear.Name = "secondColorClear";
      this.secondColorClear.Size = new System.Drawing.Size(28, 23);
      this.secondColorClear.TabIndex = 30;
      this.secondColorClear.Text = "...";
      this.secondColorClear.UseVisualStyleBackColor = true;
      this.secondColorClear.Click += new System.EventHandler(this.m_secondFillTextBox_ButtonCustomClick);
      // 
      // roomColorClear
      // 
      this.roomColorClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.roomColorClear.Image = ((System.Drawing.Image)(resources.GetObject("roomColorClear.Image")));
      this.roomColorClear.Location = new System.Drawing.Point(366, 6);
      this.roomColorClear.Name = "roomColorClear";
      this.roomColorClear.Size = new System.Drawing.Size(28, 23);
      this.roomColorClear.TabIndex = 29;
      this.roomColorClear.Text = "...";
      this.roomColorClear.UseVisualStyleBackColor = true;
      this.roomColorClear.Click += new System.EventHandler(this.m_roomFillTextBox_ButtonCustomClick);
      // 
      // m_subTitleTextTextBox
      // 
      this.m_subTitleTextTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_subTitleTextTextBox.Location = new System.Drawing.Point(82, 151);
      this.m_subTitleTextTextBox.Name = "m_subTitleTextTextBox";
      this.m_subTitleTextTextBox.ReadOnly = true;
      this.m_subTitleTextTextBox.Size = new System.Drawing.Size(278, 21);
      this.m_subTitleTextTextBox.TabIndex = 28;
      this.m_subTitleTextTextBox.Watermark = null;
      this.m_subTitleTextTextBox.DoubleClick += new System.EventHandler(this.m_subTitleTextTextBox_DoubleClick);
      this.m_subTitleTextTextBox.Enter += new System.EventHandler(this.m_subTitleTextTextBox_Enter);
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.BackColor = System.Drawing.Color.Transparent;
      this.label14.Location = new System.Drawing.Point(7, 154);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(68, 13);
      this.label14.TabIndex = 26;
      this.label14.Text = "Su&btitle Text";
      // 
      // m_changeSubtitleTextButton
      // 
      this.m_changeSubtitleTextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeSubtitleTextButton.Location = new System.Drawing.Point(397, 151);
      this.m_changeSubtitleTextButton.Name = "m_changeSubtitleTextButton";
      this.m_changeSubtitleTextButton.Size = new System.Drawing.Size(75, 23);
      this.m_changeSubtitleTextButton.TabIndex = 27;
      this.m_changeSubtitleTextButton.Text = "Change...";
      this.m_changeSubtitleTextButton.UseVisualStyleBackColor = true;
      this.m_changeSubtitleTextButton.Click += new System.EventHandler(this.m_changeSubtitleTextButton_Click);
      // 
      // m_secondFillTextBox
      // 
      this.m_secondFillTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_secondFillTextBox.Location = new System.Drawing.Point(209, 35);
      this.m_secondFillTextBox.Name = "m_secondFillTextBox";
      this.m_secondFillTextBox.ReadOnly = true;
      this.m_secondFillTextBox.Size = new System.Drawing.Size(151, 21);
      this.m_secondFillTextBox.TabIndex = 25;
      this.m_secondFillTextBox.Watermark = null;
      this.m_secondFillTextBox.DoubleClick += new System.EventHandler(this.m_secondFillTextBox_DoubleClick);
      this.m_secondFillTextBox.Enter += new System.EventHandler(this.m_secondFillTextBox_Enter);
      // 
      // m_objectTextTextBox
      // 
      this.m_objectTextTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_objectTextTextBox.Location = new System.Drawing.Point(82, 122);
      this.m_objectTextTextBox.Name = "m_objectTextTextBox";
      this.m_objectTextTextBox.ReadOnly = true;
      this.m_objectTextTextBox.Size = new System.Drawing.Size(278, 21);
      this.m_objectTextTextBox.TabIndex = 24;
      this.m_objectTextTextBox.Watermark = null;
      this.m_objectTextTextBox.DoubleClick += new System.EventHandler(this.m_objectTextTextBox_DoubleClick);
      this.m_objectTextTextBox.Enter += new System.EventHandler(this.m_objectTextTextBox_Enter);
      // 
      // m_roomTextTextBox
      // 
      this.m_roomTextTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_roomTextTextBox.Location = new System.Drawing.Point(82, 93);
      this.m_roomTextTextBox.Name = "m_roomTextTextBox";
      this.m_roomTextTextBox.ReadOnly = true;
      this.m_roomTextTextBox.Size = new System.Drawing.Size(278, 21);
      this.m_roomTextTextBox.TabIndex = 23;
      this.m_roomTextTextBox.Watermark = null;
      this.m_roomTextTextBox.DoubleClick += new System.EventHandler(this.m_roomTextTextBox_DoubleClick);
      this.m_roomTextTextBox.Enter += new System.EventHandler(this.m_roomTextTextBox_Enter);
      // 
      // m_roomBorderTextBox
      // 
      this.m_roomBorderTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_roomBorderTextBox.Location = new System.Drawing.Point(82, 64);
      this.m_roomBorderTextBox.Name = "m_roomBorderTextBox";
      this.m_roomBorderTextBox.ReadOnly = true;
      this.m_roomBorderTextBox.Size = new System.Drawing.Size(278, 21);
      this.m_roomBorderTextBox.TabIndex = 22;
      this.m_roomBorderTextBox.Watermark = null;
      this.m_roomBorderTextBox.DoubleClick += new System.EventHandler(this.m_roomBorderTextBox_DoubleClick);
      this.m_roomBorderTextBox.Enter += new System.EventHandler(this.m_roomBorderTextBox_Enter);
      // 
      // m_roomFillTextBox
      // 
      this.m_roomFillTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_roomFillTextBox.Location = new System.Drawing.Point(82, 6);
      this.m_roomFillTextBox.Name = "m_roomFillTextBox";
      this.m_roomFillTextBox.ReadOnly = true;
      this.m_roomFillTextBox.Size = new System.Drawing.Size(278, 21);
      this.m_roomFillTextBox.TabIndex = 6;
      this.m_roomFillTextBox.Watermark = null;
      this.m_roomFillTextBox.DoubleClick += new System.EventHandler(this.m_roomFillTextBox_DoubleClick);
      this.m_roomFillTextBox.Enter += new System.EventHandler(this.m_roomFillTextBox_Enter);
      // 
      // txtRight
      // 
      this.txtRight.Location = new System.Drawing.Point(418, 163);
      this.txtRight.Name = "txtRight";
      this.txtRight.Size = new System.Drawing.Size(55, 21);
      this.txtRight.TabIndex = 25;
      // 
      // txtDown
      // 
      this.txtDown.Location = new System.Drawing.Point(418, 141);
      this.txtDown.Name = "txtDown";
      this.txtDown.Size = new System.Drawing.Size(55, 21);
      this.txtDown.TabIndex = 24;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.BackColor = System.Drawing.Color.Transparent;
      this.label12.Location = new System.Drawing.Point(376, 166);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(36, 13);
      this.label12.TabIndex = 23;
      this.label12.Text = "Right:";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.BackColor = System.Drawing.Color.Transparent;
      this.label10.Location = new System.Drawing.Point(374, 147);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(38, 13);
      this.label10.TabIndex = 22;
      this.label10.Text = "Down:";
      // 
      // chkCustomPosition
      // 
      this.chkCustomPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.chkCustomPosition.AutoSize = true;
      this.chkCustomPosition.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.chkCustomPosition.Location = new System.Drawing.Point(370, 125);
      this.chkCustomPosition.Name = "chkCustomPosition";
      this.chkCustomPosition.Size = new System.Drawing.Size(102, 17);
      this.chkCustomPosition.TabIndex = 21;
      this.chkCustomPosition.Text = "Custom Position";
      this.chkCustomPosition.UseVisualStyleBackColor = true;
      // 
      // pnlObjectSyntaxHelp
      // 
      this.pnlObjectSyntaxHelp.BackColor = System.Drawing.Color.LightCyan;
      this.pnlObjectSyntaxHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pnlObjectSyntaxHelp.Controls.Add(this.label9);
      this.pnlObjectSyntaxHelp.Location = new System.Drawing.Point(212, 27);
      this.pnlObjectSyntaxHelp.Name = "pnlObjectSyntaxHelp";
      this.pnlObjectSyntaxHelp.Size = new System.Drawing.Size(154, 157);
      this.pnlObjectSyntaxHelp.TabIndex = 20;
      this.pnlObjectSyntaxHelp.Visible = false;
      // 
      // label9
      // 
      this.label9.Location = new System.Drawing.Point(6, 3);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(143, 143);
      this.label9.TabIndex = 3;
      this.label9.Text = resources.GetString("label9.Text");
      // 
      // lblObjectSyntaxHelp
      // 
      this.lblObjectSyntaxHelp.AutoSize = true;
      this.lblObjectSyntaxHelp.Cursor = System.Windows.Forms.Cursors.Hand;
      this.lblObjectSyntaxHelp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblObjectSyntaxHelp.ForeColor = System.Drawing.Color.Blue;
      this.lblObjectSyntaxHelp.Location = new System.Drawing.Point(8, 11);
      this.lblObjectSyntaxHelp.Name = "lblObjectSyntaxHelp";
      this.lblObjectSyntaxHelp.Size = new System.Drawing.Size(76, 13);
      this.lblObjectSyntaxHelp.TabIndex = 19;
      this.lblObjectSyntaxHelp.Text = "Object Syntax";
      this.lblObjectSyntaxHelp.Click += new System.EventHandler(this.lblObjectSyntaxHelp_Click);
      // 
      // txtObjects
      // 
      this.txtObjects.AcceptsReturn = true;
      this.txtObjects.Location = new System.Drawing.Point(7, 27);
      this.txtObjects.Multiline = true;
      this.txtObjects.Name = "txtObjects";
      this.txtObjects.Size = new System.Drawing.Size(359, 157);
      this.txtObjects.TabIndex = 5;
      this.txtObjects.Watermark = "Enter objects, each on a new line.";
      this.txtObjects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtObjects_KeyDown);
      // 
      // m_objectsPositionGroupBox
      // 
      this.m_objectsPositionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_objectsPositionGroupBox.BackColor = System.Drawing.Color.Transparent;
      this.m_objectsPositionGroupBox.Controls.Add(this.m_cCheckBox);
      this.m_objectsPositionGroupBox.Controls.Add(this.m_nwCheckBox);
      this.m_objectsPositionGroupBox.Controls.Add(this.m_seCheckBox);
      this.m_objectsPositionGroupBox.Controls.Add(this.m_nCheckBox);
      this.m_objectsPositionGroupBox.Controls.Add(this.m_sCheckBox);
      this.m_objectsPositionGroupBox.Controls.Add(this.m_neCheckBox);
      this.m_objectsPositionGroupBox.Controls.Add(this.m_swCheckBox);
      this.m_objectsPositionGroupBox.Controls.Add(this.m_wCheckBox);
      this.m_objectsPositionGroupBox.Controls.Add(this.m_eCheckBox);
      this.m_objectsPositionGroupBox.Location = new System.Drawing.Point(376, 7);
      this.m_objectsPositionGroupBox.Name = "m_objectsPositionGroupBox";
      this.m_objectsPositionGroupBox.Size = new System.Drawing.Size(102, 114);
      this.m_objectsPositionGroupBox.TabIndex = 18;
      this.m_objectsPositionGroupBox.TabStop = false;
      this.m_objectsPositionGroupBox.Text = "&Position";
      // 
      // m_cCheckBox
      // 
      this.m_cCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_cCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_cCheckBox.Location = new System.Drawing.Point(38, 48);
      this.m_cCheckBox.Name = "m_cCheckBox";
      this.m_cCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_cCheckBox.TabIndex = 12;
      this.m_cCheckBox.Tag = "Position";
      this.m_cCheckBox.Text = "o";
      this.m_cCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_cCheckBox.UseVisualStyleBackColor = true;
      this.m_cCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // m_nwCheckBox
      // 
      this.m_nwCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_nwCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_nwCheckBox.Location = new System.Drawing.Point(6, 16);
      this.m_nwCheckBox.Name = "m_nwCheckBox";
      this.m_nwCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_nwCheckBox.TabIndex = 8;
      this.m_nwCheckBox.Tag = "Position";
      this.m_nwCheckBox.Text = "ã";
      this.m_nwCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_nwCheckBox.UseVisualStyleBackColor = true;
      this.m_nwCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // m_seCheckBox
      // 
      this.m_seCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_seCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_seCheckBox.Location = new System.Drawing.Point(70, 80);
      this.m_seCheckBox.Name = "m_seCheckBox";
      this.m_seCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_seCheckBox.TabIndex = 16;
      this.m_seCheckBox.Tag = "Position";
      this.m_seCheckBox.Text = "æ";
      this.m_seCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_seCheckBox.UseVisualStyleBackColor = true;
      this.m_seCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // m_nCheckBox
      // 
      this.m_nCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_nCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_nCheckBox.Location = new System.Drawing.Point(38, 16);
      this.m_nCheckBox.Name = "m_nCheckBox";
      this.m_nCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_nCheckBox.TabIndex = 9;
      this.m_nCheckBox.Tag = "Position";
      this.m_nCheckBox.Text = "á";
      this.m_nCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_nCheckBox.UseVisualStyleBackColor = true;
      this.m_nCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // m_sCheckBox
      // 
      this.m_sCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_sCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_sCheckBox.Location = new System.Drawing.Point(38, 80);
      this.m_sCheckBox.Name = "m_sCheckBox";
      this.m_sCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_sCheckBox.TabIndex = 15;
      this.m_sCheckBox.Tag = "Position";
      this.m_sCheckBox.Text = "â";
      this.m_sCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_sCheckBox.UseVisualStyleBackColor = true;
      this.m_sCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // m_neCheckBox
      // 
      this.m_neCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_neCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_neCheckBox.Location = new System.Drawing.Point(70, 16);
      this.m_neCheckBox.Name = "m_neCheckBox";
      this.m_neCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_neCheckBox.TabIndex = 10;
      this.m_neCheckBox.Tag = "Position";
      this.m_neCheckBox.Text = "ä";
      this.m_neCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_neCheckBox.UseVisualStyleBackColor = true;
      this.m_neCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // m_swCheckBox
      // 
      this.m_swCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_swCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_swCheckBox.Location = new System.Drawing.Point(6, 80);
      this.m_swCheckBox.Name = "m_swCheckBox";
      this.m_swCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_swCheckBox.TabIndex = 14;
      this.m_swCheckBox.Tag = "Position";
      this.m_swCheckBox.Text = "å";
      this.m_swCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_swCheckBox.UseVisualStyleBackColor = true;
      this.m_swCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // m_wCheckBox
      // 
      this.m_wCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_wCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_wCheckBox.Location = new System.Drawing.Point(6, 48);
      this.m_wCheckBox.Name = "m_wCheckBox";
      this.m_wCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_wCheckBox.TabIndex = 11;
      this.m_wCheckBox.Tag = "Position";
      this.m_wCheckBox.Text = "ß";
      this.m_wCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_wCheckBox.UseVisualStyleBackColor = true;
      this.m_wCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // m_eCheckBox
      // 
      this.m_eCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
      this.m_eCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
      this.m_eCheckBox.Location = new System.Drawing.Point(70, 48);
      this.m_eCheckBox.Name = "m_eCheckBox";
      this.m_eCheckBox.Size = new System.Drawing.Size(26, 26);
      this.m_eCheckBox.TabIndex = 13;
      this.m_eCheckBox.Tag = "Position";
      this.m_eCheckBox.Text = "à";
      this.m_eCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.m_eCheckBox.UseVisualStyleBackColor = true;
      this.m_eCheckBox.CheckedChanged += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
      // 
      // chkHandDrawnRoom
      // 
      this.chkHandDrawnRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.chkHandDrawnRoom.AutoSize = true;
      this.chkHandDrawnRoom.Location = new System.Drawing.Point(220, 18);
      this.chkHandDrawnRoom.Name = "chkHandDrawnRoom";
      this.chkHandDrawnRoom.Size = new System.Drawing.Size(117, 17);
      this.chkHandDrawnRoom.TabIndex = 8;
      this.chkHandDrawnRoom.Text = "Hand Drawn Edges";
      this.chkHandDrawnRoom.UseVisualStyleBackColor = true;
      this.chkHandDrawnRoom.CheckedChanged += new System.EventHandler(this.chkHandDrawnRoom_CheckedChanged);
      // 
      // groupRoundedCorners
      // 
      this.groupRoundedCorners.BackColor = System.Drawing.SystemColors.Control;
      this.groupRoundedCorners.Controls.Add(this.chkCornersSame);
      this.groupRoundedCorners.Controls.Add(this.txtBottomRight);
      this.groupRoundedCorners.Controls.Add(this.txtTopLeft);
      this.groupRoundedCorners.Controls.Add(this.txtBottomLeft);
      this.groupRoundedCorners.Controls.Add(this.txtTopRight);
      this.groupRoundedCorners.Location = new System.Drawing.Point(6, 42);
      this.groupRoundedCorners.Name = "groupRoundedCorners";
      this.groupRoundedCorners.Size = new System.Drawing.Size(208, 126);
      this.groupRoundedCorners.TabIndex = 6;
      this.groupRoundedCorners.Text = "Rounded Corners";
      this.groupRoundedCorners.Visible = false;
      // 
      // chkCornersSame
      // 
      this.chkCornersSame.AutoSize = true;
      this.chkCornersSame.Checked = true;
      this.chkCornersSame.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCornersSame.Location = new System.Drawing.Point(3, 8);
      this.chkCornersSame.Name = "chkCornersSame";
      this.chkCornersSame.Size = new System.Drawing.Size(132, 17);
      this.chkCornersSame.TabIndex = 4;
      this.chkCornersSame.Text = "Make all corners equal";
      this.chkCornersSame.UseVisualStyleBackColor = true;
      this.chkCornersSame.CheckedChanged += new System.EventHandler(this.chkCornersSame_CheckedChanged);
      // 
      // txtBottomRight
      // 
      this.txtBottomRight.DecimalPlaces = 2;
      this.txtBottomRight.Enabled = false;
      this.txtBottomRight.Location = new System.Drawing.Point(117, 75);
      this.txtBottomRight.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.txtBottomRight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.txtBottomRight.Name = "txtBottomRight";
      this.txtBottomRight.Size = new System.Drawing.Size(80, 21);
      this.txtBottomRight.TabIndex = 3;
      this.txtBottomRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtBottomRight.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
      this.txtBottomRight.ValueChanged += new System.EventHandler(this.redrawSampleOnChange);
      // 
      // txtTopLeft
      // 
      this.txtTopLeft.DecimalPlaces = 2;
      this.txtTopLeft.Location = new System.Drawing.Point(3, 30);
      this.txtTopLeft.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.txtTopLeft.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.txtTopLeft.Name = "txtTopLeft";
      this.txtTopLeft.Size = new System.Drawing.Size(80, 21);
      this.txtTopLeft.TabIndex = 0;
      this.txtTopLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtTopLeft.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
      this.txtTopLeft.ValueChanged += new System.EventHandler(this.redrawSampleOnChange);
      // 
      // txtBottomLeft
      // 
      this.txtBottomLeft.DecimalPlaces = 2;
      this.txtBottomLeft.Enabled = false;
      this.txtBottomLeft.Location = new System.Drawing.Point(3, 75);
      this.txtBottomLeft.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.txtBottomLeft.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.txtBottomLeft.Name = "txtBottomLeft";
      this.txtBottomLeft.Size = new System.Drawing.Size(80, 21);
      this.txtBottomLeft.TabIndex = 1;
      this.txtBottomLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtBottomLeft.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
      this.txtBottomLeft.ValueChanged += new System.EventHandler(this.redrawSampleOnChange);
      // 
      // txtTopRight
      // 
      this.txtTopRight.DecimalPlaces = 2;
      this.txtTopRight.Enabled = false;
      this.txtTopRight.Location = new System.Drawing.Point(117, 30);
      this.txtTopRight.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
      this.txtTopRight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.txtTopRight.Name = "txtTopRight";
      this.txtTopRight.Size = new System.Drawing.Size(80, 21);
      this.txtTopRight.TabIndex = 2;
      this.txtTopRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtTopRight.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
      this.txtTopRight.ValueChanged += new System.EventHandler(this.redrawSampleOnChange);
      // 
      // cboDrawType
      // 
      this.cboDrawType.FormattingEnabled = true;
      this.cboDrawType.ItemHeight = 13;
      this.cboDrawType.Items.AddRange(new object[] {
            "Straight Edges",
            "Rounded Corners",
            "Ellipse",
            "Octagonal"});
      this.cboDrawType.Location = new System.Drawing.Point(6, 15);
      this.cboDrawType.Name = "cboDrawType";
      this.cboDrawType.Size = new System.Drawing.Size(203, 21);
      this.cboDrawType.TabIndex = 8;
      this.cboDrawType.SelectedIndexChanged += new System.EventHandler(this.cboDrawType_SelectedIndexChanged);
      // 
      // pnlSampleRoomShape
      // 
      this.pnlSampleRoomShape.Location = new System.Drawing.Point(220, 40);
      this.pnlSampleRoomShape.Name = "pnlSampleRoomShape";
      this.pnlSampleRoomShape.Size = new System.Drawing.Size(235, 128);
      this.pnlSampleRoomShape.TabIndex = 4;
      this.pnlSampleRoomShape.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSampleRoomShape_Paint);
      // 
      // txtName
      // 
      this.txtName.Location = new System.Drawing.Point(15, 28);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(474, 21);
      this.txtName.TabIndex = 1;
      this.txtName.Watermark = null;
      // 
      // txtSubTitle
      // 
      this.txtSubTitle.Location = new System.Drawing.Point(15, 68);
      this.txtSubTitle.Name = "txtSubTitle";
      this.txtSubTitle.Size = new System.Drawing.Size(426, 21);
      this.txtSubTitle.TabIndex = 3;
      this.txtSubTitle.Watermark = null;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(12, 52);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(43, 13);
      this.label7.TabIndex = 2;
      this.label7.Text = "S&ubtitle";
      // 
      // cboBorderStyle
      // 
      this.cboBorderStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboBorderStyle.FormattingEnabled = true;
      this.cboBorderStyle.Items.AddRange(new object[] {
            "Solid",
            "Dash",
            "DashDot",
            "DashDotDot",
            "Dot",
            "None"});
      this.cboBorderStyle.Location = new System.Drawing.Point(92, 95);
      this.cboBorderStyle.Name = "cboBorderStyle";
      this.cboBorderStyle.Size = new System.Drawing.Size(159, 21);
      this.cboBorderStyle.TabIndex = 10;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.BackColor = System.Drawing.Color.Transparent;
      this.label8.Location = new System.Drawing.Point(16, 98);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(70, 13);
      this.label8.TabIndex = 9;
      this.label8.Text = "Border St&yle:";
      // 
      // chkStartRoom
      // 
      this.chkStartRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.chkStartRoom.AutoSize = true;
      this.chkStartRoom.Location = new System.Drawing.Point(267, 98);
      this.chkStartRoom.Name = "chkStartRoom";
      this.chkStartRoom.Size = new System.Drawing.Size(80, 17);
      this.chkStartRoom.TabIndex = 11;
      this.chkStartRoom.Text = "Start Room";
      this.chkStartRoom.UseVisualStyleBackColor = true;
      this.chkStartRoom.CheckedChanged += new System.EventHandler(this.chkStartRoom_CheckedChanged);
      // 
      // toolTip
      // 
      this.toolTip.BackColor = System.Drawing.Color.LightBlue;
      this.toolTip.BodyText = null;
      this.toolTip.FooterText = null;
      this.toolTip.ForeColor = System.Drawing.Color.Black;
      this.toolTip.GradientColor = System.Drawing.Color.Empty;
      this.toolTip.OwnerDraw = true;
      this.toolTip.TitleText = null;
      // 
      // chkEndRoom
      // 
      this.chkEndRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.chkEndRoom.AutoSize = true;
      this.chkEndRoom.Location = new System.Drawing.Point(359, 99);
      this.chkEndRoom.Name = "chkEndRoom";
      this.chkEndRoom.Size = new System.Drawing.Size(74, 17);
      this.chkEndRoom.TabIndex = 12;
      this.chkEndRoom.Text = "End Room";
      this.chkEndRoom.UseVisualStyleBackColor = true;
      // 
      // cboReference
      // 
      this.cboReference.DisplayMember = "Name";
      this.cboReference.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboReference.FormattingEnabled = true;
      this.cboReference.Location = new System.Drawing.Point(92, 131);
      this.cboReference.Name = "cboReference";
      this.cboReference.Size = new System.Drawing.Size(159, 21);
      this.cboReference.TabIndex = 100;
      this.cboReference.ValueMember = "ID";
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.BackColor = System.Drawing.Color.Transparent;
      this.label13.Location = new System.Drawing.Point(24, 134);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(61, 13);
      this.label13.TabIndex = 99;
      this.label13.Text = "Reference:";
      // 
      // m_tabControl
      // 
      this.m_tabControl.Controls.Add(this.tabDescription);
      this.m_tabControl.Controls.Add(this.tabObjects);
      this.m_tabControl.Controls.Add(this.tabColors);
      this.m_tabControl.Controls.Add(this.tabRegions);
      this.m_tabControl.Controls.Add(this.tabRoomShapes);
      this.m_tabControl.Location = new System.Drawing.Point(15, 167);
      this.m_tabControl.Name = "m_tabControl";
      this.m_tabControl.SelectedIndex = 0;
      this.m_tabControl.Size = new System.Drawing.Size(488, 216);
      this.m_tabControl.TabIndex = 101;
      this.m_tabControl.SelectedIndexChanged += new System.EventHandler(this.m_tabControl_SelectedIndexChanged);
      // 
      // tabDescription
      // 
      this.tabDescription.Controls.Add(this.m_descriptionTextBox);
      this.tabDescription.Location = new System.Drawing.Point(4, 22);
      this.tabDescription.Name = "tabDescription";
      this.tabDescription.Padding = new System.Windows.Forms.Padding(3);
      this.tabDescription.Size = new System.Drawing.Size(480, 190);
      this.tabDescription.TabIndex = 0;
      this.tabDescription.Text = "Description";
      this.tabDescription.UseVisualStyleBackColor = true;
      // 
      // tabObjects
      // 
      this.tabObjects.Controls.Add(this.txtRight);
      this.tabObjects.Controls.Add(this.txtDown);
      this.tabObjects.Controls.Add(this.m_objectsPositionGroupBox);
      this.tabObjects.Controls.Add(this.label12);
      this.tabObjects.Controls.Add(this.lblObjectSyntaxHelp);
      this.tabObjects.Controls.Add(this.label10);
      this.tabObjects.Controls.Add(this.pnlObjectSyntaxHelp);
      this.tabObjects.Controls.Add(this.chkCustomPosition);
      this.tabObjects.Controls.Add(this.txtObjects);
      this.tabObjects.Location = new System.Drawing.Point(4, 22);
      this.tabObjects.Name = "tabObjects";
      this.tabObjects.Padding = new System.Windows.Forms.Padding(3);
      this.tabObjects.Size = new System.Drawing.Size(480, 190);
      this.tabObjects.TabIndex = 1;
      this.tabObjects.Text = "Objects";
      this.tabObjects.UseVisualStyleBackColor = true;
      // 
      // tabColors
      // 
      this.tabColors.Controls.Add(this.subtitleColorClear);
      this.tabColors.Controls.Add(this.m_roomFillTextBox);
      this.tabColors.Controls.Add(this.objectColorClear);
      this.tabColors.Controls.Add(this.m_changeRoomTextButton);
      this.tabColors.Controls.Add(this.roomTextColorClear);
      this.tabColors.Controls.Add(this.label3);
      this.tabColors.Controls.Add(this.roomBorderColorClear);
      this.tabColors.Controls.Add(this.label2);
      this.tabColors.Controls.Add(this.secondColorClear);
      this.tabColors.Controls.Add(this.m_changeObjectTextButton);
      this.tabColors.Controls.Add(this.roomColorClear);
      this.tabColors.Controls.Add(this.m_changeRoomBorderButton);
      this.tabColors.Controls.Add(this.m_subTitleTextTextBox);
      this.tabColors.Controls.Add(this.label4);
      this.tabColors.Controls.Add(this.label14);
      this.tabColors.Controls.Add(this.label11);
      this.tabColors.Controls.Add(this.m_changeSubtitleTextButton);
      this.tabColors.Controls.Add(this.m_changeSecondFillButton);
      this.tabColors.Controls.Add(this.m_secondFillTextBox);
      this.tabColors.Controls.Add(this.m_changeRoomFillButton);
      this.tabColors.Controls.Add(this.m_objectTextTextBox);
      this.tabColors.Controls.Add(this.label5);
      this.tabColors.Controls.Add(this.m_roomTextTextBox);
      this.tabColors.Controls.Add(this.comboBox1);
      this.tabColors.Controls.Add(this.m_roomBorderTextBox);
      this.tabColors.Location = new System.Drawing.Point(4, 22);
      this.tabColors.Name = "tabColors";
      this.tabColors.Padding = new System.Windows.Forms.Padding(3);
      this.tabColors.Size = new System.Drawing.Size(480, 190);
      this.tabColors.TabIndex = 2;
      this.tabColors.Text = "Colors";
      this.tabColors.UseVisualStyleBackColor = true;
      // 
      // tabRegions
      // 
      this.tabRegions.Controls.Add(this.cboRegion);
      this.tabRegions.Controls.Add(this.label6);
      this.tabRegions.Location = new System.Drawing.Point(4, 22);
      this.tabRegions.Name = "tabRegions";
      this.tabRegions.Padding = new System.Windows.Forms.Padding(3);
      this.tabRegions.Size = new System.Drawing.Size(480, 190);
      this.tabRegions.TabIndex = 3;
      this.tabRegions.Text = "Regions";
      this.tabRegions.UseVisualStyleBackColor = true;
      // 
      // tabRoomShapes
      // 
      this.tabRoomShapes.Controls.Add(this.chkHandDrawnRoom);
      this.tabRoomShapes.Controls.Add(this.cboDrawType);
      this.tabRoomShapes.Controls.Add(this.groupRoundedCorners);
      this.tabRoomShapes.Controls.Add(this.pnlSampleRoomShape);
      this.tabRoomShapes.Location = new System.Drawing.Point(4, 22);
      this.tabRoomShapes.Name = "tabRoomShapes";
      this.tabRoomShapes.Padding = new System.Windows.Forms.Padding(3);
      this.tabRoomShapes.Size = new System.Drawing.Size(480, 190);
      this.tabRoomShapes.TabIndex = 4;
      this.tabRoomShapes.Text = "Room Shapes";
      this.tabRoomShapes.UseVisualStyleBackColor = true;
      // 
      // RoomPropertiesDialog
      // 
      this.AcceptButton = this.m_okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_cancelButton;
      this.ClientSize = new System.Drawing.Size(512, 424);
      this.Controls.Add(this.m_tabControl);
      this.Controls.Add(this.cboReference);
      this.Controls.Add(this.label13);
      this.Controls.Add(this.chkEndRoom);
      this.Controls.Add(this.chkStartRoom);
      this.Controls.Add(this.cboBorderStyle);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.txtSubTitle);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.m_isDarkCheckBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.m_cancelButton);
      this.Controls.Add(this.m_okButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(265, 295);
      this.Name = "RoomPropertiesDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Room Properties";
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RoomPropertiesDialog_KeyUp);
      ((System.ComponentModel.ISupportInitialize)(this.txtRight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtDown)).EndInit();
      this.pnlObjectSyntaxHelp.ResumeLayout(false);
      this.m_objectsPositionGroupBox.ResumeLayout(false);
      this.groupRoundedCorners.ResumeLayout(false);
      this.groupRoundedCorners.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.txtBottomRight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTopLeft)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtBottomLeft)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtTopRight)).EndInit();
      this.m_tabControl.ResumeLayout(false);
      this.tabDescription.ResumeLayout(false);
      this.tabDescription.PerformLayout();
      this.tabObjects.ResumeLayout(false);
      this.tabObjects.PerformLayout();
      this.tabColors.ResumeLayout(false);
      this.tabColors.PerformLayout();
      this.tabRegions.ResumeLayout(false);
      this.tabRegions.PerformLayout();
      this.tabRoomShapes.ResumeLayout(false);
      this.tabRoomShapes.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox m_isDarkCheckBox;
        private System.Windows.Forms.TextBox m_descriptionTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button m_changeObjectTextButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_changeRoomTextButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_changeRoomBorderButton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button m_changeRoomFillButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button m_changeSecondFillButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.Label label6;
        private TrizbortTextBox txtName;
        private TrizbortTextBox txtObjects;
        private System.Windows.Forms.GroupBox m_objectsPositionGroupBox;
        private System.Windows.Forms.CheckBox m_cCheckBox;
        private System.Windows.Forms.CheckBox m_nwCheckBox;
        private System.Windows.Forms.CheckBox m_seCheckBox;
        private System.Windows.Forms.CheckBox m_nCheckBox;
        private System.Windows.Forms.CheckBox m_sCheckBox;
        private System.Windows.Forms.CheckBox m_neCheckBox;
        private System.Windows.Forms.CheckBox m_swCheckBox;
        private System.Windows.Forms.CheckBox m_wCheckBox;
        private System.Windows.Forms.CheckBox m_eCheckBox;
        private TrizbortTextBox txtSubTitle;
        private System.Windows.Forms.Label label7;
        private TrizbortTextBox m_roomFillTextBox;
        private TrizbortTextBox m_secondFillTextBox;
        private TrizbortTextBox m_objectTextTextBox;
        private TrizbortTextBox m_roomTextTextBox;
        private TrizbortTextBox m_roomBorderTextBox;
        private System.Windows.Forms.ComboBox cboBorderStyle;
        private System.Windows.Forms.Label label8;
    private System.Windows.Forms.NumericUpDown txtBottomRight;
    private System.Windows.Forms.NumericUpDown txtTopRight;
    private System.Windows.Forms.NumericUpDown txtBottomLeft;
    private System.Windows.Forms.NumericUpDown txtTopLeft;
    private System.Windows.Forms.Panel pnlSampleRoomShape;
    private System.Windows.Forms.ComboBox cboDrawType;
    private System.Windows.Forms.Panel groupRoundedCorners;
    private System.Windows.Forms.CheckBox chkCornersSame;
    private System.Windows.Forms.CheckBox chkStartRoom;
    private System.Windows.Forms.Label lblObjectSyntaxHelp;
    private System.Windows.Forms.Panel pnlObjectSyntaxHelp;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.CheckBox chkHandDrawnRoom;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.CheckBox chkCustomPosition;
    private System.Windows.Forms.NumericUpDown txtRight;
    private System.Windows.Forms.NumericUpDown txtDown;
    private TrizbortToolTip toolTip;
    private System.Windows.Forms.CheckBox chkEndRoom;
    private System.Windows.Forms.ComboBox cboReference;
    private System.Windows.Forms.Label label13;
    private TrizbortTextBox m_subTitleTextTextBox;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.Button m_changeSubtitleTextButton;
    private System.Windows.Forms.Button subtitleColorClear;
    private System.Windows.Forms.Button objectColorClear;
    private System.Windows.Forms.Button roomTextColorClear;
    private System.Windows.Forms.Button roomBorderColorClear;
    private System.Windows.Forms.Button secondColorClear;
    private System.Windows.Forms.Button roomColorClear;
    private System.Windows.Forms.TabControl m_tabControl;
    private System.Windows.Forms.TabPage tabDescription;
    private System.Windows.Forms.TabPage tabObjects;
    private System.Windows.Forms.TabPage tabColors;
    private System.Windows.Forms.TabPage tabRegions;
    private System.Windows.Forms.TabPage tabRoomShapes;
  }
}
