using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Wallet
{
    public class MSP_WT_MbtcRootObject : ResponseResult, ISerializableObject
    {
        public MSP_WT_MbtcRootObject()
        {
            WT_Mbtc = new List<MSP_WT_Mbtc>();    
        }
        
        [JsonProperty("WT_Mbtc")]
        public IList<MSP_WT_Mbtc> WT_Mbtc { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_WT_Mbtc";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_WT_Mbtc);
        }
    }
}