using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roggle.Core
{
    public abstract class BaseRoggle : IRoggle
    {
        public RoggleLogLevel AcceptedLogLevels { get; set; }

        public BaseRoggle(RoggleLogLevel acceptedLogLevels)
        {
            AcceptedLogLevels = acceptedLogLevels;
        }

        public abstract void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error);
        public abstract void Write(Exception e, RoggleLogLevel level = RoggleLogLevel.Error);
        public abstract void Write(string message, Exception e, RoggleLogLevel level = RoggleLogLevel.Error);

        public virtual void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            // Cast object to Exception
            Exception e = (Exception)args.ExceptionObject;

            // Write unhandled error in used logger
            Write("Unhandled exception thrown.", e, RoggleLogLevel.Error);
        }
    }
}
