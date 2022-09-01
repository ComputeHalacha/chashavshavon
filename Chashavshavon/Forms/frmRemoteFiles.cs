using Chashavshavon.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Tahara;

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

            txtCurrentFileName.Text = Path.GetFileNameWithoutExtension(_mainForm.CurrentFileName);

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
                RemoteFunctions.RunRemoteAction("NewUser",
                    xml =>
                    {
                        Program.Inform("משתמש החדש נבראה בהצלחה");
                        lvFileNames.Items.Add("אין קבצים...");
                        txtCurrentFileName.Text = _mainForm.CurrentFileName;
                    },
                    s => Program.Warn(s));
            }
        }

        private void btnSaveCurrent_Click(object sender, EventArgs e)
        {
            SaveUser();
            if (lvFileNames.FindItemWithText(txtCurrentFileName.Text) != null &&
                !Program.AskUser(string.Format("קובץ בשם" + "{0}\"{1}\"{0}" +
                        ".כבר קיים ברשימת קובצים שלכם{0}?האם להחליפו בקובץ הנוכחי",
                        Environment.NewLine,
                        txtCurrentFileName.Text)))
            {
                return;
            }

            RemoteFunctions.SaveFileAs(_mainForm.CurrentFile,
                       txtCurrentFileName.Text,
                       doc => Program.Inform(":הקובץ נשמרה ברשת בשם" + Environment.NewLine + txtCurrentFileName.Text),
                       s => Program.Warn(s));
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

            RemoteFunctions.RunRemoteAction("DeleteUser",
                    doc =>
                    {
                        Program.Inform("המשתמש נמחקה בהצלחה");
                        lvFileNames.Items.Clear();
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
            if (lvFileNames.SelectedItems.Count > 0 || lvFileNames.Items.Count == 1)
            {
                var file = lvFileNames.Items.Count == 1 ? lvFileNames.Items[0] : lvFileNames.SelectedItems[0];
                var fileName = file?.Text ?? "";

                if (Program.AskUser(" האם אתם בטוחים שאתם רוצים למחוק את הקובץ בשם" + Environment.NewLine + fileName))
                {
                    RemoteFunctions.RunRemoteAction("DeleteFile",
                        doc =>
                        {
                            Program.Inform("הקובץ " + fileName + " נמחקה");
                            lvFileNames.Items.Remove(file);
                        },
                        w => Program.Warn(w),
                        RemoteFunctions.NewParam("fileName", fileName));
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
            if (lvFileNames.SelectedItems.Count > 0 || lvFileNames.Items.Count == 1)
            {
                var file = lvFileNames.Items.Count == 1 ? lvFileNames.Items[0] : lvFileNames.SelectedItems[0];
                var fileName = file?.Text ?? "";
                string html = RemoteFunctions.GetRemoteResponseText("GetFileAsHTML",
                                    RemoteFunctions.NewParam("fileName", fileName));
                FrmBrowser fb = new FrmBrowser
                {
                    Text = "הצגת קובץ רשת - " + fileName,
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
            if (lvFileNames.SelectedItems.Count > 0 || lvFileNames.Items.Count == 1)
            {
                var file = lvFileNames.Items.Count == 1 ? lvFileNames.Items[0] : lvFileNames.SelectedItems[0];
                var fileName = file.Text;

                using (SaveFileDialog sfd = new SaveFileDialog
                {
                    OverwritePrompt = true,
                    DefaultExt = ".pmj",
                    InitialDirectory = Program.BackupFolderPath,
                    RestoreDirectory = false,
                    FileName = fileName + ".pmj"
                })
                {
                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        string json = RemoteFunctions.GetFileJson(fileName);
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
            RemoteFunctions.RunRemoteAction("GetFileList",
                doc =>
                    {
                        SaveUser();
                        lvFileNames.Items.Clear();

                        foreach (var file in doc["fileList"])
                        {
                            lvFileNames.Items.Add(new ListViewItem(new string[]{
                                file.Value<string>("fileName"),
                                file.Value<string>("modifiedDate") }));
                        }

                        if (lvFileNames.Items.Count == 0)
                        {
                            lvFileNames.Items.Add("אין קבצים...");
                        }
                        else
                        {

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

        private void switcherAlwaysUpdateRemote_ChoiceSwitched(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            SaveUser();
            if (lvFileNames.SelectedItems.Count > 0 || lvFileNames.Items.Count == 1)
            {
                var file = lvFileNames.Items.Count == 1 ? lvFileNames.Items[0] : lvFileNames.SelectedItems[0];
                var fileName = file.Text;
                string fileText = RemoteFunctions.GetFileJson(fileName);

                using (FrmImport i = new FrmImport(fileText))
                {
                    bool added = false;
                    if (i.ShowDialog() == DialogResult.OK)
                    {
                        (List<Entry> entries, List<Kavuah> kavuahs, List<TaharaEvent> taharaEvents) = (i.EntryList, i.KavuahList, i.TaharaEventList);
                        foreach (Entry entry in entries)
                        {
                            if (!Program.EntryList.Exists(o => Onah.IsSimilarOnah(o, entry)))
                            {
                                Program.EntryList.Add(entry);
                                added = true;
                            }
                        }
                        foreach (Kavuah kavuah in kavuahs)
                        {
                            if (!Program.KavuahList.Exists(o => Kavuah.IsSameKavuah(o, kavuah)))
                            {
                                Program.KavuahList.Add(kavuah);
                                added = true;
                            }
                        }
                        foreach (TaharaEvent taharaEvent in taharaEvents)
                        {
                            if (!Program.TaharaEventList.Exists(te => te.TaharaEventType==taharaEvent.TaharaEventType && te.DateTime == taharaEvent.DateTime))
                            {
                                Program.TaharaEventList.Add(taharaEvent);
                                added = true;
                            }
                        }
                        if (added)
                        {
                            this._mainForm.SaveCurrentFile();
                            this._mainForm.RefreshData();
                        }
                    }
                }
            }
        }
    }
}