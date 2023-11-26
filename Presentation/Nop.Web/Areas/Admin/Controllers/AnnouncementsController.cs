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
using Nop.Web.Areas.Admin.Models.Announcements;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class AnnouncementsController : BaseAdminController
    {
        #region Fields

        private readonly IAnnouncementsService _announcementsService;
        private readonly ILanguageService _languageService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IWorkContext _workContext;
        private readonly IMspHelper _mspHelper;

        #endregion

        #region Ctor

        public AnnouncementsController(IAnnouncementsService announcementsService,
            ILanguageService languageService,
            IDateTimeHelper dateTimeHelper,
            IEventPublisher eventPublisher,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            IUrlRecordService urlRecordService,
            IStoreService storeService,
            IStoreMappingService storeMappingService,
            ICustomerActivityService customerActivityService,
            IMspHelper mspHelper,
            IWorkContext workContext)
        {
            this._announcementsService = announcementsService;
            this._languageService = languageService;
            this._dateTimeHelper = dateTimeHelper;
            this._eventPublisher = eventPublisher;
            this._localizationService = localizationService;
            this._permissionService = permissionService;
            this._urlRecordService = urlRecordService;
            this._storeService = storeService;
            this._storeMappingService = storeMappingService;
            this._customerActivityService = customerActivityService;
            this._mspHelper = mspHelper;
            this._workContext = workContext;
        }

        #endregion

        #region Announcement

        //WilliamHo 20180907 MSP-99 \/
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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
                return AccessDeniedView();

            var model = new AnnouncementsListModel
            {
                //populate publish status in searching criteria
                AvailablePublishedOptions = _announcementsService.GetPublishedValues()
                //AvailablePublishedOptions = EnumExtendMethod.ToSelectListItems<MSP_Announce_Content_Status>(true) //testing
            };

            return View(model);
        }

        ///// <summary>
        ///// List all the announcement items
        ///// </summary>
        ///// <param name="command"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public virtual IActionResult List(DataSourceRequest command, AnnouncementsListModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
        //        return AccessDeniedKendoGridJson();

        //    var announcementLists = _announcementsService.GetAllAnnouncements(model.CreatedOnFrom, model.CreatedOnTo, 
        //        model.PublishedDateFrom, model.PublishedDateTo, model.SearchPublishedId, model.SearchText);

        //    var gridModel = new DataSourceResult
        //    {
        //        Data = announcementLists.PagedForCommand(command).Select(x =>
        //        {
        //            #region old codes
        //            //var _model = new AnnouncementsModel
        //            //{
        //            //    //little performance optimization: ensure that "Content" is not returned
        //            //    Content = "",
        //            //    StartDate = x.PublishedOnUtc.HasValue ? _dateTimeHelper.ConvertToUserTime(x.PublishedOnUtc.Value, DateTimeKind.Utc) : x.PublishedOnUtc,
        //            //    EndDate = x.ExpiredOnUtc.HasValue ? _dateTimeHelper.ConvertToUserTime(x.ExpiredOnUtc.Value, DateTimeKind.Utc) : x.ExpiredOnUtc,
        //            //    CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc),
        //            //    Id = x.Id,
        //            //    ContentTitle = x.ContentTitle,
        //            //    IsPublished = (x.Status == MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>())
        //            //};
        //            //return _model;
        //            #endregion
        //            var m = x.ToAnnouncementModel();
        //            //little performance optimization: ensure that "Content" is not returned
        //            m.Content = "";
        //            if (x.PublishedOnUtc.HasValue)
        //                m.StartDate = _dateTimeHelper.ConvertToUserTime(x.PublishedOnUtc.Value, DateTimeKind.Utc);
        //            if (x.ExpiredOnUtc.HasValue)
        //                m.EndDate = _dateTimeHelper.ConvertToUserTime(x.ExpiredOnUtc.Value, DateTimeKind.Utc);
        //            m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
        //            m.Id = x.Id;
        //            m.ContentTitle = x.ContentTitle;
        //            m.IsPublished = x.Status == MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
        //            return m;
        //        }),
        //        Total = announcementLists.Count
        //    };

        //    return Json(gridModel);
        //}

        #region Pagination White Mouse - Erictan
        /// <summary>
        /// List all the announcement items
        /// </summary>
        /// <param name="command"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult List(DataSourceRequest command, AnnouncementsListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
                return AccessDeniedKendoGridJson();

            //wailiang 20180912 MSP-95 \/
            if (!model.CreatedOnFrom.HasValue || !model.CreatedOnTo.HasValue)
            {
                return Json(model);
            }
            //wailiang 20180912 MSP-95 /\

            var announcementLists = _announcementsService.GetAllAnnouncements(
                createFromUtc: model.CreatedOnFrom,
                createToUtc: model.CreatedOnTo,
                publishFromUtc: model.PublishedDateFrom,
                publishToUtc: model.PublishedDateTo,
                published: model.SearchPublishedId,
                commentText: model.SearchText,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = announcementLists.Select(x =>
                {
                    var m = x.ToAnnouncementModel();
                    //little performance optimization: ensure that "Content" is not returned
                    m.Content = "";
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
                    m.ContentTittleChinese = x.ContentTitle_CN; //WilliamHo 20181226 MDT-177
                    m.IsPublished = x.Status == MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
                    return m;
                }),
                Total = announcementLists.TotalCount
            };

            return Json(gridModel);
        }

        ///// <summary>
        ///// List all the announcement items
        ///// </summary>
        ///// <param name="command"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public virtual IActionResult List(DataSourceRequest command, AnnouncementsListModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
        //        return AccessDeniedKendoGridJson();

        //    var announcementLists = _announcementsService.GetAllAnnouncements(model.CreatedOnFrom, model.CreatedOnTo,
        //        model.PublishedDateFrom, model.PublishedDateTo, model.SearchPublishedId, model.SearchText);

        //    var gridModel = new DataSourceResult
        //    {
        //        Data = announcementLists.PagedForCommand(command).Select(x =>
        //        {
        //            var m = x.ToAnnouncementModel();
        //            //little performance optimization: ensure that "Content" is not returned
        //            m.Content = "";
        //            if (x.PublishedOnUtc.HasValue)
        //                m.StartDate = _dateTimeHelper.ConvertToUserTime(x.PublishedOnUtc.Value, DateTimeKind.Utc);
        //            if (x.ExpiredOnUtc.HasValue)
        //                m.EndDate = _dateTimeHelper.ConvertToUserTime(x.ExpiredOnUtc.Value, DateTimeKind.Utc);
        //            m.CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc);
        //            m.Id = x.Id;
        //            m.ContentTitle = x.ContentTitle;
        //            m.IsPublished = x.Status == MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
        //            return m;
        //        }),
        //        Total = announcementLists.Count
        //    };

        //    return Json(gridModel);
        //}
        #endregion

        /// <summary>
        /// Redirect to create page
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
                return AccessDeniedView();

            var model = new AnnouncementsModel
            {
                IsPublished = false,
            };
            return View(model);
        }

        /// <summary>
        /// POST create action and insert new announcement
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(AnnouncementsModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
                return AccessDeniedView();

            if (ModelState.IsValid) //Passed AnnouncementsValidator checking
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now
                string announcement = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();
                string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
                string inactive = MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>();

                #region comment code shutdown function :RW 20181210 MDT-131
                //var announcementItem = model.ToAnnouncementEntity();
                //announcementItem.ContentType = announcement;
                ////announcementItem.PublishedOnUtc = model.StartDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.StartDate.Value) : model.StartDate; //Atiqah 20181031 MSP-353
                ////announcementItem.ExpiredOnUtc = model.EndDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.EndDate.Value) : model.EndDate; //Atiqah 20181031 MSP-353

                //announcementItem.Status = model.IsPublished ? active : inactive;
                //announcementItem.CreatedOnUtc = DateTime.UtcNow;
                //announcementItem.CreatedBy = AdminID;
                //announcementItem.UpdatedOnUtc = DateTime.UtcNow;
                //announcementItem.UpdatedBy = AdminID;
                //announcementItem.OnlyVisibleToDepositUser = model.OnlyVisibleToDepositUser; //Jerry 20181015 MSP-338
                //announcementItem.IsShutDown = model.IsShutDownDate;//Tony Liew 20181130 MDT-114
                //announcementItem.PublishedOnUtc = model.StartDate; //Atiqah 20181031 MSP-353

                ////Tony Liew 20181130 MDT-114\/
                //if (!announcementItem.IsShutDown)
                //{
                //    announcementItem.ShutDownStartOnUtc =  null;//Tony Liew 20181130 MDT-114
                //    announcementItem.ShutDownEndOnUtc =  null;//Tony Liew 20181130 MDT-114
                //    announcementItem.ExpiredOnUtc = model.EndDate; //Atiqah 20181031 MSP-353 //Tony Liew 20181130 MDT-114
                //    _announcementsService.InsertAnnouncementContent(announcementItem);
                //}
                //else
                //{
                //    announcementItem.ShutDownStartOnUtc = model.ShutDownStartDate ;//Tony Liew 20181130 MDT-114
                //    announcementItem.ShutDownEndOnUtc = model.ShutDownEndDate ;//Tony Liew 20181130 MDT-114
                //    announcementItem.ExpiredOnUtc =  model.ShutDownEndDate; //Atiqah 20181031 MSP-353 //Tony Liew 20181130 MDT-114

                //    if (_announcementsService.IsNotOverlapShutdownAnnouncement(model.ShutDownStartDate, model.ShutDownEndDate, model.Id))
                //    {
                //        _announcementsService.InsertAnnouncementContent(announcementItem);
                //        SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.Added"));
                //        _customerActivityService.InsertActivity("AddNewAnnouncement", _localizationService.GetResource("ActivityLog.AddNewAnnouncement"), announcementItem.Id);
                //    }
                //    else
                //    {
                //        ErrorNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.IsShutDown.Insert.NotSuccess"));
                //        _customerActivityService.InsertActivity("AddNewAnnouncement", _localizationService.GetResource("ActivityLog.AddNewAnnouncement"), announcementItem.Id);
                //    }

                //}
                #endregion

                //Tony Liew 20181130 MDT-114 /\

                //RW 20181210 MDT-131
                var announcementItem = model.ToAnnouncementEntity();
                announcementItem.ContentType = announcement;
                //announcementItem.PublishedOnUtc = model.StartDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.StartDate.Value) : model.StartDate; //Atiqah 20181031 MSP-353
                //announcementItem.ExpiredOnUtc = model.EndDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.EndDate.Value) : model.EndDate; //Atiqah 20181031 MSP-353
                announcementItem.PublishedOnUtc = model.StartDate; //Atiqah 20181031 MSP-353
                announcementItem.ExpiredOnUtc = model.EndDate; //Atiqah 20181031 MSP-353
                announcementItem.Status = model.IsPublished ? active : inactive;
                announcementItem.CreatedOnUtc = DateTime.UtcNow;
                announcementItem.CreatedBy = AdminID;
                announcementItem.UpdatedOnUtc = DateTime.UtcNow;
                announcementItem.UpdatedBy = AdminID;
                announcementItem.OnlyVisibleToDepositUser = model.OnlyVisibleToDepositUser; //Jerry 20181015 MSP-338
                announcementItem.ContentTitle_CN = model.ContentTittleChinese;//Tony Liew 20181228 MSP-510
                announcementItem.Content_CN = model.ContentChinese; //Tony Liew 20181228 MSP-510
                /* //WilliamHo 20181227 MDT-185 \/
                announcementItem.GRCLanding = model.IsGRCLanding; // Tony Liew 20181218 MDT-161
                announcementItem.IsGRCPopUp = model.IsGRCPopUp;// Tony Liew 20181218 MDT-161
                announcementItem.MSInformation = model.IsMSInformation;// Tony Liew 20181218 MDT-161
                announcementItem.IsMSPopUp = model.IsMSPopUp;// Tony Liew 20181218 MDT-161
                announcementItem.MSLanding = model.IsMSLanding;// Tony Liew 20181218 MDT-161
                //WilliamHo 20181227 MDT-185 /\ */

                _announcementsService.InsertAnnouncementContent(announcementItem);
                //RW 20181210 MDT-131

                //activity log
                _customerActivityService.InsertActivity("AddNewAnnouncement", _localizationService.GetResource("ActivityLog.AddNewAnnouncement"), announcementItem.Id);
                SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.Added"));

                //Tony Liew 20181130 MDT-114 /\
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = announcementItem.Id });
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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
                return AccessDeniedView();

            string announcement = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();
            string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();

            var data = _announcementsService.GetAnnouncementById(id, announcement);
            if (data == null)
                return RedirectToAction("List");

            #region comment code :RW 20181210 MDT-131
            //var model = data.ToAnnouncementModel();
            ///*WilliamHo 20181026 MSP-353
            ////model.CreatedOn = _dateTimeHelper.ConvertToUserTime(data.CreatedOnUtc, DateTimeKind.Utc);
            ////if (data.PublishedOnUtc.HasValue)
            ////    model.StartDate = _dateTimeHelper.ConvertToUserTime(data.PublishedOnUtc.Value, DateTimeKind.Utc);
            ////if (data.ExpiredOnUtc.HasValue)
            ////    model.EndDate = _dateTimeHelper.ConvertToUserTime(data.ExpiredOnUtc.Value, DateTimeKind.Utc);
            //*/
            //model.CreatedOn = data.CreatedOnUtc; //WilliamHo 20181026 MSP-353
            //model.StartDate = data.PublishedOnUtc; //WilliamHo 20181026 MSP-353
            //model.EndDate = model.IsShutDownDate? data.ShutDownEndOnUtc:data.ExpiredOnUtc; //WilliamHo 20181026 MSP-353 //Tony Liew 20181130 MDT-114
            //model.IsPublished = data.Status == active;
            //model.IsShutDownDate = data.IsShutDown;//Tony Liew 20181130 MDT-114
            //model.ShutDownEndDate = data.ShutDownEndOnUtc;//Tony Liew 20181130 MDT-114
            //model.ShutDownStartDate = data.ShutDownStartOnUtc;//Tony Liew 20181130 MDT-114
            //                                                  //var model = new AnnouncementsModel
            //                                                  //{
            //                                                  //    ContentTitle = announcementItem.ContentTitle,
            //                                                  //    Content = announcementItem.Content,
            //                                                  //    CreatedOn = announcementItem.CreatedOnUtc,
            //                                                  //    StartDate = announcementItem.PublishedOnUtc,
            //                                                  //    EndDate = announcementItem.ExpiredOnUtc,
            //                                                  //    IsPublished = (announcementItem.Status == active)
            //                                                  //};
            #endregion

            //RW 20181210 MDT-131
            var model = data.ToAnnouncementModel();
            /*WilliamHo 20181026 MSP-353
            //model.CreatedOn = _dateTimeHelper.ConvertToUserTime(data.CreatedOnUtc, DateTimeKind.Utc);
            //if (data.PublishedOnUtc.HasValue)
            //    model.StartDate = _dateTimeHelper.ConvertToUserTime(data.PublishedOnUtc.Value, DateTimeKind.Utc);
            //if (data.ExpiredOnUtc.HasValue)
            //    model.EndDate = _dateTimeHelper.ConvertToUserTime(data.ExpiredOnUtc.Value, DateTimeKind.Utc);
            */
            model.CreatedOn = data.CreatedOnUtc; //WilliamHo 20181026 MSP-353
            model.StartDate = data.PublishedOnUtc; //WilliamHo 20181026 MSP-353
            model.EndDate = data.ExpiredOnUtc; //WilliamHo 20181026 MSP-353
            model.IsPublished = data.Status == active;
            model.ContentChinese = data.Content_CN;//Tony Liew 20181217 MDT-140
            model.ContentTittleChinese = data.ContentTitle_CN;//Tony Liew 20181217 MDT-140
            /* //WilliamHo 20181227 MDT-185 \/
            model.IsGRCLanding = data.GRCLanding; // Tony Liew 20181218 MDT-161
            model.IsGRCPopUp = data.IsGRCPopUp;// Tony Liew 20181218 MDT-161
            model.IsMSInformation = data.MSInformation;// Tony Liew 20181218 MDT-161
            model.IsMSPopUp = data.IsMSPopUp;// Tony Liew 20181218 MDT-161
            model.IsMSLanding = data.MSLanding;// Tony Liew 20181218 MDT-161
            //WilliamHo 20181227 MDT-185 /\ */

            //var model = new AnnouncementsModel
            //{
            //    ContentTitle = announcementItem.ContentTitle,
            //    Content = announcementItem.Content,
            //    CreatedOn = announcementItem.CreatedOnUtc,
            //    StartDate = announcementItem.PublishedOnUtc,
            //    EndDate = announcementItem.ExpiredOnUtc,
            //    IsPublished = (announcementItem.Status == active)
            //};
            //RW 20181210 MDT-131

            return View(model);
        }

        /// <summary>
        /// POST edit action and update informations
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(AnnouncementsModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
                return AccessDeniedView();

            string announcement = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();
            string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
            string inactive = MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>();

            var announcementItem = _announcementsService.GetAnnouncementById(model.Id, announcement);
            if (announcementItem == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now

                #region comment code :RW 20181210 MDT-131
                //announcementItem = model.ToAnnouncementEntity(announcementItem);
                ////announcementItem.ContentTitle = model.ContentTitle;
                ////announcementItem.Content = model.Content;
                ///*WilliamHo 20181026 MSP-353
                ////announcementItem.PublishedOnUtc = model.StartDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.StartDate.Value) : model.StartDate;
                ////announcementItem.ExpiredOnUtc = model.EndDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.EndDate.Value) : model.EndDate;
                //*/

                //announcementItem.Status = model.IsPublished ? active : inactive;
                //announcementItem.UpdatedOnUtc = DateTime.UtcNow;
                //announcementItem.UpdatedBy = AdminID;
                //announcementItem.OnlyVisibleToDepositUser = model.OnlyVisibleToDepositUser; //Jerry 20181015 MSP-338
                //announcementItem.IsShutDown = model.IsShutDownDate;//Tony Liew 20181130 MDT-114
                //announcementItem.PublishedOnUtc = model.StartDate; //WilliamHo 20181026 MSP-353

                ////Tony Liew 20181130 MDT-114\/
                //if (!announcementItem.IsShutDown)
                //{
                //    announcementItem.ShutDownStartOnUtc = null ;//Tony Liew 20181130 MDT-114
                //    announcementItem.ShutDownEndOnUtc = null;//Tony Liew 20181130 MDT-114
                //    announcementItem.ExpiredOnUtc = model.EndDate; //WilliamHo 20181026 MSP-353 //Tony Liew 20181130 MDT-114
                //    _announcementsService.UpdateAnnouncementContent(announcementItem);
                //    SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.Updated"));
                //}
                //else
                //{
                //    //Tony Liew 20181130 MDT-114 \/

                //    announcementItem.ShutDownStartOnUtc = model.ShutDownStartDate;//Tony Liew 20181130 MDT-114
                //    announcementItem.ShutDownEndOnUtc = model.ShutDownEndDate;//Tony Liew 20181130 MDT-114
                //    announcementItem.ExpiredOnUtc = model.ShutDownEndDate ; //WilliamHo 20181026 MSP-353 //Tony Liew 20181130 MDT-114
                //    if (_announcementsService.IsNotOverlapShutdownAnnouncement(model.ShutDownStartDate, model.ShutDownEndDate, model.Id))
                //    {
                //        _announcementsService.UpdateAnnouncementContent(announcementItem);
                //        SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.Updated"));
                //        _customerActivityService.InsertActivity("EditAnnouncement", _localizationService.GetResource("ActivityLog.EditAnnouncement"), announcementItem.Id);
                //    }
                //    else
                //    {
                //        ErrorNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.IsShutDown.Update.NotSuccess"));
                //        _customerActivityService.InsertActivity("AddNewAnnouncement", _localizationService.GetResource("ActivityLog.AddNewAnnouncement"), announcementItem.Id);
                //    }  
                //}
                #endregion

                //RW 20181210 MDT-131
                announcementItem = model.ToAnnouncementEntity(announcementItem);
                //announcementItem.ContentTitle = model.ContentTitle;
                //announcementItem.Content = model.Content;
                /*WilliamHo 20181026 MSP-353
                //announcementItem.PublishedOnUtc = model.StartDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.StartDate.Value) : model.StartDate;
                //announcementItem.ExpiredOnUtc = model.EndDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.EndDate.Value) : model.EndDate;
                */
                announcementItem.PublishedOnUtc = model.StartDate; //WilliamHo 20181026 MSP-353
                announcementItem.ExpiredOnUtc = model.EndDate; //WilliamHo 20181026 MSP-353
                announcementItem.Status = model.IsPublished ? active : inactive;
                announcementItem.UpdatedOnUtc = DateTime.UtcNow;
                announcementItem.UpdatedBy = AdminID;
                announcementItem.OnlyVisibleToDepositUser = model.OnlyVisibleToDepositUser; //Jerry 20181015 MSP-338
                announcementItem.Content_CN = model.ContentChinese;//Tony Liew 20181217 MDT-140
                announcementItem.ContentTitle_CN = model.ContentTittleChinese;//Tony Liew 20181217 MDT-140
                /* //WilliamHo 20181227 MDT-185 \/
                announcementItem.GRCLanding = model.IsGRCLanding; // Tony Liew 20181218 MDT-161
                announcementItem.IsGRCPopUp = !model.IsGRCLanding? model.IsGRCLanding: model.IsGRCPopUp;// Tony Liew 20181218 MDT-161
                announcementItem.MSInformation = model.IsMSInformation;// Tony Liew 20181218 MDT-161
                announcementItem.IsMSPopUp = !model.IsMSLanding ? model.IsMSLanding : model.IsMSPopUp;// Tony Liew 20181218 MDT-161
                announcementItem.MSLanding = model.IsMSLanding;// Tony Liew 20181218 MDT-161
                //WilliamHo 20181227 MDT-185 /\ */

                _announcementsService.UpdateAnnouncementContent(announcementItem);

                //activity log
                _customerActivityService.InsertActivity("EditAnnouncement", _localizationService.GetResource("ActivityLog.EditAnnouncement"), announcementItem.Id);

                SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.Updated"));
                //RW 20181210 MDT-131

                //Tony Liew 20181130 MDT-114 /\
                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = announcementItem.Id });
                }
                return RedirectToAction("List");
            }

            return View(model);
        }

        /// <summary>
        /// Delete announcement by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAnnouncementsItems))
                return AccessDeniedView();

            string announcement = MSP_Announce_Content_ContentType.Announcement.ToValue<MSP_Announce_Content_ContentType>();
            var announcementItem = _announcementsService.GetAnnouncementById(id, announcement);
            if (announcementItem == null)
                throw new ArgumentException("No announcement found with the specified id");

            _announcementsService.DeleteAnnouncementContent(announcementItem);

            //activity log
            _customerActivityService.InsertActivity("DeleteAnnouncement", _localizationService.GetResource("ActivityLog.DeleteAnnouncement"), id);
            SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.Announcements.AnnouncementsItems.Deleted"));
            //return new NullJsonResult();
            return RedirectToAction("List");
        }

        //WilliamHo 20180907 MSP-99 /\

        #endregion

    }
}
 