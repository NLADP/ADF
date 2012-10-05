using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
    /// <summary>
    /// Structure representing the value object AccountNumber (Dutch bank bankrekeningnummer).
    /// Must be 9 or 10 digits.
    /// N.B. Does not allow Dutch 'girorekening'!
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
    public struct AccountNumber : IValueObject, IComparable
    {
        private readonly string value;

        private static readonly Regex Expression = new Regex(@"(^[0-9]{9})|(^[0-9]{10})");

        #region CodeGuard(Constructors)

        /// <summary>
        /// Initializes a new instance of <see cref="AccountNumber"/> with the supplied value.
        /// </summary>
        /// <remarks>
        /// If the supplied value could not be validated, an exception is thrown.
        /// </remarks>
        /// <param name="newvalue">The bank account number to use.</param>
        public AccountNumber(string newvalue)
        {
            value = string.Empty;

            if (string.IsNullOrEmpty(newvalue)) return;

            if (!IsValidAccountNumber(newvalue)) throw new FormatException("value is not a valid account number");

            value = newvalue;
        }

        /// <summary>
        /// Checks whether the supplied value is a valid <see cref="AccountNumber"/> or not.
        /// </summary>
        /// <param name="newvalue">The value to check.</param>
        /// <returns>True if the supplied value is a valid <see cref="AccountNumber"/>, false 
        /// otherwise.</returns>
        private static bool IsValidAccountNumber(string newvalue)
        {
            if (string.IsNullOrEmpty(newvalue)) return true;
            return Expression.IsMatch(newvalue) && ElfProef(newvalue);
            
        }

        #endregion CodeGuard(Constructors)

        #region CodeGuard(Operators)

        /// <summary>
        /// Returns the equality of two <see cref="AccountNumber"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="AccountNumber"/>.</param>
        /// <param name="j">The second <see cref="AccountNumber"/>.</param>
        /// <returns>
        /// <c>true</c> if both social-fiscal numbers are the same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(AccountNumber i, AccountNumber j)
        {
            return (i.value == j.value);
        }

        /// <summary>
        /// Returns the inequality of two <see cref="AccountNumber"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="AccountNumber"/>.</param>
        /// <param name="j">The second <see cref="AccountNumber"/>.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="AccountNumber"/>s are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(AccountNumber i, AccountNumber j)
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
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override bool Equals(object obj)
        {
            try
            {
                return (this == (AccountNumber) obj);
            }
            catch (Exception)
            {
                return false;
            }
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
        /// Creates a new empty <see cref="AccountNumber"/> object.
        /// </summary>
        /// <returns>The new <see cref="AccountNumber"/>.</returns>
        public static AccountNumber New()
        {
            return new AccountNumber(string.Empty);
        }

        /// <summary>
        /// Creates a new <see cref="AccountNumber"/> object using the specified value.
        /// </summary>
        /// <param name="value">The supplied value.</param>
        /// <returns>The new <see cref="AccountNumber"/>.</returns>
        public static AccountNumber New(string value)
        {
            return new AccountNumber(value);
        }

        #endregion CodeGuard(New)

        #region CodeGuard(Empty)

        private static AccountNumber empty = new AccountNumber(string.Empty);


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
        /// A <see cref="T:System.String"></see> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <summary>
        /// Returns the AccountNumber value.
        /// </summary>
        /// <value>The AccountNumber value.</value>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Gets or sets the empty <see cref="AccountNumber"/>.
        /// </summary>
        public static AccountNumber Empty
        {
            get { return empty; }
            set { empty = value; }
        }

        #endregion  CodeGuard(Value)

        #region CodeGuard(Custom)

        /// <summary>
        /// Indicates whether the supplied value is a valid <see cref="AccountNumber"/> or not.
        /// </summary>
        /// <param name="rekeningNr">The supplied value.</param>
        /// <returns>True if the supplied value is a valid <see cref="AccountNumber"/>, false 
        /// otherwise.</returns>
        private static bool ElfProef(string rekeningNr)
        {
            bool result = false;
            long som = 0;
            var cleanrekeningNr = rekeningNr.Replace(".", "");

            // Een bankrekeningnummer bestaat uit 9 of 10 cijfers 
            if ((cleanrekeningNr.Length == 9) || (cleanrekeningNr.Length == 10))
            {
                for (int i = 1; (i <= cleanrekeningNr.Length); i++)
                {
                    int cijfer = int.Parse(cleanrekeningNr.Substring((i - 1), 1), CultureInfo.CurrentCulture);
                    som += (cijfer*(i + 1));
                }
                // De som van de vermenigvuldigingen moet deelbaar zijn door 11 
                result = (((som%11) == 0));
            }
            return result;
        }

        #endregion  CodeGuard(Custom)

        /// <summary>
        /// Compares an <see cref="AccountNumber"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="AccountNumber"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="AccountNumber"/> is equal to the supplied object. Value greater than zero 
        /// indicates that the <see cref="AccountNumber"/> is greater than the supplied object.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.ArgumentException.#ctor(System.String)")]
        public int CompareTo(object obj)
        {
            if (!(obj is AccountNumber)) throw new ArgumentException("obj is not an AccountNumber");

            var other = (AccountNumber) obj;

            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="AccountNumber"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="AccountNumber"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out AccountNumber result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = Empty;
                return true;
            }

            if (!IsValidAccountNumber(s))
            {
                result = Empty;
                return false;
            }

            result = new AccountNumber(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out AccountNumber result)
        {
            return TryParse(s, out result);
        }
    }
}