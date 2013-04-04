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
        /// Returns a value indicating whether the length of the specified string is equal to the specified length.
        /// </summary>
        /// <param name="value">The string, the length of which is to be checked.</param>
        /// <param name="length">the length to check against.</param>
        /// <returns>
        /// true if length of the string is equal to the specified length;
        /// otherwise, false.
        /// </returns>
        public static bool HasExactLength(this string value, int length)
        {
            if (value == null) return false;

            return (value.Length == length);
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
            
            return values.Aggregate(string.Empty, (current, word) => current + (word.Substring(0, 1).ToUpper() + word.Substring(1) + " ")).TrimEnd();
        }    
	
		public static string Left(this string origin, int length)
        {
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

            var returnCols = new List<string>();
            var columns = line.Split(new [] { separator }, StringSplitOptions.None);

            string currentColumn = string.Empty;

            foreach (string col in columns)
            {
                if (currentColumn.IsNullOrEmpty())
                {
                    if (col.StartsWith("\"") && !col.EndsWith("\""))
                        currentColumn = col;
                    else
                        returnCols.Add(col.TrimSurroundingChars('"'));
                }
                else
                {
                    currentColumn += separator + col;

                    if (currentColumn.EndsWith("\""))
                    {
                        returnCols.Add(currentColumn.TrimSurroundingChars('"'));
                        currentColumn = string.Empty;
                    }
                }
            }

            if (!currentColumn.IsNullOrEmpty())
                returnCols.Add(currentColumn.TrimSurroundingChars('"'));

            return returnCols.ToArray();
        }

        public static bool IsIn(this string value, StringComparison comparison, params string[] values)
        {
            return !value.IsNullOrEmpty() && values.Any(val => value.Equals(val, comparison));
        }

        public static bool IsIn(this string value, params string[] values)
        {
            return value.IsIn(StringComparison.OrdinalIgnoreCase, values);
        }
	}
}
