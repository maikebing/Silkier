# Silkier
[![License](https://img.shields.io/github/license/maikebing/Silkier.svg)](https://github.com/maikebing/Silkier/blob/master/LICENSE)
  [![Build status](https://ci.appveyor.com/api/projects/status/fle0qe4uk9lyjax5?svg=true)](https://ci.appveyor.com/project/MaiKeBing/silkier)  ![.NET Core](https://github.com/maikebing/Silkier/workflows/.NET%20Core/badge.svg)

## Silkier is a modern, fluent, asynchronous, testable, portable,   collection of extensions!

```c#
var  obj=builder.Create<SSHClient>("192.168.1.111","root","kissme")
        .Connect()
        .DownloadFile("file.txt")
        .ToJson<Obj>();
```



## What is Silkier?

 [![Nuget Version](https://img.shields.io/nuget/v/Silkier.svg)](https://www.nuget.org/packages/Silkier/)
 Silkier   is a common collection of extensions.  For example, retry, partitioning in parallel,ObjectPool，RestClient's extension, LITTLE-ENDIAN and BIG-ENDIAN coversions and more .....

Silkier  是一个常用扩展集合 比如 重试、分区并行、对象池、RestClient的扩展、  大小端转换以及更多 BIG-ENDIAN coversions and more .....

## What is Silkier.EFCore?

[![Nuget Version](https://img.shields.io/nuget/v/Silkier.EFCore.svg)](https://www.nuget.org/packages/Silkier.EFCore/)

 Silkier.EFCore is an extension for EF.Core, and the main features include executing the original sql statement, converting the original sql statement to a tuple or a class or array or json  object or DataTable .[Read more](/Silkier.EFCore/readme.md) 

Silkier.EFCore 是一个针对EF.Core的扩展， 主要功能包括 执行原始sql语句， 将原始sql语句转换为 元组或者类或者数组。[更多内容](/Silkier.EFCore/readme.md) 



 ## What is Silkier.AspNetCore?
 [![Nuget Version](https://img.shields.io/nuget/v/Silkier.AspNetCore.svg)](https://www.nuget.org/packages/Silkier.AspNetCore/)

Silkier.AspNetCore have  ConfigureWindowsServices    and  UseJsonToSettings  and more ...

Silkier.AspNetCore 有用于配置Windows或者linux时的服务方式运行的参数，完全自动识别是否服务方式等， 同时有使用特殊json作为配置的扩展。 

 

  ## What is Silkier.HealthChecks.NTPServer?

 [![Nuget Version](https://img.shields.io/nuget/v/Silkier.HealthChecks.NTPServer.svg)](https://www.nuget.org/packages/Silkier.HealthChecks.NTPServer/)  

 Silkier.HealthChecks.NTPServer is an NTP time server health check and time difference check extension
  Silkier.HealthChecks.NTPServer 是一个NTP时间服务器健康检查和时间差异检查的扩展。 



## Where can I get it?

Get it from NuGet. You can simply install it with the Package Manager console:

```powershell
PM> Install-Package Silkier
PM> Install-Package Silkier.EFCore
PM> Install-Package Silkier.AspNetCore
PM> Install-Package Silkier.HealthChecks.NTPServer
```
