using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// PropertyDto Class
    /// </summary>
    public class PropertyDto
    {
        public PropertyDto()
        {
            accType = new List<Adm_StandardCodeCustom>();
        }

        /// <summary>
        /// Gets or sets the accType 
        /// </summary>
        [JsonProperty("accType")]
        public IList<Adm_StandardCodeCustom> accType { get; set; }
    }
}