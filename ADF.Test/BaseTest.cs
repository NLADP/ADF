using System;
using Adf.Core.Tasks;
using Adf.Core.Test;
using Adf.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test
{
    public class BaseTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestManager.Clear();
            ValidationManager.Clear();
        }
    }

    [TestClass]
    public class BaseTest<T>  where T : ITask
    {
        protected T Task;

        [TestInitialize]
        public void Initialize()
        {
            TestManager.Clear();
            ValidationManager.Clear();

            Task = (T)Activator.CreateInstance(typeof(T), ApplicationTask.Main, null);
        }
    }
}
