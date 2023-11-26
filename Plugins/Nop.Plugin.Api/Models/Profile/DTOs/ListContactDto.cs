using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Tony Liew 20190415 RDT-198 \/
namespace Nop.Plugin.Api.Models.Profile.DTOs
{
    /// <summary>
    /// ListContactDto Class
    /// </summary>
    public class ListContactDto
    {
        /// <summary>
        /// Gets or Sets the id
        /// </summary>
        [JsonProperty("id")]
        public int id { get; set; }

        /// <summary>
        /// Gets or Sets the countryCode
        /// </summary>
        [JsonProperty("countryCode")]
        public string countryCode { get; set; }

        /// <summary>
        /// Gets or Sets the msisdn
        /// </summary>
        [JsonProperty("msisdn")]
        public string msisdn { get; set; }

        /// <summary>
        /// Gets or Sets the primary
        /// </summary>
        [JsonProperty("primary")]
        public bool primary { get; set; }
    }
}
// Tony Liew 20190415 RDT-198 /\
