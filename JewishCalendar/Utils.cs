using System;
using System.Globalization;
using System.Text;

namespace JewishCalendar
{
    /// <summary>
    /// Nusach of siddur. Used for generating correct text of Sefira counting.
    /// </summary>
    public enum Nusach
    {
        /// <summary>
        /// Nusach Ashkenaz.
        /// </summary>
        Ashkenaz,
        /// <summary>
        /// Nusach Sefard and Ari
        /// </summary>
        Sefard,
        /// <summary>
        /// Nusach of Eidot Hamizrach and Aram Tzova.
        /// </summary>
        Sefardi
    }

    /// <summary>
    /// Contains general static arrays, some useful utility functions and other such pitchifkes.
    /// </summary>
    public static class Utils
    {

        #region Public Constructors

        /// <summary>
        /// static constructor
        /// </summary>
        static Utils()
        {
            // IMPORTANT NOTE: For using this file with a .NET Micro Framework project, the following must be removed.
            HebrewCultureInfo.DateTimeFormat.Calendar = HebrewCalendar;
        }

        #endregion Public Constructors

        #region Public Fields

        /// <summary>
        /// The Jewish names for the days of the week in English as an array. For example, DaysOfWeek[5] is Erev Shabbos
        /// </summary>
        public static string[] DaysOfWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Erev Shabbos", "Shabbos Kodesh" };

        /// <summary>
        /// A .NET Hebrew calendar.
        /// IMPORTANT NOTE: For using this file with a .NET Micro Framework project, the following must be removed.
        /// </summary>
        public static HebrewCalendar HebrewCalendar = new HebrewCalendar();

        /// <summary>
        /// The hebrew culture info
        /// </summary>
        public static CultureInfo HebrewCultureInfo = new CultureInfo("he-il");

        /// <summary>
        /// Names of days of week in Hebrew. יום ראשון is JewishDOWNames[0].
        /// </summary>
        public static string[] JewishDOWNames = { "יום ראשון", "יום שני", "יום שלישי", "יום רביעי", "יום חמישי", "ערב שבת קודש", "שבת קודש" };

        /// <summary>
        /// Names of days of week in Hebrew. ראשון is JewishDOWNames[0].
        /// </summary>
        public static string[] JewishDOWNamesShort = { "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת" };

        /// <summary>
        /// Array of name of the Jewish Months. Month numbers correspond to the array index, so  Nissan is JewishMonthNamesEnglish[1] etc.
        /// </summary>
        public static string[] JewishMonthNamesEnglish = { "", "Nissan", "Iyar", "Sivan", "Tamuz", "Av", "Ellul", "Tishrei", "Cheshvan", "Kislev", "Teves", "Shvat", "Adar", "Adar Sheini" };

        /// <summary>
        /// Array of Hebrew names of the Jewish Months. Month numbers correspond to the array index, so  ניסן is JewishMonthNamesHebrew[1] etc.
        /// </summary>
        public static string[] JewishMonthNamesHebrew = { "", "ניסן", "אייר", "סיון", "תמוז", "אב", "אלול", "תשרי", "חשון", "כסלו", "טבת", "שבט", "אדר", "אדר שני" };

        #endregion Public Fields

        #region Private Fields

        private static readonly string[] _hebsingles = { "", "אחד", "שנים", "שלשה", "ארבעה", "חמשה", "ששה", "שבעה", "שמונה", "תשעה" };
        private static readonly string[] _hebTens = { "", "עשר", "עשרים", "שלושים", "ארבעים" };
        private static readonly char[] _hundreds = new char[] { 'ק', 'ר', 'ש', 'ת' };
        private static readonly char[] _sings = new char[] { 'א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט' };
        private static readonly char[] _tens = new char[] { 'י', 'כ', 'ל', 'מ', 'נ', 'ס', 'ע', 'פ', 'צ' };

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Returns the nusach for Sefiras Ha'omer for the given day and minhag
        /// </summary>
        /// <param name="dayOfOmer">The day of the Omer for which to get the nusach for</param>
        /// <param name="nusach">Should it be La'Omer (Nusach Sefard) or Ba'Omer (Nusach Ashkenaz) or Sfardi Nusach (Eidot Hamizrach)?</param>        
        /// <returns></returns>
        public static string GetOmerNusach(int dayOfOmer, Nusach nusach)
        {
            int weeks = Convert.ToInt32(dayOfOmer / 7),
                days = dayOfOmer % 7;
            string txt = "היום ";

            if (dayOfOmer == 1)
            {
                txt += "יום אחד ";
            }
            else
            {
                if (dayOfOmer == 2)
                {
                    txt += "שני ";
                }
                else
                {
                    if (dayOfOmer == 10)
                    {
                        txt += "עשרה ";
                    }
                    else
                    {
                        txt += _hebsingles[(dayOfOmer % 10)] + " ";
                        if (dayOfOmer > 10)
                        {
                            if (dayOfOmer > 20 && ((dayOfOmer % 10) > 0))
                            {
                                txt += "ו";
                            }
                            txt += _hebTens[dayOfOmer / 10] + " ";
                        }
                    }
                }
                txt += (dayOfOmer >= 11 ? "יום" : "ימים") + " ";

                if (nusach == Nusach.Sefardi)
                {
                    txt += "לעומר" + " ";
                }

                if (dayOfOmer >= 7)
                {
                    txt += "שהם ";
                    if (weeks == 1)
                    {
                        txt += "שבוע אחד ";
                    }
                    else if (weeks == 2)
                    {
                        txt += "שני שבועות ";
                    }
                    else if (weeks > 0)
                    {
                        txt += _hebsingles[weeks] + " שבועות ";
                    }
                    if (days == 1)
                    {
                        txt += "ויום אחד ";
                    }
                    else if (days == 2)
                    {
                        txt += "ושני ימים ";
                    }
                    else if (days > 0)
                    {
                        txt += "ו" + _hebsingles[days] + " ימים ";
                    }
                }
            }
            if (nusach == Nusach.Sefard)
            {
                txt += "לעומר";
            }
            else if (nusach == Nusach.Ashkenaz)
            {
                txt += "בעומר";
            }
            return txt;
        }

