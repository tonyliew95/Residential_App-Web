// WKK 20190306 RDT-63 [API] Account - login
using Newtonsoft.Json;

namespace Nop.Plugin.Api.Models.Account.DTOs
{
    /// <summary>
    /// User Login Dto
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }

        ///// <summary>
        ///// Gets or sets the mobile number
        ///// </summary>
        //[JsonProperty("mobile")]
        //public string Mobile { get; set; }
    }
}