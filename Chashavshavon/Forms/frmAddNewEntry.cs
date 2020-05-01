using JewishCalendar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tahara;

namespace Chashavshavon
{
    public partial class FrmAddNewEntry : Form
    {
        private static readonly Calendar _gregCal = new GregorianCalendar(GregorianCalendarTypes.Localized);

        //The combos are changed dynamically and we don't want to fire the change event during initial loading.
        private bool _loading = true;
        private DateTime _displayingSecularDate;
        private JewishDate _displayingJewishDate;
        private DateTime _secularDateAtMidnight;
        private IEnumerable<SpecialDay> _holidays;
        private DailyZmanim _dailyZmanim;


        public FrmAddNewEntry(DateTime date)
        {
            this.InitializeComponent();
            this._displayingSecularDate = date;
            this.InitDropdowns();
            this.GoToDate(date);
            this.SetDayNightRadio();
        }

        private void GoToDate(DateTime date)
        {
            this._displayingSecularDate = date;
            this._displayingJewishDate = new JewishDate(date);
            this.SetSecularDate();
            this._dailyZmanim = new DailyZmanim(this._secularDateAtMidnight, Program.CurrentLocation);
            this.ShowCurrentDateZmanimData();
        }

        private void ShowCurrentDateZmanimData()
        {
            this._holidays = Zmanim.GetHolidays(this._displayingJewishDate, Program.CurrentLocation.IsInIsrael).Cast<SpecialDay>();
            this.cmbYear.SelectedItem = Program.HebrewCalendar.GetYear(this._displayingSecularDate);
            this.cmbMonth.SelectedIndex = Program.HebrewCalendar.GetMonth(this._displayingSecularDate) - 1;
            this.cmbDay.SelectedIndex = Program.HebrewCalendar.GetDayOfMonth(this._displayingSecularDate) - 1;

            this._loading = false;
            this.FillProblemOnahs();
            this.ShowDateData();

            string jdateText = this._displayingJewishDate.ToLongDateStringHeb();
            this.Text = jdateText;
            this.lblCaption.Text = this.Text;
        }

        private void GoToSelectedDate()
        {
            this.GoToDate(new DateTime((int)this.cmbYear.SelectedItem,
                ((MonthObject)this.cmbMonth.SelectedItem).MonthInYear,
                this.cmbDay.SelectedIndex + 1,
                Program.HebrewCalendar));
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            this.GoToSelectedDate();
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
            this.GoToSelectedDate();
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
            this.GoToSelectedDate();
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            this.rbDay.Checked = !this.rbNight.Checked;
        }


        private void FillProblemOnahs()
        {
            if (this._loading)
            {
                return;
            }

            foreach (Control c in this.tableLayoutPanel1.Controls)
            {
                c.Dispose();
            }
            this.tableLayoutPanel1.Controls.Clear();
            if (Program.EntryList.Count > 0)
            {
                Entry entry = Program.EntryList.FirstOrDefault(en => !en.IsInvisible && en.DateTime.IsSameday(this._displayingSecularDate));
                if (entry != null)
                {
                    this.tableLayoutPanel1.Controls.Add(new Label()
                    {
                        Text = "ראיה בעונת " + entry.HebrewDayNight + " - הפלגה [" + entry.Interval.ToString() + "]",
                        ForeColor = Color.Red,
                        BackColor = Color.Transparent,
                        Font = new Font(this.tableLayoutPanel1.Font, FontStyle.Bold),
                        AutoSize = true,
                        Padding = new Padding(5),
                        Margin = new Padding(5),
                        RightToLeft = RightToLeft.Yes
                    });
                }
            }
            if (Program.ProblemOnahs != null)
            {
                foreach (Onah o in Program.ProblemOnahs.Where(o => o.DateTime.IsSameday(this._displayingSecularDate) && !o.IsIgnored))
                {
                    this.tableLayoutPanel1.Controls.Add(new Label()
                    {
                        Text = "♦ " + o.HebrewDayNight + ": " + o.Name,
                        RightToLeft = RightToLeft.Yes,
                        AutoSize = true,
                        BackColor = Color.Transparent,
                        Font = this.tableLayoutPanel1.Font,
                        ForeColor = this.tableLayoutPanel1.ForeColor
                    });
                }
            }
            if (this.tableLayoutPanel1.Controls.Count == 0)
            {
                this.tableLayoutPanel1.Controls.Add(new Label()
                {
                    Text = "אין חששות ביום זה.",
                    RightToLeft = RightToLeft.Yes,
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = this.tableLayoutPanel1.Font,
                    ForeColor = Color.DarkSlateBlue
                });
            }
        }

