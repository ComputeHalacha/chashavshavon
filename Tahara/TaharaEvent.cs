using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tahara
{
    public enum TaharaEventType
    {
        Hefsek,
        Mikvah,
        Shailah
    }
    [Serializable()]
    public class TaharaEvent
    {
        public TaharaEvent(TaharaEventType type)
        {
            //Set default to true
            this.TaharaEventType = type;
        }

        #region Public Instance Properties
        public TaharaEventType TaharaEventType { get; set; }
        public string TaharaEventTypeName
        {
            get
            {
                switch (this.TaharaEventType)
                {
                    case TaharaEventType.Hefsek:
                        return "הפסק טהרה";
                    case TaharaEventType.Mikvah:
                        return "מקוה";
                    case TaharaEventType.Shailah:
                        return "שאלה";
                }

                return null;
            }
        }

        public DateTime DateTime { get; set; }
        public string Notes { get; set; }
        
        [XmlIgnore]
        public string TaharaEventDescriptionHebrew => this.ToString();
        [XmlIgnore]
        public string TaharaEventDescriptionEnglish => this.ToStringEng();
        #endregion

        #region Public Functions

        public override string ToString()
        {
            return TaharaEventTypeName + " - " + this.DateTime.ToString("dddd dd MMM yyyy", Utils.CultureInfo);
        }        
        
        public string ToStringEng()
        {
            string val = "";

            switch (this.TaharaEventType)
            {
                case TaharaEventType.Hefsek:
                    val += "Hefsek Tahara";
                    break;
                case TaharaEventType.Mikvah:
                    val += "Mikvah";
                    break;
                case TaharaEventType.Shailah:
                    val += "Shailah";
                    break;
            }
            val += " " + this.DateTime.ToString("dddd dd MMM yyyy");
            return val;
        }

        public static void SortList(List<TaharaEvent> list)
        {
            list.Sort((te1, te2) =>
                        te1.DateTime.CompareTo(te2.DateTime));
        }
        #endregion
    }
}