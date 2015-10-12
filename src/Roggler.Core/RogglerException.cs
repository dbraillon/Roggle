using System;

namespace Roggler.Core
{
    /// <summary>
    /// Base exception class in every Roggler.
    /// </summary>
    public class RogglerException : Exception
    {
        public RogglerException() : base() { }
        public RogglerException(string message) : base(message) { }
        public RogglerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
