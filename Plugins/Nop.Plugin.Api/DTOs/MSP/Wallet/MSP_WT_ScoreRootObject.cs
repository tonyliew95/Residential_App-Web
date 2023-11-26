using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Wallet
{
    public class MSP_WT_ScoreRootObject : ResponseResult, ISerializableObject
    {
        public MSP_WT_ScoreRootObject()
        {
            WT_Score = new List<MSP_WT_Score>();    
        }
        
        [JsonProperty("WT_Score")]
        public IList<MSP_WT_Score> WT_Score { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_WT_Score";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_WT_Score);
        }
    }
}