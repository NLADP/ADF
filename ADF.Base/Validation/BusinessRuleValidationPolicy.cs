using System.Linq;
using System.Reflection;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Represents BusinessRuleValidationPolicy. This class does some sort of validations on a 
    /// validatable object.
    /// </summary>
    public class BusinessRuleValidationPolicy : IValidationPolicy
    {
        #region IValidationPolicy Members

        /// <summary>
        /// Implements some sort of validation on a validatable object. When validation fails, the 
        /// implementing services are required to add validation messages to the validation manager.
        /// </summary>
        /// <param name="validatable">The object to validate. In most cases this will be a domain 
        /// obect or a domain collection.</param>
        public void Validate(object validatable)
        {
            Validate(validatable, false);
        }
        
        /// <summary>
        /// Implements some sort of validation on a validatable object. When validation fails, the 
        /// implementing services are required to add validation messages to the validation manager.
        /// </summary>
        /// <param name="validatable">The object to validate. In most cases this will be a domain 
        /// obect or a domain collection.</param>
        /// <param name="suppressWarnings">Decides if warnings and infos are added to the ValidationManager.</param>
        public void Validate(object validatable, bool suppressWarnings)
        {
            var type = validatable.GetType();

            //Validate This Business Entity
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            
            foreach (var methodInfo in methods.Where(methodInfo => methodInfo.IsDefined(typeof (BusinessRuleAttribute), true)))
            {
                if (!typeof(ValidationResult).IsAssignableFrom(methodInfo.ReturnType))
                {
                    throw new InvalidMethodSignatureException(
                        string.Format(
                            "Business rules validations must return a validation result. Signature of method {0}.{1} should be ValidationResult {1}(object context)",
                            type.Name,
                            methodInfo.Name));
                }
                var validationResult = (ValidationResult) methodInfo.Invoke(validatable, null);
                if (validationResult.IsError || (!suppressWarnings && (validationResult.IsWarning || validationResult.IsInformational)))
                {
                    ValidationManager.AddValidationResult(validationResult);
                }
            }
        }

        #endregion
    }
}
