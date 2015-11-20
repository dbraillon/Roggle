using System;
using NUnit.Framework;
using Roggle.Core;

namespace Roggle.Tests
{
    [TestFixture]
    public class RoggleTests
    {
        [Test]
        public void WriteTests()
        {
            GRoggle.Use(new FileRoggle());
            GRoggle.Use(new EventLogRoggle());

            GRoggle.Write("I'm testing a debug message !", RoggleLogLevel.Debug);
            GRoggle.Write("I'm testing an information message !", RoggleLogLevel.Info);
            GRoggle.Write("I'm testing a warning message !", RoggleLogLevel.Warning);
            GRoggle.Write("I'm testing an error message !", RoggleLogLevel.Error);
        }

        //[Test]
        //public void EventLogRoggleTests()
        //{
        //    GRoggle.Use<EventLogRoggle>();
        //    GRoggle.Current.FormatDebug("I'm testing a debug message !");
        //    GRoggle.Current.WriteInformation("I'm testing an information message !");
        //    GRoggle.Current.WriteWarning("I'm testing a warning message !");
        //    GRoggle.Current.WriteError("I'm testing an error message !");
        //}

        //[Test]
        //public void EmailRoggleTests()
        //{
        //    GRoggle.Use<EmailRoggle>();
        //    GRoggle.Current.FormatDebug("I'm testing a debug message !");
        //    GRoggle.Current.WriteInformation("I'm testing an information message !");
        //    GRoggle.Current.WriteWarning("I'm testing a warning message !");
        //    GRoggle.Current.WriteError("I'm testing an error message !");
        //}
    }
}