        /// <summary>
        /// Determine if this object is contained in a list of objects
        /// </summary>
        /// <param name="obj">The object to test</param>
        /// <param name="list">Any number of parameters which together make up the list of objects to look through</param>
        /// <returns>True; if the item is in the parameter list. Otherwise, False</returns>
        public static bool In(this object obj, params object[] list)
        {
            return Array.IndexOf(list, obj) > -1;
        }

        /// <summary>
        /// Determine if the given SpecialDayType contains the given type. Equivalent to Enum.HasFlag.
        /// </summary>
        /// <param name="specialDayType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsSpecialDayType(this SpecialDayTypes specialDayType, SpecialDayTypes value)
        {
            return (specialDayType & value) == value;
        }

        /// <summary>
        /// Determines if the given Gregorian date and time is within the rules for DST.
        /// If no time zone info is available; if the location is in Israel, the current Israeli rules are used.
        /// Otherwise, the local system rules are used. [This may be very incorrect if the user is viewing any other location but the local system one]
        /// </summary>
        /// <param name="date">The secular date</param>
        /// <param name="location">Where in the world?</param>
        /// <returns>True if the given date and time is DST for the given location, otherwise False.</returns>
        public static bool IsDateTimeDST(DateTime date, Location location)
        {
            if (location != null && location.TimeZoneInfo != null)
            {
                return location.TimeZoneInfo.IsAmbiguousTime(date) || location.TimeZoneInfo.IsDaylightSavingTime(date);
            }
            else if (location.IsInIsrael)
            {
                return IsIsraelDst(date);
            }
            else
            {
                //Not sure about this. Should we return the current system setting or US rules?                
                //return IsUsaDst(date);
                return TimeZoneInfo.Local.IsAmbiguousTime(date) || TimeZoneInfo.Local.IsDaylightSavingTime(date);
            }
        }

        /// <summary>
        /// Converts a number into its Jewish number equivalent. I.E. 254 is רכ"ד
        /// NOTE: The exact thousands numbers (1000, 2000, 3000 etc.)
        /// will look awfully similar to the single digits, but will be formatted with a double apostrophe I.E. 2000 = "''ב"
        /// </summary>
        /// <param name="number">The number to convert</param>
        /// <returns>A Hebrew string representation of the number</returns>
        public static string ToNumberHeb(this int number)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Min value is 1");
            }

