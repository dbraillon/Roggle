using System;
using System.ComponentModel.DataAnnotations;

namespace Roggle.Core
{
    [Flags]
    public enum RoggleLogLevel
    {
        [Display(Name = "D")]
        Debug    = 1,

        [Display(Name = "I")]
        Info     = 2,

        [Display(Name = "W")]
        Warning  = 4,

        [Display(Name = "E")]
        Error    = 8,

        [Display(Name = "C")]
        Critical = 16
    }
}
