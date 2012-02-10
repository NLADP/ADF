using System;
using System.Reflection;
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
		private readonly double min; 
        private readonly double max;

		/// <summary>
		/// Creates a new <see cref="InRangeAttribute"/> instance with the supplied minimum and maximum 
        /// value.
		/// </summary>
		/// <param name="min">The minimum value of the range.</param>
		/// <param name="max">The maximum value of the range.</param>
        public InRangeAttribute(double min, double max)
		{
			this.min = min;
			this.max = max;
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
                newValue = (double)value;
            }
            catch
            {
                return ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeInRangeInvalid", propertyToValidate.Name);
            }
            
            if (!newValue.InRange(min, max))
                return ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeInRangeInvalidRange", propertyToValidate.Name, min, max);
            
            return ValidationResult.Success;
            
        }

        /// <summary>
        /// Returns the maximum value.
        /// </summary>
        public double Max
	    {
            get { return max; }
	    }

        /// <summary>
        /// Returns the minimum value.
        /// </summary>
        public double Min
	    {
            get { return min; }
	    }
	}
}
