using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Account.DTOs;
using Nop.Plugin.Api.Models.Base;
using System;

// WKK 20190315 RDT-63 [API] Account - login
namespace Nop.Plugin.Api.Models.Account.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class Login_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Login_ResponseResult()
        {
            data = new LoginDto();
        }


        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(LoginDto);
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public LoginDto data { get; set; }
    }
}
