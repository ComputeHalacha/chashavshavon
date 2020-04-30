using System;

namespace JewishCalendar
{
    /// <summary>
    /// A simpler Time Span.
    /// Explicitly cast-able to and from TimeSpan.
    /// </summary>
    public struct TimeOfDay
    {
        /// <summary>
        /// The hour
        /// </summary>
        public int Hour;

        /// <summary>
        /// The minute
        /// </summary>
        public int Minute;

        /// <summary>
        /// The seconds
        /// </summary>
        public int Seconds;

        /// <summary>
        /// Returns the total number of seconds that have passed from 0:00:00 until this time
        /// </summary>
        public int TotalSeconds =>
            (this.Hour * 3600) + (this.Minute * 60) + this.Seconds;

        /// <summary>
        /// Returns a TimeSpan representation of this TimeOfDay
        /// </summary>
        /// <returns></returns>
        public TimeSpan ToTimeSpan()
        {
            return new TimeSpan(this.Hour, this.Minute, this.Seconds);
        }

        /// <summary>
        /// An TimeOfDay that does not represent a real time.
        /// Use in the place of null or empty etc.
        /// Note: very different from TimeSpan.Zero which represents "zero hour" or midnight.
        /// </summary>
        public static TimeOfDay NoValue =>
            //-1 minutes is a beautiful meaningless value.
            //(negative hours is pretty meaningless too, but using negative hours can sometimes be useful for calculations)
            new TimeOfDay { Minute = -1 };

        /// <summary>
        /// Add minutes
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static TimeOfDay operator +(TimeOfDay t, int min)
        {
            var result = new TimeOfDay
            {
                Hour = t.Hour,
                Minute = t.Minute + min,
                Seconds = t.Seconds
            };
            while (result.Minute >= 60)
            {
                result.Minute -= 60;
                result.Hour++;
            }
            //If we are adding a negative number
            while (result.Minute < 0)
            {
                result.Minute += 60;
                result.Hour--;
            }
            while (result.Hour >= 24)
            {
                result.Hour -= 24;
            }
            return result;
        }

        /// <summary>
        /// Add minutes
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static TimeOfDay operator +(TimeOfDay t, double min)
        {
            var result = new TimeOfDay
            {
                Hour = t.Hour,
                Minute = t.Minute + (int)Math.Floor(min),
                Seconds = (int)(60 * (min - Math.Floor(min)))
            };
            while (result.Seconds >= 60)
            {
                result.Minute++;
                result.Seconds -= 60;
            }
            while (result.Minute >= 60)
            {
                result.Hour++;
                result.Minute -= 60;
            }
            //If we are adding a negative number
            while (result.Seconds < 0)
            {
                result.Minute--;
                result.Seconds += 60;
            }
            while (result.Minute < 0)
            {
                result.Minute += 60;
                result.Hour--;
            }

            while (result.Hour >= 24)
            {
                result.Hour -= 24;
            }
            while (result.Hour < 0)
            {
                result.Hour += 24;
            }
            return result;
        }

        /// <summary>
        /// Add the number of hours and minutes in the given TimeOfDay to the current TimeOfDay
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        //Let's cheat and use a real TimeSpan for this.
        public static TimeOfDay operator +(TimeOfDay t1, TimeOfDay t2)
        {
            return (TimeOfDay)(t1.ToTimeSpan().Add(t2.ToTimeSpan()));
        }

        /// <summary>
        /// Add a TimeSpan to this TimeOfDay
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static TimeOfDay operator +(TimeOfDay t, TimeSpan ts)
        {
            return (TimeOfDay)(t.ToTimeSpan().Add(ts));
        }

