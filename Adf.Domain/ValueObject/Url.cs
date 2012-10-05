using System;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
    /// <summary>
    /// Class representing the _value object Url.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1036:OverrideMethodsOnComparableTypes")]
    public struct Url : IValueObject, IComparable, IComparable<Url>, IEquatable<Url>
    {
        private readonly string _value;

        #region CodeGuard(Constructors)

        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> with the supplied value.
        /// </summary>
        /// <remarks>
        /// If the value could not be validated, a <see cref="FormatException"/> exception is thrown.
        /// </remarks>
        /// <param name="newvalue">The value to use.</param>
        public Url(string newvalue)
        {
            _value = null;
            
            if (string.IsNullOrEmpty(newvalue)) return;
            
            if (!IsValidUrl(newvalue)) throw new FormatException("value is not a valid url");
            
            _value = newvalue;
        }
        
        /// <summary>
        /// Checks whether the supplied value is a valid <see cref="Url"/> or not.
        /// </summary>
        /// <param name="newValue">The value to check.</param>
        /// <returns>True if the supplied value is a valid <see cref="Url"/>, false otherwise.</returns>
        private static bool IsValidUrl(string newValue)
        {
            if (string.IsNullOrEmpty(newValue)) return false;

           try
            {
                new Uri(newValue);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        #endregion CodeGuard(Constructors)

        #region CodeGuard(Operators)

        /// <summary>
        /// Returns the equality of the two supplied <see cref="Url"/>s.
        /// </summary>
        /// <param name="i">The first <see cref="Url"/>.</param>
        /// <param name="j">The second <see cref="Url"/>.</param>
        /// <returns>
        /// <c>true</c> if both the supplied <see cref="Url"/>s are the same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(Url i, Url j)
        {
            return i.Equals(j);
        }

        /// <summary>
        /// Returns the inequality of the two supplied <see cref="Url"/>s.
        /// </summary>
        /// <param name="i">The first <see cref="Url"/>.</param>
        /// <param name="j">The second <see cref="Url"/>.</param>
        /// <returns>
        /// <c>true</c> if the supplied <see cref="Url"/>s are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(Url i, Url j)
        {
            return !i.Equals(j);
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
			if (obj == null) return false;

            if (obj is Url)
            {
                return Equals((Url) obj);              
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion CodeGuard(Operators)

        #region CodeGuard(New)

        /// <summary>
        /// Creates and returns a new empty <see cref="Url"/>.
        /// </summary>
        /// <returns>The newly created <see cref="Url"/>.</returns>
        public static Url New()
        {
            return new Url(string.Empty);
        }

        /// <summary>
        /// Creates and return a new empty <see cref="Url"/> using the specified value.
        /// </summary>
        /// <param name="value">The value to use.</param>
        /// <returns>The newly created <see cref="Url"/>.</returns>
        public static Url New(string value)
        {
            return new Url(value);
        }

        #endregion CodeGuard(New)

        #region CodeGuard(Empty)

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(_value); }
        }

        #endregion CodeGuard(Empty)

        #region CodeGuard(Value)

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="Url"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(_value) ? string.Empty : _value;
        }

        /// <summary>
        /// Returns the value of this <see cref="Url"/>.
        /// </summary>
        /// <value>The value of this <see cref="Url"/>.</value>
        public object Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets or sets an empty <see cref="Url"/>.
        /// </summary>
        public static Url Empty { get; set; }

        #endregion  CodeGuard(Value)
        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="Url"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="Url"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out Url result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = Empty;
                return true;
            }

            if (!IsValidUrl(s))
            {
                result = Empty;
                return false;
            }

            result = new Url(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out Url result)
        {
            return TryParse(s, out result);
        }

        /// <summary>
        /// Compares an <see cref="Url"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="Postcode"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="Postcode"/> is equal to the supplied object. Value greater than zero indicates 
        /// that the <see cref="Postcode"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (!(obj is Url))
            {
                throw new ArgumentException("Argument is not a URL");
            }

            return CompareTo((Url) obj);
        }

        /// <summary>
        /// Compares an <see cref="Url"/> to the supplied <see cref="Url"/>.
        /// </summary>
        /// <param name="other">The <see cref="Url"/> to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the <see cref="Url"/> 
        /// is less than the supplied <see cref="Url"/>. Value zero indicates that the <see cref="Url"/> 
        /// is equal to the supplied <see cref="Url"/>. Value greater than zero indicates that the 
        /// <see cref="Url"/> is greater than the supplied <see cref="Url"/>.</returns>
        public int CompareTo(Url other)
        {
            return _value.CompareTo(other._value);
        }

        /// <summary>
        /// Indicates whether an <see cref="Url"/> is equal to the supplied <see cref="Url"/> or not.
        /// </summary>
        /// <param name="other">The <see cref="Url"/> to compare to.</param>
        /// <returns>True if an <see cref="Url"/> is equal to the supplied <see cref="Url"/>, false
        /// otherwise.</returns>
        public bool Equals(Url other)
        {
            return (_value == other._value);
        }
    }
}
