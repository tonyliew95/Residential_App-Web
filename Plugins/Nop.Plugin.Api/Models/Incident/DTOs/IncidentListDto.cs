using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using Nop.Plugin.Api.Models.Media;
using System.Collections.Generic;

//Tony Liew 20190306 RDT-116 \/
namespace Nop.Plugin.Api.Models.Incident.DTOs
{
    /// <summary>
    /// IncidentListDto class
    /// </summary>
    [JsonObject(Title = "incidentDto")]
    public class IncidentListDto
    {
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

        /// <summary>
        /// Gets or sets the id 
        /// </summary>
        [JsonProperty("id")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the IncidentTitle 
        /// </summary>
        [JsonProperty("title")]
        public string title { get; set; }

        /// <summary>
        /// Gets or sets the Location 
        /// </summary>
        [JsonProperty("location")]
        public string location { get; set; }

        /// <summary>
        /// Gets or sets the reportedDatetime 
        /// </summary>
        [JsonProperty("reportedDatetime")]
        public string reportedDatetime { get; set; }

        /// <summary>
        /// Gets or sets the createdDatetime 
        /// </summary>
        [JsonProperty("createdDatetime")]
        public string createdDatetime { get; set; }

        /// <summary>
        /// Gets or sets the Status 
        /// </summary>
        [JsonProperty("status")]
        public string status { get; set; }

        /// <summary>
        /// Gets or sets the Status Name 
        /// </summary>
        [JsonProperty("statusName")]
        public string statusName { get; set; }

        /// <summary>
        /// Gets or sets the media 
        /// </summary>
        [JsonProperty("media")]
        public IList<mediaDto> media { get; set; }

    }
}
//Tony Liew 20190306 RDT-116 /\