using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190417 RDT-202 \/
namespace Nop.Plugin.Api.Models.Facility.DTOs
{
    /// <summary>
    /// Booking Facility Dto
    /// </summary>
    public class BookingFacilityListingDto
    {
        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        [JsonProperty("orgId")]
        public int? orgId { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("propId")]
        public int? propId { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("record")]
        public Record record { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("success")]
        public string success { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("pending")]
        public string pending { get; set; }

        /// <summary>
        /// Gets or sets the reschedule 
        /// </summary>
        [JsonProperty("reschedule")]
        public string reschedule { get; set; }
    }

    /// <summary>
    /// Record Class
    /// </summary>
    public class Record
    {
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
        /// Gets or sets the date 
        /// </summary>
        [JsonProperty("date")]
        public DateTime date { get; set; }

        /// <summary>
        /// Gets or sets the reqDate 
        /// </summary>
        [JsonProperty("reqDate")]
        public DateTime? reqDate { get; set; }

        /// <summary>
        /// Gets or sets the status 
        /// </summary>
        [JsonProperty("status")]
        public string status { get; set; }

        /// <summary>
        /// Gets or sets the statusName 
        /// </summary>
        [JsonProperty("statusName")]
        public string statusName { get; set; }
    }
}
//Tony Liew 20190417 RDT-202 /\