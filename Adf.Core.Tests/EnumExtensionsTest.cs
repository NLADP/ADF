using System;
using System.Reflection;
using Adf.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Core.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class EnumExtensionsTest
    {
        [TestMethod]
        public void IsIn()
        {
            Assert.IsTrue(BindingFlags.Default.IsIn(BindingFlags.DeclaredOnly, BindingFlags.Default, BindingFlags.CreateInstance));
        }

        [TestMethod]
        public void IsNotIn()
        {
            Assert.IsFalse(BindingFlags.ExactBinding.IsIn(BindingFlags.DeclaredOnly, BindingFlags.Default, BindingFlags.CreateInstance));
        }        
        
        [TestMethod]
        public void WrongType()
        {
            Assert.IsFalse(DayOfWeek.Friday.IsIn(BindingFlags.DeclaredOnly, BindingFlags.Default, BindingFlags.CreateInstance));
        }
    }
}
