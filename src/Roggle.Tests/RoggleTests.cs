using System;
using NUnit.Framework;
using Roggle.Core;

namespace Roggle.Tests
{
    [TestFixture]
    public class RoggleTests
    {
        [Test]
        public void FileRoggleTests()
        {
            GRoggle.Use<FileRoggle>();
            GRoggle.Current.WriteDebug("I'm testing a debug message !");
            GRoggle.Current.WriteInformation("I'm testing an information message !");
            GRoggle.Current.WriteWarning("I'm testing a warning message !");
            GRoggle.Current.WriteError("I'm testing an error message !");
        }

        [Test]
        public void EventLogRoggleTests()
        {
            GRoggle.Use<EventLogRoggle>();
            GRoggle.Current.WriteDebug("I'm testing a debug message !");
            GRoggle.Current.WriteInformation("I'm testing an information message !");
            GRoggle.Current.WriteWarning("I'm testing a warning message !");
            GRoggle.Current.WriteError("I'm testing an error message !");
        }

        [Test]
        public void EmailRoggleTests()
        {
            GRoggle.Use<EmailRoggle>();
            GRoggle.Current.WriteDebug("I'm testing a debug message !");
            GRoggle.Current.WriteInformation("I'm testing an information message !");
            GRoggle.Current.WriteWarning("I'm testing a warning message !");
            GRoggle.Current.WriteError("I'm testing an error message !");
        }
    }
}
