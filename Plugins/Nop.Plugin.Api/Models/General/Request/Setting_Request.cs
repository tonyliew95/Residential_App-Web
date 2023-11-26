using Newtonsoft.Json;


namespace Nop.Plugin.Api.Models.General.Request
{
    /// <summary>
    /// Setting Request paramaters
    /// </summary>
    public class Setting_Request
    {
        /// <summary>
        /// Gets or sets the signature
        /// </summary>
        [JsonProperty("signature")]
        public string signature { get; set; }
    }
}
