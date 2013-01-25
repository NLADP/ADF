using System;
using System.Reflection;
using Adf.Core.Extensions;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>
	/// Attribute to determine if the value to check is exact as the specified length.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public sealed class ExactLengthAttribute : Attribute, IPropertyValidator
	{
		private readonly int length;

		/// <summary>
		/// Creates a new <see cref="ExactLengthAttribute"/> instance with the supplied length.
		/// </summary>
		/// <param name="length">The supplied length.</param>
        public ExactLengthAttribute(int length)
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
            if (value == null || value.ToString() == string.Empty) return ValidationResult.Success;
		    
            return !value.ToString().HasExactLength(length) ? ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeExactLengthInvalid", propertyToValidate.Name, length) : ValidationResult.Success;
		}
	    
        /// <summary>
        /// Returns the length.
        /// </summary>
	    public int Length
	    {
	        get{return (int)length;}
	    }
	}
}
