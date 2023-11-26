using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190403 RDT-175 \/
namespace Nop.Plugin.Api.Models.FamilyTenant.DTOs
{
    /// <summary>
    /// FamilyListingDto class
    /// </summary>
    public class FamilyTenantListingDto
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
        /// Gets or sets the type 
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

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
        /// Gets or sets the period 
        /// </summary>
        [JsonProperty("period")]
        public string period { get; set; }

        /// <summary>
        /// Gets or sets the createdDatetime 
        /// </summary>
        [JsonProperty("createdDatetime")]
        public string createdDatetime { get; set; }

        /// <summary>
        /// Gets or sets the email 
        /// </summary>
        [JsonProperty("email")]
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the info 
        /// </summary>
        [JsonProperty("info")]
        public string info { get; set; }

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
        /// Gets or sets the emergency 
        /// </summary>
        [JsonProperty("emergency")]
        public bool emergency { get; set; }

        /// <summary>
        /// Gets or sets the media 
        /// </summary>
        [JsonProperty("media")]
        public IList<mediaDto> media { get; set; }
    }
}
//Tony Liew 20190403 RDT-175 /\