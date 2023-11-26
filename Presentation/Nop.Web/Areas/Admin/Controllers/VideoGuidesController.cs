using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Enumeration;
using Nop.Services.Announcements;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Extensions;
using Nop.Web.Areas.Admin.Models.VideoGuide;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class VideoGuidesController : BaseAdminController
    {
        #region Fields

        private readonly IAnnouncementsService _announcementsService;
        private readonly ILanguageService _languageService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IMspHelper _mspHelper;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Ctor

        public VideoGuidesController(IAnnouncementsService announcementsService,
            ILanguageService languageService,
            IDateTimeHelper dateTimeHelper,
            IMspHelper mspHelper,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IUrlRecordService urlRecordService,
            IStoreService storeService,
            IStoreMappingService storeMappingService,
            ICustomerActivityService customerActivityService,
            IWorkContext workContext)
        {
            this._announcementsService = announcementsService;
            this._languageService = languageService;
            this._dateTimeHelper = dateTimeHelper;
            this._mspHelper = mspHelper;
            this._eventPublisher = eventPublisher;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._urlRecordService = urlRecordService;
            this._storeService = storeService;
            this._storeMappingService = storeMappingService;
            this._customerActivityService = customerActivityService;
            this._workContext = workContext;
        }

        #endregion

        #region Video Guide

        //Atiqah 20180912 MSP-102 \/
        public virtual IActionResult Index()
        {
            return RedirectToAction("List");
        }

        /// <summary>
        /// List searching criteria info
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVideoGuides))
                return AccessDeniedView();

            var model = new VideoGuideListModel
            {
                //populate publish status in searching criteria
                AvailablePublishedOptions = _announcementsService.GetVideoGuidePublishedValue()
            };

            return View(model);
        }

        /// <summary>
        /// List all the video guide items
        /// </summary>
        /// <param name="command"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult List(DataSourceRequest command, VideoGuideListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVideoGuides))
                return AccessDeniedKendoGridJson();

            //wailiang 20180912 MSP-95 \/
            if (!model.CreatedOnFrom.HasValue || !model.CreatedOnTo.HasValue)
            {
                return Json(model);
            }
            //wailiang 20180912 MSP-95 /\

            //Atiqah 20180925 MSP-102\/
            //var lists = _announcementsService.GetAllVideoGuides(model.CreatedOnFrom, model.CreatedOnTo,
            //    model.PublishedDateFrom, model.PublishedDateTo, model.SearchPublishedId, model.SearchText);
            //Atiqah 20180925 MSP-102/\

            //Atiqah 20180925 MSP-102\/
            var lists = _announcementsService.GetAllVideoGuides(
                createFromUtc: model.CreatedOnFrom,
                createToUtc: model.CreatedOnTo,
                publishFromUtc: model.PublishedDateFrom,
                publishToUtc: model.PublishedDateTo,
                publishedId: model.SearchPublishedId,
                commentText: model.SearchText,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);
            //Atiqah 20180925 MSP-102/\

            var gridModel = new DataSourceResult
            {
                Data = lists.Select(x =>
                {
                    var m = x.ToVideoGuideModel();
                    //little performance optimization: ensure that "Content" is not returned
                    m.Content = "";
                    m.ContentUrl = "";
                    //Atiqah 20181031 MSP-353 \/
                    //if (x.PublishedOnUtc.HasValue)
                    //    m.StartDate = _dateTimeHelper.ConvertToUserTime(x.PublishedOnUtc.Value, DateTimeKind.Utc);
                    //if (x.ExpiredOnUtc.HasValue)
                    //    m.EndDate = _dateTimeHelper.ConvertToUserTime(x.ExpiredOnUtc.Value, DateTimeKind.Utc);
                    //m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
                    //Atiqah 20181031 MSP-353 /\
                    m.CreatedOn = x.CreatedOnUtc; //Atiqah 20181031 MSP-353
                    m.StartDate = x.PublishedOnUtc; //Atiqah 20181031 MSP-353
                    m.EndDate = x.ExpiredOnUtc; //Atiqah 20181031 MSP-353
                    m.Id = x.Id;
                    m.ContentTitle = x.ContentTitle;
                    //WilliamHo 20181226 MDT-180 \/
                    //m.ContentName = x.ContentName; // Comment by WilliamHo 20181226 MDT-180
                    m.ContentName = "";
                    m.ContentTittleChinese = x.ContentTitle_CN; 
                    //WilliamHo 20181226 MDT-180 /\
                    m.IsPublished = x.Status == MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
                    return m;
                }),
                Total = lists.TotalCount
            };

            return Json(gridModel);
        }

        /// <summary>
        /// Redirect to create page
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVideoGuides))
                return AccessDeniedView();

            var model = new VideoGuideModel
            {
                IsPublished = false
            };

            return View(model);
        }

        /// <summary>
        /// POST create action and insert new video guide
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(VideoGuideModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVideoGuides))
                return AccessDeniedView();

            if (ModelState.IsValid) //Passed VideoGuideValidator checking
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now
                string videoGuide = MSP_Announce_Content_ContentType.Video.ToValue<MSP_Announce_Content_ContentType>();
                string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
                string inactive = MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>();

                var data = model.ToVideoGuideEntity();
                data.ContentType = videoGuide;
                //data.PublishedOnUtc = model.StartDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.StartDate.Value) : model.StartDate; //Atiqah 20181031 MSP-353
                //data.ExpiredOnUtc = model.EndDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.EndDate.Value) : model.EndDate; //Atiqah 20181031 MSP-353
                data.PublishedOnUtc = model.StartDate; //Atiqah 20181031 MSP-353
                data.ExpiredOnUtc = model.EndDate; //Atiqah 20181031 MSP-353 //Tony Liew 20181130 MDT-114
                data.Status = model.IsPublished ? active : inactive;
                data.CreatedOnUtc = DateTime.UtcNow;
                data.CreatedBy = AdminID;
                data.UpdatedOnUtc = DateTime.UtcNow;
                data.UpdatedBy = AdminID;
                data.OnlyVisibleToDepositUser = model.OnlyVisibleToDepositUser; //Jerry 20181015 MSP-341
                data.ContentTitle_CN = model.ContentTittleChinese;//Tony Liew 20181217 MDT-143
                _announcementsService.InsertAnnouncementContent(data);

                //activity log
                _customerActivityService.InsertActivity("AddNewVideoGuide", _localizationService.GetResource("ActivityLog.AddNewVideoGuide"), data.Id);

                SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.VideoGuideItems.Added"));

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = data.Id });
                }
                return RedirectToAction("List");
            }

            return View(model);
        }

        /// <summary>
        /// Redirect to edit page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual IActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVideoGuides))
                return AccessDeniedView();

            string videoGuide = MSP_Announce_Content_ContentType.Video.ToValue<MSP_Announce_Content_ContentType>();
            string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();

            var data = _announcementsService.GetAnnouncementById(id, videoGuide);
            if (data == null)
                return RedirectToAction("List");

            var model = data.ToVideoGuideModel();
            //Atiqah 20181029 MSP-353 \/
            //model.CreatedOn = _dateTimeHelper.ConvertToUserTime(data.CreatedOnUtc, DateTimeKind.Utc);
            //if (data.PublishedOnUtc.HasValue)
            //    model.StartDate = _dateTimeHelper.ConvertToUserTime(data.PublishedOnUtc.Value, DateTimeKind.Utc);
            //if (data.ExpiredOnUtc.HasValue)
            //    model.EndDate = _dateTimeHelper.ConvertToUserTime(data.ExpiredOnUtc.Value, DateTimeKind.Utc);
            //Atiqah 20181029 MSP-353 /\
            model.CreatedOn = data.CreatedOnUtc; //Atiqah 20181029 MSP-353
            model.StartDate = data.PublishedOnUtc; //Atiqah 20181029 MSP-353
            model.EndDate = model.EndDate; //Atiqah 20181029 MSP-353 //Tony Liew 20181130 MDT-114
            model.IsPublished = data.Status == active;
            model.ContentTittleChinese = data.ContentTitle_CN; //Tony Liew 20181217 MDT-143

            return View(model);
        }

        /// <summary>
        /// POST edit action and update informations
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(VideoGuideModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVideoGuides))
                return AccessDeniedView();

            string videoGuide = MSP_Announce_Content_ContentType.Video.ToValue<MSP_Announce_Content_ContentType>();
            string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
            string inactive = MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>();

            var videoGuideItem = _announcementsService.GetAnnouncementById(model.Id, videoGuide);
            if (videoGuideItem == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now
                videoGuideItem = model.ToVideoGuideEntity(videoGuideItem);
                //videoGuideItem.PublishedOnUtc = model.StartDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.StartDate.Value) : model.StartDate; //Atiqah 20181029 MSP-353
                //videoGuideItem.ExpiredOnUtc = model.EndDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.EndDate.Value) : model.EndDate; //Atiqah 20181029 MSP-353
                videoGuideItem.PublishedOnUtc = model.StartDate; //wailiang 20181031 MSP-425
                videoGuideItem.ExpiredOnUtc = model.EndDate; //Atiqah 20181029 MSP-353 //Tony Liew 20181130 MDT-114
                videoGuideItem.Status = model.IsPublished ? active : inactive;
                videoGuideItem.UpdatedOnUtc = DateTime.UtcNow;
                videoGuideItem.UpdatedBy = AdminID;
                videoGuideItem.OnlyVisibleToDepositUser = model.OnlyVisibleToDepositUser; //Jerry 20181015 MSP-341
                videoGuideItem.ContentTitle_CN = model.ContentTittleChinese; //Tony Liew 20181217 MDT-143

                _announcementsService.UpdateAnnouncementContent(videoGuideItem);

                //activity log
                _customerActivityService.InsertActivity("EditVideoGuide", _localizationService.GetResource("ActivityLog.EditVideoGuide"), videoGuideItem.Id);

                SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.VideoGuideItems.Updated"));

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = videoGuideItem.Id });
                }
                return RedirectToAction("List");
            }

            return View(model);
        }

        /// <summary>
        /// Delete video guide by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageVideoGuides))
                return AccessDeniedView();

            string videoGuide = MSP_Announce_Content_ContentType.Video.ToValue<MSP_Announce_Content_ContentType>();
            var videoGuideItem = _announcementsService.GetAnnouncementById(id, videoGuide);
            if (videoGuideItem == null)
                throw new ArgumentException("No video guide found with the specified id");

            _announcementsService.DeleteAnnouncementContent(videoGuideItem);

            //activity log
            _customerActivityService.InsertActivity("DeleteVideoGuide", _localizationService.GetResource("ActivityLog.DeleteVideoGuide"), id);
            SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.VideoGuideItems.Deleted"));

            //return new NullJsonResult();
            return RedirectToAction("List");
        }
        //Atiqah 20180912 MSP-102 /\

        #endregion
    }
}
