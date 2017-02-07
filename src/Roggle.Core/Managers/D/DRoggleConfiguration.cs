using System;
using System.Collections.Generic;

namespace Roggle.Core
{
    /// <summary>
    /// Contains all configured Roggles.
    /// </summary>
    public class DRoggleConfiguration
    {
        internal List<BaseRoggle> Roggles { get; set; }

        public DRoggleConfiguration()
        {
            Roggles = new List<BaseRoggle>();
        }

        public void Use<TRoggle>() 
            where TRoggle : BaseRoggle, new()
        {
            Roggles.Add(Activator.CreateInstance<TRoggle>());
        }

        public void Use(BaseRoggle roggle)
        {
            Roggles.Add(roggle);
        }
    }
}
