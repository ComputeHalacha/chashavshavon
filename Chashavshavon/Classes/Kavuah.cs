using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chashavshavon.Utils;
using Microsoft.VisualBasic;

namespace Chashavshavon
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
        public Kavuah()
        {
            //Set default to true
            this.Active = true;
        }

        public static List<Kavuah> KavuahsList { get; set; }

        public DayNight DayNight { get; set; }
        public KavuahType KavuahType { get; set; }
        public int Number { get; set; }
        public bool Active { get; set; }
        public bool IsMaayanPasuach { get; set; }
        public bool CancelsOnahBeinanis { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public Entry SettingEntry { get; set; }
        public DateTime SettingEntryDate { get; set; }
        public int SettingEntryInterval { get; set; }
        public string Notes { get; set; }
        public string KavuahDescriptionHebrew
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                switch (this.KavuahType)
                {
                    case KavuahType.Haflagah:
                        sb.AppendFormat("הפלגה - {0} ימים ", this.Number);
                        break;
                    case KavuahType.DayOfMonth:
                        sb.AppendFormat("יום החדש - {0} בחדש ", Zmanim.DaysOfMonthHebrew[this.Number]);
                        break;
                    case KavuahType.DayOfWeek:
                        sb.AppendFormat("יום השבוע - {0} כל {1} שבועות ",
                            Program.CultureInfo.DateTimeFormat.GetDayName(this.SettingEntryDate.DayOfWeek),
                            this.Number / 7);
                        break;
                    case KavuahType.Sirug:
                        sb.AppendFormat("הפלגה בסירוג - {0} לחודש כל {1} חודשים",
                            this.SettingEntryDate.ToString("dd"),
                            this.Number);
                        break;
                    case Chashavshavon.KavuahType.HaflagaMaayanPasuach:
                        sb.AppendFormat("הפלגה - {0} ימים - ע\"פ מעיין פתוח ", this.Number);
                        break;
                    case Chashavshavon.KavuahType.DayOfMonthMaayanPasuach:
                        sb.AppendFormat("יום החדש - {0} בחדש - ע\"פ מעיין פתוח ", Zmanim.DaysOfMonthHebrew[this.Number]);
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
                //The user can set IsMaayanPasuach for any kavuah in frmKavuahs
                if (this.IsMaayanPasuach &&
                    !this.KavuahType.In(KavuahType.HaflagaMaayanPasuach, KavuahType.DayOfMonthMaayanPasuach))
                {
                    sb.Append(" - ע\"פ מעיין פתוח ");
                }
                sb.AppendFormat("<עונת {0}> {1}", (this.DayNight == DayNight.Day ? "יום" : "לילה"), this.Notes);
                return sb.ToString();
            }
        }        

        /// <summary>
        /// Gets a list of proposed Kavuahs from the Entry list and prompts the user either them add them or "NoKavuah" them 
        /// </summary>
        /// <returns></returns>
        public static bool FindAndPromptKavuahs()
        {
            var lastThree = new Queue<Entry>();
            Entry[] last3Array;
            List<Kavuah> foundKavuahList = new List<Kavuah>();

            foreach (Entry entry in Entry.EntryList)
            {
                //For cheshboning out Kavuas we need at most just the last 3 entries
                //First, add this entry.
                lastThree.Enqueue(entry);
                if (lastThree.Count > 3)
                {
                    //pop out the earliest one - leaves us with this entry and the previous 2.
                    lastThree.Dequeue();
                }
                last3Array = lastThree.ToArray<Entry>();

                //Gets a list of Kavuahs from the given 3 entries
                foundKavuahList.AddRange(GetProposedKavuahList(last3Array));                
            }
            
            //Get distinct proposed Kavuah list
            for (int i = 0; i < foundKavuahList.Count; i++)
            {
                if (foundKavuahList.Exists(ka => IsSimilarKavuah(ka, foundKavuahList[i])))
                {
                    foundKavuahList.RemoveAt(i);
                }
            }

            //Remove all found kavuahs that are already in the active list
            foundKavuahList.RemoveAll(k => InActiveKavuahList(k));

            //If there are any left
            if (foundKavuahList.Count > 0)
            {
                //Prompt user to decide which ones to keep and their details
                frmKavuahPrompt fkp = new frmKavuahPrompt(foundKavuahList);
                if (fkp.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //For each found kavuah, either we add it to the main list 
                    //or we set it as a "NoKavuah" for the third entry so it shouldn't pop up again
                    foreach (Kavuah k in foundKavuahList)
                    {
                        //The ListToAdd property contains the ones the user decided to add
                        if (fkp.ListToAdd.Contains(k))
                        {
                            Kavuah.KavuahsList.Add(k);
                        }
                        else
                        {
                            //The SettingEtry is set when the list kavuah was added to the list
                            k.SettingEntry.NoKavuahList.Add(k);
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }                

        #region Private Functions
        /// <summary>
        /// Takes an array of 3 entries and returns a list of Kavuahs that can be determined from them
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        private static List<Kavuah> GetProposedKavuahList(Entry[] entries)
        {
            List<Kavuah> kavuahs = new List<Kavuah>();

            //First get those Kavuahs that are not dependent on the Entries being 3 in a row
            FindDayOfMonthKavuah(entries[0], kavuahs);
            FindDilugDayOfMonthKavuah(entries[0], kavuahs);
            FindDayOfWeekKavuah(entries[0], kavuahs);

            //Now the Kavuahs that need 3 in-a-row
            if (entries.Count() == 3)
            {
                //The Kavuahs that need all to be the same DayNight
                if (entries[0].DayNight.In(entries[1].DayNight, entries[2].DayNight))
                {
                    FindHaflagahKavuah(entries, kavuahs);
                    FindSirugKavuah(entries, kavuahs);
                    FindDilugHaflagahKavuah(entries, kavuahs);
                }
                //Now the Kavuah that needs 3 in-a-row and doesn't need to be the same DayNight
                FindMaayanPasuachHaflagahKavuah(entries, kavuahs);            
            }
            return kavuahs;
        }

        private static bool IsSimilarKavuah(Kavuah a, Kavuah b)
        {
            return (a != b &&
                a.KavuahType == b.KavuahType &&
                a.DayNight == b.DayNight &&
                a.Number == b.Number);
        }

        private static bool InActiveKavuahList(Kavuah kavuah)
        {
            return KavuahsList.Exists(k => IsSimilarKavuah(k, kavuah) && k.Active);
        }

        /// <summary>
        /// Cheshbon out Ma'ayan Pasuachs of haflaga - this does not need that the last one is the same DayNight
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="kavuahs"></param>
        private static void FindMaayanPasuachHaflagahKavuah(Entry[] entries, List<Kavuah> kavuahs)
        {            
            if (!kavuahs.Exists(k => k.KavuahType == KavuahType.Haflagah))
            {
                if ((entries[0].Interval == entries[1].Interval) &&
                    (entries[0].DayNight == entries[1].DayNight) &&
                    (entries[2].Interval < entries[1].Interval) &&
                    (entries[2].Interval > (entries[1].Interval - 5)))
                {
                    //"NoKavuah" list for the 3rd entry does not include this "find" 
                    if (!entries[2].NoKavuahList.Exists(k =>
                            (k.DayNight == entries[0].DayNight) &&
                            (k.KavuahType == KavuahType.HaflagaMaayanPasuach) &&
                            (k.Number == entries[0].Interval)))
                    {
                        kavuahs.Add(new Kavuah()
                        {
                            DayNight = entries[0].DayNight,
                            KavuahType = KavuahType.HaflagaMaayanPasuach,
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
        }

        /// <summary>
        /// Cheshbon out Dilug Haflagas
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="kavuahs"></param>
        private static void FindDilugHaflagahKavuah(Entry[] entries, List<Kavuah> kavuahs)
        {           
            if ((entries[2].Interval - entries[1].Interval) == (entries[1].Interval - entries[0].Interval) &&
                ((entries[2].Interval - entries[1].Interval) != 0))
            {
                //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                if (!entries[2].NoKavuahList.Exists(k =>
                        (k.KavuahType == KavuahType.DilugHaflaga) &&
                        (k.Number == (entries[2].Interval - entries[1].Interval))))
                {
                    kavuahs.Add(new Kavuah()
                    {
                        DayNight = entries[0].DayNight,
                        KavuahType = KavuahType.DilugHaflaga,
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
        /// Cheshbon out Kavuah of Sirug - we go here that Sirug Kavuahs need 3 in a row with no intervening entries
        /// </summary>
        /// <param name="entries"></param>
        /// <param name="kavuahs"></param>
        private static void FindSirugKavuah(Entry[] entries, List<Kavuah> kavuahs)
        {            
            int monthDiff = Convert.ToInt32(DateAndTime.DateDiff(DateInterval.Month, entries[0].DateTime, entries[1].DateTime));
            if (monthDiff > 1 &&
                (!kavuahs.Exists(k => k.KavuahType == KavuahType.DayOfMonth)) &&
                (entries[0].Day == entries[1].Day) &&
                (entries[1].Day == entries[2].Day) &&
                (DateAndTime.DateDiff(DateInterval.Month, entries[1].DateTime, entries[2].DateTime) == monthDiff))
            {

                //If the "NoKavuah" list for the 3rd entry does not include this "find"
                if (!entries[2].NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.Sirug) &&
                            (k.Number == monthDiff)))
                {
                    kavuahs.Add(new Kavuah()
                    {
                        DayNight = entries[0].DayNight,
                        KavuahType = KavuahType.Sirug,
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
            if ((entries[0].Interval == entries[1].Interval) &&
                (entries[1].Interval == entries[2].Interval))
            {
                //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                if (!entries[2].NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.Haflagah) &&
                            (k.Number == entries[0].Interval)))
                {
                    kavuahs.Add(new Kavuah()
                    {
                        DayNight = entries[0].DayNight,
                        KavuahType = KavuahType.Haflagah,
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
        /// Cheshbon out Kavuah of same day of the week - these kavuahs are even if there are entries in middle
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="kavuahs"></param>
        private static void FindDayOfWeekKavuah(Entry entry, List<Kavuah> kavuahs)
        {            
            foreach (Entry firstFind in Entry.EntryList.Where(e => e.DateTime > entry.DateTime &&
                e.DayOfWeek == entry.DayOfWeek &&
                e.DayNight == entry.DayNight))
            {
                int interval = (firstFind.DateTime - entry.DateTime).Days;

                Entry secondFind = Entry.EntryList.FirstOrDefault(en => en.DateTime == firstFind.DateTime.AddDays(interval) &&
                    en.DayOfWeek == entry.DayOfWeek &&
                    en.DayNight == entry.DayNight);

                if (secondFind != null)
                {
                    //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                    if (!secondFind.NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.DayOfWeek) &&
                            (k.Number == interval)))
                    {
                        kavuahs.Add(new Kavuah()
                        {
                            DayNight = entry.DayNight,
                            KavuahType = KavuahType.DayOfWeek,
                            Number = interval,
                            SettingEntry = secondFind,
                            SettingEntryDate = secondFind.DateTime,
                            SettingEntryInterval = secondFind.Interval //The interval is enough for the day of week
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
        private static void FindDilugDayOfMonthKavuah(Entry entry, List<Kavuah> kavuahs)
        {           
            Entry secondFind = Entry.EntryList.FirstOrDefault(en =>
                en.DayNight == entry.DayNight &&
                entry.Day != en.Day &&
                entry.DateTime.AddMonths(1).Month == en.DateTime.Month);
            if (secondFind != null)
            {
                Entry finalFind = Entry.EntryList.FirstOrDefault(en =>
                    en.DayNight == entry.DayNight &&
                    (en.Day - secondFind.Day) == (secondFind.Day - entry.Day) &&
                    entry.DateTime.AddMonths(2).Month == en.DateTime.Month);

                if (finalFind != null)
                {
                    //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                    if (!finalFind.NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.DilugDayOfMonth) &&
                            (k.Number == (finalFind.Day - secondFind.Day))))
                    {
                        kavuahs.Add(new Kavuah()
                        {
                            DayNight = entry.DayNight,
                            KavuahType = KavuahType.DilugDayOfMonth,
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
        /// Cheshbon out Kavuah of same day-of-month - inlcuding Ma'ayan Pasuach for the third Entry
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="kavuahs"></param>
        private static void FindDayOfMonthKavuah(Entry entry, List<Kavuah> kavuahs)
        {            
            if (Entry.EntryList.Exists(en =>
                    en.DayNight == entry.DayNight &&
                    entry.Day == en.Day &&
                    entry.DateTime.AddMonths(1).Month == en.DateTime.Month))
            {
                Entry thirdFind = Entry.EntryList.FirstOrDefault(en =>
                    en.Day <= entry.Day &&
                    en.Day >= (entry.Day - 5) &&
                    ((en.DayNight == entry.DayNight) || (en.Day < entry.Day)) &&
                    entry.DateTime.AddMonths(2).Month == en.DateTime.Month);

                //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                if (thirdFind != null && !thirdFind.NoKavuahList.Exists(k =>
                        (k.KavuahType.In(KavuahType.DayOfMonth, KavuahType.DayOfMonthMaayanPasuach)) &&
                        (k.Number == entry.Day)))
                {
                    kavuahs.Add(new Kavuah()
                    {
                        DayNight = entry.DayNight,
                        KavuahType = (thirdFind.Day < entry.Day ? KavuahType.DayOfMonthMaayanPasuach : KavuahType.DayOfMonth),
                        CancelsOnahBeinanis = true,
                        SettingEntry = thirdFind,
                        SettingEntryDate = thirdFind.DateTime,
                        SettingEntryInterval = thirdFind.Interval,
                        Number = entry.Day
                    });
                }
            }
        }
        #endregion
    }
}