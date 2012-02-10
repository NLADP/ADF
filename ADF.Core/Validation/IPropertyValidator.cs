using System.Reflection;

namespace Adf.Core.Validation
{
	/// <summary>
    /// Defines a method that a value type or class implements to check the validity of a specified value 
    /// for a specified property.
	/// </summary>
	public interface IPropertyValidator
	{
		/// <summary>
        /// Checks a specified value for a specified property and returns a <see cref="ValidationResult"/>.
		/// </summary>
		/// <param name="propertyToValidate">The property to check for.</param>
		/// <param name="value">The value to check.</param>
        /// <returns>The <see cref="ValidationResult"/> for the validity checking.</returns>
		ValidationResult IsValid(PropertyInfo propertyToValidate, object value);
	}
}
