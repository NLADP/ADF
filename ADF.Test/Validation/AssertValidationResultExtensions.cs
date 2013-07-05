using Adf.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test.Validation
{
    public static class AssertValidationResultExtensions
    {
        public static ValidationResult DoesSucceed(this ValidationResult result)
        {
            Assert.IsTrue(result.IsSuccess, "Validation was expected to be succesful, but is of severity '{0}'.", result.Severity);

            return result;
        }

        public static ValidationResult DoesNotSucceed(this ValidationResult result)
        {
            Assert.IsTrue(!result.IsSuccess, "Validation was expected to fail, but appears to be succesful.");

            return result;
        }

        public static ValidationResult HasSeverity(this ValidationResult result, ValidationResultSeverity severity)
        {
            Assert.AreEqual(result.Severity, severity, "Validation was expected to end with severity '{0}', but has severity '{1}'.", result.Severity, severity);

            return result;
        }

        public static ValidationResult HasError(this ValidationResult result)
        {
            Assert.AreEqual(result.Severity, ValidationResultSeverity.Error, "Validation was expected to end with severity 'Error', but has severity '{0}'.", result.Severity);

            return result;
        }
    }
}
