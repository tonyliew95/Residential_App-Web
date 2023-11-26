using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using System;

namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class UpdateLanguage_ResponseResult : ApiResponse  , ISerializableObject
    {
        /// <summary>
        /// Gets or Sets the Object
        /// </summary>
        [JsonIgnore]
        public new object data { get; set; }

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
