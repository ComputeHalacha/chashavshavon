using Chashavshavon.Utils;
using JewishCalendar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chashavshavon
{
    public partial class frmAddNewEntry : Form
    {
        //The combos are changed dynamically and we don't want to fire the change event during initial loading.
        private bool _loading = true;
        private DateTime _date;

        public frmAddNewEntry(DateTime date)
        {
            InitializeComponent();

            this._date = date;
            this.FillZmanData();
            this.cmbYear.SelectedItem = Program.HebrewCalendar.GetYear(this._date);
            this.cmbMonth.SelectedIndex = Program.HebrewCalendar.GetMonth(this._date) - 1;
            this.cmbDay.SelectedIndex = Program.HebrewCalendar.GetDayOfMonth(this._date) - 1;

            this.SetDateAndDayNight();

            //The timer is for the clock
            this.timer1.Start();

            this._loading = false;
            this.FillZmanim();
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            this.FillZmanim();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            this._loading = true;
            this.FillDays();
            this._loading = false;
            this.FillZmanim();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            this._loading = true;
            this.FillMonths();
            this.FillDays();
            this._loading = false;
            this.FillZmanim();
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            rbDay.Checked = !rbNight.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void FillZmanim()
        {
            if (this._loading)
            {
                return;
            }

            this._date = new DateTime((int)cmbYear.SelectedItem,
                ((MonthObject)cmbMonth.SelectedItem).MonthInYear,
                this.cmbDay.SelectedIndex + 1,
                Program.HebrewCalendar);

            var jd = new JewishDate(this._date);
            var suntimes = JewishCalendar.Zmanim.GetNetzShkia(this._date, Program.CurrentLocation);
            var netz = suntimes[0];
            var shkiah = suntimes[1];
            this.Text = this._date.ToString("dddd dd MMM yyyy", Program.CultureInfo);
            this.lblLocation.Text = Program.GetCurrentPlaceName() + " - " + this.Text;

            StringBuilder sb = new StringBuilder("נץ - ");
            sb.Append(netz.Hour.ToString());
            sb.Append(":");
            sb.Append(netz.Minute.ToString("0#"));
            sb.Append("\nשקיעה - ");
            sb.Append(shkiah.Hour.ToString());
            sb.Append(":");
            sb.Append(shkiah.Minute.ToString("0#"));

            lblZmanim.Text = sb.ToString();


            foreach (Control c in this.tableLayoutPanel1.Controls)
            {
                c.Dispose();
            }
            this.tableLayoutPanel1.Controls.Clear();
            if (Entry.EntryList.Count > 0)
            {
                var entry = Entry.EntryList.FirstOrDefault(en => !en.IsInvisible && Program.IsSameday(en.DateTime, this._date));
                if (entry != null)
                {
                    this.tableLayoutPanel1.Controls.Add(new Label()
                    {
                        Text = "ראיה בעונת " + entry.HebrewDayNight + " - הפלגה [" + entry.Interval.ToString() + "]",
                        ForeColor = Color.Red,
                        BackColor = Color.Pink,
                        Font = new Font(this.tableLayoutPanel1.Font, FontStyle.Bold),
                        AutoSize = true,
                        Padding = new Padding(5),
                        Margin = new Padding(5),
                        RightToLeft = RightToLeft.Yes
                    });
                }
            }
            if (ProblemOnahs.ProblemOnahList != null)
            {
                foreach (Onah o in ProblemOnahs.ProblemOnahList.Where(o => o.DateTime == this._date && !o.IsIgnored))
                {
                    this.tableLayoutPanel1.Controls.Add(new Label()
                    {
                        Text = "♦ " + o.HebrewDayNight + ": " + o.Name,
                        RightToLeft = RightToLeft.Yes,
                        AutoSize = true
                    });
                }
            }
            if (this.tableLayoutPanel1.Controls.Count == 0)
            {
                this.tableLayoutPanel1.Controls.Add(new Label()
                {
                    Text = "אין התראות ביום זה.",
                    RightToLeft = RightToLeft.Yes,
                    AutoSize = true
                });
            }
            if (this.tableLayoutPanel1.Controls.Count < 5)
            {
                this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                this.tableLayoutPanel1.BackColor = Color.White;
            }
            else
            {
                this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
                this.tableLayoutPanel1.BackColor = Color.WhiteSmoke;
            }
        }

        private void SetDateAndDayNight()
        {

            var now = DateTime.Now;
            var jd = new JewishDate(now);
            var zman = new JewishCalendar.Zmanim(now, Program.CurrentLocation);
            var isNight = zman.GetShkia() <= now.TimeOfDay;

            this.rbDay.Checked = !isNight;
            this.rbNight.Checked = isNight;

            string todayString = Program.Today.ToString("dddd dd MMM yyyy");
            this.lblToday.Text = todayString;
        }

        private void FillZmanData()
        {
            for (int i = 5750; i < 6000; i++)
            {
                cmbYear.Items.Add(i);
            }

            cmbYear.SelectedItem = Program.HebrewCalendar.GetYear(this._date);

            this.FillMonths();
            this.FillDays();
        }

        private void FillMonths()
        {
            string currentSelection = null;
            if (cmbMonth.Items.Count != 0)
            {
                currentSelection = cmbMonth.Text;
                cmbMonth.Items.Clear();
            }
            int year = (int)cmbYear.SelectedItem;
            for (int i = 0; i < Program.HebrewCalendar.GetMonthsInYear(year); i++)
            {
                MonthObject month = new MonthObject(year, i + 1);
                if (currentSelection == null && month.Year == Program.HebrewCalendar.GetYear(this._date) && month.MonthInYear == Program.HebrewCalendar.GetMonth(this._date))
                {
                    currentSelection = month.MonthName;
                }
                cmbMonth.Items.Add(month);
                if (month.MonthName == currentSelection)
                {
                    cmbMonth.SelectedItem = month;
                }
            }
        }

        private void FillDays()
        {
            MonthObject curMonth = (MonthObject)cmbMonth.SelectedItem;
            KeyValuePair<int, string> curDay;

            if (cmbDay.Items.Count == 0)
            {
                curDay = new KeyValuePair<int, string>(Program.HebrewCalendar.GetDayOfMonth(this._date),
                    Utils.Zmanim.DaysOfMonthHebrew[Program.HebrewCalendar.GetDayOfMonth(this._date)]);
            }
            else
            {
                curDay = (KeyValuePair<int, string>)cmbDay.SelectedItem;
            }

            cmbDay.Items.Clear();
            for (int i = 1; i < curMonth.DaysInMonth + 1; i++)
            {
                KeyValuePair<int, string> day = new KeyValuePair<int, string>(i, Utils.Zmanim.DaysOfMonthHebrew[i]);
                cmbDay.Items.Add(day);
            }

            //If the current month does not have as many days as the last one and the 30th day was selected, we go to day 29.
            if (curDay.Key > curMonth.DaysInMonth)
            {
                cmbDay.SelectedIndex = cmbDay.Items.Count - 1;
            }
            else
            {
                cmbDay.SelectedItem = curDay;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Program.MainForm.AddNewEntry(new Entry()
            {
                Day = this.cmbDay.SelectedIndex + 1,
                Month = (MonthObject)cmbMonth.SelectedItem,
                Year = (int)cmbYear.SelectedItem,
                DayNight = rbNight.Checked ? DayNight.Night : DayNight.Day,
                Notes = this.txtNotes.Text
            }, this);
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void cmbYear_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = GeneralUtils.ToJNum((int)e.Value % 1000);
        }
    }
}
