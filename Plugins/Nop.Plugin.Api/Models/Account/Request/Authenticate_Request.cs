using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Account.DTOs;
using Nop.Plugin.Api.Models.Base;

// WKK 20190306 RDT-63 [API] Account - login
namespace Nop.Plugin.Api.Models.Account.Request
{
    /// <summary>
    /// Authenticate Request parameters
    /// </summary>
    public class Authenticate_Request
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Authenticate_Request()
        {
        }

        /// <summary>
        /// Gets or sets the user login request parameters
        /// </summary>
        [JsonProperty("userDto")]
        public LoginRequest loginRequest { get; set; }

        /// <summary>
        /// Gets or sets the signature
        /// </summary>
        [JsonProperty("signature")]
        public string signature { get; set; }

    }

    /// <summary>
    /// Store a class for user login request parameters
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email 
        /// </summary>
        [JsonProperty("email")]
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the encrypted password 
        /// </summary>
        [JsonProperty("password")]
        public string password { get; set; }
    }
}
