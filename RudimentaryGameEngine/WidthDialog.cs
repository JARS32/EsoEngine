using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RudimentaryGameEngine
{
	public partial class WidthDialog : Form
	{
		public int width = 1;

		public WidthDialog()
		{
			InitializeComponent();
		}

		private void btnDone_Click(object sender, EventArgs e)
		{
			width = Convert.ToInt32(NUPWidth.Value);
			Close();
		}

		private void NUPWidth_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode.ToString() == "Return")
			{
				width = Convert.ToInt32(NUPWidth.Value);
				Close();
			}
		}
	}
}
