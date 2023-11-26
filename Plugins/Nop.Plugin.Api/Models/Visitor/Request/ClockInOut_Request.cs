using Newtonsoft.Json;

// WKK 20190416 RDT-197 [API] Visitor - Clock In/Out
namespace Nop.Plugin.Api.Models.Visitor.Request
{
    /// <summary>
    /// Visitor Clock In/Out
    /// </summary>
    public class ClockInOut_Request
    {
        /// <summary>
        /// Gets or sets the trxId 
        /// </summary>
        [JsonProperty("trxId")]
        public int trxId { get; set; }

        /// <summary>
        /// Gets or sets the clockType 
        /// </summary>
        [JsonProperty("clockType")]
        public string clockType { get; set; }

    }
}
