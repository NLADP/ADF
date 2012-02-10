using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Adf.Core.Extensions
{
    ///<summary>
    /// Extensions to ExcludeAttribute, to validate whether a member is excluded.
    ///</summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Returns a value indicating whether the value of the specified field will be exclded from a list.
        /// </summary>
        /// <param name="mi">The field to check.</param>
        /// <returns>
        /// true if the value of the specified field needs to be excluded from a list; otherwise, false.
        /// </returns>
        /// <exception cref="System.NullReferenceException">
        /// Object reference not set to an instance of an object.
        /// </exception>
        public static bool IsExcluded(this MemberInfo mi)
        {
            return (mi != null) ? (mi.GetCustomAttributes(typeof(ExcludeAttribute), false).Length > 0) : false;
        }

        public static MemberInfo GetExpressionMember<T>(this Expression<Func<T, object>> propertyExpression)
        {
            MemberExpression body = null;
            if (propertyExpression.Body is UnaryExpression)
            {
                var unary = propertyExpression.Body as UnaryExpression;
                if (unary.Operand is MemberExpression)
                    body = unary.Operand as MemberExpression;
            }
            else if (propertyExpression.Body is MemberExpression)
            {
                body = propertyExpression.Body as MemberExpression;
            }
            if (body == null)
                throw new ArgumentException("'propertyExpression' should be a member expression");

            // Extract the right part (after "=>")
            //            var vmExpression = body.Expression as ConstantExpression;

            return body.Member;
        }

        public static string GetPropertyPath<T>(this Expression<Func<T, object>> propertyExpression)
        {
            var unaryExpression = propertyExpression.Body as UnaryExpression;    // ValueTypes are passed as UnaryExpressions

            var memberExpression = unaryExpression == null
                                       ? propertyExpression.Body as MemberExpression
                                       : unaryExpression.Operand as MemberExpression;

            var path = new List<string>();

            while (memberExpression != null)
            {
                path.Insert(0, memberExpression.Member.Name);

                memberExpression = memberExpression.Expression as MemberExpression;
            }

            return string.Join(".", path);
        }
    }
}
