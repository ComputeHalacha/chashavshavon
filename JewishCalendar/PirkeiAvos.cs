namespace JewishCalendar
{
    /// <summary>
    /// Gets the Pirkei Avos Perek/Prakim for any given Shabbos in the summer months.
    /// The rules are: the Parshiyaos, Nasso, Pinchas and Shoftim start a new cycle and the last two weeks in Ellul get double prakim.
    /// The calculations of this class work OK for Eretz Yisroel, but Chu"l seems to be incorrect.
    /// The issue is that in Chu"l, Achron Shel Pesach and the second day of Shavuos can both fall out on Shabbos.
    /// Sample code to get the Perek for the given Shabbos:
    /// <code>
    /// JewishCalendar.JewishDate jd = new JewishDate(5776, 2, 13);
    /// int[] Prakim = PirkeiAvos.GetPirkeiAvos(jd, true);
    /// string text = String.Join(" and ", prakim.Select(p => p.ToSuffixedString()) + 
    ///     (prakim.Length > 1 ? " Prakim" : " Perek");
    /// </code>
    /// </summary>
    public static class PirkeiAvos
    {
        private static JewishDate _savedPesachDay1;
        private static bool _savedInIsrael;

        /// <summary>
        /// Returns an array of Perek number/s for the given Jewish Date and location.
        /// If the given day does not have Pirkei Avos, an empty array is returned.
        /// </summary>
        /// <param name="jDate"></param>
        /// <param name="inIsrael"></param>
        /// <returns>An array of integers representing the Perek/Prakim of the given Shabbos.
        /// If the given day does not have Pirkei Avos, an empty array is returned.
        /// </returns>
        public static int[] GetPirkeiAvos(JewishDate jDate, bool inIsrael)
        {
            if (jDate.DayOfWeek != System.DayOfWeek.Saturday)
            {
                return new int[] { };
            }

            int jYear = jDate.Year,
                jMonth = jDate.Month,
                jDay = jDate.Day;
            //Pirkei Avos is from after Pesach until Rosh Hashana
            if ((jMonth == 1 && jDay > (inIsrael ? 21 : 22)) ||
                //All Shabbosim through Iyar, Sivan, Tamuz, Av - besides for the day/s of Shavuos and Tisha B'Av
                ((jMonth > 1 && jMonth < 6 &&
                    (!((jMonth == 3 && jDay == 6) || (!inIsrael && jMonth == 3 && jDay == 7))) &&
                    (!(jMonth == 5 && jDay == 9)))))
            {
                return new int[] { GetSinglePerek(jDate, inIsrael) };
            }
            //Ellul can have multiple prakim
            else if (jMonth == 6)
            {
                return GetEllulPrakim(jDate, inIsrael);
            }
            //No Pirkei Avos
            else
            {
                return new int[] { };
            }
        }

        /// <summary>
        /// Gets the single Perek for a typical Summer Shabbos day.
        /// </summary>
        /// <param name="jDate"></param>
        /// <param name="inIsrael"></param>
        /// <returns></returns>
        private static int GetSinglePerek(JewishDate jDate, bool inIsrael)
        {
            int jYear = jDate.Year,
               jMonth = jDate.Month,
               jDay = jDate.Day;

            //Save the first day of Pesach. Most subsequent calls will be for the same year and location.
            if (_savedPesachDay1 == null || jYear != _savedPesachDay1.Year || _savedInIsrael != inIsrael)
            {
                _savedPesachDay1 = new JewishDate(jYear, 1, 15);
                _savedInIsrael = inIsrael;
            }

            //How many days after the first day of pesach was the first shabbos after pesach
            int firstShabbosInterval = (inIsrael ? 7 : 8) + (6 - _savedPesachDay1.DayInWeek);
            //What number shabbos after pesach is the current date
            int currentShabbosNumber = (jMonth == 1 && jDay == (firstShabbosInterval + 15) ? 1 :
                ((jDate.AbsoluteDate - (_savedPesachDay1.AbsoluteDate + firstShabbosInterval)) / 7) + 1);
            int perekAvos = currentShabbosNumber % 6;
            if (perekAvos == 0)
            {
                perekAvos = 6;
            }
            //If the second day of Shavuos was on Shabbos, we missed a week. 
            //The second day of Pesach is always the same day as the first day of Shavuos.
            //So if Pesach was on Thursday, Shavuos will be on Friday and Shabbos in Chu"l.
            //Pesach can never come out on Friday, so in E. Yisroel Shavuos is never on Shabbos.
            if ((!inIsrael) && _savedPesachDay1.DayOfWeek == System.DayOfWeek.Thursday && (jMonth > 3 || (jMonth == 3 && jDay > 6)))
            {
                perekAvos = (perekAvos == 1 ? 6 : perekAvos - 1);
            }
            //If Tisha B'Av was on Shabbos, we missed a week. The first day of Pesach is always the same day of the week as Tisha b'av.
            if (_savedPesachDay1.DayOfWeek == System.DayOfWeek.Saturday && (jMonth > 5 || (jMonth == 5 && jDay > 9)))
            {
                perekAvos = (perekAvos == 1 ? 6 : perekAvos - 1);
            }

            return perekAvos;
        }

        /// <summary>
        /// Gets the double Prakim for the weeks in Ellul.
        /// </summary>
        /// <param name="jDate"></param>
        /// <param name="inIsrael"></param>
        /// <returns></returns>
        private static int[] GetEllulPrakim(JewishDate jDate, bool inIsrael)
        {
            int[] prakim = null;
            int jYear = jDate.Year,
                jMonth = jDate.Month,
                jDay = jDate.Day;
            //The fist day of Ellul.
            //The year/month/day/absoluteDay constructor for JewishDateMicro is used for efficiency.
            var day1 = new JewishDate(jYear, 6, 1, jDate.AbsoluteDate - jDate.Day + 1);
            int day1DOW = day1.DayInWeek;
            int shabbos1Day = day1DOW == 6 ? 1 : ((6 - (day1DOW + 6) % 6) + 1);
            var shabbos1Date = new JewishDate(jYear, 6, shabbos1Day, day1.AbsoluteDate + shabbos1Day - 1);
            //Which shabbos in Ellul are we working out now?
            int currentShabbosNumber = jDay == shabbos1Day ? 1 : ((jDay - shabbos1Day) / 7) + 1;

            switch (GetSinglePerek(shabbos1Date, inIsrael))
            {
                case 1:
                    switch (currentShabbosNumber)
                    {
                        case 1:
                            prakim = new int[] { 1 };
                            break;
                        case 2:
                            prakim = new int[] { 2 };
                            break;
                        case 3:
                            prakim = new int[] { 3, 4 };
                            break;
                        case 4:
                            prakim = new int[] { 5, 6 };
                            break;
                    }
                    break;
                case 2:
                    switch (currentShabbosNumber)
                    {
                        case 1:
                            prakim = new int[] { 2 };
                            break;
                        case 2:
                            prakim = new int[] { 3 };
                            break;
                        case 3:
                            prakim = new int[] { 4 };
                            break;
                        case 4:
                            prakim = new int[] { 5, 6 };
                            break;
                    }
                    break;
                case 3:
                    switch (currentShabbosNumber)
                    {
                        case 1:
                            prakim = new int[] { 3 };
                            break;
                        case 2:
                            prakim = new int[] { 4 };
                            break;
                        case 3:
                            prakim = new int[] { 5 };
                            break;
                        case 4:
                            prakim = new int[] { 6 };
                            break;
                    }
                    break;
                case 4:
                    //This can only happen in Chutz La'aretz
                    switch (currentShabbosNumber)
                    {
                        case 1:
                            prakim = new int[] { 4, 5 };
                            break;
                        case 2:
                            prakim = new int[] { 6, 1 };
                            break;
                        case 3:
                            prakim = new int[] { 2, 3 };
                            break;
                        case 4:
                            prakim = new int[] { 4, 5, 6 };
                            break;
                    }
                    break;
                case 5:
                    switch (currentShabbosNumber)
                    {
                        case 1:
                            prakim = new int[] { 5, 6 };
                            break;
                        case 2:
                            prakim = new int[] { 1, 2 };
                            break;
                        case 3:
                            prakim = new int[] { 3, 4 };
                            break;
                        case 4:
                            prakim = new int[] { 5, 6 };
                            break;
                    }
                    break;
                case 6:
                    switch (currentShabbosNumber)
                    {
                        case 1:
                            prakim = new int[] { 6 };
                            break;
                        case 2:
                            prakim = new int[] { 1, 2 };
                            break;
                        case 3:
                            prakim = new int[] { 3, 4 };
                            break;
                        case 4:
                            prakim = new int[] { 5, 6 };
                            break;
                    }
                    break;
            }

            return prakim;
        }
    }
}
