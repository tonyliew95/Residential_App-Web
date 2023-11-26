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
    public partial class AnnouncementsService : IAnnouncementsService
    {
        #region Fields
        private readonly IRepository<MSP_Announce_Content> _mspAnnouncementContentRepository;
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
        public AnnouncementsService(
            IRepository<MSP_Announce_Content> mspAnnouncementContentRepository,
            ILocalizationService localizationService,
            IDateTimeHelper dateTimeHelper,
            IMspHelper mspHelper,
            IEventPublisher eventPublisher
        )
        {
            this._mspAnnouncementContentRepository = mspAnnouncementContentRepository;
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
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.List.SearchPublished.All"), Value = "2" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.List.SearchPublished.Published"), Value = "0" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.List.SearchPublished.Unpublished"), Value = "1" }
            };
            return _publishValues;
        }

        /// <summary>
        /// Populate published status list for promotions
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetPromotionsPublishedValues() //WilliamHo 20180910 MSP-100
        {
            var _publishValues = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.PromotionsItems.List.SearchPublished.All"), Value = "2" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.PromotionsItems.List.SearchPublished.Published"), Value = "0" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.PromotionsItems.List.SearchPublished.Unpublished"), Value = "1" }
            };
            return _publishValues;
        }

        /// <summary>
        /// Populate published status list for userguides
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetUserGuidesPublishedValue() //wailiang 20180910 MSP-101
        {
            var _publishValues = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.UserGuides.UserGuidesItems.List.SearchPublished.All"), Value = "2" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.UserGuides.UserGuidesItems.List.SearchPublished.Published"), Value = "0" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.UserGuides.UserGuidesItems.List.SearchPublished.Unpublished"), Value = "1" }
            };
            return _publishValues;
        }

        /// <summary>
        /// Populate published status list for videoguides
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetVideoGuidePublishedValue() //Atiqah 20180912 MSP-102
        {
            var _publishValues = new List<SelectListItem>
            {
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.VideoGuideItems.List.SearchPublished.All"), Value = "2" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.VideoGuideItems.List.SearchPublished.Published"), Value = "0" },
                new SelectListItem { Text = _localizationService.GetResource("Admin.ContentManagement.Announcements.VideoGuideItems.List.SearchPublished.Unpublished"), Value = "1" }
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
        public virtual IPagedList<MSP_Announce_Content> GetAllAnnouncements(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue) //WilliamHo 20180905 MSP-99
        {
            string announcement = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();

            var query = _mspAnnouncementContentRepository.Table;
            query = query.Where(x => x.ContentType == announcement);

            string published = string.Empty;
            if (publishedId < 2)
                published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
                    : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

            if (!string.IsNullOrEmpty(published))
                query = query.Where(x => x.Status == published);

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
                //query = query.Where(x => x.PublishedOnUtc <= publishTo);   //LeeChurn 20181009 MSP-225
                query = query.Where(x => x.ExpiredOnUtc <= publishTo);     //LeeChurn 20181009 MSP-225
            }

            if (!string.IsNullOrEmpty(commentText))
            {
                //query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText)); //Comment by WilliamHo 20181226 MDT-177
                query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText) || x.ContentTitle_CN.Contains(commentText) || x.Content_CN.Contains(commentText)); //WilliamHo 20181226 MDT-177
            }

            query = query.OrderByDescending(x => x.CreatedOnUtc);

            var msp_Announce = new PagedList<MSP_Announce_Content>(query, pageIndex, pageSize);

            return msp_Announce;             
        }


        ///// <summary>
        ///// Gets all announcements
        ///// </summary>
        ///// <param name="createFromUtc"></param>
        ///// <param name="createToUtc"></param>
        ///// <param name="publishFromUtc"></param>
        ///// <param name="publishToUtc"></param>
        ///// <param name="publishedId"></param>
        ///// <param name="commentText"></param>
        ///// <returns></returns>
        //public virtual IList<MSP_Announce_Content> GetAllAnnouncements(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int publishedId = 0, string commentText = null) //WilliamHo 20180905 MSP-99
        //{
        //    string announcement = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();

        //    var query = _mspAnnouncementContentRepository.Table;
        //    query = query.Where(x => x.ContentType == announcement);

        //    string published = string.Empty;
        //    if (publishedId < 2)
        //        published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
        //            : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

        //    if (!string.IsNullOrEmpty(published))
        //        query = query.Where(x => x.Status == published);

        //    if (createFromUtc.HasValue)
        //    {
        //        var createFrom = _dateTimeHelper.ConvertToUtcTime(createFromUtc.Value);
        //        query = query.Where(x => x.CreatedOnUtc >= createFrom);
        //    }

        //    if (createToUtc.HasValue)
        //    {
        //        var createTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(createToUtc.Value));
        //        query = query.Where(x => x.CreatedOnUtc <= createTo);
        //    }

        //    if (publishFromUtc.HasValue)
        //    {
        //        var publishFrom = _dateTimeHelper.ConvertToUtcTime(publishFromUtc.Value);
        //        query = query.Where(x => x.PublishedOnUtc >= publishFrom);
        //    }

        //    if (publishToUtc.HasValue)
        //    {
        //        var publishTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(publishToUtc.Value));
        //        query = query.Where(x => x.PublishedOnUtc <= publishTo);
        //    }

        //    if (!string.IsNullOrEmpty(commentText))
        //    {
        //        query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText));
        //    }

        //    query = query.OrderByDescending(x => x.CreatedOnUtc);

        //    return query.ToList();
        //}
        #endregion 


        /// <summary>
        /// Gets an announcement content by id and content type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual MSP_Announce_Content GetAnnouncementById(int id, string contentType)
        {
            if (id == 0) return null;
            
            var result = (from x in _mspAnnouncementContentRepository.Table
                         where x.ContentType == contentType && x.Id == id
                         select x).FirstOrDefault();
            return result;
        }

        /// <summary>
        /// Inserts a new announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        public virtual void InsertAnnouncementContent(MSP_Announce_Content announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));

            _mspAnnouncementContentRepository.Insert(announcement);

            //event notification
            _eventPublisher.EntityInserted(announcement);
        }

        /// <summary>
        /// Updates an announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        public virtual void UpdateAnnouncementContent(MSP_Announce_Content announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));

            _mspAnnouncementContentRepository.Update(announcement);

            //event notification
            _eventPublisher.EntityUpdated(announcement);
        }

        /// <summary>
        /// Deletes an announcement content
        /// </summary>
        /// <param name="announcement">Announcement, User Guide, Video Guide or Promotion model</param>
        public virtual void DeleteAnnouncementContent(MSP_Announce_Content announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement));

            _mspAnnouncementContentRepository.Delete(announcement);

            //event notification
            _eventPublisher.EntityDeleted(announcement);
        }

        /// <summary>
        /// Gets all userguides
        /// </summary>
        /// <param name="createFromUtc"></param>
        /// <param name="createToUtc"></param>
        /// <param name="publishFromUtc"></param>
        /// <param name="publishToUtc"></param>
        /// <param name="publishedId"></param>
        /// <param name="commentText"></param>
        /// <returns></returns>
        public virtual IList<MSP_Announce_Content> GetAllUserGuides(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int publishedId = 0, string commentText = null) //wailiang 20180905 MSP-101
        {
            string type = MSP_Announce_Content_ContentType.UserGuide.ToValue<MSP_Announce_Content_ContentType>();

            var query = _mspAnnouncementContentRepository.Table;
            query = query.Where(x => x.ContentType == type);

            string published = string.Empty;
            if (publishedId < 2)
                published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
                    : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

            if (!string.IsNullOrEmpty(published))
                query = query.Where(x => x.Status == published);

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
                //query = query.Where(x => x.PublishedOnUtc <= publishTo); //LeeChurn 20181010 MSP-229
                query = query.Where(x => x.ExpiredOnUtc <= publishTo);     //LeeChurn 20181010 MSP-229
            }

            if (!string.IsNullOrEmpty(commentText))
            {
                //query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText)); //Comment by WilliamHo 20181226 MDT-179
                query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText) || x.ContentTitle_CN.Contains(commentText) || x.Content_CN.Contains(commentText)); //WilliamHo 20181226 MDT-179
            }

            query = query.OrderByDescending(x => x.CreatedOnUtc);

            return query.ToList();
        }

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
        public virtual IPagedList<MSP_Announce_Content> GetAllPromotions(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue) //WilliamHo 20180910 MSP-100
        {
            string type = MSP_Announce_Content_ContentType.Promotion.ToValue<MSP_Announce_Content_ContentType>();

            var query = _mspAnnouncementContentRepository.Table;
            query = query.Where(x => x.ContentType == type);

            string published = string.Empty;
            if (publishedId < 2)
                published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
                    : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

            if (!string.IsNullOrEmpty(published))
                query = query.Where(x => x.Status == published);

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
                //query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText)); //Comment by WilliamHo 20181226 MDT-178
                query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText) || x.ContentTitle_CN.Contains(commentText) || x.Content_CN.Contains(commentText)); //WilliamHo 20181226 MDT-178
            }

            query = query.OrderByDescending(x => x.CreatedOnUtc);

            var msp_Announce = new PagedList<MSP_Announce_Content>(query, pageIndex, pageSize);

            return msp_Announce;
        }

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
        public virtual IPagedList<MSP_Announce_Content> GetAllVideoGuides(DateTime? createFromUtc = null, DateTime? createToUtc = null,
            DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
            int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue) //Atiqah 20180912 MSP-102
        {
            string type = MSP_Announce_Content_ContentType.Video.ToValue<MSP_Announce_Content_ContentType>();

            var query = _mspAnnouncementContentRepository.Table;
            query = query.Where(x => x.ContentType == type);

            string published = string.Empty;
            if (publishedId < 2)
                published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
                    : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

            if (!string.IsNullOrEmpty(published))
                query = query.Where(x => x.Status == published);

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
                query = query.Where(x => x.PublishedOnUtc <= publishTo);
            }

            if (!string.IsNullOrEmpty(commentText))
            {
                //query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText) || x.ContentName.Contains(commentText)); // Comment by WilliamHo 20181226 MDT-180
                query = query.Where(x => x.ContentTitle.Contains(commentText) || x.ContentName.Contains(commentText) || x.ContentTitle_CN.Contains(commentText)); //WilliamHo 20181226 MDT-180
            }

            query = query.OrderByDescending(x => x.CreatedOnUtc);

            //return query.ToList();

            //Atiqah 20180925 MSP-102 \/
            var VideoGuide = new PagedList<MSP_Announce_Content>(query, pageIndex, pageSize);

            return VideoGuide;
            //Atiqah 20180925 MSP-102 /\
        }
        #endregion

        //#region Duplicate Services wailiang 20181226 MDT-134, MDT-150, MDT-151, MDT-152
        ///// <summary>
        ///// Gets all announcements
        ///// </summary>
        ///// <param name="createFromUtc"></param>
        ///// <param name="createToUtc"></param>
        ///// <param name="publishFromUtc"></param>
        ///// <param name="publishToUtc"></param>
        ///// <param name="publishedId"></param>
        ///// <param name="commentText"></param>
        ///// <returns></returns>
        //public virtual IPagedList<MSP_Announce_Content> GetRoleAnnouncements(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue) //wailiang 20181218 MDT-134
        //{
        //    string announcement = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();

        //    var query = _mspAnnouncementContentRepository.Table;
        //    query = query.Where(x => x.ContentType == announcement);

        //    string published = string.Empty;
        //    if (publishedId < 2)
        //        published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
        //            : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

        //    if (!string.IsNullOrEmpty(published))
        //        query = query.Where(x => x.Status == published);

        //    if (createFromUtc.HasValue)
        //    {
        //        var createFrom = _dateTimeHelper.ConvertToUtcTime(createFromUtc.Value);
        //        query = query.Where(x => x.CreatedOnUtc >= createFrom);
        //    }

        //    if (createToUtc.HasValue)
        //    {
        //        var createTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(createToUtc.Value));
        //        query = query.Where(x => x.CreatedOnUtc <= createTo);
        //    }

        //    if (publishFromUtc.HasValue)
        //    {
        //        var publishFrom = _dateTimeHelper.ConvertToUtcTime(publishFromUtc.Value);
        //        query = query.Where(x => x.PublishedOnUtc >= publishFrom);
        //    }

        //    if (publishToUtc.HasValue)
        //    {
        //        var publishTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(publishToUtc.Value));
        //        query = query.Where(x => x.ExpiredOnUtc <= publishTo);
        //    }

        //    if (!string.IsNullOrEmpty(commentText))
        //    {
        //        query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText));
        //    }

        //    query = query.OrderByDescending(x => x.CreatedOnUtc);

        //    var msp_Announce = new PagedList<MSP_Announce_Content>(query, pageIndex, pageSize);

        //    return msp_Announce;
        //}

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
        //public virtual IPagedList<MSP_Announce_Content> GetRolePromotions(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue) //wailiang 20181219 MDT-150
        //{
        //    string type = MSP_Announce_Content_ContentType.Promotion.ToValue<MSP_Announce_Content_ContentType>();

        //    var query = _mspAnnouncementContentRepository.Table;
        //    query = query.Where(x => x.ContentType == type);

        //    string published = string.Empty;
        //    if (publishedId < 2)
        //        published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
        //            : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

        //    if (!string.IsNullOrEmpty(published))
        //        query = query.Where(x => x.Status == published);

        //    if (createFromUtc.HasValue)
        //    {
        //        var createFrom = _dateTimeHelper.ConvertToUtcTime(createFromUtc.Value);
        //        query = query.Where(x => x.CreatedOnUtc >= createFrom);
        //    }

        //    if (createToUtc.HasValue)
        //    {
        //        var createTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(createToUtc.Value));
        //        query = query.Where(x => x.CreatedOnUtc <= createTo);
        //    }

        //    if (publishFromUtc.HasValue)
        //    {
        //        var publishFrom = _dateTimeHelper.ConvertToUtcTime(publishFromUtc.Value);
        //        query = query.Where(x => x.PublishedOnUtc >= publishFrom);
        //    }

        //    if (publishToUtc.HasValue)
        //    {
        //        var publishTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(publishToUtc.Value));
        //        query = query.Where(x => x.ExpiredOnUtc <= publishTo);
        //    }

        //    if (!string.IsNullOrEmpty(commentText))
        //        query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText));

        //    query = query.OrderByDescending(x => x.CreatedOnUtc);

        //    var msp_Announce = new PagedList<MSP_Announce_Content>(query, pageIndex, pageSize);

        //    return msp_Announce;
        //}

        ///// <summary>
        ///// Gets all userguides
        ///// </summary>
        ///// <param name="createFromUtc"></param>
        ///// <param name="createToUtc"></param>
        ///// <param name="publishFromUtc"></param>
        ///// <param name="publishToUtc"></param>
        ///// <param name="publishedId"></param>
        ///// <param name="commentText"></param>
        ///// <returns></returns>
        //public virtual IList<MSP_Announce_Content> GetRoleUserGuides(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int publishedId = 0, string commentText = null) //wailiang 20181219 MDT-151
        //{
        //    string type = MSP_Announce_Content_ContentType.UserGuide.ToValue<MSP_Announce_Content_ContentType>();

        //    var query = _mspAnnouncementContentRepository.Table;
        //    query = query.Where(x => x.ContentType == type);

        //    string published = string.Empty;
        //    if (publishedId < 2)
        //        published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
        //            : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

        //    if (!string.IsNullOrEmpty(published))
        //        query = query.Where(x => x.Status == published);

        //    if (createFromUtc.HasValue)
        //    {
        //        var createFrom = _dateTimeHelper.ConvertToUtcTime(createFromUtc.Value);
        //        query = query.Where(x => x.CreatedOnUtc >= createFrom);
        //    }

        //    if (createToUtc.HasValue)
        //    {
        //        var createTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(createToUtc.Value));
        //        query = query.Where(x => x.CreatedOnUtc <= createTo);
        //    }

        //    if (publishFromUtc.HasValue)
        //    {
        //        var publishFrom = _dateTimeHelper.ConvertToUtcTime(publishFromUtc.Value);
        //        query = query.Where(x => x.PublishedOnUtc >= publishFrom);
        //    }

        //    if (publishToUtc.HasValue)
        //    {
        //        var publishTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(publishToUtc.Value));
        //        query = query.Where(x => x.ExpiredOnUtc <= publishTo);
        //    }

        //    if (!string.IsNullOrEmpty(commentText))
        //        query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText));

        //    query = query.OrderByDescending(x => x.CreatedOnUtc);

        //    return query.ToList();
        //}

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
        //public virtual IPagedList<MSP_Announce_Content> GetRoleVideoGuides(DateTime? createFromUtc = null, DateTime? createToUtc = null,
        //    DateTime? publishFromUtc = null, DateTime? publishToUtc = null,
        //    int publishedId = 0, string commentText = null, int pageIndex = 0, int pageSize = int.MaxValue) //wailiang 20181219 MDT-152
        //{
        //    string type = MSP_Announce_Content_ContentType.Video.ToValue<MSP_Announce_Content_ContentType>();

        //    var query = _mspAnnouncementContentRepository.Table;
        //    query = query.Where(x => x.ContentType == type);

        //    string published = string.Empty;
        //    if (publishedId < 2)
        //        published = publishedId == 0 ? MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>()
        //            : publishedId == 1 ? MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>() : "";

        //    if (!string.IsNullOrEmpty(published))
        //        query = query.Where(x => x.Status == published);

        //    if (createFromUtc.HasValue)
        //    {
        //        var createFrom = _dateTimeHelper.ConvertToUtcTime(createFromUtc.Value);
        //        query = query.Where(x => x.CreatedOnUtc >= createFrom);
        //    }

        //    if (createToUtc.HasValue)
        //    {
        //        var createTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(createToUtc.Value));
        //        query = query.Where(x => x.CreatedOnUtc <= createTo);
        //    }

        //    if (publishFromUtc.HasValue)
        //    {
        //        var publishFrom = _dateTimeHelper.ConvertToUtcTime(publishFromUtc.Value);
        //        query = query.Where(x => x.PublishedOnUtc >= publishFrom);
        //    }

        //    if (publishToUtc.HasValue)
        //    {
        //        var publishTo = _dateTimeHelper.ConvertToUtcTime(_mspHelper.ToDate(publishToUtc.Value));
        //        query = query.Where(x => x.PublishedOnUtc <= publishTo);
        //    }

        //    if (!string.IsNullOrEmpty(commentText))
        //        query = query.Where(x => x.ContentTitle.Contains(commentText) || x.Content.Contains(commentText) || x.ContentName.Contains(commentText));

        //    query = query.OrderByDescending(x => x.CreatedOnUtc);
            
        //    var VideoGuide = new PagedList<MSP_Announce_Content>(query, pageIndex, pageSize);

        //    return VideoGuide;
        //}
        //#endregion

        #region Comment Code Tony Liew 20181217 
        ///// <summary>
        ///// Ensure no new insert shutdown announcement overlap with existing shutdown announcement 
        ///// </summary>
        ///// <param name="editShutdownDateStart"></param>
        ///// <param name="editShutdownDateEnd"></param>
        ///// <returns></returns>
        //public bool IsNotOverlapShutdownAnnouncement(DateTime? editShutdownDateStart, DateTime? editShutdownDateEnd)
        //{
        //    string type = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();
        //    //Tony Liew 20181130 MDT-114 \/
        //    // If result return 1 or more record, it means there are overlap shutdown announcement 
        //    var result = (from table in _mspAnnouncementContentRepository.Table
        //                 where 
        //                 (table.ShutDownStartOnUtc >= editShutdownDateStart
        //                 && table.ShutDownStartOnUtc <= editShutdownDateEnd) 
        //                 ||
        //                 (table.ShutDownEndOnUtc >= editShutdownDateStart
        //                 && table.ShutDownEndOnUtc <= editShutdownDateEnd)
        //                 && table.IsShutDown
        //                 && table.ContentType == type
        //                  select table).ToList();
        //    //Tony Liew 20181130 MDT-114 /\

        //    if (result.Count > 0) return false;
        //    else return true;

        //}
        #endregion
    }
}
