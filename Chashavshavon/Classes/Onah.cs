using System;
using System.Collections.Generic;
using System.Text;
using Chashavshavon.Utils;

namespace Chashavshavon
{
    public class Onah
    {
        private static string[] _monthNames;
        private static System.Globalization.HebrewCalendar hc;
        public static string[] DaysOfWeekHebrew = { "יום א", "יום ב", "יום ג", "יום ד", "יום ה", "יום ו", "שבת" };
        //To translate a day number into a hebrew date - days start at 1, not 0.
        public static string[] DaysOfMonthHebrew = { "", "א'", "ב'", "ג'", "ד'", "ה'", "ו'", "ז'", "ח'", "ט'", "י'", "י\"א", "י\"ב", "י\"ג", "י\"ד", "ט\"ו", "ט\"ז", "י\"ז", "י\"ח", "י\"ט", "כ'", "כ\"א", "כ\"ב", "כ\"ג", "כ\"ד", "כ\"ה", "כ\"ו", "כ\"ז", "כ\"ח", "כ\"ט", "ל'" };
        public int Day { get; set; }
        public MonthObject Month { get; set; }
        public int Year { get; set; }
        public DayNight DayNight { get; set; }
        public string Name { get; set; }

        static Onah()
        {
            if (_monthNames == null)
            {
                _monthNames = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames;
            }
            hc = new System.Globalization.HebrewCalendar();
        }

        public Onah()
        {
            if (_monthNames == null)
            {
                _monthNames = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames;
            }
            hc = new System.Globalization.HebrewCalendar();
        }

        public Onah(DayNight dayNight)
            : this()
        {
            this.DayNight = dayNight;
        }

        public Onah(int day, int month, int year, DayNight dayNight)
            : this(dayNight)
        {
            this.Day = day;
            this.Month = new MonthObject(year, month);
            this.Year = year;
        }

        public Onah(DateTime dateTime)
            : this()
        {
            this.Day = hc.GetDayOfMonth(dateTime);
            this.Year = hc.GetYear(dateTime);
            this.Month = new MonthObject(this.Year, hc.GetMonth(dateTime));
        }

        public Onah(DateTime dateTime, DayNight dayNight)
            : this(dateTime)
        {
            this.DayNight = dayNight;
        }

        public Onah AddDays(int numberOfDays)
        {
            return new Onah(this.DateTime.AddDays((int)numberOfDays), this.DayNight);
        }

        public int GetInterval(Onah otherOnah)
        {
            TimeSpan intervalSpan = this.DateTime.Subtract(otherOnah.DateTime);
            return intervalSpan.Days + 1;
        }

        public static Onah GetPreviousOnah(Onah onah)
        {
            Onah previousOnah;
            if (onah.DayNight == DayNight.Day)
            {
                previousOnah = new Onah(onah.DateTime, DayNight.Night);
            }
            else
            {
                previousOnah = new Onah(onah.DateTime.AddDays(-1), DayNight.Day);
            }
            return previousOnah;
        }

        public static bool IsSameOnah(Onah first, Onah second)
        {
            return (GeneralUtils.IsSameday(first.DateTime, second.DateTime) && first.DayNight == second.DayNight);
        }

        /// <summary>
        /// This function is used to sort the Onahs/Entries by date
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int CompareOnahs(Onah first, Onah second)
        {
            if (GeneralUtils.IsSameday(first.DateTime, second.DateTime))
            {
                if (first.DayNight == DayNight.Night && second.DayNight == DayNight.Day)
                {
                    return -1;
                }
                else if (first.DayNight == DayNight.Day && second.DayNight == DayNight.Night)
                {
                    return 1;
                }
            }
            else if (first.DateTime > second.DateTime)
            {
                return 1;
            }
            else if (first.DateTime < second.DateTime)
            {
                return -1;
            }
            return 0;
        }

        public DateTime DateTime
        {
            get
            {
                DateTime hDate = new DateTime(this.Year, this.Month.MonthInYear, this.Day, hc);
                return hDate;
            }
        }

        public string HebrewDayNight
        {
            get
            {
                return this.DayNight == DayNight.Day ? "יום" : "לילה";
            }
        }

        public string MonthName
        {
            get
            {
                return this.Month.MonthName;
            }
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                return hc.GetDayOfWeek(this.DateTime);
            }
        }

        public string HebrewDayOfWeek
        {
            get
            {
                return DaysOfWeekHebrew[(int)this.DayOfWeek];
            }
        }

        public static string[] MonthNames
        {
            get
            {
                return _monthNames;
            }
        }

        public static System.Globalization.HebrewCalendar HebrewCalendar
        {
            get
            {
                return hc;
            }
        }
    }

    public class MonthObject
    {
        public int Year { get; private set; }
        public int MonthIndex { get; private set; }
        public int MonthInYear { get; private set; }
        public string MonthName { get; private set; }
        public int DaysInMonth { get; private set; }

        public MonthObject(int year, int monthInYear)
        {
            this.Year = year;
            this.MonthInYear = monthInYear;
            this.DaysInMonth = Onah.HebrewCalendar.GetDaysInMonth(year, monthInYear);

            int monthsInYear = Onah.HebrewCalendar.GetMonthsInYear(year);
            bool isLeapYear = (monthsInYear == 13);

            if (monthInYear <= 6 || isLeapYear)
            {
                this.MonthIndex = monthInYear - 1;
            }
            else
            {
                this.MonthIndex = monthInYear;
            }
            this.MonthName = Onah.MonthNames[MonthIndex];
        }
    }

    [Serializable()]
    public class Kavuah
    {
        public DayNight DayNight { get; set; }
        public ProblemOnahType ProblemOnahType { get; set; }
        public int Number { get; set; }
        public string Notes { get; set; }
        public string KavuahDescriptionHebrew
        {
            get
            {
                return (ProblemOnahType == Chashavshavon.ProblemOnahType.Haflagah ? " הפלגה, כל " + this.Number.ToString() + " ימים " : " יום החדש, כל " + Onah.DaysOfMonthHebrew[this.Number] + " בחדש ") +
                    " - עונת " + (this.DayNight == DayNight.Day ? " יום " : " לילה ") +
                    "  " + this.Notes;
            }
        }
        public static List<Kavuah> KavuahsList { get; set; }
    }

    public enum DayNight
    {
        Day = 1,
        Night = 2
    }

    public enum ProblemOnahType
    {
        Haflagah,
        DayOfMonth
    }
}
