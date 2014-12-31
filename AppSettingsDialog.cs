using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Trizbort
{
	public partial class AppSettingsDialog : Form 
	{
		public AppSettingsDialog() 
		{
			InitializeComponent();
		}

		public bool InvertMouseWheel 
		{
			get { return m_invertWheelCheckBox.Checked; }
			set { m_invertWheelCheckBox.Checked = value; }
		}

		public int MouseDragButton 
		{
			get { return m_dragButtonComboBox.SelectedIndex; }
			set { m_dragButtonComboBox.SelectedIndex = value; }
		}
	}
}
