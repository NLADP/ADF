using System;
using System.Collections.Generic;
using System.Reflection;
using Adf.Core.Domain;
using System.Linq;
using Adf.Core.Extensions;

namespace Adf.Base.Domain
{
    public class ObjectDtoMappingProvider : IObjectMappingProvider
    {
        /// <summary>
        /// Binds the field for a domainobject to item from the source object.
        /// </summary>
        /// <param name="target">Object to bind fields in.</param>
        /// <param name="source">Source used for binding fields, such as DataSet, RowState or IInternalState.</param>
        /// <param name="mapping"></param>
        public TDomainObject Bind<TDomainObject, TSource>(TDomainObject target, TSource source, Mapping<TDomainObject, TSource> mapping)
        {
            if (source == null || target == null) return target;

            foreach (var map in GetProperties(target, source, mapping))
            {

                object value = map.Value.GetValue(source, null);

                PropertyHelper.SetValue(target, map.Key, value);

            }
            return target;
        }

        private Dictionary<PropertyInfo, PropertyInfo> GetProperties<TDomainObject, TSource>(TDomainObject target, TSource source, Mapping<TDomainObject, TSource> mapping)
    {
        if (mapping == null)
        {
            var props = new Dictionary<PropertyInfo, PropertyInfo>();

            var properties = source.GetType().GetProperties();

            foreach (var sourceProperty in properties)
            {
                var targetProperty = target.GetType().GetProperty(sourceProperty.Name); // ??
                //target.GetType().GetProperty(sourceProperty.Name.ToLower());

                // no match or can't set, skip this property
                if (targetProperty == null || !targetProperty.CanWrite) continue;

                props.Add(targetProperty, sourceProperty);
            }

            return props;
        }

        return mapping.ToDictionary(map => map.Key.GetPropertyInfo(), map => map.Value.GetPropertyInfo());
    }

        /// <summary>
        /// Binds the field for a domainobject to item from the source object.
        /// </summary>
        /// <param name="source">Object to bind fields from to target object.</param>
        /// <param name="target">Target used to binding fields to, and persist to a data source later on.</param>
        /// <param name="mapping"></param>
        public TTarget Persist<TDomainObject, TTarget>(TDomainObject source, TTarget target, Mapping<TDomainObject, TTarget> mapping)
        {
            if (target == null || source == null) return target;

            var properties = target.GetType().GetProperties();

            foreach (var targetProperty in properties)
            {
                var sourceProperty = source.GetType().GetProperty(targetProperty.Name); // ??
                //target.GetType().GetProperty(sourceProperty.Name.ToLower());

                // no match, skip this property
                if (sourceProperty == null) continue;

                var value = sourceProperty.GetValue(source, null);
                //object value = sourceProperty.GetValue(source, null);

                PropertyHelper.SetValue(target, targetProperty, value);
            }

            return target;
        }
    }
}
