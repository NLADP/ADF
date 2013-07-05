using System;
using System.Reflection;
using Adf.Core;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>
	/// Attribute to determine if the value to check is in the range of the ones defined in min and max 
    /// values.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public sealed class InSqlSmallDateTimeRangeAttribute : Attribute, IPropertyValidator
	{
	    public InSqlSmallDateTimeRangeAttribute()
	    {
	        Min = new DateTime(1900, 1, 1);
	        Max = new DateTime(2079, 6, 6);
	    }

	    /// <summary>
        /// Determines whether the specified value is valid for the supplied property. 
        /// </summary>
        /// <param name="propertyToValidate">The supplied property.</param>
        /// <param name="value">The supplied value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public ValidationResult IsValid(PropertyInfo propertyToValidate, object value)
        {
            // enable to be empty
            if (value == null)
                return ValidationResult.Success;

            if (!(value is DateTime))
            {
                return ValidationResult.CreateError(propertyToValidate, Config.Domain.AttributeInRangeInvalid, propertyToValidate.Name);
            }

            var newValue = (DateTime) value;

            if (Min > newValue || Max < newValue)
            {
                return ValidationResult.CreateError(propertyToValidate, Config.Domain.AttributeInRangeInvalidRange, propertyToValidate.Name, Min, Max);
            }
            
            return ValidationResult.Success;
        }

	    /// <summary>
	    /// Returns the maximum value.
	    /// </summary>
	    public DateTime Max { get; private set; }

	    /// <summary>
	    /// Returns the minimum value.
	    /// </summary>
	    public DateTime Min { get; private set; }
	}
}
