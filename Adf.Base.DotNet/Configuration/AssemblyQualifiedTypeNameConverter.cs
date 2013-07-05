using System;
using System.ComponentModel;
using System.Configuration;

namespace Adf.Base.Configuration
{
    /// <summary>
    /// Represents a configuration converter that converts a string to <see cref="Type"/> based on a fully qualified name.
    /// </summary>
    public class AssemblyQualifiedTypeNameConverter : ConfigurationConverterBase
    {
        /// <summary>
        /// Returns the assembly qualified name for the passed in Type.
        /// </summary>
        /// <param name="context">The container representing this System.ComponentModel.TypeDescriptor.</param>
        /// <param name="culture">Culture info for assembly</param>
        /// <param name="value">Value to convert.</param>
        /// <param name="destinationType">Type to convert to.</param>
        /// <returns>Assembly Qualified Name as a string</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            Type typeValue = value as Type;
            if (typeValue == null)
            {
                throw new ArgumentException(string.Format(DotNet.Properties.Resources.Culture, DotNet.Properties.Resources.ExceptionCanNotConvertType, typeof(Type).Name));
            }

            return (typeValue).AssemblyQualifiedName;
        }

        /// <summary>
        /// Returns a type based on the assembly qualified name passed in as data.
        /// </summary>
        /// <param name="context">The container representing this System.ComponentModel.TypeDescriptor.</param>
        /// <param name="culture">Culture info for assembly.</param>
        /// <param name="value">Data to convert.</param>
        /// <returns>Type of the data</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string stringValue = (string)value;
            Type result = String.IsNullOrEmpty(stringValue) ? null : Type.GetType(stringValue, false);
            if (result == null)
            {
                throw new ArgumentException(string.Format(DotNet.Properties.Resources.Culture, DotNet.Properties.Resources.ExceptionInvalidType, stringValue));
            }

            return result;
        }
    }
}
