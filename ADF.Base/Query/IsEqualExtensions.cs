using System;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Query;
using System.Linq;

namespace Adf.Base.Query
{
    public static partial class IsEqualExtensions
    {
        public static readonly OperatorType Operation = OperatorType.IsEqual;

        public static Q IsEqual<Q>(this Q query, IDomainObject value) where Q : IAdfQuery
        {
            return query.IsEqual(value.Id);
        }

        public static Q IsEqual<Q>(this Q query, IValueObject value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.Value);
        }

        public static Q IsEqual<Q>(this Q query, Descriptor value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.Name);
        }

        public static Q IsEqual<Q>(this Q query, Enum value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.ToString());
        }

        public static Q IsEqual<Q>(this Q query, object value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value);
        }

        public static Q IsEqual<Q>(this Q query, IColumn column) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, column, ParameterType.Column);
        }

        public static Q IsEqualWhenNotEmpty<Q>(this Q query, IDomainObject value) where Q : IAdfQuery
        {
            return (value.IsEmpty) ? query : query.IsEqualWhenNotEmpty(value.Id);
        }

        public static Q IsEqualWhenNotEmpty<Q>(this Q query, IValueObject value) where Q : IAdfQuery
        {
            if (!value.IsEmpty)
            {
                query.CompleteLast(Operation, value.Value);
            }
            else
            {
                query.Wheres.Remove(query.Wheres.Last());
            }
            return query;
        }

        public static Q IsEqualWhenNotEmpty<Q>(this Q query, int? value) where Q : IAdfQuery
        {
            if (value.HasValue)
            {
                query.CompleteLast(Operation, value.Value);
            }
            else
            {
                query.Wheres.Remove(query.Wheres.Last());
            }
            return query;
        }

        public static Q IsTrue<Q>(this Q query) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, true);
        }
    }
}
