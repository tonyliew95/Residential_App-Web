using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Base;
using Nop.Plugin.Api.Models.Announcement.DTOs;
using System;
using System.Collections.Generic;

//WKK 20190315 RDT-122 [API] Notice - Announcement detail
namespace Nop.Plugin.Api.Models.Announcement.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class AnnouncementDetails_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public AnnouncementDetails_ResponseResult()
        {
            data = new AnnouncementDetails();
        }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(AnnouncementDetailsDto);
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public AnnouncementDetails data { get; set; }

    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class AnnouncementDetails
    {
        /// <summary>
        /// Gets or sets the AnnouncementList 
        /// </summary>
        [JsonProperty("annDto")]
        public AnnouncementDetailsDto announcementDetail { get; set; }

    }
}
