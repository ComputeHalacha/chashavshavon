using System;

namespace JewishCalendar
{
    /// <summary>
    /// Represents a single day in the Jewish calendar - Month are Nissan based.
    /// </summary>
    /// <remarks>
    /// The calculations and functions used by this class's representation of the Jewish Date are based on open source algorithms.
    /// 
    /// <list type="numeric">
    /// <listheader>This class has 3 main advantages over Globalization.HebrewCalendar:</listheader>
    /// 
    /// <item>
    /// The Months are numbered from Nissan. The regular .NET class Globalization.HebrewCalendar has Tishrei as month #1.
    /// This becomes confusing, as months after Adar get a different number -
    /// depending on whether the year is a leap year or not.
    /// The Torah also instructs us to call Nissan the first month. (See Ramban in Drasha for Rosh Hashana)
    /// Hence this "Nissan first" Jewish Date class.
    /// </item>
    /// 
    /// <item>
    /// It can represent any Jewish Date from creation until the Jewish year 6000. 
    /// Globalization.HebrewCalendar can only represent dates starting from the Gregorian year 1583.
    /// </item>
    /// 
    /// <item>
    /// It can be used for projects which do not have access to Globalization.HebrewCalendar,
    /// such as .NET Micro Framework projects etc.
    /// </item>
    /// </list>     
    /// </remarks>
    [Serializable]
    public class JewishDate
    {
        #region Private Variables
        private int _day, _month, _year, _absoluteDate;
        private DateTime _gregorianDate;
        #endregion

        #region Constructors
        /// <summary>
        /// static constructor
        /// </summary>
        static JewishDate()
        {
            MinDate = new JewishDate(1, 7, 1);
            MaxDate = new JewishDate(5999, 6, 29);
        }

        /// <summary>
        /// Empty constructor. Sets the date to the current system date.
        /// </summary>
        public JewishDate() : this(DateTime.Now) { }

        /// <summary>
        /// Get the current Jewish date in the given location. Cut-off time is sunset.
        /// </summary>
        /// <param name="location">The location. This will be used to determine the time of sunset.</param>
        public JewishDate(Location location) : this(DateTime.Now, location) { }

        /// <summary>
        /// Creates a new JewishDate with the specified Hebrew year, month, day and absolute day.        
        /// Caution: If the absolute day doesn't correctly match the given year/month/day, weird things will happen.
        /// </summary>
        /// <param name="year">The year - counted from the creation of the world</param>
        /// <param name="month">The Jewish month. As it is in the Torah, Nissan is 1.</param>
        /// <param name="day">The day of the month</param>
        /// <param name="absoluteDay">The "absolute day"</param>
        public JewishDate(int year, int month, int day, int absoluteDay)
        {
            this._year = year;
            this._month = month;
            this._day = day;
            this._absoluteDate = absoluteDay;
            this._gregorianDate = JewishDateCalculations.GetGregorianDateFromJewishDate(this);
        }

        /// <summary>
        /// Creates a new JewishDate with the specified Hebrew year, month, day, absolute day and Gregorian Date.
        /// This is the quickest constructor as it does no calculations at all. 
        /// Caution: If the absolute day doesn't correctly match the given year/month/day, weird things will happen.
        /// </summary>
        /// <param name="year">The year - counted from the creation of the world</param>
        /// <param name="month">The Jewish month. As it is in the Torah, Nissan is 1.</param>
        /// <param name="day">The day of the month</param>
        /// <param name="absoluteDay">The "absolute day"</param>
        /// <param name="gd">The secular date</param>
        public JewishDate(int year, int month, int day, int absoluteDay, DateTime gd)
        {
            this._year = year;
            this._month = month;
            this._day = day;
            this._absoluteDate = absoluteDay;
            this._gregorianDate = gd;
        }

        /// <summary>
        /// Creates a new JewishDate with the specified Hebrew year, month and day
        /// </summary>
        /// <param name="year">The year - counted from the creation of the world</param>
        /// <param name="month">The Jewish month. As it is in the Torah, Nissan is 1.</param>
        /// <param name="day">The day of the month</param>
        public JewishDate(int year, int month, int day) :
            this(year, month, day, JewishDateCalculations.GetAbsoluteFromJewishDate(year, month, day))
        { }

