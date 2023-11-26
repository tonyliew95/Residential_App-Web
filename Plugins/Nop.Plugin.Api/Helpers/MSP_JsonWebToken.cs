using System;
using System.IdentityModel.Tokens.Jwt;

namespace Nop.Plugin.Api.Helpers
{
    using Microsoft.IdentityModel.Tokens;
    using Nop.Plugin.Api.Enumeration;
    using System.Security.Claims;
    using static Nop.Plugin.Api.Helpers.CryptoHelper;

    /// <summary>
    /// This is a program to authenticate Json Web Token
    /// 1. Id_Token (for registration)
    /// 2. Access_Token (for login and query after login success)
    /// </summary>
    public class MSP_JsonWebToken
    {
        private readonly DateTime DefaultDatetime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private readonly AWSKeys AwsPublicKey = CreateAwsPublicKeys();

        private JwtSecurityTokenHandler AccessHandler;
        private JwtSecurityTokenHandler IdHandler;
        private JwtSecurityToken AccessToken;
        private JwtSecurityToken IdToken;
        private Token IdTokenObject;
        private Token AccessTokenObject;
        private readonly string access_token;
        private readonly string id_token;
        private string accessTokenClientId;
        private string Scope;

        /// <summary>
        /// Valid token 
        /// </summary>
        public bool IsValidToken { get; set; }
        /// <summary>
        /// Id token validation
        /// </summary>
        public bool IdTokenValid { get; set; }
        /// <summary>
        /// Access token validation
        /// </summary>
        public bool AccessTokenValid { get; set; }
        /// <summary>
        /// Extract email from token
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Customer GlobalGuid
        /// </summary>
        //public Guid UserGuid { get; set; } //WilliamHo 20181030
        public string UserGuid { get; set; } //WilliamHo 20181030
        /// <summary>
        /// Introducer GlobalGuid
        /// </summary>
        //public Guid? ReferralGuid { get; set; } //WilliamHo 20181030
        public string ReferralGuid { get; set; } //WilliamHo 20181030
        /// <summary>
        /// Session Id / User Guid
        /// </summary>
        //public Guid SessionId { get; set; } //WilliamHo 20181030
        public string SessionId { get; set; } //WilliamHo 20181030
        /// <summary>
        /// User role, m = merchant; c = consumer
        /// </summary>
        public string UserRole { get; set; }
        /// <summary>
        /// Indicate the user from allowed to perform deposit or not allow to perform deposit into MS
        /// </summary>
        public bool IsUSCitizen { get; set; }
        /// <summary>
        /// JWT ID (unique identifier for this token)
        /// </summary>
        //public Guid JwtTokenGuid { get; set; } //WilliamHo 20181030
        public string JwtTokenGuid { get; set; } //WilliamHo 20181030
        /// <summary>
        /// Any general error message return during token validation
        /// </summary>
        public string ErrorMessage { get; set; }

        private struct Token
        {
            //public bool IsValidToken { get; set; }
            public string TokenType { get; set; }
            /*WilliamHo 20181030 change it to string as ACE assume these 2 field as normal string data type instead of GUID data type
            //public Guid UserGuid { get; set; }
            //public Guid? ReferralGuid { get; set; }
            */
            public string UserGuid { get; set; } //WilliamHo 20181030
            public string ReferralGuid { get; set; } //WilliamHo 20181030
            public string UserEmail { get; set; }
            public string Username { get; set; }
            public string Userrole { get; set; }
            public bool USCitizen { get; set; }
            //public Guid JwtTokenGuid { get; set; } //WilliamHo 20181030
            public string JwtTokenGuid { get; set; }   //WilliamHo 20181030
        }

