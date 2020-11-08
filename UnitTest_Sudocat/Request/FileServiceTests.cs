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
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111");
            FileRequest fileService = new FileRequest();
            var res = fileService.Upload(@"C:\Users\i\Desktop\dzynb.docx");
            Assert.AreEqual(res.status, 100);
        }
    }
}