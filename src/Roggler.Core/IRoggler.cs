
namespace Roggler.Core
{
    /// <summary>
    /// Interface used to create or use any Roggler.
    /// </summary>
    public interface IRoggler
    {
        /// <summary>
        /// Method to create the underlying logging system.
        /// </summary>
        void Create();

        /// <summary>
        /// Method to write a debug message.
        /// </summary>
        /// <param name="message">Debug message.</param>
        void WriteDebug(string message);

        /// <summary>
        /// Method to write an information message.
        /// </summary>
        /// <param name="message">Information message.</param>
        void WriteInformation(string message);

        /// <summary>
        /// Method to write a warning message.
        /// </summary>
        /// <param name="message">Warning message.</param>
        void WriteWarning(string message);

        /// <summary>
        /// Method to write an error message.
        /// </summary>
        /// <param name="message">Error message.</param>
        void WriteError(string message);
    }
}
