using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// ModuleDto Class
    /// </summary>
    public class ModuleDto
    {
        public ModuleDto()
        {
            visibleType = new List<Adm_StandardCodeCustom>();
        }

        /// <summary>
        /// Gets or sets the visibleType 
        /// </summary>
        [JsonProperty("visibleType")]
        public IList<Adm_StandardCodeCustom> visibleType { get; set; }
        
    }
}