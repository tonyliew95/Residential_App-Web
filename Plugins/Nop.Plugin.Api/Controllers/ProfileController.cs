using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.Enumeration;
using Nop.Plugin.Api.Helpers;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Profile.Request;
using Nop.Plugin.Api.Models.Profile.ResponseResults;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services.Customers;
using Nop.Services.Residential.Helpers.BaseHelper;
using Nop.Services.Security;
using Nop.Web;
using System;
using System.Net;
using Nop.Services.Residential.Profile;
using IdentityServer4.Services;
using System.Linq;
using Nop.Core.Domain.Residential.Custom;
using System.Configuration;

namespace Nop.Plugin.Api.Controllers
{
    /// <summary>
    /// Profile Controller
    /// </summary>
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    public class ProfileController : BasesApiController
    {
        private ILog log;
        private readonly IBaseHelper _baseHelper;
        private readonly ICryptographyService _cryptographyService;
        private readonly ICustomerService _customerService;
        private readonly IProfileApiService _profileApiService;
        private readonly IProfileServices _profileServices;

        public ProfileController(
            IBaseHelper baseHelper
            , ICryptographyService cryptographyService
            , ICustomerService customerService
            , IProfileApiService profileApiService
            , IProfileServices profileServices
            , IJsonFieldsSerializer jsonFieldsSerializer) : base(jsonFieldsSerializer)
        {
            this._baseHelper = baseHelper;
            this._cryptographyService = cryptographyService;
            this._customerService = customerService;
            this._profileApiService = profileApiService;
            this._profileServices = profileServices;
            this.log = LogManager.GetLogger(Startup.repository_API.Name, typeof(ProfileController));
        }

        #region module : account-settings

        //WKK 20190326 RDT-173 [API] P.Account settings - Change display name
        #region Change display name
        /// <summary>
        /// Mobile Frontend API: Change display name
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/profile/account-settings/displayName")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult UpdateDisplayName([FromBody]UpdateName_Request request)
        {
            var response = new ApiResponse();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            if (customerId == 0)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            if (request == null)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            else
            if (request.displayName == null)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            #region Execution
            try
            {
                bool success = _profileApiService.UpdateDisplayNameEmailId(customerId, request.displayName);

                if (!success)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                    ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else
                {
                    response.data = new { userDto = request };
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        #region Add User Contact Number
        /// <summary>
        /// Frontend API: Add User Contact Number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/profile/account-settings/contact/add")]
        [ProducesResponseType(typeof(AddContactNumber_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult addContact([FromBody]AddContactNumber_Request request) //Tony Liew 20190319 RDT-68
        {
            var response = new AddContactNumber_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            string[] header = { Request.Headers["sdkVersion"], Request.Headers["platform"], Request.Headers["deviceUuid"] };

            #region Validation

            if (customer == null)
            {
                //logFE.Info("ReturnCode : " + response.ReturnCode + ", ReturnMessage : " + response.ReturnMessage);
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
                else
                {
                    if (string.IsNullOrEmpty(request.contactDto.countryCode) )
                    {
                      ErrorResultObject.meta.code = (int)ProfileController_Global.countryCodeFormatInvalid;
                      ErrorResultObject.meta.message = ProfileController_Global.countryCodeFormatInvalid.ToDescription<ProfileController_AddContactNumber>();
                      return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (!string.IsNullOrEmpty(request.contactDto.msisdn))
                    {
                        if (!int.TryParse(request.contactDto.msisdn, out int returnPhoneNumber)) //check whether the input is numerical number or not
                        {
                            ErrorResultObject.meta.code = (int)ProfileController_Global.notNumericalNumber;
                            ErrorResultObject.meta.message = ProfileController_Global.notNumericalNumber.ToDescription<ProfileController_Global>();
                            return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                        }
                    }
                    else
                    {
                        ErrorResultObject.meta.code = (int)ProfileController_Global.contactNumberIsEmpty;
                        ErrorResultObject.meta.message = ProfileController_Global.contactNumberIsEmpty.ToDescription<ProfileController_Global>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }
            #endregion

            #region Execution
            try
            {
                response = _profileApiService.AddUserContactNumber(request, customer.Id , request.contactDto.countryCode + "|" + request.contactDto.msisdn + "|" + header[2]);
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

        #region Update User Contact Number
        /// <summary>
        /// Frontend API: Update User Contact Number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/profile/account-settings/contact/update")]
        [ProducesResponseType(typeof(UpdateContact_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult updateContact([FromBody]UpdateContact_Request request) //Tony Liew 20190319 RDT-67
        {
            var response = new UpdateContact_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            string[] header = { Request.Headers["sdkVersion"], Request.Headers["platform"], Request.Headers["deviceUuid"] };

            #region Validation

            if (customer == null)
            {
                //logFE.Info("ReturnCode : " + response.ReturnCode + ", ReturnMessage : " + response.ReturnMessage);
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
                else
                {
                    if (string.IsNullOrEmpty(request.countryCode))
                    {
                        ErrorResultObject.meta.code = (int)ProfileController_Global.countryCodeFormatInvalid;
                        ErrorResultObject.meta.message = ProfileController_Global.countryCodeFormatInvalid.ToDescription<ProfileController_AddContactNumber>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (!string.IsNullOrEmpty(request.msisdn))
                    {
                        if (!int.TryParse(request.msisdn, out int returnPhoneNumber)) //check whether the input is numerical number or not
                        {
                            ErrorResultObject.meta.code = (int)ProfileController_Global.notNumericalNumber;
                            ErrorResultObject.meta.message = ProfileController_Global.notNumericalNumber.ToDescription<ProfileController_Global>();
                            return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                        }
                    }
                    else
                    {
                        ErrorResultObject.meta.code = (int)ProfileController_Global.contactNumberIsEmpty;
                        ErrorResultObject.meta.message = ProfileController_Global.contactNumberIsEmpty.ToDescription<ProfileController_Global>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }

            #endregion

            #region Execution
            try
            {
                response = _profileApiService.UpdateUserContactNumber(request, customer.Id);
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

        #region Delete User Contact Number
        /// <summary>
        /// Frontend API: Delete User Contact Number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/api/profile/account-settings/contact/remove")]
        [ProducesResponseType(typeof(RemoveContact_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult removeContact([FromBody]RemoveContact_Request request) //Tony Liew 20190415 RDT-200
        {
            var response = new RemoveContact_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            string[] header = { Request.Headers["sdkVersion"], Request.Headers["platform"], Request.Headers["deviceUuid"] };

            #region Validation

            if (customer == null)
            {
                //logFE.Info("ReturnCode : " + response.ReturnCode + ", ReturnMessage : " + response.ReturnMessage);
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
                else
                {
                    if (request.contactId == 0)
                    {
                        ErrorResultObject.meta.code = (int)ProfileController_ChangeContactNumber.NoContactNumberId;
                        ErrorResultObject.meta.message = ProfileController_ChangeContactNumber.NoContactNumberId.ToDescription<ProfileController_ChangeContactNumber>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }

            #endregion

            #region Execution
            try
            {
                response = _profileApiService.RemoveContact(request, customer.Id);
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

        #region List User Contact Number
        /// <summary>
        /// Frontend API: Update User Contact Number
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/profile/account-settings/contact/list")]
        [ProducesResponseType(typeof(ListContact_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult listContact([FromQuery]ListContact_Request request) //Tony Liew 20190415 RDT-198
        {
            var response = new ListContact_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            string[] header = { Request.Headers["sdkVersion"], Request.Headers["platform"], Request.Headers["deviceUuid"] };

            #region Validation

            if (customer == null)
            {
                //logFE.Info("ReturnCode : " + response.ReturnCode + ", ReturnMessage : " + response.ReturnMessage);
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
            #endregion

            #region Execution
            try
            {
                response = _profileApiService.ListContact(request, customer.Id);
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

        #region Update Password
        /// <summary>
        /// Frontend API: Update Password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/profile/account-settings/password")]
        [ProducesResponseType(typeof(UpdatePassword_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult updatePassword([FromBody]UpdatePassword_Request request) //Tony Liew 20190308 RDT-69
        {
            var response = new UpdatePassword_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            string[] header = { Request.Headers["sdkVersion"], Request.Headers["platform"], Request.Headers["deviceUuid"] };

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
                else
                {
                    if (string.IsNullOrEmpty(request.password.newPassword))
                    {
                        log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                        ErrorResultObject.meta.code = (int)ProfileController_ChangePassword.enterNewPassword;
                        ErrorResultObject.meta.message = ProfileController_ChangePassword.enterNewPassword.ToDescription<ProfileController_ChangePassword>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (string.IsNullOrEmpty(request.password.currentPassword))
                    {
                        log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                        ErrorResultObject.meta.code = (int)ProfileController_ChangePassword.enterOldPassword;
                        ErrorResultObject.meta.message = ProfileController_ChangePassword.enterOldPassword.ToDescription<ProfileController_ChangePassword>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                    else if (request.password.newPassword == request.password.currentPassword || request.password.currentPassword == request.password.newPassword)
                    {
                        log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                        ErrorResultObject.meta.code = (int)ProfileController_ChangePassword.oldNewPasswordSame;
                        ErrorResultObject.meta.message = ProfileController_ChangePassword.oldNewPasswordSame.ToDescription<ProfileController_ChangePassword>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }


            var decryptedNewPassword = _cryptographyService.TripleDesDecryptor(request.password.newPassword);
            var decryptedCurrentPassword = _cryptographyService.TripleDesDecryptor(request.password.currentPassword);

            #region Validation
            string signatureString = decryptedCurrentPassword + "|" + decryptedNewPassword + "|" + header[2];
            if (!_cryptographyService.VerifyRSADigitalSignatureSHA1(signatureString, request.signature))
            {
                response.meta.code = (int)RES_GlobalEnum.invalidSignature;
                response.meta.message = RES_GlobalEnum.invalidSignature.ToValue<RES_GlobalEnum>();

                return Ok(response);
            }
            #endregion

            #region Execution
            try
            {
                response = _profileApiService.changePassword(decryptedNewPassword, decryptedCurrentPassword, customer.Id, customerEmail, header[2]);
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

        //WKK 20190403 RDT-183 [API] P.Account settings - Update profile picture
        #region Update profile picture
        /// <summary>
        /// Mobile Frontend API: Update profile picture
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/profile/image")]
        [ProducesResponseType(typeof(updatePicture_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult profileImage([FromBody] UpdatePicture_Request request)
        {
            var response = new updatePicture_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

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
            else
            if (request.media == null)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            #region Execution
            try
            {
                string file = RES_FileEnum.avatar.ToValue<RES_FileEnum>();
                string imagePath = _baseHelper.returnURL(request.media.content, customerId, file, request.media.type);

                String imageServer = ConfigurationManager.AppSettings["S3ImageUrl"];

                //bool success = _profileApiService.UpdateProfilePicture(customerId, imageServer + imagePath);
                bool success = _profileApiService.UpdateProfilePicture(customerId, imagePath);
                if (success)
                {
                    response.data.media.type = request.media.type;
                    response.data.media.content = imageServer + imagePath;
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
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        //WKK 20190408 RDT-188 [API] P.Account settings - update identity number
        #region update identity number
        /// <summary>
        /// Mobile Frontend API: User update his/her identity number
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/profile/account-settings/identitynumber")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult updateIdentityNumber([FromBody] UpdateIC_Request request)
        {
            var response = new ApiResponse();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

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
            else
            if (request.identity == null)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            if (string.IsNullOrEmpty(request.identity.type) || 
                !(request.identity.type == UserIdentityType.NewIC.ToValue<UserIdentityType>() ||
                request.identity.type == UserIdentityType.OldIC.ToValue<UserIdentityType>() ||
                request.identity.type == UserIdentityType.ArmedForceIC.ToValue<UserIdentityType>() ||
                request.identity.type == UserIdentityType.Passport.ToValue<UserIdentityType>()))
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidIdentityType;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidIdentityType.ToDescription<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            if (string.IsNullOrEmpty(request.identity.number))
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidIdentityNumber;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidIdentityNumber.ToDescription<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            // check duplicate IC number
            var userProfile = _profileApiService.GetUserByIdNumber(request.identity.number);
            if (userProfile != null)
            {
                if (userProfile.CustomerId != customerId)
                {
                    ErrorResultObject.meta.code = (int)ProfileController_UpdateIdentityNumber.DuplicateIdentityNumber;
                    ErrorResultObject.meta.message = ProfileController_UpdateIdentityNumber.DuplicateIdentityNumber.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }

            #region Execution
            try
            {
                bool success = _profileApiService.UpdateIdentityNumber(customerId, request.identity.type, request.identity.number);
                if (!success)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                    ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = ex.HResult;
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        #endregion

        #region module : details

        // WKK 20190325 RDT-164 [API] Profile - Detail
        #region Get profile details
        /// <summary>
        /// Frontend API: To retrieve user properties he/she have with property details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/profile/details")]
        [ProducesResponseType(typeof(ProfileDetails_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult infoDetails()
        {
            var response = new ProfileDetails_ResponseResult();
            response.meta.code = (int)RES_GlobalEnum.success;
            response.meta.message = RES_GlobalEnum.success.ToDescription<RES_GlobalEnum>();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            if (customer == null)
            {
                response.meta.code = (int)RES_GlobalEnum.invalidUser;
                response.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
            }
            else
            {
                try
                {
                    response.data = new profileDetailsDto() { profileDto = _profileApiService.GetProfileDto(customer.Id) };
                    response.data.profileDto.email = customerEmail;
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    response.meta.code = (int)RES_GlobalEnum.unhandledException;
                    response.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>();
                }
            }

            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
        }

        #endregion

        #endregion

        #region module : properties

        #region Get Property Unit Listing
        /// <summary>
        /// Frontend API: Get Properties Unit Listing
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/api/profile/properties")]
        [ProducesResponseType(typeof(PropertyUnit_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult listProperties([FromQuery]PropertyUnit_Request request) //Tony Liew 20190315 RDT-65
        {
            var response = new PropertyUnit_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            var customer = _customerService.GetCustomerByEmail(customerEmail);

            string[] header = { Request.Headers["sdkVersion"], Request.Headers["platform"], Request.Headers["deviceUuid"] };

            #region Validation

            if (customer == null)
            {
                log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            else // this block for debuging/troubleshooting for mobile team
            {
                if (request == null)
                {
                    log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                    ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
            }
            #endregion

            #region Execution
            try
            {
                response = _profileApiService.listProperty(request, customer.Id);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>();

                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        //WKK 20190327 RDT-167 [API] Property Unit - Set default property
        #region Set default property
        /// <summary>
        /// Mobile Frontend API: Set default property
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/profile/properties/default")]
        [ProducesResponseType(typeof(UpdateDefaultProp_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult UpdateDefaultProp([FromBody]UpdateDefaultProp_Request request)
        {
            var response = new UpdateDefaultProp_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            if (customerId == 0)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            if (request == null)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            else
            if (request.propId == 0)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            #region Execution
            try
            {
                // check exist property id
                var property = _profileApiService.GetMntOrgUnitPropertyById(request.propId);
                if (property == null)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidPropertyId;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidPropertyId.ToDescription<RES_GlobalEnum>();
                    log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }

                bool success = _profileApiService.UpdateDefaultProp(customerId, request.propId);

                if (!success)
                {
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                    ErrorResultObject.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else
                {
                    response.data.defaultPropId = request.propId;
                    var userAcct = _profileApiService.GetUserAccountByPropId(customerId, request.propId);
                    response.data.accPropType = userAcct.AccountType;
                    response.data.defaultOrgId = userAcct.UserOrgId;
                }
            }
            catch (Exception ex)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        //WKK 20190410 RDT-168 [API] Property Unit - Bind new property
        #region Bind new property
        /// <summary>
        /// Mobile Frontend API: Bind property with activation code and identity number
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/profile/properties/binding")]
        [ProducesResponseType(typeof(PropertyUnit_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult propertyBinding([FromBody] PropertyBinding_Request request)
        {
            var response = new PropertyUnit_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();

            var customerEmail = Request.Headers["email"].ToString();
            int customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            // check customer id
            if (customerId == 0)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            if (request == null)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.noRequest;
                ErrorResultObject.meta.message = RES_GlobalEnum.noRequest.ToDescription<RES_GlobalEnum>();
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            
            // check activation code
            if (string.IsNullOrEmpty(request.activationCode))
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidActivationCode;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidActivationCode.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            // check identity number
            if (string.IsNullOrEmpty(request.identityNumber))
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidIdentityNumber;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidIdentityNumber.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            // check identity number exist
            var userProfile = _profileApiService.GetUserByIdNumber(request.identityNumber);
            if (userProfile == null)
            {
                ErrorResultObject.meta.code = (int)ProfileController_UpdateIdentityNumber.IdentityNumberNotExist;
                ErrorResultObject.meta.message = ProfileController_UpdateIdentityNumber.IdentityNumberNotExist.ToDescription<ProfileController_UpdateIdentityNumber>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }

            // check activation code
            var userOrg = _profileApiService.CheckActivationCode(request.activationCode);
            if (userOrg == null)
            {
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidActivationCode;
                ErrorResultObject.meta.message = RES_GlobalEnum.invalidActivationCode.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            else
            {
                // check customer id
                if (userOrg.CustomerId != null)
                {
                    if (userOrg.CustomerId != customerId)
                    {
                        ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidCustomerId;
                        ErrorResultObject.meta.message = RES_GlobalEnum.invalidCustomerId.ToDescription<RES_GlobalEnum>();
                        log.Info("code : " + ErrorResultObject.meta.code + ", message : " + ErrorResultObject.meta.message);
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }

            #region Execution
            try
            {
                bool success = _profileApiService.propertyBinding(customerId, request.activationCode, request.identityNumber, request.defaultProperty);

                if (success)
                {
                    var propertyUnit_Request = new PropertyUnit_Request();
                    response = _profileApiService.listProperty(propertyUnit_Request, customerId);
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
                ErrorResultObject.meta.message = ex.Message;
                log.Error(ex.Message);
                return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
            }
            #endregion

            #region returnJson
            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
            #endregion
        }

        #endregion

        #endregion

        #region module : languages

        #region Update Language
        /// <summary>
        /// Frontend API: Update user default language
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/api/profile/languages/update")]
        [ProducesResponseType(typeof(UpdateLanguage_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult updateLanguage([FromBody]UpdateLanguage_Request request)
        {
            var responseResult = new UpdateLanguage_ResponseResult();
            var ErrorResultObject = new ErrorsResultObject();
            int LanguageId;

            var CustomerEmail = Request.Headers["email"].ToString();
            int CustomerId = _customerService.GetCustomerIdByEmail(CustomerEmail);

            #region Validation
            try
            {
                if (CustomerId < 0)
                {
                    log.Info("code : " + responseResult.meta.code + ", message : " + responseResult.meta.message);
                    ErrorResultObject.meta.code = (int)RES_GlobalEnum.invalidUser;
                    ErrorResultObject.meta.message = RES_GlobalEnum.invalidUser.ToValue<RES_GlobalEnum>();
                    return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                }
                else
                {
                    if (!_profileServices.ValidateLanguageCode(request.languageCode, out LanguageId))
                    {
                        ErrorResultObject.meta.code = (int)ProfileController_UpdateLanguage.InvalidLanguageCode;
                        ErrorResultObject.meta.message = ProfileController_UpdateLanguage.InvalidLanguageCode.ToDescription<ProfileController_UpdateLanguage>();
                        return ErrorResult(HttpStatusCode.OK, ErrorResultObject);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.BadRequest, ErrorResultObject);
            }
            #endregion

            #region Execution
            try
            {
                if (LanguageId > 0) _profileServices.UpdateCustomerLanguage(CustomerId, LanguageId);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
                ErrorResultObject.meta.message = ex.Message;
                return ErrorResult(HttpStatusCode.BadRequest, ErrorResultObject);
            }
            #endregion

            #region returnJson
            if (responseResult.meta.code == 0) responseResult.meta.message = responseResult.meta.message;
            var json = _jsonFieldsSerializer.Serialize(responseResult, "");
            return new RawJsonActionResult(json);
            #endregion
        }
        #endregion

        // WKK 20190327 RDT-174        [API] P.Language - List language
        #region Get language list with user default language
        /// <summary>
        /// Frontend API: Get language list with user default language.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("/api/profile/languages/list")]
        [ProducesResponseType(typeof(ListLanguage_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult listLanguage()
        {
            var response = new ListLanguage_ResponseResult();
            response.meta.code = (int)RES_GlobalEnum.success;
            response.meta.message = RES_GlobalEnum.success.ToDescription<RES_GlobalEnum>();

            var customerEmail = Request.Headers["email"].ToString();
            var customerId = _customerService.GetCustomerIdByEmail(customerEmail);

            if (customerId == 0)
            {
                response.meta.code = (int)RES_GlobalEnum.invalidUser;
                response.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                log.Info("code : " + response.meta.code + ", message : " + response.meta.message);
            }
            else
            {
                try
                {
                    var languageList = _profileApiService.GetLanguageList();
                    var userProfile = _profileApiService.GetUserProfile(customerId);

                    response.data.languageDto = languageList.Select(p => new languageDto { name = p.Name, code = p.UniqueSeoCode }).ToList();

                    if (userProfile != null && languageList.Count > 0)
                        response.data.defaultLanguage = languageList.Find(p => p.Id == userProfile.Locale_Id).UniqueSeoCode;

                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    response.meta.code = (int)RES_GlobalEnum.unhandledException;
                    response.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>();
                }
            }

            var json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
        }

        #endregion

        #endregion

        #region module : help & support

        #region Get FAQ Uri
        ///// <summary>
        ///// Frontend API: Get FAQ HTML page Uri
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("/api/profile/faq")]
        //[ProducesResponseType(typeof(GetFAQUri_ResponseResult), (int)HttpStatusCode.OK)]
        //[GetRequestsErrorInterceptorActionFilter]
        //public IActionResult GetFAQUri()
        //{
        //    var responseResult = new GetFAQUri_ResponseResult();
        //    var ErrorResultObject = new ErrorsResultObject();

        //    #region Validation
        //    try
        //    {
        //        //if (responseResult.meta.code > 0)
        //        //{
        //        //    ErrorResultObject.ReturnCode = responseResult.meta.code;
        //        //    ErrorResultObject.ReturnMessage = responseResult.meta.message;
        //        //    return ErrorResult(HttpStatusCode.BadRequest, ErrorResultObject);

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        ErrorResultObject.meta.code    = (int)RES_GlobalEnum.unhandledException;
        //        ErrorResultObject.meta.message = ex.Message;
        //        return ErrorResult(HttpStatusCode.BadRequest, ErrorResultObject);
        //    }
        //    #endregion

        //    #region Execution
        //    try
        //    {
        //        Faq dto = new Faq();
        //        string domain = _baseHelper.getSettingValueByKey("RES_DomainAddress", "https://res.ggit2u.pw/");
        //        dto.uri = string.Concat(domain, "faq");
        //        responseResult.data = dto;

        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        ErrorResultObject.meta.code    = (int)RES_GlobalEnum.unhandledException;
        //        ErrorResultObject.meta.message = ex.Message;
        //        return ErrorResult(HttpStatusCode.BadRequest, ErrorResultObject);
        //    }
        //    #endregion

        //    #region returnJson
        //    if (responseResult.meta.code == 0) responseResult.meta.message = responseResult.meta.message;
        //    var json = _jsonFieldsSerializer.Serialize(responseResult, "");
        //    return new RawJsonActionResult(json);
        //    #endregion
        //}
        #endregion

        #region Get Application Rules
        ///// <summary>
        ///// Frontend API: Get Terms and Policies HTML page Uri
        ///// </summary>
        ///// <param name="param"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("/api/profile/app-rules")]
        //[ProducesResponseType(typeof(appRules_ResponseResult), (int)HttpStatusCode.OK)]
        //[GetRequestsErrorInterceptorActionFilter]
        //public IActionResult appRules()
        //{
        //    var responseResult = new appRules_ResponseResult();
        //    var ErrorResultObject = new ErrorsResultObject();

        //    #region Validation
        //    try
        //    {
        //        //if (responseResult.meta.code > 0)
        //        //{
        //        //    ErrorResultObject.ReturnCode = responseResult.meta.code;
        //        //    ErrorResultObject.ReturnMessage = responseResult.meta.message;
        //        //    return ErrorResult(HttpStatusCode.BadRequest, ErrorResultObject);

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
        //        ErrorResultObject.meta.message = ex.Message;
        //        return ErrorResult(HttpStatusCode.BadRequest, ErrorResultObject);
        //    }
        //    #endregion

        //    #region Execution
        //    try
        //    {
        //        appRules dto = new appRules();
        //        string domain = _baseHelper.getSettingValueByKey("RES_DomainAddress", "https://res.ggit2u.pw/");
        //        dto.termsUri = string.Concat(domain, "termsandconditions");
        //        dto.policyUri = string.Concat(domain, "privacypolicy");
        //        responseResult.data = dto;

        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message, ex);
        //        ErrorResultObject.meta.code = (int)RES_GlobalEnum.unhandledException;
        //        ErrorResultObject.meta.message = ex.Message;
        //        return ErrorResult(HttpStatusCode.BadRequest, ErrorResultObject);
        //    }
        //    #endregion

        //    #region returnJson
        //    if (responseResult.meta.code == 0) responseResult.meta.message = responseResult.meta.message;
        //    var json = _jsonFieldsSerializer.Serialize(responseResult, "");
        //    return new RawJsonActionResult(json);
        //    #endregion
        //}
        #endregion

        #endregion

    }
}
