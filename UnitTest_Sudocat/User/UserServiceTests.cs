using Client.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Client.User.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            UserService userService = new UserService();
            var res = userService.Login("sudodog", "ssss11111");
            Console.WriteLine(res);
            Assert.AreEqual(res.status, 0);
        }
    }
}