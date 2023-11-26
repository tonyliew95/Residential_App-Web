using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.FamilyTenant.DTOs;
using System;
using System.Collections.Generic;
//Tony Liew 20190403 RDT-175  \/
namespace Nop.Plugin.Api.Models.FamilyTenant.ResponseResult
{
    /// <summary>
    /// General return results
    /// </summary>
    public class FamilyTenantListing_ResponseResult :  ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public FamilyTenantListing_ResponseResult()
        {
            data = new FamilyTenantList();
            pagination = new ApiResponsePagination();
        }

        /// <summary>
        /// Gets or Sets for the pagination
        /// </summary>
        [JsonProperty("pagination")]
        public ApiResponsePagination pagination { get; set; }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public new FamilyTenantList data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(FamilyTenantListingDto);
        }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class FamilyTenantList
    {
        /// <summary>
        /// Gets or sets the FamilyLists 
        /// </summary>
        [JsonProperty("familyDto")]
        public IList<FamilyTenantListingDto> familyLists { get; set; }

    }
}
//Tony Liew 20190403 RDT-175 /\