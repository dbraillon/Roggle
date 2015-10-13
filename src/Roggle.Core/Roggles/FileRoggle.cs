﻿using System;
using System.Configuration;
using System.IO;

namespace Roggle.Core
{
    /// <summary>
    /// Roggle interface based on the Windows FileInfo class.
    /// </summary>
    public class FileRoggle : IRoggle
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
        public void Create()
        {
            try
            {
                // Get path from app.config, set default value if null
                LogFilePath = ConfigurationManager.AppSettings.Get("RoggleLogFilePath") ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roggle", "default.log");

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
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException(string.Format("File Roggle cannot write inside the file at path {0}. Wanted to write {1}.", LogFilePath, message), e);
            }
        }
    }
}
