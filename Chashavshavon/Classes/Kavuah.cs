﻿using System;
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
                        sb.AppendFormat("הפלגה בדילוג {1}{0} ימים ",
                            (this.Number < 0 ? "-" : "+"),
                            this.Number);
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
                sb.AppendFormat(" - עונת {0}  {1}", (this.DayNight == DayNight.Day ? " יום " : " לילה "), this.Notes);
                return sb.ToString();
            }
        }

        /// <summary>
        /// Takes an array of 3 entries and returns a list of Kavuahs that can be determined from them
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static List<Kavuah> GetProposedKavuahList(Entry[] entries)
        {
            if (entries.Length != 3)
            {
                throw new Exception("GetKavuah expects an array of exactly 3 entries");
            }
            List<Kavuah> kavuahs = new List<Kavuah>();


            if (entries[0].DayNight.In(entries[1].DayNight, entries[2].DayNight))
            {
                //Cheshbon out Kavuah of same haflagah
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
                            CancelsOnahBeinanis = true,
                            Number = entries[0].Interval
                        });
                    }
                }

                //Cheshbon out Kavuah of same day of week
                if ((entries[0].DayOfWeek == entries[1].DayOfWeek) &&
                    (entries[1].DayOfWeek == entries[2].DayOfWeek) &&
                    (entries[1].Interval == entries[2].Interval))
                {
                    //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                    if (!entries[2].NoKavuahList.Exists(k =>
                                (k.KavuahType == KavuahType.DayOfWeek) &&
                                (k.Number == entries[0].Interval)))
                    {
                        kavuahs.Add(new Kavuah()
                        {
                            DayNight = entries[0].DayNight,
                            KavuahType = KavuahType.DayOfWeek,
                            SettingEntry = entries[2],
                            SettingEntryDate = entries[2].DateTime,
                            CancelsOnahBeinanis = true,
                            Number = entries[2].Interval //The interval is enough for the day of week
                        });
                    }
                }
                
                //Cheshbon out Kavuah of Siurug
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
                            CancelsOnahBeinanis = true,
                            Number = monthDiff
                        });
                    }
                }

                //Cheshbon out Kavuah of same day-of-month
                if ((entries[0].Day == entries[1].Day &&
                    entries[1].Day == entries[2].Day) &&
                    (entries[0].DateTime.AddMonths(1).Month == entries[1].DateTime.Month) &&
                    (entries[1].DateTime.AddMonths(1).Month == entries[2].DateTime.Month))
                {
                    //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                    if (!entries[2].NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.DayOfMonth) &&
                            (k.Number == entries[0].Day)))
                    {
                        kavuahs.Add(new Kavuah()
                        {
                            DayNight = entries[0].DayNight,
                            KavuahType = KavuahType.DayOfMonth,
                            //For this type of Kavuah, you need 4 Entries to cancel the regular days.
                            CancelsOnahBeinanis = false,
                            SettingEntry = entries[2],
                            SettingEntryDate = entries[2].DateTime,
                            Number = entries[0].Day
                        });
                    }
                }

                //Cheshbon out Dilug Haflagas
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
                            SettingEntryDate = entries[2].DateTime
                        });
                    }
                }

                //Cheshbon out Dilug Yom Hachodesh
                if (((entries[2].Day - entries[1].Day) == (entries[1].Day - entries[0].Day) &&
                    (entries[2].Day - entries[1].Day) != 0) &&
                    (entries[0].DateTime.AddMonths(1).Month == entries[1].DateTime.Month) &&
                    (entries[1].DateTime.AddMonths(1).Month == entries[2].DateTime.Month))
                {
                    //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                    if (!entries[2].NoKavuahList.Exists(k =>
                            (k.KavuahType == KavuahType.DilugDayOfMonth) &&
                            (k.Number == (entries[2].Day - entries[1].Day))))
                    {
                        kavuahs.Add(new Kavuah()
                        {
                            DayNight = entries[0].DayNight,
                            KavuahType = KavuahType.DilugDayOfMonth,
                            //For this type of Kavuah, the regular days are not cancelled by default
                            Number = (entries[2].Day - entries[1].Day),
                            SettingEntry = entries[2],
                            SettingEntryDate = entries[2].DateTime
                        });
                    }
                }               
            }


            //Cheshbon out Ma'ayan Pasuachs of Yom Hachodesh - this does not need that the last one is the same DayNight
            if (!kavuahs.Exists(k => k.KavuahType == KavuahType.DayOfMonth))
            {
                if ((entries[0].Day == entries[1].Day) &&
                    (entries[0].DayNight == entries[1].DayNight) &&
                    (entries[1].DateTime.AddMonths(1) > entries[2].DateTime) && 
                    (entries[1].DateTime.AddMonths(1).AddDays(-5) < entries[2].DateTime) &&                    
                    ((entries[1].DateTime - entries[0].DateTime).Days < 31))
                {
                    //"NoKavuah" list for the 3rd entry does not include this "find" 
                    if (!entries[2].NoKavuahList.Exists(k =>
                            (k.KavuahType.In(KavuahType.DayOfMonth, KavuahType.DayOfMonthMaayanPasuach)) &&
                            (k.DayNight == entries[0].DayNight) &&
                            (k.Number == entries[0].Day)))
                    {
                        kavuahs.Add(new Kavuah()
                        {
                            DayNight = entries[0].DayNight,
                            KavuahType = KavuahType.DayOfMonthMaayanPasuach,
                            IsMaayanPasuach = true,
                            //For ma'ayan Pasuach, the regular days are usually not cancelled
                            CancelsOnahBeinanis = false,
                            SettingEntry = entries[2],
                            SettingEntryDate = entries[2].DateTime,
                            Number = entries[0].Day
                        });
                    }
                }
            }

            //Cheshbon out Ma'ayan Pasuachs of haflaga - this does not need that the last one is the same DayNight
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
                            Number = entries[0].Interval
                        });
                    }
                }
            }

            return kavuahs;
        }

        public static bool FindAndPromptKavuahs()
        {
            var lastThree = new Queue<Entry>();
            Entry[] last3Array;
            List<Kavuah> foundKavuahList = new List<Kavuah>();

            foreach (Entry entry in Entry.EntryList)
            {
                //For cheshboning out kavuas we need just the last 3 entries
                //First, add this entry
                lastThree.Enqueue(entry);
                if (lastThree.Count > 3)
                {
                    //pop out the earliest one - leaves us with this entry and the previous 2.
                    lastThree.Dequeue();
                }
                last3Array = lastThree.ToArray<Entry>();

                //You can't make a kavuah until you have 3 entries in the list to compare 
                //and they are all of the same DayNight
                if (lastThree.Count == 3 &&
                    last3Array[0].DayNight.In(last3Array[1].DayNight, last3Array[2].DayNight))
                {
                    //Gets a list of Kavuahs from the given 3 entries
                    foundKavuahList.AddRange(GetProposedKavuahList(last3Array));
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

        public bool IsSameKavuah(Kavuah b)
        {
            return (this.KavuahType == b.KavuahType &&
                this.DayNight == b.DayNight &&
                this.Number == b.Number);
        }

        public static bool InActiveKavuahList(Kavuah kavuah)
        {
            return KavuahsList.Exists(k => k.IsSameKavuah(kavuah) && k.Active);
        }
    }
}