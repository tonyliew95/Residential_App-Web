using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Nop.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Nop.Web.Infrastructure
{
    public class ApiResponseHeader
    {
        private static ApiConfiguration _apiConfig;

        public ApiResponseHeader()
        {
            Initializer();            
        }

        public static void Initializer()
        {
            if (_apiConfig == null)
            {
                using (StreamReader r = new StreamReader(@"api-settings.json"))
                {
                    string json = r.ReadToEnd();
                    _apiConfig = JsonConvert.DeserializeObject<ApiConfiguration>(json);
                }
            }
        }

        public virtual void SetResponseHeader(HttpContext context)
        {
            context.Response.Headers.Append("api-version", _apiConfig.Version); //Version
            context.Response.Headers.Append("cache-control", _apiConfig.ResponseConfiguration.CacheControl); //HTTP 1.1
            context.Response.Headers.Append("expires", _apiConfig.ResponseConfiguration.Expires); //Proxies
            context.Response.Headers.Append("pragma", _apiConfig.ResponseConfiguration.Pragma); //HTTP 1.0
            context.Response.Headers.Append("x-content-type-options", _apiConfig.ResponseConfiguration.XContentTypeOptions); //Prevent XSS on file uploads
            context.Response.Headers.Append("x-xss-protection", _apiConfig.ResponseConfiguration.XssProtection); //X-XSS Protection
            context.Response.Headers.Append("x-frame-options", _apiConfig.ResponseConfiguration.XFrameOptions); //X-Frame-Options 
            context.Response.Cookies.Delete(".Nop.Antiforgery"); //remove cookies
            context.Response.Cookies.Delete(".Nop.Customer"); //remove cookies
            context.Response.Headers.Remove("Set-Cookie");  //remove cookies
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json; charset=utf-8";
        }
    }
}
