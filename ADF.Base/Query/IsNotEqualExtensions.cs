using System;
using System.Linq;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static partial class IsNotEqualExtensions
    {
        public static readonly OperatorType Operation = OperatorType.IsNotEqual;

        public static Q IsNotEqual<Q>(this Q query, IDomainObject value) where Q : IAdfQuery
        {
            return query.IsNotEqual(value.Id);
        }

        public static Q IsNotEqual<Q>(this Q query, IValueObject value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.Value);
        }

        public static Q IsNotEqual<Q>(this Q query, Descriptor value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.Name);
        }

        public static Q IsNotEqual<Q>(this Q query, Enum value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.ToString());
        }

        public static Q IsNotEqual<Q>(this Q query, object value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value);
        }

        public static Q IsNotEqual<Q>(this Q query, IColumn column) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, column, ParameterType.Column);
        }

        public static Q IsNotEqual<Q>(this Q q, IAdfQuery query) where Q : IAdfQuery
        {
            return q.CompleteLast(Operation, query, ParameterType.Query);
        }

        public static Q IsFalse<Q>(this Q query) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, false);
        }
    }
}
