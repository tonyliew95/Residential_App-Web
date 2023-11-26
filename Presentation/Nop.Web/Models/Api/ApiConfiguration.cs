using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Api
{
    public class ApiConfiguration
    {
        public string Version { get; set; }
        public ResponseConfiguration ResponseConfiguration;
        public Settings Settings;
        public CORS CORS;
    }

    public class ResponseConfiguration
    {
        public string CacheControl { get; set; }
        public string Expires { get; set; }
        public string Pragma { get; set; }
        public string XContentTypeOptions { get; set; }
        public string XssProtection { get; set; }
        public string XFrameOptions { get; set; }
    }

    public class Settings
    {
        public bool EnabledSwaggerUI { get; set; }
    }

    public class CORS
    {
        public string[] Origins { get; set; }
        public string[] Methods { get; set; }
        public string[] Headers { get; set; }
    }
}
