using System;
using Adf.Core.Test;
using Adf.Core.Validation;

namespace Adf.Test.Validation
{
    public class TestValidationHandler : IValidationHandler
    {
        #region Implementation of IValidationHandler

        /// <summary>
        /// Handles the specified <see cref="ValidationResultCollection"/> with the specified parameters.
        /// </summary>
        /// <param name="validationResults">The <see cref="ValidationResultCollection"/> to handle.</param>
        /// <param name="p">The parameters to use during handling.</param>
        public void Handle(ValidationResultCollection validationResults, params object[] p)
        {
            TestManager.Register(TestItemType.ValidationResult, validationResults, validationResults.IsSucceeded ? TestAction.ValidationSucceeded : TestAction.ValidationFailed, validationResults.ToString());

            Console.WriteLine(validationResults.ConvertToString(Environment.NewLine));
        }

        #endregion
    }
}
