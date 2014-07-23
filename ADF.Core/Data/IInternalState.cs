using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Core.Data
{
    /// <summary>
    /// Implements the all internal state. An internal state object is holds the state of a domain object. 
    /// A domain object itself is stateless. The internal state is a kind of a property bag for the domain object.
    /// </summary>
    public interface IInternalState
    {
        /// <summary>
        /// Gets or sets the new instance of <see cref="Core.Identity.ID"/> structure with the PrimaryKey value of a table.
        /// </summary>
        /// <returns>An instance of <see cref="Core.Identity.ID"/> structure that is the PrimaryKey value of a table.</returns>
        ID ID { get; set; }

        /// <summary>
        /// Gets the status of a table's row is empty or not.
        /// </summary>
        /// <returns>True if the table's row is null; otherwise, false.</returns>
        bool IsEmpty { get; }

        /// <summary>
        /// Gets the changed Adf.Core.InternalStatus and use to 
        /// check the status of Adf.Core.InternalState before the data save into database.
        /// </summary>
        /// <returns>True if the status of Adf.Core.InternalState is New / NewChanged / Changed; otherwise, false.</returns>
        bool IsAltered { get; }
        bool IsNew { get; }

        #region Column methods

        /// <summary>
        /// Determines wether or not the IInternalState has the specified property.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Returns true if the state has the property; otherwise false.</returns>
        bool Has(IColumn property);

        /// <summary>
        /// Get the data of specified <see cref="IColumn"/>.
        /// Also converts the column value by the specified type.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <returns>Converts the column value by the specified type if the value is not empty; otherwise, default value.</returns>
        T Get<T>(IColumn property);

        /// <summary>
        /// Set the specified value to the column.
        /// Here the column is specified <see cref="ColumnDescriber"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to set.</typeparam>
        /// <param name="property">The <see cref="ColumnDescriber"/> used to provides the column name.</param>
        /// <param name="value">The value will set into the column.</param>
        void Set<T>(IColumn property, T value);

        /// <summary>
        /// Get the data for nullable value type.
        /// Provides automatic conversion between a nullable type and its underlying primitive type.
        /// </summary>
        /// <typeparam name="T">The nullable value type of element to get.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Converts the column value into nullable type value if column value is not empty; otherwise, null.</returns>
        /// <exception cref="System.NotSupportedException">The conversion cannot be performed.</exception>
        T? GetNullable<T>(IColumn property) where T : struct;

        /// <summary>
        /// Set the specified nullable value to the column.
        /// Here the column is specified <see cref="IColumn"/>.
        /// </summary>
        /// <typeparam name="T">The nullable value type of element to set.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <param name="value">The nullable value type will set into the column.</param>
        void SetNullable<T>(IColumn property, T? value) where T : struct;

        /// <summary>
        /// Get the data of specified <see cref="IColumn"/>.
        /// Used only for Adf.Core.IValueObject object.
        /// </summary>
        /// <typeparam name="T">The type of element to get.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>An instance of specified type.</returns>
        T GetValue<T>(IColumn property) where T : IValueObject;

        /// <summary>
        /// Set the specified value to the column.
        /// Used only for Adf.Core.IValueObject object.
        /// </summary>
        /// <typeparam name="T">The type of element to set.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <param name="value">The value which will set into the column.</param>
        void SetValue<T>(IColumn property, T value) where T : IValueObject;

        #endregion
    }
}
