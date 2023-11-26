using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System.Collections.Generic;

// WKK 20190322 RDT-63 [API] Account - login
namespace Nop.Plugin.Api.Models.Profile.DTOs
{
    /// <summary>
    /// profileDto Class
    /// </summary>
    public class profileDto
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public profileDto()
        {
            properties = new List<Mnt_UserPropertyCustom>();
        }

        /// <summary>
        /// Gets or sets the properties 
        /// </summary>
        [JsonProperty("properties")]
        public List<Mnt_UserPropertyCustom> properties { get; set; }

        /// <summary>
        /// Gets or sets the defaultOrgId 
        /// </summary>
        [JsonProperty("defaultOrgId")]
        public int? defaultOrgId { get; set; }

        /// <summary>
        /// Gets or sets the defaultPropId 
        /// </summary>
        [JsonProperty("defaultPropId")]
        public int? defaultPropId { get; set; }

        /// <summary>
        /// Gets or sets the Account Type 
        /// </summary>
        [JsonProperty("accPropType")]
        public string accPropType { get; set; }

        /// <summary>
        /// Gets or sets the displayName 
        /// </summary>
        [JsonProperty("displayName")]
        public string displayName { get; set; }

        /// <summary>
        /// Gets or sets the image 
        /// </summary>
        [JsonProperty("image")]
        public string image { get; set; }

        /// <summary>
        /// Gets or sets the email 
        /// </summary>
        [JsonProperty("email")]
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the userId 
        /// </summary>
        [JsonProperty("userId")]
        public int userId { get; set; }

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
        /// Gets or sets the msisdnId 
        /// </summary>
        [JsonProperty("msisdnId")]
        public int msisdnId { get; set; }

        /// <summary>
        /// Gets or sets the emailId 
        /// </summary>
        [JsonProperty("emailId")]
        public int? emailId { get; set; }

    }
}