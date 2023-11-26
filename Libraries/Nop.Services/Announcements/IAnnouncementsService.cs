using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Msp.Setting;
using System;
using System.Collections.Generic;
using Nop.Core; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Announcements
{
    /// <summary>
    /// Announcements service interface
    /// Shareable betweens Announcement, Promotion, User Guide and Video Guide
    /// </summary>
    public partial interface IAnnouncementsService
    {
        /// <summary>
        /// Retrieve published value in LocaleStringResource
        /// </summary>
        /// <returns></returns>
        IList<SelectListItem> GetPublishedValues(); //WilliamHo 20180904 MSP-99

        #region Pagination White Mouse - Erictan
        /// <summary>
        /// Retrieve announcements records
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <param name="createFromUtc">Create From UTC</param>
        /// <param name="createToUtc">Create To UTC</param>
        /// <param name="publishFromUtc">Publish From</param>
        /// <param name="publishToUtc">Publish To</param>
        /// <param name="published">Published</param>
        /// <param name="commentText">Comment Text</param>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="pageSize">Pase Size</param>
        /// <returns></returns>
        IPagedList<MSP_Announce_Content> GetAllAnnouncements(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int published = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue); //WilliamHo 20180905 MSP-99

        ///// <summary>
        ///// Retrieve announcements records
        ///// </summary>
        ///// <param name="pageIndex"></param>
        ///// <param name="pageSize"></param>
        ///// <param name="createFromUtc"></param>
        ///// <param name="createToUtc"></param>
        ///// <param name="publishFromUtc"></param>
        ///// <param name="publishToUtc"></param>
        ///// <param name="published"></param>
        ///// <param name="commentText"></param>
        ///// <returns></returns>
        //IList<MSP_Announce_Content> GetAllAnnouncements(DateTime? createFromUtc = null, DateTime? createToUtc = null, 
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int published = 0, string commentText = null); //WilliamHo 20180905 MSP-99
        #endregion 

        /// <summary>
        /// Retrieve announcement by Id and Content Type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentType">Announcement, Promotion, User Guide or Video Guide</param>
        /// <returns></returns>
        MSP_Announce_Content GetAnnouncementById(int id, string contentType);

        /// <summary>
        /// Inserts a new announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        void InsertAnnouncementContent(MSP_Announce_Content announcement);

        /// <summary>
        /// Updates an announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        void UpdateAnnouncementContent(MSP_Announce_Content announcement);

        /// <summary>
        /// Deletes an announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        void DeleteAnnouncementContent(MSP_Announce_Content announcement);

        /// <summary>
        /// Populate published status list for userguides
        /// </summary>
        /// <returns></returns>
        IList<SelectListItem> GetPromotionsPublishedValues(); //WilliamHo 20180904 MSP-99

        /// <summary>
        /// Populate published status list for promotions
        /// </summary>
        /// <returns></returns>
        IList<SelectListItem> GetUserGuidesPublishedValue(); //wailiang 20180910 MSP-101

        /// <summary>
        /// Populate published status list for video guides
        /// </summary>
        /// <returns></returns>
        IList<SelectListItem> GetVideoGuidePublishedValue(); //Atiqah 20180912 MSP-102

        /// <summary>
        /// Gets all userguides content
        /// </summary>
        /// <param name="createFromUtc"></param>
        /// <param name="createToUtc"></param>
        /// <param name="publishFromUtc"></param>
        /// <param name="publishToUtc"></param>
        /// <param name="publishedId"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        IList<MSP_Announce_Content> GetAllUserGuides(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int publishedId = 0, string commentText = null); //wailiang 20180910 MSP-101

        /* WilliamHo 20180925 Change to PageList
        /// <summary>
        /// Gets all promotions content
        /// </summary>
        /// <param name="createFromUtc"></param>
        /// <param name="createToUtc"></param>
        /// <param name="publishFromUtc"></param>
        /// <param name="publishToUtc"></param>
        /// <param name="publishedId"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        IList<MSP_Announce_Content> GetAllPromotions(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int publishedId = 0, string commentText = null); //WilliamHo 20180910 MSP-100
        */

        //WilliamHo 20180925 Change to PageList
        /// <summary>
        /// Gets all promotions content
        /// </summary>
        /// <param name="createFromUtc"></param>
        /// <param name="createToUtc"></param>
        /// <param name="publishFromUtc"></param>
        /// <param name="publishToUtc"></param>
        /// <param name="publishedId"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        IPagedList<MSP_Announce_Content> GetAllPromotions(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int published = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue);//WilliamHo 20180910 MSP-100

        /// <summary>
        /// Gets all video guides content
        /// </summary>
        /// <param name="createFromUtc"></param>
        /// <param name="createToUtc"></param>
        /// <param name="publishFromUtc"></param>
        /// <param name="publishToUtc"></param>
        /// <param name="publishedId"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        IPagedList<MSP_Announce_Content> GetAllVideoGuides(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue); //Atiqah 20180912 MSP-102

        //#region Duplicate Services wailiang 20181226 MDT-134, MDT-150, MDT-151, MDT-152
        ///// <summary>
        ///// Retrieve role announcements records
        ///// </summary>
        ///// <param name="pageIndex">Page Index</param>
        ///// <param name="pageSize">Page Size</param>
        ///// <param name="createFromUtc">Create From UTC</param>
        ///// <param name="createToUtc">Create To UTC</param>
        ///// <param name="publishFromUtc">Publish From</param>
        ///// <param name="publishToUtc">Publish To</param>
        ///// <param name="published">Published</param>
        ///// <param name="commentText">Comment Text</param>
        ///// <param name="pageIndex">Page Index</param>
        ///// <param name="pageSize">Pase Size</param>
        ///// <returns></returns>
        //IPagedList<MSP_Announce_Content> GetRoleAnnouncements(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue); //wailiang 20181218 MDT-134

        ///// <summary>
        ///// Gets all promotions content
        ///// </summary>
        ///// <param name="createFromUtc"></param>
        ///// <param name="createToUtc"></param>
        ///// <param name="publishFromUtc"></param>
        ///// <param name="publishToUtc"></param>
        ///// <param name="publishedId"></param>
        ///// <param name="commentText"></param>
        ///// <returns></returns>
        //IPagedList<MSP_Announce_Content> GetRolePromotions(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int published = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue);//wailiang 20181219 MDT-150

        ///// <summary>
        ///// Gets all userguides content
        ///// </summary>
        ///// <param name="createFromUtc"></param>
        ///// <param name="createToUtc"></param>
        ///// <param name="publishFromUtc"></param>
        ///// <param name="publishToUtc"></param>
        ///// <param name="publishedId"></param>
        ///// <param name="commentText"></param>
        ///// <returns></returns>
        //IList<MSP_Announce_Content> GetRoleUserGuides(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int publishedId = 0, string commentText = null); //wailiang 20181219 MDT-151

        ///// <summary>
        ///// Gets all video guides content
        ///// </summary>
        ///// <param name="createFromUtc"></param>
        ///// <param name="createToUtc"></param>
        ///// <param name="publishFromUtc"></param>
        ///// <param name="publishToUtc"></param>
        ///// <param name="publishedId"></param>
        ///// <param name="commentText"></param>
        ///// <returns></returns>
        //IPagedList<MSP_Announce_Content> GetRoleVideoGuides(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue); //wailiang 20181219 MDT-152
        //#endregion

        #region Comment Code Tony Liew 20181217  
        ///// <summary>
        ///// Ensure no new insert shutdown announcement overlap with existing shutdown announcement 
        ///// </summary>
        ///// <param name = "editShutdownDateStart" ></ param >
        ///// < param name="editShutdownDateEnd"></param>
        ///// <returns></returns>
        ////bool IsNotOverlapShutdownAnnouncement(DateTime? modelInputStart, DateTime? modelInputEnd);
        #endregion
    }
}
