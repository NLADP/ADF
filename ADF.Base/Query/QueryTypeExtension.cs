using System.Linq;
using Adf.Base.Data;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static class QueryTypeExtension
    {
        public static Q SetType<Q>(this Q query, QueryType type) where Q : IAdfQuery
        {
            query.QueryType = type;

            return query;
        }

        public static Q Select<Q>(this Q query) where Q : IAdfQuery
        {
            return SetType(query, QueryType.Select);
        }

        public static Q Select<Q>(this Q query, params IExpression[] expressions) where Q : IAdfQuery
        {
            if (expressions != null)
            {
                query.Selects.AddRange(expressions);
            }
            return query.Select();
        }

        public static Q Select<Q>(this Q query, params IColumn[] columns) where Q : IAdfQuery
        {
            if (columns != null)
            {
                query.Select(columns.Select(c => new Expression {Column = c, Type = ExpressionType.Column }).ToArray());
            }
            return query;
        }

        public static Q Select<Q>(this Q query, ITable table) where Q : IAdfQuery
        {
            query.Selects.Add(new Expression { Column = new ColumnDescriber("*", table), Type = ExpressionType.Table });
            return query;
        }

        public static Q Distinct<Q>(this Q query) where Q : IAdfQuery
        {
            query.Distinct = true;

            return query;
        }

        public static Q Count<Q>(this Q query) where Q : IAdfQuery
        {
            return SetType(query, QueryType.Count);
        }

        public static Q StoredProcedure<Q>(this Q query, ITable storedProcedure) where Q : IAdfQuery
        {
            return query
                .SetType(QueryType.StoredProcedure)
                .From(storedProcedure);
        }
    }
}
