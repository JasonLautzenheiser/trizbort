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
    partial class DisambiguateRoomsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisambiguateRoomsDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_transcriptContextTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_roomNamesListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.m_roomDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.m_thisRoomButton = new System.Windows.Forms.Button();
            this.m_newRoomButton = new System.Windows.Forms.Button();
            this.m_AutoChooseTheRestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(962, 114);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trizbort isn\'t sure which room the transcript is referring to.\r\n\r\nEither several " +
    "rooms have the same name but different descriptions, or the room description has" +
    " changed during play.\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 153);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(282, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "When the transcript says:";
            // 
            // m_transcriptContextTextBox
            // 
            this.m_transcriptContextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_transcriptContextTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_transcriptContextTextBox.Location = new System.Drawing.Point(86, 216);
            this.m_transcriptContextTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_transcriptContextTextBox.Multiline = true;
            this.m_transcriptContextTextBox.Name = "m_transcriptContextTextBox";
            this.m_transcriptContextTextBox.ReadOnly = true;
            this.m_transcriptContextTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_transcriptContextTextBox.Size = new System.Drawing.Size(898, 172);
            this.m_transcriptContextTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 416);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(303, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "Which room does it mean?";
            // 
            // m_roomNamesListBox
            // 
            this.m_roomNamesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.m_roomNamesListBox.FormattingEnabled = true;
            this.m_roomNamesListBox.ItemHeight = 32;
            this.m_roomNamesListBox.Location = new System.Drawing.Point(86, 518);
            this.m_roomNamesListBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_roomNamesListBox.Name = "m_roomNamesListBox";
            this.m_roomNamesListBox.Size = new System.Drawing.Size(318, 164);
            this.m_roomNamesListBox.TabIndex = 5;
            this.m_roomNamesListBox.SelectedIndexChanged += new System.EventHandler(this.RoomNamesListBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 480);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 32);
            this.label4.TabIndex = 4;
            this.label4.Text = "&Room";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(414, 480);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 32);
            this.label5.TabIndex = 6;
            this.label5.Text = "&Description";
            // 
            // m_roomDescriptionTextBox
            // 
            this.m_roomDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_roomDescriptionTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.m_roomDescriptionTextBox.Location = new System.Drawing.Point(420, 518);
            this.m_roomDescriptionTextBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_roomDescriptionTextBox.Multiline = true;
            this.m_roomDescriptionTextBox.Name = "m_roomDescriptionTextBox";
            this.m_roomDescriptionTextBox.ReadOnly = true;
            this.m_roomDescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.m_roomDescriptionTextBox.Size = new System.Drawing.Size(564, 164);
            this.m_roomDescriptionTextBox.TabIndex = 7;
            // 
            // m_thisRoomButton
            // 
            this.m_thisRoomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_thisRoomButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.m_thisRoomButton.Location = new System.Drawing.Point(130, 724);
            this.m_thisRoomButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_thisRoomButton.Name = "m_thisRoomButton";
            this.m_thisRoomButton.Size = new System.Drawing.Size(278, 48);
            this.m_thisRoomButton.TabIndex = 8;
            this.m_thisRoomButton.Text = "&This Room";
            this.m_thisRoomButton.UseVisualStyleBackColor = true;
            // 
            // m_newRoomButton
            // 
            this.m_newRoomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_newRoomButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.m_newRoomButton.Location = new System.Drawing.Point(420, 724);
            this.m_newRoomButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_newRoomButton.Name = "m_newRoomButton";
            this.m_newRoomButton.Size = new System.Drawing.Size(278, 48);
            this.m_newRoomButton.TabIndex = 9;
            this.m_newRoomButton.Text = "A &New Room";
            this.m_newRoomButton.UseVisualStyleBackColor = true;
            // 
            // m_AutoChooseTheRestButton
            // 
            this.m_AutoChooseTheRestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_AutoChooseTheRestButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.m_AutoChooseTheRestButton.Location = new System.Drawing.Point(710, 724);
            this.m_AutoChooseTheRestButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_AutoChooseTheRestButton.Name = "m_AutoChooseTheRestButton";
            this.m_AutoChooseTheRestButton.Size = new System.Drawing.Size(278, 48);
            this.m_AutoChooseTheRestButton.TabIndex = 10;
            this.m_AutoChooseTheRestButton.Text = "&Auto-Choose the Rest";
            this.m_AutoChooseTheRestButton.UseVisualStyleBackColor = true;
            // 
            // DisambiguateRoomsDialog
            // 
            this.AcceptButton = this.m_thisRoomButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1012, 796);
            this.Controls.Add(this.m_AutoChooseTheRestButton);
            this.Controls.Add(this.m_newRoomButton);
            this.Controls.Add(this.m_thisRoomButton);
            this.Controls.Add(this.m_roomDescriptionTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_roomNamesListBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_transcriptContextTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1002, 779);
            this.Name = "DisambiguateRoomsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Disambiguate Rooms";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_transcriptContextTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox m_roomNamesListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox m_roomDescriptionTextBox;
        private System.Windows.Forms.Button m_thisRoomButton;
        private System.Windows.Forms.Button m_newRoomButton;
        private System.Windows.Forms.Button m_AutoChooseTheRestButton;
    }
}
