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

        public DayNight DayNight { get; set; }
        public KavuahType KavuahType { get; set; }
        public int Number { get; set; }
        public bool Active { get; set; }
        public bool IsMaayanPauach { get; set; }
        public bool CancelsOnahBeinanis { get; set; }
        public string Notes { get; set; }
        public string KavuahDescriptionHebrew
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                switch (KavuahType)
                {
                    case KavuahType.Haflagah:
                        sb.AppendFormat(" הפלגה, כל {0} ימים ", this.Number);
                        break;
                    case KavuahType.DayOfMonth:
                        sb.AppendFormat(" יום החדש, כל {0} בחדש ", Zmanim.DaysOfMonthHebrew[this.Number]);
                        break;
                    case KavuahType.DilugHaflaga:
                        sb.AppendFormat(" הפלגה של דילוג {1}{0} ימים ", 
                            (this.Number < 0 ? "-" : "+"), 
                            this.Number);
                        break;
                    case KavuahType.DilugDayOfMonth:
                        sb.AppendFormat(" יום החודש של דילוג {1}{0} ימים ", 
                            (this.Number < 0 ? "-" : "+"), 
                            this.Number);
                        break;
                }
                if (this.IsMaayanPauach)
                {
                    sb.Append(" - ע\"פ מעיין פתוח - ");
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
        public static List<Kavuah> GetKavuahListFromEntries(Entry[] entries)        
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
                            Number = entries[0].Interval
                        });
                    }
                }            

                //Cheshbon out Kavuah of same day
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
                            Number = (entries[2].Interval - entries[1].Interval)
                        });
                    }
                }

                //Cheshbon out Dilug Yom Hachodesh
                if ((entries[2].Day - entries[1].Day) == (entries[1].Day - entries[0].Day))
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
                            Number = (entries[2].Day - entries[1].Day)
                        });
                    }
                }

                //Cheshbon out Maayan Pasuachs                    
                if (!kavuahs.Exists(k => k.KavuahType == KavuahType.Haflagah))
                {
                    int maximumHaflagah = entries.Max(e => e.Interval);
                    if (entries.All(e => e.Interval >= (maximumHaflagah - 5)))
                    {
                        //If the "NoKavuah" list for the 3rd entry does not include this "find", 
                        if (!entries[2].NoKavuahList.Exists(k =>
                                (k.KavuahType == KavuahType.Haflagah) &&
                                (k.Number == (entries[2].Day - entries[1].Day))))
                        {
                            kavuahs.Add(new Kavuah()
                            {
                                DayNight = entries[0].DayNight,
                                KavuahType = KavuahType.Haflagah,
                                IsMaayanPauach = true,
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

        public static List<Kavuah> KavuahsList { get; set; }
        
    }
}