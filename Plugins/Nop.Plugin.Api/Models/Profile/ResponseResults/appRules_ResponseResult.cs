using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Base;
using Nop.Plugin.Api.Models.Profile.DTOs;
using System;

namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// General return results 
    /// </summary>
    public class AppRules_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public AppRules_ResponseResult()
        {
            data = new appRules();
        }

        /// <summary>
        /// Gets and Sets the data
        /// </summary>
        [JsonProperty("data")]
        public new appRules data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(appRules);
        }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class appRules
    {
        /// <summary>
        /// Gets or sets the terms Dto 
        /// </summary>
        [JsonProperty("termsDto")]
        public string termsUri { get; set; }

        /// <summary>
        /// Gets or sets the policy Dto 
        /// </summary>
        [JsonProperty("policyDto")]
        public string policyUri { get; set; }
    }
}
