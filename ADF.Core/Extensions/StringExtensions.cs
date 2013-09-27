using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Extensions
{
    ///<summary>
    /// Extension methods for strings.
    ///</summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Breaks the long words in the specified string into smaller words by inserting spaces in the string.
        /// </summary>
        /// <param name="longWord">The string in which the long words exist.</param>
        /// <returns>
        /// The resultant string with the smaller words.
        /// </returns>
        public static string BreakLongWords(this string longWord)
        {
            if (String.IsNullOrEmpty(longWord))
                return String.Empty;

            const int maxWordWidh = 50;

            //return if total length of string is smaller as maxWidth of the words. 
            //Extra manipulation is not needed --> performance
            if (longWord.Length <= maxWordWidh)
                return longWord;

            //Check for words longer than maxWordWidh characters
            var myWords = new StringBuilder();
            foreach (var word in longWord.Split(' '))
            {
                if (myWords.Length > 0)
                    myWords.Append(" ");
                myWords.Append(SplitLongString(word, maxWordWidh));
            }

            return myWords.ToString();
        }

        /// <summary>
        /// Splits the specified string into the words of the specified length.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <param name="maximumWidth">The maximum width of each word in the output string.</param>
        /// <returns>
        /// The output string.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// value is null.
        /// </exception>
        private static string SplitLongString(string value, int maximumWidth)
        {
            if (value.Length <= maximumWidth)
                return value;

            var newString = new StringBuilder();

            newString.Append(value.Substring(0, maximumWidth));
            newString.Append(" ");

            //add the rest recursive (for that de following string is also longer as maximumWidth
            newString.Append(SplitLongString(value.Substring(maximumWidth, value.Length - maximumWidth), maximumWidth));
            return newString.ToString();
        }

        /// <summary>
        /// Returns a value indicating whether the length of the specified string is greater than or 
        /// equal to the specified length.
        /// </summary>
        /// <param name="value">The string, the length of which is to check.</param>
        /// <param name="length">The length to check against.</param>
        /// <returns>
        /// true if the length of the string is greater than or equal to the specified length;
        /// otherwise, false.
        /// </returns>
        public static bool HasMinLength(this string value, int length)
        {
            if (value == null) return false;

            return (value.Length >= length);
        }

        /// <summary>
        /// Returns a value indicating whether the length of the specified string is smaller than or 
        /// equal to the specified length.
        /// </summary>
        /// <param name="value">The string, the length of which is to check.</param>
        /// <param name="length">The length to check against.</param>
        /// <returns>
        /// true if the length of the string is smaller than or equal to the specified length;
        /// otherwise, false.
        /// </returns>
        public static bool HasMaxLength(this string value, int length)
        {
            if (value == null) return false;

            return (value.Length <= length);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static string Replace(this string str, string oldValue, string newValue, StringComparison comparison)
        {
            var sb = new StringBuilder();

            int previousIndex = 0;
            int index = str.IndexOf(oldValue, comparison);
            while (index != -1)
            {
                sb.Append(str.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = str.IndexOf(oldValue, index, comparison);
            }
            sb.Append(str.Substring(previousIndex));

            return sb.ToString();
        }

        public static string Capitalize(this string value)
        {
            if (value.IsNullOrEmpty()) return value;

            var values = value.Split(' ');

            return
                values.Aggregate(string.Empty,
                                 (current, word) => current + (word.Substring(0, 1).ToUpper() + word.Substring(1) + " "))
                      .TrimEnd();
        }
	
		public static string Left(this string origin, int length)
		{
		    if (origin.IsNullOrEmpty()) return origin;

            return (origin.Length <= length) ? origin : origin.Substring(0, length);
        }

        public static string TrimSurroundingChars(this string origin, char surroundingChar)
        {
            if (origin.IsNullOrEmpty()) return origin;

            if (origin.Length > 1 &&
                origin.StartsWith(surroundingChar.ToString()) &&
                origin.EndsWith(surroundingChar.ToString()))
                return origin.Substring(1, origin.Length - 2);

            return origin;
        }

        public static string[] SplitCsvLine(this string line, string separator)
        {
            if (line.IsNullOrEmpty()) return new string[0];

            var list = new List<string>();

            var fields = line.Split(new[] { separator }, StringSplitOptions.None);

            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];

                while (field.ToCharArray().Count(c => c == '"') % 2 != 0 && i < fields.Length)
                    field += separator + fields[++i];

                list.Add(field.TrimSurroundingChars('"').Replace("\"\"", "\""));
            }

            return list.ToArray();
        }

        public static bool IsIn(this string value, params string[] p)
        {
            if (p == null || p.Length == 0) return false;

            return p.Any(s => value.ToUpper() == s.ToUpper());
        }

        public static string FillRight(this string value, int length, char fillwith)
        {
            if (length <= 0) return string.Empty;
            if (value.IsNullOrEmpty()) return new string(fillwith, length);

            return value + new string(fillwith, length - value.Length);
        }

        public static string FillLeft(this string value, int length, char fillwith)
        {
            if (length <= 0) return string.Empty;
            if (value.IsNullOrEmpty()) return new string(fillwith, length);

            return new string(fillwith, length - value.Length) + value;
        }

        public static string Toggle(this string origin, string toggle, string format = "{0}{1}")
        {
            return origin.Contains(toggle) ? origin.Replace(toggle, "") : string.Format(format, origin, toggle);
        }

        public static bool ContainsOneOf(this string value, params string[] toCheck)
        {
            if (value.IsNullOrEmpty() || toCheck == null) return false;

            return toCheck.Any(value.Contains);
        }

        public static string FormatAndJoin(this IEnumerable<string> elements, string format = "{0}", string separator = " ")
        {
            return String.Join(separator, elements.Select(e => string.Format(format, e)));
        }

        public static string FormatOrEmpty(this string format, params object[] parms)
        {
            return parms.Any(p => p == null || p.ToString() == string.Empty) ? string.Empty : string.Format(format, parms);
        }

        public static string Format(this string format, params object[] parms)
        {
            return format.IsNullOrEmpty() ? string.Empty : string.Format(format, parms);
        }
    }
}
