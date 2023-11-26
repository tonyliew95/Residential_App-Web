using Nop.Plugin.Api.DTOs;
using System;

//Tony Liew 20190408 RDT-177 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.ResponseResult
{
    /// <summary>
    ///  General return results
    /// </summary>
    public class EditFamilyTenant_ResponseResult: ApiResponse, ISerializableObject
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
//Tony Liew 20190408 RDT-177 /\