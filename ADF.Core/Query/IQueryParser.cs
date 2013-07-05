using Adf.Core.Data;

namespace Adf.Core.Query
{
    public interface IQueryParser
    {
        DataSourceType Type { get;  }
        string Parse(IAdfQuery query);
    }
}
