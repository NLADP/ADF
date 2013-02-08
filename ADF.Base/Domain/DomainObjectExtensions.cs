using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Adf.Base.Validation;
using Adf.Core.Domain;
using Adf.Core.Validation;
using Adf.Core.Extensions;

namespace Adf.Base.Domain
{
    public static class DomainObjectExtensions
    {
        public static void Validate(this IDomainObject domainObject)
        {
            ValidationManager.Validate(domainObject);

            foreach (var pi in domainObject.GetCompositions())
            {
                var value = pi.GetValue(domainObject, null);
                if (value == null)
                    continue;

                if (value is IDomainCollection)
                {
                    var children = value as IDomainCollection;

                    foreach (IDomainObject child in children)
                    {
                        child.Validate();
                    }
                }

                if (value is IDomainObject)
                {
                    var child = value as IDomainObject;

                    if (!child.IsEmpty || pi.HasAttribute<NonEmptyAttribute>())
                    {
                        child.Validate();
                    }
                }
            }
        }

        public static Type GetFactoryType(this IDomainObject domainObject)
        {
            return domainObject.GetType().GetFactoryType();
        }

        public static Type GetFactoryType(this Type domainObjectType)
        {
            var typeName = domainObjectType.AssemblyQualifiedName.Replace(domainObjectType.FullName, domainObjectType.FullName + "Factory");

            return Type.GetType(typeName);
        }

        public static T InvokeFactoryMethod<T>(this IDomainObject domainObject, string method, params object[] p)
        {
            if (domainObject == null) return default(T);
            if (method == null) return default(T);

            return (T)domainObject.GetFactoryType().GetMethod(method, BindingFlags.Static | BindingFlags.Public).Invoke(null, p);
        }
        
        // internal so cant accidentally be called in user code, its a dirty way anyway
        internal static bool Save(this IDomainObject domainObject)
        {
            return domainObject.InvokeFactoryMethod<bool>("Save", domainObject);
        }

        public static bool Remove(this IDomainObject domainObject)
        {
            return domainObject.InvokeFactoryMethod<bool>("Remove", domainObject);
        }

        public static IEnumerable GetAll(this IDomainObject domainObject)
        {
            return domainObject.InvokeFactoryMethod<IEnumerable>("GetAll");
        }

        public static IDomainObject Empty(this IDomainObject domainObject)
        {
            var type = domainObject.GetFactoryType();

            return type.GetProperty("Empty").GetValue(domainObject, null) as IDomainObject;
        }

        public static IDomainObject Empty(Type domainObjectType)
        {
            var type = domainObjectType.GetFactoryType();

            return type.GetProperty("Empty").GetValue(null, null) as IDomainObject;
        }

        public static bool IsNullOrEmpty(this IDomainObject domainObject)
        {
            return domainObject == null || domainObject.IsEmpty;
        }

        /// <summary>
        /// Saves the compositions of the supplied <see cref="DomainObject"/>.
        /// </summary>
        /// <param name="domainobject">The <see cref="DomainObject"/>.</param>
        /// <returns>True if the saving is successful, false otherwise.</returns>
        public static bool SaveCompositions(this IDomainObject domainobject)
        {
            // Only manage properties that are composites
            foreach (var pi in GetCompositions(domainobject))
            {
                var value = pi.GetValue(domainobject, null);
                if (value == null)
                    continue;

                if (value is IDomainCollection)
                {
                    var children = value as IDomainCollection;

                    if (!children.IsAltered && !children.HasRemovedItems)
                        continue;

                    if (!children.Save())
                        return false;
                }

                if (value is IDomainObject)
                {
                    var child = value as IDomainObject;

                    if (!child.IsAltered)
                        continue;

                    if (!child.Save())
                        return false;
                }
            }
            return true;
        }

        public static bool RemoveCompositions(this IDomainObject domainobject)
        {
            foreach (var pi in GetCompositions(domainobject))
            {
                var value = pi.GetValue(domainobject, null);
                if (value == null)
                    continue;

                var domainCollection = value as IDomainCollection;
                if (domainCollection != null)
                {
                    if (!domainCollection.RemoveAll().Save()) return false;
                }

                var domainObject = value as IDomainObject;
                if (domainObject != null)
                {
                    if (!domainObject.Remove()) return false;
                }
            }
            return true;
        }

        private static IEnumerable<PropertyInfo> GetCompositions(this IDomainObject domainObject)
        {
            return domainObject.GetType()
                .GetProperties()
                .Where(pi => CompositionAttribute.IsComposite(pi));
        }

        public static T CopyProperties<T>(this T copy, T origin) where T : IDomainObject
        {
            foreach (var pi in copy.GetType().GetProperties().Where(pi => pi.CanWrite))
            {
                pi.SetValue(copy, pi.GetValue(origin, null), null);
            }

            return copy;
        }
    }
}
