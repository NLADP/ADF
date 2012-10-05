using Adf.Core.Test;
using Adf.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test
{
    public abstract class BaseTest 
    {
        [TestInitialize]
        public void Initialize()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
            TestManager.Clear();
            ValidationManager.Clear();
        }

        [TestCleanup]
        public void CleanupTest()
        {
            OnCleanup();
        }

        protected virtual void OnCleanup()
        {
            
        }
    }
}
