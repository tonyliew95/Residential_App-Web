using Newtonsoft.Json;
//Tony Liew 20190306 RDT-116 \/
namespace Nop.Plugin.Api.Models.Incident.DTOs
{
    /// <summary>
    /// incidentMediaDto class
    /// </summary>
    public class incidentMediaDto
    {
        /// <summary>
        /// Gets or sets the type 
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the content 
        /// </summary>
        [JsonProperty("content")]
        public string content { get; set; }
    }
}
//Tony Liew 20190306 RDT-116 /\