        /// <summary>
        /// Creates a Jewish date that corresponds to the given Gregorian date
        /// </summary>
        /// <param name="date">The Gregorian date from which to create the Jewish Date</param>
        public JewishDate(DateTime date)
        {
            this.SetFromAbsoluteDate(JewishDateCalculations.GetAbsoluteFromGregorianDate(date));
            this._gregorianDate = date;
            this.TimeOfDay = date.TimeOfDay;
        }

        /// <summary>
        /// Creates a Jewish date that corresponds to the given Gregorian date in the given location. Cut-off time is sunset.
        /// </summary>
        /// <param name="date">The Gregorian date from which to create the Jewish Date</param>
        /// <param name="location">The location. This will be used to determine the time of sunset.</param>
        public JewishDate(DateTime date, Location location)
        {
            int abs = JewishDateCalculations.GetAbsoluteFromGregorianDate(date);
            var zman = new Zmanim(date, location);

            if (zman.GetShkia() <= date.TimeOfDay)
            {
                abs++;
            }

            this.SetFromAbsoluteDate(abs);
            this._gregorianDate = date;
            this.TimeOfDay = date.TimeOfDay;
        }

        /// <summary>
        /// Creates a Hebrew date from the "absolute date".
        /// In other words, the Hebrew date on the day that is the given number of days after/before December 31st, 1 BCE
        /// </summary>
        /// <param name="absoluteDate">The number of days elapsed since the theoretical Gregorian date Sunday, December 31, 1 BCE.
        /// Since there is no year 0 in the calendar, the year following 1 BCE is 1 CE.
        /// So, the Gregorian date January 1, 1 CE is absolute date number 1.</param>
        public JewishDate(int absoluteDate)
        {
            this.SetFromAbsoluteDate(absoluteDate);
            this._gregorianDate = JewishDateCalculations.GetGregorianDateFromJewishDate(this);
        }

        /// <summary>
        /// Sets the current Jewish date to the date represented by the given "Absolute Date" -
        /// which is the number of days after/before December 31st, 1 BCE.
        /// The logic here was translated from the C code - which in turn were translated
        /// from the Lisp code in ''Calendrical Calculations'' by Nachum Dershowitz and Edward M. Reingold in
        /// Software---Practice &amp; Experience, vol. 20, no. 9 (September, 1990), pp. 899--928.
        /// </summary>
        /// <param name="absoluteDate"></param>
        private void SetFromAbsoluteDate(int absoluteDate)
        {
            this._absoluteDate = absoluteDate;

            //To save on calculations, start with an estimation of a few years before date
            this._year = 3761 + (absoluteDate / (absoluteDate > 0 ? 366 : 300));

            //The following is from the original code; it starts the calculations way back when and takes almost as long to calculate all of them...
            //this._year = ((absoluteDate + JewishDateCalculations.HEBREW_EPOCH) / 366); // Approximation from below.

            // Search forward for year from the approximation.
            while (absoluteDate >= JewishDateCalculations.GetAbsoluteFromJewishDate((this._year + 1), 7, 1))
            {
                this._year++;
            }
            // Search forward for month from either Tishrei or Nissan.
            if (absoluteDate < JewishDateCalculations.GetAbsoluteFromJewishDate(this._year, 1, 1))
            {
                this._month = 7; //  Start at Tishrei
            }
            else
            {
                this._month = 1; //  Start at Nissan
            }
            while (absoluteDate > JewishDateCalculations.GetAbsoluteFromJewishDate(
                this._year,
                this._month,
                (JewishDateCalculations.DaysInJewishMonth(this._year, this._month))))
            {
                this._month++;
            }
            // Calculate the day by subtraction.
            this._day = (absoluteDate - JewishDateCalculations.GetAbsoluteFromJewishDate(this._year, this._month, 1) + 1);
        }

        #endregion Constructors

