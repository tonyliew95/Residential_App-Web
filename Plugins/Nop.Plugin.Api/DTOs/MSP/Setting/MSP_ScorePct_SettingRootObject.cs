using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Setting;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Api.DTOs.Msp.Setting
{
    public class MSP_ScorePct_SettingRootObject : FilterParam, ISerializableObject
    {
        public MSP_ScorePct_SettingRootObject()
        {
            ScorePct_Setting = new List<MSP_ScorePct_Setting>();
        }

        [JsonProperty("ScorePct_Setting")]
        public IList<MSP_ScorePct_Setting> ScorePct_Setting { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_ScorePct_Setting";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(MSP_ScorePct_Setting);
        }
    }
}
