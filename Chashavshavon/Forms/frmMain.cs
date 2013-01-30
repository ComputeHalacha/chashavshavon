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
using System.Globalization;

namespace Chashavshavon
{
    public partial class frmMain : Form
    {

        #region Private Variables
        private DateTime _monthToDisplay;
        private string _tempXMLFileName = Program.TempFolderPath + @"\ChashavshavonTempFile.xml";
        #endregion

        #region Constructors
        public frmMain()
        {
            this.StartUp();
        }

        public frmMain(string filePath)
        {
            this.CurrentFileIsRemote = false;
            this.CurrentFile = filePath;
            this.StartUp();
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

            //Checks to see if the user is connected to the internet, and activates remote functionality if they are.
            this.TestInternet();
            //Load the last opened file. If it does not exist or this is the first run, a blank list is presented            
            this.LoadXmlFile();
            this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
            this._monthToDisplay = Program.Today;
            this.DisplayMonth();

            foreach (string f in Properties.Settings.Default.RecentFiles)
            {
                recentFilesToolStripMenuItem.DropDownItems.Add(f);
            }
            this.recentFilesToolStripMenuItem.Enabled = this.clearRecentFilesToolStripMenuItem.Enabled = recentFilesToolStripMenuItem.HasDropDownItems;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveCurrentFile();
            if (!Properties.Settings.Default.OpenLastFile)
            {
                Properties.Settings.Default.CurrentFile = null;
                Properties.Settings.Default.IsCurrentFileRemote = false;
            }

            //the temp folder is only deleted if the user manually closed the app.
            //Otherwise we may be in an installer run and do not want to delete the installation files.
            Program.BeforeExit(e.CloseReason == CloseReason.UserClosing);
        }

        private void frmMain_ResizeBegin(object sender, EventArgs e)
        {
            this.luachTableLayout.Visible = false;
            this.luachTableLayout.SuspendLayout();
        }


        private void frmMain_ResizeEnd(object sender, EventArgs e)
        {
            this.luachTableLayout.ResumeLayout();
            this.luachTableLayout.Visible = true;
        }

        void AddNewEntry(object sender, EventArgs e)
        {
            using (frmAddNewEntry f = new frmAddNewEntry((DateTime)((Control)sender).Tag))
            {
                if (f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    //Refresh in case of change to current month
                    this.DisplayMonth();
                }
            }
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            this._monthToDisplay = Program.HebrewCalendar.AddMonths(this._monthToDisplay, 1);
            this.DisplayMonth();
        }

        private void btnLastMonth_Click(object sender, EventArgs e)
        {
            this._monthToDisplay = Program.HebrewCalendar.AddMonths(this._monthToDisplay, -1);
            this.DisplayMonth();
        }

        private void dgEntries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == this.dgEntries.Columns["NotesColumn"].Index)
            {
                DataGridViewRow r = this.dgEntries.Rows[e.RowIndex];
                if (r.DataBoundItem is Entry)
                {
                    Entry entry = (Entry)r.DataBoundItem;
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

        private void dgEntries_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgEntries.Columns[e.ColumnIndex] == btnDeleteColumn &&
                dgEntries.Rows[e.RowIndex].DataBoundItem is Entry)
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

        private void clearRecentFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.RecentFiles.Clear();
            this.recentFilesToolStripMenuItem.DropDownItems.Clear();
            this.recentFilesToolStripMenuItem.Enabled = this.clearRecentFilesToolStripMenuItem.Enabled = false;
        }

