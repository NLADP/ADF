using System;
using System.Linq.Expressions;
using Adf.Core.Extensions;

namespace Adf.Core.Validation
{
    public static class ValidationResultExtensions
    {
        public static bool HasResults(this ValidationResultCollection collection)
        {
            return collection.IsNullOrEmpty();
        }

        public static ValidationResult CreateError<T>(this T validatedobject, Expression<Func<T, object>> expression, string message, params object[] p)
        {
            return ValidationResult.CreateError(expression, message, p);
        }

        public static ValidationResult CreateInfo<T>(this T validatedobject, Expression<Func<T, object>> expression, string message, params object[] p)
        {
            return ValidationResult.CreateInfo(expression, message, p);
        }

        public static ValidationResult CreateWarning<T>(this T validatedobject, Expression<Func<T, object>> expression, string message, params object[] p) 
        {
            return ValidationResult.CreateWarning(expression, message, p);
        }
    }
}
