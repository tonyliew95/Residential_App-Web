using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Wallet
{
    public class MSP_WalletRootObject : ResponseResult, ISerializableObject
    {
        public MSP_WalletRootObject()
        {
            Wallet = new List<MSP_Wallet>();    
        }
        
        [JsonProperty("Wallet")]
        public IList<MSP_Wallet> Wallet { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_Wallet";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_Wallet);
        }
    }
}