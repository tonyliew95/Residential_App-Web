using System;
using Nop.Plugin.Api.DTOs;

namespace Nop.Plugin.Api.Models.Base
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseResult
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ResponseResult()
        {
            ReturnCode = 0;
            ReturnMessage = "API_ResponseResult.Successful";
        }

        /// <summary>
        /// Gets or sets the Return Code
        /// </summary>
        public int ReturnCode { get; set; }

        /// <summary>
        /// Gets or sets the Return Message
        /// </summary>
        public string ReturnMessage { get; set; }
    }
}
