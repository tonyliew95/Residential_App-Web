using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nop.Core.Domain.Msp.Member;
using Nop.Plugin.Api.Models;
using Nop.Plugin.Api.Models.Base;

namespace Nop.Plugin.Api.DTOs.Msp.Member
{
    public class MSP_SessionRootObject : ResponseResult, ISerializableObject
    {
        public MSP_SessionRootObject()
        {
            MemberSession = new List<MSP_Session>();    
        }
        
        [JsonProperty("MemberSession")]
        public IList<MSP_Session> MemberSession { get; set; }

        public string GetPrimaryPropertyName()
        {
            return "MSP_Session";
        }

        public Type GetPrimaryPropertyType()
        {
            return typeof (MSP_Session);
        }
    }
}