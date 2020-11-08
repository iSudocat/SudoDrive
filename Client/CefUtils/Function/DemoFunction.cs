using CefSharp.Web;
using Client.CefUtils.VO;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client.CefUtils.Function
{
    public class DemoFunction
    {
        public double Add(int a, int b)
        {
            return a + b;
        }
    }
}