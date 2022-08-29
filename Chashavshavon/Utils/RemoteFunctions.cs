using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Chashavshavon.Utils
{
    static class RemoteFunctions
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

        public static bool IsConnectedToInternet()
        {
            InternetConnectionState_e flags = 0;
            return InternetGetConnectedState(ref flags, 0);
        }

        public static KeyValuePair<string, string> NewParam(string name, string value)
        {
            return new KeyValuePair<string, string>(name, value);
        }

        public static bool SaveFile(string fileName, string json)
        {
            var jtoken = ExecuteRemoteCall("SetFileText", NewParam("fileName", fileName), NewParam("fileText", json));
            return jtoken.Value<bool>("succeeded");
        }

        public static string GetFileJson(string fileName)
        {
            var jtoken = ExecuteRemoteCall("GetFileText", NewParam("fileName", fileName));
            if (jtoken.Value<bool>("succeeded"))
            {
                return jtoken.Value<string>("fileText");
            }
            else
            {
                return null;
            }
        }

        public static void RunRemoteAction(string functionName, Action<JToken> onSuccess, Action<string> onFail, params KeyValuePair<string, string>[] fields)
        {
            try
            {
                string errorMessage = null;
                var jtoken = ExecuteRemoteCall(functionName, fields);
                if (!jtoken.Value<bool>("succeeded"))
                {
                    switch (jtoken.Value<int>("errorId"))
                    {
                        case 1:
                            errorMessage = "אורך השם משתמש חייב להיות לפחות שני תווים, שם משתמש שהוקשה לא חוקי";
                            break;
                        case 2:
                            errorMessage = "הסיסמה שהוקשה לא חוקי, אורך הסיסמה חייב להיות לפחות ארבע תווים";
                            break;
                        case 3:
                            errorMessage = "שם המשתמש וסיסמה שהוקשו קיימים כבר ברשת";
                            break;
                        case 4:
                            errorMessage = "לא נמצא במערכת משתמש בשם וסיסמה שהוקשו";
                            break;
                        case 5:
                            errorMessage = "לא ניתן לשמור קובץ בלי שם במערכת";
                            break;
                        case 6:
                            errorMessage = "קובץ עם שם הקובץ שהוקש קיים כבר במערכת למשתמש הזאת. נא לשמור הקובץ בשם אחר";
                            break;
                        case 7:
                            errorMessage = "לא נמצא במערכת קובץ בשם זה למשתמש הזאת";
                            break;
                    }
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    onFail?.Invoke(errorMessage);
                }
                else
                {
                    onSuccess?.Invoke(jtoken);
                }
            }
            catch (Exception e)
            {
                onFail?.Invoke("לא ניתן כעת להתחבר לשרת חשבשבון." + Environment.NewLine + e.Message);
            }
        }

        public static string GetRemoteResponseText(string function, params KeyValuePair<string, string>[] fields)
        {
            string responseText = null;
            WebRequest request = WebRequest.Create((Program.RunInDevMode ?
                                                Properties.Resources.LocalAppURL : Properties.Resources.AppURL) +
                                "/" + function);
            request.Method = "POST";

            StringBuilder postData = new StringBuilder();
            postData.Append("userName=" + Properties.Settings.Default.RemoteUserName);
            postData.Append("&password=" + Properties.Settings.Default.RemotePassword);
            foreach (KeyValuePair<string, string> field in fields)
            {
                postData.AppendFormat("&{0}={1}", field.Key, field.Value);
            }

            byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            request.Headers.Add("charset", "uft-8");
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
            {
                dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseText = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
            }
            dataStream.Dispose();
            response.Close();
            return responseText;
        }

        public static void ProcessRemoteException(Exception excep, string logFilePath)
        {
            string currentFileText = Program.MainForm is Form ? Program.MainForm.CurrentFileJson : "";
            string errorLogText = File.Exists(logFilePath) ? File.ReadAllText(logFilePath) : "";
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string url = GetComputeURL(Properties.Resources.SendErrorReportURL);
            WebRequest request = WebRequest.Create(url);
            System.Web.Script.Serialization.JavaScriptSerializer jse = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = jse.Serialize(new
            {
                App = "Chashavshavon",
                Version = version,
                excep.Message,
                excep.Source,
                TargetSite = excep.TargetSite.ToString(),
                excep.StackTrace,
                CurrentFile = currentFileText,
                CurrentFileExt = "xml",
                ErrorLog = errorLogText,
                ErrorLogExt = "csv"
            });
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);

            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.ContentLength = byteArray.Length;
            request.Timeout = 30000;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(resStream);
                string responseText =
                    jse.Deserialize<System.Collections.Generic.Dictionary<String, String>>(streamReader.ReadToEnd())["d"];
                streamReader.Close();
                streamReader.Dispose();
                resStream.Close();
                resStream.Dispose();
                if (responseText != "OK")
                {
                    throw new Exception(responseText);
                }
            }
            //Once sent off, we can delete the log file
            File.Delete(logFilePath);
        }

        public static Version GetLatestVersion()
        {
            Version version = null;
            string versionString = null;

            try
            {
                string url = GetComputeURL(Properties.Resources.GetLatestVersionURL);
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";

                WebResponse response = request.GetResponse();
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    versionString = reader.ReadToEnd();
                    reader.Close();
                    reader.Dispose();
                    dataStream.Close();
                    dataStream.Dispose();

                }
                response.Close();
            }
            catch (Exception ex)
            {
                Program.HandleException(ex, true);
            }

            if (!string.IsNullOrEmpty(versionString))
            {
                version = new Version(versionString);
            }
            return version;
        }

        public static string DownloadLatestVersion()
        {
            string installerPath = null;
            string path = Program.TempFolderPath + @"\InstallChashavshavon.exe";

            try
            {
                WebRequest request = WebRequest.Create(GetComputeURL(Properties.Resources.DownloadApplicationURL));
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    if (response.ContentType == "application/octet-stream")
                    {
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        Stream dataStream = response.GetResponseStream();
                        byte[] buffer = new byte[256];
                        FileStream fs = new FileStream(path, FileMode.Create);
                        int numBytesRead = 0;
                        while (true)
                        {
                            numBytesRead = dataStream.Read(buffer, 0, buffer.Length);
                            if (numBytesRead == 0)
                            {
                                break;
                            }
                            fs.Write(buffer, 0, numBytesRead);
                        }

                        dataStream.Close();
                        dataStream.Dispose();

                        fs.Close();
                        fs.Dispose();
                        installerPath = path;
                    }
                }
                response.Close();
            }
            catch (Exception ex)
            {
                Program.HandleException(ex, true);
            }

            return installerPath;
        }

        private static string GetComputeURL(string relativeURL)
        {
            return "http://" + (Program.RunInDevMode ?
                Properties.Resources.ComputeURLLocalHost : Properties.Resources.ComputeURLHost) +
                relativeURL;
        }

        private static JToken ExecuteRemoteCall(string function, params KeyValuePair<string, string>[] fields)
        {
            string jsonText = GetRemoteResponseText(function, fields);
            return JToken.Parse(jsonText);
        }
    }
}
