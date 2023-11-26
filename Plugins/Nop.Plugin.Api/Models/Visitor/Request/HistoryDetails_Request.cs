using Newtonsoft.Json;

//WKK 20190411 RDT-192 [API] Visitor - Record History Details
namespace Nop.Plugin.Api.Models.Visitor.Request
{
    /// <summary>
    /// History Details input parameters
    /// </summary>
    public class HistoryDetails_Request
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
        /// Gets or sets the trxId 
        /// </summary>
        [JsonProperty("trxId")]
        public int trxId { get; set; }
    }
}
