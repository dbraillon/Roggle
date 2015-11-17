using System;
using System.Configuration;
using System.Diagnostics;

namespace Roggle.Core
{
    /// <summary>
    /// Roggle interface based on the Windows EventLog class.
    /// </summary>
    public class EventLogRoggle : IRoggle
    {
        /// <summary>
        /// Max event message length.
        /// </summary>
        private const int MaxEventLogEntryLength = 30000;

        /// <summary>
        /// Event source name used to created and write to the event log. Will be filled by application config with the key RoggleEventSourceName. Default is Roggle.
        /// </summary>
        public string EventSourceName { get; set; }

        /// <summary>
        /// Event log name used to create the event log. Will be filled by application config with the key RoggleEventLogName. Default is Application.
        /// </summary>
        public string EventLogName { get; set; }

        /// <summary>
        /// Create the event source if not exist and log. In order to create the event source, the application must have administrative rights.
        /// </summary>
        public EventLogRoggle(string eventSourceName, string eventLogName)
        {
            try
            {
                // Get event source name and event log name from app.config, set default value if null
                EventSourceName = eventSourceName;
                EventLogName = eventLogName;

                // Check if event source does not already exists
                if (!EventLog.SourceExists(EventSourceName))
                {
                    // Event source does not exist, create it
                    EventLog.CreateEventSource(EventSourceName, EventLogName);

                    // Write a test entry
                    EventLog.WriteEntry(EventSourceName, "Roggle in da place !", EventLogEntryType.Information);
                }
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException(string.Format("Application event Roggle cannot create the event source with source name {0} and log name {1}.", EventSourceName, EventLogName), e);
            }
        }

        public void FormatDebug(string message)
        {
            // Write debug message
            Write(message, EventLogEntryType.Information);
        }

        public void WriteInformation(string message)
        {
            // Write information message
            Write(message, EventLogEntryType.Information);
        }

        public void WriteWarning(string message)
        {
            // Write warning message
            Write(message, EventLogEntryType.Warning);
        }

        public void WriteError(string message)
        {
            // Write error message
            Write(message, EventLogEntryType.Error);
        }

        /// <summary>
        /// Base event writing for application event Roggle.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="type">The type of message to log.</param>
        public void Write(string message, EventLogEntryType type)
        {
            try
            {
                // Truncate message if necessary
                if (message.Length > MaxEventLogEntryLength)
                {
                    message = string.Concat(message.Substring(0, MaxEventLogEntryLength - 3), "...");
                }

                // Try to write in EventLog
                EventLog.WriteEntry(EventSourceName, message, type);
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException(string.Format("Application event Roggle cannot write inside the event source {0}. Wanted to write {1}", EventSourceName, message), e);
            }
        }
    }
}
