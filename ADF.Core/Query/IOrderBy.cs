using Adf.Core.Data;
using Adf.Core.Domain;

namespace Adf.Core.Query
{
    public interface IOrderBy
    {
        IColumn Column { get; set; }
        SortOrder SortOrder { get; set; }
    }
}
