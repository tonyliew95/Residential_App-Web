using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Profile.DTOs;

// Tony Liew 20190412 RDT-68 \/
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// Add Contact Number Request
    /// </summary>
    public class AddContactNumber_Request
    {
        /// <summary>
        /// Gets or Sets the contactDto
        /// </summary>
        [JsonProperty("contactDto")]
        public AddContactNumberDto contactDto { get; set; }

        /// <summary>
        /// Gets or Sets the signature
        /// </summary>
        [JsonProperty("signature")]
        public string signature { get; set; }
    }
}
// Tony Liew 20190412 RDT-68 /\