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
    partial class AutomapDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutomapDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_textBox = new System.Windows.Forms.TextBox();
            this.m_browseButton = new System.Windows.Forms.Button();
            this.m_startButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.m_singleStepCheckBox = new System.Windows.Forms.CheckBox();
            this.m_roomsWithSameNameAreSameRoomCheckBox = new System.Windows.Forms.CheckBox();
            this.m_verboseTranscriptCheckBox = new System.Windows.Forms.CheckBox();
            this.m_guessExitsCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkAssumeTwoWayConnections = new System.Windows.Forms.CheckBox();
            this.m_startFromEndCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_addRegionCommandTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.m_addObjectCommandTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(372, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trizbort can automatically generate a map from a transcript of a game, even while" +
    " the game is in progress.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "&Transcript File";
            // 
            // m_textBox
            // 
            this.m_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_textBox.Location = new System.Drawing.Point(16, 69);
            this.m_textBox.Name = "m_textBox";
            this.m_textBox.Size = new System.Drawing.Size(378, 23);
            this.m_textBox.TabIndex = 1;
            // 
            // m_browseButton
            // 
            this.m_browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_browseButton.Location = new System.Drawing.Point(399, 69);
            this.m_browseButton.Name = "m_browseButton";
            this.m_browseButton.Size = new System.Drawing.Size(75, 23);
            this.m_browseButton.TabIndex = 2;
            this.m_browseButton.Text = "&Browse...";
            this.m_browseButton.UseVisualStyleBackColor = true;
            this.m_browseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // m_startButton
            // 
            this.m_startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_startButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_startButton.Location = new System.Drawing.Point(278, 367);
            this.m_startButton.Name = "m_startButton";
            this.m_startButton.Size = new System.Drawing.Size(116, 23);
            this.m_startButton.TabIndex = 11;
            this.m_startButton.Text = "Start Automapping";
            this.m_startButton.UseVisualStyleBackColor = true;
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(400, 367);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_cancelButton.TabIndex = 12;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // m_singleStepCheckBox
            // 
            this.m_singleStepCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_singleStepCheckBox.AutoSize = true;
            this.m_singleStepCheckBox.Location = new System.Drawing.Point(16, 369);
            this.m_singleStepCheckBox.Name = "m_singleStepCheckBox";
            this.m_singleStepCheckBox.Size = new System.Drawing.Size(142, 19);
            this.m_singleStepCheckBox.TabIndex = 10;
            this.m_singleStepCheckBox.Text = "&Step with F11 (Debug)";
            this.m_singleStepCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_roomsWithSameNameAreSameRoomCheckBox
            // 
            this.m_roomsWithSameNameAreSameRoomCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_roomsWithSameNameAreSameRoomCheckBox.AutoSize = true;
            this.m_roomsWithSameNameAreSameRoomCheckBox.Location = new System.Drawing.Point(13, 46);
            this.m_roomsWithSameNameAreSameRoomCheckBox.Name = "m_roomsWithSameNameAreSameRoomCheckBox";
            this.m_roomsWithSameNameAreSameRoomCheckBox.Size = new System.Drawing.Size(260, 19);
            this.m_roomsWithSameNameAreSameRoomCheckBox.TabIndex = 4;
            this.m_roomsWithSameNameAreSameRoomCheckBox.Text = "&Assume no two rooms have the same name.";
            this.m_roomsWithSameNameAreSameRoomCheckBox.UseVisualStyleBackColor = true;
            // 
            // m_verboseTranscriptCheckBox
            // 
            this.m_verboseTranscriptCheckBox.AutoSize = true;
            this.m_verboseTranscriptCheckBox.Location = new System.Drawing.Point(13, 22);
            this.m_verboseTranscriptCheckBox.Name = "m_verboseTranscriptCheckBox";
            this.m_verboseTranscriptCheckBox.Size = new System.Drawing.Size(312, 19);
            this.m_verboseTranscriptCheckBox.TabIndex = 3;
            this.m_verboseTranscriptCheckBox.Text = "Treat transcript as &VERBOSE; expect room descriptions.";
            this.m_verboseTranscriptCheckBox.UseVisualStyleBackColor = true;
            this.m_verboseTranscriptCheckBox.CheckedChanged += new System.EventHandler(this.VerboseTranscriptCheckBox_CheckedChanged);
            // 
            // m_guessExitsCheckBox
            // 
            this.m_guessExitsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_guessExitsCheckBox.AutoSize = true;
            this.m_guessExitsCheckBox.Location = new System.Drawing.Point(13, 71);
            this.m_guessExitsCheckBox.Name = "m_guessExitsCheckBox";
            this.m_guessExitsCheckBox.Size = new System.Drawing.Size(371, 19);
            this.m_guessExitsCheckBox.TabIndex = 5;
            this.m_guessExitsCheckBox.Text = "&Guess possible exits from a room by reading its initial description.";
            this.m_guessExitsCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkAssumeTwoWayConnections);
            this.groupBox1.Controls.Add(this.m_startFromEndCheckBox);
            this.groupBox1.Controls.Add(this.m_roomsWithSameNameAreSameRoomCheckBox);
            this.groupBox1.Controls.Add(this.m_verboseTranscriptCheckBox);
            this.groupBox1.Controls.Add(this.m_guessExitsCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(16, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 148);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "&Options";
            // 
            // chkAssumeTwoWayConnections
            // 
            this.chkAssumeTwoWayConnections.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAssumeTwoWayConnections.AutoSize = true;
            this.chkAssumeTwoWayConnections.Location = new System.Drawing.Point(13, 119);
            this.chkAssumeTwoWayConnections.Name = "chkAssumeTwoWayConnections";
            this.chkAssumeTwoWayConnections.Size = new System.Drawing.Size(226, 19);
            this.chkAssumeTwoWayConnections.TabIndex = 7;
            this.chkAssumeTwoWayConnections.Text = "Always assume two-way connections.";
            this.chkAssumeTwoWayConnections.UseVisualStyleBackColor = true;
            // 
            // m_startFromEndCheckBox
            // 
            this.m_startFromEndCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_startFromEndCheckBox.AutoSize = true;
            this.m_startFromEndCheckBox.Location = new System.Drawing.Point(13, 95);
            this.m_startFromEndCheckBox.Name = "m_startFromEndCheckBox";
            this.m_startFromEndCheckBox.Size = new System.Drawing.Size(294, 19);
            this.m_startFromEndCheckBox.TabIndex = 6;
            this.m_startFromEndCheckBox.Text = "&Start processing from the end of the transcript file. ";
            this.m_startFromEndCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.m_addRegionCommandTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.m_addObjectCommandTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(16, 253);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 108);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Commands";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(170, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(262, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "followed by region name to add room to region.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 15);
            this.label7.TabIndex = 9;
            this.label7.Text = ">";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_addRegionCommandTextBox
            // 
            this.m_addRegionCommandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_addRegionCommandTextBox.Location = new System.Drawing.Point(65, 75);
            this.m_addRegionCommandTextBox.Name = "m_addRegionCommandTextBox";
            this.m_addRegionCommandTextBox.Size = new System.Drawing.Size(100, 23);
            this.m_addRegionCommandTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(256, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "followed by an object name adds it to the map.";
            this.label5.Click += new System.EventHandler(this.Label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = ">";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // m_addObjectCommandTextBox
            // 
            this.m_addObjectCommandTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.m_addObjectCommandTextBox.Location = new System.Drawing.Point(65, 47);
            this.m_addObjectCommandTextBox.Name = "m_addObjectCommandTextBox";
            this.m_addObjectCommandTextBox.Size = new System.Drawing.Size(100, 23);
            this.m_addObjectCommandTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "At the in-game prompt:";
            // 
            // AutomapDialog
            // 
            this.AcceptButton = this.m_startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(486, 402);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.m_singleStepCheckBox);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_startButton);
            this.Controls.Add(this.m_browseButton);
            this.Controls.Add(this.m_textBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutomapDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Automapping";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_textBox;
        private System.Windows.Forms.Button m_browseButton;
        private System.Windows.Forms.Button m_startButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.CheckBox m_singleStepCheckBox;
        private System.Windows.Forms.CheckBox m_roomsWithSameNameAreSameRoomCheckBox;
        private System.Windows.Forms.CheckBox m_verboseTranscriptCheckBox;
        private System.Windows.Forms.CheckBox m_guessExitsCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_addObjectCommandTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_addRegionCommandTextBox;
        private System.Windows.Forms.CheckBox m_startFromEndCheckBox;
    private System.Windows.Forms.CheckBox chkAssumeTwoWayConnections;
  }
}
