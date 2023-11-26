using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Plugin.Api.Models;

namespace Nop.Plugin.Api.DTOs.Errors
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorsRootObject : ISerializableObject
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("meta")]
        public Meta meta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPrimaryPropertyName()
        {
            return "errors";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Type GetPrimaryPropertyType()
        {
            return meta.GetType();
        }
    }
}