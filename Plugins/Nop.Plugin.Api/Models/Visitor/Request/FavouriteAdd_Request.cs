using Newtonsoft.Json;

// WKK 20190417 RDT-195 [API] Visitor - Favourite Add
namespace Nop.Plugin.Api.Models.Visitor.ResponseResults
{
    /// <summary>
    /// Add user's visitor into favourite visitor list
    /// </summary>
    public class FavouriteVisitor_Request
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
        /// Gets or sets the visitorId 
        /// </summary>
        [JsonProperty("visitorId")]
        public int visitorId { get; set; }



    }
}
