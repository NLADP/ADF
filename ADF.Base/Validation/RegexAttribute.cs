using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Adf.Core;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
	/// <summary>
	/// Attribute to determine if the value to check can be validated against the provided regular expression.
	/// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public sealed class RegexAttribute : Attribute, IPropertyValidator
	{
	    /// <summary>
        /// Creates a new <see cref="RegexAttribute"/> with the supplied expression.
		/// </summary>
		/// <param name="expression">The supplied expression.</param>
		public RegexAttribute(string expression)
		{
			this.Expression = expression;
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
        public ValidationResult IsValid(PropertyInfo propertyToValidate, object value)
		{
            if (value == null) return ValidationResult.Success;

            return !Regex.IsMatch(value.ToString(), Expression) 
                ? ValidationResult.CreateError(propertyToValidate, Config.Domain.AttributeRegexInvalid, propertyToValidate.Name) 
                : ValidationResult.Success;
		}

	    /// <summary>
	    /// Returns the regular expression.
	    /// </summary>
	    public string Expression { get; private set; }
	}
}
