using Newtonsoft.Json;

//WKK 20190327 RDT-167 [API] Property Unit - Set default property
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// ctor
    /// </summary>
    public class UpdateDefaultProp_Request
    {
        /// <summary>
        /// Gets or Sets the org Id
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or Sets the prop Id
        /// </summary>
        [JsonProperty("propId")]
        public int propId { get; set; }
    }
}
