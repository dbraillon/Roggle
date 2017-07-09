using SharpRaven;
using SharpRaven.Data;

namespace Roggle.Core
{
    public class SentryRoggle : BaseRoggle
    {
        protected RavenClient RavenClient { get; }

        public SentryRoggle(string dsn,
            RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Error | RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Critical) 
            : base(acceptedLogLevels)
        {
            RavenClient = new RavenClient(dsn);
        }

        public override void Write(string message, RoggleLogLevel level)
        {
            RavenClient.Capture(new SentryEvent(message) { Level = ToSentryErrorLevel(level) });
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
