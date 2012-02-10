using System;
using System.Reflection;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>
	/// Attribute to determine if the value to check is in the range of the ones defined in min and max 
    /// values.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public sealed class InSqlDateTimeRangeAttribute : Attribute, IPropertyValidator
	{
        private readonly DateTime min = new DateTime(1753, 1, 1);
        private readonly DateTime max = new DateTime(9999, 12, 31);

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
                return ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeInRangeInvalid", propertyToValidate.Name);
            }
//            double newValue;
//            try
//            {
//                newValue = (double)value;
//            }
//            catch
//            {
//                return ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeInRangeInvalid", propertyToValidate.Name);
//            }

            DateTime newValue = (DateTime) value;

            if (min >= newValue || max <= newValue)
            {
                return ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeInRangeInvalidRange", propertyToValidate.Name, min, max);
            }
            
//            if (!NumberHelper.CheckRange(newValue, min, max))
//                return ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeInRangeInvalidRange", propertyToValidate.Name, min, max);
            
            return ValidationResult.Success;
            
        }

        /// <summary>
        /// Returns the maximum value.
        /// </summary>
        public DateTime Max
	    {
            get { return max; }
	    }

        /// <summary>
        /// Returns the minimum value.
        /// </summary>
        public DateTime Min
	    {
            get { return min; }
	    }
	}
}
