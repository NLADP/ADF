namespace Adf.Core.Validation
{
    /// <summary>
    /// Defines methods that a class implements to validate a validatable object.
    /// </summary>
    public interface IValidationPolicy
    {
        /// <summary>
        /// Does some sort of validation on a validatable object. When validation fails, the 
        /// implementing services are required to add validation messages to the ValidationManager.
        /// </summary>
        /// <param name="validatable">The object to validate, in most cases this will be a domain object 
        /// or a domain collection.</param>
        void Validate(object validatable);

        /// <summary>
        /// Does some sort of validation on a validatable object. When validation fails, the 
        /// implementing services are required to add validation messages to the ValidationManager.
        /// </summary>
        /// <param name="validatable">The object to validate, in most cases this will be a domain obect 
        /// or a domain collection.</param>
        /// <param name="suppressWarnings">Decides if warnings are added in the ValidationManager.</param>
        void Validate(object validatable, bool suppressWarnings);
    }
}
