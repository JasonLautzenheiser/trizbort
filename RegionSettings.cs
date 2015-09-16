using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Trizbort
{
  public partial class RegionSettings : Form
  {
    public Region RegionToChange { get; private set; }
    private readonly List<Region> regions;

    private readonly string originalName;

    public RegionSettings(Region region, List<Region> regions)
    {
      InitializeComponent();
      RegionToChange = new Region { RColor = region.RColor, RegionName = region.RegionName, TextColor = region.TextColor };

      this.regions = regions;
      originalName = RegionToChange.RegionName;
      txtRegionName.Text = RegionToChange.RegionName;
      if (RegionToChange.RegionName == Trizbort.Region.DefaultRegion)
        txtRegionName.Enabled = false;
      pnlRegionColor.BackColor = RegionToChange.RColor;
      lblTextColor.ForeColor = RegionToChange.TextColor;
    }

    private void btnRegionColor_Click(object sender, EventArgs e)
    {
      RegionToChange.RColor = Colors.ShowColorDialog(RegionToChange.RColor, this);
      refreshColorPanel();
    }

    private void refreshColorPanel()
    {
      pnlRegionColor.BackColor = RegionToChange.RColor;
      lblTextColor.ForeColor = RegionToChange.TextColor;
    }

    private void btnRegionTextColor_Click(object sender, EventArgs e)
    {
      RegionToChange.TextColor = Colors.ShowColorDialog(RegionToChange.TextColor, this);
      refreshColorPanel();
    }

    private void m_okButton_Click(object sender, EventArgs e)
    {
      txtRegionName.Text = txtRegionName.Text.Trim().Replace("\"", "'");
      if (Trizbort.Region.ValidRegionName(txtRegionName.Text))
      {
        if (!txtRegionName.Text.Equals(originalName, StringComparison.OrdinalIgnoreCase) && regions.Any(p => p.RegionName.Equals(txtRegionName.Text, StringComparison.OrdinalIgnoreCase)))
        {
          MessageBox.Show($"A Region already exists with the name '{txtRegionName.Text}'");
        }
        else
        {
          RegionToChange.RegionName = txtRegionName.Text;
          DialogResult = DialogResult.OK;
        }
      }
      else
      {
        MessageBox.Show("You can't have an empty region name","Empty Region Name",MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtRegionName.Focus();
      }
    }

    private void txtRegionName_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar.ToString() == "_" || e.KeyChar.ToString() == ":")
        e.Handled = true;
    }
  }
}
