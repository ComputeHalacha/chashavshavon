using System;
using System.Linq;
using System.Windows.Forms;
using Tahara;

namespace Chashavshavon
{
    public partial class FrmAddKavuah : Form
    {
        public FrmAddKavuah()
        {
            InitializeComponent();
        }

        private void frmAddKavuah_Load(object sender, EventArgs e)
        {
            LoadIntervalNumbers();

            foreach (Entry entry in Program.EntryList)
            {
                cmbSettingEntry.Items.Add(entry);
            }
            if (Program.EntryList.Count > 0)
            {
                cmbSettingEntry.Items.Add("ראייה אחרת לא רשומה");

                Entry last = Program.EntryList.Last();
                //Select the last entry as it is probably the setting entry
                cmbSettingEntry.SelectedItem = last;
                if (last.Interval > 0)
                {
                    cmbNumber.SelectedItem = last.Interval;
                }
            }
            else
            {
                cmbSettingEntry.Items.Add("ראייה לא רשומה");
                cmbSettingEntry.SelectedIndex = 0;
            }
        }

        private void HaflagahTypeChanged(object sender, EventArgs e)
        {
            if (rbDayOfMonth.Checked)
            {
                LoadHebrewNumbers();
                lblNumber.Text = "תבחר יום בחדש";
            }
            else if (rbInterval.Checked || rbdayOfWeek.Checked)
            {
                LoadIntervalNumbers();
                lblNumber.Text = "תבחר מספר הימים בין הראיות";
            }
            else if (rbDilugHaflagah.Checked)
            {
                LoadDilugNumbers();
                lblNumber.Text = "תבחר מספר ימי דילוג של ההפלגה";
            }
            else if (rbDilugDayOfMonth.Checked)
            {
                LoadDilugNumbers();
                lblNumber.Text = "תבחר מספר ימי דילוג של יום החודש";
            }

            SetCancelOnaBeinanis();
        }

        private void LoadHebrewNumbers()
        {
            cmbNumber.DataSource = null;
            cmbNumber.Items.Clear();
            string[] a = GeneralUtils.DaysOfMonthHebrew;
            for (int i = 1; i < a.Length; i++)
            {
                cmbNumber.Items.Add(a[i]);
            }
            if (cmbSettingEntry.SelectedItem is Entry)
            {
                cmbNumber.SelectedIndex = ((Entry)cmbSettingEntry.SelectedItem).Day - 1;
            }
            else
            {
                cmbNumber.SelectedIndex = 0;
            }
        }

        private void LoadIntervalNumbers()
        {
            cmbNumber.DataSource = null;
            cmbNumber.Items.Clear();
            int[] a = new int[300];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i + 1;
            }
            cmbNumber.DataSource = a.Where(i => i != 0).ToList();
            if (cmbSettingEntry.SelectedItem is Entry && ((Entry)cmbSettingEntry.SelectedItem).Interval > 0)
            {
                cmbNumber.SelectedItem = ((Entry)cmbSettingEntry.SelectedItem).Interval;
            }
            else
            {
                cmbNumber.SelectedItem = 25;
            }
        }

