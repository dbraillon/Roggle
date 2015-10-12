using System;
using System.Configuration;
using System.Diagnostics;

namespace Roggler.Core
{
    /// <summary>
    /// Roggler class based on the Windows EventLog class.
    /// </summary>
    public class EventLogRoggler : IRoggler
    {
        /// <summary>
        /// Max event message length.
        /// </summary>
        private const int MaxEventLogEntryLength = 30000;

        /// <summary>
        /// Event source name used to created and write to the event log. Will be filled by application config with the key LoggerEventSourceName. Default is Roggler.
        /// </summary>
        public string EventSourceName { get; set; }

        /// <summary>
        /// Event log name used to create the event log. Will be filled by application config with the key LoggerEventLogName. Default is Application.
        /// </summary>
        public string EventLogName { get; set; }

        /// <summary>
        /// Create the event source if not exist and log. In order to create the event source, the application must have administrative rights.
        /// </summary>
        public void Create()
        {
            try
            {
                // Get event source name and event log name from app.config, set default value if null
                EventSourceName = ConfigurationManager.AppSettings.Get("LoggerEventSourceName") ?? "Roggler";
                EventLogName = ConfigurationManager.AppSettings.Get("LoggerEventLogName") ?? "Application";

                // Check if event source does not already exists
                if (!EventLog.SourceExists(EventSourceName))
                {
                    // Event source does not exist, create it
                    EventLog.CreateEventSource(EventSourceName, EventLogName);

                    // Write a test entry
                    EventLog.WriteEntry(EventSourceName, "Roggler in da place !", EventLogEntryType.Information);
                }
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Logger exception and throw it
                throw new RogglerException(string.Format("Application event roggler cannot create the event source with source name {0} and log name {1}.", EventSourceName, EventLogName), e);
            }
        }

        public void WriteDebug(string message)
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
        /// Base event writing for application event logger.
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
                // Exception occurs, encapsulate it inside a Roggler exception and throw it
                throw new RogglerException(string.Format("Application event roggler cannot write inside the event source {0}. Wanted to write {1}", EventSourceName, message), e);
            }
        }
    }
}
