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
            this.m_nameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_isDarkCheckBox = new System.Windows.Forms.CheckBox();
            this.m_tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.m_objectsTextBox = new System.Windows.Forms.TextBox();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.m_descriptionTextBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.m_changeSecondFillButton = new System.Windows.Forms.Button();
            this.m_secondFillTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_changeObjectTextButton = new System.Windows.Forms.Button();
            this.m_objectTextTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_changeRoomTextButton = new System.Windows.Forms.Button();
            this.m_roomTextTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_changeRoomBorderButton = new System.Windows.Forms.Button();
            this.m_roomBorderTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.m_changeRoomFillButton = new System.Windows.Forms.Button();
            this.m_roomFillTextBox = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cboRegion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.m_objectsPositionGroupBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_okButton.Location = new System.Drawing.Point(267, 317);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(75, 23);
            this.m_okButton.TabIndex = 4;
            this.m_okButton.Text = "OK";
            this.m_okButton.UseVisualStyleBackColor = true;
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(348, 317);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_cancelButton.TabIndex = 5;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // m_nameTextBox
            // 
            this.m_nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_nameTextBox.Location = new System.Drawing.Point(15, 34);
            this.m_nameTextBox.Name = "m_nameTextBox";
            this.m_nameTextBox.Size = new System.Drawing.Size(408, 21);
            this.m_nameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "&Name";
            // 
            // m_isDarkCheckBox
            // 
            this.m_isDarkCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_isDarkCheckBox.AutoSize = true;
            this.m_isDarkCheckBox.Location = new System.Drawing.Point(375, 60);
            this.m_isDarkCheckBox.Name = "m_isDarkCheckBox";
            this.m_isDarkCheckBox.Size = new System.Drawing.Size(48, 17);
            this.m_isDarkCheckBox.TabIndex = 2;
            this.m_isDarkCheckBox.Text = "&Dark";
            this.m_isDarkCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_tabControl
            // 
            this.m_tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_tabControl.Controls.Add(this.tabPage1);
            this.m_tabControl.Controls.Add(this.tabPage2);
            this.m_tabControl.Controls.Add(this.tabPage3);
            this.m_tabControl.Controls.Add(this.tabPage4);
            this.m_tabControl.Location = new System.Drawing.Point(15, 83);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new System.Drawing.Size(408, 228);
            this.m_tabControl.TabIndex = 3;
            this.m_tabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.m_objectsTextBox);
            this.tabPage1.Controls.Add(this.m_objectsPositionGroupBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(400, 202);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Objects";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // m_objectsTextBox
            // 
            this.m_objectsTextBox.AcceptsReturn = true;
            this.m_objectsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_objectsTextBox.Location = new System.Drawing.Point(2, 6);
            this.m_objectsTextBox.Multiline = true;
            this.m_objectsTextBox.Name = "m_objectsTextBox";
            this.m_objectsTextBox.Size = new System.Drawing.Size(288, 193);
            this.m_objectsTextBox.TabIndex = 0;
            // 
            // m_objectsPositionGroupBox
            // 
            this.m_objectsPositionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_objectsPositionGroupBox.Controls.Add(this.m_cCheckBox);
            this.m_objectsPositionGroupBox.Controls.Add(this.m_nwCheckBox);
            this.m_objectsPositionGroupBox.Controls.Add(this.m_seCheckBox);
            this.m_objectsPositionGroupBox.Controls.Add(this.m_nCheckBox);
            this.m_objectsPositionGroupBox.Controls.Add(this.m_sCheckBox);
            this.m_objectsPositionGroupBox.Controls.Add(this.m_neCheckBox);
            this.m_objectsPositionGroupBox.Controls.Add(this.m_swCheckBox);
            this.m_objectsPositionGroupBox.Controls.Add(this.m_wCheckBox);
            this.m_objectsPositionGroupBox.Controls.Add(this.m_eCheckBox);
            this.m_objectsPositionGroupBox.Location = new System.Drawing.Point(296, 0);
            this.m_objectsPositionGroupBox.Name = "m_objectsPositionGroupBox";
            this.m_objectsPositionGroupBox.Size = new System.Drawing.Size(102, 114);
            this.m_objectsPositionGroupBox.TabIndex = 1;
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
            this.m_cCheckBox.TabIndex = 4;
            this.m_cCheckBox.Tag = "Position";
            this.m_cCheckBox.Text = "o";
            this.m_cCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_cCheckBox.UseVisualStyleBackColor = true;
            this.m_cCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // m_nwCheckBox
            // 
            this.m_nwCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_nwCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.m_nwCheckBox.Location = new System.Drawing.Point(6, 16);
            this.m_nwCheckBox.Name = "m_nwCheckBox";
            this.m_nwCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_nwCheckBox.TabIndex = 0;
            this.m_nwCheckBox.Tag = "Position";
            this.m_nwCheckBox.Text = "ã";
            this.m_nwCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_nwCheckBox.UseVisualStyleBackColor = true;
            this.m_nwCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // m_seCheckBox
            // 
            this.m_seCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_seCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.m_seCheckBox.Location = new System.Drawing.Point(70, 80);
            this.m_seCheckBox.Name = "m_seCheckBox";
            this.m_seCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_seCheckBox.TabIndex = 8;
            this.m_seCheckBox.Tag = "Position";
            this.m_seCheckBox.Text = "æ";
            this.m_seCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_seCheckBox.UseVisualStyleBackColor = true;
            this.m_seCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // m_nCheckBox
            // 
            this.m_nCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_nCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.m_nCheckBox.Location = new System.Drawing.Point(38, 16);
            this.m_nCheckBox.Name = "m_nCheckBox";
            this.m_nCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_nCheckBox.TabIndex = 1;
            this.m_nCheckBox.Tag = "Position";
            this.m_nCheckBox.Text = "á";
            this.m_nCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_nCheckBox.UseVisualStyleBackColor = true;
            this.m_nCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // m_sCheckBox
            // 
            this.m_sCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_sCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.m_sCheckBox.Location = new System.Drawing.Point(38, 80);
            this.m_sCheckBox.Name = "m_sCheckBox";
            this.m_sCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_sCheckBox.TabIndex = 7;
            this.m_sCheckBox.Tag = "Position";
            this.m_sCheckBox.Text = "â";
            this.m_sCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_sCheckBox.UseVisualStyleBackColor = true;
            this.m_sCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // m_neCheckBox
            // 
            this.m_neCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_neCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.m_neCheckBox.Location = new System.Drawing.Point(70, 16);
            this.m_neCheckBox.Name = "m_neCheckBox";
            this.m_neCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_neCheckBox.TabIndex = 2;
            this.m_neCheckBox.Tag = "Position";
            this.m_neCheckBox.Text = "ä";
            this.m_neCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_neCheckBox.UseVisualStyleBackColor = true;
            this.m_neCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // m_swCheckBox
            // 
            this.m_swCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_swCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.m_swCheckBox.Location = new System.Drawing.Point(6, 80);
            this.m_swCheckBox.Name = "m_swCheckBox";
            this.m_swCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_swCheckBox.TabIndex = 6;
            this.m_swCheckBox.Tag = "Position";
            this.m_swCheckBox.Text = "å";
            this.m_swCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_swCheckBox.UseVisualStyleBackColor = true;
            this.m_swCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // m_wCheckBox
            // 
            this.m_wCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_wCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.m_wCheckBox.Location = new System.Drawing.Point(6, 48);
            this.m_wCheckBox.Name = "m_wCheckBox";
            this.m_wCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_wCheckBox.TabIndex = 3;
            this.m_wCheckBox.Tag = "Position";
            this.m_wCheckBox.Text = "ß";
            this.m_wCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_wCheckBox.UseVisualStyleBackColor = true;
            this.m_wCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // m_eCheckBox
            // 
            this.m_eCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.m_eCheckBox.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.m_eCheckBox.Location = new System.Drawing.Point(70, 48);
            this.m_eCheckBox.Name = "m_eCheckBox";
            this.m_eCheckBox.Size = new System.Drawing.Size(26, 26);
            this.m_eCheckBox.TabIndex = 5;
            this.m_eCheckBox.Tag = "Position";
            this.m_eCheckBox.Text = "à";
            this.m_eCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.m_eCheckBox.UseVisualStyleBackColor = true;
            this.m_eCheckBox.Click += new System.EventHandler(this.PositionCheckBox_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.m_descriptionTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(400, 202);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Description";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // m_descriptionTextBox
            // 
            this.m_descriptionTextBox.AcceptsReturn = true;
            this.m_descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_descriptionTextBox.Location = new System.Drawing.Point(2, 6);
            this.m_descriptionTextBox.Multiline = true;
            this.m_descriptionTextBox.Name = "m_descriptionTextBox";
            this.m_descriptionTextBox.Size = new System.Drawing.Size(395, 193);
            this.m_descriptionTextBox.TabIndex = 3;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.comboBox1);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.m_changeSecondFillButton);
            this.tabPage3.Controls.Add(this.m_secondFillTextBox);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.m_changeObjectTextButton);
            this.tabPage3.Controls.Add(this.m_objectTextTextBox);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.m_changeRoomTextButton);
            this.tabPage3.Controls.Add(this.m_roomTextTextBox);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.m_changeRoomBorderButton);
            this.tabPage3.Controls.Add(this.m_roomBorderTextBox);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.m_changeRoomFillButton);
            this.tabPage3.Controls.Add(this.m_roomFillTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(400, 202);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Colors";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Bottom",
            "BottomRight",
            "Right",
            "TopRight"});
            this.comboBox1.Location = new System.Drawing.Point(81, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Second Fill";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // m_changeSecondFillButton
            // 
            this.m_changeSecondFillButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeSecondFillButton.Location = new System.Drawing.Point(319, 40);
            this.m_changeSecondFillButton.Name = "m_changeSecondFillButton";
            this.m_changeSecondFillButton.Size = new System.Drawing.Size(75, 23);
            this.m_changeSecondFillButton.TabIndex = 19;
            this.m_changeSecondFillButton.Text = "Ch&ange...";
            this.m_changeSecondFillButton.UseVisualStyleBackColor = true;
            this.m_changeSecondFillButton.Click += new System.EventHandler(this.m_changeSecondFillButton_Click);
            // 
            // m_secondFillTextBox
            // 
            this.m_secondFillTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_secondFillTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_secondFillTextBox.Enabled = false;
            this.m_secondFillTextBox.Location = new System.Drawing.Point(208, 42);
            this.m_secondFillTextBox.Name = "m_secondFillTextBox";
            this.m_secondFillTextBox.ReadOnly = true;
            this.m_secondFillTextBox.Size = new System.Drawing.Size(105, 21);
            this.m_secondFillTextBox.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Object Text";
            // 
            // m_changeObjectTextButton
            // 
            this.m_changeObjectTextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeObjectTextButton.Location = new System.Drawing.Point(319, 127);
            this.m_changeObjectTextButton.Name = "m_changeObjectTextButton";
            this.m_changeObjectTextButton.Size = new System.Drawing.Size(75, 23);
            this.m_changeObjectTextButton.TabIndex = 16;
            this.m_changeObjectTextButton.Text = "Ch&ange...";
            this.m_changeObjectTextButton.UseVisualStyleBackColor = true;
            this.m_changeObjectTextButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // m_objectTextTextBox
            // 
            this.m_objectTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_objectTextTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_objectTextTextBox.Enabled = false;
            this.m_objectTextTextBox.Location = new System.Drawing.Point(81, 129);
            this.m_objectTextTextBox.Name = "m_objectTextTextBox";
            this.m_objectTextTextBox.ReadOnly = true;
            this.m_objectTextTextBox.Size = new System.Drawing.Size(232, 21);
            this.m_objectTextTextBox.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Room Text";
            // 
            // m_changeRoomTextButton
            // 
            this.m_changeRoomTextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeRoomTextButton.Location = new System.Drawing.Point(319, 98);
            this.m_changeRoomTextButton.Name = "m_changeRoomTextButton";
            this.m_changeRoomTextButton.Size = new System.Drawing.Size(75, 23);
            this.m_changeRoomTextButton.TabIndex = 13;
            this.m_changeRoomTextButton.Text = "Ch&ange...";
            this.m_changeRoomTextButton.UseVisualStyleBackColor = true;
            this.m_changeRoomTextButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // m_roomTextTextBox
            // 
            this.m_roomTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_roomTextTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_roomTextTextBox.Enabled = false;
            this.m_roomTextTextBox.Location = new System.Drawing.Point(81, 100);
            this.m_roomTextTextBox.Name = "m_roomTextTextBox";
            this.m_roomTextTextBox.ReadOnly = true;
            this.m_roomTextTextBox.Size = new System.Drawing.Size(232, 21);
            this.m_roomTextTextBox.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Room Border";
            // 
            // m_changeRoomBorderButton
            // 
            this.m_changeRoomBorderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeRoomBorderButton.Location = new System.Drawing.Point(319, 69);
            this.m_changeRoomBorderButton.Name = "m_changeRoomBorderButton";
            this.m_changeRoomBorderButton.Size = new System.Drawing.Size(75, 23);
            this.m_changeRoomBorderButton.TabIndex = 10;
            this.m_changeRoomBorderButton.Text = "Ch&ange...";
            this.m_changeRoomBorderButton.UseVisualStyleBackColor = true;
            this.m_changeRoomBorderButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // m_roomBorderTextBox
            // 
            this.m_roomBorderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_roomBorderTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_roomBorderTextBox.Enabled = false;
            this.m_roomBorderTextBox.Location = new System.Drawing.Point(81, 71);
            this.m_roomBorderTextBox.Name = "m_roomBorderTextBox";
            this.m_roomBorderTextBox.ReadOnly = true;
            this.m_roomBorderTextBox.Size = new System.Drawing.Size(232, 21);
            this.m_roomBorderTextBox.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Room Fill";
            // 
            // m_changeRoomFillButton
            // 
            this.m_changeRoomFillButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_changeRoomFillButton.Location = new System.Drawing.Point(319, 11);
            this.m_changeRoomFillButton.Name = "m_changeRoomFillButton";
            this.m_changeRoomFillButton.Size = new System.Drawing.Size(75, 23);
            this.m_changeRoomFillButton.TabIndex = 7;
            this.m_changeRoomFillButton.Text = "Ch&ange...";
            this.m_changeRoomFillButton.UseVisualStyleBackColor = true;
            this.m_changeRoomFillButton.Click += new System.EventHandler(this.m_changeLargeFontButton_Click);
            // 
            // m_roomFillTextBox
            // 
            this.m_roomFillTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_roomFillTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_roomFillTextBox.Enabled = false;
            this.m_roomFillTextBox.Location = new System.Drawing.Point(81, 13);
            this.m_roomFillTextBox.Name = "m_roomFillTextBox";
            this.m_roomFillTextBox.ReadOnly = true;
            this.m_roomFillTextBox.Size = new System.Drawing.Size(232, 21);
            this.m_roomFillTextBox.TabIndex = 6;
            this.m_roomFillTextBox.TextChanged += new System.EventHandler(this.m_roomFillTextBox_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cboRegion);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(400, 202);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Regions";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cboRegion
            // 
            this.cboRegion.FormattingEnabled = true;
            this.cboRegion.Items.AddRange(new object[] {
            "Bottom",
            "BottomRight",
            "Right",
            "TopRight"});
            this.cboRegion.Location = new System.Drawing.Point(69, 11);
            this.cboRegion.Name = "cboRegion";
            this.cboRegion.Size = new System.Drawing.Size(186, 21);
            this.cboRegion.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Region: ";
            // 
            // RoomPropertiesDialog
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(435, 352);
            this.Controls.Add(this.m_tabControl);
            this.Controls.Add(this.m_isDarkCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_nameTextBox);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_okButton);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(265, 295);
            this.Name = "RoomPropertiesDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Room Properties";
            this.m_tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.m_objectsPositionGroupBox.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.TextBox m_nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox m_isDarkCheckBox;
        private System.Windows.Forms.TabControl m_tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox m_objectsTextBox;
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox m_descriptionTextBox;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button m_changeObjectTextButton;
        private System.Windows.Forms.TextBox m_objectTextTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button m_changeRoomTextButton;
        private System.Windows.Forms.TextBox m_roomTextTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button m_changeRoomBorderButton;
        private System.Windows.Forms.TextBox m_roomBorderTextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button m_changeRoomFillButton;
        private System.Windows.Forms.TextBox m_roomFillTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button m_changeSecondFillButton;
        private System.Windows.Forms.TextBox m_secondFillTextBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox cboRegion;
        private System.Windows.Forms.Label label6;
    }
}
