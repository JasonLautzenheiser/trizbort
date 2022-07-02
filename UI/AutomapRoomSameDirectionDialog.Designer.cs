using System.Drawing;

namespace Trizbort.UI
{
  sealed partial class AutomapRoomSameDirectionDialog
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
            this.btnRoom1 = new System.Windows.Forms.Button();
            this.btnRoom2 = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnKeepBoth = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRoom1
            // 
            this.btnRoom1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRoom1.Location = new System.Drawing.Point(522, 26);
            this.btnRoom1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnRoom1.Name = "btnRoom1";
            this.btnRoom1.Size = new System.Drawing.Size(334, 46);
            this.btnRoom1.TabIndex = 0;
            this.btnRoom1.Click += new System.EventHandler(this.btnRoom1_Click);
            // 
            // btnRoom2
            // 
            this.btnRoom2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRoom2.Location = new System.Drawing.Point(522, 84);
            this.btnRoom2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnRoom2.Name = "btnRoom2";
            this.btnRoom2.Size = new System.Drawing.Size(334, 46);
            this.btnRoom2.TabIndex = 1;
            this.btnRoom2.Click += new System.EventHandler(this.btnRoom2_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(26, 26);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(484, 162);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnKeepBoth
            // 
            this.btnKeepBoth.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnKeepBoth.Location = new System.Drawing.Point(522, 142);
            this.btnKeepBoth.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnKeepBoth.Name = "btnKeepBoth";
            this.btnKeepBoth.Size = new System.Drawing.Size(334, 46);
            this.btnKeepBoth.TabIndex = 3;
            this.btnKeepBoth.Text = "Keep Both";
            this.btnKeepBoth.Click += new System.EventHandler(this.btnKeepBoth_Click);
            // 
            // AutomapRoomSameDirectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(880, 214);
            this.Controls.Add(this.btnKeepBoth);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnRoom2);
            this.Controls.Add(this.btnRoom1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutomapRoomSameDirectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.AutomapRoomSameDirectionDialog_Shown);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnRoom1;
    private System.Windows.Forms.Button btnRoom2;
    private System.Windows.Forms.Label lblMessage;
    private System.Windows.Forms.Button btnKeepBoth;
  }
}