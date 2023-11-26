using Newtonsoft.Json;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// Incident_StatusDto Class
    /// </summary>
    public class Incident_StatusDto
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