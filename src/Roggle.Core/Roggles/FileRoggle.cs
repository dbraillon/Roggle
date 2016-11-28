using System;
using System.Linq;
using System.IO;

namespace Roggle.Core
{
    /// <summary>
    /// Roggle interface based on the Windows FileInfo class.
    /// </summary>
    public class FileRoggle : BaseRoggle
    {
        /// <summary>
        /// Max file length bytes. 10 Mo.
        /// </summary>
        public int MaxFileLength { get; set; }

        /// <summary>
        /// Path used to created and write to the file log. Default is Program Data/Roggle/roggle.log.
        /// </summary>
        public string LogFilePath { get; set; }
        
        /// <summary>
        /// Create the file log. If the file does not exist, this method will create the file.
        /// </summary>
        public FileRoggle(string logFilePath = null, int maxFileLength = 10485760, 
            RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Error | RoggleLogLevel.Critical) 
            : base(acceptedLogLevels)
        {
            try
            {
                // Set log file path, set defaults if necessary
                LogFilePath = logFilePath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms), "Roggle", "roggle.log");
                MaxFileLength = maxFileLength;

                // Create path if not exists
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));

                // Write a test entry and create the file if necessary
                if (!File.Exists(LogFilePath))
                {
                    File.AppendAllLines(LogFilePath, new string[] { "Log file successfuly created !" });
                }
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException($"File Roggle cannot create the file at path {LogFilePath}, check inner exception.", e);
            }
        }
        
        public override void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            // Try to write in file log
            File.AppendAllLines(LogFilePath, new string[] { message });

            #region Check if file size is not max

            bool isLessThanMax = false;
            FileInfo logFile = new FileInfo(LogFilePath);

            while (!isLessThanMax)
            {
                if (logFile.Length >= MaxFileLength)
                {
                    // Get all lines
                    string[] contentLines = File.ReadAllLines(LogFilePath);

                    // Skip first line
                    File.WriteAllLines(LogFilePath, contentLines.Skip(1).ToArray());
                }
                else
                {
                    isLessThanMax = true;
                }
            }

            #endregion
        }
    }
}
