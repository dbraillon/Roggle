using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Roggle.Core;

namespace Roggle.Tests
{
    [TestClass]
    public class RoggleTests
    {
        [TestMethod]
        public void FileRoggleTests()
        {
            GRoggle.Use<FileRoggle>();
            GRoggle.Current.WriteDebug("I'm testing a debug message !");
            GRoggle.Current.WriteInformation("I'm testing an information message !");
            GRoggle.Current.WriteWarning("I'm testing a warning message !");
            GRoggle.Current.WriteError("I'm testing an error message !");
        }

        [TestMethod]
        public void EventLogTests()
        {
            GRoggle.Use<EventLogRoggle>();
            GRoggle.Current.WriteDebug("I'm testing a debug message !");
            GRoggle.Current.WriteInformation("I'm testing an information message !");
            GRoggle.Current.WriteWarning("I'm testing a warning message !");
            GRoggle.Current.WriteError("I'm testing an error message !");
        }
    }
}
