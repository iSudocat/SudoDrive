using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Request.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        [TestMethod()]
        public void LoginTest()
        {
            UserRequest userService = new UserRequest();
            var status = userService.Login("sudodog", "ssss11111", out LoginResponse res);
            Console.WriteLine(res);
            Assert.AreEqual(status, 0);
        }

        [TestMethod()]
        public void refreshTokenTest()
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);
            var res = userService.refreshToken();
            Assert.AreEqual(res, 0);
        }
    }
}