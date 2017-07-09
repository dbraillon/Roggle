using System;

namespace Roggle.Core
{
    [Obsolete("Overseer will be shutdown very soon", true)]
    public class OverseerEvent
    {
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public OverseerEventLevel Level { get; set; }
    }
}
