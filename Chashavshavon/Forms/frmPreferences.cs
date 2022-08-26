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
            InitializeComponent();
            SetRadioButtons();
        }

        private void frmPreferences_Load(object sender, EventArgs e)
        {

            if (cbPlaces.Items.Count == 0)
            {
                FillPlaces();
            }

            for (int i = 0; i < cbPlaces.Items.Count; i++)
            {
                Location place = (Location)cbPlaces.Items[i];
                if (place.Name == Program.CurrentLocation.Name)
                {
                    cbPlaces.SelectedIndex = i;
                    break;
                }
            }

            _regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon", true);
            if (_regKey == null)
            {
                _regKey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Chashavshavon", RegistryKeyPermissionCheck.ReadWriteSubTree);
                _regKey.SetValue("Entry", "", RegistryValueKind.String);
                _regKey.SetValue("Straight", "1", RegistryValueKind.String);
            }
            else
            {
                cbRequirePassword.Checked = (_regKey.GetValue("Straight").ToString() == "0");
                string pw = Convert.ToString(_regKey.GetValue("Entry"));
                if (!string.IsNullOrEmpty(pw))
                {
                    txtPassword.Text = GeneralUtils.Decrypt(pw, "kedoshimteeheeyoo");
                }
            }
            _loading = false;
        }

        private void FillPlaces()
        {
            cbPlaces.Items.Clear();
            IEnumerable<Location> places = Locations.LocationsList.Where(l => l.IsInIsrael == rbPlacesInIsrael.Checked);
            //First Hebrew named ones
            foreach (Location place in places.Where(l => !string.IsNullOrWhiteSpace(l.NameHebrew)).OrderBy(l => l.NameHebrew))
            {
                cbPlaces.Items.Add(place);
            }
            foreach (Location place in places.Where(l => string.IsNullOrWhiteSpace(l.NameHebrew)).OrderBy(l => l.Name))
            {
                cbPlaces.Items.Add(place);
            }
            cbPlaces.SelectedIndex = 0;
        }

        private void rbPlacesInIsrael_CheckedChanged(object sender, EventArgs e)
        {
            FillPlaces();
        }

        private void rbPlacesInDiaspora_CheckedChanged(object sender, EventArgs e)
        {
            FillPlaces();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SavePreferencesBeforeClose())
            {
                Close();
            }
        }

        private bool SavePreferencesBeforeClose()
        {
            if (cbRequirePassword.Checked && txtPassword.Text.Length < 4)
            {
                Program.Exclaim("הסיסמה חייבת להיות לפחות 4 תוים");
                return false;
            }
            else
            {
                _regKey.SetValue("Entry", GeneralUtils.Encrypt(txtPassword.Text, "kedoshimteeheeyoo"), RegistryValueKind.String);
                _regKey.SetValue("Straight", cbRequirePassword.Checked ? "0" : "1", RegistryValueKind.String);
                _regKey.Close();
                //the form closing method checks to see if the regKey was nullified to determine if preferences were already saved.
                _regKey = null;
            }
            Program.CurrentLocation = ((Location)cbPlaces.SelectedItem);
            Properties.Settings.Default.LocationName = Program.CurrentLocation.Name;
            Properties.Settings.Default.Save();
            ((FrmMain)Owner).AfterChangePreferences();
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            _regKey.Close();
            //the form closing method checks to see if the regKey was nullified to determine if preferences were already saved.
            //In this function, where the user wants to cancel all changes and close this form, we inform the closing function,
            //that you can go ahead and close now without doing anything...
            _regKey = null;
            Close();
        }

        private void cbRequirePassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.Enabled = cbRequirePassword.Checked;
            if (txtPassword.Enabled)
            {
                txtPassword.Focus();
            }
        }

        private void frmPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If the user opted to close the form without clicking on a cancel button, we will save the preferences.
            //We check to see if the regKey was nullified to determine if the preferences were already saved.
            if (_regKey != null)
            {
                if (e.CloseReason == CloseReason.UserClosing && !SavePreferencesBeforeClose())
                {
                    e.Cancel = true;
                    return;
                }
                if (_regKey != null)
                {
                    _regKey.Close();
                    _regKey = null;
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
            if (txtPassword.PasswordChar == char.MinValue)
            {
                txtPassword.PasswordChar = '•';
            }
            else
            {
                txtPassword.PasswordChar = char.MinValue;
            }
        }

        private void rbOpenLastFile_CheckedChanged(object sender, EventArgs e)
        {
            if (!_loading)
            {
                Properties.Settings.Default.OpenLastFile = false;
                Properties.Settings.Default.openNewFile = false;
                Properties.Settings.Default.openFileDialog = false;
                Properties.Settings.Default.openNoFile = false;

                if (rbOpenLastFile.Checked)
                {
                    Properties.Settings.Default.OpenLastFile = true;
                }
                else if (rbOpenNewFile.Checked)
                {
                    Properties.Settings.Default.openNewFile = true;
                }
                else if (rbOpenFileDialog.Checked)
                {
                    Properties.Settings.Default.openFileDialog = true;
                }
                else if (rbDontOpenFile.Checked)
                {
                    Properties.Settings.Default.openNoFile = true;
                }
            }
        }

        private void SetRadioButtons()
        {
            bool wasLoading = _loading;
            _loading = true;
            rbPlacesInIsrael.Checked = Program.CurrentLocation.IsInIsrael;
            rbPlacesInDiaspora.Checked = (!rbPlacesInIsrael.Checked);
            rbOpenLastFile.Checked = Properties.Settings.Default.OpenLastFile;
            rbOpenNewFile.Checked = Properties.Settings.Default.openNewFile;
            rbOpenFileDialog.Checked = Properties.Settings.Default.openFileDialog;
            rbDontOpenFile.Checked = Properties.Settings.Default.openNoFile;
            _loading = wasLoading;
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            SetRadioButtons();
            Program.CurrentLocation = Locations.GetPlace(Properties.Settings.Default.LocationName);
            for (int i = 0; i < cbPlaces.Items.Count; i++)
            {
                Location place = (Location)cbPlaces.Items[i];
                if (place.Name == Program.CurrentLocation.Name)
                {
                    cbPlaces.SelectedIndex = i;
                    break;
                }
            }

            _regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon", true);
            cbRequirePassword.Checked = (_regKey.GetValue("Straight").ToString() == "0");
            string pw = Convert.ToString(_regKey.GetValue("Entry"));
            if (!string.IsNullOrEmpty(pw))
            {
                txtPassword.Text = GeneralUtils.Decrypt(pw, "kedoshimteeheeyoo");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (Program.AskUser("האם אתם בטוחים שברצונכם לאפס כל ההעדפות לברירת מחדל?",
                    "חשבשבון - איפוס"))
            {
                //The current file doesn't get reset.
                string currentFile = Properties.Settings.Default.CurrentFile;
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.CurrentFile = currentFile;

                SetRadioButtons();
                Program.CurrentLocation = Locations.GetPlace(Properties.Settings.Default.LocationName);
                for (int i = 0; i < cbPlaces.Items.Count; i++)
                {
                    Location place = (Location)cbPlaces.Items[i];
                    if (place.Name == Program.CurrentLocation.Name)
                    {
                        cbPlaces.SelectedIndex = i;
                        break;
                    }
                }

                _regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon", true);
                if (_regKey == null)
                {
                    _regKey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Chashavshavon", RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
                _regKey.SetValue("Entry", "", RegistryValueKind.String);
                _regKey.SetValue("Straight", "1", RegistryValueKind.String);
                cbRequirePassword.Checked = false;
                txtPassword.Text = "";
            }
        }
    }
}