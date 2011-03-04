using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Chashavshavon
{
    public partial class frmPreferences : Form
    {
        private RegistryKey _regKey;
        public frmPreferences()
        {
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            this.rbLocsInDiaspora.Checked = (!this.rbLocsInIsrael.Checked);
            if (this.cbLocations.Items.Count == 0)
            {
                FillLocations();
            }
            this.cbLocations.Text = Properties.Settings.Default.UserLocation;
            this._regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon", true);
            this.cbRequirePassword.Checked = (this._regKey.GetValue("Straight").ToString() == "0");
            this.txtPassword.Text = Utils.GeneralUtils.Decrypt(this._regKey.GetValue("Entry").ToString(), "kedoshimteeheeyoo");
        }

        private void FillLocations()
        {
            cbLocations.Items.Clear();
            string xpath;

            if (this.rbLocsInIsrael.Checked == true)
            {
                xpath = "//Location[@Israel='true']/Name";
            }
            else
            {
                xpath = "//Location[not(@Israel)]/Name";
            }

            foreach (System.Xml.XmlNode node in frmMain.LocationsXmlDoc.SelectNodes(xpath))
            {
                this.cbLocations.Items.Add(node.InnerText);
            }
            this.cbLocations.SelectedIndex = 0;
        }

        private void rbLocsInIsrael_CheckedChanged(object sender, EventArgs e)
        {
            this.FillLocations();
        }

        private void rbLocsInDiaspora_CheckedChanged(object sender, EventArgs e)
        {
            this.FillLocations();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.cbRequirePassword.Checked && this.txtPassword.Text.Length < 4)
            {
                MessageBox.Show("הסיסמה חייבת להיות לפחות 4 תוים",
                            "חשבשבון",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                this._regKey.SetValue("Entry", Utils.GeneralUtils.Encrypt(this.txtPassword.Text, "kedoshimteeheeyoo"), RegistryValueKind.String);
                this._regKey.SetValue("Straight", this.cbRequirePassword.Checked ? "0" : "1", RegistryValueKind.String);
            }

            Properties.Settings.Default.UserLocation = this.cbLocations.Text;
            Properties.Settings.Default.Save();
            ((frmMain)this.Owner).AfterChangePreferences();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbRequirePassword_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPassword.Enabled = cbRequirePassword.Checked;
            if (this.txtPassword.Enabled)
            {
                this.txtPassword.Focus();
            }
        }

        private void frmPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._regKey.Close();
        }
    }
}