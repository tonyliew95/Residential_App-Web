using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Wallet
{
    public class MSP_WT_Mbtc_FloatRootObject : ResponseResult, ISerializableObject
    {
        public MSP_WT_Mbtc_FloatRootObject()
        {
            WT_Mbtc_Float = new List<MSP_WT_Mbtc_Float>();    
        }
        
        [JsonProperty("WT_Mbtc_Float")]
        public IList<MSP_WT_Mbtc_Float> WT_Mbtc_Float { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_WT_Mbtc_Float";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_WT_Mbtc_Float);
        }
    }
}