using System;
using System.Collections.Generic;
using System.Linq;

namespace Chashavshavon
{
    public static class ProblemOnahs
    {
        public static List<Onah> ProblemOnahList { get; private set; }

        #region Calculate Problem Onahs

        public static void CalculateProblemOnahs()
        {
            //Clears the list and gets it ready to accept new problems
            if (ProblemOnahList == null)
            {
                ProblemOnahList = new List<Onah>();
            }
            else
            {
                ProblemOnahList.Clear();
            }

            //A list of Onahs that need to be kept. This first list is worked out from the list of Entries.
            //Problem Onahs are searched for from the date of each entry until the number of months specified in the
            //Property Setting "numberMonthsAheadToWarn"
            SetEntryListDependentProblemOnahs();

            //Get the onahs that need to be kept for Kavuahs of yom hachodesh, sirug,
            //and other Kavuahs that are not dependent on the actual entry list
            SetIndependentKavuahProblemOnahs();

            //Clean out doubles
            Onah.ClearDoubleOnahs(ProblemOnahList);
        }


        public static string GetNextOnahText()
        {
            string nextProblemText = "";
            if (ProblemOnahList.Count > 0)
            {
                //We need to determine the earliest problem Onah, so we need to do a
                //special sort on the list where the night Onah is before the day one for the same date.
                ProblemOnahList.Sort(Onah.CompareOnahs);

                Onah nowProblem = ProblemOnahList.FirstOrDefault(o => (!o.IsIgnored) && Onah.IsSameOnahPeriod(o, Program.NowOnah));
                Onah nextProblem = ProblemOnahList.FirstOrDefault(o => (!o.IsIgnored) && (Onah.CompareOnahs(o, Program.NowOnah) == 1));

                if (nowProblem != null)
                {
                    nextProblemText += "עכשיו הוא " + nowProblem.Name;
                }

                if (nextProblem != null)
                {
                    if (nextProblemText.Length > 0)
                    {
                        nextProblemText += " - ";
                    }
                    nextProblemText += "העונה הבאה בעוד " +
                        ((nextProblem.DateTime - Program.Today).Days + 1).ToString() +
                        " ימים - בתאריך: " +
                        nextProblem.DateTime.ToString("dd MMMM yyyy") +
                        " (" +
                        nextProblem.HebrewDayNight +
                        ") שהוא " +
                        nextProblem.Name;
                }
            }
            return nextProblemText;
        }

        private static void SetEntryDependentKavuahProblemOnahs(Entry entry)
        {
            //Kavuah Haflagah - with or without Maayan Pasuach
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                k.KavuahType.In(KavuahType.Haflagah, KavuahType.HaflagaMaayanPasuach) && k.Active))
            {
                Onah kavuahHaflaga = entry.AddDays(kavuah.Number - 1);
                kavuahHaflaga.DayNight = kavuah.DayNight;
                AddProblemOnah(kavuahHaflaga, "קבוע " + kavuah.ToString());
            }

