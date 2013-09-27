using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Adf.Core.Domain;
using Adf.Core.TypeExtensions;
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
            return type.IsGenericType() && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static bool IsSmartReference(this Type type)
        {
            return type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(ISmartReference);
        }

        public static bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }

        public static T New<T>(this Type type, params object[] args)
        {
            // performance optimization
            //            Type[] argTypes = args.Select(a => a == null ? typeof(object) : a.GetType()).ToArray();
            var argType = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var a = args[i];

                argType[i] = a == null ? typeof(object) : a.GetType();
            }

            return ObjectActivator.GetActivator<T>(type, argType)(args);
        }

        public static MethodInfo FindMethod(this Type type, string method, params object[] p)
        {
            Type[] types = p.Select(param => param == null ? typeof(object) : param.GetType()).ToArray();

            return type.GetMethod(method, types);
        }

        public static bool ParameterTypesMap(this MethodBase method, Type[] argTypes)
        {
            var parameters = method.GetParameters();

            if (parameters.Length != argTypes.Length) return false;

            return
                !parameters.Where((p, i) => !p.ParameterType.IsAssignableFrom(argTypes[i]) && argTypes[i] != typeof(object))
                    .Any();
        }


        #region Extensions for TypeInfo

        public static IEnumerable<FieldInfo> GetFields(this Type type, BindingFlags bindingAttr = BindingFlags.Default)
        {
            return type.GetTypeInfo().DeclaredFields;
        }

        public static bool IsAssignableFrom(this Type t, Type c)
        {
            return t.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
        }


        public static PropertyInfo GetProperty(this Type type, string propertyName)
        {
            return type.GetTypeInfo().GetDeclaredProperty(propertyName);
        }

        public static bool IsGenericType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType;
        }

//        public static Type ReflectedType(this MemberInfo memberInfo)
//        {
//            // hack!! this is not the same as ReflectedType!
//            return memberInfo.DeclaringType;
//        }

        public static bool IsInstanceOfType(this Type t, object o)
        {
            // copy of .net4
            return o != null && t.IsAssignableFrom(o.GetType());
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type)
        {
//            return type.GetTypeInfo().DeclaredProperties;
            return type.GetRuntimeProperties();

            // bug: this is a workaround for Xamarin.Android, which throws an stackoverflowexc
//            var properties = type.GetTypeInfo().DeclaredProperties.ToList();
//
//            var baseType = type.GetTypeInfo().BaseType;
//            if (baseType != typeof(object))
//            {
//                var baseProperties = GetProperties(baseType);
//                properties.AddRange(baseProperties);
//        }
//
//            return properties;
        }

        public static MethodInfo GetMethod(this Type type, string methodName, BindingFlags flags)
        {
            return type.GetTypeInfo().GetDeclaredMethod(methodName);
        }

        public static MethodInfo GetMethod(this Type type, string methodName, Type[] types)
        {
            return type.GetTypeInfo().GetDeclaredMethods(methodName).FirstOrDefault(m => m.ParameterTypesMap(types));
        }

        public static IEnumerable<MethodInfo> GetMethods(this Type type, string name = null)
        {
            return name == null ? type.GetTypeInfo().DeclaredMethods : type.GetTypeInfo().GetDeclaredMethods(name);
        }

        public static IEnumerable<MemberInfo> GetMember(this Type type, string name, BindingFlags flags = BindingFlags.Default)
        {
            var prop = type.GetRuntimeProperty(name);
            var field = type.GetRuntimeField(name);

            if (prop != null) yield return prop;

            if (field != null) yield return field;
        }

        public static FieldInfo GetField(this Type type, string value)
        {
            return type.GetTypeInfo().GetDeclaredField(value);
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GetTypeInfo().GenericTypeParameters;
        }

        public static IEnumerable<Type> GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags flags = BindingFlags.Default)
        {
            return type.GetTypeInfo().DeclaredConstructors;
        }

        #endregion Extensions for TypeInfo

    }
}
