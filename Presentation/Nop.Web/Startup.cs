using System;
using System.IO;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nop.Web.Framework.Infrastructure.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Nop.Core.Infrastructure;
using Nop.Core.Configuration;
using System.Security.Principal;
using Nop.Core.Data;
using Nop.Core.Domain.Configuration;
using Nop.Web.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Nop.Core;
using Nop.Core.Enumeration;
using Nop.Web.Models.Api;
using Nop.Web.Infrastructure;

namespace Nop.Web
{
    /// <summary>
    /// Represents startup class of application
    /// </summary>
    public class Startup
    {
        #region Properties

        /// <summary>
        /// Get configuration root of the application
        /// </summary>
        public IConfigurationRoot Configuration { get; }
        public readonly ApiConfiguration apiConfig;

        public static ILoggerRepository repository_API { get; set; }

        
        #endregion

        #region Ctor

        public Startup(IHostingEnvironment environment)
        {
            using (StreamReader r = new StreamReader(@"api-settings.json"))
            {
                string json = r.ReadToEnd();
                apiConfig = JsonConvert.DeserializeObject<ApiConfiguration>(json);
            }

            //create configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            repository_API = LogManager.CreateRepository("NETCoreRepository_API");
            XmlConfigurator.Configure(repository_API, new FileInfo("log4net_API.config"));
        }

