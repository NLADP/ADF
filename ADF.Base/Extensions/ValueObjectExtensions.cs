using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Base.Domain;

namespace Adf.Base.Extensions
{
    public static class ValueObjectExtensions
    {
        public static Money Sum(this IEnumerable<Money> source)
        {
            if (source == null) throw new ArgumentNullException("source");

            Money sum = 0;
            foreach (Money v in source) sum += v;
            return sum;
        }

        public static Money Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Money> selector)
        {
            return Sum(source.Select(selector));
        }

        public static Money Abs(this Money money)
        {
            if (money.IsEmpty) return money;

            var absvalue = Math.Abs(money.Amount.Value);

            return new Money(absvalue);
        }

        public static int Sign(this Money money)
        {
            return money.IsEmpty ? 0 : Math.Sign(money.Amount.Value);
        }
    }
}
