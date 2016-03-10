using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using GryphonSecurity_v2_1.Domain.Entity;
using GryphonSecurity_v2_1;

namespace TestApp3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            
        }
        [TestMethod]
        public void TestGetCreateUser()
        {
            Controller control = Controller.Instance;
            User expectedUser = new User("Mike", "Heerwagen", "Kollegiebakken 9", 41836990, "masas", "123");
            Boolean actualTrue = control.createUser(expectedUser);
            Boolean expectedTrue = true;
            Assert.AreEqual(expectedTrue, actualTrue);
            User acutalUser = control.getUser();
            Assert.AreEqual(expectedUser.Firstname, acutalUser.Firstname);

        }
    }
}
