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
      this.m_invertWheelCheckBox = new System.Windows.Forms.CheckBox();
      this.cboImageSaveType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.chkSaveToImage = new DevComponents.DotNetBar.Controls.CheckBoxX();
      this.chkSaveToPDF = new DevComponents.DotNetBar.Controls.CheckBoxX();
      this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
      this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
      this.tabInform7 = new DevComponents.DotNetBar.SuperTabItem();
      this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
      this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
      this.superTabControl1.SuspendLayout();
      this.superTabControlPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_okButton
      // 
      this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_okButton.Location = new System.Drawing.Point(280, 256);
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
      this.m_cancelButton.Location = new System.Drawing.Point(361, 256);
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
      this.groupBox1.Location = new System.Drawing.Point(6, 16);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(305, 80);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Preferences";
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
      this.groupBox2.Controls.Add(this.chkSaveToImage);
      this.groupBox2.Controls.Add(this.chkSaveToPDF);
      this.groupBox2.Controls.Add(this.cboImageSaveType);
      this.groupBox2.Controls.Add(this.label2);
      this.groupBox2.Location = new System.Drawing.Point(3, 102);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(305, 100);
      this.groupBox2.TabIndex = 6;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Smart Save";
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
      this.superTabControl1.Size = new System.Drawing.Size(432, 237);
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
      this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.superTabControlPanel1.Location = new System.Drawing.Point(0, 26);
      this.superTabControlPanel1.Name = "superTabControlPanel1";
      this.superTabControlPanel1.Size = new System.Drawing.Size(432, 211);
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
      this.ClientSize = new System.Drawing.Size(446, 292);
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
        private System.Windows.Forms.ComboBox cboImageSaveType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkSaveAtZoom;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSaveToImage;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkSaveToPDF;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem tabInform7;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
	}
}