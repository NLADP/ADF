using System;
using Adf.Core.Domain;

namespace Adf.Base.Types
{
    public static class TypeExtensions
    {
        public static bool IsValueObject(this Type type)
        {
            return typeof(IValueObject).IsAssignableFrom(type);
        }

        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof (Nullable<>));
        }

        public static bool IsSmartReference(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(ISmartReference));
        }

        public static bool IsEnum(this Type type)
        {
            return type.IsEnum;
        }
    }
}
