using System;
using System.Globalization;

namespace Tahara
{
    public static class Utils
    {
        private static readonly char[] _jsd = { 'א', 'ב', 'ג', 'ד', 'ה', 'ו', 'ז', 'ח', 'ט' };
        private static readonly char[] _jtd = { 'י', 'כ', 'ל', 'מ', 'נ', 'ס', 'ע', 'פ', 'צ' };
        private static readonly char[] _jhd = { 'ק', 'ר', 'ש', 'ת' };

        public static readonly HebrewCalendar HebrewCalendar = new HebrewCalendar();
        public static readonly CultureInfo CultureInfo = new CultureInfo("he-IL", false);
        public static string[] DaysOfWeekHebrewFull = { "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת קודש" };
        public static string[] DaysOfWeekHebrew = { "יום א", "יום ב", "יום ג", "יום ד", "יום ה", "יום ו", "שבת" };
        //To translate a day number into a hebrew date - days start at 1, not 0.
        public static string[] DaysOfMonthHebrew = { "", "א'", "ב'", "ג'", "ד'", "ה'", "ו'", "ז'", "ח'", "ט'", "י'", "י\"א", "י\"ב", "י\"ג", "י\"ד", "ט\"ו", "ט\"ז", "י\"ז", "י\"ח", "י\"ט", "כ'", "כ\"א", "כ\"ב", "כ\"ג", "כ\"ד", "כ\"ה", "כ\"ו", "כ\"ז", "כ\"ח", "כ\"ט", "ל'" };

        /// <summary>
        /// Gets the Jewish representation of a number (365 = שס"ה)
        /// Minimum number is 1 and maximum is 9999.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToJNum(this int number)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException("Min value is 1");
            }

            if (number > 9999)
            {
                throw new ArgumentOutOfRangeException("Max value is 9999");
            }

            int n = number;
            string retval = "";

            if (n >= 1000)
            {
                retval += _jsd[(n - (n % 1000)) / 1000 - 1].ToString() + '\'';
                n %= 1000;
            }

            while (n >= 400)
            {
                retval += 'ת';
                n -= 400;
            }

            if (n >= 100)
            {
                retval += _jhd[(n - (n % 100)) / 100 - 1].ToString();
                n %= 100;
            }

            if (n == 15)
            {
                retval += "טו";
            }
            else if (n == 16)
            {
                retval += "טז";
            }
            else
            {
                if (n > 9)
                {
                    retval += _jtd[(n - (n % 10)) / 10 - 1].ToString();
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
    }
}
