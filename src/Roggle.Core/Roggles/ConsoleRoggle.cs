using System;

namespace Roggle.Core
{
    /// <summary>
    /// Roggle interface based on Windows Console class.
    /// </summary>
    public class ConsoleRoggle : BaseRoggle
    {
        public ConsoleRoggle(RoggleLogLevel acceptedLogLevels) : base(acceptedLogLevels)
        {
        }

        public void WriteBase(string message, RoggleLogLevel level)
        {
            try
            {
                // Format the incoming message
                string format = "[{0} - {1}] {2}";
                string formattedMessage = string.Format(format, DateTime.Now, RoggleHelper.GetDisplayValue(level), message);

                // Write the message in console
                Console.WriteLine(formattedMessage);
            }
            catch (Exception e)
            {
                // Exception occurs, encapsulate it inside a Roggle exception and throw it
                throw new RoggleException(string.Format("Console Roggle cannot write a line in console. Wanted to write {0}", message), e);
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
