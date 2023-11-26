using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using Nop.Core;
using Nop.Core.Domain.Customers;
//using Nop.Core.Domain.Msp.Security; //Atiqah 20190131 MDT-205
using Nop.Core.Enumeration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Extensions;
using Nop.Web.Areas.Admin.Models.Customers;
using Nop.Web.Areas.Admin.Models.Security;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
//using static Nop.Web.Areas.Admin.Models.Security.SecurityQuestionViewModel;

namespace Nop.Web.Areas.Admin.Controllers
{
    public partial class SecurityController : BaseAdminController
	{
		#region Fields

        private readonly ILogger _logger;
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        //private readonly ISecurityQuestionManagementService _securityQuestionManagementService; //Atiqah 20190131 MDT-205
        private readonly ICustomerActivityService _customerActivityService;


        #endregion

        #region Ctor

        public SecurityController
            (ILogger logger
            , IWorkContext workContext
            ,IPermissionService permissionService
            ,ICustomerService customerService 
            ,ILocalizationService localizationService
            //,ISecurityQuestionManagementService securityQuestionManagementService //Atiqah 20190131 MDT-205
            , ICustomerActivityService customerActivityService

            )
        {
            this._logger = logger;
            this._workContext = workContext;
            this._permissionService = permissionService;
            this._customerService = customerService;
            this._localizationService = localizationService;
            //this._securityQuestionManagementService = securityQuestionManagementService; //Atiqah 20190131 MDT-205
            this._customerActivityService = customerActivityService;
        }

		#endregion 

        #region Methods

        public virtual IActionResult AccessDenied(string pageUrl)
        {
            var currentCustomer = _workContext.CurrentCustomer;
            if (currentCustomer == null || currentCustomer.IsGuest())
            {
                _logger.Information($"Access denied to anonymous request on {pageUrl}");
                return View();
            }

            _logger.Information($"Access denied to user #{currentCustomer.Email} '{currentCustomer.Email}' on {pageUrl}");

            return View();
        }

        public virtual IActionResult Permissions()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            var model = new PermissionMappingModel();

            var permissionRecords = _permissionService.GetAllPermissionRecords();
            var customerRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var pr in permissionRecords)
            {
                model.AvailablePermissions.Add(new PermissionRecordModel
                {
                    //Name = pr.Name,
                    Name = pr.GetLocalizedPermissionName(_localizationService, _workContext),
                    SystemName = pr.SystemName
                });
            }
            foreach (var cr in customerRoles)
            {
                model.AvailableCustomerRoles.Add(new CustomerRoleModel
                {
                    Id = cr.Id,
                    Name = cr.Name
                });
            }
            foreach (var pr in permissionRecords)
                foreach (var cr in customerRoles)
                {
                    var allowed = pr.CustomerRoles.Count(x => x.Id == cr.Id) > 0;
                    if (!model.Allowed.ContainsKey(pr.SystemName))
                        model.Allowed[pr.SystemName] = new Dictionary<int, bool>();
                    model.Allowed[pr.SystemName][cr.Id] = allowed;
                }

