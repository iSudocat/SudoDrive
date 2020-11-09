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
            #region 临时测试用
            CosConfig.Bucket = "sudodrive-1251910132";
            CosConfig.Region = "ap-chengdu";
            #endregion

            fileListThread = new Thread(FileTask.run);
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


            FileTask.Add(new FCB
            {
                Operation = OperationType.Upload,
                FileName = "王玥昊,郑国周 - 午后柠檬树下的阳光.mp3",
                LocalPath = @"F:\CloudMusic\王玥昊,郑国周 - 午后柠檬树下的阳光.mp3",
                RemotePath = @"users\sudodog\CloudMusic\王玥昊,郑国周 - 午后柠檬树下的阳光.mp3",
                Status = 0
            });
        }
    }
}