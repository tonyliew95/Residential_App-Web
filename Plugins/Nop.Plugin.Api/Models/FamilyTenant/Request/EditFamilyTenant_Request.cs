using Newtonsoft.Json;

//Tony Liew 20190408 RDT-177 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.Request
{
    /// <summary>
    /// Edit Family Tenant Class
    /// </summary>
    public class EditFamilyTenant_Request
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

        /// <summary>
        /// Gets or sets the countryCode 
        /// </summary>
        [JsonProperty("countryCode")]
        public string countryCode { get; set; }

        /// <summary>
        /// Gets or sets the msisdn 
        /// </summary>
        [JsonProperty("msisdn")]
        public string msisdn { get; set; }

        /// <summary>
        /// Gets or sets the emergency 
        /// </summary>
        [JsonProperty("emergency")]
        public bool emergency { get; set; }

        /// <summary>
        /// Gets or sets the ocpyStart 
        /// </summary>
        [JsonProperty("ocpyStart")]
        public string ocpyStart { get; set; }

        /// <summary>
        /// Gets or sets the ocpyEnd 
        /// </summary>
        [JsonProperty("ocpyEnd")]
        public string ocpyEnd { get; set; }

        /// <summary>
        /// Gets or sets the reminder 
        /// </summary>
        [JsonProperty("reminder")]
        public string reminder { get; set; }

        /// <summary>
        /// Gets or sets the info 
        /// </summary>
        [JsonProperty("info")]
        public string info { get; set; }
    }
}
//Tony Liew 20190408 RDT-177 /\