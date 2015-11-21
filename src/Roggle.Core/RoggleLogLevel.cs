using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roggle.Core
{
    [Flags]
    public enum RoggleLogLevel
    {
        Debug       = 1,
        Info        = 2,
        Warning     = 4,
        Error       = 8
    }
}
