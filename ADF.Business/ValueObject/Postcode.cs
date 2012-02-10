using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
    /// <summary>
    /// Class representing the value object Postcode.
    /// </summary>
    public struct Postcode : IValueObject, IComparable
    {
        private string value;

        //TODO: Add actual regular expression for Postcode

        /// <summary>
        /// Regular expression for validating a Dutch postal code.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")] 
        public static Regex Expression = new Regex(@"^[1-9]{1}[0-9]{3}\s{0,1}?[a-zA-Z]{2}$");

        // public static Regex Expression = new Regex(@"^[1-9]{1}[0-9]{3}$"); Belgian Postcode

        #region CodeGuard(Constructors)

        /// <summary>
        /// Initializes a new instance of the <see cref="Postcode"/> with the supplied value.
        /// </summary>
        /// <remarks>
        /// If the postal code could not be validated, a <see cref="FormatException"/> 
        /// exception is thrown.
        /// </remarks>
        /// <param name="newvalue">The supplied value to use.</param>
        public Postcode(string newvalue)
        {
            value = null;

            if (string.IsNullOrEmpty(newvalue)) return;

            if (!Expression.IsMatch(newvalue))
            {
                throw new FormatException("postcode is not in a valid dutch format");
            }

            value = newvalue;
        }

        #endregion CodeGuard(Constructors)

        #region CodeGuard(Operators)

        /// <summary>
        /// Returns the equality of the two supplied <see cref="Postcode"/>s.
        /// </summary>
        /// <param name="i">The first <see cref="Postcode"/>.</param>
        /// <param name="j">The second <see cref="Postcode"/>.</param>
        /// <returns>
        /// <c>true</c> if both the supplied postal codes are the same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(Postcode i, Postcode j)
        {
            return (i.value == j.value);
        }

        /// <summary>
        /// Indicates whether the first supplied <see cref="Postcode"/> is greater than the second 
        /// supplied <see cref="Postcode"/>.
        /// </summary>
        /// <param name="i">The first <see cref="Postcode"/>.</param>
        /// <param name="j">The second <see cref="Postcode"/>.</param>
        /// <returns>True if the first supplied <see cref="Postcode"/> is greater than the second supplied 
        /// <see cref="Postcode"/>, false otherwise.</returns>
        public static bool operator >(Postcode i, Postcode j)
        {
            return (i.value.CompareTo(j.value) > 0);
        }

        /// <summary>
        /// Indicates whether the first supplied <see cref="Postcode"/> is less than the second 
        /// supplied <see cref="Postcode"/>.
        /// </summary>
        /// <param name="i">The first <see cref="Postcode"/>.</param>
        /// <param name="j">The second <see cref="Postcode"/>.</param>
        /// <returns>True if the first supplied <see cref="Postcode"/> is less than the second supplied 
        /// <see cref="Postcode"/>, false otherwise.</returns>
        public static bool operator <(Postcode i, Postcode j)
        {
            return (i.value.CompareTo(j.value) < 0);
        }

        /// <summary>
        /// Returns the inequality of the two supplied <see cref="Postcode"/>s.
        /// </summary>
        /// <param name="i">The first <see cref="Postcode"/>.</param>
        /// <param name="j">The second <see cref="Postcode"/>.</param>
        /// <returns>
        /// <c>true</c> if the supplied <see cref="Postcode"/>s are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(Postcode i, Postcode j)
        {
            return (i.value != j.value);
        }

        /// <summary>
        /// Checkes whether this instance is equal to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare against.</param>
        /// <returns>
        /// <c>true</c> if this instance is equal to the supplied object; otherwise <c>false</c>
        /// </returns>
        public override bool Equals(object obj)
        {
            // Make sure the cast that follows won't fail
            if (obj == null || obj.GetType() != GetType())
                return false;

            return (this == (Postcode) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        #endregion CodeGuard(Operators)

        #region CodeGuard(New)

        /// <summary>
        /// Creates and returns a new empty <see cref="Postcode"/> object.
        /// </summary>
        /// <returns>The newly created <see cref="Postcode"/>.</returns>
        public static Postcode New()
        {
            return new Postcode(string.Empty);
        }

        /// <summary>
        /// Creates and returns a new empty <see cref="Postcode"/> object using the specified value.
        /// </summary>
        /// <param name="value">The supplied value.</param>
        /// <returns>The newly created <see cref="Postcode"/>.</returns>
        public static Postcode New(string value)
        {
            return new Postcode(value);
        }

        #endregion CodeGuard(New)

        #region CodeGuard(Empty)
        /// <summary>
        /// The empty <see cref="Postcode"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")] public static Postcode Empty = new Postcode(string.Empty);

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(value); }
        }

        #endregion CodeGuard(Empty)

        #region CodeGuard(Value)

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="Postcode"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// Returns the postal code.
        /// </summary>
        /// <value>The postal code.</value>
        public object Value
        {
            get { return value; }
        }

        #endregion  CodeGuard(Value)
        /// <summary>
        /// Compares an <see cref="Postcode"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="Postcode"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="Postcode"/> is equal to the supplied object. Value greater than zero indicates 
        /// that the <see cref="Postcode"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            Postcode other;
            try
            {
                other = (Postcode) obj;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("obj is not a Postcode");
            }

            if (this < other)
            {
                return -1;
            }
            if (this > other)
            {
                return 1;
            }
            return 0;
        }

        #region static TryParse
        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="Postcode"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="Postcode"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out Postcode result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = Empty;
                return true;
            }

            if (!Expression.IsMatch(s))
            {
                result = Empty;
                return false;
            }

            result = new Postcode(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out Postcode result)
        {
            return TryParse(s, out result);
        }

        #endregion
    }
}