        private bool ValidateToken(string token, bool expiryFlag = true, bool audienceFlag = false, bool actorFlag = false, bool issuerFlag = false)
        {
            bool success = false;
            var JwtTokenHandler = new JwtSecurityTokenHandler();
            if (JwtTokenHandler.CanReadToken(token))
            {
                try
                {
                    var Token = JwtTokenHandler.ReadJwtToken(token);
                    var Header = Token.Header;
                    var KeyId = Header.Kid;
                    //var AwsPublicKey = CreateAwsPublicKeys();
                    if (string.IsNullOrEmpty(AwsPublicKey.client_id)) throw new Exception("Public key not found");
                    var Audience = AwsPublicKey.client_id;
                    var Issuer = AwsPublicKey.issuer;
                    var keys = AwsPublicKey.keys;
                    var AccessPubKey = new AWSPublicKey();
                    foreach (var key in keys)
                    {
                        if (key.kid == KeyId)
                        {
                            AccessPubKey = key;
                            break;
                        }
                    }
                    var jwKey = new JsonWebKey
                    {
                        Kid = AccessPubKey.kid,
                        Alg = AccessPubKey.alg,
                        E = AccessPubKey.e,
                        Kty = AccessPubKey.kty,
                        N = AccessPubKey.n,
                        Use = AccessPubKey.use
                    };
                    TokenValidationParameters validationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Issuer,             //issuer URL
                        ValidAudience = Audience,         //client_id
                        IssuerSigningKey = jwKey,         //public key
                        ValidateLifetime = expiryFlag,
                        ValidateAudience = audienceFlag,
                        ValidateActor = actorFlag,
                        ValidateIssuer = issuerFlag
                    };
                    try
                    {
                        ClaimsPrincipal claimsPrincipal = JwtTokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
                        success = true;
                    }
                    catch (SecurityTokenExpiredException SecExpiryEx)
                    {
                        //Token is expired
                        ErrorMessage = "Token is expired";
                        Console.WriteLine(SecExpiryEx.Message);
                    }
                    catch (SecurityTokenSignatureKeyNotFoundException SecKeyEx)
                    {
                        //Signature validation failed, token's kid not match with publickey's kid
                        ErrorMessage = "Invalid Signature";
                        Console.WriteLine(SecKeyEx.Message);
                    }
                    catch (SecurityTokenInvalidAudienceException AudEx)
                    {
                        //Client Id mismatch
                        ErrorMessage = "Invalid Client Id";
                        Console.WriteLine(AudEx.Message);
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage = "Unhandled exception";
                        Console.WriteLine(ex.Message);
                    }
                }
                catch(Exception ex)
                {
                    ErrorMessage = ex.Message;
                    Console.WriteLine(ex.Message);
                }
            }
            return success;
        }

        /// <summary>
        /// Ctor to process Id token and Access token.
        /// Registration and login usage.
        /// </summary>
        /// <param name="IdToken"></param>
        /// <param name="AccessToken"></param>
        /// <param name="CheckExpired"></param>
        public MSP_JsonWebToken(string IdToken, string AccessToken, bool CheckExpired)
        {
            id_token = IdToken;
            access_token = AccessToken;
            AccessHandler = new JwtSecurityTokenHandler();
            IdHandler = new JwtSecurityTokenHandler();
            IdTokenValid = ValidateToken(id_token, CheckExpired);
            AccessTokenValid = ValidateToken(access_token, CheckExpired);
            if (!IdTokenValid || !AccessTokenValid) //if either one of the token validation failed, exit program immediately
            {
                IsValidToken = false;
                Console.WriteLine("Token validation fail");
                return;
            }
            else
            {
                Console.WriteLine("Token validation success");
            }
            InitializeAll();
            LoadAllData();
        }

        /// <summary>
        /// Ctor to process Id token and Access token.
        /// Withdrawal application use
        /// </summary>
        /// <param name="IdToken"></param>
        /// <param name="AccessToken"></param>
        /// <param name="CheckExpired"></param>
        /// <param name="ElevatedExpiry"></param>
        public MSP_JsonWebToken(string IdToken, string AccessToken, bool CheckExpired, bool ElevatedExpiry)
        {
            id_token = IdToken;
            access_token = AccessToken;
            AccessHandler = new JwtSecurityTokenHandler();
            IdHandler = new JwtSecurityTokenHandler();
            IdTokenValid = ValidateToken(id_token, CheckExpired);
            AccessTokenValid = ValidateToken(access_token, CheckExpired);
            if (!IdTokenValid || !AccessTokenValid) //if either one of the token validation failed, exit program immediately
            {
                IsValidToken = false;
                Console.WriteLine("Token validation fail");
                return;
            }
            else
            {
                Console.WriteLine("Token validation success");
            }
            InitializeAll(ElevatedExpiry);
            if(!IsValidToken)
            {
                Console.WriteLine("Token elevated expiry timeout");
                ErrorMessage = "Token elevated expiry timeout";
                return;
            }
            LoadAllData();
        }

        /// <summary>
        /// Ctor to process Access token. Login and query usage.
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <param name="CheckExpired"></param>
        public MSP_JsonWebToken(string AccessToken, bool CheckExpired)
        {
            access_token = AccessToken;
            AccessHandler = new JwtSecurityTokenHandler();
            AccessTokenValid = ValidateToken(access_token, CheckExpired);
            if (!AccessTokenValid) //if token validation failed, exit program immediately
            {
                IsValidToken = false;
                Console.WriteLine("Token validation fail");
                return;
            }
            else
            {
                Console.WriteLine("Token validation success");
            }
            InitializeAccess();
            LoadAccessData();
        }

