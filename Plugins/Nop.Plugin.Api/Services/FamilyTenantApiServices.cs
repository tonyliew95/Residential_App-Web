using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.User;
using Nop.Core.Domain.Residential.Organization;
using Nop.Core.Enumeration;
using Nop.Plugin.Api.Enumeration;
using Nop.Plugin.Api.Models.FamilyTenant.DTOs;
using Nop.Plugin.Api.Models.FamilyTenant.Request;
using Nop.Plugin.Api.Models.FamilyTenant.ResponseResult;
using Nop.Plugin.Api.Models.Media;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services;
using Nop.Services.Customers;
using Nop.Services.Residential.FamilyTenant;
using Nop.Services.Residential.Helpers.BaseHelper;
using Nop.Services.Residential.Helpers.FormatingHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Services
{
    /// <summary>
    /// Family/Tenant Api Service Class
    /// </summary>
    public class FamilyTenantApiServices : IFamilyTenantApiServices
    {
        private readonly IFamilyTenantServices _familyTenantServices;
        private readonly IFormatingHelper _formatingHelper;
        private readonly IRepository<Mnt_UserAccount> _mntUserAccountRepository;
        private readonly IRepository<Mnt_UserOrganization> _mntUserOrganizationRepository;
        private readonly ICustomerService _customerService;
        private readonly IRepository<Mnt_UserProperty_Mapping> _mntUserPropertyMappingRepository;
        private readonly IBaseHelper _baseHelper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="familyTenantServices"></param>
        /// <param name="formatingHelper"></param>
        /// <param name="mntUserAccountRepository"></param>
        /// <param name="mntUserOrganizationRepository"></param>
        /// <param name="customerService"></param>
        /// <param name="mntUserPropertyMappingRepository"></param>
        /// <param name="baseHelper"></param>
        public FamilyTenantApiServices
        (
             IFamilyTenantServices familyTenantServices
            , IFormatingHelper formatingHelper
            , IRepository<Mnt_UserAccount> mntUserAccountRepository
            , IRepository<Mnt_UserOrganization> mntUserOrganizationRepository
            , ICustomerService customerService
            , IRepository<Mnt_UserProperty_Mapping> mntUserPropertyMappingRepository
            , IBaseHelper baseHelper
        )
        {
            this._familyTenantServices = familyTenantServices;
            this._formatingHelper = formatingHelper;
            this._mntUserAccountRepository = mntUserAccountRepository;
            this._mntUserOrganizationRepository = mntUserOrganizationRepository;
            this._customerService = customerService;
            this._mntUserPropertyMappingRepository = mntUserPropertyMappingRepository;
            this._baseHelper = baseHelper;
        }

        #region Get Family/Tenant List
        /// <summary>
        /// Get Family/Tenant List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        public FamilyTenantListing_ResponseResult GetFamilyTenantList(FamilyTenantListing_Request request)//Tony Liew 20190403 RDT-175 
        {
            var responseResult = new FamilyTenantListing_ResponseResult();

            var query = _familyTenantServices.getFamilyTenantList(request.propId, request.orgId).Where(p => p.type == request.type);

            if (request.pageNum <= 0 || request.pageSize <= 0)
            {
                var resultList = query.ToList().Select(q => new FamilyTenantListingDto()
                {
                    orgId = q.orgId,
                    accId = q.accId,
                    name = q.name,
                    type = q.type,
                    accType = q.accType,
                    msisdn = q.msisdn,
                    period = _formatingHelper.GetPeriodDateTime(q.periodStart, q.periodEnd),
                    createdDatetime = _formatingHelper.ToUnixTime(q.createdDatetime),
                    emergency = q.emergency,
                    media = q.media.ToList().Select(p => new mediaDto()
                    {
                        content = string.Concat(Config.S3ImageUrl, p.content),
                        type = p.type
                    }).ToList()
                }).ToList();

                responseResult.data.familyLists = resultList;
                responseResult.pagination.pageTotal = resultList.Count;
            }
            else
            {
                var pgList = new PagedList<Mnt_FamilyTenantCustom>(query, request.pageNum - 1, request.pageSize);

                var pg_Query = pgList.Select(q => new FamilyTenantListingDto()
                {
                    orgId = q.orgId,
                    propId = q.propId,
                    accId = q.accId,
                    type = q.type,
                    name = q.name,
                    accType = q.accType,
                    countryCode = q.countryCode,
                    msisdn = q.msisdn,
                    period = _formatingHelper.GetPeriodDateTime(q.periodStart, q.periodEnd),
                    createdDatetime = _formatingHelper.ToUnixTime(q.createdDatetime),
                    emergency = q.emergency,
                    info = q.info,
                    ocpyStart = _formatingHelper.getDateFormat(q.periodStart),
                    ocpyEnd = _formatingHelper.getDateFormat(q.periodEnd),
                    reminder = _formatingHelper.getDateFormat(q.reminder),
                    email = q.email,
                    media = q.media.ToList().Select(p => new mediaDto()
                    {
                        content = string.Concat(Config.S3ImageUrl, p.content),
                        type = p.type
                    }).ToList()
                }).ToList();

                responseResult.data.familyLists = pg_Query;
                responseResult.pagination.pageNum = request.pageNum;
                responseResult.pagination.pageSize = request.pageSize;
                responseResult.pagination.pageTotal = pgList.TotalPages;
                responseResult.pagination.totalRecord = pgList.TotalCount;
            }

            return responseResult;
        }

        #endregion

        #region Get Family/Tenant Details
        //Tony Liew 20190418 Commented by Tony Liew \/
        /// <summary>
        /// Get Family/Tenant Details
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns> 
        //public FamilyTenantDetails_ResponseResult GetFamilyTenantDetails(FamilyTenantDetails_Request request)//Tony Liew 20190416 RDT-186 
        //{
        //    var responseResult = new FamilyTenantDetails_ResponseResult();

        //    var query = _familyTenantServices.getFamilyTenantDetails(request.propId, request.orgId , request.accId).Select(q => q).Where(p => p.type == request.type).FirstOrDefault();
        //    if (query == null)
        //    {
        //        responseResult.meta.code = (int)FamilyTenantController_Details.invalidIncidentDetails;
        //        responseResult.meta.message = FamilyTenantController_Details.invalidIncidentDetails.ToDescription<FamilyTenantController_Details>();
        //        return responseResult;
        //    }

        //    var result = new FamilyTenantDetailsDto()
        //    {
        //        orgId = query.orgId,
        //        propId = query.propId,
        //        accId = query.accId,
        //        name = query.name,
        //        email = query.email,
        //        accType = query.accType,
        //        countryCode = query.countryCode,
        //        msisdn = query.msisdn,
        //        info = query.info,
        //        emergency = query.emergency,
        //        ocpyStart = _formatingHelper.getDateFormat(query.periodStart),
        //        ocpyEnd = _formatingHelper.getDateFormat(query.periodEnd),
        //        reminder = _formatingHelper.getDateFormat(query.reminder),
        //        media = query.media.ToList().Select(p => new mediaDto()
        //        {
        //            content = string.Concat(Config.S3ImageUrl, p.content),
        //            type = p.type
        //        }).ToList()
        //    };

        //    responseResult.data.familyTenantDetails = result;

        //    return responseResult;
        //}
        //Tony Liew 20190418 Commented by Tony Liew /\
        #endregion

        #region Add Family/Tenant 
        /// <summary>
        /// Add Family/Tenant 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns> 
        public AddFamilyTenant_ResponseResult InsertFamilyTenant(AddFamilyTenant_Request request , int customerId)//Tony Liew 20190404 RDT-176 
        {
            var dateTime = DateTime.UtcNow;

            var responseResult = new AddFamilyTenant_ResponseResult();

            string file = RES_FileEnum.familyTenant.ToValue<RES_FileEnum>();

            #region Validate Input Dates
            if (request.accType == FamilyTenantController_Global.tenant.ToValue<FamilyTenantController_Global>()
                    || request.accType == FamilyTenantController_Global.subTenant.ToValue<FamilyTenantController_Global>())
            {
                if (_formatingHelper.getDateTimeFormatUTC(request.ocpyEnd, "") < _formatingHelper.getDateTimeFormatUTC(request.ocpyStart, ""))
                {
                    responseResult.meta.code = (int)FamilyTenantController_Insert.invalidOccupyStart;
                    responseResult.meta.message = FamilyTenantController_Insert.invalidOccupyStart.ToDescription<FamilyTenantController_Insert>();
                    return responseResult;
                }
                else if (_formatingHelper.getDateTimeFormatUTC(request.reminder, "") > _formatingHelper.getDateTimeFormatUTC(request.ocpyEnd, "")
                    || _formatingHelper.getDateTimeFormatUTC(request.reminder, "") < _formatingHelper.getDateTimeFormatUTC(request.ocpyStart, ""))
                {
                    responseResult.meta.code = (int)FamilyTenantController_Insert.invalidReminderDate;
                    responseResult.meta.message = FamilyTenantController_Insert.invalidReminderDate.ToDescription<FamilyTenantController_Insert>();
                    return responseResult;
                }
            }
            #endregion

            #region Check whether the current user is in Mnt_UserOrganization
            var query = (from uOrg in _mntUserOrganizationRepository.Table where uOrg.CustomerId == customerId && uOrg.OrganizationId == request.orgId select uOrg);
            if (!(query.Any(q => q.CustomerId == customerId)))
            {
                responseResult.meta.code = (int)FamilyTenantController_Insert.userNotInSystem;
                responseResult.meta.message = FamilyTenantController_Insert.userNotInSystem.ToDescription<FamilyTenantController_Insert>();
                return responseResult;
            }
            #endregion

            #region Get User Organization Id from Mnt_UserPropertyMapping

            var getUserOrganizationByPropertyId = (from prop in _mntUserPropertyMappingRepository.Table
                                                   where (from userOrg in _mntUserOrganizationRepository.Table
                                                          where userOrg.OrganizationId == request.orgId
                                                          select userOrg.Id).Contains(prop.UserOrgId) && prop.OrgUnitPropId == request.propId
                                                   select prop).FirstOrDefault();

            if (getUserOrganizationByPropertyId == null)
            {
                responseResult.meta.code = (int)FamilyTenantController_Insert.propertiesDoesNotMap;
                responseResult.meta.message = FamilyTenantController_Insert.propertiesDoesNotMap.ToDescription<FamilyTenantController_Insert>();
                return responseResult;
            }
            #endregion

            #region Check valid user from email requested by current customer
            var requestCustomerId = _customerService.GetCustomerIdByEmail(request.email); // Get Customer's Id by requested email from current user
            if (requestCustomerId == 0)
            {
                responseResult.meta.code = (int)RES_GlobalEnum.invalidUser;
                responseResult.meta.message = RES_GlobalEnum.invalidUser.ToDescription<RES_GlobalEnum>();
                return responseResult;
            }

            #endregion

            #region Get current user account
            var currentUser = _familyTenantServices.getUserAccount(customerId, getUserOrganizationByPropertyId.UserOrgId);
            if (currentUser.Count() == 0)
            {
                responseResult.meta.code = (int)FamilyTenantController_Insert.userNotInSystem;
                responseResult.meta.message = FamilyTenantController_Insert.userNotInSystem.ToDescription<FamilyTenantController_Insert>();
                return responseResult;
            }
            #endregion

            #region Create User Account

                var createUserAccount = new Mnt_UserAccount()
                {
                    UserOrgId = getUserOrganizationByPropertyId.UserOrgId,
                    CustomerId = requestCustomerId,
                    AccountType = request.type,
                    Occupancy = request.accType,
                    Reid = Guid.NewGuid().ToString(),
                    Status = FamilyTenantController_Global.pending.ToValue<FamilyTenantController_Global>(),
                    CreatedBy = customerId,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedBy = customerId,
                    UpdatedOnUtc = DateTime.UtcNow
                };

                #region Start to insert Family Tenant

                if (request.accType == FamilyTenantController_Global.tenant.ToValue<FamilyTenantController_Global>()
                    || request.accType == FamilyTenantController_Global.subTenant.ToValue<FamilyTenantController_Global>())
                {
                    createUserAccount.AccountType = request.accType;
                    _familyTenantServices.insertUserAccount(createUserAccount);

                    var tenant = new Mnt_FamilyTenant()
                    {
                        UserAccId = createUserAccount.Id,
                        OrganizationId = request.orgId,
                        OrgUnitPropertyId = request.propId,
                        MasterUserId = currentUser.FirstOrDefault().Id,
                        FamilyTenantUserId = createUserAccount.Id,
                        Emergency = request.emergency,
                        Deleted = false,
                        Name = request.name,
                        Email = request.email,
                        CountryCode = request.countryCode,
                        Msisdn = request.msisdn,
                        Info = request.info,
                        OccupyStart = _formatingHelper.getDateTimeFormatUTC(request.ocpyStart, ""),
                        OccupyEnd = _formatingHelper.getDateTimeFormatUTC(request.ocpyEnd, ""),
                        Reminder = _formatingHelper.getDateTimeFormatUTC(request.reminder, ""),
                        CreatedBy = customerId,
                        CreatedOnUtc = dateTime,
                        UpdatedBy = customerId,
                        UpdatedOnUtc = dateTime
                    };

                    foreach (var i in request.media)
                    {
                        tenant.FileType = i.type;
                        tenant.File_URI = _baseHelper.returnURL(i.content, customerId, file, i.type);
                    }

                    _familyTenantServices.insertFamilyMemberOrTenant(tenant);
                }
                else if (request.accType == FamilyTenantController_Global.family.ToValue<FamilyTenantController_Global>()
                    || request.accType == FamilyTenantController_Global.primary.ToValue<FamilyTenantController_Global>())
                {
                    createUserAccount.AccountType = request.accType;
                    _familyTenantServices.insertUserAccount(createUserAccount);

                    var family = new Mnt_FamilyTenant()
                    {
                        UserAccId = createUserAccount.Id,
                        OrganizationId = request.orgId,
                        OrgUnitPropertyId = request.propId,
                        MasterUserId = currentUser.FirstOrDefault().Id,
                        FamilyTenantUserId = createUserAccount.Id,
                        Emergency = request.emergency,
                        Deleted = false,
                        Name = request.name,
                        Email = request.email,
                        CountryCode = request.countryCode,
                        Msisdn = request.msisdn,
                        Info = request.info,
                        CreatedBy = customerId,
                        CreatedOnUtc = dateTime,
                        UpdatedBy = customerId,
                        UpdatedOnUtc = dateTime,
                    };
                    foreach (var i in request.media)
                    {
                        family.FileType = i.type;
                        family.File_URI = _baseHelper.returnURL(i.content, customerId, file, i.type);
                    }
                    _familyTenantServices.insertFamilyMemberOrTenant(family);
                }
                #endregion

                #region Start Binding Properties to the User Account request by user
                var bindingRequest = new Mnt_BindingProperty()
                {
                    UserAccId = createUserAccount.Id,
                    UserPropId = request.propId
                };
                _familyTenantServices.insertBindingProperties(bindingRequest);
                #endregion

            #endregion

            return responseResult;
        }
        #endregion

        #region Edit Family/Tenant Emergency Number
        /// <summary>
        /// Add Family/Tenant 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns> 
        public EditFamilyTenant_ResponseResult UpdateFamilyTenant(EditFamilyTenant_Request request, int customerId)//Tony Liew 20190411 RDT-177 
        {
            var dateTime = DateTime.UtcNow;

            var responseResult = new EditFamilyTenant_ResponseResult();

            var targetMember = _familyTenantServices.getFamilyTenant(request.orgId , request.accId , request.propId).FirstOrDefault(); // Get target Family member
            if (targetMember == null)
            {
                responseResult.meta.code = (int)FamilyTenantController_Updated.userAccountNotInSystem;
                responseResult.meta.message = FamilyTenantController_Updated.userAccountNotInSystem.ToDescription<FamilyTenantController_Updated>();
                return responseResult;
            }

            #region Tenant
            if (request.type == FamilyTenantController_Global.tenant.ToValue<FamilyTenantController_Global>())
            {
                targetMember.CountryCode = request.countryCode;
                targetMember.Msisdn = request.msisdn;
                targetMember.Emergency = false;
                targetMember.OccupyStart = _formatingHelper.getDateTimeFormatUTC(request.ocpyStart, "");
                targetMember.OccupyEnd = _formatingHelper.getDateTimeFormatUTC(request.ocpyEnd, "");
                targetMember.Reminder = _formatingHelper.getDateTimeFormatUTC(request.reminder, "");
                targetMember.Info = request.info;

                _familyTenantServices.updateFamilyTenant(targetMember);
            }
            #endregion

            #region Family
            else if (request.type == FamilyTenantController_Global.family.ToValue<FamilyTenantController_Global>())
            {
                var targetUserEmergencyContact = _familyTenantServices.getUserEmergencyNumber(customerId, targetMember.UserAccId).FirstOrDefault();
                if (targetUserEmergencyContact == null)
                {
                    if (request.emergency)
                    {
                        var requestAddEmergencyContact = new Mnt_UserEmergency()
                        {
                            CustomerId = customerId,
                            EmergencyUserId = targetMember.UserAccId,
                            CountryCode = request.countryCode,
                            Msisdn = request.msisdn,
                            CreatedBy = customerId,
                            CreatedOnUtc = dateTime,
                            UpdatedBy = customerId,
                            UpdatedOnUtc = dateTime
                        };
                        _familyTenantServices.insertUserEmergencyNumber(requestAddEmergencyContact);
                    }
                }
                else
                {
                    targetUserEmergencyContact.CountryCode = request.countryCode;
                    targetUserEmergencyContact.Msisdn = request.msisdn;
                    targetUserEmergencyContact.UpdatedOnUtc = dateTime;
                    targetUserEmergencyContact.UpdatedBy = customerId;
                    _familyTenantServices.updatedUserEmergencyNumber(targetUserEmergencyContact);
                }

                targetMember.Emergency = request.emergency;
                targetMember.CountryCode = request.countryCode;
                targetMember.Msisdn = request.msisdn;
                targetMember.UpdatedOnUtc = dateTime;
                targetMember.Info = request.info;
                _familyTenantServices.updateFamilyTenant(targetMember);
            }
            #endregion

            return responseResult;
        }
        #endregion

        #region Remove Family/Tenant 
        /// <summary>
        /// Add Family/Tenant 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns> 
        public RemoveFamilyTenant_ResponseResult RemoveFamilyTenant(RemoveFamilyTenant_Request request, int customerId)//Tony Liew 20190411 RDT-177 
        {
            var dateTime = DateTime.UtcNow;

            var responseResult = new RemoveFamilyTenant_ResponseResult();

            var targetMember = _familyTenantServices.getUserAccount(request.accId).FirstOrDefault();
            if (targetMember == null)
            {
                responseResult.meta.code = (int)FamilyTenantController_Updated.userAccountNotInSystem;
                responseResult.meta.message = FamilyTenantController_Updated.userAccountNotInSystem.ToDescription<FamilyTenantController_Updated>();
                return responseResult;
            }

            targetMember.Status = FamilyTenantController_Global.terminate.ToValue<FamilyTenantController_Global>();
            targetMember.UpdatedOnUtc = dateTime;
            targetMember.UpdatedBy = customerId;
            _mntUserAccountRepository.Update(targetMember);
          

            return responseResult;
        }
        #endregion

    }
}
