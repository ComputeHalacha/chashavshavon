using System;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class FrmEnterPassword : Form
    {
        private readonly string _password;
        public FrmEnterPassword(string password)
        {
            InitializeComponent();
            _password = password;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            DialogResult = GeneralUtils.Encrypt(textBox1.Text, "kedoshimteeheeyoo") == _password ? DialogResult.Yes : DialogResult.No;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
