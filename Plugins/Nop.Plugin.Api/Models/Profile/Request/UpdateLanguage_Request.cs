using Newtonsoft.Json;

namespace Nop.Plugin.Api.Models.Profile.Request
{
    /// <summary>
    /// ctor
    /// </summary>
    public class UpdateLanguage_Request
    {
        /// <summary>
        /// Gets or Sets the 
        /// </summary>
        [JsonProperty("languageCode")]
        public string languageCode { get; set; }

    }
}
