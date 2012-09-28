using System;
using System.Linq.Expressions;
using System.Reflection;
using Adf.Core.Validation;
using Adf.Core.Extensions;

namespace Adf.Base.Validation
{
    /// <summary>
    /// Represents helper function for PropertyInfo.
    /// </summary>
    public static class PropertyInfoExtensions
    {
        /// <summary>
        /// Checks whether the supplied property has any <see cref="CustomAttribute"/> or not.
        /// </summary>
        /// <param name="pi">The property.</param>
        /// <returns>True if the supplied property has any <see cref="CustomAttribute"/>, false 
        /// otherwise.</returns>
        public static bool IsNonEmpty(this PropertyInfo pi)
        {
            if (pi == null) return false;

            var attributes = (NonEmptyAttribute[]) pi.GetCustomAttributes(typeof (NonEmptyAttribute), false);

            return (attributes.Length > 0);
        }

        public static ValidationResult IsValid<T>(this IPropertyValidator validator, T entity, Expression<Func<T, object>> propertyExpression, Func<bool> predicate = null)
        {
            // return if condition is false
            if (predicate != null && !predicate()) return ValidationResult.Success;

            return validator.IsValid(propertyExpression.GetPropertyInfo(), propertyExpression.Compile()(entity));
        }
    }
}