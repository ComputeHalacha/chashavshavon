using System;
using System.Linq;
using System.Windows.Forms;
using Tahara;

namespace Chashavshavon
{
    public partial class frmAddKavuah : Form
    {
        public frmAddKavuah()
        {
            this.InitializeComponent();
        }

        private void frmAddKavuah_Load(object sender, EventArgs e)
        {
            this.LoadIntervalNumbers();

            foreach (Entry entry in Program.EntryList)
            {
                this.cmbSettingEntry.Items.Add(entry);
            }
            if (Program.EntryList.Count > 0)
            {
                this.cmbSettingEntry.Items.Add("ראייה אחרת לא רשומה");

                Entry last = Program.EntryList.Last();
                //Select the last entry as it is probably the setting entry
                this.cmbSettingEntry.SelectedItem = last;
                if (last.Interval > 0)
                {
                    this.cmbNumber.SelectedItem = last.Interval;
                }
            }
            else
            {
                this.cmbSettingEntry.Items.Add("ראייה לא רשומה");
                this.cmbSettingEntry.SelectedIndex = 0;
            }
        }

        private void HaflagahTypeChanged(object sender, EventArgs e)
        {
            if (this.rbDayOfMonth.Checked)
            {
                this.LoadHebrewNumbers();
                this.lblNumber.Text = "תבחר יום בחדש";
            }
            else if (this.rbInterval.Checked || this.rbdayOfWeek.Checked)
            {
                this.LoadIntervalNumbers();
                this.lblNumber.Text = "תבחר מספר הימים בין הראיות";
            }
            else if (this.rbDilugHaflagah.Checked)
            {
                this.LoadDilugNumbers();
                this.lblNumber.Text = "תבחר מספר ימי דילוג של ההפלגה";
            }
            else if (this.rbDilugDayOfMonth.Checked)
            {
                this.LoadDilugNumbers();
                this.lblNumber.Text = "תבחר מספר ימי דילוג של יום החודש";
            }

            this.SetCancelOnaBeinanis();
        }

