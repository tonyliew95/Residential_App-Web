using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.User;
using Nop.Core.Domain.Residential.Mobile;
using Nop.Core.Domain.Residential.Organization;
using Nop.Services.Customers;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Nop.Core.Enumeration;

namespace Nop.Services.Residential.Profile
{
    public class ProfileServices : IProfileServices
    {
        private readonly ICustomerService _customerService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Language> _languageRepository;
        private readonly IRepository<Mnt_UserMsisdn> _mntCustomerMsisdnRepository;
        private readonly IRepository<Mnt_Organization> _mntOrganizationRepository;
        private readonly IRepository<Mnt_OrgImage> _mntOrgImageRepository;
        private readonly IRepository<Mnt_UserOrganization> _mntUserOrganizationRepository;
        private readonly IRepository<Mnt_UserProfile> _mntUserProfileRepository;
        private readonly IRepository<Mnt_UserProperty> _mntUserPropertyRepository;
        private readonly IRepository<Mnt_Org_Block> _mntOrgBlockRepository;
        private readonly IRepository<Mnt_Org_Level> _mntOrgLevelRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Mnt_OrgUnitProperty> _mntOrgUnitPropertyRepository;
        private readonly IRepository<Mnt_BindingProperty> _mntBindingPropertyRepository;
        private readonly IRepository<Mnt_UserAccount> _mntUserAccountRepository;
        private readonly IRepository<Mnt_UserProperty_Mapping> _mntUserPropertyMappingRepository;

        public ProfileServices
        (
            ICustomerService customerService
            , IEventPublisher eventPublisher
            , IRepository<Language> languageRepository
            , IRepository<Mnt_UserMsisdn> mntCustomerMsisdnRepository
            , IRepository<Mnt_Organization> mntOrganizationRepository
            , IRepository<Mnt_OrgImage> mntOrgImageRepository
            , IRepository<Mnt_UserOrganization> mntUserOrganizationRepository
            , IRepository<Mnt_UserProfile> mntUserProfileRepository
            , IRepository<Mnt_UserProperty> mntUserPropertyRepository
            , IRepository<Mnt_Org_Block> mntOrgBlockRepository
            , IRepository<Mnt_Org_Level> mntOrgLevelRepository
            , IRepository<Customer> customerRepository
            , IRepository<Mnt_OrgUnitProperty> mntOrgUnitPropertyRepository
            , IRepository<Mnt_BindingProperty> mntBindingPropertyRepository
            , IRepository<Mnt_UserAccount> mntUserAccountRepository
            , IRepository<Mnt_UserProperty_Mapping> mntUserPropertyMappingRepository
        )
        {
            this._customerService = customerService;
            this._eventPublisher = eventPublisher;
            this._languageRepository = languageRepository;
            this._mntCustomerMsisdnRepository = mntCustomerMsisdnRepository;
            this._mntOrganizationRepository = mntOrganizationRepository;
            this._mntOrgImageRepository = mntOrgImageRepository;
            this._mntUserOrganizationRepository = mntUserOrganizationRepository;
            this._mntUserProfileRepository = mntUserProfileRepository;
            this._mntUserPropertyRepository = mntUserPropertyRepository;
            this._mntOrgBlockRepository = mntOrgBlockRepository;
            this._mntOrgLevelRepository = mntOrgLevelRepository;
            this._customerRepository = customerRepository;
            this._mntOrgUnitPropertyRepository = mntOrgUnitPropertyRepository;
            this._mntBindingPropertyRepository = mntBindingPropertyRepository;
            this._mntUserAccountRepository = mntUserAccountRepository;
            this._mntUserPropertyMappingRepository = mntUserPropertyMappingRepository;
        }

