using Adf.Business.SmartReferences;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Business.Tests
{
    [TestClass]
    public class SmartReferenceTest
    {
        [TestMethod]
        public void GetAll()
        {
            var references = SmartReferenceFactory.GetAll<MaturityLevel>();

            Assert.IsTrue(references.IsInitialised, "Smart references weren't found");
        }
    }
}
