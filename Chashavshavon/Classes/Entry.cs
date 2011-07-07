using System;
using System.Collections.Generic;
using System.Text;

namespace Chashavshavon
{
    public class Entry : Onah
    {

        private List<Kavuah> _noKavuahs = new List<Kavuah>();
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
        public List<Kavuah> NoKavuahs { 
            get 
            { 
                return this._noKavuahs; 
            }
        }

    }
}
