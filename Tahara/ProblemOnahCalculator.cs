using System;
using System.Collections.Generic;
using System.Linq;

namespace Tahara
{
    public class ProblemOnahCalculator
    {
        public DateTime Today { get; set; }
        public Onah NowOnah { get; set; }
        public int NumberMonthsAheadToWarn { get; set; }
        public bool DilugChodeshPastEnds { get; set; }
        public bool KeepLongerHaflaga { get; set; }
        public bool OnahBenIs24Hours { get; set; }
        public bool ShowOhrZeruah { get; set; }
        public List<Entry> EntryList { get; set; }
        public List<Kavuah> KavuahList { get; set; }
        public List<Onah> ProblemOnahList { get; private set; }

        #region Calculate Problem Onahs

        public List<Onah> CalculateProblemOnahs()
        {
            //Clears the list and gets it ready to accept new problems
            if (this.ProblemOnahList == null)
            {
                this.ProblemOnahList = new List<Onah>();
            }
            else
            {
                this.ProblemOnahList.Clear();
            }

            //A list of Onahs that need to be kept. This first list is worked out from the list of Entries.
            //Problem Onahs are searched for from the date of each entry until the number of months specified in the
            //Property Setting "numberMonthsAheadToWarn"
            this.SetEntryListDependentProblemOnahs();

            //Get the onahs that need to be kept for Kavuahs of yom hachodesh, sirug,
            //and other Kavuahs that are not dependent on the actual entry list
            this.SetIndependentKavuahProblemOnahs();

            //Clean out doubles
            Onah.ClearDoubleOnahs(this.ProblemOnahList);
            this.ProblemOnahList.Sort(Onah.CompareOnahs);

            return this.ProblemOnahList;
        }


