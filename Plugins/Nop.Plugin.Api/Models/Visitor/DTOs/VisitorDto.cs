using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System;
using System.Collections.Generic;

// WKK 20190418 RDT-194 [API] Visitor - Favourite Details
namespace Nop.Plugin.Api.Models.Visitor.DTOs
{
    /// <summary>
    /// VisitorDto class
    /// </summary>
    [JsonObject(Title = "VisitorDto")]
    public class VisitorDto
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public VisitorDto()
        {
            vehicle = new VehicleDto();
            media = new MediaCustom();
        }

        /// <summary>
        /// Gets or sets the id 
        /// </summary>
        [JsonProperty("id")]
        public int id { get; set; }

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
        /// Gets or sets the identityNumber 
        /// </summary>
        [JsonProperty("identityNumber")]
        public string identityNumber { get; set; }

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
        /// Gets or sets the driveIn 
        /// </summary>
        [JsonProperty("driveIn")]
        public bool driveIn { get; set; }

        /// <summary>
        /// Gets or sets the vehicleType 
        /// </summary>
        [JsonIgnore]
        [JsonProperty("vehicleType")]
        public string vehicleType { get; set; }

        /// <summary>
        /// Gets or sets the vehicleNumber 
        /// </summary>
        [JsonIgnore]
        [JsonProperty("vehicleNumber")]
        public string vehicleNumber { get; set; }

        /// <summary>
        /// Gets or sets the vehicle 
        /// </summary>
        [JsonProperty("vehicle")]
        public VehicleDto vehicle { get; set; }

        /// <summary>
        /// Gets or sets the media 
        /// </summary>
        [JsonProperty("media")]
        public MediaCustom media { get; set; }

        /// <summary>
        /// Gets or sets the Image path url
        /// </summary>
        [JsonIgnore]
        [JsonProperty("Image")]
        public string Image { get; set; }
        
    }
}