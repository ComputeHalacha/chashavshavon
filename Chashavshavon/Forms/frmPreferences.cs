using Chashavshavon.Utils;
using JewishCalendar;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class frmPreferences : Form
    {
        private RegistryKey _regKey;
        public frmPreferences()
        {
            this.InitializeComponent();
            this.SetRadioButtons();
        }

        private void Preferences_Load(object sender, EventArgs e)
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
                this._regKey.SetValue("Entry", GeneralUtils.Encrypt(this.txtPassword.Text, "kedoshimteeheeyoo"), RegistryValueKind.String);
                this._regKey.SetValue("Straight", this.cbRequirePassword.Checked ? "0" : "1", RegistryValueKind.String);
            }

            Program.CurrentLocation = ((Location)this.cbPlaces.SelectedItem);
            Properties.Settings.Default.LocationName = Program.CurrentLocation.Name;
            Properties.Settings.Default.Save();
            ((frmMain)this.Owner).AfterChangePreferences();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
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
            this._regKey.Close();
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
                this.txtPassword.PasswordChar = '•';
            }
            else
            {
                this.txtPassword.PasswordChar = char.MinValue;
            }
        }

        private void rbOpenLastFile_CheckedChanged(object sender, EventArgs e)
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

        private void SetRadioButtons()
        {
            this.rbPlacesInIsrael.Checked = Program.CurrentLocation.IsInIsrael;
            this.rbPlacesInDiaspora.Checked = (!this.rbPlacesInIsrael.Checked);
            this.rbOpenLastFile.Checked = Properties.Settings.Default.OpenLastFile;
            this.rbOpenNewFile.Checked = Properties.Settings.Default.openNewFile;
            this.rbOpenFileDialog.Checked = Properties.Settings.Default.openFileDialog;
            this.rbDontOpenFile.Checked = Properties.Settings.Default.openNoFile;
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
            if (MessageBox.Show("האם אתם בטוחים שברצונכם לאפס כל ההעדפות לברירת מחדל?",
                    "חשבשבון - איפוס",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
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