        public string GetNextOnahText()
        {
            string nextProblemText = "";
            if (this.ProblemOnahList.Count > 0)
            {
                //We need to determine the earliest problem Onah, so we need to do a
                //special sort on the list where the night Onah is before the day one for the same date.
                this.ProblemOnahList.Sort(Onah.CompareOnahs);

                Onah nowProblem = this.ProblemOnahList.FirstOrDefault(o => (!o.IsIgnored) && Onah.IsSameOnahPeriod(o, this.NowOnah));
                Onah nextProblem = this.ProblemOnahList.FirstOrDefault(o => (!o.IsIgnored) && (Onah.CompareOnahs(o, this.NowOnah) == 1));

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
                        ((nextProblem.DateTime - this.Today).Days + 1).ToString() +
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

        private void SetEntryDependentKavuahProblemOnahs(Entry entry)
        {
            //Kavuah Haflagah - with or without Maayan Pasuach
            foreach (Kavuah kavuah in this.KavuahList.Where(k =>
                k.KavuahType.In(KavuahType.Haflagah, KavuahType.HaflagaMaayanPasuach) && k.Active))
            {
                Onah kavuahHaflaga = entry.AddDays(kavuah.Number - 1);
                kavuahHaflaga.DayNight = kavuah.DayNight;
                this.AddProblemOnah(kavuahHaflaga, "קבוע " + kavuah.ToString());
            }

            //Kavuah Dilug Haflagos - from each actual entry. 
            foreach (Kavuah kavuah in this.KavuahList.Where(k =>
                k.KavuahType == KavuahType.DilugHaflaga && k.Active))
            {
                Onah kavuahDilugHaflaga = entry.AddDays(entry.Interval + kavuah.Number - 1);
                kavuahDilugHaflaga.DayNight = kavuah.DayNight;
                this.AddProblemOnah(kavuahDilugHaflaga, "קבוע " + kavuah.ToString() + " ע\"פ ראייה");
            }
        }

        private void SetEntryListDependentProblemOnahs()
        {
            IEnumerable<Entry> entryList = this.EntryList.Where(en => !en.IsInvisible);
            foreach (Entry entry in entryList)
            {
                this.SetOnahBeinenisProblemOnahs(entry, entryList.Where(e => e != entry));
                if (entry >= entryList.Last())
                {
                    this.SetEntryDependentKavuahProblemOnahs(entry);
                }
            }
        }

        /// <summary>
        /// Work out the Kavuahs of yom hachodesh, sirug, dilug Yom hachodesh
        /// and other Kavuahs that are not dependant on the entry list
        /// </summary>
        /// <param name="onahs"></param>
        /// <returns></returns>
        private void SetIndependentKavuahProblemOnahs()
        {
            //Kavuahs of Yom Hachodesh and Sirug
            foreach (Kavuah kavuah in this.KavuahList.Where(k =>
                                        k.Active &&
                                        k.SettingEntryDate > DateTime.MinValue &&
                                        k.KavuahType.In(KavuahType.DayOfMonth,
                                                        KavuahType.DayOfMonthMaayanPasuach,
                                                        KavuahType.Sirug)))
            {
                for (DateTime dt = Utils.HebrewCalendar.AddMonths(kavuah.SettingEntryDate,
                        kavuah.KavuahType == KavuahType.Sirug ? kavuah.Number : 1);
                    dt <= Utils.HebrewCalendar.AddMonths(this.Today, this.NumberMonthsAheadToWarn);
                    dt = Utils.HebrewCalendar.AddMonths(dt, (kavuah.KavuahType == KavuahType.Sirug ? kavuah.Number : 1)))
                {
                    this.AddProblemOnah(
                        onah: new Onah(dt, kavuah.DayNight)
                        {
                            Day = Utils.HebrewCalendar.GetDayOfMonth(kavuah.SettingEntryDate)
                        },
                        name: "קבוע " + kavuah.ToString());
                }
            }

            //Kavuahs of "Day of week"
            foreach (Kavuah kavuah in this.KavuahList.Where(k =>
                k.KavuahType == KavuahType.DayOfWeek && k.Active))
            {
                for (DateTime dt = kavuah.SettingEntryDate.AddDays(kavuah.Number);
                    dt <= Utils.HebrewCalendar.AddMonths(this.Today, this.NumberMonthsAheadToWarn);
                    dt = dt.AddDays(kavuah.Number))
                {
                    this.AddProblemOnah(new Onah(dt, kavuah.DayNight), "קבוע " + kavuah.ToString());
                }
            }

            //Kavuahs of Yom Hachodesh of Dilug
            foreach (Kavuah kavuah in this.KavuahList.Where(k =>
                                        k.Active &&
                                        k.KavuahType == KavuahType.DilugDayOfMonth))
            {
                DateTime dt = kavuah.SettingEntryDate;
                for (int i = 0; i >= 0; i++)
                {
                    dt = Utils.HebrewCalendar.AddMonths(dt, 1);
                    DateTime dtNext = dt.AddDays(kavuah.Number * i);
                    //We stop when we get to the beginning or end of the month
                    if ((!this.DilugChodeshPastEnds && dtNext.Month != dt.Month) ||
                        dtNext > Utils.HebrewCalendar.AddMonths(this.Today, this.NumberMonthsAheadToWarn))
                    {
                        break;
                    }

                    this.AddProblemOnah(new Onah(dtNext, kavuah.DayNight), "קבוע " + kavuah.ToString());
                }
            }
        }

        private void SetOnahBeinenisProblemOnahs(Entry entry, IEnumerable<Entry> entryList)
        {
            Kavuah cancelKavuah =
                this.KavuahList.LastOrDefault(k => k.Active && k.CancelsOnahBeinanis);

            //Yom Hachodesh
            this.AddOnahBeinunisProblem(entry.AddMonths(1), "יום החודש", cancelKavuah);

            //The haflaga based probelm onahs are only calculated from the last entry
            if (entry < entryList.LastOrDefault())
            {
                return;
            }

            //Day Thirty
            this.AddOnahBeinunisProblem(entry.AddDays(29), "יום שלושים", cancelKavuah);

            //Day Thirty One
            this.AddOnahBeinunisProblem(entry.AddDays(30), "יום ל\"א", cancelKavuah);

            //Haflagah
            if (entry.Interval > 1)
            {
                Onah onasHaflagah = entry.AddDays(entry.Interval - 1);
                if (this.KeepLongerHaflaga ||
                    !entryList.Any(e => e > entry && e < onasHaflagah))
                {
                    //Note the Haflaga is always just the Onah it occurred on - not 24 hours  -
                    //even according to those that require it for 30, 31 and Yom Hachodesh.
                    this.AddProblemOnah(onasHaflagah,
                        "יום הפלגה (" + entry.Interval + ")", cancelKavuah);
                }

                //The Ta"z
                if (this.KeepLongerHaflaga)
                {
                    //Go through all earlier entries in the list that have a longer haflaga than this one  - 
                    //and they are not kept anyways due to onah Beinonis
                    foreach (Entry e in entryList.Where(en =>
                                        en < entry && en.Interval > entry.Interval &&
                                        en.Interval != 30 && en.Interval != 31))
                    {
                        //See if their haflaga was never surpassed by an Entry after them
                        if (!entryList.Any(oe => oe > e && oe.Interval > e.Interval))
                        {
                            this.AddProblemOnah(entry.AddDays(e.Interval - 1),
                                  "יום הפלגה (" + e.Interval + ") שלא נתבטלה", cancelKavuah);
                        }
                    }
                }
            }
        }

        private void AddOnahBeinunisProblem(Onah probOnah, string name, Kavuah cancelKavuah)
        {
            //We don't flag the Ohr Zarua if it's included in Onah Beinonis of 24 hours as Onah Beinonis is stricter.
            this.AddProblemOnah(probOnah, name, cancelKavuah,
                noOhrZarua: this.OnahBenIs24Hours && probOnah.DayNight == DayNight.Day);

            //If the user wants to keep 24 for the Onah Beinenis
            if (this.OnahBenIs24Hours)
            {
                Onah otherOnah = probOnah.Clone();
                otherOnah.DayNight = otherOnah.DayNight == DayNight.Day ? DayNight.Night : DayNight.Day;
                this.AddProblemOnah(otherOnah, name, cancelKavuah, noOhrZarua: true);
            }
        }

        private void AddProblemOnah(Onah onah, string name, Kavuah cancelKavuah = null, bool noOhrZarua = false)
        {
            onah.Name = name;
            onah.IsIgnored = cancelKavuah != null && cancelKavuah.SettingOnah < onah;
            this.ProblemOnahList.Add(onah);

            if (!noOhrZarua && this.ShowOhrZeruah)
            {
                this.AddProblemOnah(Onah.GetPreviousOnah(onah), "או\"ז של " + name,
                    cancelKavuah,
                    noOhrZarua: true);
            }
        }
        #endregion Calculate Problem Onahs

    }
}
