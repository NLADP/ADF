using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class DomainCollection<T> : KeyedCollection<ID, T>, IDomainCollection where T : IDomainObject
    {
        private readonly Collection<T> removed = new Collection<T>();

        ///<summary>
        /// Constr
        ///</summary>
        public DomainCollection(IEnumerable<T> collection = null)
        {
            if (collection != null) { Initialize(collection); }
        }

        public DomainCollection(params T[] p)
        {
            if (p != null) Initialize(p.ToList());
        }

        /// <summary>
        /// When implemented in a derived class, extracts the key from the specified element.
        /// </summary>
        /// <param name="item">The element from which to extract the key.</param>
        /// <returns>The key for the specified element.</returns>
        protected override ID GetKeyForItem(T item)
        {
            return item == null ? IdManager.Empty() : item.Id;
        }

        /// <summary>
        /// Returns whether at least one of the domain objects in the collections is altered.
        /// </summary>
        public bool IsAltered
        {
            get
            {
                return this.Any(domainobject => domainobject.IsAltered);
            }
        }

        /// <summary>
        /// Returns whether at least one of the domain objects in the collections had been removed.
        /// </summary>
        public bool HasBeenRemoved
        {
            get
            {
                return removed != null && removed.Count > 0;
            }
        }

        /// <summary>
        /// Adds the objects of the supplied list to the end of the instance of Collection.
        /// </summary>
        /// <param name="list">The list.</param>
        public void Initialize(IEnumerable<T> list)
        {
            AddRange(list);

            IsInitialised = true;
        }

        /// <summary>
        /// Gets or sets whether the Collection is initialized or not.
        /// </summary>
        public bool IsInitialised { get; private set; }

        public void Reset()
        {
            Clear();

            IsInitialised = false;
        }

        /// <summary>
        /// Saves the Collection.
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            var succeeded = this.Aggregate(true, (current, item) => current & item.Save());

            succeeded = removed.Aggregate(succeeded, (current, item) => current & item.Remove());

            removed.Clear();

            return succeeded;
        }

        /// <summary>
        /// Deletes all items in this collection.
        /// </summary>
        /// <returns></returns>
        public bool RemoveAll()
        {
            foreach (var item in Items.ToList())
            {
                RemoveItem(item);
            }

            return Save();
        }

        /// <summary>
        /// Adds the objects of the supplied list to the end of the Collection.
        /// </summary>
        /// <param name="list">The list.</param>
        public DomainCollection<T> AddRange(IEnumerable<T> list)
        {
            if (list == null) return this;

            foreach (var item in list)
                Add(item);

            IsInitialised = true;

            return this;
        }

        /// <summary>
        /// Sets or adds the item to the Collection.
        /// </summary>
        /// <param name="item">The item.</param>
        public DomainCollection<T> AddItem(T item)
        {
            if (Contains(item))
            {
                SetItem(IndexOf(item), item);
            }
            else
            {
                Add(item);
            }
            return this;
        }

        /// <summary>
        /// Removes the supplied item from the Collection.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True if the removal is successful, false otherwise.</returns>
        public bool RemoveItem(T item)
        {
            if (Contains(item))
            {
                removed.Add(item);

                return Remove(item);
            }

            return true;
        }

        /// <summary>
        /// Sorts the list using the supplied comparer.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        protected virtual void Sort(IComparer<T> comparer)
        {
            var list = Items;

            list.Sort(comparer);
        }

        /// <summary>
        /// Sorts the list using the supplied property and <see cref="SortOrder"/>.
        /// </summary>
        /// <param name="sortProperty">The property.</param>
        /// <param name="order">The <see cref="SortOrder"/>.</param>
        public void Sort(string sortProperty, SortOrder order)
        {
            Sort(new ListSorter<T>(sortProperty, order));
        }

        /// <summary>
        /// Gets a IList&lt;T&gt; wrapper around the System.Collections.ObjectModel.Collection&lt;T&gt;.
        /// </summary>
        public new List<T> Items
        {
            get
            {
                return (List<T>)base.Items;
            }
        }

    }
}
