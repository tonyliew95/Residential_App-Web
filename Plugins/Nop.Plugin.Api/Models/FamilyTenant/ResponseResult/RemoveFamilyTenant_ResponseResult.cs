using Nop.Plugin.Api.DTOs;
using System;

// Tony Liew 20190416 RDT-178 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.ResponseResult
{
    /// <summary>
    ///  General return results
    /// </summary>
    public class RemoveFamilyTenant_ResponseResult : ApiResponse ,ISerializableObject
    {
        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(object);
        }
    }
}
// Tony Liew 20190416 RDT-178 /\