using JewishCalendar;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Tahara;

namespace Chashavshavon
{
    static class Program
    {
        public static readonly HebrewCalendar HebrewCalendar = new HebrewCalendar();
        public static readonly CultureInfo CultureInfo = new CultureInfo("he-IL", false);
        public static readonly string TempFolderPath = Path.GetTempPath() + @"\ChashInstall";
        public static readonly string BackupFolderPath = Directory.GetCurrentDirectory() + @"\Backups";
        public static readonly List<Entry> EntryList = new List<Entry>();
        public static readonly List<Kavuah> KavuahList = new List<Kavuah>();
        public static readonly List<TaharaEvent> TaharaEventList = new List<TaharaEvent>();
        public static readonly List<Onah> ProblemOnahs = new List<Onah>();

        public static bool RunInDevMode { get; private set; } = false;

        //We need to keep track of the Jewish "today" as DateTime.Now will give the wrong day if it is now after shkiah and before midnight.
        public static DateTime Today { get; set; }
        public static Onah NowOnah { get; set; }
        //Keeps track of where user is; for calculating zmanim
        public static Location CurrentLocation { get; set; }
        public static FrmMain MainForm { get; set; }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            //If first run after an upgrade
            if (Properties.Settings.Default.NeedsSettingsUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.NeedsSettingsUpgrade = false;
            }

            if (!Directory.Exists(TempFolderPath))
            {
                Directory.CreateDirectory(TempFolderPath);
            }

            if (!Directory.Exists(BackupFolderPath))
            {
                Directory.CreateDirectory(BackupFolderPath);
            }

            if (Properties.Settings.Default.DevMode)
            {
                RunInDevMode = true;
            }

#if DEBUG
            RunInDevMode = true;
#endif

            if (string.IsNullOrEmpty(Properties.Settings.Default.ChashFilesPath))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                    @"\Chashavshavon Files";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                Properties.Settings.Default.ChashFilesPath = path;
                Properties.Settings.Default.Save();
            }

            if (Properties.Settings.Default.RecentFiles == null)
            {
                Properties.Settings.Default.RecentFiles = new System.Collections.Specialized.StringCollection();
            }

