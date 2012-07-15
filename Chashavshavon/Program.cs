using System;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;

namespace Chashavshavon
{
    static class Program
    {
        public static readonly HebrewCalendar HebrewCalendar = new HebrewCalendar();
        public static readonly CultureInfo CultureInfo = new CultureInfo("he-IL", false);
        public static readonly string TempFolderPath = System.IO.Path.GetTempPath() + @"\ChashInstall";

        //We need to keep track of the Jewish "today" as DateTime.Now will give the wrong day if it is now after shkiah and before midnight.
        public static DateTime Today { get; set; }
        public static Onah NowOnah { get; set; }
        //Keeps track of where user is; for calculating zmanim
        public static Chashavshavon.Utils.Place CurrentPlace { get; set; }
        public static frmMain MainForm { get; set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);            

            if (!System.IO.Directory.Exists(Program.TempFolderPath))
            {
                System.IO.Directory.CreateDirectory(Program.TempFolderPath);
            }

            if (string.IsNullOrEmpty(Properties.Settings.Default.ChashFilesPath))
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Chashavshavon Files";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
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
                MainForm = new frmMain(args[0]);
            }
            else
            {
                MainForm = new frmMain();
            }
            Application.Run(MainForm);
        }

        public static void BeforeExit(bool keepTemp)
        {
            Properties.Settings.Default.Save();
            if (!keepTemp && System.IO.Directory.Exists(Program.TempFolderPath))
            {
                try
                {
                    System.IO.Directory.Delete(Program.TempFolderPath, true);
                }
                catch { }
            }
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        public static void HandleException(Exception excep, bool silent = false)
        {
            var bgw = new System.ComponentModel.BackgroundWorker();
            bgw.DoWork += delegate
            {
                string logFilePath = System.IO.Directory.GetCurrentDirectory() + "\\ErrorLog.csv";
                while (excep.InnerException != null)
                {
                    excep = excep.InnerException;
                }

                if (Properties.Settings.Default.UseLocalURL)
                {
                    MessageBox.Show(excep.Message);
                }

                try
                {
                    System.IO.File.AppendAllText(logFilePath,
                        "\"" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"" +
                        excep.Message + "\",\"" + excep.Source + "\",\"" + excep.TargetSite +
                        "\"" + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.UseLocalURL)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                if ((Utils.RemoteFunctions.IsConnectedToInternet() || Properties.Settings.Default.UseLocalURL) &&
                           !string.IsNullOrEmpty(Properties.Resources.ErrorGetterAddress) &&
                           (silent ||
                           MessageBox.Show("ארעה שגיעה.\nהאם אתם מסכימים שישלח פרטי השגיאה למתכנתי חשבשבון כדי שיוכלו להיות מודעים להבעיה והאיך לטפל בה?\nלא תשלח שום מידע שיכול לפגוע בפרטיות המשתמש.",
                                           "חשבשבון",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Exclamation,
                                           MessageBoxDefaultButton.Button1,
                                           MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes))
                {
                    try
                    {
                        Utils.RemoteFunctions.ProcessRemoteException(excep, logFilePath);
                        if (!silent)
                        {
                            MessageBox.Show("פרטי השגיאה נשלחו בהצלחה.",
                                    "חשבשבון",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.None,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            System.IO.File.AppendAllText(logFilePath,
                                "\"" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"While sending error info message - " +
                                ex.Message + "\",\"" + ex.Source + "\",\"" + ex.TargetSite +
                                "\"" + Environment.NewLine);
                        }
                        catch { }

                        if (!silent)
                        {
                            MessageBox.Show("נכשלה שליחת פרטי השגיאה",
                                    "חשבשבון",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
                        }
                    }
                }

            };
            bgw.RunWorkerAsync();
        }

        internal static string GetCurrentPlaceName()
        {
            return String.IsNullOrWhiteSpace(CurrentPlace.NameHebrew) ?
                CurrentPlace.Name : CurrentPlace.NameHebrew;
        }

        #region Extention Methods
        /// <summary>
        /// Tests to see if an object equals any one of the objects in a list.
        /// This function works like the SQL keyword "IN" - "SELECT * FROM Orders WHERE OrderId IN (5432, 9886, 8824)".
        /// </summary>
        /// <param name="test">Object to be searched for - is supplied by the compiler using the caller object</param>
        /// <param name="list">List of objects to search in.</param>
        /// <returns></returns>		
        public static bool In(this Object o, params object[] list)
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
            return (HebrewCalendar.GetYear(firstDate) == HebrewCalendar.GetYear(secondDate) &&
                HebrewCalendar.GetMonth(firstDate) == HebrewCalendar.GetMonth(secondDate) &&
                HebrewCalendar.GetDayOfMonth(firstDate) == HebrewCalendar.GetDayOfMonth(secondDate));
        }
        #endregion
    }
}