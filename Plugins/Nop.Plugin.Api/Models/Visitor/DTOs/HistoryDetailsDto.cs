using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using System;
using System.Collections.Generic;

//WKK 20190411 RDT-192 [API] Visitor - Record History Details
namespace Nop.Plugin.Api.Models.Visitor.DTOs
{
    /// <summary>
    /// HistoryDetailsDto class
    /// </summary>
    [JsonObject(Title = "HistoryDetailsDto")]
    public class HistoryDetailsDto
    {
        /// <summary>
        /// Gets or sets the trxId 
        /// </summary>
        [JsonProperty("trxId")]
        public int trxId { get; set; }

        /// <summary>
        /// Gets or sets the orgId 
        /// </summary>
        [JsonProperty("orgId")]
        public int orgId { get; set; }

        /// <summary>
        /// Gets or sets the orgName 
        /// </summary>
        [JsonProperty("orgName")]
        public string orgName { get; set; }

        /// <summary>
        /// Gets or sets the propId 
        /// </summary>
        [JsonProperty("propId")]
        public int propId { get; set; }

        /// <summary>
        /// Gets or sets the resident 
        /// </summary>
        [JsonProperty("resident")]
        public ResidentDto resident { get; set; }

        /// <summary>
        /// Gets or sets the visitor 
        /// </summary>
        [JsonProperty("visitor")]
        public VisitDto visitor { get; set; }
    }

    public class ResidentDto
    {
        /// <summary>
        /// Gets or sets the name 
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the unit 
        /// </summary>
        [JsonProperty("unit")]
        public string unit { get; set; }

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
    }

    public class VisitDto
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public VisitDto()
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
        /// Gets or sets the visitingDate 
        /// </summary>
        [JsonProperty("visitingDate")]
        public string visitingDate { get; set; }

        /// <summary>
        /// Gets or sets the visiting Date For Query Purpose
        /// </summary>
        [JsonIgnore]
        [JsonProperty("visitingDate_ForQuery")]
        public DateTime visitingDate_ForQuery { get; set; }

        /// <summary>
        /// Gets or sets the clockInTimestamp 
        /// </summary>
        [JsonProperty("clockInTimestamp")]
        public string clockInTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the clockOutTimestamp 
        /// </summary>
        [JsonProperty("clockOutTimestamp")]
        public string clockOutTimestamp { get; set; }

        /// <summary>
        /// Gets or sets the driveIn 
        /// </summary>
        [JsonProperty("driveIn")]
        public bool driveIn { get; set; }

        /// <summary>
        /// Gets or sets the vehicle 
        /// </summary>
        [JsonProperty("vehicle")]
        public VehicleDto vehicle { get; set; }

        ///// <summary>
        ///// Gets or sets the media 
        ///// </summary>
        [JsonProperty("media")]
        public MediaCustom media { get; set; }

        /// <summary>
        /// Gets or sets the Resident Id
        /// </summary>
        [JsonIgnore]
        [JsonProperty("ResidentId")]
        public int ResidentId { get; set; }

        /// <summary>
        /// Gets or sets the Resident Unit 
        /// </summary>
        [JsonIgnore]
        [JsonProperty("ResidentUnit")]
        public string ResidentUnit { get; set; }

        /// <summary>
        /// Gets or sets the Organization Id 
        /// </summary>
        [JsonIgnore]
        [JsonProperty("OrganizationId")]
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the Organization name 
        /// </summary>
        [JsonIgnore]
        [JsonProperty("OrganizationName")]
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the Property Id 
        /// </summary>
        [JsonIgnore]
        [JsonProperty("PropertyId")]
        public int PropertyId { get; set; }

        /// <summary>
        /// Gets or sets the TrxVisitor Id 
        /// </summary>
        [JsonIgnore]
        [JsonProperty("TrxVisitorId")]
        public int TrxVisitorId { get; set; }

        /// <summary>
        /// Gets or sets the Image path url
        /// </summary>
        [JsonIgnore]
        [JsonProperty("Image")]
        public string Image { get; set; }

    }

    public class VehicleDto
    {
        /// <summary>
        /// Gets or sets the type 
        /// </summary>
        [JsonProperty("type")]
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the number 
        /// </summary>
        [JsonProperty("number")]
        public string number { get; set; }
    }
}