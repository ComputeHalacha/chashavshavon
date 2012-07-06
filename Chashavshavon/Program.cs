﻿using System;
using System.Globalization;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace Chashavshavon
{
    static class Program
    {
        private static readonly string runtimeArgumentHandle = Utils.GeneralUtils.Decrypt(Properties.Settings.Default.ApplicationSetId,
                                Properties.Settings.Default.ApplicationRuntime);
        public static readonly HebrewCalendar HebrewCalendar = new HebrewCalendar();
        public static readonly CultureInfo CultureInfo = new CultureInfo("he-IL", false);
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

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string logPath = System.IO.Directory.GetCurrentDirectory() + "\\ErrorLog.csv";
            Exception excep = e.Exception;
            while (excep.InnerException != null)
            {
                excep = excep.InnerException;
            }

            if (Properties.Settings.Default.UseLocalURL)
            {
                MessageBox.Show(excep.Message);
            }

            System.IO.File.AppendAllText(logPath,
                "\"" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"" + 
                excep.Message + "\",\"" + excep.Source + "\",\"" + excep.TargetSite + 
                "\"" + Environment.NewLine,
                System.Text.Encoding.UTF8);
            try
            {
                if ((Utils.RemoteFunctions.IsConnectedToInternet() || Properties.Settings.Default.UseLocalURL) &&
                        !string.IsNullOrEmpty(Properties.Settings.Default.ErrorGetterAddress) &&
                        MessageBox.Show("ארעה שגיעה.\nהאם אתם מסקימים לשלוח פרטי השגיאה למתכנתי חשבשבון כדי שיוכלו להיות מודעים להבעיה והאיך לטפל בה?\nלא תשלח שום מידע שיכול לפגוע בפרטיות המשתמש.",
                                        "חשבשבון",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Exclamation,
                                        MessageBoxDefaultButton.Button1,
                                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.Yes)            
                {
                    var mm = new MailMessage(Properties.Settings.Default.ErrorGetterAddress, Properties.Settings.Default.ErrorGetterAddress);
                    mm.Subject = "Chashavshavon Version: " +
                            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " Exception";
                    mm.IsBodyHtml = true;
                    mm.BodyEncoding = System.Text.Encoding.UTF8;
                    mm.Attachments.Add(new System.Net.Mail.Attachment(logPath));
                    if (MainForm is Form)
                    {
                        mm.Attachments.Add(new System.Net.Mail.Attachment(MainForm.GetTempXmlFile()));
                    }
                    mm.Body = "Exception: " + excep.Message +
                        "<br />Source: " + excep.Source +
                        "<br />Target Site: " + excep.TargetSite +
                        "<br />Stack Trace: " + excep.StackTrace.Replace(Environment.NewLine, "<br />");
                    var mailClient = new SmtpClient()
                    {
                        Port = 997,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Credentials = new NetworkCredential(Properties.Settings.Default.ErrorGetterAddress,
                            runtimeArgumentHandle)
                    };
                    mailClient.SendAsync(mm, null);
                    mailClient.Dispose();
                    mm.Dispose();
                }
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.UseLocalURL)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
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