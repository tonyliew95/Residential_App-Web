using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core;
using Nop.Core.Enumeration;
using Nop.Services.Announcements;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Residential.Incident;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Extensions;
using Nop.Web.Areas.Admin.Models.Incidents;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class IncidentsController : BaseAdminController //wailiang 20190320 RDT-127
    {
        #region Fields

        private readonly IIncidentServices _incidentServices;
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

        public IncidentsController(IIncidentServices incidentServices,
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
            this._incidentServices = incidentServices;
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

        #region Incidents //wailiang 20190319 RDT-127
        
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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageIncidents))
                return AccessDeniedView();

            var model = new IncidentsListModel
            {
                StatusValue = _incidentServices.GetIncidentStatusValue(),
                //Categories = _incidentServices.GetIncidentCategoryValue(),
                //Types = _incidentServices.GetIncidentTypeValue() //wailiang 20190320 RDT-129
            };

            return View(model);
        }

        /// <summary>
        /// List all the incidents items
        /// </summary>
        /// <param name="command"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult List(DataSourceRequest command, IncidentsListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageIncidents))
                return AccessDeniedKendoGridJson();

            if (!model.Inc_DateFrom.HasValue || !model.Inc_DateTo.HasValue)
            {
                return Json(model);
            }

            var lists = _incidentServices.GetAllIncident(
                _title: model.Title,
                inc_datefrom: model.Inc_DateFrom,
                inc_dateto: model.Inc_DateTo,
                //inc_category: model.Category,
                //inc_type: model.Type, //wailiang 20190320 RDT-129
                _status: model.Status,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            var gridModel = new DataSourceResult
            {
                Data = lists.Select(x =>
                {
                    var m = x.ToIncidentModel();

                    m.Inc_DateTime = x.Inc_DateTime;
                    //m.Category = x.Inc_Category;
                    //m.Type = x.Inc_Type; //wailiang 20190320 RDT-129
                    m.Status = x.Status;
                    m.Title = x.Title;
                    m.Id = x.Id;

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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageIncidents))
                return AccessDeniedView();

            var model = new IncidentsModel
            {
                StatusValue = _incidentServices.CreateIncidentStatusValue(),
                //CategoryValue = _incidentServices.CreateIncidentCategoryValue(),
                //TypeValue = _incidentServices.CreateIncidentTypeValue()
            };

            return View(model);
        }

        /// <summary>
        /// POST create action and insert new incidents
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Create(IncidentsModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageIncidents))
                return AccessDeniedView();

            if (ModelState.IsValid) //Passed incidentssValidator checking
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now

                var data = model.ToIncidentEntity();

                data.Title = model.Title;
                data.Desc = model.Desc;
                data.Inc_DateTime = model.Inc_DateTime;
                data.Status = model.Status;
                //data.Inc_CategoryId = model.Category;
                //data.Inc_TypeId = model.Type;
                data.CreatedOnUtc = DateTime.UtcNow;
                data.CreatedBy = AdminID;
                data.UpdatedOnUtc = DateTime.UtcNow;
                data.UpdatedBy = AdminID;
                data.ReportedBy = AdminID;

                _incidentServices.InsertIncidentReport(data);

                //activity log
                _customerActivityService.InsertActivity("AddNewIncidents", _localizationService.GetResource("ActivityLog.AddNewIncidents"), data.Id);

                SuccessNotification(_localizationService.GetResource("Incidents.IncidentsList.IncidentsItems.Added"));

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
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageIncidents))
                return AccessDeniedView();
            
            var data = _incidentServices.GetIncidentById(id);
            if (data == null)
                return RedirectToAction("List");

            var model = data.ToIncidentModel();

            model.StatusValue = _incidentServices.CreateIncidentStatusValue();
            model.StatusValue.Select(x => new SelectListItem { Selected = x.Value == data.Status, Text = x.Text, Value = x.Value });
            //model.CategoryValue = _incidentServices.CreateIncidentCategoryValue();
            //model.CategoryValue.Select(x => new SelectListItem { Selected = x.Value == data.Inc_CategoryId.ToString(), Text = x.Text, Value = x.Value });
            //model.TypeValue = _incidentServices.CreateIncidentTypeValue();
            //model.TypeValue.Select(x => new SelectListItem { Selected = x.Value == data.Inc_TypeId.ToString(), Text = x.Text, Value = x.Value });

            return View(model);
        }

        /// <summary>
        /// POST edit action and update informations
        /// </summary>
        /// <param name="model"></param>
        /// <param name="continueEditing"></param>
        /// <returns></returns>
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual IActionResult Edit(IncidentsModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageIncidents))
                return AccessDeniedView();
            
            var incidentsItem = _incidentServices.GetIncidentById(model.Id);
            if (incidentsItem == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                var AdminID = _workContext.CurrentCustomer.Id; //get the current user working now
                incidentsItem = model.ToIncidentEntity(incidentsItem);

                incidentsItem.UpdatedOnUtc = DateTime.UtcNow;
                incidentsItem.UpdatedBy = AdminID;
                incidentsItem.ReportedBy = AdminID;
                incidentsItem.Title = model.Title;
                incidentsItem.Desc = model.Desc;
                incidentsItem.Status = model.Status;

                _incidentServices.UpdateIncident(incidentsItem);

                //activity log
                _customerActivityService.InsertActivity("EditIncidents", _localizationService.GetResource("ActivityLog.EditIncidents"), incidentsItem.Id);

                SuccessNotification(_localizationService.GetResource("Incidents.IncidentsList.IncidentsItems.Updated"));

                if (continueEditing)
                {
                    //selected tab
                    SaveSelectedTabName();

                    return RedirectToAction("Edit", new { id = incidentsItem.Id });
                }
                return RedirectToAction("List");
            }

            return View(model);
        }

        /// <summary>
        /// Delete incidents by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual IActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageIncidents))
                return AccessDeniedView();
            
            var incidentsItem = _incidentServices.GetIncidentById(id);
            if (incidentsItem == null)
                throw new ArgumentException("No incidents found with the specified id");

            _incidentServices.DeleteIncident(incidentsItem);

            //activity log
            _customerActivityService.InsertActivity("DeleteIncidents", _localizationService.GetResource("ActivityLog.DeleteIncidents"), id);
            SuccessNotification(_localizationService.GetResource("Incidents.IncidentsList.IncidentsItems.Deleted"));
            
            return RedirectToAction("List");
        }

        #endregion

    }
}