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
      this.components = new System.ComponentModel.Container();
      this.m_vScrollBar = new System.Windows.Forms.VScrollBar();
      this.m_hScrollBar = new System.Windows.Forms.HScrollBar();
      this.m_cornerPanel = new System.Windows.Forms.Panel();
      this.ctxCanvasMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.regionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.joinRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.swapObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.roomPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.mapSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.lblZoom = new System.Windows.Forms.Label();
      this.roomTooltip = new DevComponents.DotNetBar.SuperTooltip();
      this.m_minimap = new Trizbort.Minimap();
      this.ctxCanvasMenu.SuspendLayout();
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
      // ctxCanvasMenu
      // 
      this.ctxCanvasMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.regionToolStripMenuItem,
            this.darkToolStripMenuItem,
            this.joinRoomsToolStripMenuItem,
            this.swapObjectsToolStripMenuItem,
            this.toolStripSeparator1,
            this.roomPropertiesToolStripMenuItem,
            this.toolStripSeparator2,
            this.mapSettingsToolStripMenuItem,
            this.applicationSettingsToolStripMenuItem});
      this.ctxCanvasMenu.Name = "ctxCanvasMenu";
      this.ctxCanvasMenu.Size = new System.Drawing.Size(190, 170);
      this.ctxCanvasMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxCanvasMenu_Opening);
      // 
      // regionToolStripMenuItem
      // 
      this.regionToolStripMenuItem.Enabled = false;
      this.regionToolStripMenuItem.Name = "regionToolStripMenuItem";
      this.regionToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.regionToolStripMenuItem.Text = "&Set Region";
      // 
      // darkToolStripMenuItem
      // 
      this.darkToolStripMenuItem.Enabled = false;
      this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
      this.darkToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.darkToolStripMenuItem.Text = "&Dark";
      // 
      // joinRoomsToolStripMenuItem
      // 
      this.joinRoomsToolStripMenuItem.Enabled = false;
      this.joinRoomsToolStripMenuItem.Name = "joinRoomsToolStripMenuItem";
      this.joinRoomsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.joinRoomsToolStripMenuItem.Text = "&Join Rooms";
      this.joinRoomsToolStripMenuItem.Click += new System.EventHandler(this.joinRoomsToolStripMenuItem_Click);
      // 
      // swapObjectsToolStripMenuItem
      // 
      this.swapObjectsToolStripMenuItem.Enabled = false;
      this.swapObjectsToolStripMenuItem.Name = "swapObjectsToolStripMenuItem";
      this.swapObjectsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.swapObjectsToolStripMenuItem.Text = "S&wap Objects";
      this.swapObjectsToolStripMenuItem.Click += new System.EventHandler(this.swapObjectsToolStripMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
      // 
      // roomPropertiesToolStripMenuItem
      // 
      this.roomPropertiesToolStripMenuItem.Enabled = false;
      this.roomPropertiesToolStripMenuItem.Name = "roomPropertiesToolStripMenuItem";
      this.roomPropertiesToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.roomPropertiesToolStripMenuItem.Text = "&Properties...";
      this.roomPropertiesToolStripMenuItem.Click += new System.EventHandler(this.roomPropertiesToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
      // 
      // mapSettingsToolStripMenuItem
      // 
      this.mapSettingsToolStripMenuItem.Name = "mapSettingsToolStripMenuItem";
      this.mapSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.mapSettingsToolStripMenuItem.Text = "&Map Settings...";
      this.mapSettingsToolStripMenuItem.Click += new System.EventHandler(this.mapSettingsToolStripMenuItem_Click);
      // 
      // applicationSettingsToolStripMenuItem
      // 
      this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
      this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
      this.applicationSettingsToolStripMenuItem.Text = "&Application Settings...";
      this.applicationSettingsToolStripMenuItem.Click += new System.EventHandler(this.applicationSettingsToolStripMenuItem_Click);
      // 
      // lblZoom
      // 
      this.lblZoom.AutoSize = true;
      this.lblZoom.BackColor = System.Drawing.Color.Transparent;
      this.lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblZoom.ForeColor = System.Drawing.SystemColors.GrayText;
      this.lblZoom.Location = new System.Drawing.Point(4, 4);
      this.lblZoom.Name = "lblZoom";
      this.lblZoom.Size = new System.Drawing.Size(50, 20);
      this.lblZoom.TabIndex = 7;
      this.lblZoom.Text = "Zoom";
      // 
      // roomTooltip
      // 
      this.roomTooltip.CheckTooltipPosition = false;
      this.roomTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
      this.roomTooltip.PositionBelowControl = false;
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
      this.ContextMenuStrip = this.ctxCanvasMenu;
      this.Controls.Add(this.lblZoom);
      this.Controls.Add(this.m_minimap);
      this.Controls.Add(this.m_cornerPanel);
      this.Controls.Add(this.m_hScrollBar);
      this.Controls.Add(this.m_vScrollBar);
      this.Name = "Canvas";
      this.Size = new System.Drawing.Size(419, 335);
      this.ctxCanvasMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.VScrollBar m_vScrollBar;
        private System.Windows.Forms.HScrollBar m_hScrollBar;
        private System.Windows.Forms.Panel m_cornerPanel;
        private Minimap m_minimap;
        private System.Windows.Forms.ContextMenuStrip ctxCanvasMenu;
        private System.Windows.Forms.ToolStripMenuItem regionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.Label lblZoom;
        private DevComponents.DotNetBar.SuperTooltip roomTooltip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem roomPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mapSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinRoomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem swapObjectsToolStripMenuItem;
    }
}
