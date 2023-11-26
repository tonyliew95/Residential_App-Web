using Newtonsoft.Json;
using Nop.Core.Domain.Residential.Custom;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.Profile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Models.Profile.ResponseResults
{
    /// <summary>
    /// General return results 
    /// </summary>
    public class PropertyUnit_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public PropertyUnit_ResponseResult()
        {
            data = new propertyUnitList();
        }

        /// <summary>
        /// Gets or Sets for the data
        /// </summary>
        [JsonProperty("data")]
        public new propertyUnitList data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            //return typeof(propertyUnitDto);  // 20190408 WKK this class propertyUnitDto duplicated with Nop.Core.Domain.Residential.Custom.Mnt_UserPropertyCustom
            return typeof(Mnt_UserPropertyCustom);
        }
    }

    /// <summary>
    /// Store a class for data return
    /// </summary>
    public class propertyUnitList
    {
        /// <summary>
        /// Gets or sets the propertyDto 
        /// </summary>
        [JsonProperty("propertyDto")]
        //public IList<propertyUnitDto> propertyDto { get; set; }  // 20190408 WKK this class propertyUnitDto duplicated with Nop.Core.Domain.Residential.Custom.Mnt_UserPropertyCustom
        public IList<Mnt_UserPropertyCustom> propertyDto { get; set; }

        /// <summary>
        /// Gets or sets the default Org Id 
        /// </summary>
        [JsonProperty("defaultOrgId")]
        public int? defaultOrgId { get; set; }

        /// <summary>
        /// Gets or sets the default property id 
        /// </summary>
        [JsonProperty("defaultPropId")]
        public int? defaultPropId { get; set; }

        /// <summary>
        /// Gets or sets the accPropType
        /// </summary>
        [JsonProperty("accPropType")]
        public string accPropType { get; set; }
    }
}
