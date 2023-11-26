using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using Nop.Plugin.Api.DTOs;
using System;

//WKK 20190403 RDT-183 [API] P.Account settings - Update profile picture
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// Update Picture Response Result
    /// </summary>
    public class updatePicture_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public updatePicture_ResponseResult()
        {
            data = new mediaData();
        }

        /// <summary>
        /// Gets or Sets the data
        /// </summary>
        [JsonProperty("data")]
        public new mediaData data { get; set; }
    }

    /// <summary>
    /// Response data
    /// </summary>
    public class mediaData
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public mediaData()
        {
            media = new MediaCustom();
        }
        
        /// <summary>
        /// Gets or Sets the media
        /// </summary>
        [JsonProperty("media")]
        public MediaCustom media { get; set; }
    }
}
