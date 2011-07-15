using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chashavshavon.Utils;

namespace Chashavshavon
{
    public enum KavuahType
    {
        Haflagah,
        DayOfMonth,
        HaflagaMaayanPasuach,
        DilugHaflaga,
        DilugDayOfMonth
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
        public DateTime? DilugStartDate { get; set; }
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
                    case Chashavshavon.KavuahType.HaflagaMaayanPasuach:
                        sb.AppendFormat("הפלגה - {0} ימים - ע\"פ מעיין פתוח ", this.Number);
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
                if (this.IsMaayanPasuach && this.KavuahType != KavuahType.HaflagaMaayanPasuach)
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
                            CancelsOnahBeinanis = true,
                            Number = entries[0].Interval
                        });
                    }
                }            

                //Cheshbon out Kavuah of same day-of-month
                if ((entries[0].Day == entries[1].Day) && (entries[1].Day == entries[2].Day))
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
                            CancelsOnahBeinanis = false ,
                            Number = entries[0].Day
                        });                        
                    }
                }

                //Cheshbon out Dilug Haflagas
                if ((entries[2].Interval - entries[1].Interval) == (entries[1].Interval - entries[0].Interval))
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
                            DilugStartDate = entries[2].DateTime
                        });
                    }
                }

                //Cheshbon out Dilug Yom Hachodesh
                if ((entries[2].Day - entries[1].Day) == (entries[1].Day - entries[0].Day) && 
                    (entries[2].Day - entries[1].Day) != 0)
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
                            DilugStartDate = entries[2].DateTime
                        });
                    }
                }

                //Cheshbon out Ma'ayan Pasuachs
                if (!kavuahs.Exists(k => k.KavuahType == KavuahType.HaflagaMaayanPasuach))
                {
                    int maximumHaflagah = entries.Max(e => e.Interval);
                    if (entries.All(e => e.Interval >= (maximumHaflagah - 5)))
                    {
                        //If the "NoKavuah" list for the 3rd entry does not include this "find" 
                        if (!entries[2].NoKavuahList.Exists(k =>
                                (k.KavuahType == KavuahType.HaflagaMaayanPasuach) &&
                                (k.Number == maximumHaflagah)))
                        {
                            kavuahs.Add(new Kavuah()
                            {
                                DayNight = entries[0].DayNight,
                                KavuahType = KavuahType.HaflagaMaayanPasuach,
                                IsMaayanPasuach = true,
                                //For ma'ayan Pasuach, the regular days are usually not cancelled
                                CancelsOnahBeinanis = false, 
                                Number = maximumHaflagah 
                            });
                        }
                    }
                }
            }

            return kavuahs;
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