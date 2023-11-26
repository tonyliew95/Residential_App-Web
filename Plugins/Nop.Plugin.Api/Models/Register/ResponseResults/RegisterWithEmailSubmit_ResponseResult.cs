using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Register.DTOs;
using Nop.Plugin.Api.Models.Base;
using System;

namespace Nop.Plugin.Api.Models.Register.ResponseResults
{
    /// <summary>
    /// General return results 
    /// </summary>
    public class RegisterWithEmailSubmit_ResponseResult : ResponseResult, ISerializableObject
    {
        /// <summary>
        /// Gets and sets Customer GUID
        /// </summary>
        [JsonProperty("CustomerGUID")]
        public Guid? CustomerGUID { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "RegisterWithEmailSubmit";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(RegisterWithEmailSubmitDto);
        }
    }
}
