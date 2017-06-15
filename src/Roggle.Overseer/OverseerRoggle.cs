using System;
using System.IO;
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
            try
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
            catch (Exception e)
            {
                try
                {
                    // Try to write a overseer.log
                    var content =
                        $"Something goes wrong while trying to log to Overseer: {e.Message}.{Environment.NewLine}" +
                        $"RoggleOverseer wanted to write the following error:{Environment.NewLine}" +
                        $"{Source} - {message}";
                    File.WriteAllText("overseer.log", content);
                }
                catch
                {
                    // Nothing else to do ...
                }
            }
        }
    }
}
