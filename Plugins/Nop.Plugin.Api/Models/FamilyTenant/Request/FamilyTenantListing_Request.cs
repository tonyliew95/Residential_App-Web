using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Base;

//Tony Liew 20190403 RDT-175 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.Request
{
    /// <summary>
    /// FamilyListing input parameter
    /// </summary>
    public class FamilyTenantListing_Request : ApiFilterParamPagination
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
        /// Gets or sets the type 
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }
    }
}
//Tony Liew 20190403 RDT-175 /\
