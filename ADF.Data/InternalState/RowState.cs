using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Identity;
using Adf.Base.Types;
using Adf.Core.Types;

namespace Adf.Data.InternalState
{
    /// <summary>
    /// Represents the current state of a table's row with regard to its relationship to the <see cref="System.Data.DataRow"/>.
    /// Implementing the internal state of a domain object.
    /// Also it creates generic list of <see cref="RowState"/> and <see cref="System.Data.DataSet"/> from a <see cref="System.Data.DataRow"/>.
    /// </summary>
    public class RowState : IInternalState
    {
        private readonly DataRow row;

        #region InternalStatus

        private InternalStatus status = InternalStatus.Undefined;

        /// <summary>
        /// Gets the changed status of Adf.Core.InternalStatus and use to 
        /// check the status of Adf.Core.InternalState before the data save into database.
        /// </summary>
        /// <returns>True if the status of Adf.Core.InternalState is New / NewChanged / Changed; otherwise, false.</returns>
        public bool IsAltered
        {
            get { return status.IsAltered; }
        }

        public bool IsNew
        {
            get { return status.IsNew; }
        }

        /// <summary>
        /// Commits all the changes made to this DataRow since the last time <see cref="System.Data.DataRow.AcceptChanges()"/> was called.
        /// </summary>
        /// <exception cref="System.Data.RowNotInTableException">The row does not belong to the table.</exception>
        public void AcceptChanges()
        {
            row.AcceptChanges();

            ChangeStatus(InternalStatus.Ok);
        }

        /// <summary>
        /// Parse the specified status of <see cref="System.Data.DataRowState"/> into <see cref="Core.Domain.InternalStatus"/>.
        /// </summary>
        /// <param name="state">The <see cref="System.Data.DataRowState"/> which will be changed.</param>
        /// <returns>Sends the expected status of <see cref="Core.Domain.InternalStatus"/>.</returns>
        private static InternalStatus ParseStatus(DataRowState state)
        {
            if (state == DataRowState.Added || state == DataRowState.Detached)
                return InternalStatus.New;

            if (state == DataRowState.Deleted)
                return InternalStatus.Removed;

            if (state == DataRowState.Modified)
                return InternalStatus.Changed;

            return InternalStatus.Ok;
        }

        public void ChangeStatus(InternalStatus desired)
        {
            status = status.DetermineStatus(desired);
        }

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="RowState"/> class with the specified <see cref="System.Data.DataRow"/>.
        /// </summary>
        /// <param name="datarow">The <see cref="System.Data.DataRow"/> to give the <see cref="System.Data.DataRowState"/> value.</param>
        private RowState(DataRow datarow)
        {
            if (datarow != null)
            {
                InternalStatus desired = ParseStatus(datarow.RowState);
                ChangeStatus(desired);
            }

            row = datarow;
        }

        /// <summary>
        /// Create a new instance of the <see cref="RowState"/> class with the specified <see cref="System.Data.DataRow"/>.
        /// </summary>
        /// <param name="datarow">The <see cref="System.Data.DataRow"/> to give the <see cref="System.Data.DataRowState"/> value.</param>
        /// <returns>A new instance of the <see cref="RowState"/> class.</returns>
        public static RowState New(DataRow datarow)
        {
            return new RowState(datarow);
        }

        /// <summary>
        /// Adds new instance of the <see cref="RowState"/> class by the 
        /// <see cref="System.Data.DataRow"/> values of specified <see cref="System.Data.DataTable"/>
        /// and creates a <see cref="System.Collections.Generic.List&lt;RowState&gt;"/>.
        /// </summary>
        /// <param name="datatable">The <see cref="System.Data.DataRow"/> values of <see cref="System.Data.DataTable"/> help to create the instancec of <see cref="RowState"/>.</param>
        /// <returns>Create <see cref="System.Collections.Generic.List&lt;RowState&gt;"/> if the specified <see cref="System.Data.DataTable"/> is not null; otherwise, return null.</returns>
        public static List<RowState> Fill(DataTable datatable)
        {
            return datatable == null ? null : (from DataRow row in datatable.Rows select new RowState(row)).ToList();
        }

