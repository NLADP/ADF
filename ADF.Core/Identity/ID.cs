﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Model           : Synopsis
//     Template        : Value Object.tpl
//     Runtime Version : $Version$
//     Generation date : 8-12-2006 11:05:41
//
//     Changes to this file may cause incorrect behavior and may be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Adf.Core.Domain;

namespace Adf.Core.Identity
{
    /// <summary>
    /// Represents the value object ID.
    /// Provides methods to create a new ID, check the eqality or non equality of two IDs etc.
    /// </summary>
    [Serializable]
    public struct ID : IValueObject, IComparable
    {
        private readonly object value;
        private readonly int hashCode;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ID"/> with the specified value.
        /// </summary>
        /// <param name="newValue">The value to use.</param>
        public ID(object newValue)
        {
            value = newValue;

            hashCode = value == null || value.ToString().Length == 0 ? 0 : value.ToString().ToUpperInvariant().GetHashCode();

            if (hashCode == 0) value = null;    // is empty
        }

        #endregion Constructors        
        
        #region Operators

        /// <summary>
        /// Returns the equality of two <see cref="ID"/>s.
        /// </summary>
        /// <param name="x">The first <see cref="ID"/>.</param>
        /// <param name="y">The second <see cref="ID"/>.</param>
        /// <returns>
        /// true if both the IDs are same; otherwise, <c>false</c>
        /// </returns>
        public static bool operator ==(ID x, ID y)
        {
            // performance & memory optimization
            return x.hashCode == y.hashCode &&
                   (x.hashCode == 0 ||
                    x.value.ToString().Equals(y.value.ToString(), StringComparison.OrdinalIgnoreCase));  // equal hashes could also be a hash collision, so be sure that those are equal
        }

        /// <summary>
        /// Returns the inequality of two <see cref="ID"/>s.
        /// </summary>
        /// <param name="x">The first <see cref="ID"/>.</param>
        /// <param name="y">The second <see cref="ID"/>.</param>
        /// <returns>
        /// true if the IDs are not same; otherwise, <c>false</c>
        /// </returns>
        public static bool operator !=(ID x, ID y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Compares this instance to another <see cref="ID"/>.
        /// </summary>
        /// <param name="obj">The <see cref="ID"/> to compare against.</param>
        /// <returns>
        /// true if both the <see cref="ID"/>s are same; otherwise, <c>false</c>
        /// </returns>
        public override bool Equals(object obj)
        {
            // Make sure the cast that follows won't fail
            if (obj == null || !(obj is ID))
                return false;

            return (this == (ID) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            return hashCode;
        }

        public int CompareTo(object obj)
        {
            throw new NotSupportedException("Id should not be used in sort expressions. Please use another property.");
        }

        #endregion Operators


        #region Empty


        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <returns>
        /// true if this instance is empty; otherwise, false.
        /// </returns>
        public bool IsEmpty
        {
            get { return hashCode == 0; }
        }

        #endregion Empty

        #region Value

        /// <summary>
        /// Returns the string representation of the Value of this <see cref="ID"/>.
        /// </summary>
        /// <returns>
        /// The string representation of the value of this <see cref="ID"/>.
        /// </returns>
        public override string ToString()
        {
            return IsEmpty ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Gets the value of this <see cref="ID"/>.
        /// </summary>
        /// <return>
        /// The value of this <see cref="ID"/>.
        /// </return>
        public object Value
        {
            get { return value; }
        }

        #endregion Value

        #region TryParse

        /// <summary>
        /// Checks whether the specified string is a valid <see cref="ID"/>.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <param name="result">The resulting <see cref="ID"/>, or the empty <see cref="ID"/> if the string is not a valid <see cref="ID"/>.</param>
        /// <returns>
        /// true if the specified string is a valid <see cref="ID"/>; otherwise, false.
        /// </returns>
        public static bool TryParse(string s, out ID result)
        {
            result = IdManager.New(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out ID result)
        {
            return TryParse(s, out result);
        }

        #endregion

    }
}
