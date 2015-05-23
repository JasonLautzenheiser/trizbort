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
      this.m_tabControl = new DevComponents.DotNetBar.SuperTabControl();
      this.superTabControlPanel4 = new DevComponents.DotNetBar.SuperTabControlPanel();
      this.tabRegions = new DevComponents.DotNetBar.SuperTabItem();
      this.superTabControlPanel3 = new DevComponents.DotNetBar.SuperTabControlPanel();
      this.m_secondFillTextBox = new DevComponents.DotNetBar.Controls.TextBoxX();
      this.m_objectTextTextBox = new DevComponents.DotNetBar.Controls.TextBoxX();
      this.m_roomTextTextBox = new DevComponents.DotNetBar.Controls.TextBoxX();
      this.m_roomBorderTextBox = new DevComponents.DotNetBar.Controls.TextBoxX();
      this.m_roomFillTextBox = new DevComponents.DotNetBar.Controls.TextBoxX();
      this.tabColors = new DevComponents.DotNetBar.SuperTabItem();
      this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
      this.tabDescription = new DevComponents.DotNetBar.SuperTabItem();
      this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
      this.txtObjects = new DevComponents.DotNetBar.Controls.TextBoxX();
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
      this.tabObjects = new DevComponents.DotNetBar.SuperTabItem();
      this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
      this.txtSubTitle = new DevComponents.DotNetBar.Controls.TextBoxX();
      this.label7 = new System.Windows.Forms.Label();
      this.cboBorderStyle = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.m_tabControl)).BeginInit();
      this.m_tabControl.SuspendLayout();
      this.superTabControlPanel4.SuspendLayout();
      this.superTabControlPanel3.SuspendLayout();
      this.superTabControlPanel2.SuspendLayout();
      this.superTabControlPanel1.SuspendLayout();
      this.m_objectsPositionGroupBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_okButton
      // 
      this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_okButton.Location = new System.Drawing.Point(339, 346);
      this.m_okButton.Name = "m_okButton";
      this.m_okButton.Size = new System.Drawing.Size(75, 23);
      this.m_okButton.TabIndex = 97;
      this.m_okButton.Text = "O&K";
      this.m_okButton.UseVisualStyleBackColor = true;
      // 
      // m_cancelButton
      // 
      this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_cancelButton.Location = new System.Drawing.Point(420, 346);
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
      this.m_isDarkCheckBox.Location = new System.Drawing.Point(447, 72);
      this.m_isDarkCheckBox.Name = "m_isDarkCheckBox";
      this.m_isDarkCheckBox.Size = new System.Drawing.Size(48, 17);
      this.m_isDarkCheckBox.TabIndex = 4;
      this.m_isDarkCheckBox.Text = "&Dark";
      this.m_isDarkCheckBox.UseVisualStyleBackColor = true;
      // 
      // m_descriptionTextBox
      // 
      this.m_descriptionTextBox.AcceptsReturn = true;
      this.m_descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_descriptionTextBox.Location = new System.Drawing.Point(3, 3);
      this.m_descriptionTextBox.Multiline = true;
      this.m_descriptionTextBox.Name = "m_descriptionTextBox";
      this.m_descriptionTextBox.Size = new System.Drawing.Size(467, 204);
      this.m_descriptionTextBox.TabIndex = 13;
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
      this.comboBox1.Location = new System.Drawing.Point(80, 41);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(121, 21);
      this.comboBox1.TabIndex = 15;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.BackColor = System.Drawing.Color.Transparent;
      this.label5.Location = new System.Drawing.Point(5, 44);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(57, 13);
      this.label5.TabIndex = 9;
      this.label5.Text = "&Second Fill";
      // 
      // m_changeSecondFillButton
      // 
      this.m_changeSecondFillButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeSecondFillButton.Location = new System.Drawing.Point(390, 39);
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
      this.label4.Location = new System.Drawing.Point(5, 131);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(64, 13);
      this.label4.TabIndex = 19;
      this.label4.Text = "Ob&ject Text";
      // 
      // m_changeObjectTextButton
      // 
      this.m_changeObjectTextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeObjectTextButton.Location = new System.Drawing.Point(390, 126);
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
      this.label3.Location = new System.Drawing.Point(5, 102);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(59, 13);
      this.label3.TabIndex = 16;
      this.label3.Text = "Room &Text";
      // 
      // m_changeRoomTextButton
      // 
      this.m_changeRoomTextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeRoomTextButton.Location = new System.Drawing.Point(390, 97);
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
      this.label2.Location = new System.Drawing.Point(5, 73);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(69, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "Room &Border";
      // 
      // m_changeRoomBorderButton
      // 
      this.m_changeRoomBorderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeRoomBorderButton.Location = new System.Drawing.Point(390, 68);
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
      this.label11.Location = new System.Drawing.Point(5, 15);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(49, 13);
      this.label11.TabIndex = 10;
      this.label11.Text = "Room &Fill";
      // 
      // m_changeRoomFillButton
      // 
      this.m_changeRoomFillButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_changeRoomFillButton.Location = new System.Drawing.Point(390, 10);
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
      this.cboRegion.Location = new System.Drawing.Point(65, 13);
      this.cboRegion.Name = "cboRegion";
      this.cboRegion.Size = new System.Drawing.Size(186, 21);
      this.cboRegion.TabIndex = 20;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.BackColor = System.Drawing.Color.Transparent;
      this.label6.Location = new System.Drawing.Point(12, 16);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(47, 13);
      this.label6.TabIndex = 21;
      this.label6.Text = "Region: ";
      // 
      // m_tabControl
      // 
      this.m_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.m_tabControl.BackColor = System.Drawing.Color.Transparent;
      this.m_tabControl.CloseButtonOnTabsAlwaysDisplayed = false;
      // 
      // 
      // 
      // 
      // 
      // 
      this.m_tabControl.ControlBox.CloseBox.Name = "";
      // 
      // 
      // 
      this.m_tabControl.ControlBox.MenuBox.Name = "";
      this.m_tabControl.ControlBox.Name = "";
      this.m_tabControl.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.m_tabControl.ControlBox.MenuBox,
            this.m_tabControl.ControlBox.CloseBox});
      this.m_tabControl.Controls.Add(this.superTabControlPanel3);
      this.m_tabControl.Controls.Add(this.superTabControlPanel1);
      this.m_tabControl.Controls.Add(this.superTabControlPanel4);
      this.m_tabControl.Controls.Add(this.superTabControlPanel2);
      this.m_tabControl.Location = new System.Drawing.Point(19, 130);
      this.m_tabControl.Name = "m_tabControl";
      this.m_tabControl.ReorderTabsEnabled = true;
      this.m_tabControl.SelectedTabFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.m_tabControl.SelectedTabIndex = 0;
      this.m_tabControl.Size = new System.Drawing.Size(472, 210);
      this.m_tabControl.TabFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.m_tabControl.TabIndex = 5;
      this.m_tabControl.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabObjects,
            this.tabDescription,
            this.tabColors,
            this.tabRegions});
      this.m_tabControl.TabStop = false;
      this.m_tabControl.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.VisualStudio2008Dock;
      this.m_tabControl.Enter += new System.EventHandler(this.m_tabControl_Enter);
      // 
      // superTabControlPanel4
      // 
      this.superTabControlPanel4.Controls.Add(this.cboRegion);
      this.superTabControlPanel4.Controls.Add(this.label6);
      this.superTabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.superTabControlPanel4.Location = new System.Drawing.Point(0, 0);
      this.superTabControlPanel4.Name = "superTabControlPanel4";
      this.superTabControlPanel4.Size = new System.Drawing.Size(472, 210);
      this.superTabControlPanel4.TabIndex = 0;
      this.superTabControlPanel4.TabItem = this.tabRegions;
      // 
      // tabRegions
      // 
      this.tabRegions.AttachedControl = this.superTabControlPanel4;
      this.tabRegions.GlobalItem = false;
      this.tabRegions.Name = "tabRegions";
      this.tabRegions.Text = "Re&gions";
      // 
      // superTabControlPanel3
      // 
      this.superTabControlPanel3.Controls.Add(this.m_secondFillTextBox);
      this.superTabControlPanel3.Controls.Add(this.m_objectTextTextBox);
      this.superTabControlPanel3.Controls.Add(this.m_roomTextTextBox);
      this.superTabControlPanel3.Controls.Add(this.m_roomBorderTextBox);
      this.superTabControlPanel3.Controls.Add(this.m_roomFillTextBox);
      this.superTabControlPanel3.Controls.Add(this.comboBox1);
      this.superTabControlPanel3.Controls.Add(this.label5);
      this.superTabControlPanel3.Controls.Add(this.m_changeRoomFillButton);
      this.superTabControlPanel3.Controls.Add(this.m_changeSecondFillButton);
      this.superTabControlPanel3.Controls.Add(this.label11);
      this.superTabControlPanel3.Controls.Add(this.label4);
      this.superTabControlPanel3.Controls.Add(this.m_changeRoomBorderButton);
      this.superTabControlPanel3.Controls.Add(this.m_changeObjectTextButton);
      this.superTabControlPanel3.Controls.Add(this.label2);
      this.superTabControlPanel3.Controls.Add(this.label3);
      this.superTabControlPanel3.Controls.Add(this.m_changeRoomTextButton);
      this.superTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.superTabControlPanel3.Location = new System.Drawing.Point(0, 26);
      this.superTabControlPanel3.Name = "superTabControlPanel3";
      this.superTabControlPanel3.Size = new System.Drawing.Size(472, 184);
      this.superTabControlPanel3.TabIndex = 0;
      this.superTabControlPanel3.TabItem = this.tabColors;
      // 
      // m_secondFillTextBox
      // 
      // 
      // 
      // 
      this.m_secondFillTextBox.Border.Class = "TextBoxBorder";
      this.m_secondFillTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.m_secondFillTextBox.ButtonCustom.Text = "Clear";
      this.m_secondFillTextBox.ButtonCustom.Visible = true;
      this.m_secondFillTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_secondFillTextBox.Location = new System.Drawing.Point(207, 41);
      this.m_secondFillTextBox.Name = "m_secondFillTextBox";
      this.m_secondFillTextBox.ReadOnly = true;
      this.m_secondFillTextBox.Size = new System.Drawing.Size(177, 21);
      this.m_secondFillTextBox.TabIndex = 25;
      this.m_secondFillTextBox.ButtonCustomClick += new System.EventHandler(this.m_secondFillTextBox_ButtonCustomClick);
      this.m_secondFillTextBox.DoubleClick += new System.EventHandler(this.m_secondFillTextBox_DoubleClick);
      this.m_secondFillTextBox.Enter += new System.EventHandler(this.m_secondFillTextBox_Enter);
      // 
      // m_objectTextTextBox
      // 
      // 
      // 
      // 
      this.m_objectTextTextBox.Border.Class = "TextBoxBorder";
      this.m_objectTextTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.m_objectTextTextBox.ButtonCustom.Text = "Clear";
      this.m_objectTextTextBox.ButtonCustom.Visible = true;
      this.m_objectTextTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_objectTextTextBox.Location = new System.Drawing.Point(80, 128);
      this.m_objectTextTextBox.Name = "m_objectTextTextBox";
      this.m_objectTextTextBox.ReadOnly = true;
      this.m_objectTextTextBox.Size = new System.Drawing.Size(304, 21);
      this.m_objectTextTextBox.TabIndex = 24;
      this.m_objectTextTextBox.ButtonCustomClick += new System.EventHandler(this.m_objectTextTextBox_ButtonCustomClick);
      this.m_objectTextTextBox.DoubleClick += new System.EventHandler(this.m_objectTextTextBox_DoubleClick);
      this.m_objectTextTextBox.Enter += new System.EventHandler(this.m_objectTextTextBox_Enter);
      // 
      // m_roomTextTextBox
      // 
      // 
      // 
      // 
      this.m_roomTextTextBox.Border.Class = "TextBoxBorder";
      this.m_roomTextTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.m_roomTextTextBox.ButtonCustom.Text = "Clear";
      this.m_roomTextTextBox.ButtonCustom.Visible = true;
      this.m_roomTextTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_roomTextTextBox.Location = new System.Drawing.Point(80, 99);
      this.m_roomTextTextBox.Name = "m_roomTextTextBox";
      this.m_roomTextTextBox.ReadOnly = true;
      this.m_roomTextTextBox.Size = new System.Drawing.Size(304, 21);
      this.m_roomTextTextBox.TabIndex = 23;
      this.m_roomTextTextBox.ButtonCustomClick += new System.EventHandler(this.m_roomTextTextBox_ButtonCustomClick);
      this.m_roomTextTextBox.DoubleClick += new System.EventHandler(this.m_roomTextTextBox_DoubleClick);
      this.m_roomTextTextBox.Enter += new System.EventHandler(this.m_roomTextTextBox_Enter);
      // 
      // m_roomBorderTextBox
      // 
      // 
      // 
      // 
      this.m_roomBorderTextBox.Border.Class = "TextBoxBorder";
      this.m_roomBorderTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.m_roomBorderTextBox.ButtonCustom.Text = "Clear";
      this.m_roomBorderTextBox.ButtonCustom.Visible = true;
      this.m_roomBorderTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_roomBorderTextBox.Location = new System.Drawing.Point(80, 70);
      this.m_roomBorderTextBox.Name = "m_roomBorderTextBox";
      this.m_roomBorderTextBox.ReadOnly = true;
      this.m_roomBorderTextBox.Size = new System.Drawing.Size(304, 21);
      this.m_roomBorderTextBox.TabIndex = 22;
      this.m_roomBorderTextBox.ButtonCustomClick += new System.EventHandler(this.m_roomBorderTextBox_ButtonCustomClick);
      this.m_roomBorderTextBox.DoubleClick += new System.EventHandler(this.m_roomBorderTextBox_DoubleClick);
      this.m_roomBorderTextBox.Enter += new System.EventHandler(this.m_roomBorderTextBox_Enter);
      // 
      // m_roomFillTextBox
      // 
      // 
      // 
      // 
      this.m_roomFillTextBox.Border.Class = "TextBoxBorder";
      this.m_roomFillTextBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.m_roomFillTextBox.ButtonCustom.Text = "Clear";
      this.m_roomFillTextBox.ButtonCustom.Visible = true;
      this.m_roomFillTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.m_roomFillTextBox.Location = new System.Drawing.Point(80, 12);
      this.m_roomFillTextBox.Name = "m_roomFillTextBox";
      this.m_roomFillTextBox.ReadOnly = true;
      this.m_roomFillTextBox.Size = new System.Drawing.Size(304, 21);
      this.m_roomFillTextBox.TabIndex = 21;
      this.m_roomFillTextBox.ButtonCustomClick += new System.EventHandler(this.m_roomFillTextBox_ButtonCustomClick);
      this.m_roomFillTextBox.DoubleClick += new System.EventHandler(this.m_roomFillTextBox_DoubleClick);
      this.m_roomFillTextBox.Enter += new System.EventHandler(this.m_roomFillTextBox_Enter);
      // 
      // tabColors
      // 
      this.tabColors.AttachedControl = this.superTabControlPanel3;
      this.tabColors.GlobalItem = false;
      this.tabColors.Name = "tabColors";
      this.tabColors.Text = "&Colors";
      // 
      // superTabControlPanel2
      // 
      this.superTabControlPanel2.Controls.Add(this.m_descriptionTextBox);
      this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.superTabControlPanel2.Location = new System.Drawing.Point(0, 0);
      this.superTabControlPanel2.Name = "superTabControlPanel2";
      this.superTabControlPanel2.Size = new System.Drawing.Size(472, 210);
      this.superTabControlPanel2.TabIndex = 0;
      this.superTabControlPanel2.TabItem = this.tabDescription;
      // 
      // tabDescription
      // 
      this.tabDescription.AttachedControl = this.superTabControlPanel2;
      this.tabDescription.GlobalItem = false;
      this.tabDescription.Name = "tabDescription";
      this.tabDescription.Text = "D&escription";
      // 
      // superTabControlPanel1
      // 
      this.superTabControlPanel1.Controls.Add(this.txtObjects);
      this.superTabControlPanel1.Controls.Add(this.m_objectsPositionGroupBox);
      this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.superTabControlPanel1.Location = new System.Drawing.Point(0, 26);
      this.superTabControlPanel1.Name = "superTabControlPanel1";
      this.superTabControlPanel1.Size = new System.Drawing.Size(472, 184);
      this.superTabControlPanel1.TabIndex = 1;
      this.superTabControlPanel1.TabItem = this.tabObjects;
      // 
      // txtObjects
      // 
      this.txtObjects.AcceptsReturn = true;
      // 
      // 
      // 
      this.txtObjects.Border.Class = "TextBoxBorder";
      this.txtObjects.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.txtObjects.Location = new System.Drawing.Point(4, 6);
      this.txtObjects.Multiline = true;
      this.txtObjects.Name = "txtObjects";
      this.txtObjects.Size = new System.Drawing.Size(359, 156);
      this.txtObjects.TabIndex = 7;
      this.txtObjects.WatermarkText = "Enter objects, each on a new line.";
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
      this.m_objectsPositionGroupBox.Location = new System.Drawing.Point(369, 4);
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
      // tabObjects
      // 
      this.tabObjects.AttachedControl = this.superTabControlPanel1;
      this.tabObjects.GlobalItem = false;
      this.tabObjects.Name = "tabObjects";
      this.tabObjects.Text = "&Objects";
      // 
      // txtName
      // 
      // 
      // 
      // 
      this.txtName.Border.Class = "TextBoxBorder";
      this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.txtName.Location = new System.Drawing.Point(15, 28);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(474, 21);
      this.txtName.TabIndex = 1;
      // 
      // txtSubTitle
      // 
      // 
      // 
      // 
      this.txtSubTitle.Border.Class = "TextBoxBorder";
      this.txtSubTitle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.txtSubTitle.Location = new System.Drawing.Point(15, 68);
      this.txtSubTitle.Name = "txtSubTitle";
      this.txtSubTitle.Size = new System.Drawing.Size(426, 21);
      this.txtSubTitle.TabIndex = 3;
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
      this.cboBorderStyle.Size = new System.Drawing.Size(186, 21);
      this.cboBorderStyle.TabIndex = 6;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.BackColor = System.Drawing.Color.Transparent;
      this.label8.Location = new System.Drawing.Point(16, 98);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(70, 13);
      this.label8.TabIndex = 5;
      this.label8.Text = "Border St&yle:";
      // 
      // RoomPropertiesDialog
      // 
      this.AcceptButton = this.m_okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_cancelButton;
      this.ClientSize = new System.Drawing.Size(507, 381);
      this.Controls.Add(this.cboBorderStyle);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.txtSubTitle);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.m_tabControl);
      this.Controls.Add(this.m_isDarkCheckBox);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.m_cancelButton);
      this.Controls.Add(this.m_okButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(265, 295);
      this.Name = "RoomPropertiesDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Room Properties";
      this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RoomPropertiesDialog_KeyUp);
      ((System.ComponentModel.ISupportInitialize)(this.m_tabControl)).EndInit();
      this.m_tabControl.ResumeLayout(false);
      this.superTabControlPanel4.ResumeLayout(false);
      this.superTabControlPanel4.PerformLayout();
      this.superTabControlPanel3.ResumeLayout(false);
      this.superTabControlPanel3.PerformLayout();
      this.superTabControlPanel2.ResumeLayout(false);
      this.superTabControlPanel2.PerformLayout();
      this.superTabControlPanel1.ResumeLayout(false);
      this.m_objectsPositionGroupBox.ResumeLayout(false);
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
        private DevComponents.DotNetBar.SuperTabControl m_tabControl;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem tabObjects;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel3;
        private DevComponents.DotNetBar.SuperTabItem tabColors;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel4;
        private DevComponents.DotNetBar.SuperTabItem tabRegions;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem tabDescription;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtObjects;
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
        private DevComponents.DotNetBar.Controls.TextBoxX txtSubTitle;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.TextBoxX m_roomFillTextBox;
        private DevComponents.DotNetBar.Controls.TextBoxX m_secondFillTextBox;
        private DevComponents.DotNetBar.Controls.TextBoxX m_objectTextTextBox;
        private DevComponents.DotNetBar.Controls.TextBoxX m_roomTextTextBox;
        private DevComponents.DotNetBar.Controls.TextBoxX m_roomBorderTextBox;
        private System.Windows.Forms.ComboBox cboBorderStyle;
        private System.Windows.Forms.Label label8;
    }
}
