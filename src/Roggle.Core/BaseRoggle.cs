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

        // Abstract implementation of IRoggle
        public abstract void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error);
        public abstract void Write(Exception e, RoggleLogLevel level = RoggleLogLevel.Error);
        public abstract void Write(string message, Exception e, RoggleLogLevel level = RoggleLogLevel.Error);

        // Implement a virtual version of UnhandledException method from IRoggle
        public virtual void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            // Cast object to Exception
            Exception e = (Exception)args.ExceptionObject;

            // Write unhandled error in used logger
            Write("Unhandled exception thrown.", e, RoggleLogLevel.Error);
        }
    }
}
