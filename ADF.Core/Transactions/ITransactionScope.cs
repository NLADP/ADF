using System;

namespace Adf.Core.Transactions
{
    public interface ITransactionScope : IDisposable
    {
        void Complete();
    }
}
