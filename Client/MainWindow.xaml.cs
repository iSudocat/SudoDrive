using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CefSharp;
using CefSharp.Wpf;
using Client.CefUtils.Function;
using Client.CefUtils.Scheme;
using Client.Request;
using Client.TencentCos;
using Client.TencentCos.Task;
using Client.TencentCos.Task.List;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread uploadTask, downloadTask;
        public MainWindow()
        {

            // 启动任务队列进程
            uploadTask = new Thread(UploadTaskList.run);
            uploadTask.Start();

            downloadTask = new Thread(DownloadTaskList.run);
            downloadTask.Start();

#if DEBUG
            var settings = new CefSettings()
            {
                // 开启调试窗口，在普通浏览器中打开这个页面
                RemoteDebuggingPort = 8088
            };

            Cef.Initialize(settings);
#else
            // 设置自定义协议
            var settings = new CefSettings();
            settings.RegisterScheme(new CefCustomScheme()
            {
                SchemeName = CefSharpSchemeHandlerFactory.SchemeName,
                SchemeHandlerFactory = new CefSharpSchemeHandlerFactory(),
                IsLocal = true,
                IsSecure = true
            });
            Cef.Initialize(settings);
#endif

            // 初始化控件
            InitializeComponent();

            // 设置自定义 JavaScript 函数
            // 异步调用示例
            browser.JavascriptObjectRepository.ResolveObject += (sender, e) =>
            {
                var repo = e.ObjectRepository;
                if (e.ObjectName == "cloudFileFunction")
                {
                    //Binding options is an optional param, defaults to null
                    BindingOptions bindingOptions = null;
                    //Use the default binder to serialize values into complex objects, CamelCaseJavascriptNames = true is the default
                    bindingOptions = BindingOptions.DefaultBinder;
                    repo.Register("cloudFileFunction", new CloudFileFunction(), isAsync: true, options: bindingOptions);
                }
                if (e.ObjectName == "fileFunction")
                {
                    //Binding options is an optional param, defaults to null
                    BindingOptions bindingOptions = null;
                    //Use the default binder to serialize values into complex objects, CamelCaseJavascriptNames = true is the default
                    bindingOptions = BindingOptions.DefaultBinder;
                    repo.Register("fileFunction", new FileFunction(), isAsync: true, options: bindingOptions);
                }
            };

#if DEBUG
            browser.Address = "http://localhost:9528/";
#else
            browser.Address = "sudodrive://index.html";
#endif

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            uploadTask.Abort();
            downloadTask.Abort();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);

            for (int i = 301; i <= 2333; i++)
            {
                UploadTaskList.Add(new FileControlBlock
                {
                    FileName = i + ".txt",
                    LocalPath = @"C:\Users\i\Desktop\测试数据\a lot of txt",
                    RemotePath = @"users\sudodog\测试数据\a lot of txt",
                    Status = StatusType.Waiting
                });
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Convert.ToInt64(tbkey.Text)
            UploadTaskList.SetStatus(0, StatusType.RequestPause);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UploadTaskList.SetStatus(0, StatusType.RequestRusume);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);

            DownloadTaskList.Add(new FileControlBlock
            {
                FileName = "Thomas Greenberg - Hopeful Hearts.mp3",
                Guid = "3d04e25d-5a27-4fc2-a2c3-3d17f262df8d",
                LocalPath = @"C:\Users\i\Desktop",
                Status = StatusType.Waiting
            });
        }
    }
}