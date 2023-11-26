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
    public partial interface IGrcAnnouncementsService
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
        IPagedList<MSP_GrcAnnouncement> GetAllAnnouncements(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
			bool? active = null, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue); //WilliamHo 20180905 MSP-99

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
		MSP_GrcAnnouncement GetAnnouncementById(int id);

        /// <summary>
        /// Inserts a new announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        void InsertAnnouncementContent(MSP_GrcAnnouncement announcement);

        /// <summary>
        /// Updates an announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        void UpdateAnnouncementContent(MSP_GrcAnnouncement announcement);

        /// <summary>
        /// Deletes an announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        void DeleteAnnouncementContent(MSP_GrcAnnouncement announcement);
    }
}
