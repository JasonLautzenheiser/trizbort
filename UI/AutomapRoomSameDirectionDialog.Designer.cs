namespace Trizbort.UI
{
  partial class AutomapRoomSameDirectionDialog
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
      this.btnRoom1 = new DevComponents.DotNetBar.ButtonX();
      this.btnRoom2 = new DevComponents.DotNetBar.ButtonX();
      this.lblMessage = new DevComponents.DotNetBar.LabelX();
      this.btnKeepBoth = new DevComponents.DotNetBar.ButtonX();
      this.SuspendLayout();
      // 
      // btnRoom1
      // 
      this.btnRoom1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
      this.btnRoom1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
      this.btnRoom1.Location = new System.Drawing.Point(261, 13);
      this.btnRoom1.Name = "btnRoom1";
      this.btnRoom1.Size = new System.Drawing.Size(167, 23);
      this.btnRoom1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
      this.btnRoom1.TabIndex = 0;
      this.btnRoom1.Click += new System.EventHandler(this.btnRoom1_Click);
      // 
      // btnRoom2
      // 
      this.btnRoom2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
      this.btnRoom2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
      this.btnRoom2.Location = new System.Drawing.Point(261, 42);
      this.btnRoom2.Name = "btnRoom2";
      this.btnRoom2.Size = new System.Drawing.Size(167, 23);
      this.btnRoom2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
      this.btnRoom2.TabIndex = 1;
      this.btnRoom2.Click += new System.EventHandler(this.btnRoom2_Click);
      // 
      // lblMessage
      // 
      // 
      // 
      // 
      this.lblMessage.BackgroundStyle.Class = "";
      this.lblMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
      this.lblMessage.Location = new System.Drawing.Point(13, 13);
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size(242, 81);
      this.lblMessage.TabIndex = 2;
      this.lblMessage.TextLineAlignment = System.Drawing.StringAlignment.Near;
      this.lblMessage.WordWrap = true;
      // 
      // btnKeepBoth
      // 
      this.btnKeepBoth.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
      this.btnKeepBoth.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
      this.btnKeepBoth.Location = new System.Drawing.Point(261, 71);
      this.btnKeepBoth.Name = "btnKeepBoth";
      this.btnKeepBoth.Size = new System.Drawing.Size(167, 23);
      this.btnKeepBoth.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
      this.btnKeepBoth.TabIndex = 3;
      this.btnKeepBoth.Text = "Keep Both";
      this.btnKeepBoth.Click += new System.EventHandler(this.btnKeepBoth_Click);
      // 
      // AutomapRoomSameDirectionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(440, 107);
      this.Controls.Add(this.btnKeepBoth);
      this.Controls.Add(this.lblMessage);
      this.Controls.Add(this.btnRoom2);
      this.Controls.Add(this.btnRoom1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AutomapRoomSameDirectionDialog";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Shown += new System.EventHandler(this.AutomapRoomSameDirectionDialog_Shown);
      this.ResumeLayout(false);

    }

    #endregion

    private DevComponents.DotNetBar.ButtonX btnRoom1;
    private DevComponents.DotNetBar.ButtonX btnRoom2;
    private DevComponents.DotNetBar.LabelX lblMessage;
    private DevComponents.DotNetBar.ButtonX btnKeepBoth;
  }
}