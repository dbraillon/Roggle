using System;
using System.Collections.Generic;

namespace Roggle.Core
{
    /// <summary>
    /// G for Global, GlobalRoggle allow you to write log from everywhere as long as you have first initialized it.
    /// </summary>
    public class GRoggle
    {
        #region Singleton pattern

        private static GRoggle _instance;

        private static GRoggle Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GRoggle();
                }

                return _instance;
            }
        }

        #endregion

        /// <summary>
        /// Specify to the Roggle to use a given type of log system. This can be called multiple times but at least once.
        /// </summary>
        /// <param name="roggles">Existing log systems in Roggle.Core.</param>
        public static void Use(params IRoggle[] roggles)
        {
            Use(true, roggles);
        }

        /// <summary>
        /// Specify to the Roggle to use a given type of log system. This can be called multiple times but at least once.
        /// </summary>
        /// <param name="logUnhandledException">Whether you want or not to log unhandled exception.</param>
        /// <param name="roggles">Existing log systems in Roggle.Core.</param>
        public static void Use(bool logUnhandledException, params IRoggle[] roggles)
        {
            // Check arguments
            if (roggles == null) throw new ArgumentNullException(nameof(roggles));

            // Create the Roggle
            Instance.Roggles.AddRange(roggles);

            // Check if user wants to log unhandled exceptions
            if (logUnhandledException)
            {
                foreach (BaseRoggle roggle in roggles)
                {
                    // Add an event to retrieve unhandled exceptions
                    AppDomain.CurrentDomain.UnhandledException += roggle.UnhandledException;
                }
            }
        }

        /// <summary>
        /// Write message in underlying log systems.
        /// </summary>
        /// <param name="message">The message to be log.</param>
        /// <param name="level">The level of the message to be log.</param>
        public static void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            foreach (var roggle in Instance.Roggles)
            {
                if (roggle.AcceptedLogLevels.HasFlag(level))
                {
                    roggle.Write(message, level);
                }
            }
        }

        /// <summary>
        /// Write an exception in underlying log systems.
        /// </summary>
        /// <param name="exception">The exception to be log.</param>
        /// <param name="level">The level of the exception to be log.</param>
        public static void Write(Exception exception, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            foreach (var roggle in Instance.Roggles)
            {
                if (roggle.AcceptedLogLevels.HasFlag(level))
                {
                    roggle.Write(exception, level);
                }
            }
        }

        /// <summary>
        /// Write a message and an exception in underlying log systems.
        /// </summary>
        /// <param name="message">The message to be log.</param>
        /// <param name="exception">The exception to be log.</param>
        /// <param name="level">The level of the exception to be log.</param>
        public static void Write(string message, Exception exception, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            foreach (var roggle in Instance.Roggles)
            {
                if (roggle.AcceptedLogLevels.HasFlag(level))
                {
                    roggle.Write(message, exception, level);
                }
            }
        }


        public List<IRoggle> Roggles { get; set; }

        private GRoggle()
        {
            Roggles = new List<IRoggle>();
        }
    }
}
