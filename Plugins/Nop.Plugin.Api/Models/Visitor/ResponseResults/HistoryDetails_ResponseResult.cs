using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Visitor.DTOs;

//WKK 20190411 RDT-192 [API] Visitor - Record History Details
namespace Nop.Plugin.Api.Models.Visitor.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class HistoryDetails_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public HistoryDetails_ResponseResult()
        {
            data = new HistoryDetails();
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public new HistoryDetails data { get; set; }

    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class HistoryDetails
    {
        /// <summary>
        /// Gets or sets the HistoryDetailsDto 
        /// </summary>
        [JsonProperty("visitorDto")]
        public HistoryDetailsDto visitorDto { get; set; }

    }
}
