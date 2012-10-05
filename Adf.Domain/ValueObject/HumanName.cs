using System;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject 
{
    /// <summary>
    /// Structure representing the value object HumanName.
    /// </summary>
    public struct HumanName: IValueObject, IEquatable<HumanName>, IComparable, IComparable<HumanName>
    {
        private string value;

        /// <summary>
        /// Returns the value of the <see cref="HumanName"/>.
        /// </summary>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Returns the value of the <see cref="HumanName"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return value;
        }
        
        #region CodeGuard(Expression)
        
        private static Regex Expression = new Regex(@"");

        #endregion CodeGuard(Expression)
        /// <summary>
        /// Initializes an instance of <see cref="HumanName"/> with the supplied string.
        /// </summary>
        /// <param name="s">The supplied string.</param>
        public HumanName(string s)
        {
            //initialize
            value = null;

            if (string.IsNullOrEmpty(s)) return;

            if (IsValidHumanName(s))
            {
                throw new FormatException(String.Format("Value {0} is not a valid human name", s));
            }

            value = s;            
        }

        #region Validity

        #region CodeGuard(IsValid)
        
        /// <summary>
        /// Checks whether a value is a valid human name.
        /// </summary>
        /// <param name="s">The name to try.</param>
        /// <returns>True if the supplied value is a valid human name.</returns>
        public static bool IsValidHumanName(string s)
        {
            return !Expression.IsMatch(s);
        }

        #endregion CodeGuard(IsValid)

        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="HumanName"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The resulting <see cref="HumanName"/>.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out HumanName result)
        {
            result = Empty;

            if (string.IsNullOrEmpty(s))
            {
                return true;
            }

            if (!IsValidHumanName(s))
            {
                return false;
            }

            result = new HumanName(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out HumanName result)
        {
            return TryParse(s, out result);
        }

        #endregion

        #region Empty
        /// <summary>
        /// Returns a <see cref="HumanName"/> with an empty value.
        /// </summary>
        public static HumanName Empty
        {
            get { return new HumanName(string.Empty); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public bool IsEmpty
        {
            get { return string.IsNullOrEmpty(value); }
        }

        #endregion Empty

        #region Equality

        /// <summary>
        /// Identifies if the internal value of the parameter is equal to this object's internal value.
        /// </summary>
        /// <param name="other">The supplied <see cref="HumanName"/>.</param>
        /// <returns>True if this <see cref="HumanName"/> is equal to the supplied 
        /// <see cref="HumanName"/>.</returns>
        public bool Equals(HumanName other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Compares this instance to the supplied object.
        /// </summary>
        /// <param name="obj">The supplied object to compare against.</param>
        /// <returns>
        /// <c>true</c> if both the <see cref="HumanName"/>s are same; otherwise <c>false</c>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is HumanName)
                return Equals((HumanName) obj);

            return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Returns the equality of the two supplied <see cref="HumanName"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="HumanName"/>.</param>
        /// <param name="j">The second <see cref="HumanName"/>.</param>
        /// <returns>
        /// <c>true</c> if both the <see cref="HumanName"/> are same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(HumanName i, HumanName j)
        {
            return i.Equals(j);
        }
        
        /// <summary>
        /// Returns the inequality of two <see cref="HumanName"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="HumanName"/>.</param>
        /// <param name="j">The second <see cref="HumanName"/>.</param>
        /// <returns>
        /// <c>true</c> if the Urls are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(HumanName i, HumanName j)
        {
            return !i.Equals(j);
        }

        #endregion Equality

        #region Comparison

        ///<summary>
        ///Compares the current instance with the supplied object.
        ///</summary>
        ///
        ///<returns>
        ///A 32-bit signed integer. Value less than zero indicates that the <see cref="HumanName"/> is 
        ///less than the supplied object. Value zero indicates that the <see cref="HumanName"/> is equal 
        ///to the supplied object. Value greater than zero indicates that the <see cref="HumanName"/> 
        ///is greater than the supplied object.
        ///</returns>
        ///
        ///<param name="obj">An object to compare with this instance.</param>
        ///<exception cref="T:System.ArgumentException">obj is not the same type as this instance.</exception><filterpriority>2</filterpriority>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (!(obj is HumanName)) 
                throw new ArgumentException("Object is not a human name");

            HumanName other = (HumanName) obj;

            return value.CompareTo(other.value);
        }

        ///<summary>
        ///Compares the current object with the supplied <see cref="HumanName"/>.
        ///</summary>
        ///
        ///<returns>
        ///A 32-bit signed integer. Value less than zero indicates that the <see cref="HumanName"/> is 
        ///less than the supplied <see cref="HumanName"/>. Value zero indicates that the 
        ///<see cref="HumanName"/> is equal to the supplied <see cref="HumanName"/>. Value greater than 
        ///zero indicates that the <see cref="HumanName"/> is greater than the supplied 
        ///<see cref="HumanName"/>.
        ///</returns>
        ///
        ///<param name="other">An <see cref="HumanName"/> to compare with this object.</param>
        public int CompareTo(HumanName other)
        {
            return value.CompareTo(other.value);
        }

        

        #endregion

        #region CodeGuard(Custom)

        #endregion CodeGuard(Custom)
    }
}
