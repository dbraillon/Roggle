
using System;

namespace Roggle.Core
{
    /// <summary>
    /// Interface used to create or use any Roggle.
    /// </summary>
    public interface IRoggle
    {
        void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error);
        void UnhandledException(object sender, UnhandledExceptionEventArgs args);
    }
}
