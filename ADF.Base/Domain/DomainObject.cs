using System;
using Adf.Base.Data;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Identity;
using Adf.Core.Types;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Represents base DomainObject. Provides functionalities to manage a DomainObject.
    /// </summary>
    [Serializable]
    public abstract class DomainObject : IDomainObject
    {
        /// <summary>
        /// The state of this instance.
        /// </summary>
        protected IInternalState state = NullInternalState.Null;

        /// <summary>
        /// Returns the state of this instance.
        /// </summary>
        /// <returns>The state of this instance.</returns>
        public IInternalState GetState()
        {
            return state;
        }

        #region InternalState

        /// <summary>
        /// Gets the <see cref="ID"/> of this instance.
        /// </summary>
        /// <value>The <see cref="ID"/> of this instance.</value>
        public virtual ID Id
        {
            get { return state.ID; }
        }

        /// <summary>
        /// Is the domain object altered, that is NewChanged or Changed?
        /// </summary>
        public bool IsAltered
        {
            get { return state.IsAltered; }
        }

        public bool IsNew
        {
            get { return state.IsNew; }
        }

        #endregion InternalState

        #region Empty

        /// <summary>
        /// Gets whether this instance is empty or not.
        /// </summary>
        public virtual bool IsEmpty
        {
            get { return state.IsEmpty; }
        }

        #endregion

        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value></value>
        public virtual string Title
        {
            get { return GetType().Name; }
        }

        /// <summary>
        /// Overrides the default ToString method of <see cref="object"/> and Returns the Title property of the <seealso cref="DomainObject"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Title ?? string.Empty;
        }

        #region Operators

        /// <summary>
        /// Checks whether a business class has an identical <see href="ID"/> as the current one.
        /// </summary>
        /// <param name="domainObject">Instance of a domain object to compare to the current one.</param>
        /// <returns>True if both instance have the samen ID</returns>
        public bool IsEqual(DomainObject domainObject)
        {
            if (domainObject == null)
                return false;

            return (Id == domainObject.Id);
        }

        /// <summary>
        /// Checks whether both <seealso cref="DomainObject"/> objects have an identical <see href="ID"/>
        /// </summary>
        /// <param name="x">First DomainObject.</param>
        /// <param name="y">Second DomainObject.</param>
        /// <returns></returns>
        public static bool operator ==(DomainObject x, DomainObject y)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(x, y))
                return true;

            // If one is null, but not both, return false.
            if (((object) x == null) || ((object) y == null))
                return false;

            if (x.GetType() != y.GetType()) return false;

            return (x.Id == y.Id);
        }

        /// <summary>
        /// Checks whether both <seealso cref="DomainObject"/> objects are not identical.
        /// </summary>
        /// <param name="x">I.</param>
        /// <param name="y">J.</param>
        /// <returns></returns>
        public static bool operator !=(DomainObject x, DomainObject y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Returns the hash code of this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Equalses the specified <seealso cref="DomainObject"/>. It calls and returns the == operator.
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var other = obj as DomainObject;

            if (other == null) return false;

            return this == other;
        }

        #endregion

        #region IComparable

        /// <summary>
        /// Compares an <see cref="DomainObject"/> to the supplied object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>A 32-bit signed integer. Value less than zero indicates that the 
        /// <see cref="DomainObject"/> is less than the supplied object. Value zero indicates that the 
        /// <see cref="DomainObject"/> is equal to the supplied object. Value greater than zero 
        /// indicates that the <see cref="DomainObject"/> is greater than the supplied object.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            var other = obj as DomainObject;

            if (other == null) throw new ArgumentException("Object is not an valid domain object");

            return (Title ?? string.Empty).CompareTo(other.Title);
        }

        #endregion IComparable

        #region Get & Set

        public T Get<T>(IColumn column)
        {
            return Converter.To<T>(state.Get<T>(column));
        }

        public void Set<T>(IColumn column, T value)
        {
            if (!PropertyHelper.IsEqual(Get<T>(column), value)) state.Set(column, Converter.ToPrimitive(value));
        }

        #endregion Get & Set
    }
}
