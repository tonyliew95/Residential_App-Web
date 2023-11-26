using log4net;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.User;
using Nop.Core.Domain.Residential.Mobile;
using Nop.Core.Domain.Residential.Organization;
using Nop.Core.Domain.Residential.Setting;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Enumeration;
using Nop.Plugin.Api.Models.Profile.DTOs;
using Nop.Plugin.Api.Models.Profile.Request;
using Nop.Plugin.Api.Models.Profile.ResponseResults;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Residential.Profile;
using Nop.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Dynamic;

namespace Nop.Plugin.Api.Services
{
    public class ProfileApiService : IProfileApiService
    {
        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerService _customerService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ILocalizationService _localizationService;
        private readonly IProfileServices _profileServices;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Language> _languageRepository;
        private readonly IStoreContext _storeContext;
        private readonly IUtilityService _utilityService;
        private readonly IRepository<Mnt_UserProfile> _userProfileRepository;
        private readonly IRepository<Mnt_UserEmail> _userEmailRepository;
        private readonly IRepository<Mnt_OrgUnitProperty> _mntOrgUnitPropertyRepository;
        private readonly IRepository<Mnt_BindingProperty> _mntBindingPropertyRepository;
        private readonly IRepository<Mnt_UserAccount> _mntUserAccountRepository;
        private readonly IRepository<Adm_StandardCode> _admStandardCodeRepository;
        private readonly IRepository<Mnt_OrgModuleSubscription> _mntOrgModuleSubscriptionRepository;
        private readonly IRepository<Mnt_UserOrganization> _mntUserOrganizationRepository;
        private readonly IRepository<Mnt_UserProperty_Mapping> _mntUserPropertyMappingRepository;
        private readonly IRepository<Mnt_UserMsisdn> _mntUserMsisdnRepository;
        private readonly IRepository<Mnt_UserEmail> _mntUserEmailRepository;
        private ILog log;

        public ProfileApiService(
            CustomerSettings customerSettings
            , ICustomerService customerService
            , ICustomerRegistrationService customerRegistrationService
            , ILocalizationService localizationService
            , IProfileServices profileServices
            , IRepository<Customer> customerRepository
            , IRepository<Language> languageRepository
            , IStoreContext storeContext
            , IUtilityService utilityService
            , IRepository<Mnt_UserProfile> userProfileRepository
            , IRepository<Mnt_UserEmail> userEmailRepository
            , IRepository<Mnt_OrgUnitProperty> mntOrgUnitPropertyRepository
            , IRepository<Mnt_BindingProperty> mntBindingPropertyRepository
            , IRepository<Mnt_UserAccount> mntUserAccountRepository
            , IRepository<Adm_StandardCode> admStandardCodeRepository
            , IRepository<Mnt_OrgModuleSubscription> mntOrgModuleSubscriptionRepository
            , IRepository<Mnt_UserOrganization> mntUserOrganizationRepository
            , IRepository<Mnt_UserProperty_Mapping> mntUserPropertyMappingRepository
            , IRepository<Mnt_UserMsisdn> mntUserMsisdnRepository
            , IRepository<Mnt_UserEmail> mntUserEmailRepository
)
        {
            this._customerRepository = customerRepository;
            this._customerService = customerService;
            this._customerRegistrationService = customerRegistrationService;
            this._customerSettings = customerSettings;
            this._languageRepository = languageRepository;
            this._localizationService = localizationService;
            this._profileServices = profileServices;
            this._storeContext = storeContext;
            this._utilityService = utilityService;
            this._userProfileRepository = userProfileRepository;
            this._userEmailRepository = userEmailRepository;
            this._mntOrgUnitPropertyRepository = mntOrgUnitPropertyRepository;
            this._mntBindingPropertyRepository = mntBindingPropertyRepository;
            this._mntUserAccountRepository = mntUserAccountRepository;
            this._admStandardCodeRepository = admStandardCodeRepository;
            this._mntOrgModuleSubscriptionRepository = mntOrgModuleSubscriptionRepository;
            this._mntUserOrganizationRepository = mntUserOrganizationRepository;
            this.log = LogManager.GetLogger(Startup.repository_API.Name, typeof(ProfileApiService));
            this._mntUserPropertyMappingRepository = mntUserPropertyMappingRepository;
            this._mntUserMsisdnRepository = mntUserMsisdnRepository;
            this._mntUserEmailRepository = mntUserEmailRepository;
        }

