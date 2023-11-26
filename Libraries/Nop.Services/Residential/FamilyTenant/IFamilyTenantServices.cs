using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.User;
using Nop.Core.Domain.Residential.Organization;
using System.Linq;

namespace Nop.Services.Residential.FamilyTenant
{
    public interface IFamilyTenantServices
    {
        /// <summary>
        /// Insert family/tenant
        /// </summary>
        /// <param name="user">incidents</param>
        void insertFamilyMemberOrTenant(Mnt_FamilyTenant user); //Tony Liew 20190403 RDT-176 

        /// <summary>
        /// Insert User Account
        /// </summary>
        /// <param name="user">incidents</param>
        void insertUserAccount(Mnt_UserAccount user); //Tony Liew 20190404 RDT-176 

        /// <summary>
        /// Insert User Account
        /// </summary>
        /// <param name="binding">incidents</param>
        void insertBindingProperties(Mnt_BindingProperty binding); //Tony Liew 20190404 RDT-176 

        /// <summary>
        /// Get a list of Family/Tenant List
        /// </summary>
        /// <param name="propId"></param>
        /// <param name="orgId"></param>
        /// <param name="accountType"></param>
        /// <returns></returns>
        IQueryable<Mnt_FamilyTenantCustom> getFamilyTenantList(int propId, int orgId); //Tony Liew 20190403 RDT-175 

        ///// <summary>
        ///// Get a list of Family/Tenant List
        ///// </summary>
        ///// <param name="propId"></param>
        ///// <param name="orgId"></param>
        ///// <param name="accId"></param>
        ///// <returns></returns>
        //IQueryable<Mnt_FamilyTenantDetailsCustom> getFamilyTenantDetails(int propId, int orgId, int accId); //Tony Liew 20190416 RDT-186 

        /// <summary>
        /// Update family/tenant
        /// </summary>
        /// <param name="user">incidents</param>
        void updateFamilyTenant(Mnt_FamilyTenant user); //Tony Liew 20190404 RDT-177 

        /// <summary>
        /// Delete family/tenant
        /// </summary>
        /// <param name="user">incidents</param>
        void deleteFamilyTenant(Mnt_FamilyTenant user); //Tony Liew 20190404 RDT-178 

        /// <summary>
        /// Get User Account By CustomerId and userOrgId
        /// </summary>
        /// <param name="customerId">incidents</param>
        /// <param name="userOrgId">incidents</param>
        IQueryable<Mnt_UserAccount> getUserAccount(int customerId, int userOrgId); //Tony Liew 20190410 RDT-177 

        /// <summary>
        /// Get User Account By UserAccountId
        /// </summary>
        /// <param name="customerId">incidents</param>
        IQueryable<Mnt_UserAccount> getUserAccount(int userAccountId); //Tony Liew 20190416 RDT-178

        /// <summary>
        /// Insert Users Emergency Number
        /// </summary>
        /// <param name="emergency">incidents</param>
        void insertUserEmergencyNumber(Mnt_UserEmergency emergency); //Tony Liew 20190411 RDT-177

        /// <summary>
        /// Get User Family/Tenant By orgId , accId and propId 
        /// </summary>
        /// <param name="orgId">incidents</param>
        /// <param name="AccId">incidents</param>
        /// <param name="propId">incidents</param>
        IQueryable<Mnt_FamilyTenant> getFamilyTenant(int orgId, int AccId , int propId); //Tony Liew 20190410 RDT-177 

        /// <summary>
        /// Get User Family/Tenant By orgId and accId
        /// </summary>
        /// <param name="orgId">incidents</param>
        /// <param name="AccId">incidents</param>
        IQueryable<Mnt_FamilyTenant> getFamilyTenant(int orgId, int AccId); //Tony Liew 20190416 RDT-178

        /// <summary>
        /// Get User Emergency Number
        /// </summary>
        /// <param name="customerId">incidents</param>
        IQueryable<Mnt_UserEmergency> getUserEmergencyNumber(int customerId, int targetUserId); //Tony Liew 20190410 RDT-177 

        /// <summary>
        /// Update family/tenant
        /// </summary>
        /// <param name="user">incidents</param>
        void updatedUserEmergencyNumber(Mnt_UserEmergency emergency);//Tony Liew 20190404 RDT-177 
    }
}
