using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using System;

//Tony Liew 20190319 RDT-67 \/
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class UpdateContact_ResponseResult : ApiResponse, ISerializableObject
    {
        ///// <summary>
        ///// Gets or Sets the data
        ///// </summary>
        //[JsonProperty("data")]
        //public new updateMobileViaEmail data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(object);
        }

    }

    /// <summary>
    /// update Mobile Via Email class
    /// </summary>
    public class updateMobileViaEmail
    {


    }

}
//Tony Liew 20190319 RDT-67 /\