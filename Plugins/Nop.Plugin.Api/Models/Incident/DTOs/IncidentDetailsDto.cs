using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System.Collections.Generic;
//Tony Liew 20190306 RDT-117 \/
namespace Nop.Plugin.Api.Models.Incident.DTOs
{
    /// <summary>
    /// IncidentListDto class
    /// </summary>
    [JsonObject(Title = "incidentDto")]
    public class IncidentDetailsDto
    {

        /// <summary>
        /// Gets or sets the IncidentTitle 
        /// </summary>
        [JsonProperty("title")]
        public string incidentTitle { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("propId")]
        public int propId { get; set; }

        /// <summary>
        /// Gets or sets the Location 
        /// </summary>
        [JsonProperty("description")]
        public string description { get; set; }


        /// <summary>
        /// Gets or sets the Location 
        /// </summary>
        [JsonProperty("location")]
        public string location { get; set; }


        /// <summary>
        /// Gets or sets the Date 
        /// </summary>
        [JsonProperty("date")]
        public string date { get; set; }

        /// <summary>
        /// Gets or sets the Date 
        /// </summary>
        [JsonProperty("time")]
        public string time { get; set; }

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
        public IList<MediaCustom> media { get; set; }

    }
}
//Tony Liew 20190306 RDT-117 /\
