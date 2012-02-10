using System;

namespace Adf.Core.Validation
{
    /// <summary>
    /// Represents custom attribute of a field to indicate whether the field of a class will be included
    /// in the validation process.
    /// Used during the validation of an object to check whether a field of the object will be included
    /// in the validation process.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IncludeInValidationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a <see cref="IncludeInValidationAttribute"/>.
        /// </summary>
        public IncludeInValidationAttribute()
        {
        }

        /// <summary>
        /// Initializes a <see cref="IncludeInValidationAttribute"/> with the specified string 
        /// indicating DependsOnModelContext.
        /// </summary>
        /// <param name="dependsOnModelContext">The string which depends on the Model Context.</param>
        public IncludeInValidationAttribute(string dependsOnModelContext)
        {
            DependsOnModelContext = dependsOnModelContext;
        }

        /// <summary>
        /// Gets or sets the DependsOnModelContext.
        /// </summary>
        /// <returns>The DependsOnModelContext of this object.</returns>
        public string DependsOnModelContext { get; private set; }
    }
}