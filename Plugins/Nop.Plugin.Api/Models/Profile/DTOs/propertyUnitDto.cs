using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;

// Tony Liew 20190315 RDT-65 \/
namespace Nop.Plugin.Api.Models.Profile.DTOs
{
    /// <summary>
    /// propertyUnitDto Class
    /// </summary>
    public class propertyUnitDto_duplicated // 20190408 WKK this class propertyUnitDto duplicated with Nop.Core.Domain.Residential.Custom.Mnt_UserPropertyCustom
    {
        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the orgName 
        /// </summary>
        [JsonProperty("orgName")]
        public string orgName { get; set; }

        /// <summary>
        /// Gets or sets the orgImage 
        /// </summary>
        [JsonProperty("orgImage")]
        public string orgImage { get; set; }

        /// <summary>
        /// Gets or sets the Account Type 
        /// </summary>
        [JsonProperty("accPropType")]
        public string accPropType { get; set; }

        /// <summary>
        /// Gets or sets the reId 
        /// </summary>
        [JsonProperty("reId")]
        public string reId { get; set; }

        /// <summary>
        /// Gets or sets the user property id 
        /// </summary>
        [JsonProperty("id")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the block 
        /// </summary>
        [JsonProperty("block")]
        public string block { get; set; }

        /// <summary>
        /// Gets or sets the level 
        /// </summary>
        [JsonProperty("level")]
        public string level { get; set; }

        /// <summary>
        /// Gets or sets the unit 
        /// </summary>
        [JsonProperty("unit")]
        public string unit { get; set; }

        // WKK 20190405 RDT-164 [API] Profile - Detail
        /// <summary>
        /// Gets or sets the module 
        /// </summary>
        [JsonProperty("module")]
        public moduleDtoCustom moduleDto { get; set; }
    }
}
// Tony Liew 20190315 RDT-65 /\