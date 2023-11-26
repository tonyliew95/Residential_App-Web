using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Base;
using System;

namespace Nop.Plugin.Api.Models.Account.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class Register_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Register_ResponseResult()
        {
        }


        public string GetPrimaryPropertyName()
        {
            return "Register";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(object);
        }
    }
}
