using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Tony Liew 20190416 RDT-178 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.Request
{
    /// <summary>
    /// RemoveFamilyTenant Request
    /// </summary>
    public class RemoveFamilyTenant_Request 
    {
        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

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

// Tony Liew 20190416 RDT-178 /\