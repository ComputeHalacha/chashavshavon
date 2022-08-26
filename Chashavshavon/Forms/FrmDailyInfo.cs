using JewishCalendar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tahara;

namespace Chashavshavon
{
    public partial class FrmDailyInfo : Form
    {
        //The combos are changed dynamically and we don't want to fire the change event during initial loading.
        private bool _loading = true;
        private DateTime _displayingSecularDate;
        private JewishDate _displayingJewishDate;
        private DateTime _secularDateAtMidnight;
        private IEnumerable<SpecialDay> _holidays;
        private DailyZmanim _dailyZmanim;


        public FrmDailyInfo(DateTime date)
        {
            InitializeComponent();
            _displayingSecularDate = date;
            InitDropdowns();
            GoToDate(date);
            SetDayNightRadio();
            InitSwitchers();
        }

        private void GoToDate(DateTime date)
        {
            _displayingSecularDate = date;
            _displayingJewishDate = new JewishDate(date);
            SetSecularDate();
            _dailyZmanim = new DailyZmanim(_secularDateAtMidnight, Program.CurrentLocation);
            ShowCurrentDateZmanimData();
        }

        private void ShowCurrentDateZmanimData()
        {
            _holidays = Zmanim.GetHolidays(_displayingJewishDate, Program.CurrentLocation.IsInIsrael).Cast<SpecialDay>();
            cmbYear.SelectedItem = Program.HebrewCalendar.GetYear(_displayingSecularDate);
            cmbMonth.SelectedIndex = Program.HebrewCalendar.GetMonth(_displayingSecularDate) - 1;
            cmbDay.SelectedIndex = Program.HebrewCalendar.GetDayOfMonth(_displayingSecularDate) - 1;

            _loading = false;
            FillProblemOnahs();
            ShowDateData();

            string jdateText = _displayingJewishDate.ToLongDateStringHeb();
            Text = jdateText;
            lblCaption.Text = Text;
        }

