using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Tony Liew 20190415 RDT-200 \/
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class RemoveContact_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public RemoveContact_ResponseResult()
        {
        }

        /// <summary>
        /// ISerializableObject implementation
        /// </summary>
        /// <returns></returns>
        public new string GetPrimaryPropertyName()
        {
            return "data";
        }

        /// <summary>
        /// ISerializableObject implementation
        /// </summary>
        /// <returns></returns>
        public new Type GetPrimaryPropertyType()
        {
            return typeof(object);
        }
    }
}
//Tony Liew 20190415 RDT-198 /\
