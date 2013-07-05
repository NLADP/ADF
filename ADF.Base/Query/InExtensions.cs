using System;
using System.Collections;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static partial class InExtensions
    {
        public static readonly OperatorType Operation = OperatorType.In;
        public static readonly OperatorType NotInOperation = OperatorType.NotIn;

        public static Q In<Q>(this Q query, IAdfQuery subquery) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, subquery, ParameterType.Query);
        }

        public static Q In<Q>(this Q query, IEnumerable values) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, values, ParameterType.ValueList);
        }

        public static Q NotIn<Q>(this Q query, IAdfQuery subquery) where Q : IAdfQuery
        {
            return query.CompleteLast(NotInOperation, subquery, ParameterType.Query);
        }

        public static Q NotIn<Q>(this Q query, IEnumerable values) where Q : IAdfQuery
        {
            return query.CompleteLast(NotInOperation, values, ParameterType.ValueList);
        }

        public static Q IsNull<Q>(this Q query) where Q : IAdfQuery
        {
            return query.CompleteLast(OperatorType.IsNull, null);
        }

        public static Q IsNotNull<Q>(this Q query) where Q : IAdfQuery
        {
            return query.CompleteLast(OperatorType.IsNotNull, null);
        }
    }
}
