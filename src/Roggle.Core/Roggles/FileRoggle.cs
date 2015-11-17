using System;
using System.Configuration;
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
        private const int MaxFileLength = 10485760;

        /// <summary>
        /// Path used to created and write to the file log. Will be filled by application config with the key RoggleLogFilePath. Default is %localappdata%/Roggle/default.log.
        /// </summary>
        public string LogFilePath { get; set; }
        
        /// <summary>
        /// Create the file log. If the file does not exist, this method will create the file.
        /// </summary>
        public FileRoggle(string logFilePath)
        {
            try
            {
                // Get path from app.config, set default value if null
                LogFilePath = logFilePath;

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

        public void WriteDebug(string message)
        {
            // Write debug message
            Write(string.Format("[D] > {0}", message));
        }

        public void WriteInformation(string message)
        {
            // Write information message
            Write(string.Format("[I] > {0}", message));
        }

        public void WriteWarning(string message)
        {
            // Write warning message
            Write(string.Format("[W] > {0}", message));
        }

        public void WriteError(string message)
        {
            // Write error message
            Write(string.Format("[E] > {0}", message));
        }

        /// <summary>
        /// Base event writing for file Roogle.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Write(string message)
        {
            try
            {
                // Try to write in file log
                File.AppendAllLines(LogFilePath, new string[] { string.Format("{0} > {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), message) });

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
    }
}
