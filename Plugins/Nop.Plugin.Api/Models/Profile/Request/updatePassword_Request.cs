using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Profile.DTOs;

//Tony Liew 20190311 RDT-69 \/
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// ctor
    /// </summary>
    public class UpdatePassword_Request
    {
        /// <summary>
        /// Gets or Sets the password
        /// </summary>
        [JsonProperty("passwordDto")]
        public PasswordDto password { get; set; }

        /// <summary>
        /// Gets or Sets the signature
        /// </summary>
        [JsonProperty("signature")]
        public string signature { get; set; }
    }
}
//Tony Liew 20190311 RDT-69 /\
