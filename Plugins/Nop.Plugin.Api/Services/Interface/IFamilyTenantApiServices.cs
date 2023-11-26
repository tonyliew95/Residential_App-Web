using Nop.Plugin.Api.Models.FamilyTenant.Request;
using Nop.Plugin.Api.Models.FamilyTenant.ResponseResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190403 RDT-175 \/
namespace Nop.Plugin.Api.Services.Interface
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IFamilyTenantApiServices
    {
        /// <summary>
        /// Get Family/Tenant List
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        FamilyTenantListing_ResponseResult GetFamilyTenantList(FamilyTenantListing_Request request); //Tony Liew 20190403 RDT-175 

        ///// <summary>
        ///// Get Family/Tenant Details
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns> 
        //FamilyTenantDetails_ResponseResult GetFamilyTenantDetails(FamilyTenantDetails_Request request);//Tony Liew 20190416 RDT-186 

        /// <summary>
        /// Get Family/Tenant List
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns> 
        AddFamilyTenant_ResponseResult InsertFamilyTenant(AddFamilyTenant_Request request, int customerId);//Tony Liew 20190404 RDT-176 

        /// <summary>
        /// Add Family/Tenant 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns> 
        EditFamilyTenant_ResponseResult UpdateFamilyTenant(EditFamilyTenant_Request request, int customerId);//Tony Liew 20190411 RDT-177 

        /// <summary>
        /// Add Family/Tenant 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="customerId"></param>
        /// <returns></returns> 
        RemoveFamilyTenant_ResponseResult RemoveFamilyTenant(RemoveFamilyTenant_Request request, int customerId);//Tony Liew 20190411 RDT-177 

    }
}
//Tony Liew 20190403 RDT-175 /\