using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.Enumeration;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Incident.Request;
using Nop.Plugin.Api.Models.Incident.ResponseResults;
using Nop.Plugin.Api.Services.Interfaces;
using Nop.Services.Customers;
using Nop.Services.Residential.Helpers.ValidatorHelper;
using Nop.Web;
using System;
using System.Net;

namespace Nop.Plugin.Api.Controllers
{
    /// <summary>
    /// Incident Controller
    /// </summary>
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    public class IncidentController : BasesApiController
    {
        private readonly ICustomerService _customerService;
        private readonly IIncidentApiService _incidentService;
        private readonly IValidatorHelper _validatorHelper;
        private ILog log;

        /// <summary>
        /// Incident Controller Ctor
        /// </summary>
        /// <param name="customerService"></param>
        /// <param name="incidentService"></param>
        /// <param name="validatorHelper"></param>
        /// <param name="jsonFieldsSerializer"></param>
        public IncidentController(
            ICustomerService customerService,
            IIncidentApiService incidentService,
            IValidatorHelper validatorHelper,
            IJsonFieldsSerializer jsonFieldsSerializer) : base(jsonFieldsSerializer)
        {
            _incidentService = incidentService;
            _customerService = customerService;
            _validatorHelper = validatorHelper;
            log = LogManager.GetLogger(Startup.repository_API.Name, typeof(IncidentController));
        }

        #region Incident List
        /// <summary>
        /// Mobile Frontend API: Incident listing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/incident/list")]
        [ProducesResponseType(typeof(IncidentList_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult IncidentListing([FromQuery] IncidentList_Request request) //Tony Liew 20190306 RDT-116
        {
            var response = new IncidentList_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();
            //string Language = Request.Headers["Accept-Language"];

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            #region Validation
            try
            {
                if (customer == null)
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else // this block for debuging/troubleshooting for mobile team
                {
                    if (request == null)
                    {
                        log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                        ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                        ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToValue<RES_GlobalEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response = _incidentService.GetIncidentList(request, customer.Id);
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
            //if (response.ReturnCode == 0) response.ReturnMessage = _utilityHelper.GetLanguage(Language, response.ReturnMessage); 
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }
        #endregion

        #region Incident Details
        /// <summary>
        /// Mobile Frontend API: Incident Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/incident/details")]
        [ProducesResponseType(typeof(IncidentDetails_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult IncidentDetails([FromQuery] IncidentDetails_Request request) //Tony Liew 20190306 RDT-117
        {
            var response = new IncidentDetails_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            //string Language = Request.Headers["Accept-Language"];

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            #region Validation
            try
            {
                if (customer == null)
                {
                    //logFE.Info("ReturnCode : " + response.ReturnCode + ", ReturnMessage : " + response.ReturnMessage);
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else // this block for debuging/troubleshooting for mobile team
                {
                    if (request == null)
                    {
                        //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                        ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                        ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToValue<RES_GlobalEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response = _incidentService.GetIncidentDetails(request, customer.Id);
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
            //if (response.ReturnCode == 0) response.ReturnMessage = _utilityHelper.GetLanguage(Language, response.ReturnMessage); 
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }
        #endregion

        #region Incident Report
        /// <summary>
        /// Mobile Frontend API: Incident Report
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/incident/report")]
        [ProducesResponseType(typeof(IncidentReport_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult IncidentReport([FromBody] IncidentReport_Request request) //Tony Liew 20190307 RDT-118
        {
            var response = new IncidentReport_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();
           
            string Language = Request.Headers["Accept-Language"];

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            #region Validation
            try
            {
                if (customer == null)
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code    = (int)RES_GlobalEnum.invalidUser;
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
                  else if (!string.IsNullOrEmpty(request.incident.date) && !_validatorHelper.validateInputDate(request.incident.date))
                  {
                      log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                      ErrorResultObject.meta.code    = (int)IncidentController_Insert.invalidDateFormat;
                      ErrorResultObject.meta.message = IncidentController_Insert.invalidDateFormat.ToValue<IncidentController_Insert>();
                      return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                  }
                  else if (!string.IsNullOrEmpty(request.incident.time) && !_validatorHelper.validateInputTime(request.incident.time))
                  {
                      log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                      ErrorResultObject.meta.code    = (int)IncidentController_Insert.invalidTimeFormat;
                      ErrorResultObject.meta.message = IncidentController_Insert.invalidTimeFormat.ToValue<IncidentController_Insert>();
                      return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                  }
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                _incidentService.InsertIncidentReport(request, customer.Id);
                response.meta.message = IncidentController_Insert.reportSuccessfully.ToDescription<IncidentController_Insert>();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code    = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            //if (response.ReturnCode == 0) response.ReturnMessage = _utilityHelper.GetLanguage(Language, response.ReturnMessage); 
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }
        #endregion

    }
}
