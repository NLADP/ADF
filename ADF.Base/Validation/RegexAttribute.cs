using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>
	/// Attribute to determine if the value to check can be validated against the provided regular expression.
	/// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class RegexAttribute : Attribute, IPropertyValidator
	{
		private string expression;

		/// <summary>
        /// Creates a new <see cref="RegexAttribute"/> with the supplied expression.
		/// </summary>
		/// <param name="expression">The supplied expression.</param>
		public RegexAttribute(string expression)
		{
			this.expression = expression;
		}

		/// <summary>
        /// Determines whether the specified value is valid for the supplied property. It uses 
        /// Regex.IsMatch(string input, string pattern).
		/// </summary>
        /// <param name="propertyToValidate">The supplied property.</param>
        /// <param name="value">The supplied value.</param>
		/// <returns>
        /// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
		/// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "Adf.Core.ValidationResult.CreateError(System.String,System.Object[])")]
        public ValidationResult IsValid(PropertyInfo propertyToValidate, object value)
		{
            if (value == null) 
//                return ValidationResult.CreateError(property, "Adf.Business.AttributeRegexNull", property.Name);
                // this is not a NonEmpty attribute!
                return ValidationResult.Success;

            if (!Regex.IsMatch(value.ToString(), expression))
                return ValidationResult.CreateError(propertyToValidate, "Adf.Business.AttributeRegexInvalid", propertyToValidate.Name);
		    
		    return ValidationResult.Success;
		}
	    
        /// <summary>
        /// Returns the regular expression.
        /// </summary>
	    public string Expression
	    {
	        get{return expression;}
	    }
	}
}
