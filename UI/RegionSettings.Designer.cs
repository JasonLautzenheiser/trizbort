namespace Trizbort.UI
{
    partial class RegionSettings
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
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRegionColor = new System.Windows.Forms.Button();
            this.btnRegionTextColor = new System.Windows.Forms.Button();
            this.pnlRegionColor = new System.Windows.Forms.Panel();
            this.lblTextColor = new System.Windows.Forms.Label();
            this.txtRegionName = new System.Windows.Forms.TextBox();
            this.pnlRegionColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_okButton.Location = new System.Drawing.Point(120, 82);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(75, 23);
            this.m_okButton.TabIndex = 6;
            this.m_okButton.Text = "OK";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.m_okButton_Click);
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(201, 82);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_cancelButton.TabIndex = 7;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "&Name:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "&Region Color:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "&Text Color:";
            // 
            // btnRegionColor
            // 
            this.btnRegionColor.Location = new System.Drawing.Point(90, 28);
            this.btnRegionColor.Name = "btnRegionColor";
            this.btnRegionColor.Size = new System.Drawing.Size(24, 23);
            this.btnRegionColor.TabIndex = 3;
            this.btnRegionColor.Text = "...";
            this.btnRegionColor.UseVisualStyleBackColor = true;
            this.btnRegionColor.Click += new System.EventHandler(this.btnRegionColor_Click);
            // 
            // btnRegionTextColor
            // 
            this.btnRegionTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegionTextColor.Location = new System.Drawing.Point(90, 51);
            this.btnRegionTextColor.Name = "btnRegionTextColor";
            this.btnRegionTextColor.Size = new System.Drawing.Size(24, 23);
            this.btnRegionTextColor.TabIndex = 5;
            this.btnRegionTextColor.Text = "...";
            this.btnRegionTextColor.UseVisualStyleBackColor = true;
            this.btnRegionTextColor.Click += new System.EventHandler(this.btnRegionTextColor_Click);
            // 
            // pnlRegionColor
            // 
            this.pnlRegionColor.Controls.Add(this.lblTextColor);
            this.pnlRegionColor.Location = new System.Drawing.Point(120, 29);
            this.pnlRegionColor.Name = "pnlRegionColor";
            this.pnlRegionColor.Size = new System.Drawing.Size(157, 44);
            this.pnlRegionColor.TabIndex = 8;
            // 
            // lblTextColor
            // 
            this.lblTextColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTextColor.AutoSize = true;
            this.lblTextColor.Location = new System.Drawing.Point(43, 16);
            this.lblTextColor.Name = "lblTextColor";
            this.lblTextColor.Size = new System.Drawing.Size(71, 13);
            this.lblTextColor.TabIndex = 3;
            this.lblTextColor.Text = "&Region Color:";
            // 
            // txtRegionName
            // 
            this.txtRegionName.Location = new System.Drawing.Point(90, 6);
            this.txtRegionName.Name = "txtRegionName";
            this.txtRegionName.Size = new System.Drawing.Size(187, 20);
            this.txtRegionName.TabIndex = 9;
            this.txtRegionName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRegionName_KeyPress);
            // 
            // RegionSettings
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(288, 117);
            this.Controls.Add(this.txtRegionName);
            this.Controls.Add(this.pnlRegionColor);
            this.Controls.Add(this.btnRegionTextColor);
            this.Controls.Add(this.btnRegionColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_cancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegionSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Region Settings";
            this.pnlRegionColor.ResumeLayout(false);
            this.pnlRegionColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRegionColor;
        private System.Windows.Forms.Button btnRegionTextColor;
        private System.Windows.Forms.Panel pnlRegionColor;
        private System.Windows.Forms.Label lblTextColor;
        private System.Windows.Forms.TextBox txtRegionName;
    }
}