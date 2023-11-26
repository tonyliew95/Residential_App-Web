using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using System;
using System.Collections.Generic;

// WKK 20190327 RDT-174        [API] P.Language - List language
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// Profile Detail return results
    /// </summary>
    public class ListLanguage_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ListLanguage_ResponseResult()
        {
            data = new languageData();
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
            return typeof(languageData);
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public languageData data { get; set; }
    }

    /// <summary>
    /// Language data
    /// </summary>
    public class languageData
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public languageData()
        {
            languageDto = new List<languageDto>();
        }

        /// <summary>
        /// Gets or sets the Language Dto
        /// </summary>
        [JsonProperty("languageDto")]
        public List<languageDto> languageDto { get; set; }

        /// <summary>
        /// Gets or sets the default Language
        /// </summary>
        [JsonProperty("defaultLanguage")]
        public string defaultLanguage { get; set; }
    }

    /// <summary>
    /// Language Dto
    /// </summary>
    public class languageDto
    {
        /// <summary>
        /// Gets or sets the Language name
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the Language code
        /// </summary>
        [JsonProperty("code")]
        public string code { get; set; }
    }
}
