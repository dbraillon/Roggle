using System;

namespace Roggle.Core
{
    public class CustomRoggle : IRoggle
    {
        public RoggleLogLevel AcceptedLogLevels { get; set; }
        protected Action<string, RoggleLogLevel> Write1Func { get; }
        protected Action<Exception, RoggleLogLevel> Write2Func { get; }
        protected Action<object, UnhandledExceptionEventArgs> UnhandledExceptionFunc { get; }
        protected Action<string, Exception, RoggleLogLevel> Write3Func { get; }

        public CustomRoggle(
            Action<string, RoggleLogLevel> write1Func,
            Action<Exception, RoggleLogLevel> write2Func,
            Action<string, Exception, RoggleLogLevel> write3Func,
            Action<object, UnhandledExceptionEventArgs> unhandledExceptionFunc,
            RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Error | RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Critical)
        {
            AcceptedLogLevels = acceptedLogLevels;
            Write1Func = write1Func;
            Write2Func = write2Func;
            Write3Func = write3Func;
        }

        public void UnhandledException(object sender, UnhandledExceptionEventArgs args) => UnhandledExceptionFunc(sender, args);
        public void Write(string message, RoggleLogLevel level) => Write1Func(message, level);
        public void Write(Exception e, RoggleLogLevel level) => Write2Func(e, level);
        public void Write(string message, Exception e, RoggleLogLevel level) => Write3Func(message, e, level);
    }
}
