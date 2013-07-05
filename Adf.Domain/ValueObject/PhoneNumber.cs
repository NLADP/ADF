using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
    /// <summary>
    /// Class representing the value object PhoneNumber.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
    public struct PhoneNumber : IValueObject, IComparable
    {
        private string value;

        /// <summary>
        /// Regular expression for validating a Dutch phone number.
        /// -Can be either a 06 (mobile) number, a 3-digit netnummer or a 4 digit netnummer
        /// -Total of 10 digits
        /// -Separators allowed: dash("-"), space (" ") or nothing ("")
        /// -Netnummer has to start with 0
        /// -Abonneenummer cannot start with 0
        /// -0800 or 0900 numbers are not allowed
        /// -International numbers are not allowed
        /// </summary>
        private static Regex Expression = new Regex(@"((^06((\s{0,1})|(\-{0,1}))[1-9]{1}[0-9]{7}$)|(^[0][1-9]{1}[0-9]{1}(\s{0,1}|\-{0,1})[1-9]{1}[0-9]{6}$)|(^[0][1-9]{1}[0-9]{2}(\s{0,1}|\-{0,1})[1-9]{1}[0-9]{5}$))");

        #region CodeGuard(Constructors)

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumber"/> with the supplied value.
        /// </summary>
        /// <remarks>
        /// If the phone number could not be validated, a <see cref="FormatException"/> 
        /// exception is thrown.
        /// </remarks>
        /// <param name="newvalue">The value to use.</param>
        public PhoneNumber(string newvalue)
        {
            value = newvalue;

            if (string.IsNullOrEmpty(newvalue)) return;
            
            if (!Expression.IsMatch(value)) throw new FormatException("value is not a valid phonenumber");

            value = newvalue;
        }

        #endregion CodeGuard(Constructors)

        #region CodeGuard(Operators)

       /// <summary>
        /// Returns the equality of the two supplied <see cref="PhoneNumber"/>s.
        /// </summary>
        /// <param name="i">The first <see cref="PhoneNumber"/>.</param>
        /// <param name="j">The second <see cref="PhoneNumber"/>.</param>
        /// <returns>
        /// <c>true</c> if both phone numbers are the same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(PhoneNumber i, PhoneNumber j)
        {
            return (i.value == j.value);
        }

        /// <summary>
        /// Returns the inequality of the two supplied <see cref="PhoneNumber"/>s.
        /// </summary>
        /// <param name="i">The first <see cref="PhoneNumber"/>.</param>
        /// <param name="j">The second <see cref="PhoneNumber"/>.</param>
        /// <returns>
        /// <c>true</c> if the phone numbers are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(PhoneNumber i, PhoneNumber j)
        {
            return (i.value != j.value);
        }

        /// <summary>
        /// Checks whether this instance is equal to the supplied object.
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

            return (this == (PhoneNumber) obj);
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
        /// Creates a new empty <see cref="PhoneNumber"/> object.
        /// </summary>
        /// <returns>The new <see cref="PhoneNumber"/>.</returns>
        public static PhoneNumber New()
        {
            return new PhoneNumber(string.Empty);
        }

        /// <summary>
        /// Creates and returns a new empty <see cref="PhoneNumber"/> object using the specified value.
        /// </summary>
        /// <param name="value">The supplied value.</param>
        /// <returns>The newly created <see cref="PhoneNumber"/>.</returns>
        public static PhoneNumber New(string value)
        {
            return new PhoneNumber(value);
        }

        #endregion CodeGuard(New)

        #region CodeGuard(Empty)

        private static PhoneNumber empty = new PhoneNumber(string.Empty);
        /// <summary>
        /// Gets or sets the empty <see cref="PhoneNumber"/>.
        /// </summary>
        public static PhoneNumber Empty
        {
            get { return empty; }
            set { empty = value; }
        }

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
        /// A <see cref="string"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// Returns the phone number.
        /// </summary>
        /// <value>The phone number.</value>
        public object Value
        {
            get { return value; }
        }

        #endregion  CodeGuard(Value)
        /// <summary>
        /// Compares an <see cref="PhoneNumber"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="PhoneNumber"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="PhoneNumber"/> is equal to the supplied object. Value greater than zero indicates 
        /// that the <see cref="PhoneNumber"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            if (obj is PhoneNumber)
            {
                PhoneNumber other = (PhoneNumber) obj;
                return value.CompareTo(other.value);
            }

            throw new ArgumentException("obj is not a PhoneNumber");
        }

        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="PhoneNumber"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="PhoneNumber"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out PhoneNumber result)
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

            result = new PhoneNumber(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out PhoneNumber result)
        {
            return TryParse(s, out result);
        }
    }
}