            return View(model);
        }

        [HttpPost, ActionName("Permissions")]
        public virtual IActionResult PermissionsSave(IFormCollection form)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageAcl))
                return AccessDeniedView();

            var permissionRecords = _permissionService.GetAllPermissionRecords();
            var customerRoles = _customerService.GetAllCustomerRoles(true);

            foreach (var cr in customerRoles)
            {
                var formKey = "allow_" + cr.Id;
                var permissionRecordSystemNamesToRestrict = !StringValues.IsNullOrEmpty(form[formKey])
                    ? form[formKey].ToString().Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList()
                    : new List<string>();

                foreach (var pr in permissionRecords)
                {

                    var allow = permissionRecordSystemNamesToRestrict.Contains(pr.SystemName);
                    if (allow)
                    {
                        if (pr.CustomerRoles.FirstOrDefault(x => x.Id == cr.Id) == null)
                        {
                            pr.CustomerRoles.Add(cr);
                            _permissionService.UpdatePermissionRecord(pr);
                        }
                    }
                    else
                    {
                        if (pr.CustomerRoles.FirstOrDefault(x => x.Id == cr.Id) != null)
                        {
                            pr.CustomerRoles.Remove(cr);
                            _permissionService.UpdatePermissionRecord(pr);
                        }
                    }
                }
            }

            SuccessNotification(_localizationService.GetResource("Admin.Configuration.ACL.Updated"));
            return RedirectToAction("Permissions");
        }

        #endregion

        #region Backend Function: Security Question management
        //Atiqah 20190131 MDT-205 \/
        //Tony Liew 20180828 MSP-47 \/
        /// <summary>
        /// List searching criteria info
        /// </summary>
        /// <returns></returns>
        //public virtual IActionResult Index() 
        //{
        //    return RedirectToAction("GetSecurityQuestionsList");
        //}
        ////Tony Liew 20180828 MSP-47 /\

        ////Tony Liew 20180828 MSP-47 \/
        //public virtual IActionResult List()
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageSecurityQuestions))
        //        return AccessDeniedView();

        //    var model = new SecurityQuestionViewModel
        //    {
        //        //AvailableStatusOptions = _securityQuestionManagementService.GetPublishedValues()
        //        AvailableStatusOptions = EnumExtendMethod.ToSelectListItems<MSP_SecurityQuestion_Status>(true)
        //    };

        //    return View(model);
        //}
        ////Tony Liew 20180828 MSP-47 /\

        ////Tony Liew 20180828 MSP-47 \/
        ///// <summary>
        ///// List all the Security Question
        ///// </summary>
        ///// <param name="command"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public virtual IActionResult List(DataSourceRequest command, SecurityQuestionViewModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageSecurityQuestions))
        //        return AccessDeniedKendoGridJson();

        //    var questions = _securityQuestionManagementService.SecurityQuestionsList(model.SearchStatusID, pageIndex: command.Page - 1
        //        , pageSize: command.PageSize);
        //    var active = MSP_SecurityQuestion_Status.Active.ToDescription<MSP_SecurityQuestion_Status>();
        //    var inactive = MSP_SecurityQuestion_Status.Inactive.ToDescription<MSP_SecurityQuestion_Status>();
        //    int index = 1;
        //    var gridModel = new DataSourceResult
        //    {
        //        Data = questions.Select(x =>
        //        {
        //            //Tony Liew 20181001 MSP-47 \/
        //            //var _model = new SecurityQuestionViewModel();
        //            //_model.QuestionID = x.Id;
        //            //_model.Index = index++;
        //            //_model.Question = x.Question;
        //            //_model.Status = x.Status == MSP_SecurityQuestion_Status.Active.ToValue<MSP_SecurityQuestion_Status>() ? active : inactive;
        //            //Tony Liew 20181001 MSP-47 /\
        //            var _model = new SecurityQuestionViewModel
        //            {
        //                QuestionID = x.Id,
        //                Index = index++,
        //                Question = x.Question,
        //                Status = x.Status == MSP_SecurityQuestion_Status.Active.ToValue<MSP_SecurityQuestion_Status>() ? active : inactive
        //            };

        //            return _model;
        //        }),

        //        Total = questions.TotalCount
        //     };

        //    return Json(gridModel);

        //}
        ////Tony Liew 20180828 MSP-47 /\

        ////Tony Liew 20180828 MSP-47 \/
        ///// <summary>
        ///// Prepare Security Question's Model
        ///// </summary>
        ///// <returns></returns>
        //protected virtual void PrepareSecurityQuestionModel(SecurityQuestionViewModel model, MSP_SecurityQuestion question) 
        //{
        //    if (question != null)
        //    {
        //        var QuestionModel = _securityQuestionManagementService.GetQuestionID(question.Id);
        //        if (QuestionModel != null)
        //        {
        //            model.QuestionID = QuestionModel.Id; 
        //            model.Question = QuestionModel.Question;
        //            model.Status = QuestionModel.Status;
        //        }
        //    }
        //}
        ////Tony Liew 20180828 MSP-47 /\

        ///// <summary>
        ///// Redirect to create page
        ///// </summary>
        ///// <returns></returns>
        //public virtual IActionResult Create() //Tony Liew 20180906 MSP-47
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageSecurityQuestions))
        //        return AccessDeniedView();

        //    var model = new SecurityQuestionViewModel
        //    {
        //        IsActive = false
        //    };

        //    return View(model);
        //}

        ///// <summary>
        ///// POST create action and insert new question
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="continueEditing"></param>
        ///// <returns></returns>
        //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        //public virtual IActionResult Create(SecurityQuestionViewModel model, bool continueEditing)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageSecurityQuestions))
        //        return AccessDeniedView();

        //    if (ModelState.IsValid)
        //    {
        //        var AdminID = _workContext.CurrentCustomer.Id;
        //        var inactive = MSP_SecurityQuestion_Status.Inactive.ToValue<MSP_SecurityQuestion_Status>();
        //        var active = MSP_SecurityQuestion_Status.Active.ToValue<MSP_SecurityQuestion_Status>();

        //        var securityQuestion = model.ToSecurityEntity();

        //        securityQuestion.Question = model.Question;

        //        if (model.IsActive)
        //            securityQuestion.Status = active;
        //        else
        //            securityQuestion.Status = inactive;

        //        securityQuestion.CreatedOnUTC = DateTime.UtcNow;
        //        securityQuestion.UpdatedOnUtc = DateTime.UtcNow;
        //        securityQuestion.CreatedBy = AdminID;
        //        securityQuestion.UpdatedBy = AdminID;

        //        _securityQuestionManagementService.AddSecuirityQuestion(securityQuestion);
        //        //activity log
        //        _customerActivityService.InsertActivity("AddNewAnnouncement", _localizationService.GetResource("ActivityLog.AddNewSecurityQuestion"), securityQuestion.Id);
        //        SuccessNotification(_localizationService.GetResource("Admin.Configuration.SecurityQuestions.Added"));

        //        if (continueEditing)
        //        {
        //            //selected tab
        //            SaveSelectedTabName();

        //            return RedirectToAction("Edit", new { id = securityQuestion.Id });
        //        }
        //        return RedirectToAction("List");
        //    }

        //    return View(model);
        //}

        ///// <summary>
        ///// Redirect to edit page
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public virtual IActionResult Edit(int id) //Tony Liew 20180906 MSP-47
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageSecurityQuestions))
        //        return AccessDeniedView();
        //    var inactive = MSP_SecurityQuestion_Status.Inactive.ToValue<MSP_SecurityQuestion_Status>();
        //    var active = MSP_SecurityQuestion_Status.Active.ToValue<MSP_SecurityQuestion_Status>();
        //    var Question = _securityQuestionManagementService.GetQuestionID(id);
        //    if (Question == null)
        //        return RedirectToAction("List");

        //    var model = Question.ToSecurityModel();
        //    model.IsActive = Question.Status == inactive ? false : true;

        //    PrepareSecurityQuestionModel(model, Question);
        //    return View(model);
        //}

        ///// <summary>
        ///// POST edit action and update question
        ///// </summary>
        ///// <param name="model"></param>
        ///// <param name="continueEditing"></param>
        ///// <returns></returns>
        //[HttpPost, ParameterBasedOnFormName("continue-editing", "continueEditing")]
        //public virtual IActionResult Edit(SecurityQuestionViewModel model, bool continueEditing) //Tony Liew 20180903 MSP-47
        //{

        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageSecurityQuestions))
        //        return AccessDeniedView();

        //    var securityQuestion = _securityQuestionManagementService.GetQuestionID(model.QuestionID);
        //    string active = MSP_SecurityQuestion_Status.Active.ToValue<MSP_SecurityQuestion_Status>();
        //    string inactive = MSP_SecurityQuestion_Status.Inactive.ToValue<MSP_SecurityQuestion_Status>();
        //    if (ModelState.IsValid) //Passed SecurityValidator checking
        //    {
        //        securityQuestion = model.ToSecurityEntity(securityQuestion);

        //        if(model.IsActive)
        //            securityQuestion.Status = active;
        //        else
        //            securityQuestion.Status = inactive;

        //        securityQuestion.UpdatedOnUtc = DateTime.UtcNow;
        //        securityQuestion.Question = model.Question;

        //        _securityQuestionManagementService.UpdateSecurityQuestion(securityQuestion);

        //        //activity log
        //        _customerActivityService.InsertActivity("AddNewAnnouncement", _localizationService.GetResource("ActivityLog.EditSecurityQuestion"), securityQuestion.Id);

        //        SuccessNotification(_localizationService.GetResource("Admin.Configuration.SecurityQuestions.Edited"));
        //        if (continueEditing)
        //        {
        //            //selected tab
        //            SaveSelectedTabName();

        //            return RedirectToAction("Edit", new { id = securityQuestion.Id });
        //        }
        //        return RedirectToAction("List");
        //    }

        //    return View(model);
        //}
        //Atiqah 20190131 MDT-205 /\
        #endregion
    }
}