using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Profile.DTOs;
using System;

// WKK 20190325 RDT-164 [API] Profile - Detail
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// Profile Detail return results
    /// </summary>
    public class ProfileDetails_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ProfileDetails_ResponseResult()
        {
            data = new profileDetailsDto();
        }


        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(profileDetailsDto);
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public profileDetailsDto data { get; set; }
    }

    /// <summary>
    /// profile Details Dto
    /// </summary>
    public class profileDetailsDto
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public profileDetailsDto()
        {
            profileDto = new profileDto();
        }

        /// <summary>
        /// Gets or sets the profile Dto
        /// </summary>
        [JsonProperty("profileDto")]
        public profileDto profileDto { get; set; }
    }
}
