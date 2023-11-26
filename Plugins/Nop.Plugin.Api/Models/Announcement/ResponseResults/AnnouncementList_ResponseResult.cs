using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Base;
using Nop.Plugin.Api.Models.Announcement.DTOs;
using System;
using System.Collections.Generic;

//WKK 20190315 RDT-121 \/
namespace Nop.Plugin.Api.Models.Announcement.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class AnnouncementList_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public AnnouncementList_ResponseResult()
        {
            //base.data = new AnnouncementList();
            data = new AnnouncementList();
            pagination = new ApiResponsePagination();
        }

        /// <summary>
        /// Gets or Sets for the pagination
        /// </summary>
        [JsonProperty("pagination")]
        public ApiResponsePagination pagination { get; set; }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public AnnouncementList data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(AnnouncementListDto);
        }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class AnnouncementList
    {
        /// <summary>
        /// Gets or sets the AnnouncementList 
        /// </summary>
        [JsonProperty("annDto")]
        public IList<AnnouncementListDto> announcementLists { get; set; }

    }
}
