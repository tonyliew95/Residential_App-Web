using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Wallet
{
    public class MSP_WT_ConsumptionRootObject : ResponseResult, ISerializableObject
    {
        public MSP_WT_ConsumptionRootObject()
        {
            WT_Consumption = new List<MSP_WT_Consumption>();    
        }
        
        [JsonProperty("WT_Consumption")]
        public IList<MSP_WT_Consumption> WT_Consumption { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_WT_Consumption";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_WT_Consumption);
        }
    }
}