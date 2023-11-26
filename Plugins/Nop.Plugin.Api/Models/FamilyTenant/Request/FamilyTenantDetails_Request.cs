using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Tony Liew 20190416 RDT-186 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.Request
{
    /// <summary>
    /// Family Tenant Details Request
    /// </summary>
    public class FamilyTenantDetails_Request
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
        /// Gets or sets the accId 
        /// </summary>
        [JsonProperty("accId")]
        public int accId { get; set; }

        /// <summary>
        /// Gets or sets the type 
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }
    }
}
// Tony Liew 20190416 RDT-186 /\