        #region Public Properties
        /// <summary>
        /// The number of days elapsed since the theoretical Gregorian date Sunday, December 31, 1 BCE.
        /// Since there is no year 0 in the calendar, the year following 1 BCE is 1 CE.
        /// So, the Gregorian date January 1, 1 CE is absolute date number 1.
        /// </summary>
        public int AbsoluteDate
        {
            get => this._absoluteDate;
            set
            {
                if (value != this._absoluteDate)
                {
                    this.SetFromAbsoluteDate(value);
                    this._gregorianDate = JewishDateCalculations.GetGregorianDateFromJewishDate(this);
                }
            }
        }

        /// <summary>
        /// The Day in the month for this Jewish Date.
        /// </summary>
        public int Day => this._day;

        /// <summary>
        /// The index of the day of the week for this Jewish Date. Sunday is 0.
        /// </summary>
        public int DayInWeek => Math.Abs(this.AbsoluteDate % 7);

        /// <summary>
        /// The day of the week for this Jewish Date
        /// </summary>
        public DayOfWeek DayOfWeek => (DayOfWeek)this.DayInWeek;

        /// <summary>
        /// The Jewish Month. As in the Torah, Nissan is month 1
        /// </summary>
        public int Month => this._month;

        /// <summary>
        /// The name of the current Jewish Month (in English)
        /// </summary>
        public string MonthName => Utils.GetProperMonthName(this._year, this._month);

        /// <summary>
        /// The number of years since creation
        /// </summary>
        public int Year => this._year;

        /// <summary>
        /// Represents the time of day for this JewishDate
        /// </summary>
        public TimeSpan TimeOfDay { get; set; }

        /// <summary>
        /// Get the Gregorian Date for the current Hebrew Date
        /// </summary>
        /// <returns></returns>
        public DateTime GregorianDate
        {
            get => this._gregorianDate;
            set
            {
                if (this._gregorianDate != value)
                {
                    this._gregorianDate = value;
                    this.SetFromAbsoluteDate(JewishDateCalculations.GetAbsoluteFromGregorianDate(value));
                    this.TimeOfDay = value.TimeOfDay;
                }
            }
        }

        /// <summary>
        /// The Hour component of the time of day represented by this Jewish Date
        /// </summary>
        public int Hour => this.TimeOfDay.Hours;

        /// <summary>
        /// The Minute component of the time of day represented by this Jewish Date
        /// </summary>
        public int Minute => this.TimeOfDay.Minutes;

        /// <summary>
        /// The Second component of the time of day represented by this Jewish Date
        /// </summary>
        public int Second => this.TimeOfDay.Seconds;

        /// <summary>
        /// The Millisecond component of the time of day represented by this Jewish Date
        /// </summary>
        public int Millisecond => this.TimeOfDay.Milliseconds;

        /// <summary>
        /// Minimum valid date that can be represented by this class
        /// </summary>
        /// 
        public static JewishDate MinDate { get; private set; }
        /// <summary>
        /// Maximum valid date that can be represented by this class
        /// </summary>
        public static JewishDate MaxDate { get; private set; }

        #endregion Public Properties

        #region Public Functions
        /// <summary>
        /// Returns true if both objects have the same day, month and year. You can also use the == operator or the extension method IsSameDate(JewishDate js) for the same purpose.
        /// </summary>
        /// <param name="jd2"></param>
        /// <returns></returns>
        public override bool Equals(object jd2)
        {
            if (!(jd2 is JewishDate))
            {
                return false;
            }
            if (ReferenceEquals(this, jd2))
            {
                return true;
            }
            return this.IsSameDate((JewishDate)jd2);
        }

        /// <summary>
        /// Gets the difference in months between two JewishDates. 
        /// If the second date is before this one, the number will be negative.
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        /// <remarks>Ignores Day part. For example, from 29 Kislev to 1 Teves will 
        /// return 1 even though they are only a day or two apart</remarks>
        public int DateDiffMonth(JewishDate jd)
        {
            int month = jd._month,
             year = jd._year,
             months = 0;

            while (!(year == this._year && month == this._month))
            {
                if (this.AbsoluteDate > jd.AbsoluteDate)
                {
                    months--;
                    month++;
                    if (month > JewishDateCalculations.MonthsInJewishYear(year))
                    {
                        month = 1;
                    }
                    else if (month == 7)
                    {
                        year++;
                    }
                }
                else
                {
                    months++;
                    month--;
                    if (month < 1)
                    {
                        month = JewishDateCalculations.MonthsInJewishYear(year);
                    }
                    else if (month == 6)
                    {
                        year--;
                    }
                }
            }

            return months;
        }

