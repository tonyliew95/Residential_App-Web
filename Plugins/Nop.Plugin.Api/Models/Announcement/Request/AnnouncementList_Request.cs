using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Base;

//WKK 20190315 RDT-121 \/
namespace Nop.Plugin.Api.Models.Announcement.Request
{
    /// <summary>
    /// Announcement list input parameters
    /// </summary>
    public class AnnouncementList_Request : ApiFilterParamPagination
    {
        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }
    }
}
