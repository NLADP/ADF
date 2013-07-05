using System.Transactions;
using Adf.Core.Transactions;

namespace Adf.Base.Transactions
{
    public class TransactionScopeProvider : ITransactionScope
    {
        private TransactionScope _transactionScope;

        public TransactionScopeProvider()
        {
            var options = new TransactionOptions {IsolationLevel = IsolationLevel.ReadCommitted};

            _transactionScope = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public void Complete()
        {
            _transactionScope.Complete();
        }

        public void Dispose()
        {
            _transactionScope.Dispose();
        }
    }
}
