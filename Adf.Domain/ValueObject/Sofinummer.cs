using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
    /// <summary>
    /// Structure representing the value object SofiNumber (Social-fiscal number).
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
    [Masked(@"00\.00\.00\.000")]
    public struct Sofinummer : IValueObject, IComparable
    {
        private string value;

        ///// <summary>
        ///// Regular expression for validating a Dutch social-fiscal number.
        ///// </summary>
        ////  private static Regex Expression = new Regex(@"^[0-9]{2}[.][0-9]{2}[.][0-9]{2}[.][0-9]{3}");

        #region CodeGuard(Constructors)

        /// <summary>
        /// Initializes a new instance of the <see cref="Sofinummer"/> with the supplied value.
        /// </summary>
        /// <remarks>
        /// If the social-fiscal number could not be validated, a <see cref="FormatException"/> 
        /// exception is thrown.
        /// </remarks>
        /// <param name="newvalue">The value to use.</param>
        public Sofinummer(string newvalue)
        {
            value = null;

            if (string.IsNullOrEmpty(newvalue)) return;

            if (!IsValidSofinummer(newvalue)) throw new FormatException("value is not a valid dutch social security number");

            value = newvalue;
        }

        /// <summary>
        /// Checks whether the supplied value is a valid <see cref="Sofinummer"/> or not.
        /// </summary>
        /// <param name="newvalue">The value to check.</param>
        /// <returns>True if the supplied value is a valid <see cref="Sofinummer"/>, false otherwise.</returns>
        private static bool IsValidSofinummer(string newvalue)
        {
            return /*Expression.IsMatch(newvalue) &&*/ ElfProefSofi(newvalue);
        }

        #endregion CodeGuard(Constructors)

        #region CodeGuard(Operators)

      

        /// <summary>
        /// Returns the equality of the two supplied <see cref="Sofinummer"/>s.
        /// </summary>
        /// <param name="i">The first <see cref="Sofinummer"/>.</param>
        /// <param name="j">The second <see cref="Sofinummer"/>.</param>
        /// <returns>
        /// <c>true</c> if both the supplied social-fiscal numbers are the same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(Sofinummer i, Sofinummer j)
        {
            return (i.value == j.value);
        }

        /// <summary>
        /// Returns the inequality of the two supplied <see cref="Sofinummer"/>s.
        /// </summary>
        /// <param name="i">The first <see cref="Sofinummer"/>.</param>
        /// <param name="j">The second <see cref="Sofinummer"/>.</param>
        /// <returns>
        /// <c>true</c> if the two supplied social-fiscal numbers are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(Sofinummer i, Sofinummer j)
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

            return (this == (Sofinummer) obj);
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
        /// Creates a new empty <see cref="Sofinummer"/> object.
        /// </summary>
        /// <returns>The new <see cref="Sofinummer"/>.</returns>
        public static Sofinummer New()
        {
            return new Sofinummer(string.Empty);
        }

        /// <summary>
        /// Creates and returns a new empty <see cref="Sofinummer"/> object using the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The newly created <see cref="Sofinummer"/>.</returns>
        public static Sofinummer New(string value)
        {
            return new Sofinummer(value);
        }

        #endregion CodeGuard(New)

        #region CodeGuard(Empty)
        /// <summary>
        /// The empty <see cref="Sofinummer"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")] public static Sofinummer Empty = new Sofinummer(string.Empty);

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
        /// Returns the social-fiscal number.
        /// </summary>
        /// <value>The social-fiscal number.</value>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Indicates whether the supplied value is a valid <see cref="Sofinummer"/> or not.
        /// </summary>
        /// <param name="sofiNr">The supplied value.</param>
        /// <returns>True if the supplied value is a valid <see cref="Sofinummer"/>, false otherwise.</returns>
        private static bool ElfProefSofi(string sofiNr)
        {
            bool result = false;
            long som = 0;
            string cleanSofiNr = sofiNr.Replace(".", "");


            // Een Sofinummer bestaat uit 9 cijfers 
            if ((cleanSofiNr.Length == 9))
            {
                for (int i = 1; (i <= 9); i++)
                {
                    int cijfer = int.Parse(cleanSofiNr.Substring((i - 1), 1), CultureInfo.CurrentCulture);
                    if (i == 9)
                    {
                        som += (cijfer*-(i + 1));
                    }
                    else
                    {
                        som += (cijfer*(i + 1));
                    }
                }
                // De som van de vermenigvuldigingen moet deelbaar zijn door 11 
                result = (((som%11) == 0));
            }
            return result;
        }

        #endregion  CodeGuard(Value)
        /// <summary>
        /// Compares an <see cref="Sofinummer"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="Sofinummer"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="Sofinummer"/> is equal to the supplied object. Value greater than zero indicates 
        /// that the <see cref="Sofinummer"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            if (obj is Sofinummer)
            {
                Sofinummer other = (Sofinummer)obj;
                return value.CompareTo(other.value);
            }

            throw new ArgumentException("obj is not a Sofinummer");
        }

        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="Sofinummer"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="Sofinummer"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out Sofinummer result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = Empty;
                return true;
            }

            if (!IsValidSofinummer(s))
            {
                result = Empty;
                return false;
            }

            result = new Sofinummer(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out Sofinummer result)
        {
            return TryParse(s, out result);
        }
    }
}