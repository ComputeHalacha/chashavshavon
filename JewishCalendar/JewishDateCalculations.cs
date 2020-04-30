using System;
using System.Collections.Generic;

namespace JewishCalendar
{
    /// <summary>
    /// Static class that contains functions for Jewish calendar calculations.
    /// </summary>
    /// <remarks>This class does much of the actual Jewish Date calculations for the <see cref="JewishDate">JewishDate</see> class.
    /// The calculations do not use System.Globalization.HebrewCalendar so they can be used with .NET micro projects.
    /// Most of the Jewish date logic and calculations were translated from the C code
    /// which in turn were translated from the Lisp code in "Calendrical Calculations" by Nachum Dershowitz and Edward M. Reingold
    /// in Software---Practice &amp; Experience, vol. 20, no. 9 (September, 1990), pp. 899--928.
    /// </remarks>
    public static class JewishDateCalculations
    {
        /// <summary>
        /// To save on calculations, a table of years and their elapsed days values is saved in memory
        /// </summary>
        private static readonly Dictionary<int, int> _yearCache = new Dictionary<int, int>();

        /// <summary>
        /// Absolute date of start of Jewish calendar
        /// </summary>
        public const int HEBREW_EPOCH = -1373429;

        /// <summary>
        /// Determines if the given Jewish Year is a Leap Year
        /// </summary>
        /// <param name="year"></param>
        /// <returns>True if the year is a leap year and False if not</returns>
        /// <remarks>This function is identical to
        /// <see cref="JewishDateCalculations.IsJewishLeapYear(int)">JewishDate.IsLeapYear</see>.
        /// The only difference is, the JewishDate class's version uses Globalization.HebrewCalendars.IsLeapYear 
        /// which adds a check to make sure that the year is within the range of the HebrewCalendar class.
        /// </remarks>
        public static bool IsJewishLeapYear(int year)
        {
            return (((7 * year) + 1) % 19) < 7;
        }

        /// <summary>
        /// Gets the number of months in the given Jewish year
        /// </summary>
        /// <param name="year">The Jewish Year for which to get the number of months for</param>
        /// <returns>The number of months in the given year</returns>        
        public static int MonthsInJewishYear(int year)
        {
            return IsJewishLeapYear(year) ? 13 : 12;
        }

        /// <summary>
        /// Does Cheshvan have a full 30 days in the given Jewish Year?
        /// </summary>
        /// <param name="year">The given Jewish Year</param>
        /// <returns>Whether or not Cheshvan has 30 days in the given year</returns>       
        public static bool IsLongCheshvan(int year)
        {
            return (DaysInJewishYear(year) % 10) == 5;
        }

        /// <summary>
        /// Does Kislev have 29 days for the given Jewish year?
        /// </summary>
        /// <param name="year">The given Jewish Year</param>
        /// <returns>Whether or not Kislev has 29 days in the given year</returns>        
        public static bool IsShortKislev(int year)
        {
            return (DaysInJewishYear(year) % 10) == 3;
        }

        /// <summary>
        /// Compute the number of days in the given Jewish month
        /// </summary>
        /// <param name="year">The Jewish year</param>
        /// <param name="month">The Nissan based Jewish Month (Nissan is 1 and Adar Sheini is 13)</param>
        /// <returns>The number of days in the given Jewish Month</returns>
        /// <remarks>If you are not using the .NET micro framework, use 
        /// <see cref="JewishDateCalculations.DaysInJewishMonth(int, int)">JewishDate.DaysInJewishMonth</see> 
        /// instead of this function.</remarks>
        public static int DaysInJewishMonth(int year, int month)
        {
            if ((month == 2) || (month == 4) || (month == 6) || ((month == 8) &&
                (!IsLongCheshvan(year))) || ((month == 9) && IsShortKislev(year)) || (month == 10) || ((month == 12) &&
                (!IsJewishLeapYear(year))) || (month == 13))
            {
                return 29;
            }
            else
            {
                return 30;
            }
        }

        /// <summary>
        /// Get the total number of days in the given Jewish year.
        /// From Rosh Hashana of the given year until the next Rosh Hashana.
        /// </summary>
        /// <param name="year">The given Jewish Year</param>
        /// <returns>The number of days in the given Jewish Year</returns>        
        public static int DaysInJewishYear(int year)
        {
            return ((GetElapsedDays(year + 1)) - (GetElapsedDays(year)));
        }

