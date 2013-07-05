using System.Collections;
using System.Linq;
using System.Reflection;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Represents an ChildValidationPolicy. Provides methods to validate child business entities found against a DomainObject.
    /// </summary>
    public class ChildValidationPolicy : IValidationPolicy
    {
        #region IValidationPolicy Members

        /// <summary>
        /// Validates validatable objects. When validation fails, the 
        /// implementing services are required to add validation messages to the validation manager.
        /// </summary>
        /// <param name="validatable">The object to validate, in most cases this will be a domain object 
        /// or a domain collection.</param>
        public void Validate(object validatable)
        {
            Validate(validatable, false);
        }

        /// <summary>
        /// Validates validatable objects. When validation fails, the 
        /// implementing services are required to add validation messages to the validation manager.
        /// </summary>
        /// <param name="validatable">The object to validate, in most cases this will be a domain obect 
        /// or a domain collection.</param>
        /// <param name="suppressWarnings">Decides if warnings are added in the ValidationManager.</param>
        public void Validate(object validatable, bool suppressWarnings)
        {
            var type = validatable.GetType();
            
            // Validate aggregated child BusinessEntities
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var field in fields.Where(field => field.IsDefined(typeof (IncludeInValidationAttribute), true)))
            {
                if (typeof(IEnumerable).IsAssignableFrom(field.FieldType))
                {
                    var enumerable = (IEnumerable)field.GetValue(validatable);
                    if (enumerable != null)
                    {
                        foreach (var nestedItem in enumerable)
                        {
                            ValidateChild(suppressWarnings, nestedItem);
                        }
                    }
                    continue;
                }

                var value = field.GetValue(validatable);
                if (value != null)
                {
                    ValidateChild(suppressWarnings, value);
                }
            }
        }
        
        /// <summary>
        /// Validates child business entity objects. When validation fails, the 
        /// implementing services are required to add validation messages to the validation manager.
        /// </summary>
        /// <param name="suppressWarnings">Decides if warnings are added in the ValidationManager.</param>
        /// <param name="value">The object to validate. In most cases this will be a domain 
        /// obect or a domain collection.</param>
        private void ValidateChild(bool suppressWarnings, object value)
        {

            using (ValidationManager.CreateValidationScope(string.Empty))
            {
                Validate(value, suppressWarnings);
            }
        }

        #endregion
    }
}
