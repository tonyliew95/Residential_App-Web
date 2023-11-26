using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Services.Interfaces;
using Nop.Services.Customers;
using Nop.Services.Residential.Helpers.ValidatorHelper;
using Nop.Web;
using System;
using System.Net;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Models.Visitor.Request;
using Nop.Plugin.Api.Models.Visitor.ResponseResults;
using Nop.Plugin.Api.Enumeration;
using Nop.Services.Residential.Visitor;
using Nop.Services.Residential.Helpers.FormatingHelper;
using Nop.Services.Residential.Helpers.BaseHelper;

namespace Nop.Plugin.Api.Controllers
{
    /// <summary>
    /// Visitor Controller
    /// </summary>
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    public class VisitorController : BasesApiController
    {
        private readonly ICustomerService _customerService;
        private readonly IVisitorApiService _visitorApiService;
        private readonly IVisitorService _visitorService;
        private readonly IValidatorHelper _validatorHelper;
        private readonly IFormatingHelper _formatingHelper;
        private readonly IBaseHelper _baseHelper;
        private ILog log;

        /// <summary>
        /// Visitor Controller Ctor
        /// </summary>
        /// <param name="customerService"></param>
        /// <param name="visitorApiService"></param>
        /// <param name="visitorService"></param>
        /// <param name="validatorHelper"></param>
        /// <param name="formatingHelper"></param>
        /// <param name="baseHelper"></param>
        /// <param name="jsonFieldsSerializer"></param>
        public VisitorController(
            ICustomerService customerService,
            IVisitorApiService visitorApiService,
            IVisitorService visitorService,
            IValidatorHelper validatorHelper,
            IFormatingHelper formatingHelper,
            IBaseHelper baseHelper,
            IJsonFieldsSerializer jsonFieldsSerializer) : base(jsonFieldsSerializer)
        {
            this._visitorApiService = visitorApiService;
            this._visitorService = visitorService;
            this._customerService = customerService;
            this._validatorHelper = validatorHelper;
            this._formatingHelper = formatingHelper;
            this._baseHelper = baseHelper;
            log = LogManager.GetLogger(Startup.repository_API.Name, typeof(AnnouncementController));
        }

        // WKK 20190411 RDT-189 [API] Visitor - Request History List
        #region Visitor - Request History List

        /// <summary>
        /// Mobile Frontend API: Visitor - Request History List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/visitor/history/list")]
        [ProducesResponseType(typeof(HistoryList_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult HistoryList([FromQuery] HistoryList_Request request)
        {
            var response = new HistoryList_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            #region Validation
            try
            {
                if (customerId == 0)
                {
                    ErrorResultObject.meta.code   = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                if (request == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.dateFrom != null)
                {
                    if (!_validatorHelper.validateInputDate(request.dateFrom))
                    {
                        ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidVisitDate;
                        ErrorResultObject.meta.message = VisitorControllerEnum.InvalidVisitDate.ToDescription<VisitorControllerEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
                else if (request.dateTo != null)
                {
                    if (!_validatorHelper.validateInputDate(request.dateTo))
                    {
                        ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidVisitDate;
                        ErrorResultObject.meta.message = VisitorControllerEnum.InvalidVisitDate.ToDescription<VisitorControllerEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response = _visitorApiService.GetHistoryListing(request, customerId);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        // WKK 20190411 RDT-192 [API] Visitor - Record History Details
        // WKK 20190416 RDT-191 [API] Visitor - Request Details
        #region Visitor Request Details

        /// <summary>
        /// Mobile Frontend API: Visitor Request Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/visitor/history/details")]
        [Route("/api/visitor/submission/details")]
        [ProducesResponseType(typeof(HistoryDetails_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult HistoryDetails([FromQuery]HistoryDetails_Request request)
        {
            var response = new HistoryDetails_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            #region Validation
            try
            {
                if (request == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if(request.orgId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.trxId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response.data.visitorDto = _visitorApiService.GetHistoryDetails(request.trxId);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        // WKK 20190415 RDT-190 [API] Visitor - Request Submission
        #region Submit a visitor request.

        /// <summary>
        /// Mobile Frontend API: Request Submission
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/visitor/submission")]
        [ProducesResponseType(typeof(HistoryDetails_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult VisitorRequestSubmission([FromBody]VisitorSubmission_Request request)
        {
            var response = new HistoryDetails_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            #region Validation
            try
            {
                if (customerId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                if (request == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.visitorId < 1)
                {
                    if (string.IsNullOrEmpty(request.name))
                    {
                        ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidVisitorName;
                        ErrorResultObject.meta.message = VisitorControllerEnum.InvalidVisitorName.ToDescription<VisitorControllerEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (string.IsNullOrEmpty(request.msisdn))
                    {
                        ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidPhoneNumber;
                        ErrorResultObject.meta.message = VisitorControllerEnum.InvalidPhoneNumber.ToDescription<VisitorControllerEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (string.IsNullOrEmpty(request.identityNum))
                    {
                        ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidIdentityNumber;
                        ErrorResultObject.meta.message = RES_GlobalEnum.invalidIdentityNumber.ToDescription<RES_GlobalEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
                else if (string.IsNullOrEmpty(request.purpose))
                {
                    ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidVisitType;
                    ErrorResultObject.meta.message = VisitorControllerEnum.InvalidVisitType.ToDescription<VisitorControllerEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (string.IsNullOrEmpty(request.visitingDate))
                {
                    ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidVisitDate;
                    ErrorResultObject.meta.message = VisitorControllerEnum.InvalidVisitDate.ToDescription<VisitorControllerEnum>();

                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (!_validatorHelper.validateInputDate(request.visitingDate))
                {
                    ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidVisitDate;
                    ErrorResultObject.meta.message = VisitorControllerEnum.InvalidVisitDate.ToDescription<VisitorControllerEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);

                }
                else if (request.driveIn == true)
                {
                    if (string.IsNullOrEmpty(request.vehicle.type))
                    {
                        ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidVehicleType;
                        ErrorResultObject.meta.message = VisitorControllerEnum.InvalidVehicleType.ToDescription<VisitorControllerEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (string.IsNullOrEmpty(request.vehicle.number))
                    {
                        ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidVehicleNumber;
                        ErrorResultObject.meta.message = VisitorControllerEnum.InvalidVehicleNumber.ToDescription<VisitorControllerEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                int trxId = _visitorApiService.VisitorRequestSubmission(request, customerId);

                if (trxId > 0)
                {
                    response.data.visitorDto = _visitorApiService.GetHistoryDetails(trxId);
                }
                else
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                    ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        // WKK 20190416 RDT-197 [API] Visitor - Clock In/Out
        #region Clock In/Out

        /// <summary>
        /// Mobile Frontend API: Clock In/Out
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/visitor/clocktime")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult VisitorTimeClock([FromBody]ClockInOut_Request request)
        {
            var response = new ApiResponse();
            var ErrorResultObject = new ErrorsResultObject();

            #region Validation
            try
            {
                if (request == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (string.IsNullOrEmpty(request.clockType))
                {
                    ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidClockInOut;
                    ErrorResultObject.meta.message = VisitorControllerEnum.InvalidClockInOut.ToDescription<VisitorControllerEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.trxId == 0)
                {
                    ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidTrxId;
                    ErrorResultObject.meta.message = VisitorControllerEnum.InvalidTrxId.ToDescription<VisitorControllerEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                if (_visitorService.CheckDuplicateClockInOut(request.trxId, request.clockType))
                {
                    if (request.clockType == VisitorTimestampType.ClockIn.ToValue<VisitorTimestampType>())
                    {
                        ErrorResultObject.meta.code = (int)VisitorControllerEnum.AlreadyClockIn;
                        ErrorResultObject.meta.message = VisitorControllerEnum.AlreadyClockIn.ToDescription<VisitorControllerEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (request.clockType == VisitorTimestampType.ClockOut.ToValue<VisitorTimestampType>())
                    {
                        ErrorResultObject.meta.code = (int)VisitorControllerEnum.AlreadyClockOut;
                        ErrorResultObject.meta.message = VisitorControllerEnum.AlreadyClockOut.ToDescription<VisitorControllerEnum>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }

                var visit = _visitorService.GetTrxVisitor(request.trxId);
                if (visit == null)
                {
                    ErrorResultObject.meta.code = (int)VisitorControllerEnum.InvalidTrxId;
                    ErrorResultObject.meta.message = VisitorControllerEnum.InvalidTrxId.ToDescription<VisitorControllerEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (visit.VisitingDate.Date != DateTime.UtcNow.AddHours(_baseHelper.getUtcOffsetByOrgId(visit.OrganizationId)).Date)
                {
                    ErrorResultObject.meta.code = (int)VisitorControllerEnum.WrongVisitDate;
                    ErrorResultObject.meta.message = VisitorControllerEnum.WrongVisitDate.ToDescription<VisitorControllerEnum>() + " : " 
                        + _formatingHelper.getDateFormat(visit.VisitingDate);
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                _visitorApiService.VisitorTimeClock(request.trxId, request.clockType);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        // WKK 20190417 RDT-193 [API] Visitor - Favourite Listing
        #region Get Favourite List

        /// <summary>
        /// Mobile Frontend API: Visitor - Favourite Listing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/visitor/favourite/list")]
        [ProducesResponseType(typeof(FavouriteList_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FavouriteVisitorList([FromQuery] FavouriteList_Request request)
        {
            var response = new FavouriteList_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            #region Validation
            try
            {
                if (customerId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                if (request == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response = _visitorApiService.GetFavouriteVisitorList(request, customerId);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        // WKK 20190417 RDT-195 [API] Visitor - Favourite Add
        #region Add user's visitor into favourite visitor list

        /// <summary>
        /// Mobile Frontend API: Add user's visitor into favourite visitor list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/visitor/favourite/add")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FavouriteVisitorAdd([FromBody]FavouriteVisitor_Request request)
        {
            var response = new ApiResponse();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            #region Validation
            try
            {
                if (customerId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                if (request == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.visitorId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidVisitorId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidVisitorId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                _visitorApiService.FavouriteVisitorAdd(request, customerId);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        // WKK 20190418 RDT-196 [API] Visitor - Favourite Remove
        #region Remove user's visitor from favourite visitor list

        /// <summary>
        /// Mobile Frontend API: Remove user's visitor from favourite visitor list
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("/api/visitor/favourite/remove")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FavouriteVisitorRemove([FromBody]FavouriteVisitor_Request request)
        {
            var response = new ApiResponse();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            #region Validation
            try
            {
                if (customerId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                if (request == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.visitorId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidVisitorId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidVisitorId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                _visitorApiService.FavouriteVisitorRemove(request, customerId);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        // WKK 20190418 RDT-194 [API] Visitor - Favourite Details
        #region Retrieve user's visitor favourite details by visitor id.

        /// <summary>
        /// Mobile Frontend API: Retrieve user's visitor favourite details by visitor id.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/visitor/favourite/details")]
        [ProducesResponseType(typeof(FavouriteDetails_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FavouriteVisitorDetails([FromQuery]FavouriteVisitor_Request request)
        {
            var response = new FavouriteDetails_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            #region Validation
            try
            {
                if (customerId == 0)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                if (request == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.orgId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noOrgId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noOrgId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.propId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (request.visitorId < 1)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidVisitorId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidVisitorId.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message, ex);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                response.data.visitorDto = _visitorApiService.GetVisitorDetails(request.visitorId);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>() + " : " + ex.Message;
                log.Error(ex.Message, ex);
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
