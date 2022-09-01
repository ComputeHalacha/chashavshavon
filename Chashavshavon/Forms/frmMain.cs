using Chashavshavon.Utils;
using JewishCalendar;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Tahara;

namespace Chashavshavon
{
    public partial class FrmMain : Form
    {
        #region Private Variables

        private DateTime _monthToDisplay;
        private static readonly string _tempJSONFileName = Program.TempFolderPath + @"\ChashavshavonTempFile.json";
        private static Font _smallFont;
        private static Font _medFont;
        private static readonly Font _hebrewDayFont = new Font("Verdana", 18f, FontStyle.Bold);
        private static readonly Font _goyishDateFont = new Font("Verdana", 8f, FontStyle.Bold);
        private static readonly SolidBrush _highlightBrush = new SolidBrush(Color.FromArgb(50, Color.DarkSlateBlue));
        private static readonly DateTimeFormatInfo _sysCulture = CultureInfo.InstalledUICulture.DateTimeFormat;
        private static readonly TextFormatFlags _textFormatFlags =
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter |
            TextFormatFlags.NoPrefix;

        #endregion Private Variables

        #region Constructors

        public FrmMain()
        {
            StartUp();
        }

        public FrmMain(string filePath)
        {
            CurrentFile = filePath;
            StartUp();
        }

        #endregion Constructors

        #region Event Handlers
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (CloseMeFirst)
            {
                Close();
            }

            dgEntries.AutoGenerateColumns = false;

            //Checks to see if the user is connected to the internet, and activates remote functionality if they are.
            TestInternet();
            if (Properties.Settings.Default.openNewFile)
            {
                CurrentFile = null;
                saveFileDialog1.Title = "נא לבחור שם ומיקום לקובץ החדש";
                saveFileDialog1.CreatePrompt = false;
                saveFileDialog1.CheckFileExists = false;
                saveFileDialog1.OverwritePrompt = true;
                saveFileDialog1.DefaultExt = "pmj";
                saveFileDialog1.FileName = Program.Today.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pmj";

                if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    CurrentFile = saveFileDialog1.FileName;
                }
            }
            else if (Properties.Settings.Default.openFileDialog)
            {
                openFileDialog1.ShowDialog(this);
                openFileDialog1.CheckFileExists = true;
                CurrentFile = openFileDialog1.FileName;

            }
            else if (Properties.Settings.Default.openNoFile)
            {
                CurrentFile = null;
            }
            //Load the last opened file. If it does not exist or this is the first run, a blank list is presented
            LoadJSONFile();
            bindingSourceEntries.DataSource = Program.EntryList.Where(en => !en.IsInvisible);
            _monthToDisplay = Program.Today;
            DisplayMonth();