        /// <summary>
        /// Compares this Jewish Date to another one to see if they both represent the same Jewish calendar date.         
        /// </summary>
        /// <param name="jd1">This JewishDate</param>
        /// <param name="jd2">The JewishDate to test against this one</param>
        /// <returns>Whether or not the two represent the same Jewish calendar date</returns>
        public static bool IsSameDate(this JewishDate jd1, JewishDate jd2)
        {
            if (jd2 == null)
            {
                return false;
            }

            return jd1.Year == jd2.Year && jd1.Month == jd2.Month && jd1.Day == jd2.Day;
        }

        /// <summary>
        /// Gets the Gregorian date that starts at midnight of the given Jewish Date
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static DateTime GetGregorianDateFromJewishDate(JewishDate jd)
        {
            return GetGregorianDateFromAbsolute(jd.AbsoluteDate).Add(jd.TimeOfDay);
        }

        /// <summary>
        /// The number of days elapsed since the theoretical Gregorian date Sunday, December 31, 1 BCE.
        /// Since there is no year 0 in the calendar, the year following 1 BCE is 1 CE.
        /// So, the Gregorian date January 1, 1 CE is absolute date number 1.
        /// </summary>
        public static int GetAbsoluteFromJewishDate(int year, int month, int day)
        {
            int dayInYear = day; // Days so far this month.
            if (month < 7)
            { // Before Tishrei, so add days in prior months
                // this year before and after Nissan.
                int m = 7;
                while (m <= (MonthsInJewishYear(year)))
                {
                    dayInYear += DaysInJewishMonth(year, m);
                    m++;
                };
                m = 1;
                while (m < month)
                {
                    dayInYear += DaysInJewishMonth(year, m);
                    m++;
                }
            }
            else
            { // Add days in prior months this year
                int m = 7;
                while (m < month)
                {
                    dayInYear += DaysInJewishMonth(year, m);
                    m++;
                }
            }
            // Days elapsed before absolute date 1. -  Days in prior years.
            return dayInYear + (GetElapsedDays(year) + HEBREW_EPOCH);
        }

        /// <summary>
        /// Computed the absolute date for the given Gregorian Date
        /// NOTE: If you are not using the .NET micro framework, 
        /// you can use the following instead: (int)((YOUR_DATETIME.Subtract(new DateTime(1, 1, 1)).TotalDays + 1));
        /// </summary>
        /// <param name="date">The Gregorian Date</param>
        /// <returns></returns>
        public static int GetAbsoluteFromGregorianDate(DateTime date)
        {
            return GetAbsoluteFromGregorianDate(date.Year, date.Month, date.Day);
        }

        /// <summary>
        /// Computed the absolute date for the given Gregorian Year, Month and Day
        /// NOTE: If you are not using the .NET micro framework, 
        /// you can use the following instead: (int)((new DateTime(year, month, day).Subtract(new DateTime(1, 1, 1)).TotalDays + 1));
        /// </summary>
        /// <param name="year">The Gregorian Year</param>
        /// <param name="month">The Gregorian Month</param>
        /// <param name="day">The Gregorian Day</param>
        /// <returns></returns>
        public static int GetAbsoluteFromGregorianDate(int year, int month, int day)
        {
            int numberOfDays = day;           // days this month
            // add days in prior months this year
            for (int i = month - 1; i > 0; i--)
            {
                numberOfDays += DaysInGregorianMonth(i, year);
            }

            return (numberOfDays          // days this year
                   + 365 * (year - 1)     // days in previous years ignoring leap days
                   + (year - 1) / 4       // Julian leap days before this year...
                   - (year - 1) / 100     // ...minus prior century years...
                   + (year - 1) / 400);   // ...plus prior years divisible by 400
        }

        /// <summary>
        /// Calculates the Gregorian Date from an Absolute Date
        /// The Absolute Date is the number of days elapsed since the theoretical Gregorian date Sunday, December 31, 1 BCE.
        /// Since there is no year 0 in the calendar, the year following 1 BCE is 1 CE.
        /// So, the Gregorian date January 1, 1 CE is absolute date number 1.
        /// </summary>
        /// <param name="abs"></param>
        /// <returns></returns>
        public static DateTime GetGregorianDateFromAbsolute(int abs)
        {
            if (abs >= 730120) // 1/1/2000
            {
                return new DateTime(2000, 1, 1).AddDays(abs - 730120);
            }
            else
            {
                int day, month, year;

                // Search forward year by year from approximate year
                year = abs / 366;
                while (abs >= GetAbsoluteFromGregorianDate(year + 1, 1, 1))
                {
                    year++;
                }
                if (year < 1)
                {
                    return DateTime.MinValue;
                }
                else if (year > 9999)
                {
                    return DateTime.MaxValue;
                }
                // Search forward month by month from January
                month = 1;
                while (abs > GetAbsoluteFromGregorianDate(
                    year, month, DaysInGregorianMonth(month, year)))
                {
                    month++;
                }

                day = abs - GetAbsoluteFromGregorianDate(year, month, 1) + 1;

                return new DateTime(year, month, day);
            }
        }

