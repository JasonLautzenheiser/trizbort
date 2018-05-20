namespace Trizbort.UI
{
  partial class MapStatisticsView
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
      this.txtStats = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // txtStats
      // 
      this.txtStats.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtStats.Location = new System.Drawing.Point(13, 13);
      this.txtStats.Multiline = true;
      this.txtStats.Name = "txtStats";
      this.txtStats.ReadOnly = true;
      this.txtStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtStats.Size = new System.Drawing.Size(400, 308);
      this.txtStats.TabIndex = 0;
      // 
      // MapStatisticsView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(425, 333);
      this.Controls.Add(this.txtStats);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MapStatisticsView";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Map Statistics";
      this.Load += new System.EventHandler(this.MapStatisticsView_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtStats;
  }
}