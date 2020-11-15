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
    public class FileRequestTests
    {
        [TestMethod()]
        public void UploadTest()
        {
            /*
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111");
            FileRequest fileRequest = new FileRequest();
            var res = fileRequest.Upload(@"C:\Users\i\Desktop\5896f7d1d7bc0.png");
            var status = fileService.ConfirmUpload(res.data.file.id, res.data.file.guid);
            Assert.AreEqual(status, 0);
            */
        }

        [TestMethod()]
        public void GetFileListTest()
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);
            FileRequest fileRequest = new FileRequest();
            fileRequest.GetFileList("/users/sudodog/测试数据/a lot of txt",
                out int status, out List<Response.FileListResponse.File> fileList);
            Console.WriteLine("文件数： " + fileList.Count);
            Assert.AreEqual(fileList.Count, 463);

        }

        [TestMethod()]
        public void DownloadTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);
            FileRequest fileRequest = new FileRequest();
            //int status = fileRequest.Delete(out _, "/users/sudodog/测试数据/test", "");
            int status = fileRequest.Delete(out _, "", "/users/sudodog/测试数据/test/delTest.txt");
            Assert.AreEqual(status, 0);
        }

        [TestMethod()]
        public void NewFolderTest()
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);
            FileRequest fileRequest = new FileRequest();
            int status = fileRequest.NewFolder("/users/sudodog/defg", out _);
            Assert.AreEqual(status, 101);
        }

        [TestMethod()]
        public void SearchFileTest()
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);
            FileRequest fileRequest = new FileRequest();
            fileRequest.SearchFile("1",
                out _, out List<Response.FileListResponse.File> fileList);
            Console.WriteLine("文件数： " + fileList.Count);
            Assert.AreEqual(fileList.Count, 140);

        }
    }
}