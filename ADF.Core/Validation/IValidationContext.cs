using System;

namespace Adf.Core.Validation
{
    /// <summary>
    /// Defines methods that a class implements to add <see cref="ValidationResult"/> to a ValidationContext,
    /// to clear a ValidationContext, to create a ValidationScope etc.
    /// </summary>
    public interface IValidationContext
    {
        /// <summary>
        /// Clears the ValidationContext.
        /// </summary>
        void Clear();
        
        /// <summary>
        /// Gets the <see cref="ValidationResultCollection"/>.
        /// </summary>
        ValidationResultCollection ValidationResults { get; }

        /// <summary>
        /// Adds the specified <see cref="ValidationResult"/> to the ValidationContext.
        /// </summary>
        /// <param name="result">The <see cref="ValidationResult"/> to add.</param>
        void AddValidationResult(ValidationResult result);

        /// <summary>
        /// Creates and returns a ValidationScope with the specified scope name.
        /// </summary>
        /// <param name="scopeName">The scope name of the ValidationScope.</param>
        /// <returns>
        /// The newly created ValidationScope.
        /// </returns>
        IDisposable CreateValidationScope(string scopeName);
    }
}