        private void SetDayNightRadio()
        {
            bool isNight = this._dailyZmanim.ShkiaAtElevation <= DateTime.Now.TimeOfDay;
            this.rbDay.Checked = !isNight;
            this.rbNight.Checked = isNight;
        }

        private void InitDropdowns()
        {
            for (int i = 5750; i < 6000; i++)
            {
                this.cmbYear.Items.Add(i);
            }

            this.cmbYear.SelectedItem = Program.HebrewCalendar.GetYear(this._displayingSecularDate);

            this.FillMonths();
            this.FillDays();
        }

        private void FillMonths()
        {
            string currentSelection = null;
            if (this.cmbMonth.Items.Count != 0)
            {
                currentSelection = this.cmbMonth.Text;
                this.cmbMonth.Items.Clear();
            }
            int year = (int)this.cmbYear.SelectedItem;
            for (int i = 0; i < Program.HebrewCalendar.GetMonthsInYear(year); i++)
            {
                var month = new MonthObject(year, i + 1);
                if (currentSelection == null && month.Year == Program.HebrewCalendar.GetYear(this._displayingSecularDate) && month.MonthInYear == Program.HebrewCalendar.GetMonth(this._displayingSecularDate))
                {
                    currentSelection = month.MonthName;
                }
                this.cmbMonth.Items.Add(month);
                if (month.MonthName == currentSelection)
                {
                    this.cmbMonth.SelectedItem = month;
                }
            }
        }

        private void FillDays()
        {
            var curMonth = (MonthObject)this.cmbMonth.SelectedItem;
            KeyValuePair<int, string> curDay;

            if (this.cmbDay.Items.Count == 0)
            {
                curDay = new KeyValuePair<int, string>(Program.HebrewCalendar.GetDayOfMonth(this._displayingSecularDate),
                    GeneralUtils.DaysOfMonthHebrew[Program.HebrewCalendar.GetDayOfMonth(this._displayingSecularDate)]);
            }
            else
            {
                curDay = (KeyValuePair<int, string>)this.cmbDay.SelectedItem;
            }

            this.cmbDay.Items.Clear();
            for (int i = 1; i < curMonth.DaysInMonth + 1; i++)
            {
                var day = new KeyValuePair<int, string>(i, GeneralUtils.DaysOfMonthHebrew[i]);
                this.cmbDay.Items.Add(day);
            }

            //If the current month does not have as many days as the last one and the 30th day was selected, we go to day 29.
            if (curDay.Key > curMonth.DaysInMonth)
            {
                this.cmbDay.SelectedIndex = this.cmbDay.Items.Count - 1;
            }
            else
            {
                this.cmbDay.SelectedItem = curDay;
            }
        }


