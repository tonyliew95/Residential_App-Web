using Newtonsoft.Json;

//Tony Liew 20190319 RDT-67 \/
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// Update Mobile Via Email request
    /// </summary>
    public class UpdateContact_Request
    {
        /// <summary>
        /// Gets or Sets the contactId
        /// </summary>
        [JsonProperty("contactId")]
        public int contactId { get; set; }

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
        /// Gets or Sets the primary
        /// </summary>
        [JsonProperty("primary")]
        public bool primary { get; set; }

    }
}
//Tony Liew 20190319 RDT-67 /\
