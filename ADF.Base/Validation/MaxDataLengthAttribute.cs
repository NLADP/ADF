using System;
using System.Reflection;
using Adf.Base.Domain;
using Adf.Core.Extensions;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Attribute to determine if the value to check exceeds the specified max data length.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class MaxDataLengthAttribute : Attribute, IPropertyValidator
    {
        private readonly int precision;
        private readonly int scale;

        /// <summary>
        /// Creates a new <see cref="MaxLengthAttribute"/> instance with the supplied data length.
        /// </summary>
        /// <param name="precision">The supplied precision.</param>
        /// <param name="scale">The supplied scale.</param>
        public MaxDataLengthAttribute(int precision, int scale = 0)
        {
            this.precision = precision;
            this.scale = scale;
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

            return !HasMaxDataLength(value, precision, scale)
                 ? ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeMaxDataLengthInvalid", propertyToValidate.Name, precision, scale)
                 : ValidationResult.Success;
        }

        private static bool HasMaxDataLength(object value, int precision, int scale)
        {
            double newValue;
            if (Double.TryParse(value.ToString(), out newValue))
            {
                if (newValue < Math.Pow(10, precision - scale))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the precision.
        /// </summary>
        public int Precision
        {
            get { return precision; }
        }

        /// <summary>
        /// Returns the scale.
        /// </summary>
        public int Scale
        {
            get { return scale; }
        }
    }
}