        /// <summary>
        /// Compute the number of days in the given month of the Gregorian calendar.
        /// NOTE: If you are not using the .NET Micro framework, 
        /// you can use the GregorianCalendar.GetDaysInMonth function instead of this function
        /// </summary>
        /// <param name="month">The Gregorian Month</param>
        /// <param name="year">The Gregorian Year</param>
        /// <returns></returns>
        public static int DaysInGregorianMonth(int month, int year)
        {
            switch (month)
            {
                case 2:
                    if ((((year % 4) == 0) && ((year % 100) != 0))
                        || ((year % 400) == 0))
                    {
                        return 29;
                    }
                    else
                    {
                        return 28;
                    }

                case 4:
                case 6:
                case 9:
                case 11: return 30;
                default: return 31;
            }
        }

        /// <summary>
        /// Returns the correct Secular Date for a JewishDate at the given Time and Location.
        /// </summary>
        /// <param name="jd">The Jewish Date</param>
        /// <param name="location"></param>
        /// <param name="timeOfDay"></param>
        /// <returns></returns>
        /// <remarks>
        /// When using a JewishDate constructor that takes a "Location" object,
        /// if the initializing DateTime was after sunset, the Jewish date was set to the next day, but not the GregorianDate.
        /// The GregorianDate property therefore only represents the correct Secular Date for the original location and time.
        /// This function returns the correct GregorianDate for a JewishDate at the given time and place.
        /// </remarks>
        public static DateTime GetGregorianDateFromJewishDate(JewishDate jd, TimeOfDay timeOfDay, Location location)
        {
            //The Gregorian date that starts at midnight of the given Jewish Date
            DateTime gregDateAtMidnight = GetGregorianDateFromJewishDate(jd);

            //If given time is after mid-day (sunset is never, ever before mid-day; not even at the North and South Poles)
            //and given time is after sunset at the given location
            if (timeOfDay.Hour >= 12 && timeOfDay >= new Zmanim(jd.GregorianDate, location).GetShkia())
            {
                // From sunset to midnight:
                // Jewish today is Secular tomorrow and Secular Today is Jewish Yesterday
                // (please sir, keep your yarmulka on!) [double meanings all around]
                return gregDateAtMidnight.AddDays(-1);
            }
            else
            {
                return gregDateAtMidnight;
            }
        }

        /// <summary>
        /// Computes the number of days elapsed from the Sunday prior to the start of the
        /// Jewish calendar to the mean conjunction of Tishrei of the given Jewish year.
        /// </summary>
        /// <param name="year">The Jewish Year</param>
        /// <returns>The number of days elapsed</returns>
        private static int GetElapsedDays(int year)
        {
            if (_yearCache.ContainsKey(year))
            {
                return _yearCache[year];
            }

            int monthsElapsed = (235 * ((year - 1) / 19)) + (12 * ((year - 1) % 19)) + (7 * ((year - 1) % 19) + 1) / 19; // Leap months this cycle -  Regular months in this cycle. -  Months in complete cycles so far.
            int partsElapsed = 204 + 793 * (monthsElapsed % 1080);
            int hoursElapsed = 5 + 12 * monthsElapsed + 793 * (monthsElapsed / 1080) + partsElapsed / 1080;
            int conjunctionDay = (1 + 29 * monthsElapsed + hoursElapsed / 24);
            int conjunctionParts = 1080 * (hoursElapsed % 24) + partsElapsed % 1080;
            int alternativeDay = conjunctionDay;

            // at the end of a leap year -  15 hours, 589 parts or later... -  ...or is on a Monday at... -  ...of a common year, -  at 9 hours, 204 parts or later... -  ...or is on a Tuesday... -  If new moon is at or after midday,
            if ((conjunctionParts >= 19440) ||
                (((conjunctionDay % 7) == 2) && (conjunctionParts >= 9924) && (!IsJewishLeapYear(year))) ||
                (((conjunctionDay % 7) == 1) && (conjunctionParts >= 16789) && (IsJewishLeapYear(year - 1))))
            {
                // Then postpone Rosh HaShanah one day
                alternativeDay += 1;
            }

            if ((alternativeDay % 7).In(0, 3, 5)) // or Friday -  or Wednesday, -  If Rosh HaShanah would occur on Sunday,
            {
                // Then postpone it one (more) day
                alternativeDay += 1;
            }

            _yearCache.Add(year, alternativeDay);

            return alternativeDay;
        }
    }
}