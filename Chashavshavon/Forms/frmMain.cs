using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Chashavshavon.Utils;
using Microsoft.Win32;

namespace Chashavshavon
{
    public partial class frmMain : Form
    {
        //List of locations
        public static XmlDocument LocationsXmlDoc;

        #region Private Variables
        //The combos are changed dynamically and we don't want to fire the change event during initial loading.
        private bool _loading;        
        #endregion

        #region Constructors
        public frmMain()
        {
            //In case we will display the password entry form, we hide this until form load
            this.Hide();
            string password = this.GetPassword();

            InitializeComponent();
            LocationsXmlDoc = new XmlDocument();


            this.bindingSourceEntries.DataSource = Entry.EntryList;

            //Fill the location list from the xml file
            LocationsXmlDoc.Load(Application.StartupPath + "\\Locations.xml");

            //The following sets all output displays of date time functions to Jewish dates for the current thread
            Program.CultureInfo.DateTimeFormat.Calendar = Program.HebrewCalendar;
            System.Threading.Thread.CurrentThread.CurrentCulture = Program.CultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = Program.CultureInfo;

            Zmanim.SetSummerTime();
            this.SetLocation();
            this.SetDateAndDayNight();
            this.FillZmanData();

            //The timer is for the clock
            this.timer1.Start();

            if (password == null)
            {
                this.Show();
            }
            else
            {
                //Prompt for a password and don't stop prompting until the user gets it right or gives up
                frmEnterPassword f = new frmEnterPassword(password);
                do
                {
                    f.ShowDialog();
                    if (f.DialogResult == DialogResult.No)
                        MessageBox.Show("סיסמה שגויה",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                }
                while (f.DialogResult == DialogResult.No);
                //If the user canceled etc. we will close this in form load
                this.CloseMeFirst = f.DialogResult != DialogResult.Yes;
            }
        }

        public frmMain(string filePath)
            : this()
        {
            this.CurrentFileIsRemote = false;
            this.CurrentFile = filePath;
        }
        #endregion

        #region Event Handlers
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (this.CloseMeFirst)
            {
                this.Close();
            }

            dgEntries.AutoGenerateColumns = false;

            //The combos are changed dynamically and we don't want to fire the change event during initial loading.        
            this._loading = true;
            //Checks to see if the user is connected to the internet, and activates remote functionality if they are.
            this.TestInternet();
            //Load the last opened file. If it does not exist or this is the first run, a blank list is presented            
            this.LoadXmlFile();
            this._loading = false;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveCurrentFile();
            if (!Properties.Settings.Default.OpenLastFile)
            {
                Properties.Settings.Default.CurrentFile = null;
                Properties.Settings.Default.IsCurrentFileRemote = false;
            }
            Properties.Settings.Default.Save();
        }

        private void ShowDayDetails(object sender, EventArgs e)
        {
            switch (((Control)((Control)sender).Parent).Name)
            {
                case "pnlYesterday":
                    this.ShowDayDetails(Program.Today.AddDays(-1));
                    break;
                case "pnlToday":
                    this.ShowDayDetails(Program.Today);
                    break;
                case "pnlTomorrow":
                    this.ShowDayDetails(Program.Today.AddDays(1));
                    break;
                case "pnlDayAfter":
                    this.ShowDayDetails(Program.Today.AddDays(2));
                    break;
            }
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._loading)
            {
                return;
            }
            this.FillDays();
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
            this._loading = false;
            this.FillDays();
            this.FillZmanim();
        }

        private void rbDay_CheckedChanged(object sender, EventArgs e)
        {
            rbDay.Checked = !rbNight.Checked;
        }

        private void rbNight_CheckedChanged(object sender, EventArgs e)
        {
            rbNight.Checked = !rbDay.Checked;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            this.AddNewEntry(new Entry()
            {
                Day = this.cmbDay.SelectedIndex + 1,
                Month = (MonthObject)cmbMonth.SelectedItem,
                Year = (int)cmbYear.SelectedItem,
                DayNight = rbNight.Checked ? DayNight.Night : DayNight.Day,
                Notes = this.txtNotes.Text
            });
        }

