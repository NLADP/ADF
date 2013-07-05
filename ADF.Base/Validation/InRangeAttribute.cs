using System;
using System.Reflection;
using Adf.Core;
using Adf.Core.Extensions;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>
	/// Attribute to determine if the value to check is in the range of the ones defined in min and max 
    /// values.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public sealed class InRangeAttribute : Attribute, IPropertyValidator
	{
	    /// <summary>
		/// Creates a new <see cref="InRangeAttribute"/> instance with the supplied minimum and maximum 
        /// value.
		/// </summary>
		/// <param name="min">The minimum value of the range.</param>
		/// <param name="max">The maximum value of the range.</param>
        public InRangeAttribute(double min, double max)
		{
			Min = min;
			Max = max;
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

            if (value == null)
                return ValidationResult.Success;

            double newValue;
            try
            {
                newValue = Convert.ToDouble(value);
            }
            catch
            {
                return ValidationResult.CreateError(propertyToValidate, Config.Domain.AttributeInRangeInvalid, propertyToValidate.Name);
            }
            
            if (!newValue.InRange(Min, Max))
                return ValidationResult.CreateError(propertyToValidate, Config.Domain.AttributeInRangeInvalidRange, propertyToValidate.Name, Min, Max);
            
            return ValidationResult.Success;
            
        }

	    /// <summary>
	    /// Returns the maximum value.
	    /// </summary>
	    public double Max { get; private set; }

	    /// <summary>
	    /// Returns the minimum value.
	    /// </summary>
	    public double Min { get; private set; }
	}
}
