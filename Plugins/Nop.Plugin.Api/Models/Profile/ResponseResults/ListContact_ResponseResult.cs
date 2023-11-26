using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Profile.DTOs;
using System;
using System.Collections.Generic;

//Tony Liew 20190415 RDT-198 \/
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class ListContact_ResponseResult : ApiResponse , ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ListContact_ResponseResult()
        {
            data = new ListContact();
            pagination = new ApiResponsePagination();
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
        /// Gets or Sets for the pagination
        /// </summary>
        [JsonProperty("pagination")]
        public ApiResponsePagination pagination { get; set; }

        /// <summary>
        /// ISerializableObject implementation
        /// </summary>
        /// <returns></returns>
        public new Type GetPrimaryPropertyType()
        {
            return typeof(ListContactDto);
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public new ListContact data { get; set; }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class ListContact
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("contactDto")]
        public IList<ListContactDto> contactDto { get; set; }

    }
}
//Tony Liew 20190415 RDT-198 /\