using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Member;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Member
{
    public class MSP_MemberTreeRootObject : ResponseResult, ISerializableObject
    {
        public MSP_MemberTreeRootObject()
        {
            MemberTree = new List<MSP_MemberTree>();    
        }
        
        [JsonProperty("MemberTree")]
        public IList<MSP_MemberTree> MemberTree { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MemberTree";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_MemberTree);
        }
    }
}