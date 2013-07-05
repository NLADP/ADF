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
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns>Converts the column value by the specified type if the value is not empty; otherwise, default value.</returns>
        T Get<T>(IColumn property);

        /// <summary>
        /// Get the data of specified <see cref="IColumn"/>.
        /// </summary>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <returns></returns>
        object Get(IColumn property);

        /// <summary>
        /// Set the specified value to the column.
        /// Here the column is specified <see cref="IColumn"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to set.</typeparam>
        /// <param name="property">The <see cref="IColumn"/> used to provides the column name.</param>
        /// <param name="value">The value will set into the column.</param>
        void Set<T>(IColumn property, T value);

        #endregion
    }
}