        /// <summary>
        /// This prevents any 'internal' application from being able to access information 
        /// or perform tasks that it shouldn't be able to.
        /// </summary>
        /// <param name="TargetScope">Prefix scope setting</param>
        /// <returns></returns>
        public bool ValidScope(string TargetScope)
        {
            if (string.IsNullOrEmpty(Scope) || string.IsNullOrEmpty(TargetScope)) return false;
            if (Scope == TargetScope) return true;

            return false;
        }

        /// <summary>
        /// Any application registered with ACE can use the client_id in the access_token to 
        /// identify which platform is sending the request. We check finsys id in our public key json.
        /// </summary>
        /// <returns></returns>
        public bool ValidPlatform()
        {
            if (accessTokenClientId == AwsPublicKey.client_id) return true;

            return false;
        }

        private void LoadAccessData()
        {
            UserGuid = AccessTokenObject.UserGuid;
            //Email = AccessTokenObject.UserEmail; //WilliamHo 20181009 scrum discussed that do not store email
            Email = "";
            SessionId = AccessTokenObject.UserGuid;
            JwtTokenGuid = AccessTokenObject.JwtTokenGuid;
        }

        private void LoadAllData()
        {
            //User GUID
            UserGuid = IdTokenObject.UserGuid;

            //Referral GUID, only show in id_token when registration
            //if (Guid.Empty.Equals(IdTokenObject.ReferralGuid))
            //    ReferralGuid = null;
            //else
            ReferralGuid = IdTokenObject.ReferralGuid;

            //Email, shown in id_token. Not sure access_token will have the email or not.
            //Email = IdTokenObject.UserEmail; //WilliamHo 20181009 scrum discussed that do not store email
            Email = "";

            //Temporary use UserGUID as session ID
            SessionId = AccessTokenObject.UserGuid;

            //US Citizen prohibited to deposit action
            IsUSCitizen = IdTokenObject.USCitizen;

            //User role. m = merchant, c = consumer
            var role = string.Empty;
            if (!string.IsNullOrEmpty(IdTokenObject.Userrole))
            {
                role = IdTokenObject.Userrole;
                role = role[0].ToString();
            }
            UserRole = role;
            //consumer only allow to do deposit(US Citizen = false)
            //merchant not allow deposit(US Citizen = true)
            //if (UserRole == GlobalSettingEnum.MerchantValue) 
            //if(string.Equals(UserRole, GlobalSettingEnum.MerchantValue,StringComparison.OrdinalIgnoreCase))
            //    IsUSCitizen = true;

            JwtTokenGuid = AccessTokenObject.JwtTokenGuid;
        }

