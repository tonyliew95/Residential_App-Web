using Nop.Core.Data;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.User;
using Nop.Core.Domain.Residential.Organization;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Tony Liew 20190403 RDT-175 \/
namespace Nop.Services.Residential.FamilyTenant
{
    public class FamilyTenantServices: IFamilyTenantServices
    {
        private readonly IRepository<Mnt_UserAccount> _mntUserAccountRepository;
        private readonly IRepository<Mnt_UserProfile> _mntUserProfileRepository;
        private readonly IRepository<Mnt_FamilyTenant> _mntFamilyTenantRepository;
        private readonly IRepository<Mnt_UserOrganization> _mntUserOrganizationRepository;
        private readonly IRepository<Mnt_BindingProperty> _mntBindingPropertyRepository;
        private readonly IRepository<Mnt_UserEmergency> _mntUserEmergencyRepository;
        private readonly IEventPublisher _eventPublisher;

        public FamilyTenantServices
        (
              IRepository<Mnt_UserAccount> mntUserAccountRepository
            , IRepository<Mnt_UserProfile> mntUserProfileRepository
            , IRepository<Mnt_FamilyTenant> mntFamilyTenantRepository
            , IRepository<Mnt_UserOrganization> mntUserOrganizationRepository
            , IEventPublisher eventPublisher
            , IRepository<Mnt_BindingProperty> mntBindingPropertyRepository
            , IRepository<Mnt_UserEmergency> mntUserEmergencyRepository
        )
        {
            this._mntUserAccountRepository = mntUserAccountRepository;
            this._mntUserProfileRepository = mntUserProfileRepository;
            this._mntFamilyTenantRepository = mntFamilyTenantRepository;
            this._mntUserOrganizationRepository = mntUserOrganizationRepository;
            this._eventPublisher = eventPublisher;
            this._mntBindingPropertyRepository = mntBindingPropertyRepository;
            this._mntUserEmergencyRepository = mntUserEmergencyRepository;
        }

        #region Insert Family Member Or Tenant
        /// <summary>
        /// Insert family/tenant
        /// </summary>
        /// <param name="user">incidents</param>
        public void insertFamilyMemberOrTenant(Mnt_FamilyTenant user) //Tony Liew 20190403 RDT-176 
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _mntFamilyTenantRepository.Insert(user);

            //event notification
            _eventPublisher.EntityInserted(user);
        }
        #endregion

        #region Insert User Account
        /// <summary>
        /// Insert User Account
        /// </summary>
        /// <param name="user">incidents</param>
        public void insertUserAccount(Mnt_UserAccount user) //Tony Liew 20190404 RDT-176 
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _mntUserAccountRepository.Insert(user);

