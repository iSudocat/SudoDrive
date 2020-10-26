using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace Client.CefUtils.Scheme
{
    class CefSharpSchemeHandlerFactory : ISchemeHandlerFactory
    {
        public static string SchemeName { get; set; } = "sudodrive";
        public static string webroot { get; set; } = "webroot";

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            var uri = request.Url;

            // 删除协议部分
            uri = uri.Substring(SchemeName.Length + 3);

            // 删除多余的斜线
            uri = uri.Trim('/');

            // 合并路径
            var fileName = Path.Combine(Environment.CurrentDirectory, webroot, uri);

            // 检查路径信息
            if (Directory.Exists(fileName))
            {
                fileName = Path.Combine(fileName, "index.html");
            };

            return ResourceHandler.FromFilePath(fileName);
        }

    }
}
