using System;

namespace Roggle.Core
{
    /// <summary>
    /// Base class to create a Roggle log system.
    /// </summary>
    public abstract class BaseRoggle : IRoggle
    {
        public RoggleLogLevel AcceptedLogLevels { get; set; }

        public BaseRoggle(RoggleLogLevel acceptedLogLevels)
        {
            AcceptedLogLevels = acceptedLogLevels;
        }

        // Write a message log to roggle system
        public abstract void Write(string message, RoggleLogLevel level);
        public void Write(Exception e, RoggleLogLevel level)
        {
            Write(e.ToString(), level);
        }
        public void Write(string message, Exception e, RoggleLogLevel level)
        {
            Write($"{message}{Environment.NewLine}{e.ToString()}", level);
        }

        // Called when an unhandled exception is thrown
        public virtual void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            // Cast object to Exception
            Exception e = (Exception)args.ExceptionObject;

            // Write unhandled error in Roggles
            Write(string.Join(Environment.NewLine, "Unhandled exception thrown.", e), RoggleLogLevel.Error);
        }
    }
}