        /// <summary>
        /// Subtract minutes.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static TimeOfDay operator -(TimeOfDay t, int min)
        {
            var result = new TimeOfDay
            {
                Hour = t.Hour,
                Minute = t.Minute - min,
                Seconds = t.Seconds
            };
            while (result.Minute < 0)
            {
                result.Minute += 60;
                result.Hour--;
            }
            //If we are subtracting a negative number
            while (result.Minute >= 60)
            {
                result.Minute -= 60;
                result.Hour++;
            }

            while (result.Hour >= 24)
            {
                result.Hour -= 24;
            }

            while (result.Hour < 0)
            {
                result.Hour += 24;
            }
            return result;
        }

        /// <summary>
        /// Subtract minutes.
        /// </summary>
        /// <param name="t"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static TimeOfDay operator -(TimeOfDay t, double min)
        {
            var result = new TimeOfDay
            {
                Hour = t.Hour,
                Minute = t.Minute - (int)Math.Floor(min),
                Seconds = t.Seconds - (int)(60d * (min - Math.Floor(min)))
            };
            while (result.Seconds < 0)
            {
                result.Seconds += 60;
                result.Minute--;
            }
            while (result.Minute < 0)
            {
                result.Minute += 60;
                result.Hour--;
            }
            //If we are subtracting a negative number
            while (result.Seconds >= 60)
            {
                result.Seconds -= 60;
                result.Minute++;
            }
            while (result.Minute >= 60)
            {
                result.Minute -= 60;
                result.Hour++;
            }
            return result;
        }

        /// <summary>
        /// Subtract the number of hours and minutes in the given TimeOfDay from the current TimeOfDay
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static TimeOfDay operator -(TimeOfDay t1, TimeOfDay t2)
        {
            return (TimeOfDay)(t1.ToTimeSpan().Subtract(t2.ToTimeSpan()));
        }

        /// <summary>
        /// Subtract a TimeSpan from this TimeOfDay
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static TimeOfDay operator -(TimeOfDay t, TimeSpan ts)
        {
            return (TimeOfDay)(t.ToTimeSpan().Subtract(ts));
        }

        /// <summary>
        /// Compares 2 TimeOfDay objects
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool operator ==(TimeOfDay t1, TimeOfDay t2)
        {
            return (t1.Hour == t2.Hour && t1.Minute == t2.Minute && t1.Seconds == t2.Seconds);
        }

        /// <summary>
        /// Compares 2 TimeOfDay objects for inequality
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool operator !=(TimeOfDay t1, TimeOfDay t2)
        {
            return !(t1 == t2);
        }

        /// <summary>
        /// Compare the current TimeOfDay to a TimeSpan
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool operator ==(TimeOfDay t1, TimeSpan ts)
        {
            return t1.TotalSeconds == Convert.ToInt32(Math.Floor(ts.TotalSeconds));
        }

        /// <summary>
        /// Compare the current TimeOfDay to a TimeSpan for inequality
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool operator !=(TimeOfDay t1, TimeSpan ts)
        {
            return !(t1 == ts);
        }

        /// <summary>
        /// Returns true if the current TimeOfDay is after the second one.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool operator >(TimeOfDay t1, TimeOfDay t2)
        {
            return t1.TotalSeconds > t2.TotalSeconds;
        }

        /// <summary>
        /// Returns true if the current TimeOfDay is before the second one.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool operator <(TimeOfDay t1, TimeOfDay t2)
        {
            return t1.TotalSeconds < t2.TotalSeconds;
        }

        /// <summary>
        /// Returns true if the current TimeOfDay is after the TimeSpan.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool operator >(TimeOfDay t1, TimeSpan ts)
        {
            return t1.TotalSeconds > ts.TotalSeconds;
        }

        /// <summary>
        /// Returns true if the current TimeOfDay is not before the second one.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool operator >=(TimeOfDay t1, TimeOfDay t2)
        {
            return t1.TotalSeconds >= t2.TotalSeconds;
        }

        /// <summary>
        /// Returns true if the current TimeOfDay is not after the second one.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static bool operator <=(TimeOfDay t1, TimeOfDay t2)
        {
            return ((t1.Hour * 60 + t1.Minute) <= (t2.Hour * 60 + t2.Minute));
        }

