namespace JewishCalendar
{
    /// <summary>
    /// Does the calculations for ascertaining the Daf Yomi for any Jewish date since Daf Yomi was initiated.
    /// Sample of use to get todays daf:
    /// <code>
    /// JewishCalendar.JewishDate jd = new JewishDate(DateTime.Now); 
    /// JewishCalendar.Daf dafOfDay = JewishCalendar.DafYomi.GetDafYomi(jd);
    /// string strDafOfDay = dafOfDay.ToStringHeb();
    /// </code>
    /// The algorithms was converted to C# (and tweaked) 
    /// from the C code in Danny Sadinoff's HebCal - Copyright (C) 1994.
    /// The HebCal code for dafyomi was adapted by Aaron Peromsik from Bob Newell's public domain daf.el.
    /// </summary>
    public static class DafYomi
    {
        /// <summary>
        /// Computes the Day Yomi for the given day.
        /// </summary>
        /// <param name="jewishDate">The Jewish date for which to get the Daf Yomi information</param>
        /// <returns>A Daf object containing the DafYomi information for the given day</returns>
        public static Daf GetDafYomi(JewishDate jewishDate)
        {
            return GetSingleDaf(jewishDate.AbsoluteDate);
        }

        private static readonly Masechta[] masechtaList = new Masechta[]
        {
            new Masechta ("Berachos", "ברכות", 64),
            new Masechta ("Shabbos", "שבת", 157),
            new Masechta ("Eruvin", "ערובין", 105),
            new Masechta ("Pesachim", "פסחים", 121),
            new Masechta ("Shekalim", "שקלים", 22),
            new Masechta ("Yoma", "יומא", 88),
            new Masechta ("Sukkah", "סוכה", 56),
            new Masechta ("Beitzah", "ביצה", 40),
            new Masechta ("Rosh Hashana", "ראש השנה", 35),
            new Masechta ("Taanis", "תענית", 31),
            new Masechta ("Megillah", "מגילה", 32),
            new Masechta ("Moed Katan", "מועד קטן", 29),
            new Masechta ("Chagigah", "חגיגה", 27),
            new Masechta ("Yevamos", "יבמות", 122),
            new Masechta ("Kesubos", "כתובות", 112),
            new Masechta ("Nedarim", "נדרים", 91),
            new Masechta ("Nazir", "נזיר", 66),
            new Masechta ("Sotah", "סוטה", 49),
            new Masechta ("Gitin", "גיטין", 90),
            new Masechta ("Kiddushin", "קדושין", 82),
            new Masechta ("Baba Kamma","בבא קמא",119),
            new Masechta ("Baba Metzia","בבא מציעא",119),
            new Masechta ("Baba Batra","בבא בתרא",176),
            new Masechta ("Sanhedrin","סנהדרין",113),
            new Masechta ("Makkot","מכות",24),
            new Masechta ("Shevuot","שבועות",49),
            new Masechta ("Avodah Zarah","עבודה זרה",76),
            new Masechta ("Horayot","הוריות",14),
            new Masechta ("Zevachim","זבחים",120),
            new Masechta ("Menachos", "מנחות", 110),
            new Masechta ("Chullin", "חולין", 142),
            new Masechta ("Bechoros", "בכורות", 61),
            new Masechta ("Arachin", "ערכין", 34),
            new Masechta ("Temurah", "תמורה", 34),
            new Masechta ("Kerisos", "כריתות", 28),
            new Masechta ("Meilah", "מעילה", 22),
            new Masechta ("Kinnim", "קנים", 4),
            new Masechta ("Tamid", "תמיד", 10),
            new Masechta ("Midos", "מדות", 4),
            new Masechta ("Niddah", "נדה",73)
        };

