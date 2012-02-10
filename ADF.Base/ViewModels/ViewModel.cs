using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Base.ViewModels
{
    public class ViewModel<T> : IDomainObject where T : IDomainObject
    {
        public T DomainObject { get; protected set; }

        public ViewModel(T domainobject)
        {
            DomainObject = domainobject;
        }

        public virtual ID Id { get { return DomainObject.Id; } }

        public virtual string Title { get { return DomainObject.Title; } }

        public bool IsEmpty { get { return DomainObject.IsEmpty; } }

        public bool IsAltered { get { return DomainObject.IsAltered; } }

        #region IComparable

        public int CompareTo(object obj)
        {
            return DomainObject.CompareTo(obj);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ViewModel<T>);
        }

        public bool Equals(ViewModel<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DomainObject.Equals(other.DomainObject);
        }

        public override int GetHashCode()
        {
            return DomainObject.GetHashCode();
        }

        #endregion
    }
}
