using Nop.Core.Domain.Customers;
using System;

namespace Nop.Plugin.Api.Services.Interface
{
    public interface IUtilityService
    {
        /// <summary>
        /// Generate the Jwt Token
        /// </summary>
        /// <returns></returns>
        string GenerateJwtToken(Customer customer, string deviceuuid);
    }
}