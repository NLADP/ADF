using System;
using System.Reflection;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Attribute to determine whether a property is composite.
    /// </summary>

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class CompositionAttribute : Attribute
    {
        /// <summary>
        /// Determines whether the specified property is composite.
        /// </summary>
        /// <param name="pi">The property.</param>
        /// <returns>
        /// 	<c>true</c> if the specified property is composite; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsComposite(PropertyInfo pi)
        {
            if (pi == null)
                return false;

            return (pi.GetCustomAttributes(typeof(CompositionAttribute), false).Length > 0);
        }
    }
}