        private void dgEntries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.dgEntries.Columns["NotesColumn"].Index)
            {
                DataGridViewRow r = this.dgEntries.Rows[e.RowIndex];
                Entry entry = (Entry)r.DataBoundItem;
                string nkText = "";
                foreach (Kavuah nk in entry.NoKavuahList)
                {
                    nkText += " לא לרשום קבוע " + nk.KavuahDescriptionHebrew;
                }
                if (nkText.Length > 0)
                {
                    e.Value += " [" + nkText.Trim() + "]";
                }
            }
        }

        private void dgEntries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEntries.Columns[e.ColumnIndex] == btnDeleteColumn)
            {
                this.DeleteEntry((Entry)dgEntries.Rows[e.RowIndex].DataBoundItem);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile();
            MessageBox.Show("הקובץ נשמרה" + (CurrentFileIsRemote ? " ברשת " : ""),
                            "חשבשבון",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFile();
        }

        private void AbouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog(this);
        }

        private void btnViewTextCalendar_Click(object sender, EventArgs e)
        {
            this.ShowCalendarTextList();
        }

        private void btnViewTextEntryList_Click(object sender, EventArgs e)
        {
            this.ShowEntryTextList();
        }

        private void btnPrintEntryList_Click(object sender, EventArgs e)
        {
            this.PrintEntryList();
        }

        private void btnPrintCalendar_Click(object sender, EventArgs e)
        {
            this.PrintCalendarList();
        }

        private void calToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLuach f = new frmLuach(Program.Today, this.ProblemOnas);
            f.ShowDialog();
        }

        private void btnOpenLuach2_Click(object sender, EventArgs e)
        {
            frmLuach f = new frmLuach(Program.Today, this.ProblemOnas);
            f.ShowDialog();
        }

        private void btnOpenLuach_Click(object sender, EventArgs e)
        {
            frmLuach f = new frmLuach(Program.Today, this.ProblemOnas);
            f.ShowDialog();
        }

        private void btnOpenKavuahs_Click(object sender, EventArgs e)
        {
            this.ShowKavuahList();
        }

        private void btnCheshbonKavuahs_Click(object sender, EventArgs e)
        {
            if (Kavuah.FindAndPromptKavuahs())
            {
                //Save the new Kavuahs to the file
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("לא נמצאו וסת קבוע אפשריים");
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile();
            openFileDialog1.ShowDialog();
            openFileDialog1.CheckFileExists = true;
            CurrentFile = openFileDialog1.FileName;
            CurrentFileIsRemote = false;
            LoadXmlFile();
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences prefs = new frmPreferences();
            prefs.Top = Convert.ToInt32(this.Top + ((this.Height / 2) - (prefs.Height / 2)));
            prefs.Left = Convert.ToInt32(this.Left + ((this.Width / 2) - (prefs.Width / 2)));
            prefs.Show(this);
        }

        private void openKavuaListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowKavuahList();
        }

        private void AddKavuahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddKavuah f = new frmAddKavuah();
            f.ShowDialog(this);
            if (f.DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                this.SaveCurrentFile();
                this.TestInternet();
                this.LoadXmlFile();
            }
        }

        private void SearchForKavuahsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Kavuah.FindAndPromptKavuahs())
            {
                //Save the new Kavuahs to the file
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("לא נמצאו וסת קבוע אפשריים");
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TestInternet();
            this.LoadXmlFile();
        }

        private void toolStripMenuItemRemote_Click(object sender, EventArgs e)
        {
            frmRemoteFiles f = new frmRemoteFiles();
            f.Show(this);
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintCalendarList();
        }

        private void TextListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCalendarTextList();
        }

        private void toolStripMenuItemPrintEntryList_Click(object sender, EventArgs e)
        {
            this.PrintEntryList();
        }

        private void toolStripMenuItemEntryList_Click(object sender, EventArgs e)
        {
            this.ShowEntryTextList();
        }

        private void pbWeb_Click(object sender, EventArgs e)
        {
            frmRemoteFiles f = new frmRemoteFiles();
            f.Show(this);
        }

        private void cmbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillZmanim();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void SourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
            string fileName;
            if (CurrentFileIsRemote)
            {
                fileName = Path.GetTempFileName();
                File.WriteAllText(fileName, CurrentFileXML);
            }
            else
            {
                fileName = CurrentFile;
            }
            System.Diagnostics.Process.Start("notepad.exe", fileName);
        }
        #endregion

        #region Private Functions
        private void FillZmanData()
        {
            for (int i = 5600; i < 6001; i++)
            {
                cmbYear.Items.Add(i);
            }

            cmbYear.SelectedItem = Program.HebrewCalendar.GetYear(Program.Today);

            this.FillMonths();
            this.FillDays();
            this.FillZmanim();
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
                if (currentSelection == null && month.Year == Program.HebrewCalendar.GetYear(Program.Today) && month.MonthInYear == Program.HebrewCalendar.GetMonth(Program.Today))
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
                curDay = new KeyValuePair<int, string>(Program.HebrewCalendar.GetDayOfMonth(Program.Today), Zmanim.DaysOfMonthHebrew[Program.HebrewCalendar.GetDayOfMonth(Program.Today)]);
            }
            else
            {
                curDay = (KeyValuePair<int, string>)cmbDay.SelectedItem;
            }

            cmbDay.Items.Clear();
            for (int i = 1; i < curMonth.DaysInMonth + 1; i++)
            {
                KeyValuePair<int, string> day = new KeyValuePair<int, string>(i, Zmanim.DaysOfMonthHebrew[i]);
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

        private void FillCalendar()
        {
            DateTime tomorrow = Program.Today.AddDays(1);
            DateTime dayAfterTomorrow = Program.Today.AddDays(2);
            DateTime yesterday = Program.Today.AddDays(-1);

            MonthObject ms = new MonthObject(Program.HebrewCalendar.GetYear(yesterday),
                Program.HebrewCalendar.GetMonth(yesterday));
            this.lblYesterdayDate.Text = Zmanim.DaysOfMonthHebrew[Program.HebrewCalendar.GetDayOfMonth(yesterday)] +
                " " + ms.MonthName;
            this.toolTip1.SetToolTip(this.lblYesterdayDate, this.GetToolTipForDate(yesterday));
            lblYesterdayWeekDay.Text = Zmanim.GetDayOfWeekText(yesterday);

            ms = new MonthObject(Program.HebrewCalendar.GetYear(Program.Today),
                Program.HebrewCalendar.GetMonth(Program.Today));
            this.lblTodayDate.Text = Zmanim.DaysOfMonthHebrew[Program.HebrewCalendar.GetDayOfMonth(Program.Today)] +
                " " + ms.MonthName;
            this.toolTip1.SetToolTip(this.lblTodayDate, this.GetToolTipForDate(Program.Today));
            lblTodayWeekDay.Text = Zmanim.GetDayOfWeekText(Program.Today);

            ms = new MonthObject(Program.HebrewCalendar.GetYear(tomorrow),
                Program.HebrewCalendar.GetMonth(tomorrow));
            this.lblTomorrowDate.Text = Zmanim.DaysOfMonthHebrew[Program.HebrewCalendar.GetDayOfMonth(tomorrow)] +
                " " + ms.MonthName;
            this.toolTip1.SetToolTip(this.lblTomorrowDate, this.GetToolTipForDate(tomorrow));
            lblTomorrowWeekDay.Text = Zmanim.GetDayOfWeekText(tomorrow);

            ms = new MonthObject(Program.HebrewCalendar.GetYear(dayAfterTomorrow),
                Program.HebrewCalendar.GetMonth(dayAfterTomorrow));
            this.lblNextdayDate.Text = Zmanim.DaysOfMonthHebrew[Program.HebrewCalendar.GetDayOfMonth(dayAfterTomorrow)] +
                " " + ms.MonthName;
            this.toolTip1.SetToolTip(this.lblNextdayDate, this.GetToolTipForDate(dayAfterTomorrow));
            lblNextDayWeekDay.Text = Zmanim.GetDayOfWeekText(dayAfterTomorrow);

            lblYesterdayEntryStuff.Text = "";
            lblTodayEntryStuff.Text = "";
            lblTomorrowEntryStuff.Text = "";
            lblNextDayEntryStuff.Text = "";

            foreach (Entry entry in Entry.EntryList)
            {
                if (entry.DateTime.IsSameday(yesterday))
                {
                    lblYesterdayEntryStuff.Text = "עונה: " + entry.HebrewDayNight + "\nהפלגה: " + entry.Interval.ToString();
                }
                if (entry.DateTime.IsSameday(Program.Today))
                {
                    lblTodayEntryStuff.Text = "עונה: " + entry.HebrewDayNight + "\nהפלגה: " + entry.Interval.ToString();
                }
                if (entry.DateTime.IsSameday(tomorrow))
                {
                    lblTomorrowEntryStuff.Text = "עונה: " + entry.HebrewDayNight + "\nהפלגה: " + entry.Interval.ToString();
                }
                if (entry.DateTime.IsSameday(dayAfterTomorrow))
                {
                    lblNextDayEntryStuff.Text = "עונה: " + entry.HebrewDayNight + "\nהפלגה: " + entry.Interval.ToString();
                }
            }
            this.CalculateCalendar();
        }

        private void ShowKavuahList()
        {
            frmKavuahs f = new frmKavuahs();
            f.ShowDialog(this);
            if (f.DialogResult != System.Windows.Forms.DialogResult.Cancel)
            {
                this.SaveCurrentFile();
                this.TestInternet();
                this.LoadXmlFile();
            }
        }

        private void SaveAs()
        {
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "pm";
            openFileDialog1.FileName = Program.Today.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pm";
            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            CurrentFile = openFileDialog1.FileName;
            CurrentFileIsRemote = false;

            SaveCurrentFile();
            return;
        }

        private void CreateNewFile()
        {
            if (!string.IsNullOrEmpty(this.CurrentFile))
            {
                this.SaveCurrentFile();
            }
            saveFileDialog1.Title = "נא לבחור שם ומיקום לקובץ החדש";
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.DefaultExt = "pm";
            saveFileDialog1.FileName = Program.Today.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pm";

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CurrentFile = saveFileDialog1.FileName;
                CurrentFileIsRemote = false;
                LoadXmlFile();
            }
        }

        private void CalculateCalendar()
        {
            this.ClearCalendar();
            if (Entry.EntryList.Count == 0)
            {
                return;
            }
            //Clears the list and gets it ready to accept new problems
            this.ProblemOnas = new List<Onah>();

            //A list of 8 Onahs starting from yesterday until 2 days from now. Will be used to display 
            //in the calendar (right side of form) and text for printing.
            var onahs = GetCalendarOnahs();

            //A list of Onahs that need to be kept. The list is worked out from the list of Entries.
            this.SetProblemOnahs();

            //Get the onahs that need to be kept for Kavuahs of yom hachodesh, sirug, 
            //dilug haflagos (from projected day - not actual entry)
            //and other Kavuahs that are not dependent on the actual entry list
            this.ProblemOnas.AddRange(this.GetIndependentKavuahOnahs(onahs));

            //Goes through the list of problem Onahs and matches it up to the 8 Onahs in the calendar.
            //If any match - meaning that calendar Onah needs to be kept, the appropriate calendar box is filled and colored.
            this.ProcessProblemOnahList(onahs);

            //The lblNextProblem displays the next upcoming Onah that needs to be kept
            this.lblNextProblem.Text = GetNextOnahText();
            this.SetWeekListHtml();
        }

        /// <summary>
        /// Work out the Kavuahs of yom hachodesh, sirug, dilug haflagos (from projected day - not actual entry)
        /// and other Kavuahs that are not dependant on the entry list
        /// </summary>
        /// <param name="onahs"></param>
        /// <returns></returns>
        private List<Onah> GetIndependentKavuahOnahs(List<Onah> onahs)
        {
            var list = new List<Onah>();

            //Kavuahs of Yom Hachodesh and Sirug
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                                        k.Active &&
                                        k.KavuahType.In(KavuahType.DayOfMonth,
                                                        KavuahType.DayOfMonthMaayanPasuach,
                                                        KavuahType.Sirug)))
            {
                for (DateTime dt = kavuah.SettingEntryDate.AddMonths(
                        kavuah.KavuahType == KavuahType.Sirug ? kavuah.Number : 1);
                    dt <= Program.Today.AddMonths(Properties.Settings.Default.numberMonthsAheadToWarn); 
                    dt = dt.AddMonths(kavuah.KavuahType == KavuahType.Sirug ? kavuah.Number : 1))
                {
                    Onah o = new Onah(dt, kavuah.DayNight) { 
                        Name = kavuah.KavuahDescriptionHebrew,
                        Day = kavuah.Number
                    };
                    list.Add(o);
                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        var ooz = Onah.GetPreviousOnah(o);
                        ooz.Name = " או\"ז - " + o.Name;
                        list.Add(ooz);
                    }
                }
            }

            //Kavuahs of Yom Haflaga of Dilug - cheshboned from the theoretical Entries
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                                        k.Active &&
                                        k.KavuahType == KavuahType.DilugHaflaga))
            {
                DateTime dt = kavuah.SettingEntryDate;
                for (int i = 0; i >= 0; i++)
                {
                    //For negative dilugim, we stop when we get to 0
                    if ((kavuah.SettingEntryInterval + (kavuah.Number * i)) < 1)
                    {
                        break;
                    }
                    dt = dt.AddDays(kavuah.SettingEntryInterval + (kavuah.Number * i));                    
                    if(dt > Program.Today.AddMonths(Properties.Settings.Default.numberMonthsAheadToWarn))
                    {
                        break;
                    }

                    Onah o = new Onah(dt, kavuah.DayNight) 
                    { 
                        Name = kavuah.KavuahDescriptionHebrew,
                        //Negative kavuahs of haflagah are just a chumra
                        IsChumrah = kavuah.Number < 1 
                    };
                    list.Add(o);
                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        var ooz = Onah.GetPreviousOnah(o);
                        ooz.Name = " או\"ז - " + o.Name;
                        list.Add(ooz);
                    }
                }   
            }
            
            return list;
        }

        private string GetNextOnahText()
        {
            string nextProblemText = "";
            if (this.ProblemOnas.Count > 0)
            {
                //We need to determine the earliest problem Onah, so we need to do a 
                //special sort on the list where the night Onah is before the day one for the same date.
                this.ProblemOnas.Sort(Onah.CompareOnahs);

                Onah nowProblem = this.ProblemOnas.FirstOrDefault(o => (!o.IsIgnored) && Onah.IsSameOnah(o, Program.NowOnah));
                Onah nextProblem = this.ProblemOnas.FirstOrDefault(o => (!o.IsIgnored) && (Onah.CompareOnahs(o, Program.NowOnah) == 1));

                if (nowProblem != null)
                {
                    nextProblemText += "עכשיו הוא " + nowProblem.Name;
                }

                if (nextProblem != null)
                {
                    if (nextProblemText.Length > 0)
                    {
                        nextProblemText += " - ";
                    }
                    nextProblemText += "העונה הבאה בעוד " +
                        ((nextProblem.DateTime - Program.Today).Days + 1).ToString() +
                        " ימים - בתאריך: " +
                        nextProblem.DateTime.ToString("dd MMMM yyyy") +
                        " (" +
                        nextProblem.HebrewDayNight +
                        ") שהוא " +
                        nextProblem.Name;
                }
            }
            return nextProblemText;
        }

        private void ProcessProblemOnahList(List<Onah> onahs)
        {
            foreach (Onah onah in onahs)
            {
                foreach (Onah problemOnah in this.ProblemOnas.Where(o => Onah.IsSameOnah(onah, o)))
                {
                    switch (onahs.IndexOf(onah))
                    {
                        case 0: //yesterday night
                            ProccessProblem(lblYesterdayNight, lblYesterdayDate, problemOnah);
                            break;
                        case 1: //yesterday day  
                            ProccessProblem(lblYesterdayDay, lblYesterdayDate, problemOnah);
                            break;
                        case 2: //today night
                            ProccessProblem(lblTodayNight, lblTodayDate, problemOnah);
                            break;
                        case 3: //today day    
                            ProccessProblem(lblTodayDay, lblTodayDate, problemOnah);
                            break;
                        case 4: //tomorrow night
                            ProccessProblem(lblTomorrowNight, lblTomorrowDate, problemOnah);
                            break;
                        case 5: //tomorrow day 
                            ProccessProblem(lblTomorrowDay, lblTomorrowDate, problemOnah);
                            break;
                        case 6: //day after tomorrow night
                            ProccessProblem(lblNextDayNight, lblNextdayDate, problemOnah);
                            break;
                        case 7: //day after tomorrow day  
                            ProccessProblem(lblNextDayDay, lblNextdayDate, problemOnah);
                            break;
                    }
                }
            }
        }

        private List<Onah> GetCalendarOnahs()
        {
            var onahs = new List<Onah>();
            var days = new DateTime[4];
            DateTime yesterday = Program.Today.AddDays(-1),
                     tomorrow = Program.Today.AddDays(1),
                     dayAfterTomorrow = Program.Today.AddDays(2);

            days[0] = yesterday;
            days[1] = Program.Today;
            days[2] = tomorrow;
            days[3] = dayAfterTomorrow;

            foreach (DateTime date in days)
            {
                onahs.Add(new Onah(date, DayNight.Night));
                onahs.Add(new Onah(date, DayNight.Day));
            }

            return onahs;
        }

        private void SetProblemOnahs()
        {
            Onah thirty,
               thirtyOhrZarua,
               thirtyOne,
               thirtyOneOhrZarua,
               intervalHaflagah,
               intervalHaflagahOhrZarua,
               kavuahHaflaga,
               kavuahHaflagaOhrZarua,
               kavuahDayOfWeek,
               kavuahDayOfWeekOhrZarua,
               kavuahDilugHaflaga,
               kavuahDilugHaflagaOhrZarua,
               kavuahDilugDayofMonth,
               kavuahDilugDayofMonthOhrZarua;

            foreach (Entry entry in Entry.EntryList)
            {
                bool hasCancelByKavuah = Kavuah.KavuahsList.Exists(k => k.Active && k.CancelsOnahBeinanis);

                //The 30th day after any entry is usually assur as an onah beinanus (unless there is a set Kavuah)
                thirty = entry.AddDays(29);
                thirty.Name = "יום שלושים";
                thirty.IsIgnored = hasCancelByKavuah;
                this.ProblemOnas.Add(thirty);

                //If the user wants to keep 24 for the Onah Beinenis
                if (Properties.Settings.Default.OnahBenIs24Hours)
                {
                    Onah o = thirty.Clone();
                    o.DayNight = o.DayNight == DayNight.Day ? DayNight.Night : DayNight.Day;
                    this.ProblemOnas.Add(o);
                }

                //If the user wants to see the Ohr Zarua  - the previous onah
                if (Properties.Settings.Default.ShowOhrZeruah)
                {
                    thirtyOhrZarua = Onah.GetPreviousOnah(thirty);
                    thirtyOhrZarua.Name = "או\"ז - יום שלושים";
                    thirtyOhrZarua.IsIgnored = hasCancelByKavuah;
                    this.ProblemOnas.Add(thirtyOhrZarua);
                }

                //31 is also an onah beinonus 
                thirtyOne = entry.AddDays(30);
                thirtyOne.Name = "יום ל\"א";
                thirtyOne.IsIgnored = hasCancelByKavuah;
                this.ProblemOnas.Add(thirtyOne);

                if (Properties.Settings.Default.OnahBenIs24Hours)
                {
                    Onah o = thirtyOne.Clone();
                    o.DayNight = o.DayNight == DayNight.Day ? DayNight.Night : DayNight.Day;
                    this.ProblemOnas.Add(o);
                }

                if (Properties.Settings.Default.ShowOhrZeruah)
                {
                    thirtyOneOhrZarua = Onah.GetPreviousOnah(thirtyOne);
                    thirtyOneOhrZarua.Name = "או\"ז - יום ל\"א";
                    thirtyOneOhrZarua.IsIgnored = hasCancelByKavuah;
                    this.ProblemOnas.Add(thirtyOneOhrZarua);
                }

                //Each Entry has an interval - the number of days from the previous entry
                if (entry.Interval > 0)
                {
                    intervalHaflagah = entry.AddDays(entry.Interval - 1);
                    intervalHaflagah.Name = "יום הפלגה";
                    intervalHaflagah.IsIgnored = hasCancelByKavuah;
                    this.ProblemOnas.Add(intervalHaflagah);

                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        intervalHaflagahOhrZarua = Onah.GetPreviousOnah(intervalHaflagah);
                        intervalHaflagahOhrZarua.Name = "או\"ז - יום הפלגה";
                        intervalHaflagahOhrZarua.IsIgnored = hasCancelByKavuah;
                        this.ProblemOnas.Add(intervalHaflagahOhrZarua);
                    }
                }

                //Kavuah Haflagah - with or without Maayan Pasuach
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                    k.KavuahType.In(KavuahType.Haflagah, KavuahType.HaflagaMaayanPasuach) && k.Active))
                {
                    kavuahHaflaga = entry.AddDays(kavuah.Number);
                    kavuahHaflaga.DayNight = kavuah.DayNight;
                    kavuahHaflaga.Name = kavuah.KavuahDescriptionHebrew;
                    this.ProblemOnas.Add(kavuahHaflaga);

                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        kavuahHaflagaOhrZarua = Onah.GetPreviousOnah(kavuahHaflaga);
                        kavuahHaflagaOhrZarua.Name = " או\"ז - " + kavuahHaflaga.Name;
                        this.ProblemOnas.Add(kavuahHaflagaOhrZarua);
                    }
                }

                //Kavuah Day of week
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k => k.KavuahType == KavuahType.DayOfWeek && k.Active))
                {
                    //DayOfWeek kavuas are the same as a haflagah just needs only 3 entries
                    kavuahDayOfWeek = entry.AddDays(kavuah.Number);
                    kavuahDayOfWeek.DayNight = kavuah.DayNight;
                    kavuahDayOfWeek.Name = kavuah.KavuahDescriptionHebrew;
                    this.ProblemOnas.Add(kavuahDayOfWeek);

                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        kavuahDayOfWeekOhrZarua = Onah.GetPreviousOnah(kavuahDayOfWeek);
                        kavuahDayOfWeekOhrZarua.Name = " או\"ז - " + kavuahDayOfWeek.Name;
                        this.ProblemOnas.Add(kavuahDayOfWeekOhrZarua);
                    }
                }

                //Kavvuah Dilug Haflagos - even if one was off, only works out from entry not from what was supposed to be
                //This is a machlokes.
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k => k.KavuahType == KavuahType.DilugHaflaga && k.Active))
                {
                    kavuahDilugHaflaga = entry.AddDays(entry.Interval + kavuah.Number);
                    kavuahDilugHaflaga.DayNight = kavuah.DayNight;
                    kavuahDilugHaflaga.Name = kavuah.KavuahDescriptionHebrew;
                    this.ProblemOnas.Add(kavuahDilugHaflaga);

                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        kavuahDilugHaflagaOhrZarua = Onah.GetPreviousOnah(kavuahDilugHaflaga);
                        kavuahDilugHaflagaOhrZarua.Name = " או\"ז - " + kavuahDilugHaflaga.Name;
                        this.ProblemOnas.Add(kavuahDilugHaflagaOhrZarua);
                    }
                }

                //Kavvuah Dilug Yom Hachodesh - even if one was off, only works out from entry not from what was supposed to be
                //This is a machlokes.
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k => k.KavuahType == KavuahType.DilugDayOfMonth && k.Active))
                {
                    DateTime next = entry.DateTime.AddMonths(1);
                    next = next.AddDays(kavuah.Number);
                    kavuahDilugDayofMonth = new Onah(next, kavuah.DayNight);
                    kavuahDilugDayofMonth.Name = kavuah.KavuahDescriptionHebrew;
                    this.ProblemOnas.Add(kavuahDilugDayofMonth);

                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        kavuahDilugDayofMonthOhrZarua = Onah.GetPreviousOnah(kavuahDilugDayofMonth);
                        kavuahDilugDayofMonthOhrZarua.Name = " או\"ז - " + kavuahDilugDayofMonth.Name;
                        this.ProblemOnas.Add(kavuahDilugDayofMonthOhrZarua);
                    }
                }
            }

        }

        private void SetWeekListHtml()
        {
            // First we combine double onahs - such as if one onah is also day 30 and also a haflagah.
            // We will only display it once, but with both descriptions.
            // If one of them is to be ignored though, it will get it's own row.
            var onahsToAdd = new List<Onah>();
            foreach (Onah onah in this.ProblemOnas.Where(on => on.DateTime >= Program.Today || Program.Today.IsSameday(on.DateTime)))
            {
                if (onahsToAdd.Exists(o => Onah.IsSameOnah(o, onah) && o.IsIgnored == onah.IsIgnored))
                {
                    onahsToAdd.Where(o => Onah.IsSameOnah(o, onah)).First().Name += " וגם " + onah.Name;
                }
                else
                {
                    onahsToAdd.Add(onah);
                }
            }

            var sb = new StringBuilder("<html><head><meta content='(text/html;charset=UTF-8;' />" +
                "<title>לוח חשבשבון ");
            sb.Append(Program.Today.ToLongDateString());
            sb.Append("</title><style>" +
                "body,table,td{text-align:right;direction:rtl;font-family:narkisim;}" +
                "table{border:solid 1px silver;width:100%;}" +
                "div.ignored{float:right;color:#999999;width:400px;text-align:right;}" +
                "td{padding:3px;margin:1px;}" +
                "tr.alt td{background-color:#f1f1f1;}" +
                "tr.red td{color:#ff0000;}" +
                "tr.ignored td{color:#999999;}</style></head>");
            sb.AppendFormat("<body><h3>לוח חשבשבון - עונות הבאות - {0}</h3>", Program.Today.ToLongDateString());
            if (onahsToAdd.Exists(o => o.IsIgnored))
            {
                sb.Append("<div class='ignored'>רשומות באפור לא פעילים עקב וסת קבוע</div>");
            }
            sb.Append("<table><tr><th>יום</th><th>תאריך</th><th>יום/לילה</th><th>סיבה</th></tr>");

            int count = 0;
            foreach (Onah onah in onahsToAdd)
            {
                sb.AppendFormat("<tr class='{0}'><td>{1}</td><td>{2} {3}</td><td>{4}</td><td width='50%'>{5}</td></tr>",
                    (count++ % 2 == 0 ? "alt" : "") + (Onah.IsSameOnah(Program.NowOnah, onah) ? " red" : "") + (onah.IsIgnored ? " ignored" : ""),
                    Zmanim.GetDayOfWeekText(onah.DateTime),
                    Zmanim.DaysOfMonthHebrew[onah.Day],
                    onah.Month.MonthName,
                    onah.HebrewDayNight,
                    onah.Name);
            }
            sb.AppendFormat("</table><br /><hr /><strong>{0}</strong><hr />", this.lblNextProblem.Text);
            sb.Append("</body></html>");
            this.WeekListHtml = sb.ToString();
        }

        private void ClearCalendar()
        {
            foreach (Label l in gbCalendar.Controls.OfType<Panel>().SelectMany(pnl => pnl.Controls.OfType<Label>()))
            {
                switch (Convert.ToString(l.Tag))
                {
                    case "Date":
                        l.BackColor = Color.Wheat;
                        l.ForeColor = Color.Black;
                        break;
                    case "Day":
                    case "Night":
                        l.Text = "";
                        l.BackColor = Color.White;
                        l.ForeColor = Color.Black;
                        l.Font = new Font(l.Font.FontFamily, 8);
                        break;
                }
            }
        }

        private void ProccessProblem(Label lbl, Label lblDate, Onah problemOnah)
        {
            lbl.Text = lbl.Text.Trim();
            if (problemOnah.IsIgnored)
            {
                lbl.Text += string.Format("{0}[{1}]",
                    (lbl.Text.Length > 0 ? Environment.NewLine : ""),
                    problemOnah.Name);
            }
            else
            {
                lbl.Text += (lbl.Text.Length > 0 ? Environment.NewLine : "") + problemOnah.Name;
            }

            this.toolTip1.SetToolTip(lbl, lbl.Text);

            //If this onah is to be ignored and the same onah doesn't have another non-ignoreable problem

            lblDate.BackColor = Color.SteelBlue;
            lblDate.ForeColor = Color.Wheat;
            if (lbl.Name.Contains("Today") && Program.NowOnah.DayNight == problemOnah.DayNight)
            {
                lbl.BackColor = Color.Red;
                lbl.ForeColor = Color.Wheat;
            }
            else
            {
                lbl.BackColor = Color.Tan;
            }
        }

        /// <summary>
        /// Saves all changes back to the source file. 
        /// </summary>
        /// <remarks>
        /// This function is run whenever a change is made to the list and when closing the app.
        /// </remarks>
        private void SaveCurrentFile()
        {
            //If no file was originally loaded, CurrentFile will be null. 
            //In this case, if there are entries in the list
            //we prompt the user to create a file to save to.
            while (string.IsNullOrEmpty(this.CurrentFile))
            {
                if (((Entry.EntryList.Count + Kavuah.KavuahsList.Count) > 0) &&
                    MessageBox.Show("?שמירת הרשימה מצריך קובץ. האם ליצור קובץ חדש",
                        "חשבשבון",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.SaveAs();
                }
                else
                {
                    return;
                }
            }

            XmlTextWriter xtw;
            Stream stream = null;

            if (this.CurrentFileIsRemote)
            {
                stream = new MemoryStream();
            }
            else
            {
                stream = File.CreateText(this.CurrentFile).BaseStream;
            }
            xtw = new XmlTextWriter(stream, Encoding.UTF8);
            xtw.WriteStartDocument();
            xtw.WriteStartElement("Entries");
            foreach (Entry entry in Entry.EntryList)
            {
                xtw.WriteStartElement("Entry");
                xtw.WriteElementString("Date", entry.DateTime.ToString("dd MMMM yyyy") + " " + entry.HebrewDayNight);
                xtw.WriteElementString("Day", entry.Day.ToString());
                xtw.WriteElementString("Month", entry.Month.MonthInYear.ToString());
                xtw.WriteElementString("Year", entry.Year.ToString());
                xtw.WriteElementString("DN", ((int)entry.DayNight).ToString());
                xtw.WriteElementString("Notes", entry.Notes);
                foreach (Kavuah k in entry.NoKavuahList)
                {
                    xtw.WriteStartElement("NoKavuah");
                    xtw.WriteAttributeString("KavuahType", k.KavuahType.ToString());
                    xtw.WriteAttributeString("Number", k.Number.ToString());
                    xtw.WriteEndElement();
                }
                xtw.WriteEndElement();
            }

            var ser = new System.Xml.Serialization.XmlSerializer(typeof(List<Kavuah>));
            ser.Serialize(xtw, Kavuah.KavuahsList);

            xtw.WriteEndDocument();
            xtw.Flush();
            if (this.CurrentFileIsRemote)
            {
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                try
                {
                    Utils.RemoteFunctions.SaveCurrentFile(this.CurrentFile, sr.ReadToEnd());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "חשבשבון", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                sr.Close();
                sr.Dispose();
            }
            xtw.Close();
            stream.Dispose();
            Properties.Settings.Default.IsCurrentFileRemote = CurrentFileIsRemote;
            Properties.Settings.Default.CurrentFile = this.CurrentFile;
            Properties.Settings.Default.Save();
            this.SetCaptionText();
        }


        /// <summary>
        /// Sorts the list of entries in order of occurrence, then sets the Interval for each Entry - 
        /// which is the days elapsed since the previous Entry.
        /// This is in order to Cheshbon out the Haflagah        
        /// </summary>
        private void SortEntriesAndSetInterval()
        {
            Entry.EntryList.Sort(Onah.CompareOnahs);

            Entry previousEntry = null;
            foreach (Entry entry in Entry.EntryList)
            {
                if (previousEntry != null)
                {
                    entry.SetInterval(previousEntry);
                }
                else
                {
                    entry.Interval = 0;
                }
                previousEntry = entry;
            }
        }

        private frmBrowser ShowCalendarTextList(bool print = false)
        {
            var fb = new frmBrowser(print);
            fb.Text = "לוח חשבשבון_" + Program.Today.ToString("dd_MMM_yyyy").Replace("'", "").Replace("\"", "");
            fb.Html = this.WeekListHtml;
            fb.Show();
            return fb;
        }

        private frmBrowser ShowEntryTextList(bool print = false)
        {
            var fb = new frmBrowser(print);
            fb.Text = "רשימת חשבשבון_" + Program.Today.ToString("dd_MMM_yyyy").Replace("'", "").Replace("\"", "");
            fb.Html = this.GetEntryListText();
            fb.Show();
            return fb;
        }

        private void PrintCalendarList()
        {
            this.ShowCalendarTextList(true);
        }

        private void PrintEntryList()
        {
            this.ShowEntryTextList(true);
        }

        private string GetEntryListText()
        {
            var sb = new StringBuilder("<html><head><meta content='text/html;charset=UTF-8;' />" +
                "<title>רשימת וסתות</title>" +
                "<style>body,table,td{text-align:right;direction:rtl;font-family:narkisim;}" +
                "table{border:solid 1px silver;width:100%;}" +
                "td{padding:3px;margin:1px;}tr.alt td{background-color:#f1f1f1;}</style></head>");
            sb.AppendFormat("<body><h3>רשימת וסתות - {0}</h3><table>", Program.Today.ToLongDateString());
            sb.Append("<tr><th>מספר</th><th>תאריך</th><th>יום/לילה</th><th>הפלגה</th><th>הערות</th></tr>");
            int count = 0;
            foreach (Entry e in Entry.EntryList)
            {
                sb.AppendFormat("<tr{0}><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td width='50%'>{5}</td></tr>",
                    (count++ % 2 == 0 ? " class='alt'" : ""),
                    count,
                    e.DateTime.ToLongDateString(),
                    e.HebrewDayNight,
                    e.Interval,
                    (string.IsNullOrEmpty(e.Notes) ? "&nbsp;" : e.Notes));
            }
            sb.Append("</table></body></html>");
            return sb.ToString();
        }

        private void SetDateAndDayNight()
        {
            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkiah = tc.GetSunset(DateTime.Now, Program.CurrentLocation);
            AstronomicalTime netz = tc.GetSunrise(DateTime.Now, Program.CurrentLocation);
            DateTime now = DateTime.Now;

            if (Properties.Settings.Default.IsSummerTime)
            {
                shkiah.Hour++;
                netz.Hour++;
            }

            int curHour = now.Hour;
            int curMin = now.Minute;

            bool isAfterNetz = curHour > netz.Hour || (curHour == netz.Hour && curMin > netz.Minute);
            bool isBeforeShkiah = (curHour < shkiah.Hour || (curHour == shkiah.Hour && curMin < shkiah.Minute));
            bool isNightTime = (!isAfterNetz) || (!isBeforeShkiah);
            bool isAfterMidnight = now.Hour < shkiah.Hour || (now.Hour == shkiah.Hour && now.Minute < shkiah.Minute);

            Program.Today = isNightTime && !isAfterMidnight ? now.AddDays(1) : now; //after shkia before midnight is tomorrow in Jewish...
            this.rbDay.Checked = !isNightTime;
            this.rbNight.Checked = isNightTime;
            Program.NowOnah = new Onah(Program.Today, isNightTime ? DayNight.Night : DayNight.Day);

            string todayString = Program.Today.ToString("dd MMMM");
            foreach (string holiday in JewishHolidays.GetHebrewHolidays(Program.Today, Properties.Settings.Default.UserInIsrael))
            {
                todayString += " - " + holiday;
            }
            this.lblToday.Text = todayString;
        }

        private string GetToolTipForDate(DateTime dateTime)
        {
            StringBuilder toolTip = new StringBuilder();
            foreach (string holiday in JewishHolidays.GetHebrewHolidays(dateTime, Properties.Settings.Default.UserInIsrael))
            {
                toolTip.AppendLine(holiday);
            }

            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkiah = tc.GetSunset(dateTime, Program.CurrentLocation);
            AstronomicalTime netz = tc.GetSunrise(dateTime, Program.CurrentLocation);

            if (Properties.Settings.Default.IsSummerTime)
            {
                shkiah.Hour++;
                netz.Hour++;
            }
            toolTip.Append("שקיעה - ");
            toolTip.Append(shkiah.Hour.ToString());
            toolTip.Append(":");
            toolTip.Append(shkiah.Minute.ToString("0#"));
            toolTip.Append("\nנץ - ");
            toolTip.Append(netz.Hour.ToString());
            toolTip.Append(":");
            toolTip.Append(netz.Minute.ToString("0#"));
            return toolTip.ToString();
        }

        private void FillZmanim()
        {
            int day = this.cmbDay.SelectedIndex + 1;
            int month = ((MonthObject)cmbMonth.SelectedItem).MonthInYear;
            int year = (int)cmbYear.SelectedItem;

            DateTime selDate = new DateTime(year, month, day, Program.HebrewCalendar);

            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkiah = tc.GetSunset(selDate, Program.CurrentLocation);
            AstronomicalTime netz = tc.GetSunrise(selDate, Program.CurrentLocation);

            if (Properties.Settings.Default.IsSummerTime)
            {
                shkiah.Hour++;
                netz.Hour++;
            }

            string sHoliday = null;
            foreach (string holiday in JewishHolidays.GetHebrewHolidays(selDate, Properties.Settings.Default.UserInIsrael))
            {
                sHoliday += holiday + " ";
            }

            if (sHoliday != null)
            {
                sHoliday = " - " + sHoliday;
            }

            lblLocation.Text = Program.CurrentLocation.Name + " - " + selDate.ToString("dddd dd MMM yyyy") + sHoliday;

            StringBuilder sb = new StringBuilder("נץ - ");
            sb.Append(netz.Hour.ToString());
            sb.Append(":");
            sb.Append(netz.Minute.ToString("0#"));
            sb.Append("\nשקיעה - ");
            sb.Append(shkiah.Hour.ToString());
            sb.Append(":");
            sb.Append(shkiah.Minute.ToString("0#"));

            lblZmanim.Text = sb.ToString();
        }

        private void ShowDayDetails(DateTime dateTime)
        {
            frmAddNewEntry f = new frmAddNewEntry(Program.HebrewCalendar.GetDayOfMonth(dateTime),
                Program.HebrewCalendar.GetMonth(dateTime),
                Program.HebrewCalendar.GetYear(dateTime));
            f.ShowDialog();
        }

        private void SetLocation()
        {
            string locName = Properties.Settings.Default.UserLocation;
            XmlNode locNode = LocationsXmlDoc.SelectSingleNode("//Location[Name='" + locName + "']");

            Program.CurrentLocation = new Location();

            Program.CurrentLocation.Name = locName;
            if (locNode != null)
            {
                Program.CurrentLocation.LatitudeDegrees = Convert.ToInt32(locNode.SelectSingleNode("LatitudeDegrees").InnerText);
                Program.CurrentLocation.LatitudeMinutes = Convert.ToInt32(locNode.SelectSingleNode("LatitudeMinutes").InnerText);
                Program.CurrentLocation.LatitudeType = (locNode.SelectSingleNode("LatitudeType").InnerText == "N" ? LatitudeTypeEnum.North : LatitudeTypeEnum.South);
                Program.CurrentLocation.LongitudeDegrees = Convert.ToInt32(locNode.SelectSingleNode("LongitudeDegrees").InnerText);
                Program.CurrentLocation.LongitudeMinutes = Convert.ToInt32(locNode.SelectSingleNode("LongitudeMinutes").InnerText);
                Program.CurrentLocation.LongitudeType = (locNode.SelectSingleNode("LongitudeType").InnerText == "E" ? LongitudeTypeEnum.East : LongitudeTypeEnum.West);
                Program.CurrentLocation.TimeZone = Convert.ToInt32(locNode.SelectSingleNode("Timezone").InnerText);
                Program.CurrentLocation.Elevation = Convert.ToInt32(locNode.SelectSingleNode("Elevation").InnerText);
            }
        }

        private string GetPassword()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Chashavshavon");
            if (regKey == null)
            {
                regKey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("Chashavshavon", RegistryKeyPermissionCheck.ReadWriteSubTree);
                regKey.SetValue("Entry", "", RegistryValueKind.String);
                regKey.SetValue("Straight", "1", RegistryValueKind.String);
            }
            string password = regKey.GetValue("Straight").ToString() == "1" ? null : regKey.GetValue("Entry").ToString();
            regKey.Close();
            return password;
        }

        #endregion

        #region Public Functions
        public void AddNewEntry(Entry newEntry)
        {
            this.bindingSourceEntries.Add(newEntry);
            this.SortEntriesAndSetInterval();
            Kavuah.FindAndPromptKavuahs();
            this.FillCalendar();
            this.SaveCurrentFile();
            //In case there were changes to the notes on some entries such as if there was a NoKavuah added
            this.bindingSourceEntries.ResetBindings(false);
        }

        public void DeleteEntry(Entry entry)
        {
            if (MessageBox.Show("האם אתם בטוחים שברצונכם למחוק השורה של " +
                                                    entry.DateTime.ToString("dd MMMM yyyy"),
                                                  "מחיקת שורה " + entry.DateTime.ToString("dd MMMM yyyy"),
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question,
                                                  MessageBoxDefaultButton.Button1,
                                                  MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
            {
                if (Kavuah.KavuahsList.Exists(k => k.SettingEntry == entry ||
                    (k.SettingEntryDate == entry.DateTime && k.DayNight == entry.DayNight)))
                {
                    if (MessageBox.Show(" נמצאו וסתי קבוע שהוגדרו על פי רשומה הזאת. האם אתם עדיין בטוחים שברצונכם למחוק השורה של " +
                                                entry.DateTime.ToString("dd MMMM yyyy") +
                                                " וגם כל וסת הקבוע שנרשמו בגללה?",
                                              "מחיקת שורה " + entry.DateTime.ToString("dd MMMM yyyy"),
                                              MessageBoxButtons.YesNo,
                                              MessageBoxIcon.Exclamation,
                                              MessageBoxDefaultButton.Button2,
                                              MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)
                    {
                        Kavuah.KavuahsList.RemoveAll(k => k.SettingEntry == entry ||
                            (k.SettingEntryDate == entry.DateTime && k.DayNight == entry.DayNight));
                    }
                    else
                    {
                        return;
                    }
                }
                this.bindingSourceEntries.Remove(entry);
                this.SortEntriesAndSetInterval();
                Kavuah.FindAndPromptKavuahs();
                this.FillCalendar();
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.ResetBindings(false);
            }
        }

        /// <summary>
        /// Checks to see if the user is connected to the internet, and activates remote functionality if they are.
        /// If the current file is a remote one and they are not connected, they are duly complained to about it...
        /// </summary>
        /// <returns></returns>
        public bool TestInternet()
        {
            bool hasInternet = Properties.Settings.Default.UseLocalURL || Utils.RemoteFunctions.IsConnectedToInternet();
            RemoteToolStripMenuItem.Visible = hasInternet;
            if (CurrentFileIsRemote && !hasInternet)
            {
                MessageBox.Show("הקובץ שאמור ליפתח" + Environment.NewLine + "\"" +
                    CurrentFile + "\"" + Environment.NewLine + "הוא קובץ רשת, אבל אין גישה לרשת כרגע" + Environment.NewLine + ".לכן תפתח קובץ ריק",
                    "חשבשבון",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                CurrentFileIsRemote = false;
                CurrentFile = Program.Today.ToString("ddMMMyyyy_hhmm").Replace("\"", "").Replace("'", "") + ".pm";
            }
            return hasInternet;
        }

        public void LoadXmlFile()
        {
            //Clear previous list data
            Entry.EntryList.Clear();
            //Clear previous Kavuahs
            if (Kavuah.KavuahsList != null)
            {
                Kavuah.KavuahsList.Clear();
            }
            this.lblNextProblem.Text = "";

            XmlDocument xml = new XmlDocument();

            if (CurrentFileIsRemote || File.Exists(CurrentFile))
            {
                try
                {
                    xml.LoadXml(this.CurrentFileXML);
                }
                catch (XmlException)
                {
                    MessageBox.Show("הקובץ שאמור ליפתח" + Environment.NewLine + "\"" +
                        CurrentFile + "\"" + Environment.NewLine +
                        " איננה קובץ חשבשבון תקינה. תפתח רשימה ריקה.",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign);
                }
            }

            if (xml.HasChildNodes)
            {
                foreach (XmlNode entryNode in xml.SelectNodes("//Entry"))
                {
                    int day = Convert.ToInt32(entryNode.SelectSingleNode("Day").InnerText);
                    int month = Convert.ToInt32(entryNode.SelectSingleNode("Month").InnerText);
                    int year = Convert.ToInt32(entryNode.SelectSingleNode("Year").InnerText); ;
                    DayNight dayNight = (DayNight)Convert.ToInt32(entryNode.SelectSingleNode("DN").InnerText);
                    string notes = entryNode.SelectSingleNode("Notes").InnerText; ;

                    Entry newEntry = new Entry(day, month, year, dayNight, notes);

                    // If during the addition of a new Entry the program finds 
                    // a set of 3 entries that might have been considered a Kavuah;
                    // such as if there are 3 of the same haflagas in a row,
                    // the user is prompted to create a new kavuah. If they choose not to,
                    // a NoKavuah element is added to the 3rd entry so the user 
                    // won't be prompted again each time the list is reloaded.
                    foreach (XmlNode k in entryNode.SelectNodes("NoKavuah"))
                    {
                        Kavuah ka = new Kavuah();
                        ka.KavuahType = (KavuahType)Enum.Parse(typeof(KavuahType), k.Attributes["KavuahType"].InnerText);
                        ka.Number = Convert.ToInt32(k.Attributes["Number"].InnerText);
                        ka.SettingEntryDate = newEntry.DateTime;
                        ka.DayNight = newEntry.DayNight;
                        newEntry.NoKavuahList.Add(ka);
                    }
                    Entry.EntryList.Add(newEntry);
                }

                //After the list of Entries, there is a lst of Kavuahs
                if (xml.SelectNodes("//Kavuah").Count > 0)
                {
                    var ser = new XmlSerializer(typeof(List<Kavuah>));
                    try
                    {
                        Kavuah.KavuahsList = (List<Kavuah>)ser.Deserialize(
                            new StringReader(xml.SelectSingleNode("//ArrayOfKavuah").OuterXml));
                    }
                    catch
                    {
                        MessageBox.Show("רשימת וסת קבוע בקובץ שאמור ליפתח" + Environment.NewLine + "\"" +
                        CurrentFile + "\"" + Environment.NewLine +
                        " איננה תקינה. תפתח רשימת קבוע ריקה.",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign);
                    }
                }
            }

            if (Kavuah.KavuahsList == null)
            {
                Kavuah.KavuahsList = new List<Kavuah>();
            }

            this.SetCaptionText();
            this.SortEntriesAndSetInterval();
            this.FillCalendar();
            this.bindingSourceEntries.ResetBindings(false);
        }

        public void SetCaptionText()
        {
            this.Text = "חשבשבון - " + Program.CurrentLocation.Name + " - " +
                (this.CurrentFileIsRemote ? "קובץ רשת - " : "") + this.CurrentFileName;
            this.pbWeb.Visible = this.CurrentFileIsRemote;
        }

        public void AfterChangePreferences()
        {
            this.SetLocation();
            this.SetDateAndDayNight();
            this.FillCalendar();
            this.FillZmanim();
            this.CalculateCalendar();
            this.SetCaptionText();
        }
        #endregion

        #region Properties
        public List<Onah> ProblemOnas { get; private set; }
        public bool CloseMeFirst { get; set; }
        public string WeekListHtml { get; set; }
        public string CurrentFile
        {
            get
            {
                return Properties.Settings.Default.CurrentFile;
            }
            set
            {
                Properties.Settings.Default.CurrentFile = value;
                Properties.Settings.Default.Save();
                this.SetCaptionText();
            }
        }
        public bool CurrentFileIsRemote
        {
            get
            {
                return Properties.Settings.Default.IsCurrentFileRemote;
            }
            set
            {
                Properties.Settings.Default.IsCurrentFileRemote = value;
                Properties.Settings.Default.Save();
                this.SetCaptionText();
            }
        }
        public string CurrentFileXML
        {
            get
            {
                string xml = null;
                if (this.CurrentFileIsRemote)
                {
                    try
                    {
                        xml = Utils.RemoteFunctions.GetCurrentFileText(this.CurrentFile);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "חשבשבון", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    xml = File.ReadAllText(this.CurrentFile);
                }

                return (string.IsNullOrEmpty(xml) ? "<Entries />" : xml);
            }
        }
        public string CurrentFileName
        {
            get
            {
                string[] cf = CurrentFile.Split(new char[] { '\\', '/' });
                return cf[cf.Length - 1];
            }
        }
        #endregion                        
    }
}