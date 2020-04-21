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

        public static void SetSummerTime()
        {
            Properties.Settings.Default.IsSummerTime = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now) ||
                TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now);
        }

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
    }

    //The following source code is Copyright © by Ulrich and Ziporah Greve (2005)
    public class TimesCalculation
    {
        public AstronomicalTime GetSunrise(DateTime currentDate, Place place)
        {
            AstronomicalTime sunrise;
            TimesCalculationInternal calcInternal = new TimesCalculationInternal();
            sunrise = calcInternal.GetSunsetOrSunrise(currentDate,
              Occurrence.Sunrise, place);
            return sunrise;
        }

        public AstronomicalTime GetSunset(DateTime currentDate, Place place)
        {
            AstronomicalTime sunset;
            TimesCalculationInternal calcInternal = new TimesCalculationInternal();
            sunset = calcInternal.GetSunsetOrSunrise(currentDate,
              Occurrence.Sunset, place);
            return sunset;
        }

        public AstronomicalTime GetSunriseDegreesBelowHorizon(DateTime currentDate,
                                                              double DegreesBelowHorizon,
                                                              Place place)
        {
            AstronomicalTime sunrise = GetSunrise(currentDate, place);
            TimesCalculationInternal calcInternal = new TimesCalculationInternal();
            AstronomicalTime db = calcInternal.GetDegreesBelowHorizon(currentDate,
              DegreesBelowHorizon, place);
            return sunrise - db;
        }

        public AstronomicalTime GetSunsetDegreesBelowHorizon(DateTime currentDate,
                                                             double DegreesBelowHorizon,
                                                             Place place)
        {
            AstronomicalTime sunset = GetSunset(currentDate, place);
            TimesCalculationInternal calcInternal = new TimesCalculationInternal();
            AstronomicalTime db = calcInternal.GetDegreesBelowHorizon(currentDate,
              DegreesBelowHorizon, place);
            return sunset + db;
        }

        public AstronomicalTime GetProportionalHours(AstronomicalTime sunrise,
                                                     AstronomicalTime sunset,
                                                     double ProportionalHours)
        {
            int sunriseTime = sunrise.Hour * 60 + sunrise.Minute;
            int sunsetTime = sunset.Hour * 60 + sunset.Minute;
            int result = sunriseTime +
                         (int)(((sunsetTime - sunriseTime) * ProportionalHours) / 12);

            return new AstronomicalTime(result / 60, result % 60);
        }
        public AstronomicalTime GetShaaZmanit(AstronomicalTime sunrise,
                                              AstronomicalTime sunset)
        {
            int sunriseTime = sunrise.Hour * 60 + sunrise.Minute;
            int sunsetTime = sunset.Hour * 60 + sunset.Minute;
            int result = (sunsetTime - sunriseTime) / 12;

            return new AstronomicalTime(result / 60, result % 60);
        }
    }

    public class AstronomicalTime
    {
        public AstronomicalTime(int hour, int min)
        {
            this.Hour = hour;
            this.Minute = min;
        }

        public static AstronomicalTime operator +(AstronomicalTime t,
                            int min)
        {
            AstronomicalTime result = new AstronomicalTime(t.Hour, t.Minute + min);
            while (result.Minute >= 60)
            {
                result.Minute -= 60;
                result.Hour++;
            }
            return result;
        }
        public static AstronomicalTime operator -(AstronomicalTime t,
                            int min)
        {
            AstronomicalTime result = new AstronomicalTime(t.Hour, t.Minute - min);
            while (result.Minute < 0)
            {
                result.Minute += 60;
                result.Hour--;
            }
            return result;
        }

        public static AstronomicalTime operator +(AstronomicalTime t,
                            AstronomicalTime tAdd)
        {
            return t + (tAdd.Hour * 60 + tAdd.Minute);
        }
        public static AstronomicalTime operator -(AstronomicalTime t,
                            AstronomicalTime tSub)
        {
            return t - (tSub.Hour * 60 + tSub.Minute);
        }

        public int Hour, Minute;
    }

    public enum DiasporaOrIsrael
    {
        Diaspora,
        Israel
    }

    public enum Holiday
    {

        RoshHodesh = 0,
        ErevRoshHaShana,
        RoshHaShanaI,
        RoshHaShanaII,
        TzomGedaliah,
        ErevYomKippur,
        YomKippur,
        ErevSuccoth,
        Succoth,
        SuccothI,
        SuccothII,
        HolHamoed,
        HoschanaRaba,
        SheminiAtzeret,
        SimhathTorah,
        ChanukkaI,
        ChanukkaII,
        ChanukkaIII,
        ChanukkaIV,
        ChanukkaV,
        ChanukkaVI,
        ChanukkaVII,
        ChanukkaVIII,
        FastofTebeth,
        TuBiShevat,
        FastOfEsther,
        Purim,
        ShushanPurim,
        PurimKatan,
        ShushanPurimKatan,
        ShabbatHagadol,
        ErevPessach,
        PessachI,
        PessachII,
        PessachVII,
        PessachVIII,
        PessachSheni,
        LagBaomer,
        ErevShavuoth,
        Shavuoth,
        ShavuothI,
        ShavuothII,
        TzomTammuz,
        FastofAv,
        TuBeav
    }

    public static class JewishHolidays
    {
        private static readonly string[] HolidaysInHebrew;

        static JewishHolidays()
        {
            HolidaysInHebrew = new string[]
            {
                "ראש חודש",
                "ערב ראש השנה",
                "ראש השנה - יום א'",
                "ראש השנה - יום ב'",
                "צום גדליה",
                "ערב יום כיפור",
                "יום כיפור",
                "ערב סוכות",
                "סוכות",
                "סוכות יום א'",
                "סוכות יום ב'",
                "חול המועד",
                "הושענא רבא",
                "שמיני עצרת",
                "שמחת תורה",
                "חנוכה נר א'",
                "חנוכה נר ב'",
                "חנוכה נר ג'",
                "חנוכה נר ד'",
                "חנוכה נר ה'",
                "חנוכה נר ו'",
                "חנוכה נר ז'",
                "חנוכה נר ח'",
                "עשרה בטבת",
                "ט\"ו בשבט",
                "תענית אסתר",
                "פורים",
                "שושן פורים",
                "פורים קטן",
                "שושן פורים קטן",
                "שבת הגדול",
                "ערב פסח",
                "פסח יום א'",
                "פסח יום ב'",
                "שביעי של פסח",
                "אחרון של פסח",
                "פסח שני",
                "ל\"ג בעומר",
                "ערב שבועות",
                "שבועות",
                "שבועות יום א'",
                "שבועות יום ב'",
                "י\"ז בתמוז",
                "תשעה באב",
                "ט\"ו באב"
            };
        }

        public static List<string> GetHebrewHolidays(DateTime dt, bool inIsrael)
        {
            DiasporaOrIsrael diasporaOrIsrael = (inIsrael ? DiasporaOrIsrael.Israel : DiasporaOrIsrael.Diaspora);
            List<Holiday> holidays = GetHolidaysForDate(dt, Program.HebrewCalendar, diasporaOrIsrael);
            List<string> hebs = new List<string>();

            SetShabbosSedra(dt, inIsrael, hebs);

            for (int i = 0; i < holidays.Count; i++)
            {
                hebs.Add(HolidaysInHebrew[(int)holidays[i]]);
            }

            if (Program.HebrewCalendar.GetMonth(dt).In(7, 8, 9, 10))
            {
                int year = Program.HebrewCalendar.GetYear(dt);
                int month = Program.HebrewCalendar.IsLeapYear(year) ? 8 : 7;
                DateTime firstDayOfPesach = new DateTime(year, month, 15, Program.HebrewCalendar);
                int dayOfSefirah = (dt - firstDayOfPesach).Days;

                if (dayOfSefirah > 0 && dayOfSefirah < 50)
                {
                    hebs.Add(dayOfSefirah.ToString() + " לעומר");
                }
            }

            return hebs;
        }

        private static void SetShabbosSedra(DateTime dt, bool inIsrael, List<string> hebs)
        {
            if (dt.DayOfWeek != DayOfWeek.Saturday)
            {
                return;
            }

            JewishCalendar.JewishDate jd = new JewishCalendar.JewishDate(dt);
            if (!((jd.Month == 7 && jd.Day.In(1, 2, 10, 15, 16, 17, 18, 19, 20, 21, 22, (inIsrael ? 0 : 23))) ||
                (jd.Month == 1 && jd.Day.In(15, 16, 17, 18, 19, 20, 21, (inIsrael ? 0 : 22)) ||
                (jd.Month == 3 && jd.Day.In(6, (inIsrael ? 0 : 7))))))
            {
                var parshas = JewishCalendar.Sedra.GetSedra(jd, inIsrael);
                hebs.Add("שבת פרשת " + string.Join(" - ", parshas.Select(i => i.nameHebrew)));
            }
        }

        public static List<Holiday> GetHolidaysForDate(DateTime dt, HebrewCalendar hcal, DiasporaOrIsrael diasporaOrIsrael)
        {
            int hebrewDay = hcal.GetDayOfMonth(dt);
            int hebrewMonth = hcal.GetMonth(dt);
            int hebrewYear = hcal.GetYear(dt);

            int hebrewMonthNisan = 7;
            if (hcal.IsLeapYear(hebrewYear))
            {
                hebrewMonthNisan = 8;
            }

            int hebrewMonthIyar = 8;
            if (hcal.IsLeapYear(hebrewYear))
            {
                hebrewMonthIyar = 9;
            }

            int hebrewMonthSivan = 9;
            if (hcal.IsLeapYear(hebrewYear))
            {
                hebrewMonthSivan = 10;
            }

            int hebrewMonthTammuz = 10;
            if (hcal.IsLeapYear(hebrewYear))
            {
                hebrewMonthTammuz = 11;
            }

            int hebrewMonthAv = 11;
            if (hcal.IsLeapYear(hebrewYear))
            {
                hebrewMonthAv = 12;
            }

            int hebrewMonthElul = 12;
            if (hcal.IsLeapYear(hebrewYear))
            {
                hebrewMonthElul = 13;
            }

            List<Holiday> holidays = new List<Holiday>();

            if (hebrewDay == 30)
            {
                holidays.Add(Holiday.RoshHodesh);
            }

            if (hebrewDay == 1 && hebrewMonth != 1)
            {
                holidays.Add(Holiday.RoshHodesh);
            }

            //------- Tishri
            if (hebrewDay == 29 && hebrewMonth == hebrewMonthElul)
            {
                holidays.Add(Holiday.ErevRoshHaShana);
            }

            if (hebrewDay == 1 && hebrewMonth == 1) // 1 Tishri
            {
                holidays.Add(Holiday.RoshHaShanaI);
            }

            if (hebrewDay == 2 && hebrewMonth == 1) // 2 Tishri
            {
                holidays.Add(Holiday.RoshHaShanaII);
            }

            if (GetWeekdayOfHebrewDate(3, 1, hebrewYear) == DayOfWeek.Saturday) // 3 Tishri
            {
                if (hebrewDay == 4 && hebrewMonth == 1) // 4 Tishri
                {
                    holidays.Add(Holiday.TzomGedaliah);
                }
            }
            else
            {
                if (hebrewDay == 3 && hebrewMonth == 1) // 3 Tishri
                {
                    holidays.Add(Holiday.TzomGedaliah);
                }
            }

            if (hebrewDay == 9 && hebrewMonth == 1)
            {
                holidays.Add(Holiday.ErevYomKippur);
            }

            if (hebrewDay == 10 && hebrewMonth == 1) //10 Tishri
            {
                holidays.Add(Holiday.YomKippur);
            }

            if (hebrewDay == 14 && hebrewMonth == 1)
            {
                holidays.Add(Holiday.ErevSuccoth);
            }

            if (hebrewDay == 15 && hebrewMonth == 1) //15 Tishri
            {
                if (diasporaOrIsrael == DiasporaOrIsrael.Diaspora)
                {
                    holidays.Add(Holiday.SuccothI);
                }
                else
                {
                    holidays.Add(Holiday.Succoth);
                }
            }
            if (hebrewDay == 16 && hebrewMonth == 1) //16 Tishri
            {
                if (diasporaOrIsrael == DiasporaOrIsrael.Diaspora)
                {
                    holidays.Add(Holiday.SuccothII);
                }
                else
                {
                    holidays.Add(Holiday.HolHamoed);
                }
            }
            if (hebrewDay == 17 && hebrewMonth == 1) //17 Tishri
            {
                holidays.Add(Holiday.HolHamoed);
            }

            if (hebrewDay == 18 && hebrewMonth == 1)
            {
                holidays.Add(Holiday.HolHamoed);
            }

            if (hebrewDay == 19 && hebrewMonth == 1)
            {
                holidays.Add(Holiday.HolHamoed);
            }

            if (hebrewDay == 20 && hebrewMonth == 1)
            {
                holidays.Add(Holiday.HolHamoed);
            }

            if (hebrewDay == 21 && hebrewMonth == 1)
            {
                holidays.Add(Holiday.HoschanaRaba);
            }

            if (hebrewDay == 22 && hebrewMonth == 1 &&
               diasporaOrIsrael == DiasporaOrIsrael.Diaspora)
            {
                holidays.Add(Holiday.SheminiAtzeret);
            }

            if (hebrewDay == 23 && hebrewMonth == 1 &&
               diasporaOrIsrael == DiasporaOrIsrael.Diaspora)
            {
                holidays.Add(Holiday.SimhathTorah);
            }

            if (hebrewDay == 22 && hebrewMonth == 1 &&
               diasporaOrIsrael == DiasporaOrIsrael.Israel)
            {
                holidays.Add(Holiday.SheminiAtzeret);
                holidays.Add(Holiday.SimhathTorah);
            }

            //------- Kislev/Tevet

            if (hebrewDay == 25 && hebrewMonth == 3)
            {
                holidays.Add(Holiday.ChanukkaI);
            }

            if (hebrewDay == 26 && hebrewMonth == 3)
            {
                holidays.Add(Holiday.ChanukkaII);
            }

            if (hebrewDay == 27 && hebrewMonth == 3)
            {
                holidays.Add(Holiday.ChanukkaIII);
            }

            if (hebrewDay == 28 && hebrewMonth == 3)
            {
                holidays.Add(Holiday.ChanukkaIV);
            }

            if (hebrewDay == 29 && hebrewMonth == 3)
            {
                holidays.Add(Holiday.ChanukkaV);
            }

            int daysInKislew = hcal.GetDaysInMonth(hebrewYear, 3);

            if (daysInKislew == 29)
            {

                if (hebrewDay == 1 && hebrewMonth == 4)
                {
                    holidays.Add(Holiday.ChanukkaVI);
                }

                if (hebrewDay == 2 && hebrewMonth == 4)
                {
                    holidays.Add(Holiday.ChanukkaVII);
                }

                if (hebrewDay == 3 && hebrewMonth == 4)
                {
                    holidays.Add(Holiday.ChanukkaVIII);
                }
            }

            else
            {

                if (hebrewDay == 30 && hebrewMonth == 3)
                {
                    holidays.Add(Holiday.ChanukkaVI);
                }

                if (hebrewDay == 1 && hebrewMonth == 4)
                {
                    holidays.Add(Holiday.ChanukkaVII);
                }

                if (hebrewDay == 2 && hebrewMonth == 4)
                {
                    holidays.Add(Holiday.ChanukkaVIII);
                }
            }

            //------- Tevet
            if (hebrewDay == 10 && hebrewMonth == 4)
            {
                holidays.Add(Holiday.FastofTebeth);
            }

            //------- Shevat
            if (hebrewDay == 15 && hebrewMonth == 5)
            {
                holidays.Add(Holiday.TuBiShevat);
            }

            //------- Adar

            if (hcal.IsLeapYear(hebrewYear))
            {

                if (GetWeekdayOfHebrewDate(13, 7, hebrewYear) == DayOfWeek.Saturday)
                {

                    if (hebrewDay == 11 && hebrewMonth == 7)
                    {
                        holidays.Add(Holiday.FastOfEsther);
                    }
                }
                else
                {

                    if (hebrewDay == 13 && hebrewMonth == 7)
                    {
                        holidays.Add(Holiday.FastOfEsther);
                    }
                }


                if (hebrewDay == 14 && hebrewMonth == 7)
                {
                    holidays.Add(Holiday.Purim);
                }

                if (hebrewDay == 15 && hebrewMonth == 7)
                {
                    holidays.Add(Holiday.ShushanPurim);
                }

                // ------------------- Purim Katan ------------------------

                if (hebrewDay == 14 && hebrewMonth == 6)
                {
                    holidays.Add(Holiday.PurimKatan);
                }

                if (hebrewDay == 15 && hebrewMonth == 6)
                {
                    holidays.Add(Holiday.ShushanPurimKatan);
                }

                // --------------------------------------------------------

            }

            else
            {


                if (GetWeekdayOfHebrewDate(13, 6, hebrewYear) == DayOfWeek.Saturday)
                {

                    if (hebrewDay == 11 && hebrewMonth == 6)
                    {
                        holidays.Add(Holiday.FastOfEsther);
                    }
                }
                else
                {

                    if (hebrewDay == 13 && hebrewMonth == 6)
                    {
                        holidays.Add(Holiday.FastOfEsther);
                    }
                }

                if (hebrewDay == 14 && hebrewMonth == 6)
                {
                    holidays.Add(Holiday.Purim);
                }

                if (hebrewDay == 15 && hebrewMonth == 6)
                {
                    holidays.Add(Holiday.ShushanPurim);
                }
            }

            //------- Nisan

            int hebrewDayOfShabbatHagadol = 14;
            while (GetWeekdayOfHebrewDate(hebrewDayOfShabbatHagadol, hebrewMonthNisan,
                                          hebrewYear) != DayOfWeek.Saturday)
            {
                hebrewDayOfShabbatHagadol--;
            }

            if (hebrewDay == hebrewDayOfShabbatHagadol && hebrewMonth == hebrewMonthNisan)
            {
                holidays.Add(Holiday.ShabbatHagadol);
            }

            if (hebrewDay == 14 && hebrewMonth == hebrewMonthNisan)
            {
                holidays.Add(Holiday.ErevPessach);
            }

            if (hebrewDay == 15 && hebrewMonth == hebrewMonthNisan)
            {
                holidays.Add(Holiday.PessachI);
            }

            if (hebrewDay == 16 && hebrewMonth == hebrewMonthNisan)
            {
                if (diasporaOrIsrael == DiasporaOrIsrael.Diaspora)
                {
                    holidays.Add(Holiday.PessachII);
                }
                else
                {
                    holidays.Add(Holiday.HolHamoed);
                }
            }

            if (hebrewDay == 17 && hebrewMonth == hebrewMonthNisan)
            {
                holidays.Add(Holiday.HolHamoed);
            }

            if (hebrewDay == 18 && hebrewMonth == hebrewMonthNisan)
            {
                holidays.Add(Holiday.HolHamoed);
            }

            if (hebrewDay == 19 && hebrewMonth == hebrewMonthNisan)
            {
                holidays.Add(Holiday.HolHamoed);
            }

            if (hebrewDay == 20 && hebrewMonth == hebrewMonthNisan)
            {
                holidays.Add(Holiday.HolHamoed);
            }

            if (hebrewDay == 21 && hebrewMonth == hebrewMonthNisan)
            {
                holidays.Add(Holiday.PessachVII);
            }

            if (diasporaOrIsrael == DiasporaOrIsrael.Diaspora)
            {

                if (hebrewDay == 22 && hebrewMonth == hebrewMonthNisan)
                {
                    holidays.Add(Holiday.PessachVIII);
                }
            }




            if (hebrewDay == 18 && hebrewMonth == hebrewMonthIyar)
            {
                holidays.Add(Holiday.LagBaomer);
            }

            // *********** Pessach Scheni **********

            if (hebrewDay == 14 && hebrewMonth == hebrewMonthIyar)
            {
                holidays.Add(Holiday.PessachSheni);
            }

            // ******** End Pessach Scheni *********

            //------- Sivan

            if (hebrewDay == 5 && hebrewMonth == hebrewMonthSivan)
            {
                holidays.Add(Holiday.ErevShavuoth);
            }

            if (diasporaOrIsrael == DiasporaOrIsrael.Diaspora)
            {

                if (hebrewDay == 6 && hebrewMonth == hebrewMonthSivan)
                {
                    holidays.Add(Holiday.ShavuothI);
                }

                if (hebrewDay == 7 && hebrewMonth == hebrewMonthSivan)
                {
                    holidays.Add(Holiday.ShavuothII);
                }
            }
            else
            {

                if (hebrewDay == 6 && hebrewMonth == hebrewMonthSivan)
                {
                    holidays.Add(Holiday.Shavuoth);
                }
            }

            //------- Tammuz

            if (GetWeekdayOfHebrewDate(17, hebrewMonthTammuz, hebrewYear) == DayOfWeek.Saturday)
            {
                if (hebrewDay == 18 && hebrewMonth == hebrewMonthTammuz)
                {
                    holidays.Add(Holiday.TzomTammuz);
                }
            }

            else
            {

                if (hebrewDay == 17 && hebrewMonth == hebrewMonthTammuz)
                {
                    holidays.Add(Holiday.TzomTammuz);
                }
            }

            //------- Av

            if (hebrewDay == 15 && hebrewMonth == hebrewMonthAv)
            {
                holidays.Add(Holiday.TuBeav);
            }

            if (GetWeekdayOfHebrewDate(9, hebrewMonthAv, hebrewYear) == DayOfWeek.Saturday)
            {
                if (hebrewDay == 10 && hebrewMonth == hebrewMonthAv)
                {
                    holidays.Add(Holiday.FastofAv);
                }
            }

            else
            {

                if (hebrewDay == 9 && hebrewMonth == hebrewMonthAv)
                {
                    holidays.Add(Holiday.FastofAv);
                }
            }


            return holidays;
        }

        private static DayOfWeek GetWeekdayOfHebrewDate(int day, int month, int year)
        {
            DateTime dt = new DateTime(year, month, day, Program.HebrewCalendar);
            return Program.HebrewCalendar.GetDayOfWeek(dt);
        }
    }

    public enum LatitudeTypeEnum
    {
        North = 1, South = 2
    }

    public enum LongitudeTypeEnum
    {
        East = 1, West = 2
    }

    enum Occurrence
    {
        Sunrise, Sunset
    }

    internal class TimesCalculationInternal
    {
        private bool leap(int y)
        {
            if (y % 400 == 0)
            {
                return true;
            }

            if (y % 100 != 0)
            {
                if (y % 4 == 0)
                {
                    return true;
                }
            }
            return false;
        }

        private int doy(int d, int m, int y)
        {
            int[] monCount = { 0, 1, 32, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366 };

            if ((m > 2) && (leap(y)))
            {
                return monCount[m] + d + 1;
            }
            else
            {
                return monCount[m] + d;
            }
        }

        private double todec(double deg, double min)
        {
            return (deg + min / 60.0);
        }

        private double M(double x)
        {
            return (0.9856 * x - 3.251);
        }

        private double L(double x)
        {
            return (x + 1.916 * Math.Sin(0.01745 * x) + 0.02 * Math.Sin(2 * 0.01745 * x) + 282.565);
        }

        private double adj(double x)
        {
            return (-0.06571 * x - 6.620);
        }

        private double rad2deg(double rad)
        {
            return 57.29578 * rad;
        }

        private double[] suntime(int d, int m, int y,
                    int zendeg, int zenmin,
                    int londeg, int lonmin, int ew,
                    int latdeg, int latmin, int ns,
                    int tz, int elevation)
        {
            double lonhr;
            double longitude, latitude;
            double coslat, sinlat, cosz;
            double sindec, cosdec;
            double xm_rise, xm_set;
            double xl_rise, xl_set;
            double a_rise, a_set;
            double ahr_rise, ahr_set;
            double h_rise, h_set;
            double t_rise, t_set;
            double ut_rise, ut_set;

            int day;

            if (zendeg == 90)
            {
                double z = zendeg + zenmin / 60.0;
                double earthRadiusInMeters = 6356.9 * 1000.0;
                double elevationAdjustment = rad2deg(Math.Acos(earthRadiusInMeters / (earthRadiusInMeters + elevation)));
                z += elevationAdjustment;
                zendeg = (int)Math.Floor(z);
                zenmin = (int)((z - Math.Floor(z)) * 60.0);
            }

            day = doy(d, m, y);

            cosz = Math.Cos(0.01745 * todec(zendeg, zenmin));

            longitude = todec(londeg, lonmin) * ((ew == 0) ? 1 : -1);
            lonhr = longitude / 15.0;
            latitude = todec(latdeg, latmin) * ((ns == 0) ? 1 : -1);
            coslat = Math.Cos(0.01745 * latitude);
            sinlat = Math.Sin(0.01745 * latitude);

            t_rise = day + (6.0 + lonhr) / 24.0;
            t_set = day + (18.0 + lonhr) / 24.0;

            xm_rise = M(t_rise);
            xl_rise = L(xm_rise);
            xm_set = M(t_set);
            xl_set = L(xm_set);

            a_rise = 57.29578 * Math.Atan(0.91746 * Math.Tan(0.01745 * xl_rise));
            a_set = 57.29578 * Math.Atan(0.91746 * Math.Tan(0.01745 * xl_set));

            if (Math.Abs(a_rise + 360.0 - xl_rise) > 90.0)
            {
                a_rise += 180.0;
            }

            if (a_rise > 360.0)
            {
                a_rise -= 360.0;
            }

            if (Math.Abs(a_set + 360.0 - xl_set) > 90.0)
            {
                a_set += 180.0;
            }

            if (a_set > 360.0)
            {
                a_set -= 360.0;
            }

            ahr_rise = a_rise / 15.0;
            sindec = 0.39782 * Math.Sin(0.01745 * xl_rise);
            cosdec = Math.Sqrt(1.0 - sindec * sindec);
            h_rise = (cosz - sindec * sinlat) / (cosdec * coslat);

            ahr_set = a_set / 15.0;
            sindec = 0.39782 * Math.Sin(0.01745 * xl_set);
            cosdec = Math.Sqrt(1.0 - sindec * sindec);
            h_set = (cosz - sindec * sinlat) / (cosdec * coslat);

            if (Math.Abs(h_rise) <= 1.0)
            {
                h_rise = 57.29578 * Math.Acos(h_rise);
            }
            else
            {
                return null; //NO_SUNRISE;
            }

            if (Math.Abs(h_set) <= 1.0)
            {
                h_set = 57.29578 * Math.Acos(h_set);
            }
            else
            {
                return null; //NO_SUNSET;
            }

            ut_rise = ((360.0 - h_rise) / 15.0) + ahr_rise + adj(t_rise) + lonhr;
            ut_set = (h_rise / 15.0) + ahr_set + adj(t_set) + lonhr;

            double sunrise = ut_rise + tz;  // sunrise
            double sunset = ut_set + tz;  // sunset
            double[] result = { sunrise, sunset };
            return result;
        }
        private AstronomicalTime timeadj(double t)
        {
            int hour, min;
            double time;

            time = t;

            if (time < 0)
            {
                time += 24.0;
            }

            hour = (int)Math.Floor(time);
            min = (int)Math.Floor((time - hour) * 60.0 + 0.5);

            if (min >= 60)
            {
                hour += 1;
                min -= 60;
            }

            if (hour > 24)
            {
                hour -= 24;
            }

            return new AstronomicalTime(hour, min);
        }

        public AstronomicalTime GetSunsetOrSunrise(DateTime currentDate,
                    Occurrence occurrence,
                    Place place)
        {
            double[] sunriseSunset = suntime(currentDate.Day, currentDate.Month, currentDate.Year,
                90, 50,
                place.LongitudeDegrees, place.LongitudeMinutes, (place.LongitudeType == LongitudeTypeEnum.West) ? 0 : 1,
                place.LatitudeDegrees, place.LatitudeMinutes, (place.LatitudeType == LatitudeTypeEnum.South) ? 1 : 0,
                place.TimeZone, place.Elevation);
            if (sunriseSunset != null)
            {
                AstronomicalTime sunrise = timeadj(sunriseSunset[0]);
                AstronomicalTime sunset = timeadj(sunriseSunset[1]);

                while (sunrise.Hour > 12)
                {
                    sunrise.Hour -= 12;
                }

                while (sunset.Hour < 12)
                {
                    sunset.Hour += 12;
                }

                AstronomicalTime result = null;
                if (occurrence == Occurrence.Sunrise)
                {
                    result = sunrise;
                }

                if (occurrence == Occurrence.Sunset)
                {
                    result = sunset;
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public AstronomicalTime GetDegreesBelowHorizon(DateTime currentDate,
                    double DegreesBelowHorizon,
                    Place place)
        {
            double[] sunriseSunset = suntime(currentDate.Day, currentDate.Month, currentDate.Year,
                90, 50,
                place.LongitudeDegrees, place.LongitudeMinutes, (place.LongitudeType == LongitudeTypeEnum.West) ? 0 : 1,
                place.LatitudeDegrees, place.LatitudeMinutes, (place.LatitudeType == LatitudeTypeEnum.South) ? 1 : 0,
                place.TimeZone, place.Elevation);
            if (sunriseSunset != null)
            {
                double db = DegreesBelowHorizon + 90.0;
                int deghour = (int)db;
                db = db - deghour;
                db *= 60.0;
                int degmin = (int)db;

                double[] sunriseSunset2 = suntime(currentDate.Day, currentDate.Month, currentDate.Year,
                    deghour, degmin,
                    place.LongitudeDegrees, place.LongitudeMinutes, (place.LongitudeType == LongitudeTypeEnum.West) ? 0 : 1,
                    place.LatitudeDegrees, place.LatitudeMinutes, (place.LatitudeType == LatitudeTypeEnum.South) ? 1 : 0,
                    place.TimeZone, place.Elevation);
                if (sunriseSunset2 != null)
                {
                    AstronomicalTime sunset = timeadj(sunriseSunset[1]);
                    AstronomicalTime sunset2 = timeadj(sunriseSunset2[1]);

                    while (sunset.Hour < 12)
                    {
                        sunset.Hour += 12;
                    }

                    while (sunset2.Hour < 12)
                    {
                        sunset2.Hour += 12;
                    }

                    int iS = sunset.Hour * 60 + sunset.Minute;
                    int iS2 = sunset2.Hour * 60 + sunset2.Minute;
                    int iDiff = iS2 - iS;

                    return new AstronomicalTime(iDiff / 60, iDiff % 60);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}