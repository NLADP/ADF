using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Validation;

namespace Adf.Base.Validation
{
    public static class ValidationResultExtensions
    {
        public static ValidationResult CreateError<T>(this T validatedobject, Expression<Func<T, object>> propertyexpression, string message, params object[] p)
        {
            return ValidationResult.CreateError((PropertyInfo) propertyexpression.GetExpressionMember(), message, p);
        }

        public static ValidationResult CreateInfo<T>(this T validatedobject, Expression<Func<T, object>> propertyexpression, string message, params object[] p)
        {
            return ValidationResult.CreateInfo((PropertyInfo) propertyexpression.GetExpressionMember(), message, p);
        }

        public static ValidationResult CreateWarning<T>(this T validatedobject, Expression<Func<T, object>> propertyexpression, string message, params object[] p) 
        {
            return ValidationResult.CreateWarning((PropertyInfo) propertyexpression.GetExpressionMember(), message, p);
        }
    }
}