        /// <summary>
        /// Returns true if the current TimeOfDay is not before the TimeSpan.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool operator >=(TimeOfDay t1, TimeSpan ts)
        {
            return ((t1.Hour * 60 + t1.Minute) >= ts.TotalMinutes);
        }

        /// <summary>
        /// Returns true if the current TimeOfDay is not after the TimeSpan.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool operator <=(TimeOfDay t1, TimeSpan ts)
        {
            return ((t1.Hour * 60 + t1.Minute) <= ts.TotalMinutes);
        }

        /// <summary>
        /// Returns true if the current TimeOfDay is before the TimeSpan.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static bool operator <(TimeOfDay t1, TimeSpan ts)
        {
            return ((t1.Hour * 60 + t1.Minute) < ts.TotalMinutes);
        }

        /// <summary>
        /// Explicitly convert (cast) an TimeOfDay into a TimeSpan.
        /// </summary>
        /// <param name="hm"></param>
        /// <returns></returns>
        public static explicit operator TimeSpan(TimeOfDay hm)
        {
            return hm.ToTimeSpan();
        }

        /// <summary>
        /// Explicitly convert (cast) a TimeSpan into an TimeOfDay
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static explicit operator TimeOfDay(TimeSpan ts)
        {
            return new TimeOfDay { Hour = ts.Hours, Minute = ts.Minutes };
        }

        /// <summary>
        /// The total number of minutes represented by this TimeOfDay (includes the hours)
        /// </summary>
        public int TotalMinutes => this.Hour * 60 + this.Minute;


        /// <summary>
        /// The hour and minute displayed in the format: h:MM tt
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToString(false);
        }

        /// <summary>
        /// This TimeOfDay displayed in the format: h:MM:SS tt
        /// </summary>
        /// <returns></returns>
        public string ToString(bool showSeconds)
        {
            return (this.Hour <= 12 ? (this.Hour == 0 ? 12 : this.Hour) : this.Hour - 12).ToString() +
":" + ((this.Minute < 10 ? "0" : "") + this.Minute.ToString()) +
(showSeconds ? (":" + (this.Seconds < 10 ? "0" : "") + this.Seconds.ToString()) : "") +
(this.Hour < 12 ? " AM" : " PM");
        }

        /// <summary>
        /// This TimeOfDay displayed in the format: HH:MM:SS
        /// </summary>
        /// <param name="army"></param>
        /// <param name="amPm"></param>
        /// <param name="showSeconds"></param>
        /// <returns></returns>
        public string ToString(bool army, bool amPm = true, bool showSeconds = false)
        {
            return army
? this.ToString24H(showSeconds) : (amPm ? this.ToString()
: (this.Hour <= 12 ? (this.Hour == 0 ? 12 : this.Hour) : this.Hour - 12).ToString() +
":" + (this.Minute < 10 ? "0" + this.Minute.ToString() : this.Minute.ToString())) +
(showSeconds ? (":" + (this.Seconds < 10 ? "0" : "") + this.Seconds.ToString()) : "");
        }

        /// <summary>
        /// Returns the current time in the format HH:mm:SS
        /// </summary>
        /// <returns></returns>
        public string ToString24H(bool showSeconds = false)
        {
            return (this.Hour.ToString() +
":" + (this.Minute < 10 ? "0" + this.Minute.ToString() : this.Minute.ToString())) +
(showSeconds ? (":" + (this.Seconds < 10 ? "0" : "") + this.Seconds.ToString()) : "");
        }

        /// <summary>
        /// Tests 2 TimeOfDay objects for equality.
        /// </summary>
        /// <param name="obj">The object to compare to this one.</param>
        /// <returns>True if the two object are Equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is TimeOfDay)
            {
                return ((TimeOfDay)obj) == this;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the hashcode for this instance
        /// </summary>
        /// <returns>The HashCode for this instance</returns>
        public override int GetHashCode()
        {
            return this.Hour.GetHashCode() ^ this.Minute.GetHashCode();
        }
    }
}