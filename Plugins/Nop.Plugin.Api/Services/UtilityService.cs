using Microsoft.IdentityModel.Tokens;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Residential.Mobile;
using Nop.Core.Infrastructure;
using Nop.Plugin.Api.Domain;
using Nop.Plugin.Api.Services.Interface;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Claims;
using System.Text;
using System.IO;
using Nop.Core;

namespace Nop.Plugin.Api.Services
{
    public class UtilityService : IUtilityService
    {
        private const string PATH = "~/App_Data/ApiConfiguration/Crypto/";
        private const string SHARED_KEY_FILE = "SharedKey.dat";
        private readonly IRepository<Mnt_MobileSession> _mobileSessionRepository;

        public UtilityService(
            IRepository<Mnt_MobileSession> mobileSessionRepository)
        {
            _mobileSessionRepository = mobileSessionRepository;
        }

        /// <summary>
        /// Read bytes from file and convert it into Base64 string.
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Secret key</returns>
        protected virtual string ReadBufferFile(string fileName)
        {
            byte[] buffer;
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    buffer = reader.ReadBytes(100); //try to read bytes up to 100 size of byte array
                    reader.Close();
                }
                if (buffer.Length > 0)
                    return Convert.ToBase64String(buffer);
            }
            return string.Empty;
        }

        /// <summary>
        /// Generate the Jwt Token
        /// </summary>
        /// <returns></returns>
        public string GenerateJwtToken(Customer customer, string deviceuuid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var appSettings = EngineContext.Current.Resolve<ApiSettings>();

            string tokenSecretKey = ReadBufferFile(CommonHelper.MapPath($"{PATH}{SHARED_KEY_FILE}"));

            //if (appSettings.TokenSecretKey == null) appSettings.TokenSecretKey = "TokenSecretKey";
            if (appSettings.TokenIssuer == null) appSettings.TokenIssuer = "GGIT Sdn Bhd";
            if (appSettings.TokenExpiryMinutes == 0) appSettings.TokenExpiryMinutes = 1000000;

            var key = Encoding.ASCII.GetBytes(tokenSecretKey);
            var tokenExpiryMins = appSettings.TokenExpiryMinutes;
            var tokenIssuer = appSettings.TokenIssuer;
            string jti = Guid.NewGuid().ToString();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Issuer
                    new Claim(JwtRegisteredClaimNames.Iss, tokenIssuer),   

                    // UserName
                    //new Claim(JwtRegisteredClaimNames.Sub, user.UserName),       

                    // Issued at
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),

                    // Unique Id for all Jwt tokes
                    new Claim(JwtRegisteredClaimNames.Jti, jti), 

                    // Email is unique
                    new Claim(JwtRegisteredClaimNames.Email, customer.Email),   
                                
                    // Admin role
                    new Claim("admin", customer.IsAdmin().ToString().ToLower()),

                    // Expiry time
                    //new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddMinutes(tokenExpireMins).ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenExpiryMins),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // save to db table [Mnt_MobileSession]
            var mnt_MobileSession = new Mnt_MobileSession()
            {
                CustomerId = customer.Id,
                TokenId = jti,
                DeviceUuid = deviceuuid,
                Valid = true,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = null
            };

            _mobileSessionRepository.Insert(mnt_MobileSession);

            return tokenString;
        }
    }
}