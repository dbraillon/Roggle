using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roggle.Core
{
    /// <summary>
    /// D for Disposable, DisposableRoggle allows you to write log in memory and flush it when it disposes or whenever you want to.
    /// </summary>
    public class DRoggle : IDisposable
    {
        public DRoggleConfiguration Configuration { get; set; }
        public Dictionary<RoggleLogLevel, StringBuilder> Logs { get; set; }

        private DRoggle()
        {
            Configuration = new DRoggleConfiguration();
            Logs = RoggleHelper.GetEnumValues<RoggleLogLevel>().ToDictionary(x => x, x => new StringBuilder());
        }

        public DRoggle(BaseRoggle roggle) : this()
        {
            Configuration.Roggles.Add(roggle);
        }

        public DRoggle(DRoggleConfiguration conf) : this()
        {
            Configuration = conf;
        }

        public DRoggle(Action<DRoggleConfiguration> conf) : this()
        {
            conf(Configuration);
        }

        /// <summary>
        /// Write message in underlying log systems.
        /// </summary>
        /// <param name="message">The message to be log.</param>
        /// <param name="level">The level of the message to be log.</param>
        public void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            Logs[level].AppendLine(message);
        }

        /// <summary>
        /// Write an exception in underlying log systems.
        /// </summary>
        /// <param name="exception">The exception to be log.</param>
        /// <param name="level">The level of the exception to be log.</param>
        public void Write(Exception exception, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            Write(exception.ToString(), level: level);
        }

        /// <summary>
        /// Write a message and an exception in underlying log systems.
        /// </summary>
        /// <param name="message">The message to be log.</param>
        /// <param name="exception">The exception to be log.</param>
        /// <param name="level">The level of the exception to be log.</param>
        public void Write(string message, Exception exception, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            Write(string.Join(Environment.NewLine, message, exception.ToString()), level: level);
        }

        /// <summary>
        /// Clear all level of logs.
        /// </summary>
        public void Clear()
        {
            foreach (var log in Logs)
            {
                Clear(log.Key);
            }
        }

        /// <summary>
        /// Clear a precise level of logs.
        /// </summary>
        /// <param name="level">The level of log to be cleared.</param>
        public void Clear(RoggleLogLevel level)
        {
            Logs[level].Clear();
        }

        /// <summary>
        /// Call underlying Write method for each Roggle registered and for each level of log then clear all level of logs.
        /// </summary>
        public void Flush()
        {
            foreach (var roggle in Configuration.Roggles)
            foreach (var log in Logs)
            {
                if (log.Value.Length > 0)
                {
                    roggle.Write(log.Value.ToString(), level: log.Key);
                }
            }

            Clear();
        }
        
        /// <summary>
        /// Dispose DRoggle and flush logs in memory.
        /// </summary>
        public void Dispose()
        {
            Flush();
        }
    }
}