        #region Change Password
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="newInputPassword"></param>
        /// <param name="oldInputPassword"></param>
        /// <param name="customerId"></param>
        /// <param name="email"></param>
        /// <param name="deviceuuid"></param>
        /// <returns></returns>
        public UpdatePassword_ResponseResult changePassword(string newInputPassword, string oldInputPassword, int customerId, string email, string deviceuuid) //Tony Liew 20190308 RDT-69
        {
            var responseResult = new UpdatePassword_ResponseResult();


            var changePasswordRequest = new ChangePasswordRequest(email, false, _customerSettings.DefaultPasswordFormat, newInputPassword, oldInputPassword);
            var changePasswordResult = _customerRegistrationService.ChangePassword(changePasswordRequest, deviceuuid);
            if (!changePasswordResult.Success)
            {
                if (changePasswordResult.Errors.FirstOrDefault() == ProfileController_ChangePassword.EmailIsNotProvided.ToDescription<ProfileController_ChangePassword>())
                    responseResult.meta.code = (int)ProfileController_ChangePassword.EmailIsNotProvided;
                else if (changePasswordResult.Errors.FirstOrDefault() == ProfileController_ChangePassword.PasswordIsNotProvided.ToDescription<ProfileController_ChangePassword>())
                    responseResult.meta.code = (int)ProfileController_ChangePassword.PasswordIsNotProvided;
                else if (changePasswordResult.Errors.FirstOrDefault() == ProfileController_ChangePassword.PasswordIsNotProvided.ToDescription<ProfileController_ChangePassword>())
                    responseResult.meta.code = (int)ProfileController_ChangePassword.EmailNotFound;
                else if (changePasswordResult.Errors.FirstOrDefault() == ProfileController_ChangePassword.OldPasswordDoesntMatch.ToDescription<ProfileController_ChangePassword>())
                    responseResult.meta.code = (int)ProfileController_ChangePassword.OldPasswordDoesntMatch;
                else if (changePasswordResult.Errors.FirstOrDefault() == ProfileController_ChangePassword.PasswordMatchesWithPrevious.ToDescription<ProfileController_ChangePassword>())
                    responseResult.meta.code = (int)ProfileController_ChangePassword.PasswordMatchesWithPrevious;

                responseResult.meta.message = changePasswordResult.Errors.FirstOrDefault();

                return responseResult;
            }
            else
            {
                responseResult.meta.code = (int)RES_GlobalEnum.success;
                responseResult.meta.message = RES_GlobalEnum.success.ToDescription<RES_GlobalEnum>();
                responseResult.data.returnAccessToken = _utilityService.GenerateJwtToken(_customerService.GetCustomerById(customerId), deviceuuid);
                responseResult.data.tokenType = "bearer"; // Temporary hardcore
                return responseResult;
            }
        }
        #endregion

        #region Property Unit
        /// <summary>
        /// Get properties listing
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public PropertyUnit_ResponseResult listProperty(PropertyUnit_Request request, int customerId) //Tony Liew 20190308 RDT-69
        {
            var responseResult = new PropertyUnit_ResponseResult();

            if (request.pageNum < 1) request.pageNum = 1;
            if (request.pageSize < 1) request.pageSize = 10;

            // populate propertyDto
            //var query = _profileServices.listProperties(customerId).Select(q => new propertyUnitDto() // 20190408 WKK this class propertyUnitDto duplicated with Nop.Core.Domain.Residential.Custom.Mnt_UserPropertyCustom
            var query = _profileServices.listProperties(customerId).Select(q => new Mnt_UserPropertyCustom()
            {
                orgId = q.orgId,
                orgName = q.orgName,
                orgImage = q.orgImage,
                accPropType = q.accPropType,
                id = q.id,
                block = q.block,
                level = q.level,
                unit = q.unit,
                reId = q.reId,
            })
            .ToList();

            responseResult.data.propertyDto = query;

            // populate accPropType and defaultPropId
            var user = _userProfileRepository.Table.Where(u => u.CustomerId == customerId).FirstOrDefault();
            if (user != null)
            {
                responseResult.data.defaultPropId = user.UserProp_Id;
                responseResult.data.defaultOrgId = responseResult.data.propertyDto.Where(p => p.id == responseResult.data.defaultPropId).FirstOrDefault().orgId;
                responseResult.data.accPropType = responseResult.data.propertyDto.Where(p => p.id == responseResult.data.defaultPropId).FirstOrDefault().accPropType;
            }
            else
                responseResult.data.defaultPropId = null;

            // populate the moduleDto
            foreach (var property in responseResult.data.propertyDto)
            {
                property.moduleDto = _mntOrgModuleSubscriptionRepository.Table
                    .Where(p => p.OrganizationId == property.orgId)
                    .Select(s => new moduleDtoCustom
                    {
                        announcement = s.Announcement,
                        incident = s.Incident,
                        facility = s.Facility,
                        familytenant = s.FamilyTenant,
                        intercom = s.Intercom,
                        visitor = s.Visitor,
                        community = s.Community
                    })
                    .FirstOrDefault();
            }

            return responseResult;
        }
        #endregion

