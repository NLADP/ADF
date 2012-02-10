using Adf.Core.Data;

namespace Adf.Core.Query
{
    public interface IJoin
    {
        JoinType Type { get; set; }
        IColumn SourceColumn { get; set; }
        IColumn JoinColumn { get; set; }
    }
}
