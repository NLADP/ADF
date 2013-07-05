using System;
using System.Reflection;
using Adf.Core;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>    
    /// Attribute to determine whether the date value is from past.
	/// </summary>
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	public sealed class NotInPastAttribute : Attribute, IPropertyValidator
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

            if (!(value is DateTime)) throw new ArgumentException("value for NotInPastAttribute is not a DateTime");

            return !(Convert.ToDateTime(value.ToString()) >= DateTime.Now) 
                ? ValidationResult.CreateError(propertyToValidate, Config.Domain.AttributeNotInPastInvalid, propertyToValidate.Name) 
                : ValidationResult.Success;
		}
	}
}
