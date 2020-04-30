namespace JewishCalendar
{
    /// <summary>
    /// Represents the molad for a single month
    /// </summary>
    public class Molad
    {
        /// <summary>
        /// The date of the molad
        /// </summary>
        public JewishDate JewishDate { get; set; }

        /// <summary>
        /// Represents the time of the molad - not including the chalakim
        /// </summary>
        public TimeOfDay Time { get; set; }

        /// <summary>
        /// Represents the Chalakim (1/1080 of an hour) part of the molad
        /// </summary>
        public int Chalakim { get; set; }

        /// <summary>
        /// Returns the time of the molad as a string in the format: Monday Night, 8:33 PM and 12 Chalakim
        /// NOTE: the cutoff time to be considered "night" is 8 PM.
        /// To specify another nightfall time (such as the real sunset time), use the function: ToString(TimeOfDay nightfall)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString(new TimeOfDay { Hour = 20, Minute = 0 });
        }

        /// <summary>
        /// Returns the time of the molad as a string in the format: Monday Night, 8:33 PM and 12 Chalakim
        /// </summary>
        /// <param name="nightfall">Used to determine when to display "Night" or "Motzai Shabbos" etc.</param>
        /// <returns></returns>
        public string ToString(TimeOfDay nightfall)
        {
            var sb = new System.Text.StringBuilder();

            if (nightfall == TimeOfDay.NoValue)
            {
                sb.Append(Utils.DaysOfWeek[this.JewishDate.DayInWeek]);
            }
            else if (this.JewishDate.DayOfWeek == System.DayOfWeek.Saturday && this.Time >= nightfall)
            {
                sb.Append("Motzai Shabbos,");
            }
            else if (this.JewishDate.DayOfWeek == System.DayOfWeek.Friday && this.Time >= nightfall)
            {
                sb.Append("Shabbos Night,");
            }
            else
            {
                sb.AppendFormat("{0},{1}",
                    Utils.DaysOfWeek[this.JewishDate.DayInWeek],
                    this.Time >= nightfall ? " Night" : "");
            }
            sb.AppendFormat(" {0} and {1} Chalakim",
                this.Time,
                this.Chalakim);

            return sb.ToString();
        }

        /// <summary>
        /// Returns the time of the molad as a string in the format: ליל שני 20:33 12 חלקים
        /// </summary>
        /// <param name="nightfall">Used to determine when to display "ליל/יום" or "מוצאי שב"ק" etc.</param>
        /// <returns></returns>
        public string ToStringHeb(TimeOfDay nightfall)
        {
            var sb = new System.Text.StringBuilder();
            if (this.JewishDate.DayOfWeek == System.DayOfWeek.Saturday)
            {
                sb.Append(this.Time >= nightfall ? "מוצאי שב\"ק" : "יום שב\"ק");
            }
            else if (this.JewishDate.DayOfWeek == System.DayOfWeek.Friday)
            {
                sb.Append(this.Time >= nightfall ? "ליל שב\"ק" : "ערב שב\"ק");
            }
            else
            {
                sb.AppendFormat("{0}{1}",
                    this.Time >= nightfall ? "ליל" : "יום",
                    Utils.JewishDOWNames[this.JewishDate.DayInWeek].Replace("יום", ""));
            }
            sb.AppendFormat(" {0} {1} חלקים",
                this.Time.ToString24H(),
                this.Chalakim);

            return sb.ToString();
        }

        /// <summary>
        /// Returns the molad for the given year and month
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public static Molad GetMolad(int month, int year)
        {
            int totalMonths, partsElapsed, hoursElapsed, parts, monthAdj;

            monthAdj = month - 7;
            if (monthAdj < 0)
            {
                monthAdj += JewishDateCalculations.MonthsInJewishYear(year);
            }

            totalMonths = monthAdj + 235 * ((year - 1) / 19) + 12 * ((year - 1) % 19) +
                ((((year - 1) % 19) * 7) + 1) / 19;
            partsElapsed = 204 + (793 * (totalMonths % 1080));
            hoursElapsed = 5 + (12 * totalMonths) + 793 * (totalMonths / 1080) + partsElapsed / 1080 - 6;
            parts = (partsElapsed % 1080) + 1080 * (hoursElapsed % 24);

            return new Molad
            {
                JewishDate = new JewishDate((1 + (29 * totalMonths)) + (hoursElapsed / 24)),
                Time = new TimeOfDay { Hour = hoursElapsed % 24, Minute = (parts % 1080) / 18 },
                Chalakim = parts % 18
            };
        }
    }
}