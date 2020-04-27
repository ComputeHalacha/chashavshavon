using System;
using System.Collections.Generic;

namespace Tahara
{
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
            this.DaysInMonth = Utils.HebrewCalendar.GetDaysInMonth(year, monthInYear);

            int monthsInYear = Utils.HebrewCalendar.GetMonthsInYear(year);
            bool isLeapYear = (monthsInYear == 13);

            if (monthInYear <= 6 || isLeapYear)
            {
                this.MonthIndex = monthInYear - 1;
            }
            else
            {
                this.MonthIndex = monthInYear;
            }
            this.MonthName = Utils.CultureInfo.DateTimeFormat.MonthNames[this.MonthIndex];
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as MonthObject);
        }

        public bool Equals(MonthObject other)
        {
            return other != null &&
                   this.Year == other.Year &&
                   this.MonthInYear == other.MonthInYear;
        }

        public override int GetHashCode()
        {
            int hashCode = -1090210929;
            hashCode = hashCode * -1521134295 + this.Year.GetHashCode();
            hashCode = hashCode * -1521134295 + this.MonthInYear.GetHashCode();
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
                ? this.MonthName + " " + (this.Year % 1000).ToJNum()
                : "";
        }
    }
}