            //event notification
            _eventPublisher.EntityInserted(user);
        }
        #endregion

        #region Insert Binding Properties
        /// <summary>
        /// Insert User Account
        /// </summary>
        /// <param name="user">incidents</param>
        public void insertBindingProperties(Mnt_BindingProperty binding) //Tony Liew 20190404 RDT-176 
        {
            if (binding == null)
                throw new ArgumentNullException(nameof(binding));

            _mntBindingPropertyRepository.Insert(binding);

            //event notification
            _eventPublisher.EntityInserted(binding);
        }
        #endregion

        #region Insert Users Emergency Number
        /// <summary>
        /// Insert Users Emergency Number
        /// </summary>
        /// <param name="emergency">incidents</param>
        public void insertUserEmergencyNumber(Mnt_UserEmergency emergency) //Tony Liew 20190411 RDT-177
        {
            if (emergency == null)
                throw new ArgumentNullException(nameof(emergency));

            _mntUserEmergencyRepository.Insert(emergency);

            //event notification
            _eventPublisher.EntityInserted(emergency);
        }
        #endregion

        #region  Get User Family/Tenant By orgId ,accId and propId
        /// <summary>
        /// Get User Family/Tenant By orgId , accId and propId 
        /// </summary>
        /// <param name="orgId">incidents</param>
        /// <param name="AccId">incidents</param>
        /// <param name="propId">incidents</param>
        public IQueryable<Mnt_FamilyTenant> getFamilyTenant(int orgId, int AccId , int propId) //Tony Liew 20190410 RDT-177 
        {
            var query = (from fam in _mntFamilyTenantRepository.Table
                         where fam.OrganizationId == orgId && fam.UserAccId == AccId && fam.OrgUnitPropertyId == propId
                         select fam).AsQueryable();

            return query;
        }
        #endregion

        #region  Get User Family/Tenant By orgId ,accId
        /// <summary>
        /// Get User Family/Tenant By orgId and accId
        /// </summary>
        /// <param name="orgId">incidents</param>
        /// <param name="AccId">incidents</param>
        public IQueryable<Mnt_FamilyTenant> getFamilyTenant(int orgId, int AccId) //Tony Liew 20190416 RDT-178
        {
            var query = (from fam in _mntFamilyTenantRepository.Table
                         where fam.OrganizationId == orgId && fam.UserAccId == AccId 
                         select fam).AsQueryable();

            return query;
        }
        #endregion

        #region Get User Emergency Number
        /// <summary>
        /// Get User Emergency Number
        /// </summary>
        /// <param name="customerId">incidents</param>
        public IQueryable<Mnt_UserEmergency> getUserEmergencyNumber(int customerId, int targetUserId) //Tony Liew 20190410 RDT-177 
        {
            var query = (from emergency in _mntUserEmergencyRepository.Table
                         where emergency.CustomerId == customerId && emergency.EmergencyUserId == targetUserId
                         select emergency).AsQueryable();

            return query;
        }
        #endregion

        #region Get list of Family/Tenant
        /// <summary>
        /// Get a list of Family/Tenant List
        /// </summary>
        /// <param name="propId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IQueryable<Mnt_FamilyTenantCustom> getFamilyTenantList(int propId , int orgId) //Tony Liew 20190403 RDT-175 
        {
            var query = (from member in _mntFamilyTenantRepository.Table 
                         join uAccount in _mntUserAccountRepository.Table on member.UserAccId equals uAccount.Id
                         where member.OrgUnitPropertyId == propId
                         select new Mnt_FamilyTenantCustom()
                         {
                            orgId = member.OrganizationId,
                            propId = member.OrgUnitPropertyId,
                            accId = uAccount.Id,
                            type = uAccount.AccountType,
                            name = member.Name,
                            accType = uAccount.Occupancy,
                            countryCode = member.CountryCode,
                            periodStart = member.OccupyStart,
                            periodEnd = member.OccupyEnd,
                            msisdn = member.Msisdn,
                            createdDatetime = member.CreatedOnUtc,
                            emergency = member.Emergency,
                            info = member.Info,
                            email = member.Email,
                            reminder = member.Reminder,
                            media = (from member in _mntFamilyTenantRepository.Table where member.UserAccId == uAccount.Id select new MediaCustom() {type = member.FileType , content = member.File_URI}).AsQueryable()
                         }).AsQueryable();


            if (orgId != -1)
                query = query.Select(q => q).Where(p => p.orgId == orgId);

            query = query.OrderByDescending(q => q.createdDatetime);
            return query;
        }
        #endregion

        #region Get Details of Family/Tenant
        ///// <summary>
        ///// Get a list of Family/Tenant List
        ///// </summary>
        ///// <param name="propId"></param>
        ///// <param name="orgId"></param>
        ///// <param name="accId"></param>
        ///// <returns></returns>
        //public IQueryable<Mnt_FamilyTenantDetailsCustom> getFamilyTenantDetails(int propId, int orgId , int accId) //Tony Liew 20190416 RDT-186 
        //{
        //    var query = (from member in _mntFamilyTenantRepository.Table
        //                 join uAccount in _mntUserAccountRepository.Table on member.UserAccId equals uAccount.Id
        //                 where member.OrgUnitPropertyId == propId && member.UserAccId == accId && member.OrganizationId == orgId
        //                 select new Mnt_FamilyTenantDetailsCustom()
        //                 {
        //                     orgId = member.OrganizationId,
        //                     propId = member.OrgUnitPropertyId,
        //                     accId = uAccount.Id,
        //                     name = member.Name,
        //                     type = uAccount.AccountType,
        //                     info = member.Info,
        //                     accType = uAccount.Occupancy,
        //                     email = member.Email,
        //                     countryCode = member.CountryCode,
        //                     periodStart = member.OccupyStart,
        //                     periodEnd = member.OccupyEnd,
        //                     reminder = member.Reminder,
        //                     msisdn = member.Msisdn,
        //                     createdDatetime = member.CreatedOnUtc,
        //                     emergency = member.Emergency,
        //                     media = (from member in _mntFamilyTenantRepository.Table where member.UserAccId == uAccount.Id select new MediaCustom() { type = member.FileType, content = member.File_URI }).AsQueryable()
        //                 }).AsQueryable();

        //    query = query.OrderByDescending(q => q.createdDatetime);
        //    return query;
        //}
        #endregion

        #region Get User Account By CustomerId and userOrgId
        /// <summary>
        /// Get User Account By CustomerId and userOrgId
        /// </summary>
        /// <param name="customerId">incidents</param>
        public IQueryable<Mnt_UserAccount> getUserAccount(int customerId , int userOrgId) //Tony Liew 20190410 RDT-177 
        {
            var query = (from account in _mntUserAccountRepository.Table
                         where account.CustomerId == customerId && account.UserOrgId == userOrgId
                         select account).AsQueryable();

            return query;
        }
        #endregion

        #region Get User Account By userAccountId
        /// <summary>
        /// Get User Account By userAccountId
        /// </summary>
        /// <param name="customerId">incidents</param>
        public IQueryable<Mnt_UserAccount> getUserAccount(int userAccountId) //Tony Liew 20190416 RDT-178
        {
            var query = (from account in _mntUserAccountRepository.Table
                         where account.Id == userAccountId
                         select account).AsQueryable();

            return query;
        }
        #endregion

        #region Get User Organisation id by OrgId and PropId
        /// <summary>
        /// Get User Family/Tenant By orgId and accId
        /// </summary>
        /// <param name="customerId">incidents</param>
        public IQueryable<Mnt_UserOrganization> GetMnt_UserOrganizations(int orgId, int propId) //Tony Liew 20190410 RDT-177 
        {
            var query = (from uOrg in _mntUserOrganizationRepository.Table
                         where uOrg.OrganizationId == orgId && uOrg.OrganizationId == orgId && uOrg.OrganizationId == propId
                         select uOrg).AsQueryable();

            return query;
        }
        #endregion

        #region Update Family Member Or Tenant
        /// <summary>
        /// Update family/tenant
        /// </summary>
        /// <param name="user">incidents</param>
        public void updateFamilyTenant(Mnt_FamilyTenant user) //Tony Liew 20190404 RDT-177 
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _mntFamilyTenantRepository.Update(user);

            //event notification
            _eventPublisher.EntityInserted(user);
        }
        #endregion

        #region Delete Family Member Or Tenant
        /// <summary>
        /// Update family/tenant
        /// </summary>
        /// <param name="user">incidents</param>
        public void deleteFamilyTenant(Mnt_FamilyTenant user) //Tony Liew 20190404 RDT-178 
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _mntFamilyTenantRepository.Insert(user);

            //event notification
            _eventPublisher.EntityInserted(user);
        }
        #endregion

        #region Update User Emergency Number
        /// <summary>
        /// Update family/tenant
        /// </summary>
        /// <param name="user">incidents</param>
        public void updatedUserEmergencyNumber(Mnt_UserEmergency emergency)//Tony Liew 20190404 RDT-177 
        {
            if (emergency == null)
                throw new ArgumentNullException(nameof(emergency));

            _mntUserEmergencyRepository.Update(emergency);

            //event notification
            _eventPublisher.EntityInserted(emergency);
        }
        #endregion

    }
}
//Tony Liew 20190403 RDT-175 /\
