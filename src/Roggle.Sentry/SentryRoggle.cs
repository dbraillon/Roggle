using System;
using SharpRaven;
using SharpRaven.Data;

namespace Roggle.Core
{
    public class SentryRoggle : IRoggle
    {
        protected RavenClient RavenClient { get; }
        public RoggleLogLevel AcceptedLogLevels { get; set; }

        public SentryRoggle(string dsn,
            RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Error | RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Critical)
        {
            AcceptedLogLevels = acceptedLogLevels;
            RavenClient = new RavenClient(dsn);
        }

        public void Write(string message, RoggleLogLevel level)
        {
            RavenClient.Capture(new SentryEvent(message) { Level = ToSentryErrorLevel(level) });
        }

        public void Write(Exception e, RoggleLogLevel level)
        {
            RavenClient.Capture(new SentryEvent(e) { Level = ToSentryErrorLevel(level) });
        }

        public void Write(string message, Exception e, RoggleLogLevel level)
        {
            RavenClient.Capture(new SentryEvent(e) { Level = ToSentryErrorLevel(level), Message = message });
        }

        public void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            // Cast object to Exception
            Exception e = (Exception)args.ExceptionObject;

            // Write unhandled error in Roggles
            Write("Unhandled exception thrown.", e, RoggleLogLevel.Error);
        }

        public ErrorLevel ToSentryErrorLevel(RoggleLogLevel level)
        {
            return
                level == RoggleLogLevel.Critical ? ErrorLevel.Fatal :
                level == RoggleLogLevel.Debug ? ErrorLevel.Debug :
                level == RoggleLogLevel.Error ? ErrorLevel.Error :
                level == RoggleLogLevel.Info ? ErrorLevel.Info :
                level == RoggleLogLevel.Warning ? ErrorLevel.Warning :
                ErrorLevel.Error;
        }
    }
}
