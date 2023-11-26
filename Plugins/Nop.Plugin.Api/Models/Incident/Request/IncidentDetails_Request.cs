using Newtonsoft.Json;

//Tony Liew 20190306 RDT-117 \/
namespace Nop.Plugin.Api.Models.Incident.Request
{
    /// <summary>
    /// Incident Details input parameters
    /// </summary>
    public class IncidentDetails_Request
    {
        /// <summary>
        /// Gets or sets the id 
        /// </summary>
        [JsonProperty("incId")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("propId")]
        public int propId { get; set; }

    }
}
//Tony Liew 20190306 RDT-117 /\