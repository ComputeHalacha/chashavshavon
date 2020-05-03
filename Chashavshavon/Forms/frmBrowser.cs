using System;
using System.Text;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class FrmBrowser : Form
    {
        private readonly bool _isForPrint = false;

        public FrmBrowser(bool print = false)
        {
            this.InitializeComponent();
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
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(this.saveFileDialog1.FileName, this.webBrowser1.DocumentText, Encoding.UTF8);
                Program.Inform("\"הקובץ " + this.saveFileDialog1.FileName + "\", נשמרה בהצלחה");
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
            get => this.webBrowser1.DocumentText;
            set => this.webBrowser1.DocumentText = value;
        }
    }
}
