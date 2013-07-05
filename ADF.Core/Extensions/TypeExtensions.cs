using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Adf.Core.Domain;
using Adf.Core.Types;

namespace Adf.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsValueObject(this Type type)
        {
            return typeof(IValueObject).IsAssignableFrom(type);
        }

        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>);
        }

        public static bool IsSmartReference(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ISmartReference);
        }

        public static bool IsEnum(this Type type)
        {
            return type.IsEnum;
        }

        public static T New<T>(this Type type, params object[] args)
        {
            // performance optimization
//            Type[] argTypes = args.Select(a => a == null ? typeof(object) : a.GetType()).ToArray();
            var argType = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var a = args[i];

                argType[i] = a == null ? typeof (object) : a.GetType();
            }

            return ObjectActivator.GetActivator<T>(type, argType)(args);
        }

        public static MethodInfo FindMethod(this Type type, string method, params object[] p)
        {
            Type[] types = p.Select(param => param == null ? typeof(object) : param.GetType()).ToArray();

            return type.GetMethod(method, types);
        }

    }
}
