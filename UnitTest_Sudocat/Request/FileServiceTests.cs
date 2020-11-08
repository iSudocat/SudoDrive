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
            var res = fileService.Upload(@"C:\Users\i\Desktop\5896f7d1d7bc0.png");
            var status = fileService.ConfirmUpload(res.data.file.id, res.data.file.guid);
            Assert.AreEqual(status, 0);
        }
    }
}