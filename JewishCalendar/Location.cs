using System;
using System.Linq;

namespace JewishCalendar
{
    /// <summary>
    /// Represents a place or location. Used for calculating the zmanim.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Latitude type
        /// </summary>
        public enum LatitudeTypes
        {
            /// <summary>
            /// North of the equator
            /// </summary>
            North,

            /// <summary>
            /// South of the equator
            /// </summary>
            South
        }

        /// <summary>
        /// Longitude type
        /// </summary>
        public enum LongitudeTypes
        {
            /// <summary>
            /// East of Greenwich England
            /// </summary>
            East,

            /// <summary>
            /// West of Greenwich England
            /// </summary>
            West
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public Location() { }

        /// <summary>
        /// Create a new location.
        /// </summary>
        /// <param name="name">Name of Location</param>
        /// <param name="timeZone">The number of hours this location is offset from GMT (UTC)</param>
        /// <param name="latitude">The latitude of this location</param>
        /// <param name="longitute">The longitude of this location</param>
        public Location(string name, int timeZone, double latitude, double longitute)
        {
            this.Name = name;
            this.TimeZone = timeZone;

            this.LatitudeType = latitude >= 0 ? LatitudeTypes.North : LatitudeTypes.South;
            this.LongitudeType = longitute >= 0 ? LongitudeTypes.West : LongitudeTypes.East;

            double absLat = Math.Abs(latitude);
            double absLon = Math.Abs(longitute);

            this.LatitudeDegrees = (int)Math.Truncate(Math.Floor(absLat));
            this.LatitudeMinutes = 60 * (absLat - this.LatitudeDegrees);

            this.LongitudeDegrees = (int)Math.Truncate(Math.Floor(absLon));
            this.LongitudeMinutes = 60 * (absLon - this.LongitudeDegrees);
        }

        /// <summary>
        /// The english name for this place
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The name of this place in Hebrew
        /// </summary>
        public string NameHebrew { get; set; }

        /// <summary>
        /// Is this location in Israel?
        /// </summary>
        public bool IsInIsrael { get; set; }

        /// <summary>
        /// Number of latitude degrees
        /// </summary>
        public int LatitudeDegrees { get; set; }

        /// <summary>
        /// Number of latitude minutes
        /// </summary>
        public double LatitudeMinutes { get; set; }

        /// <summary>
        /// Is this place above or below the equator?
        /// </summary>
        public LatitudeTypes LatitudeType { get; set; }

        /// <summary>
        /// Number of longitude degrees
        /// </summary>
        public int LongitudeDegrees { get; set; }

        /// <summary>
        /// Number of longitude minutes
        /// </summary>
        public double LongitudeMinutes { get; set; }

        /// <summary>
        /// Is this east or West of Greenwich England
        /// </summary>
        public LongitudeTypes LongitudeType { get; set; }

        /// <summary>
        /// Time zone in relation to GMT
        /// </summary>
        public int TimeZone { get; set; }

        /// <summary>
        /// Needed for figuring out date of transition to DST.
        /// </summary>
        public string TimeZoneName
        {
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.TimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(value.Trim());
                    }
                }
                catch (TimeZoneNotFoundException)
                {
                    this.TimeZoneInfo = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(t =>
                        t.Id.ToLower().Contains(value.Trim().ToLower()));
                }
            }
        }

        /// <summary>
        /// Needed for figuring out date of transition to DST.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; set; }

        /// <summary>
        /// Elevation of this location in meters
        /// </summary>
        public int Elevation { get; set; }

        /// <summary>
        /// Number of minutes before sunset is candle lighting.
        /// The default for this is, in Israel 30 minutes and in Chutz La'aretz 18 minutes.
        /// </summary>
        public int CandleLighting { get; set; }

        /// <summary>
        /// Compares another Location to this one to see if they are geographically identical.
        /// Does not take Location Name or Candle Lighting Time into account.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsSameLocation(Location other)
        {
            return other.IsInIsrael == this.IsInIsrael &&
other.TimeZone == this.TimeZone &&
other.Elevation == this.Elevation &&
other.LatitudeType == this.LatitudeType &&
other.LatitudeDegrees == this.LatitudeDegrees &&
other.LatitudeMinutes == this.LatitudeMinutes &&
other.LongitudeType == this.LongitudeType &&
other.LongitudeDegrees == this.LongitudeDegrees &&
other.LongitudeMinutes == this.LongitudeMinutes;
        }


        /// <summary>
        /// Returns the name of this Location in English.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}