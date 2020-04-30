/****************************************************************************************************************
 * Computes the Sedra/Sedras of the week for the given day.
 * Sample of use:
 *     var sedras = JewishCalendar.Sedra.GetSedra(JewishDate, isInIsrael);
 *     string strSedra = string.Join(" - ", sedras.Select(i => i.nameEng));
 * The code was converted to C# and tweaked by CBS.
 * It is directly based on the C code in Danny Sadinoff's HebCal - Copyright (C) 1994.
 * Portions of that code are Copyright (c) 2002 Michael J. Radwin. All Rights Reserved.
 * Many of the algorithms were taken from hebrew calendar routines implemented by Nachum Dershowitz
 * ***************************************************************************************************************/

using System;

namespace JewishCalendar
{
    /// <summary>
    /// This class should not be instantiated publicly.
    /// It is only used internally as a container to hold the data for all the sedras of a single year.
    /// To get the sedra for any given date, use the public static function: "GetSedra"
    /// </summary>
    public class Sedra
    {
        #region static

        /// <summary>
        ///  Because most consecutive calls to GetSedra will be within the same year,
        ///  and, in order to figure out the sedra for a single day we need to calculate the entire year,
        ///  so we save the last year calculated and use it if it is called again.
        ///  If memory is an issue, remove the first few lines of code in the Sedra constructor
        /// </summary>
        private static Sedra _lastSedraCalculated = null;

        private static readonly Parsha[] ParshaList =
        {
            new Parsha("Bereshis", "בראשית"),
            new Parsha("Noach", "נח"),
            new Parsha("Lech-Lecha", "לך לך"),
            new Parsha("Vayera", "וירא"),
            new Parsha("Chayei Sara", "חיי שרה"),
            new Parsha("Toldos", "תולדות"),
            new Parsha("Vayetzei", "ויצא"),
            new Parsha("Vayishlach", "וישלח"),
            new Parsha("Vayeishev", "וישב"),
            new Parsha("Mikeitz", "מקץ"),
            new Parsha("Vayigash", "ויגש"),
            new Parsha("Vayechi", "ויחי"),
            new Parsha("Shemos", "שמות"),
            new Parsha("Va'era", "וארא"),
            new Parsha("Bo", "בא"),
            new Parsha("Beshalach", "בשלח"),
            new Parsha("Yisro", "יתרו"),
            new Parsha("Mishpatim", "משפטים"),
            new Parsha("Terumah", "תרומה"),
            new Parsha("Tetzaveh", "תצוה"),
            new Parsha("Ki Sisa", "כי תשא"),
            new Parsha("Vayakhel", "ויקהל"),
            new Parsha("Pekudei", "פקודי"),
            new Parsha("Vayikra", "ויקרא"),
            new Parsha("Tzav", "צו"),
            new Parsha("Shmini", "שמיני"),
            new Parsha("Tazria", "תזריע"),
            new Parsha("Metzora", "מצורע"),
            new Parsha("Achrei Mos", "אחרי מות"),
            new Parsha("Kedoshim", "קדושים"),
            new Parsha("Emor", "אמור"),
            new Parsha("Behar", "בהר"),
            new Parsha("Bechukosai", "בחקותי"),
            new Parsha("Bamidbar", "במדבר"),
            new Parsha("Nasso", "נשא"),
            new Parsha("Beha'aloscha", "בהעלתך"),
            new Parsha("Sh'lach", "שלח"),
            new Parsha("Korach", "קרח"),
            new Parsha("Chukas", "חקת"),
            new Parsha("Balak", "בלק"),
            new Parsha("Pinchas", "פינחס"),
            new Parsha("Matos", "מטות"),
            new Parsha("Masei", "מסעי"),
            new Parsha("Devarim", "דברים"),
            new Parsha("Va'eschanan", "ואתחנן"),
            new Parsha("Eikev", "עקב"),
            new Parsha("Re'eh", "ראה"),
            new Parsha("Shoftim", "שופטים"),
            new Parsha("Ki Seitzei", "כי תצא"),
            new Parsha("Ki Savo", "כי תבא"),
            new Parsha("Nitzavim", "נצבים"),
            new Parsha("Vayeilech", "וילך"),
            new Parsha("Ha'Azinu", "האזינו"),
            new Parsha("Vezos Habracha", "וזאת הברכה")
        };

        private static int GetDayOnOrBefore(int day_of_week, int date)
        {
            return date - ((date - day_of_week) % 7);
        }

        private static readonly int[] shabbos_short = {
            52, 52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, -21, 23, 24, 25, 25, -26, -28, 30, -31, 33, 34, 35, 36, 37, 38, 39, 40, -41, 43, 44, 45, 46, 47,
            48, 49, 50 };

