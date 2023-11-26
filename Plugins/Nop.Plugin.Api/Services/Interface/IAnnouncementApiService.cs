using Nop.Plugin.Api.Models.Announcement.DTOs;
using Nop.Plugin.Api.Models.Announcement.Request;
using Nop.Plugin.Api.Models.Announcement.ResponseResults;

namespace Nop.Plugin.Api.Services.Interfaces
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IAnnouncementApiService
    {
        //WKK 20190315 RDT-122 [API] Notice - Announcement detail
        /// <summary>
        /// Get Announcement Details
        /// </summary>
        /// <param name="announcementId"></param>
        /// <returns></returns>
        AnnouncementDetailsDto GetAnnouncementDetails(int announcementId);

        //WKK 20190315 RDT-121 [API] Notice - Announcement list
        /// <summary>
        /// Get Announcement List
        /// </summary>
        /// <param name="param"></param>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        AnnouncementList_ResponseResult GetAnnouncementList(AnnouncementList_Request param, int CustomerId); 
    }
}
