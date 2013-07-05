using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Adf.Base.Domain;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Types;

namespace Adf.Base.Types
{
    public class DomainObjectTypeConverter : ITypeConverter
    {
        /// <summary>
        /// Decides whether a specific converter can convert this type.
        /// </summary>
        /// <param name="type">Type to convert</param>
        /// <returns>True if converter can convert, false if it can't.</returns>
        public bool CanConvert(Type type)
        {
            return typeof(IDomainObject).IsAssignableFrom(type);
        }

        /// <summary>
        /// Convert a type, usually primitive, to a rich type.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="value">Object to convert into type.</param>
        /// <returns>Object converted to rich type.</returns>
        public T To<T>(object value)
        {
            return (T) To(typeof (T), value);
        }

        public object To(Type type, object value)
        {
            object[] parms = { value };
            var constructors = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            var constr = constructors.FirstOrDefault(c => c.GetParameters().Count() == 1 && c.GetParameters()[0].ParameterType == typeof(IInternalState));

            return (constr != null) ? constr.Invoke(parms) : null;
        }

        /// <summary>
        /// Extracts the primitive value of a rich type.
        /// </summary>
        /// <typeparam name="T">Type to extract value from.</typeparam>
        /// <param name="value"></param>
        /// <returns>Primitive type which is extract from rich type.</returns>
        public object ToPrimitive<T>(T value)
        {
            var d = value as DomainObject;

            return (d != null) ? d.GetState() : new NullInternalState();
        }
    }
}
