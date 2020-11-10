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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread fileListThread;
        public MainWindow()
        {

            // 启动任务队列进程
            fileListThread = new Thread(TaskList.run);
            fileListThread.Start();
            

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
            fileListThread.Abort();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserRequest userService = new UserRequest();
            var res = userService.Login("sudodog", "ssss11111");

            for(int i = 1; i <= 100; i++)
            {
                TaskList.Add(new FileControlBlock
                {
                    Operation = OperationType.Upload,
                    FileName = i + ".txt",
                    LocalPath = @"C:\Users\i\Desktop\测试数据\a lot of txt\" + i + ".txt",
                    RemotePath = @"users\sudodog\测试数据\a lot of txt\" + i + ".txt",
                    Status = StatusType.Waiting
                });
            }


            /*
            FileTask.Add(new FCB
            {
                Operation = OperationType.Upload,
                FileName = "1.txt",
                LocalPath = @"F:\软件合集\Adobe CC 2019 SP\1.txt",
                RemotePath = @"软件合集\Adobe CC 2019 SP\1.txt",
                Status = 0
            });

            FileTask.Add(new FCB
            {
                Operation = OperationType.Upload,
                FileName = "2.txt",
                LocalPath = @"F:\软件合集\Adobe CC 2019 SP\2.txt",
                RemotePath = @"软件合集\Adobe CC 2019 SP\2.txt",
                Status = 0
            });

            FileTask.Add(new FCB
            {
                Operation = OperationType.Upload,
                FileName = "3.txt",
                LocalPath = @"F:\软件合集\Adobe CC 2019 SP\3.txt",
                RemotePath = @"软件合集\Adobe CC 2019 SP\3.txt",
                Status = 0
            });
            */
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Convert.ToInt64(tbkey.Text)
            TaskList.SetStatus(0, StatusType.RequestPause);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TaskList.SetStatus(0, StatusType.RequestRusume);
        }
    }
}