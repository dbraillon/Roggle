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
        /// Whether or not you want to use secure connection with Smtp server. Default is False.
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Retrieve the data in app.config.
        /// </summary>
        public EmailRoggle(string host, int port, string login, string password, string from, string to, string subject, bool useSsl = false)
        {
            Host = host;
            Port = port;
            Login = login;
            Password = password;
            From = from;
            To = to;
            Subject = subject;
            UseSsl = useSsl;
        }

        public virtual void FormatDebug(string message)
        {
            // Write debug message
            Write(string.Format("Debug{0}{1}", Environment.NewLine, message));
        }

        public virtual void WriteInformation(string message)
        {
            // Write information message
            Write(string.Format("Information{0}{1}", Environment.NewLine, message));
        }

        public virtual void WriteWarning(string message)
        {
            // Write warning message
            Write(string.Format("Warning{0}{1}", Environment.NewLine, message));
        }

        public virtual void WriteError(string message)
        {
            // Write error message
            Write(string.Format("Error{0}{1}", Environment.NewLine, message));
        }

        /// <summary>
        /// Base event writing for application event Roggle.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public virtual void Write(string message)
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
                    client.EnableSsl = UseSsl;
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
