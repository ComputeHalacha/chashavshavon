using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tahara
{
    public enum KavuahType
    {
        Haflagah,
        DayOfMonth,
        DayOfWeek,
        Sirug,
        DilugHaflaga,
        DilugDayOfMonth,
        HaflagaMaayanPasuach,
        DayOfMonthMaayanPasuach
    }
    [Serializable()]
    public class Kavuah
    {
        /// <summary>
        /// Rate the strength of each Kavuah Type. Lower is better.
        /// </summary>
        public static readonly Dictionary<KavuahType, int> KavuahTypeRatings =
            new Dictionary<KavuahType, int>
            {
                {KavuahType.DayOfMonth, 1},
                {KavuahType.DayOfWeek, 1},
                {KavuahType.Haflagah, 2},
                {KavuahType.Sirug, 3},
                {KavuahType.DilugHaflaga, 4},
                {KavuahType.DilugDayOfMonth, 4},
                {KavuahType.HaflagaMaayanPasuach, 5},
                {KavuahType.DayOfMonthMaayanPasuach, 5}
            };

        public Kavuah()
        {
            //Set default to true
            this.Active = true;
        }

        public Kavuah(DayNight dayNight) : this()
        {
            this.DayNight = dayNight;
        }

        public Kavuah(KavuahType kavuahType, DayNight dayNight) : this(dayNight)
        {
            this.KavuahType = kavuahType;
        }

        #region Public Instance Properties
        public DayNight DayNight { get; set; }
        public KavuahType KavuahType { get; set; }
        public int Number { get; set; }
        public bool Active { get; set; }
        public bool IsMaayanPasuach { get; set; }
        public bool CancelsOnahBeinanis { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public Entry SettingEntry { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public Onah SettingOnah => this.SettingEntry ?? new Onah(this.SettingEntryDate, this.DayNight);
        public DateTime SettingEntryDate { get; set; }
        public int SettingEntryInterval { get; set; }
        public string Notes { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public string KavuahDescriptionHebrew => this.ToString();
        #endregion

        #region Public Functions

        /// <summary>
        /// Is this Kavuah a "stronger" Kavua type than the given one
        /// </summary>
        /// <param name="kavuah"></param>
        /// <returns></returns>
        private bool IsBetterKavuah(Kavuah kavuah)
        {
            return KavuahTypeRatings[this.KavuahType] < KavuahTypeRatings[kavuah.KavuahType];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            switch (this.KavuahType)
            {
                case KavuahType.Haflagah:
                    sb.AppendFormat("הפלגה - {0} ימים ", this.Number);
                    break;
                case KavuahType.DayOfMonth:
                    sb.AppendFormat("יום החדש - {0} בחדש ", Utils.DaysOfMonthHebrew[this.Number]);
                    break;
                case KavuahType.DayOfWeek:
                    sb.AppendFormat("יום השבוע - {0} כל {1} שבועות ",
                        Utils.CultureInfo.DateTimeFormat.GetDayName(this.SettingEntryDate.DayOfWeek),
                        this.Number / 7);
                    break;
                case KavuahType.Sirug:
                    sb.AppendFormat("יום החודש בסירוג - {0} לחודש כל {1} חודשים",
                        this.SettingEntryDate.ToString("dd"),
                        this.Number);
                    break;
                case KavuahType.HaflagaMaayanPasuach:
                    sb.AppendFormat("הפלגה - {0} ימים - ע\"פ מעיין פתוח ", this.Number);
                    break;
                case KavuahType.DayOfMonthMaayanPasuach:
                    sb.AppendFormat("יום החדש - {0} בחדש - ע\"פ מעיין פתוח ", Utils.DaysOfMonthHebrew[this.Number]);
                    break;
                case KavuahType.DilugHaflaga:
                    sb.AppendFormat("הפלגה בדילוג ({1}{0}) ימים ",
                        (this.Number < 0 ? "-" : "+"),
                        Math.Abs(this.Number));
                    break;
                case KavuahType.DilugDayOfMonth:
                    sb.AppendFormat("יום החודש בדילוג {1}{0} ימים ",
                        (this.Number < 0 ? "-" : "+"),
                        this.Number);
                    break;
            }
            //The user can set IsMaayanPasuach for any Kavuah in frmKavuahs
            if (this.IsMaayanPasuach &&
                !this.KavuahType.In(KavuahType.HaflagaMaayanPasuach, KavuahType.DayOfMonthMaayanPasuach))
            {
                sb.Append(" - ע\"פ מעיין פתוח ");
            }
            sb.AppendFormat("[עונת {0}] {1}", (this.DayNight == DayNight.Day ? "יום" : "לילה"), this.Notes);
            return sb.ToString();
        }
        #endregion

        #region Public Static Functions        
        /// <summary>
        /// Removes double Kavuahs from list
        /// </summary>
        /// <param name="list"></param>
        public static void ClearDoubleKavuahs(List<Kavuah> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list.Exists(ka => ka.Active &&
                    IsSimilarKavuah(ka, list[i]) /*|| ka.IsBetterKavuah(list[i])*/))
                {
                    list.RemoveAt(i);
                }
            }
        }
        #endregion

        #region Private Static Functions
        private static bool IsSimilarKavuah(Kavuah a, Kavuah b)
        {
            return (a != b &&
                a.KavuahType == b.KavuahType &&
                a.DayNight == b.DayNight &&
                a.Number == b.Number);
        }

        private static bool HasSameSettingEntry(Kavuah a, Kavuah b)
        {
            return (a != b &&
                Onah.IsSameOnahPeriod(a.SettingOnah, b.SettingOnah));
        }

        private static bool InActiveKavuahList(Kavuah kavuah, List<Kavuah> kavuahList)
        {
            return kavuahList.Exists(k => IsSimilarKavuah(k, kavuah) && k.Active);
        }

        /// <summary>
        /// Works out all possible Kavuahs from the current list of entries.         
        /// </summary>
        /// <returns></returns>
        public static List<Kavuah> GetProposedKavuahList(List<Entry> entryList, List<Kavuah> prevKavuahs)
        {
            var foundKavuahList = new List<Kavuah>();
            var qSetOfThree = new Queue<Entry>();

            foreach (Entry entry in entryList.Where(en => !en.IsInvisible))
            {
                //First we work out those Kavuahs that are not dependent on their Entries being 3 in a row
                FindDayOfMonthKavuah(entry, entryList, foundKavuahList);
                FindDilugDayOfMonthKavuah(entry, entryList, foundKavuahList);
                FindDayOfWeekKavuah(entry, entryList, foundKavuahList);

                //For cheshboning out the other Kavuahs we need the last 3 entries
                //First, add the current entry of the loop.
                qSetOfThree.Enqueue(entry);
                //if the queue is too "full"
                if (qSetOfThree.Count > 3)
                {
                    //pop out the earliest one - leaves us with just this entry and the previous 2.
                    qSetOfThree.Dequeue();
                }

                //We can't stat cheshboning until we have 3 entries to compare to each other
                if (qSetOfThree.Count == 3)
                {
                    Entry[] last3Array = qSetOfThree.ToArray<Entry>();

                    //Work out the Kavuah types that need all the entries to be of the same DayNight onah                    
                    if ((last3Array[0].DayNight == last3Array[1].DayNight) &&
                        (last3Array[1].DayNight == last3Array[2].DayNight))
                    {
                        FindHaflagahKavuah(last3Array, foundKavuahList);
                        FindSirugKavuah(last3Array, foundKavuahList);
                        FindDilugHaflagahKavuah(last3Array, foundKavuahList);
                    }
                    //The Maayan-Pasuach Haflagah needs 3 in-a-row but doesn't need to be the same DayNight
                    FindMaayanPasuachHaflagahKavuah(last3Array, prevKavuahs, foundKavuahList);
                }
            }

            //Remove double finds
            ClearDoubleKavuahs(foundKavuahList);

            //Remove all found kavuahs that are already in the active list
            foundKavuahList.RemoveAll(k => InActiveKavuahList(k, prevKavuahs));

            return foundKavuahList;
        }

        /// <summary>
        /// Cheshbon out Kavuah of Sirug
        /// We go here according to those that Sirug Kavuahs need 3 in a row with no intervening entries
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="kavuahs"></param>
        private static void FindSirugKavuah(Entry[] entries, List<Kavuah> kavuahs)
        {
            //We get the difference in months between the first 2 entries
            int monthDiff = Convert.ToInt32(DateAndTime.DateDiff(DateInterval.Month,
                entries[0].DateTime,
                entries[1].DateTime));
            //If the difference is 1, than it can not be a Sirug Kavuah - rather it may be a DayOfMonth kavuah.
            //We now check to see if the third Entry is the same number of months 
            //after the second one, and that all 3 entries are on the same day of the month.
            if (monthDiff > 1 &&
                (entries[0].Day == entries[1].Day) &&
                (entries[1].Day == entries[2].Day) &&
                (DateAndTime.DateDiff(DateInterval.Month, entries[1].DateTime, entries[2].DateTime) == monthDiff))
            {
                //If the "NoKavuah" list for the 3rd entry does not include this "find"
                if (!entries[2].NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.Sirug) &&
                            (k.Number == monthDiff)))
                {
                    kavuahs.Add(new Kavuah(KavuahType.Sirug, entries[0].DayNight)
                    {
                        SettingEntry = entries[2],
                        SettingEntryDate = entries[2].DateTime,
                        SettingEntryInterval = entries[2].Interval,
                        CancelsOnahBeinanis = true,
                        Number = monthDiff
                    });
                }
            }
        }

        /// <summary>
        /// Cheshbon out Kavuah of same haflagah
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="kavuahs"></param>            
        private static void FindHaflagahKavuah(Entry[] entries, List<Kavuah> kavuahs)
        {
            //We compare the intervals between the entries, if they are the same, we have a Kavuah
            if ((entries[0].Interval == entries[1].Interval) &&
                (entries[1].Interval == entries[2].Interval))
            {
                //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                if (!entries[2].NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.Haflagah) &&
                            (k.Number == entries[0].Interval)))
                {
                    kavuahs.Add(new Kavuah(KavuahType.Haflagah, entries[0].DayNight)
                    {
                        SettingEntry = entries[2],
                        SettingEntryDate = entries[2].DateTime,
                        SettingEntryInterval = entries[2].Interval,
                        CancelsOnahBeinanis = true,
                        Number = entries[0].Interval
                    });
                }
            }
        }

        /// <summary>
        /// Cheshbon out Ma'ayan Pasuachs of haflaga - this does not need that the last one is the same DayNight
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="kavuahs"></param>
        private static void FindMaayanPasuachHaflagahKavuah(Entry[] entries, List<Kavuah> prevKavuahs, List<Kavuah> kavuahs)
        {
            //We check the entries to see if the first 2 entries have the same interval and the third
            //one is within 5 days before the projected third interval. 
            //This 3rd entry does not need to be in the same Onah as the first 2 as there was a Ma'ayan Pasuach during the correct Onah.
            //Note, the number 5 is just a generality, Ma'ayan Pasuach is dependent on the actual length of the period
            if ((entries[0].Interval == entries[1].Interval) &&
                (entries[0].DayNight == entries[1].DayNight) &&
                (entries[2].Interval < entries[1].Interval) &&
                (entries[2].Interval > (entries[1].Interval - 5)))
            {
                //"NoKavuah" list for the 3rd entry does not include this "find" and found kavuahs
                if ((!entries[2].NoKavuahList.Exists(k =>
                        (k.DayNight == entries[0].DayNight) &&
                        (k.KavuahType.In(KavuahType.HaflagaMaayanPasuach, KavuahType.Haflagah) &&
                        (k.Number == entries[0].Interval))) &&
                        (!prevKavuahs.Exists(kv =>
                            kv.Active &&
                            (kv.DayNight == entries[0].DayNight) &&
                            (kv.KavuahType.In(KavuahType.HaflagaMaayanPasuach, KavuahType.Haflagah)) &&
                            (kv.Number == entries[0].Interval)))))
                {
                    kavuahs.Add(new Kavuah(KavuahType.HaflagaMaayanPasuach, entries[0].DayNight)
                    {
                        IsMaayanPasuach = true,
                        //For Ma'ayan Pasuach, the regular days are usually not cancelled
                        CancelsOnahBeinanis = false,
                        SettingEntry = entries[2],
                        SettingEntryDate = entries[2].DateTime,
                        SettingEntryInterval = entries[2].Interval,
                        Number = entries[0].Interval
                    });
                }
            }
        }

        /// <summary>
        /// Cheshbon out Dilug Haflaga Kavuahs
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="kavuahs"></param>
        private static void FindDilugHaflagahKavuah(Entry[] entries, List<Kavuah> kavuahs)
        {
            //We check the three entries if their interval "Dilug"s are the same.
            //If the "Dilug" is 0 it will be a regular Kavuah of Haflagah but not a Dilug one
            if (((entries[2].Interval - entries[1].Interval) == (entries[1].Interval - entries[0].Interval)) &&
                ((entries[2].Interval - entries[1].Interval) != 0))
            {
                //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                if (!entries[2].NoKavuahList.Exists(k =>
                        (k.KavuahType == KavuahType.DilugHaflaga) &&
                        (k.Number == (entries[2].Interval - entries[1].Interval))))
                {
                    kavuahs.Add(new Kavuah(KavuahType.DilugHaflaga, entries[0].DayNight)
                    {
                        //For this type of Kavuah, the regular days are not cancelled by default
                        CancelsOnahBeinanis = false,
                        Number = (entries[2].Interval - entries[1].Interval),
                        SettingEntry = entries[2],
                        SettingEntryDate = entries[2].DateTime,
                        SettingEntryInterval = entries[2].Interval,
                    });
                }
            }
        }

        /// <summary>
        /// Cheshbon out Kavuah of same day of the week
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="kavuahs"></param>
        private static void FindDayOfWeekKavuah(Entry entry, List<Entry> entries, List<Kavuah> kavuahs)
        {
            //We go through the proceeding entries in the list looking for those that are on the same day of the week as the given entry
            //Note, similar to Yom Hachodesh based kavuahs, it is irrelevant if there were other entries in the interim (משמרת הטהרה)
            foreach (Entry firstFind in entries.Where(e =>
                !e.IsInvisible &&
                e.DateTime > entry.DateTime &&
                e.DayOfWeek == entry.DayOfWeek &&
                e.DayNight == entry.DayNight))
            {
                //We get the interval in days between the found entry and the given entry
                int interval = (firstFind.DateTime - entry.DateTime).Days;

                //We now look for a second entry that is also on the same day of the week 
                //and that has the same interval from the previously found entry
                Entry secondFind = entries.FirstOrDefault(en =>
                    !en.IsInvisible &&
                    en.DateTime.IsSameday(firstFind.DateTime.AddDays(interval)) &&
                    en.DayOfWeek == entry.DayOfWeek &&
                    en.DayNight == entry.DayNight);

                if (secondFind != null)
                {
                    //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                    if (!secondFind.NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.DayOfWeek) &&
                            (k.Number == interval)))
                    {
                        kavuahs.Add(new Kavuah(KavuahType.DayOfWeek, entry.DayNight)
                        {
                            Number = interval,
                            SettingEntry = secondFind,
                            SettingEntryDate = secondFind.DateTime,
                            //The interval is enough for keeping track of the DayOfWeek Kavuah
                            SettingEntryInterval = secondFind.Interval
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Cheshbon out Dilug Yom Hachodesh
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="kavuahs"></param>
        private static void FindDilugDayOfMonthKavuah(Entry entry, List<Entry> entries, List<Kavuah> kavuahs)
        {
            //First, we look for any entry that is in the next Jewish month after the given entry - 
            //but not on the same day as that would be a regular DayOfMonth Kavuah with no Dilug.
            //Note, it is irrelevant if there were other entries in the interim     
            Entry secondFind = entries.FirstOrDefault(en =>
                !en.IsInvisible &&
                en.DayNight == entry.DayNight &&
                entry.Day != en.Day &&
                entry.DateTime.AddMonths(1).Month == en.DateTime.Month &&
                entry.DateTime.AddMonths(1).Year == en.DateTime.Year);
            if (secondFind != null)
            {
                //Now we look for another entry that is in the 3rd month and has the same "Dilug" as the previous find
                Entry finalFind = entries.FirstOrDefault(en =>
                    !en.IsInvisible &&
                    en.DayNight == entry.DayNight &&
                    (en.Day - secondFind.Day) == (secondFind.Day - entry.Day) &&
                    entry.DateTime.AddMonths(2).Month == en.DateTime.Month &&
                    entry.DateTime.AddMonths(2).Year == en.DateTime.Year);

                if (finalFind != null)
                {
                    //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                    if (!finalFind.NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.DilugDayOfMonth) &&
                            (k.Number == (finalFind.Day - secondFind.Day))))
                    {
                        kavuahs.Add(new Kavuah(KavuahType.DilugDayOfMonth, entry.DayNight)
                        {
                            Number = (finalFind.Day - secondFind.Day),
                            SettingEntry = finalFind,
                            SettingEntryDate = finalFind.DateTime,
                            SettingEntryInterval = finalFind.Interval,
                        });
                    }
                }
            }
        }

        /// <summary>
        /// Cheshbon out Kavuah of same day-of-month - including Ma'ayan Pasuach for the third Entry
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="kavuahs"></param>
        private static void FindDayOfMonthKavuah(Entry entry, List<Entry> entries, List<Kavuah> kavuahs)
        {
            MonthObject oneMonthAhead = entry.AddMonths(1).Month;

            //We look for an entry that is exactly one Jewish month later
            //Note, it is irrelevant if there were other entries in the interim
            if (entries.Exists(nextEntry =>
                    !nextEntry.IsInvisible &&
                    nextEntry.DayNight == entry.DayNight &&
                    entry.Day == nextEntry.Day &&
                    nextEntry.Month == oneMonthAhead))
            {
                MonthObject twoMonthsAhead = entry.AddMonths(2).Month;
                //Now we look for another entry that is exactly two Jewish months later - 
                //... or if it is within 5 days before that day: Ma'ayan Pasuach.
                //Note, the number 5 is just a generality, Ma'ayan Pasuach is dependent on the actual length of the period 
                Entry thirdFind = entries.FirstOrDefault(thirdEntry =>
                    !thirdEntry.IsInvisible &&
                    ((entry.Day - thirdEntry.Day) <= 5) &&
                    ((thirdEntry.DayNight == entry.DayNight) || (thirdEntry.Day < entry.Day)) &&
                    thirdEntry.Month == twoMonthsAhead);

                //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                if (thirdFind != null && !thirdFind.NoKavuahList.Exists(k =>
                        (k.KavuahType.In(KavuahType.DayOfMonth, KavuahType.DayOfMonthMaayanPasuach)) &&
                        (k.Number == entry.Day)))
                {
                    kavuahs.Add(new Kavuah(entry.DayNight)
                    {
                        KavuahType = (thirdFind.Day == entry.Day
                            ? KavuahType.DayOfMonth
                            : KavuahType.DayOfMonthMaayanPasuach),
                        CancelsOnahBeinanis = thirdFind.Day == entry.Day,
                        SettingEntry = thirdFind,
                        SettingEntryDate = thirdFind.DateTime,
                        SettingEntryInterval = thirdFind.Interval,
                        Number = entry.Day
                    });
                }
            }
        }
        #endregion

        #region public static functions
        public static bool IsSameKavuah(Kavuah a, Kavuah b)
        {
            return (a.Active == b.Active && IsSimilarKavuah(a, b));
        }
        #endregion
    }
}