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
    public class GetFAQUri_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public GetFAQUri_ResponseResult()
        {
            data = new Faq();
        }

        /// <summary>
        /// Gets and Sets the data
        /// </summary>
        [JsonProperty("data")]
        public new Faq data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(Faq);
        }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class Faq
    {
        /// <summary>
        /// Gets or sets the FAQ Dto 
        /// </summary>
        [JsonProperty("faqDto")]
        public string uri { get; set; }

    }
}
