using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.General.Request;
using Nop.Plugin.Api.Models.General.ResponseResults;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services.Customers;
using Nop.Services.Security;
using Nop.Web;
using System;
using System.Net;
using static Nop.Plugin.Api.Models.General.ResponseResults.Setting_ResponseResult;

namespace Nop.Plugin.Api.Controllers
{
    /// <summary>
    /// Profile Controller
    /// </summary>
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    public class GeneralController : BasesApiController
    {
        private readonly ICustomerService _customerService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IGeneralApiService _generalApiService;
        private ILog log;

        public GeneralController(
            ICustomerService customerService
            , ICryptographyService cryptographyService
            , IGeneralApiService generalApiService
            , IJsonFieldsSerializer jsonFieldsSerializer) : base(jsonFieldsSerializer)
        {
            this._customerService = customerService;
            this._cryptographyService = cryptographyService;
            this._generalApiService = generalApiService;
            this.log = LogManager.GetLogger(Startup.repository_API.Name, typeof(GeneralController));
        }
        
        #region Get default setting 
        /// <summary>
        /// Frontend API: Get dafault setting
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/general/setting")]
        [ProducesResponseType(typeof(Setting_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult settings([FromQuery]Setting_Request request) //JK 20190322 RDT-166
        {
            var response = new Setting_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            string signature = Request.Headers["sdkVersion"].ToString() + "|" + Request.Headers["platform"].ToString() + "|" + Request.Headers["deviceUuid"].ToString();
            string language = Request.Headers["Accept-Language"];
            
            #region Validation
            if (request == null)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToValue<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            else
            {
                // check the signature
                bool verified = _cryptographyService.VerifyRSADigitalSignatureSHA1(signature, request.signature);
                if (!verified)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidSignature;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidSignature.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            #endregion

            #region Execution
            try
            {
                SettingList list = new SettingList();
                list.settingDto = _generalApiService.GetGeneralSetting();
                response.data = list;
                //response = _profileApiService.listProperty(request , customer.Id);
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToValue<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
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
