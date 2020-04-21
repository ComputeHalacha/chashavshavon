using Chashavshavon.Utils;
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
            InitializeComponent();
        }

        private void Preferences_Load(object sender, EventArgs e)
        {
            this.rbPlacesInIsrael.Checked = Program.CurrentPlace.IsInIsrael;
            this.rbPlacesInDiaspora.Checked = (!this.rbPlacesInIsrael.Checked);

            if (this.cbPlaces.Items.Count == 0)
            {
                FillPlaces();
            }

            for (int i = 0; i < this.cbPlaces.Items.Count; i++)
            {
                Place place = (Place)this.cbPlaces.Items[i];
                if (place.PlaceId == Program.CurrentPlace.PlaceId)
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
                this.txtPassword.Text = Utils.GeneralUtils.Decrypt(pw, "kedoshimteeheeyoo");
            }

            //We wait until the form is loaded...
            this.cbPlaces.SelectedIndexChanged += delegate
            {
                Zmanim.SetSummerTime();
                this.cbSummerTime.Checked = Properties.Settings.Default.IsSummerTime;                
            };
        }

        private void FillPlaces()
        {
            cbPlaces.Items.Clear();
            IEnumerable<Place> places = Utils.Place.PlacesList.Where(l => l.IsInIsrael == this.rbPlacesInIsrael.Checked);
            //First Hebrew named ones
            foreach (Place place in places.Where(l => !string.IsNullOrWhiteSpace(l.NameHebrew)).OrderBy(l => l.NameHebrew))
            {
                this.cbPlaces.Items.Add(place);
            }
            foreach (Place place in places.Where(l => string.IsNullOrWhiteSpace(l.NameHebrew)).OrderBy(l => l.Name))
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
                this._regKey.SetValue("Entry", Utils.GeneralUtils.Encrypt(this.txtPassword.Text, "kedoshimteeheeyoo"), RegistryValueKind.String);
                this._regKey.SetValue("Straight", this.cbRequirePassword.Checked ? "0" : "1", RegistryValueKind.String);
            }

            Program.CurrentPlace = ((Place)this.cbPlaces.SelectedItem);
            Properties.Settings.Default.UserPlaceId = Program.CurrentPlace.PlaceId;
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

        private void cbPlacess_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.Value is Place place)
            {
                e.Value = (string.IsNullOrEmpty(place.NameHebrew) ? place.Name : place.NameHebrew);
            }
        }
    }
}