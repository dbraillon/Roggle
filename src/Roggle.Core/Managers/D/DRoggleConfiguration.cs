using System;
using System.Collections.Generic;

namespace Roggle.Core
{
    /// <summary>
    /// Contains all configured Roggles.
    /// </summary>
    public class DRoggleConfiguration
    {
        internal List<IRoggle> Roggles { get; set; }

        public DRoggleConfiguration()
        {
            Roggles = new List<IRoggle>();
        }

        public void Use<TRoggle>() 
            where TRoggle : IRoggle, new()
        {
            Roggles.Add(Activator.CreateInstance<TRoggle>());
        }

        public void Use(IRoggle roggle)
        {
            Roggles.Add(roggle);
        }
    }
}