        private void GoToSelectedDate()
        {
            GoToDate(new DateTime((int)cmbYear.SelectedItem,
                ((MonthObject)cmbMonth.SelectedItem).MonthInYear,
                cmbDay.SelectedIndex + 1,
                Program.HebrewCalendar));
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }
            GoToSelectedDate();
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }
            _loading = true;
            FillDays();
            _loading = false;
            GoToSelectedDate();
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading)
            {
                return;
            }
            _loading = true;
            FillMonths();
            FillDays();
            _loading = false;
            GoToSelectedDate();
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            rbDay.Checked = !rbNight.Checked;
        }


        private void FillProblemOnahs()
        {
            if (_loading)
            {
                return;
            }

            foreach (Control c in tableLayoutPanel1.Controls)
            {
                c.Dispose();
            }
            tableLayoutPanel1.Controls.Clear();

            if (Program.EntryList.Count > 0)
            {
                Entry entry = Program.EntryList.FirstOrDefault(en => !en.IsInvisible && en.DateTime.IsSameday(_displayingSecularDate));
                if (entry != null)
                {
                    tableLayoutPanel1.Controls.Add(new Label()
                    {
                        Text = "ראיה בעונת " + entry.HebrewDayNight + " - הפלגה [" + entry.Interval.ToString() + "]",
                        ForeColor = Color.Red,
                        BackColor = Color.Transparent,
                        Font = new Font(tableLayoutPanel1.Font, FontStyle.Bold),
                        AutoSize = true,
                        Padding = new Padding(5),
                        Margin = new Padding(5),
                        RightToLeft = RightToLeft.Yes
                    });
                }
            }
            if (Program.ProblemOnahs != null)
            {
                foreach (Onah o in Program.ProblemOnahs.Where(o => o.DateTime.IsSameday(_displayingSecularDate) && !o.IsIgnored))
                {
                    tableLayoutPanel1.Controls.Add(new Label()
                    {
                        Text = "♦ " + o.HebrewDayNight + ": " + o.Name,
                        RightToLeft = RightToLeft.Yes,
                        AutoSize = true,
                        BackColor = Color.Transparent,
                        Font = tableLayoutPanel1.Font,
                        ForeColor = tableLayoutPanel1.ForeColor
                    });
                }
            }
            if (tableLayoutPanel1.Controls.Count == 0)
            {
                tableLayoutPanel1.Controls.Add(new Label()
                {
                    Text = "אין חששות ביום זה.",
                    RightToLeft = RightToLeft.Yes,
                    AutoSize = true,
                    BackColor = Color.Transparent,
                    Font = tableLayoutPanel1.Font,
                    ForeColor = Color.DarkSlateBlue
                });
            }
        }

        private void SetDayNightRadio()
        {
            bool isNight = _dailyZmanim.ShkiaAtElevation <= DateTime.Now.TimeOfDay;
            rbDay.Checked = !isNight;
            rbNight.Checked = isNight;
        }

        private void InitSwitchers()
        {
            choiceSwitcherHefsek.ChoiceOneSelected = Program.TaharaEventList.Any(te =>
               te.DateTime.IsSameday(_displayingSecularDate) &&
               te.TaharaEventType == TaharaEventType.Hefsek);
            choiceSwitcherMikvah.ChoiceOneSelected = Program.TaharaEventList.Any(te =>
               te.DateTime.IsSameday(_displayingSecularDate) &&
               te.TaharaEventType == TaharaEventType.Mikvah);
            choiceSwitcherShailah.ChoiceOneSelected = Program.TaharaEventList.Any(te =>
               te.DateTime.IsSameday(_displayingSecularDate) &&
               te.TaharaEventType == TaharaEventType.Shailah);
        }

        private void InitDropdowns()
        {
            for (int i = 5750; i < 6000; i++)
            {
                cmbYear.Items.Add(i);
            }

            cmbYear.SelectedItem = Program.HebrewCalendar.GetYear(_displayingSecularDate);

            FillMonths();
            FillDays();
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
                if (currentSelection == null && month.Year == Program.HebrewCalendar.GetYear(_displayingSecularDate) && month.MonthInYear == Program.HebrewCalendar.GetMonth(_displayingSecularDate))
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
                curDay = new KeyValuePair<int, string>(Program.HebrewCalendar.GetDayOfMonth(_displayingSecularDate),
                    GeneralUtils.DaysOfMonthHebrew[Program.HebrewCalendar.GetDayOfMonth(_displayingSecularDate)]);
            }
            else
            {
                curDay = (KeyValuePair<int, string>)cmbDay.SelectedItem;
            }

            cmbDay.Items.Clear();
            for (int i = 1; i < curMonth.DaysInMonth + 1; i++)
            {
                KeyValuePair<int, string> day = new KeyValuePair<int, string>(i, GeneralUtils.DaysOfMonthHebrew[i]);
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

        private void choiceSwitcherHefsek_ChoiceSwitched(object sender, EventArgs e)
        {
            TaharaEvent hefesk = Program.TaharaEventList.FirstOrDefault(te =>
                te.DateTime.IsSameday(_displayingSecularDate) &&
                te.TaharaEventType == TaharaEventType.Hefsek);
            if (choiceSwitcherHefsek.ChoiceOneSelected && hefesk == null)
            {
                Program.MainForm.AddTaharaEvent(new TaharaEvent(TaharaEventType.Hefsek) { DateTime = _displayingSecularDate });
            }
            else if (!choiceSwitcherHefsek.ChoiceOneSelected && hefesk != null)
            {
                Program.MainForm.RemoveTaharaEvent(hefesk);
            }
        }

        private void choiceSwitcherMikvah_ChoiceSwitched(object sender, EventArgs e)
        {
            TaharaEvent mikvah = Program.TaharaEventList.FirstOrDefault(te =>
                te.DateTime.IsSameday(_displayingSecularDate) &&
                te.TaharaEventType == TaharaEventType.Mikvah);
            if (choiceSwitcherMikvah.ChoiceOneSelected && mikvah == null)
            {
                Program.MainForm.AddTaharaEvent(new TaharaEvent(TaharaEventType.Mikvah) { DateTime = _displayingSecularDate });
            }
            else if (!choiceSwitcherMikvah.ChoiceOneSelected && mikvah != null)
            {
                Program.MainForm.RemoveTaharaEvent(mikvah);
            }
        }

        private void choiceSwitcherShailah_ChoiceSwitched(object sender, EventArgs e)
        {
            TaharaEvent shailah = Program.TaharaEventList.FirstOrDefault(te =>
                te.DateTime.IsSameday(_displayingSecularDate) &&
                te.TaharaEventType == TaharaEventType.Shailah);
            if (choiceSwitcherShailah.ChoiceOneSelected && shailah == null)
            {
                Program.MainForm.AddTaharaEvent(new TaharaEvent(TaharaEventType.Shailah) { DateTime = _displayingSecularDate });
            }
            else if (!choiceSwitcherShailah.ChoiceOneSelected && shailah != null)
            {
                Program.MainForm.RemoveTaharaEvent(shailah);
            }
        }


        private void btnToday_Click(object sender, EventArgs e)
        {
            _loading = true;
            _displayingSecularDate = DateTime.Now;
            _displayingJewishDate = new JewishDate(_displayingSecularDate);
            SetSecularDate();
            _dailyZmanim = new DailyZmanim(_secularDateAtMidnight, Program.CurrentLocation);
            if (_dailyZmanim.ShkiaAtElevation <= DateTime.Now.TimeOfDay)
            {
                _displayingSecularDate = _displayingSecularDate.AddDays(1);
                _displayingJewishDate = new JewishDate(_displayingSecularDate);
                SetSecularDate();
                _dailyZmanim = new DailyZmanim(_secularDateAtMidnight, Program.CurrentLocation);
            }

            ShowCurrentDateZmanimData();
            SetDayNightRadio();
            FillProblemOnahs();
            _loading = false;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            Program.MainForm.AddNewEntry(new Entry()
            {
                Day = cmbDay.SelectedIndex + 1,
                Month = (MonthObject)cmbMonth.SelectedItem,
                Year = (int)cmbYear.SelectedItem,
                DayNight = rbNight.Checked ? DayNight.Night : DayNight.Day,
                Notes = txtNotes.Text
            }, this);
            DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cmbYear_Format(object sender, ListControlConvertEventArgs e)
        {
            e.Value = GeneralUtils.ToJNum((int)e.Value % 1000);
        }
        private void ShowDateData()
        {
            Cursor = Cursors.WaitCursor;
            bool showSeconds = true;
            Daf dy = DafYomi.GetDafYomi(_displayingJewishDate);
            TimeOfDay[] netzshkia = _dailyZmanim.NetzShkiaAtElevation;
            TimeOfDay[] netzshkiaMishor = _dailyZmanim.NetzShkiaMishor;
            TimeOfDay netz = _dailyZmanim.NetzAtElevation;
            TimeOfDay shkia = _dailyZmanim.ShkiaAtElevation;
            TimeOfDay netzMishor = _dailyZmanim.NetzMishor;
            TimeOfDay shkiaMishor = _dailyZmanim.ShkiaMishor;
            TimeOfDay chatzos = _dailyZmanim.Chatzos;
            double shaaZmanis = _dailyZmanim.ShaaZmanis;
            double shaaZmanis90 = _dailyZmanim.ShaaZmanisMga;
            StringBuilder html = new StringBuilder();

            html.AppendFormat("<div class=\"padWidth royalBlue bold\">{0}",
                _displayingJewishDate.ToLongDateStringHeb());
            html.AppendFormat("<span class=\"sdate\">{0}</span></div>",
                _displayingSecularDate.ToString("d", GeneralUtils.SecularDateCultureInfo));

            //If the secular day is a day behind as day being displayed is todays date and it is after sunset,
            //the user may get confused as the secular date for today and tomorrow will be the same.
            //So we esplain'in it to them...
            if (_secularDateAtMidnight.Date != _displayingSecularDate.Date)
            {
                html.Append("<div class=\"padWidth rosyBrown seven italic\">שים לב: תאריך הלועזי מתחיל בשעה 0:00</div>");
            }

            DisplayDateDiff(html);

            html.Append("<br />");
            if (_holidays.Count() > 0)
            {
                foreach (SpecialDay h in _holidays)
                {
                    html.AppendFormat("<div class=\"padWidth\">{0}", h.NameHebrew);
                    if (h.NameEnglish == "Shabbos Mevarchim")
                    {
                        JewishDate nextMonth = _displayingJewishDate + 12;
                        html.AppendFormat(" - חודש {0}", JewishCalendar.Utils.GetProperMonthNameHeb(nextMonth.Year, nextMonth.Month));

                        Molad molad = Molad.GetMolad(nextMonth.Month, nextMonth.Year);
                        int dim = JewishDateCalculations.DaysInJewishMonth(_displayingJewishDate.Year, _displayingJewishDate.Month);
                        int dow = dim - _displayingJewishDate.Day;
                        if (dim == 30)
                        {
                            dow--;
                        }
                        html.AppendFormat("<div>המולד: {0}</div>", molad.ToStringHeb(_dailyZmanim.ShkiaAtElevation));
                        html.AppendFormat("<div>ראש חודש: {0}{1}</div>",
                            JewishCalendar.Utils.JewishDOWNames[dow], (dim == 30 ? ", " + JewishCalendar.Utils.JewishDOWNames[(dow + 1) % 7] : ""));
                    }
                    html.Append("</div>");
                    if (h.NameEnglish.Contains("Sefiras Ha'omer"))
                    {
                        html.AppendFormat("<div class=\"nine bluoid\">{0}</div>",
                            JewishCalendar.Utils.GetOmerNusach(_displayingJewishDate.GetDayOfOmer(), Properties.Settings.Default.Nusach));
                    }

                    if (h.DayType.IsSpecialDayType(SpecialDayTypes.EruvTavshilin))
                    {
                        html.Append("<div class=\"padWidth crimson bold\">עירוב תבשילין</div>");
                    }
                }
            }

            html.Append("<table>");

            if (shkia != TimeOfDay.NoValue &&
                    _holidays.Any(h => h.DayType.IsSpecialDayType(SpecialDayTypes.HasCandleLighting)))
            {
                AddLine(html, "הדלקת נרות", (shkia - _dailyZmanim.Location.CandleLighting).ToString24H(showSeconds),
                    wideDescription: false);
                html.Append("<tr><td class=\"nobg\" colspan=\"3\">&nbsp;</td></tr>");
            }

            AddLine(html, "פרשת השבוע",
                string.Join(" ", Sedra.GetSedra(_displayingJewishDate, _dailyZmanim.Location.IsInIsrael).Select(i => i.nameHebrew)),
                wideDescription: false);
            if (dy != null)
            {
                AddLine(html, "דף יומי", dy.ToStringHeb(), wideDescription: false);
            }

            html.Append("</table><br />");
            html.AppendFormat("<div class=\"padBoth lightSteelBlueBG ghostWhite nine bold clear\">זמני היום ב{0}</div>",
                _dailyZmanim.Location.NameHebrew);
            html.Append("<table>");

            if (netz == TimeOfDay.NoValue)
            {
                AddLine(html, "הנץ החמה", "השמש אינו עולה", bold: true, emphasizeValue: true);
            }
            else
            {
                if (_displayingJewishDate.Month == 1 && _displayingJewishDate.Day == 14)
                {
                    AddLine(html, "סו\"ז אכילת חמץ", ((netz - 90) + (int)Math.Floor(shaaZmanis90 * 4D)).ToString24H(showSeconds),
                        bold: true);
                    AddLine(html, "סו\"ז שריפת חמץ", ((netz - 90) + (int)Math.Floor(shaaZmanis90 * 5D)).ToString24H(showSeconds),
                        bold: true);
                    html.Append("<br />");
                }

                AddLine(html, "עלות השחר - 90", (netzMishor - 90).ToString24H(showSeconds));
                AddLine(html, "עלות השחר - 72", (netzMishor - 72).ToString24H(showSeconds));

                if (netz == netzMishor)
                {
                    AddLine(html, "הנץ החמה", netz.ToString24H(showSeconds), bold: true, emphasizeValue: true);
                }
                else
                {
                    AddLine(html, "הנה\"ח <span class=\"reg lightSteelBlue\">...מ " + _dailyZmanim.Location.Elevation.ToString() + " מטר</span>",
                        netz.ToString24H(showSeconds));
                    AddLine(html, "הנה\"ח <span class=\"reg lightSteelBlue\">...גובה פני הים</span>",
                        netzMishor.ToString24H(showSeconds), bold: true, emphasizeValue: true);
                }
                AddLine(html, "סוזק\"ש - מג\"א", _dailyZmanim.GetZman(ZmanType.KShmMga).ToString24H(showSeconds));
                AddLine(html, "סוזק\"ש - הגר\"א", _dailyZmanim.GetZman(ZmanType.KshmGra).ToString24H(showSeconds));
                AddLine(html, "סוז\"ת - מג\"א", _dailyZmanim.GetZman(ZmanType.TflMga).ToString24H(showSeconds));
                AddLine(html, "סוז\"ת - הגר\"א", _dailyZmanim.GetZman(ZmanType.TflGra).ToString24H(showSeconds));
            }
            if (netz != TimeOfDay.NoValue && shkia != TimeOfDay.NoValue)
            {
                AddLine(html, "חצות היום והלילה", chatzos.ToString24H(showSeconds));
                AddLine(html, "מנחה גדולה", _dailyZmanim.GetZman(ZmanType.MinchaG).ToString24H(showSeconds));
                AddLine(html, "מנחה קטנה", _dailyZmanim.GetZman(ZmanType.MinchaK).ToString24H(showSeconds));
                AddLine(html, "פלג המנחה", _dailyZmanim.GetZman(ZmanType.MinchaPlg).ToString24H(showSeconds));
            }
            if (shkia == TimeOfDay.NoValue)
            {
                AddLine(html, "שקיעת החמה", "השמש אינו שוקע", bold: true, emphasizeValue: true);
            }
            else
            {
                if (shkia == shkiaMishor)
                {
                    AddLine(html, "שקיעת החמה", shkia.ToString24H(showSeconds), bold: true, emphasizeValue: true);
                }
                else
                {
                    AddLine(html, "שקה\"ח <span class=\"reg lightSteelBlue\">...גובה פני הים</span>", shkiaMishor.ToString24H(showSeconds));
                    AddLine(html, "שקה\"ח <span class=\"reg lightSteelBlue\">...מ " + _dailyZmanim.Location.Elevation.ToString() + " מטר</span>",
                        shkia.ToString24H(showSeconds), bold: true, emphasizeValue: true);
                }

                AddLine(html, "צאת הכוכבים 45", (shkia + 45).ToString24H(showSeconds));
                AddLine(html, "רבינו תם", (shkia + 72).ToString24H(showSeconds));
                AddLine(html, "72 דקות זמניות", (shkia + (int)(shaaZmanis * 1.2)).ToString24H(showSeconds));
                AddLine(html, "72 דקות זמניות לחומרה", (shkia + (int)(shaaZmanis90 * 1.2)).ToString24H(showSeconds));
            }
            html.Append("</table>");
            webBrowser1.DocumentText = Properties.Resources.InfoHTMLHeb
                .Replace("{{BODY}}", html.ToString());
            Cursor = Cursors.Default;
        }


        #region private functions
        private void AddLine(StringBuilder sb, string header, string value, bool wideDescription = true, bool bold = false, bool emphasizeValue = false)
        {
            sb.Append("<tr>");
            sb.AppendFormat("<td class=\"{0}{1}\"><span class=\"header\">{2}</span></td><td>&nbsp;</td>",
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
            JewishDate now = new JewishDate(_dailyZmanim.Location);
            int diffDays = _displayingJewishDate.AbsoluteDate - now.AbsoluteDate;

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
                    Itenso.TimePeriod.DateDiff dateDiff = new Itenso.TimePeriod.DateDiff(
                        _displayingSecularDate,
                        now.GregorianDate,
                        GeneralUtils.SecularDateCultureInfo.DateTimeFormat.Calendar,
                        DayOfWeek.Sunday, Itenso.TimePeriod.YearMonth.January);
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
            _displayingSecularDate = _displayingJewishDate.GregorianDate;
            /*-------------------------------------------------------------------------------------------------------------------------------
             * The zmanim shown will always be for the Gregorian Date that starts at midnight of the current Jewish Date.
             * We use the JewishDateCalculations.GetGregorianDateFromJewishDate function 
             * which gets the Gregorian Date that will be at midnight of the given Jewish day.  
            ----------------------------------------------------------------------------------------------------------------------------------*/
            _secularDateAtMidnight = JewishDateCalculations.GetGregorianDateFromJewishDate(_displayingJewishDate);
        }
        #endregion private functions

        private void btnPrintDailyInfo_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
        }

        private void btnSaveDailyInfo_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sd = new SaveFileDialog { FileName = Text.Replace("'", "").Replace("\"", "") + ".html" })
            {
                if (sd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sd.FileName, webBrowser1.DocumentText, Encoding.UTF8);
                    Program.Inform("\"הקובץ " + sd.FileName + "\", נשמרה בהצלחה");
                }
            }
        }

    }
}
