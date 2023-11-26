using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Transaction;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Api.DTOs.Msp.Transaction
{
    public class MSP_SettlementRootObject : ResponseResult, ISerializableObject
    {
        public MSP_SettlementRootObject()
        {
            Settlement = new List<MSP_Settlement>();
        }

        [JsonProperty("Settlement")]
        public IList<MSP_Settlement> Settlement { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_Settltment";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(MSP_Settlement);
        }
        
    }
}