        #region Get properties listing
        /// <summary>
        /// Get a list of Properties
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public IQueryable<PropertyCustom> listProperties(int customerId, int PageIndex = 1, int PageSize = int.MaxValue) // Tony Liew 20190315 RDT-65 
        {
            String imageServer = ConfigurationManager.AppSettings["S3ImageUrl"];
            string userAcctStatus = UserAccount_StatusType.Active.ToValue<UserAccount_StatusType>();

            var query = (from userAcct in _mntUserAccountRepository.Table
                         join bindProp in _mntBindingPropertyRepository.Table on userAcct.Id equals bindProp.UserAccId
                         join prop in _mntOrgUnitPropertyRepository.Table on bindProp.UserPropId equals prop.Id
                         join org in _mntOrganizationRepository.Table on prop.OrganizationId equals org.Id into joinOrg
                         join block in _mntOrgBlockRepository.Table on prop.BlockId equals block.Id into joinBlock
                         join level in _mntOrgLevelRepository.Table on prop.LevelId equals level.Id into joinLevel
                         join userPropMap in _mntUserPropertyMappingRepository.Table on prop.Id equals userPropMap.OrgUnitPropId
                         join userOrg in _mntUserOrganizationRepository.Table on userPropMap.UserOrgId equals userOrg.Id
                         where userAcct.CustomerId == customerId
                            && userAcct.Status == userAcctStatus
                         from org in joinOrg.DefaultIfEmpty()
                         join orgImage in _mntOrgImageRepository.Table on org.OrgImageId equals orgImage.Id into orgImageJoin
                         from orgImage in orgImageJoin.DefaultIfEmpty()
                         from block in joinBlock.DefaultIfEmpty()
                         from level in joinLevel.DefaultIfEmpty()
                         select new PropertyCustom()
                         {
                             orgId = org.Id,
                             orgName = org.Name,
                             id = prop.Id,
                             block = block.Name,
                             level = level.Name,
                             unit = prop.UnitNumber,
                             reId = userOrg.Reid,
                             updatedOnUtc = userOrg.UpdatedOnUtc,
                             accPropType = userAcct.AccountType,
                             orgImage = imageServer + orgImage.File_URI
                         }).AsQueryable();

            query = query.OrderByDescending(q => q.updatedOnUtc);
            return query;
        }
        #endregion

        //WKK 20190430 RDT-168 [API] Property Unit - Bind new property
        #region Get properties count
        /// <summary>
        /// Get Properties count
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int countProperties(int customerId) 
        {
            string userAcctStatus = UserAccount_StatusType.Active.ToValue<UserAccount_StatusType>();

            var count = (
                from userAcct 
                    in _mntUserAccountRepository.Table
                join bindProp 
                    in _mntBindingPropertyRepository.Table 
                        on userAcct.Id equals bindProp.UserAccId
                where 
                    userAcct.CustomerId == customerId
                    && userAcct.Status == userAcctStatus
                select bindProp
                )
                .Count();

            return count;
        }
        #endregion

        #region Insert Customer Msisdn
        /// <summary>
        /// Insert customer Msisdn
        /// </summary>
        /// <param name="customerMsisdn"></param>
        public void insertCustomerMsisdn(Mnt_UserMsisdn customerMsisdn) //Tony Liew 20190319 RDT-67 
        {
            if (customerMsisdn == null)
                throw new ArgumentNullException(nameof(customerMsisdn));

            _mntCustomerMsisdnRepository.Insert(customerMsisdn);

            //event notification
            _eventPublisher.EntityInserted(customerMsisdn);
        }
        #endregion

        #region Search Customer Msisdn
        /// <summary>
        /// Search Customer Msisdn
        /// </summary>
        /// <param name="customerId"></param>
        public IQueryable<Mnt_UserMsisdn> SearchCustomerMsisdn(int customerId) //Tony Liew 20190412 RDT-68 
        {
            return (from msisdn in _mntCustomerMsisdnRepository.Table
                         where msisdn.CustomerId == customerId && msisdn.IsVerified select msisdn).AsQueryable(); //Get customer/user contact number/msisdn by customer id, this will get more than one records for further uses 
        }
        #endregion

        #region Update Customer Msisdn
        /// <summary>
        /// Update customer Msisdn
        /// </summary>
        /// <param name="customerMsisdn"></param>
        public void updateCustomerMsisdn(Mnt_UserMsisdn customerMsisdn) //Tony Liew 20190319 RDT-67
        {
            if (customerMsisdn == null)
                throw new ArgumentNullException(nameof(customerMsisdn));

            _mntCustomerMsisdnRepository.Update(customerMsisdn);

            //event notification
            _eventPublisher.EntityInserted(customerMsisdn);
        }
        #endregion

        #region Delete Customer Msisdn
        /// <summary>
        /// Delete customer Msisdn
        /// </summary>
        /// <param name="customerMsisdn"></param>
        public void deleteCustomerMsisdn(Mnt_UserMsisdn customerMsisdn) //Tony Liew 20190319 RDT-200
        {
            if (customerMsisdn == null)
                throw new ArgumentNullException(nameof(customerMsisdn));

            _mntCustomerMsisdnRepository.Delete(customerMsisdn);

            //event notification
            _eventPublisher.EntityInserted(customerMsisdn);
        }
        #endregion

