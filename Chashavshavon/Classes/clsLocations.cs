using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Chashavshavon.Utils;

namespace Chashavshavon
{
    public static class clsLocations
    {


        
        public static List<Location> Locations = new List<Location>();

        static clsLocations()
        {
            XmlDocument locationsXml = new XmlDocument();
            locationsXml.Load(Application.StartupPath + "\\Locations.xml");
            foreach (XmlNode locNode in locationsXml.SelectNodes("//LOC"))
            {
                Locations.Add(GetLocationObjectFromNode(locNode));
            }
        }

        public static TimeSpan GetShkiaTime(DateTime forDate, Location location)
        {
            TimesCalculation tc = new TimesCalculation();
            AstronomicalTime shkia = tc.GetSunset(forDate, location);
            TimeZone tz = System.TimeZone.CurrentTimeZone;

            return (new TimeSpan(shkia.Hour + (tz.IsDaylightSavingTime(forDate) ? 1 : 0),
                shkia.Minute, 0));
        }

        public static Location GetLocation(int locationId)
        {
            return Locations.Where(l => l.LocationId == locationId).SingleOrDefault();
        }

        public static Location GetLocationObjectFromNode(XmlNode locNode)
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

            Location location = new Location();

            location.LocationId = Convert.ToInt32(locNode.Attributes["ID"].InnerText);
            location.Name = eng.InnerText;
            location.NameHebrew = (heb != null ? heb.InnerText : null);
            location.IsInIsrael = (israel != null && israel.InnerText == "1");
            location.LatitudeDegrees = Convert.ToInt32(Math.Floor(Math.Abs(latNum)));
            location.LatitudeMinutes = Convert.ToInt32((Math.Abs(latNum) - location.LatitudeDegrees) * 60);
            location.LatitudeType = (latNum >= 0) ? LatitudeTypeEnum.North : LatitudeTypeEnum.South;
            location.LongitudeDegrees = Convert.ToInt32(Math.Floor(Math.Abs(lonNum)));
            location.LongitudeMinutes = Convert.ToInt32((Math.Abs(lonNum) - location.LongitudeDegrees) * 60);
            location.LongitudeType = lonNum < 0 ? LongitudeTypeEnum.East : LongitudeTypeEnum.West;
            location.TimeZone = Convert.ToInt32(timezone.InnerText);
            location.Elevation = Convert.ToInt32(elevation.InnerText);
            return location;
        }
    }
}
