using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Data;
using Nop.Core.Domain.Msp.Setting;
using Nop.Core.Enumeration;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Announcements
{ 
    /// <summary>
    /// Announcements Service
    /// </summary>
    public partial class GrcAnnouncementsService : IGrcAnnouncementsService
    {
        #region Fields
        private readonly IRepository<MSP_GrcAnnouncement> _mspGrcAnnouncementRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IMspHelper _mspHelper;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mspAnnouncementContentRepository"></param>
        /// <param name="localizationService"></param>
        /// <param name="eventPublisher"></param>
        public GrcAnnouncementsService(
            IRepository<MSP_GrcAnnouncement> mspGrcAnnouncementRepository,
            ILocalizationService localizationService,
            IDateTimeHelper dateTimeHelper,
            IMspHelper mspHelper,
            IEventPublisher eventPublisher
        )
        {
            this._mspGrcAnnouncementRepository = mspGrcAnnouncementRepository;
            this._localizationService = localizationService;
            this._eventPublisher = eventPublisher;
            this._dateTimeHelper = dateTimeHelper;
            this._mspHelper = mspHelper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Populate published status list
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetPublishedValues() //WilliamHo 20180904 MSP-99
        {
            var _publishValues = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.GrcAnnouncements.List.SearchPublished.All"), Value = "2" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.GrcAnnouncements.List.SearchPublished.Active"), Value = "0" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.GrcAnnouncements.List.SearchPublished.NotActive"), Value = "1" }
            };
            return _publishValues;
        }

        #region Pagination White Mouse
        /// <summary>
        /// Gets all announcements
        /// </summary>
        /// <param name="createFromUtc"></param>
        /// <param name="createToUtc"></param>
        /// <param name="publishFromUtc"></param>
        /// <param name="publishToUtc"></param>
        /// <param name="publishedId"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        public virtual IPagedList<MSP_GrcAnnouncement> GetAllAnnouncements(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            bool? active = null, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue) //WilliamHo 20180905 MSP-99
        {
            string announcement = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();

            var query = _mspGrcAnnouncementRepository.Table;

            if (active.HasValue)
                query = query.Where(x => x.IsActive == active);

            if (createFromUtc.HasValue)
            {
                var createFrom = _dateTimeHelper.ConvertToUtcTime(createFromUtc.Value);
                query = query.Where(x => x.CreatedOnUtc >= createFrom);
            }

            if (createToUtc.HasValue)
            {
                var createTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(createToUtc.Value));
                query = query.Where(x => x.CreatedOnUtc <= createTo);
            }

            if (publishFromUtc.HasValue)
            {
                var publishFrom = _dateTimeHelper.ConvertToUtcTime(publishFromUtc.Value);
                query = query.Where(x => x.PublishedOnUtc >= publishFrom);
            }

            if (publishToUtc.HasValue)
            {
                var publishTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(publishToUtc.Value));
                query = query.Where(x => x.ExpiredOnUtc <= publishTo);    
            }

            if (!string.IsNullOrEmpty(commentText))
            {
                query = query.Where(x => x.Content1_EN.Contains(commentText) || x.Content2_EN.Contains(commentText) || x.Content1_CN.Contains(commentText) || x.Content2_CN.Contains(commentText)); 
            }

            query = query.OrderByDescending(x => x.CreatedOnUtc);

            var msp_Announce = new PagedList<MSP_GrcAnnouncement>(query, pageIndex, pageSize);

            return msp_Announce;             
        }


        #endregion 


        /// <summary>
        /// Gets an announcement content by id and content type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual MSP_GrcAnnouncement GetAnnouncementById(int id)
        {
            if (id == 0) return null;
            
            var result = (from x in _mspGrcAnnouncementRepository.Table
                         where x.Id == id
                         select x).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Inserts a new announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        public virtual void InsertAnnouncementContent(MSP_GrcAnnouncement announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));

			_mspGrcAnnouncementRepository.Insert(announcement);

            //event notification
            _eventPublisher.EntityInserted(announcement);
        }

        /// <summary>
        /// Updates an announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        public virtual void UpdateAnnouncementContent(MSP_GrcAnnouncement announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));

			_mspGrcAnnouncementRepository.Update(announcement);

            //event notification
            _eventPublisher.EntityUpdated(announcement);
        }

        /// <summary>
        /// Deletes an announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        public virtual void DeleteAnnouncementContent(MSP_GrcAnnouncement announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));

			_mspGrcAnnouncementRepository.Delete(announcement);

            //event notification
            _eventPublisher.EntityDeleted(announcement);
        }

		#endregion
	}
}
