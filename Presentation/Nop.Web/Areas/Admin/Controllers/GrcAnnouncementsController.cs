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
    public partial class GrcAnnouncementsController : BaseAdminController
    {
        #region Fields

        private readonly IGrcAnnouncementsService _announcementsService;
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

        public GrcAnnouncementsController(IGrcAnnouncementsService announcementsService,
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

		#region GrcAnnouncement

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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageGrcAnnouncements))
                return AccessDeniedView();

            var model = new GrcAnnouncementsListModel
            {
                //populate publish status in searching criteria
                AvailablePublishedOptions = _announcementsService.GetPublishedValues()
                //AvailablePublishedOptions = EnumExtendMethod.ToSelectListItems<MSP_Announce_Content_Status>(true) //testing
            };

            return View(model);
        }

        #region Pagination White Mouse - Erictan
        /// <summary>
        /// List all the announcement items
        /// </summary>
        /// <param name="command"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult List(DataSourceRequest command, GrcAnnouncementsListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageGrcAnnouncements))
                return AccessDeniedKendoGridJson();

            if (!model.CreatedOnFrom.HasValue || !model.CreatedOnTo.HasValue)
            {
                return Json(model);
            }

			bool? isactive = null;
			if (model.SearchActive == 0) isactive = true;
			else if (model.SearchActive == 1) isactive = false;

			var announcementLists = _announcementsService.GetAllAnnouncements(
                createFromUtc: model.CreatedOnFrom,
                createToUtc: model.CreatedOnTo,
                publishFromUtc: model.PublishedDateFrom,
                publishToUtc: model.PublishedDateTo,
                active: isactive,
                commentText: model.SearchText,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = announcementLists.Select(x =>
                {
                    var m = x.ToGrcAnnouncementModel();
                    //little performance optimization: ensure that "Content" is not returned
                    m.Content1_EN = "";
                    m.CreatedOn = x.CreatedOnUtc; //Atiqah 20181031 MSP-353
                    m.StartDate = x.PublishedOnUtc; //Atiqah 20181031 MSP-353
                    m.EndDate = x.ExpiredOnUtc; //Atiqah 20181031 MSP-353
                    m.Id = x.Id;
                    m.Title_EN = x.Title_EN;
                    m.Title_CN = x.Title_CN; //WilliamHo 20181226 MDT-177
					m.IsActive = x.IsActive;
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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageGrcAnnouncements))
                return AccessDeniedView();

            var model = new GrcAnnouncementsModel
            {
                IsActive = false,
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
        public virtual IActionResult Create(GrcAnnouncementsModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageGrcAnnouncements))
                return AccessDeniedView();

            if (ModelState.IsValid) //Passed AnnouncementsValidator checking
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now

                var announcementItem = model.ToGrcAnnouncementEntity();
                announcementItem.PublishedOnUtc = model.StartDate; //Atiqah 20181031 MSP-353
                announcementItem.ExpiredOnUtc = model.EndDate; //Atiqah 20181031 MSP-353
                announcementItem.CreatedOnUtc = DateTime.UtcNow;
                announcementItem.CreatedBy = AdminID;
                announcementItem.UpdatedOnUtc = DateTime.UtcNow;
                announcementItem.UpdatedBy = AdminID;

                _announcementsService.InsertAnnouncementContent(announcementItem);

                //activity log
                _customerActivityService.InsertActivity("AddNewAnnouncement", _localizationService.GetResource("ActivityLog.AddNewGrcAnnouncement"), announcementItem.Id);
                SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.GrcAnnouncements.Added"));

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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageGrcAnnouncements))
                return AccessDeniedView();

            var data = _announcementsService.GetAnnouncementById(id);
            if (data == null)
                return RedirectToAction("List");

            //RW 20181210 MDT-131
            var model = data.ToGrcAnnouncementModel();
            model.CreatedOn = data.CreatedOnUtc; //WilliamHo 20181026 MSP-353
            model.StartDate = data.PublishedOnUtc; //WilliamHo 20181026 MSP-353
            model.EndDate = data.ExpiredOnUtc; //WilliamHo 20181026 MSP-353
			model.IsActive = data.IsActive;

            return View(model);
        }

        /// <summary>
        /// POST edit action and update informations
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(GrcAnnouncementsModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageGrcAnnouncements))
                return AccessDeniedView();

            var announcementItem = _announcementsService.GetAnnouncementById(model.Id);
            if (announcementItem == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now

                announcementItem = model.ToGrcAnnouncementEntity(announcementItem);
                announcementItem.PublishedOnUtc = model.StartDate; //WilliamHo 20181026 MSP-353
                announcementItem.ExpiredOnUtc = model.EndDate; //WilliamHo 20181026 MSP-353
                announcementItem.UpdatedOnUtc = DateTime.UtcNow;
                announcementItem.UpdatedBy = AdminID;

                _announcementsService.UpdateAnnouncementContent(announcementItem);

                //activity log
                _customerActivityService.InsertActivity("EditAnnouncement", _localizationService.GetResource("ActivityLog.EditGrcAnnouncement"), announcementItem.Id);

                SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.GrcAnnouncements.Updated"));

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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageGrcAnnouncements))
                return AccessDeniedView();

            var announcementItem = _announcementsService.GetAnnouncementById(id);
            if (announcementItem == null)
                throw new ArgumentException("No announcement found with the specified id");

            _announcementsService.DeleteAnnouncementContent(announcementItem);

            //activity log
            _customerActivityService.InsertActivity("DeleteAnnouncement", _localizationService.GetResource("ActivityLog.DeleteGrcAnnouncement"), id);
            SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.GrcAnnouncements.Deleted"));
            //return new NullJsonResult();
            return RedirectToAction("List");
        }


        #endregion

    }
}
 