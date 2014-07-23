using System;
using System.Reflection;
using Adf.Core;

namespace Adf.Base.Data
{
    /// <summary>
    /// Provides functionality to examine types and create <see cref="ColumnDescriber"/> 
    /// objects from each of the fields available in the type and not excluded (<see cref="Adf.Core.ExcludeAttribute"/>).
    /// </summary>
    public class BusinessDescriber
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessDescriber"/> class with no arguments.
        /// </summary>
        protected BusinessDescriber() {}

        /// <summary>
        /// Provide a null value column by the specified type of <see cref="System.Type"/> and column name.
        /// </summary>
        /// <param name="type">The property value of <see cref="System.Type"/> used to create the <see cref="ColumnDescriber"/>.</param>
        /// <param name="name">Column name use to create the <see cref="ColumnDescriber"/>.</param>
        /// <returns>The expected <see cref="ColumnDescriber"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Name is null.</exception>
        /// <exception cref="System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but obj is null.</exception>
        public static ColumnDescriber GetColumn(Type type, string name)
        {
            if (type == null) return null;

            var memberInfos = type.GetMember(name, (BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
            var mi = memberInfos.Length > 0 ? memberInfos[0] : null;

            if (ExcludeAttribute.IsExcluded(mi)) return null;

            var propertyInfo = mi as PropertyInfo;
            if (propertyInfo != null) return propertyInfo.GetValue(null, null) as ColumnDescriber;

            var fieldInfo = mi as FieldInfo;
            if (fieldInfo != null) return fieldInfo.GetValue(null) as ColumnDescriber;

            return null;
        }
    }
}
