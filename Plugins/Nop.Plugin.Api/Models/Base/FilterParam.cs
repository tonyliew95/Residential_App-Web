using System;

namespace Nop.Plugin.Api.Models.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FilterParam
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public FilterParam()
        {
            SessionID = null;
            ActionCode = 0;
        }

        /// <summary>
        /// Gets or sets the Session ID
        /// </summary>
        public string SessionID { get; set; }

        /// <summary>
        /// Gets or sets the Action Code
        /// </summary>
        public int? ActionCode { get; set; }
    }
}
