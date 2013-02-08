using Adf.Core.State;
using Adf.Core.Validation;

namespace Adf.Web
{
    /// <summary>
    /// Provides methods to parse validation result collection and reflect the exception on the UI.
    /// </summary>
    public class SmartWebValidationHandler : IValidationHandler
    {
        public const string Key = "PendingValidationResults";

        /// <summary>   
        /// Prepares the validation results for the ExceptionControl
        /// </summary>
        /// <param name="validationResults">Collection of validation results.</param>
        /// <param name="p">Variable no of objects.</param>
        public void Handle(ValidationResultCollection validationResults, params object[] p)
        {
            var results = StateManager.Personal[Key] as ValidationResultCollection;

            if (results != null)
            {
                results.AddRange(validationResults);
            }
            else
            {
                StateManager.Personal[Key] = validationResults;
            }
        }

        public static ValidationResultCollection GetValidationResults()
        {
            return StateManager.Personal[Key] as ValidationResultCollection;
        }
    }
}