        /// <summary>
        /// Gets the DafYomi for the given day
        /// </summary>
        /// <param name="absoluteDate"></param>
        /// <returns></returns>
        private static Daf GetSingleDaf(int absoluteDate)
        {
            int dafcnt = 40;
            int cno, dno, osday, nsday, total, count, j, blatt;

            osday = JewishDateCalculations.GetAbsoluteFromGregorianDate(1923, 9, 11);
            nsday = JewishDateCalculations.GetAbsoluteFromGregorianDate(1975, 6, 24);

            /*  No cycle, new cycle, old cycle */
            if (absoluteDate < osday)
            {
                return null; /* daf yomi hadn't started yet */
            }

            if (absoluteDate >= nsday)
            {
                cno = 8 + ((absoluteDate - nsday) / 2711);
                dno = (absoluteDate - nsday) % 2711;
            }
            else
            {
                cno = 1 + ((absoluteDate - osday) / 2702);
                dno = (absoluteDate - osday) / 2702;
            }

            /* Find the daf taking note that the cycle changed slightly after cycle 7. */
            total = blatt = 0;
            count = -1;

            /* Fix Shekalim for old cycles */
            if (cno <= 7)
            {
                masechtaList[4].Dappim = 13;
            }
            else
            {
                masechtaList[4].Dappim = 22;
            }

            /* Find the daf */
            j = 0;
            while (j < dafcnt)
            {
                count++;
                total = total + masechtaList[j].Dappim - 1;
                if (dno < total)
                {
                    blatt = (masechtaList[j].Dappim + 1) - (total - dno);
                    /* fiddle with the weird ones near the end */
                    switch (count)
                    {
                        case 36:
                            blatt += 21;
                            break;

                        case 37:
                            blatt += 24;
                            break;

                        case 38:
                            blatt += 33;
                            break;

                        default:
                            break;
                    }
                    /* Bailout */
                    j = 1 + dafcnt;
                }
                j++;
            }

            return new Daf(masechtaList[count], blatt);
        }
    }

    /// <summary>
    /// Represents a single Masechta in Shas.
    /// This structure is not meant to be instantiated directly.
    /// To access the DafYomi, use <see cref="DafYomi.GetDafYomi(JewishDate)"/>
    /// </summary>
    public struct Masechta
    {
        /// <summary>
        /// The name of the masechta in English
        /// </summary>
        public string NameEnglish;

        /// <summary>
        /// The name of the masechta in Hebrew
        /// </summary>
        public string NameHebrew;

        /// <summary>
        /// The number of dappim in the current masechta
        /// </summary>
        public int Dappim;

        /// <summary>
        /// Create a new Masechta
        /// </summary>
        /// <param name="eng"></param>
        /// <param name="heb"></param>
        /// <param name="dappim"></param>
        internal Masechta(string eng, string heb, int dappim) { this.NameEnglish = eng; this.NameHebrew = heb; this.Dappim = dappim; }
    }

    /// <summary>
    /// Represents a single Daf in Shas.
    /// This class is not meant to be instantiated directly.
    /// To access the DafYomi, use <see cref="DafYomi.GetDafYomi(JewishDate)"/>
    /// </summary>
    public class Daf
    {
        /// <summary>
        /// The masechta this daf is in
        /// </summary>
        public Masechta Masechta { get; private set; }

        /// <summary>
        /// The number of this daf
        /// </summary>
        public int DafNumber { get; private set; }

        /// <summary>
        /// Create a new Daf object
        /// </summary>
        /// <param name="m"></param>
        /// <param name="d"></param>
        internal Daf(Masechta m, int d) { this.Masechta = m; this.DafNumber = d; }

        /// <summary>
        /// Returns the name of the Masechta and daf number in English, For example: Sukkah, Daf 3
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Masechta.NameEnglish + ", Daf " + this.DafNumber.ToString();
        }

        /// <summary>
        /// Returns the name of the Masechta and daf number in Hebrew. For example: 'סוכה דף כ.
        /// </summary>
        /// <returns></returns>
        public string ToStringHeb()
        {
            return this.Masechta.NameHebrew + " דף " + this.DafNumber.ToNumberHeb();
        }
    }
}