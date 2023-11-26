using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;

//WKK 20190403 RDT-183 [API] P.Account settings - Update profile picture
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// ctor
    /// </summary>
    public class UpdatePicture_Request
    {
        /// <summary>
        /// Gets or Sets the media
        /// </summary>
        [JsonProperty("media")]
        public MediaCustom media { get; set; }
    }
}
