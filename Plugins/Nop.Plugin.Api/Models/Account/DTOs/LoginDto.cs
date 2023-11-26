using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Profile.DTOs;
using System;

namespace Nop.Plugin.Api.Models.Account.DTOs
{
    /// <summary>
    /// Log in results
    /// </summary>
    [JsonObject(Title = "Account")]
    public class LoginDto
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public LoginDto()
        {
            profileDto = new profileDto();
            //moduleDto = null;
        }

        /// <summary>
        /// Gets or sets the Login access Token
        /// </summary>
        [JsonProperty("accessToken")]
        public string accessToken { get; set; }

        /// <summary>
        /// Gets or sets the token Type
        /// </summary>
        [JsonProperty("tokenType")]
        public string tokenType { get; set; }

        /// <summary>
        /// Gets or sets the profile Dto
        /// </summary>
        [JsonProperty("profileDto")]
        public profileDto profileDto { get; set; }

        ///// <summary>
        ///// Gets or sets the module Dto
        ///// </summary>
        //[JsonProperty("moduleDto")]
        //public object moduleDto { get; set; }


    }
}
