using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using System;
//Tony Liew 20190311 RDT-69 \/
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class UpdatePassword_ResponseResult : ApiResponse  , ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public UpdatePassword_ResponseResult()
        {
            this.data = new updatePasswordData();
        }

        /// <summary>
        /// Gets or Sets the data
        /// </summary>
        [JsonProperty("data")]
        public new updatePasswordData data { get; set; }


        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(updatePasswordData);
        }
    }

    /// <summary>
    /// Update Password Data Class
    /// </summary>
    public class updatePasswordData
    {
        /// <summary>
        /// Gets or Sets the accessToken
        /// </summary>
        [JsonProperty("accessToken")]
        public string returnAccessToken { get; set; }

        /// <summary>
        /// Gets or Sets the tokenType
        /// </summary>
        [JsonProperty("tokenType")]
        public string tokenType { get; set; }
    }
}
//Tony Liew 20190311 RDT-69 /\