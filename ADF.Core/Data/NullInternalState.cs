using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Identity;
using Adf.Core.Types;

namespace Adf.Core.Data
{
    /// <summary>
    /// Implements the all internal state with null value. It holds the null state of a domain object. 
    /// It is a kind of a property bag for the null domain object.
    /// </summary>
//    [Serializable]
    public class NullInternalState : IInternalState
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NullInternalState"/> class with no arguments.
        /// </summary>
        public static readonly IInternalState Null = new NullInternalState();

        //        /// <summary>
        //        /// Creates a new array instance of the <see cref="NullInternalState"/> class with array size 0.
        //        /// </summary>
        //        public static readonly NullInternalState[] NullArray = new NullInternalState[0];

        /// <summary>
        /// Creates an empty list if <see cref="NullInternalState"/>  
        /// </summary>
        public static readonly IEnumerable<IInternalState> NullList = (IEnumerable<IInternalState>) Enumerable.Empty<NullInternalState>();

        /// <summary>
        /// Determines wether or not the IInternalState has the specified property.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Returns true if the state has the property; otherwise false.</returns>
        public bool Has(IColumn property)
        {
            return false;
        }

        /// <summary>
        /// Get the default data of specified type.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>Converts the specified type into default value.</returns>
        public T Get<T>(IColumn property)
        {
            return default(T);
        }

        /// <summary>
        /// Get null for the specified <see cref="IColumn"/>.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Always returns null.</returns>
        public object Get(IColumn property)
        {
            return null;
        }

        /// <summary>
        /// Gets or sets the new instance of <see cref="Core.Identity.ID"/> structure with empty value.
        /// </summary>
        /// <returns>An instance of <see cref="Core.Identity.ID"/> structure with empty value.</returns>
        public ID ID
        {
            get { return IdManager.Empty(); }
            set { }
        }

        /// <summary>
        /// Gets the undefined status of <see cref="Core.Domain.InternalStatus"/>.
        /// </summary>
        /// <returns>A <see cref="Core.Domain.InternalStatus"/> with undefined status.</returns>
        public InternalStatus InternalStatus
        {
            get { return InternalStatus.Undefined; }
        }

        /// <summary>
        /// Gets status of empty row. 
        /// Returns only if row's value is not empty.
        /// </summary>
        /// <returns>True if row's value is not empty.</returns>
        public bool IsEmpty
        {
            get { return true; }
        }

        /// <summary>
        /// Gets changed status of Adf.Core.InternalStatus. Returns only false.
        /// </summary>
        /// <returns>The status of Adf.Core.InternalState is false.</returns>
        public bool IsAltered
        {
            get { return false; }
        }

        public bool IsNew
        {
            get { return false; }
        }

        /// <summary>
        /// Declared for future use.
        /// </summary>
        /// <typeparam name="T">The type of element to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The value which will set into the column.</param>
        public void Set<T>(IColumn property, T value)
        {
            //Do nothing
        }

        /// <summary>
        /// Declared for future use.
        /// </summary>
        /// <typeparam name="T">The type of element to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The value which will set into the column.</param>
        public void SetNullable<T>(IColumn property, T? value) where T : struct
        {
            //Do nothing
        }

        /// <summary>
        /// Declared for future use.
        /// </summary>
        /// <typeparam name="T">The type of element to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The value which will set into the column.</param>
        public void SetValue<T>(IColumn property, T value) where T : IValueObject
        {
            //Do nothing
        }
    }
}
