using Chashavshavon.Utils;
using JewishCalendar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class FrmPreferences : Form
    {
        private RegistryKey _regKey;
        private bool _loading = true;

        public FrmPreferences()
        {
            this.InitializeComponent();
            this.SetRadioButtons();
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {

            if (this.cbPlaces.Items.Count == 0)
            {
                this.FillPlaces();
            }

            for (int i = 0; i < this.cbPlaces.Items.Count; i++)
            {
                var place = (Location)this.cbPlaces.Items[i];
                if (place.Name == Program.CurrentLocation.Name)
                {
                    this.cbPlaces.SelectedIndex = i;
                    break;
                }
            }

            this._regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon", true);
            if (this._regKey == null)
            {
                this._regKey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Chashavshavon", RegistryKeyPermissionCheck.ReadWriteSubTree);
                this._regKey.SetValue("Entry", "", RegistryValueKind.String);
                this._regKey.SetValue("Straight", "1", RegistryValueKind.String);
            }
            else
            {
                this.cbRequirePassword.Checked = (this._regKey.GetValue("Straight").ToString() == "0");
                string pw = Convert.ToString(this._regKey.GetValue("Entry"));
                if (!string.IsNullOrEmpty(pw))
                {
                    this.txtPassword.Text = GeneralUtils.Decrypt(pw, "kedoshimteeheeyoo");
                }
            }
            this._loading = false;
        }

        private void FillPlaces()
        {
            this.cbPlaces.Items.Clear();
            IEnumerable<Location> places = Locations.LocationsList.Where(l => l.IsInIsrael == this.rbPlacesInIsrael.Checked);
            //First Hebrew named ones
            foreach (Location place in places.Where(l => !string.IsNullOrWhiteSpace(l.NameHebrew)).OrderBy(l => l.NameHebrew))
            {
                this.cbPlaces.Items.Add(place);
            }
            foreach (Location place in places.Where(l => string.IsNullOrWhiteSpace(l.NameHebrew)).OrderBy(l => l.Name))
            {
                this.cbPlaces.Items.Add(place);
            }
            this.cbPlaces.SelectedIndex = 0;
        }

        private void rbPlacesInIsrael_CheckedChanged(object sender, EventArgs e)
        {
            this.FillPlaces();
        }

        private void rbPlacesInDiaspora_CheckedChanged(object sender, EventArgs e)
        {
            this.FillPlaces();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.SavePreferencesBeforeClose())
            {
                this.Close();
            }
        }

        private bool SavePreferencesBeforeClose()
        {
            if (this.cbRequirePassword.Checked && this.txtPassword.Text.Length < 4)
            {
                Program.Exclaim("������ ����� ����� ����� 4 ����");
                return false;
            }
            else
            {
                this._regKey.SetValue("Entry", GeneralUtils.Encrypt(this.txtPassword.Text, "kedoshimteeheeyoo"), RegistryValueKind.String);
                this._regKey.SetValue("Straight", this.cbRequirePassword.Checked ? "0" : "1", RegistryValueKind.String);
                this._regKey.Close();
                //the form closing method checks to see if the regKey was nullified to determine if preferences were already saved.
                this._regKey = null;
            }
            Program.CurrentLocation = ((Location)this.cbPlaces.SelectedItem);
            Properties.Settings.Default.LocationName = Program.CurrentLocation.Name;
            Properties.Settings.Default.Save();
            ((FrmMain)this.Owner).AfterChangePreferences();
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            this._regKey.Close();
            //the form closing method checks to see if the regKey was nullified to determine if preferences were already saved.
            //In this function, where the user wants to cancel all changes and close this form, we inform the closing function,
            //that you can go ahead and close now without doing anything...
            this._regKey = null;
            this.Close();
        }

        private void cbRequirePassword_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPassword.Enabled = this.cbRequirePassword.Checked;
            if (this.txtPassword.Enabled)
            {
                this.txtPassword.Focus();
            }
        }

        private void frmPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If the user opted to close the form without clicking on a cancel button, we will save the preferences.
            //We check to see if the regKey was nullified to determine if the preferences were already saved.
            if (this._regKey != null)
            {
                if (e.CloseReason == CloseReason.UserClosing && !this.SavePreferencesBeforeClose())
                {
                    e.Cancel = true;
                    return;
                }
                if (this._regKey != null)
                {
                    this._regKey.Close();
                    this._regKey = null;
                }
            }
        }

        private void cbPlacess_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.Value is Location place)
            {
                e.Value = (string.IsNullOrEmpty(place.NameHebrew) ? place.Name : place.NameHebrew);
            }
        }

        private void pbShowPassword_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.PasswordChar == char.MinValue)
            {
                this.txtPassword.PasswordChar = '�';
            }
            else
            {
                this.txtPassword.PasswordChar = char.MinValue;
            }
        }

        private void rbOpenLastFile_CheckedChanged(object sender, EventArgs e)
        {
            if (!this._loading)
            {
                Properties.Settings.Default.OpenLastFile = false;
                Properties.Settings.Default.openNewFile = false;
                Properties.Settings.Default.openFileDialog = false;
                Properties.Settings.Default.openNoFile = false;

                if (this.rbOpenLastFile.Checked)
                {
                    Properties.Settings.Default.OpenLastFile = true;
                }
                else if (this.rbOpenNewFile.Checked)
                {
                    Properties.Settings.Default.openNewFile = true;
                }
                else if (this.rbOpenFileDialog.Checked)
                {
                    Properties.Settings.Default.openFileDialog = true;
                }
                else if (this.rbDontOpenFile.Checked)
                {
                    Properties.Settings.Default.openNoFile = true;
                }
            }
        }

        private void SetRadioButtons()
        {
            bool wasLoading = this._loading;
            this._loading = true;
            this.rbPlacesInIsrael.Checked = Program.CurrentLocation.IsInIsrael;
            this.rbPlacesInDiaspora.Checked = (!this.rbPlacesInIsrael.Checked);
            this.rbOpenLastFile.Checked = Properties.Settings.Default.OpenLastFile;
            this.rbOpenNewFile.Checked = Properties.Settings.Default.openNewFile;
            this.rbOpenFileDialog.Checked = Properties.Settings.Default.openFileDialog;
            this.rbDontOpenFile.Checked = Properties.Settings.Default.openNoFile;
            this._loading = wasLoading;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            this.SetRadioButtons();
            Program.CurrentLocation = Locations.GetPlace(Properties.Settings.Default.LocationName);
            for (int i = 0; i < this.cbPlaces.Items.Count; i++)
            {
                var place = (Location)this.cbPlaces.Items[i];
                if (place.Name == Program.CurrentLocation.Name)
                {
                    this.cbPlaces.SelectedIndex = i;
                    break;
                }
            }

            this._regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon", true);
            this.cbRequirePassword.Checked = (this._regKey.GetValue("Straight").ToString() == "0");
            string pw = Convert.ToString(this._regKey.GetValue("Entry"));
            if (!string.IsNullOrEmpty(pw))
            {
                this.txtPassword.Text = GeneralUtils.Decrypt(pw, "kedoshimteeheeyoo");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (Program.AskUser("��� ��� ������ �������� ���� �� ������� ������ ����?",
                    "������� - �����"))
            {
                //The current file doesn't get reset.
                string currentFile = Properties.Settings.Default.CurrentFile;
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.CurrentFile = currentFile;

                this.SetRadioButtons();
                Program.CurrentLocation = Locations.GetPlace(Properties.Settings.Default.LocationName);
                for (int i = 0; i < this.cbPlaces.Items.Count; i++)
                {
                    var place = (Location)this.cbPlaces.Items[i];
                    if (place.Name == Program.CurrentLocation.Name)
                    {
                        this.cbPlaces.SelectedIndex = i;
                        break;
                    }
                }

                this._regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon", true);
                if (this._regKey == null)
                {
                    this._regKey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Chashavshavon", RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                this._regKey.SetValue("Entry", "", RegistryValueKind.String);
                this._regKey.SetValue("Straight", "1", RegistryValueKind.String);
                this.cbRequirePassword.Checked = false;
                this.txtPassword.Text = "";
            }
        }
    }
}