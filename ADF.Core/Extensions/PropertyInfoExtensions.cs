using System.Reflection;

namespace Adf.Core.Extensions
{
    ///<summary>
    /// Extensions for working with PropertyInfo objects.
    ///</summary>
    public static class PropertyInfoExtensions
    {
        public static bool HasAttribute<T>(this PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof (T), false).Length > 0;
        }
    }
}
