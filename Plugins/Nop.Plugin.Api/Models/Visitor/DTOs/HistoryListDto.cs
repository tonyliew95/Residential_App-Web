using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System;
using System.Collections.Generic;

//WKK 20190411 RDT-189 [API] Visitor - Record History Listing
namespace Nop.Plugin.Api.Models.Visitor.DTOs
{
    /// <summary>
    /// HistoryListDto class
    /// </summary>
    [JsonObject(Title = "HistoryListDto")]
    public class HistoryListDto
    {
        /// <summary>
        /// Gets or sets the org Id 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("propId")]
        public int propId { get; set; }

        /// <summary>
        /// Gets or sets the trxId 
        /// </summary>
        [JsonProperty("trxId")]
        public int trxId { get; set; }

        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the visitingDate 
        /// </summary>
        [JsonProperty("visitingDate")]
        public string visitingDate { get; set; }

        /// <summary>
        /// Gets or sets the visiting Date For Query Purpose
        /// </summary>
        [JsonIgnore]
        [JsonProperty("visitingDateForQuery")]
        public DateTime? visitingDate_ForQuery { get; set; }

        ///// <summary>
        ///// Gets or sets the clockType 
        ///// </summary>
        //[JsonProperty("clockType")]
        //public string clockType { get; set; }

        ///// <summary>
        ///// Gets or sets the clockTimestamp 
        ///// </summary>
        //[JsonProperty("clockTimestamp")]
        //public string clockTimestamp { get; set; }

        ///// <summary>
        ///// Gets or sets the clockTimestamp Date For Query Purpose
        ///// </summary>
        //[JsonIgnore]
        //[JsonProperty("clockTimestampForQuery")]
        //public DateTime? clockTimestamp_ForQuery { get; set; }

        /// <summary>
        /// Gets or sets the clock
        /// </summary>
        [JsonProperty("clock")]
        public IEnumerable<ClockDto> clock { get; set; }

    }

    /// <summary>
    /// ClockDto class
    /// </summary>
    [JsonObject(Title = "ClockDto")]
    public class ClockDto
    {
        /// <summary>
        /// Gets or sets the clockType 
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the clockTimestamp 
        /// </summary>
        [JsonProperty("timestamp")]
        public string timestamp { get; set; }

        /// <summary>
        /// Gets or sets the clockTimestamp Date For Query Purpose
        /// </summary>
        [JsonIgnore]
        [JsonProperty("clockTimestampForQuery")]
        public DateTime? clockTimestamp_ForQuery { get; set; }
    }
}