            foreach (string f in Properties.Settings.Default.RecentFiles)
            {
                recentFilesToolStripMenuItem.DropDownItems.Add(f);
            }
            recentFilesToolStripMenuItem.Enabled = clearRecentFilesToolStripMenuItem.Enabled = recentFilesToolStripMenuItem.HasDropDownItems;
        }


        private void AbouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAboutBox ab = new FrmAboutBox())
            {
                ab.ShowDialog(this);
            }
        }

        private void AddKavuahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewKavuah(this);
        }

        private void AddNewEntry(object sender, EventArgs e)
        {
            using (FrmDailyInfo f = new FrmDailyInfo((DateTime)((Control)sender).Tag))
            {
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    //Refresh in case of change to current month
                    DisplayMonth();
                }
            }
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            FrmDailyInfo f = new FrmDailyInfo(Program.Today);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //Refresh in case of change to current month
                DisplayMonth();
            }
        }

        private void btnAddKavuah_Click(object sender, EventArgs e)
        {
            AddNewKavuah(this);
        }

        private void btnCheshbonKavuahs_Click(object sender, EventArgs e)
        {
            if (FindAndPromptKavuahs())
            {
                //Save the new Kavuahs to the file
                SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                bindingSourceEntries.DataSource = Program.EntryList.Where(en => !en.IsInvisible);
            }
            else
            {
                Program.Inform("לא נמצאו וסת קבוע אפשריים");
            }
        }

        private void btnLastMonth_Click(object sender, EventArgs e)
        {
            _monthToDisplay = Program.HebrewCalendar.AddMonths(_monthToDisplay, -1);
            DisplayMonth();
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            _monthToDisplay = Program.HebrewCalendar.AddMonths(_monthToDisplay, 1);
            DisplayMonth();
        }

        private void btnOpenKavuahs_Click(object sender, EventArgs e)
        {
            ShowKavuahList();
        }

        private void btnPrefs_Click(object sender, EventArgs e)
        {
            FrmPreferences prefs = new FrmPreferences();
            prefs.Show(this);
        }

        private void btnPrintCalendar_Click(object sender, EventArgs e)
        {
            PrintCalendarList();
        }

        private void btnPrintEntryList_Click(object sender, EventArgs e)
        {
            PrintEntryList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            _monthToDisplay = Program.Today;
            DisplayMonth();
        }

        private void btnViewTextCalendar_Click(object sender, EventArgs e)
        {
            ShowCalendarTextList();
        }

        private void btnViewTextEntryList_Click(object sender, EventArgs e)
        {
            ShowEntryTextList();
        }

        private void clearRecentFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RecentFiles.Clear();
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            recentFilesToolStripMenuItem.Enabled = clearRecentFilesToolStripMenuItem.Enabled = false;
        }

        private void dgEntries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEntries.Rows[e.RowIndex].DataBoundItem is Entry)
            {
                DataGridViewColumn column = dgEntries.Columns[e.ColumnIndex];
                Entry entry = (Entry)dgEntries.Rows[e.RowIndex].DataBoundItem;

                if (column == btnDeleteColumn)
                {
                    DeleteEntry(entry);
                }
                else if (column == DateColumn)
                {
                    _monthToDisplay = entry.DateTime;
                    DisplayMonth(highlightDay: true);
                }
            }
        }

        private void dgEntries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgEntries.Columns["NotesColumn"].Index)
            {
                DataGridViewRow r = dgEntries.Rows[e.RowIndex];
                if (r.DataBoundItem is Entry entry)
                {
                    string nkText = "";
                    foreach (Kavuah nk in entry.NoKavuahList)
                    {
                        nkText += " לא לרשום קבוע " + nk.ToString();
                    }
                    if (nkText.Length > 0)
                    {
                        e.Value += " [" + nkText.Trim() + "]";
                    }
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveCurrentFile();
                //the temp folder is only deleted if the user manually closed the app.
                //Otherwise we may be in an installer run and do not want to delete the installation files.
                Program.BeforeExit(e.CloseReason == CloseReason.UserClosing);
            }
            catch (Exception ex)
            {
                Program.ErrorMessage("ארעה שגיעה.\nיתכן שלא נשמר הפעולות אחרונות שנעשתה בתוכנה.\nפרטי השגיעה רשומים למטה.\n---------------------------------------------\n." + ex.Message);
            }
        }

        /*private void frmMain_ResizeBegin(object sender, EventArgs e)
        {
            this.luachTableLayout.Visible = false;
            this.luachTableLayout.SuspendLayout();
        }

        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            this.luachTableLayout.ResumeLayout();
            this.luachTableLayout.Visible = true;
        }*/

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentFile(); //why not...
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(openFileDialog1.FileName);
                    if (doc.SelectNodes("//Kavuah").Count > 0)
                    {
                        try
                        {
                            XmlSerializer ser = new XmlSerializer(typeof(List<Kavuah>));
                            List<Kavuah> list = (List<Kavuah>)ser.Deserialize(
                                new StringReader(doc.SelectSingleNode("//ArrayOfKavuah").OuterXml));

                            Program.KavuahList.AddRange(list);

                            /* If the setting entry of the Kavuah is not contained
                            * on the current list, than frmKavuah will cause an error
                            * as it uses the current EntryList as a DataSource for a drop-down
                            * for the SettingEntry field and it sets its displayed value for each Kavuah
                            * as the settingEntryDate of the Kavuah. So each Kavuah in the list
                            * needs a corresponding Entry in the EntryList.
                            * This is the scenario that prompted the whole IsInvisible property of the
                            * Entry class. */
                            foreach (Kavuah kav in list)
                            {
                                if (!Program.EntryList.Exists(en =>
                                    en.DateTime.IsSameday(kav.SettingEntryDate) &&
                                    en.DayNight == kav.DayNight &&
                                    en.Interval == kav.SettingEntryInterval))
                                {
                                    Program.EntryList.Add(new Entry(
                                        Program.HebrewCalendar.GetDayOfMonth(kav.SettingEntryDate),
                                        Program.HebrewCalendar.GetMonth(kav.SettingEntryDate),
                                        Program.HebrewCalendar.GetYear(kav.SettingEntryDate),
                                        kav.DayNight,
                                        null)
                                    {
                                        IsInvisible = true
                                    });
                                }
                            }
                            //Clean out any overlappers
                            Kavuah.ClearDoubleKavuahs(Program.KavuahList);

                            CalculateProblemOnahs();
                            DisplayMonth();
                            ShowKavuahList();
                        }
                        catch
                        {
                            Program.Exclaim("רשימת וסת קבוע בקובץ\"" +
                                Path.GetFileName(openFileDialog1.FileName) + "\" .איננה תקינה");
                        }
                    }
                    else
                    {
                        Program.Inform("רשימת וסת קבוע בקובץ\"" +
                            Path.GetFileName(openFileDialog1.FileName) + "\" ריקה.");
                    }
                }
                catch (Exception ex)
                {
                    Program.ErrorMessage(ex.Message);
                }
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewFile();
        }

        private void OpenBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.BackupFolderPath);
        }

        private void openKavuaListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowKavuahList();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
            openFileDialog1.ShowDialog(this);
            openFileDialog1.CheckFileExists = true;
            CurrentFile = openFileDialog1.FileName;
            LoadJSONFile();
            CalculateProblemOnahs();
            DisplayMonth();
        }

        private void pbWeb_Click(object sender, EventArgs e)
        {
            FrmRemoteFiles f = new FrmRemoteFiles();
            f.Show(this);
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPreferences prefs = new FrmPreferences();
            prefs.Show(this);
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintCalendarList();
        }

        private void recentFilesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (File.Exists(e.ClickedItem.Text))
            {
                SaveCurrentFile();
                CurrentFile = e.ClickedItem.Text;
                LoadJSONFile();
                CalculateProblemOnahs();
                DisplayMonth();
            }
            else
            {
                recentFilesToolStripMenuItem.HideDropDown(); // was blocking message box
                if (Program.AskUser("הקובץ \"" + e.ClickedItem.Text +
                        "\" לא נמצא.\nלהסירה מרשימת קבצים אחרונים?"))
                {
                    Properties.Settings.Default.RecentFiles.Remove(e.ClickedItem.Text);
                    recentFilesToolStripMenuItem.DropDownItems.Remove(e.ClickedItem);
                    recentFilesToolStripMenuItem.Enabled = clearRecentFilesToolStripMenuItem.Enabled = recentFilesToolStripMenuItem.HasDropDownItems;
                }
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
            Program.Inform("הקובץ נשמרה");
        }

        private void SearchForKavuahsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindAndPromptKavuahs())
            {
                //Save the new Kavuahs to the file
                SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                bindingSourceEntries.DataSource = Program.EntryList.Where(en => !en.IsInvisible);
            }
            else
            {
                Program.Inform("לא נמצאו וסת קבוע אפשריים");
            }
        }

        private void SourceTextMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CurrentFile) && File.Exists(CurrentFile))
            {
                SaveCurrentFile();

                System.Diagnostics.Process notepad = new System.Diagnostics.Process();
                int progThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

                notepad.StartInfo.FileName = "notepad.exe";
                notepad.StartInfo.Arguments = CurrentFile;
                notepad.EnableRaisingEvents = true;
                notepad.Exited += delegate
                {
                    //sometimes this is called twice - once in a different thread...
                    if (progThreadId == System.Threading.Thread.CurrentThread.ManagedThreadId)
                    {
                        RefreshData();
                    }
                };
                notepad.Start();
                notepad.WaitForExit();
                notepad.Dispose();
            }
            else
            {
                Program.Exclaim(".הצגת מקור מצריך קובץ");
            }
        }

        private void SourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(CurrentFile))
            {
                SaveCurrentFile();
                GetTempJSONFile();
                System.Diagnostics.Process.Start(_tempJSONFileName);
            }
            else
            {
                Program.Exclaim(".הצגת מקור מצריך קובץ");
            }
        }

        private void TextListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCalendarTextList();
        }

        private void toolStripMenuItemEntryList_Click(object sender, EventArgs e)
        {
            ShowEntryTextList();
        }

        private void toolStripMenuItemPrintEntryList_Click(object sender, EventArgs e)
        {
            PrintEntryList();
        }

        private void toolStripMenuItemRemote_Click(object sender, EventArgs e)
        {
            FrmRemoteFiles f = new FrmRemoteFiles();
            f.Show(this);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ImportEntriesStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.CheckFileExists = false;
            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            string fileText = File.ReadAllText(openFileDialog1.FileName);
            if (string.IsNullOrEmpty(fileText))
            {
                return;
            }
            using (FrmImport i = new FrmImport(fileText))
            {
                bool added = false;
                if (i.ShowDialog() == DialogResult.OK)
                {
                    (List<Entry> entries, List<Kavuah> kavuahs, List<TaharaEvent> taharaEvents) = (i.EntryList, i.KavuahList, i.TaharaEventList);
                    foreach (Entry entry in entries)
                    {
                        if (!Program.EntryList.Exists(o => Onah.IsSimilarOnah(o, entry)))
                        {
                            Program.EntryList.Add(entry);
                            added = true;
                        }
                    }
                    foreach (Kavuah kavuah in kavuahs)
                    {
                        if (!Program.KavuahList.Exists(o => Kavuah.IsSameKavuah(o, kavuah)))
                        {
                            Program.KavuahList.Add(kavuah);
                            added = true;
                        }
                    }
                    foreach (TaharaEvent taharaEvent in taharaEvents)
                    {
                        if (!Program.TaharaEventList.Exists(te => te.TaharaEventType == taharaEvent.TaharaEventType && te.DateTime == taharaEvent.DateTime))
                        {
                            Program.TaharaEventList.Add(taharaEvent);
                            added = true;
                        }
                    }
                    if (added)
                    {
                        SaveCurrentFile();
                        RefreshData();
                    }
                }
            }
        }
        #endregion Event Handlers

        #region Private Functions

        private void CreateNewFile()
        {
            if (!string.IsNullOrEmpty(CurrentFile))
            {
                SaveCurrentFile();
            }
            saveFileDialog1.Title = "נא לבחור שם ומיקום לקובץ החדש";
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.DefaultExt = "pmj";
            saveFileDialog1.FileName = Program.Today.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pmj";

            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                CurrentFile = saveFileDialog1.FileName;
                RefreshData();
            }
        }

        private void SaveAs(Form sourceForm = null)
        {
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "pmj";
            openFileDialog1.FileName = Program.Today.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pmj";
            if (openFileDialog1.ShowDialog(sourceForm ?? this) != DialogResult.OK)
            {
                return;
            }
            CurrentFile = openFileDialog1.FileName;
            SaveCurrentFile();
            return;
        }

        private void ShowKavuahList()
        {
            using (FrmKavuahs f = new FrmKavuahs())
            {
                if (f.ShowDialog(this) != DialogResult.Cancel)
                {
                    SaveCurrentFile();
                    TestInternet();
                    LoadJSONFile();
                }

                //In case some Kavuahs were added or deleted...
                CalculateProblemOnahs();
                DisplayMonth();
            }
        }

        private void StartUp()
        {
            //In case we will display the password entry form, we hide this until form load
            Hide();
            string password = GetPassword();

            InitializeComponent();

            _smallFont = new Font(Font.FontFamily, 6f);
            _medFont = new Font(Font.FontFamily, 8f);

            //The following sets all output displays of date time functions to Jewish dates for the current thread
            Program.CultureInfo.DateTimeFormat.Calendar = Program.HebrewCalendar;
            System.Threading.Thread.CurrentThread.CurrentCulture = Program.CultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = Program.CultureInfo;

            SetLocation();
            SetDateAndDayNight();

            if (password == null)
            {
                Show();
            }
            else
            {
                //Prompt for a password and don't stop prompting until the user gets it right or gives up
                using (FrmEnterPassword f = new FrmEnterPassword(password))
                {
                    do
                    {
                        f.ShowDialog(this);
                        if (f.DialogResult == DialogResult.No)
                        {
                            Program.Exclaim("סיסמה שגויה");
                        }
                    }
                    while (f.DialogResult == DialogResult.No);
                    //If the user canceled etc. we will close this in form load
                    CloseMeFirst = f.DialogResult != DialogResult.Yes;
                }
            }

            for (int i = 0; i < 7; i++)
            {
                dowLayoutTable.Controls.Add(new Label()
                {
                    Text = GeneralUtils.DaysOfWeekHebrewFull[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(255, 225, 228, 230),
                    BackColor = Color.Transparent,
                    Font = new Font("Narkisim", 12f)
                }, i, 0);
            }
        }

        private void DisplayMonth(bool highlightDay = false)
        {
            int year = Program.HebrewCalendar.GetYear(_monthToDisplay);
            MonthObject month = new MonthObject(year, Program.HebrewCalendar.GetMonth(_monthToDisplay));
            DateTime firstDayOfMonth = _monthToDisplay.AddDays(1 - Program.HebrewCalendar.GetDayOfMonth(_monthToDisplay));
            int firstDayOfWeek = 1 + (int)firstDayOfMonth.DayOfWeek;
            int currentRow = 0, currentColumn = firstDayOfWeek - 1;

            luachTableLayout.Visible = false;
            luachTableLayout.SuspendLayout();

            foreach (Control c in luachTableLayout.Controls)
            {
                c.Dispose();
            }
            luachTableLayout.Controls.Clear();

            lblMonthName.Text = _monthToDisplay.ToString("MMM yyyy", Program.CultureInfo);
            btnLastMonth.Text = "  " + Program.HebrewCalendar.AddMonths(_monthToDisplay, -1).ToString("MMM") + "  ";
            btnNextMonth.Text = "  " + Program.HebrewCalendar.AddMonths(_monthToDisplay, 1).ToString("MMM") + "  ";

            if (((int)firstDayOfMonth.DayOfWeek >= 6) &&
                   Program.HebrewCalendar.GetDaysInMonth(year, month.MonthInYear)
                > 29)
            {
                luachTableLayout.RowCount = 6;
                luachTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
                foreach (RowStyle rs in luachTableLayout.RowStyles)
                {
                    if (rs.SizeType == SizeType.Percent)
                    {
                        rs.Height = 16.66667F;
                    }
                }
            }
            else
            {
                luachTableLayout.RowCount = 5;
            }

            Padding dayInfoMargin = new Padding(0,
                (luachTableLayout.Height / luachTableLayout.RowCount) / 5, 0, 0);


            for (int i = 1; i < month.DaysInMonth + 1; i++)
            {
                DateTime date = new DateTime(Program.HebrewCalendar.GetYear(_monthToDisplay),
                    Program.HebrewCalendar.GetMonth(_monthToDisplay),
                    i,
                    Program.HebrewCalendar);

                Panel pnl = new TableLayoutPanel()
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    Padding = new Padding(0),
                    BackColor = Color.Transparent
                };

                Image dayBgImgMain = Properties.Resources.WhiteMarble;
                Image dayBgImgLeft = null;
                Image dayBgImgRight = null;

                luachTableLayout.Controls.Add(pnl, currentColumn, currentRow);

                string daySpecialText = "";
                bool isInIsrael = Program.CurrentLocation.IsInIsrael;
                JewishDate jd = new JewishDate(date);
                IEnumerable<SpecialDay> holidays = Zmanim.GetHolidays(jd, isInIsrael).Cast<SpecialDay>();
                if (jd.DayOfWeek == DayOfWeek.Saturday)
                {
                    if (!((jd.Month == 7 && jd.Day.In(1, 2, 10, 15, 16, 17, 18, 19, 20, 21, 22, (isInIsrael ? 0 : 23))) ||
                        (jd.Month == 1 && jd.Day.In(15, 16, 17, 18, 19, 20, 21, (isInIsrael ? 0 : 22)) ||
                        (jd.Month == 3 && jd.Day.In(6, (isInIsrael ? 0 : 7))))))
                    {
                        Parsha[] parshas = Sedra.GetSedra(jd, isInIsrael);
                        daySpecialText = "שבת פרשת " + string.Join(" - ", parshas.Select(p => p.nameHebrew)) + Environment.NewLine;
                    }
                }
                if (holidays.Any(h => h.DayType.IsSpecialDayType(SpecialDayTypes.HasCandleLighting)) &&
                    !holidays.Any(h => h.DayType.IsSpecialDayType(SpecialDayTypes.MajorYomTov)))
                {
                    TimeOfDay shkiah = Zmanim.GetShkia(date, Program.CurrentLocation);
                    shkiah -= Program.CurrentLocation.CandleLighting;
                    daySpecialText += "הדלק\"נ: " +
                        shkiah.ToString(true, false, false) + Environment.NewLine;
                }

                daySpecialText += Zmanim.GetHolidaysText(
                    holidays.Where(h => h.NameHebrew != "ערב שבת").ToArray(),
                    Environment.NewLine, true);

                if (!string.IsNullOrWhiteSpace(daySpecialText))
                {
                    pnl.Controls.Add(new Label()
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Top,
                        Text = daySpecialText,
                        Font = _smallFont,
                        ForeColor = Color.DarkSlateGray,
                        TextAlign = ContentAlignment.TopCenter,
                        RightToLeft = RightToLeft.Yes,
                        Margin = dayInfoMargin,
                        AutoEllipsis = true
                    });
                }

                if (date.DayOfWeek == DayOfWeek.Saturday || holidays.Any(h =>
                 h.DayType.IsSpecialDayType(SpecialDayTypes.MajorYomTov)))
                {
                    dayBgImgMain = Properties.Resources.BlueMarble;
                }
                else if (
                    holidays.Any(h => h.DayType.IsSpecialDayType(SpecialDayTypes.MinorYomtov)))
                {
                    dayBgImgMain = Properties.Resources.LightBlueMarble;
                }

                string onahText = "";
                bool hasDayOnah = false;
                bool hasNightOnah = false;
                if (Program.ProblemOnahs != null)
                {
                    foreach (Onah o in Program.ProblemOnahs.Where(o => o.DateTime.IsSameday(date) && !o.IsIgnored))
                    {
                        if (!string.IsNullOrWhiteSpace(onahText))
                        {
                            onahText += Environment.NewLine;
                        }
                        if (o.DayNight == DayNight.Day)
                        {
                            hasDayOnah = true;
                        }
                        else if (o.DayNight == DayNight.Night)
                        {
                            hasNightOnah = true;
                        }
                        onahText += o.HebrewDayNight + ": " + o.Name;
                    }
                }

                Entry entry = Program.EntryList.FirstOrDefault(en =>
                    !en.IsInvisible &&
                    en.DateTime.IsSameday(date));
                if (!string.IsNullOrWhiteSpace(onahText))
                {
                    if (hasNightOnah)
                    {
                        dayBgImgRight = Properties.Resources.ParchmentMarbleTile;
                    }
                    if (hasDayOnah)
                    {
                        dayBgImgLeft = Properties.Resources.ParchmentMarbleTile;
                    }

                    pnl.Controls.Add(new Label()
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Top,
                        Text = onahText,
                        Font = _smallFont,
                        ForeColor = Color.FromArgb(0x80, 0x50, 0),
                        TextAlign = ContentAlignment.BottomCenter,
                        RightToLeft = RightToLeft.Yes,
                        AutoEllipsis = true,
                        Margin = dayInfoMargin
                    });
                }
                if (entry != null)
                {
                    string entryText = "ראיה - עונת " + entry.HebrewDayNight;
                    if (entry.Interval > 0)
                    {
                        entryText += Environment.NewLine + " הפלגה: " + entry.Interval.ToString();
                    }
                    if (entry.DayNight == DayNight.Day)
                    {
                        dayBgImgLeft = Properties.Resources.PinkMarbleTile;
                    }
                    else
                    {
                        dayBgImgRight = Properties.Resources.PinkMarbleTile;
                    }
                    pnl.Controls.Add(new Label()
                    {
                        AutoSize = true,
                        Anchor = AnchorStyles.Top,
                        Text = entryText,
                        ForeColor = Color.DarkRed,
                        Font = _smallFont,
                        TextAlign = ContentAlignment.BottomCenter,
                        RightToLeft = RightToLeft.Yes,
                        Margin = dayInfoMargin
                    });
                }

                IEnumerable<TaharaEvent> taharaEvents = Program.TaharaEventList.Where(te =>
                   te.DateTime.IsSameday(date));

                pnl.Paint += delegate (object sender, PaintEventArgs e)
                {
                    using (Graphics g = e.Graphics)
                    {
                        int pWidth = pnl.DisplayRectangle.Width;
                        int pHeight = pnl.DisplayRectangle.Height;

                        if (dayBgImgRight == null && dayBgImgLeft == null)
                        {
                            g.DrawImage(dayBgImgMain, 0, 0, pWidth, pHeight);
                        }
                        else
                        {
                            float halfX = pWidth / 2f;

                            g.DrawImage(dayBgImgLeft ?? dayBgImgMain, 0, 0, halfX, pHeight);
                            g.DrawImage(dayBgImgRight ?? dayBgImgMain, halfX, 0, halfX, pHeight);
                        }
                        if (highlightDay && date.Date == _monthToDisplay.Date)
                        {
                            g.DrawRectangle(new Pen(_highlightBrush, pHeight / 3), pnl.DisplayRectangle);
                        }
                        if (Program.Today.IsSameday(date))
                        {
                            float eh = pHeight / 3.3f;
                            float ew = pWidth / 3.3f;
                            g.FillClosedCurve(_highlightBrush, new PointF[] {
                                new PointF(ew, eh),
                                new PointF((pWidth - ew), eh),
                                new PointF((pWidth - ew), (pHeight - eh)),
                                new PointF(ew, (pHeight - eh))
                            }, System.Drawing.Drawing2D.FillMode.Alternate, 3f);
                        }

                        string jdText = jd.Day.ToNumberHeb().Replace("'", "");

                        TextRenderer.DrawText(g, jdText, _hebrewDayFont,
                            new Point(pWidth * 2 - 40, 10), Color.Maroon, _textFormatFlags);

                        g.DrawString(date.ToString("%d", _sysCulture),
                            _goyishDateFont,
                            Brushes.DarkSlateBlue,
                            20,
                            10);

                        if (Program.EntryList.Count > 0)
                        {
                            Entry latestEntry = Program.EntryList.LastOrDefault(en => en.DateTime < date);
                            if (latestEntry != null)
                            {
                                double day = (date - latestEntry.DateTime).TotalDays + 1;
                                if (day > 0)
                                {
                                    g.DrawString("יום " + day.ToString(), _smallFont, Brushes.Red, pWidth - 40, pHeight - 20);
                                }
                            }
                        }
                        int counter = 0;
                        foreach (TaharaEvent te in taharaEvents)
                        {
                            Brush color = Brushes.Black;
                            float x = 0.0f;
                            switch (te.TaharaEventType)
                            {
                                case TaharaEventType.Hefsek:
                                    color = Brushes.Blue;
                                    x = pWidth - 75;
                                    break;
                                case TaharaEventType.Mikvah:
                                    color = Brushes.Green;
                                    x = (pWidth / 2) - 15;
                                    break;
                                case TaharaEventType.Shailah:
                                    color = Brushes.MediumVioletRed;
                                    x = 35;
                                    break;
                            }
                            counter++;
                            g.DrawString(te.TaharaEventTypeName,
                                _medFont,
                                color,
                                x,
                                pHeight - 50);
                        }

                    }
                };

                string toolTipText = date.ToLongDateString() +
                    Environment.NewLine +
                    date.ToString("D", _sysCulture) +
                    (!string.IsNullOrWhiteSpace(onahText) ?
                        Environment.NewLine + "--------------------------------" + Environment.NewLine + onahText : "");

                //Sets the tag, tooltip and click function for all controls of the day
                foreach (Control cntrl in GeneralUtils.GetAllControls(luachTableLayout.GetControlFromPosition(currentColumn, currentRow)))
                {
                    cntrl.Tag = date;
                    cntrl.Click += new EventHandler(AddNewEntry);
                    toolTip1.SetToolTip(cntrl, toolTipText);
                }

                if (currentColumn == luachTableLayout.ColumnCount - 1)
                {
                    currentColumn = 0;
                    currentRow++;
                }
                else
                {
                    currentColumn++;
                }
            }
            luachTableLayout.ResumeLayout();
            luachTableLayout.Visible = true;
        }

        private void CreateLocalBackup()
        {
            System.ComponentModel.BackgroundWorker bgw = new System.ComponentModel.BackgroundWorker();
            bgw.DoWork += delegate
            {
                if (File.Exists(CurrentFile))
                {
                    string path = Program.BackupFolderPath + "\\" +
                        Path.GetFileNameWithoutExtension(CurrentFile) +
                        "_" +
                        DateTime.Now.ToString("d-MMM-yy_HH-mm-ss", CultureInfo.GetCultureInfo("en-us").DateTimeFormat) +
                        ".pmj";
                    if (File.Exists(path))
                    {
                        path = path.Replace(".pmj", "_Version_1" + DateTime.Now.Millisecond.ToString() + ".pmj");
                    }
                    File.Copy(CurrentFile, path);
                }
            };
            bgw.RunWorkerAsync();
        }

        private string GetEntryListText()
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach (Entry e in Program.EntryList.Where(en => !en.IsInvisible))
            {
                sb.AppendFormat("<tr{0}><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td width='50%'>{5}</td></tr>",
                   (count++ % 2 == 0 ? " class='alt'" : ""),
                   count,
                   e.DateTime.ToLongDateString(),
                   e.HebrewDayNight,
                   e.Interval,
                   (string.IsNullOrEmpty(e.Notes) ? "&nbsp;" : e.Notes));
            }
            return Properties.Resources.EntryListHtmlTemplate
                .Replace("{{DATE}}", Program.Today.ToLongDateString())
                .Replace("{{ENTRY_ROWS}}", sb.ToString());
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

        private void PrintCalendarList()
        {
            ShowCalendarTextList(true);
        }

        private void PrintEntryList()
        {
            ShowEntryTextList(true);
        }

        public void RefreshData()
        {
            TestInternet();
            LoadJSONFile();
            DisplayMonth();
        }

        private void SetDateAndDayNight()
        {
            DateTime date = DateTime.Now;
            Zmanim zman = new Zmanim(date, Program.CurrentLocation);
            bool isNight = zman.GetShkia() <= date.TimeOfDay;
            if (isNight)
            {
                date = date.AddDays(1);
            }
            Program.Today = date;
            Program.NowOnah = new Onah(Program.Today, isNight ? DayNight.Night : DayNight.Day);
        }

        private void SetLocation()
        {
            string location = Properties.Settings.Default.LocationName;
            if (string.IsNullOrWhiteSpace(location))
            {
                location = Properties.Settings.Default.LocationName = "Jerusalem"; //Yerushalayim
            }

            Program.CurrentLocation = Locations.GetPlace(location);
        }

        private FrmBrowser ShowCalendarTextList(bool print = false)
        {
            FrmBrowser fb = new FrmBrowser(print)
            {
                Text = "לוח חשבשבון_" + Program.Today.ToString("dd_MMM_yyyy").Replace("'", "").Replace("\"", ""),
                Html = WeekListHtml
            };
            fb.Show();
            return fb;
        }


        private FrmBrowser ShowEntryTextList(bool print = false)
        {
            FrmBrowser fb = new FrmBrowser(print)
            {
                Text = "רשימת חשבשבון_" + Program.Today.ToString("dd_MMM_yyyy").Replace("'", "").Replace("\"", ""),
                Html = GetEntryListText()
            };
            fb.Show();
            return fb;
        }
        #endregion Private Functions

        #region Public Functions

        public void AddNewEntry(Entry newEntry, Form sourceForm = null)
        {
            Program.EntryList.Add(newEntry);
            Entry.SortEntriesAndSetInterval(Program.EntryList);
            FindAndPromptKavuahs();
            CalculateProblemOnahs();
            SaveCurrentFile(sourceForm);
            //In case there were changes to the notes on some entries such as if there was a NoKavuah added
            bindingSourceEntries.DataSource = Program.EntryList.Where(en => !en.IsInvisible);
        }

        public bool AddNewKavuah(Form owner)
        {
            using (FrmAddKavuah f = new FrmAddKavuah())
            {
                f.ShowDialog(owner);
                if (f.DialogResult != DialogResult.Cancel)
                {
                    SaveCurrentFile();
                    RefreshData();
                    return true;
                }
            }
            return false;
        }

        public void AddTaharaEvent(TaharaEvent te, Form sourceForm = null)
        {
            Program.TaharaEventList.Add(te);
            TaharaEvent.SortList(Program.TaharaEventList);
            SaveCurrentFile(sourceForm);
            RefreshData();
        }


        public void RemoveTaharaEvent(TaharaEvent te, Form sourceForm = null)
        {
            Program.TaharaEventList.Remove(te);
            SaveCurrentFile(sourceForm);
            RefreshData();
        }


        public void AfterChangePreferences()
        {
            SetLocation();
            SetDateAndDayNight();
            CalculateProblemOnahs();
            SetCaptionText();
            DisplayMonth();
        }

        public void DeleteEntry(Entry entry)
        {
            if (Program.AskUser("האם אתם בטוחים שברצונכם למחוק השורה של " +
                                                    entry.DateTime.ToString("dd MMMM yyyy"),
                                                  "מחיקת שורה " + entry.DateTime.ToString("dd MMMM yyyy")))
            {
                if (Program.KavuahList.Exists(k => k.SettingEntry == entry ||
                    (k.SettingEntryDate == entry.DateTime && k.DayNight == entry.DayNight)))
                {
                    if (Program.AskUser(" נמצאו וסתי קבוע שהוגדרו על פי רשומה הזאת. האם אתם עדיין בטוחים שברצונכם למחוק השורה של " +
                                                entry.DateTime.ToString("dd MMMM yyyy") +
                                                " וגם כל וסת הקבוע שנרשמו בגללה?",
                                              "מחיקת שורה " + entry.DateTime.ToString("dd MMMM yyyy")))
                    {
                        Program.KavuahList.RemoveAll(k => k.SettingEntry == entry ||
                            (k.SettingEntryDate == entry.DateTime && k.DayNight == entry.DayNight));
                    }
                    else
                    {
                        return;
                    }
                }
                Program.EntryList.Remove(entry);
                Entry.SortEntriesAndSetInterval(Program.EntryList);
                FindAndPromptKavuahs();
                CalculateProblemOnahs();
                SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                bindingSourceEntries.DataSource = Program.EntryList.Where(en => !en.IsInvisible);
                DisplayMonth();
            }
        }

        public string GetTempJSONFile()
        {
            File.WriteAllText(_tempJSONFileName, CurrentFileJson ?? "");
            return _tempJSONFileName;
        }

        public void LoadJSONFile()
        {
            CreateLocalBackup();
            lblNextProblem.Text = "";

            if (File.Exists(CurrentFile))
            {
                try
                {
                    (List<Entry> entryList, List<Kavuah> kavuahList, List<TaharaEvent> taharaEvents) =
                        Program.LoadEntriesKavuahsFromJson(CurrentFileJson);
                    Program.EntryList.Clear();
                    Program.EntryList.AddRange(entryList);
                    Program.KavuahList.Clear();
                    Program.KavuahList.AddRange(kavuahList);
                    Program.TaharaEventList.Clear();
                    Program.TaharaEventList.AddRange(taharaEvents);
                }
                catch (Exception)
                {
                    Program.Exclaim("הקובץ שאמור ליפתח" + Environment.NewLine + "\"" +
                        CurrentFile + "\"" + Environment.NewLine +
                        " איננה קובץ חשבשבון תקינה. תפתח רשימה ריקה.");
                    //Clear previous list data
                    Program.EntryList.Clear();
                    //Clear previous Kavuahs
                    if (Program.KavuahList != null)
                    {
                        Program.KavuahList.Clear();
                    }
                }
            }

            if (File.Exists(CurrentFile) && !Properties.Settings.Default.RecentFiles.Contains(CurrentFile))
            {
                Properties.Settings.Default.RecentFiles.Insert(0, CurrentFile);
                recentFilesToolStripMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(CurrentFile));
                recentFilesToolStripMenuItem.Enabled = clearRecentFilesToolStripMenuItem.Enabled = true;
            }

            SetCaptionText();
            CalculateProblemOnahs();
            bindingSourceEntries.DataSource = Program.EntryList.Where(en => !en.IsInvisible);
        }

        private void CalculateProblemOnahs()
        {
            ProblemOnahCalculator c = new ProblemOnahCalculator
            {
                EntryList = Program.EntryList,
                KavuahList = Program.KavuahList,
                Today = Program.Today,
                NowOnah = Program.NowOnah,
                NumberMonthsAheadToWarn = Properties.Settings.Default.NumberMonthsAheadToWarn,
                DilugChodeshPastEnds = Properties.Settings.Default.DilugChodeshPastEnds,
                KeepLongerHaflaga = Properties.Settings.Default.KeepLongerHaflagah,
                OnahBenIs24Hours = Properties.Settings.Default.OnahBenIs24Hours,
                ShowOhrZeruah = Properties.Settings.Default.ShowOhrZeruah
            };

            Program.ProblemOnahs.Clear();
            Program.ProblemOnahs.AddRange(c.CalculateProblemOnahs());
            //The lblNextProblem displays the next upcoming Onah that needs to be kept
            lblNextProblem.Text = c.GetNextOnahText();
            WeekListHtml = GetWeekListHtml();
        }

        /// <summary>
        /// Gets a list of proposed Kavuahs according to the entries in the Entry list 
        /// and prompts the user to either add them, ignore them for now, or "NoKavuah" them 
        /// </summary>
        /// <returns></returns>
        private bool FindAndPromptKavuahs()
        {
            //The following function does all the hard work - 
            //cheshboning out all the prospective Kavuahs from the current entry list. 
            List<Kavuah> kavuahList = Kavuah.GetProposedKavuahList(Program.EntryList, Program.KavuahList);

            if (kavuahList.Count > 0)
            {
                //Prompt user to decide which ones to keep and edit their details
                using (FrmKavuahPrompt fkp = new FrmKavuahPrompt(kavuahList))
                {
                    if (fkp.ShowDialog() == DialogResult.OK)
                    {
                        //For each found Kavuah, either we add it to the main list 
                        //or we set it as a "NoKavuah" for the third entry so it shouldn't pop up again
                        foreach (Kavuah kv in kavuahList)
                        {
                            //The ListToAdd property contains the ones the user decided to add
                            if (fkp.ListToAdd.Contains(kv))
                            {
                                Program.KavuahList.Add(kv);
                            }
                            else
                            {
                                //The SettingEtry is set when the Kavuah was added to the proposed list
                                kv.SettingEntry.NoKavuahList.Add(kv);
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetWeekListHtml()
        {
            // First we combine double onahs - such as if one onah is also day 30 and also a haflagah.
            // We will only display it once, but with both descriptions.
            // If one of them is to be ignored though, it will get it's own row.
            List<Onah> onahsToAdd = new List<Onah>();
            foreach (Onah onah in Program.ProblemOnahs.Where(on => on.DateTime >= Program.Today || Program.Today.IsSameday(on.DateTime)))
            {
                if (onahsToAdd.Exists(o => Onah.IsSameOnahPeriod(o, onah) && o.IsIgnored == onah.IsIgnored))
                {
                    onahsToAdd.Where(o => Onah.IsSameOnahPeriod(o, onah)).First().Name += ", וגם " + onah.Name;
                }
                else
                {
                    onahsToAdd.Add(onah.Clone());
                }
            }


            StringBuilder sb = new StringBuilder();
            if (onahsToAdd.Exists(o => o.IsIgnored))
            {
                sb.Append("<tr><td class='ignored' colspan='6'>רשומות באפור לא פעילים עקב וסת קבוע</td></tr>");
            }

            foreach (Onah onah in onahsToAdd)
            {
                sb.AppendFormat("<tr class='prob {0}'><td>{1}</td><td>{2} {3}</td><td>{4}</td><td width='50%'>{5}</td></tr>",
                    (Onah.IsSameOnahPeriod(Program.NowOnah, onah) ? " red" : "") + (onah.IsIgnored ? " ignored" : ""),
                    GeneralUtils.GetDayOfWeekText(onah.DateTime),
                    GeneralUtils.DaysOfMonthHebrew[onah.Day],
                    onah.Month.ToString(),
                    onah.HebrewDayNight,
                    onah.Name);
            }

            return Properties.Resources.WeekProblemListHtmlTemplate
                .Replace("{{DATE}}", Program.Today.ToLongDateString())
                .Replace("{{PROBLEM_ROWS}}", sb.ToString())
                .Replace("{{NEXT_PROBLEM_TEXT}}", lblNextProblem.Text);
        }

        /// <summary>
        /// Saves all changes back to the source file.
        /// </summary>
        /// <remarks>
        /// This function is run whenever a change is made to the list and when closing the app.
        /// </remarks>
        public void SaveCurrentFile(Form sourceForm = null)
        {
            //If no file was originally loaded, CurrentFile will be null.
            //In this case, if there are entries in the list
            //we prompt the user to create a file to save to.
            while (string.IsNullOrEmpty(CurrentFile))
            {
                if (((Program.EntryList.Count + Program.KavuahList.Count) > 0) &&
                    Program.AskUser("?שמירת הרשימה מצריך קובץ. האם ליצור קובץ חדש"))
                {
                    SaveAs(sourceForm);
                }
                else
                {
                    return;
                }
            }
            string dir = Path.GetDirectoryName(CurrentFile);

            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            catch (IOException)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                    @"\Chashavshavon Files";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                CurrentFile = CurrentFile.Replace(dir, path);
                Program.Exclaim("חשבשבון היה צריל להעתיק הקובץ הפעילה למיקום אחר.\nהקובץ עכשיו נמצא ב: \n" +
                    CurrentFile);
            }

            FileStream fs;
            if (!File.Exists(CurrentFile))
            {
                fs = File.Create(CurrentFile);
            }
            else
            {
                fs = File.Open(CurrentFile, FileMode.Create);
            }

            using (fs)
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                JsonSerializer jsonSerializer = new JsonSerializer
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented
                };
                jsonSerializer.Serialize(jw, new
                {
                    Entries = Program.EntryList.Select(e => new
                    {
                        When = $"עונת {e.HebrewDayNight} - {e.JewishDate.ToLongDateStringHeb()}",
                        Abs = e.JewishDate.AbsoluteDate,
                        DN = (int)e.DayNight,
                        e.IsInvisible,
                        e.Notes,
                        NoKavuah = e.NoKavuahList.Select(nk => new
                        {
                            KavuahType = nk.KavuahType.ToString(),
                            nk.Number
                        })

                    }),
                    Kavuahs = Program.KavuahList,
                    TaharaEvents = Program.TaharaEventList
                });
            }
            Properties.Settings.Default.CurrentFile = CurrentFile;
            Properties.Settings.Default.Save();
            SetCaptionText();

            if (RemoteFunctions.IsConnectedToInternet() && Properties.Settings.Default.AlwaysUpdateRemote && !string.IsNullOrEmpty(Properties.Settings.Default.RemoteUserName) && !string.IsNullOrEmpty(Properties.Settings.Default.RemotePassword))
            {
                RemoteFunctions.SaveFile(CurrentFile, null, null);
            }
        }

        public void SetCaptionText()
        {
            string fileName = !string.IsNullOrWhiteSpace(CurrentFileName)
                ? " - " + CurrentFileName
                : "";
            Text = " חשבשבון גירסה " +
                Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - " +
                Program.GetCurrentPlaceName() + fileName;

        }

        /// <summary>
        /// Checks to see if the user is connected to the internet, and activates remote functionality if they are.
        /// If the current file is a remote one and they are not connected, they are duly complained to about it...
        /// </summary>
        /// <returns></returns>
        public bool TestInternet()
        {
            bool hasInternet = Program.RunInDevMode || RemoteFunctions.IsConnectedToInternet();
            RemoteToolStripMenuItem.Visible = hasInternet;
            return hasInternet;
        }

        #endregion Public Functions

        #region Properties

        public bool CloseMeFirst { get; set; }

        public string CurrentFile
        {
            get => Properties.Settings.Default.CurrentFile;
            set
            {
                Properties.Settings.Default.CurrentFile = value;
                Properties.Settings.Default.Save();
                SetCaptionText();
            }
        }

        public string CurrentFileName => Path.GetFileName(CurrentFile);

        public string CurrentFileJson
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(CurrentFile))
                {
                    return File.ReadAllText(CurrentFile);
                }
                else
                {
                    return "{}";
                }
            }
        }
        public string WeekListHtml { get; set; }

        #endregion Properties                
    }
}