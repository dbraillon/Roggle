using System;

namespace Roggle.Core
{
    /// <summary>
    /// Base exception class in every Roggle.
    /// </summary>
    public class RoggleException : Exception
    {
        public RoggleException() : base() { }
        public RoggleException(string message) : base(message) { }
        public RoggleException(string message, Exception innerException) : base(message, innerException) { }
    }
}