        // WKK 20190322 RDT-163 [API] Login - profile dto
        #region User Profile

        /// <summary>
        /// Get user Profile
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public profileDto GetProfileDto(int customerId)
        {
            var profile = new profileDto();
            String imageServer = ConfigurationManager.AppSettings["S3ImageUrl"];

            var user = _userProfileRepository.Table
                .Where(up => up.CustomerId == customerId)
                .FirstOrDefault();

            if (user != null)
            {
                profile.defaultPropId = user.UserProp_Id;
                profile.displayName = user.Name;
                profile.image = imageServer + user.Picture;
                profile.userId = user.CustomerId;
                //Tony Liew 20190418 RDT-163 \/
                if (user.Msisdn_Id != null)
                {
                    var msisdn = _mntUserMsisdnRepository.Table
                        .Where(p => p.Id == user.Msisdn_Id)
                        .FirstOrDefault();

                    if (msisdn != null)
                    {
                        profile.countryCode = msisdn.CountryCode;
                        profile.msisdn = msisdn.Msisdn;
                        profile.msisdnId = msisdn.Id;
                    }
                }
                if (user.Email_Id != null)
                {
                    var email = _mntUserEmailRepository.Table.
                        Where(p => p.Id == user.Email_Id)
                        .FirstOrDefault();
                    if (email != null)
                    {
                        profile.emailId = email.Id;
                    }
                }
                else profile.emailId = null; 
            }
            //Tony Liew 20190418 RDT-163 /\

            profile.properties = _profileServices.GetProperties(customerId);

            foreach (var property in profile.properties)
            {
                property.moduleDto = _mntOrgModuleSubscriptionRepository.Table
                    .Where(p => p.OrganizationId == property.orgId)
                    .Select(s => new moduleDtoCustom {
                        announcement = s.Announcement,
                        incident = s.Incident,
                        facility = s.Facility,
                        familytenant = s.FamilyTenant,
                        intercom = s.Intercom,
                        visitor = s.Visitor,
                        community = s.Community
                    })
                    .FirstOrDefault();
            }

            profile.defaultOrgId = profile.properties.Where(p => p.id == profile.defaultPropId).FirstOrDefault().orgId;
            profile.accPropType = profile.properties.Where(p => p.id == profile.defaultPropId).FirstOrDefault().accPropType;

            return profile;
        }

        /// <summary>
        /// Get account type
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="propId"></param>
        /// <returns></returns>
        public Mnt_UserAccount GetUserAccountByPropId(int customerId, int? propId)
        {
            var UserAccount = (
                from userAcct in _mntUserAccountRepository.Table
                join bindProp in _mntBindingPropertyRepository.Table on userAcct.Id equals bindProp.UserAccId
                    where bindProp.UserPropId == propId
                        && userAcct.CustomerId == customerId
                    select userAcct)
                    .FirstOrDefault();

            if (UserAccount != null)
                return UserAccount;
            else
                return null;
        }

        #endregion

        //WKK 20190326 RDT-173 [API] P.Account settings - Change display name
        //WKK 20190327 RDT-61 [API] Account - registration
        #region User Profile - Change display name and default email id

