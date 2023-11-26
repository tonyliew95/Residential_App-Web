using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190307 RDT-118 \/
namespace Nop.Plugin.Api.Models.Incident.ResponseResults
{
    /// <summary>
    /// General return results
    /// </summary>
    public class IncidentReport_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Gets or Sets the Object
        /// </summary>
        [JsonIgnore]
        public new object data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }
        public Type GetPrimaryPropertyType()
        {
            return typeof(object);
        }
    }
    //Tony Liew 20190307 RDT-118 /\
}
