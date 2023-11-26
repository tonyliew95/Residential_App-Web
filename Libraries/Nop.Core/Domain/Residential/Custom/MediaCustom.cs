using Newtonsoft.Json;
////WKK 20190315 RDT-121 \/
namespace Nop.Core.Domain.Residential.Custom
{
    /// <summary>
    /// MediaCustom class
    /// </summary>
    public class MediaCustom
    {
        /// <summary>
        /// Gets or sets the type 
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the content 
        /// </summary>
        [JsonProperty("content")]
        public string content { get; set; }
    }
}
////WKK 20190315 RDT-121 /\
