using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Visitor.DTOs;
using System;
using System.Collections.Generic;

//WKK 20190411 RDT-189 [API] Visitor - Record History Listing
namespace Nop.Plugin.Api.Models.Visitor.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class HistoryList_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public HistoryList_ResponseResult()
        {
            data = new VisitorHistoryList();
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
        public new VisitorHistoryList data { get; set; }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class VisitorHistoryList
    {
        /// <summary>
        /// Gets or sets the VisitorHistoryList 
        /// </summary>
        [JsonProperty("visitorDto")]
        public IList<HistoryListDto> visitorDto { get; set; }

    }
}
