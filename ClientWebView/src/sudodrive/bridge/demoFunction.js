let demoFunction;

if (typeof(CefSharp) === "undefined") {
    // 如果不运行在 CEF 环境下，我们需要模拟一个环境让 nodejs 认为我们这个操作是正常的

    demoFunction = {};
    demoFunction.add = async function(a, b){
        // 函数体写不写无所谓
        return a + b;
    }
} else {
    // 如果运行在 CEF 环境
    window.CefSharp.BindObjectAsync("demoFunction");

    demoFunction = window.demoFunction;
}

export default demoFunction;