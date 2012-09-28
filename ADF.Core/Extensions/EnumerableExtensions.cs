using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Get<T>(this object[] list, int index)
        {
            if (list.Count() < index)
            {
                return (T) list[index];
            }

            throw new ArgumentOutOfRangeException("index");
        }

        public static int IndexOf(this IEnumerable<IDomainObject> objects, ID id)
        {
            if (objects == null) return -1;

            var i = 0;

            foreach (var o in objects)
            {
                if (o.Id == id) return i;

                i++;
            }

            return -1;
        }

        /// <summary>
        /// Converts a specified list to a string using the specified separator.
        /// </summary>
        /// <param name="list">The list to convert.</param>
        /// <param name="separator">The separator used for the conversion.</param>
        /// <returns>
        /// The generated string after the conversion.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// list is null.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// separator is null.
        /// </exception>
        public static string ConvertToString(this IEnumerable list, string separator)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in list)
            {
                //add seperator if we are not appending the first element
                if (sb.Length > 0) sb.Append(separator);

                sb.Append(item);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Converts a specified list of a particular type of elements to a string using the 
        /// specified separator.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to convert.</param>
        /// <param name="separator">The separator used for the conversion.</param>
        /// <returns>
        /// The generated string after the conversion.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// list is null.
        /// </exception>
        public static string ConvertToString<T>(this IEnumerable<T> list, string separator)
        {
            var sb = new StringBuilder();

            foreach (var item in list)
            {
                //add seperator if we are not appending the first element
                if (sb.Length > 0) sb.Append(separator);

                sb.Append(item);
            }
            return sb.ToString();
        }

        public static int Count(this IEnumerable enumerable)
        {
            int result = 0;

            IEnumerator enumerator = enumerable.GetEnumerator();
            
            while (enumerator.MoveNext()) result++;
            
            return result;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable != null) && (enumerable.Any());
        }
    }
}
