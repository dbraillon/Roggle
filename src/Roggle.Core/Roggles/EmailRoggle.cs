using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Roggle.Core
{
    /// <summary>
    /// Roggle interface based on the System.Net.Mail class.
    /// </summary>
    public class EmailRoggle : IRoggle
    {
        /// <summary>
        /// Host used to connect on a Smtp server. Will be filled by application config with the key RoggleEmailHost.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port used to connect on a Smtp server. Will be filled by application config with the key RoggleEmailPort.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Login used to connect on a Smtp server. Will be filled by application config with the key RoggleEmailLogin.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password used to connect on a Smtp server. Will be filled by application config with the key RoggleEmailPassword.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// From used to set the sender of the log email. Will be filled by application config with the key RoggleEmailFrom.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// To used to set the recipient of the log email. Will be filled by application config with the key RoggleEmailTo.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Subject used to set the subject of the log email. Will be filled by application config with the key RoggleEmailSubject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Retrieve the data in app.config.
        /// </summary>
        public void Create()
        {
            try
            {
                // Get all necessary data from app.config, no default value
                Host = ConfigurationManager.AppSettings.Get("RoggleEmailHost");
                Port = int.Parse(ConfigurationManager.AppSettings.Get("RoggleEmailPort"));
                Login = ConfigurationManager.AppSettings.Get("RoggleEmailLogin");
                Password = ConfigurationManager.AppSettings.Get("RoggleEmailPassword");
                From = ConfigurationManager.AppSettings.Get("RoggleEmailFrom");
                To = ConfigurationManager.AppSettings.Get("RoggleEmailTo");
                Subject = ConfigurationManager.AppSettings.Get("RoggleEmailSubject");
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException("Email Roggle cannot retrieve the data in app.config.", e);
            }
        }

        public void WriteDebug(string message)
        {
            // Write debug message
            Write(string.Format("Debug{0}{1}", Environment.NewLine, message));
        }

        public void WriteInformation(string message)
        {
            // Write information message
            Write(string.Format("Information{0}{1}", Environment.NewLine, message));
        }

        public void WriteWarning(string message)
        {
            // Write warning message
            Write(string.Format("Warning{0}{1}", Environment.NewLine, message));
        }

        public void WriteError(string message)
        {
            // Write error message
            Write(string.Format("Error{0}{1}", Environment.NewLine, message));
        }

        /// <summary>
        /// Base event writing for application event Roggle.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Write(string message)
        {
            try
            {
                // Send an email with app.config data
                using (SmtpClient client = new SmtpClient())
                {
                    MailMessage mail = new MailMessage(From, To);
                    mail.Subject = Subject;
                    mail.Body = message;

                    client.Host = Host;
                    client.Port = Port;
                    client.Credentials = new NetworkCredential(Login, Password);
                    client.Send(mail);
                }
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException(string.Format("Email Roggle cannot send an email to {0}. Wanted to write {1}", To, message), e);
            }
        }
    }
}
