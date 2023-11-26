using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nop.Plugin.Api.Models
{
    // erictan - 20180822
    /// <summary>
    /// Error Result Object
    /// </summary>
    public interface IErrorsResultObject
    {
        int ReturnCode { get; set; }
        string ReturnMessage { get; set; }
    }
}