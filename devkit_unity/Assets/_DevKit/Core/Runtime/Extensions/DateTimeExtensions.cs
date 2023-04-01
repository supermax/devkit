using System;

namespace DevKit.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string Invariant(this TimeZone dt)
        {
            var tz = System.TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now)
                ? System.TimeZone.CurrentTimeZone.DaylightName
                : System.TimeZone.CurrentTimeZone.StandardName;
            return tz;
        }
    }
}
