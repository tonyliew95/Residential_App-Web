using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Announcement.ResponseResults;
using Nop.Plugin.Api.Services.Interfaces;
using Nop.Services.Customers;
using Nop.Services.Residential.Helpers.ValidatorHelper;
using Nop.Web;
using System;
using System.Net;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Models.Announcement.Request;

namespace Nop.Plugin.Api.Controllers
{
    /// <summary>
    /// Announcement Controller
    /// </summary>
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    public class AnnouncementController : BasesApiController
    {
        private readonly ICustomerService _customerService;
        private readonly IAnnouncementApiService _announcementApiService;
        private readonly IValidatorHelper _validatorHelper;
        private ILog log;

        /// <summary>
        /// Announcement Controller Ctor
        /// </summary>
        /// <param name="customerService"></param>
        /// <param name="announcementService"></param>
        /// <param name="validatorHelper"></param>
        /// <param name="jsonFieldsSerializer"></param>
        public AnnouncementController(
            ICustomerService customerService,
            IAnnouncementApiService announcementService,
            IValidatorHelper validatorHelper,
            IJsonFieldsSerializer jsonFieldsSerializer) : base(jsonFieldsSerializer)
        {
            _announcementApiService = announcementService;
            _customerService = customerService;
            _validatorHelper = validatorHelper;
            log = LogManager.GetLogger(Startup.repository_API.Name, typeof(AnnouncementController));
        }

        //WKK 20190315 RDT-121 [API] Notice - Announcement list
        #region Announcement List

        /// <summary>
        /// Mobile Frontend API: Announcement listing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/announcement/list")]
        [ProducesResponseType(typeof(AnnouncementList_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult AnnouncementListing([FromQuery] AnnouncementList_Request request)
        {
            var response = new AnnouncementList_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            //var customer = _customerService.GetCustomerByEmail(customerEmail);
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            #region Validation
            try
            {
                //if (customer == null)
                if (customerId == 0)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code   = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                if (request == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
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
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code =  ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                //response = _announcementApiService.GetAnnouncementList(param, customer.Id);
                response = _announcementApiService.GetAnnouncementList(request, customerId);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        //WKK 20190315 RDT-122 [API] Notice - Announcement detail
        #region Announcement Details

        /// <summary>
        /// Mobile Frontend API: Announcement Details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/announcement/details")]
        [ProducesResponseType(typeof(AnnouncementDetails_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult AnnouncementDetails([FromQuery]AnnouncementDetails_Request request)
        {
            var response = new AnnouncementDetails_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            #region Validation
            try
            {
                if (request == null)
                {
                    //log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
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
                response.data.announcementDetail = _announcementApiService.GetAnnouncementDetails(request.id);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code  = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
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
