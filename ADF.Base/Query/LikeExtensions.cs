using System;
using Adf.Core;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static class LikeExtensions
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

        public static Q LikeLeft<Q>(this Q query, string value) where Q : IAdfQuery
        {
            return query.CompleteLast(OperatorType.LikeLeft, value);
        }

        public static Q LikeRight<Q>(this Q query, string value) where Q : IAdfQuery
        {
            return query.CompleteLast(OperatorType.LikeRight, value);
        }
    }
}
