using System;
using System.Reflection;
using Adf.Base.Domain;
using Adf.Core.Extensions;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Attribute to determine if the value to check exceeds the specified max length.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class MaxLengthAttribute : Attribute, IPropertyValidator
    {
        private readonly int length;
        private readonly int decimalLength;

        /// <summary>
        /// Creates a new <see cref="MaxLengthAttribute"/> instance with the supplied length.
        /// </summary>
        /// <param name="length">The supplied length.</param>
        public MaxLengthAttribute(int length)
        {
            this.length = length;
        }

        /// <summary>
        /// Creates a new <see cref="MaxLengthAttribute"/> instance with the supplied length.
        /// Implemented only for the value object; Money.
        /// </summary>
        /// <param name="length">The supplied length.</param>
        /// <param name="decimallength">The supplied length for value after the decimal delimiter.</param>
        public MaxLengthAttribute(int length, int decimallength)
        {
            this.length = length;
            decimalLength = decimallength;
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

            if (decimalLength != 0)
                return !HasMaxLength(value, length)
                     ? ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeMaxLengthMoneyInvalid", propertyToValidate.Name, length, decimalLength)
                     : ValidationResult.Success;

            return !value.ToString().HasMaxLength(length)
                ? ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeMaxLengthInvalid", propertyToValidate.Name, length)
                : ValidationResult.Success;
        }

        private static bool HasMaxLength(object value, int length)
        {
            Money money;
            Money.TryParse(value.ToString(), out money);

            if (money != null && money.Amount.HasValue)
            {
                if (money.Amount.Value < (decimal)Math.Pow(10, length))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the maximum length.
        /// </summary>
        public int Length
        {
            get { return length; }
        }

        /// <summary>
        /// Returns the maximum length for value after the decimal delimiter.
        /// </summary>
        public int DecimalLength
        {
            get { return decimalLength; }
        }
    }
}
