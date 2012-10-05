using System;
using Adf.Base.Domain;

namespace Adf.Base.DotNet.Extensions
{
    public static class MoneyExtensions
    {
        public static Money Round(this Money money, int decimals = 2, MidpointRounding midpointRounding = MidpointRounding.ToEven)
        {
            return money.IsEmpty ? money : new Money(decimal.Round(money.Amount.Value, decimals, midpointRounding));
        }
    }
}
