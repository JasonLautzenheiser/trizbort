using System.Windows.Forms;

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


    public int DefaultImageType
    {
      get { return cboImageSaveType.SelectedIndex; }
      set { cboImageSaveType.SelectedIndex = value; }
    }

    private void AppSettingsDialog_Load(object sender, System.EventArgs e)
    {
      
    }

    private void cboImageSaveType_Enter(object sender, System.EventArgs e)
    {
      cboImageSaveType.DroppedDown = true;
    }
  }
}
