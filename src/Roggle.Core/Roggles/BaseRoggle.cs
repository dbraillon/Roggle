using System;

namespace Roggle.Core
{
    /// <summary>
    /// Base class to create a Roggle log system.
    /// </summary>
    public abstract class BaseRoggle
    {
        public RoggleLogLevel AcceptedLogLevels { get; set; }

        public BaseRoggle(RoggleLogLevel acceptedLogLevels)
        {
            AcceptedLogLevels = acceptedLogLevels;
        }

        /// <summary>
        /// Build the final log message that will be wrote.
        /// </summary>
        /// <param name="message">Message coming from managers.</param>
        /// <param name="level">Level of log.</param>
        /// <returns>The built message.</returns>
        public virtual string BuildMessage(string message, RoggleLogLevel level)
        {
            var dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
            var levelStr = RoggleHelper.GetDisplayValue(level);

            return $"[{levelStr}] ({dateStr}): {message}";
        }

        /// <summary>
        /// Base method to write to a Roggle.
        /// </summary>
        /// <param name="message">Message coming from managers.</param>
        /// <param name="level">Level of log.</param>
        public virtual void WriteBase(string message, RoggleLogLevel level)
        {
            try
            {
                Write(BuildMessage(message, level), level);
            }
            catch (Exception e)
            {
                throw new RoggleException($"Something goes wrong when trying to write logs with {GetType().Name}. Check inner exception.", e);
            }
        }
        
        // Write a message log to roggle system
        public abstract void Write(string message, RoggleLogLevel level);

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
