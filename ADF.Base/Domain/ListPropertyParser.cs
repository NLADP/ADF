using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Represents Enum property parsing operations.
    /// </summary>
    public class ListPropertyParser : IPropertyParser
    {
        public void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture = null)
        {
            throw new NotImplementedException();
        }

        public ICollection GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            var list = target as IList;
            if (list == null) throw new InvalidOperationException("Not an IList");

            var argTypes = target.GetType().GetGenericArguments();

            if (argTypes.Length != 1) throw new InvalidOperationException("Please use a IList<T>");

            var valueList = PropertyHelper.GetCollection(argTypes[0], null, includeEmpty, items);

            
            return valueList;
        }

        public ICollection<ValueItem> GetValueItems(object target, ICollection items)
        {
//            PropertyHelper.GetValueItems()
//            foreach (var valueItem in items)
//            {
//                valueItem.Selected = list.Cast<object>().Any(o => o.ToString() == valueItem.Value.ToString());
//            }
            throw new NotImplementedException();
        }

//        public ICollection<ValueItem> GetCollection(Type targetType, bool includeEmpty, IEnumerable items = null)
//        {
//            throw new NotImplementedException();
//        }

        public bool IsEmpty(object value)
        {
            if (value == null) return true;

            var list = value as IList;

            if (list == null) throw new InvalidOperationException("Not an IList");

            return list.Count == 0;
        }

        public bool IsEqual(object compare, object to)
        {
            throw new NotImplementedException();
        }

        public bool IsParsable(Type type)
        {
            return type.IsGenericType() && typeof(IList).IsAssignableFrom(type);
        }
    }
}
