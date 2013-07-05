using System.Linq;
using System.Reflection;
using Adf.Base.Domain;
using Adf.Core.Extensions;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Represents an AttributeValidationPolicy. Provides methods to validate <see cref="DomainObject"/>s.
    /// </summary>
    public class AttributeValidationPolicy : IValidationPolicy
    {
        
        #region IValidationPolicy Members

        /// <summary>
        /// Validates <see cref="DomainObject"/>s. 
        /// </summary>
        /// <param name="validatable">The object to validate, in most cases this will be a domain obect 
        /// or a domain collection.</param>
        public void Validate(object validatable)
        {
            Validate(validatable, false);
        }

        /// <summary>
        /// Validates <see cref="DomainObject"/>s. 
        /// </summary>
        /// <param name="validatable">The object to validate, in most cases this will be a domain obect 
        /// or a domain collection.</param>
        /// <param name="suppressWarnings">Decides if warnings are added in the ValidationManager.</param>
        public void Validate(object validatable, bool suppressWarnings)
        {
            var type = validatable.GetType();

            // Validate aggregated child BusinessEntities
            var properties  = type.GetProperties();

            foreach (var property in properties)
            {
                if (!ValidationManager.ValidationResults.ContainsErrorForProperty(property))
                {
                    // If NonEmpty attribute fails, do not check other attributes, 
                    // otherwise you'll get a NonEmpty and MinLength error at the same time
                    var nonEmptyResult = ValidationResult.Success;

                    if (property.IsDefined(typeof (NonEmptyAttribute), false))
                    {
                        var nonempty = property.GetCustomAttributes(typeof (NonEmptyAttribute), false).First() as NonEmptyAttribute;

                        if (nonempty != null)
                        {
                            nonEmptyResult = nonempty.IsValid(property, property.GetValue(validatable, new object[] {}));
                        }

                        if (!nonEmptyResult.IsSuccess)
                        {
                            ValidationManager.AddValidationResult(nonEmptyResult);
                        }
                    }

                    if (nonEmptyResult.IsSuccess)
                    {
                        foreach (IPropertyValidator validator in property.GetCustomAttributes(typeof (IPropertyValidator), false))
                        {
                            var result = validator.IsValid(property, property.GetValue(validatable, new object[] {}));
                            if (!result.IsSuccess)
                            {
                                ValidationManager.AddValidationResult(result);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
