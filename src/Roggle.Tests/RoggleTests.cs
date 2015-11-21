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
    }
}
