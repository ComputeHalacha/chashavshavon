using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chashavshavon
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("wininet.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private extern static bool InternetGetConnectedState(ref InternetConnectionState_e lpdwFlags, int dwReserved);

        [Flags]
        enum InternetConnectionState_e : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            if (args.Length > 0)
            {
                Application.Run(new frmMain(args[0]));
            }
            else
            {
                Application.Run(new frmMain());
            }
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            System.IO.File.AppendAllText(System.IO.Directory.GetCurrentDirectory() + "\\ErrorLog.csv",
                "\"" + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "\",\"" +
                (e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message) +
                "\"" + Environment.NewLine);
            MessageBox.Show("ארעה שגיאה" + "\n\n" + (e.Exception.InnerException != null ? e.Exception.InnerException.Message : e.Exception.Message),
                            "שגיאה",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }

        public static bool IsConnectedToInternet()
        {
            InternetConnectionState_e flags = 0;
            return InternetGetConnectedState(ref flags, 0);
        }
    }
}