using Adf.Core.Identity;

namespace Adf.Core.Domain
{
    public interface IDomainHierarchy : IDomainObject
    {
        IDomainHierarchy GetParent();
        IDomainCollection GetChildren();
        ID ParentId { get; }
    }
}
