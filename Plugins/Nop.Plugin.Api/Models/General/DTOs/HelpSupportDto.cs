using Newtonsoft.Json;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// HelpSupportDto Class
    /// </summary>
    public class HelpSupportDto
    {
        /// <summary>
        /// Gets or sets the id
        /// </summary>
        [JsonProperty("id")]
        public string id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the url
        /// </summary>
        [JsonProperty("url")]
        public string url { get; set; }

    }
}