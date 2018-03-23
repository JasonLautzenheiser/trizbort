using System;
using System.Windows.Forms;

namespace Trizbort.UI {
  public partial class AppSettingsDialog : Form {
    public AppSettingsDialog() {
      InitializeComponent();
    }

    public string DefaultFontName { get => txtDefaultFontName.Text; set => txtDefaultFontName.Text = value; }

    public int DefaultImageType { get => cboImageSaveType.SelectedIndex; set => cboImageSaveType.SelectedIndex = value; }

    public float GenHorizontalMargin { get => (float) m_preferredHorizontalMargin.Value; set => m_preferredHorizontalMargin.Value = (decimal) value; }

    public float GenVerticalMargin { get => (float) m_preferredVerticalMargin.Value; set => m_preferredVerticalMargin.Value = (decimal) value; }

    public bool HandDrawnGlobal { get => chkDefaultHandDrawn.Checked; set => chkDefaultHandDrawn.Checked = value; }

    public bool InvertMouseWheel { get => m_invertWheelCheckBox.Checked; set => m_invertWheelCheckBox.Checked = value; }

    public bool LoadLastProjectOnStart { get => chkLoadLast.Checked; set => chkLoadLast.Checked = value; }

    public int PortAdjustDetail { get => cboPortAdjustDetail.SelectedIndex; set => cboPortAdjustDetail.SelectedIndex = value; }

    public bool SaveAt100 { get => chkSaveAtZoom.Checked; set => chkSaveAtZoom.Checked = value; }

    public bool SaveTADSToADV3Lite { get => chkSaveTADSToADV3Lite.Checked; set => chkSaveTADSToADV3Lite.Checked = value; }

    public bool SaveToImage { get => chkSaveToImage.Checked; set => chkSaveToImage.Checked = value; }

    public bool SaveToPDF { get => chkSaveToPDF.Checked; set => chkSaveToPDF.Checked = value; }

    public bool ShowFullPathInTitleBar { get => chkFullPathTitleBar.Checked; set => chkFullPathTitleBar.Checked = value; }

    public bool ShowToolTipsOnObjects { get => chkShowTooltips.Checked; set => chkShowTooltips.Checked = value; }

    public bool SpecifyGenMargins { get => chkSpecifyMargins.Checked; set => chkSpecifyMargins.Checked = value; }

    private void cboImageSaveType_Enter(object sender, EventArgs e) {
      cboImageSaveType.DroppedDown = true;
    }

    private void cboPortAdjustDetail_Enter(object sender, EventArgs e) {
      cboPortAdjustDetail.DroppedDown = true;
    }
  }
}