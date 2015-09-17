using System;
using System.Text;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class frmBrowser : Form
    {
        private bool _isForPrint = false;

        public frmBrowser(bool print = false)
        {
            InitializeComponent();
            this._isForPrint = print;
        }

        private void frmBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Just making sure... browsers are resource guzzlers
            this.webBrowser1.Dispose();
        }      

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            this.webBrowser1.ShowPrintDialog();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.FileName = this.Text + ".html";
            if (this.saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.File.WriteAllText(this.saveFileDialog1.FileName, this.webBrowser1.DocumentText, Encoding.UTF8);
                MessageBox.Show("\"הקובץ " + this.saveFileDialog1.FileName + "\", נשמרה בהצלחה",
                    "חשבשבון",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (this._isForPrint)
            {
                this.webBrowser1.ShowPrintDialog();
            }
        }
        
        public string Html 
        { 
            get
            {
                return this.webBrowser1.DocumentText;
            }
            set
            {
                this.webBrowser1.DocumentText = value;
            }
        }          
    }
}
