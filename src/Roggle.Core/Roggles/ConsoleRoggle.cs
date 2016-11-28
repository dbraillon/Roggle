using System;

namespace Roggle.Core
{
    /// <summary>
    /// Roggle class based on System.Console class.
    /// </summary>
    public class ConsoleRoggle : BaseRoggle
    {
        public ConsoleRoggle(RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Error | RoggleLogLevel.Critical)
            : base(acceptedLogLevels)
        {
        }

        public override void Write(string message, RoggleLogLevel level)
            => Console.WriteLine(message);
    }
}