        private static readonly int[] shabbos_long = {
            52, 52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, -21, 23, 24, 25, 25, -26, -28, 30, -31, 33, 34, 35, 36, 37, 38, 39, 40, -41, 43, 44, 45, 46, 47,
            48, 49, -50 };

        private static readonly int[] mon_short = {
            51, 52, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
            18, 19, 20, -21, 23, 24, 25, 25, -26, -28, 30, -31, 33, 34, 35, 36, 37, 38, 39, 40, -41, 43, 44, 45, 46, 47, 48,
            49, -50 };

        private static readonly int[] mon_long = {
            51, 52, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, -21, 23, 24, 25, 25, -26, -28,
            30, -31, 33, 34, 34, 35, 36, 37, -38, 40, -41, 43, 44, 45, 46, 47, 48, 49, -50 };

        private static readonly int[] thu_normal = {
            52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
            18, 19, 20, -21, 23, 24, 25, 25, 25, -26, -28, 30, -31, 33, 34, 35, 36, 37, 38, 39, 40, -41, 43, 44, 45, 46, 47,
            48, 49, 50 };

        private static readonly int[] thu_normal_Israel = {
            52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, -21, 23, 24, 25, 25, -26, -28, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, -41, 43, 44, 45,
            46, 47, 48, 49, 50 };

        private static readonly int[] thu_long = {
            52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17,
            18, 19, 20, 21, 22, 23, 24, 25, 25, -26, -28, 30, -31, 33, 34, 35, 36, 37, 38, 39, 40, -41, 43, 44, 45, 46, 47,
            48, 49, 50 };

        private static readonly int[] shabbos_short_leap = {
            52, 52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, -41,
            43, 44, 45, 46, 47, 48, 49, -50 };

        private static readonly int[] shabbos_long_leap = {
            52, 52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
            16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 28, 29, 30, 31, 32, 33, 34, 34, 35, 36, 37, -38, 40, -41,
            43, 44, 45, 46, 47, 48, 49, -50 };

        private static readonly int[] mon_short_leap = {
            51, 52, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 28, 29, 30, 31, 32, 33, 34, 34, 35, 36, 37, -38, 40, -41, 43,
            44, 45, 46, 47, 48, 49, -50 };

        private static readonly int[] mon_short_leap_Israel = {
            51, 52, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
            15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
            -41, 43, 44, 45, 46, 47, 48, 49, -50 };

        private static readonly int[] mon_long_leap = {
            51, 52, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 28, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, -41,
            43, 44, 45, 46, 47, 48, 49, 50 };

        private static readonly int[] mon_long_leap_Israel = {
            51, 52, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
            15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
            41, 42, 43, 44, 45, 46, 47, 48, 49, 50 };

        private static readonly int[] thu_short_leap = {
            52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42,
            43, 44, 45, 46, 47, 48, 49, 50 };

        private static readonly int[] thu_long_leap = {
            52, 53, 53, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42,
            43, 44, 45, 46, 47, 48, 49, -50 };

        private enum YearType
        {
            Incomplete,
            Regular,
            Complete
        }

        #endregion static

        #region private

        private readonly int _year;
        private readonly bool _inIsrael;
        private readonly int[] _sedraArray;
        private int _sedraNumWeeks => this._sedraArray.Length;
        private readonly int _firstSatInYear;

