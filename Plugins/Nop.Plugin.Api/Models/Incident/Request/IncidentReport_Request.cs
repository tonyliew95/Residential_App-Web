using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Incident.DTOs;
using System.Collections.Generic;

//Tony Liew 20190307 RDT-118 \/
namespace Nop.Plugin.Api.Models.Incident.Request
{
    /// <summary>
    /// Incident Report input parameters
    /// </summary>
    public class IncidentReport_Request
    {
        /// <summary>
        /// Gets or Sets the incidents
        /// </summary>
        [JsonProperty("incidentDto")]
        public IncidentDto incident { get; set; }
    }
}//Tony Liew 20190307 RDT-118 /\
