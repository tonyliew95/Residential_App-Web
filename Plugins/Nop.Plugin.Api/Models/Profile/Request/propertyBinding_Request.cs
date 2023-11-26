using Newtonsoft.Json;

//WKK 20190410 RDT-168 [API] Property Unit - Bind new property
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// ctor
    /// </summary>
    public class PropertyBinding_Request
    {
        /// <summary>
        /// Gets or Sets the activation Code
        /// </summary>
        [JsonProperty("activationCode")]
        public string activationCode { get; set; }

        /// <summary>
        /// Gets or Sets the identity Number
        /// </summary>
        [JsonProperty("identityNumber")]
        public string identityNumber { get; set; }

        /// <summary>
        /// Gets or Sets the default property
        /// </summary>
        [JsonProperty("default")]
        public bool defaultProperty { get; set; }
    }
}
