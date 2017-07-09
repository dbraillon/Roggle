using System;

namespace Roggle.Core
{
    [Obsolete("Overseer will be shutdown very soon", true)]
    public enum OverseerEventLevel
    {
        Debug,
        Information,
        Warning,
        Error,
        Critical
    }
}
