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

using Trizbort.UI.Controls;

namespace Trizbort.UI
{
  public partial class MainForm
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.m_menuStrip = new System.Windows.Forms.MenuStrip();
      this.m_fileMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileNewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.m_fileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileOpenFromWebMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
      this.m_fileSaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.smartSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
      this.m_fileExportMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileExportPDFMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileExportImageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
      this.m_fileExportAlanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileExportHugoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileExportInform7MenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileExportInform6MenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileExportTADSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zILToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_fileExportQuestMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
      this.alanToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.hugoToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.inform7ToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.inform6ToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tADSToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.zILToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.questToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.questRoomsToTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
      this.m_fileRecentMapsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
      this.m_fileExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_editMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
      this.toggleTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
      this.m_editSelectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_editSelectNoneMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectSpecialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectAllRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectedUnconnectedRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectRoomsWObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectRoomsWoObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
      this.selectAllConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectDanglingConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.selectSelfLoopingConnectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.m_editCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_editCopyColorToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_editPasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
      this.m_editDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.m_editPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.roomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_editAddRoomMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_editRenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_editChangeRegionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
      this.m_editIsDarkMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.makeRoomDarkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.makeRoomLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
      this.startRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.endRoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.roomShapeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.handDrawnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ellipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.roundedEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.octagonalEdgesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
      this.joinRoomsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
      this.swapObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.swapNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.swapFormatsFillsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.swapRegionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.connectionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_lineStylesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_plainLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.m_toggleDottedLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_toggleDirectionalLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.m_upLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_downLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.m_inLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_outLinesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_reverseLineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.validationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.roomsMustHaveUniqueNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.roomsMustHaveADescriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.roomsMustHaveASubtitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.roomsMustNotHaveADanglingConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewZoomMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewZoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewZoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
      this.m_viewZoomFiftyPercentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewZoomOneHundredPercentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewZoomTwoHundredPercentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
      this.m_viewZoomMiniIn = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewZoomMiniOut = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewEntireMapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_viewResetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
      this.m_viewMinimapMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.automappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_automapStartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_automapStopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_projectMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_projectSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.appSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
      this.m_projectResetToDefaultSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
      this.mapStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mapStatisticsExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_helpMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_onlineHelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_checkForUpdatesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.m_helpAboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_toolStrip = new System.Windows.Forms.ToolStrip();
      this.m_toggleDottedLinesButton = new System.Windows.Forms.ToolStripButton();
      this.m_toggleDirectionalLinesButton = new System.Windows.Forms.ToolStripButton();
      this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.statusBar = new System.Windows.Forms.StatusStrip();
      this.Canvas = new Trizbort.UI.Controls.Canvas();
      this.m_automapBar = new Trizbort.UI.Controls.AutomapBar();
      this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
      this.m_viewShowGridMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_menuStrip.SuspendLayout();
      this.m_toolStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_menuStrip
      // 
      this.m_menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_fileMenu,
            this.m_editMenu,
            this.roomsToolStripMenuItem,
            this.connectionsToolStripMenuItem,
            this.validationToolStripMenuItem,
            this.m_viewMenu,
            this.automappingToolStripMenuItem,
            this.m_projectMenu,
            this.m_helpMenu});
      this.m_menuStrip.Location = new System.Drawing.Point(0, 0);
      this.m_menuStrip.Name = "m_menuStrip";
      this.m_menuStrip.Size = new System.Drawing.Size(1924, 24);
      this.m_menuStrip.TabIndex = 1;
      // 
      // m_fileMenu
      // 
      this.m_fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_fileNewMenuItem,
            this.toolStripSeparator1,
            this.m_fileOpenMenuItem,
            this.m_fileOpenFromWebMenuItem,
            this.toolStripSeparator9,
            this.m_fileSaveMenuItem,
            this.m_fileSaveAsMenuItem,
            this.smartSaveToolStripMenuItem,
            this.toolStripSeparator10,
            this.m_fileExportMenu,
            this.toolStripSeparator12,
            this.m_fileRecentMapsMenuItem,
            this.toolStripMenuItem7,
            this.m_fileExitMenuItem});
      this.m_fileMenu.Name = "m_fileMenu";
      this.m_fileMenu.Size = new System.Drawing.Size(37, 20);
      this.m_fileMenu.Text = "&File";
      this.m_fileMenu.DropDownOpening += new System.EventHandler(this.FileMenu_DropDownOpening);
      // 
      // m_fileNewMenuItem
      // 
      this.m_fileNewMenuItem.Name = "m_fileNewMenuItem";
      this.m_fileNewMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
      this.m_fileNewMenuItem.Size = new System.Drawing.Size(270, 22);
      this.m_fileNewMenuItem.Text = "&New Map";
      this.m_fileNewMenuItem.Click += new System.EventHandler(this.FileNewMenuItem_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(267, 6);
      // 
      // m_fileOpenMenuItem
      // 
      this.m_fileOpenMenuItem.Name = "m_fileOpenMenuItem";
      this.m_fileOpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
      this.m_fileOpenMenuItem.Size = new System.Drawing.Size(270, 22);
      this.m_fileOpenMenuItem.Text = "&Open Map...";
      this.m_fileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
      // 
      // m_fileOpenFromWebMenuItem
      // 
      this.m_fileOpenFromWebMenuItem.Name = "m_fileOpenFromWebMenuItem";
      this.m_fileOpenFromWebMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
      this.m_fileOpenFromWebMenuItem.Size = new System.Drawing.Size(270, 22);
      this.m_fileOpenFromWebMenuItem.Text = "Open Map from Web...";
      this.m_fileOpenFromWebMenuItem.Click += new System.EventHandler(this.m_fileOpenFromWebMenuItem_Click);
      // 
      // toolStripSeparator9
      // 
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new System.Drawing.Size(267, 6);
      // 
      // m_fileSaveMenuItem
      // 
      this.m_fileSaveMenuItem.Name = "m_fileSaveMenuItem";
      this.m_fileSaveMenuItem.ShortcutKeyDisplayString = "";
      this.m_fileSaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
      this.m_fileSaveMenuItem.Size = new System.Drawing.Size(270, 22);
      this.m_fileSaveMenuItem.Text = "&Save Map";
      this.m_fileSaveMenuItem.Click += new System.EventHandler(this.FileSaveMenuItem_Click);
      // 
      // m_fileSaveAsMenuItem
      // 
      this.m_fileSaveAsMenuItem.Name = "m_fileSaveAsMenuItem";
      this.m_fileSaveAsMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
      this.m_fileSaveAsMenuItem.Size = new System.Drawing.Size(270, 22);
      this.m_fileSaveAsMenuItem.Text = "Save Map &As...";
      this.m_fileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
      // 
      // smartSaveToolStripMenuItem
      // 
      this.smartSaveToolStripMenuItem.Name = "smartSaveToolStripMenuItem";
      this.smartSaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
      this.smartSaveToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
      this.smartSaveToolStripMenuItem.Text = "S&mart Save";
      this.smartSaveToolStripMenuItem.Click += new System.EventHandler(this.smartSaveToolStripMenuItem_Click);
      // 
      // toolStripSeparator10
      // 
      this.toolStripSeparator10.Name = "toolStripSeparator10";
      this.toolStripSeparator10.Size = new System.Drawing.Size(267, 6);
      // 
      // m_fileExportMenu
      // 
      this.m_fileExportMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_fileExportPDFMenuItem,
            this.m_fileExportImageMenuItem,
            this.toolStripMenuItem9,
            this.m_fileExportAlanMenuItem,
            this.m_fileExportHugoMenuItem,
            this.m_fileExportInform7MenuItem,
            this.m_fileExportInform6MenuItem,
            this.m_fileExportTADSMenuItem,
            this.zILToolStripMenuItem,
            this.m_fileExportQuestMenuItem,
            this.toolStripSeparator13,
            this.alanToTextToolStripMenuItem,
            this.hugoToTextToolStripMenuItem,
            this.inform7ToTextToolStripMenuItem,
            this.inform6ToTextToolStripMenuItem,
            this.tADSToTextToolStripMenuItem,
            this.zILToClipboardToolStripMenuItem,
            this.questToTextToolStripMenuItem,
            this.questRoomsToTextToolStripMenuItem});
      this.m_fileExportMenu.Name = "m_fileExportMenu";
      this.m_fileExportMenu.Size = new System.Drawing.Size(270, 22);
      this.m_fileExportMenu.Text = "&Export";
      // 
      // m_fileExportPDFMenuItem
      // 
      this.m_fileExportPDFMenuItem.Name = "m_fileExportPDFMenuItem";
      this.m_fileExportPDFMenuItem.Size = new System.Drawing.Size(304, 22);
      this.m_fileExportPDFMenuItem.Text = "&PDF...";
      this.m_fileExportPDFMenuItem.Click += new System.EventHandler(this.FileExportPDFMenuItem_Click);
      // 
      // m_fileExportImageMenuItem
      // 
      this.m_fileExportImageMenuItem.Name = "m_fileExportImageMenuItem";
      this.m_fileExportImageMenuItem.Size = new System.Drawing.Size(304, 22);
      this.m_fileExportImageMenuItem.Text = "&Image...";
      this.m_fileExportImageMenuItem.Click += new System.EventHandler(this.FileExportImageMenuItem_Click);
      // 
      // toolStripMenuItem9
      // 
      this.toolStripMenuItem9.Name = "toolStripMenuItem9";
      this.toolStripMenuItem9.Size = new System.Drawing.Size(301, 6);
      // 
      // m_fileExportAlanMenuItem
      // 
      this.m_fileExportAlanMenuItem.Name = "m_fileExportAlanMenuItem";
      this.m_fileExportAlanMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
      this.m_fileExportAlanMenuItem.Size = new System.Drawing.Size(304, 22);
      this.m_fileExportAlanMenuItem.Text = "&Alan...";
      this.m_fileExportAlanMenuItem.Click += new System.EventHandler(this.FileExportAlanMenuItem_Click);
      // 
      // m_fileExportHugoMenuItem
      // 
      this.m_fileExportHugoMenuItem.Name = "m_fileExportHugoMenuItem";
      this.m_fileExportHugoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.H)));
      this.m_fileExportHugoMenuItem.Size = new System.Drawing.Size(304, 22);
      this.m_fileExportHugoMenuItem.Text = "&Hugo...";
      this.m_fileExportHugoMenuItem.Click += new System.EventHandler(this.FileExportHugoMenuItem_Click);
      // 
      // m_fileExportInform7MenuItem
      // 
      this.m_fileExportInform7MenuItem.Name = "m_fileExportInform7MenuItem";
      this.m_fileExportInform7MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D7)));
      this.m_fileExportInform7MenuItem.Size = new System.Drawing.Size(304, 22);
      this.m_fileExportInform7MenuItem.Text = "Inform &7...";
      this.m_fileExportInform7MenuItem.Click += new System.EventHandler(this.FileExportInform7MenuItem_Click);
      // 
      // m_fileExportInform6MenuItem
      // 
      this.m_fileExportInform6MenuItem.Name = "m_fileExportInform6MenuItem";
      this.m_fileExportInform6MenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D6)));
      this.m_fileExportInform6MenuItem.Size = new System.Drawing.Size(304, 22);
      this.m_fileExportInform6MenuItem.Text = "Inform &6...";
      this.m_fileExportInform6MenuItem.Click += new System.EventHandler(this.FileExportInform6MenuItem_Click);
      // 
      // m_fileExportTADSMenuItem
      // 
      this.m_fileExportTADSMenuItem.Name = "m_fileExportTADSMenuItem";
      this.m_fileExportTADSMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
      this.m_fileExportTADSMenuItem.Size = new System.Drawing.Size(304, 22);
      this.m_fileExportTADSMenuItem.Text = "&TADS...";
      this.m_fileExportTADSMenuItem.Click += new System.EventHandler(this.FileExportTadsMenuItem_Click);
      // 
      // zILToolStripMenuItem
      // 
      this.zILToolStripMenuItem.Name = "zILToolStripMenuItem";
      this.zILToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
      this.zILToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.zILToolStripMenuItem.Text = "ZIL...";
      this.zILToolStripMenuItem.Click += new System.EventHandler(this.zILToolStripMenuItem_Click);
      // 
      // m_fileExportQuestMenuItem
      // 
      this.m_fileExportQuestMenuItem.Name = "m_fileExportQuestMenuItem";
      this.m_fileExportQuestMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Q)));
      this.m_fileExportQuestMenuItem.Size = new System.Drawing.Size(304, 22);
      this.m_fileExportQuestMenuItem.Text = "&Quest...";
      this.m_fileExportQuestMenuItem.Click += new System.EventHandler(this.FileExportQuestMenuItem_Click);
      // 
      // toolStripSeparator13
      // 
      this.toolStripSeparator13.Name = "toolStripSeparator13";
      this.toolStripSeparator13.Size = new System.Drawing.Size(301, 6);
      // 
      // alanToTextToolStripMenuItem
      // 
      this.alanToTextToolStripMenuItem.Name = "alanToTextToolStripMenuItem";
      this.alanToTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.L)));
      this.alanToTextToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.alanToTextToolStripMenuItem.Text = "Alan to Clipboard";
      this.alanToTextToolStripMenuItem.Click += new System.EventHandler(this.alanToTextToolStripMenuItem_Click);
      // 
      // hugoToTextToolStripMenuItem
      // 
      this.hugoToTextToolStripMenuItem.Name = "hugoToTextToolStripMenuItem";
      this.hugoToTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.H)));
      this.hugoToTextToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.hugoToTextToolStripMenuItem.Text = "Hugo to Clipboard";
      this.hugoToTextToolStripMenuItem.Click += new System.EventHandler(this.hugoToTextToolStripMenuItem_Click);
      // 
      // inform7ToTextToolStripMenuItem
      // 
      this.inform7ToTextToolStripMenuItem.Name = "inform7ToTextToolStripMenuItem";
      this.inform7ToTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D7)));
      this.inform7ToTextToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.inform7ToTextToolStripMenuItem.Text = "Inform 7 to Clipboard";
      this.inform7ToTextToolStripMenuItem.Click += new System.EventHandler(this.inform7ToTextToolStripMenuItem_Click);
      // 
      // inform6ToTextToolStripMenuItem
      // 
      this.inform6ToTextToolStripMenuItem.Name = "inform6ToTextToolStripMenuItem";
      this.inform6ToTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D6)));
      this.inform6ToTextToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.inform6ToTextToolStripMenuItem.Text = "Inform 6 to Clipboard";
      this.inform6ToTextToolStripMenuItem.Click += new System.EventHandler(this.inform6ToTextToolStripMenuItem_Click);
      // 
      // tADSToTextToolStripMenuItem
      // 
      this.tADSToTextToolStripMenuItem.Name = "tADSToTextToolStripMenuItem";
      this.tADSToTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.T)));
      this.tADSToTextToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.tADSToTextToolStripMenuItem.Text = "TADS to Clipboard";
      this.tADSToTextToolStripMenuItem.Click += new System.EventHandler(this.tADSToTextToolStripMenuItem_Click);
      // 
      // zILToClipboardToolStripMenuItem
      // 
      this.zILToClipboardToolStripMenuItem.Name = "zILToClipboardToolStripMenuItem";
      this.zILToClipboardToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Z)));
      this.zILToClipboardToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.zILToClipboardToolStripMenuItem.Text = "ZIL to Clipboard";
      this.zILToClipboardToolStripMenuItem.Click += new System.EventHandler(this.zILToClipboardToolStripMenuItem_Click);
      // 
      // questToTextToolStripMenuItem
      // 
      this.questToTextToolStripMenuItem.Name = "questToTextToolStripMenuItem";
      this.questToTextToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.questToTextToolStripMenuItem.Text = "Quest to Clipboard";
      this.questToTextToolStripMenuItem.Click += new System.EventHandler(this.questToTextToolStripMenuItem_Click);
      // 
      // questRoomsToTextToolStripMenuItem
      // 
      this.questRoomsToTextToolStripMenuItem.Name = "questRoomsToTextToolStripMenuItem";
      this.questRoomsToTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Q)));
      this.questRoomsToTextToolStripMenuItem.Size = new System.Drawing.Size(304, 22);
      this.questRoomsToTextToolStripMenuItem.Text = "Quest to Clipboard (no header)";
      this.questRoomsToTextToolStripMenuItem.Click += new System.EventHandler(this.questRoomsToTextToolStripMenuItem_Click);
      // 
      // toolStripSeparator12
      // 
      this.toolStripSeparator12.Name = "toolStripSeparator12";
      this.toolStripSeparator12.Size = new System.Drawing.Size(267, 6);
      // 
      // m_fileRecentMapsMenuItem
      // 
      this.m_fileRecentMapsMenuItem.Name = "m_fileRecentMapsMenuItem";
      this.m_fileRecentMapsMenuItem.Size = new System.Drawing.Size(270, 22);
      this.m_fileRecentMapsMenuItem.Text = "&Recent Maps";
      // 
      // toolStripMenuItem7
      // 
      this.toolStripMenuItem7.Name = "toolStripMenuItem7";
      this.toolStripMenuItem7.Size = new System.Drawing.Size(267, 6);
      // 
      // m_fileExitMenuItem
      // 
      this.m_fileExitMenuItem.Name = "m_fileExitMenuItem";
      this.m_fileExitMenuItem.Size = new System.Drawing.Size(270, 22);
      this.m_fileExitMenuItem.Text = "E&xit";
      this.m_fileExitMenuItem.Click += new System.EventHandler(this.FileExitMenuItem_Click);
      // 
      // m_editMenu
      // 
      this.m_editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator6,
            this.toggleTextToolStripMenuItem,
            this.toolStripSeparator8,
            this.m_editSelectAllMenuItem,
            this.m_editSelectNoneMenuItem,
            this.selectSpecialToolStripMenuItem,
            this.toolStripSeparator5,
            this.m_editCopyMenuItem,
            this.m_editCopyColorToolMenuItem,
            this.m_editPasteMenuItem,
            this.toolStripMenuItem8,
            this.m_editDeleteMenuItem,
            this.toolStripSeparator3,
            this.m_editPropertiesMenuItem});
      this.m_editMenu.Name = "m_editMenu";
      this.m_editMenu.Size = new System.Drawing.Size(39, 20);
      this.m_editMenu.Text = "&Edit";
      // 
      // toolStripSeparator6
      // 
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new System.Drawing.Size(208, 6);
      // 
      // toggleTextToolStripMenuItem
      // 
      this.toggleTextToolStripMenuItem.Name = "toggleTextToolStripMenuItem";
      this.toggleTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
      this.toggleTextToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
      this.toggleTextToolStripMenuItem.Text = "Toggle Text";
      this.toggleTextToolStripMenuItem.Click += new System.EventHandler(this.toggleTextToolStripMenuItem_Click);
      // 
      // toolStripSeparator8
      // 
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new System.Drawing.Size(208, 6);
      // 
      // m_editSelectAllMenuItem
      // 
      this.m_editSelectAllMenuItem.Name = "m_editSelectAllMenuItem";
      this.m_editSelectAllMenuItem.ShortcutKeyDisplayString = "Ctrl + A";
      this.m_editSelectAllMenuItem.Size = new System.Drawing.Size(211, 22);
      this.m_editSelectAllMenuItem.Text = "Select All";
      this.m_editSelectAllMenuItem.Click += new System.EventHandler(this.EditSelectAllMenuItem_Click);
      // 
      // m_editSelectNoneMenuItem
      // 
      this.m_editSelectNoneMenuItem.Name = "m_editSelectNoneMenuItem";
      this.m_editSelectNoneMenuItem.ShortcutKeyDisplayString = "Escape";
      this.m_editSelectNoneMenuItem.Size = new System.Drawing.Size(211, 22);
      this.m_editSelectNoneMenuItem.Text = "Select None";
      this.m_editSelectNoneMenuItem.Click += new System.EventHandler(this.EditSelectNoneMenuItem_Click);
      // 
      // selectSpecialToolStripMenuItem
      // 
      this.selectSpecialToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllRoomsToolStripMenuItem,
            this.selectedUnconnectedRoomsToolStripMenuItem,
            this.selectRoomsWObjectsToolStripMenuItem,
            this.selectRoomsWoObjectsToolStripMenuItem,
            this.toolStripSeparator15,
            this.selectAllConnectionsToolStripMenuItem,
            this.selectDanglingConnectionsToolStripMenuItem,
            this.selectSelfLoopingConnectionsToolStripMenuItem});
      this.selectSpecialToolStripMenuItem.Name = "selectSpecialToolStripMenuItem";
      this.selectSpecialToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
      this.selectSpecialToolStripMenuItem.Text = "Select Special";
      // 
      // selectAllRoomsToolStripMenuItem
      // 
      this.selectAllRoomsToolStripMenuItem.Name = "selectAllRoomsToolStripMenuItem";
      this.selectAllRoomsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.selectAllRoomsToolStripMenuItem.Text = "Select All Rooms";
      this.selectAllRoomsToolStripMenuItem.Click += new System.EventHandler(this.selectAllRoomsToolStripMenuItem_Click);
      // 
      // selectedUnconnectedRoomsToolStripMenuItem
      // 
      this.selectedUnconnectedRoomsToolStripMenuItem.Name = "selectedUnconnectedRoomsToolStripMenuItem";
      this.selectedUnconnectedRoomsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.selectedUnconnectedRoomsToolStripMenuItem.Text = "Selected Unconnected Rooms";
      this.selectedUnconnectedRoomsToolStripMenuItem.Click += new System.EventHandler(this.selectedUnconnectedRoomsToolStripMenuItem_Click);
      // 
      // selectRoomsWObjectsToolStripMenuItem
      // 
      this.selectRoomsWObjectsToolStripMenuItem.Name = "selectRoomsWObjectsToolStripMenuItem";
      this.selectRoomsWObjectsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.selectRoomsWObjectsToolStripMenuItem.Text = "Select Rooms w/ Objects";
      this.selectRoomsWObjectsToolStripMenuItem.Click += new System.EventHandler(this.selectRoomsWObjectsToolStripMenuItem_Click);
      // 
      // selectRoomsWoObjectsToolStripMenuItem
      // 
      this.selectRoomsWoObjectsToolStripMenuItem.Name = "selectRoomsWoObjectsToolStripMenuItem";
      this.selectRoomsWoObjectsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.selectRoomsWoObjectsToolStripMenuItem.Text = "Select Rooms w/o Objects";
      this.selectRoomsWoObjectsToolStripMenuItem.Click += new System.EventHandler(this.selectRoomsWoObjectsToolStripMenuItem_Click);
      // 
      // toolStripSeparator15
      // 
      this.toolStripSeparator15.Name = "toolStripSeparator15";
      this.toolStripSeparator15.Size = new System.Drawing.Size(241, 6);
      // 
      // selectAllConnectionsToolStripMenuItem
      // 
      this.selectAllConnectionsToolStripMenuItem.Name = "selectAllConnectionsToolStripMenuItem";
      this.selectAllConnectionsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.selectAllConnectionsToolStripMenuItem.Text = "Select All Connections";
      this.selectAllConnectionsToolStripMenuItem.Click += new System.EventHandler(this.selectAllConnectionsToolStripMenuItem_Click);
      // 
      // selectDanglingConnectionsToolStripMenuItem
      // 
      this.selectDanglingConnectionsToolStripMenuItem.Name = "selectDanglingConnectionsToolStripMenuItem";
      this.selectDanglingConnectionsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.selectDanglingConnectionsToolStripMenuItem.Text = "Select Dangling Connections";
      this.selectDanglingConnectionsToolStripMenuItem.Click += new System.EventHandler(this.selectDanglingConnectionsToolStripMenuItem_Click);
      // 
      // selectSelfLoopingConnectionsToolStripMenuItem
      // 
      this.selectSelfLoopingConnectionsToolStripMenuItem.Name = "selectSelfLoopingConnectionsToolStripMenuItem";
      this.selectSelfLoopingConnectionsToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
      this.selectSelfLoopingConnectionsToolStripMenuItem.Text = "Select Self Looping Connections";
      this.selectSelfLoopingConnectionsToolStripMenuItem.Click += new System.EventHandler(this.selectSelfLoopingConnectionsToolStripMenuItem_Click);
      // 
      // toolStripSeparator5
      // 
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new System.Drawing.Size(208, 6);
      // 
      // m_editCopyMenuItem
      // 
      this.m_editCopyMenuItem.Name = "m_editCopyMenuItem";
      this.m_editCopyMenuItem.ShortcutKeyDisplayString = "Ctrl + C";
      this.m_editCopyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
      this.m_editCopyMenuItem.Size = new System.Drawing.Size(211, 22);
      this.m_editCopyMenuItem.Text = "Copy";
      this.m_editCopyMenuItem.Click += new System.EventHandler(this.m_editCopyMenuItem_Click);
      // 
      // m_editCopyColorToolMenuItem
      // 
      this.m_editCopyColorToolMenuItem.Name = "m_editCopyColorToolMenuItem";
      this.m_editCopyColorToolMenuItem.ShortcutKeyDisplayString = "Ctrl + Alt + C";
      this.m_editCopyColorToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.C)));
      this.m_editCopyColorToolMenuItem.Size = new System.Drawing.Size(211, 22);
      this.m_editCopyColorToolMenuItem.Text = "Copy Color";
      this.m_editCopyColorToolMenuItem.Click += new System.EventHandler(this.m_editCopyColorToolMenuItem_Click);
      // 
      // m_editPasteMenuItem
      // 
      this.m_editPasteMenuItem.Name = "m_editPasteMenuItem";
      this.m_editPasteMenuItem.ShortcutKeyDisplayString = "Ctrl + V";
      this.m_editPasteMenuItem.Size = new System.Drawing.Size(211, 22);
      this.m_editPasteMenuItem.Text = "Paste";
      this.m_editPasteMenuItem.Click += new System.EventHandler(this.m_editPasteMenuItem_Click);
      // 
      // toolStripMenuItem8
      // 
      this.toolStripMenuItem8.Name = "toolStripMenuItem8";
      this.toolStripMenuItem8.Size = new System.Drawing.Size(208, 6);
      // 
      // m_editDeleteMenuItem
      // 
      this.m_editDeleteMenuItem.Name = "m_editDeleteMenuItem";
      this.m_editDeleteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
      this.m_editDeleteMenuItem.Size = new System.Drawing.Size(211, 22);
      this.m_editDeleteMenuItem.Text = "&Delete";
      this.m_editDeleteMenuItem.Click += new System.EventHandler(this.EditDeleteMenuItem_Click);
      // 
      // toolStripSeparator3
      // 
      this.toolStripSeparator3.Name = "toolStripSeparator3";
      this.toolStripSeparator3.Size = new System.Drawing.Size(208, 6);
      // 
      // m_editPropertiesMenuItem
      // 
      this.m_editPropertiesMenuItem.Name = "m_editPropertiesMenuItem";
      this.m_editPropertiesMenuItem.ShortcutKeyDisplayString = "Enter";
      this.m_editPropertiesMenuItem.Size = new System.Drawing.Size(211, 22);
      this.m_editPropertiesMenuItem.Text = "P&roperties";
      this.m_editPropertiesMenuItem.Click += new System.EventHandler(this.EditPropertiesMenuItem_Click);
      // 
      // roomsToolStripMenuItem
      // 
      this.roomsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_editAddRoomMenuItem,
            this.m_editRenameMenuItem,
            this.m_editChangeRegionMenuItem,
            this.toolStripSeparator16,
            this.m_editIsDarkMenuItem,
            this.makeRoomDarkToolStripMenuItem,
            this.makeRoomLightToolStripMenuItem,
            this.toolStripSeparator17,
            this.startRoomToolStripMenuItem,
            this.endRoomToolStripMenuItem,
            this.toolStripSeparator2,
            this.roomShapeToolStripMenuItem,
            this.toolStripSeparator18,
            this.joinRoomsToolStripMenuItem,
            this.toolStripSeparator19,
            this.swapObjectsToolStripMenuItem,
            this.swapNamesToolStripMenuItem,
            this.swapFormatsFillsToolStripMenuItem,
            this.swapRegionsToolStripMenuItem});
      this.roomsToolStripMenuItem.Name = "roomsToolStripMenuItem";
      this.roomsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
      this.roomsToolStripMenuItem.Text = "&Rooms";
      // 
      // m_editAddRoomMenuItem
      // 
      this.m_editAddRoomMenuItem.Name = "m_editAddRoomMenuItem";
      this.m_editAddRoomMenuItem.ShortcutKeyDisplayString = "R";
      this.m_editAddRoomMenuItem.Size = new System.Drawing.Size(229, 22);
      this.m_editAddRoomMenuItem.Text = "Add &Room";
      this.m_editAddRoomMenuItem.Click += new System.EventHandler(this.EditAddRoomMenuItem_Click);
      // 
      // m_editRenameMenuItem
      // 
      this.m_editRenameMenuItem.Name = "m_editRenameMenuItem";
      this.m_editRenameMenuItem.ShortcutKeyDisplayString = "";
      this.m_editRenameMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
      this.m_editRenameMenuItem.Size = new System.Drawing.Size(229, 22);
      this.m_editRenameMenuItem.Text = "Re&name";
      this.m_editRenameMenuItem.Click += new System.EventHandler(this.EditRenameMenuItem_Click);
      // 
      // m_editChangeRegionMenuItem
      // 
      this.m_editChangeRegionMenuItem.Name = "m_editChangeRegionMenuItem";
      this.m_editChangeRegionMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F2)));
      this.m_editChangeRegionMenuItem.Size = new System.Drawing.Size(229, 22);
      this.m_editChangeRegionMenuItem.Text = "Change Region";
      this.m_editChangeRegionMenuItem.Click += new System.EventHandler(this.m_editChangeRegionMenuItem_Click);
      // 
      // toolStripSeparator16
      // 
      this.toolStripSeparator16.Name = "toolStripSeparator16";
      this.toolStripSeparator16.Size = new System.Drawing.Size(226, 6);
      // 
      // m_editIsDarkMenuItem
      // 
      this.m_editIsDarkMenuItem.Name = "m_editIsDarkMenuItem";
      this.m_editIsDarkMenuItem.ShortcutKeyDisplayString = "K";
      this.m_editIsDarkMenuItem.Size = new System.Drawing.Size(229, 22);
      this.m_editIsDarkMenuItem.Text = "Toggle Dar&kness";
      this.m_editIsDarkMenuItem.Click += new System.EventHandler(this.EditIsDarkMenuItem_Click);
      // 
      // makeRoomDarkToolStripMenuItem
      // 
      this.makeRoomDarkToolStripMenuItem.Name = "makeRoomDarkToolStripMenuItem";
      this.makeRoomDarkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
      this.makeRoomDarkToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.makeRoomDarkToolStripMenuItem.Text = "Force Darkness";
      this.makeRoomDarkToolStripMenuItem.Click += new System.EventHandler(this.makeRoomDarkToolStripMenuItem_Click);
      // 
      // makeRoomLightToolStripMenuItem
      // 
      this.makeRoomLightToolStripMenuItem.Name = "makeRoomLightToolStripMenuItem";
      this.makeRoomLightToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.K)));
      this.makeRoomLightToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.makeRoomLightToolStripMenuItem.Text = "Force Lighted";
      this.makeRoomLightToolStripMenuItem.Click += new System.EventHandler(this.makeRoomLightToolStripMenuItem_Click);
      // 
      // toolStripSeparator17
      // 
      this.toolStripSeparator17.Name = "toolStripSeparator17";
      this.toolStripSeparator17.Size = new System.Drawing.Size(226, 6);
      // 
      // startRoomToolStripMenuItem
      // 
      this.startRoomToolStripMenuItem.Name = "startRoomToolStripMenuItem";
      this.startRoomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
      this.startRoomToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.startRoomToolStripMenuItem.Text = "Start Room";
      this.startRoomToolStripMenuItem.Click += new System.EventHandler(this.startRoomToolStripMenuItem_Click);
      // 
      // endRoomToolStripMenuItem
      // 
      this.endRoomToolStripMenuItem.Name = "endRoomToolStripMenuItem";
      this.endRoomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
      this.endRoomToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.endRoomToolStripMenuItem.Text = "End Room";
      this.endRoomToolStripMenuItem.Click += new System.EventHandler(this.endRoomToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(226, 6);
      // 
      // roomShapeToolStripMenuItem
      // 
      this.roomShapeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.handDrawnToolStripMenuItem,
            this.ellipseToolStripMenuItem,
            this.roundedEdgesToolStripMenuItem,
            this.octagonalEdgesToolStripMenuItem});
      this.roomShapeToolStripMenuItem.Name = "roomShapeToolStripMenuItem";
      this.roomShapeToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.roomShapeToolStripMenuItem.Text = "Room &Shape";
      // 
      // handDrawnToolStripMenuItem
      // 
      this.handDrawnToolStripMenuItem.Name = "handDrawnToolStripMenuItem";
      this.handDrawnToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
      this.handDrawnToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
      this.handDrawnToolStripMenuItem.Text = "Square Corners";
      this.handDrawnToolStripMenuItem.Click += new System.EventHandler(this.handDrawnToolStripMenuItem_Click);
      // 
      // ellipseToolStripMenuItem
      // 
      this.ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem";
      this.ellipseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
      this.ellipseToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
      this.ellipseToolStripMenuItem.Text = "Ellipse";
      this.ellipseToolStripMenuItem.Click += new System.EventHandler(this.ellipseToolStripMenuItem_Click);
      // 
      // roundedEdgesToolStripMenuItem
      // 
      this.roundedEdgesToolStripMenuItem.Name = "roundedEdgesToolStripMenuItem";
      this.roundedEdgesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
      this.roundedEdgesToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
      this.roundedEdgesToolStripMenuItem.Text = "Rounded Edges";
      this.roundedEdgesToolStripMenuItem.Click += new System.EventHandler(this.roundedEdgesToolStripMenuItem_Click);
      // 
      // octagonalEdgesToolStripMenuItem
      // 
      this.octagonalEdgesToolStripMenuItem.Name = "octagonalEdgesToolStripMenuItem";
      this.octagonalEdgesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D8)));
      this.octagonalEdgesToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
      this.octagonalEdgesToolStripMenuItem.Text = "Octagonal Edges";
      this.octagonalEdgesToolStripMenuItem.Click += new System.EventHandler(this.octagonalEdgesToolStripMenuItem_Click);
      // 
      // toolStripSeparator18
      // 
      this.toolStripSeparator18.Name = "toolStripSeparator18";
      this.toolStripSeparator18.Size = new System.Drawing.Size(226, 6);
      // 
      // joinRoomsToolStripMenuItem
      // 
      this.joinRoomsToolStripMenuItem.Name = "joinRoomsToolStripMenuItem";
      this.joinRoomsToolStripMenuItem.ShortcutKeyDisplayString = "J";
      this.joinRoomsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.joinRoomsToolStripMenuItem.Text = "&Join Rooms";
      this.joinRoomsToolStripMenuItem.Click += new System.EventHandler(this.joinRoomsToolStripMenuItem_Click);
      // 
      // toolStripSeparator19
      // 
      this.toolStripSeparator19.Name = "toolStripSeparator19";
      this.toolStripSeparator19.Size = new System.Drawing.Size(226, 6);
      // 
      // swapObjectsToolStripMenuItem
      // 
      this.swapObjectsToolStripMenuItem.Name = "swapObjectsToolStripMenuItem";
      this.swapObjectsToolStripMenuItem.ShortcutKeyDisplayString = "W";
      this.swapObjectsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.swapObjectsToolStripMenuItem.Text = "S&wap Objects";
      this.swapObjectsToolStripMenuItem.Click += new System.EventHandler(this.swapObjectsToolStripMenuItem_Click);
      // 
      // swapNamesToolStripMenuItem
      // 
      this.swapNamesToolStripMenuItem.Name = "swapNamesToolStripMenuItem";
      this.swapNamesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
      this.swapNamesToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.swapNamesToolStripMenuItem.Text = "Swap Names";
      this.swapNamesToolStripMenuItem.Click += new System.EventHandler(this.swapNamesToolStripMenuItem_Click);
      // 
      // swapFormatsFillsToolStripMenuItem
      // 
      this.swapFormatsFillsToolStripMenuItem.Name = "swapFormatsFillsToolStripMenuItem";
      this.swapFormatsFillsToolStripMenuItem.ShortcutKeyDisplayString = "Shift+W";
      this.swapFormatsFillsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.swapFormatsFillsToolStripMenuItem.Text = "Swap Formats / Fills";
      this.swapFormatsFillsToolStripMenuItem.Click += new System.EventHandler(this.swapFormatsFillsToolStripMenuItem_Click);
      // 
      // swapRegionsToolStripMenuItem
      // 
      this.swapRegionsToolStripMenuItem.Name = "swapRegionsToolStripMenuItem";
      this.swapRegionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
      this.swapRegionsToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
      this.swapRegionsToolStripMenuItem.Text = "Swap Regions";
      this.swapRegionsToolStripMenuItem.Click += new System.EventHandler(this.swapRegionsToolStripMenuItem_Click);
      // 
      // connectionsToolStripMenuItem
      // 
      this.connectionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_lineStylesMenuItem,
            this.m_reverseLineMenuItem});
      this.connectionsToolStripMenuItem.Name = "connectionsToolStripMenuItem";
      this.connectionsToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
      this.connectionsToolStripMenuItem.Text = "&Connections";
      // 
      // m_lineStylesMenuItem
      // 
      this.m_lineStylesMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_plainLinesMenuItem,
            this.toolStripMenuItem3,
            this.m_toggleDottedLinesMenuItem,
            this.m_toggleDirectionalLinesMenuItem,
            this.toolStripMenuItem1,
            this.m_upLinesMenuItem,
            this.m_downLinesMenuItem,
            this.toolStripMenuItem2,
            this.m_inLinesMenuItem,
            this.m_outLinesMenuItem});
      this.m_lineStylesMenuItem.Name = "m_lineStylesMenuItem";
      this.m_lineStylesMenuItem.ShortcutKeyDisplayString = "";
      this.m_lineStylesMenuItem.Size = new System.Drawing.Size(180, 22);
      this.m_lineStylesMenuItem.Text = "&Line Styles";
      // 
      // m_plainLinesMenuItem
      // 
      this.m_plainLinesMenuItem.Name = "m_plainLinesMenuItem";
      this.m_plainLinesMenuItem.ShortcutKeyDisplayString = "P";
      this.m_plainLinesMenuItem.Size = new System.Drawing.Size(172, 22);
      this.m_plainLinesMenuItem.Text = "&Plain";
      this.m_plainLinesMenuItem.Click += new System.EventHandler(this.PlainLinesMenuItem_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(169, 6);
      // 
      // m_toggleDottedLinesMenuItem
      // 
      this.m_toggleDottedLinesMenuItem.Name = "m_toggleDottedLinesMenuItem";
      this.m_toggleDottedLinesMenuItem.ShortcutKeyDisplayString = "T";
      this.m_toggleDottedLinesMenuItem.Size = new System.Drawing.Size(172, 22);
      this.m_toggleDottedLinesMenuItem.Text = "Do&tted";
      this.m_toggleDottedLinesMenuItem.Click += new System.EventHandler(this.ToggleDottedLines_Click);
      // 
      // m_toggleDirectionalLinesMenuItem
      // 
      this.m_toggleDirectionalLinesMenuItem.Name = "m_toggleDirectionalLinesMenuItem";
      this.m_toggleDirectionalLinesMenuItem.ShortcutKeyDisplayString = "A";
      this.m_toggleDirectionalLinesMenuItem.Size = new System.Drawing.Size(172, 22);
      this.m_toggleDirectionalLinesMenuItem.Text = "One Way &Arrow";
      this.m_toggleDirectionalLinesMenuItem.Click += new System.EventHandler(this.ToggleDirectionalLines_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 6);
      // 
      // m_upLinesMenuItem
      // 
      this.m_upLinesMenuItem.Name = "m_upLinesMenuItem";
      this.m_upLinesMenuItem.ShortcutKeyDisplayString = "U";
      this.m_upLinesMenuItem.Size = new System.Drawing.Size(172, 22);
      this.m_upLinesMenuItem.Text = "&Up";
      this.m_upLinesMenuItem.Click += new System.EventHandler(this.UpLinesMenuItem_Click);
      // 
      // m_downLinesMenuItem
      // 
      this.m_downLinesMenuItem.Name = "m_downLinesMenuItem";
      this.m_downLinesMenuItem.ShortcutKeyDisplayString = "D";
      this.m_downLinesMenuItem.Size = new System.Drawing.Size(172, 22);
      this.m_downLinesMenuItem.Text = "&Down";
      this.m_downLinesMenuItem.Click += new System.EventHandler(this.DownLinesMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(169, 6);
      // 
      // m_inLinesMenuItem
      // 
      this.m_inLinesMenuItem.Name = "m_inLinesMenuItem";
      this.m_inLinesMenuItem.ShortcutKeyDisplayString = "I";
      this.m_inLinesMenuItem.Size = new System.Drawing.Size(172, 22);
      this.m_inLinesMenuItem.Text = "&In";
      this.m_inLinesMenuItem.Click += new System.EventHandler(this.InLinesMenuItem_Click);
      // 
      // m_outLinesMenuItem
      // 
      this.m_outLinesMenuItem.Name = "m_outLinesMenuItem";
      this.m_outLinesMenuItem.ShortcutKeyDisplayString = "O";
      this.m_outLinesMenuItem.Size = new System.Drawing.Size(172, 22);
      this.m_outLinesMenuItem.Text = "&Out";
      this.m_outLinesMenuItem.Click += new System.EventHandler(this.OutLinesMenuItem_Click);
      // 
      // m_reverseLineMenuItem
      // 
      this.m_reverseLineMenuItem.Name = "m_reverseLineMenuItem";
      this.m_reverseLineMenuItem.ShortcutKeyDisplayString = "V";
      this.m_reverseLineMenuItem.Size = new System.Drawing.Size(180, 22);
      this.m_reverseLineMenuItem.Text = "Re&verse Line";
      this.m_reverseLineMenuItem.Click += new System.EventHandler(this.ReverseLineMenuItem_Click);
      // 
      // validationToolStripMenuItem
      // 
      this.validationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roomsMustHaveUniqueNamesToolStripMenuItem,
            this.roomsMustHaveADescriptionToolStripMenuItem,
            this.roomsMustHaveASubtitleToolStripMenuItem,
            this.roomsMustNotHaveADanglingConnectionToolStripMenuItem});
      this.validationToolStripMenuItem.Name = "validationToolStripMenuItem";
      this.validationToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
      this.validationToolStripMenuItem.Text = "Validation";
      // 
      // roomsMustHaveUniqueNamesToolStripMenuItem
      // 
      this.roomsMustHaveUniqueNamesToolStripMenuItem.Name = "roomsMustHaveUniqueNamesToolStripMenuItem";
      this.roomsMustHaveUniqueNamesToolStripMenuItem.Size = new System.Drawing.Size(319, 22);
      this.roomsMustHaveUniqueNamesToolStripMenuItem.Text = "Rooms Must Have a Unique Name";
      this.roomsMustHaveUniqueNamesToolStripMenuItem.Click += new System.EventHandler(this.roomsMustHaveUniqueNamesToolStripMenuItem_Click);
      // 
      // roomsMustHaveADescriptionToolStripMenuItem
      // 
      this.roomsMustHaveADescriptionToolStripMenuItem.Name = "roomsMustHaveADescriptionToolStripMenuItem";
      this.roomsMustHaveADescriptionToolStripMenuItem.Size = new System.Drawing.Size(319, 22);
      this.roomsMustHaveADescriptionToolStripMenuItem.Text = "Rooms Must Have a Description";
      this.roomsMustHaveADescriptionToolStripMenuItem.Click += new System.EventHandler(this.roomsMustHaveADescriptionToolStripMenuItem_Click);
      // 
      // roomsMustHaveASubtitleToolStripMenuItem
      // 
      this.roomsMustHaveASubtitleToolStripMenuItem.Name = "roomsMustHaveASubtitleToolStripMenuItem";
      this.roomsMustHaveASubtitleToolStripMenuItem.Size = new System.Drawing.Size(319, 22);
      this.roomsMustHaveASubtitleToolStripMenuItem.Text = "Rooms Must Have a Subtitle";
      this.roomsMustHaveASubtitleToolStripMenuItem.Click += new System.EventHandler(this.roomsMustHaveASubtitleToolStripMenuItem_Click);
      // 
      // roomsMustNotHaveADanglingConnectionToolStripMenuItem
      // 
      this.roomsMustNotHaveADanglingConnectionToolStripMenuItem.Name = "roomsMustNotHaveADanglingConnectionToolStripMenuItem";
      this.roomsMustNotHaveADanglingConnectionToolStripMenuItem.Size = new System.Drawing.Size(319, 22);
      this.roomsMustNotHaveADanglingConnectionToolStripMenuItem.Text = "Rooms Must Not Have a Dangling Connection";
      this.roomsMustNotHaveADanglingConnectionToolStripMenuItem.Click += new System.EventHandler(this.roomsMustNotHaveADanglingConnectionToolStripMenuItem_Click);
      // 
      // m_viewMenu
      // 
      this.m_viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_viewZoomMenu,
            this.m_viewEntireMapMenuItem,
            this.m_viewResetMenuItem,
            this.toolStripMenuItem6,
            this.m_viewMinimapMenuItem,
            this.toolStripSeparator21,
            this.m_viewShowGridMenuItem});
      this.m_viewMenu.Name = "m_viewMenu";
      this.m_viewMenu.Size = new System.Drawing.Size(44, 20);
      this.m_viewMenu.Text = "&View";
      // 
      // m_viewZoomMenu
      // 
      this.m_viewZoomMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_viewZoomInMenuItem,
            this.m_viewZoomOutMenuItem,
            this.toolStripSeparator7,
            this.m_viewZoomFiftyPercentMenuItem,
            this.m_viewZoomOneHundredPercentMenuItem,
            this.m_viewZoomTwoHundredPercentMenuItem,
            this.toolStripSeparator20,
            this.m_viewZoomMiniIn,
            this.m_viewZoomMiniOut});
      this.m_viewZoomMenu.Name = "m_viewZoomMenu";
      this.m_viewZoomMenu.Size = new System.Drawing.Size(204, 22);
      this.m_viewZoomMenu.Text = "&Zoom";
      // 
      // m_viewZoomInMenuItem
      // 
      this.m_viewZoomInMenuItem.Name = "m_viewZoomInMenuItem";
      this.m_viewZoomInMenuItem.ShortcutKeyDisplayString = "+ / Mouse Wheel";
      this.m_viewZoomInMenuItem.Size = new System.Drawing.Size(205, 22);
      this.m_viewZoomInMenuItem.Text = "&In";
      this.m_viewZoomInMenuItem.Click += new System.EventHandler(this.ViewZoomInMenuItem_Click);
      // 
      // m_viewZoomOutMenuItem
      // 
      this.m_viewZoomOutMenuItem.Name = "m_viewZoomOutMenuItem";
      this.m_viewZoomOutMenuItem.ShortcutKeyDisplayString = "- / Mouse Wheel";
      this.m_viewZoomOutMenuItem.Size = new System.Drawing.Size(205, 22);
      this.m_viewZoomOutMenuItem.Text = "&Out";
      this.m_viewZoomOutMenuItem.Click += new System.EventHandler(this.ViewZoomOutMenuItem_Click);
      // 
      // toolStripSeparator7
      // 
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new System.Drawing.Size(202, 6);
      // 
      // m_viewZoomFiftyPercentMenuItem
      // 
      this.m_viewZoomFiftyPercentMenuItem.Name = "m_viewZoomFiftyPercentMenuItem";
      this.m_viewZoomFiftyPercentMenuItem.Size = new System.Drawing.Size(205, 22);
      this.m_viewZoomFiftyPercentMenuItem.Text = "&50%";
      this.m_viewZoomFiftyPercentMenuItem.Click += new System.EventHandler(this.ViewZoomFiftyPercentMenuItem_Click);
      // 
      // m_viewZoomOneHundredPercentMenuItem
      // 
      this.m_viewZoomOneHundredPercentMenuItem.Name = "m_viewZoomOneHundredPercentMenuItem";
      this.m_viewZoomOneHundredPercentMenuItem.Size = new System.Drawing.Size(205, 22);
      this.m_viewZoomOneHundredPercentMenuItem.Text = "&100%";
      this.m_viewZoomOneHundredPercentMenuItem.Click += new System.EventHandler(this.ViewZoomOneHundredPercentMenuItem_Click);
      // 
      // m_viewZoomTwoHundredPercentMenuItem
      // 
      this.m_viewZoomTwoHundredPercentMenuItem.Name = "m_viewZoomTwoHundredPercentMenuItem";
      this.m_viewZoomTwoHundredPercentMenuItem.Size = new System.Drawing.Size(205, 22);
      this.m_viewZoomTwoHundredPercentMenuItem.Text = "&200%";
      this.m_viewZoomTwoHundredPercentMenuItem.Click += new System.EventHandler(this.ViewZoomTwoHundredPercentMenuItem_Click);
      // 
      // toolStripSeparator20
      // 
      this.toolStripSeparator20.Name = "toolStripSeparator20";
      this.toolStripSeparator20.Size = new System.Drawing.Size(202, 6);
      // 
      // m_viewZoomMiniIn
      // 
      this.m_viewZoomMiniIn.Name = "m_viewZoomMiniIn";
      this.m_viewZoomMiniIn.ShortcutKeyDisplayString = "ctrl / Mouse Wheel";
      this.m_viewZoomMiniIn.Size = new System.Drawing.Size(205, 22);
      this.m_viewZoomMiniIn.Text = "&-1%";
      this.m_viewZoomMiniIn.Click += new System.EventHandler(this.ViewZoomMiniIn_Click);
      // 
      // m_viewZoomMiniOut
      // 
      this.m_viewZoomMiniOut.Name = "m_viewZoomMiniOut";
      this.m_viewZoomMiniOut.ShortcutKeyDisplayString = "ctrl / Mouse Wheel";
      this.m_viewZoomMiniOut.Size = new System.Drawing.Size(205, 22);
      this.m_viewZoomMiniOut.Text = "&+1%";
      this.m_viewZoomMiniOut.Click += new System.EventHandler(this.ViewZoomMiniOut_Click);
      // 
      // m_viewEntireMapMenuItem
      // 
      this.m_viewEntireMapMenuItem.Name = "m_viewEntireMapMenuItem";
      this.m_viewEntireMapMenuItem.ShortcutKeyDisplayString = "Ctrl + Home";
      this.m_viewEntireMapMenuItem.Size = new System.Drawing.Size(204, 22);
      this.m_viewEntireMapMenuItem.Text = "&Entire Map";
      this.m_viewEntireMapMenuItem.Click += new System.EventHandler(this.ViewEntireMapMenuItem_Click);
      // 
      // m_viewResetMenuItem
      // 
      this.m_viewResetMenuItem.Name = "m_viewResetMenuItem";
      this.m_viewResetMenuItem.ShortcutKeyDisplayString = "Home";
      this.m_viewResetMenuItem.Size = new System.Drawing.Size(204, 22);
      this.m_viewResetMenuItem.Text = "&Reset";
      this.m_viewResetMenuItem.Click += new System.EventHandler(this.ViewResetMenuItem_Click);
      // 
      // toolStripMenuItem6
      // 
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      this.toolStripMenuItem6.Size = new System.Drawing.Size(201, 6);
      // 
      // m_viewMinimapMenuItem
      // 
      this.m_viewMinimapMenuItem.Name = "m_viewMinimapMenuItem";
      this.m_viewMinimapMenuItem.Size = new System.Drawing.Size(204, 22);
      this.m_viewMinimapMenuItem.Text = "&Mini Map";
      this.m_viewMinimapMenuItem.Click += new System.EventHandler(this.ViewMinimapMenuItem_Click);
      // 
      // automappingToolStripMenuItem
      // 
      this.automappingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_automapStartMenuItem,
            this.m_automapStopMenuItem});
      this.automappingToolStripMenuItem.Name = "automappingToolStripMenuItem";
      this.automappingToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
      this.automappingToolStripMenuItem.Text = "&Automap";
      // 
      // m_automapStartMenuItem
      // 
      this.m_automapStartMenuItem.Name = "m_automapStartMenuItem";
      this.m_automapStartMenuItem.Size = new System.Drawing.Size(107, 22);
      this.m_automapStartMenuItem.Text = "&Start...";
      this.m_automapStartMenuItem.Click += new System.EventHandler(this.AutomapStartMenuItem_Click);
      // 
      // m_automapStopMenuItem
      // 
      this.m_automapStopMenuItem.Name = "m_automapStopMenuItem";
      this.m_automapStopMenuItem.Size = new System.Drawing.Size(107, 22);
      this.m_automapStopMenuItem.Text = "S&top";
      this.m_automapStopMenuItem.Click += new System.EventHandler(this.AutomapStopMenuItem_Click);
      // 
      // m_projectMenu
      // 
      this.m_projectMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_projectSettingsMenuItem,
            this.appSettingsToolStripMenuItem,
            this.toolStripSeparator11,
            this.m_projectResetToDefaultSettingsMenuItem,
            this.toolStripSeparator14,
            this.mapStatisticsToolStripMenuItem,
            this.mapStatisticsExportToolStripMenuItem});
      this.m_projectMenu.Name = "m_projectMenu";
      this.m_projectMenu.Size = new System.Drawing.Size(48, 20);
      this.m_projectMenu.Text = "&Tools";
      // 
      // m_projectSettingsMenuItem
      // 
      this.m_projectSettingsMenuItem.Name = "m_projectSettingsMenuItem";
      this.m_projectSettingsMenuItem.Size = new System.Drawing.Size(226, 22);
      this.m_projectSettingsMenuItem.Text = "Map &Settings...";
      this.m_projectSettingsMenuItem.Click += new System.EventHandler(this.ProjectSettingsMenuItem_Click);
      // 
      // appSettingsToolStripMenuItem
      // 
      this.appSettingsToolStripMenuItem.Name = "appSettingsToolStripMenuItem";
      this.appSettingsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.appSettingsToolStripMenuItem.Text = "&App Settings...";
      this.appSettingsToolStripMenuItem.Click += new System.EventHandler(this.appSettingsToolStripMenuItem_Click);
      // 
      // toolStripSeparator11
      // 
      this.toolStripSeparator11.Name = "toolStripSeparator11";
      this.toolStripSeparator11.Size = new System.Drawing.Size(223, 6);
      // 
      // m_projectResetToDefaultSettingsMenuItem
      // 
      this.m_projectResetToDefaultSettingsMenuItem.Name = "m_projectResetToDefaultSettingsMenuItem";
      this.m_projectResetToDefaultSettingsMenuItem.Size = new System.Drawing.Size(226, 22);
      this.m_projectResetToDefaultSettingsMenuItem.Text = "&Restore Default Map Settings";
      this.m_projectResetToDefaultSettingsMenuItem.Click += new System.EventHandler(this.ProjectResetToDefaultSettingsMenuItem_Click);
      // 
      // toolStripSeparator14
      // 
      this.toolStripSeparator14.Name = "toolStripSeparator14";
      this.toolStripSeparator14.Size = new System.Drawing.Size(223, 6);
      // 
      // mapStatisticsToolStripMenuItem
      // 
      this.mapStatisticsToolStripMenuItem.Name = "mapStatisticsToolStripMenuItem";
      this.mapStatisticsToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.mapStatisticsToolStripMenuItem.Text = "Map Statistics...";
      this.mapStatisticsToolStripMenuItem.Click += new System.EventHandler(this.mapStatisticsToolStripMenuItem_Click);
      // 
      // mapStatisticsExportToolStripMenuItem
      // 
      this.mapStatisticsExportToolStripMenuItem.Name = "mapStatisticsExportToolStripMenuItem";
      this.mapStatisticsExportToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
      this.mapStatisticsExportToolStripMenuItem.Text = "Map Statistic E&xport...";
      this.mapStatisticsExportToolStripMenuItem.Click += new System.EventHandler(this.mapStatisticsExportToolStripMenuItem_Click);
      // 
      // m_helpMenu
      // 
      this.m_helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_onlineHelpMenuItem,
            this.m_checkForUpdatesMenuItem,
            this.toolStripMenuItem4,
            this.m_helpAboutMenuItem});
      this.m_helpMenu.Name = "m_helpMenu";
      this.m_helpMenu.Size = new System.Drawing.Size(44, 20);
      this.m_helpMenu.Text = "&Help";
      // 
      // m_onlineHelpMenuItem
      // 
      this.m_onlineHelpMenuItem.Name = "m_onlineHelpMenuItem";
      this.m_onlineHelpMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
      this.m_onlineHelpMenuItem.Size = new System.Drawing.Size(171, 22);
      this.m_onlineHelpMenuItem.Text = "Online Help";
      this.m_onlineHelpMenuItem.Click += new System.EventHandler(this.HelpAndSupportMenuItem_Click);
      // 
      // m_checkForUpdatesMenuItem
      // 
      this.m_checkForUpdatesMenuItem.Name = "m_checkForUpdatesMenuItem";
      this.m_checkForUpdatesMenuItem.Size = new System.Drawing.Size(171, 22);
      this.m_checkForUpdatesMenuItem.Text = "Check for &Updates";
      this.m_checkForUpdatesMenuItem.Click += new System.EventHandler(this.CheckForUpdatesMenuItem_Click);
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(168, 6);
      // 
      // m_helpAboutMenuItem
      // 
      this.m_helpAboutMenuItem.Name = "m_helpAboutMenuItem";
      this.m_helpAboutMenuItem.Size = new System.Drawing.Size(171, 22);
      this.m_helpAboutMenuItem.Text = "&About";
      this.m_helpAboutMenuItem.Click += new System.EventHandler(this.HelpAboutMenuItem_Click);
      // 
      // m_toolStrip
      // 
      this.m_toolStrip.AutoSize = false;
      this.m_toolStrip.Dock = System.Windows.Forms.DockStyle.Left;
      this.m_toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_toggleDottedLinesButton,
            this.m_toggleDirectionalLinesButton});
      this.m_toolStrip.Location = new System.Drawing.Point(0, 24);
      this.m_toolStrip.Name = "m_toolStrip";
      this.m_toolStrip.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
      this.m_toolStrip.Size = new System.Drawing.Size(38, 983);
      this.m_toolStrip.TabIndex = 2;
      // 
      // m_toggleDottedLinesButton
      // 
      this.m_toggleDottedLinesButton.AutoSize = false;
      this.m_toggleDottedLinesButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_toggleDottedLinesButton.BackgroundImage")));
      this.m_toggleDottedLinesButton.CheckOnClick = true;
      this.m_toggleDottedLinesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_toggleDottedLinesButton.Image = global::Trizbort.Properties.Resources.LineStyle;
      this.m_toggleDottedLinesButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_toggleDottedLinesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_toggleDottedLinesButton.Name = "m_toggleDottedLinesButton";
      this.m_toggleDottedLinesButton.Size = new System.Drawing.Size(32, 32);
      this.m_toggleDottedLinesButton.Text = "Toggle Dotted Lines (T)";
      this.m_toggleDottedLinesButton.Click += new System.EventHandler(this.ToggleDottedLines_Click);
      // 
      // m_toggleDirectionalLinesButton
      // 
      this.m_toggleDirectionalLinesButton.AutoSize = false;
      this.m_toggleDirectionalLinesButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_toggleDirectionalLinesButton.BackgroundImage")));
      this.m_toggleDirectionalLinesButton.CheckOnClick = true;
      this.m_toggleDirectionalLinesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.m_toggleDirectionalLinesButton.Image = global::Trizbort.Properties.Resources.LineDirection;
      this.m_toggleDirectionalLinesButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_toggleDirectionalLinesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.m_toggleDirectionalLinesButton.Name = "m_toggleDirectionalLinesButton";
      this.m_toggleDirectionalLinesButton.Size = new System.Drawing.Size(32, 32);
      this.m_toggleDirectionalLinesButton.Text = "Toggle One Way Arrow Lines (A)";
      this.m_toggleDirectionalLinesButton.Click += new System.EventHandler(this.ToggleDirectionalLines_Click);
      // 
      // toolStripSeparator4
      // 
      this.toolStripSeparator4.Name = "toolStripSeparator4";
      this.toolStripSeparator4.Size = new System.Drawing.Size(6, 6);
      // 
      // statusBar
      // 
      this.statusBar.Location = new System.Drawing.Point(0, 1007);
      this.statusBar.Name = "statusBar";
      this.statusBar.Size = new System.Drawing.Size(1924, 22);
      this.statusBar.TabIndex = 5;
      this.statusBar.Text = "statusStrip1";
      // 
      // Canvas
      // 
      this.Canvas.BackColor = System.Drawing.Color.White;
      this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Canvas.Location = new System.Drawing.Point(38, 24);
      this.Canvas.MinimapVisible = true;
      this.Canvas.Name = "Canvas";
      this.Canvas.Size = new System.Drawing.Size(1886, 954);
      this.Canvas.TabIndex = 0;
      // 
      // m_automapBar
      // 
      this.m_automapBar.BackColor = System.Drawing.SystemColors.Info;
      this.m_automapBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.m_automapBar.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.m_automapBar.ForeColor = System.Drawing.SystemColors.InfoText;
      this.m_automapBar.Location = new System.Drawing.Point(38, 978);
      this.m_automapBar.MaximumSize = new System.Drawing.Size(4096, 29);
      this.m_automapBar.MinimumSize = new System.Drawing.Size(2, 29);
      this.m_automapBar.Name = "m_automapBar";
      this.m_automapBar.Size = new System.Drawing.Size(1886, 29);
      this.m_automapBar.TabIndex = 4;
      // 
      // toolStripSeparator21
      // 
      this.toolStripSeparator21.Name = "toolStripSeparator21";
      this.toolStripSeparator21.Size = new System.Drawing.Size(201, 6);
      // 
      // m_viewShowGridMenuItem
      // 
      this.m_viewShowGridMenuItem.Name = "m_viewShowGridMenuItem";
      this.m_viewShowGridMenuItem.Size = new System.Drawing.Size(204, 22);
      this.m_viewShowGridMenuItem.Text = "Show &Grid";
      this.m_viewShowGridMenuItem.Click += new System.EventHandler(this.ViewShowGridMenuItem_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1924, 1029);
      this.Controls.Add(this.Canvas);
      this.Controls.Add(this.m_automapBar);
      this.Controls.Add(this.m_toolStrip);
      this.Controls.Add(this.m_menuStrip);
      this.Controls.Add(this.statusBar);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.m_menuStrip;
      this.Name = "MainForm";
      this.Text = "Trizbort - Interactive Fiction Mapper";
      this.Load += new System.EventHandler(this.MainForm_Load);
      this.m_menuStrip.ResumeLayout(false);
      this.m_menuStrip.PerformLayout();
      this.m_toolStrip.ResumeLayout(false);
      this.m_toolStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip m_menuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_fileMenu;
        private System.Windows.Forms.ToolStripMenuItem m_fileExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_projectMenu;
        private System.Windows.Forms.ToolStripMenuItem m_projectSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileNewMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem m_editMenu;
        private System.Windows.Forms.ToolStripMenuItem m_editDeleteMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem m_editPropertiesMenuItem;
        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.ToolStripButton m_toggleDottedLinesButton;
        private System.Windows.Forms.ToolStripButton m_toggleDirectionalLinesButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem m_lineStylesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_viewMenu;
        private System.Windows.Forms.ToolStripMenuItem m_viewZoomMenu;
        private System.Windows.Forms.ToolStripMenuItem m_viewZoomInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_viewZoomOutMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem m_viewZoomFiftyPercentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_viewZoomOneHundredPercentMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_viewZoomTwoHundredPercentMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem m_viewZoomMiniOut;
        private System.Windows.Forms.ToolStripMenuItem m_viewZoomMiniIn;
        private System.Windows.Forms.ToolStripMenuItem m_viewResetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_editRenameMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem m_fileOpenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem m_fileSaveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileSaveAsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem m_projectResetToDefaultSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportMenu;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportPDFMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportImageMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem m_helpMenu;
        private System.Windows.Forms.ToolStripMenuItem m_helpAboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_plainLinesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem m_toggleDottedLinesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_toggleDirectionalLinesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem m_upLinesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_downLinesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem m_inLinesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_outLinesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_reverseLineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_onlineHelpMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem m_checkForUpdatesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem automappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_automapStartMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_automapStopMenuItem;
        private AutomapBar m_automapBar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem m_viewMinimapMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportAlanMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportHugoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportInform7MenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportQuestMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileRecentMapsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem m_editSelectAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_editSelectNoneMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem m_viewEntireMapMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportTADSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_fileExportInform6MenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem m_editCopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_editPasteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_editCopyColorToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smartSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_editChangeRegionMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem alanToTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hugoToTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inform7ToTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inform6ToTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tADSToTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questToTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem questRoomsToTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem mapStatisticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapStatisticsExportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectSpecialToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectAllRoomsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectAllConnectionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
    private System.Windows.Forms.ToolStripMenuItem selectedUnconnectedRoomsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectDanglingConnectionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectSelfLoopingConnectionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectRoomsWObjectsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem selectRoomsWoObjectsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toggleTextToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zILToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem zILToClipboardToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem roomsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem connectionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem m_editAddRoomMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
    private System.Windows.Forms.ToolStripMenuItem m_editIsDarkMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem roomShapeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem handDrawnToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem ellipseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem roundedEdgesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem octagonalEdgesToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
    private System.Windows.Forms.ToolStripMenuItem joinRoomsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
    private System.Windows.Forms.ToolStripMenuItem swapObjectsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem swapNamesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem swapFormatsFillsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem swapRegionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem makeRoomDarkToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem makeRoomLightToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem validationToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem roomsMustHaveUniqueNamesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem roomsMustHaveADescriptionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem roomsMustHaveASubtitleToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem roomsMustNotHaveADanglingConnectionToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem startRoomToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
    private System.Windows.Forms.ToolStripMenuItem endRoomToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusBar;
    private System.Windows.Forms.ToolStripMenuItem m_fileOpenFromWebMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
    private System.Windows.Forms.ToolStripMenuItem m_viewShowGridMenuItem;
  }
}

