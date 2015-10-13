using System;

namespace Roggle.Core
{
    public static class Roggle
    {
        /// <summary>
        /// Current Roggle, is null unless you called Roggle.Use<> in the first place.
        /// </summary>
        public static IRoggle Current { get; set; }

        /// <summary>
        /// Specify to the Roggle to use a given type of log system.
        /// </summary>
        /// <typeparam name="TRoggle">An existing log system in Roggle.Core.</typeparam>
        /// <param name="logUnhandledException">Whether you want or not to log unhandled exception.</param>
        public static void Use<TRoggle>(bool logUnhandledException = true) where TRoggle : IRoggle
        {
            // Create the Roggle
            Current = (TRoggle)Activator.CreateInstance(typeof(TRoggle));
            Current.Create();

            // Check if the user wants to log unhandled exceptions
            if (logUnhandledException)
            {
                // Add an event to retrieve unhandled exceptions
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            }
        }

        // From AppDomain.CurrentDomain.UnhandledException
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            // Cast object to Exception
            Exception e = (Exception)args.ExceptionObject;

            // Write unhandled error in used logger
            Current.WriteError(string.Format("Unhandled exception thrown:{0}{1}", Environment.NewLine, e.ToString()));
        }
    }
}