        private Sedra(int year, bool inIsrael)
        {
            //If the last call is within the same year as this one, we reuse the data.
            //If memory is an issue, remove these next few lines
            if (_lastSedraCalculated != null && _lastSedraCalculated._year == year && _lastSedraCalculated._inIsrael == inIsrael)
            {
                this._firstSatInYear = _lastSedraCalculated._firstSatInYear;
                this._sedraArray = _lastSedraCalculated._sedraArray;
                return;
            }

            //Save the data in case the next call is for the same year
            _lastSedraCalculated = this;

            bool longCheshvon = JewishDateCalculations.IsLongCheshvan(year);
            bool shortKislev = JewishDateCalculations.IsShortKislev(year);
            int roshHashana = JewishDateCalculations.GetAbsoluteFromJewishDate(year, 7, 1);
            var roshHashanaDOW = (DayOfWeek)Math.Abs(roshHashana % 7);
            YearType yearType;

            if (longCheshvon && !shortKislev)
            {
                yearType = YearType.Complete;
            }
            else if (!longCheshvon && shortKislev)
            {
                yearType = YearType.Incomplete;
            }
            else
            {
                yearType = YearType.Regular;
            }

            this._year = year;
            this._inIsrael = inIsrael;

            /* find and save the first shabbos on or after Rosh Hashana */
            this._firstSatInYear = GetDayOnOrBefore(6, roshHashana + 6);

            if (!JewishDateCalculations.IsJewishLeapYear(year))
            {
                switch (roshHashanaDOW)
                {
                    case DayOfWeek.Saturday:
                        if (yearType == YearType.Incomplete)
                        {
                            this._sedraArray = shabbos_short;
                        }
                        else if (yearType == YearType.Complete)
                        {
                            this._sedraArray = shabbos_long;
                        }
                        break;

                    case DayOfWeek.Monday:
                        if (yearType == YearType.Incomplete)
                        {
                            this._sedraArray = mon_short;
                        }
                        else if (yearType == YearType.Complete)
                        {
                            this._sedraArray = this._inIsrael ? mon_short : mon_long;
                        }
                        break;

                    case DayOfWeek.Tuesday:
                        if (yearType == YearType.Regular)
                        {
                            this._sedraArray = this._inIsrael ? mon_short : mon_long;
                        }
                        break;

                    case DayOfWeek.Thursday:
                        if (yearType == YearType.Regular)
                        {
                            this._sedraArray = this._inIsrael ? thu_normal_Israel : thu_normal;
                        }
                        else if (yearType == YearType.Complete)
                        {
                            this._sedraArray = thu_long;
                        }
                        break;

                    default:
                        throw new Exception("improper sedra year type calculated.");
                }
            }
            else  /* leap year */
            {
                switch (roshHashanaDOW)
                {
                    case DayOfWeek.Saturday:
                        if (yearType == YearType.Incomplete)
                        {
                            this._sedraArray = shabbos_short_leap;
                        }
                        else if (yearType == YearType.Complete)
                        {
                            this._sedraArray = this._inIsrael ? shabbos_short_leap : shabbos_long_leap;
                        }
                        break;

                    case DayOfWeek.Monday:
                        if (yearType == YearType.Incomplete)
                        {
                            this._sedraArray = this._inIsrael ? mon_short_leap_Israel : mon_short_leap;
                        }
                        else if (yearType == YearType.Complete)
                        {
                            this._sedraArray = this._inIsrael ? mon_long_leap_Israel : mon_long_leap;
                        }
                        break;

                    case DayOfWeek.Tuesday:
                        if (yearType == YearType.Regular)
                        {
                            this._sedraArray = this._inIsrael ? mon_long_leap_Israel : mon_long_leap;
                        }
                        break;

                    case DayOfWeek.Thursday:
                        if (yearType == YearType.Incomplete)
                        {
                            this._sedraArray = thu_short_leap;
                        }
                        else if (yearType == YearType.Complete)
                        {
                            this._sedraArray = thu_long_leap;
                        }
                        break;

                    default:
                        throw new Exception("improper sedra year type calculated.");
                }
            }
        }

        #endregion private

        #region public

        /// <summary>
        /// Gets the Parsha/s for the given Jewish date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="inIsrael"></param>
        /// <returns></returns>
        public static Parsha[] GetSedra(JewishDate date, bool inIsrael)
        {
            Parsha[] parshaArray;

            //If we are between the first day of Sukkos and Simchas Torah, the parsha will always be Vezos Habracha.
            if (date.Month == 7 && date.Day >= 15 && date.Day < (inIsrael ? 23 : 24))
            {
                return new Parsha[] { ParshaList[53] };
            }

            var sedraOrder = new Sedra(date.Year, inIsrael);
            int absDate = date.AbsoluteDate;
            int index;
            int weekNum;

            /* find the first saturday on or after today's date */
            absDate = GetDayOnOrBefore(6, absDate + 6);

            weekNum = (absDate - sedraOrder._firstSatInYear) / 7;
            if (weekNum >= sedraOrder._sedraNumWeeks)
            {
                int indexLast = sedraOrder._sedraArray[sedraOrder._sedraNumWeeks - 1];
                if (indexLast < 0)
                {
                    /* advance 2 parashiyot ahead after a doubled week */
                    index = (-indexLast) + 2;
                }
                else
                {
                    index = indexLast + 1;
                }
            }
            else
            {
                index = sedraOrder._sedraArray[weekNum];
            }

            if (index >= 0)
            {
                parshaArray = new Parsha[] { ParshaList[index] };
            }
            else
            {
                int i = -index;      /* undouble the parsha */
                parshaArray = new Parsha[] { ParshaList[i], ParshaList[i + 1] };
            }
            return parshaArray;
        }

        #endregion public
    }

    /// <summary>
    /// Represents a single parsha in the torah. The weekly sedra for any given week contains either one or two parshas.
    /// </summary>
    public class Parsha
    {
        /// <summary>
        /// Create a new Parsha object
        /// </summary>
        /// <param name="eng"></param>
        /// <param name="heb"></param>
        public Parsha(string eng, string heb) { this.nameEng = eng; this.nameHebrew = heb; }

        /// <summary>
        /// Name of this parsha in English
        /// </summary>
        public string nameEng;

        /// <summary>
        /// Name of this parsha in Hebrew
        /// </summary>
        public string nameHebrew;
    }
}