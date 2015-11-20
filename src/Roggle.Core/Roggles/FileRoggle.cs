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
        /// Path used to created and write to the file log. Will be filled by application config with the key RoggleLogFilePath. Default is %localappdata%/Roggle/default.log.
        /// </summary>
        public string LogFilePath { get; set; }
        
        /// <summary>
        /// Create the file log. If the file does not exist, this method will create the file.
        /// </summary>
        public FileRoggle(string logFilePath = null, int maxFileLength = 10485760, RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Error) 
            : base(acceptedLogLevels)
        {
            try
            {
                // Get path from app.config, set default value if null
                LogFilePath = logFilePath ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonPrograms), "Roggle", "roggle.log");
                MaxFileLength = maxFileLength;

                // Create path if not exists
                Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));

                // Write a test entry and create the file if necessary
                File.AppendAllLines(LogFilePath, new string[] { "Log file successfuly created !" });
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException(string.Format("File Roggle cannot create the file at path {0}.", LogFilePath), e);
            }
        }

        /// <summary>
        /// Base event writing for file Roogle.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void WriteBase(string message, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            try
            {
                string format = "[{0} - {1}] {2}";
                string formattedMessage = string.Format(format, DateTime.Now, RoggleExtensions.GetDisplayValue(level), message);

                // Try to write in file log
                File.AppendAllLines(LogFilePath, new string[] { formattedMessage });

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
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException(string.Format("File Roggle cannot write inside the file at path {0}. Wanted to write {1}.", LogFilePath, message), e);
            }
        }

        public override void Write(string message, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            WriteBase(message, level);
        }

        public override void Write(Exception e, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            WriteBase(e.ToString(), level);
        }

        public override void Write(string message, Exception e, RoggleLogLevel level = RoggleLogLevel.Error)
        {
            string concatenatedMessage = string.Join(Environment.NewLine, message, e.ToString());

            WriteBase(concatenatedMessage, level);
        }
    }
}