        #region Get User Msisdn By User Id
        /// <summary>
        /// Get User Msisdn By User Id
        /// </summary>
        /// <param name="userId"></param>
        public IQueryable<Mnt_UserMsisdn> getUserMsisdnByUserId(int userId) //Tony Liew 20190319 RDT-67 
        {
            var query = (from msisdn in _mntCustomerMsisdnRepository.Table
                         where msisdn.CustomerId == userId
                         select msisdn).AsQueryable();
            return query;
        }
        #endregion

        #region Validate Language Code
        /// <summary>
        /// Validate LanguageCode and if exist return LanguageId
        /// </summary>
        /// <param name="LanguageCode"></param>
        /// <param name="LanguageId"></param>
        /// <returns></returns>
        public bool ValidateLanguageCode(string LanguageCode, out int LanguageId) //JK 20190322 RDT-71
        {
            LanguageId = 0;
            var language = _languageRepository.Table.Where(o => o.UniqueSeoCode.ToLower() == LanguageCode.ToLower()).FirstOrDefault();

            if (language != null)
            {
                LanguageId = language.Id;
                return true;
            }

            return false;
        }
        #endregion

        #region Update Customer Language
        /// <summary>
        /// Update customer language
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="LanguageId"></param>
        public void UpdateCustomerLanguage(int CustomerID, int LanguageId) //JK 20190322 RDT-71
        {
            var CurrentLanguage = (from table in _mntUserProfileRepository.Table
                                   where table.CustomerId == CustomerID
                                   select table).FirstOrDefault();

            if (CurrentLanguage != null)
            {
                CurrentLanguage.Locale_Id = LanguageId;
                _mntUserProfileRepository.Update(CurrentLanguage);
            }
        }
        #endregion

        #region User Property List
        /// <summary>
        /// Get a list of user properties
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<Mnt_UserPropertyCustom> GetProperties(int customerId) // WKK 20190322 RDT-163 [API] Login - profile dto
        {
            String imageServer = ConfigurationManager.AppSettings["S3ImageUrl"];
            string userAcctStatus = UserAccount_StatusType.Active.ToValue<UserAccount_StatusType>();

            var query = (
                from userAcct in _mntUserAccountRepository.Table
                join bindProp in _mntBindingPropertyRepository.Table on userAcct.Id equals bindProp.UserAccId
                join prop in _mntOrgUnitPropertyRepository.Table on bindProp.UserPropId equals prop.Id
                join org in _mntOrganizationRepository.Table on prop.OrganizationId equals org.Id into joinOrg
                join block in _mntOrgBlockRepository.Table on prop.BlockId equals block.Id into joinBlock
                join level in _mntOrgLevelRepository.Table on prop.LevelId equals level.Id into joinLevel
                join userPropMap in _mntUserPropertyMappingRepository.Table on prop.Id equals userPropMap.OrgUnitPropId
                join userOrg in _mntUserOrganizationRepository.Table on userPropMap.UserOrgId equals userOrg.Id
                where userAcct.CustomerId == customerId 
                    && userAcct.Status == userAcctStatus
                from org in joinOrg.DefaultIfEmpty()
                join orgImage in _mntOrgImageRepository.Table on org.OrgImageId equals orgImage.Id into orgImageJoin
                from orgImage in orgImageJoin.DefaultIfEmpty()
                from block in joinBlock.DefaultIfEmpty()
                from level in joinLevel.DefaultIfEmpty()
                select new Mnt_UserPropertyCustom()
                {
                    orgId = prop.OrganizationId,
                    orgName = org.Name,
                    orgImage = imageServer + orgImage.File_URI,
                    accPropType = userAcct.AccountType,
                    id = prop.Id,
                    block = block.Name,
                    level = level.Name,
                    unit = prop.UnitNumber,
                    reId = userOrg.Reid
                })
                .AsQueryable();

            //var query = (from uorg in _mntUserOrganizationRepository.Table
            //             join org in _mntOrganizationRepository.Table on uorg.OrganizationId equals org.Id
            //             join prop in _mntUserPropertyRepository.Table on uorg.Id equals prop.UserOrgId
            //             join block in _mntOrgBlockRepository.Table on prop.BlockId equals block.Id
            //             join level in _mntOrgLevelRepository.Table on prop.LevelId equals level.Id
            //             where uorg.CustomerId == customerId
            //             select new Mnt_UserPropertyCustom()
            //             {
            //                 orgId = prop.UserOrgId,
            //                 orgName = org.Name,
            //                 id = prop.Id,
            //                 block = block.Name,
            //                 level = level.Name,
            //                 unit = prop.UnitNumber,
            //                 reId = uorg.Reid
            //             }).AsQueryable();

            query = query.OrderByDescending(q => q.id);

            return query.ToList();
        }
        #endregion

    }
}
