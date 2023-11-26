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
using Nop.Web.Areas.Admin.Models.UserGuides;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class UserGuidesController : BaseAdminController
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

        public UserGuidesController(IAnnouncementsService announcementsService,
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

        #region UserGuides

        //wailiang 20180910 MSP-101 \/
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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageUserGuides))
                return AccessDeniedView();

            var model = new UserGuidesListModel
            {
                //populate publish status in searching criteria
                AvailablePublishedOptions = _announcementsService.GetUserGuidesPublishedValue()
            };

            return View(model);
        }

        /// <summary>
        /// List all the userguides items
        /// </summary>
        /// <param name="command"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult List(DataSourceRequest command, UserGuidesListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageUserGuides))
                return AccessDeniedKendoGridJson();

            //wailiang 20180912 MSP-95 \/
            if (!model.CreatedOnFrom.HasValue || !model.CreatedOnTo.HasValue)
            {
                return Json(model);
            }
            //wailiang 20180912 MSP-95 /\

            var lists = _announcementsService.GetAllUserGuides(model.CreatedOnFrom, model.CreatedOnTo,
                model.PublishedDateFrom, model.PublishedDateTo, model.SearchPublishedId, model.SearchText);

            var gridModel = new DataSourceResult
            {
                Data = lists.PagedForCommand(command).Select(x =>
                {
                    var m = x.ToUserGuidesModel();
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
                    m.ContentTittleChinese = x.ContentTitle_CN; //WilliamHo 20181226 MDT-179
                    m.IsPublished = x.Status == MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
                    return m;
                }),
                Total = lists.Count
            };

            return Json(gridModel);
        }

        /// <summary>
        /// Redirect to create page
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageUserGuides))
                return AccessDeniedView();

            var model = new UserGuidesModel
            {
                IsPublished = false
            };

            return View(model);
        }

        /// <summary>
        /// POST create action and insert new userguides
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(UserGuidesModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageUserGuides))
                return AccessDeniedView();

            if (ModelState.IsValid) //Passed UserGuidesValidator checking
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now
                string userguides = MSP_Announce_Content_ContentType.UserGuide.ToValue<MSP_Announce_Content_ContentType>();
                string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
                string inactive = MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>();

                var data = model.ToUserGuidesEntity();
                data.ContentType = userguides;
                //data.PublishedOnUtc = model.StartDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.StartDate.Value) : model.StartDate; //Atiqah 20181031 MSP-353
                //data.ExpiredOnUtc = model.EndDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.EndDate.Value) : model.EndDate; //Atiqah 20181031 MSP-353
                data.PublishedOnUtc = model.StartDate;//Atiqah 20181031 MSP-353
                data.ExpiredOnUtc = model.EndDate; //Atiqah 20181031 MSP-353
                data.Status = model.IsPublished ? active : inactive;
                data.CreatedOnUtc = DateTime.UtcNow;
                data.CreatedBy = AdminID;
                data.UpdatedOnUtc = DateTime.UtcNow;
                data.UpdatedBy = AdminID;
                data.OnlyVisibleToDepositUser = model.OnlyVisibleToDepositUser; //Jerry 20181015 MSP-340
                data.ContentTitle_CN = model.ContentTittleChinese;//Tony Liew 20181228 MSP-510
                data.Content_CN = model.ContentChinese;//Tony Liew 20181228 MSP-510


                _announcementsService.InsertAnnouncementContent(data);

                //activity log
                _customerActivityService.InsertActivity("AddNewUserGuides", _localizationService.GetResource("ActivityLog.AddNewUserGuides"), data.Id);

                SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.UserGuides.UserGuidesItems.Added"));

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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageUserGuides))
                return AccessDeniedView();

            string userguides = MSP_Announce_Content_ContentType.UserGuide.ToValue<MSP_Announce_Content_ContentType>();
            string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();

            var data = _announcementsService.GetAnnouncementById(id, userguides);
            if (data == null)
                return RedirectToAction("List");

            var model = data.ToUserGuidesModel();
            //Atiqah 20181029 MSP-353 \/
            //model.CreatedOn = _dateTimeHelper.ConvertToUserTime(data.CreatedOnUtc, DateTimeKind.Utc);
            //if (data.PublishedOnUtc.HasValue)
            //    model.StartDate = _dateTimeHelper.ConvertToUserTime(data.PublishedOnUtc.Value, DateTimeKind.Utc);
            //if (data.ExpiredOnUtc.HasValue)
            //    model.EndDate = _dateTimeHelper.ConvertToUserTime(data.ExpiredOnUtc.Value, DateTimeKind.Utc);
            //Atiqah 20181029 MSP-353 /\
            model.CreatedOn = data.CreatedOnUtc; //Atiqah 20181029 MSP-353
            model.StartDate = data.PublishedOnUtc; //Atiqah 20181029 MSP-353
            model.EndDate =  data.ExpiredOnUtc; //Atiqah 20181029 MSP-353 //Tony Liew 20181130 MDT-114
            model.IsPublished = data.Status == active;
            model.ContentChinese = data.Content_CN;//Tony Liew 20181217 MDT-142
            model.ContentTittleChinese = data.ContentTitle_CN;//Tony Liew 20181217 MDT-142
            data.Content_CN = model.ContentChinese;//Tony Liew 20181217 MDT-142
            data.ContentTitle_CN = model.ContentTittleChinese;//Tony Liew 20181217 MDT-142

            return View(model);
        }

        /// <summary>
        /// POST edit action and update informations
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(UserGuidesModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageUserGuides))
                return AccessDeniedView();

            string UserGuides = MSP_Announce_Content_ContentType.UserGuide.ToValue<MSP_Announce_Content_ContentType>();
            string active = MSP_Announce_Content_Status.Active.ToValue<MSP_Announce_Content_Status>();
            string inactive = MSP_Announce_Content_Status.Inactive.ToValue<MSP_Announce_Content_Status>();

            var userguidesItem = _announcementsService.GetAnnouncementById(model.Id, UserGuides);
            if (userguidesItem == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now
                userguidesItem = model.ToUserGuidesEntity(userguidesItem);
                //userguidesItem.PublishedOnUtc = model.StartDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.StartDate.Value) : model.StartDate; //Atiqah 20181029 MSP-353
                //userguidesItem.ExpiredOnUtc = model.EndDate.HasValue ? _dateTimeHelper.ConvertToUtcTime(model.EndDate.Value) : model.EndDate; //Atiqah 20181029 MSP-353
                userguidesItem.PublishedOnUtc = model.StartDate; //Atiqah 20181029 MSP-353
                userguidesItem.ExpiredOnUtc = model.EndDate;  //Atiqah 20181029 MSP-353 //Tony Liew 20181130 MDT-114
                userguidesItem.Status = model.IsPublished ? active : inactive;
                userguidesItem.UpdatedOnUtc = DateTime.UtcNow;
                userguidesItem.UpdatedBy = AdminID;
                userguidesItem.OnlyVisibleToDepositUser = model.OnlyVisibleToDepositUser; //Jerry 20181015 MSP-340
                userguidesItem.Content_CN = model.ContentChinese;//Tony Liew 20181217 MDT-142
                userguidesItem.ContentTitle_CN = model.ContentTittleChinese;//Tony Liew 20181217 MDT-142

                _announcementsService.UpdateAnnouncementContent(userguidesItem);

                //activity log
                _customerActivityService.InsertActivity("EditUserGuides", _localizationService.GetResource("ActivityLog.EditUserGuides"), userguidesItem.Id);

                SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.UserGuides.UserGuidesItems.Updated"));

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = userguidesItem.Id });
                }
                return RedirectToAction("List");
            }

            return View(model);
        }

        /// <summary>
        /// Delete userguides by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageUserGuides))
                return AccessDeniedView();

            string userguides = MSP_Announce_Content_ContentType.UserGuide.ToValue<MSP_Announce_Content_ContentType>();
            var userguidesItem = _announcementsService.GetAnnouncementById(id, userguides);
            if (userguidesItem == null)
                throw new ArgumentException("No userguides found with the specified id");

            _announcementsService.DeleteAnnouncementContent(userguidesItem);

            //activity log
            _customerActivityService.InsertActivity("DeletUserGuides", _localizationService.GetResource("ActivityLog.DeleteUserGuides"), id);
            SuccessNotification(_localizationService.GetResource("Admin.ContentManagement.UserGuides.UserGuidesItems.Deleted"));

            //return new NullJsonResult();
            return RedirectToAction("List");
        }
        //wailiang 201809010 MSP-101 /\

        #endregion

    }
}