        /// <summary> 
        /// Returns a <see cref="System.Data.DataRow"/>. 
        /// </summary> 
        /// <returns>The <see cref="System.Data.DataRow"/> if <see cref="RowState"/> is not empty; otherwise, null.</returns> 
        public DataRow BuildUpDataRow()
        {
            return IsEmpty ? null : row;
        }  

        #endregion

        #region Empty

        /// <summary>
        /// Create a new instance of the <see cref="RowState"/> class with the null value.
        /// </summary>
        public static readonly RowState Empty = new RowState(null);

        /// <summary>
        /// Gets the existance of a <see cref="System.Data.DataRow"/> is found or not.
        /// </summary>
        /// <returns>True if the <see cref="System.Data.DataRow"/> is null; otherwise, false.</returns>
        public bool IsEmpty
        {
            get { return (row == null); }
        }

        #endregion

        #region Keys

        /// <summary>
        /// Gets the name of primary key from a <see cref="System.Data.DataRow"/>.
        /// </summary>
        /// <returns>"ID" if the length of primary key is less than 1; otherwise, name of primary key.</returns>
        public string PrimaryKey
        {
            get
            {
                if (row.Table.PrimaryKey.Length < 1) return "ID";

                return row.Table.PrimaryKey[0].ColumnName;
            }
        }

        /// <summary>
        /// Gets or sets the new instance of <see cref="Core.Identity.ID"/> structure with the PrimaryKey value of <see cref="System.Data.DataRow"/>.
        /// </summary>
        /// <returns>An instance of <see cref="Core.Identity.ID"/> structure that is the PrimaryKey value of <see cref="System.Data.DataRow"/>.</returns>
        public ID ID
        {
            get { return IdManager.New(row[PrimaryKey]); }
            set { row[PrimaryKey] = value.Value; }
        }

        #endregion

        #region Cell Methods

        /// <summary>
        /// Provide a method to get the column's value of <see cref="System.Data.DataRow"/> by the specified column name.
        /// </summary>
        /// <param name="column">The column name of a <see cref="System.Data.DataRow"/>.</param>
        /// <returns>Column value of <see cref="System.Data.DataRow"/> in the form of <see cref="System.Object"/>.</returns>
        protected virtual object GetCell(string column)
        {
            return row[column];
        }

        /// <summary>
        /// Provide a method to set the column value of <see cref="System.Data.DataRow"/> by the specified column name and value.
        /// Also set the changed status of <see cref="Core.Domain.InternalStatus"/>.
        /// </summary>
        /// <param name="column">The column name of a <see cref="System.Data.DataRow"/></param>
        /// <param name="value">The <see cref="System.Object"/> value will set into the column.</param>
        protected virtual void SetCell(string column, object value)
        {
            row[column] = value;

            status = status.DetermineStatus(InternalStatus.Changed);
        }

        /// <summary>
        /// Provide a method to get the column's value of <see cref="System.Data.DataRow"/> by the specified <see cref="ColumnDescriber"/>.
        /// </summary>
        /// <param name="column">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>Column value of <see cref="System.Data.DataRow"/> in the form of <see cref="System.Object"/>.</returns>
        protected virtual object GetCell(IColumn column)
        {
            return GetCell(column.ColumnName);
        }

        /// <summary>
        /// Provide a method to set the column value of <see cref="System.Data.DataRow"/> by the specified <see cref="ColumnDescriber"/> and value.
        /// </summary>
        /// <param name="column">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The <see cref="System.Object"/> value will set into the column.</param>
        protected virtual void SetCell(IColumn column, object value)
        {
            SetCell(column.ColumnName, value);
        }

        #endregion

        #region Get & Set Methods

