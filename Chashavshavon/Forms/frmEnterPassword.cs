using System;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class frmEnterPassword : Form
    {
        private string _password;
        public frmEnterPassword(string password)
        {
            InitializeComponent();
            this._password = password;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.DialogResult = Utils.GeneralUtils.Encrypt(this.textBox1.Text, "kedoshimteeheeyoo") == this._password ? DialogResult.Yes : DialogResult.No;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
