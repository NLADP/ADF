using Adf.Core.Objects;
using Adf.Core.Query;

namespace Adf.Core.Transactions
{
    public static class TransactionManager
    {
        public static ITransactionScope New()
        {
            return ObjectFactory.BuildUp<ITransactionScope>();
        }
    }
}
