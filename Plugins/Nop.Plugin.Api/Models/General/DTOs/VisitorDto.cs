using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// VisitorDto Class
    /// </summary>
    public class VisitorDto
    {
        public VisitorDto()
        {
            visitor_Vehicle = new List<Adm_StandardCodeCustom>();
            visitor_Purpose = new List<Visitor_PurposeDto>();
            visitor_Timestamp = new List<Adm_StandardCodeCustom>();
        }

        /// <summary>
        /// Gets or sets the visitor_Vehicle 
        /// </summary>
        [JsonProperty("vehicle")]
        public IList<Adm_StandardCodeCustom> visitor_Vehicle { get; set; }

        /// <summary>
        /// Gets or sets the visitor_Purpose 
        /// </summary>
        [JsonProperty("purpose")]
        public IList<Visitor_PurposeDto> visitor_Purpose { get; set; }

        /// <summary>
        /// Gets or sets the visitor_Timestamp 
        /// </summary>
        [JsonProperty("timestamp")]
        public IList<Adm_StandardCodeCustom> visitor_Timestamp { get; set; }

    }
}