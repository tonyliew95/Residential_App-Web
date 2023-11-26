using Newtonsoft.Json;

//WKK 20190315 RDT-121 \/
namespace Nop.Plugin.Api.Models.Announcement.DTOs
{
    /// <summary>
    /// announcementMediaDto class
    /// </summary>
    public class announcementMediaDto
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
