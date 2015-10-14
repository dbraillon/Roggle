Roggle
======

Simple log system for .Net applications.

Installation
------------

Roggle is available as a NuGet package. So, you can install it using the NuGet Package Console window:

```
PM> Install-Package Roggle
```

You can use Roggle in any project, but it must be first initialized. In a web application, update Global.asax file to match the following code:

```csharp
protected void Application_Start()
{
    GRoggle.Use<FileRoggle>();
}
```

Usage
-----

After initialization you can call Roggle this way:

```csharp
GRoggle.Current.WriteDebug("This is a debug message");
GRoggle.Current.WriteInformation("This is an information message");
GRoggle.Current.WriteWarning("This is a warning message");
GRoggle.Current.WriteError("This is an error message");
```
