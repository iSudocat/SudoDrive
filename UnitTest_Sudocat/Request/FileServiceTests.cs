using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.Request;


namespace Client.Request.Tests
{
    [TestClass()]
    public class FileServiceTests
    {
        [TestMethod()]
        public void UploadTest()
        {
            UserService userService = new UserService();
            userService.Login("sudodog", "ssss11111");
            FileService fileService = new FileService();
            var res = fileService.Upload(@"C:\Users\i\Desktop\dzynb.docx");
            Assert.AreEqual(res.status, 100);
        }
    }
}