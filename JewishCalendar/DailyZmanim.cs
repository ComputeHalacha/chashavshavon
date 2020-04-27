using System;

namespace JewishCalendar
{
    /// <summary>
    /// The type of the single Zman
    /// </summary>
    public enum ZmanType
    {
        /// <summary>
        /// Sunrise at sea level
        /// </summary>
        NetzMishor,
        /// <summary>
        /// Sunrise at current Locations elevation
        /// </summary>
        NetzAtElevation,
        /// <summary>
        /// Say Shma by... according to the Magen Avraham
        /// </summary>
        KShmMga,
        /// <summary>
        /// Say Shma by... according to the Gr"a
        /// </summary>
        KshmGra,
        /// <summary>
        /// Say Shmonah Esray of Shacharis by... according to the Magen Avraham
        /// </summary>
        TflMga,
        /// <summary>
        /// Say Shmonah Esray of Shacharis by... according to the Gr"a
        /// </summary>
        TflGra,
        /// <summary>
        /// Chatzos of the night and the day
        /// </summary>
        Chatzos,
        /// <summary>
        /// Mincha Gedolah
        /// </summary>
        MinchaG,
        /// <summary>
        /// Mincha Ketana
        /// </summary>
        MinchaK,
        /// <summary>
        /// Plag Hamincha
        /// </summary>
        MinchaPlg,
        /// <summary>
        /// Sunset at current Locations elevation
        /// </summary>
        ShkiaAtElevation,
        /// <summary>
        /// Sunset at sea level
        /// </summary>
        ShkiaMishor
    }
    /// <summary>
    /// Gives efficient access to the daily Zmanim for a single day at the given location
    /// </summary>
    public class DailyZmanim
    {
        private readonly Zmanim _zmanim;
        private TimeOfDay[] _netzshkiaAtElevation = null;
        private TimeOfDay[] _netzshkiaMishor = null;
        private TimeOfDay _chatzos = TimeOfDay.NoValue;
        private double _shaaZmanis;
        private double _shaaZmanisMga;

        /// <summary>
        /// Create a new DailyZmanim object for the given date and location
        /// </summary>
        /// <param name="sd">The Gregorian Date</param>
        /// <param name="location">The location</param>
        public DailyZmanim(DateTime sd, Location location)
        {
            this._zmanim = new Zmanim(sd, location);
        }

        /// <summary>
        /// Access the underlying Zmanim object
        /// </summary>
        public Zmanim Zmanim => this._zmanim;
        /// <summary>
        /// The Gregorian Date
        /// </summary>
        public DateTime SecularDate
        {
            get => this._zmanim.SecularDate;
            set
            {
                if (value.Date != this.SecularDate.Date)
                {
                    this._zmanim.SecularDate = value;
                    this.Reset();
                }
            }
        }
        /// <summary>
        /// The Jewish Date
        /// </summary>
        public JewishDate JewishDate => new JewishDate(this._zmanim.SecularDate);

