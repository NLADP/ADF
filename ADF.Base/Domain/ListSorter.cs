using System;
using System.Collections.Generic;
using Adf.Core.Domain;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Represents helper class that sorts a list of a particular type of objects.
    /// Provides method to compare two objects of a particular type.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public class ListSorter<T> : IComparer<T>
    {
        private readonly string property;
        private readonly SortOrder order = SortOrder.Ascending;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListSorter{T}"/> class with the 
        /// specified property and <see cref="SortOrder"/>.
		/// </summary>
		/// <param name="property">The property on which the sorting will be done.</param>
        /// <param name="order">The <see cref="SortOrder"/> according to which the sorting will be done.</param>
		public ListSorter(string property, SortOrder order)
		{
			this.property = property;
			this.order = order;
		}

        /// <summary>
        /// Compares two specified objects of the same type.
        /// </summary>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        /// <returns>
        /// A 32-bit signed integer. Returns less than zero, zero or greater than zero 
        /// depending on the first object is less than, equal to or greater than the second object 
        /// respectively.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// x is null.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// y is null.
        /// </exception>
        /// <exception cref="System.NullReferenceException">
        /// Object reference not set to an instance of an object.
        /// </exception>
        public int Compare(T x, T y)
        {
            var xvalue = PropertyHelper.GetValue(x, property) as IComparable;
            var yvalue = PropertyHelper.GetValue(y, property) as IComparable;
            
            if (xvalue == null && yvalue == null) return 0;

            if (xvalue == null) return (order == SortOrder.Ascending) ? -1 : 1;
            if (yvalue == null) return (order == SortOrder.Ascending) ? 1 : -1;

            return (order == SortOrder.Ascending) ? xvalue.CompareTo(yvalue) : yvalue.CompareTo(xvalue);
        }
    }
}
