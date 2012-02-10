using System.Collections.Generic;
using Adf.Core.Data;

namespace Adf.Core.Query
{
    public interface IAdfQuery
    {
        int? Top { get; set; }
        int? TimeOut { get; set; }
        bool? Distinct { get; set; }

        List<IExpression> Selects { get; }
        
        QueryType QueryType { get; set; }
        
        List<ITable> Tables { get; }
        
        List<IJoin> Joins { get; }
        
        List<IWhere> Wheres { get; }

        List<IOrderBy> OrderBys { get; }

        List<IExpression> GroupBys { get; }
    }
}