        private void InitializeAll(bool ElevatedExpiry = false)
        {
            // Identify token format
            AccessTokenValid = AccessHandler.CanReadToken(access_token);
            IdTokenValid = IdHandler.CanReadToken(id_token);

            if (IdTokenValid)
            {
                IdToken = IdHandler.ReadJwtToken(id_token); // Load token into JWT library token
                Token IdObject = new Token();
                var IdPayLoad = IdToken.Payload;

                if (IdPayLoad.ContainsKey("token_use"))     // Token type
                {
                    IdPayLoad.TryGetValue("token_use", out object Value);
                    if (Value.ToString() == "id")
                    {
                        IsValidToken = true;
                    }
                    else
                    {
                        IdTokenValid = false;
                        return;
                    }
                    IdObject.TokenType = Value.ToString();
                }
                if (IdPayLoad.ContainsKey("sub"))           // Customer Guid
                {
                    IdPayLoad.TryGetValue("sub", out object Value);
                    //if (Guid.TryParse(Value.ToString(), out Guid guid)) //WilliamHo 20181030 
                    //{
                    //    IdObject.UserGuid = guid;
                    //    IdObject.Username = guid.ToString();
                    //}
                    //else
                    //{
                    //    IsValidToken = false;
                    //    IdObject.UserGuid = Guid.Empty;
                    //}
                    IdObject.UserGuid = Value.ToString(); //WilliamHo 20181030 
                    IdObject.Username = Value.ToString(); //WilliamHo 20181030 
                }
                if (IdPayLoad.ContainsKey("email"))         // Customer Email
                {
                    IdPayLoad.TryGetValue("email", out object Value);
                    IdObject.UserEmail = Value.ToString();
                    Email = Value.ToString();
                }
                if (IdPayLoad.ContainsKey("ace_ref"))       // Referral Guid
                {
                    IdPayLoad.TryGetValue("ace_ref", out object Value);
                    //if (Guid.TryParse(Value.ToString(), out Guid guid)) //WilliamHo 20181030 
                    //    IdObject.ReferralGuid = guid;
                    //else
                    //    IdObject.ReferralGuid = null;
                    IdObject.ReferralGuid = Value.ToString();
                }
                if (IdPayLoad.ContainsKey("ace_role"))      // Role of the user. c for consumer, m for merchant, blank if none (MS user only)
                {
                    IdPayLoad.TryGetValue("ace_role", out object Value);
                    IdObject.Userrole = Value.ToString();
                }
                if (IdPayLoad.ContainsKey("ace_depo"))      // Deposit / Non-Deposit User. true for deposit user, false for non-deposit (should be blocked from making deposits in MS)
                {
                    IdPayLoad.TryGetValue("ace_depo", out object Value);
                    IdObject.USCitizen = Value.ToString() != "true";
                }
                //No need to check again the token expiry as ValidateToken(string token) will handle this part
                //if (IdPayLoad.ContainsKey("exp"))           // Token expiry
                //{
                //    IdPayLoad.TryGetValue("exp", out object Value);
                //    var TokenExpireTime = DefaultDatetime.AddSeconds(Convert.ToInt64(Value));
                //    var CurrentDatetime = DateTime.UtcNow;

                //    //Token Timestamp older than Current Timestamp, return -1
                //    if (TokenExpireTime.CompareTo(CurrentDatetime) < 0) IdObject.IsExpiredToken = true;
                //}

                //WilliamHo 20181122 API Version 1.0.4 \/
                /* Require to check id_token => ace_elev_exp, this field will expired in 5 or ACE set the going to expire time */
                if (ElevatedExpiry)
                { 
                    if (IdPayLoad.ContainsKey("ace_elev_exp"))           // Token expiry
                    {
                        IdPayLoad.TryGetValue("ace_elev_exp", out object Value);
                        var TokenExpireTime = DefaultDatetime.AddSeconds(Convert.ToInt64(Value));
                        var CurrentDatetime = DateTime.UtcNow;

                        //ace_elev_exp Timestamp older than Current Timestamp, return -1
                        if (TokenExpireTime.CompareTo(CurrentDatetime) < 0)
                        {
                            IsValidToken = false;
                            return;
                        }
                    }
                    //WilliamHo 20190108 MSP-654 \/
                    else
                    {
                        IsValidToken = false;
                        return;
                    }
                    //WilliamHo 20190108 MSP-654 /\
                }
                //WilliamHo 20181122 API Version 1.0.4 \/

                IdTokenObject = IdObject;
            }

            if (AccessTokenValid)
            {
                AccessToken = AccessHandler.ReadJwtToken(access_token);
                Token AccessObject = new Token();
                var AccessPayLoad = AccessToken.Payload;

                if (AccessPayLoad.ContainsKey("token_use"))     // Token type
                {
                    AccessPayLoad.TryGetValue("token_use", out object Value);
                    if (Value.ToString() == "access")
                    {
                        IsValidToken = true;
                    }
                    else
                    {
                        AccessTokenValid = false;
                        return;
                    }
                    AccessObject.TokenType = Value.ToString();
                }
                if (AccessPayLoad.ContainsKey("sub"))           // Customer Guid
                {
                    AccessPayLoad.TryGetValue("sub", out object Value);
                    //if (Guid.TryParse(Value.ToString(), out Guid guid)) //WilliamHo 20181030 
                    //{
                    //    AccessObject.UserGuid = guid;
                    //    AccessObject.Username = guid.ToString();
                    //}
                    //else
                    //{
                    //    IsValidToken = false;
                    //    AccessObject.UserGuid = Guid.Empty;
                    //}
                    AccessObject.UserGuid = Value.ToString(); //WilliamHo 20181030 
                    AccessObject.Username = Value.ToString(); //WilliamHo 20181030 
                }
                if (AccessPayLoad.ContainsKey("email"))         // Customer Email
                {
                    AccessPayLoad.TryGetValue("email", out object Value);
                    AccessObject.UserEmail = Value.ToString();
                    Email = Value.ToString();
                }
                if (AccessPayLoad.ContainsKey("client_id"))         // client id
                {
                    AccessPayLoad.TryGetValue("client_id", out object Value);
                    accessTokenClientId = Value.ToString();
                }
                if (AccessPayLoad.ContainsKey("jti"))           // JWT ID (unique identifier for this token)
                {
                    AccessPayLoad.TryGetValue("jti", out object Value);
                    //if (Guid.TryParse(Value.ToString(), out Guid guid)) //WilliamHo 20181030 
                    //    AccessObject.JwtTokenGuid = guid;
                    AccessObject.JwtTokenGuid = Value.ToString(); //WilliamHo 20181030 
                }
                AccessTokenObject = AccessObject;
                //No need to check again the token expiry as ValidateToken(string token) will handle this part
                //if (AccessPayLoad.ContainsKey("exp"))           // Token expiry
                //{
                //    AccessPayLoad.TryGetValue("exp", out object Value);
                //    var TokenExpireTime = DefaultDatetime.AddSeconds(Convert.ToInt64(Value));
                //    var CurrentDatetime = DateTime.UtcNow;

                //    //Token Timestamp older than Current Timestamp, return -1
                //    if (TokenExpireTime.CompareTo(CurrentDatetime) < 0) AccessObject.IsExpiredToken = true;
                //}
            }
        }

