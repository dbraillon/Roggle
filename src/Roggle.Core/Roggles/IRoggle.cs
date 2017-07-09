using System;

namespace Roggle.Core
{
    public interface IRoggle
    {
        RoggleLogLevel AcceptedLogLevels { get; set; }

        void UnhandledException(object sender, UnhandledExceptionEventArgs args);
        void Write(Exception e, RoggleLogLevel level);
        void Write(string message, Exception e, RoggleLogLevel level);
        void Write(string message, RoggleLogLevel level);
    }
}