using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class frmRemoteFiles : Form
    {
        private frmMain _mainForm;

        public frmRemoteFiles()
        {
            InitializeComponent();
        }

        private void frmRemoteFiles_Load(object sender, EventArgs e)
        {
            this._mainForm = (frmMain)this.Owner;

            if (!this._mainForm.TestInternet())
            {
                MessageBox.Show("��� ���� ���� ����", "�������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
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
            if (ValidateUserFields())
            {
                this.LogIn();
            }
        }

        private void btnNewUser_Click(object sender, EventArgs e)
        {
            if (ValidateUserFields())
            {
                this.SaveUser();
                if (this.GetRemoteResponse("NewUser") != null)
                {
                    MessageBox.Show("����� ���� ����� ������", "�������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.lbFileNames.Items.Add("��� �����...");
                    this.txtCurrentFileName.Text = this._mainForm.CurrentFileName;
                }
            }
        }

        private void btnSaveCurrent_Click(object sender, EventArgs e)
        {
            this.SaveUser();
            if (_mainForm.CurrentFileIsRemote && _mainForm.CurrentFileName == this.txtCurrentFileName.Text)
            {
                if (MessageBox.Show(string.Format("���� ���" + "{0}\"{1}\"{0}" + ".��� ���� ������ ������ ����{0}?��� ������� ����� ������", Environment.NewLine, this.txtCurrentFileName.Text),
                                    "�������",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2,
                                    MessageBoxOptions.RightAlign) == DialogResult.Yes)
                {
                    if (!this.SaveCurrentFile(this.txtCurrentFileName.Text, this._mainForm.CurrentFileXML))
                    {
                        return;
                    }
                }
            }
            else if (!this.AddCurrentFile(this.txtCurrentFileName.Text, this._mainForm.CurrentFileXML))
            {
                return;
            }
            this._mainForm.CurrentFileIsRemote = true;
            this._mainForm.CurrentFile = this.txtCurrentFileName.Text;
            MessageBox.Show(":����� ����� ���� ���" + Environment.NewLine + this.txtCurrentFileName.Text,
                            "�������",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RightAlign);
            this.LogIn();
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
            if ((!ValidateUserFields()) || MessageBox.Show("?��� ��� ������ ���� ����� ����� ����� ���� ������� �� �� ������ ������� ������",
                                    "�������",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2,
                                    MessageBoxOptions.RightAlign) != DialogResult.Yes)
            {
                return;
            }

            if (this.GetRemoteResponse("DeleteUser") != null)
            {
                MessageBox.Show("������ ����� ������", "�������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.lbFileNames.Items.Clear();
                this.txtUserName.Text = "";
                this.txtPassword.Text = "";
                Properties.Settings.Default.RemotePassword = "";
                Properties.Settings.Default.RemoteUserName = "";
                if (_mainForm.CurrentFileIsRemote)
                {
                    _mainForm.CurrentFileIsRemote = false;
                    _mainForm.CurrentFile = DateTime.Now.ToString("ddMMMyyyy_hhmm").Replace("\"", "").Replace("'", "") + ".pm";
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.SaveUser();
            if (this.lbFileNames.SelectedItem != null)
            {
                if (MessageBox.Show(" ��� ��� ������ ���� ����� ����� �� ����� ���" + Environment.NewLine + this.lbFileNames.SelectedItem.ToString(),
                                    "�������",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2,
                                    MessageBoxOptions.RightAlign) == DialogResult.Yes
                    &&
                    this.GetRemoteResponse("DeleteFile",
                                      Utils.RemoteFunctions.NewParam("fileName",
                                      this.lbFileNames.SelectedItem.ToString())) != null)
                {
                    MessageBox.Show("����� " + this.lbFileNames.SelectedItem + " �����",
                                    "�������",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.RightAlign);
                    if (this._mainForm.CurrentFileIsRemote && this._mainForm.CurrentFileName == this.lbFileNames.SelectedItem.ToString())
                    {
                        this._mainForm.CurrentFileIsRemote = false;
                        this._mainForm.CurrentFile = DateTime.Now.ToString("ddMMMyyyy_hhmm").Replace("\"", "").Replace("'", "") + ".pm";
                    }
                    this.lbFileNames.Items.Remove(this.lbFileNames.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("��� ���� ���� ������� ���� ����� ������",
                               "�������",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Exclamation,
                               MessageBoxDefaultButton.Button1,
                               MessageBoxOptions.RightAlign);
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
                this._mainForm.CurrentFile = lbFileNames.SelectedItem.ToString();
                this._mainForm.CurrentFileIsRemote = true;
                this._mainForm.LoadXmlFile();
                this.Close();
            }
            else
            {
                MessageBox.Show("��� ���� ���� ������� ���� ����� ������",
                                "�������",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.RightAlign);
            }
        }

        private void LogIn()
        {
            XmlDocument doc = this.GetRemoteResponse("GetFileList");
            if (doc != null)
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
                    this.lbFileNames.Items.Add("��� �����...");
                }
            }
        }

        private bool ValidateUserFields()
        {
            List<string> messages = new List<string>();
            if (this.txtUserName.Text.Length < 3)
            {
                messages.Add(string.Format("{0}             �� ����� ����� ����� ����  * {0}          ���� ��� ����� ���� ����� ����� ��� �����", Environment.NewLine));
            }
            if (txtPassword.Text.Length < 4)
            {
                messages.Add(string.Format("{0}             ����� ����� ����� ����  * {0}          ���� ������ ���� ����� ����� ���� �����", Environment.NewLine));
            }
            if (messages.Count > 0)
            {
                MessageBox.Show("��� ����� ������ �����" + Environment.NewLine + string.Join(Environment.NewLine, messages.ToArray()),
                                "�������",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.RightAlign);
                return false;
            }
            else
            {
                return true;
            }
        }

        private XmlDocument GetRemoteResponse(string function, params KeyValuePair<string, string>[] fields)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc = Utils.RemoteFunctions.ExecuteRemoteCall(function, fields);
                XmlNode errorNode = doc.SelectSingleNode("//error");
                if (errorNode != null)
                {
                    MessageBox.Show(errorNode.InnerText, "�������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    return null;
                }
                else
                {
                    return doc;
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message, "�������", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                return null;
            }
        }

        private bool AddCurrentFile(string fileName, string xml)
        {
            return this.GetRemoteResponse("AddFile",
                                    Utils.RemoteFunctions.NewParam("fileName", fileName),
                                    Utils.RemoteFunctions.NewParam("fileText", xml)) != null;
        }

        private bool SaveCurrentFile(string fileName, string xml)
        {
            return this.GetRemoteResponse("SetFileText",
                                    Utils.RemoteFunctions.NewParam("fileName", fileName),
                                    Utils.RemoteFunctions.NewParam("fileText", xml)) != null;
        }
    }
}