using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.FamilyTenant.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Tony Liew 20190416 RDT-186 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.ResponseResult
{
    /// <summary>
    /// General return results
    /// </summary>
    public class FamilyTenantDetails_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public FamilyTenantDetails_ResponseResult()
        {
            data = new FamilyTenantDetails();
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public new FamilyTenantDetails data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(FamilyTenantDetailsDto);
        }
    }


    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class FamilyTenantDetails
    {
        /// <summary>
        /// Gets or sets the FamilyTenantDetails 
        /// </summary>
        [JsonProperty("familyDto")]
        public FamilyTenantDetailsDto familyTenantDetails { get; set; }

    }
}
// Tony Liew 20190416 RDT-186 /\
