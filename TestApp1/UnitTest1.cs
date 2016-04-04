using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace TestApp1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            String expected = "hello worl";
            String actual = "hello world";
            Assert.AreEqual(expected, actual);
        }
    }
}
