using Adf.Core.Domain;

namespace Adf.Base.Domain
{
    public static class DomainHierarchyExtensions
    {
        /// <summary>
        /// This method checks whether the parent of the target refers to the target as one of its parents (recursive). 
        /// If so, you are creating a circular reference.
        /// </summary>
        /// <param name="target">Object to check.</param>
        /// <returns>True if a circular reference is created, false if not.</returns>
        public static bool HasCircularParent(this IDomainHierarchy target)
        {
            return HasCircularParent(target.GetParent(), target);
        }

        private static bool HasCircularParent(this IDomainHierarchy parent, IDomainHierarchy originaltarget)
        {
            var domain = parent as IDomainObject;
            if (domain.IsNullOrEmpty()) return false;

            return parent.Equals(originaltarget) || HasCircularParent(parent.GetParent(), originaltarget);
        }
    }
}
