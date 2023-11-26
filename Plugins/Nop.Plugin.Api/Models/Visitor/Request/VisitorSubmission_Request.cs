using Newtonsoft.Json;
using Nop.Plugin.Api.Models.Media;
using Nop.Plugin.Api.Models.Visitor.DTOs;

// WKK 20190415 RDT-190 [API] Visitor - Request Submission
namespace Nop.Plugin.Api.Models.Visitor.ResponseResults
{
    /// <summary>
    /// Submit a visitor request
    /// </summary>
    public class VisitorSubmission_Request
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

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

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
        /// Gets or sets the identityNum 
        /// </summary>
        [JsonProperty("identityNum")]
        public string identityNum { get; set; }

        /// <summary>
        /// Gets or sets the email
        /// </summary>
        [JsonProperty("email")]
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the purpose 
        /// </summary>
        [JsonProperty("purpose")]
        public string purpose { get; set; }

        /// <summary>
        /// Gets or sets the visitingDate 
        /// </summary>
        [JsonProperty("visitingDate")]
        public string visitingDate { get; set; }

        /// <summary>
        /// Gets or sets the driveIn 
        /// </summary>
        [JsonProperty("driveIn")]
        public bool? driveIn { get; set; }

        /// <summary>
        /// Gets or sets the vehicle 
        /// </summary>
        [JsonProperty("vehicle")]
        public VehicleDto vehicle { get; set; }

        /// <summary>
        /// Gets or sets the Resident media
        /// </summary>
        [JsonProperty("media")]
        public mediaDto media { get; set; }

    }
}