        /// <summary>
        /// Get the data of specified <see cref="ColumnDescriber"/>.
        /// Also converts the column value of <see cref="System.Data.DataRow"/> by the specified type.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>Converts the column value of <see cref="System.Data.DataRow"/> by the specified type if the value is not empty; otherwise, default value.</returns>
        public T Get<T>(IColumn property)
        {
            object value = GetCell(property);

            return (value is DBNull) ? default(T) : (T)value;
        }

        /// <summary>
        /// Set the specified value to the column of <see cref="System.Data.DataRow"/>.
        /// Here the column is specified <see cref="ColumnDescriber"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The value will set into the column.</param>
        public void Set<T>(IColumn property, T value)
        {
            if (!IsEqual(GetCell(property), value))
            {
                SetCell(property, value == null ? (object) DBNull.Value : value);
            }
        }

        private static bool IsEqual<T>(object dbValue, T? objectValue) where T : struct
        {
            return objectValue.HasValue 
                ? IsEqual(dbValue, objectValue.Value) 
                : IsEqual<object>(dbValue, null);
        }

        private static bool IsEqual(IValueObject originalValue, IValueObject newValue)
        {
            if (originalValue.IsEmpty && newValue.IsEmpty) return true;

            if (originalValue.IsEmpty || newValue.IsEmpty) return false;

            return originalValue.Equals(newValue);
        }

        private static bool IsEqual<T>(object dbValue, T objectValue)
        {
            if (dbValue == DBNull.Value) dbValue = null;

            if (ReferenceEquals(dbValue, objectValue)) return true;

            if (dbValue == null || objectValue == null) return false;

            var equatable = objectValue as IEquatable<T>;

            if (equatable != null)
            {
                return equatable.Equals((T) dbValue);
            }
            return false;   // sorry, can't compare
        }

        /// <summary>
        /// Get the data for nullable value type.
        /// Provides automatic conversion between a nullable type and its underlying primitive type.
        /// </summary>
        /// <typeparam name="T">The nullable value type of element to get.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>Converts the column value of <see cref="System.Data.DataRow"/> into nullable type value 
        /// if column value of <see cref="System.Data.DataRow"/> is not empty; otherwise, null.</returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        public T? GetNullable<T>(IColumn property) where T : struct
        {
            object value = GetCell(property);

            if (value is DBNull) return null;

            var nullableConverter = new NullableConverter(typeof(T?));

            return (T?)nullableConverter.ConvertFrom(value);
        }

        /// <summary>
        /// Set the specified nullable value to the column of <see cref="System.Data.DataRow"/>.
        /// Here the column is specified <see cref="ColumnDescriber"/>.
        /// </summary>
        /// <typeparam name="T">The nullable value type of element to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The nullable value type will set into the column.</param>
        public void SetNullable<T>(IColumn property, T? value) where T : struct
        {
            if (!IsEqual(GetCell(property), value))
            {
                SetCell(property, value.HasValue ? value.Value : (object)DBNull.Value);
            }
        }

        /// <summary>
        /// Get the data of specified <see cref="ColumnDescriber"/>.
        /// Used only for Adf.Core.IValueObject object.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>An instance of specified type.</returns>
        public T GetValue<T>(IColumn property) where T : IValueObject
        {
            var value = GetCell(property);

            if (value is DBNull) return default(T);

            return typeof(T).New<T>(value);
        }

        /// <summary>
        /// Set the specified value to the column of <see cref="System.Data.DataRow"/>.
        /// Used only for Adf.Core.IValueObject object.
        /// </summary>
        /// <typeparam name="T">The type of element to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The value which will set into the column.</param>
        public void SetValue<T>(IColumn property, T value) where T : IValueObject
        {
            if (!IsEqual(GetValue<T>(property), (IValueObject)value))
            {
                SetCell(property, value.IsEmpty ? (object)DBNull.Value : value.Value);
            }
        }

        #endregion
    }
}
