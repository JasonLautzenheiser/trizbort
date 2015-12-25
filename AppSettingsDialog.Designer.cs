namespace Trizbort {
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
      this.chkSaveAtZoom = new System.Windows.Forms.CheckBox();
      this.chkLoadLast = new System.Windows.Forms.CheckBox();
      this.m_invertWheelCheckBox = new System.Windows.Forms.CheckBox();
      this.labelG = new System.Windows.Forms.Label();
      this.cboPortAdjustDetail = new System.Windows.Forms.ComboBox();
      this.txtDefaultFontName = new System.Windows.Forms.TextBox();
      this.labelFont = new System.Windows.Forms.Label();
      this.cboImageSaveType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.chkSaveToImage = new DevComponents.DotNetBar.Controls.CheckBoxX();
      this.chkSaveToPDF = new DevComponents.DotNetBar.Controls.CheckBoxX();
      this.chkSaveTADSToADV3Lite = new DevComponents.DotNetBar.Controls.CheckBoxX();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.chkDefaultHandDrawn = new DevComponents.DotNetBar.Controls.CheckBoxX();
      this.labelV = new System.Windows.Forms.Label();
      this.labelH = new System.Windows.Forms.Label();
      this.chkSpecifyMargins = new DevComponents.DotNetBar.Controls.CheckBoxX();
      this.m_preferredVerticalMargin = new System.Windows.Forms.NumericUpDown();
      this.m_preferredHorizontalMargin = new System.Windows.Forms.NumericUpDown();
      this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
      this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
      this.tabInform7 = new DevComponents.DotNetBar.SuperTabItem();
      this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
      this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
      this.superTabControl1.SuspendLayout();
      this.superTabControlPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_okButton
      // 
      this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_okButton.Location = new System.Drawing.Point(280, 318);
      this.m_okButton.Name = "m_okButton";
      this.m_okButton.Size = new System.Drawing.Size(75, 23);
      this.m_okButton.TabIndex = 4;
      this.m_okButton.Text = "&OK";
      this.m_okButton.UseVisualStyleBackColor = true;
      // 
      // m_cancelButton
      // 
      this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_cancelButton.Location = new System.Drawing.Point(361, 318);
      this.m_cancelButton.Name = "m_cancelButton";
      this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
      this.m_cancelButton.TabIndex = 5;
      this.m_cancelButton.Text = "C&ancel";
      this.m_cancelButton.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.BackColor = System.Drawing.Color.Transparent;
      this.groupBox1.Controls.Add(this.chkSaveAtZoom);
      this.groupBox1.Controls.Add(this.m_invertWheelCheckBox);
      this.groupBox1.Controls.Add(this.chkLoadLast);
      this.groupBox1.Controls.Add(this.cboPortAdjustDetail);
      this.groupBox1.Controls.Add(this.labelG);
      this.groupBox1.Location = new System.Drawing.Point(6, 16);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(400, 64);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Preferences";
      // 
      // chkLoadLast
      // 
      this.chkLoadLast.AutoSize = true;
      this.chkLoadLast.Location = new System.Drawing.Point(203, 20);
      this.chkLoadLast.Name = "chkLoadLast";
      this.chkLoadLast.Size = new System.Drawing.Size(131, 17);
      this.chkLoadLast.TabIndex = 2;
      this.chkLoadLast.Text = "&Open last project on start";
      this.toolTip2.SetToolTip(this.chkLoadLast, "If this is checked, Trizbort will load the last project on startup\r\n");
      this.chkLoadLast.UseVisualStyleBackColor = true;
      // 
      // chkSaveAtZoom
      // 
      this.chkSaveAtZoom.AutoSize = true;
      this.chkSaveAtZoom.Location = new System.Drawing.Point(23, 20);
      this.chkSaveAtZoom.Name = "chkSaveAtZoom";
      this.chkSaveAtZoom.Size = new System.Drawing.Size(131, 17);
      this.chkSaveAtZoom.TabIndex = 0;
      this.chkSaveAtZoom.Text = "&Save images at 100%";
      this.toolTip1.SetToolTip(this.chkSaveAtZoom, "If this is unchecked, images will be saved at their current zoom %\r\n");
      this.chkSaveAtZoom.UseVisualStyleBackColor = true;
      // 
      // m_invertWheelCheckBox
      // 
      this.m_invertWheelCheckBox.AutoSize = true;
      this.m_invertWheelCheckBox.Location = new System.Drawing.Point(23, 43);
      this.m_invertWheelCheckBox.Name = "m_invertWheelCheckBox";
      this.m_invertWheelCheckBox.Size = new System.Drawing.Size(152, 17);
      this.m_invertWheelCheckBox.TabIndex = 1;
      this.m_invertWheelCheckBox.Text = "Invert Mouse Wheel &Zoom";
      this.m_invertWheelCheckBox.UseVisualStyleBackColor = true;
      // 
      // cboPortAdjustDetail
      // 
      this.cboPortAdjustDetail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboPortAdjustDetail.FormattingEnabled = true;
      this.cboPortAdjustDetail.Items.AddRange(new object[] {
            "NSEW (4)",
            "Diagonals (8)",
            "All ports (16)"});
      this.cboPortAdjustDetail.Location = new System.Drawing.Point(300, 40);
      this.cboPortAdjustDetail.Name = "cboPortAdjustDetail";
      this.cboPortAdjustDetail.Size = new System.Drawing.Size(84, 17);
      this.cboPortAdjustDetail.TabIndex = 3;
      this.cboPortAdjustDetail.Enter += new System.EventHandler(this.cboPortAdjustDetail_Enter);
      // 
      // labelG
      // 
      this.labelG.AutoSize = true;
      this.labelG.Location = new System.Drawing.Point(200, 43);
      this.labelG.Name = "labelG";
      this.labelG.Size = new System.Drawing.Size(130,21);
      this.labelG.TabIndex = 2;
      this.labelG.Text = "Port Ad&just Detail:";
      // 
      // txtDefaultFontName
      // 
      this.txtDefaultFontName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDefaultFontName.BackColor = System.Drawing.SystemColors.Window;
      this.txtDefaultFontName.CausesValidation = false;
      this.txtDefaultFontName.Location = new System.Drawing.Point(300, 35);
      this.txtDefaultFontName.Name = "txtDefaultFontName";
      this.txtDefaultFontName.Size = new System.Drawing.Size(75, 21);
      this.txtDefaultFontName.TabIndex = 5;
      // 
      // labelFont
      // 
      this.labelFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.labelFont.AutoSize = true;
      this.labelFont.Location = new System.Drawing.Point(200, 35);
      this.labelFont.Name = "labelFont";
      this.labelFont.Size = new System.Drawing.Size(97, 13);
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
      this.cboImageSaveType.Location = new System.Drawing.Point(6, 68);
      this.cboImageSaveType.Name = "cboImageSaveType";
      this.cboImageSaveType.Size = new System.Drawing.Size(201, 21);
      this.cboImageSaveType.TabIndex = 3;
      this.cboImageSaveType.Enter += new System.EventHandler(this.cboImageSaveType_Enter);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(6, 52);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(133, 13);
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
      this.groupBox2.Location = new System.Drawing.Point(6, 78);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(400, 94);
      this.groupBox2.TabIndex = 12;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Smart Save";
      // 
      // labelV
      // 
      this.labelV.AutoSize = true;
      this.labelV.Location = new System.Drawing.Point(10, 60);
      this.labelV.Name = "label2";
      this.labelV.Size = new System.Drawing.Size(40,13);
      this.labelV.TabIndex = 11;
      this.labelV.Text = "Vertical";
      // 
      // m_preferredVerticalMargin
      // 
      this.m_preferredVerticalMargin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_preferredVerticalMargin.DecimalPlaces = 1;
      this.m_preferredVerticalMargin.Location = new System.Drawing.Point(130, 60);
      this.m_preferredVerticalMargin.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_preferredVerticalMargin.Name = "m_preferredVerticalMargin";
      this.m_preferredVerticalMargin.Size = new System.Drawing.Size(55, 21);
      this.m_preferredVerticalMargin.TabIndex = 10;
      this.m_preferredVerticalMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_preferredVerticalMargin.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
      // 
      // labelH
      // 
      this.labelH.AutoSize = true;
      this.labelH.Location = new System.Drawing.Point(10, 35);
      this.labelH.Name = "labelH";
      this.labelH.Size = new System.Drawing.Size(40,13);
      this.labelH.TabIndex = 9;
      this.labelH.Text = "Horizontal";
      // 
      // m_preferredHorizontalMargin
      // 
      this.m_preferredHorizontalMargin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_preferredHorizontalMargin.DecimalPlaces = 1;
      this.m_preferredHorizontalMargin.Location = new System.Drawing.Point(130, 35);
      this.m_preferredHorizontalMargin.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
      this.m_preferredHorizontalMargin.Name = "m_preferredHorizontalMargin";
      this.m_preferredHorizontalMargin.Size = new System.Drawing.Size(55, 21);
      this.m_preferredHorizontalMargin.TabIndex = 8;
      this.m_preferredHorizontalMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.m_preferredHorizontalMargin.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
      //
      // chkSpecifyMargins
      // 
      this.chkSpecifyMargins.BackgroundStyle.Class = "";
      this.chkSpecifyMargins.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.chkSpecifyMargins.Checked = true;
      this.chkSpecifyMargins.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkSpecifyMargins.CheckValue = "Y";
      this.chkSpecifyMargins.Location = new System.Drawing.Point(10, 10);
      this.chkSpecifyMargins.Name = "chkSpecifyMargins";
      this.chkSpecifyMargins.Size = new System.Drawing.Size(140, 23);
      this.chkSpecifyMargins.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
      this.chkSpecifyMargins.TabIndex = 7;
      this.chkSpecifyMargins.Text = "Specify margins";
      //
      // chkDefaultHandDrawn
      // 
      this.chkDefaultHandDrawn.BackgroundStyle.Class = "";
      this.chkDefaultHandDrawn.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.chkDefaultHandDrawn.Checked = false;
      this.chkDefaultHandDrawn.CheckState = System.Windows.Forms.CheckState.Unchecked;
      this.chkDefaultHandDrawn.CheckValue = "N";
      this.chkDefaultHandDrawn.Location = new System.Drawing.Point(200, 10);
      this.chkDefaultHandDrawn.Name = "chkDefaultHandDrawn";
      this.chkDefaultHandDrawn.Size = new System.Drawing.Size(140, 23);
      this.chkDefaultHandDrawn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
      this.chkDefaultHandDrawn.TabIndex = 7;
      this.chkDefaultHandDrawn.Text = "Default Hand Drawn";
      // 
      // chkSaveTADSToADV3Lite
      // 
      this.chkSaveTADSToADV3Lite.BackgroundStyle.Class = "";
      this.chkSaveTADSToADV3Lite.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.chkSaveTADSToADV3Lite.Checked = true;
      this.chkSaveTADSToADV3Lite.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkSaveTADSToADV3Lite.CheckValue = "Y";
      this.chkSaveTADSToADV3Lite.Location = new System.Drawing.Point(214, 21);
      this.chkSaveTADSToADV3Lite.Name = "chkSaveTADSToADV3Lite";
      this.chkSaveTADSToADV3Lite.Size = new System.Drawing.Size(140, 23);
      this.chkSaveTADSToADV3Lite.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
      this.chkSaveTADSToADV3Lite.TabIndex = 6;
      this.chkSaveTADSToADV3Lite.Text = "Save TADS to ADV3Lite";
      // 
      // chkSaveToImage
      // 
      // 
      // 
      // 
      this.chkSaveToImage.BackgroundStyle.Class = "";
      this.chkSaveToImage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.chkSaveToImage.Checked = true;
      this.chkSaveToImage.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkSaveToImage.CheckValue = "Y";
      this.chkSaveToImage.Location = new System.Drawing.Point(107, 21);
      this.chkSaveToImage.Name = "chkSaveToImage";
      this.chkSaveToImage.Size = new System.Drawing.Size(100, 23);
      this.chkSaveToImage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
      this.chkSaveToImage.TabIndex = 5;
      this.chkSaveToImage.Text = "Save to Image";
      // 
      // chkSaveToPDF
      // 
      // 
      // 
      // 
      this.chkSaveToPDF.BackgroundStyle.Class = "";
      this.chkSaveToPDF.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.chkSaveToPDF.Checked = true;
      this.chkSaveToPDF.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkSaveToPDF.CheckValue = "Y";
      this.chkSaveToPDF.Location = new System.Drawing.Point(7, 21);
      this.chkSaveToPDF.Name = "chkSaveToPDF";
      this.chkSaveToPDF.Size = new System.Drawing.Size(100, 23);
      this.chkSaveToPDF.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
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
      this.groupBox3.Location = new System.Drawing.Point(6, 172);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(400, 90);
      this.groupBox3.TabIndex = 1;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "App defaults";
      // 
      // superTabControl1
      // 
      this.superTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      // 
      // 
      // 
      // 
      // 
      // 
      this.superTabControl1.ControlBox.CloseBox.Name = "";
      // 
      // 
      // 
      this.superTabControl1.ControlBox.MenuBox.Name = "";
      this.superTabControl1.ControlBox.Name = "";
      this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
      this.superTabControl1.Controls.Add(this.superTabControlPanel1);
      this.superTabControl1.Controls.Add(this.superTabControlPanel2);
      this.superTabControl1.Location = new System.Drawing.Point(7, 13);
      this.superTabControl1.Name = "superTabControl1";
      this.superTabControl1.ReorderTabsEnabled = true;
      this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
      this.superTabControl1.SelectedTabIndex = 1;
      this.superTabControl1.Size = new System.Drawing.Size(432, 300);
      this.superTabControl1.TabFont = new System.Drawing.Font("Tahoma", 8.25F);
      this.superTabControl1.TabIndex = 7;
      this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1,
            this.tabInform7});
      this.superTabControl1.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.VisualStudio2008Dock;
      this.superTabControl1.Text = "superTabControl1";
      // 
      // superTabControlPanel2
      // 
      this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.superTabControlPanel2.Location = new System.Drawing.Point(0, 26);
      this.superTabControlPanel2.Name = "superTabControlPanel2";
      this.superTabControlPanel2.Size = new System.Drawing.Size(432, 211);
      this.superTabControlPanel2.TabIndex = 0;
      this.superTabControlPanel2.TabItem = this.tabInform7;
      // 
      // tabInform7
      // 
      this.tabInform7.AttachedControl = this.superTabControlPanel2;
      this.tabInform7.GlobalItem = false;
      this.tabInform7.Name = "tabInform7";
      this.tabInform7.Text = "Inform 7";
      this.tabInform7.Visible = false;
      // 
      // superTabControlPanel1
      // 
      this.superTabControlPanel1.Controls.Add(this.groupBox1);
      this.superTabControlPanel1.Controls.Add(this.groupBox2);
      this.superTabControlPanel1.Controls.Add(this.groupBox3);
      this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.superTabControlPanel1.Location = new System.Drawing.Point(0, 26);
      this.superTabControlPanel1.Name = "superTabControlPanel1";
      this.superTabControlPanel1.Size = new System.Drawing.Size(432, 360);
      this.superTabControlPanel1.TabIndex = 1;
      this.superTabControlPanel1.TabItem = this.superTabItem1;
      // 
      // superTabItem1
      // 
      this.superTabItem1.AttachedControl = this.superTabControlPanel1;
      this.superTabItem1.GlobalItem = false;
      this.superTabItem1.Name = "superTabItem1";
      this.superTabItem1.Text = "General";
      // 
      // AppSettingsDialog
      // 
      this.AcceptButton = this.m_okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_cancelButton;
      this.ClientSize = new System.Drawing.Size(446, 350);
      this.Controls.Add(this.superTabControl1);
      this.Controls.Add(this.m_okButton);
      this.Controls.Add(this.m_cancelButton);
      this.Font = new System.Drawing.Font("Tahoma", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AppSettingsDialog";
      this.Padding = new System.Windows.Forms.Padding(10);
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
      ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
      this.superTabControl1.ResumeLayout(false);
      this.superTabControlPanel1.ResumeLayout(false);
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
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSpecifyMargins;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkDefaultHandDrawn;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSaveTADSToADV3Lite;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSaveToImage;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSaveToPDF;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown m_preferredHorizontalMargin;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.NumericUpDown m_preferredVerticalMargin;
        private System.Windows.Forms.Label labelV;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem tabInform7;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
	}
}