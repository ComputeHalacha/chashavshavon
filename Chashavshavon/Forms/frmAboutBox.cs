using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Chashavshavon
{
    partial class FrmAboutBox : Form
    {
        public FrmAboutBox()
        {
            this.InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAboutBox));
            this.panel1 = new System.Windows.Forms.Panel();
            this.llContact = new System.Windows.Forms.LinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.llGetLatestVersion = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right);
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.llContact);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(11, 90);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(430, 236);
            this.panel1.TabIndex = 0;
            // 
            // llContact
            // 
            this.llContact.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.llContact.AutoSize = true;
            this.llContact.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.llContact.Location = new System.Drawing.Point(244, 198);
            this.llContact.Name = "llContact";
            this.llContact.Size = new System.Drawing.Size(171, 16);
            this.llContact.TabIndex = 6;
            this.llContact.TabStop = true;
            this.llContact.Text = "www.compute.co.il/contact";
            this.llContact.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llContact_LinkClicked);
            // 
            // label5
            // 
            this.label5.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            this.label5.Location = new System.Drawing.Point(280, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "הודעה חשובה:";
            // 
            // label4
            // 
            this.label4.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.label4.Font = new System.Drawing.Font("Tahoma", 7F);
            this.label4.Location = new System.Drawing.Point(12, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(403, 148);
            this.label4.TabIndex = 5;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // button1
            // 
            this.button1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.button1.BackColor = System.Drawing.Color.Lavender;
            this.button1.BackgroundImage = Properties.Resources.DarkBlueMarbleTile;
            this.button1.BackgroundImageLayout = ImageLayout.Stretch;
            this.button1.Cursor = Cursors.Hand;
            this.button1.DialogResult = DialogResult.Cancel;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Narkisim", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
            this.button1.ForeColor = System.Drawing.Color.Lavender;
            this.button1.Location = new System.Drawing.Point(12, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 29);
            this.button1.TabIndex = 1;
            this.button1.Text = "סגור";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label1
            // 
            this.label1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Narkisim", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
            this.label1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.label1.Location = new System.Drawing.Point(159, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 59);
            this.label1.TabIndex = 3;
            this.label1.Text = "חשבשבון";
            // 
            // label2
            // 
            this.label2.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, 177);
            this.label2.Location = new System.Drawing.Point(135, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "תוכנית לחישוב החודשי של בתי ישראל";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = Properties.Resources.scroll;
            this.pictureBox1.Location = new System.Drawing.Point(388, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 61);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Location = new System.Drawing.Point(7, 14);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = RightToLeft.Yes;
            this.lblVersion.Size = new System.Drawing.Size(105, 13);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "גירסה";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // llGetLatestVersion
            // 
            this.llGetLatestVersion.AutoSize = true;
            this.llGetLatestVersion.BackColor = System.Drawing.Color.Transparent;
            this.llGetLatestVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            this.llGetLatestVersion.Location = new System.Drawing.Point(7, 32);
            this.llGetLatestVersion.Name = "llGetLatestVersion";
            this.llGetLatestVersion.Size = new System.Drawing.Size(107, 16);
            this.llGetLatestVersion.TabIndex = 7;
            this.llGetLatestVersion.TabStop = true;
            this.llGetLatestVersion.Text = "חפש גירסה חדשה";
            this.llGetLatestVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llGetLatestVersion_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = Properties.Resources.BlueMarble;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.llGetLatestVersion);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.lblVersion);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(448, 387);
            this.panel2.TabIndex = 8;
            // 
            // AboutBox
            // 
            this.AcceptButton = this.button1;
            this.BackColor = System.Drawing.Color.Lavender;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(448, 387);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
            this.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            this.lblVersion.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.llContact.Visible = this.llGetLatestVersion.Visible =
                Program.RunInDevMode || Utils.RemoteFunctions.IsConnectedToInternet();
        }

        private void llContact_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.compute.co.il/contact/?heb=y");
        }

        private void llGetLatestVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Version lVersion = Utils.RemoteFunctions.GetLatestVersion(),
                tVersion = Assembly.GetExecutingAssembly().GetName().Version;

            if (lVersion == null || lVersion <= tVersion)
            {
                Program.Inform("יש לכם גירסה האחרונה: " + tVersion.ToString());
            }
            else
            {
                if (Program.AskUser("יש גירסה חדשה. גירסה: " + lVersion.ToString() +
                    "\nהאם אתם רוצים להורידו ולהתקינו?"))
                {
                    using (var bgw = new BackgroundWorker())
                    {
                        bgw.DoWork += delegate (object sndr, DoWorkEventArgs dwea)
                        {
                            string installer = null;
                            try
                            {
                                installer = Utils.RemoteFunctions.DownloadLatestVersion();
                            }
                            catch (Exception ex)
                            {
                                Program.HandleException(ex, true);
                            }

                            if (!string.IsNullOrEmpty(installer) && File.Exists(installer))
                            {
                                System.Diagnostics.Process.Start(installer);
                                dwea.Result = true;
                            }
                            else
                            {
                                dwea.Result = false;
                            }
                        };
                        bgw.RunWorkerCompleted += delegate (object sndr, RunWorkerCompletedEventArgs rwcea)
                        {
                            if ((bool)rwcea.Result)
                            {
                                //We are installing a new version....
                                Application.Exit();
                            }
                            else
                            {
                                Program.Warn("נכשלה התקנת גירסה החדשה.");
                            }
                        };
                        bgw.RunWorkerAsync();
                    }
                }
            }
        }
    }
}
