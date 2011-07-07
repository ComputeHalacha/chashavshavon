using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chashavshavon
{
    [Serializable()]
    public class Kavuah
    {
        public DayNight DayNight { get; set; }
        public ProblemOnahType ProblemOnahType { get; set; }
        public int Number { get; set; }
        public string Notes { get; set; }
        public string KavuahDescriptionHebrew
        {
            get
            {
                return (ProblemOnahType == Chashavshavon.ProblemOnahType.Haflagah ? " הפלגה, כל " + this.Number.ToString() + " ימים " : " יום החדש, כל " + Onah.DaysOfMonthHebrew[this.Number] + " בחדש ") +
                    " - עונת " + (this.DayNight == DayNight.Day ? " יום " : " לילה ") +
                    "  " + this.Notes;
            }
        }
        public static List<Kavuah> KavuahsList { get; set; }
    }
}
