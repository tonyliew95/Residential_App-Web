using Newtonsoft.Json;
using Nop.Plugin.Api.DTOs;
using Nop.Plugin.Api.Models.General.DTOs;
using Nop.Plugin.Api.Models.Profile.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Models.General.ResponseResults
{
    /// <summary>
    /// General return results 
    /// </summary>
    public class Setting_ResponseResult : ApiResponse, ISerializableObject
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Setting_ResponseResult()
        {
            data = new SettingList();
        }
        
        /// <summary>
        /// Gets and Sets the data
        /// </summary>
        [JsonProperty("data")]
        public new SettingList data { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "data";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(SettingList);
        }

        /// <summary>
        /// Store a class for data return
        /// </summary>
        public class SettingList
        {
            /// <summary>
            /// Gets or sets the FAQ Dto 
            /// </summary>
            [JsonProperty("settingDto")]
            public SettingDto settingDto { get; set; }

        }
    }
}
