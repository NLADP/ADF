//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Model           : Soho
//     Template        : Value Object.tpl
//     Runtime Version : $Version$
//     Generation date : 22-3-2007 15:59:58
//
//     Changes to this file may cause incorrect behavior and may be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject 
{
    /// <summary>
    /// Structure representing the value object Isbn.
    /// </summary>
    public struct Isbn: IValueObject, IEquatable<Isbn>, IComparable, IComparable<Isbn>
    {
        private string value;

        /// <summary>
        /// Returns the value of the <see cref="Isbn"/>.
        /// </summary>
        public object Value
        {
            get { return value; }
        }

        /// <summary>
        /// Returns the value of the <see cref="Isbn"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return value;
        }
        
        #region CodeGuard(Expression)

        /// <summary>
        /// Regular expression for validating a Isbn
        /// ISBN-10 match
        /// Matches:        0 93028 923 4
        ///                 1-56389-668-0
        ///                 1-56389-016-X
        /// Non-Matches:    123456789
        /// 
        /// More information att http://www.isbn.org/standards/home/isbn/international/html/usm4.htm
        /// </summary>
        private static Regex Expression = new Regex(@"^(?=.{13}$)\d{1,5}([- ])\d{1,7}\1\d{1,6}\1(\d|X)$");

        #endregion CodeGuard(Expression)
        /// <summary>
        /// Initializes an instance of <see cref="Isbn"/> with the supplied value.
        /// </summary>
        /// <param name="s">The value.</param>
        public Isbn(string s)
        {
            //initialize
            value = null;

            if (string.IsNullOrEmpty(s)) return;

            if (!IsValidIsbn(s))
            {
                throw new FormatException(String.Format("Value {0} is not a valid ", s));
            }

            value = s;            
        }

        #region Validity

        #region CodeGuard(IsValid)
        
        /// <summary>
        /// Checks whether the supplied value is a valid <see cref="Isbn"/>.
        /// </summary>
        /// <param name="s">The supplied value to try.</param>
        /// <returns>true if the supplied value is a valid <see cref="Isbn"/>, false otherwise.</returns>
        public static bool IsValidIsbn(string s)
        {
            return Expression.IsMatch(s);
        }

        #endregion CodeGuard(IsValid)

        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="Isbn"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The resulting <see cref="Isbn"/>.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out Isbn result)
        {
            result = Empty;

            if (string.IsNullOrEmpty(s))
            {
                return true;
            }

            if (!IsValidIsbn(s))
            {
                return false;
            }

            result = new Isbn(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out Isbn result)
        {
            return TryParse(s, out result);
        }

        #endregion

        #region Empty
        /// <summary>
        /// Returns an empty <see cref="Isbn"/>.
        /// </summary>
        public static Isbn Empty
        {
            get { return new Isbn(string.Empty); }
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
        /// <param name="other">The supplied <see cref="Isbn"/>.</param>
        /// <returns>True if this <see cref="Isbn"/> is equal to the supplied <see cref="Isbn"/>.</returns>
        public bool Equals(Isbn other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Compares this instance to the supplied object.
        /// </summary>
        /// <param name="obj">The supplied object to compare against.</param>
        /// <returns>
        /// <c>true</c> if both the <see cref="Isbn"/>s are the same; otherwise <c>false</c>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Isbn)
                return Equals((Isbn) obj);

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
        /// Returns the equality of two <see cref="Isbn"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="Isbn"/>.</param>
        /// <param name="j">The second <see cref="Isbn"/>.</param>
        /// <returns>
        /// <c>true</c> if both the <see cref="Isbn"/>s are the same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(Isbn i, Isbn j)
        {
            return i.Equals(j);
        }
        
        /// <summary>
        /// Returns the inequality of two <see cref="Isbn"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="Isbn"/>.</param>
        /// <param name="j">The second <see cref="Isbn"/>.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="Isbn"/>s are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(Isbn i, Isbn j)
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
        ///A 32-bit signed integer. Value less than zero indicates that the <see cref="Isbn"/> is 
        ///less than the supplied object. Value zero indicates that the <see cref="Isbn"/> is equal 
        ///to the supplied object. Value greater than zero indicates that the <see cref="Isbn"/> 
        ///is greater than the supplied object.
        ///</returns>
        ///
        ///<param name="obj">An object to compare with this instance.</param>
        ///<exception cref="T:System.ArgumentException">obj is not of the same type as this instance.</exception><filterpriority>2</filterpriority>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (!(obj is Isbn)) 
                throw new ArgumentException("Object is not a ");

            Isbn other = (Isbn) obj;

            return value.CompareTo(other.value);
        }

        ///<summary>
        ///Compares the current object with the supplied <see cref="Isbn"/>.
        ///</summary>
        ///
        ///<returns>
        ///A 32-bit signed integer. Value less than zero indicates that the <see cref="Isbn"/> is 
        ///less than the supplied <see cref="Isbn"/>. Value zero indicates that the 
        ///<see cref="Isbn"/> is equal to the supplied <see cref="Isbn"/>. Value greater than 
        ///zero indicates that the <see cref="Isbn"/> is greater than the supplied 
        ///<see cref="Isbn"/>.
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        public int CompareTo(Isbn other)
        {
            return value.CompareTo(other.value);
        }

        #endregion

        #region CodeGuard(Custom)

        #endregion CodeGuard(Custom)
    }
}
