using System;
using System.Collections.Generic;
using System.Text;
using Chashavshavon.Utils;
using System.Xml.Serialization;

namespace Chashavshavon
{
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
    
    public class Onah
    {
        private static string[] _monthNames;
        private static System.Globalization.HebrewCalendar _hebrewCalendar;       

        public int Day { get; set; }        
        public MonthObject Month { get; set; }
        public int Year { get; set; }
        public DayNight DayNight { get; set; }        
        public string Name { get; set; }        
        public bool IsIgnored { get; set; }

        static Onah()
        {
            if (_monthNames == null)
            {
                _monthNames = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames;
            }
            _hebrewCalendar = new System.Globalization.HebrewCalendar();
        }

        public Onah()
        {
            if (_monthNames == null)
            {
                _monthNames = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.MonthNames;
            }
            _hebrewCalendar = new System.Globalization.HebrewCalendar();
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
            this.Day = _hebrewCalendar.GetDayOfMonth(dateTime);
            this.Year = _hebrewCalendar.GetYear(dateTime);
            this.Month = new MonthObject(this.Year, _hebrewCalendar.GetMonth(dateTime));
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
            return (first.DateTime.IsSameday(second.DateTime) && first.DayNight == second.DayNight);
        }

        /// <summary>
        /// This function is used to sort the Onahs/Entries by date
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static int CompareOnahs(Onah first, Onah second)
        {
            if (first.DateTime.IsSameday(second.DateTime))
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
                DateTime hDate = new DateTime(this.Year, this.Month.MonthInYear, this.Day, _hebrewCalendar);
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
                return _hebrewCalendar.GetDayOfWeek(this.DateTime);
            }
        }
        
        public string HebrewDayOfWeek
        {
            get
            {
                return Zmanim.DaysOfWeekHebrew[(int)this.DayOfWeek];
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
                return _hebrewCalendar;
            }
        }
    }
}