        private void recentFilesToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (File.Exists(e.ClickedItem.Text))
            {
                this.SaveCurrentFile();
                CurrentFile = e.ClickedItem.Text;
                CurrentFileIsRemote = false;
                LoadXmlFile();
                this.CalculateProblemOnahs();
                DisplayMonth();
            }
            else
            {
                this.recentFilesToolStripMenuItem.HideDropDown(); // was blocking message box
                if (MessageBox.Show("הקובץ \"" + e.ClickedItem.Text +
                        "\" לא נמצא.\nלהסירה מרשימת קבצים אחרונים?",
                     "חשבשבון",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == System.Windows.Forms.DialogResult.Yes)
                {
                    Properties.Settings.Default.RecentFiles.Remove(e.ClickedItem.Text);
                    recentFilesToolStripMenuItem.DropDownItems.Remove(e.ClickedItem);
                    this.recentFilesToolStripMenuItem.Enabled = this.clearRecentFilesToolStripMenuItem.Enabled = recentFilesToolStripMenuItem.HasDropDownItems;
                }
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile(); //why not...            
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(this.openFileDialog1.FileName);
                    if (doc.SelectNodes("//Kavuah").Count > 0)
                    {
                        try
                        {
                            var ser = new XmlSerializer(typeof(List<Kavuah>));
                            var list = (List<Kavuah>)ser.Deserialize(
                                new StringReader(doc.SelectSingleNode("//ArrayOfKavuah").OuterXml));

                            Kavuah.KavuahsList.AddRange(list);

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
                                if (!Entry.EntryList.Exists(en =>
                                    en.DateTime == kav.SettingEntryDate &&
                                    en.DayNight == kav.DayNight &&
                                    en.Interval == kav.SettingEntryInterval))
                                {
                                    Entry.EntryList.Add(new Entry(
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
                            Kavuah.ClearDoubleKavuahs(Kavuah.KavuahsList);

                            this.CalculateProblemOnahs();
                            this.DisplayMonth();
                            this.ShowKavuahList();
                        }
                        catch
                        {
                            MessageBox.Show("רשימת וסת קבוע בקובץ\"" +
                                Path.GetFileName(this.openFileDialog1.FileName) + "\" .איננה תקינה",
                                "חשבשבון",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        }
                    }
                    else
                    {
                        MessageBox.Show("רשימת וסת קבוע בקובץ\"" +
                            Path.GetFileName(this.openFileDialog1.FileName) + "\" ריקה.",
                            "חשבשבון",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AbouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutBox ab = new AboutBox())
            {
                ab.ShowDialog(this);
            }
        }


        private void OpenBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Program.BackupFolderPath);
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            frmAddNewEntry f = new frmAddNewEntry(Program.Today);
            if (f.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                //Refresh in case of change to current month
                this.DisplayMonth();
            }
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

        private void btnOpenKavuahs_Click(object sender, EventArgs e)
        {
            this.ShowKavuahList();
        }

        private void btnPrefs_Click(object sender, EventArgs e)
        {
            frmPreferences prefs = new frmPreferences();
            prefs.Show(this);
        }

        private void btnCheshbonKavuahs_Click(object sender, EventArgs e)
        {
            if (Kavuah.FindAndPromptKavuahs())
            {
                //Save the new Kavuahs to the file
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
            }
            else
            {
                MessageBox.Show("לא נמצאו וסת קבוע אפשריים");
            }
        }

        private void btnAddKavuah_Click(object sender, EventArgs e)
        {
            this.AddNewKavuah(this);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveCurrentFile();
            openFileDialog1.ShowDialog(this);
            openFileDialog1.CheckFileExists = true;
            CurrentFile = openFileDialog1.FileName;
            CurrentFileIsRemote = false;
            LoadXmlFile();
            this.CalculateProblemOnahs();
            DisplayMonth();
        }

        private void PreferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPreferences prefs = new frmPreferences();
            prefs.Show(this);
        }

        private void openKavuaListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowKavuahList();
        }

        private void AddKavuahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AddNewKavuah(this);
        }

        private void SearchForKavuahsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Kavuah.FindAndPromptKavuahs())
            {
                //Save the new Kavuahs to the file
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
            }
            else
            {
                MessageBox.Show("לא נמצאו וסת קבוע אפשריים");
            }
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshData();
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

        private void SourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.CurrentFile))
            {
                SaveCurrentFile();
                this.GetTempXmlFile();
                System.Diagnostics.Process.Start(this._tempXMLFileName);
            }
            else
            {
                MessageBox.Show(".הצגת מקור מצריך קובץ",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void SourceTextMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.CurrentFile) && File.Exists(CurrentFile))
            {
                SaveCurrentFile();
                var notepad = new System.Diagnostics.Process();
                notepad.StartInfo.FileName = "notepad.exe";
                notepad.StartInfo.Arguments = this.CurrentFile;
                notepad.EnableRaisingEvents = true;
                notepad.Exited += delegate { this.RefreshData(); };
                notepad.Start();
                notepad.WaitForExit();
                notepad.Dispose();
            }
            else
            {
                MessageBox.Show(".הצגת מקור מצריך קובץ",
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
            }
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            this._monthToDisplay = Program.Today;
            this.DisplayMonth();
        }
        #endregion

        #region Private Functions
        private void StartUp()
        {
            //In case we will display the password entry form, we hide this until form load
            this.Hide();
            string password = this.GetPassword();

            InitializeComponent();

            //The following sets all output displays of date time functions to Jewish dates for the current thread
            Program.CultureInfo.DateTimeFormat.Calendar = Program.HebrewCalendar;
            System.Threading.Thread.CurrentThread.CurrentCulture = Program.CultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = Program.CultureInfo;

            Zmanim.SetSummerTime();
            this.SetLocation();
            this.SetDateAndDayNight();

            if (password == null)
            {
                this.Show();
            }
            else
            {
                //Prompt for a password and don't stop prompting until the user gets it right or gives up
                using (frmEnterPassword f = new frmEnterPassword(password))
                {
                    do
                    {
                        f.ShowDialog(this);
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
        }

        private void ShowKavuahList()
        {
            using (frmKavuahs f = new frmKavuahs())
            {
                if (f.ShowDialog(this) != System.Windows.Forms.DialogResult.Cancel)
                {
                    this.SaveCurrentFile();
                    this.TestInternet();
                    this.LoadXmlFile();
                }

                //In case some Kavuahs were added or deleted...
                this.CalculateProblemOnahs();
                this.DisplayMonth();
            }
        }

        private void SaveAs(Form sourceForm = null)
        {
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.DefaultExt = "pm";
            openFileDialog1.FileName = Program.Today.ToString("ddMMMyyyy").Replace("\"", "").Replace("'", "") + ".pm";
            if (openFileDialog1.ShowDialog(sourceForm ?? this) != DialogResult.OK)
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

            if (saveFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                CurrentFile = saveFileDialog1.FileName;
                CurrentFileIsRemote = false;
                LoadXmlFile();
            }
        }

        #region Calculate Problem Onahs
        private void CalculateProblemOnahs()
        {
            //Clears the list and gets it ready to accept new problems
            if (this.ProblemOnas == null)
            {
                this.ProblemOnas = new List<Onah>();
            }
            this.ProblemOnas.Clear();

            //A list of Onahs that need to be kept. This first list is worked out from the list of Entries.
            //Problem Onahs are searched for from the date of each entry until the number of months specified in the 
            //Property Setting "numberMonthsAheadToWarn"
            this.SetEntryListDependentProblemOnahs();

            //Get the onahs that need to be kept for Kavuahs of yom hachodesh, sirug, 
            //dilug (from projected day - not actual entry)
            //and other Kavuahs that are not dependent on the actual entry list
            this.SetIndependentKavuahProblemOnahs();

            //Clean out doubles
            //TODO:Figure out why more than just doubles are getting deleted from the following
            //Onah.ClearDoubleOnahs(this.ProblemOnas);

            //The lblNextProblem displays the next upcoming Onah that needs to be kept
            this.lblNextProblem.Text = GetNextOnahText();
            this.SetWeekListHtml();
        }

        private void SetEntryListDependentProblemOnahs()
        {
            foreach (Entry entry in Entry.EntryList.Where(en => !en.IsInvisible))
            {
                this.SetOnahBeinenisProblemOnahs(entry);
                this.SetEntryDependentKavuahProblemOnahs(entry);
            }
        }

        private void SetOnahBeinenisProblemOnahs(Entry entry)
        {
            bool cancelOnahBeinenis = Kavuah.KavuahsList.Exists(k => k.Active && k.CancelsOnahBeinanis);

            //Day Thirty
            Onah thirty = entry.AddDays(29);
            thirty.Name = "יום שלושים";
            thirty.IsIgnored = cancelOnahBeinenis;
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
                Onah thirtyOhrZarua = Onah.GetPreviousOnah(thirty);
                thirtyOhrZarua.Name = "או\"ז של יום שלושים";
                thirtyOhrZarua.IsIgnored = cancelOnahBeinenis;
                this.ProblemOnas.Add(thirtyOhrZarua);
            }

            //Day Thirty One
            Onah thirtyOne = entry.AddDays(30);
            thirtyOne.Name = "יום ל\"א";
            thirtyOne.IsIgnored = cancelOnahBeinenis;
            this.ProblemOnas.Add(thirtyOne);

            if (Properties.Settings.Default.OnahBenIs24Hours)
            {
                Onah o = thirtyOne.Clone();
                o.DayNight = o.DayNight == DayNight.Day ? DayNight.Night : DayNight.Day;
                this.ProblemOnas.Add(o);
            }

            if (Properties.Settings.Default.ShowOhrZeruah)
            {
                Onah thirtyOneOhrZarua = Onah.GetPreviousOnah(thirtyOne);
                thirtyOneOhrZarua.Name = "או\"ז של יום ל\"א";
                thirtyOneOhrZarua.IsIgnored = cancelOnahBeinenis;
                this.ProblemOnas.Add(thirtyOneOhrZarua);
            }

            //Haflagah
            if (entry.Interval > 1)
            {
                if (!Properties.Settings.Default.KeepLongerHaflagah)
                {
                    Onah intervalHaflagah = entry.AddDays(entry.Interval - 1);
                    intervalHaflagah.Name = "יום הפלגה (" + entry.Interval + ")";
                    intervalHaflagah.IsIgnored = cancelOnahBeinenis;
                    this.ProblemOnas.Add(intervalHaflagah);

                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        Onah intervalHaflagahOhrZarua = Onah.GetPreviousOnah(intervalHaflagah);
                        intervalHaflagahOhrZarua.Name = "או\"ז של " + intervalHaflagah.Name;
                        intervalHaflagahOhrZarua.IsIgnored = cancelOnahBeinenis;
                        this.ProblemOnas.Add(intervalHaflagahOhrZarua);
                    }
                }
                else
                {
                    //First we look for a proceeding entry where the haflagah is longer than this one
                    DateTime longerHaflagah = (from e in Entry.EntryList
                                               where e.DateTime > entry.DateTime &&
                                                     e.Interval > entry.Interval
                                               select e.DateTime).FirstOrDefault();

                    //If no such entry was found, we keep on going...
                    if (longerHaflagah == DateTime.MinValue)
                    {
                        longerHaflagah = Program.HebrewCalendar.AddMonths(Program.Today,
                            Properties.Settings.Default.NumberMonthsAheadToWarn);
                    }

                    //TODO:How to cheshbon out the Shach (or rather not like the Shach).
                    //Is the question from the actual next entry or from the theoretical next entry, or both?

                    //First the theoretical problems - not based on real entries
                    //We get the first problem Onah
                    Onah on = entry.AddDays(entry.Interval - 1);
                    //We don't flag the "שלא נתבטלה" for the first one, so we keep track
                    bool isFirst = true;
                    while (on.DateTime < longerHaflagah)
                    {
                        on.Name = "יום הפלגה (" + entry.Interval + ")";
                        if (!isFirst)
                        {
                            on.Name += " שלא נתבטלה";
                        }
                        on.IsIgnored = cancelOnahBeinenis;
                        this.ProblemOnas.Add(on);
                        if (Properties.Settings.Default.ShowOhrZeruah)
                        {
                            Onah ooz = Onah.GetPreviousOnah(on);
                            ooz.Name = "או\"ז של " + on.Name;
                            ooz.IsIgnored = cancelOnahBeinenis;
                            this.ProblemOnas.Add(ooz);
                        }
                        isFirst = false;
                        on = on.AddDays(entry.Interval - 1);
                    }

                    //Now for the non-overided haflagah from the actual entries
                    foreach (Entry en in Entry.EntryList.Where(e =>
                        e.DateTime > entry.DateTime && e.DateTime < longerHaflagah))
                    {
                        on = en.AddDays(entry.Interval - 1);
                        on.Name = "יום הפלגה (" + entry.Interval + ") שלא נתבטלה";
                        on.IsIgnored = cancelOnahBeinenis;
                        this.ProblemOnas.Add(on);
                        if (Properties.Settings.Default.ShowOhrZeruah)
                        {
                            Onah ooz = Onah.GetPreviousOnah(on);
                            ooz.Name = "או\"ז של " + on.Name;
                            ooz.IsIgnored = cancelOnahBeinenis;
                            this.ProblemOnas.Add(ooz);
                        }
                    }
                }
            }
        }

        private void SetEntryDependentKavuahProblemOnahs(Entry entry)
        {
            //Kavuah Haflagah - with or without Maayan Pasuach
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                k.KavuahType.In(KavuahType.Haflagah, KavuahType.HaflagaMaayanPasuach) && k.Active))
            {
                Onah kavuahHaflaga = entry.AddDays(kavuah.Number - 1);
                kavuahHaflaga.DayNight = kavuah.DayNight;
                kavuahHaflaga.Name = "קבוע " + kavuah.ToString();
                this.ProblemOnas.Add(kavuahHaflaga);

                if (Properties.Settings.Default.ShowOhrZeruah)
                {
                    Onah kavuahHaflagaOhrZarua = Onah.GetPreviousOnah(kavuahHaflaga);
                    kavuahHaflagaOhrZarua.Name = " או\"ז של " + kavuahHaflaga.Name;
                    this.ProblemOnas.Add(kavuahHaflagaOhrZarua);
                }
            }

            if (Properties.Settings.Default.CheshbonKavuahByActualEntry)
            {
                //Kavvuah Dilug Haflagos - from actual entry not from what was supposed to be. We cheshbon both.
                //The theoretical ones, are worked out in the function "GetIndependentKavuahOnahs"
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k => k.KavuahType == KavuahType.DilugHaflaga && k.Active))
                {
                    Onah kavuahDilugHaflaga = entry.AddDays(entry.Interval + kavuah.Number - 1);
                    kavuahDilugHaflaga.DayNight = kavuah.DayNight;
                    kavuahDilugHaflaga.Name = "קבוע " + kavuah.ToString() + " ע\"פ ראייה";
                    this.ProblemOnas.Add(kavuahDilugHaflaga);

                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        Onah kavuahDilugHaflagaOhrZarua = Onah.GetPreviousOnah(kavuahDilugHaflaga);
                        kavuahDilugHaflagaOhrZarua.Name = " או\"ז של " + kavuahDilugHaflaga.Name;
                        this.ProblemOnas.Add(kavuahDilugHaflagaOhrZarua);
                    }
                }

                //TODO: Does Dilug of Yom Hachodesh continue from an actual entry even if it is off cheshbon? For ex. 35, 34, 33, 36 - is the next "35" or just continues theoretically 32, 31....
                //Kavvuah Dilug Yom Hachodesh - even if one was off, only works out from entry not from what was supposed to be.
                //We cheshbon both.
                //The theoretical ones, are worked out in the function "GetIndependentKavuahOnahs"
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k => k.KavuahType == KavuahType.DilugDayOfMonth && k.Active))
                {
                    DateTime next = Program.HebrewCalendar.AddMonths(entry.DateTime, 1);
                    next = next.AddDays(kavuah.Number);
                    Onah kavuahDilugDayofMonth = new Onah(next, kavuah.DayNight);
                    kavuahDilugDayofMonth.Name = "קבוע " + kavuah.ToString();
                    this.ProblemOnas.Add(kavuahDilugDayofMonth);

                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        Onah kavuahDilugDayofMonthOhrZarua = Onah.GetPreviousOnah(kavuahDilugDayofMonth);
                        kavuahDilugDayofMonthOhrZarua.Name = " או\"ז של " + kavuahDilugDayofMonth.Name;
                        this.ProblemOnas.Add(kavuahDilugDayofMonthOhrZarua);
                    }
                }
            }
        }

        /// <summary>
        /// Work out the Kavuahs of yom hachodesh, sirug, dilug haflagos (from projected day - not actual entry)
        /// and other Kavuahs that are not dependant on the entry list
        /// </summary>
        /// <param name="onahs"></param>
        /// <returns></returns>
        private void SetIndependentKavuahProblemOnahs()
        {
            //Kavuahs of Yom Hachodesh and Sirug
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                                        k.Active &&
                                        k.SettingEntryDate > DateTime.MinValue &&
                                        k.KavuahType.In(KavuahType.DayOfMonth,
                                                        KavuahType.DayOfMonthMaayanPasuach,
                                                        KavuahType.Sirug)))
            {
                for (DateTime dt = Program.HebrewCalendar.AddMonths(kavuah.SettingEntryDate,
                        kavuah.KavuahType == KavuahType.Sirug ? kavuah.Number : 1);
                    dt <= Program.HebrewCalendar.AddMonths(Program.Today, Properties.Settings.Default.NumberMonthsAheadToWarn);
                    dt = Program.HebrewCalendar.AddMonths(dt, (kavuah.KavuahType == KavuahType.Sirug ? kavuah.Number : 1)))
                {
                    Onah o = new Onah(dt, kavuah.DayNight)
                    {
                        Name = kavuah.ToString(),
                        Day = Program.HebrewCalendar.GetDayOfMonth(kavuah.SettingEntryDate)
                    };
                    this.ProblemOnas.Add(o);
                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        var ooz = Onah.GetPreviousOnah(o);
                        ooz.Name = " או\"ז של " + o.Name;
                        this.ProblemOnas.Add(ooz);
                    }
                }
            }

            //Kavuahs of "Day of week" - cheshboned from the theoretical Entries
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                k.KavuahType == KavuahType.DayOfWeek && k.Active))
            {
                for (DateTime dt = kavuah.SettingEntryDate.AddDays(kavuah.Number);
                    dt <= Program.HebrewCalendar.AddMonths(Program.Today, Properties.Settings.Default.NumberMonthsAheadToWarn);
                    dt = dt.AddDays(kavuah.Number))
                {
                    Onah o = new Onah(dt, kavuah.DayNight)
                    {
                        Name = "קבוע " + kavuah.ToString(),
                    };
                    this.ProblemOnas.Add(o);
                    if (Properties.Settings.Default.ShowOhrZeruah)
                    {
                        var ooz = Onah.GetPreviousOnah(o);
                        ooz.Name = " או\"ז של " + o.Name;
                        this.ProblemOnas.Add(ooz);
                    }
                }
            }

            if (Properties.Settings.Default.CheshbonKavuahByCheshbon)
            {
                //Kavuahs of Yom Hachodesh of Dilug - cheshboned from the theoretical Entries
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                                            k.Active &&
                                            k.KavuahType == KavuahType.DilugDayOfMonth))
                {
                    DateTime dt = kavuah.SettingEntryDate;
                    for (int i = 0; i >= 0; i++)
                    {
                        dt = Program.HebrewCalendar.AddMonths(dt, 1);
                        DateTime dtNext = dt.AddDays(kavuah.Number * i);
                        //We stop when we get to the beginning or end of the month
                        if (dtNext.Month != dt.Month ||
                            dtNext > Program.HebrewCalendar.AddMonths(Program.Today, Properties.Settings.Default.NumberMonthsAheadToWarn))
                        {
                            break;
                        }

                        Onah o = new Onah(dtNext, kavuah.DayNight)
                        {
                            Name = "קבוע " + kavuah.ToString()
                        };
                        this.ProblemOnas.Add(o);
                        if (Properties.Settings.Default.ShowOhrZeruah)
                        {
                            var ooz = Onah.GetPreviousOnah(o);
                            ooz.Name = " או\"ז של " + o.Name;
                            this.ProblemOnas.Add(ooz);
                        }
                    }
                }

                //Kavuahs of Yom Haflaga of Dilug - cheshboned from the theoretical Entries
                foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                                            k.Active &&
                                            k.KavuahType == KavuahType.DilugHaflaga))
                {
                    DateTime dt = kavuah.SettingEntryDate;
                    for (int i = 1; ; i++)
                    {
                        //For negative dilugim, we stop when we get to 0
                        if (((kavuah.SettingEntryInterval) + (kavuah.Number * i)) < 1)
                        {
                            break;
                        }
                        dt = dt.AddDays(((kavuah.SettingEntryInterval + (kavuah.Number * i))) + (-1));
                        if (dt > Program.HebrewCalendar.AddMonths(Program.Today, Properties.Settings.Default.NumberMonthsAheadToWarn))
                        {
                            break;
                        }

                        Onah o = new Onah(dt, kavuah.DayNight)
                        {
                            Name = "קבוע " + kavuah.ToString() + " ע\"פ חשבון",
                            //Negative kavuahs of haflagah are just a chumra
                            IsChumrah = kavuah.Number < 1
                        };
                        this.ProblemOnas.Add(o);
                        if (Properties.Settings.Default.ShowOhrZeruah)
                        {
                            var ooz = Onah.GetPreviousOnah(o);
                            ooz.Name = " או\"ז של " + o.Name;
                            this.ProblemOnas.Add(ooz);
                        }
                    }
                }
            }
        }

        private string GetNextOnahText()
        {
            string nextProblemText = "";
            if (this.ProblemOnas.Count > 0)
            {
                //We need to determine the earliest problem Onah, so we need to do a 
                //special sort on the list where the night Onah is before the day one for the same date.
                this.ProblemOnas.Sort(Onah.CompareOnahs);

                Onah nowProblem = this.ProblemOnas.FirstOrDefault(o => (!o.IsIgnored) && Onah.IsSameOnahPeriod(o, Program.NowOnah));
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

        private void SetWeekListHtml()
        {
            // First we combine double onahs - such as if one onah is also day 30 and also a haflagah.
            // We will only display it once, but with both descriptions.
            // If one of them is to be ignored though, it will get it's own row.
            var onahsToAdd = new List<Onah>();
            foreach (Onah onah in this.ProblemOnas.Where(on => on.DateTime >= Program.Today || Program.Today.IsSameday(on.DateTime)))
            {
                if (onahsToAdd.Exists(o => Onah.IsSameOnahPeriod(o, onah) && o.IsIgnored == onah.IsIgnored))
                {
                    onahsToAdd.Where(o => Onah.IsSameOnahPeriod(o, onah)).First().Name += ", וגם " + onah.Name;
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
                    (count++ % 2 == 0 ? "alt" : "") + (Onah.IsSameOnahPeriod(Program.NowOnah, onah) ? " red" : "") + (onah.IsIgnored ? " ignored" : ""),
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

        private void SetDateAndDayNight()
        {
            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkiah = tc.GetSunset(DateTime.Now, Program.CurrentPlace);
            AstronomicalTime netz = tc.GetSunrise(DateTime.Now, Program.CurrentPlace);
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
            Program.NowOnah = new Onah(Program.Today, isNightTime ? DayNight.Night : DayNight.Day);
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

            lblDate.BackColor = Color.SlateGray;
            lblDate.ForeColor = Color.Lavender;
            if (lbl.Name.Contains("Today") && Program.NowOnah.DayNight == problemOnah.DayNight)
            {
                lbl.BackColor = Color.Red;
                lbl.ForeColor = Color.Lavender;
            }
            else
            {
                lbl.BackColor = Color.Lavender;
            }
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
            foreach (Entry entry in Entry.EntryList.Where(en => !en.IsInvisible))
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

        private void CreateLocalBackup()
        {
            var bgw = new System.ComponentModel.BackgroundWorker();
            bgw.DoWork += delegate
            {
                if (File.Exists(this.CurrentFile))
                {
                    string path = Program.BackupFolderPath + "\\" +
                        Path.GetFileNameWithoutExtension(this.CurrentFile) +
                        "_" +
                        DateTime.Now.ToString("d-MMM-yy_HH-mm-ss", CultureInfo.GetCultureInfo("en-us").DateTimeFormat) +
                        ".pm";
                    if (File.Exists(path))
                    {
                        path = path.Replace(".pm", "_Version_1" + DateTime.Now.Millisecond.ToString() + ".pm");
                    }
                    File.Copy(this.CurrentFile, path);                    
                }
            };
            bgw.RunWorkerAsync();
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
            foreach (Entry e in Entry.EntryList.Where(en => !en.IsInvisible))
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

        private string GetToolTipForDate(DateTime dateTime)
        {
            StringBuilder toolTip = new StringBuilder();
            foreach (string holiday in JewishHolidays.GetHebrewHolidays(dateTime, Program.CurrentPlace.IsInIsrael))
            {
                toolTip.AppendLine(holiday);
            }

            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkiah = tc.GetSunset(dateTime, Program.CurrentPlace);
            AstronomicalTime netz = tc.GetSunrise(dateTime, Program.CurrentPlace);

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

        private void ShowDayDetails(DateTime dateTime)
        {
            using (frmAddNewEntry f = new frmAddNewEntry(dateTime))
            {
                f.ShowDialog(this);
            }
        }

        private void SetLocation()
        {
            int locId = Properties.Settings.Default.UserPlaceId;
            if (locId <= 0)
            {
                locId = Properties.Settings.Default.UserPlaceId = 151; //Yerushalayim
            }

            Program.CurrentPlace = Utils.Place.GetPlace(locId);
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

        private void RefreshData()
        {
            this.TestInternet();
            this.LoadXmlFile();
            this.DisplayMonth();
        }

        private void DisplayMonth()
        {
            int year = Program.HebrewCalendar.GetYear(this._monthToDisplay);
            MonthObject month = new MonthObject(year, Program.HebrewCalendar.GetMonth(this._monthToDisplay));
            DateTime firstDayOfMonth = this._monthToDisplay.AddDays(1 - Program.HebrewCalendar.GetDayOfMonth(this._monthToDisplay));
            int firstDayOfWeek = 1 + (int)firstDayOfMonth.DayOfWeek;
            int currentRow = 1, currentColumn = firstDayOfWeek - 1;
            var sysCulture = CultureInfo.InstalledUICulture.DateTimeFormat;

            this.luachTableLayout.Visible = false;
            this.luachTableLayout.SuspendLayout();

            foreach (Control c in this.luachTableLayout.Controls)
            {
                c.Dispose();
            }
            this.luachTableLayout.Controls.Clear();

            this.lblMonthName.Text = this._monthToDisplay.ToString("MMM yyyy", Program.CultureInfo);
            this.btnLastMonth.Text = "  " + Program.HebrewCalendar.AddMonths(this._monthToDisplay, -1).ToString("MMM") + "  ";
            this.btnNextMonth.Text = "  " + Program.HebrewCalendar.AddMonths(this._monthToDisplay, 1).ToString("MMM") + "  ";

            if (((int)firstDayOfMonth.DayOfWeek >= 6) &&
                   Program.HebrewCalendar.GetDaysInMonth(year, month.MonthInYear)
                > 29)
            {
                this.luachTableLayout.RowCount = 7;
                this.luachTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66667F));
                foreach (RowStyle rs in this.luachTableLayout.RowStyles)
                {
                    if (rs.SizeType == SizeType.Percent)
                    {
                        rs.Height = 16.66667F;
                    }
                }
            }
            else
            {
                this.luachTableLayout.RowCount = 6;
            }

            for (int i = 0; i < 7; i++)
            {
                Panel dow = new Panel()
                {
                    BackgroundImage = Properties.Resources.DarkBlueMarbleBar,
                    BackgroundImageLayout = ImageLayout.Tile,
                    Margin = new Padding(0),
                    Height = 20,
                    Padding = new Padding(0),
                    Dock = DockStyle.Fill
                };
                dow.Controls.Add(new Label()
                {
                    Text = Zmanim.DaysOfWeekHebrewFull[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.White,
                    BackColor = Color.Transparent,
                    Font = new Font(this.luachTableLayout.Font.FontFamily, 12f)
                });
                this.luachTableLayout.Controls.Add(dow, i, 0);
            }

            for (int i = 1; i < month.DaysInMonth + 1; i++)
            {
                DateTime date = new DateTime(Program.HebrewCalendar.GetYear(this._monthToDisplay),
                    Program.HebrewCalendar.GetMonth(this._monthToDisplay),
                    i,
                    Program.HebrewCalendar);
                
                Panel pnl = new Panel()
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0),
                    BackColor = Color.Transparent,
                    BackgroundImage = Properties.Resources.WhiteMarble,
                    BackgroundImageLayout = ImageLayout.Stretch                    
                };

                if (Program.Today.IsSameday(date))
                {
                    pnl.Padding = new Padding(2);
                    Panel border = new Panel()
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0),
                        Padding = new Padding(3),
                        BackColor = Color.LightSlateGray
                    };
                    border.Controls.Add(pnl);
                    this.luachTableLayout.Controls.Add(border, currentColumn, currentRow);
                }
                else
                {
                    this.luachTableLayout.Controls.Add(pnl, currentColumn, currentRow);
                }
                
                var itlp = new TableLayoutPanel()
                { 
                    ColumnCount = 2,
                    RowCount = 1,                    
                    Height=40,                    
                    Dock = DockStyle.Top,                    
                    Margin=new Padding(0),
                    Padding = new Padding(0)
                };
                                
                itlp.Controls.Add(new Label()
                {                    
                    Dock = DockStyle.Fill,
                    AutoSize = true,                    
                    Font = new Font("Verdana", 18f, FontStyle.Bold),
                    ForeColor = Color.Maroon,
                    Text = Zmanim.DaysOfMonthHebrew[i].Replace("\"", "").Replace("\'", ""),                    
                });

                itlp.Controls.Add(new Label()
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,                    
                    TextAlign=ContentAlignment.TopRight,
                    Font = new Font("Verdana", 8f, FontStyle.Bold),
                    ForeColor = Color.DarkSlateBlue,
                    Text = date.ToString("%d", sysCulture)                    
                });

                pnl.Controls.Add(itlp);
                
                string daySpecialText = "";
                foreach (string holiday in JewishHolidays.GetHebrewHolidays(date, Program.CurrentPlace.IsInIsrael))
                {
                    daySpecialText += holiday + " ";
                }
                if (daySpecialText.Length > 0)
                {
                    pnl.BackgroundImage = Properties.Resources.BlueMarble;
                    pnl.BackgroundImageLayout = ImageLayout.Stretch;
                    pnl.Controls.Add(new Label()
                    {
                        Dock = DockStyle.Bottom,
                        Padding = new Padding(0, 7, 0, 2),
                        Text = daySpecialText,
                        Font = new Font(Font.FontFamily, 7f),
                        ForeColor = Color.DarkGreen,
                        TextAlign = ContentAlignment.MiddleCenter,
                        RightToLeft = System.Windows.Forms.RightToLeft.Yes
                    });
                }

                string onahText = "";

                if (this.ProblemOnas != null)
                {
                    var pOnahs = this.ProblemOnas.Where(o => o.DateTime == date);
                    if (pOnahs.Count() > 0)
                    {
                        foreach (var o in pOnahs)
                        {
                            onahText += "\t♦  " + (o.IsIgnored ? "[" : "") + o.HebrewDayNight + ": " +
                                o.Name + (o.IsIgnored ? "]" : "") + Environment.NewLine;
                        }                        
                    }
                }

                Entry entry = Entry.EntryList.FirstOrDefault(en =>
                    !en.IsInvisible &&
                    en.DateTime == date);
                if (entry != null)
                {
                    pnl.BackgroundImage = Properties.Resources.PinkMarbleTile;
                    pnl.BackgroundImageLayout = ImageLayout.Stretch;
                    pnl.BackColor = Color.Transparent;
                    pnl.Controls.Add(new Label()
                    {
                        Dock = DockStyle.Bottom,
                        Text = "ראיה - עונת " + entry.HebrewDayNight + Environment.NewLine + " הפלגה: " + entry.Interval.ToString(),
                        ForeColor = Color.Red,
                        Font = new Font(Font.FontFamily, 7f),
                        TextAlign = ContentAlignment.TopCenter,
                        RightToLeft = System.Windows.Forms.RightToLeft.Yes
                    });
                }
                else if (!string.IsNullOrEmpty(onahText))
                {
                    pnl.BackgroundImage = Properties.Resources.ParchmentMarbleTile;
                    pnl.BackgroundImageLayout = ImageLayout.Stretch;
                    pnl.BackColor = Color.Transparent;
                    pnl.Controls.Add(new Label()
                    {
                        Dock = DockStyle.Bottom,
                        Text = (onahText.Length >= 18 ? 
                            onahText.Substring(0, 17) : onahText).PadRight(20, '.'),
                        TextAlign = ContentAlignment.TopCenter,
                        Font = new Font(Font.FontFamily, 7f),
                        ForeColor = Color.Black,
                        RightToLeft = System.Windows.Forms.RightToLeft.Yes
                    });                    
                }
                else if (currentColumn == luachTableLayout.ColumnCount - 1)
                {
                    pnl.BackgroundImage = Properties.Resources.BlueMarble;
                    pnl.BackgroundImageLayout = ImageLayout.Stretch;
                }

                string toolTipText = date.ToLongDateString() + 
                    Environment.NewLine + 
                    date.ToString("D", sysCulture) + 
                    (!string.IsNullOrWhiteSpace(onahText) ? 
                        Environment.NewLine + "--------------------------------" + Environment.NewLine + onahText : "");

                //Sets the tag, tooltip and click function for all controls of the day
                foreach (Control cntrl in Utils.GeneralUtils.GetAllControls(this.luachTableLayout.GetControlFromPosition(currentColumn, currentRow)))
                {
                    cntrl.Tag = date;
                    cntrl.Click += new EventHandler(AddNewEntry);
                    this.toolTip1.SetToolTip(cntrl, toolTipText);                   
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
            this.luachTableLayout.ResumeLayout();
            this.luachTableLayout.Visible = true;
        }

        #endregion
        #endregion

        #region Public Functions
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
            while (string.IsNullOrEmpty(this.CurrentFile))
            {
                if (((Entry.EntryList.Count + Kavuah.KavuahsList.Count) > 0) &&
                    MessageBox.Show("?שמירת הרשימה מצריך קובץ. האם ליצור קובץ חדש",
                        "חשבשבון",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.SaveAs(sourceForm);
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
                xtw.WriteElementString("IsInvisible", entry.IsInvisible.ToString());
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

            var ser = new XmlSerializer(typeof(List<Kavuah>));
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

        public void AddNewEntry(Entry newEntry, Form sourceForm = null)
        {
            Entry.EntryList.Add(newEntry);
            this.SortEntriesAndSetInterval();
            Kavuah.FindAndPromptKavuahs();
            this.CalculateProblemOnahs();
            this.SaveCurrentFile(sourceForm);
            //In case there were changes to the notes on some entries such as if there was a NoKavuah added
            this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
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
                Entry.EntryList.Remove(entry);
                this.SortEntriesAndSetInterval();
                Kavuah.FindAndPromptKavuahs();
                this.CalculateProblemOnahs();
                this.SaveCurrentFile();
                //In case there were changes to the notes on some entries such as if there was a NoKavuah added
                this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
                this.DisplayMonth();
            }
        }

        public bool AddNewKavuah(Form owner)
        {
            using (frmAddKavuah f = new frmAddKavuah())
            {
                f.ShowDialog(owner);
                if (f.DialogResult != System.Windows.Forms.DialogResult.Cancel)
                {
                    this.SaveCurrentFile();
                    this.RefreshData();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks to see if the user is connected to the internet, and activates remote functionality if they are.
        /// If the current file is a remote one and they are not connected, they are duly complained to about it...
        /// </summary>
        /// <returns></returns>
        public bool TestInternet()
        {
            bool hasInternet = Properties.Settings.Default.DevMode || Utils.RemoteFunctions.IsConnectedToInternet();
            RemoteToolStripMenuItem.Visible = hasInternet;
            if (CurrentFileIsRemote && !hasInternet)
            {
                MessageBox.Show("הקובץ שאמור ליפתח" + Environment.NewLine + "\"" +
                    CurrentFile + "\"" + Environment.NewLine + "הוא קובץ רשת, אבל אין גישה לרשת כרגע" + Environment.NewLine + ".לכן תפתח קובץ ריק",
                    "חשבשבון",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                CurrentFileIsRemote = false;
                CurrentFile = Program.Today.ToString("ddMMMyyyy_hhmm").Replace("\"", "").Replace("'", "") + ".pm";
            }
            return hasInternet;
        }

        public void LoadXmlFile()
        {
            this.CreateLocalBackup();
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
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                    //Clear previous list data
                    Entry.EntryList.Clear();
                    //Clear previous Kavuahs
                    if (Kavuah.KavuahsList != null)
                    {
                        Kavuah.KavuahsList.Clear();
                    }
                    this.CalculateProblemOnahs();
                }
            }

            if (xml.HasChildNodes)
            {
                foreach (XmlNode entryNode in xml.SelectNodes("//Entry"))
                {
                    bool isInvisible = entryNode.SelectSingleNode("IsInvisible") == null ?
                        false :
                        Convert.ToBoolean(entryNode.SelectSingleNode("IsInvisible").InnerText);
                    int day = Convert.ToInt32(entryNode.SelectSingleNode("Day").InnerText);
                    int month = Convert.ToInt32(entryNode.SelectSingleNode("Month").InnerText);
                    int year = Convert.ToInt32(entryNode.SelectSingleNode("Year").InnerText); ;
                    DayNight dayNight = (DayNight)Convert.ToInt32(entryNode.SelectSingleNode("DN").InnerText);
                    string notes = entryNode.SelectSingleNode("Notes").InnerText;

                    Entry newEntry = new Entry(day, month, year, dayNight, notes)
                    {
                        IsInvisible = isInvisible
                    };

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
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        //Clear previous Kavuahs
                        if (Kavuah.KavuahsList != null)
                        {
                            Kavuah.KavuahsList.Clear();
                        }
                    }
                }
            }

            if (Kavuah.KavuahsList == null)
            {
                Kavuah.KavuahsList = new List<Kavuah>();
            }

            if (File.Exists(CurrentFile) && !Properties.Settings.Default.RecentFiles.Contains(CurrentFile))
            {
                Properties.Settings.Default.RecentFiles.Insert(0, CurrentFile);
                this.recentFilesToolStripMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(CurrentFile));
                this.recentFilesToolStripMenuItem.Enabled = this.clearRecentFilesToolStripMenuItem.Enabled = true;
            }

            this.SetCaptionText();
            this.SortEntriesAndSetInterval();
            this.CalculateProblemOnahs();
            this.bindingSourceEntries.DataSource = Entry.EntryList.Where(en => !en.IsInvisible);
        }

        public void SetCaptionText()
        {
            this.Text = " חשבשבון גירסה " +
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " - " +
                Program.GetCurrentPlaceName() + " - " +
                (this.CurrentFileIsRemote ? "קובץ רשת - " : "") + this.CurrentFileName;
            if (this.pbWeb != null)
            {
                this.pbWeb.Visible = this.CurrentFileIsRemote;
            }
        }

        public void AfterChangePreferences()
        {
            this.SetLocation();
            this.SetDateAndDayNight();
            this.CalculateProblemOnahs();
            this.SetCaptionText();
            this.DisplayMonth();
        }

        public string GetTempXmlFile()
        {
            File.WriteAllText(this._tempXMLFileName, CurrentFileXML ?? "");
            return this._tempXMLFileName;
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
                else if (!String.IsNullOrWhiteSpace(this.CurrentFile))
                {
                    xml = File.ReadAllText(this.CurrentFile);
                }

                return (string.IsNullOrWhiteSpace(xml) ? "<Entries />" : xml);
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