using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static class GroupByExtensions
    {
        public static Q GroupBy<Q>(this Q query, IColumn column) where Q : IAdfQuery
        {
            query.GroupBys.Add(new Expression {Column = column, Type = ExpressionType.Column});

            return query;
        }
    }
}
