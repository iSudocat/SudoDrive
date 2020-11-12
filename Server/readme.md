# 概述

# 目录
```
│
│  appsettings.json                 普通的配置文件
│  appsettings.Development.json     普通的开发模式配置文件
│
├─Controllers       WEB API 接口定义
├─Exceptions        异常定义
├─Libraries         基本库
├─Migrations        数据库迁移
├─Middlewares       中间件
├─Models            数据模型
│   ├─Entities      实体模型（数据库模型）
│   ├─DTO           服务器返回信息模型
│   └─VO            客户端参数接收模型
└─Services          业务逻辑定义
    └─Implements
```

# 调试与发布的基本配置信息
1. 将 `appsettings-example.json` 复制一份副本并命名为 `appsettings.json`。
2. 将 `appsettings-example.Development.json` 复制一份副本并命名为 `appsettings.Development.json`。
3. 将上述两个文件中的 `tokenManagement.secret` 修改为符合 SHA256 JWT Secret 规则的字符串。