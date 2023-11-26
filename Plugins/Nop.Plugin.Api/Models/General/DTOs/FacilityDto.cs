using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// FacilityDto Class
    /// </summary>
    public class FacilityDto
    {
        public FacilityDto()
        {
            facility_Status = new List<Adm_StandardCodeCustom>();
        }

        /// <summary>
        /// Gets or sets the facility_Status 
        /// </summary>
        [JsonProperty("status")]
        public IList<Adm_StandardCodeCustom> facility_Status { get; set; }
        
    }
}