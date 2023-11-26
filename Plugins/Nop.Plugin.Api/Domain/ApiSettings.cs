using Nop.Core.Configuration;

namespace Nop.Plugin.Api.Domain
{
    public class ApiSettings : ISettings
    {
        public bool EnableApi { get; set; }
        public bool AllowRequestsFromSwagger { get; set; }
        public bool EnableLogging { get; set; }

        // WKK 20190306 RDT-63 [API] Account - login
        public string TokenSecretKey { get; set; }
        public int TokenExpiryMinutes { get; set; }
        public string TokenIssuer { get; set; }
        // WKK 20190306 RDT-63 [API] Account - login
    }
}