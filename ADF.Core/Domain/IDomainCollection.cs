using System.Collections;

namespace Adf.Core.Domain
{
    // todo: make IDomainCollection generic <IDomainObject>

    /// <summary>
    /// Defines methods that a value type or class implements to save, sort the the collection of 
    /// DomainObjects.
    /// </summary>
    public interface IDomainCollection : ICollection
    {
        /// <summary>
        /// Gets a value indicating whether the collection of DomainObjects is altered.
        /// </summary>
        /// <returns>
        /// true if the collection of DomainObjects is altered; otherwise, false.
        /// </returns>
        bool IsAltered { get; }

        /// <summary>
        /// Gets a value indicating whether the collection of DomainObjects has been removed.
        /// </summary>
        /// <returns>
        /// true if the collection of DomainObjects has been removed; otherwise, false.
        /// </returns>
        bool HasRemovedItems { get; }

        /// <summary>
        /// Saves the collection of DomainObjects and returns a value indicating whether the 
        /// collection of DomainObjects is successfully saved.
        /// </summary>
        /// <returns>
        /// true if the collection of DomainObjects is successfully saved, false otherwise.
        /// </returns>
        bool Save();

        /// <summary>
        /// Deletes all items in the collection.
        /// </summary>
        /// <returns></returns>
        IDomainCollection RemoveAll();

        /// <summary>
        /// Sorts the collection of DomainObjects on the specified property according to the 
        /// specified <see cref="SortOrder"/>.
        /// </summary>
        /// <param name="sortProperty">The property on which the sorting will be done.</param>
        /// <param name="order">The <see cref="SortOrder"/> according to which the sorting will be done.</param>
        IDomainCollection Sort(string sortProperty, SortOrder order);
    }


    public interface IDomainCollection<out T> : IDomainCollection where T : IDomainObject
    {

    }
}
