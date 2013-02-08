using System;
using System.Reflection;
using Adf.Core.Extensions;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>
	/// Attribute to determine if the value to check has at least the specified minimum length.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public sealed class MinLengthAttribute : Attribute, IPropertyValidator
	{
		private readonly int length;

		/// <summary>
		/// Creates a new <see cref="MinLengthAttribute"/> instance with the supplied length.
		/// </summary>
        /// <param name="length">The supplied length.</param>
		public MinLengthAttribute(int length)
		{
			this.length = length;
		}

		/// <summary>
        /// Determines whether the specified value is valid for the supplied property. 
		/// </summary>
        /// <param name="propertyToValidate">The supplied property.</param>
        /// <param name="value">The supplied value.</param>
		/// <returns>
        /// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
		/// </returns>
        public ValidationResult IsValid(PropertyInfo propertyToValidate, object value)
		{
            if (value == null) return ValidationResult.Success;

		    return (value.ToString().HasMinLength(length))
                ? ValidationResult.Success
		        : ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeMinLengthInvalid", propertyToValidate.Name, length);
		}
	    
        /// <summary>
        /// Returns the minimum length.
        /// </summary>
	    public int Length
	    {
	        get
	        {
	            return length;
	        }
	    }
	}
}
