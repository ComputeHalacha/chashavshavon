using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net.Mail;

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

        public static bool SaveCurrentFile(string fileName, string xml)
        {
            return ExecuteRemoteCall("SetFileText", NewParam("fileName", fileName), NewParam("fileText", xml)) != null;
        }

        public static string GetCurrentFileText(string fileName)
        {
            return ExecuteRemoteCall("GetFileText", NewParam("fileName", fileName)).OuterXml;
        }

        public static XmlDocument GetRemoteResponse(string function, params KeyValuePair<string, string>[] fields)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc = ExecuteRemoteCall(function, fields);
                XmlNode errorNode = doc.SelectSingleNode("//error");
                if (errorNode != null)
                {
                    MessageBox.Show(errorNode.InnerText,
                        "חשבשבון",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.RightAlign);
                    return null;
                }
                else
                {
                    return doc;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("לא ניתן כעת להתחבר לשרת חשבשבון." + Environment.NewLine + e.Message,
                    "חשבשבון",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
                return null;
            }
        }

        public static string GetRemoteResponseText(string function, params KeyValuePair<string, string>[] fields)
        {
            string responseText = null;
            WebRequest request = WebRequest.Create(
                                (Properties.Settings.Default.UseLocalURL ?
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
            using (MailMessage mailMessage = new MailMessage(Properties.Resources.ErrorGetterAddress,
                    Properties.Resources.ErrorGetterAddress))
            {
                mailMessage.Subject = "Chashavshavon Version: " +
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() +
                        " Exception";
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                if (File.Exists(logFilePath))
                {
                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(logFilePath));
                }
                if (Program.MainForm is Form)
                {
                    string currentFileTextPath = Program.MainForm.GetTempXmlFile();
                    if ((!string.IsNullOrEmpty(currentFileTextPath)) && File.Exists(currentFileTextPath))
                    {
                        mailMessage.Attachments.Add(new System.Net.Mail.Attachment(currentFileTextPath));
                    }
                }
                mailMessage.Body = "Exception: " + excep.Message +
                    "<br />Source: " + excep.Source +
                    "<br />Target Site: " + excep.TargetSite +
                    "<br />Stack Trace: <pre>" + excep.StackTrace + "</pre>";

                var mailClient = new SmtpClient()
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = new NetworkCredential(Properties.Resources.ErrorGetterAddress,
                        Utils.GeneralUtils.Decrypt(Properties.Resources.ApplicationSetId,
                                    Properties.Resources.ApplicationRuntime))
                };
                mailClient.Send(mailMessage);
                mailClient.Dispose();
            }
        }

        public static Version GetLatestVersion()
        {
            Version version = null;
            string versionString = null;

            try
            {
                string url = "http://" +
                    (Properties.Settings.Default.UseLocalURL ?
                        Properties.Resources.ComputeURLLocalHost : Properties.Resources.ComputeURLHost) +
                    Properties.Resources.GetLatestVersionURL;
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                byte[] byteArray = Encoding.UTF8.GetBytes("{}");
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.OK)
                {
                    dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    var jse = new System.Web.Script.Serialization.JavaScriptSerializer();
                    //The web method returns: "{'d': '0.0.0.0'}"
                    versionString = jse.Deserialize<Dictionary<string, string>>(reader.ReadToEnd())["d"];
                    reader.Close();
                    dataStream.Close();
                }
                dataStream.Dispose();
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

        public static String DownloadLatestVersion()
        {
            string installerPath = null;            
            string path = Program.TempFolderPath +  @"\InstallChashavshavon.exe";

            try
            {
                if (!System.IO.Directory.Exists(Program.TempFolderPath))
                {
                    System.IO.Directory.CreateDirectory(Program.TempFolderPath);
                }

                WebRequest request = WebRequest.Create("http://" +
                    (Properties.Settings.Default.UseLocalURL ?
                        Properties.Resources.ComputeURLLocalHost : Properties.Resources.ComputeURLHost) +
                    Properties.Resources.DownloadApplicationURL);
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

                        var dataStream = response.GetResponseStream();
                        Byte[] buffer = new Byte[256];
                        FileStream fs = new FileStream(path, FileMode.Create);                
                        int numBytesRead = 0;
                        while(true)
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


        private static XmlDocument ExecuteRemoteCall(string function, params KeyValuePair<string, string>[] fields)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(GetRemoteResponseText(function, fields));
            XmlNode errorNode = doc.SelectSingleNode("//error");
            if (errorNode != null)
            {
                switch (int.Parse(errorNode.Attributes["errorId"].InnerText))
                {
                    case 1:
                        errorNode.InnerText = "אורך השם משתמש חייב להיות לפחות שני תווים, שם משתמש שהוקשה לא חוקי";
                        break;
                    case 2:
                        errorNode.InnerText = "הסיסמה שהוקשה לא חוקי, אורך הסיסמה חייב להיות לפחות ארבע תווים";
                        break;
                    case 3:
                        errorNode.InnerText = "שם המשתמש וסיסמה שהוקשו קיימים כבר ברשת";
                        break;
                    case 4:
                        errorNode.InnerText = "לא נמצא במערכת משתמש בשם וסיסמה שהוקשו";
                        break;
                    case 5:
                        errorNode.InnerText = "לא ניתן לשמור קובץ בלי שם במערכת";
                        break;
                    case 6:
                        errorNode.InnerText = "קובץ עם שם הקובץ שהוקש קיים כבר במערכת למשתמש הזאת. נא לשמור הקובץ בשם אחר";
                        break;
                    case 7:
                        errorNode.InnerText = "לא נמצא במערכת קובץ בשם זה למשתמש הזאת";
                        break;
                }
            }
            return doc;
        }
    }
}
