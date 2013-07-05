using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Adf.Core;
using Adf.Core.Domain;

namespace Adf.Business.ValueObject
{
	/// <summary>
	/// Class representing the value object File.
	/// </summary>
	public struct FileName : IValueObject, IComparable
	{
		private string value;

		//TODO: Add actual regular expression for File

		#region CodeGuard(Constructors)

        /// <summary>
        /// Initializes a new instance of the <see cref="FileName"/>.
        /// </summary>
        /// <remarks>
        /// If the filename could not be validated, a <see cref="FormatException"/> 
        /// exception is thrown.
        /// </remarks>
        /// <param name="newvalue">The filename to use.</param>
		public FileName(string newvalue)
		{
			value = null;
            
            if (string.IsNullOrEmpty(newvalue)) return;

			if (!IsValidFileName(newvalue))
			{
                throw new FormatException("value is not a valid filename");
			    
			}
            value = newvalue;
		}
	    
        /// <summary>
        /// Checks whether the supplied value is a valid <see cref="FileName"/> or not.
        /// </summary>
        /// <param name="newValue">The value to check.</param>
        /// <returns>True if the supplied value is a valid <see cref="FileName"/>, false otherwise.</returns>
	    private static bool IsValidFileName(string newValue)
	    {
	        try
	        {
	            new FileInfo(newValue);
	        }
	        catch
	        {
	            return false;
	        }
	        return true;
	        
	    }

		#endregion CodeGuard(Constructors)

		#region CodeGuard(Operators)

        /// <summary>
        /// Returns the equality of two <see cref="FileName"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="FileName"/>.</param>
        /// <param name="j">The second <see cref="FileName"/>.</param>
        /// <returns>
        /// <c>true</c> if both filenames are the same; otherwise <c>false</c>
        /// </returns>
		public static bool operator ==(FileName i, FileName j) 
		{
			return (i.value == j.value);
		}

        /// <summary>
        /// Returns the inequality of two <see cref="FileName"/> objects.
        /// </summary>
        /// <param name="i">The first <see cref="FileName"/>.</param>
        /// <param name="j">The second <see cref="FileName"/>.</param>
        /// <returns>
        /// <c>true</c> if the filenames are different; otherwise <c>false</c>
        /// </returns>
		public static bool operator !=(FileName i, FileName j) 
		{
			return (i.value != j.value);
		}

        /// <summary>
        /// Indicates whether the first supplied <see cref="FileName"/> is greater than the second 
        /// supplied <see cref="FileName"/>.
        /// </summary>
        /// <param name="i">The first <see cref="FileName"/>.</param>
        /// <param name="j">The second <see cref="FileName"/>.</param>
        /// <returns>True if the first supplied <see cref="FileName"/> is greater than the second supplied 
        /// <see cref="FileName"/>, false otherwise.</returns>
        public static bool operator >(FileName i, FileName j)
        {
            return (i.value.CompareTo(j.value) > 0);
        }

        /// <summary>
        /// Indicates whether the first supplied <see cref="FileName"/> is less than the second 
        /// supplied <see cref="FileName"/>.
        /// </summary>
        /// <param name="i">The first <see cref="FileName"/>.</param>
        /// <param name="j">The second <see cref="FileName"/>.</param>
        /// <returns>True if the first supplied <see cref="FileName"/> is less than the second supplied 
        /// <see cref="FileName"/>, false otherwise.</returns>
        public static bool operator <(FileName i, FileName j)
        {
            return (i.value.CompareTo(j.value) < 0);
        }


        /// <summary>
        /// Compares this instance to another <see cref="FileName"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="FileName"/> object to compare against.</param>
        /// <returns>
        /// <c>true</c> if both filenames are the same; otherwise <c>false</c>
        /// </returns>
        public override bool Equals(object obj) 
		{
			// Make sure the cast that follows won't fail
        	if (obj == null || obj.GetType() != GetType())
				return false;

			return (this == (FileName) obj);
	}

        /// <summary>
        /// Serves as a hash function for a particular type, suitable for use in hashing algorithms and 
        /// data structures like a hash table.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="FileName"/>.
        /// </returns>
		public override int GetHashCode() 
		{
			return value.GetHashCode();
		}

        /// <summary>
        /// Compares an <see cref="FileName"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="FileName"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="FileName"/> is equal to the supplied object. Value greater than zero indicates 
        /// that the <see cref="FileName"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            FileName other;
            try
            {
                other = (FileName)obj;
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("obj is not a FileName");
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
		#endregion CodeGuard(Operators)

		#region CodeGuard(New)

        /// <summary>
        /// Creates a new empty <see cref="FileName"/> object.
        /// </summary>
        /// <returns>The new <see cref="FileName"/>.</returns>
		public static FileName New()
		{
			return new FileName(string.Empty);
		}

        /// <summary>
        /// Creates a new empty <see cref="FileName"/> object using the specified value.
        /// </summary>
        /// <param name="value">The supplied value.</param>
        /// <returns>The new <see cref="FileName"/>.</returns>
		public static FileName New(string value)
		{
			return new FileName(value);
		}

		#endregion CodeGuard(New)

		#region CodeGuard(Empty)
        /// <summary>
        /// The empty <see cref="FileName"/>.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        public static FileName Empty = new FileName(string.Empty);

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
            return string.IsNullOrEmpty(value) ? string.Empty : value;
		}

        /// <summary>
        /// Returns the filename.
        /// </summary>
        /// <value>The filename.</value>
		public object Value
		{
			get { return value; }
		}

		#endregion  CodeGuard(Value)

        /// <summary>
        /// Tries to parse the supplied string into the supplied <see cref="FileName"/> object.
        /// </summary>
        /// <param name="s">The string to parse.</param>
        /// <param name="result">The <see cref="FileName"/> object.</param>
        /// <returns>True if the parsing is successful, false otherwise.</returns>
        public static bool TryParse(string s, out FileName result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = Empty;
                return true;
            }

            if (!IsValidFileName(s))
            {
                result = Empty;
                return false;
            }

            result = new FileName(s);
            return true;
        }

        public static bool TryParse(string s, IFormatProvider provider, out FileName result)
        {
            return TryParse(s, out result);
        }
    }
}
