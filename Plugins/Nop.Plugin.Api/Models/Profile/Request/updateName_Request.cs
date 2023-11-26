using Newtonsoft.Json;

//WKK 20190326 RDT-173 [API] P.Account settings - Change display name
namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// ctor
    /// </summary>
    public class UpdateName_Request
    {
        /// <summary>
        /// Gets or Sets the Name
        /// </summary>
        [JsonProperty("displayName")]
        public string displayName { get; set; }
    }
}
