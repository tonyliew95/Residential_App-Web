using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Base;
using Nop.Plugin.Api.Models.Incident.DTOs;
using System;
using System.Collections.Generic;

//Tony Liew 20190306 RDT-117 \/
namespace Nop.Plugin.Api.Models.Incident.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class IncidentDetails_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public IncidentDetails_ResponseResult()
        {
            data = new IncidentDetails();
        }

        /// <summary>
        /// Gets or sets the data 
        /// </summary>
        [JsonProperty("data")]
        public new IncidentDetails data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(IncidentDetailsDto);
        }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class IncidentDetails
    {
        /// <summary>
        /// Gets or sets the IncidentList 
        /// </summary>
        [JsonProperty("incidentDto")]
        public IncidentDetailsDto IncidentDetail { get; set; }

    }
}
//Tony Liew 20190306 RDT-117 /\
