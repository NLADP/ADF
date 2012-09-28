using System;
using System.Reflection;

namespace Adf.Core.Data
{
    /// <summary>
    /// Provides functionality to examine types and create <see cref="ColumnDescriber"/> 
    /// objects from each of the fields available in the type and not excluded (<see cref="Adf.Core.ExcludeAttribute"/>).
    /// </summary>
    public class BusinessDescriber
    {

        /// <summary>
        /// Creates a new instance of the <see cref="BusinessDescriber"/> class.
        /// </summary>
        public static readonly BusinessDescriber Empty = new BusinessDescriber();

        /// <summary>
        /// Gets the instance of <see cref="BusinessDescriber"/> is empty or not.
        /// </summary>
        /// <returns>True if the instance of <see cref="BusinessDescriber"/> is empty; otherwise, false.</returns>
        public bool IsEmpty
        {
            get { return this == Empty; }
        }

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
        public static IColumn GetColumn(Type type, string name)
        {
            if (type == null) return ColumnDescriber.Empty;

            ColumnDescriber describer = ColumnDescriber.Empty;

            var memberInfos = type.GetMember(name, (BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy));
            var mi = memberInfos.Length > 0 ? memberInfos[0] : null;

            if (!ExcludeAttribute.IsExcluded(mi))
            {
                var propertyInfo = mi as PropertyInfo;
                var fieldInfo = mi as FieldInfo;

                if (propertyInfo != null)
                {
                    describer = propertyInfo.GetValue(null, null) as ColumnDescriber;
                }
                else if (fieldInfo != null)
                {
                    describer = fieldInfo.GetValue(null) as ColumnDescriber;
                }
            }

            return describer;
        }
    }
}
