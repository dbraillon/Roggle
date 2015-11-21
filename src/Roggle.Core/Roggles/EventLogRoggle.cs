using System;
using System.Configuration;
using System.Diagnostics;

namespace Roggle.Core
{
    /// <summary>
    /// Roggle interface based on the Windows EventLog class.
    /// </summary>
    public class EventLogRoggle : BaseRoggle
    {
        /// <summary>
        /// Max event message length.
        /// </summary>
        private const int MaxEventLogEntryLength = 30000;

        /// <summary>
        /// Event source name used to created and write to the event log. Default is Application.
        /// </summary>
        public string EventSourceName { get; set; }

        /// <summary>
        /// Event log name used to create the event log. Default is Application.
        /// </summary>
        public string EventLogName { get; set; }

        /// <summary>
        /// Create the event source if not exist and log. In order to create the event source, the application must have administrative rights.
        /// </summary>
        public EventLogRoggle(string eventSourceName = null, string eventLogName = null, RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Error)
            : base(acceptedLogLevels)
        {
            try
            {
                // Set event source and log name, set defaults if necessary
                EventSourceName = eventSourceName ?? "Application";
                EventLogName = eventLogName ?? "Application";

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

        /// <summary>
        /// Base event writing for application event Roggle.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="type">The type of message to log.</param>
        public void WriteBase(string message, EventLogEntryType type)
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

        public EventLogEntryType ToEventLogEntryType(RoggleLogLevel level)
        {
            switch (level)
            {
                case RoggleLogLevel.Debug: return EventLogEntryType.Information;
                case RoggleLogLevel.Error: return EventLogEntryType.Error;
                case RoggleLogLevel.Info: return EventLogEntryType.Information;
                case RoggleLogLevel.Warning: return EventLogEntryType.Warning;
                default: throw new NotImplementedException(string.Format("RoggleLogLevel {0} has not been implemented.", level));
            }
        }

        public override void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            WriteBase(message, ToEventLogEntryType(level));
        }

        public override void Write(Exception e, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            WriteBase(e.ToString(), ToEventLogEntryType(level));
        }

        public override void Write(string message, Exception e, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            string concatenatedMessage = string.Join(Environment.NewLine, message, e.ToString());

            WriteBase(concatenatedMessage, ToEventLogEntryType(level));
        }
    }
}
