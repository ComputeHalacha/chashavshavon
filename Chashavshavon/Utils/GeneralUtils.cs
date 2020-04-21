using System;
using System.IO;
using System.Security.Cryptography;

namespace Chashavshavon.Utils
{
    /// <summary>
    /// Summary description for GeneralUtils.
    /// </summary>
    public abstract class GeneralUtils
    {
        private GeneralUtils() { }

        private static readonly char[] _jsd = { 'à', 'á', 'â', 'ã', 'ä', 'å', 'æ', 'ç', 'è' };
        private static readonly char[] _jtd = { 'é', 'ë', 'ì', 'î', 'ð', 'ñ', 'ò', 'ô', 'ö' };
        private static readonly char[] _jhd = { '÷', 'ø', 'ù', 'ú' };


        #region String Encryption Functions
        /// <summary>
        /// Encrypts a byte array using the Rijndael algorithm
        /// </summary>
        /// <param name="data">The byte array to encrypt</param>
        /// <param name="Key">A byte array containing the "password"</param>
        /// <param name="IV">A byte array containing the "salt" for the initialization vector</param>
        /// <returns>The encrypted data as a byte array</returns>
        public static byte[] Encrypt(byte[] data, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(data, 0, data.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        /// <summary>
        /// Encrypts a string using the Rijndael algorithm
        /// </summary>
        /// <param name="text">The text to encrypt</param>
        /// <param name="Password">The password to use</param>
        /// <returns>The encrypted string</returns>
        public static string Encrypt(string text, string Password)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(text);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// Decrypts a string using the Rijndael algorithm
        /// </summary>
        /// <param name="cipherText">The text to decrypt</param>
        /// <param name="Password">The password to use</param>
        /// <returns>The decrypted string</returns>
        public static string Decrypt(string cipherText, string Password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
            cs.Dispose();
            return System.Text.Encoding.Unicode.GetString(ms.ToArray());
        }

        //Recursively gets all controls contained in the given control or any of it's descendants
        public static System.Collections.Generic.IEnumerable<System.Windows.Forms.Control> GetAllControls(System.Windows.Forms.Control control)
        {
            var l = new System.Collections.Generic.List<System.Windows.Forms.Control>();
            l.Add(control);
            foreach (System.Windows.Forms.Control c in control.Controls)
            {
                l.AddRange(GetAllControls(c));
            }
            return l;
        }

        #endregion
        
        /// <summary>
        /// Gets the Jewish representation of a number (365 = ùñ"ä)
        /// Minimum number is 1 and maximum is 9999.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToJNum(int number)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException("Min value is 1");
            }

            if (number > 9999)
            {
                throw new ArgumentOutOfRangeException("Max value is 9999");
            }

            var n = number;
            var retval = "";

            if (n >= 1000)
            {
                retval += _jsd[(int)((n - (n % 1000)) / 1000) - 1].ToString() + '\'';
                n = n % 1000;
            }

            while (n >= 400)
            {
                retval += 'ú';
                n -= 400;
            }

            if (n >= 100)
            {
                retval += _jhd[(int)((n - (n % 100)) / 100) - 1].ToString();
                n = n % 100;
            }

            if (n == 15)
            {
                retval += "èå";
            }
            else if (n == 16)
            {
                retval += "èæ";
            }
            else
            {
                if (n > 9)
                {
                    retval += _jtd[(int)((n - (n % 10)) / 10) - 1].ToString();
                }
                if (n % 10 > 0)
                {
                    retval += _jsd[(n % 10) - 1];
                }
            }
            if (number > 999 && number % 1000 < 10)
            {
                retval = '\'' + retval;
            }
            else if (retval.Length > 1)
            {
                retval = retval.Insert(retval.Length - 1, "\"");
            }
            return retval;
        }

    }
}