        /// <summary>
        /// Update user Profile - Change display name and default email id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="name">display name</param>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public bool UpdateDisplayNameEmailId(int customerId, string name, int emailId = 0)
        {
            try
            {
                var user = _userProfileRepository.Table.Where(u => u.CustomerId == customerId).FirstOrDefault();
                if (user == null)
                {
                    var newUser = new Mnt_UserProfile();
                    newUser.CustomerId = customerId;
                    newUser.Name = name;
                    newUser.CreatedOnUtc = DateTime.UtcNow;
                    newUser.UpdatedOnUtc = DateTime.UtcNow;

                    if (emailId > 0) newUser.Email_Id = emailId;

                    _userProfileRepository.Insert(newUser);
                }
                else
                {
                    user.Name = name;
                    if (emailId > 0) user.Email_Id = emailId;

                    _userProfileRepository.Update(user);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                return false;
            }

            return true;
        }

        #endregion

        //WKK 20190327 RDT-61 [API] Account - registration
        #region User Profile - Add email

        /// <summary>
        /// Update user Profile - Add email
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="email"></param>
        /// <returns>user email id</returns>
        public int AddEmail(int customerId, string email)
        {
            try
            {
                var userEmail = _userEmailRepository.Table.Where(u => u.CustomerId == customerId).FirstOrDefault();
                if (userEmail == null)
                {
                    var newEmail = new Mnt_UserEmail();
                    newEmail.CustomerId = customerId;
                    newEmail.Email = email;

                    _userEmailRepository.Insert(newEmail);

                    return newEmail.Id;
                }
                else
                {
                    userEmail.Email = email;
                    _userEmailRepository.Update(userEmail);
                }

                return userEmail.Id;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                return 0;
            }
        }

        #endregion

        //WKK 20190327 RDT-167 [API] Property Unit - Set default property
        #region User Profile - Set default property

        /// <summary>
        /// Update user Profile - Set default property
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="propId">default property id</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool UpdateDefaultProp(int customerId, int propId, int orgId = 0)
        {
            try
            {
                var user = _userProfileRepository.Table.Where(u => u.CustomerId == customerId).FirstOrDefault();
                if (user == null)
                {
                    var newUser = new Mnt_UserProfile();
                    newUser.CustomerId = customerId;
                    newUser.UserProp_Id = propId;
                    newUser.CreatedOnUtc = DateTime.UtcNow;
                    newUser.UpdatedOnUtc = DateTime.UtcNow;

                    _userProfileRepository.Insert(newUser);
                }
                else
                {
                    user.UserProp_Id = propId;

                    _userProfileRepository.Update(user);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Get Mnt_OrgUnitProperty property by id
        /// </summary>
        /// <returns></returns>
        public Mnt_OrgUnitProperty GetMntOrgUnitPropertyById(int id)
        {
            return _mntOrgUnitPropertyRepository.Table.Where(p => p.Id == id).FirstOrDefault();
        }

        #endregion

        // WKK 20190327 RDT-174        [API] P.Language - List language
        #region List languag

        /// <summary>
        /// Get language List
        /// </summary>
        /// <returns></returns>
        public List<Language> GetLanguageList()
        {
            return _languageRepository.Table.ToList();
        }

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Mnt_UserProfile GetUserProfile(int customerId)
        {
            return _userProfileRepository.Table.Where(u => u.CustomerId == customerId).FirstOrDefault();
        }

        #endregion

        //WKK 20190403 RDT-183 [API] P.Account settings - Update profile picture
        #region Update profile picture

        /// <summary>
        /// Update user Profile - Update profile picture
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="imagePath">image full url</param>
        /// <returns></returns>
        public bool UpdateProfilePicture(int customerId, string imagePath)
        {
            try
            {
                var user = _userProfileRepository.Table.Where(u => u.CustomerId == customerId).FirstOrDefault();
                if (user == null)
                {
                    var newUser = new Mnt_UserProfile();
                    newUser.CustomerId = customerId;
                    newUser.Picture = imagePath;
                    newUser.CreatedOnUtc = DateTime.UtcNow;
                    newUser.UpdatedOnUtc = DateTime.UtcNow;

                    _userProfileRepository.Insert(newUser);
                }
                else
                {
                    user.Picture = imagePath;

                    _userProfileRepository.Update(user);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                return false;
            }

            return true;
        }

        #endregion

        //WKK 20190408 RDT-188 [API] P.Account settings - update identity number
        #region update identity number

        /// <summary>
        /// Update user Profile - User update his/her identity number
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="idType"></param>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public bool UpdateIdentityNumber(int customerId, string idType, string idNumber)
        {
            try
            {
                var user = _userProfileRepository.Table.Where(u => u.CustomerId == customerId).FirstOrDefault();
                if (user == null)
                {
                    var newUser = new Mnt_UserProfile();
                    newUser.CustomerId = customerId;
                    newUser.Identity_Number = idNumber;
                    newUser.Identity_Type = idType;
                    newUser.CreatedOnUtc = DateTime.UtcNow;
                    newUser.UpdatedOnUtc = DateTime.UtcNow;

                    _userProfileRepository.Insert(newUser);
                }
                else
                {
                    user.Identity_Number = idNumber;
                    user.Identity_Type = idType;

                    _userProfileRepository.Update(user);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Get user profile by using the ic number - check for duplicate IC
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public Mnt_UserProfile GetUserByIdNumber(string idNumber)
        {
            try
            {
                var user = _userProfileRepository.Table.Where(u => u.Identity_Number == idNumber).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);

                return null;
            }
        }

        #endregion

        //WKK 20190410 RDT-168 [API] Property Unit - Bind new property
        #region Bind new property

        /// <summary>
        /// Property Unit - Bind new property
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="activationCode"></param>
        /// <param name="idNumber"></param>
        /// <param name="defaultProperty">Is Default Property ?</param>
        /// <returns></returns>
        public bool propertyBinding(int customerId, string activationCode, string idNumber, bool defaultProperty)
        {
            // find the id number 
            var user = _userProfileRepository.Table.Where(u => u.Identity_Number == idNumber).FirstOrDefault();
            if (user != null && user.CustomerId == customerId)
            {
                // find the activation code
                var userOrg = _mntUserOrganizationRepository.Table.Where(a => a.ActivationCode == activationCode).FirstOrDefault();
                if (userOrg != null)
                {
                    // check the customer id
                    if (userOrg.CustomerId == null || userOrg.CustomerId == user.CustomerId)
                    {
                        // update customer id 
                        if (userOrg.CustomerId == null)
                        {
                            userOrg.CustomerId = user.CustomerId;
                            _mntUserOrganizationRepository.Update(userOrg);
                        }

                        // check already exist user account
                        var userAcct = _mntUserAccountRepository.Table.Where(ua => ua.UserOrgId == userOrg.Id).FirstOrDefault();
                        if (userAcct == null)
                        {
                            // create user account
                            var newUserAcct = new Mnt_UserAccount();

                            newUserAcct.UserOrgId = userOrg.Id;
                            newUserAcct.CustomerId = user.CustomerId;
                            newUserAcct.AccountType = UserAccountType.Owner.ToValue<UserAccountType>();
                            newUserAcct.Reid = userOrg.Reid;
                            newUserAcct.Status = UserAccount_StatusType.Active.ToValue<UserAccount_StatusType>();
                            newUserAcct.CreatedBy = user.CustomerId;
                            newUserAcct.CreatedOnUtc = DateTime.UtcNow;
                            newUserAcct.UpdatedBy = user.CustomerId;
                            newUserAcct.UpdatedOnUtc = DateTime.UtcNow;

                            _mntUserAccountRepository.Insert(newUserAcct);

                            userAcct = newUserAcct;
                        }

                        // find the property id
                        var orgUnitPropId = (from p in _mntUserPropertyMappingRepository.Table where p.UserOrgId == userOrg.Id select p.OrgUnitPropId).FirstOrDefault();
                        if (orgUnitPropId > 0)
                        {
                            // bind property to [Mnt_BindingProperty] with [Mnt_UserAccount] and [Mnt_UserOrganization]
                            var bindProperty = _mntBindingPropertyRepository.Table.Where(p => p.UserAccId == userAcct.Id && p.UserPropId == orgUnitPropId).FirstOrDefault();
                            if (bindProperty == null)
                            {
                                var newBindProperty = new Mnt_BindingProperty()
                                {
                                    UserAccId = userAcct.Id,
                                    UserPropId = orgUnitPropId
                                };

                                _mntBindingPropertyRepository.Insert(newBindProperty);
                            }

                            // sets default user property id
                            //if (user.UserProp_Id == null)
                            //{
                            try
                            {
                                // count properties for this user
                                var count = _profileServices.countProperties(customerId);
                                if (count > 0)
                                {
                                    if (defaultProperty)
                                    {
                                        user.UserProp_Id = orgUnitPropId;
                                        _userProfileRepository.Update(user);
                                    }
                                }
                                else
                                {
                                    user.UserProp_Id = orgUnitPropId;
                                    _userProfileRepository.Update(user);
                                }
                            }
                            catch (Exception ex)
                            {
                                log.Error(ex.Message);
                            }
                            //}

                            return true;
                        }

                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check activation code exist
        /// </summary>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        public Mnt_UserOrganization CheckActivationCode(string activationCode)
        {
            try
            {
                var acct = _mntUserOrganizationRepository.Table.Where(u => u.ActivationCode == activationCode).FirstOrDefault();
                return acct;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return null;
            }
        }

        #endregion

        #region P.Account Setting

        #region Add
        /// <summary>
        /// Add User Contact Number
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public AddContactNumber_ResponseResult AddUserContactNumber(AddContactNumber_Request request , int customerId , string signature) // Tony Liew 20190412 RDT-67 
        {
            var responseResult = new AddContactNumber_ResponseResult();

            var addContactRequest = new Mnt_UserMsisdn()
            {
                CountryCode = request.contactDto.countryCode,
                CustomerId = customerId,
                Msisdn = request.contactDto.msisdn,
                Type = ProfileController_Global.mobile.ToDescription<ProfileController_Global>(),
                UpdatedOnUtc = DateTime.UtcNow,
                IsVerified = true // In phase 1, isVerified is true
            };

            _profileServices.insertCustomerMsisdn(addContactRequest);

            var userProfile = _userProfileRepository.Table
                         .Where(up => up.CustomerId == customerId)
                         .FirstOrDefault();

            if (userProfile == null)
            {
                responseResult.meta.code = (int)ProfileController_Global.userNotInSystem;
                responseResult.meta.message = ProfileController_Global.userNotInSystem.ToDescription<ProfileController_Global>();
                return responseResult;
            }

            else if (!_profileServices.SearchCustomerMsisdn(customerId).Any(q => q.CustomerId == customerId)) // If the program does not found any contact related to the customer
                                                                                                         //, the program will set the contact number as default number into user's profile
            {
                userProfile.Msisdn_Id = addContactRequest.Id;
                _userProfileRepository.Update(userProfile);
            }
            else if(request.contactDto.primary)
            {
                userProfile.Msisdn_Id = addContactRequest.Id;
                _userProfileRepository.Update(userProfile);
            }
            //Phase 2 will generate a sms for verification code
            return responseResult;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update contact Via Email
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public UpdateContact_ResponseResult UpdateUserContactNumber(UpdateContact_Request request, int customerId)  //Tony Liew 20190319 RDT-67 // RDT-199
        {
            var responseResult = new UpdateContact_ResponseResult();
            var userProfile = _userProfileRepository.Table
                       .Where(up => up.CustomerId == customerId)
                       .FirstOrDefault();

            var query = _profileServices.SearchCustomerMsisdn(customerId); 

            if (userProfile == null)
            {
                responseResult.meta.code = (int)ProfileController_Global.userNotInSystem;
                responseResult.meta.message = ProfileController_Global.userNotInSystem.ToDescription<ProfileController_AddContactNumber>();
                return responseResult;
            }
            else
            {
                if (!query.Any(q => q.Msisdn == request.msisdn) && request.contactId == 0) // This indicate the user/customer does not have contact number before, 
                                                                                           //this statement shows when the db does not found the msisdn and the contactId 
                                                                                           //not pass to API, the API assume this is a new request by user

                {
                    var addContactRequest = new Mnt_UserMsisdn()
                    {
                        CountryCode = request.countryCode,
                        CustomerId = customerId,
                        Msisdn = request.msisdn,
                        Type = ProfileController_Global.mobile.ToDescription<ProfileController_Global>(),
                        UpdatedOnUtc = DateTime.UtcNow,
                        IsVerified = true // In phase 1, isVerified is true
                    };
                    _profileServices.insertCustomerMsisdn(addContactRequest);
                }
                else
                {
                    var targetCustomerMsisdn = query.Where(p => p.Id == request.contactId).FirstOrDefault();
                    if (targetCustomerMsisdn == null)
                    {
                        responseResult.meta.code = (int)ProfileController_ChangeContactNumber.NoContactNumberId;
                        responseResult.meta.message = ProfileController_ChangeContactNumber.NoContactNumberId.ToDescription<ProfileController_ChangeContactNumber>();
                        return responseResult;
                    }
                    else if (targetCustomerMsisdn.Msisdn == request.msisdn)
                    {
                        responseResult.meta.code = (int)ProfileController_ChangeContactNumber.SameContactNumber;
                        responseResult.meta.message = ProfileController_ChangeContactNumber.SameContactNumber.ToDescription<ProfileController_ChangeContactNumber>();
                        return responseResult;
                    }
                    else
                    {
                        targetCustomerMsisdn.Msisdn = request.msisdn;
                        _profileServices.updateCustomerMsisdn(targetCustomerMsisdn);
                    }
                }
            }

            userProfile.Msisdn_Id = request.contactId;
            _userProfileRepository.Update(userProfile);
            //_workflowMessageService.S

            return responseResult;
        }

        #endregion

        #region List

        /// <summary>
        /// List User Contact Number
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public ListContact_ResponseResult ListContact(ListContact_Request request, int customerId)  //Tony Liew 20190415 RDT-198
        {
            var responseResult = new ListContact_ResponseResult();

            var userProfile = _userProfileRepository.Table
                      .Where(up => up.CustomerId == customerId)
                      .FirstOrDefault();

            if (userProfile == null)
            {
                responseResult.meta.code = (int)ProfileController_Global.userNotInSystem;
                responseResult.meta.message = ProfileController_Global.userNotInSystem.ToDescription<ProfileController_Global>();
                return responseResult;
            }

            if (request.pageNum <= 0 || request.pageSize <= 0)
            {

                responseResult.data.contactDto = _profileServices.SearchCustomerMsisdn(customerId).
                                                    ToList().Select(p => new ListContactDto()
                                                    {
                                                        id = p.Id,
                                                        countryCode = p.CountryCode,
                                                        msisdn = p.Msisdn,
                                                        primary = p.Id == userProfile.Msisdn_Id ? true : false
                                                    }).ToList();
            }
            else
            {
                var PgList = new PagedList<Mnt_UserMsisdn>(_profileServices.SearchCustomerMsisdn(customerId).OrderBy(p => p.Id), request.pageNum - 1, request.pageSize);

                responseResult.data.contactDto = PgList.ToList().Select(p => new ListContactDto()
                                                   {
                                                       id = p.Id,
                                                       countryCode = p.CountryCode,
                                                       msisdn = p.Msisdn,
                                                       primary = p.Id == userProfile.Msisdn_Id ? true : false
                                                   }).ToList();

                responseResult.pagination.pageNum = PgList.PageIndex + 1;
                responseResult.pagination.pageSize = PgList.PageSize;
                responseResult.pagination.pageTotal = PgList.TotalPages;
                responseResult.pagination.totalRecord = PgList.TotalCount;
            }

            return responseResult;
        }
        #endregion

        #region Delete

        /// <summary>
        /// Delete User Contact Number
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public RemoveContact_ResponseResult RemoveContact(RemoveContact_Request request, int customerId)  //Tony Liew 20190415 RDT-200
        {
            var responseResult = new RemoveContact_ResponseResult();

            var userProfile = _userProfileRepository.Table
                      .Where(up => up.CustomerId == customerId)
                      .FirstOrDefault();

            var query = _profileServices.SearchCustomerMsisdn(customerId); // Get Query 

            if (userProfile == null)
            {
                responseResult.meta.code = (int)ProfileController_Global.userNotInSystem;
                responseResult.meta.message = ProfileController_Global.userNotInSystem.ToDescription<ProfileController_Global>();
                return responseResult;
            }
            else if (request.contactId == userProfile.Msisdn_Id)
            {
               
                userProfile.Msisdn_Id = query.Where(q => q.Id != request.contactId).FirstOrDefault()?.Id; // Get new default msisdn automatically
                _userProfileRepository.Update(userProfile); // Update the latest default msisdn

                var getRequestedMsisdn = query.Where(q => q.Id == request.contactId).FirstOrDefault(); //Get requested Msisdn for delete
                _profileServices.deleteCustomerMsisdn(getRequestedMsisdn); //Delete request msisdn
               
            }
            else
            {
                var getRequestedMsisdn = query.Where(q => q.Id == request.contactId).FirstOrDefault(); //Get requested Msisdn for delete
                _profileServices.deleteCustomerMsisdn(getRequestedMsisdn); //Delete request msisdn
            }
            return responseResult;
        }

         
        #endregion

        #endregion

    }
}