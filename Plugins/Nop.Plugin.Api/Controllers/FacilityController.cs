using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.Enumeration;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Facility.Request;
using Nop.Plugin.Api.Models.Facility.ResponseResult;
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

//Tony Liew 20190417 \/
namespace Nop.Plugin.Api.Controllers
{
    /// <summary>
    /// Facility Controller
    /// </summary>
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    public class FacilityController : BasesApiController
    {
        private ILog log;
        private readonly ICustomerService _customerService;
        private readonly IValidatorHelper _validatorHelper;
        private readonly IFacilityApiServices _facilityApiServices;

        /// <summary>
        /// Facility Controller Ctor
        /// <param name="facilityApiServices"></param>
        /// <param name="customerService"></param>
        /// <param name="jsonFieldsSerializer"></param>
        /// <param name="validatorHelper"></param>
        /// </summary>
        public FacilityController
        (
            IJsonFieldsSerializer jsonFieldsSerializer
            , ICustomerService customerService
            , IValidatorHelper validatorHelper
            , IFacilityApiServices facilityApiServices
        ) : base(jsonFieldsSerializer)
        {
            this._customerService = customerService;
            this._validatorHelper = validatorHelper;
            this._facilityApiServices = facilityApiServices;
            log = LogManager.GetLogger(Startup.repository_API.Name, typeof(FacilityController));
        }

        #region Facility Listing

        /// <summary>
        /// Mobile Frontend API: Booking Listing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/facility/booking/list")]
        [ProducesResponseType(typeof(BookingFacilityListing_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult FamilyTenantListing([FromQuery]BookingFacilityListing_Request request) //Tony Liew 20190417 RDT-202
        {
            var response = new BookingFacilityListing_ResponseResult();
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
                else if (!string.IsNullOrEmpty(request.dateFrom) && !_validatorHelper.validateInputDate(request.dateFrom))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FacilityController_Global.invalidDateFormat;
                    ErrorResultObject.meta.message = FacilityController_Global.invalidDateFormat.ToValue<FacilityController_Global>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else if (!string.IsNullOrEmpty(request.dateTo) && !_validatorHelper.validateInputTime(request.dateTo))
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)FacilityController_Global.invalidDateFormat;
                    ErrorResultObject.meta.message = FacilityController_Global.invalidDateFormat.ToValue<FacilityController_Global>();
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
                response = _facilityApiServices.BookingListing(request);
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

//Tony Liew 20190417 /\
