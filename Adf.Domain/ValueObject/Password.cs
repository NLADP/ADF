using System;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Domain;
using Adf.Core.Encryption;

namespace Adf.Domain.ValueObject
{
    [Serializable]
    public struct Password : IValueObject, IEquatable<Password>, IComparable, IComparable<Password>
    {
        private readonly string value;
        public object Value
        {
            get { return value; }
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        #region CodeGuard(Expression)

        private static readonly Regex Expression = new Regex(@".*");

        #endregion CodeGuard(Expression)

        public Password(string s)
        {
            //initialize
            value = null;

            if (string.IsNullOrEmpty(s)) return;

            if (!IsValidPassword(s))
            {
                throw new FormatException(String.Format("Value {0} is not a valid password", s));
            }

            value = s;
        }

        #region Validity

        #region CodeGuard(IsValid)

        /// <summary>
        /// Checks whether a value is a valid password.
        /// </summary>
        /// <param name="s">Name to try</param>
        /// <returns>true f parameter is a valid password</returns>
        public static bool IsValidPassword(string s)
        {
            return Expression.IsMatch(s);
        }

        #endregion CodeGuard(IsValid)

        /// <summary>
        /// Checks whether the value presented is a valid password.
        /// </summary>
        /// <param name="s">The string that is validated to see if it contains a valid <see cref="Password"/>.</param>
        /// <param name="result">The resulting password, or the Password.Empty if the string did not contain a valid <see cref="Password"/>.</param>
        /// <returns>
        /// A <see cref="Password"></see> containing a fully qualified type name.
        /// </returns>
        public static bool TryParse(string s, out Password result)
        {
            result = Empty;

            if (string.IsNullOrEmpty(s))
            {
                return true;
            }

            if (!IsValidPassword(s))
            {
                return false;
            }

            result = new Password(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out Password result)
        {
            return TryParse(s, out result);
        }

        #endregion

        #region Empty

        public static Password Empty
        {
            get { return new Password(string.Empty); }
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
        /// <param name="other">password</param>
        /// <returns></returns>
        public bool Equals(Password other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Compares this instance to another <see cref="Password"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="Password"/> object to compare against.</param>
        /// <returns>
        /// <c>true</c> if both passwords are the same; otherwise <c>false</c>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Password)
                return Equals((Password)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// Returns the equality of two <see cref="Password"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="Password"/>.</param>
        /// <param name="j">The second <see cref="Password"/>.</param>
        /// <returns>
        /// <c>true</c> if both passwords are the same; otherwise <c>false</c>
        /// </returns>
        public static bool operator ==(Password i, Password j)
        {
            return i.Equals(j);
        }

        /// <summary>
        /// Returns the inequality of two <see cref="Password"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="Password"/>.</param>
        /// <param name="j">The second <see cref="Password"/>.</param>
        /// <returns>
        /// <c>true</c> if the passwords are different; otherwise <c>false</c>
        /// </returns>
        public static bool operator !=(Password i, Password j)
        {
            return !i.Equals(j);
        }

        #endregion Equality

        #region Comparison

        ///<summary>
        ///Compares the current instance with another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than obj. Zero This instance is equal to obj. Greater than zero This instance is greater than obj. 
        ///</returns>
        ///
        ///<param name="obj">An object to compare with this instance. </param>
        ///<exception cref="T:System.ArgumentException">obj is not the same type as this instance. </exception><filterpriority>2</filterpriority>
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (!(obj is Password))
                throw new ArgumentException("Object is not a password");

            Password other = (Password)obj;

            return value.CompareTo(other.value);
        }

        ///<summary>
        ///Compares the current object with another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings: Value Meaning Less than zero This object is less than the other parameter.Zero This object is equal to other. Greater than zero This object is greater than other. 
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        public int CompareTo(Password other)
        {
            return value.CompareTo(other.value);
        }

        #endregion

        #region CodeGuard(Custom)

        public Password Encrypt()
        {
            return new Password(this.Encrypt(EncryptionType.Passwords));
        }

        #endregion CodeGuard(Custom)
    }
}
