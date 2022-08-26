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
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAboutBox));
            panel1 = new System.Windows.Forms.Panel();
            llContact = new System.Windows.Forms.LinkLabel();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            lblVersion = new System.Windows.Forms.Label();
            llGetLatestVersion = new System.Windows.Forms.LinkLabel();
            panel2 = new System.Windows.Forms.Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right);
            panel1.BackColor = System.Drawing.Color.White;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(llContact);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Location = new System.Drawing.Point(11, 90);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(430, 236);
            panel1.TabIndex = 0;
            // 
            // llContact
            // 
            llContact.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            llContact.AutoSize = true;
            llContact.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            llContact.Location = new System.Drawing.Point(244, 198);
            llContact.Name = "llContact";
            llContact.Size = new System.Drawing.Size(171, 16);
            llContact.TabIndex = 6;
            llContact.TabStop = true;
            llContact.Text = "www.compute.co.il/contact";
            llContact.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(llContact_LinkClicked);
            // 
            // label5
            // 
            label5.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 177);
            label5.Location = new System.Drawing.Point(280, 18);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(135, 21);
            label5.TabIndex = 6;
            label5.Text = "הודעה חשובה:";
            // 
            // label4
            // 
            label4.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            label4.Font = new System.Drawing.Font("Tahoma", 7F);
            label4.Location = new System.Drawing.Point(12, 50);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(403, 148);
            label4.TabIndex = 5;
            label4.Text = resources.GetString("label4.Text");
            // 
            // button1
            // 
            button1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            button1.BackColor = System.Drawing.Color.Lavender;
            button1.BackgroundImage = Properties.Resources.DarkBlueMarbleTile;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Cursor = Cursors.Hand;
            button1.DialogResult = DialogResult.Cancel;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new System.Drawing.Font("Narkisim", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
            button1.ForeColor = System.Drawing.Color.Lavender;
            button1.Location = new System.Drawing.Point(12, 346);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "סגור";
            button1.UseVisualStyleBackColor = false;
            button1.Click += new System.EventHandler(button1_Click_1);
            // 
            // label1
            // 
            label1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Narkisim", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
            label1.ForeColor = System.Drawing.Color.SaddleBrown;
            label1.Location = new System.Drawing.Point(159, 8);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(225, 59);
            label1.TabIndex = 3;
            label1.Text = "חשבשבון";
            // 
            // label2
            // 
            label2.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Times New Roman", 9.75F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, 177);
            label2.Location = new System.Drawing.Point(135, 63);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(247, 20);
            label2.TabIndex = 4;
            label2.Text = "תוכנית לחישוב החודשי של בתי ישראל";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            pictureBox1.Image = Properties.Resources.scroll;
            pictureBox1.Location = new System.Drawing.Point(388, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(61, 61);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = System.Drawing.Color.Transparent;
            lblVersion.Location = new System.Drawing.Point(7, 14);
            lblVersion.Name = "lblVersion";
            lblVersion.RightToLeft = RightToLeft.Yes;
            lblVersion.Size = new System.Drawing.Size(105, 13);
            lblVersion.TabIndex = 5;
            lblVersion.Text = "גירסה";
            lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // llGetLatestVersion
            // 
            llGetLatestVersion.AutoSize = true;
            llGetLatestVersion.BackColor = System.Drawing.Color.Transparent;
            llGetLatestVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            llGetLatestVersion.Location = new System.Drawing.Point(7, 32);
            llGetLatestVersion.Name = "llGetLatestVersion";
            llGetLatestVersion.Size = new System.Drawing.Size(107, 16);
            llGetLatestVersion.TabIndex = 7;
            llGetLatestVersion.TabStop = true;
            llGetLatestVersion.Text = "חפש גירסה חדשה";
            llGetLatestVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(llGetLatestVersion_LinkClicked);
            // 
            // panel2
            // 
            panel2.BackgroundImage = Properties.Resources.BlueMarble;
            panel2.Controls.Add(button1);
            panel2.Controls.Add(llGetLatestVersion);
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(lblVersion);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new System.Drawing.Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(448, 387);
            panel2.TabIndex = 8;
            // 
            // AboutBox
            // 
            AcceptButton = button1;
            BackColor = System.Drawing.Color.Lavender;
            CancelButton = button1;
            ClientSize = new System.Drawing.Size(448, 387);
            Controls.Add(panel2);
            Font = new System.Drawing.Font("Narkisim", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 177);
            ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutBox";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += new System.EventHandler(AboutBox_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            lblVersion.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            llContact.Visible = llGetLatestVersion.Visible =
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
                    using (BackgroundWorker bgw = new BackgroundWorker())
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
