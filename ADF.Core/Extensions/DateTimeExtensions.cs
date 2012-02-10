using System;

namespace Adf.Core.Extensions
{
    public static class DateTimeExtensions
    {

        public static bool IsInRange(this DateTime current, DateTime start, DateTime end)
        {
            return start <= current && current <= end;
        }

        public static bool IsInRange(this DateTime? current, DateTime? start, DateTime? end)
        {
            return current != null && start != null && end != null && current.Value.IsInRange(start.Value, end.Value);
        }

        public static bool IsInRangeWithinYear(this DateTime year, DateTime start, DateTime end)
        {
            return start.DayOfYear <= year.DayOfYear && year.DayOfYear <= end.DayOfYear;
        }

    }
}
