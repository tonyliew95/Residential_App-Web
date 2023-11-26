using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// IncidentDto Class
    /// </summary>
    public class IncidentDto
    {
        public IncidentDto()
        {
            incident_Status = new List<Incident_StatusDto>();
        }

        /// <summary>
        /// Gets or sets the incident_Status 
        /// </summary>
        [JsonProperty("status")]
        public IList<Incident_StatusDto> incident_Status { get; set; }
        
    }
}