        /// <summary>
        /// The Location
        /// </summary>
        public Location Location
        {
            get => this._zmanim.Location;
            set
            {
                if (!this.Location.IsSameLocation(value))
                {
                    this._zmanim.Location = value;
                    this.Reset();
                }
            }
        }
        /// <summary>
        /// Gets an array of two TimeOfDay structures for the current Date and Location. 
        /// The first is the time of Netz for the current date at the elevation 
        /// and coordinates of the current Location and the second is the time of shkia.        
        /// </summary>
        public TimeOfDay[] NetzShkiaAtElevation
        {
            get
            {
                if (this._netzshkiaAtElevation == null)
                {
                    this._netzshkiaAtElevation = this._zmanim.GetNetzShkia(true);
                }
                return this._netzshkiaAtElevation;
            }
        }
        /// <summary>
        /// Gets an array of two TimeOfDay structures at sea level for the current Date and Location. 
        /// The first is the time of Netz for the current date and location and the second is the time of shkia.
        /// The elevation is NOT kept into account.
        /// </summary>
        public TimeOfDay[] NetzShkiaMishor
        {
            get
            {
                if (this._netzshkiaMishor == null)
                {
                    this._netzshkiaMishor = this._zmanim.GetNetzShkia(false);
                }
                return this._netzshkiaMishor;
            }
        }
        /// <summary>
        /// Sunrise for the current Date at the elevation and coordinates of the current Location. 
        /// </summary>
        public TimeOfDay NetzAtElevation => this.NetzShkiaAtElevation[0];
        /// <summary>
        /// Sunset for the current Date at the elevation and coordinates of the current Location.
        /// </summary>
        public TimeOfDay ShkiaAtElevation => this.NetzShkiaAtElevation[1];
        /// <summary>
        /// Sunrise at sea level for the current Date at the coordinates of the current Location.
        /// </summary>
        public TimeOfDay NetzMishor => this.NetzShkiaMishor[0];
        /// <summary>
        /// Sunset at sea level for the current Date at the coordinates of the current Location.
        /// </summary>
        public TimeOfDay ShkiaMishor => this.NetzShkiaMishor[1];
        /// <summary>
        /// Chatzos of the day and the night
        /// </summary>
        public TimeOfDay Chatzos
        {
            get
            {
                if (this._chatzos == TimeOfDay.NoValue)
                {
                    this._chatzos = Zmanim.GetChatzos(this.NetzShkiaMishor);
                }
                return this._chatzos;
            }
        }
        /// <summary>
        /// The length of Shaa zmanis in minutes for current date and location.
        /// Configured from netz to shkia at sea level.
        /// </summary>
        public Double ShaaZmanis
        {
            get
            {
                if (this._shaaZmanis == 0)
                {
                    this._shaaZmanis = Zmanim.GetShaaZmanis(this.NetzShkiaMishor);
                }
                return this._shaaZmanis;
            }
        }
        /// <summary>
        /// The length of Shaa zmanis in minutes for current date and location.
        /// Configured from alos hashachar to tzais hakochavim at sea level.
        /// </summary>
        public Double ShaaZmanisMga
        {
            get
            {
                if (this._shaaZmanisMga == 0)
                {
                    this._shaaZmanisMga = Zmanim.GetShaaZmanisMga(this.NetzShkiaMishor, this.Location.IsInIsrael);
                }
                return this._shaaZmanisMga;
            }
        }

        /// <summary>
        /// Gets a single Zman for the current Date and Location
        /// </summary>
        /// <param name="type">The type of Zman to return</param>
        /// <param name="offset">Optional. The number of minutes to offset the zman by.</param>
        /// <returns></returns>
        public TimeOfDay GetZman(ZmanType type, int offset = 0)
        {
            TimeOfDay hm = TimeOfDay.NoValue;
            switch (type)
            {
                case ZmanType.NetzMishor:
                    hm = this.NetzMishor;
                    break;

                case ZmanType.NetzAtElevation:
                    hm = this.NetzAtElevation;
                    break;

                case ZmanType.KShmMga:
                    hm = (this.NetzMishor - 90d) + (this.ShaaZmanisMga * 3d);
                    break;

                case ZmanType.KshmGra:
                    hm = this.NetzMishor + (this.ShaaZmanis * 3d);
                    break;

                case ZmanType.TflMga:
                    hm = (this.NetzMishor - 90d) + (this.ShaaZmanisMga * 4d);
                    break;

                case ZmanType.TflGra:
                    hm = this.NetzMishor + (this.ShaaZmanis * 4d);
                    break;

                case ZmanType.Chatzos:
                    hm = this.Chatzos;
                    break;

                case ZmanType.MinchaG:
                    hm = this.Chatzos + (this.ShaaZmanis * 0.5);
                    break;

                case ZmanType.MinchaK:
                    hm = this.NetzMishor + (this.ShaaZmanis * 9.5);
                    break;

                case ZmanType.MinchaPlg:
                    hm = this.NetzMishor + (this.ShaaZmanis * 10.75);
                    break;

                case ZmanType.ShkiaAtElevation:
                    hm = this.ShkiaAtElevation;
                    break;

                case ZmanType.ShkiaMishor:
                    hm = this.ShkiaMishor;
                    break;

            }
            if (offset != 0)
            {
                hm += offset;
            }
            return hm;
        }

        /// <summary>
        /// Used to invalidate previously calculated Zmanim.
        /// Use when changing the Date or Location
        /// </summary>
        private void Reset()
        {
            this._netzshkiaAtElevation = null;
            this._netzshkiaMishor = null;
            this._chatzos = TimeOfDay.NoValue;
            this._shaaZmanis = 0;
            this._shaaZmanisMga = 0;
        }
    }
}
