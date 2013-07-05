using Adf.Core.Test;
using Adf.Core.Transactions;
using Adf.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test
{
    public abstract class BaseTest 
    {
        private ITransactionScope _transaction;

        [TestInitialize]
        public void Initialize()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
            TestManager.Clear();
            ValidationManager.Clear();

            _transaction = TransactionManager.New();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            OnCleanUp();
        }

        protected virtual void OnCleanUp()
        {
            _transaction.Dispose();
        }
    }
}
