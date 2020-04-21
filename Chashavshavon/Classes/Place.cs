using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Chashavshavon.Utils
{
    public class Place
    {        
        public int PlaceId { get; private set; }
        public string Name { get; private set; }
        public string NameHebrew { get; private set; }
        public bool IsInIsrael { get; private set; }
        public int LatitudeDegrees { get; private set; }
        public int LatitudeMinutes { get; private set; }
        public LatitudeTypeEnum LatitudeType { get; private set; }
        public int LongitudeDegrees { get; private set; }
        public int LongitudeMinutes { get; private set; }
        public LongitudeTypeEnum LongitudeType { get; private set; }
        public int TimeZone { get; private set; }
        public int Elevation { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }

        public static List<Place> PlacesList = new List<Place>();
        
        static Place()
        {
            XmlDocument PlacesXml = new XmlDocument();
            PlacesXml.Load(Application.StartupPath + "\\Places.xml");
            foreach (XmlNode locNode in PlacesXml.SelectNodes("//PLC"))
            {
                PlacesList.Add(GetPlaceObjectFromNode(locNode));
            }
        }

        public static Place GetPlace(int PlaceId)
        {
            return PlacesList.Where(l => l.PlaceId == PlaceId).SingleOrDefault();
        }

        private static Place GetPlaceObjectFromNode(XmlNode locNode)
        {
            XmlNode eng = locNode.SelectSingleNode("ENG"),
                    heb = locNode.SelectSingleNode("HEB"),
                    israel = locNode.SelectSingleNode("IL"),
                    lat = locNode.SelectSingleNode("LAT"),
                    lon = locNode.SelectSingleNode("LON"),
                    elevation = locNode.SelectSingleNode("ELV"),
                    timezone = locNode.SelectSingleNode("TZ");
            float latNum = Convert.ToSingle(lat.InnerText),
                  lonNum = Convert.ToSingle(lon.InnerText);
            int latDegrees = Convert.ToInt32(Math.Floor(Math.Abs(latNum))),
                latMinutes = Convert.ToInt32((Math.Abs(latNum) - latDegrees) * 60),
                lonDegrees = Convert.ToInt32(Math.Floor(Math.Abs(lonNum))),
                lonMinutes = Convert.ToInt32((Math.Abs(lonNum) - lonDegrees) * 60);

            return new Place()
            {
                PlaceId = Convert.ToInt32(locNode.Attributes["ID"].InnerText),
                Name = eng.InnerText,
                NameHebrew = (heb?.InnerText),
                IsInIsrael = (israel != null && israel.InnerText == "1"),
                LatitudeDegrees = latDegrees,
                LatitudeMinutes = latMinutes,
                LatitudeType = (latNum >= 0) ? LatitudeTypeEnum.North : LatitudeTypeEnum.South,
                LongitudeDegrees = lonDegrees,
                LongitudeMinutes = lonMinutes,
                LongitudeType = lonNum < 0 ? LongitudeTypeEnum.East : LongitudeTypeEnum.West,
                TimeZone = Convert.ToInt32(timezone.InnerText),
                Elevation = Convert.ToInt32(elevation.InnerText)
            };            
        }
    }
}
