using System.Windows.Forms;
namespace Chashavshavon
{
    partial class AboutBox : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Panel panel1;
        private Button button1;
        private PictureBox pictureBox1;
        private Label label5;
        private Label label4;
        private Label label1;
        private Label label2;
        private Label lblVersion;
        private LinkLabel llContact;
        private LinkLabel llGetLatestVersion;
    }
}
