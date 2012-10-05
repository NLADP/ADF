using System;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
    public struct Html : IValueObject, IComparable, IFormattable
    {
		private readonly string _value;
        
		#region CodeGuard(Constructors)

		/// <summary>
		/// Initializes a new instance of the <see cref="Email"/> class.
		/// </summary>
		/// <remarks>
		/// If the e-mail address could not be validated, a <see cref="FormatException"/> 
		/// exception is thrown.
		/// </remarks>
		/// <param name="newvalue">The e-mail address to use.</param>
        public Html(string newvalue)
		{
		    //initialize
			_value = string.Empty;

            if (string.IsNullOrEmpty(newvalue)) return;
		    
			_value = newvalue;
		}
	    

		#endregion CodeGuard(Constructors)

		#region CodeGuard(Operators)

		/// <summary>
        /// Returns the equality of two <see cref="Html"/> objects.
		/// </summary>
        /// <param name="i">The first <see cref="Html"/>.</param>
        /// <param name="j">The second <see cref="Html"/>.</param>
		/// <returns>
		/// <c>true</c> if they are the same; otherwise <c>false</c>
		/// </returns>
        public static bool operator ==(Html i, Html j) 
		{
			return (i._value == j._value);
		}

		/// <summary>
        /// Returns the inequality of two <see cref="Html"/> objects.
		/// </summary>
        /// <param name="i">The first <see cref="Html"/>.</param>
        /// <param name="j">The second <see cref="Html"/>.</param>
		/// <returns>
		/// <c>true</c> if they differ; otherwise <c>false</c>
		/// </returns>
        public static bool operator !=(Html i, Html j) 
		{
			return (i._value != j._value);
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

			return (this == (Html)obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type, suitable for use in hashing algorithms and 
        /// data structures like a hash table.
		/// </summary>
		/// <returns>
        /// The hash code for the current <see cref="Html"/> object.
		/// </returns>
		public override int GetHashCode() 
		{
			return _value.GetHashCode();
		}

        /// <summary>
        /// Indicates whether the first supplied <see cref="Html"/> is greater than the second 
        /// supplied <see cref="Html"/>.
        /// </summary>
        /// <param name="i">The first <see cref="Html"/>.</param>
        /// <param name="j">The second <see cref="Html"/>.</param>
        /// <returns>True if the first supplied <see cref="Html"/> is greater than the second supplied 
        /// <see cref="Email"/>, false otherwise.</returns>
        public static bool operator >(Html i, Html j) 
		{
			return (i._value.CompareTo(j._value) > 0);
		}

        /// <summary>
        /// Indicates whether the first supplied <see cref="Html"/> is less than the second 
        /// supplied <see cref="Html"/>.
        /// </summary>
        /// <param name="i">The first <see cref="Html"/>.</param>
        /// <param name="j">The second <see cref="Html"/>.</param>
        /// <returns>True if the first supplied <see cref="Html"/> is less than the second supplied 
        /// <see cref="Html"/>, false otherwise.</returns>
        public static bool operator <(Html i, Html j) 
		{
			return (i._value.CompareTo(j._value) < 0);
		}

        /// <summary>
        /// Compares the current <see cref="Html"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="Html"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="Html"/> is equal to the supplied object. Value greater than zero indicates that 
        /// the <see cref="Html"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            Html other;
            try
            {
                other = (Html)obj;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("obj is not an Html string");
            }

            if (this < other)
            {
                return -1;
            }
            if (this > other)
            {
                return 1;
            }
            return 0;
        }

        public static implicit operator Html(string text)
        {
            return New(text);
        }

	    #endregion CodeGuard(Operators)

		#region CodeGuard(New)

		/// <summary>
        /// Creates a new empty <see cref="Html"/> object.
		/// </summary>
        /// <returns>The new <see cref="Html"/>.</returns>
        public static Html New()
		{
            return new Html(string.Empty);
		}

		/// <summary>
        /// Creates a new empty <see cref="Html"/> object using the specified initial html string.
		/// </summary>
		/// <param name="value">The initial html string.</param>
        /// <returns>The new <see cref="Html"/>.</returns>
        public static Html New(string value)
		{
            return new Html(value);
		}

		#endregion CodeGuard(New)

		#region CodeGuard(Empty)
        /// <summary>
        /// The empty <see cref="Html"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static Html Empty; 

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
		/// Returns a <see cref="string"/> that represents the current <see cref="Email"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="string"/> that represents the current <see cref="Email"/>.
		/// </returns>
		public override string ToString()
		{
            return string.IsNullOrEmpty(_value) ? string.Empty : _value;
		}

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.IsNullOrEmpty(_value) ? string.Empty : _value;
        }
		
		/// <summary>
		/// Returns the html string.
		/// </summary>
		/// <value>The html string.</value>
		public object Value
		{
			get { return _value; }
		}

		#endregion  CodeGuard(Value)

        #region static TryParse
        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="Html"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="Html"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out Html result)
        {
            result = string.IsNullOrEmpty(s) ? Empty : new Html(s);

            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out Html result)
        {
            return TryParse(s, out result);
        }

        #endregion
    }
}
