using System;
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
            if (string.IsNullOrEmpty(longWord))
                return string.Empty;

            const int maxWordWidh = 50;

            //return if total length of string is smaller as maxWidth of the words. 
            //Extra manipulation is not needed --> performance
            if (longWord.Length <= maxWordWidh)
                return longWord;

            //Check for words longer than maxWordWidh characters
            StringBuilder myWords = new StringBuilder();
            foreach (string word in longWord.Split(' '))
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

            StringBuilder newString = new StringBuilder();

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

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
