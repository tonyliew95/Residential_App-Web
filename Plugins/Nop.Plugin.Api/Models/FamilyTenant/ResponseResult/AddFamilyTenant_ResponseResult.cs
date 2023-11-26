using Nop.Plugin.Api.DTOs;
using System;

//Tony Liew 20190404 RDT-176  \/
namespace Nop.Plugin.Api.Models.FamilyTenant.ResponseResult
{
    /// <summary>
    /// General return results
    /// </summary>
    public class AddFamilyTenant_ResponseResult : ApiResponse, ISerializableObject
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
//Tony Liew 20190404 RDT-176 /\