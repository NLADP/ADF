namespace Adf.Core.Validation
{
    /// <summary>
    /// Defines a method that a class implements to handle the specified <see cref="ValidationResultCollection"/>
    /// with the specified parameters.
    /// </summary>
    public interface IValidationHandler
    {
        /// <summary>
        /// Handles the specified <see cref="ValidationResultCollection"/> with the specified parameters.
        /// </summary>
        /// <param name="validationResults">The <see cref="ValidationResultCollection"/> to handle.</param>
        /// <param name="p">The parameters to use during handling.</param>
        void Handle(ValidationResultCollection validationResults, params object[] p);
    }
}
