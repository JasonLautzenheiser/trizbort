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
    partial class Canvas
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_vScrollBar = new System.Windows.Forms.VScrollBar();
            this.m_hScrollBar = new System.Windows.Forms.HScrollBar();
            this.m_cornerPanel = new System.Windows.Forms.Panel();
            this.m_minimap = new Trizbort.Minimap();
            this.SuspendLayout();
            // 
            // m_vScrollBar
            // 
            this.m_vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_vScrollBar.Location = new System.Drawing.Point(403, 128);
            this.m_vScrollBar.Name = "m_vScrollBar";
            this.m_vScrollBar.Size = new System.Drawing.Size(16, 191);
            this.m_vScrollBar.TabIndex = 0;
            this.m_vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // m_hScrollBar
            // 
            this.m_hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hScrollBar.Location = new System.Drawing.Point(0, 319);
            this.m_hScrollBar.Name = "m_hScrollBar";
            this.m_hScrollBar.Size = new System.Drawing.Size(403, 16);
            this.m_hScrollBar.TabIndex = 1;
            this.m_hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // m_cornerPanel
            // 
            this.m_cornerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cornerPanel.Location = new System.Drawing.Point(403, 319);
            this.m_cornerPanel.Name = "m_cornerPanel";
            this.m_cornerPanel.Size = new System.Drawing.Size(16, 16);
            this.m_cornerPanel.TabIndex = 2;
            // 
            // m_minimap
            // 
            this.m_minimap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_minimap.BackColor = System.Drawing.Color.White;
            this.m_minimap.Canvas = null;
            this.m_minimap.Location = new System.Drawing.Point(222, 0);
            this.m_minimap.Name = "m_minimap";
            this.m_minimap.Size = new System.Drawing.Size(197, 128);
            this.m_minimap.TabIndex = 6;
            this.m_minimap.TabStop = false;
            // 
            // Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_minimap);
            this.Controls.Add(this.m_cornerPanel);
            this.Controls.Add(this.m_hScrollBar);
            this.Controls.Add(this.m_vScrollBar);
            this.Name = "Canvas";
            this.Size = new System.Drawing.Size(419, 335);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar m_vScrollBar;
        private System.Windows.Forms.HScrollBar m_hScrollBar;
        private System.Windows.Forms.Panel m_cornerPanel;
        private Minimap m_minimap;
    }
}
