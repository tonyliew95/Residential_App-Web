using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Tony Liew 20190307 RDT-118 \/
namespace Nop.Core.Enumeration
{
    /// <summary>
    /// An enumeration file that store all the Global enum for entire project
    /// </summary>
    public enum RES_GlobalEnum
    {
        /// <summary>
        /// 1000 = Success
        /// </summary>
        [ValueAttribute("S")]
        [Description("Success")]
        success = 1000,

        /// <summary>
        /// 2000 = session timeout
        /// </summary>
        [ValueAttribute("session timeout")]
        [Description("session timeout")]
        sessionTimeout = 2000,

        /// <summary>
        /// 2001 = unable to verify signature
        /// </summary>
        [ValueAttribute("unable to verify signature")]
        [Description("unable to verify signature")]
        invalidSignature = 2001,

        /// <summary>
        /// 2002 = email or password unable to verify
        /// </summary>
        [ValueAttribute("email or password unable to verify")]
        [Description("email or password unable to verify")]
        invalidEmailOrPassword = 2002,


        /// <summary>
        /// 2003 = Invalid User
        /// </summary>
        [ValueAttribute("invalid User")]
        [Description("invalid User")]
        invalidUser = 2003,

        #region Authorization

        /// <summary>
        /// 2005 = Authorization error : Token is expired 
        /// </summary>
        [DescriptionAttribute("Authorization error : Token is expired")]
        [ValueAttribute("Authorization error : Token is expired")]
        tokenExpired = 2005,

        /// <summary>
        /// 2006 = Authorization error : Invalid Client Id 
        /// </summary>
        [DescriptionAttribute("Authorization error : Invalid Client Id")]
        [ValueAttribute("Authorization error : Invalid Client Id")]
        invalidClientId = 2006,

        /// <summary>
        /// 2007 = Authorization error : Token is empty 
        /// </summary>
        [DescriptionAttribute("Authorization error : Token is empty")]
        [ValueAttribute("Authorization error : Token is empty")]
        tokenEmpty = 2007,

        /// <summary>
        /// 2008 = No authorization header 
        /// </summary>
        [DescriptionAttribute("No authorization header")]
        [ValueAttribute("No authorization header")]
        noAuthorizationHeader = 2008,

        #endregion

        /// <summary>
        /// 2004 = No request is passed into API
        /// </summary>
        [ValueAttribute("No request is passed into API")]
        [Description("No request is passed into API")]
        noRequest = 2004,

        /// <summary>
        /// 2009 = No organization Id in request
        /// </summary>
        [ValueAttribute("No organization Id in request")]
        [Description("No organization Id in request")]
        noOrgId = 2009,

        /// <summary>
        /// 2010 = Invalid property id
        /// </summary>
        [Value("Invalid property id")]
        [Description("Invalid property id")]
        invalidPropertyId = 2010,

        /// <summary>
        /// 2011 = Invalid Identity Type
        /// </summary>
        [Value("Invalid Identity Type")]
        [Description("Invalid Identity Type")]
        invalidIdentityType = 2011,

        /// <summary>
        /// 2012 = Invalid Identity Number
        /// </summary>
        [Value("Invalid Identity Number")]
        [Description("Invalid Identity Number")]
        invalidIdentityNumber = 2012,

        /// <summary>
        /// 2013 = Invalid Activation Code
        /// </summary>
        [Value("Invalid Activation Code")]
        [Description("Invalid Activation Code")]
        invalidActivationCode = 2013,

        /// <summary>
        /// 2014 = Invalid Customer Id
        /// </summary>
        [Value("Invalid Customer Id")]
        [Description("Invalid Customer Id")]
        invalidCustomerId = 2014,

        /// <summary>
        /// 2015 = Invalid User Account Id
        /// </summary>
        [Value("Invalid User Account Id")]
        [Description("Invalid User Account Id")]
        invalidAccountId = 2015,

        /// <summary>
        /// 2016 = Invalid Visitor Id
        /// </summary>
        [Description("Invalid Visitor Id")]
        invalidVisitorId = 2016,

        /// <summary>
        /// 4999 = unhandled exception error
        /// </summary>
        [ValueAttribute("unhandled exception error")]
        [Description("unhandled exception error")]
        unhandledException = 4999,
    }
    //Tony Liew 20190307 RDT-118 /\
    
}
