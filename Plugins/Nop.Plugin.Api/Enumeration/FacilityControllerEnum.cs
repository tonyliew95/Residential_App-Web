using Nop.Core.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Enumeration
{
    /// <summary>
    /// Facility Controller [Global] Enumeration
    /// </summary>
    public enum FacilityController_Global
    {
        /// <summary>
        /// pending
        /// </summary>
        [ValueAttribute("P")]
        [Description("Pending")]
        pending,

        /// <summary>
        /// Success
        /// </summary>
        [ValueAttribute("S")]
        [Description("Success")]
        success,

        /// <summary>
        /// Reschedule
        /// </summary>
        [ValueAttribute("R")]
        [Description("Reschedule")]
        reschedule,

        /// <summary>
        /// Failed
        /// </summary>
        [ValueAttribute("F")]
        [Description("Failed")]
        failed,

        /// <summary>
        /// Cancel
        /// </summary>
        [ValueAttribute("C")]
        [Description("Cancel")]
        cancel,

        /// <summary>
        /// 3013 = Invalid Date Format
        /// </summary>
        [Description("invalid date format")]
        invalidDateFormat = 3024,

    }
}
