using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Wallet
{
    public class MSP_WT_BankRootObject : ResponseResult, ISerializableObject
    {
        public MSP_WT_BankRootObject()
        {
            WT_Bank = new List<MSP_WT_Bank>();    
        }
        
        [JsonProperty("WT_Bank")]
        public IList<MSP_WT_Bank> WT_Bank { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_WT_Bank";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_WT_Bank);
        }
    }
}