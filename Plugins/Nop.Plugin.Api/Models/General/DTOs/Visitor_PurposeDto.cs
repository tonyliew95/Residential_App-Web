using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// Visitor_PurposeDto Class
    /// </summary>
    public class Visitor_PurposeDto
    {
        /// <summary>
        /// Gets or sets the code
        /// </summary>
        [JsonProperty("code")]
        public string code { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

    }
}