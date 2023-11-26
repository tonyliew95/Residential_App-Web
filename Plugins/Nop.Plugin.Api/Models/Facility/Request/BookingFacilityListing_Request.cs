using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Base;

//Tony Liew 20190417 RDT-202 \/
namespace Nop.Plugin.Api.Models.Facility.Request
{
    /// <summary>
    /// BookingFacility Request
    /// </summary>
    public class BookingFacilityListing_Request : ApiFilterParamPagination
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
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("dateTo")]
        public string dateTo { get; set; }
    }
}

//Tony Liew 20190417 RDT-202 /\