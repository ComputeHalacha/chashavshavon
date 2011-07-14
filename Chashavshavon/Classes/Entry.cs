using System;
using System.Collections.Generic;
using System.Text;

namespace Chashavshavon
{
    /// <summary>
    /// Represents a single וסת
    /// </summary>
    public class Entry : Onah
    {
        private List<Kavuah> _noKavuahList = new List<Kavuah>();

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
        public int Interval { get; set; }
        /// <summary>
        /// If during the addition of a new Entry the program finds 
        /// a set of 3 entries that might have been considered a Kavuah;
        /// such as if there are 3 of the same haflagas in a row,
        /// the user is prompted to create a new kavuah. If they choose not to,
        /// a NoKavuah element is added to the 3rd entry so the user 
        /// won't be prompted again each time the list is reloaded.
        /// The reason this is a list, is because there can be different types of Kavuahs on a single day. 
        /// </summary>
        public List<Kavuah> NoKavuahList { 
            get 
            { 
                return this._noKavuahList;
            }
        }
    }
}
