using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Wallet
{
    public class MSP_WT_ProfitRootObject : ResponseResult, ISerializableObject
    {
        public MSP_WT_ProfitRootObject()
        {
            WT_Profit = new List<MSP_WT_Profit>();    
        }
        
        [JsonProperty("WT_Profit")]
        public IList<MSP_WT_Profit> WT_Profit { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_WT_Profit";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_WT_Profit);
        }
    }
}