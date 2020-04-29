using System;

namespace JewishCalendar
{
    #region Public Enums

    /// <summary>
    /// Types of special days
    /// </summary>
    [Flags]
    public enum SpecialDayTypes
    {
        /// <summary>
        /// Shabbos
        /// </summary>
        Shabbos = 0,
        /// <summary>
        /// Major Yom Tov - where melacha is prohibited
        /// </summary>
        MajorYomTov = 1,
        /// <summary>
        /// Minor Yom Tov - where Melacha is permitted
        /// </summary>
        MinorYomtov = 2,
        /// <summary>
        /// Chol Hamoed
        /// </summary>
        CholHamoed = 4,
        /// <summary>
        /// A fast day
        /// </summary>
        FastDay = 8,
        /// <summary>
        /// Extra day information
        /// </summary>
        Information = 16,
        /// <summary>
        /// Erev shabbos or yomtov
        /// </summary>
        HasCandleLighting = 32,
        /// <summary>
        /// Erev Yomtov - when Yomtov contains a Friday
        /// </summary>
        EruvTavshilin = 64
    };

    #endregion Public Enums

    /// <summary>
    /// Represents a single special day
    /// </summary>
    public class SpecialDay
    {
        #region Public Properties

        /// <summary>
        /// Type of special day. Can have multiple values.
        /// </summary>
        public SpecialDayTypes DayType { get; set; }

        /// <summary>
        /// Name of this special day in English
        /// </summary>
        public string NameEnglish { get; set; }

        /// <summary>
        /// Name of this special day in Hebrew
        /// </summary>
        public string NameHebrew { get; set; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Create a new SpecialDay instance.
        /// </summary>
        /// <param name="nameEnglish"></param>
        /// <param name="nameHebrew"></param>
        /// <param name="dayTypes"></param>
        public SpecialDay(string nameEnglish, string nameHebrew, SpecialDayTypes dayTypes)
        {
            this.NameEnglish = nameEnglish;
            this.NameHebrew = nameHebrew;
            this.DayType = dayTypes;
        }

        /// <summary>
        /// Create a new SpecialDay of DayType SpecialDayTypes.Information.
        /// </summary>
        /// <param name="nameEnglish"></param>
        /// <param name="nameHebrew"></param>
        public SpecialDay(string nameEnglish, string nameHebrew) : this(nameEnglish, nameHebrew, SpecialDayTypes.Information) { }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Returns the name of this special day in English.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => this.NameEnglish;

        /// <summary>
        /// Returns true if the given day is a day of Yom Tov or Chol Ha'moed in the given location.
        /// </summary>
        /// <param name="jd"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static bool IsMajorYomTov(JewishDate jd, Location location)
        {
            switch (jd.Month)
            {
                case 1:
                    return jd.Day > 14 && jd.Day < (location.IsInIsrael ? 22 : 23);
                case 3:
                    return jd.Day == 6 || ((!location.IsInIsrael) && jd.Day == 7);
                case 7:
                    return jd.Day.In(1, 2, 10) ||
                        (jd.Day > 14 && jd.Day < (location.IsInIsrael ? 23 : 24));
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns true if the given day is a day of Yom Tov or Shabbos in the given location.
        /// </summary>
        /// <param name="jd"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static bool IsShabbosOrYomTov(JewishDate jd, Location location)
        {
            if (jd.DayOfWeek == DayOfWeek.Saturday)
            {
                return true;
            }

            switch (jd.Month)
            {
                case 1:
                    return jd.Day == 15 || (!location.IsInIsrael && jd.Day == 16) || jd.Day == 21 || (!location.IsInIsrael && jd.Day == 22);
                case 3:
                    return jd.Day == 6 || ((!location.IsInIsrael) && jd.Day == 7);
                case 7:
                    return jd.Day.In(1, 2, 10) ||
                        (jd.Day == 15 || (!location.IsInIsrael && jd.Day == 16) || jd.Day == 22 || (!location.IsInIsrael && jd.Day == 23));
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns true if the given day is a special day or a fast day,
        /// but not Shabbos or a major Yom Tov in the given location.
        /// </summary>
        /// <param name="jd"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static bool IsMinorYomTovOrFast(JewishDate jd, Location location)
        {
            DayOfWeek dow = jd.DayOfWeek;
            int day = jd.Day,
                month = jd.Month,
                year = jd.Year;
            if (dow == DayOfWeek.Saturday || IsMajorYomTov(jd, location))
            {
                return false;
            }
            if (day.In(30, 1))
            {
                return true;
            }
            switch (month)
            {
                case 1:
                    return day == 14;
                case 2:
                    return day.In(14, 18);
                case 4:
                    return ((day == 17 && dow != DayOfWeek.Saturday) ||
                            (day == 18 && dow == DayOfWeek.Sunday));
                case 5:
                    return ((day == 9 && dow != DayOfWeek.Saturday) ||
                            (day == 10 && dow == DayOfWeek.Sunday) ||
                            day == 15);
                case 6:
                    return day == 29;
                case 7:
                    return ((day == 3 && dow != DayOfWeek.Saturday) ||
                            (day == 4 && dow == DayOfWeek.Sunday)) ||
                            day == 9;
                case 9:
                    return day > 24;
                case 10:
                    if (day < 4)
                    {
                        return JewishDateCalculations.IsShortKislev(year) || (day < 3);
                    }
                    else
                    {
                        return day == 10;
                    }
                case 11:
                    return day == 15;
                case 12:
                case 13:
                    if (month == 12 && JewishDateCalculations.IsJewishLeapYear(year))
                    {
                        return day.In(14, 15);
                    }
                    else
                    {
                        return
                            (day == 11 && dow == DayOfWeek.Thursday) ||
                            (day == 13 && dow != DayOfWeek.Saturday) ||
                            (day.In(14, 15));
                    }
                default:
                    return false;
            }
        }

        #endregion Public Methods
    }
}