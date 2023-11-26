using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Base;
using System;

//WKK 20190411 RDT-189 [API] Visitor - Record History Listing
namespace Nop.Plugin.Api.Models.Visitor.Request
{
    /// <summary>
    /// History list input parameters
    /// </summary>
    public class HistoryList_Request : ApiFilterParamPagination
    {
        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("propId")]
        public int propId { get; set; }

        /// <summary>
        /// Gets or sets the dateFrom 
        /// </summary>
        [JsonProperty("dateFrom")]
        public string dateFrom { get; set; }

        /// <summary>
        /// Gets or sets the dateTo 
        /// </summary>
        [JsonProperty("dateTo")]
        public string dateTo { get; set; }

    }
}
