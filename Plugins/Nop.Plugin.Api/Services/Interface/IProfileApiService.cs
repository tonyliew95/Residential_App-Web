using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Residential.User;
using Nop.Core.Domain.Residential.Organization;
using Nop.Plugin.Api.Models.Profile.DTOs;
using Nop.Plugin.Api.Models.Profile.Request;
using Nop.Plugin.Api.Models.Profile.ResponseResults;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Services.Interface
{
    public interface IProfileApiService
    {
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="newInputPassword"></param>
        /// <param name="oldInputPassword"></param>
        /// <param name="customerId"></param>
        /// <param name="email"></param>
        /// <param name="deviceuuid"></param>
        /// <returns></returns>
        UpdatePassword_ResponseResult changePassword(string newInputPassword, string oldInputPassword, int customerId, string email, string deviceuuid); //Tony Liew 20190308 RDT-69 

        /// <summary>
        ///Get a list of properties
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        PropertyUnit_ResponseResult listProperty(PropertyUnit_Request request, int customerId); //Tony Liew 20190308 RDT-69

        // WKK 20190322 RDT-163 [API] Login - profile dto
        /// <summary>
        /// Get user Profile
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        profileDto GetProfileDto(int customerId);

        // WKK 20190322 RDT-163 [API] Login - profile dto
        /// <summary>
        /// Get account type
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="propId"></param>
        /// <returns></returns>
        Mnt_UserAccount GetUserAccountByPropId(int customerId, int? propId);

        /// <summary>
        /// Get user profile by using the ic number - check for duplicate IC
        /// </summary>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        Mnt_UserProfile GetUserByIdNumber(string idNumber);

        //WKK 20190326 RDT-173 [API] P.Account settings - Change display name and default email id
        /// <summary>
        /// Get user Profile - Change display name and default email id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="name"></param>
        /// <param name="emailId"></param>
        /// <returns></returns>
        bool UpdateDisplayNameEmailId(int customerId, string name, int emailId = 0);

        //WKK 20190327 RDT-61 [API] Account - registration
        /// <summary>
        /// Update user Profile - Add email
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="email"></param>
        /// <returns>user email id</returns>
        int AddEmail(int customerId, string email);

        //WKK 20190327 RDT-167 [API] Property Unit - Set default property
        /// <summary>
        /// Update user Profile - Set default property
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="propId">default property id</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        bool UpdateDefaultProp(int customerId, int propId, int orgId = 0);

        /// <summary>
        /// Get Mnt_OrgUnitProperty property by id
        /// </summary>
        /// <returns></returns>
        Mnt_OrgUnitProperty GetMntOrgUnitPropertyById(int id);

        // WKK 20190327 RDT-174        [API] P.Language - List language
        /// <summary>
        /// Get language List
        /// </summary>
        /// <returns></returns>
        List<Language> GetLanguageList();

        /// <summary>
        /// Get user profile
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Mnt_UserProfile GetUserProfile(int customerId);

        //WKK 20190403 RDT-183 [API] P.Account settings - Update profile picture
        /// <summary>
        /// Update user Profile - Update profile picture
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="imagePath">image full url</param>
        /// <returns></returns>
        bool UpdateProfilePicture(int customerId, string imagePath);

        //WKK 20190408 RDT-188 [API] P.Account settings - update identity number
        /// <summary>
        /// Update user Profile - User update his/her identity number
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="idType"></param>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        bool UpdateIdentityNumber(int customerId, string idType, string idNumber);

        //WKK 20190410 RDT-168 [API] Property Unit - Bind new property
        /// <summary>
        /// Property Unit - Bind new property
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="activationCode"></param>
        /// <param name="idNumber"></param>
        /// <param name="defaultPropert"></param>
        /// <returns></returns>
        bool propertyBinding(int customerId, string activationCode, string idNumber, bool defaultPropert);

        /// <summary>
        /// Check activation code exist
        /// </summary>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        Mnt_UserOrganization CheckActivationCode(string activationCode);

        /// <summary>
        /// Add User Contact Number
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        AddContactNumber_ResponseResult AddUserContactNumber(AddContactNumber_Request request, int customerId, string signature); // Tony Liew 20190412 RDT-67 

        /// <summary>
        /// List User Contact Number
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        ListContact_ResponseResult ListContact(ListContact_Request request, int customerId);  //Tony Liew 20190415 RDT-198

        /// <summary>
        /// Update contact 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        UpdateContact_ResponseResult UpdateUserContactNumber(UpdateContact_Request request, int customerId);//Tony Liew 20190319 RDT-67

        /// <summary>
        /// Delete User Contact Number
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        RemoveContact_ResponseResult RemoveContact(RemoveContact_Request request, int customerId); //Tony Liew 20190415 RDT-200

    }
}
