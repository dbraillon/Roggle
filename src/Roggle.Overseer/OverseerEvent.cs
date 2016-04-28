using System;

namespace Roggle.Web
{
    public class OverseerEvent
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public OverseerEventLevel Level { get; set; }
    }
}
