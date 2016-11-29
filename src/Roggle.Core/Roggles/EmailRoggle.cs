using System.Net;
using System.Net.Mail;

namespace Roggle.Core
{
    /// <summary>
    /// Roggle interface based on the System.Net.Mail class.
    /// </summary>
    public class EmailRoggle : BaseRoggle
    {
        /// <summary>
        /// Host used to connect on a Smtp server.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port used to connect on a Smtp server.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Login used to connect on a Smtp server.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Password used to connect on a Smtp server.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// From used to set the sender of the log email.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// To used to set the recipient of the log email.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Subject used to set the subject of the log email.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Whether or not you want to use secure connection with Smtp server. Default is False.
        /// </summary>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Retrieve the data in app.config.
        /// </summary>
        public EmailRoggle(string host, int port, string login, string password, 
            string from, string to, string subject, bool useSsl = false, 
            RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Error | RoggleLogLevel.Critical)
            : base(acceptedLogLevels)
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

        public override void Write(string message, RoggleLogLevel level)
        {
            // Send an email with given data
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
    }
}
