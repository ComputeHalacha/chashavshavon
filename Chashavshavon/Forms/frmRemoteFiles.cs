using Newtonsoft.Json.Linq;
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
            InitializeComponent();
        }

        private void frmRemoteFiles_Load(object sender, EventArgs e)
        {
            _mainForm = (FrmMain)Owner;

            if (!_mainForm.TestInternet())
            {
                Program.Exclaim("אין גישה לרשת כרגע");
                Close();
                return;
            }

            txtCurrentFileName.Text = _mainForm.CurrentFileName;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.RemoteUserName))
            {
                txtUserName.Text = Properties.Settings.Default.RemoteUserName;
                txtPassword.Text = Properties.Settings.Default.RemotePassword;
                LogIn();
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (ValidateUserFields())
            {
                LogIn();
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            if (ValidateUserFields())
            {
                SaveUser();
                Utils.RemoteFunctions.RunRemoteAction("NewUser",
                    xml =>
                    {
                        Program.Inform("משתמש החדש נבראה בהצלחה");
                        lbFileNames.Items.Add("אין קבצים...");
                        txtCurrentFileName.Text = _mainForm.CurrentFileName;
                    },
                    s => Program.Warn(s));
            }
        }

        private void btnSaveCurrent_Click(object sender, EventArgs e)
        {
            SaveUser();
            if (lbFileNames.Items.Contains(txtCurrentFileName.Text))
            {
                if (Program.AskUser(string.Format("קובץ בשם" + "{0}\"{1}\"{0}" +
                        ".כבר קיים ברשימת קובצים שלכם{0}?האם להחליפו בקובץ הנוכחי",
                        Environment.NewLine,
                        txtCurrentFileName.Text)))
                {
                    SaveCurrentFile(txtCurrentFileName.Text, _mainForm.CurrentFileJson);

                }
            }
            else
            {
                string fileName = txtCurrentFileName.Text;

                Utils.RemoteFunctions.RunRemoteAction("AddFile",
                doc =>
                    {
                        Program.Inform(":הקובץ נשמרה ברשת בשם" + Environment.NewLine + fileName);
                        LogIn();
                    },
                s => Program.Warn(s),
                Utils.RemoteFunctions.NewParam("fileName", fileName),
                Utils.RemoteFunctions.NewParam("fileText", _mainForm.CurrentFileJson));
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileFromList();
        }

        private void lbFileNames_DoubleClick(object sender, EventArgs e)
        {
            OpenFileFromList();
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if ((!ValidateUserFields()) ||
                !Program.AskUser("?האם אתם בטוחים שאתם רוצים למחוק משתמש הזאת מהמערכת עם כל הקבצים השמורים ברשימה"))
            {
                return;
            }

            Utils.RemoteFunctions.RunRemoteAction("DeleteUser",
                    doc =>
                    {
                        Program.Inform("המשתמש נמחקה בהצלחה");
                        lbFileNames.Items.Clear();
                        txtUserName.Text = "";
                        txtPassword.Text = "";
                        Properties.Settings.Default.RemotePassword = "";
                        Properties.Settings.Default.RemoteUserName = "";
                    },
                    s => Program.ErrorMessage(s));

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SaveUser();
            if (lbFileNames.SelectedItem != null)
            {
                if (Program.AskUser(" האם אתם בטוחים שאתם רוצים למחוק את הקובץ בשם" + Environment.NewLine + lbFileNames.SelectedItem.ToString()))
                {
                    Utils.RemoteFunctions.RunRemoteAction("DeleteFile",
                        doc =>
                        {
                            Program.Inform("הקובץ " + lbFileNames.SelectedItem + " נמחקה");
                            lbFileNames.Items.Remove(lbFileNames.SelectedItem);
                        },
                        w => Program.Warn(w),
                        Utils.RemoteFunctions.NewParam("fileName", lbFileNames.SelectedItem.ToString()));
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
            SaveUser();
            if (lbFileNames.SelectedItem != null)
            {
                string html = Utils.RemoteFunctions.GetRemoteResponseText("GetFileAsHTML",
                                    Utils.RemoteFunctions.NewParam("fileName", lbFileNames.SelectedItem.ToString()));
                FrmBrowser fb = new FrmBrowser
                {
                    Text = "הצגת קובץ רשת - " + lbFileNames.SelectedItem.ToString(),
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
            Close();
        }

        private void SaveUser()
        {
            Properties.Settings.Default.RemoteUserName = txtUserName.Text;
            Properties.Settings.Default.RemotePassword = txtPassword.Text;
            Properties.Settings.Default.Save();
        }

        private void OpenFileFromList()
        {
            SaveUser();
            if (lbFileNames.SelectedItem != null)
            {
                using (SaveFileDialog sfd = new SaveFileDialog
                {
                    OverwritePrompt = true,
                    DefaultExt = ".pmj",
                    InitialDirectory = Program.BackupFolderPath,
                    RestoreDirectory = false
                })
                {
                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        string json = Utils.RemoteFunctions.GetFileJson(lbFileNames.SelectedItem.ToString());
                        File.WriteAllText(sfd.FileName, json);
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
                        SaveUser();
                        lbFileNames.Items.Clear();

                        foreach (var file in doc["fileList"])
                        {
                            lbFileNames.Items.Add(file);
                        }

                        if (lbFileNames.Items.Count == 0)
                        {
                            lbFileNames.Items.Add("אין קבצים...");
                        }
                    },
                s => Program.Warn(s));
        }

        private bool ValidateUserFields()
        {
            List<string> messages = new List<string>();
            if (txtUserName.Text.Length < 3)
            {
                messages.Add(string.Format("{0}             שם משתמש שהוקש איננה חוקי  * {0}          אורך השם משתמש חייב להיות לפחות שני תווים", Environment.NewLine));
            }
            if (txtPassword.Text.Length < 4)
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