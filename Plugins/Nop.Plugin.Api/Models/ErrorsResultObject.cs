using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Plugin.Api.Models
{
    // Tony Liew 20190308
    /// <summary>
    /// Error Result Object
    /// </summary>
    [JsonObject(Title = "ErrorsResultObject")]
    public class ErrorsResultObject 
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorsResultObject()
        {
            meta = new Meta();
        }

        /// <summary>
        /// Returned error code
        /// </summary>
        [JsonProperty("meta")]
        public Meta meta { get; set; }

    }
}