        #endregion

        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            if (apiConfig.Settings.EnabledSwaggerUI)
            {
                // Register the Swagger generator, defining 1 or more Swagger documents
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "Residential Api", Version = "v1" });
                    //avoid identical schemaIds detected conflict by swagger
                    c.CustomSchemaIds(x => x.FullName); 
                    //var path = string.Format(@"{0}Nop.Web.xml", System.AppDomain.CurrentDomain.BaseDirectory);
                    c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Nop.Plugin.Api.xml"));
                    //c.OperationFilter<DescriptionOperationFilter>();
                    c.DescribeAllEnumsAsStrings();
                });
            }

            string[] origins = apiConfig.CORS.Origins;
            string[] methods = apiConfig.CORS.Methods;
            string[] headers = apiConfig.CORS.Headers;

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllHeaders",
                      builder =>
                      {
                          builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                                 .WithOrigins(origins)
                                 .WithHeaders(headers)
                                 .WithMethods(methods);
                      });
            });

            return services.ConfigureApplicationServices(Configuration);
        }

        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            if (apiConfig.Settings.EnabledSwaggerUI)
            {
                //var test = EngineContext.Current.Resolve<NopConfig>()
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                //application.UseSwagger();
                application.UseSwagger(c =>
                {
                    c.RouteTemplate = "residential/{documentName}/api-docs";
                });

                //Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                application.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/residential/v1/api-docs", "Residential API V1");
                    //c.RoutePrefix = "residential/swagger";
                    c.DocExpansion(DocExpansion.None);
                });
            }

            // WilliamHo 20181015 Enable CORS with CORS Middleware
            application.UseCors("AllowAllHeaders");

            // security - check for token
            application.UseMiddleware<AuthenticationMiddleware>();

            application.ConfigureRequestPipeline();
        }

        public class AuthenticationMiddleware
        {
            private const string PATH = "~/App_Data/ApiConfiguration/Crypto/";
            private const string SHARED_KEY_FILE = "SharedKey.dat";
            private readonly RequestDelegate _next;

            //protected ILogger Logger { get; }
            private ILog log;

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
                    using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        buffer = reader.ReadBytes(100); //try to read bytes up to 100 size of byte array
                        reader.Close();
                    }
                    if (buffer.Length > 0)
                        return Convert.ToBase64String(buffer);
                }
                return string.Empty;
            }

            public AuthenticationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
            {
                _next = next;

                //Logger = loggerFactory.CreateLogger(GetType().Namespace);
                //Logger.LogInformation("created AuthenticationMiddleware");
                log = LogManager.GetLogger(Startup.repository_API.Name, typeof(Startup));
                log.Info("created AuthenticationMiddleware");
            }

            public async Task Invoke(HttpContext context)
            {
                // check all traffic from api then only check authorization token
                string requestPath = string.Empty;
                requestPath = context.Request.Path.Value.ToLower();
                if (!requestPath.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
                {
                    // not from api, so dont do anything and return
                    await _next.Invoke(context);

                    return;
                }

                // check for specific path that not necessary to have token
                if (requestPath == "/api/account/login" || requestPath == "/api/account/register" || 
                    requestPath == "/api/profile/faq" || requestPath == "/api/profile/app-rules" ||
                    requestPath == "/api/general/setting" || requestPath == "/api/visitor/clocktime")
                {
                    await _next.Invoke(context);
                    return;
                }

                var response = new ApiResponse();

                response.meta.code = (int)RES_GlobalEnum.success;
                response.meta.message = RES_GlobalEnum.success.ToDescription<RES_GlobalEnum>();

                string authHeader = context.Request.Headers["Authorization"];
                //if (string.IsNullOrEmpty(authHeader))
                if (authHeader != null)
                {
                    try
                    {
                        // get access token
                        string accesstoken = string.Empty;
                        if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                            accesstoken = authHeader.Substring("Bearer ".Length).Trim();

                        // validate token
                        Meta returnResult = ValidateToken(accesstoken, false);
                        if (returnResult.code != (int)RES_GlobalEnum.success)
                        {
                            // if got error message means token not valid
                            //context.Response.StatusCode = 401; //Unauthorized
                            response.meta = returnResult;
                            //response.meta.message = "Authorization error : " + response.meta.message;
                        }
                        else
                        {
                            // read token values
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var jwtSecurityToken = new JwtSecurityToken();
                            bool tokenValid = tokenHandler.CanReadToken(accesstoken);
                            if (tokenValid)
                            {
                                jwtSecurityToken = tokenHandler.ReadJwtToken(accesstoken);

                                foreach (var item in jwtSecurityToken.Payload.Claims)
                                    context.Request.Headers.Add(item.Type, item.Value);

                                string logInfo = requestPath + " - " + context.Request.Headers["email"];
                                log.Debug(logInfo);
                                await _next.Invoke(context);
                            }
                            else
                            {
                                //context.Response.StatusCode = 401; //Unauthorized

                                response.meta.code = (int)RES_GlobalEnum.tokenEmpty;
                                response.meta.message = RES_GlobalEnum.tokenEmpty.ToValue<RES_GlobalEnum>();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //context.Response.StatusCode = 401;
                        //Unauthorized
                        response.meta.code = (int)RES_GlobalEnum.unhandledException;
                        response.meta.message = "Authorization error : " + ex.Message;
                    }
                }
                else
                {
                    // no authorization header
                    //context.Response.StatusCode = 401; //Unauthorized
                    response.meta.code = (int)RES_GlobalEnum.noAuthorizationHeader;
                    response.meta.message = RES_GlobalEnum.noAuthorizationHeader.ToValue<RES_GlobalEnum>();
                }

                if (response.meta.code != (int)RES_GlobalEnum.success)
                {
                    ApiResponseHeader apiresponse = new ApiResponseHeader();
                    apiresponse.SetResponseHeader(context);
                    var jsonString = JsonConvert.SerializeObject(response);
                    await context.Response.WriteAsync(jsonString, Encoding.UTF8);

                    //Logger.LogInformation(response.meta.message);
                    log.Warn(response.meta.message);

                    return;
                }
            }

            private Meta ValidateToken(string authToken, bool expiryFlag = true, bool audienceFlag = false, bool actorFlag = false, bool issuerFlag = false)
            {
                var returnResult = new Meta();
                returnResult.code = (int)RES_GlobalEnum.success;
                returnResult.message = RES_GlobalEnum.success.ToDescription<RES_GlobalEnum>();

                var JwtTokenHandler = new JwtSecurityTokenHandler();
                if (JwtTokenHandler.CanReadToken(authToken))
                {
                    //string tokenSecretKey = "TokenSecretKey"; //_appSettings.TokenSecretKey);
                    string tokenSecretKey = ReadBufferFile(CommonHelper.MapPath($"{PATH}{SHARED_KEY_FILE}"));
                    var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSecretKey)); 

                    var tokenIssuer = "GGIT Sdn Bhd"; //_appSettings.TokenIssuer;

                    try
                    {
                        var validationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = tokenIssuer,             
                            IssuerSigningKey = issuerSigningKey,        
                            ValidateLifetime = expiryFlag,
                            ValidateAudience = audienceFlag,
                            ValidateActor = actorFlag,
                            ValidateIssuer = issuerFlag
                        };

                        try
                        {
                            SecurityToken validatedToken;
                            //IPrincipal principal = JwtTokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                            ClaimsPrincipal claimsPrincipal = JwtTokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                        }
                        catch (SecurityTokenExpiredException SecExpiryEx)
                        {
                            //Token is expired
                            returnResult.code = (int)RES_GlobalEnum.tokenExpired;
                            returnResult.message = RES_GlobalEnum.tokenExpired.ToValue<RES_GlobalEnum>();
                            Console.WriteLine(SecExpiryEx.Message);
                        }
                        catch (SecurityTokenSignatureKeyNotFoundException SecKeyEx)
                        {
                            //Signature validation failed, token's kid not match with publickey's kid
                            returnResult.code = (int)RES_GlobalEnum.invalidSignature;
                            returnResult.message = RES_GlobalEnum.invalidSignature.ToValue<RES_GlobalEnum>();
                            Console.WriteLine(SecKeyEx.Message);
                        }
                        catch (SecurityTokenInvalidAudienceException AudEx)
                        {
                            //Client Id mismatch
                            returnResult.code = (int)RES_GlobalEnum.invalidClientId;
                            returnResult.message = RES_GlobalEnum.invalidClientId.ToValue<RES_GlobalEnum>();
                            Console.WriteLine(AudEx.Message);
                        }
                        catch (Exception ex)
                        {
                            returnResult.code = (int)RES_GlobalEnum.unhandledException;
                            returnResult.message = "Authorization error : " + ex.Message;
                            Console.WriteLine(ex.Message);
                        }

                    }
                    catch (Exception ex)
                    {
                        returnResult.code = (int)RES_GlobalEnum.unhandledException;
                        returnResult.message = "Authorization error : " + ex.Message;
                        Console.WriteLine(ex.Message);
                    }
                }

                return returnResult;
            }

        }
    }
}