            if (number > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(number), "Max value is 9999");
            }

            int n = number;
            var retval = new StringBuilder();

            if (n >= 1000)
            {
                retval.AppendFormat("{0}'", _sings[((n - (n % 1000)) / 1000) - 1]);
                n = n % 1000;
            }

            while (n >= 400)
            {
                retval.Append('ת');
                n -= 400;
            }

            if (n >= 100)
            {
                retval.Append(_hundreds[((n - (n % 100)) / 100) - 1]);
                n = n % 100;
            }

            if (n == 15)
            {
                retval.Append("טו");
            }
            else if (n == 16)
            {
                retval.Append("טז");
            }
            else
            {
                if (n > 9)
                {
                    retval.Append(_tens[((n - (n % 10)) / 10) - 1]);
                }
                if ((n % 10) > 0)
                {
                    retval.Append(_sings[(n % 10) - 1]);
                }
            }
            if (number > 999 && (number % 1000 < 10))
            {
                retval.Insert(0, "'");
            }
            else if (retval.Length == 1)
            {
                retval.Append("'");
            }
            else
            {
                retval = retval.Insert(retval.Length - 1, "\"");
            }

            return retval.ToString();
        }

        /// <summary>
        /// Add two character suffix to number. e.g. 21st, 102nd, 93rd, 500th
        /// </summary>
        /// <param name="num">The number to add the suffix to</param>
        /// <returns>A string representation of the number as a sequence item</returns>
        public static string ToSuffixedString(this int num)
        {
            string t = num.ToString();
            string suffix = "th";
            if (t.Length == 1 || (t[t.Length - 2] != '1'))
            {
                switch (t[t.Length - 1])
                {
                    case '1':
                        suffix = "st";
                        break;

                    case '2':
                        suffix = "nd";
                        break;

                    case '3':
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }
            return t + suffix;
        }

        /// <summary>
        /// Gets the "proper" name for the given Jewish Month.
        /// This means for a leap year, labeling each of the the 2 Adars.
        /// </summary>
        /// <param name="jYear"></param>
        /// <param name="jMonth"></param>
        /// <returns></returns>
        public static string GetProperMonthName(int jYear, int jMonth)
        {
            if (jMonth == 12 && JewishDateCalculations.IsJewishLeapYear(jYear))
            {
                return "Adar Rishon";
            }
            else
            {
                return JewishMonthNamesEnglish[jMonth];
            }
        }

        /// <summary>
        /// Gets the "proper" name in Hebrew for the given Jewish Month.
        /// This means for a leap year, labeling each of the the 2 Adars.
        /// </summary>
        /// <param name="jYear"></param>
        /// <param name="jMonth"></param>
        /// <returns></returns>
        public static string GetProperMonthNameHeb(int jYear, int jMonth)
        {
            if (jMonth == 12 && JewishDateCalculations.IsJewishLeapYear(jYear))
            {
                return "אדר ראשון";
            }
            else
            {
                return JewishMonthNamesHebrew[jMonth];
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Get day of week using Zellers algorithm.
        /// </summary>
        /// <remarks>Day zero is Sunday</remarks>
        /// <param name="year">The secular year</param>
        /// <param name="month">The secular month</param>
        /// <param name="day">The secular day</param>
        /// <returns>The day of week index. Sunday is 0.</returns>
        private static int getDOW(int year, int month, int day)
        {
            int adjustment = (14 - month) / 12,
                mm = month + 12 * adjustment - 2,
                yy = year - adjustment;
            return (day + (13 * mm - 1) / 5 + yy + yy / 4 - yy / 100 + yy / 400) % 7;
        }

        /// <summary>
        /// Determine if the given secular date and time is during Daylight Savings Time using the (current [2015]) Israeli rules.
        /// </summary>
        /// <param name="date">The given date and time</param>
        /// <returns>Whether or not the given DateTime is during Daylight Savings Time</returns>
        public static bool IsIsraelDst(DateTime date)
        {
            int year = date.Year, month = date.Month, day = date.Day, hour = date.Hour;

            if (month > 10 || month < 3)
            {
                return false;
            }
            else if (month > 3 && month < 10)
            {
                return true;
            }
            //DST starts at 2 AM on the Friday before the last Sunday in March
            else if (month == 3)
            {
                //Gets date of the Friday before the last Sunday
                int lastFriday = (31 - getDOW(year, 3, 31)) - 2;
                return (day > lastFriday || (day == lastFriday && hour >= 2));
            }
            //DST ends at 2 AM on the last Sunday in October
            else
            {
                //We are in October.
                //Get the date of last Sunday in October
                int lastSunday = 31 - getDOW(year, 10, 31);
                return (day < lastSunday || (day == lastSunday && hour < 2));
            }
        }

        /// <summary>
        /// Determine if the given secular date and time is during Daylight Savings Time using the USA rules.
        /// </summary>
        /// <param name="date">The given date and time</param>
        /// <returns>Whether or not the given DateTime is during Daylight Savings Time</returns>
        public static bool IsUsaDst(DateTime date)
        {
            int year = date.Year, month = date.Month, day = date.Day, hour = date.Hour;

            if (month < 3 || month == 12)
            {
                return false;
            }
            else if (month > 3 && month < 11)
            {
                return true;
            }
            //DST starts at 2:00 AM on the second Sunday in March
            else if (month == 3)
            {
                //Gets day of week on March 1st
                int firstDOW = getDOW(year, 3, 1);
                //Gets date of second Sunday
                int targetDate = firstDOW == 0 ? 8 : ((7 - (firstDOW + 7) % 7)) + 8;

                return (day > targetDate || (day == targetDate && hour >= 2));
            }
            //DST ends at 2:00 AM on the first Sunday in November
            else //dt.Month == 11
            {
                //Gets day of week on November 1st
                int firstDOW = getDOW(year, 11, 1);
                //Gets date of first Sunday
                int targetDate = firstDOW == 0 ? 1 : ((7 - (firstDOW + 7) % 7)) + 1;

                return (day < targetDate || (day == targetDate && hour < 2));
            }
        }

        #endregion Private Methods
    }
}