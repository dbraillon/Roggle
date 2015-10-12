using System;

namespace Roggler.Core
{
    public static class Roggler
    {
        /// <summary>
        /// Current Roggler, is null unless you called Roggler.Use<> in the first place.
        /// </summary>
        public static IRoggler Current { get; set; }

        /// <summary>
        /// Specify to the Roggler to use a given type of log system.
        /// </summary>
        /// <typeparam name="TRoggler">An existing log system in Roggler.Core.</typeparam>
        /// <param name="logUnhandledException">Whether you want or not to log unhandled exception.</param>
        public static void Use<TRoggler>(bool logUnhandledException = true) where TRoggler : IRoggler
        {
            // Create the roggler
            Current = (TRoggler)Activator.CreateInstance(typeof(TRoggler));
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
