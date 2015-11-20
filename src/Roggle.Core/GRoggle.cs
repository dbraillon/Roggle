using System;
using System.Collections.Generic;

namespace Roggle.Core
{
    public class GRoggle
    {
        /// <summary>
        /// Current Roggle, is null unless you called Roggle.Use<> in the first place.
        /// </summary>
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

        /// <summary>
        /// Specify to the Roggle to use a given type of log system.
        /// </summary>
        /// <typeparam name="TRoggle">An existing log system in Roggle.Core.</typeparam>
        /// <param name="logUnhandledException">Whether you want or not to log unhandled exception.</param>
        public static void Use(BaseRoggle roggle, bool logUnhandledException = true)
        {
            // Check arguments
            if (roggle == null) throw new ArgumentNullException("roggle");

            // Create the Roggle
            Instance.Roggles.Add(roggle);

            // Check if the user wants to log unhandled exceptions
            if (logUnhandledException)
            {
                // Add an event to retrieve unhandled exceptions
                AppDomain.CurrentDomain.UnhandledException += roggle.UnhandledException;
            }
        }

        public static void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            foreach (BaseRoggle roggle in Instance.Roggles)
            {
                if (roggle.AcceptedLogLevels.HasFlag(level))
                {
                    roggle.Write(message, level);
                }
            }
        }

        public static void Write(Exception exception, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            foreach (BaseRoggle roggle in Instance.Roggles)
            {
                if (roggle.AcceptedLogLevels.HasFlag(level))
                {
                    roggle.Write(exception, level);
                }
            }
        }

        public static void Write(string message, Exception exception, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            foreach (BaseRoggle roggle in Instance.Roggles)
            {
                if (roggle.AcceptedLogLevels.HasFlag(level))
                {
                    roggle.Write(message, exception, level);
                }
            }
        }


        public IList<IRoggle> Roggles { get; set; }

        private GRoggle()
        {

        }
    }
}
