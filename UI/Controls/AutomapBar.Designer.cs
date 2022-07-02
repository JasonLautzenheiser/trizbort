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

namespace Trizbort.UI.Controls
{
  sealed partial class AutomapBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_statusLabel = new System.Windows.Forms.Label();
            this.m_stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_statusLabel
            // 
            this.m_statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_statusLabel.AutoEllipsis = true;
            this.m_statusLabel.Location = new System.Drawing.Point(3, 7);
            this.m_statusLabel.Name = "m_statusLabel";
            this.m_statusLabel.Size = new System.Drawing.Size(233, 15);
            this.m_statusLabel.TabIndex = 0;
            this.m_statusLabel.Text = "(Status)";
            // 
            // m_stopButton
            // 
            this.m_stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_stopButton.BackColor = System.Drawing.SystemColors.Control;
            this.m_stopButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.m_stopButton.Location = new System.Drawing.Point(241, 2);
            this.m_stopButton.Name = "m_stopButton";
            this.m_stopButton.Size = new System.Drawing.Size(75, 23);
            this.m_stopButton.TabIndex = 1;
            this.m_stopButton.TabStop = false;
            this.m_stopButton.Text = "&Stop";
            this.m_stopButton.UseVisualStyleBackColor = false;
            this.m_stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // AutomapBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.m_stopButton);
            this.Controls.Add(this.m_statusLabel);
            this.ForeColor = System.Drawing.SystemColors.InfoText;
            this.MaximumSize = new System.Drawing.Size(4096, 29);
            this.MinimumSize = new System.Drawing.Size(2, 29);
            this.Name = "AutomapBar";
            this.Size = new System.Drawing.Size(320, 27);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label m_statusLabel;
        private System.Windows.Forms.Button m_stopButton;
    }
}
