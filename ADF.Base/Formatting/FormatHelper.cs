using System;
using System.Globalization;
using Adf.Base.State;
using Adf.Core.Extensions;
using Adf.Core.State;

namespace Adf.Base.Formatting
{
    /// <summary>
    /// Represent the helper class handling formatting objects.
    /// Provides datetime validation and formatting.
    /// </summary>
    public static class FormatHelper
    {
        /// <summary>
        /// Convert the specified datetime object into specified format.
        /// </summary>
        /// <param name="value">The datetime value converted into given format.</param>
        /// <param name="format">The datetime value will parsed by this format.</param>
        /// <returns>The formated datetime value if specified datetime object is not null; otherwise, empty string.</returns>
        public static string Format(DateTime? value, string format)
        {
            return value == null ? string.Empty : value.Value.ToString(string.IsNullOrEmpty(format) ? DefaultDateFormat : format, CultureInfo.CurrentUICulture);
        }

        public static string DefaultDateFormat
        {
            get
            {
                return StateManager.Settings.GetOrDefault("DateFormat", CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
            }
        }

        public static string DecimalFormat
        {
            get
            {
                return StateManager.Settings.GetOrDefault("DecimalFormat", "N");
            }
        }

//        /// <summary>
//        /// Get the correct datetime by the specified reference type datetime object.
//        /// </summary>
//        /// <param name="value">The reference type datetime object.</param>
//        /// <returns>The datetime if the specified value is valid; otherwise, null.</returns>
//        private static DateTime? GetDate(object value)
//        {
//            if (value is DateTime || value is DateTime?) return (DateTime?) value;
//
//            return null;
//        }

        /// <summary>
        /// Parse the specified datetime object into specified format.
        /// </summary>
        /// <param name="value">The datetime value which will be parsed.</param>
        /// <returns>Expected datetime value.</returns>
        /// <exception cref="System.ArgumentNullException">A null reference is passed.</exception>
        /// <exception cref="System.FormatException">The format does not meet parameter specification.</exception>
        public static DateTime? ParseToDateTime(string value)
        {
            DateTime? result;

            ParseToDateTime(value, out result);

            return result;
        }

        /// <summary>
        /// Validate the datetime value.
        /// </summary>
        /// <param name="value">The datetime value which will be parsed.</param>
        /// <returns>True if specified datetime value is not null or empty; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">A null reference is passed.</exception>
        /// <exception cref="System.FormatException">The format does not meet parameter specification.</exception>
        public static bool IsValidDateTime(string value)
        {
            DateTime? result;

            return ParseToDateTime(value, out result);
        }

        /// <summary>
        /// Parse the specified datetime into specified format.
        /// </summary>
        /// <param name="value">The datetime value which will be parse.</param>
        /// <param name="result">The parsed result would contains after parsing.</param>
        /// <returns>True if specified datetime value is not null or empty; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">A null reference is passed.</exception>
        /// <exception cref="System.FormatException">The format does not meet parameter specification.</exception>
//        private static bool ParseToDateTime(string value, string format, out DateTime? result)
        private static bool ParseToDateTime(string value, out DateTime? result)
        {
            result = null;
            DateTime date;
            bool valid = true;

            if (string.IsNullOrEmpty(value)) return true;
            
//            if (string.IsNullOrEmpty(format))
            {
                valid = DateTime.TryParse(value, CultureInfo.CurrentUICulture, DateTimeStyles.None, out date);
                result = date;
            }
//            else
//            {
//                valid = DateTime.TryParseExact(value,
//                                               StateManager.Settings[format].ToString(),
//                                               CultureInfo.CurrentUICulture,
//                                               DateTimeStyles.None,
//                                               out date);
//                result = date;
//            }

            return valid;
        }

        /// <summary>
        /// The default .NET implementation of decimal.ToString(culture) doesn't add the thousand seperator, 
        /// decimal.ToString(format, culture) only shows 2 decimal digits.
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="cult"></param>
        /// <returns></returns>
        public static string ToStringKeepOriginalDecimals(this decimal quantity, CultureInfo cult)
        {
            var text = quantity.ToString(cult);

            // set separator points
            int i = text.LastIndexOf(cult.NumberFormat.NumberDecimalSeparator);

            while (i > 0)
            {
                foreach (int groupSize in cult.NumberFormat.NumberGroupSizes)
                {
                    i -= groupSize;

                    if (i <= 0) return text;

                    if (i == 1 && quantity < 0) return text;  // last char is '-'

                    text = text.Insert(i, cult.NumberFormat.NumberGroupSeparator);
                }
            }
            return text;
        }

        public static string ToString(object value, string format = null, bool breakLongWords = false)
        {
            if (value == null)
            {
                return string.Empty;
            }
            if (value is Enum)
            {
                return (value as Enum).GetDescription();
            }
            if (value is DateTime?)
            {
                var dateTime = value as DateTime?;

                return dateTime.Value.ToString(format ?? DefaultDateFormat, CultureInfo.CurrentUICulture);
            }
            if (value is decimal)
            {
                return ((decimal)value).ToStringKeepOriginalDecimals(CultureInfo.CurrentUICulture);
            }
            if (value is IFormattable)
            {
                return ((IFormattable)value).ToString(format, CultureInfo.CurrentUICulture);
            }
            return breakLongWords ? value.ToString().BreakLongWords() : value.ToString();
        }
    }
}
