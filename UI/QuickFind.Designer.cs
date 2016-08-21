namespace Trizbort.UI
{
    partial class QuickFind
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
      this.labelX1 = new DevComponents.DotNetBar.LabelX();
      this.btnFind = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.cboFind = new DevComponents.DotNetBar.Controls.ComboBoxEx();
      this.SuspendLayout();
      // 
      // labelX1
      // 
      this.labelX1.AutoSize = true;
      // 
      // 
      // 
      this.labelX1.BackgroundStyle.Class = "";
      this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.labelX1.Location = new System.Drawing.Point(11, 6);
      this.labelX1.Name = "labelX1";
      this.labelX1.Size = new System.Drawing.Size(200, 15);
      this.labelX1.TabIndex = 2;
      this.labelX1.Text = "Enter room name, description, or objects";
      // 
      // btnFind
      // 
      this.btnFind.Location = new System.Drawing.Point(252, 74);
      this.btnFind.Name = "btnFind";
      this.btnFind.Size = new System.Drawing.Size(75, 23);
      this.btnFind.TabIndex = 3;
      this.btnFind.Text = "Find";
      this.btnFind.UseVisualStyleBackColor = true;
      this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(330, 74);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // cboFind
      // 
      this.cboFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
      this.cboFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
      this.cboFind.DisplayMember = "Text";
      this.cboFind.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
      this.cboFind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
      this.cboFind.FocusHighlightEnabled = true;
      this.cboFind.FormattingEnabled = true;
      this.cboFind.ItemHeight = 14;
      this.cboFind.Location = new System.Drawing.Point(11, 27);
      this.cboFind.Name = "cboFind";
      this.cboFind.PreventEnterBeep = true;
      this.cboFind.Size = new System.Drawing.Size(394, 20);
      this.cboFind.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
      this.cboFind.TabIndex = 5;
      this.cboFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cboFind_KeyPress);
      // 
      // QuickFind
      // 
      this.AcceptButton = this.btnFind;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(414, 109);
      this.Controls.Add(this.cboFind);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnFind);
      this.Controls.Add(this.labelX1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "QuickFind";
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Quick Find";
      this.Activated += new System.EventHandler(this.QuickFind_Activated);
      this.Deactivate += new System.EventHandler(this.QuickFind_Deactivate);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QuickFind_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

    #endregion
    private DevComponents.DotNetBar.LabelX labelX1;
    private System.Windows.Forms.Button btnFind;
    private System.Windows.Forms.Button btnCancel;
    private DevComponents.DotNetBar.Controls.ComboBoxEx cboFind;
  }
}