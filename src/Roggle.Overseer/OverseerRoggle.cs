using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Roggle.Core
{
    public class OverseerRoggle : BaseRoggle
    {
        protected string Url { get; set; }
        protected Guid Key { get; set; }
        protected string Source { get; set; }
        
        public OverseerRoggle(string url, Guid key, string source, 
            RoggleLogLevel acceptedLogLevels = RoggleLogLevel.Error | RoggleLogLevel.Info | RoggleLogLevel.Warning | RoggleLogLevel.Critical)
            : base(acceptedLogLevels)
        {
            Url = url;
            Key = key;
            Source = source;
        }

        public override void Write(string message, RoggleLogLevel level)
        {
            var newEvent = new OverseerEvent()
            {
                Description = message,
                Name = Source,
                UserId = Key,
                Level =
                    level == RoggleLogLevel.Critical ? OverseerEventLevel.Critical :
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
                    throw new RoggleException($"Overseer return an error '{response.StatusCode}: {response.ReasonPhrase}'.");
                }
            }
        }
    }
}
