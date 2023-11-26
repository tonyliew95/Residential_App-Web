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
    public class RegisterWithEmail_ResponseResult : ResponseResult, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public RegisterWithEmail_ResponseResult()
        {
        }

        public string GetPrimaryPropertyName()
        {
            return "RegisterWithEmail";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(RegisterWithEmailDto);
        }
    }
}
