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
  public sealed partial class Canvas
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
            this.addRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.sendToBackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bringToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_lineStylesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_plainLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.m_toggleDottedLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_toggleDirectionalLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.m_upLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_downLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.m_inLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_outLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_reverseLineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.startRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.regionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.roomShapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.handDrawnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ellipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roundedEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.octagonalEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.swapObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formatsFillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.roomPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mapSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblZoom = new System.Windows.Forms.Label();
            this.m_minimap = new Trizbort.UI.Controls.Minimap();
            this.trizbortToolTip1 = new Trizbort.UI.Controls.TrizbortToolTip();
            this.ctxCanvasMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_vScrollBar
            // 
            this.m_vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_vScrollBar.Location = new System.Drawing.Point(806, 256);
            this.m_vScrollBar.Name = "m_vScrollBar";
            this.m_vScrollBar.Size = new System.Drawing.Size(32, 382);
            this.m_vScrollBar.TabIndex = 0;
            this.m_vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // m_hScrollBar
            // 
            this.m_hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_hScrollBar.Location = new System.Drawing.Point(0, 638);
            this.m_hScrollBar.Name = "m_hScrollBar";
            this.m_hScrollBar.Size = new System.Drawing.Size(806, 32);
            this.m_hScrollBar.TabIndex = 1;
            this.m_hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ScrollBar_Scroll);
            // 
            // m_cornerPanel
            // 
            this.m_cornerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cornerPanel.Location = new System.Drawing.Point(806, 638);
            this.m_cornerPanel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_cornerPanel.Name = "m_cornerPanel";
            this.m_cornerPanel.Size = new System.Drawing.Size(32, 32);
            this.m_cornerPanel.TabIndex = 2;
            // 
            // ctxCanvasMenu
            // 
            this.ctxCanvasMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRoomToolStripMenuItem,
            this.toolStripSeparator7,
            this.sendToBackToolStripMenuItem,
            this.bringToFrontToolStripMenuItem,
            this.toolStripSeparator3,
            this.m_lineStylesMenuItem,
            this.m_reverseLineMenuItem,
            this.renameToolStripMenuItem,
            this.darkToolStripMenuItem,
            this.toolStripSeparator6,
            this.startRoomToolStripMenuItem,
            this.endRoomToolStripMenuItem,
            this.toolStripMenuItem1,
            this.regionToolStripMenuItem,
            this.toolStripMenuItem2,
            this.roomShapeToolStripMenuItem,
            this.joinRoomsToolStripMenuItem,
            this.swapObjectsToolStripMenuItem,
            this.toolStripSeparator1,
            this.roomPropertiesToolStripMenuItem,
            this.toolStripSeparator2,
            this.mapSettingsToolStripMenuItem,
            this.applicationSettingsToolStripMenuItem});
            this.ctxCanvasMenu.Name = "ctxCanvasMenu";
            this.ctxCanvasMenu.Size = new System.Drawing.Size(319, 654);
            this.ctxCanvasMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxCanvasMenu_Opening);
            // 
            // addRoomToolStripMenuItem
            // 
            this.addRoomToolStripMenuItem.Name = "addRoomToolStripMenuItem";
            this.addRoomToolStripMenuItem.ShortcutKeyDisplayString = "R";
            this.addRoomToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.addRoomToolStripMenuItem.Text = "Add &Room";
            this.addRoomToolStripMenuItem.Click += new System.EventHandler(this.addRoomToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(315, 6);
            // 
            // sendToBackToolStripMenuItem
            // 
            this.sendToBackToolStripMenuItem.Name = "sendToBackToolStripMenuItem";
            this.sendToBackToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.sendToBackToolStripMenuItem.Text = "Send to Back";
            this.sendToBackToolStripMenuItem.Click += new System.EventHandler(this.sendToBackToolStripMenuItem_Click);
            // 
            // bringToFrontToolStripMenuItem
            // 
            this.bringToFrontToolStripMenuItem.Name = "bringToFrontToolStripMenuItem";
            this.bringToFrontToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.bringToFrontToolStripMenuItem.Text = "Bring to Front";
            this.bringToFrontToolStripMenuItem.Click += new System.EventHandler(this.bringToFrontToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(315, 6);
            // 
            // m_lineStylesMenuItem
            // 
            this.m_lineStylesMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_plainLinesMenuItem,
            this.toolStripMenuItem3,
            this.m_toggleDottedLinesMenuItem,
            this.m_toggleDirectionalLinesMenuItem,
            this.toolStripSeparator4,
            this.m_upLinesMenuItem,
            this.m_downLinesMenuItem,
            this.toolStripSeparator5,
            this.m_inLinesMenuItem,
            this.m_outLinesMenuItem});
            this.m_lineStylesMenuItem.Name = "m_lineStylesMenuItem";
            this.m_lineStylesMenuItem.ShortcutKeyDisplayString = "";
            this.m_lineStylesMenuItem.Size = new System.Drawing.Size(318, 38);
            this.m_lineStylesMenuItem.Text = "&Line Styles";
            // 
            // m_plainLinesMenuItem
            // 
            this.m_plainLinesMenuItem.Name = "m_plainLinesMenuItem";
            this.m_plainLinesMenuItem.ShortcutKeyDisplayString = "P";
            this.m_plainLinesMenuItem.Size = new System.Drawing.Size(345, 44);
            this.m_plainLinesMenuItem.Text = "Plain";
            this.m_plainLinesMenuItem.Click += new System.EventHandler(this.m_plainLinesMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(342, 6);
            // 
            // m_toggleDottedLinesMenuItem
            // 
            this.m_toggleDottedLinesMenuItem.Name = "m_toggleDottedLinesMenuItem";
            this.m_toggleDottedLinesMenuItem.ShortcutKeyDisplayString = "T";
            this.m_toggleDottedLinesMenuItem.Size = new System.Drawing.Size(345, 44);
            this.m_toggleDottedLinesMenuItem.Text = "Dotted";
            this.m_toggleDottedLinesMenuItem.Click += new System.EventHandler(this.m_toggleDottedLinesMenuItem_Click);
            // 
            // m_toggleDirectionalLinesMenuItem
            // 
            this.m_toggleDirectionalLinesMenuItem.Name = "m_toggleDirectionalLinesMenuItem";
            this.m_toggleDirectionalLinesMenuItem.ShortcutKeyDisplayString = "A";
            this.m_toggleDirectionalLinesMenuItem.Size = new System.Drawing.Size(345, 44);
            this.m_toggleDirectionalLinesMenuItem.Text = "One Way Arrow";
            this.m_toggleDirectionalLinesMenuItem.Click += new System.EventHandler(this.m_toggleDirectionalLinesMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(342, 6);
            // 
            // m_upLinesMenuItem
            // 
            this.m_upLinesMenuItem.Name = "m_upLinesMenuItem";
            this.m_upLinesMenuItem.ShortcutKeyDisplayString = "U";
            this.m_upLinesMenuItem.Size = new System.Drawing.Size(345, 44);
            this.m_upLinesMenuItem.Text = "Up";
            this.m_upLinesMenuItem.Click += new System.EventHandler(this.m_upLinesMenuItem_Click);
            // 
            // m_downLinesMenuItem
            // 
            this.m_downLinesMenuItem.Name = "m_downLinesMenuItem";
            this.m_downLinesMenuItem.ShortcutKeyDisplayString = "D";
            this.m_downLinesMenuItem.Size = new System.Drawing.Size(345, 44);
            this.m_downLinesMenuItem.Text = "Down";
            this.m_downLinesMenuItem.Click += new System.EventHandler(this.m_downLinesMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(342, 6);
            // 
            // m_inLinesMenuItem
            // 
            this.m_inLinesMenuItem.Name = "m_inLinesMenuItem";
            this.m_inLinesMenuItem.ShortcutKeyDisplayString = "I";
            this.m_inLinesMenuItem.Size = new System.Drawing.Size(345, 44);
            this.m_inLinesMenuItem.Text = "In";
            this.m_inLinesMenuItem.Click += new System.EventHandler(this.m_inLinesMenuItem_Click);
            // 
            // m_outLinesMenuItem
            // 
            this.m_outLinesMenuItem.Name = "m_outLinesMenuItem";
            this.m_outLinesMenuItem.ShortcutKeyDisplayString = "O";
            this.m_outLinesMenuItem.Size = new System.Drawing.Size(345, 44);
            this.m_outLinesMenuItem.Text = "Out";
            this.m_outLinesMenuItem.Click += new System.EventHandler(this.m_outLinesMenuItem_Click);
            // 
            // m_reverseLineMenuItem
            // 
            this.m_reverseLineMenuItem.Name = "m_reverseLineMenuItem";
            this.m_reverseLineMenuItem.ShortcutKeyDisplayString = "V";
            this.m_reverseLineMenuItem.Size = new System.Drawing.Size(318, 38);
            this.m_reverseLineMenuItem.Text = "Reverse Line";
            this.m_reverseLineMenuItem.Click += new System.EventHandler(this.m_reverseLineMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.ShortcutKeyDisplayString = "F2";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Visible = false;
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.ShortcutKeyDisplayString = "K";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.darkToolStripMenuItem.Text = "Toggle &Darkness";
            this.darkToolStripMenuItem.Visible = false;
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(315, 6);
            // 
            // startRoomToolStripMenuItem
            // 
            this.startRoomToolStripMenuItem.Name = "startRoomToolStripMenuItem";
            this.startRoomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.startRoomToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.startRoomToolStripMenuItem.Text = "Start Room";
            this.startRoomToolStripMenuItem.Click += new System.EventHandler(this.startRoomToolStripMenuItem_Click);
            // 
            // endRoomToolStripMenuItem
            // 
            this.endRoomToolStripMenuItem.Name = "endRoomToolStripMenuItem";
            this.endRoomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.endRoomToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.endRoomToolStripMenuItem.Text = "End Room";
            this.endRoomToolStripMenuItem.Click += new System.EventHandler(this.endRoomToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(315, 6);
            this.toolStripMenuItem1.Visible = false;
            // 
            // regionToolStripMenuItem
            // 
            this.regionToolStripMenuItem.Name = "regionToolStripMenuItem";
            this.regionToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.regionToolStripMenuItem.Text = "&Set Region";
            this.regionToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(315, 6);
            this.toolStripMenuItem2.Visible = false;
            // 
            // roomShapeToolStripMenuItem
            // 
            this.roomShapeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.handDrawnToolStripMenuItem,
            this.ellipseToolStripMenuItem,
            this.roundedEdgesToolStripMenuItem,
            this.octagonalEdgesToolStripMenuItem});
            this.roomShapeToolStripMenuItem.Name = "roomShapeToolStripMenuItem";
            this.roomShapeToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.roomShapeToolStripMenuItem.Text = "Room Shape";
            this.roomShapeToolStripMenuItem.Visible = false;
            // 
            // handDrawnToolStripMenuItem
            // 
            this.handDrawnToolStripMenuItem.Name = "handDrawnToolStripMenuItem";
            this.handDrawnToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+H";
            this.handDrawnToolStripMenuItem.Size = new System.Drawing.Size(409, 44);
            this.handDrawnToolStripMenuItem.Text = "Square Corners";
            this.handDrawnToolStripMenuItem.Click += new System.EventHandler(this.handDrawnToolStripMenuItem_Click);
            // 
            // ellipseToolStripMenuItem
            // 
            this.ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem";
            this.ellipseToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+E";
            this.ellipseToolStripMenuItem.Size = new System.Drawing.Size(409, 44);
            this.ellipseToolStripMenuItem.Text = "Ellipse";
            this.ellipseToolStripMenuItem.Click += new System.EventHandler(this.ellipseToolStripMenuItem_Click);
            // 
            // roundedEdgesToolStripMenuItem
            // 
            this.roundedEdgesToolStripMenuItem.Name = "roundedEdgesToolStripMenuItem";
            this.roundedEdgesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+R";
            this.roundedEdgesToolStripMenuItem.Size = new System.Drawing.Size(409, 44);
            this.roundedEdgesToolStripMenuItem.Text = "Rounded Edges";
            this.roundedEdgesToolStripMenuItem.Click += new System.EventHandler(this.roundedEdgesToolStripMenuItem_Click);
            // 
            // octagonalEdgesToolStripMenuItem
            // 
            this.octagonalEdgesToolStripMenuItem.Name = "octagonalEdgesToolStripMenuItem";
            this.octagonalEdgesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+8";
            this.octagonalEdgesToolStripMenuItem.Size = new System.Drawing.Size(409, 44);
            this.octagonalEdgesToolStripMenuItem.Text = "Octagonal Edges";
            this.octagonalEdgesToolStripMenuItem.Click += new System.EventHandler(this.octagonalEdgesToolStripMenuItem_Click);
            // 
            // joinRoomsToolStripMenuItem
            // 
            this.joinRoomsToolStripMenuItem.Name = "joinRoomsToolStripMenuItem";
            this.joinRoomsToolStripMenuItem.ShortcutKeyDisplayString = "J";
            this.joinRoomsToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.joinRoomsToolStripMenuItem.Text = "&Join Rooms";
            this.joinRoomsToolStripMenuItem.Visible = false;
            this.joinRoomsToolStripMenuItem.Click += new System.EventHandler(this.joinRoomsToolStripMenuItem_Click);
            // 
            // swapObjectsToolStripMenuItem
            // 
            this.swapObjectsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectsToolStripMenuItem,
            this.namesToolStripMenuItem,
            this.formatsFillsToolStripMenuItem,
            this.regionsToolStripMenuItem});
            this.swapObjectsToolStripMenuItem.Name = "swapObjectsToolStripMenuItem";
            this.swapObjectsToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.swapObjectsToolStripMenuItem.Text = "S&wap";
            this.swapObjectsToolStripMenuItem.Visible = false;
            // 
            // objectsToolStripMenuItem
            // 
            this.objectsToolStripMenuItem.Name = "objectsToolStripMenuItem";
            this.objectsToolStripMenuItem.ShortcutKeyDisplayString = "W";
            this.objectsToolStripMenuItem.Size = new System.Drawing.Size(399, 44);
            this.objectsToolStripMenuItem.Text = "Objects";
            this.objectsToolStripMenuItem.Click += new System.EventHandler(this.objectsToolStripMenuItem_Click);
            // 
            // namesToolStripMenuItem
            // 
            this.namesToolStripMenuItem.Name = "namesToolStripMenuItem";
            this.namesToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+W";
            this.namesToolStripMenuItem.Size = new System.Drawing.Size(399, 44);
            this.namesToolStripMenuItem.Text = "Names";
            this.namesToolStripMenuItem.Click += new System.EventHandler(this.namesToolStripMenuItem_Click);
            // 
            // formatsFillsToolStripMenuItem
            // 
            this.formatsFillsToolStripMenuItem.Name = "formatsFillsToolStripMenuItem";
            this.formatsFillsToolStripMenuItem.ShortcutKeyDisplayString = "Shift+W";
            this.formatsFillsToolStripMenuItem.Size = new System.Drawing.Size(399, 44);
            this.formatsFillsToolStripMenuItem.Text = "Formats / Fills";
            this.formatsFillsToolStripMenuItem.Click += new System.EventHandler(this.formatsFillsToolStripMenuItem_Click);
            // 
            // regionsToolStripMenuItem
            // 
            this.regionsToolStripMenuItem.Name = "regionsToolStripMenuItem";
            this.regionsToolStripMenuItem.ShortcutKeyDisplayString = "Alt+W";
            this.regionsToolStripMenuItem.Size = new System.Drawing.Size(399, 44);
            this.regionsToolStripMenuItem.Text = "Regions";
            this.regionsToolStripMenuItem.Click += new System.EventHandler(this.regionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(315, 6);
            this.toolStripSeparator1.Visible = false;
            // 
            // roomPropertiesToolStripMenuItem
            // 
            this.roomPropertiesToolStripMenuItem.Name = "roomPropertiesToolStripMenuItem";
            this.roomPropertiesToolStripMenuItem.ShortcutKeyDisplayString = "Enter";
            this.roomPropertiesToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.roomPropertiesToolStripMenuItem.Text = "&Properties...";
            this.roomPropertiesToolStripMenuItem.Visible = false;
            this.roomPropertiesToolStripMenuItem.Click += new System.EventHandler(this.roomPropertiesToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(315, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // mapSettingsToolStripMenuItem
            // 
            this.mapSettingsToolStripMenuItem.Name = "mapSettingsToolStripMenuItem";
            this.mapSettingsToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.mapSettingsToolStripMenuItem.Text = "&Map Settings...";
            this.mapSettingsToolStripMenuItem.Click += new System.EventHandler(this.mapSettingsToolStripMenuItem_Click);
            // 
            // applicationSettingsToolStripMenuItem
            // 
            this.applicationSettingsToolStripMenuItem.Name = "applicationSettingsToolStripMenuItem";
            this.applicationSettingsToolStripMenuItem.Size = new System.Drawing.Size(318, 38);
            this.applicationSettingsToolStripMenuItem.Text = "&Application Settings...";
            this.applicationSettingsToolStripMenuItem.Click += new System.EventHandler(this.applicationSettingsToolStripMenuItem_Click);
            // 
            // lblZoom
            // 
            this.lblZoom.AutoSize = true;
            this.lblZoom.BackColor = System.Drawing.Color.Transparent;
            this.lblZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZoom.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblZoom.Location = new System.Drawing.Point(8, 8);
            this.lblZoom.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblZoom.Name = "lblZoom";
            this.lblZoom.Size = new System.Drawing.Size(100, 37);
            this.lblZoom.TabIndex = 7;
            this.lblZoom.Text = "Zoom";
            // 
            // m_minimap
            // 
            this.m_minimap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_minimap.AutoSize = true;
            this.m_minimap.BackColor = System.Drawing.Color.White;
            this.m_minimap.Canvas = null;
            this.m_minimap.Location = new System.Drawing.Point(444, 0);
            this.m_minimap.Margin = new System.Windows.Forms.Padding(12, 12, 12, 12);
            this.m_minimap.Name = "m_minimap";
            this.m_minimap.Size = new System.Drawing.Size(394, 256);
            this.m_minimap.TabIndex = 6;
            this.m_minimap.TabStop = false;
            // 
            // trizbortToolTip1
            // 
            this.trizbortToolTip1.BackColor = System.Drawing.Color.LightBlue;
            this.trizbortToolTip1.BodyText = null;
            this.trizbortToolTip1.FooterText = null;
            this.trizbortToolTip1.ForeColor = System.Drawing.Color.Black;
            this.trizbortToolTip1.GradientColor = System.Drawing.Color.Empty;
            this.trizbortToolTip1.HoverElement = null;
            this.trizbortToolTip1.IsShown = false;
            this.trizbortToolTip1.LastOwner = null;
            this.trizbortToolTip1.OwnerDraw = true;
            this.trizbortToolTip1.TitleText = null;
            // 
            // Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ContextMenuStrip = this.ctxCanvasMenu;
            this.Controls.Add(this.lblZoom);
            this.Controls.Add(this.m_minimap);
            this.Controls.Add(this.m_cornerPanel);
            this.Controls.Add(this.m_hScrollBar);
            this.Controls.Add(this.m_vScrollBar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Canvas";
            this.Size = new System.Drawing.Size(838, 670);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem roomPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mapSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinRoomsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem swapObjectsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem objectsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem namesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem formatsFillsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem regionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem roomShapeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem handDrawnToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ellipseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem roundedEdgesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem octagonalEdgesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem addRoomToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem m_lineStylesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_plainLinesMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem m_toggleDottedLinesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_toggleDirectionalLinesMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem m_upLinesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_downLinesMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem m_inLinesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_outLinesMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_reverseLineMenuItem;
    private System.Windows.Forms.ToolStripMenuItem startRoomToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem endRoomToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripMenuItem sendToBackToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem bringToFrontToolStripMenuItem;
    private TrizbortToolTip trizbortToolTip1;
  }
}
