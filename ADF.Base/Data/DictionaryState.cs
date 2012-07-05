using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Identity;

namespace Adf.Base.Data
{
    /// <summary>
    /// Represents the current state of a data source implementing the internal state of a domain object.
    /// It is a kind of a property bag for the domain object.
    /// </summary>
    [Serializable]
    public class DictionaryState : Dictionary<IColumn, object>, IInternalState, INotifyPropertyChanged
    {
        public DictionaryState() {}

        #region Serialization

        // construstor used for deserialization
        protected DictionaryState(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            IsAltered = info.GetBoolean("IsAltered");
            IsNew = info.GetBoolean("IsNew");
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("IsAltered", IsAltered);
            info.AddValue("IsNew", IsNew);
        }

        #endregion Serialization

        #region ID

        /// <summary>
        /// Gets or sets the identity of <see cref="Core.Identity.ID"/> structure.
        /// </summary>
        /// <returns>The identity of <see cref="Core.Identity.ID"/> structure.</returns>
        public ID ID
        {
            get { return IdManager.New(this[PrimaryKey]); }
            set { this[PrimaryKey] = value.Value; }
        }

        private IColumn _primaryKey;
        internal IColumn PrimaryKey
        {
            get { return _primaryKey ?? (_primaryKey = Keys.FirstOrDefault(c => c.IsIdentity) ?? Keys.First(c => c.ColumnName.Equals("Id", StringComparison.OrdinalIgnoreCase))); }
        }

        #endregion

        #region Status 

        /// <summary>
        /// Gets the record count of a <see cref="System.Collections.Hashtable"/> is 0 or not.
        /// </summary>
        /// <returns>True if the record count of <see cref="System.Collections.Hashtable"/> is 0; otherwise, false.</returns>
        public bool IsEmpty
        {
            get { return (Count == 0); }
        }

        /// <summary>
        /// Gets the status of <see cref="System.Collections.Hashtable"/> is false after perform some operations.
        /// </summary>
        /// <returns>The status of <see cref="System.Collections.Hashtable"/> is false.</returns>
        public bool IsAltered { get; protected internal set; }

        public bool IsNew { get; protected internal set; }

        #endregion

        #region Get & Set

        /// <summary>
        /// Get the data of specified <see cref="ColumnDescriber"/>.
        /// Also converts the column value by the specified type.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>Converts the column value by the specified type if the value is not empty; otherwise, default value.</returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public T Get<T>(IColumn property)
        {
            object value;

            TryGetValue(property, out value);

            return (value == null) ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Set the specified value to the key. 
        /// Here the key is specified <see cref="ColumnDescriber"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the key.</param>
        /// <param name="value">The value will set into the key.</param>
        public void Set<T>(IColumn property, T value)
        {
            if (!PropertyHelper.IsEqual(Get<T>(property), value))
            {
                this[property] = value;

                NotifyChange(property);
            }
        }

//        private static bool IsEqual<T>(object original, T newValue) 
//        {
//            if (ReferenceEquals(original, newValue)) return true;
//
//            if (original == null || newValue == null) return false;
//
//            var equatable = newValue as IEquatable<T>;
//
//            if (equatable != null)
//            {
//                return equatable.Equals((T)original);
//            }
//            return false;   // sorry, can't compare
//        }

        /// <summary>
        /// Get the data for nullable value type.
        /// Provides automatic conversion between a nullable type and its underlying primitive type.
        /// </summary>
        /// <typeparam name="T">The nullable value type of element to get.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>Converts the column value into nullable type value.</returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public T? GetNullable<T>(IColumn property) where T : struct
        {
            object value;

           TryGetValue(property, out value);

            if (value == null) return null;

            var nullableConverter = new NullableConverter(typeof(T?));

            return (T?)nullableConverter.ConvertFrom(value);
        }

        /// <summary>
        /// Set the specified nullable value to the column.
        /// Here the column is specified <see cref="ColumnDescriber"/>.
        /// </summary>
        /// <typeparam name="T">The nullable value type of element to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The nullable value type will set into the column if nullable value type has a value; otherwise, null.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public void SetNullable<T>(IColumn property, T? value) where T : struct
        {
            if (!PropertyHelper.IsEqual(GetNullable<T>(property), value))
            {
                this[property] = value;

                NotifyChange(property);
            }
        }

        /// <summary>
        /// Get the data of specified <see cref="ColumnDescriber"/>.
        /// Used only for Adf.Core.IValueObject object.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>Converts the column value by the specified type if the value is not null; otherwise, default value.</returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public T GetValue<T>(IColumn property) where T : IValueObject
        {
            object value;

            TryGetValue(property, out value);

            return value == null ? default(T) : typeof(T).New<T>(value);
        }

        /// <summary>
        /// Set the specified value to the column.
        /// Used only for Adf.Core.IValueObject object.
        /// </summary>
        /// <typeparam name="T">The type of element to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The value which will set into the column.</param>
        public void SetValue<T>(IColumn property, T value) where T : IValueObject
        {
            if (!PropertyHelper.IsEqual(GetValue<T>(property), value))
            {
                this[property] = value.IsEmpty ? null : value.Value;

                NotifyChange(property);
            }
        }

        #endregion

        private void NotifyChange(IColumn property)
        {
            IsAltered = true;

            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property.ColumnName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}