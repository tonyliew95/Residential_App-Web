using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Attributes;
using Nop.Plugin.Api.Enumeration;
using Nop.Plugin.Api.JSON.ActionResults;
using Nop.Plugin.Api.JSON.Serializers;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Account.DTOs;
using Nop.Plugin.Api.Models.Account.Request;
using Nop.Plugin.Api.Models.Account.ResponseResults;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Security;
using System;
using System.Net;
using log4net;
using Nop.Web;

namespace Nop.Plugin.Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Produces("application/json")]
    [EnableCors("AllowAllHeaders")]
    //[ApiAuthorize(Policy = JwtBearerDefaults.AuthenticationScheme, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : BasesApiController
    {
        private ILog log;
        private readonly CustomerSettings _customerSettings;
        private readonly ICryptographyService _cryptographyService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IProfileApiService _profileApiService;
        private readonly IUtilityService _utilityService;

        public AccountController(
            CustomerSettings customerSettings,
            ICryptographyService cryptographyService,
            ICustomerActivityService customerActivityService,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerService customerService,
            ILocalizationService localizationService,
            IProfileApiService profileApiService,
            IUtilityService utilityService,
            IJsonFieldsSerializer jsonFieldsSerializer) : base(jsonFieldsSerializer)
        {
            this._cryptographyService = cryptographyService;
            this._customerActivityService = customerActivityService;
            this._customerRegistrationService = customerRegistrationService;
            this._customerService = customerService;
            this._customerSettings = customerSettings;
            this._localizationService = localizationService;
            this._profileApiService = profileApiService;
            this._utilityService = utilityService;
            this.log = LogManager.GetLogger(Startup.repository_API.Name, typeof(AccountController));
        }

        // WKK 20190306 RDT-63 [API] Account - login
        #region Login (Authenticate)

        /// <summary>
        /// Issue the token for mobile api
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        [HttpPost("/api/account/login")]
        [ProducesResponseType(typeof(Login_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult Login([FromBody]Authenticate_Request request)
        {
            if (_customerSettings.UsernamesEnabled && request.loginRequest.email != null)
            {
                request.loginRequest.email = request.loginRequest.email.Trim();
            }
            string json;
            int errorCode = (int)RES_GlobalEnum.success;
            string errorMessage = RES_GlobalEnum.success.ToDescription<RES_GlobalEnum>();

            var response = new Login_ResponseResult();
            response.meta.code = errorCode;
            response.meta.message = errorMessage;

            // decrypt password
            string decryptedPassword = _cryptographyService.TripleDesDecryptor(request.loginRequest.password);
            if (decryptedPassword == null)
            {
                //decryptedPassword = request.loginRequest.password;
                response.meta.code = (int)RES_GlobalEnum.invalidEmailOrPassword;
                response.meta.message = RES_GlobalEnum.invalidEmailOrPassword.ToDescription<RES_GlobalEnum>();

                json = _jsonFieldsSerializer.Serialize(response, "");
                return new RawJsonActionResult(json);
            }

            string deviceuuid = Request.Headers["deviceUuid"].ToString();

            // check the signature
            string signatureString = request.loginRequest.email + "|" + decryptedPassword + "|" + deviceuuid;
            bool verified = _cryptographyService.VerifyRSADigitalSignatureSHA1(signatureString, request.signature);
            if (!verified)
            {
                response.meta.code = (int)RES_GlobalEnum.invalidSignature;
                response.meta.message = RES_GlobalEnum.invalidSignature.ToDescription<RES_GlobalEnum>();

                json = _jsonFieldsSerializer.Serialize(response, "");
                return new RawJsonActionResult(json);
            }

            var loginResult = _customerRegistrationService.ValidateCustomer(request.loginRequest.email, decryptedPassword , deviceuuid);//Tony Liew 20190308 RDT-69

            switch (loginResult)
            {
                case CustomerLoginResults.Successful:
                    {
                        var customer = _customerSettings.UsernamesEnabled
                            ? _customerService.GetCustomerByUsername(request.loginRequest.email)
                            : _customerService.GetCustomerByEmail(request.loginRequest.email);

                        //activity log
                        _customerActivityService.InsertActivity(customer, "PublicStore.MobileLogin",
                            _localizationService.GetResource("ActivityLog.PublicStore.MobileLogin"), customer);

                        var tokenString = _utilityService.GenerateJwtToken(customer, deviceuuid);

                        response.data = new LoginDto()
                        {
                            accessToken = tokenString,
                            tokenType = "bearer",
                            profileDto = _profileApiService.GetProfileDto(customer.Id),
                            //moduleDto = null,
                        };

                        response.data.profileDto.email = request.loginRequest.email;

                        // return basic user info (without password) and token to store client side
                        json = _jsonFieldsSerializer.Serialize(response, "");
                        return new RawJsonActionResult(json);
                    }
                case CustomerLoginResults.CustomerNotExist:
                    errorCode = (int)AccountController_LoginResults.CustomerNotExist;
                    errorMessage = AccountController_LoginResults.CustomerNotExist.ToDescription<AccountController_LoginResults>();
                    //errorMessage = _localizationService.GetResource("Account.Login.WrongCredentials.CustomerNotExist");
                    break;
                case CustomerLoginResults.Deleted:
                    errorCode = (int)AccountController_LoginResults.Deleted;
                    errorMessage = AccountController_LoginResults.Deleted.ToDescription<AccountController_LoginResults>();
                    //errorMessage = _localizationService.GetResource("Account.Login.WrongCredentials.Deleted");
                    break;
                case CustomerLoginResults.NotActive:
                    errorCode = (int)AccountController_LoginResults.NotActive;
                    errorMessage = AccountController_LoginResults.NotActive.ToDescription<AccountController_LoginResults>();
                    //errorMessage = _localizationService.GetResource("Account.Login.WrongCredentials.NotActive");
                    break;
                case CustomerLoginResults.NotRegistered:
                    errorCode = (int)AccountController_LoginResults.NotActive;
                    errorMessage = AccountController_LoginResults.NotActive.ToDescription<AccountController_LoginResults>();
                    //errorMessage = _localizationService.GetResource("Account.Login.WrongCredentials.NotRegistered");
                    break;
                case CustomerLoginResults.LockedOut:
                    errorCode = (int)AccountController_LoginResults.LockedOut;
                    errorMessage = AccountController_LoginResults.LockedOut.ToDescription<AccountController_LoginResults>();
                    //errorMessage = _localizationService.GetResource("Account.Login.WrongCredentials.LockedOut");
                    break;
                case CustomerLoginResults.WrongPassword:
                default:
                    errorCode = (int)AccountController_LoginResults.WrongPassword;
                    errorMessage = AccountController_LoginResults.WrongPassword.ToDescription<AccountController_LoginResults>();
                    //errorMessage = _localizationService.GetResource("Account.Login.WrongCredentials");
                    break;
            }

            response.meta.code = errorCode;
            response.meta.message = errorMessage;

            json = _jsonFieldsSerializer.Serialize(response, "");
            return new RawJsonActionResult(json);
        }

        /// <summary>
        /// Testing the token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("/api/account/tokendata")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult GetTokenPayload()
        {
            // get from the token - user id
            //var currentUserId = int.Parse(User.Identity.Name);

            string iis = Request.Headers["iss"].ToString();
            string iat = Request.Headers["iat"].ToString();
            string jti = Request.Headers["jti"].ToString();
            string email = Request.Headers["email"].ToString();
            string admin = Request.Headers["admin"].ToString();
            string exp = Request.Headers["exp"].ToString();
            string nbf = Request.Headers["nbf"].ToString();

            var response = new ApiResponse();
            response.meta.code = 1000;
            response.meta.message = "success";
            response.data = new { iis, iat, jti, email, admin, exp, nbf };

            return Ok(response);
        }

        #endregion

        // WKK 20190312 RDT-61 [API] Account - registration
        #region Registration

        /// <summary>
        /// Registration 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/account/register")]
        [ProducesResponseType(typeof(Register_ResponseResult), (int)HttpStatusCode.OK)]
        [GetRequestsErrorInterceptorActionFilter]
        public IActionResult Registration([FromBody]Register_Request request)
        {
            Register_ResponseResult response = new Register_ResponseResult();
            string json;
            response.meta.code = (int)RES_GlobalEnum.success;
            response.meta.message = RES_GlobalEnum.success.ToDescription<RES_GlobalEnum>();

            if (string.IsNullOrEmpty(request.userDto.Email))
            {
                //response.meta.message = _localizationService.GetResource("Account.Register.Errors.EmailIsNotProvided");
                response.meta.code = (int)AccountController_RegistrationResults.emailNotProvided;
                response.meta.message = AccountController_RegistrationResults.emailNotProvided.ToDescription<AccountController_RegistrationResults>();
            }
            else
            if (!CommonHelper.IsValidEmail(request.userDto.Email))
            {
                response.meta.code = (int)AccountController_RegistrationResults.emailNotValid;
                response.meta.message = AccountController_RegistrationResults.emailNotValid.ToDescription<AccountController_RegistrationResults>();
                //response.meta.message = _localizationService.GetResource("Common.WrongEmail");
            }
            else
            if (string.IsNullOrWhiteSpace(request.userDto.Password))
            {
                response.meta.code = (int)AccountController_RegistrationResults.passwordIsNotProvided;
                response.meta.message = AccountController_RegistrationResults.passwordIsNotProvided.ToDescription<AccountController_RegistrationResults>();
                //response.meta.message = _localizationService.GetResource("Account.Register.Errors.PasswordIsNotProvided");
            }
            else
            if (_customerSettings.UsernamesEnabled)
            {
                if (string.IsNullOrEmpty(request.userDto.DisplayName))
                {
                    response.meta.code = (int)AccountController_RegistrationResults.usernameIsNotProvided;
                    response.meta.message = AccountController_RegistrationResults.usernameIsNotProvided.ToDescription<AccountController_RegistrationResults>();
                    //response.meta.message = _localizationService.GetResource("Account.Register.Errors.UsernameIsNotProvided");
                }
            }
            else
            //validate unique user
            if (_customerService.GetCustomerByEmail(request.userDto.Email) != null)
            {
                response.meta.code = (int)AccountController_RegistrationResults.emailRegistered;
                response.meta.message = AccountController_RegistrationResults.emailRegistered.ToDescription<AccountController_RegistrationResults>();
                //response.meta.message = _localizationService.GetResource("Account.Register.Errors.EmailAlreadyExists");
            }
            else
            if (_customerSettings.UsernamesEnabled)
            {
                if (_customerService.GetCustomerByUsername(request.userDto.DisplayName) != null)
                {
                    response.meta.code = (int)AccountController_RegistrationResults.userRegistered;
                    response.meta.message = AccountController_RegistrationResults.userRegistered.ToDescription<AccountController_RegistrationResults>();
                    //response.meta.message = _localizationService.GetResource("Account.Register.Errors.UsernameAlreadyExists");
                }
            }

            // if not valid request throw error
            if (response.meta.code != (int)RES_GlobalEnum.success)
            {
                json = _jsonFieldsSerializer.Serialize(response, "");
                return new RawJsonActionResult(json);
            }

            // decrypt password
            string decryptedPassword = _cryptographyService.TripleDesDecryptor(request.userDto.Password);
            if (decryptedPassword == null)
            {
                //decryptedPassword = request.userDto.Password;
                response.meta.code = (int)RES_GlobalEnum.invalidEmailOrPassword;
                response.meta.message = RES_GlobalEnum.invalidEmailOrPassword.ToDescription<RES_GlobalEnum>();

                json = _jsonFieldsSerializer.Serialize(response, "");
                return new RawJsonActionResult(json);
            }

            string deviceuuid = Request.Headers["deviceUuid"].ToString();

            // check the signature
            string signatureString = request.userDto.Email + "|" + decryptedPassword + "|" + deviceuuid;
            bool verified = _cryptographyService.VerifyRSADigitalSignatureSHA1(signatureString, request.signature);
            if (!verified)
            {
                response.meta.code = (int)RES_GlobalEnum.invalidSignature;
                response.meta.message = RES_GlobalEnum.invalidSignature.ToDescription<RES_GlobalEnum>();

                json = _jsonFieldsSerializer.Serialize(response, "");
                return new RawJsonActionResult(json);
            }

            // create a new customer
            var guid = Guid.NewGuid();
            var customer = new Customer
            {
                CustomerGuid = guid,
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
                Username = guid.ToString().Substring(0, 30),
                Email = request.userDto.Email
            };

            // create a new customer
            _customerService.InsertCustomer(customer);

            //if (!_customerSettings.UsernamesEnabled)
            //    request.userDto.DisplayName = request.userDto.Email;

            var customerRegistrationRequest = 
                new CustomerRegistrationRequest
                (
                    customer,
                    request.userDto.Email,
                    request.userDto.Email,
                    decryptedPassword,
                    PasswordFormat.Hashed, 0
                );

            try
            {
                // update the new customer with registration data
                var result = _customerRegistrationService.RegisterCustomer(customerRegistrationRequest , deviceuuid); //Tony Liew 20190308 RDT-69

                if (result.Success)
                {
                    customer = _customerSettings.UsernamesEnabled
                        ? _customerService.GetCustomerByUsername(request.userDto.Email)
                        : _customerService.GetCustomerByEmail(request.userDto.Email);

                    // save the user email address
                    var emailId = _profileApiService.AddEmail(customer.Id, customer.Email);

                    if (emailId == 0)
                    {
                        response.meta.code = (int)RES_GlobalEnum.unhandledException;
                        response.meta.message = RES_GlobalEnum.unhandledException.ToDescription<RES_GlobalEnum>();
                    }

                    // save the user display name and default email id
                    _profileApiService.UpdateDisplayNameEmailId(customer.Id, request.userDto.DisplayName, emailId);

                    // save phone number
                    //_genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, request.userDto.Mobile);

                    json = _jsonFieldsSerializer.Serialize(response, "");
                    return new RawJsonActionResult(json);
                }
                else
                {
                    response.meta.code = (int)AccountController_RegistrationResults.registerFailed;
                    response.meta.message = AccountController_RegistrationResults.registerFailed.ToDescription<AccountController_RegistrationResults>();
                    response.meta.message = response.meta.message + " : " + result.Errors[0];

                    json = _jsonFieldsSerializer.Serialize(response, "");
                    return new RawJsonActionResult(json);
                }
            }
            catch (Exception ex)
            {
                response.meta.code = (int)RES_GlobalEnum.unhandledException;

                if (ex.InnerException == null)
                    response.meta.message = ex.Message;
                else if (ex.InnerException.InnerException == null)
                    response.meta.message = ex.InnerException.Message.ToString();
                else
                    response.meta.message = ex.InnerException.InnerException.Message.ToString();

                json = _jsonFieldsSerializer.Serialize(response, "");
                return new RawJsonActionResult(json);
            }
        }

        #endregion

        
    }
}