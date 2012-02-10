using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adf.Core;
using Adf.Core.Data;
using Adf.Core.Domain;
using Adf.Core.Query;

namespace Adf.Base.Query
{
    public static partial class IsNotEqualOrEmptyExtensions
    {
        public static readonly OperatorType Operation = OperatorType.IsNotEqualOrIsNull;

        public static Q IsNotEqualOrNull<Q>(this Q query, IDomainObject value) where Q : IAdfQuery
        {
            return query.IsNotEqualOrNull(value.Id);
        }

        public static Q IsNotEqualOrNull<Q>(this Q query, IValueObject value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.Value);
        }

        public static Q IsNotEqualOrNull<Q>(this Q query, Descriptor value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.Name);
        }

        public static Q IsNotEqualOrNull<Q>(this Q query, Enum value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value.ToString());
        }

        public static Q IsNotEqualOrNull<Q>(this Q query, object value) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, value);
        }

        public static Q IsNotEqualOrNull<Q>(this Q query, IColumn column) where Q : IAdfQuery
        {
            return query.CompleteLast(Operation, column, ParameterType.Column);
        }
    }
}
