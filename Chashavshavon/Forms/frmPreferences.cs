using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Chashavshavon.Utils;
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
            this.rbLocsInIsrael.Checked = Program.CurrentLocation.IsInIsrael;
            this.rbLocsInDiaspora.Checked = (!this.rbLocsInIsrael.Checked);

            if (this.cbLocations.Items.Count == 0)
            {
                FillLocations();
            }

            for (int i = 0; i < this.cbLocations.Items.Count; i++)
            {
                Location loc = (Location)this.cbLocations.Items[i];
                if (loc.LocationId == Program.CurrentLocation.LocationId)
                {
                    this.cbLocations.SelectedIndex = i;
                    break;
                }
            }

            this._regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon", true);
            this.cbRequirePassword.Checked = (this._regKey.GetValue("Straight").ToString() == "0");
            string pw = Convert.ToString(this._regKey.GetValue("Entry"));
            if (!string.IsNullOrEmpty(pw))
            {
                this.txtPassword.Text = Utils.GeneralUtils.Decrypt(pw, "kedoshimteeheeyoo");
            }

            //We wait until the form is loaded...
            this.cbLocations.SelectedIndexChanged += delegate
            {
                //We will only automatically set the DST if the users current time zone matches that of the selected location.
                if (TimeZoneInfo.Local.BaseUtcOffset.Hours == 
                    Program.CurrentLocation.TimeZone)
                {
                    this.cbSummerTime.Checked = DateTime.Now.IsDaylightSavingTime();
                }
            };
        }

        private void FillLocations()
        {
            cbLocations.Items.Clear();
            IEnumerable<Location> locs = clsLocations.Locations.Where(l => l.IsInIsrael == this.rbLocsInIsrael.Checked);
            //First Hebrew named ones
            foreach (Location loc in locs.Where(l => !string.IsNullOrWhiteSpace(l.NameHebrew)).OrderBy(l => l.NameHebrew))
            {
                this.cbLocations.Items.Add(loc);
            }
            foreach (Location loc in locs.Where(l => string.IsNullOrWhiteSpace(l.NameHebrew)).OrderBy(l => l.Name))
            {
                this.cbLocations.Items.Add(loc);
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

            Program.CurrentLocation = ((Location)this.cbLocations.SelectedItem);
            Properties.Settings.Default.UserLocationId = Program.CurrentLocation.LocationId;
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

        private void cbLocations_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.Value is Location)
            {
                Location loc = (Location)e.Value;
                e.Value = (string.IsNullOrEmpty(loc.NameHebrew) ? loc.Name : loc.NameHebrew);
            }
        }
    }
}