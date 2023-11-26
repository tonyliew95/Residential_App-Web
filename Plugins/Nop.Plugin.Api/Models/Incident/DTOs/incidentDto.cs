using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Models.Incident.DTOs
{
    /// <summary>
    /// incident Dto
    /// </summary>
    public class IncidentDto
    {
        /// <summary>
        /// Gets or sets the incidentTitle 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("propId")]
        public int propId { get; set; }

        /// <summary>
        /// Gets or sets the incidentTitle 
        /// </summary>
        [JsonProperty("title")]
        public string incidentTitle { get; set; }

        /// <summary>
        /// Gets or sets the incidentDescription 
        /// </summary>
        [JsonProperty("description")]
        public string incidentDescription { get; set; }

        /// <summary>
        /// Gets or sets the date 
        /// </summary>
        [JsonProperty("date")]
        public string date { get; set; }

        /// <summary>
        /// Gets or sets the time 
        /// </summary>
        [JsonProperty("time")]
        public string time { get; set; }

        /// <summary>
        /// Gets or sets the time 
        /// </summary>
        [JsonProperty("location")]
        public string location { get; set; }


        /// <summary>
        /// Gets or sets the media 
        /// </summary>
        [JsonProperty("media")]
        public IList<incidentMediaDto> media { get; set; }
    }
}
