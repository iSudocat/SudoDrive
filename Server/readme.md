# 概述

# 目录
```
│
│  appsettings.json                 普通的配置文件
│  appsettings.Development.json     普通的开发模式配置文件
│
├─Controllers       WEB API 接口定义
├─Exceptions        异常定义
├─Middlewares       中间件
├─Models            数据模型
│   ├─Entity
│   ├─DTO
│   └─VO        
└─Services          业务逻辑定义
    └─Implements
```

# 调试
1. 将 `appsettings-example.json` 复制一份副本并命名为 `appsettings.json`。
2. 将 `appsettings-example.Development.json` 复制一份副本并命名为 `appsettings.Development.json`。
3. 将上述两个文件中的 `tokenManagement.secret` 修改为符合 SHA256 JWT Secret 规则的字符串。