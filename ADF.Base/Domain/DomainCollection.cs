using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Adf.Core;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Represents collection of type <see cref="DomainObject"/>s.
    /// Provides functionality to Add, Remove, Sort and Save objects of type <see cref="DomainObject"/>. 
    /// </summary>
    /// <typeparam name="T">The Type of <see cref="DomainObject"/>s.</typeparam>
    [Serializable]
    public class DomainCollection<T> : KeyedCollection<ID, T>, IDomainCollection<T> where T : class, IDomainObject
    {
        #region Constructors

        public DomainCollection(IEnumerable<T> items = null)
        {
            Add(items);
        }

        public DomainCollection(params T[] items)
        {
            foreach (var item in items) Add(item);
        }

        #endregion Constructors

        #region KeyedCollection

        /// <summary>
        /// When implemented in a derived class, extracts the key from the specified element.
        /// </summary>
        /// <param name="item">The element from which to extract the key.</param>
        /// <returns>The key for the specified element.</returns>
        protected override ID GetKeyForItem(T item)
        {
            return item == null ? IdManager.Empty() : item.Id;
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);

            IsInitialised = true;
        }

        protected override void RemoveItem(int index)
        {
            removeditems.Add(Items[index]);

            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            removeditems.Clear();

            IsInitialised = false;
        }

        #endregion KeyedCollection

        #region Statusses

        /// <summary>
        /// Gets or sets whether the Collection is initialized or not.
        /// </summary>
        public bool IsInitialised { get; private set; }
        
        /// <summary>
        /// Returns whether at least one of the domain objects in the collections is altered.
        /// </summary>
        public bool IsAltered
        {
            get { return this.Any(domainobject => domainobject.IsAltered); }
        }

        /// <summary>
        /// Returns whether at least one of the domain objects in the collections had been removed.
        /// </summary>
        public bool HasRemovedItems
        {
            get { return removeditems != null && removeditems.Count > 0; }
        }

        #endregion Statusses

        #region Adds and Updates

        /// <summary>
        /// Adds the objects to the collection. 
        /// If one of the items was already present in the collection, this method will throw an exception.
        /// </summary>
        /// <param name="list">A list of items to add to the collection.</param>
        public DomainCollection<T> Add(IEnumerable<T> list)
        {
            if (list == null) return this;

            foreach (var item in list) Add(item);

            IsInitialised = true;

            return this;
        }

        /// <summary>
        /// Sets or adds the item to the Collection.
        /// </summary>
        /// <param name="item">The item.</param>
        public DomainCollection<T> Update(T item)
        {
            var index = IndexOf(item);

            if (index >= 0)
            {
                SetItem(index, item);
            }
            else
            {
                Add(item);
            }

            return this;
        }

        public DomainCollection<T> Update(IEnumerable<T> items)
        {
            foreach (var item in items) Update(item);

            IsInitialised = true;

            return this;
        }

        #endregion Adds and Updates

        #region Remove

        private Collection<T> _removeditems;
        private Collection<T> removeditems
        {
            get { return _removeditems ?? (_removeditems = new Collection<T>()); }
        }

        /// <summary>
        /// Deletes all items in this collection.
        /// </summary>
        /// <returns></returns>
        public IDomainCollection RemoveAll()
        {
            foreach (var item in Items.ToList()) Remove(item);

            return this;
        }

        #endregion Remove

        #region Save

        /// <summary>
        /// Saves the Collection.
        /// </summary>
        /// <returns></returns>
        public virtual bool Save()
        {
            var succeeded = this.Aggregate(true, (current, item) => current & item.Save());

            succeeded = removeditems.Aggregate(succeeded, (current, item) => current & item.Remove());

            removeditems.Clear();

            return succeeded;
        }

        #endregion Save

        #region Sorting

        /// <summary>
        /// Sorts the list using the supplied comparer.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        protected virtual DomainCollection<T> Sort(IComparer<T> comparer)
        {
            Items.Sort(comparer);

            return this;
        }

        /// <summary>
        /// Sorts the list using the supplied property and <see cref="SortOrder"/>.
        /// </summary>
        /// <param name="sortProperty">The property.</param>
        /// <param name="order">The <see cref="SortOrder"/>.</param>
        public IDomainCollection Sort(string sortProperty, SortOrder order)
        {
            return Sort(new ListSorter<T>(sortProperty, order));
        }

        /// <summary>
        /// Sorts the list using the supplied property and <see cref="SortOrder"/>.
        /// </summary>
        /// <param name="sortProperty">The property.</param>
        /// <param name="order">The <see cref="SortOrder"/>.</param>
        public DomainCollection<T> Sort(Expression<Func<T, object>> sortProperty, SortOrder order)
        {
            return Sort(new ListSorter<T>(sortProperty, order));
        }

        /// <summary>
        /// Gets a IList&lt;T&gt; wrapper around the System.Collections.ObjectModel.Collection&lt;T&gt;.
        /// </summary>
        protected internal new List<T> Items
        {
            get { return (List<T>)base.Items; }
        }

        #endregion Sorting
    }
}
