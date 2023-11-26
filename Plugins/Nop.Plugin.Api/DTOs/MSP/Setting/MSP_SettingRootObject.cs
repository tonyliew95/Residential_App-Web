using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Setting;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;
using System;
using System.Collections.Generic;


namespace Nop.Plugin.Api.DTOs.Msp.Setting
{
    public class MSP_SettingRootObject : FilterParam, ISerializableObject
    {
        public MSP_SettingRootObject()
        {
            Setting = new List<MSP_Setting>();
        }

        [JsonProperty("Setting")]
        public IList<MSP_Setting> Setting { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "Setting";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(MSP_Setting);
        }
    }
}
