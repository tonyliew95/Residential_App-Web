using Nop.Core.Enumeration;

namespace Nop.Plugin.Api.Enumeration
{
    /// <summary>
    /// Usage : ActionCode, 0  = Success case
    /// Initial number for AccountController 2XXX
    /// For each action method will follow standard for enum numbering, e.g: 2Xxx
    /// </summary>
    
    /// <summary>
    /// An enumeration file that store all the Global enum for AccountController
    /// </summary>
    #region AccountController [Global] Enumeration
    public enum AccountController_GlobalResults
    {
        /// <summary>
        /// 2001 = Invalid action code
        /// </summary>
        [DescriptionAttribute("Invalid action code.")]
        [ValueAttribute("API_AccountController_GlobalResults.InvalidActionCode")]
        InvalidActionCode = 2001,
        /// <summary>
        /// 2002 = Missing action code
        /// </summary>
        [DescriptionAttribute("Missing action code.")]
        [ValueAttribute("API_AccountController_GlobalResults.MissingActionCode")]
        MissingActionCode = 2002,
        /// <summary>
        /// 2003 = Missing username/password
        /// </summary>
        [DescriptionAttribute("Username or Password is missing.")]
        [ValueAttribute("API_AccountController_GlobalResults.MissingUsernamePassword")]
        MissingUsernamePassword = 2003,
        /// <summary>
        /// 2004 = Account not found
        /// </summary>
        [DescriptionAttribute("Account not found.")]
        [ValueAttribute("API_AccountController_GlobalResults.AccountNotFound")]
        AccountNotFound = 2004,
        /// <summary>
        /// 2005 = Token has expired
        /// </summary>
        [DescriptionAttribute("Token has expired.")]
        [ValueAttribute("API_AccountController_GlobalResults.TokenExpired")]
        TokenExpired = 2005,
        /// <summary>
        /// 2006 = Invalid Language Code
        /// </summary>
        [DescriptionAttribute("Invalid Language Code format.")]
        [ValueAttribute("API_AccountController_GlobalResults.InvalidLanguageCode")]
        InvalidLanguageCode = 2006,
        /// <summary>
        /// 2007 = Fail to update language code.
        /// </summary>
        [DescriptionAttribute("Failed to update language code.")]
        [ValueAttribute("API_AccountController_GlobalResults.UpdateLanguageFail")]
        UpdateLanguageFailed = 2007,
        /// <summary>
        /// <summary>
        /// 0 = Token Generate Success successful
        /// </summary>
        [DescriptionAttribute("Success.")]
        [ValueAttribute("API_AccountController_GlobalResults.Successful")]
        Successful = 0,
    }
    #endregion

    // WKK 20190306 RDT-63 [API] Account - login
    /// <summary>
    /// An enumeration file that store all the Login enum for AccountController
    /// </summary>
    #region AccountController [Login] Action Results
    public enum AccountController_LoginResults
    {
        /// <summary>
        /// 2100 = Customer does not exist (Username)
        /// </summary>
        [DescriptionAttribute("Account not exist.")]
        [ValueAttribute("API_AccountController_LoginResults.CustomerNotExist")]
        CustomerNotExist = 2100,

        /// <summary>
        /// 2101 = Incorrect password
        /// </summary>
        [DescriptionAttribute("Incorrect password.")]
        [ValueAttribute("API_AccountController_LoginResults.WrongPassword")]
        WrongPassword = 2101,

        /// <summary>
        /// 2102 = Account have not been activated
        /// </summary>
        [DescriptionAttribute("Account not active.")]
        [ValueAttribute("API_AccountController_LoginResults.NotActive")]
        NotActive = 2102,

        /// <summary>
        /// 2103 = Customer has been deleted
        /// </summary>
        [DescriptionAttribute("Account deleted.")]
        [ValueAttribute("API_AccountController_LoginResults.Deleted")]
        Deleted = 2103,

        /// <summary>
        /// 2104 = Locked out
        /// </summary>
        [DescriptionAttribute("Your account has been locked out.")]
        [ValueAttribute("API_AccountController_LoginResults.LockedOut")]
        LockedOut = 2104,
    }

    #endregion

    /// <summary>
    /// An enumeration file that store all the Logout enum for AccountController
    /// </summary>
    #region AccountController [Logout] Action Results
    public enum AccountController_LogoutResults
    {
        /// <summary>
        /// 0 = Login successful
        /// </summary>
        [DescriptionAttribute("Success.")]
        [ValueAttribute("API_AccountController_LogoutResults.Successful")]
        Successful = 0,
        /// <summary>
        /// 2201 = Fail to logout
        /// </summary>
        [DescriptionAttribute("Logout fail.")]
        [ValueAttribute("API_AccountController_LogoutResults.LogoutFail")]
        LogoutFail = 2201,

    }
    #endregion

    // WKK 20190312 RDT-61 [API] Account - registration
    /// <summary>
    /// An enumeration file that store all the Registration Account enum for AccountController
    /// </summary>
    #region AccountController [Registration] Result
    public enum AccountController_RegistrationResults 
    {
        /// <summary>
        /// 2200 = Email Is Not Provided
        /// </summary>
        [DescriptionAttribute("Email Is Not Provided")]
        [ValueAttribute("Email Is Not Provided")]
        emailNotProvided = 2200,

        /// <summary>
        /// 2201 = Email Not Valid
        /// </summary>
        [DescriptionAttribute("Email Not Valid")]
        [ValueAttribute("Email Not Valid")]
        emailNotValid = 2201,

        /// <summary>
        /// 2202 = Password Is Not Provided
        /// </summary>
        [DescriptionAttribute("Password Is Not Provided")]
        [ValueAttribute("Password Is Not Provided")]
        passwordIsNotProvided = 2202,

        /// <summary>
        /// 2203 = Username Is Not Provided
        /// </summary>
        [DescriptionAttribute("Username Is Not Provided")]
        [ValueAttribute("Username Is Not Provided")]
        usernameIsNotProvided = 2203,

        /// <summary>
        /// 2204 = User has been registered
        /// </summary>
        [DescriptionAttribute("User has been registered")]
        [ValueAttribute("User has been registered")]
        userRegistered = 2204,

        /// <summary>
        /// 2205 = Email has been registered
        /// </summary>
        [DescriptionAttribute("Email has been registered.")]
        [ValueAttribute("Email has been registered")]
        emailRegistered = 2205,

        /// <summary>
        /// 2206 = Error saving phone number
        /// </summary>
        [DescriptionAttribute("Error saving phone number")]
        [ValueAttribute("Error saving phone number")]
        errorSavingPhone = 2206,

        /// <summary>
        /// 2207 = Register User Failed
        /// </summary>
        [DescriptionAttribute("Register User Failed")]
        [ValueAttribute("Register User Failed")]
        registerFailed = 2207,

    }

    #endregion

}

