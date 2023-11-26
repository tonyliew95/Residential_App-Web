using Newtonsoft.Json;
//Tony Liew 20190308 RDT-69 \/
namespace Nop.Plugin.Api.Models.Profile.DTOs
{
    /// <summary>
    /// passwordDto class
    /// </summary>
    [JsonObject(Title = "passwordDto")]
    public class PasswordDto
    {
        /// <summary>
        /// Gets or sets the currentPassword 
        /// </summary>
        [JsonProperty("currentPassword")]
        public string currentPassword { get; set; }


        /// <summary>
        /// Gets or sets the newPassword 
        /// </summary>
        [JsonProperty("newPassword")]
        public string newPassword { get; set; }

    }
}
//Tony Liew 20190308 RDT-69 /\
