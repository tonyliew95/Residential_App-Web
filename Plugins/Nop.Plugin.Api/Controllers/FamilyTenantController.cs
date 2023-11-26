using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.Enumeration;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.FamilyTenant.Request;
using Nop.Plugin.Api.Models.FamilyTenant.ResponseResult;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services.Customers;
using Nop.Services.Residential.Helpers.ValidatorHelper;
using Nop.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Plugin.Api.Controllers
{
    /// <summary>
    /// FamilyTenant Controller
    /// </summary>
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    public class FamilyTenantController : BasesApiController
    {
        private readonly IFamilyTenantApiServices _familyTenantApiServices;
        private readonly ICustomerService _customerService;
        private readonly IValidatorHelper _validatorHelper;
        private ILog log;

        /// <summary>
        /// FamilyTenant Controller ctor
        /// </summary>
        /// <param name="familyTenantApiServices"></param>
        /// <param name="customerService"></param>
        /// <param name="jsonFieldsSerializer"></param>
        /// <param name="validatorHelper"></param>
        public FamilyTenantController
        (
            IFamilyTenantApiServices familyTenantApiServices
            , IJsonFieldsSerializer jsonFieldsSerializer
            , ICustomerService customerService
            , IValidatorHelper validatorHelper
        ) : base(jsonFieldsSerializer)
        {
            this._familyTenantApiServices = familyTenantApiServices;
            this._customerService = customerService;
            this._validatorHelper = validatorHelper;
            log = LogManager.GetLogger(Startup.repository_API.Name, typeof(FamilyTenantController));
        }

        #region Family Listing

        /// <summary>
        /// Mobile Frontend API: Family Tenant - Listing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/familytenant/list")]
        [ProducesResponseType(typeof(FamilyTenantListing_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FamilyTenantListing([FromQuery]FamilyTenantListing_Request request) //Tony Liew 20190403 RDT-175 
        {
            var response = new FamilyTenantListing_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);
            

            #region Validation
            try
            {
                if (customer == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                else if (request == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (string.IsNullOrEmpty(request.type)
              || (request.type != FamilyTenantController_Global.tenant.ToValue<FamilyTenantController_Global>() &&
              request.type != FamilyTenantController_Global.family.ToValue<FamilyTenantController_Global>()))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FamilyTenantController_Insert.invalidType;
                    ErrorResultObject.meta.message = FamilyTenantController_Insert.invalidType.ToDescription<FamilyTenantController_Insert>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response = _familyTenantApiServices.GetFamilyTenantList(request);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        #region Family and Tenant Details

        /// <summary>
        /// Mobile Frontend API: Family Tenant - Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("/api/familytenant/user")]
        [ProducesResponseType(typeof(FamilyTenantDetails_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FamilyTenantDetails([FromQuery]FamilyTenantDetails_Request request) //Tony Liew 20190416 RDT-186 
        {
            var response = new FamilyTenantDetails_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);


            #region Validation
            try
            {
                if (customer == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                else if (request == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.accId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidAccountId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidAccountId.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (string.IsNullOrEmpty(request.type)
               || (request.type != FamilyTenantController_Global.tenant.ToValue<FamilyTenantController_Global>() &&
               request.type != FamilyTenantController_Global.family.ToValue<FamilyTenantController_Global>()))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FamilyTenantController_Insert.invalidType;
                    ErrorResultObject.meta.message = FamilyTenantController_Insert.invalidType.ToDescription<IncidentController_Insert>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                //response = _familyTenantApiServices.GetFamilyTenantDetails(request);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        #region Family and Tenant Insert

        /// <summary>
        /// Mobile Frontend API: Family Tenant - Add
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/familytenant/user/add")]
        [ProducesResponseType(typeof(AddFamilyTenant_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FamilyTenantInsert([FromBody]AddFamilyTenant_Request request) //Tony Liew 20190404 RDT-176 
        {
            var response = new AddFamilyTenant_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            #region Validation
            try
            {
                if (customer == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                else if (request == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (string.IsNullOrEmpty(request.type))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FamilyTenantController_Insert.invalidType;
                    ErrorResultObject.meta.message = FamilyTenantController_Insert.invalidType.ToDescription<IncidentController_Insert>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (!string.IsNullOrEmpty(request.ocpyStart) && !_validatorHelper.validateInputDate(request.ocpyStart))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FamilyTenantController_Insert.invalidDateFormat;
                    ErrorResultObject.meta.message = FamilyTenantController_Insert.invalidDateFormat.ToDescription<IncidentController_Insert>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (!string.IsNullOrEmpty(request.ocpyEnd) && !_validatorHelper.validateInputDate(request.ocpyEnd))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FamilyTenantController_Insert.invalidDateFormat;
                    ErrorResultObject.meta.message = FamilyTenantController_Insert.invalidDateFormat.ToDescription<FamilyTenantController_Insert>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (!string.IsNullOrEmpty(request.reminder) && !_validatorHelper.validateInputDate(request.reminder))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FamilyTenantController_Insert.invalidDateFormat;
                    ErrorResultObject.meta.message = FamilyTenantController_Insert.invalidDateFormat.ToDescription<FamilyTenantController_Insert>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (string.IsNullOrEmpty(request.countryCode))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FamilyTenantController_Updated.countryCodeFormatInvalid;
                    ErrorResultObject.meta.message = FamilyTenantController_Updated.countryCodeFormatInvalid.ToDescription<FamilyTenantController_Updated>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.type == FamilyTenantController_Global.family.ToValue<FamilyTenantController_Global>())
                    request.emergency = true;

            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response = _familyTenantApiServices.InsertFamilyTenant(request, customer.Id);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        #region Family and Tenant Update 

        /// <summary>
        /// Mobile Frontend API: Family Tenant - Update
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/familytenant/user/update")]
        [ProducesResponseType(typeof(EditFamilyTenant_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FamilyTenantInsert([FromBody]EditFamilyTenant_Request request) //Tony Liew 20190404 RDT-176 
        {
            var response = new EditFamilyTenant_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            #region Validation
            try
            {
                if (customer == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else
                {
                    if (request == null)
                    {
                        //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                        ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                        ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToValue<RES_GlobalEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (request.orgId == 0)
                    {
                        ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                        ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToValue<RES_GlobalEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (request.propId == 0)
                    {
                        ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                        ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToValue<RES_GlobalEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (string.IsNullOrEmpty(request.type)
                        || (request.type != FamilyTenantController_Global.tenant.ToValue<FamilyTenantController_Global>() &&
                        request.type != FamilyTenantController_Global.family.ToValue<FamilyTenantController_Global>()))
                    {
                        log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                        ErrorResultObject.meta.code = (int)FamilyTenantController_Insert.invalidType;
                        ErrorResultObject.meta.message = FamilyTenantController_Insert.invalidType.ToDescription<IncidentController_Insert>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (string.IsNullOrEmpty(request.countryCode))
                    {
                        log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                        ErrorResultObject.meta.code = (int)FamilyTenantController_Updated.countryCodeFormatInvalid;
                        ErrorResultObject.meta.message = FamilyTenantController_Updated.countryCodeFormatInvalid.ToDescription<IncidentController_Insert>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (request.emergency)
                    {
                        if (string.IsNullOrEmpty(request.msisdn) || string.IsNullOrEmpty(request.countryCode))
                        {
                            log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                            ErrorResultObject.meta.code = (int)FamilyTenantController_Updated.emptyMsisdn;
                            ErrorResultObject.meta.message = FamilyTenantController_Updated.emptyMsisdn.ToDescription<IncidentController_Insert>();
                            return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                        }
                    }

                    if (request.type == FamilyTenantController_Global.tenant.ToValue<FamilyTenantController_Global>() 
                        || request.type == FamilyTenantController_Global.subTenant.ToValue<FamilyTenantController_Global>())
                        request.emergency = false;
                }

            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response = _familyTenantApiServices.UpdateFamilyTenant(request, customer.Id);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        #region Family and Tenant Remove 

        /// <summary>
        /// Mobile Frontend API: Family Tenant - Delete
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("/api/familytenant/user/remove")]
        [ProducesResponseType(typeof(RemoveFamilyTenant_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FamilyTenantRemove([FromBody]RemoveFamilyTenant_Request request) //Tony Liew 20190404 RDT-176 
        {
            var response = new RemoveFamilyTenant_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            string[] types = { };

            #region Validation
            try
            {
                if (customer == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                else if (request == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.accId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidAccountId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidAccountId.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (string.IsNullOrEmpty(request.type)
                    || (request.type != FamilyTenantController_Global.tenant.ToValue<FamilyTenantController_Global>() &&
                    request.type != FamilyTenantController_Global.family.ToValue<FamilyTenantController_Global>()))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FamilyTenantController_Insert.invalidType;
                    ErrorResultObject.meta.message = FamilyTenantController_Insert.invalidType.ToDescription<IncidentController_Insert>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response = _familyTenantApiServices.RemoveFamilyTenant(request, customer.Id);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

    }
}
