using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Base.Data
{
    /// <summary>
    /// Represents the current state of a table with regard to its relationship to the <see cref="System.Collections.Hashtable"/>.
    /// Implementing the internal state of a domain object.
    /// It is a kind of a property bag for the domain object of <see cref="System.Collections.Hashtable"/> type.
    /// </summary>
    public class SimpleInternalState : IInternalState
    {
        private readonly Hashtable data = new Hashtable();

        #region ID

        /// <summary>
        /// A object of <see cref="Core.Identity.ID"/> structure used to find the identity of <see cref="System.Collections.Hashtable"/>.
        /// </summary>
        private ID id;

        /// <summary>
        /// Gets or sets the identity of <see cref="Core.Identity.ID"/> structure with the value of <see cref="System.Collections.Hashtable"/>.
        /// </summary>
        /// <returns>The identity of <see cref="Core.Identity.ID"/> structure with the value of <see cref="System.Collections.Hashtable"/>.</returns>
        public ID ID
        {
            get { return id; }
            set { id = value; }
        }

        #endregion

        #region Internal Status

        /// <summary>
        /// Gets the exception of <see cref="System.NotImplementedException"/>.
        /// Declared for future use.
        /// </summary>
        public InternalStatus InternalStatus
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Empty

        /// <summary>
        /// Gets the record count of a <see cref="System.Collections.Hashtable"/> is 0 or not.
        /// </summary>
        /// <returns>True if the record count of <see cref="System.Collections.Hashtable"/> is 0; otherwise, false.</returns>
        public bool IsEmpty
        {
            get { return (data.Count == 0); }
        }

        /// <summary>
        /// Gets the status of <see cref="System.Collections.Hashtable"/> is false after perform some operations.
        /// </summary>
        /// <returns>The status of <see cref="System.Collections.Hashtable"/> is false.</returns>
        public bool IsAltered
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsNew
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Get & Set

        /// <summary>
        /// Determines wether or not the IInternalState has the specified property.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Returns true if the state has the property; otherwise false.</returns>
        public bool Has(IColumn property)
        {
            return data.ContainsKey(property.ColumnName);
        }

        /// <summary>
        /// Get the data of specified <see cref="IColumn"/>.
        /// Also converts the column value of <see cref="System.Collections.Hashtable"/> by the specified type.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Converts the column value of <see cref="System.Collections.Hashtable"/> by the specified type if the value is not empty; otherwise, default value.</returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public T Get<T>(IColumn property)
        {
            object value = data[property.ColumnName];
            if (value == null) return default(T);

            NullableConverter nullableConverter = new NullableConverter(typeof(T));
            return (T)nullableConverter.ConvertFrom(value);
        }

        /// <summary>
        /// Get the data of specified <see cref="IColumn"/>.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>The column value as it is.</returns>
        public object Get(IColumn property)
        {
            return data[property.ColumnName];
        }

        /// <summary>
        /// Set the specified value to the key of <see cref="System.Collections.Hashtable"/>. 
        /// Here the key is specified <see cref="IColumn"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to set.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the key of <see cref="System.Collections.Hashtable"/>.</param>
        /// <param name="value">The value will set into the key of <see cref="System.Collections.Hashtable"/>.</param>
        public void Set<T>(IColumn property, T value)
        {
            data[property.ColumnName] = value;
        }

        /// <summary>
        /// Get the data for nullable value type.
        /// Provides automatic conversion between a nullable type and its underlying primitive type.
        /// </summary>
        /// <typeparam name="T">The nullable value type of element to get.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Converts the column value into nullable type value.</returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public T? GetNullable<T>(IColumn property) where T : struct
        {
            NullableConverter nullableConverter = new NullableConverter(typeof(T?));

            return (T?)nullableConverter.ConvertFrom(data[property.ColumnName]);
        }

        /// <summary>
        /// Set the specified nullable value to the column.
        /// Here the column is specified <see cref="IColumn"/>.
        /// </summary>
        /// <typeparam name="T">The nullable value type of element to set.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <param name="value">The nullable value type will set into the column if nullable value type has a value; otherwise, null.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public void SetNullable<T>(IColumn property, T? value) where T : struct
        {
            data[property.ColumnName] = value.HasValue ? value : null;
        }

        /// <summary>
        /// Get the data of specified <see cref="ColumnDescriber"/>.
        /// Used only for Adf.Core.IValueObject object.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Converts the column value of <see cref="System.Collections.Hashtable"/> by the specified type if the value is not null; otherwise, default value.</returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public T GetValue<T>(IColumn property) where T : IValueObject
        {
            object value = data[property.ColumnName];
            if (value == null) return default(T);

            NullableConverter nullableConverter = new NullableConverter(typeof(T));
            return (T)nullableConverter.ConvertFrom(value);
        }

        /// <summary>
        /// Set the specified value to the column.
        /// Used only for Adf.Core.IValueObject object.
        /// </summary>
        /// <typeparam name="T">The type of element to set.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <param name="value">The value which will set into the column.</param>
        public void SetValue<T>(IColumn property, T value) where T : IValueObject
        {
            data[property.ColumnName] = value;
        }

        #endregion
    }
}