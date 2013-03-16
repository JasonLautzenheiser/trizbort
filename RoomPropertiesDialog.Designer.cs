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
            this.m_tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.m_objectsPositionGroupBox.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_okButton.Location = new System.Drawing.Point(267, 282);
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
            this.m_cancelButton.Location = new System.Drawing.Point(348, 282);
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
            this.m_tabControl.Location = new System.Drawing.Point(15, 81);
            this.m_tabControl.Name = "m_tabControl";
            this.m_tabControl.SelectedIndex = 0;
            this.m_tabControl.Size = new System.Drawing.Size(408, 195);
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
            this.tabPage1.Size = new System.Drawing.Size(400, 169);
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
            this.m_objectsTextBox.Size = new System.Drawing.Size(288, 160);
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
            this.tabPage2.Size = new System.Drawing.Size(400, 169);
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
            this.m_descriptionTextBox.Size = new System.Drawing.Size(395, 160);
            this.m_descriptionTextBox.TabIndex = 3;
            // 
            // RoomPropertiesDialog
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(435, 317);
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
    }
}
