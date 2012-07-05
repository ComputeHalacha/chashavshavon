using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Chashavshavon.Utils;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class frmAddKavuah : Form
    {
        public frmAddKavuah()
        {
            InitializeComponent();
        }

        private void frmAddKavuah_Load(object sender, EventArgs e)
        {
            this.LoadIntervalNumbers();

            this.cmbSettingEntry.DataSource = Entry.EntryList;
            if (Entry.EntryList.Count > 0)
            {
                Entry last = Entry.EntryList.Last();
                //Select the last entry as it is probably the setting entry
                this.cmbSettingEntry.SelectedItem = last;
                if (last.Interval > 0)
                    this.cmbNumber.SelectedItem = last.Interval;
            }
        }

        private void HaflagahTypeChanged(object sender, EventArgs e)
        {
            if (this.rbDayOfMonth.Checked)
            {
                this.lblNumber.Text = "";
                this.cmbNumber.Visible = false;
            }
            else if (this.rbInterval.Checked || this.rbdayOfWeek.Checked)
            {
                this.cmbNumber.Visible = true;
                LoadIntervalNumbers();
                this.lblNumber.Text = "תבחר מספר הימים בין הראיות";
            }
            else if (rbDilugHaflagah.Checked)
            {
                this.cmbNumber.Visible = true;
                LoadDilugNumbers();
                this.lblNumber.Text = "תבחר מספר ימי דילוג של ההפלגה";
            }
            else if (this.rbDilugDayOfMonth.Checked)
            {
                this.cmbNumber.Visible = true;
                LoadDilugNumbers();
                this.lblNumber.Text = "תבחר מספר ימי דילוג של יום החודש";
            }
        }

        private void LoadIntervalNumbers()
        {
            this.cmbNumber.DataSource = null;
            this.cmbNumber.Items.Clear();
            int[] a = new int[300];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i + 1;
            }
            this.cmbNumber.DataSource = a.Where(i => i != 0).ToList();
            this.cmbNumber.SelectedIndex = 25;
        }

        private void LoadDilugNumbers()
        {
            this.cmbNumber.DataSource = null;
            this.cmbNumber.Items.Clear();
            int[] a = new int[60];
            for (int i = -29, j = 0; i < 29; i++, j++)
            {
                a[j] = i;
            }
            this.cmbNumber.DataSource = a.Where(i => i != 0).ToList();
            this.cmbNumber.SelectedIndex = 29;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.AddedKavuah = new Kavuah();
            this.AddedKavuah.DayNight = this.rbDay.Checked ? DayNight.Day : DayNight.Night;
            if (this.rbInterval.Checked)
            {
                this.AddedKavuah.KavuahType = this.cbMaayanPasuach.Checked ?
                    KavuahType.HaflagaMaayanPasuach : KavuahType.Haflagah;
            }
            else if (this.rbDayOfMonth.Checked)
            {
                this.AddedKavuah.KavuahType = this.cbMaayanPasuach.Checked ?
                    KavuahType.DayOfMonthMaayanPasuach : KavuahType.DayOfMonth;
            }
            else if (rbdayOfWeek.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DayOfWeek;
            }
            else if (rbDilugHaflagah.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DilugHaflaga;
            }
            else if (this.rbDilugDayOfMonth.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DilugDayOfMonth;
            }
            if (this.AddedKavuah.KavuahType.In(KavuahType.DayOfMonth, KavuahType.DayOfMonthMaayanPasuach))
            {
                if (this.cmbSettingEntry.Items.Count == 0)
                {
                    MessageBox.Show("כדי להוסיף קבוע של יום החודש, צריכים לבחור ראייה הקובע." + 
                        "\nאנא תחזרו למסך העיקרית ותוסיפו הראייה שע\"פ נקבעה הקבוע הזאת לרשימת הראיות.",
                    "חשבשבון",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                    return;
                }
                this.AddedKavuah.Number = Program.HebrewCalendar.GetDayOfMonth(((DateTime)this.cmbSettingEntry.SelectedValue));
            }
            else
            {
                this.AddedKavuah.Number = this.cmbNumber.SelectedIndex + 1;
            }
            this.AddedKavuah.Active = this.cbActive.Checked;
            this.AddedKavuah.IsMaayanPasuach = this.cbMaayanPasuach.Checked;
            if (this.cmbSettingEntry.Items.Count > 0)
            {
                if (AddedKavuah.DayNight != ((Entry)this.cmbSettingEntry.SelectedItem).DayNight)
                {
                    if (MessageBox.Show("העונה של הראייה הקובעת הוא <עונת " +
                            ((Entry)this.cmbSettingEntry.SelectedItem).HebrewDayNight + 
                            ">, אבל העונה שנבחרה להקבוע החדש הוא " + 
                            this.AddedKavuah.ToString() + 
                            ".\nהאם להוסיף בכל זאת?",
                        "חשבשבון",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                this.AddedKavuah.SettingEntryDate = (DateTime)this.cmbSettingEntry.SelectedValue;
            }
            this.AddedKavuah.Notes = this.tbNotes.Text;

            Kavuah.KavuahsList.Add(this.AddedKavuah);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cmbSettingEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set the default day/night to the selected entries Day/Night
            this.rbDay.Checked = ((Entry)this.cmbSettingEntry.SelectedItem).DayNight == DayNight.Day;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public Kavuah AddedKavuah { get; set; }
    }
}
