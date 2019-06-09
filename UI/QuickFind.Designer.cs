﻿namespace Trizbort.UI
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
            this.labelX1 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            this.labelX1.Location = new System.Drawing.Point(22, 12);
            this.labelX1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(448, 32);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "Enter room name, description, or objects";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(504, 148);
            this.btnFind.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(150, 46);
            this.btnFind.TabIndex = 3;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(660, 148);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 46);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtFind
            // 
            this.txtFind.AcceptsReturn = true;
            this.txtFind.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFind.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFind.Location = new System.Drawing.Point(29, 65);
            this.txtFind.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(784, 39);
            this.txtFind.TabIndex = 6;
            this.txtFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFind_KeyPress);
            // 
            // QuickFind
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(828, 218);
            this.Controls.Add(this.txtFind);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.labelX1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
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
    private System.Windows.Forms.Label labelX1;
    private System.Windows.Forms.Button btnFind;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtFind;
  }
}