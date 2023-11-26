using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Account.DTOs;

// WKK 20190312 RDT-61 [API] Account - registration
namespace Nop.Plugin.Api.Models.Account.Request
{
    /// <summary>
    /// Register Request parameters
    /// </summary>
    public class Register_Request
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Register_Request()
        {

        }

        /// <summary>
        /// Gets or sets the userDto
        /// </summary>
        [JsonProperty("userDto")]
        public UserDto userDto { get; set; }

        /// <summary>
        /// Gets or sets the signature
        /// </summary>
        [JsonProperty("signature")]
        public string signature { get; set; }

    }
}
