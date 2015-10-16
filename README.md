Roggle
======

[![Latest version](https://img.shields.io/nuget/v/Roggle.svg)](https://www.nuget.org/packages?q=roggle) [![MIT  License](https://img.shields.io/badge/license-MIT-blue.svg)](http://www.gnu.org/licenses/lgpl-3.0.html)

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
    GRoggle.Use<FileRoggle>();
}
```

Usage
-----

[**FileRoggle**]()

Write any message inside a log file, you can configure it with configuration file. By default, it will create a log file inside AppData/Local/Roggle of the current user folder.

```csharp
GRoggle.Use<FileRoggle>();
```

You can configure FileRoggle to create a log in a specified path inside your application config file (or web.config):

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="RoggleLogFilePath" value="C:/MyApp/MyApp.log" />
  </appSettings>
</configuration>
```

[**EventLogRoggle**]()

Write any message inside the a custom log in Windows Event Viewer. By default, it will try to create an event source called Roggle inside Application log.

```csharp
GRoggle.Use<EventLogRoggle>();
```

You can configure EventLogRoggle to create a custom event source and event log inside your application config file (or web.config):

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="RoggleEventSourceName" value="MyAppSourceName" />
    <add key="RoggleEventLogName" value="MyAppLogName" />
  </appSettings>
</configuration>
```

[**EmailRoggle**]()

Write any message inside an email and send it right away.

```csharp
GRoggle.Use<EmailRoggle>();
```

You must configure EmailRoggle inside your application config file (or web.config):

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="RoggleEmailHost" value="" />      <!-- Smtp server address -->
    <add key="RoggleEmailPort" value="" />      <!-- Smtp server port -->
    <add key="RoggleEmailLogin" value="" />     <!-- Smtp server login -->
    <add key="RoggleEmailPassword" value="" />  <!-- Smtp server password -->
    <add key="RoggleEmailFrom" value="" />      <!-- Email address sending the log email -->
    <add key="RoggleEmailTo" value="" />        <!-- Email address receiving the log email -->
    <add key="RoggleEmailSubject" value="" />   <!-- A custom email subject -->
    <add key="RoggleEmailUseSsl" value="" />    <!-- Whether or not Smtp server use SSL -->
  </appSettings>
</configuration>
```

**Write a message**

After initialization you can call Roggle this way:

```csharp
GRoggle.Current.WriteDebug("This is a debug message");
GRoggle.Current.WriteInformation("This is an information message");
GRoggle.Current.WriteWarning("This is a warning message");
GRoggle.Current.WriteError("This is an error message");
```