            if (args.Length > 0)
            {
                MainForm = new FrmMain(args[0]);
            }
            else
            {
                MainForm = new FrmMain();
            }
            Application.Run(MainForm);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception, false);
        }

        public static void BeforeExit(bool keepTemp)
        {
            Properties.Settings.Default.Save();
            if (!keepTemp && Directory.Exists(TempFolderPath))
            {
                try
                {
                    Directory.Delete(TempFolderPath, true);
                }
                catch (Exception ex)
                {
                    HandleException(ex, true);
                }
            }
        }

        public static void HandleException(Exception excep, bool silent)
        {
            using (System.ComponentModel.BackgroundWorker bgw = new System.ComponentModel.BackgroundWorker())
            {
                bgw.DoWork += delegate
                {
                    string logFilePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                        "\\ErrorLog.csv";
                    while (excep.InnerException != null)
                    {
                        excep = excep.InnerException;
                    }

                    if (RunInDevMode)
                    {
                        ErrorMessage(excep.Message);
                    }

                    try
                    {
                        File.AppendAllText(logFilePath,
                            "\"" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"" +
                            excep.Message + "\",\"" + excep.Source + "\",\"" + excep.TargetSite +
                            "\",\"" + excep.StackTrace + "\"" + Environment.NewLine, System.Text.Encoding.UTF8);
                    }
                    catch (Exception ex)
                    {
                        if (RunInDevMode)
                        {
                            ErrorMessage(ex.Message);
                        }
                    }

                    if ((Utils.RemoteFunctions.IsConnectedToInternet() || RunInDevMode) &&
                               (silent ||
                               AskUser("ארעה שגיעה.\nהאם אתם מסכימים שישלח פרטי השגיאה למתכנתי חשבשבון כדי שיוכלו להיות מודעים להבעיה והאיך לטפל בה?\nלא תשלח שום מידע שיכול לפגוע בפרטיות המשתמש.")))
                    {
                        try
                        {
                            Utils.RemoteFunctions.ProcessRemoteException(excep, logFilePath);
                            if (!silent)
                            {
                                Inform("פרטי השגיאה נשלחו בהצלחה.");
                            }
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                File.AppendAllText(logFilePath,
                                    "\"" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") +
                                    "\",\"While sending error info message - " +
                                    ex.Message + "\",\"" + ex.Source + "\",\"" + ex.TargetSite +
                                    "\"" + Environment.NewLine);
                            }
                            catch { }

                            if (!silent)
                            {
                                Exclaim("נכשלה שליחת פרטי השגיאה");
                            }
                        }
                    }

                };
                bgw.RunWorkerAsync();
            }
        }

        internal static string GetCurrentPlaceName()
        {
            return string.IsNullOrWhiteSpace(CurrentLocation.NameHebrew) ?
                CurrentLocation.Name : CurrentLocation.NameHebrew;
        }

        internal static (List<Entry> entries, List<Kavuah> kavuahs) LoadEntriesKavuahsFromXml(string xmlString)
        {
            (List<Entry> entries, List<Kavuah> kavuahs) lists = (entries: new List<Entry>(), kavuahs: new List<Kavuah>());
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(xmlString);
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
                        Kavuah ka = new Kavuah(
                            (KavuahType)Enum.Parse(typeof(KavuahType), k.Attributes["KavuahType"].InnerText),
                            newEntry.DayNight)
                        {
                            Number = Convert.ToInt32(k.Attributes["Number"].InnerText),
                            SettingEntryDate = newEntry.DateTime
                        };
                        newEntry.NoKavuahList.Add(ka);
                    }
                    lists.entries.Add(newEntry);
                }
                Entry.SortEntriesAndSetInterval(lists.entries);
                //After the list of Entries, there is a lst of Kavuahs
                if (xml.SelectNodes("//Kavuah").Count > 0)
                {
                    XmlSerializer ser = new XmlSerializer(typeof(List<Kavuah>));
                    lists.kavuahs.AddRange((List<Kavuah>)ser.Deserialize(
                        new StringReader(xml.SelectSingleNode("//ArrayOfKavuah").OuterXml)));
                }
            }

            return lists;
        }


        internal static (List<Entry>, List<Kavuah>, List<TaharaEvent>) LoadEntriesKavuahsFromJson(string jsonString)
        {
            (List<Entry> entries, List<Kavuah> kavuahs, List<TaharaEvent> taharaEvents) lists = (entries: new List<Entry>(), kavuahs: new List<Kavuah>(), taharaEvents: new List<TaharaEvent>());
            JToken js = JToken.Parse(jsonString);


            if (js.HasValues)
            {
                foreach (JToken entry in js["Entries"])
                {
                    bool isInvisible = entry.Value<bool>("IsInvisible");
                    int abs = entry.Value<int>("Abs");
                    DayNight dayNight = (DayNight)entry.Value<int>("DN");
                    string notes = entry.Value<string>("Notes");
                    Entry newEntry = new Entry(JewishDateCalculations.GetGregorianDateFromAbsolute(abs), dayNight, notes)
                    {
                        IsInvisible = isInvisible
                    };

                    if (js["NoKavuah"] != null)
                    {
                        // If during the addition of a new Entry the program finds
                        // a set of 3 entries that might have been considered a Kavuah;
                        // such as if there are 3 of the same haflagas in a row,
                        // the user is prompted to create a new kavuah. If they choose not to,
                        // a NoKavuah element is added to the 3rd entry so the user
                        // won't be prompted again each time the list is reloaded.
                        foreach (JToken noKavuahNode in js["NoKavuah"])
                        {
                            Kavuah ka = new Kavuah(
                                (KavuahType)Enum.Parse(typeof(KavuahType), noKavuahNode.Value<string>("KavuahType")),
                                newEntry.DayNight)
                            {
                                Number = noKavuahNode.Value<int>("Number"),
                                SettingEntryDate = newEntry.DateTime
                            };
                            newEntry.NoKavuahList.Add(ka);
                        }
                    }
                    lists.entries.Add(newEntry);
                }
                Entry.SortEntriesAndSetInterval(lists.entries);

                //After the list of Entries, there is a list of Kavuahs
                foreach (JToken kavuah in js["Kavuahs"])
                {
                    lists.kavuahs.Add(kavuah.ToObject<Kavuah>());
                }

                //After the list of Kavuahs, there is a list of Tahara Events
                if (js["TaharaEvents"] != null)
                {
                    foreach (JToken taharaEvent in js["TaharaEvents"])
                    {
                        lists.taharaEvents.Add(new TaharaEvent((TaharaEventType)taharaEvent.Value<int>("TaharaEventType"))
                        {
                            DateTime = taharaEvent.Value<DateTime>("DateTime"),
                            Notes = taharaEvent.Value<string>("Notes")
                        });
                    }
                    TaharaEvent.SortList(lists.taharaEvents);
                }
            }

            return lists;
        }



        #region Extension Methods
        /// <summary>
        /// Tests to see if an object equals any one of the objects in a list.
        /// This function works like the SQL keyword "IN" - "SELECT * FROM Orders WHERE OrderId IN (5432, 9886, 8824)".
        /// </summary>
        /// <param name="test">Object to be searched for - is supplied by the compiler using the caller object</param>
        /// <param name="list">List of objects to search in.</param>
        /// <returns></returns>		
        public static bool In(this object o, params object[] list)
        {
            return Array.IndexOf(list, o) > -1;
        }

        /// <summary>
        /// Determines if the two given DateTime object refer to the same day.
        /// NOTE: This function was created for the situation where the time factor 
        /// of the given DateTime objects is not a factor in determining if they are the same;
        /// if they refer to the same date, the function returns true.
        /// </summary>
        /// <param name="firstDate"></param>
        /// <param name="secondDate"></param>
        /// <returns></returns>
        public static bool IsSameday(this DateTime firstDate, DateTime secondDate)
        {
            return firstDate.Date == secondDate.Date ||
                (HebrewCalendar.GetYear(firstDate) == HebrewCalendar.GetYear(secondDate) &&
                HebrewCalendar.GetMonth(firstDate) == HebrewCalendar.GetMonth(secondDate) &&
                HebrewCalendar.GetDayOfMonth(firstDate) == HebrewCalendar.GetDayOfMonth(secondDate));
        }

        /// <summary>
        /// Determine if the given SpecialDayType contains the given type. Equivalent to Enum.HasFlag.
        /// </summary>
        /// <param name="specialDayType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSpecialDayType(this SpecialDayTypes specialDayType, SpecialDayTypes value)
        {
            return specialDayType.HasFlag(value);
        }

        public static void PopMessage(string message, MessageBoxIcon icon, string caption = "חשבשבון")
        {
            MessageBox.Show(message, caption,
                            MessageBoxButtons.OK,
                            icon,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);

        }

        public static bool AskUser(string message, string caption = "חשבשבון")
        {
            return MessageBox.Show(message,
                caption,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes;
        }


        public static void Inform(string message, string caption = "חשבשבון")
        {
            PopMessage(message, MessageBoxIcon.Information, caption);
        }

        public static void Warn(string message, string caption = "חשבשבון")
        {
            PopMessage(message, MessageBoxIcon.Warning, caption);
        }

        public static void Exclaim(string message, string caption = "חשבשבון")
        {
            PopMessage(message, MessageBoxIcon.Exclamation, caption);
        }

        public static void ErrorMessage(string message, string caption = "חשבשבון")
        {
            PopMessage(message, MessageBoxIcon.Error, caption);
        }



        #endregion
    }
}