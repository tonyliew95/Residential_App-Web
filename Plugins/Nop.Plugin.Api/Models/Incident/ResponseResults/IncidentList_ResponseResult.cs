using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Base;
using Nop.Plugin.Api.Models.Incident.DTOs;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Api.Models.Incident.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class IncidentList_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public IncidentList_ResponseResult()
        {
            data = new IncidentList();
            pagination = new ApiResponsePagination();
        }

        /// <summary>
        /// Gets or Sets for the IncidentLists
        /// </summary>
        [JsonProperty("data")]
        public new IncidentList data { get; set; }


        /// <summary>
        /// Gets or Sets for the pagination
        /// </summary>
        [JsonProperty("pagination")]
        public ApiResponsePagination pagination { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(IncidentListDto);
        }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class IncidentList
    {
        /// <summary>
        /// Gets or sets the IncidentList 
        /// </summary>
        [JsonProperty("incidentDto")]
        public IList<IncidentListDto> IncidentLists { get; set; }

    }
}
