using System.Collections.Generic;
using System.Linq;

namespace Tahara
{
    /// <summary>
    /// Represents a single וסת
    /// </summary>        
    public class Entry : Onah
    {
        private readonly List<Kavuah> _noKavuahList = new List<Kavuah>();

        public Entry() : base() { }

        public Entry(int day, int month, int year, DayNight dayNight, string notes)
            : base(day, month, year, dayNight)
        {
            this.Notes = notes;
        }

        public void SetInterval(Entry previousEntry)
        {
            this.Interval = this.GetInterval(previousEntry);
        }

        public string Notes { get; set; }

        /// <summary>
        /// The number of days between the previous entry and this one.
        /// Used for Haflagah calculations.
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// When an entry should be (almost) totally ignored such as if the entry was 
        /// imported together with a Kavuah that it was a SettngEntry for etc.
        /// </summary>
        public bool IsInvisible { get; set; }

        /// <summary>
        /// If during a search for Kavuahs 
        /// (I.E. after the addition of a new Entry or during a "search for Kavuah" button click event)
        /// the program finds a set of 3 entries that might have been considered a Kavuah;
        /// such as if there are 3 of the same haflagas in a row,
        /// the user is prompted to create a new kavuah. If they choose not to,
        /// a NoKavuah element is added to the 3rd entry so the user 
        /// won't be prompted again when the list is searched again for possible Kavuahs.
        /// The reason this is a collection, is because there can be different types of Kavuahs on a single day. 
        /// </summary>        
        public List<Kavuah> NoKavuahList => this._noKavuahList;

        /// <summary>
        /// Sorts the list of entries in order of occurrence, then sets the Interval for each Entry -
        /// which is the days elapsed since the previous Entry.
        /// This is in order to Cheshbon out the Haflagah
        /// </summary>
        public static void SortEntriesAndSetInterval(List<Entry> list)
        {
            list.Sort(Onah.CompareOnahs);

            Entry previousEntry = null;
            foreach (Entry entry in list.Where(en => !en.IsInvisible))
            {
                if (previousEntry != null)
                {
                    entry.SetInterval(previousEntry);
                }
                else
                {
                    entry.Interval = 0;
                }
                previousEntry = entry;
            }
        }
    }
}
