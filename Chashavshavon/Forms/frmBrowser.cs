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
            InitializeComponent();
            _isForPrint = print;
        }

        private void frmBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Just making sure... browsers are resource guzzlers
            webBrowser1.Dispose();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = Text + ".html";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveFileDialog1.FileName, webBrowser1.DocumentText, Encoding.UTF8);
                Program.Inform("\"הקובץ " + saveFileDialog1.FileName + "\", נשמרה בהצלחה");
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (_isForPrint)
            {
                webBrowser1.ShowPrintDialog();
            }
        }

        public string Html
        {
            get => webBrowser1.DocumentText;
            set => webBrowser1.DocumentText = value;
        }
    }
}
