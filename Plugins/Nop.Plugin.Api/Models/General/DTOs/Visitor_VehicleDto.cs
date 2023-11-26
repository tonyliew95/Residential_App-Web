using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// Visitor_VehicleDto Class
    /// </summary>
    public class Visitor_VehicleDto
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