using Nop.Plugin.Api.DTOs;
using System;

// Tony Liew 20190412 RDT-68 \/
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// General return results 
    /// </summary>
    public class AddContactNumber_ResponseResult : ApiResponse, ISerializableObject
    {

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(Faq);
        }
    }
}
// Tony Liew 20190412 RDT-68 /\