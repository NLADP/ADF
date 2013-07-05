using System;
using System.Linq;
using Adf.Core;
using Adf.Core.Tasks;
using Adf.Core.Test;
using Adf.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test.Extensions
{
    public static class AssertValidationExtensions
    {
        public static T ValidationFailedWith<T>(this T task, ValidationResultSeverity severity, string code) where T : ITask
        {
            var items = TestManager.FindItems(TestItemType.ValidationResult, TestAction.ValidationFailed);
            var result = items.Select(item => (ValidationResultCollection)item.Subject).Any(rc => rc.Any(r => r.Severity == severity && r.Message.IndexOf(code, StringComparison.OrdinalIgnoreCase) != -1));

            if (!result)
            {
                Assert.Fail(severity > ValidationResultSeverity.Informational ? "Validation did not fail with the expected code '{0}'" : "Validation did not succeeed with the expected code '{0}'", code);
            }

            return task;
        }

        public static T ValidationFailedWithError<T>(this T task, string code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Error, code);
        }

        public static T ValidationSucceededWithInfo<T>(this T task, string code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Informational, code);
        }

        public static T ValidationFailedWithWarning<T>(this T task, string code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Warning, code);
        }

        public static T ValidationFailedWith<T>(this T task, ValidationResultSeverity severity, Descriptor code) where T : ITask
        {
            return ValidationFailedWith(task, severity, code.Name);
        }

        public static T ValidationFailedWithError<T>(this T task, Descriptor code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Error, code);
        }

        public static T ValidationSucceededWithInfo<T>(this T task, Descriptor code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Informational, code);
        }

        public static T ValidationFailedWithWarning<T>(this T task, Descriptor code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Warning, code);
        }
    }
}
