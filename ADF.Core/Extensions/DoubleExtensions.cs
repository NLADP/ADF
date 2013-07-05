namespace Adf.Core.Extensions
{
    /// <summary>
    /// Represents an utility class to check whether a value belongs to a specified range.
    /// Provides method to check whether a value belongs to a specified range.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the specified value belongs to a specified range.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="min">The lower limit of the range.</param>
        /// <param name="max">The upper limit of the range.</param>
        /// <returns>
        /// true if the value belongs to the range; otherwise, false.
        /// </returns>
        public static bool InRange(this double value, double? min, double? max)
        {
            if (value < min) return false;
            if (value > max) return false;
            
            return true;
        }
    }
}