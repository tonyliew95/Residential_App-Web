using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Member;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Member
{
    public class MSP_MemberScoreRootObject : ResponseResult, ISerializableObject
    {
        public MSP_MemberScoreRootObject()
        {
            MemberScore = new List<MSP_MemberScorePct>();    
        }
        
        [JsonProperty("MemberScorePct")]
        public IList<MSP_MemberScorePct> MemberScore { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_MemberScorePct";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_MemberScorePct);
        }
    }
}