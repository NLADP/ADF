using System;
using System.Linq.Expressions;
using Adf.Core.Extensions;

namespace Adf.Web.UI
{
    public static class ExpressionExtensions
    {
        public static string GetControlName<T>(this Expression<Func<T, object>> property)
        {
            return typeof(T).Name + property.GetExpressionMember().Name;
        }
    }
}
