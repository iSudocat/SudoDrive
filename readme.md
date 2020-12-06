# 概述
    SudoDrive 云盘
# 模块
- Client
    - 该模块主要用于调用 CEF 以加载本程序的界面。
- ClientWebView
    - 该模块主要实现用户界面。
    
# 客户端调试
1. 以 Debug 模式运行 ClientWebView 项目。保证 NodeJS 调试 web 服务器端口为 `8080` 。
2. 以 Debug 模式运行 Client 项目。在 `MainWindow.xaml.cs` 中定义了 NodeJS 调试 web 服务器端口为 `8080` 。
3. 使用浏览器打开 `http://localhost:8088/` 访问 CEF 调试工具。

# 服务端调试
- 见 [/Server](/Server)

# 客户端发布
- 择日再写

# 服务端发布
- 择日再写

# 项目分工
- lyx：前后端对接中的C#部分，COS请求部分
- lhr：后端总体设计与编写
- dzy：前端总体设计，前后端对接中的JS部分
- zzt：前端页面编写
- wjh：后端用户组管理部分
- gyj：后端用户管理部分