        private void LoadDilugNumbers()
        {
            cmbNumber.DataSource = null;
            cmbNumber.Items.Clear();
            int[] a = new int[60];
            for (int i = -29, j = 0; i < 29; i++, j++)
            {
                a[j] = i;
            }
            cmbNumber.DataSource = a.Where(i => i != 0).ToList();
            cmbNumber.SelectedIndex = 29;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddedKavuah = new Kavuah(rbDay.Checked ? DayNight.Day : DayNight.Night);
            if (rbInterval.Checked)
            {
                AddedKavuah.KavuahType = cbMaayanPasuach.Checked ?
                    KavuahType.HaflagaMaayanPasuach : KavuahType.Haflagah;
            }
            else if (rbDayOfMonth.Checked)
            {
                AddedKavuah.KavuahType = cbMaayanPasuach.Checked ?
                    KavuahType.DayOfMonthMaayanPasuach : KavuahType.DayOfMonth;
            }
            else if (rbdayOfWeek.Checked)
            {
                AddedKavuah.KavuahType = KavuahType.DayOfWeek;
            }
            else if (rbDilugHaflagah.Checked)
            {
                AddedKavuah.KavuahType = KavuahType.DilugHaflaga;
            }
            else if (rbDilugDayOfMonth.Checked)
            {
                AddedKavuah.KavuahType = KavuahType.DilugDayOfMonth;
            }
            AddedKavuah.Number = cmbNumber.SelectedIndex + 1;


            AddedKavuah.Active = cbActive.Checked;
            AddedKavuah.CancelsOnahBeinanis = cbCancelsOnahBeinenis.Checked;
            AddedKavuah.IsMaayanPasuach = cbMaayanPasuach.Checked;

            if (cmbSettingEntry.SelectedItem is Entry entry)
            {
                AddedKavuah.SettingEntry = entry;
                AddedKavuah.SettingEntryDate = entry.DateTime;
                AddedKavuah.SettingEntryInterval = entry.Interval;

                if (AddedKavuah.DayNight != entry.DayNight)
                {
                    if (!Program.AskUser("העונה של הראייה הקובעת הוא [עונת " +
                            entry.HebrewDayNight +
                            "[,\nאבל העונה שנבחרה להקבוע החדש הוא " +
                            AddedKavuah.ToString() +
                            ".\nהאם להמשיך בכל זאת?"))
                    {
                        return;
                    }
                }

                if (AddedKavuah.KavuahType.In(KavuahType.Haflagah, KavuahType.HaflagaMaayanPasuach) &&
                    AddedKavuah.Number != entry.Interval)
                {
                    if (!Program.AskUser("ההפלגה של הראייה הקובעת הוא " +
                           entry.Interval.ToString() +
                            " ימים,\nאבל ההפלגה שנבחרה להקבוע החדש הוא " +
                            cmbNumber.Text +
                            " ימים.\nהאם להמשיך בכל זאת?"))
                    {
                        return;
                    }
                }

                if (AddedKavuah.KavuahType.In(KavuahType.DayOfMonth, KavuahType.DayOfMonthMaayanPasuach) &&
                    AddedKavuah.Number != entry.Day)
                {
                    if (!Program.AskUser("היום החודש של הראייה הקובעת הוא יום " +
                           GeneralUtils.DaysOfMonthHebrew[entry.Day] +
                            " בחודש,\nאבל היום החודש שנבחרה להקבוע החדש הוא יום " +
                            cmbNumber.Text +
                            " בחודש.\nהאם להמשיך בכל זאת?"))
                    {
                        return;
                    }
                    else
                    {
                        AddedKavuah.SettingEntryDate = new DateTime(entry.Year,
                            entry.Month.MonthInYear,
                            AddedKavuah.Number,
                            Program.HebrewCalendar);
                    }
                }
            }
            else if (AddedKavuah.KavuahType.In(KavuahType.DayOfMonth, KavuahType.DayOfMonthMaayanPasuach))
            {
                //The list generation mechanism of problem Onahs in frmMain uses the SettingEntryDate 
                //for Yom Hachodesh based Kavuahs to determine the problematic dates. So we need to set it.
                //If it was not set by the user, we set it to about a year ago.
                DateTime lastYear = Program.HebrewCalendar.AddYears(DateTime.Now, -1);
                AddedKavuah.SettingEntryDate = new DateTime(Program.HebrewCalendar.GetYear(lastYear),
                    Program.HebrewCalendar.GetMonth(lastYear),
                    AddedKavuah.Number,
                    Program.HebrewCalendar);
            }

            AddedKavuah.Notes = tbNotes.Text;

            Program.KavuahList.Add(AddedKavuah);
            DialogResult = DialogResult.OK;
        }

        private void cmbSettingEntry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSettingEntry.SelectedItem is Entry entry)
            {

                //Set the default day/night to the selected entries Day/Night
                rbDay.Checked = entry.DayNight == DayNight.Day;

                if (rbDayOfMonth.Checked)
                {
                    cmbNumber.SelectedIndex = entry.Day - 1;
                }
                else if ((rbInterval.Checked || rbdayOfWeek.Checked) && entry.Interval != 0)
                {
                    cmbNumber.SelectedItem = entry.Interval;
                }

                SetCancelOnaBeinanis();
            }
        }

        private void SetCancelOnaBeinanis()
        {
            if ((!cbMaayanPasuach.Checked) && (rbDay.Checked || rbInterval.Checked))
            {
                cbCancelsOnahBeinenis.Checked = true;
            }
            else
            {
                cbCancelsOnahBeinenis.Checked = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public Kavuah AddedKavuah { get; set; }
    }
}
