using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Tony Liew 20190416 RDT-186 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.DTOs
{
    /// <summary>
    /// FamilyTenantDetailsDto class
    /// </summary>
    public class FamilyTenantDetailsDto
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
        /// Gets or sets the accId 
        /// </summary>
        [JsonProperty("accId")]
        public int accId { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the email 
        /// </summary>
        [JsonProperty("email")]
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the accType 
        /// </summary>
        [JsonProperty("accType")]
        public string accType { get; set; }

        /// <summary>
        /// Gets or sets the countryCode 
        /// </summary>
        [JsonProperty("countryCode")]
        public string countryCode { get; set; }

        /// <summary>
        /// Gets or sets the msisdn 
        /// </summary>
        [JsonProperty("msisdn")]
        public string msisdn { get; set; }

        /// <summary>
        /// Gets or sets the info 
        /// </summary>
        [JsonProperty("info")]
        public string info { get; set; }

        /// <summary>
        /// Gets or sets the emergency 
        /// </summary>
        [JsonProperty("emergency")]
        public bool emergency { get; set; }


        /// <summary>
        /// Gets or sets the ocpyStart 
        /// </summary>
        [JsonProperty("ocpyStart")]
        public string ocpyStart { get; set; }

        /// <summary>
        /// Gets or sets the ocpyEnd 
        /// </summary>
        [JsonProperty("ocpyEnd")]
        public string ocpyEnd { get; set; }

        /// <summary>
        /// Gets or sets the reminder 
        /// </summary>
        [JsonProperty("reminder")]
        public string reminder { get; set; }

        /// <summary>
        /// Gets or sets the media 
        /// </summary>
        [JsonProperty("media")]
        public IList<mediaDto> media { get; set; }
    }
}
// Tony Liew 20190416 RDT-186 /\