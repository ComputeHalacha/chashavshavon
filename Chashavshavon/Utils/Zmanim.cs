using JewishCalendar;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Chashavshavon.Utils
{
    public static class Zmanim
    {
        public static string[] DaysOfWeekHebrewFull = { "ראשון", "שני", "שלישי", "רביעי", "חמישי", "שישי", "שבת קודש" };
        public static string[] DaysOfWeekHebrew = { "יום א", "יום ב", "יום ג", "יום ד", "יום ה", "יום ו", "שבת" };
        //To translate a day number into a hebrew date - days start at 1, not 0.
        public static string[] DaysOfMonthHebrew = { "", "א'", "ב'", "ג'", "ד'", "ה'", "ו'", "ז'", "ח'", "ט'", "י'", "י\"א", "י\"ב", "י\"ג", "י\"ד", "ט\"ו", "ט\"ז", "י\"ז", "י\"ח", "י\"ט", "כ'", "כ\"א", "כ\"ב", "כ\"ג", "כ\"ד", "כ\"ה", "כ\"ו", "כ\"ז", "כ\"ח", "כ\"ט", "ל'" };

        public static string GetDayOfWeekText(DateTime d)
        {
            string s = DaysOfWeekHebrew[(int)Program.HebrewCalendar.GetDayOfWeek(d)];
            if (((int)Program.HebrewCalendar.GetDayOfWeek(d)) < 6)
            {
                s += "'";
            }
            return s;
        }
    }

    public class MonthObject : IEquatable<MonthObject>
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
            this.DaysInMonth = Program.HebrewCalendar.GetDaysInMonth(year, monthInYear);

            int monthsInYear = Program.HebrewCalendar.GetMonthsInYear(year);
            bool isLeapYear = (monthsInYear == 13);

            if (monthInYear <= 6 || isLeapYear)
            {
                this.MonthIndex = monthInYear - 1;
            }
            else
            {
                this.MonthIndex = monthInYear;
            }
            this.MonthName = Program.CultureInfo.DateTimeFormat.MonthNames[MonthIndex];
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MonthObject);
        }

        public bool Equals(MonthObject other)
        {
            return other != null &&
                   Year == other.Year &&
                   MonthInYear == other.MonthInYear;
        }

        public override int GetHashCode()
        {
            var hashCode = -1090210929;
            hashCode = hashCode * -1521134295 + Year.GetHashCode();
            hashCode = hashCode * -1521134295 + MonthInYear.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(MonthObject object1, MonthObject object2)
        {
            return EqualityComparer<MonthObject>.Default.Equals(object1, object2);
        }

        public static bool operator !=(MonthObject object1, MonthObject object2)
        {
            return !(object1 == object2);
        }

        public override string ToString()
        {
            return this.MonthName != null 
                ? this.MonthName + " " + GeneralUtils.ToJNum(this.Year % 1000) 
                : "";
        }
    }
}