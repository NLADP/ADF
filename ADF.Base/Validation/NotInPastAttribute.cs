using System;
using System.Reflection;
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.Convert.ToDateTime(System.String)"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Adf.Core.ValidationResult.CreateError(System.String,System.Object[])")]
        public ValidationResult IsValid(PropertyInfo propertyToValidate, object value)
		{

            if (!(value is DateTime)) throw new ArgumentException("value for NotInPastAttribute is not a DateTime");

            if (value == null)
//                return ValidationResult.CreateError(property, "Adf.Business.AttributeNotInPastNull", property.Name);
                // this is not a NonEmpty attribute!
                return ValidationResult.Success;

            if (!(Convert.ToDateTime(value.ToString()) >= DateTime.Now))
                return ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeNotInPastInvalid", propertyToValidate.Name);
            
            return ValidationResult.Success;
		}
	}
}
