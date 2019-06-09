﻿namespace Trizbort.UI {
	partial class AppSettingsDialog {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppSettingsDialog));
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkFullPathTitleBar = new System.Windows.Forms.CheckBox();
            this.chkSaveAtZoom = new System.Windows.Forms.CheckBox();
            this.m_invertWheelCheckBox = new System.Windows.Forms.CheckBox();
            this.chkLoadLast = new System.Windows.Forms.CheckBox();
            this.cboPortAdjustDetail = new System.Windows.Forms.ComboBox();
            this.labelG = new System.Windows.Forms.Label();
            this.txtDefaultFontName = new System.Windows.Forms.TextBox();
            this.labelFont = new System.Windows.Forms.Label();
            this.cboImageSaveType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSaveTADSToADV3Lite = new System.Windows.Forms.CheckBox();
            this.chkSaveToImage = new System.Windows.Forms.CheckBox();
            this.chkSaveToPDF = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.m_preferredHorizontalMargin = new System.Windows.Forms.NumericUpDown();
            this.m_preferredVerticalMargin = new System.Windows.Forms.NumericUpDown();
            this.chkSpecifyMargins = new System.Windows.Forms.CheckBox();
            this.labelH = new System.Windows.Forms.Label();
            this.labelV = new System.Windows.Forms.Label();
            this.chkDefaultHandDrawn = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.tabToolTips = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkLimitRoomDescriptionTooltipChars = new System.Windows.Forms.CheckBox();
            this.lblCharactersToShowRoom = new System.Windows.Forms.Label();
            this.chkShowDescriptionsInTooltip = new System.Windows.Forms.CheckBox();
            this.txtNumOfRoomDescriptionChars = new System.Windows.Forms.NumericUpDown();
            this.txtNumOfConnectionDescriptionChars = new System.Windows.Forms.NumericUpDown();
            this.chkLimitConnectionDescriptionTooltipChars = new System.Windows.Forms.CheckBox();
            this.lblCharactersToShowConnection = new System.Windows.Forms.Label();
            this.chkShowObjectsInTooltip = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_preferredHorizontalMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_preferredVerticalMargin)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabToolTips.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOfRoomDescriptionChars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOfConnectionDescriptionChars)).BeginInit();
            this.SuspendLayout();
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_okButton.Location = new System.Drawing.Point(560, 677);
            this.m_okButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(150, 46);
            this.m_okButton.TabIndex = 4;
            this.m_okButton.Text = "&OK";
            this.m_okButton.UseVisualStyleBackColor = true;
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(722, 677);
            this.m_cancelButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(150, 46);
            this.m_cancelButton.TabIndex = 5;
            this.m_cancelButton.Text = "C&ancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.chkFullPathTitleBar);
            this.groupBox1.Controls.Add(this.chkSaveAtZoom);
            this.groupBox1.Controls.Add(this.m_invertWheelCheckBox);
            this.groupBox1.Controls.Add(this.chkLoadLast);
            this.groupBox1.Controls.Add(this.cboPortAdjustDetail);
            this.groupBox1.Controls.Add(this.labelG);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(800, 166);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preferences";
            // 
            // chkFullPathTitleBar
            // 
            this.chkFullPathTitleBar.AutoSize = true;
            this.chkFullPathTitleBar.Location = new System.Drawing.Point(46, 120);
            this.chkFullPathTitleBar.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkFullPathTitleBar.Name = "chkFullPathTitleBar";
            this.chkFullPathTitleBar.Size = new System.Drawing.Size(324, 36);
            this.chkFullPathTitleBar.TabIndex = 4;
            this.chkFullPathTitleBar.Text = "Show Full Path in Title Bar";
            this.chkFullPathTitleBar.UseVisualStyleBackColor = true;
            // 
            // chkSaveAtZoom
            // 
            this.chkSaveAtZoom.AutoSize = true;
            this.chkSaveAtZoom.Location = new System.Drawing.Point(46, 40);
            this.chkSaveAtZoom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkSaveAtZoom.Name = "chkSaveAtZoom";
            this.chkSaveAtZoom.Size = new System.Drawing.Size(273, 36);
            this.chkSaveAtZoom.TabIndex = 0;
            this.chkSaveAtZoom.Text = "&Save images at 100%";
            this.toolTip1.SetToolTip(this.chkSaveAtZoom, "If this is unchecked, images will be saved at their current zoom %\r\n");
            this.chkSaveAtZoom.UseVisualStyleBackColor = true;
            // 
            // m_invertWheelCheckBox
            // 
            this.m_invertWheelCheckBox.AutoSize = true;
            this.m_invertWheelCheckBox.Location = new System.Drawing.Point(46, 80);
            this.m_invertWheelCheckBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_invertWheelCheckBox.Name = "m_invertWheelCheckBox";
            this.m_invertWheelCheckBox.Size = new System.Drawing.Size(333, 36);
            this.m_invertWheelCheckBox.TabIndex = 1;
            this.m_invertWheelCheckBox.Text = "Invert Mouse Wheel &Zoom";
            this.m_invertWheelCheckBox.UseVisualStyleBackColor = true;
            // 
            // chkLoadLast
            // 
            this.chkLoadLast.AutoSize = true;
            this.chkLoadLast.Location = new System.Drawing.Point(406, 40);
            this.chkLoadLast.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkLoadLast.Name = "chkLoadLast";
            this.chkLoadLast.Size = new System.Drawing.Size(318, 36);
            this.chkLoadLast.TabIndex = 2;
            this.chkLoadLast.Text = "&Open last project on start";
            this.toolTip2.SetToolTip(this.chkLoadLast, "If this is checked, Trizbort will load the last project on startup\r\n");
            this.chkLoadLast.UseVisualStyleBackColor = true;
            // 
            // cboPortAdjustDetail
            // 
            this.cboPortAdjustDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPortAdjustDetail.FormattingEnabled = true;
            this.cboPortAdjustDetail.Items.AddRange(new object[] {
            "NSEW (4)",
            "Diagonals (8)",
            "All ports (16)"});
            this.cboPortAdjustDetail.Location = new System.Drawing.Point(600, 80);
            this.cboPortAdjustDetail.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cboPortAdjustDetail.Name = "cboPortAdjustDetail";
            this.cboPortAdjustDetail.Size = new System.Drawing.Size(164, 40);
            this.cboPortAdjustDetail.TabIndex = 3;
            this.cboPortAdjustDetail.Enter += new System.EventHandler(this.cboPortAdjustDetail_Enter);
            // 
            // labelG
            // 
            this.labelG.AutoSize = true;
            this.labelG.Location = new System.Drawing.Point(400, 86);
            this.labelG.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(205, 32);
            this.labelG.TabIndex = 2;
            this.labelG.Text = "Port Ad&just Detail:";
            // 
            // txtDefaultFontName
            // 
            this.txtDefaultFontName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDefaultFontName.BackColor = System.Drawing.SystemColors.Window;
            this.txtDefaultFontName.CausesValidation = false;
            this.txtDefaultFontName.Location = new System.Drawing.Point(600, 70);
            this.txtDefaultFontName.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtDefaultFontName.Name = "txtDefaultFontName";
            this.txtDefaultFontName.Size = new System.Drawing.Size(146, 39);
            this.txtDefaultFontName.TabIndex = 5;
            // 
            // labelFont
            // 
            this.labelFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFont.AutoSize = true;
            this.labelFont.Location = new System.Drawing.Point(400, 70);
            this.labelFont.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(219, 32);
            this.labelFont.TabIndex = 4;
            this.labelFont.Text = "Default &Font Name";
            // 
            // cboImageSaveType
            // 
            this.cboImageSaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImageSaveType.FormattingEnabled = true;
            this.cboImageSaveType.Items.AddRange(new object[] {
            "PNG ",
            "JPEG ",
            "BMP ",
            "Enhanced Metafiles (EMF)"});
            this.cboImageSaveType.Location = new System.Drawing.Point(12, 136);
            this.cboImageSaveType.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cboImageSaveType.Name = "cboImageSaveType";
            this.cboImageSaveType.Size = new System.Drawing.Size(398, 40);
            this.cboImageSaveType.TabIndex = 3;
            this.cboImageSaveType.Enter += new System.EventHandler(this.cboImageSaveType_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(286, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "&Default Image Save Type:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.chkSaveTADSToADV3Lite);
            this.groupBox2.Controls.Add(this.chkSaveToImage);
            this.groupBox2.Controls.Add(this.chkSaveToPDF);
            this.groupBox2.Controls.Add(this.cboImageSaveType);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 190);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox2.Size = new System.Drawing.Size(800, 188);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Smart Save";
            // 
            // chkSaveTADSToADV3Lite
            // 
            this.chkSaveTADSToADV3Lite.Checked = true;
            this.chkSaveTADSToADV3Lite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveTADSToADV3Lite.Location = new System.Drawing.Point(428, 42);
            this.chkSaveTADSToADV3Lite.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkSaveTADSToADV3Lite.Name = "chkSaveTADSToADV3Lite";
            this.chkSaveTADSToADV3Lite.Size = new System.Drawing.Size(200, 46);
            this.chkSaveTADSToADV3Lite.TabIndex = 6;
            this.chkSaveTADSToADV3Lite.Text = "Save TADS to ADV3Lite";
            // 
            // chkSaveToImage
            // 
            this.chkSaveToImage.Checked = true;
            this.chkSaveToImage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveToImage.Location = new System.Drawing.Point(214, 42);
            this.chkSaveToImage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkSaveToImage.Name = "chkSaveToImage";
            this.chkSaveToImage.Size = new System.Drawing.Size(200, 46);
            this.chkSaveToImage.TabIndex = 5;
            this.chkSaveToImage.Text = "Save to Image";
            // 
            // chkSaveToPDF
            // 
            this.chkSaveToPDF.Checked = true;
            this.chkSaveToPDF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveToPDF.Location = new System.Drawing.Point(14, 42);
            this.chkSaveToPDF.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkSaveToPDF.Name = "chkSaveToPDF";
            this.chkSaveToPDF.Size = new System.Drawing.Size(200, 46);
            this.chkSaveToPDF.TabIndex = 4;
            this.chkSaveToPDF.Text = "Save to PDF";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.m_preferredHorizontalMargin);
            this.groupBox3.Controls.Add(this.m_preferredVerticalMargin);
            this.groupBox3.Controls.Add(this.chkSpecifyMargins);
            this.groupBox3.Controls.Add(this.labelH);
            this.groupBox3.Controls.Add(this.labelV);
            this.groupBox3.Controls.Add(this.txtDefaultFontName);
            this.groupBox3.Controls.Add(this.labelFont);
            this.groupBox3.Controls.Add(this.chkDefaultHandDrawn);
            this.groupBox3.Location = new System.Drawing.Point(12, 390);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox3.Size = new System.Drawing.Size(800, 178);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "App defaults";
            // 
            // m_preferredHorizontalMargin
            // 
            this.m_preferredHorizontalMargin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_preferredHorizontalMargin.DecimalPlaces = 1;
            this.m_preferredHorizontalMargin.Location = new System.Drawing.Point(260, 70);
            this.m_preferredHorizontalMargin.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_preferredHorizontalMargin.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_preferredHorizontalMargin.Name = "m_preferredHorizontalMargin";
            this.m_preferredHorizontalMargin.Size = new System.Drawing.Size(110, 39);
            this.m_preferredHorizontalMargin.TabIndex = 8;
            this.m_preferredHorizontalMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_preferredHorizontalMargin.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // m_preferredVerticalMargin
            // 
            this.m_preferredVerticalMargin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_preferredVerticalMargin.DecimalPlaces = 1;
            this.m_preferredVerticalMargin.Location = new System.Drawing.Point(260, 120);
            this.m_preferredVerticalMargin.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.m_preferredVerticalMargin.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.m_preferredVerticalMargin.Name = "m_preferredVerticalMargin";
            this.m_preferredVerticalMargin.Size = new System.Drawing.Size(110, 39);
            this.m_preferredVerticalMargin.TabIndex = 10;
            this.m_preferredVerticalMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.m_preferredVerticalMargin.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // chkSpecifyMargins
            // 
            this.chkSpecifyMargins.Checked = true;
            this.chkSpecifyMargins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSpecifyMargins.Location = new System.Drawing.Point(20, 20);
            this.chkSpecifyMargins.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkSpecifyMargins.Name = "chkSpecifyMargins";
            this.chkSpecifyMargins.Size = new System.Drawing.Size(280, 46);
            this.chkSpecifyMargins.TabIndex = 7;
            this.chkSpecifyMargins.Text = "Specify margins";
            // 
            // labelH
            // 
            this.labelH.AutoSize = true;
            this.labelH.Location = new System.Drawing.Point(20, 70);
            this.labelH.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(125, 32);
            this.labelH.TabIndex = 9;
            this.labelH.Text = "Horizontal";
            // 
            // labelV
            // 
            this.labelV.AutoSize = true;
            this.labelV.Location = new System.Drawing.Point(20, 120);
            this.labelV.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelV.Name = "labelV";
            this.labelV.Size = new System.Drawing.Size(92, 32);
            this.labelV.TabIndex = 11;
            this.labelV.Text = "Vertical";
            // 
            // chkDefaultHandDrawn
            // 
            this.chkDefaultHandDrawn.Location = new System.Drawing.Point(400, 20);
            this.chkDefaultHandDrawn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkDefaultHandDrawn.Name = "chkDefaultHandDrawn";
            this.chkDefaultHandDrawn.Size = new System.Drawing.Size(280, 46);
            this.chkDefaultHandDrawn.TabIndex = 7;
            this.chkDefaultHandDrawn.Text = "Default Hand Drawn";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabToolTips);
            this.tabControl1.Location = new System.Drawing.Point(8, 2);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(864, 632);
            this.tabControl1.TabIndex = 8;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox3);
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Location = new System.Drawing.Point(8, 46);
            this.tabGeneral.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabGeneral.Size = new System.Drawing.Size(848, 578);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // tabToolTips
            // 
            this.tabToolTips.Controls.Add(this.groupBox4);
            this.tabToolTips.Controls.Add(this.chkShowObjectsInTooltip);
            this.tabToolTips.Location = new System.Drawing.Point(8, 46);
            this.tabToolTips.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabToolTips.Name = "tabToolTips";
            this.tabToolTips.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabToolTips.Size = new System.Drawing.Size(848, 578);
            this.tabToolTips.TabIndex = 1;
            this.tabToolTips.Text = "ToolTips";
            this.tabToolTips.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkLimitRoomDescriptionTooltipChars);
            this.groupBox4.Controls.Add(this.lblCharactersToShowRoom);
            this.groupBox4.Controls.Add(this.chkShowDescriptionsInTooltip);
            this.groupBox4.Controls.Add(this.txtNumOfRoomDescriptionChars);
            this.groupBox4.Controls.Add(this.txtNumOfConnectionDescriptionChars);
            this.groupBox4.Controls.Add(this.chkLimitConnectionDescriptionTooltipChars);
            this.groupBox4.Controls.Add(this.lblCharactersToShowConnection);
            this.groupBox4.Location = new System.Drawing.Point(14, 78);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox4.Size = new System.Drawing.Size(600, 294);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            // 
            // chkLimitRoomDescriptionTooltipChars
            // 
            this.chkLimitRoomDescriptionTooltipChars.Location = new System.Drawing.Point(42, 40);
            this.chkLimitRoomDescriptionTooltipChars.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkLimitRoomDescriptionTooltipChars.Name = "chkLimitRoomDescriptionTooltipChars";
            this.chkLimitRoomDescriptionTooltipChars.Size = new System.Drawing.Size(418, 46);
            this.chkLimitRoomDescriptionTooltipChars.TabIndex = 14;
            this.chkLimitRoomDescriptionTooltipChars.Text = "Limit Characters of Room Description";
            this.chkLimitRoomDescriptionTooltipChars.CheckedChanged += new System.EventHandler(this.chkLimitDescriptionTooltipChars_CheckedChanged);
            // 
            // lblCharactersToShowRoom
            // 
            this.lblCharactersToShowRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCharactersToShowRoom.AutoSize = true;
            this.lblCharactersToShowRoom.Location = new System.Drawing.Point(72, 104);
            this.lblCharactersToShowRoom.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCharactersToShowRoom.Name = "lblCharactersToShowRoom";
            this.lblCharactersToShowRoom.Size = new System.Drawing.Size(225, 32);
            this.lblCharactersToShowRoom.TabIndex = 16;
            this.lblCharactersToShowRoom.Text = "Characters to Show:";
            // 
            // chkShowDescriptionsInTooltip
            // 
            this.chkShowDescriptionsInTooltip.BackColor = System.Drawing.Color.White;
            this.chkShowDescriptionsInTooltip.Checked = true;
            this.chkShowDescriptionsInTooltip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowDescriptionsInTooltip.Location = new System.Drawing.Point(12, -8);
            this.chkShowDescriptionsInTooltip.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkShowDescriptionsInTooltip.Name = "chkShowDescriptionsInTooltip";
            this.chkShowDescriptionsInTooltip.Size = new System.Drawing.Size(336, 46);
            this.chkShowDescriptionsInTooltip.TabIndex = 21;
            this.chkShowDescriptionsInTooltip.Text = "Show Descriptions in Tooltip";
            this.chkShowDescriptionsInTooltip.UseVisualStyleBackColor = false;
            this.chkShowDescriptionsInTooltip.CheckedChanged += new System.EventHandler(this.chkShowDescriptionsInTooltip_CheckedChanged);
            // 
            // txtNumOfRoomDescriptionChars
            // 
            this.txtNumOfRoomDescriptionChars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumOfRoomDescriptionChars.DecimalPlaces = 1;
            this.txtNumOfRoomDescriptionChars.Location = new System.Drawing.Point(296, 100);
            this.txtNumOfRoomDescriptionChars.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtNumOfRoomDescriptionChars.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.txtNumOfRoomDescriptionChars.Name = "txtNumOfRoomDescriptionChars";
            this.txtNumOfRoomDescriptionChars.Size = new System.Drawing.Size(110, 39);
            this.txtNumOfRoomDescriptionChars.TabIndex = 17;
            this.txtNumOfRoomDescriptionChars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumOfRoomDescriptionChars.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // txtNumOfConnectionDescriptionChars
            // 
            this.txtNumOfConnectionDescriptionChars.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumOfConnectionDescriptionChars.DecimalPlaces = 1;
            this.txtNumOfConnectionDescriptionChars.Location = new System.Drawing.Point(296, 232);
            this.txtNumOfConnectionDescriptionChars.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtNumOfConnectionDescriptionChars.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.txtNumOfConnectionDescriptionChars.Name = "txtNumOfConnectionDescriptionChars";
            this.txtNumOfConnectionDescriptionChars.Size = new System.Drawing.Size(110, 39);
            this.txtNumOfConnectionDescriptionChars.TabIndex = 20;
            this.txtNumOfConnectionDescriptionChars.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumOfConnectionDescriptionChars.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // chkLimitConnectionDescriptionTooltipChars
            // 
            this.chkLimitConnectionDescriptionTooltipChars.Location = new System.Drawing.Point(42, 172);
            this.chkLimitConnectionDescriptionTooltipChars.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkLimitConnectionDescriptionTooltipChars.Name = "chkLimitConnectionDescriptionTooltipChars";
            this.chkLimitConnectionDescriptionTooltipChars.Size = new System.Drawing.Size(458, 46);
            this.chkLimitConnectionDescriptionTooltipChars.TabIndex = 18;
            this.chkLimitConnectionDescriptionTooltipChars.Text = "Limit Characters of Connection Description";
            this.chkLimitConnectionDescriptionTooltipChars.CheckedChanged += new System.EventHandler(this.chkLimitConnectionDescriptionTooltipChars_CheckedChanged);
            // 
            // lblCharactersToShowConnection
            // 
            this.lblCharactersToShowConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCharactersToShowConnection.AutoSize = true;
            this.lblCharactersToShowConnection.Location = new System.Drawing.Point(72, 236);
            this.lblCharactersToShowConnection.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCharactersToShowConnection.Name = "lblCharactersToShowConnection";
            this.lblCharactersToShowConnection.Size = new System.Drawing.Size(225, 32);
            this.lblCharactersToShowConnection.TabIndex = 19;
            this.lblCharactersToShowConnection.Text = "Characters to Show:";
            // 
            // chkShowObjectsInTooltip
            // 
            this.chkShowObjectsInTooltip.Checked = true;
            this.chkShowObjectsInTooltip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowObjectsInTooltip.Location = new System.Drawing.Point(26, 20);
            this.chkShowObjectsInTooltip.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.chkShowObjectsInTooltip.Name = "chkShowObjectsInTooltip";
            this.chkShowObjectsInTooltip.Size = new System.Drawing.Size(336, 46);
            this.chkShowObjectsInTooltip.TabIndex = 22;
            this.chkShowObjectsInTooltip.Text = "Show Objects in Tooltip";
            // 
            // AppSettingsDialog
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(892, 741);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_cancelButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppSettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(20, 20, 20, 20);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application Settings";
            this.Load += new System.EventHandler(this.AppSettingsDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_preferredHorizontalMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_preferredVerticalMargin)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabToolTips.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOfRoomDescriptionChars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumOfConnectionDescriptionChars)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

    private System.Windows.Forms.Button m_okButton;
		private System.Windows.Forms.Button m_cancelButton;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox m_invertWheelCheckBox;
        private System.Windows.Forms.ComboBox cboPortAdjustDetail;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.ComboBox cboImageSaveType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDefaultFontName;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.CheckBox chkSaveAtZoom;
        private System.Windows.Forms.CheckBox chkLoadLast;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkSpecifyMargins;
        private System.Windows.Forms.CheckBox chkDefaultHandDrawn;
        private System.Windows.Forms.CheckBox chkSaveTADSToADV3Lite;
        private System.Windows.Forms.CheckBox chkSaveToImage;
        private System.Windows.Forms.CheckBox chkSaveToPDF;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown m_preferredHorizontalMargin;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.NumericUpDown m_preferredVerticalMargin;
        private System.Windows.Forms.Label labelV;

    private System.Windows.Forms.CheckBox chkFullPathTitleBar;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabToolTips;
		private System.Windows.Forms.NumericUpDown txtNumOfRoomDescriptionChars;
		private System.Windows.Forms.Label lblCharactersToShowRoom;
		private System.Windows.Forms.CheckBox chkLimitRoomDescriptionTooltipChars;
		private System.Windows.Forms.NumericUpDown txtNumOfConnectionDescriptionChars;
		private System.Windows.Forms.Label lblCharactersToShowConnection;
		private System.Windows.Forms.CheckBox chkLimitConnectionDescriptionTooltipChars;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.CheckBox chkShowDescriptionsInTooltip;
		private System.Windows.Forms.CheckBox chkShowObjectsInTooltip;
	}
}