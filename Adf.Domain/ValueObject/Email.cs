using System;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
    //}
	/// <summary>
	/// Class representing the value object Email.
	/// </summary>
    [Serializable]
    public struct Email : IValueObject, IComparable
	{
		private readonly string value;

		/// <summary>
		/// Regular expression for validating an e-mail address.
		/// </summary>
		private static readonly Regex Expression = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

		#region CodeGuard(Constructors)

		/// <summary>
		/// Initializes a new instance of the <see cref="Email"/> class.
		/// </summary>
		/// <remarks>
		/// If the e-mail address could not be validated, a <see cref="FormatException"/> 
		/// exception is thrown.
		/// </remarks>
		/// <param name="newvalue">The e-mail address to use.</param>
		public Email(string newvalue)
		{
		    //initialize
			value = string.Empty;

		    _hashCode = 0;

            if (string.IsNullOrEmpty(newvalue)) return;
		    
		    if (!Expression.IsMatch(newvalue))
		    {
		        throw new FormatException("emailaddress is not in a valid format");
		    }
		    
			value = newvalue;
		    _hashCode = value.ToUpperInvariant().GetHashCode();
		}
	    

		#endregion CodeGuard(Constructors)

		#region CodeGuard(Operators)

		/// <summary>
		/// Returns the equality of two <see cref="Email"/> objects.
		/// </summary>
		/// <param name="i">The first <see cref="Email"/>.</param>
		/// <param name="j">The second <see cref="Email"/>.</param>
		/// <returns>
		/// <c>true</c> if both addresses are the same; otherwise <c>false</c>
		/// </returns>
		public static bool operator ==(Email i, Email j) 
		{
            if (i._hashCode != j._hashCode) return false;

			return (i.value.ToUpperInvariant() == j.value.ToUpperInvariant());
		}

		/// <summary>
		/// Returns the inequality of two <see cref="Email"/> objects.
		/// </summary>
		/// <param name="i">The first <see cref="Email"/>.</param>
		/// <param name="j">The second <see cref="Email"/>.</param>
		/// <returns>
		/// <c>true</c> if the addresses are different; otherwise <c>false</c>
		/// </returns>
		public static bool operator !=(Email i, Email j) 
		{
			return !(i == j);
		}

		/// <summary>
		/// Compares this instance to another object.
		/// </summary>
		/// <param name="obj">The object to compare against.</param>
		/// <returns>
		/// <c>true</c> if both addresses are the same; otherwise <c>false</c>
		/// </returns>
		public override bool Equals(object obj) 
		{
			// Make sure the cast that follows won't fail
			if (obj == null || obj.GetType() != GetType())
			    return false;

			return (this == (Email)obj);
		}

	    private readonly int _hashCode;

		/// <summary>
		/// Serves as a hash function for a particular type, suitable for use in hashing algorithms and 
        /// data structures like a hash table.
		/// </summary>
		/// <returns>
        /// The hash code for the current <see cref="Email"/> object.
		/// </returns>
		public override int GetHashCode()
		{
		    return _hashCode;
		}

        /// <summary>
        /// Indicates whether the first supplied <see cref="Email"/> is greater than the second 
        /// supplied <see cref="Email"/>.
        /// </summary>
        /// <param name="i">The first <see cref="Email"/>.</param>
        /// <param name="j">The second <see cref="Email"/>.</param>
        /// <returns>True if the first supplied <see cref="Email"/> is greater than the second supplied 
        /// <see cref="Email"/>, false otherwise.</returns>
	    public static bool operator >(Email i, Email j) 
		{
            if (i.IsEmpty) return false;

			return (i.CompareTo(j) > 0);
		}

        /// <summary>
        /// Indicates whether the first supplied <see cref="Email"/> is less than the second 
        /// supplied <see cref="Email"/>.
        /// </summary>
        /// <param name="i">The first <see cref="Email"/>.</param>
        /// <param name="j">The second <see cref="Email"/>.</param>
        /// <returns>True if the first supplied <see cref="Email"/> is less than the second supplied 
        /// <see cref="Email"/>, false otherwise.</returns>
        public static bool operator <(Email i, Email j) 
		{
			return (i.CompareTo(j) < 0);
		}

        /// <summary>
        /// Compares the current <see cref="Email"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="Email"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="Email"/> is equal to the supplied object. Value greater than zero indicates that 
        /// the <see cref="Email"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            Email other;
            try
            {
                other = (Email)obj;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("obj is not an Email");
            }

            if (IsEmpty && other.IsEmpty) return 0;

            if (IsEmpty) return -1;

            return string.CompareOrdinal(value, other.value);
        }
	    #endregion CodeGuard(Operators)

		#region CodeGuard(New)

		/// <summary>
		/// Creates a new empty <see cref="Email"/> object.
		/// </summary>
		/// <returns>The new <see cref="Email"/>.</returns>
		public static Email New()
		{
			return new Email(string.Empty);
		}

		/// <summary>
		/// Creates a new empty <see cref="Email"/> object using the specified initial e-mail address.
		/// </summary>
		/// <param name="value">The initial e-mail address.</param>
		/// <returns>The new <see cref="Email"/>.</returns>
		public static Email New(string value)
		{
			return new Email(value);
		}

		#endregion CodeGuard(New)

		#region CodeGuard(Empty)
        /// <summary>
        /// The empty <see cref="Email"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static Email Empty; 

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
		/// Returns a <see cref="string"/> that represents the current <see cref="Email"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="string"/> that represents the current <see cref="Email"/>.
		/// </returns>
		public override string ToString()
		{
            return string.IsNullOrEmpty(value) ? string.Empty : value;
		}
		
		/// <summary>
		/// Returns the e-mail address.
		/// </summary>
		/// <value>The e-mail address.</value>
		public object Value
		{
			get { return value; }
		}

		#endregion  CodeGuard(Value)

	    #region static TryParse
        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="Email"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="Email"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
	    public static bool TryParse(string s, out Email result )
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

	        result = new Email(s);
	        return true;
	        
	    }

        public static bool TryParse(string s, IFormatProvider provider, out Email result)
        {
            return TryParse(s, out result);
        }

	    #endregion
	}
}
