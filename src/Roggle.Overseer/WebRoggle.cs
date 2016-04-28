using Roggle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Roggle.Web
{
    public class WebRoggle : BaseRoggle
    {
        protected string Url { get; set; }
        protected Guid Key { get; set; }
        protected string Source { get; set; }
        
        public WebRoggle(string url, Guid key, string source, 
            RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Error | RoggleLogLevel.Info | RoggleLogLevel.Warning)
            : base(acceptedLogLevels)
        {
            Url = url;
            Key = key;
            Source = source;
        }

        public void WriteBase(string message, RoggleLogLevel level)
        {
            var newEvent = new OverseerEvent()
            {
                Description = message,
                Name = Source,
                UserId = Key,
                Level =
                    level == RoggleLogLevel.Debug ? OverseerEventLevel.Debug :
                    level == RoggleLogLevel.Error ? OverseerEventLevel.Error :
                    level == RoggleLogLevel.Info ? OverseerEventLevel.Information :
                    level == RoggleLogLevel.Warning ? OverseerEventLevel.Warning :
                    OverseerEventLevel.Debug
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = AsyncHelper.RunSync(() => client.PostAsJsonAsync("api/events", newEvent));
                if (!response.IsSuccessStatusCode)
                {
                    // Do something !
                }
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
