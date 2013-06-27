using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adf.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Core.Tests
{
    [TestClass] 
    public class StringExtensionsTest
    {
        [TestMethod]
        public void CapitalizeNullString()
        {
            string test = null;

            Assert.IsNull(test.Capitalize());
        }

        [TestMethod]
        public void CapitalizeEmptyString()
        {
            Assert.AreEqual("".Capitalize(), "");
        }

        [TestMethod]
        public void CapitalizeLowerCaseString()
        {
            Assert.AreEqual("test".Capitalize(), "Test");
        }
        
        [TestMethod]
        public void CapitalizeUpperCaseString()
        {
            Assert.AreEqual("Test".Capitalize(), "Test");
        }
    
        [TestMethod]
        public void CapitalizeUpperCaseWordString()
        {
            Assert.AreEqual("Test me I'm good".Capitalize(), "Test Me I'm Good");
        }
        
        [TestMethod]
        public void CapitalizeLowerCaseWordString()
        {
            Assert.AreEqual("test me I'm good".Capitalize(), "Test Me I'm Good");
        }
    }
}
