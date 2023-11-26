using Newtonsoft.Json;

//WKK 20190408 RDT-188 [API] P.Account settings - update identity number
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// Update identity number request
    /// </summary>
    public class UpdateIC_Request
    {
        /// <summary>
        /// Gets or Sets the identity
        /// </summary>
        [JsonProperty("identity")]
        public Identity identity { get; set; }
    }

    /// <summary>
    /// Identity
    /// </summary>
    public class Identity
    {
        /// <summary>
        /// Gets or Sets the IC type - X or N or A or P
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }

        /// <summary>
        /// Gets or Sets the IC number
        /// </summary>
        [JsonProperty("number")]
        public string number { get; set; }
    }

}
