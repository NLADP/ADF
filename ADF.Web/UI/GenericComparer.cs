using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Generic class to compare objects based on a property (sortexpression) and direction
    /// Example usage (sort a generic list of Course objects):
    /// List&lt;Course&gt; courselist = new List&lt;Course&gt;(courses);
    /// courselist.Sort(new GenericComparer&lt;Course&gt;("Name", SortDirection.Ascending));
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericComparer<T> : IComparer<T>
    {
        private string sortExpression;
        private SortDirection sortDirection;

        /// <summary>
        /// Provides the initialization of sorting direction (Ascending or descending) and expression on which the sorting will work.
        /// </summary>
        /// <param name="sortExpression">The expression on which the sorting will work.</param>
        /// <param name="sortDirection">Sorting direction (Ascending or descending)</param>
        public GenericComparer(string sortExpression, SortDirection sortDirection)
        {
            this.sortExpression = sortExpression;
            this.sortDirection = sortDirection;
        }

        /// <summary>
        /// Provides the comparison result depending upon SortDirection.
        /// </summary>
        /// <param name="x">An object to compare.</param>
        /// <param name="y">An object to compare.</param>
        /// <returns>A 32-bit signed integer that indicates the relative order of the objects
        ///     being compared.The return value has these meanings: Value Meaning Less than
        ///     zero This instance is less than obj. Zero This instance is equal to obj.
        ///     Greater than zero This instance is greater than obj
        /// </returns>
        public int Compare(T x, T y)
        {
            PropertyInfo propertyInfo = typeof (T).GetProperty(sortExpression);
            IComparable obj1 = (IComparable) propertyInfo.GetValue(x, null);
            IComparable obj2 = (IComparable) propertyInfo.GetValue(y, null);

            if (sortDirection == SortDirection.Ascending)
                return obj1.CompareTo(obj2);
            else 
                return obj2.CompareTo(obj1);
        }
    }
}