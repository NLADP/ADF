using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Adf.Base.Validation;
using Adf.Business.SmartReferences;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test.Domain
{
    public static class DomainObjectTestExtensions
    {
        public static void CompareProperties(this IDomainObject domainObject)
        {
            // Fetch all properties using reflection
            PropertyInfo[] propertyInfos = domainObject.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos.Where(p => p.CanRead && p.CanWrite))
            {
                // Read and write some stuff
                object val = GenerateRandomValue(propertyInfo);
                if (val == null) 
                    Console.WriteLine(@"Skipping property {0} because the type {1} is unknown.", propertyInfo.Name, propertyInfo.PropertyType);
                else
                {
                    propertyInfo.SetValue(domainObject, val, null);

                    // Now, read back the value, and check if it's the same
                    object val2 = propertyInfo.GetValue(domainObject, null);
                    Assert.AreEqual(val, val2, "The value inserted and retrieved for property {0} are not the same.", propertyInfo.Name);
                }
            }
        }

        private static object GenerateRandomValue(PropertyInfo propertyInfo)
        {
            object val = null;

            Type t = propertyInfo.PropertyType;
            if (t.IsNullable()) t = Nullable.GetUnderlyingType(t);

            if (t == typeof(string))
            {
                val = GenerateString(propertyInfo);
            }
            else if (t == typeof(int) || t == typeof(uint) || t == typeof(ushort) || t == typeof(short) || t == typeof(long) || t == typeof(ulong))
            {
                val = GenerateNumeric(propertyInfo);
            }
            else if (t == typeof(bool)) val = true;
            else if (t == typeof(DateTime)) val = DateTime.Now.Date;
            else if (typeof(ISmartReference).IsAssignableFrom(t))
            {
                val = GenerateSmartReference(propertyInfo);
            }

            if (val != null && propertyInfo.PropertyType.IsNullable())
            {
                val = new NullableConverter(propertyInfo.PropertyType).ConvertFrom(val);
            }

            return val;
        }

        private static object GenerateSmartReference(PropertyInfo propertyInfo)
        {
            // Found out what type of SmartRef this is
            Type t = propertyInfo.PropertyType;
            Type smartRefType = t.GetGenericArguments()[0];

            return SmartReferenceFactory.GetAll(smartRefType).PickOne();
        }

        private static object GenerateNumeric(PropertyInfo propertyInfo)
        {
            var attrs = (InRangeAttribute[]) propertyInfo.GetCustomAttributes(typeof (InRangeAttribute), false);
            int value = attrs.Length > 0 ? new Random().Next(Convert.ToInt32(attrs[0].Min), Convert.ToInt32(attrs[0].Max)) : 1;

            Type t = propertyInfo.PropertyType;
            if (t.IsNullable()) t = Nullable.GetUnderlyingType(t);

            if (t == typeof(uint)) return (uint) value;
            if (t == typeof(short)) return (short) value;
            if (t == typeof(ushort)) return (ushort) value;
            if (t == typeof(long)) return (long) value;
            if (t == typeof(ulong)) return (ulong) value;

            return value;
        }

        private static string GenerateString(PropertyInfo propertyInfo)
        {
            var attrs = (MaxLengthAttribute[]) propertyInfo.GetCustomAttributes(typeof(MaxLengthAttribute), false);
            return attrs.Length > 0 ? new string('v', attrs[0].Length) : "v";
        }
    }
}
