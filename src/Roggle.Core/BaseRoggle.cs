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

        public BaseRoggle(RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Error)
        {
            AcceptedLogLevels = acceptedLogLevels;
        }

        public abstract void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error);
        public abstract void UnhandledException(object sender, UnhandledExceptionEventArgs args);
    }
}
