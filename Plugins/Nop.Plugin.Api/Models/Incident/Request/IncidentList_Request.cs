using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Base;
//Tony Liew 20190306 RDT-116 \/
namespace Nop.Plugin.Api.Models.Incident.Request
{
    /// <summary>
    /// Incident list input parameters
    /// </summary>
    public class IncidentList_Request : ApiFilterParamPagination
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
//Tony Liew 20190306 RDT-116 /\