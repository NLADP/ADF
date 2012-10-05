using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
    /// <summary>
    /// Structure representing the value object Bsn (Burger Service nummer).
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
    public struct Bsn : IValueObject, IComparable
    {
        private readonly string value;

        ///// <summary>
        ///// Regular expression for validating a Dutch <see cref="Bsn"/>.
        ///// </summary>
        ////  private static Regex Expression = new Regex(@"^[0-9]{2}[.][0-9]{2}[.][0-9]{2}[.][0-9]{3}");

        #region CodeGuard(Constructors)

        /// <summary>
        /// Initializes a new instance of the <see cref="Bsn"/> class.
        /// </summary>
        /// <remarks>
        /// If the <see cref="Bsn"/> could not be validated, a <see cref="FormatException"/> 
        /// exception is thrown.
        /// </remarks>
        /// <param name="newvalue">The <see cref="Bsn"/> to use.</param>
        public Bsn(string newvalue)
        {
            value = null;

            if (string.IsNullOrEmpty(newvalue)) return;
            if (newvalue.Length < 8 || newvalue.Length > 9) throw new FormatException("value is not a valid dutch social security number");

            // TODO: equals "000000000" hack aanpassen/verwijderen
            int val;
            if (int.TryParse(newvalue, out val) && val == 0) return;

            if (!IsValidBsn(newvalue)) throw new FormatException("value is not a valid dutch social security number");

            value = newvalue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bsn"/> class.
        /// </summary>
        /// <remarks>
        /// If the <see cref="Bsn"/> could not be validated, a <see cref="FormatException"/> 
        /// exception is thrown.
        /// </remarks>
        /// <param name="newvalue">The <see cref="Bsn"/> to use.</param>
        public Bsn(uint newvalue)
            : this(newvalue == 0 ? string.Empty : newvalue.ToString())
        {
        }

        /// <summary>
        /// Checks whether the supplied value is a valid <see cref="Bsn"/> or not.
        /// </summary>
        /// <param name="newvalue">The value to check.</param>
        /// <returns>True if the supplied value is a valid <see cref="Bsn"/>, false otherwise.</returns>
        private static bool IsValidBsn(string newvalue)
        {
            return /*Expression.IsMatch(newvalue) &&*/ ElfProef(newvalue);
        }

        #endregion CodeGuard(Constructors)

        #region CodeGuard(Operators)

        /// <summary>
        /// Returns the equality of two <see cref="Bsn"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="Bsn"/>.</param>
        /// <param name="j">The second <see cref="Bsn"/>.</param>
        /// <returns>
        /// <c>true</c> if both <see cref="Bsn"/>s are the same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(Bsn i, Bsn j)
        {
            return (i.value == j.value);
        }

        /// <summary>
        /// Returns the inequality of two <see cref="Bsn"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="Bsn"/>.</param>
        /// <param name="j">The second <see cref="Bsn"/>.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="Bsn"/>s are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(Bsn i, Bsn j)
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

            return (this == (Bsn) obj);
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
        /// Creates and returns a new empty <see cref="Bsn"/>.
        /// </summary>
        /// <returns>The newly created <see cref="Bsn"/>.</returns>
        public static Bsn New()
        {
            return new Bsn(string.Empty);
        }

        /// <summary>
        /// Creates a new empty <see cref="Bsn"/> object using the specified value.
        /// </summary>
        /// <param name="value">The specified value.</param>
        /// <returns>The new <see cref="Bsn"/>.</returns>
        public static Bsn New(string value)
        {
            return new Bsn(value);
        }

        #endregion CodeGuard(New)

        #region CodeGuard(Empty)
        /// <summary>
        /// The empty <see cref="Bsn"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static Bsn Empty = new Bsn(string.Empty);

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
        /// A <see cref="string"></see> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(value) ? string.Empty : Regex.Replace(value, @"(\d{4})(\d{2})(\d{3})", "$1.$2.$3");
        }

        /// <summary>
        /// Gets the value of this instance.
        /// </summary>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Indicates whether the supplied value is a valid <see cref="Bsn"/> or not.
        /// </summary>
        /// <param name="bsnNr">The supplied value.</param>
        /// <returns>True if the supplied value is a valid <see cref="Bsn"/>, false otherwise.</returns>
        private static bool ElfProef(string bsnNr)
        {
            bsnNr = bsnNr.Trim().Replace(".", string.Empty);

            long som = 0;

            // Is het een numeric?
            long nr;
            if (long.TryParse(bsnNr, out nr) && nr != 0)
            {
                // Een Bsn bestaat uit 9 cijfers 
                if (bsnNr.Length != 9) bsnNr = bsnNr.PadLeft(9, '0');

                for (int i = 1; i <= 8; i++)
                {
                    som += (bsnNr[i - 1] - '0') * (10 - i);
                }
                som += (bsnNr[8] - '0') * -1;
            }

            // De som van de vermenigvuldigingen moet deelbaar zijn door 11 en mag niet 0 zijn
            return som != 0 && som % 11 == 0;
        }

        #endregion  CodeGuard(Value)

        /// <summary>
        /// Compares an <see cref="Bsn"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="Bsn"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="Bsn"/> is equal to the supplied object. Value greater than zero indicates that 
        /// the <see cref="Bsn"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            if (obj is Bsn)
            {
                Bsn other = (Bsn)obj;
                return value.CompareTo(other.value);
            }

            throw new ArgumentException("obj is not a Bsn");
        }

        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="Bsn"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="Bsn"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out Bsn result)
        {
            string cleanBsnNr = s.Trim().Replace(".", string.Empty);
            result = Empty;

            int val;
            if (string.IsNullOrEmpty(cleanBsnNr))
            {
                return true;
            }

            if (cleanBsnNr.Length < 8 || cleanBsnNr.Length > 9 || !int.TryParse(cleanBsnNr, out val) || val == 0 || !IsValidBsn(cleanBsnNr))
            {
                return false;
            }

            result = new Bsn(cleanBsnNr);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out Bsn result)
        {
            return TryParse(s, out result);
        }
    }
}
