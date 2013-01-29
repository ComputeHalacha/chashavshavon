using System;
using System.Collections.Generic;
using Chashavshavon.Utils;

namespace Chashavshavon
{
    public enum DayNight
    {
        Day = 1,
        Night = 2
    }

    public class Onah
    {
        private string _name;

        public int Day { get; set; }
        public MonthObject Month { get; set; }
        public int Year { get; set; }
        public DayNight DayNight { get; set; }
        public bool IsIgnored { get; set; }
        public bool IsChumrah { get; set; }

        public Onah() { }

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
        {
            this.Day = Program.HebrewCalendar.GetDayOfMonth(dateTime);
            this.Year = Program.HebrewCalendar.GetYear(dateTime);
            this.Month = new MonthObject(this.Year, Program.HebrewCalendar.GetMonth(dateTime));
        }

        public Onah(DateTime dateTime, DayNight dayNight)
            : this(dateTime)
        {
            this.DayNight = dayNight;
        }

        public override string ToString()
        {
            return this.DateTime.ToString("dddd dd MMM yyyy", Program.CultureInfo) +
                " [עונת " + this.HebrewDayNight + "]";
        }

        public Onah Clone()
        {
            return new Onah()
            {
                Day = this.Day,
                Month = this.Month,
                Year = this.Year,
                DayNight = this.DayNight,
                Name = this.Name,
                IsIgnored = this.IsIgnored,
                IsChumrah = this.IsChumrah
            };
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

        public static bool IsSameOnahPeriod(Onah first, Onah second)
        {
            return (first.DateTime.IsSameday(second.DateTime) && first.DayNight == second.DayNight);
        }

        public static bool IsSimilarOnah(Onah first, Onah second)
        {
            return (IsSameOnahPeriod(first, second) && (first.Name == second.Name));
        }

        /// <summary>
        /// Removes double Onahs from list
        /// </summary>
        /// <param name="list"></param>
        public static void ClearDoubleOnahs(List<Onah> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Exists(o => IsSimilarOnah(o, list[i])))
                {
                    list.RemoveAt(i);
                }
            }
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
                DateTime hDate = new DateTime(this.Year, this.Month.MonthInYear, this.Day, Program.HebrewCalendar);
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
                return Program.HebrewCalendar.GetDayOfWeek(this.DateTime);
            }
        }

        public string HebrewDayOfWeek
        {
            get
            {
                return Zmanim.DaysOfWeekHebrew[(int)this.DayOfWeek];
            }
        }

        public string Name
        {
            get 
            {
                return this._name ?? this.ToString();
            }
            set 
            {
                this._name = value;
            }
        }
    }
}