        /// <summary>
        /// Adds the given number of months to the current date and returns the new Jewish Date
        /// </summary>
        /// <param name="months"></param>
        /// <returns></returns>
        public JewishDate AddMonths(int months)
        {
            int year = this._year,
                month = this._month,
                day = this._day,
                miy = JewishDateCalculations.MonthsInJewishYear(year);

            for (int i = 0; i < Math.Abs(months); i++)
            {
                if (months > 0)
                {
                    month += 1;
                    if (month > miy)
                    {
                        month = 1;
                    }
                    if (month == 7)
                    {
                        year += 1;
                        miy = JewishDateCalculations.MonthsInJewishYear(year);
                    }
                }
                else if (months < 0)
                {
                    month -= 1;
                    if (month == 0)
                    {
                        month = miy;
                    }
                    if (month == 6)
                    {
                        year -= 1;
                        miy = JewishDateCalculations.MonthsInJewishYear(year);
                    }
                }
            }
            return new JewishDate(year, month, day);
        }

        /// <summary>
        /// Adds the given number of years to the current date and returns the new Jewish Date
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        /// <remarks>If the current month is Adar Sheini and the new year is not a leap year, the month is set to Adar.
        /// If the current Day is the 30th of Cheshvan or Kislev and in the new year that month only has 29 days, 
        /// the day is set to the 1st of the following month.
        /// </remarks>
        public JewishDate AddYears(int years)
        {
            int year = this._year + years,
                month = this._month,
                day = this._day;

            if (month == 13 && !JewishDateCalculations.IsJewishLeapYear(year))
            {
                month = 12;
            }
            else if (month == 8 && day == 30 && !JewishDateCalculations.IsLongCheshvan(year))
            {
                month = 9;
                day = 1;
            }
            else if (month == 9 && day == 30 && JewishDateCalculations.IsShortKislev(year))
            {
                month = 10;
                day = 1;
            }
            return new JewishDate(year, month, day);
        }

        /// <summary>
        /// Returns the day of the Omer for the given Jewish date. If the given day is not during Sefirah, 0 is returned
        /// </summary>
        /// <returns></returns>
        public int GetDayOfOmer()
        {
            int dayOfOmer = 0;
            if ((this._month == 1 && this._day > 15) || this._month == 2 || (this._month == 3 && this._day < 6))
            {
                dayOfOmer = (this - new JewishDate(this._year, 1, 15));
            }
            return dayOfOmer;
        }

        /// <summary>
        /// Returns the HashCode for this instance
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this._year.GetHashCode() ^ this._month.GetHashCode() ^ this._day.GetHashCode();
        }

        /// <summary>
        /// Returns the Jewish date in the format: The 14th day of Adar, 5775
        /// </summary>
        /// <returns></returns>
        public string ToLongDateString()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append("The ");
            sb.Append(this._day.ToSuffixedString());
            sb.Append(" day of ");
            sb.Append(this.MonthName);
            sb.Append(", " + this._year.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// Returns the Jewish date in the format: יום חמישי כ"ט תשרי תשע"ה
        /// </summary>
        /// <returns></returns>
        public string ToLongDateStringHeb()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(Utils.JewishDOWNames[this.DayInWeek]);
            sb.Append(" ");
            sb.Append(this.ToShortDateStringHeb());
            return sb.ToString();
        }

        /// <summary>
        /// Returns the Jewish date in the format: Adar 14, 5775
        /// </summary>
        /// <returns></returns>
        public string ToShortDateString()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(this.MonthName);
            sb.Append(" " + this._day);
            sb.Append(", " + this._year.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// Returns the Jewish date in the format: כ"ו אלול תשע"ה
        /// </summary>
        /// <returns></returns>
        public string ToShortDateStringHeb()
        {
            var sb = new System.Text.StringBuilder();
            //Note for the .net micro framework there are no "format" functions
            sb.Append(this._day.ToNumberHeb());
            sb.Append(" ");
            sb.Append(Utils.GetProperMonthNameHeb(this._year, this._month));
            sb.Append(" ");
            sb.Append((this._year % 1000).ToNumberHeb());
            return sb.ToString();
        }

        /// <summary>
        /// Returns the Jewish date in the format: Adar 14, 5775
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToShortDateString();
        }

