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
    partial class ConnectionPropertiesDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionPropertiesDialog));
      this.m_cancelButton = new System.Windows.Forms.Button();
      this.m_okButton = new System.Windows.Forms.Button();
      this.m_oneWayCheckBox = new System.Windows.Forms.CheckBox();
      this.m_dottedCheckBox = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.m_endLabel = new System.Windows.Forms.Label();
      this.m_endTextBox = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.m_middleTextBox = new System.Windows.Forms.TextBox();
      this.m_startTextBox = new System.Windows.Forms.TextBox();
      this.m_customRadioButton = new System.Windows.Forms.RadioButton();
      this.m_oiRadioButton = new System.Windows.Forms.RadioButton();
      this.m_ioRadioButton = new System.Windows.Forms.RadioButton();
      this.m_duRadioButton = new System.Windows.Forms.RadioButton();
      this.m_udRadioButton = new System.Windows.Forms.RadioButton();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.connectionColorBox = new DevComponents.DotNetBar.Controls.TextBoxX();
      this.connectionColorChange = new System.Windows.Forms.Button();
      this.label11 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_cancelButton
      // 
      this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_cancelButton.Location = new System.Drawing.Point(348, 208);
      this.m_cancelButton.Name = "m_cancelButton";
      this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
      this.m_cancelButton.TabIndex = 3;
      this.m_cancelButton.Text = "Cancel";
      this.m_cancelButton.UseVisualStyleBackColor = true;
      // 
      // m_okButton
      // 
      this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_okButton.Location = new System.Drawing.Point(267, 208);
      this.m_okButton.Name = "m_okButton";
      this.m_okButton.Size = new System.Drawing.Size(75, 23);
      this.m_okButton.TabIndex = 2;
      this.m_okButton.Text = "OK";
      this.m_okButton.UseVisualStyleBackColor = true;
      // 
      // m_oneWayCheckBox
      // 
      this.m_oneWayCheckBox.AutoSize = true;
      this.m_oneWayCheckBox.Location = new System.Drawing.Point(9, 44);
      this.m_oneWayCheckBox.Name = "m_oneWayCheckBox";
      this.m_oneWayCheckBox.Size = new System.Drawing.Size(103, 17);
      this.m_oneWayCheckBox.TabIndex = 1;
      this.m_oneWayCheckBox.Text = "One Way &Arrow";
      this.m_oneWayCheckBox.UseVisualStyleBackColor = true;
      // 
      // m_dottedCheckBox
      // 
      this.m_dottedCheckBox.AutoSize = true;
      this.m_dottedCheckBox.Location = new System.Drawing.Point(10, 21);
      this.m_dottedCheckBox.Name = "m_dottedCheckBox";
      this.m_dottedCheckBox.Size = new System.Drawing.Size(59, 17);
      this.m_dottedCheckBox.TabIndex = 0;
      this.m_dottedCheckBox.Text = "&Dotted";
      this.m_dottedCheckBox.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.m_endLabel);
      this.groupBox1.Controls.Add(this.m_endTextBox);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.m_middleTextBox);
      this.groupBox1.Controls.Add(this.m_startTextBox);
      this.groupBox1.Controls.Add(this.m_customRadioButton);
      this.groupBox1.Controls.Add(this.m_oiRadioButton);
      this.groupBox1.Controls.Add(this.m_ioRadioButton);
      this.groupBox1.Controls.Add(this.m_duRadioButton);
      this.groupBox1.Controls.Add(this.m_udRadioButton);
      this.groupBox1.Location = new System.Drawing.Point(12, 90);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(411, 106);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "&Text";
      // 
      // m_endLabel
      // 
      this.m_endLabel.AutoSize = true;
      this.m_endLabel.Location = new System.Drawing.Point(216, 72);
      this.m_endLabel.Name = "m_endLabel";
      this.m_endLabel.Size = new System.Drawing.Size(25, 13);
      this.m_endLabel.TabIndex = 9;
      this.m_endLabel.Text = "&End";
      // 
      // m_endTextBox
      // 
      this.m_endTextBox.Location = new System.Drawing.Point(245, 69);
      this.m_endTextBox.Name = "m_endTextBox";
      this.m_endTextBox.Size = new System.Drawing.Size(156, 21);
      this.m_endTextBox.TabIndex = 10;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(204, 46);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(37, 13);
      this.label2.TabIndex = 7;
      this.label2.Text = "&Middle";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(210, 20);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(31, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "&Start";
      // 
      // m_middleTextBox
      // 
      this.m_middleTextBox.Location = new System.Drawing.Point(245, 43);
      this.m_middleTextBox.Name = "m_middleTextBox";
      this.m_middleTextBox.Size = new System.Drawing.Size(156, 21);
      this.m_middleTextBox.TabIndex = 8;
      // 
      // m_startTextBox
      // 
      this.m_startTextBox.Location = new System.Drawing.Point(245, 17);
      this.m_startTextBox.Name = "m_startTextBox";
      this.m_startTextBox.Size = new System.Drawing.Size(156, 21);
      this.m_startTextBox.TabIndex = 6;
      // 
      // m_customRadioButton
      // 
      this.m_customRadioButton.AutoSize = true;
      this.m_customRadioButton.Checked = true;
      this.m_customRadioButton.Location = new System.Drawing.Point(9, 70);
      this.m_customRadioButton.Name = "m_customRadioButton";
      this.m_customRadioButton.Size = new System.Drawing.Size(61, 17);
      this.m_customRadioButton.TabIndex = 4;
      this.m_customRadioButton.TabStop = true;
      this.m_customRadioButton.Text = "&Custom";
      this.m_customRadioButton.UseVisualStyleBackColor = true;
      this.m_customRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButtonCheckedChanged);
      // 
      // m_oiRadioButton
      // 
      this.m_oiRadioButton.AutoSize = true;
      this.m_oiRadioButton.Location = new System.Drawing.Point(95, 44);
      this.m_oiRadioButton.Name = "m_oiRadioButton";
      this.m_oiRadioButton.Size = new System.Drawing.Size(57, 17);
      this.m_oiRadioButton.TabIndex = 3;
      this.m_oiRadioButton.Text = "&Out/In";
      this.m_oiRadioButton.UseVisualStyleBackColor = true;
      this.m_oiRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButtonCheckedChanged);
      // 
      // m_ioRadioButton
      // 
      this.m_ioRadioButton.AutoSize = true;
      this.m_ioRadioButton.Location = new System.Drawing.Point(95, 18);
      this.m_ioRadioButton.Name = "m_ioRadioButton";
      this.m_ioRadioButton.Size = new System.Drawing.Size(57, 17);
      this.m_ioRadioButton.TabIndex = 2;
      this.m_ioRadioButton.Text = "&In/Out";
      this.m_ioRadioButton.UseVisualStyleBackColor = true;
      this.m_ioRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButtonCheckedChanged);
      // 
      // m_duRadioButton
      // 
      this.m_duRadioButton.AutoSize = true;
      this.m_duRadioButton.Location = new System.Drawing.Point(9, 44);
      this.m_duRadioButton.Name = "m_duRadioButton";
      this.m_duRadioButton.Size = new System.Drawing.Size(69, 17);
      this.m_duRadioButton.TabIndex = 1;
      this.m_duRadioButton.Text = "&Down/Up";
      this.m_duRadioButton.UseVisualStyleBackColor = true;
      this.m_duRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButtonCheckedChanged);
      // 
      // m_udRadioButton
      // 
      this.m_udRadioButton.AutoSize = true;
      this.m_udRadioButton.Location = new System.Drawing.Point(9, 18);
      this.m_udRadioButton.Name = "m_udRadioButton";
      this.m_udRadioButton.Size = new System.Drawing.Size(69, 17);
      this.m_udRadioButton.TabIndex = 0;
      this.m_udRadioButton.Text = "&Up/Down";
      this.m_udRadioButton.UseVisualStyleBackColor = true;
      this.m_udRadioButton.CheckedChanged += new System.EventHandler(this.OnRadioButtonCheckedChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.connectionColorBox);
      this.groupBox2.Controls.Add(this.connectionColorChange);
      this.groupBox2.Controls.Add(this.label11);
      this.groupBox2.Controls.Add(this.m_oneWayCheckBox);
      this.groupBox2.Controls.Add(this.m_dottedCheckBox);
      this.groupBox2.Location = new System.Drawing.Point(12, 10);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(411, 72);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "&Style";
      // 
      // connectionColorBox
      // 
      // 
      // 
      // 
      this.connectionColorBox.Border.Class = "TextBoxBorder";
      this.connectionColorBox.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.connectionColorBox.ButtonCustom.Text = "Clear";
      this.connectionColorBox.ButtonCustom.Visible = true;
      this.connectionColorBox.Cursor = System.Windows.Forms.Cursors.Arrow;
      this.connectionColorBox.Location = new System.Drawing.Point(233, 18);
      this.connectionColorBox.Name = "connectionColorBox";
      this.connectionColorBox.ReadOnly = true;
      this.connectionColorBox.Size = new System.Drawing.Size(133, 21);
      this.connectionColorBox.TabIndex = 24;
      this.connectionColorBox.ButtonCustomClick += new System.EventHandler(this.connectionColorBox_ButtonCustomClick);
      this.connectionColorBox.DoubleClick += new System.EventHandler(this.connectionColorBox_DoubleClick);
      // 
      // connectionColorChange
      // 
      this.connectionColorChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.connectionColorChange.Location = new System.Drawing.Point(372, 17);
      this.connectionColorChange.Name = "connectionColorChange";
      this.connectionColorChange.Size = new System.Drawing.Size(29, 23);
      this.connectionColorChange.TabIndex = 23;
      this.connectionColorChange.Text = "...";
      this.connectionColorChange.UseVisualStyleBackColor = true;
      this.connectionColorChange.Click += new System.EventHandler(this.connectionColorChange_Click);
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.BackColor = System.Drawing.Color.Transparent;
      this.label11.Location = new System.Drawing.Point(195, 22);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(32, 13);
      this.label11.TabIndex = 22;
      this.label11.Text = "Color";
      // 
      // ConnectionPropertiesDialog
      // 
      this.AcceptButton = this.m_okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_cancelButton;
      this.ClientSize = new System.Drawing.Size(435, 243);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.m_cancelButton);
      this.Controls.Add(this.m_okButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ConnectionPropertiesDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Connection Properties";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.CheckBox m_oneWayCheckBox;
        private System.Windows.Forms.CheckBox m_dottedCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton m_customRadioButton;
        private System.Windows.Forms.RadioButton m_oiRadioButton;
        private System.Windows.Forms.RadioButton m_ioRadioButton;
        private System.Windows.Forms.RadioButton m_duRadioButton;
        private System.Windows.Forms.RadioButton m_udRadioButton;
        private System.Windows.Forms.Label m_endLabel;
        private System.Windows.Forms.TextBox m_endTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_middleTextBox;
        private System.Windows.Forms.TextBox m_startTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.Controls.TextBoxX connectionColorBox;
        private System.Windows.Forms.Button connectionColorChange;
        private System.Windows.Forms.Label label11;
    }
}
