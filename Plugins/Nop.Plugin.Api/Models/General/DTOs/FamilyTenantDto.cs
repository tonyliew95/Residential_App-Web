using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.General.DTOs
{
    /// <summary>
    /// FamilyTenantDto Class
    /// </summary>
    public class FamilyTenantDto
    {
        public FamilyTenantDto()
        {
            familyAccType = new List<Adm_StandardCodeCustom>();
            tenantAccType = new List<Adm_StandardCodeCustom>();
        }

        /// <summary>
        /// Gets or sets the familyAccType 
        /// </summary>
        [JsonProperty("familyAccType")]
        public IList<Adm_StandardCodeCustom> familyAccType { get; set; }

        /// <summary>
        /// Gets or sets the tenantAccType 
        /// </summary>
        [JsonProperty("tenantAccType")]
        public IList<Adm_StandardCodeCustom> tenantAccType { get; set; }

    }
}