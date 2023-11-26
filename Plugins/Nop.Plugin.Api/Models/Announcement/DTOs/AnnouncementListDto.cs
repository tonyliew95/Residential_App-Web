using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System;
using System.Collections.Generic;

//WKK 20190315 RDT-121 \/
namespace Nop.Plugin.Api.Models.Announcement.DTOs
{
    /// <summary>
    /// AnnouncementListDto class
    /// </summary>
    [JsonObject(Title = "announcementDto")]
    public class AnnouncementListDto
    {
        /// <summary>
        /// Gets or sets the org Id 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the id 
        /// </summary>
        [JsonProperty("id")]
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the subject 
        /// </summary>
        [JsonProperty("subject")]
        public string subject { get; set; }

        /// <summary>
        /// Gets or sets the content 
        /// </summary>
        [JsonProperty("content")]
        public string content { get; set; }

        /// <summary>
        /// Gets or sets the Date 
        /// </summary>
        [JsonProperty("date")]
        public string date { get; set; }

        /// <summary>
        /// Gets or sets the media 
        /// </summary>
        [JsonProperty("media")]
        public IEnumerable<MediaCustom> media { get; set; }
    }
}
//WKK 20190315 RDT-121 /\