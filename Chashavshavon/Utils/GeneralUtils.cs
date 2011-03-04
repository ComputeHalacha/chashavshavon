using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Cryptography;

namespace Chashavshavon.Utils
{
	/// <summary>
	/// Summary description for GeneralUtils.
	/// </summary>
	public abstract class GeneralUtils
	{
		private GeneralUtils(){}

		#region "Is" funtions
		/// <summary>
		/// Checks a string to see if it can be converted to a number.
		/// NOTE: Returns true of string.Length == 0
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static bool IsNumeric(string str)
		{
			if(str.Length == 0) 
			{
				return true;
			}
			try
			{
				Convert.ToDecimal(str.Trim());
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Checks an object to see if it can be converted to a number.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool IsNumeric(object obj)
		{
			try
			{
				Convert.ToDecimal(obj);
				return true;
			}
			catch
			{
				return false;
			}
		}
		
		/// <summary>
		/// Checks an object to see if it is a valid DateTime.
		/// NOTE: If the given object is a date variable that was not yet assigned a value, this function will return false.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool IsDate(object obj)
		{
			try
			{
				DateTime dt = Convert.ToDateTime(obj);
				if(dt.CompareTo(DateTime.MinValue) == 0)
				{
					return false;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Checks a string to see if it can be converted to a DateTime.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>		
		public static bool IsDate(string str)
		{
			try
			{
				DateTime dt = Convert.ToDateTime(str.Trim());				
				return true;
			}
			catch
			{
				return false;
			}
		}				
		
		/// <summary>
		/// Checks an object to see if it can be converted to a bool
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static bool IsBool(object obj)
		{
			try
			{
				Convert.ToBoolean(obj);
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region age and date funtions
	
		/// <summary>
		/// Returns the age at the "dateOfReference" when the of an object/person was created/born on the "dateOfbirth"
		/// </summary>
		/// <param name="dateOfbirth">Date object was created or person was born</param>
		/// <param name="dateOfReference">The date for which the age is to be determined</param>
		/// <returns></returns>
		public static int GetAge(DateTime dateOfbirth, DateTime dateOfReference)
		{
			TimeSpan timeSpan = dateOfReference.Subtract(dateOfbirth);
			DateTime age = new DateTime(timeSpan.Ticks);
			return (age.Year - 1);
		}

		/// <summary>
		/// Returns the date of birth or date of creation for an object that is exactly the given age at the "dateOfReference" 
		/// NOTE: This function assumes that the given date is the birthday
		/// </summary>
		/// <param name="dateOfReference">The date for which the subjects age is given</param>
		/// <param name="age">The age of the subject at the Date contained in "dateOfReference"</param>
		/// <returns></returns>
		public static DateTime GetDOBByAge(DateTime dateOfReference, int age)
		{					
			DateTime dob = new DateTime(dateOfReference.Year,dateOfReference.Month,dateOfReference.Day);
			dob = dob.AddYears(age);			
			return (dob);
		}

		/// <summary>
		/// Determines if the two given DateTime object refer to the same day.
		/// NOTE: This function does not take into account the time factor of the given DateTime objects, if they refer to the same date, the function returns true
		/// </summary>
		/// <param name="firstDate"></param>
		/// <param name="secondDate"></param>
		/// <returns></returns>
		public static bool IsSameday(DateTime firstDate, DateTime secondDate)
		{
			bool isSameDate = false;
			if( firstDate.Year == secondDate.Year
				&&
				firstDate.Month == secondDate.Month
				&&
				firstDate.Day == secondDate.Day)
			{
				isSameDate = true;
			}
			return isSameDate;
		}
		#endregion

		#region "Set" functions
		/// <summary>
		/// Sets an int variable to a value
		/// </summary>
		/// <remarks>
		/// Useful for setting variables from a query resultset
		/// </remarks>
		/// <param name="privateVar"></param>
		/// <param name="val"></param>
		public static void Set(ref int privateVar, object val)
		{
			if(IsNumeric(val))
			{
				privateVar = Convert.ToInt32(val);
			}
		}
		
		/// <summary>
		/// Sets a float variable to a value
		/// </summary>
		/// <remarks>
		/// Useful for setting variables from a query resultset
		/// </remarks>
		/// <param name="privateVar"></param>
		/// <param name="val"></param>
		public static void Set(ref float privateVar, object val)
		{
			if(IsNumeric(val))
			{
				privateVar = Convert.ToSingle(val);
			}
		}
		
		/// <summary>
		/// Sets a DateTime variable to a value
		/// </summary>
		/// <remarks>
		/// Useful for setting variables from a query resultset
		/// </remarks>
		/// <param name="privateVar"></param>
		/// <param name="val"></param>
		public static void Set(ref DateTime privateVar, object val)
		{
			if(IsDate(val))
			{
				privateVar = Convert.ToDateTime(val);
			}
		}

		/// <summary>
		/// Sets a bool variable to a value
		/// </summary>
		/// <remarks>
		/// Useful for setting variables from a query resultset
		/// </remarks>
		/// <param name="privateVar"></param>
		/// <param name="val"></param>
		public static void Set(ref bool privateVar, object val)
		{
			if(IsBool(val))
			{
				privateVar = Convert.ToBoolean(val);
			}
		}
		#endregion

        #region String Encryption Functions
        /// <summary>
        /// Encrypts a byte array using the Rijndael algorithm
        /// </summary>
        /// <param name="data">The byte array to encrypt</param>
        /// <param name="Key">A byte array containg the "password"</param>
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
        /// Encypts a string using the Rijndael algorithm
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
        /// Decrypts a byte array using the Rijndael algorithm
        /// </summary>
        /// <param name="cipherData">The byte array to decrypt</param>
        /// <param name="Key">A byte array containg the "password"</param>
        /// <param name="IV">A byte array containing the "salt" for the initialization vector</param>
        /// <returns>The decrypted data as a byte array</returns>
        public static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
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
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        #endregion

        #region miscellanious functions
        /// <summary>
		/// Tests to see if an object equals any one of the objects in a list.
		/// This function works like the SQL keyword "IN" - "SELECT * FROM Orders WHERE OrderId IN (5432, 9886, 8824)".
		/// </summary>
		/// <param name="test">Object to be searched for</param>
		/// <param name="list">List of objects to search in.</param>
		/// <returns></returns>
		public static bool In(object objectToTest, params object[] list)
		{
			return (Array.IndexOf(list, objectToTest) > -1);
		}
		
		/// <summary>
		/// Returns the name (rather then the value) of an item in an enumeration
		/// </summary>
		/// <param name="o">The item in an enumeration to return the name of</param>
		/// <returns></returns>
		/// <remarks>
		/// This function may never be nessesary as the ToString() function on an enum item will return its name - 
		/// though I assume that internally the overrided ToString() on the Enum object does the same thing that we do here!
		/// </remarks>
		public static string GetEnumItemName(object o)
		{
			return Enum.GetName(o.GetType(), o);
		}

		/// <summary>
		/// Gets an enum item from a string containing the name of an object member
		/// </summary>
		/// <param name="name">The name of the enumeration member to return</param>
		/// <param name="enumType">The enumberation type to search</param>
		/// <returns>The member of the enum, or null, if not found</returns>
		public static object GetEnumItemFromName(string name, Type enumType)
		{
			object item = null;
			foreach(object t in Enum.GetValues(enumType))
			{
				if(GetEnumItemName(t) == name)
				{
					item = t;
					break;
				}
			}
			return item;
		}		
		
		/// <summary>
		/// Determines if the first type inherits from the second type
		/// </summary>
		/// <param name="derivedType">The Type to test</param>
		/// <param name="parentType">The Type to test for</param>
		/// <returns></returns>
		public static bool IsDerivedType(Type derivedType, Type parentType)
		{
			if(derivedType.BaseType.Name == "Object") //We got to the top of the list, "Object" inherits from nobody.
			{
				return false;
			}
			else if(derivedType.BaseType.Name == parentType.Name) //We found our match.
			{
				return true;
			}
			else
			{
				return IsDerivedType(derivedType.BaseType, parentType); //Recurse to one up in the inheritance chain.
			}
		}

        /// <summary>
        /// Extracts the file name part of a file path or URL
        /// </summary>
        /// <param name="path">The file path or URL for which to extract the file name</param>
        /// <returns></returns>
        public static string FileName(string path)
        {
            string[] pathParts = path.Split('\\');
            if (pathParts.Length == 1)
            {
                pathParts = path.Split('/');
            }
            string fileName = pathParts[pathParts.Length - 1];
            return fileName;
        }
		#endregion
	}
}
