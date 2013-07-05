using System.Linq;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static class PredicateExtensions
    {
        public static Q And<Q>(this Q query, IColumn column) where Q : IAdfQuery
        {
            query.Wheres.Add(new Where { Column = column, Predicate = PredicateType.And });

            return query;
        }

        public static Q Or<Q>(this Q query, IColumn column) where Q : IAdfQuery
        {
            query.Wheres.Add(new Where { Column = column, Predicate = PredicateType.Or });

            return query;
        }

        public static Q Top<Q>(this Q query, int top) where Q : IAdfQuery
        {
            query.Top = top;

            return query;
        }

        public static Q TimeOut<Q>(this Q query, int timeout) where Q : IAdfQuery
        {
            query.TimeOut = timeout;

            return query;
        }

        public static Q Where<Q>(this Q query, IColumn column) where Q : IAdfQuery
        {
            return query.And(column);
        }

        public static Q OpenBracket<Q>(this Q query, int count = 1) where Q : IAdfQuery
        {
            IWhere w = query.Wheres.Last();

            w.OpenBracket += count;

            return query;
        }

        public static Q CloseBracket<Q>(this Q query, int count = 1) where Q : IAdfQuery
        {
            IWhere w = query.Wheres.Last();

            w.CloseBracket += count;

            return query;
        }

        public static Q CompleteLast<Q>(this Q query, OperatorType operation, object value, ParameterType parameterType = null) where Q : IAdfQuery
        {
            IWhere w = query.Wheres.Last();

            w.Operator = operation;
            w.Parameter = new Parameter(value, parameterType);

            return query;
        }
    }
}
