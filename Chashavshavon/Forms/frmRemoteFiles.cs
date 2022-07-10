using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Chashavshavon
{
    public partial class FrmRemoteFiles : Form
    {
        private FrmMain _mainForm;

        public FrmRemoteFiles()
        {
            this.InitializeComponent();
        }

        private void frmRemoteFiles_Load(object sender, EventArgs e)
        {
            this._mainForm = (FrmMain)this.Owner;

            if (!this._mainForm.TestInternet())
            {
                Program.Exclaim("אין גישה לרשת כרגע");
                this.Close();
                return;
            }

            this.txtCurrentFileName.Text = this._mainForm.CurrentFileName;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.RemoteUserName))
            {
                this.txtUserName.Text = Properties.Settings.Default.RemoteUserName;
                this.txtPassword.Text = Properties.Settings.Default.RemotePassword;
                this.LogIn();
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (this.ValidateUserFields())
            {
                this.LogIn();
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            if (this.ValidateUserFields())
            {
                this.SaveUser();
                Utils.RemoteFunctions.RunRemoteAction("NewUser",
                    xml =>
                    {
                        Program.Inform("משתמש החדש נבראה בהצלחה");
                        this.lbFileNames.Items.Add("אין קבצים...");
                        this.txtCurrentFileName.Text = this._mainForm.CurrentFileName;
                    },
                    s => Program.Warn(s));
            }
        }

        private void btnSaveCurrent_Click(object sender, EventArgs e)
        {
            this.SaveUser();
            if (this.lbFileNames.Items.Contains(this.txtCurrentFileName.Text))
            {
                if (Program.AskUser(string.Format("קובץ בשם" + "{0}\"{1}\"{0}" +
                        ".כבר קיים ברשימת קובצים שלכם{0}?האם להחליפו בקובץ הנוכחי",
                        Environment.NewLine,
                        this.txtCurrentFileName.Text)))
                {
                    this.SaveCurrentFile(this.txtCurrentFileName.Text, this._mainForm.CurrentFileJson);

                }
            }
            else
            {
                string fileName = this.txtCurrentFileName.Text;

                Utils.RemoteFunctions.RunRemoteAction("AddFile",
                doc =>
                    {
                        Program.Inform(":הקובץ נשמרה ברשת בשם" + Environment.NewLine + fileName);
                        this.LogIn();
                    },
                s => Program.Warn(s),
                Utils.RemoteFunctions.NewParam("fileName", fileName),
                Utils.RemoteFunctions.NewParam("fileText", this._mainForm.CurrentFileJson));
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.OpenFileFromList();
        }

        private void lbFileNames_DoubleClick(object sender, EventArgs e)
        {
            this.OpenFileFromList();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if ((!this.ValidateUserFields()) ||
                !Program.AskUser("?האם אתם בטוחים שאתם רוצים למחוק משתמש הזאת מהמערכת עם כל הקבצים השמורים ברשימה"))
            {
                return;
            }

            Utils.RemoteFunctions.RunRemoteAction("DeleteUser",
                    doc =>
                    {
                        Program.Inform("המשתמש נמחקה בהצלחה");
                        this.lbFileNames.Items.Clear();
                        this.txtUserName.Text = "";
                        this.txtPassword.Text = "";
                        Properties.Settings.Default.RemotePassword = "";
                        Properties.Settings.Default.RemoteUserName = "";
                    },
                    s => Program.ErrorMessage(s));

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.SaveUser();
            if (this.lbFileNames.SelectedItem != null)
            {
                if (Program.AskUser(" האם אתם בטוחים שאתם רוצים למחוק את הקובץ בשם" + Environment.NewLine + this.lbFileNames.SelectedItem.ToString()))
                {
                    Utils.RemoteFunctions.RunRemoteAction("DeleteFile",
                        doc =>
                        {
                            Program.Inform("הקובץ " + this.lbFileNames.SelectedItem + " נמחקה");
                            this.lbFileNames.Items.Remove(this.lbFileNames.SelectedItem);
                        },
                        w => Program.Warn(w),
                        Utils.RemoteFunctions.NewParam("fileName", this.lbFileNames.SelectedItem.ToString()));
                }
            }
            else
            {
                Program.Exclaim("אנא בחרו קובץ מהרשימה לפני לחיצת הכפתור");
            }
        }

        private void llSite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.RunInDevMode ? Properties.Resources.LocalAppURL : Properties.Resources.AppURL);
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            this.SaveUser();
            if (this.lbFileNames.SelectedItem != null)
            {
                string html = Utils.RemoteFunctions.GetRemoteResponseText("GetFileAsHTML",
                                    Utils.RemoteFunctions.NewParam("fileName", this.lbFileNames.SelectedItem.ToString()));
                var fb = new FrmBrowser
                {
                    Text = "הצגת קובץ רשת - " + this.lbFileNames.SelectedItem.ToString(),
                    Html = html
                };
                fb.ShowDialog(this);
            }
            else
            {
                Program.Exclaim("אנא בחרו קובץ מהרשימה לפני לחיצת הכפתור");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveUser()
        {
            Properties.Settings.Default.RemoteUserName = this.txtUserName.Text;
            Properties.Settings.Default.RemotePassword = this.txtPassword.Text;
            Properties.Settings.Default.Save();
        }

        private void OpenFileFromList()
        {
            this.SaveUser();
            if (this.lbFileNames.SelectedItem != null)
            {
                using (var sfd = new SaveFileDialog
                {
                    OverwritePrompt = true,
                    DefaultExt = ".pmj",
                    InitialDirectory = Program.BackupFolderPath,
                    RestoreDirectory = false
                })
                {
                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        string xml = Utils.RemoteFunctions.GetFileXml(this.lbFileNames.SelectedItem.ToString());
                        File.WriteAllText(sfd.FileName, xml);
                        Program.Inform("הקובץ נשמרה ב" + sfd.FileName);
                    }

                }
            }
            else
            {
                Program.Exclaim("אנא בחרו קובץ מהרשימה לפני לחיצת הכפתור");
            }
        }

        private void LogIn()
        {
            Utils.RemoteFunctions.RunRemoteAction("GetFileList",
                doc =>
                    {
                        this.SaveUser();
                        this.lbFileNames.Items.Clear();
                        XmlNode files = doc.SelectSingleNode("//files");
                        if (files.HasChildNodes)
                        {
                            foreach (XmlNode file in files.SelectNodes("//file"))
                            {
                                this.lbFileNames.Items.Add(file.Attributes["fileName"].Value);
                            }
                        }
                        else
                        {
                            this.lbFileNames.Items.Add("אין קבצים...");
                        }
                    },
                s => Program.Warn(s));
        }

        private bool ValidateUserFields()
        {
            var messages = new List<string>();
            if (this.txtUserName.Text.Length < 3)
            {
                messages.Add(string.Format("{0}             שם משתמש שהוקש איננה חוקי  * {0}          אורך השם משתמש חייב להיות לפחות שני תווים", Environment.NewLine));
            }
            if (this.txtPassword.Text.Length < 4)
            {
                messages.Add(string.Format("{0}             סיסמה שהוקש איננה חוקי  * {0}          אורך הסיסמה חייב להיות לפחות ארבע תווים", Environment.NewLine));
            }
            if (messages.Count > 0)
            {
                Program.Inform("אנא תיקנו הבעיות הבאות" + Environment.NewLine +
                    string.Join(Environment.NewLine, messages.ToArray()));
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SaveCurrentFile(string fileName, string xml)
        {
            Utils.RemoteFunctions.RunRemoteAction("SetFileText",
                doc => Program.Inform(":הקובץ נשמרה ברשת בשם" + Environment.NewLine + fileName),
                s => Program.Warn(s),
                Utils.RemoteFunctions.NewParam("fileName", fileName),
                Utils.RemoteFunctions.NewParam("fileText", xml));
        }
    }
}