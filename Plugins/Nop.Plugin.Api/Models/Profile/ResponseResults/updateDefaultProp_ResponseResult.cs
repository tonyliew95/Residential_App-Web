using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using Nop.Plugin.Api.DTOs;

//WKK 20190327 RDT-167 [API] Property Unit - Set default property
namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// Set default property
    /// </summary>
    public class UpdateDefaultProp_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public UpdateDefaultProp_ResponseResult()
        {
            data = new propertyData();
        }

        /// <summary>
        /// Gets or Sets the data
        /// </summary>
        [JsonProperty("data")]
        public new propertyData data { get; set; }
    }

    /// <summary>
    /// Response data
    /// </summary>
    public class propertyData
    {
        /// <summary>
        /// Gets or Sets the defaultOrgId
        /// </summary>
        [JsonProperty("defaultOrgId")]
        public int defaultOrgId { get; set; }

        /// <summary>
        /// Gets or Sets the defaultPropId
        /// </summary>
        [JsonProperty("defaultPropId")]
        public int defaultPropId { get; set; }

        /// <summary>
        /// Gets or Sets the accPropType
        /// </summary>
        [JsonProperty("accPropType")]
        public string accPropType { get; set; }

    }
}
