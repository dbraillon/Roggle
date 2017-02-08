Roggle
======

[![Latest version](https://img.shields.io/nuget/v/Roggle.svg)](https://www.nuget.org/packages/Roggle/) [![Build status](https://ci.appveyor.com/api/projects/status/v7k1ujovx59lpe8d?svg=true)](https://ci.appveyor.com/project/dbraillon/roggle) [![License: LGPL v3](https://img.shields.io/badge/License-LGPL%20v3-blue.svg)](http://www.gnu.org/licenses/lgpl-3.0)

Simple log system for .Net applications.

Installation
------------

Roggle is available as a NuGet package. So, you can install it using the NuGet Package Console window:

```
PM> Install-Package Roggle
```

You can use Roggle in any project, but it must be first initialized. For example in a web application, update Global.asax file to match the following code:

```csharp
protected void Application_Start()
{
    // Tell GRoggle which Roggle you want to use
    GRoggle.Use(new ConsoleRoggle());
    
    // You can add mutliple Roggle, each time you write an entry
    // it will be written in all roggle you put here
    GRoggle.Use(new EventLogRoggle());
    GRoggle.Use(new FileRoggle());
}
```

Usage
-----

[**FileRoggle**](https://github.com/dbraillon/Roggle/wiki/FileRoggle)

Write any message inside a log file. By default, it will create a log file inside Program Data/Roggle.

```csharp
GRoggle.Use(new FileRoggle());
```

You can configure FileRoggle to create a log in a specified path:

```csharp
GRoggle.Use(new FileRoggle("C:/MyApp/MyApp.log"));
```

[**EventLogRoggle**](https://github.com/dbraillon/Roggle/wiki/EventLogRoggle)

Write any message inside the a custom log in Windows Event Viewer. By default, it will try to create an event source called Roggle inside Application log.

```csharp
GRoggle.Use(new EventLogRoggle());
```

You can configure EventLogRoggle to create a custom event source and event log:

```csharp
GRoggle.Use(new EventLogRoggle("MyAppEventSourceName", "MyAppEventLogName"));
```

[**EmailRoggle**]()

Write any message inside an email and send it right away. You must configure EmailRoggle in order to use it.

```csharp
GRoggle.Use(new EmailRoggle("smtp.myapp.io", 25, "mylogin", "mypassword", "fromaddress", "toaddress", "emailsubject", useSsl: true));
```

**Limit a level of log**

When initializing any Roggle you can add an argument to limit the level of log in your application.

```csharp
// This will limit the Roggle to only write any Warning or Error log
GRoggle.Use(new FileRoggle(RoggleLogLevel.Warning | RoggleLogLevel.Error));
```

**Write a message**

After initialization you can call Roggle this way:

```csharp
GRoggle.Write("This is a debug message", RoggleLogLevel.Debug);
GRoggle.Write(e, RoggleLogLevel.Error);
GRoggle.Write("This is a debug message", e, RoggleLogLevel.Error);
```

