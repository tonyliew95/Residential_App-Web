using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Base;

//WKK 20190417 RDT-193 [API] Visitor - Favourite Listing
namespace Nop.Plugin.Api.Models.Visitor.Request
{
    /// <summary>
    /// Visitor - Favourite Listing input parameters
    /// </summary>
    public class FavouriteList_Request : ApiFilterParamPagination
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
    }
}