        private void InitializeAccess()
        {
            // Identify token format
            AccessTokenValid = AccessHandler.CanReadToken(access_token);

            if (AccessTokenValid)
            {
                AccessToken = AccessHandler.ReadJwtToken(access_token);
                Token AccessObject = new Token();
                var AccessPayLoad = AccessToken.Payload;

                if (AccessPayLoad.ContainsKey("token_use"))     // Token type
                {
                    AccessPayLoad.TryGetValue("token_use", out object Value);
                    if (Value.ToString() == "access")
                    {
                        IsValidToken = true;
                    }
                    else
                    {
                        AccessTokenValid = false;
                        return;
                    }
                    AccessObject.TokenType = Value.ToString();
                }
                if (AccessPayLoad.ContainsKey("email"))         // Customer Email
                {
                    AccessPayLoad.TryGetValue("email", out object Value);
                    AccessObject.UserEmail = Value.ToString();
                    Email = Value.ToString();
                }
                if (AccessPayLoad.ContainsKey("sub"))           // Customer Guid
                {
                    AccessPayLoad.TryGetValue("sub", out object Value);
                    //if (Guid.TryParse(Value.ToString(), out Guid guid)) //WilliamHo 20181030 
                    //{
                    //    AccessObject.UserGuid = guid;
                    //    AccessObject.Username = guid.ToString();
                    //}
                    //else
                    //{
                    //    AccessObject.UserGuid = Guid.Empty;
                    //    //IsValidToken = false;
                    //}
                    AccessObject.UserGuid = Value.ToString(); //WilliamHo 20181030 
                    AccessObject.Username = Value.ToString(); //WilliamHo 20181030 

                }
                if (AccessPayLoad.ContainsKey("client_id"))         // client id
                {
                    AccessPayLoad.TryGetValue("client_id", out object Value);
                    accessTokenClientId = Value.ToString();
                }
                if (AccessPayLoad.ContainsKey("scope"))   // scope is the target path/url that token to reach
                {
                    AccessPayLoad.TryGetValue("scope", out object Value);
                    Scope = Value.ToString();
                }
                if (AccessPayLoad.ContainsKey("jti"))           // JWT ID (unique identifier for this token)
                {
                    AccessPayLoad.TryGetValue("jti", out object Value);
                    //if (Guid.TryParse(Value.ToString(), out Guid guid)) //WilliamHo 20181030 
                    //    AccessObject.JwtTokenGuid = guid;
                    AccessObject.JwtTokenGuid = Value.ToString(); //WilliamHo 20181030 
                }
                //No need to check again the token expiry as ValidateToken(string token) will handle this part
                //if (AccessPayLoad.ContainsKey("exp"))           // Token expiry
                //{
                //    AccessPayLoad.TryGetValue("exp", out object Value);
                //    var TokenExpireTime = DefaultDatetime.AddSeconds(Convert.ToInt64(Value));
                //    var CurrentDatetime = DateTime.UtcNow;

                //    //Token Timestamp older than Current Timestamp, return -1
                //    if (TokenExpireTime.CompareTo(CurrentDatetime) < 0) AccessObject.IsExpiredToken = true;
                //}
                AccessTokenObject = AccessObject;
            }
        }
    }
}
