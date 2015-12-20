using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Trizbort
{
  public partial class AppSettingsDialog : Form
  {
    public AppSettingsDialog()
    {
      InitializeComponent();
    }

    public bool SaveAt100
    {
      get { return chkSaveAtZoom.Checked; }
      set { chkSaveAtZoom.Checked = value; }
    }

    public bool InvertMouseWheel
    {
      get { return m_invertWheelCheckBox.Checked; }
      set { m_invertWheelCheckBox.Checked = value; }
    }

    public float GenHorizontalMargin
    {
      get { return (float)m_preferredHorizontalMargin.Value; }
      set { m_preferredHorizontalMargin.Value = (decimal)value; }
    }

    public float GenVerticalMargin
    {
      get { return (float)m_preferredVerticalMargin.Value; }
      set { m_preferredVerticalMargin.Value = (decimal)value; }
    }

    public bool SpecifyGenMargins
    {
      get { return chkSpecifyMargins.Checked; }
      set { chkSpecifyMargins.Checked=value; }
    }

    public bool SaveTADSToADV3Lite 
    {
      get { return chkSaveTADSToADV3Lite.Checked; }
      set { chkSaveTADSToADV3Lite.Checked=value; }
    }

    public bool LoadLastProjectOnStart
    {
      get { return chkLoadLast.Checked; }
      set { chkLoadLast.Checked=value; }
    }

    public bool SaveToImage
    {
      get { return chkSaveToImage.Checked; }
      set { chkSaveToImage.Checked=value; }
    }

    public bool SaveToPDF
    {
      get { return chkSaveToPDF.Checked; }
      set { chkSaveToPDF.Checked = value; }
    }

    public int DefaultImageType
    {
      get { return cboImageSaveType.SelectedIndex; }
      set { cboImageSaveType.SelectedIndex = value; }
    }

    public string DefaultFontName
    {
      get { return txtDefaultFontName.Text; }
      set { txtDefaultFontName.Text = value; }
    }

    public int PortAdjustDetail
    {
      get { return cboPortAdjustDetail.SelectedIndex; }
      set { cboPortAdjustDetail.SelectedIndex = value; }
    }

    private void AppSettingsDialog_Load(object sender, System.EventArgs e)
    {
      
    }

    private void cboPortAdjustDetail_Enter(object sender, System.EventArgs e)
    {
      cboPortAdjustDetail.DroppedDown = true;
    }

    private void cboImageSaveType_Enter(object sender, System.EventArgs e)
    {
      cboImageSaveType.DroppedDown = true;
    }
  }
}
