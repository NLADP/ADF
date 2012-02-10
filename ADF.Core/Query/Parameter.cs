using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Core.Query
{
    /// <summary>
    /// Parameter functions as a generic datasource parameter and can be used to pass query data to spesific datasource providers 
    /// to have them create the appropriate datasource parameter for the platform they are implemented for.
    /// </summary>
    [Serializable]
    public class Parameter
    {
        /// <summary>
        /// Gets or sets column name associated with this <see cref="Parameter"/> class.
        /// </summary>
        /// <returns>The column name associated with this <see cref="Parameter"/> class.</returns>
        public string Name { get; set; }

        /// <summary>
        /// Parameter value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the single value or subquery of <see cref="ParameterType"/>. 
        /// </summary>
        /// <returns>The single value or subquery of <see cref="ParameterType"/>.</returns>
        public ParameterType Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class with the specified value and <see cref="ParameterType"/>.
        /// </summary>
        /// <param name="value">Value of the Parameter</param>
        /// <param name="type">The <see cref="ParameterType"/> that defines the single query.</param>
        public Parameter(object value, ParameterType type = null)
        {
            Name = string.Empty;

            Value = GetValue(value);

            Type = type ?? ParameterType.Value;
        }

        private static object GetValue(object value)
        {
            var domainobject = value as IDomainObject;
            if (domainobject != null) value = domainobject.Id;

            var valueobject = value as IValueObject;
            if (valueobject != null) value = valueobject.Value;

            var descriptor = value as Descriptor;
            if (descriptor != null) value = descriptor.Name;

            var enumeration = value as Enum;
            if (enumeration != null) value = enumeration.ToString();

            var enumerable = value as IEnumerable;
            if (enumerable != null && !(value is string) && !(value is byte[])) // exclude byte[] because byte[] is a single value (ex. timestamp).
            {
                value = (from object val in enumerable select GetValue(val)).ToList();
            }

            return value;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Value);
        }
    }
}
