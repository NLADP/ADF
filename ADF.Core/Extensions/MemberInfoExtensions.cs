using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

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
            return (mi != null) && (mi.GetCustomAttributes(typeof(ExcludeAttribute), false).Length > 0);
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, object>> expression)
        {
            return (PropertyInfo) GetMemberInfo(expression);
        }

        public static MemberInfo GetMemberInfo<T, V>(this Expression<Func<T, V>> expression)
        {
            var body = ((expression.Body is MemberExpression)
                           ? expression.Body
                           : (expression.Body is UnaryExpression)
                                 ? ((UnaryExpression) expression.Body).Operand
                                 : null) as MemberExpression;

            if (body == null) throw new ArgumentException("Expression is not a valid member expression");

            // body.Member doesn't return always the correct DeclaringType (for example when a property is overridden in a sub class)
            return body.Expression.Type.GetMember(body.Member.Name).Single();
//
//            return body.Member;
        }

        public static string GetPropertyPath<T>(this Expression<Func<T, object>> propertyExpression)
        {
            var unaryExpression = propertyExpression.Body as UnaryExpression;    // ValueTypes are passed as UnaryExpressions

            var memberExpression = (unaryExpression == null ? propertyExpression.Body : unaryExpression.Operand) as MemberExpression;

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
