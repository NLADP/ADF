using System;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>
	/// Attribute to determine if the value to check is not empty.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public sealed class NonEmptyAttribute : Attribute, IPropertyValidator
	{
		/// <summary>
        /// Determines whether the specified value is valid for the supplied property.
		/// </summary>
        /// <param name="propertyToValidate">The specified property.</param>
        /// <param name="value">The specified value.</param>
		/// <returns>
        /// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
		/// </returns>
        public ValidationResult IsValid(PropertyInfo propertyToValidate, object value)
		{
		    return PropertyHelper.IsEmpty(value) 
                ? ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeNonEmptyInvalid", propertyToValidate.Name) 
                : ValidationResult.Success;
		}
	}
}