        #endregion Public Functions

        #region Operator Functions

        /// <summary>
        /// Subtract days from a Jewish date.
        /// </summary>
        /// <param name="hd"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static JewishDate operator -(JewishDate hd, int days)
        {
            return new JewishDate(hd.AbsoluteDate - days);
        }

        /// <summary>
        /// Gets the difference in days between two Jewish dates.
        /// </summary>
        /// <param name="hd"></param>
        /// <param name="hd2"></param>
        /// <returns></returns>
        public static int operator -(JewishDate hd, JewishDate hd2)
        {
            return hd.AbsoluteDate - hd2.AbsoluteDate;
        }

        /// <summary>
        /// Returns true if both objects do not have the same day, month and year
        /// </summary>
        /// <param name="jd1"></param>
        /// <param name="jd2"></param>
        /// <returns></returns>
        public static bool operator !=(JewishDate jd1, JewishDate jd2)
        {
            return !(jd1 == jd2);
        }

        /// <summary>
        /// Add days to a Jewish date.
        /// </summary>
        /// <param name="hd"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static JewishDate operator +(JewishDate hd, int days)
        {
            return new JewishDate(hd.AbsoluteDate + days);
        }

        /// <summary>
        /// Returns true if the current JewishDateMicro object is chronologically before the second JewishDate object
        /// </summary>
        /// <param name="jd1"></param>
        /// <param name="jd2"></param>
        /// <returns></returns>
        public static bool operator <(JewishDate jd1, JewishDate jd2)
        {
            if (jd1 == null || jd2 == null)
            {
                return false;
            }
            return jd1.AbsoluteDate < jd2.AbsoluteDate;
        }

        /// <summary>
        /// Returns true if the current JewishDateMicro object is not chronologically later than the second JewishDate object
        /// </summary>
        /// <param name="jd1"></param>
        /// <param name="jd2"></param>
        /// <returns></returns>
        public static bool operator <=(JewishDate jd1, JewishDate jd2)
        {
            if (jd1 == null || jd2 == null)
            {
                return false;
            }
            if (jd1 == jd2)
            {
                return true;
            }
            return jd1 < jd2;
        }

        /// <summary>
        /// Returns true if both objects have the same day, month and year. You can also use the Equals function or the extension method IsSameDate(JewishDate js) for the same purpose.
        /// </summary>
        /// <param name="jd1"></param>
        /// <param name="jd2"></param>
        /// <returns></returns>
        public static bool operator ==(JewishDate jd1, JewishDate jd2)
        {
            if (ReferenceEquals(jd1, null))
            {
                return ReferenceEquals(jd2, null);
            }
            return jd1.Equals(jd2);
        }

        /// <summary>
        /// Returns true if the current JewishDateMicro object is chronologically after the second JewishDate object
        /// </summary>
        /// <param name="jd1"></param>
        /// <param name="jd2"></param>
        /// <returns></returns>
        public static bool operator >(JewishDate jd1, JewishDate jd2)
        {
            if (jd1 == null || jd2 == null || jd1 == jd2)
            {
                return false;
            }
            return jd1.AbsoluteDate > jd2.AbsoluteDate;
        }

        /// <summary>
        /// Returns true if the current JewishDateMicro object is not chronologically earlier than the second JewishDate object
        /// </summary>
        /// <param name="jd1"></param>
        /// <param name="jd2"></param>
        /// <returns></returns>
        public static bool operator >=(JewishDate jd1, JewishDate jd2)
        {
            if (jd1 == null || jd2 == null)
            {
                return false;
            }
            if (jd1 == jd2)
            {
                return true;
            }
            return jd1 > jd2;
        }

        #endregion Operator Functions
    }
}