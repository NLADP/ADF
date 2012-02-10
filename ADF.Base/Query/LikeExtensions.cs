using System;
using System.Linq;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static partial class LikeExtensions
    {
        public static readonly OperatorType Operation = OperatorType.Like;

        public static Q Like<Q>(this Q query, Enum value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.ToString());
        }

        public static Q Like<Q>(this Q query, string value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value);
        }

        public static Q Like<Q>(this Q query, Descriptor value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.Name);
        }

        public static Q Left<Q>(this Q query) where Q : IAdfQuery
        {
            var last = query.Wheres.Last();

            if (last.Operator == OperatorType.Like) last.Operator = OperatorType.LikeLeft;
            else throw new InvalidOperationException("Applying a Left Like on a " + last.Operator);

            return query;
        }

        public static Q Right<Q>(this Q query) where Q : IAdfQuery
        {
            var last = query.Wheres.Last();

            if (last.Operator == OperatorType.Like) last.Operator = OperatorType.LikeRight;
            else throw new InvalidOperationException("Applying a Left Like on a " + last.Operator);

            return query;
        }
    }
}
