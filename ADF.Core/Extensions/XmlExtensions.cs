using System;
using System.Linq;
using System.Xml.Linq;

namespace Adf.Core.Extensions
{
    public static class XmlExtensions
    {
        public static string GetAttributeOrDefault(this XElement element, string attribute, string defaultValue = null)
        {
            defaultValue = defaultValue ?? string.Empty;

            var a = element.Attribute(attribute);
            if (a == null) return defaultValue;

            return a.Value.IsNullOrEmpty() ? defaultValue : a.Value;
        }

        public static T GetAttributeOrDefault<T>(this XElement element, string attribute, T defaultValue = default(T))
        {
            var value = element.GetAttributeOrDefault(attribute);

            return value.IsNullOrEmpty() ? defaultValue : (T) Convert.ChangeType(value, typeof(T));
        }

        public static T GetAttributeFromDescriptorOrDefault<T>(this XElement element, string attribute) where T : Descriptor
        {
            var value = element.GetAttributeOrDefault(attribute);

            return value.IsNullOrEmpty() ? Descriptor.GetDefault<T>() : Descriptor.Parse<T>(value);
        }
    }
}
