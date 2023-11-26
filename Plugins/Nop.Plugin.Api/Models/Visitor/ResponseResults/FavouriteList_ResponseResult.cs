using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Visitor.DTOs;
using System.Collections.Generic;

//WKK 20190417 RDT-193 [API] Visitor - Favourite Listing
namespace Nop.Plugin.Api.Models.Visitor.ResponseResults
{
    /// <summary>
    /// Visitor - Favourite Listing return results
    /// </summary>
    public class FavouriteList_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public FavouriteList_ResponseResult()
        {
            data = new FavouriteList();
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
        public new FavouriteList data { get; set; }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class FavouriteList
    {
        /// <summary>
        /// Gets or sets the VisitorHistoryList 
        /// </summary>
        [JsonProperty("visitorDto")]
        public IEnumerable<FavouriteListCustom> visitorDto { get; set; }

    }
}