            //Kavuah Dilug Haflagos - from each actual entry. 
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                k.KavuahType == KavuahType.DilugHaflaga && k.Active))
            {
                Onah kavuahDilugHaflaga = entry.AddDays(entry.Interval + kavuah.Number - 1);
                kavuahDilugHaflaga.DayNight = kavuah.DayNight;
                AddProblemOnah(kavuahDilugHaflaga, "קבוע " + kavuah.ToString() + " ע\"פ ראייה");
            }
        }

        private static void SetEntryListDependentProblemOnahs()
        {
            var entryList = Entry.EntryList.Where(en => !en.IsInvisible);
            foreach (Entry entry in entryList)
            {
                SetOnahBeinenisProblemOnahs(entry, entryList.Where(e => e != entry));
                SetEntryDependentKavuahProblemOnahs(entry);
            }
        }

        /// <summary>
        /// Work out the Kavuahs of yom hachodesh, sirug, dilug Yom hachodesh
        /// and other Kavuahs that are not dependant on the entry list
        /// </summary>
        /// <param name="onahs"></param>
        /// <returns></returns>
        private static void SetIndependentKavuahProblemOnahs()
        {
            //Kavuahs of Yom Hachodesh and Sirug
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                                        k.Active &&
                                        k.SettingEntryDate > DateTime.MinValue &&
                                        k.KavuahType.In(KavuahType.DayOfMonth,
                                                        KavuahType.DayOfMonthMaayanPasuach,
                                                        KavuahType.Sirug)))
            {
                for (DateTime dt = Program.HebrewCalendar.AddMonths(kavuah.SettingEntryDate,
                        kavuah.KavuahType == KavuahType.Sirug ? kavuah.Number : 1);
                    dt <= Program.HebrewCalendar.AddMonths(Program.Today, Properties.Settings.Default.NumberMonthsAheadToWarn);
                    dt = Program.HebrewCalendar.AddMonths(dt, (kavuah.KavuahType == KavuahType.Sirug ? kavuah.Number : 1)))
                {
                    AddProblemOnah(
                        onah: new Onah(dt, kavuah.DayNight)
                        {
                            Day = Program.HebrewCalendar.GetDayOfMonth(kavuah.SettingEntryDate)
                        },
                        name: "קבוע " + kavuah.ToString());
                }
            }

            //Kavuahs of "Day of week"
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                k.KavuahType == KavuahType.DayOfWeek && k.Active))
            {
                for (DateTime dt = kavuah.SettingEntryDate.AddDays(kavuah.Number);
                    dt <= Program.HebrewCalendar.AddMonths(Program.Today, Properties.Settings.Default.NumberMonthsAheadToWarn);
                    dt = dt.AddDays(kavuah.Number))
                {
                    AddProblemOnah(new Onah(dt, kavuah.DayNight), "קבוע " + kavuah.ToString());                    
                }
            }

            //Kavuahs of Yom Hachodesh of Dilug
            foreach (Kavuah kavuah in Kavuah.KavuahsList.Where(k =>
                                        k.Active &&
                                        k.KavuahType == KavuahType.DilugDayOfMonth))
            {
                DateTime dt = kavuah.SettingEntryDate;
                for (int i = 0; i >= 0; i++)
                {
                    dt = Program.HebrewCalendar.AddMonths(dt, 1);
                    DateTime dtNext = dt.AddDays(kavuah.Number * i);
                    //We stop when we get to the beginning or end of the month
                    if (dtNext.Month != dt.Month ||
                        dtNext > Program.HebrewCalendar.AddMonths(Program.Today, Properties.Settings.Default.NumberMonthsAheadToWarn))
                    {
                        break;
                    }
                    
                    AddProblemOnah(new Onah(dtNext, kavuah.DayNight), "קבוע " + kavuah.ToString());
                }
            }
        }

        private static void SetOnahBeinenisProblemOnahs(Entry entry, IEnumerable<Entry> entryList)
        {
            Kavuah cancelKavuah =
                Kavuah.KavuahsList.LastOrDefault(k => k.Active && k.CancelsOnahBeinanis);

            //Yom Hachodesh
            AddOnahBeinunisProblem(entry.AddMonths(1), "יום החודש", cancelKavuah);

            //Day Thirty
            AddOnahBeinunisProblem(entry.AddDays(29), "יום שלושים", cancelKavuah);

            //Day Thirty One
            AddOnahBeinunisProblem(entry.AddDays(30), "יום ל\"א", cancelKavuah);


            //Haflagah
            if (entry.Interval > 1)
            {
                Onah intervalHaflagah = entry.AddDays(entry.Interval - 1);
                if (Properties.Settings.Default.KeepLongerHaflagah ||
                    !entryList.Any(e => e > entry && e < intervalHaflagah))
                {
                    //Note the Haflaga is always just the Onah it occurred on - not 24 hours  -
                    //even according to those that require it for 30, 31 and Yom Hachodesh.
                    AddProblemOnah(intervalHaflagah,
                        "יום הפלגה (" + entry.Interval + ")", cancelKavuah);
                }

                //The Ta"z
                if (Properties.Settings.Default.KeepLongerHaflagah)
                {
                    //Go through all earlier entries in the list that have a longer haflaga than this one
                    foreach (Entry e in entryList.Where(en => en < entry && en.Interval > entry.Interval))
                    {
                        //See if their haflaga was never surpassed by an Entry after them
                        if (!entryList.Any(oe => oe > e && oe.Interval > e.Interval))
                        {
                            AddProblemOnah(e.AddDays(entry.Interval - 1),
                                  "יום הפלגה (" + entry.Interval + ") שלא נתבטלה", cancelKavuah);
                        }
                    }
                }
            }
        }

        private static void AddOnahBeinunisProblem(Onah probOnah, string name, Kavuah cancelKavuah)
        {
            //We don't flag the Ohr Zarua if it's included in Onah Beinonis of 24 hours as Onah Beinonis is stricter.
            AddProblemOnah(probOnah, name, cancelKavuah,
                noOhrZarua: Properties.Settings.Default.OnahBenIs24Hours && probOnah.DayNight == DayNight.Day);

            //If the user wants to keep 24 for the Onah Beinenis
            if (Properties.Settings.Default.OnahBenIs24Hours)
            {
                Onah otherOnah = probOnah.Clone();
                otherOnah.DayNight = otherOnah.DayNight == DayNight.Day ? DayNight.Night : DayNight.Day;
                AddProblemOnah(otherOnah, name, cancelKavuah, noOhrZarua: true);
            }
        }

        private static void AddProblemOnah(Onah onah, string name, Kavuah cancelKavuah = null, bool noOhrZarua = false)
        {
            onah.Name = name;
            onah.IsIgnored = cancelKavuah != null && cancelKavuah.SettingOnah < onah;
            ProblemOnahList.Add(onah);

            if (!noOhrZarua && Properties.Settings.Default.ShowOhrZeruah)
            {
                AddProblemOnah(Onah.GetPreviousOnah(onah), "או\"ז של " + name,
                    cancelKavuah,
                    noOhrZarua: true);
            }
        }
        #endregion Calculate Problem Onahs

    }
}
