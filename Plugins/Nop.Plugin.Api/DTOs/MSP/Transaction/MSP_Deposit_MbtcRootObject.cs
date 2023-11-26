using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Transaction;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;
using System;
using System.Collections.Generic;

namespace Nop.Plugin.Api.DTOs.Msp.Transaction
{
    public class MSP_Deposit_MbtcRootObject : ResponseResult, ISerializableObject
    {
        public MSP_Deposit_MbtcRootObject()
        {
            Deposit_Mbtc = new List<MSP_Mbtc_Deposit>();
        }

        [JsonProperty("DepositMbtc")]
        public IList<MSP_Mbtc_Deposit> Deposit_Mbtc { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_Deposit_Mbtc";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof(MSP_Deposit_Mbtc);
        }
    }
}
