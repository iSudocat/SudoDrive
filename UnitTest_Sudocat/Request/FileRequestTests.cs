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
        public void GetFileListTest()
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111");
            FileRequest fileRequest = new FileRequest();
            fileRequest.GetFileList("/users/sudodog/测试数据/a lot of txt", 
                out int status, out List<Response.FileListResponse.File> fileList);
            Console.WriteLine("文件数： " + fileList.Count);
            Assert.AreEqual(fileList.Count, 463);

        }
    }
}