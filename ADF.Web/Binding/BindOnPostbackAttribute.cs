using System;
using Adf.Core.Binding;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Attribute to mark whether binding must take place on every postback
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class BindOnPostbackAttribute : Attribute
    {
        /// <summary>
        /// Indicates whether the specified binder must bind this attribute of a control on postback.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <returns>True if the binder must bind this attribute of a control on postback, false
        /// otherwise. The default value is false</returns>
        public static bool MustBindOnPostback(IControlBinder binder)
        {
            if (binder == null) return false;

            return (binder.GetType().GetCustomAttributes(typeof(BindOnPostbackAttribute), false).Length > 0);
        }
    }
}
