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

        public static void Write(string message, RoggleLogLevel level)
        {
            foreach (BaseRoggle roggle in Instance.Roggles)
            {
                if (roggle.AcceptedLogLevels.HasFlag(level))
                {
                    roggle.Write(message, level);
                }
            }
        }


        public IList<IRoggle> Roggles { get; set; }

        private GRoggle()
        {

        }

        // From AppDomain.CurrentDomain.UnhandledException
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            // Cast object to Exception
            Exception e = (Exception)args.ExceptionObject;

            // Write unhandled error in used logger
            Current.WriteError(string.Format("Unhandled exception thrown:{0}{1}", Environment.NewLine, e.ToString()));
        }

        public void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            throw new NotImplementedException();
        }
    }
}