        private void btnToday_Click(object sender, EventArgs e)
        {
            this._loading = true;
            this._displayingSecularDate = DateTime.Now;
            this._displayingJewishDate = new JewishDate(this._displayingSecularDate);
            this.SetSecularDate();
            this._dailyZmanim = new DailyZmanim(this._secularDateAtMidnight, Program.CurrentLocation);
            if (this._dailyZmanim.ShkiaAtElevation <= DateTime.Now.TimeOfDay)
            {
                this._displayingSecularDate = this._displayingSecularDate.AddDays(1);
                this._displayingJewishDate = new JewishDate(this._displayingSecularDate);
                this.SetSecularDate();
                this._dailyZmanim = new DailyZmanim(this._secularDateAtMidnight, Program.CurrentLocation);
            }

            this.ShowCurrentDateZmanimData();
            this.SetDayNightRadio();
            this.FillProblemOnahs();
            this._loading = false;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Program.MainForm.AddNewEntry(new Entry()
            {
                Day = this.cmbDay.SelectedIndex + 1,
                Month = (MonthObject)this.cmbMonth.SelectedItem,
                Year = (int)this.cmbYear.SelectedItem,
                DayNight = this.rbNight.Checked ? DayNight.Night : DayNight.Day,
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
        private void ShowDateData()
        {
            this.Cursor = Cursors.WaitCursor;
            bool showSeconds = true;
            Daf dy = DafYomi.GetDafYomi(this._displayingJewishDate);
            TimeOfDay[] netzshkia = this._dailyZmanim.NetzShkiaAtElevation;
            TimeOfDay[] netzshkiaMishor = this._dailyZmanim.NetzShkiaMishor;
            TimeOfDay netz = this._dailyZmanim.NetzAtElevation;
            TimeOfDay shkia = this._dailyZmanim.ShkiaAtElevation;
            TimeOfDay netzMishor = this._dailyZmanim.NetzMishor;
            TimeOfDay shkiaMishor = this._dailyZmanim.ShkiaMishor;
            TimeOfDay chatzos = this._dailyZmanim.Chatzos;
            double shaaZmanis = this._dailyZmanim.ShaaZmanis;
            double shaaZmanis90 = this._dailyZmanim.ShaaZmanisMga;
            var html = new StringBuilder();

            html.AppendFormat("<div class=\"padWidth royalBlue bold\">{0}</div>",
                this._displayingJewishDate.ToLongDateStringHeb());
            html.AppendFormat("<div class=\"padWidth lightSteelBlue\">{0}</div>",
                this._displayingSecularDate.ToString("D", JewishCalendar.Utils.HebrewCultureInfo));

            //If the secular day is a day behind as day being displayed is todays date and it is after sunset,
            //the user may get confused as the secular date for today and tomorrow will be the same.
            //So we esplain'in it to them...
            if (this._secularDateAtMidnight.Date != this._displayingSecularDate.Date)
            {
                html.Append("<div class=\"padWidth rosyBrown seven italic\">שים לב: תאריך הלועזי מתחיל בשעה 0:00</div>");
            }

            this.DisplayDateDiff(html);

            html.Append("<br />");
            if (this._holidays.Count() > 0)
            {
                foreach (SpecialDay h in this._holidays)
                {
                    html.AppendFormat("<div class=\"padWidth\">{0}", h.NameHebrew);
                    if (h.NameEnglish == "Shabbos Mevarchim")
                    {
                        JewishDate nextMonth = this._displayingJewishDate + 12;
                        html.AppendFormat(" - חודש {0}", JewishCalendar.Utils.GetProperMonthNameHeb(nextMonth.Year, nextMonth.Month));

                        var molad = Molad.GetMolad(nextMonth.Month, nextMonth.Year);
                        int dim = JewishDateCalculations.DaysInJewishMonth(this._displayingJewishDate.Year, this._displayingJewishDate.Month);
                        int dow = dim - this._displayingJewishDate.Day;
                        if (dim == 30)
                        {
                            dow--;
                        }
                        html.AppendFormat("<div>המולד: {0}</div>", molad.ToStringHeb(this._dailyZmanim.ShkiaAtElevation));
                        html.AppendFormat("<div>ראש חודש: {0}{1}</div>",
                            JewishCalendar.Utils.JewishDOWNames[dow], (dim == 30 ? ", " + JewishCalendar.Utils.JewishDOWNames[(dow + 1) % 7] : ""));
                    }
                    html.Append("</div>");
                    if (h.NameEnglish.Contains("Sefiras Ha'omer"))
                    {
                        html.AppendFormat("<div class=\"nine bluoid\">{0}</div>",
                            JewishCalendar.Utils.GetOmerNusach(this._displayingJewishDate.GetDayOfOmer(), Properties.Settings.Default.Nusach));
                    }

                    if (h.DayType.IsSpecialDayType(SpecialDayTypes.EruvTavshilin))
                    {
                        html.Append("<div class=\"padWidth crimson bold\">עירוב תבשילין</div>");
                    }
                }
            }

            html.Append("<table>");

            if (shkia != TimeOfDay.NoValue &&
                    this._holidays.Any(h => h.DayType.IsSpecialDayType(SpecialDayTypes.HasCandleLighting)))
            {
                this.AddLine(html, "הדלקת נרות", (shkia - this._dailyZmanim.Location.CandleLighting).ToString24H(showSeconds),
                    wideDescription: false);
                html.Append("<tr><td class=\"nobg\" colspan=\"3\">&nbsp;</td></tr>");
            }

            this.AddLine(html, "פרשת השבוע",
                string.Join(" ", Sedra.GetSedra(this._displayingJewishDate, this._dailyZmanim.Location.IsInIsrael).Select(i => i.nameHebrew)),
                wideDescription: false);
            if (dy != null)
            {
                this.AddLine(html, "דף יומי", dy.ToStringHeb(), wideDescription: false);
            }

            html.Append("</table><br />");
            html.AppendFormat("<div class=\"padBoth lightSteelBlueBG ghostWhite nine bold clear\">זמני היום ב{0}</div>",
                this._dailyZmanim.Location.NameHebrew);
            html.Append("<table>");

            if (netz == TimeOfDay.NoValue)
            {
                this.AddLine(html, "הנץ החמה", "השמש אינו עולה", bold: true, emphasizeValue: true);
            }
            else
            {
                if (this._displayingJewishDate.Month == 1 && this._displayingJewishDate.Day == 14)
                {
                    this.AddLine(html, "סו\"ז אכילת חמץ", ((netz - 90) + (int)Math.Floor(shaaZmanis90 * 4D)).ToString24H(showSeconds),
                        bold: true);
                    this.AddLine(html, "סו\"ז שריפת חמץ", ((netz - 90) + (int)Math.Floor(shaaZmanis90 * 5D)).ToString24H(showSeconds),
                        bold: true);
                    html.Append("<br />");
                }

                this.AddLine(html, "עלות השחר - 90", (netzMishor - 90).ToString24H(showSeconds));
                this.AddLine(html, "עלות השחר - 72", (netzMishor - 72).ToString24H(showSeconds));

                if (netz == netzMishor)
                {
                    this.AddLine(html, "הנץ החמה", netz.ToString24H(showSeconds), bold: true, emphasizeValue: true);
                }
                else
                {
                    this.AddLine(html, "הנה\"ח <span class=\"reg lightSteelBlue\">...מ " + this._dailyZmanim.Location.Elevation.ToString() + " מטר</span>",
                        netz.ToString24H(showSeconds));
                    this.AddLine(html, "הנה\"ח <span class=\"reg lightSteelBlue\">...גובה פני הים</span>",
                        netzMishor.ToString24H(showSeconds), bold: true, emphasizeValue: true);
                }
                this.AddLine(html, "סוזק\"ש - מג\"א", this._dailyZmanim.GetZman(ZmanType.KShmMga).ToString24H(showSeconds));
                this.AddLine(html, "סוזק\"ש - הגר\"א", this._dailyZmanim.GetZman(ZmanType.KshmGra).ToString24H(showSeconds));
                this.AddLine(html, "סוז\"ת - מג\"א", this._dailyZmanim.GetZman(ZmanType.TflMga).ToString24H(showSeconds));
                this.AddLine(html, "סוז\"ת - הגר\"א", this._dailyZmanim.GetZman(ZmanType.TflGra).ToString24H(showSeconds));
            }
            if (netz != TimeOfDay.NoValue && shkia != TimeOfDay.NoValue)
            {
                this.AddLine(html, "חצות היום והלילה", chatzos.ToString24H(showSeconds));
                this.AddLine(html, "מנחה גדולה", this._dailyZmanim.GetZman(ZmanType.MinchaG).ToString24H(showSeconds));
                this.AddLine(html, "מנחה קטנה", this._dailyZmanim.GetZman(ZmanType.MinchaK).ToString24H(showSeconds));
                this.AddLine(html, "פלג המנחה", this._dailyZmanim.GetZman(ZmanType.MinchaPlg).ToString24H(showSeconds));
            }
            if (shkia == TimeOfDay.NoValue)
            {
                this.AddLine(html, "שקיעת החמה", "השמש אינו שוקע", bold: true, emphasizeValue: true);
            }
            else
            {
                if (shkia == shkiaMishor)
                {
                    this.AddLine(html, "שקיעת החמה", shkia.ToString24H(showSeconds), bold: true, emphasizeValue: true);
                }
                else
                {
                    this.AddLine(html, "שקה\"ח <span class=\"reg lightSteelBlue\">...גובה פני הים</span>", shkiaMishor.ToString24H(showSeconds));
                    this.AddLine(html, "שקה\"ח <span class=\"reg lightSteelBlue\">...מ " + this._dailyZmanim.Location.Elevation.ToString() + " מטר</span>",
                        shkia.ToString24H(showSeconds), bold: true, emphasizeValue: true);
                }

                this.AddLine(html, "צאת הכוכבים 45", (shkia + 45).ToString24H(showSeconds));
                this.AddLine(html, "רבינו תם", (shkia + 72).ToString24H(showSeconds));
                this.AddLine(html, "72 דקות זמניות", (shkia + (int)(shaaZmanis * 1.2)).ToString24H(showSeconds));
                this.AddLine(html, "72 דקות זמניות לחומרה", (shkia + (int)(shaaZmanis90 * 1.2)).ToString24H(showSeconds));
            }
            html.Append("</table>");
            this.webBrowser1.DocumentText = Properties.Resources.InfoHTMLHeb
                .Replace("{{BODY}}", html.ToString());
            this.Cursor = Cursors.Default;
        }


        #region private functions
        private void AddLine(StringBuilder sb, string header, string value, bool wideDescription = true, bool bold = false, bool emphasizeValue = false)
        {
            sb.Append("<tr>");
            sb.AppendFormat("<td class=\"{0}{1}\"><span>{2}</span></td><td>&nbsp;</td>",
                (wideDescription ? "wide" : "medium"),
                (bold ? " bold" : ""), header);
            sb.AppendFormat("<td class=\"{0} {1} bold nobg\">{2}</td>",
                (wideDescription ? "narrow" : "medium"),
                (emphasizeValue ? "crimson" : "cornFlowerBlue"),
                value);
            sb.Append("</tr>");
        }

        private void DisplayDateDiff(StringBuilder html)
        {
            var now = new JewishDate(this._dailyZmanim.Location);
            int diffDays = this._displayingJewishDate.AbsoluteDate - now.AbsoluteDate;

            html.Append("<div class=\"padWidth\">");

            if (diffDays == 0)
            {
                html.Append("היום");
            }
            else if (diffDays == 1)
            {
                html.Append("מחר");
            }
            else if (diffDays == 2)
            {
                html.Append("מחרתיים");
            }
            else if (diffDays == -1)
            {
                html.Append("אתמול");
            }
            else
            {
                int totalDays = Math.Abs(diffDays);

                if (diffDays < 0)
                {
                    html.AppendFormat("לפני {0:N0} ימים", totalDays);
                }
                else
                {
                    html.AppendFormat("בעוד {0:N0} ימים", totalDays);
                }

                if (totalDays > 29)
                {
                    var dateDiff = new Itenso.TimePeriod.DateDiff(
                        this._displayingSecularDate,
                        now.GregorianDate,
                        _gregCal, DayOfWeek.Sunday, Itenso.TimePeriod.YearMonth.January);
                    int years = Math.Abs(dateDiff.ElapsedYears),
                        months = Math.Abs(dateDiff.ElapsedMonths);

                    if (years + months > 0)
                    {
                        int singleDays = Math.Abs(dateDiff.ElapsedDays);

                        html.Append("&nbsp;&nbsp;<span class=\"purpleoid seven italic\">");

                        if (years >= 1)
                        {
                            html.AppendFormat("{0:N0} {1}", years, years >= 2 ? "שנים" : "שנה");
                        }
                        if (months >= 1)
                        {
                            html.AppendFormat(" {0:N0} {1}", months, (months >= 2 ? "חודשים" : "חודש"));
                        }
                        if (singleDays >= 1)
                        {
                            html.AppendFormat(" {0:N0} {1}", singleDays, (singleDays >= 2 ? "ימים" : "יום"));
                        }

                        html.Append("</span>");
                    }
                }
            }

            html.Append("</div>");
        }

        private void SetSecularDate()
        {
            this._displayingSecularDate = this._displayingJewishDate.GregorianDate;
            /*-------------------------------------------------------------------------------------------------------------------------------
             * The zmanim shown will always be for the Gregorian Date that starts at midnight of the current Jewish Date.
             * We use the JewishDateCalculations.GetGregorianDateFromJewishDate function 
             * which gets the Gregorian Date that will be at midnight of the given Jewish day.  
            ----------------------------------------------------------------------------------------------------------------------------------*/
            this._secularDateAtMidnight = JewishDateCalculations.GetGregorianDateFromJewishDate(this._displayingJewishDate);
        }
        #endregion private functions
    }
}
