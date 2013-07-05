using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static class ExpressionExtensions
    {

        public static Q Max<Q>(this Q query, IColumn column, string alias = null) where Q : IAdfQuery
        {
            query.Selects.Add(new Expression { Column = column, Type = ExpressionType.Max, Alias = alias ?? column.ColumnName });

            return query;
        }

        public static Q Min<Q>(this Q query, IColumn column, string alias = null) where Q : IAdfQuery
        {
            query.Selects.Add(new Expression { Column = column, Type = ExpressionType.Min, Alias = alias ?? column.ColumnName });

            return query;
        }

        public static Q Average<Q>(this Q query, IColumn column, string alias = null) where Q : IAdfQuery
        {
            query.Selects.Add(new Expression { Column = column, Type = ExpressionType.Average, Alias = alias ?? column.ColumnName });

            return query;
        }

        public static Q Count<Q>(this Q query, IColumn column, string alias = null) where Q : IAdfQuery
        {
            query.Selects.Add(new Expression { Column = column, Type = ExpressionType.Count, Alias = alias ?? column.ColumnName });
            
            return query;
        }

        public static Q Sum<Q>(this Q query, IColumn column, string alias = null) where Q : IAdfQuery
        {
            query.Selects.Add(new Expression { Column = column, Type = ExpressionType.Sum, Alias = alias ?? column.ColumnName });
            
            return query;
        }
    }
}