        private void LoadHebrewNumbers()
        {
            this.cmbNumber.DataSource = null;
            this.cmbNumber.Items.Clear();
            string[] a = GeneralUtils.DaysOfMonthHebrew;
            for (int i = 1; i < a.Length; i++)
            {
                this.cmbNumber.Items.Add(a[i]);
            }
            if (this.cmbSettingEntry.SelectedItem is Entry)
            {
                this.cmbNumber.SelectedIndex = ((Entry)this.cmbSettingEntry.SelectedItem).Day - 1;
            }
            else
            {
                this.cmbNumber.SelectedIndex = 0;
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
            if (this.cmbSettingEntry.SelectedItem is Entry && ((Entry)this.cmbSettingEntry.SelectedItem).Interval > 0)
            {
                this.cmbNumber.SelectedItem = ((Entry)this.cmbSettingEntry.SelectedItem).Interval;
            }
            else
            {
                this.cmbNumber.SelectedItem = 25;
            }
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
            this.AddedKavuah = new Kavuah(this.rbDay.Checked ? DayNight.Day : DayNight.Night);
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
            else if (this.rbdayOfWeek.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DayOfWeek;
            }
            else if (this.rbDilugHaflagah.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DilugHaflaga;
            }
            else if (this.rbDilugDayOfMonth.Checked)
            {
                this.AddedKavuah.KavuahType = KavuahType.DilugDayOfMonth;
            }
            this.AddedKavuah.Number = this.cmbNumber.SelectedIndex + 1;


            this.AddedKavuah.Active = this.cbActive.Checked;
            this.AddedKavuah.CancelsOnahBeinanis = this.cbCancelsOnahBeinenis.Checked;
            this.AddedKavuah.IsMaayanPasuach = this.cbMaayanPasuach.Checked;

            if (this.cmbSettingEntry.SelectedItem is Entry entry)
            {
                this.AddedKavuah.SettingEntry = entry;
                this.AddedKavuah.SettingEntryDate = entry.DateTime;
                this.AddedKavuah.SettingEntryInterval = entry.Interval;

                if (this.AddedKavuah.DayNight != entry.DayNight)
                {
                    if (MessageBox.Show("העונה של הראייה הקובעת הוא [עונת " +
                            entry.HebrewDayNight +
                            "[,\nאבל העונה שנבחרה להקבוע החדש הוא " +
                            this.AddedKavuah.ToString() +
                            ".\nהאם להמשיך בכל זאת?",
                        "חשבשבון",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                if (this.AddedKavuah.KavuahType.In(KavuahType.Haflagah, KavuahType.HaflagaMaayanPasuach) &&
                    this.AddedKavuah.Number != entry.Interval)
                {
                    if (MessageBox.Show("ההפלגה של הראייה הקובעת הוא " +
                           entry.Interval.ToString() +
                            " ימים,\nאבל ההפלגה שנבחרה להקבוע החדש הוא " +
                            this.cmbNumber.Text +
                            " ימים.\nהאם להמשיך בכל זאת?",
                        "חשבשבון",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                if (this.AddedKavuah.KavuahType.In(KavuahType.DayOfMonth, KavuahType.DayOfMonthMaayanPasuach) &&
                    this.AddedKavuah.Number != entry.Day)
                {
                    if (MessageBox.Show("היום החודש של הראייה הקובעת הוא יום " +
                           GeneralUtils.DaysOfMonthHebrew[entry.Day] +
                            " בחודש,\nאבל היום החודש שנבחרה להקבוע החדש הוא יום " +
                            this.cmbNumber.Text +
                            " בחודש.\nהאם להמשיך בכל זאת?",
                        "חשבשבון",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) != DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        this.AddedKavuah.SettingEntryDate = new DateTime(entry.Year,
                            entry.Month.MonthInYear,
                            this.AddedKavuah.Number,
                            Program.HebrewCalendar);
                    }
                }
            }
            else if (this.AddedKavuah.KavuahType.In(KavuahType.DayOfMonth, KavuahType.DayOfMonthMaayanPasuach))
            {
                //The list generation mechanism of problem Onahs in frmMain uses the SettingEntryDate 
                //for Yom Hachodesh based Kavuahs to determine the problematic dates. So we need to set it.
                //If it was not set by the user, we set it to about a year ago.
                DateTime lastYear = Program.HebrewCalendar.AddYears(DateTime.Now, -1);
                this.AddedKavuah.SettingEntryDate = new DateTime(Program.HebrewCalendar.GetYear(lastYear),
                    Program.HebrewCalendar.GetMonth(lastYear),
                    this.AddedKavuah.Number,
                    Program.HebrewCalendar);
            }

            this.AddedKavuah.Notes = this.tbNotes.Text;

            Program.KavuahList.Add(this.AddedKavuah);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cmbSettingEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbSettingEntry.SelectedItem is Entry entry)
            {

                //Set the default day/night to the selected entries Day/Night
                this.rbDay.Checked = entry.DayNight == DayNight.Day;

                if (this.rbDayOfMonth.Checked)
                {
                    this.cmbNumber.SelectedIndex = entry.Day - 1;
                }
                else if ((this.rbInterval.Checked || this.rbdayOfWeek.Checked) && entry.Interval != 0)
                {
                    this.cmbNumber.SelectedItem = entry.Interval;
                }

                this.SetCancelOnaBeinanis();
            }
        }

        private void SetCancelOnaBeinanis()
        {
            if ((!this.cbMaayanPasuach.Checked) && (this.rbDay.Checked || this.rbInterval.Checked))
            {
                this.cbCancelsOnahBeinenis.Checked = true;
            }
            else
            {
                this.cbCancelsOnahBeinenis.Checked = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public Kavuah AddedKavuah { get; set; }
    }
}
