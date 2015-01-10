using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Trizbort
{
    public partial class RegionSettings : Form
    {
        public Region RegionToChange { get; private set; }
        private List<Region> regions;
        public RegionSettings( Region region, List<Region> regions)
        {
            InitializeComponent();
            RegionToChange = new Region {RColor = region.RColor, RegionName = region.RegionName, TextColor = region.TextColor};
            
            this.regions = regions;

            txtRegionName.Text = RegionToChange.RegionName;
            pnlRegionColor.BackColor = RegionToChange.RColor;
            lblTextColor.ForeColor = RegionToChange.TextColor;
        }

        private void btnRegionColor_Click(object sender, System.EventArgs e)
        {
            RegionToChange.RColor = Colors.ShowColorDialog(RegionToChange.RColor,this);
            refreshColorPanel();
        }

        private void refreshColorPanel()
        {
            pnlRegionColor.BackColor = RegionToChange.RColor;
            lblTextColor.ForeColor = RegionToChange.TextColor;
        }

        private void btnRegionTextColor_Click(object sender, System.EventArgs e)
        {
            RegionToChange.TextColor = Colors.ShowColorDialog(RegionToChange.TextColor,this);
            refreshColorPanel();
        }

        private void m_okButton_Click(object sender, System.EventArgs e)
        {
            RegionToChange.RegionName = txtRegionName.Text;
            DialogResult=DialogResult.OK;
        }
    }
}
