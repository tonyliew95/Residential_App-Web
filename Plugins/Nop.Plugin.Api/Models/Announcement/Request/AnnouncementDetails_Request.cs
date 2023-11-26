using Newtonsoft.Json;

//WKK 20190315 RDT-122 [API] Notice - Announcement detail
namespace Nop.Plugin.Api.Models.Announcement.Request
{
    /// <summary>
    /// Announcement Details input parameters
    /// </summary>
    public class AnnouncementDetails_Request
    {
        /// <summary>
        /// Gets or sets the id 
        /// </summary>
        [JsonProperty("id")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }
    }
}
