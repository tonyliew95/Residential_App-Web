using Newtonsoft.Json;

// Tony Liew 20190412 RDT-68 \/
namespace Nop.Plugin.Api.Models.Profile.DTOs
{
    /// <summary>
    /// AddContactNumberDto class
    /// </summary>
    public class AddContactNumberDto
    {
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
        /// Gets or sets the primary 
        /// </summary>
        [JsonProperty("primary")]
        public bool primary { get; set; }
    }
}
// Tony Liew 20190412 RDT-68 /\