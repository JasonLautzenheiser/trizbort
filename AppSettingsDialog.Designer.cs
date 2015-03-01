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
      this.panel1 = new System.Windows.Forms.Panel();
      this.m_okButton = new System.Windows.Forms.Button();
      this.m_cancelButton = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.chkSaveAtZoom = new System.Windows.Forms.CheckBox();
      this.cboImageSaveType = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.m_invertWheelCheckBox = new System.Windows.Forms.CheckBox();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.m_okButton);
      this.panel1.Controls.Add(this.m_cancelButton);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(10, 193);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(305, 35);
      this.panel1.TabIndex = 2;
      // 
      // m_okButton
      // 
      this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.m_okButton.Location = new System.Drawing.Point(149, 12);
      this.m_okButton.Name = "m_okButton";
      this.m_okButton.Size = new System.Drawing.Size(75, 23);
      this.m_okButton.TabIndex = 0;
      this.m_okButton.Text = "OK";
      this.m_okButton.UseVisualStyleBackColor = true;
      // 
      // m_cancelButton
      // 
      this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.m_cancelButton.Location = new System.Drawing.Point(230, 12);
      this.m_cancelButton.Name = "m_cancelButton";
      this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
      this.m_cancelButton.TabIndex = 1;
      this.m_cancelButton.Text = "Cancel";
      this.m_cancelButton.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.chkSaveAtZoom);
      this.groupBox1.Controls.Add(this.cboImageSaveType);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.m_invertWheelCheckBox);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Location = new System.Drawing.Point(10, 10);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(305, 183);
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
      this.chkSaveAtZoom.TabIndex = 5;
      this.chkSaveAtZoom.Text = "Save images at 100%";
      this.toolTip1.SetToolTip(this.chkSaveAtZoom, "If this is unchecked, images will be saved at their current zoom %\r\n");
      this.chkSaveAtZoom.UseVisualStyleBackColor = true;
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
      this.cboImageSaveType.Location = new System.Drawing.Point(23, 92);
      this.cboImageSaveType.Name = "cboImageSaveType";
      this.cboImageSaveType.Size = new System.Drawing.Size(201, 21);
      this.cboImageSaveType.TabIndex = 4;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(22, 76);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(133, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Default Image Save Type:";
      // 
      // m_invertWheelCheckBox
      // 
      this.m_invertWheelCheckBox.AutoSize = true;
      this.m_invertWheelCheckBox.Location = new System.Drawing.Point(23, 43);
      this.m_invertWheelCheckBox.Name = "m_invertWheelCheckBox";
      this.m_invertWheelCheckBox.Size = new System.Drawing.Size(152, 17);
      this.m_invertWheelCheckBox.TabIndex = 0;
      this.m_invertWheelCheckBox.Text = "Invert Mouse Wheel Zoom";
      this.m_invertWheelCheckBox.UseVisualStyleBackColor = true;
      // 
      // AppSettingsDialog
      // 
      this.AcceptButton = this.m_okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.m_cancelButton;
      this.ClientSize = new System.Drawing.Size(325, 238);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.panel1);
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
      this.panel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button m_okButton;
		private System.Windows.Forms.Button m_cancelButton;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox m_invertWheelCheckBox;
        private System.Windows.Forms.ComboBox cboImageSaveType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkSaveAtZoom;
        private System.Windows.Forms.ToolTip toolTip1;
	}
}