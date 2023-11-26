using Nop.Core.Enumeration;

namespace Nop.Plugin.Api.Enumeration
{
    #region ProfileController [Global] Enumeration
    /// <summary>
    /// ProfileController [Global] Enumeration
    /// </summary>
    public enum ProfileController_Global
    {
        /// <summary>
        /// 3011 = Contact Number is not numerical number
        /// </summary>
        [Description("Contact Number is not numerical number")]
        notNumericalNumber = 3011,

        /// <summary>
        /// 3012 = Please enter contact number
        /// </summary>
        [Description("Please enter contact number")]
        contactNumberIsEmpty = 3012,

        /// <summary>
        /// 3022 = Country code is null
        /// </summary>
        [Description("Country code is null")]
        countryCodeFormatInvalid = 3022,

        /// <summary>
        /// 4000 = The User does not in the system
        /// </summary>
        [Description("The user does not in the system")]
        userNotInSystem = 4000,

        /// <summary>
        /// 1 = Mobile Phone Number
        /// </summary>
        [Description("1")]
        mobile,
    }
    #endregion


    //Tony Liew 20190308 RDT-69 \/
    /// <summary>
    /// Profile Controller [ChangePassword] Enumeration
    /// </summary>
    #region ProfileController [ChangePassword] Enumeration
    public enum ProfileController_ChangePassword
    {
        /// <summary>
        /// 3002 = Email Is Not Provided
        /// </summary>
        [Description("Email is not entered")]
        EmailIsNotProvided = 3002,

        /// <summary>
        /// 3003 = Password Is Not Provided
        /// </summary>
        [Description("Password is not entered")]
        PasswordIsNotProvided = 3003,

        /// <summary>
        /// 3004 = Email Not Found
        /// </summary>
        [Description("The specified email could not be found")]
        EmailNotFound = 3004,

        /// <summary>
        /// 3005 = Old Password Doesnt Match
        /// </summary>
        [Description("Old password doesn't match")]
        OldPasswordDoesntMatch = 3005,

        /// <summary>
        /// 3006 = Password Matches With Previous
        /// </summary>
        [Description("You entered the password that is the same as one of the last passwords you used. Please create a new password.")]
        PasswordMatchesWithPrevious = 3006,

        /// <summary>
        /// 3008 = Please enter new password
        /// </summary>
        [Description("Please enter new password")]
        enterNewPassword = 3008,

        /// <summary>
        /// 3009 = Please enter old password
        /// </summary>
        [Description("Please enter old password")]
        enterOldPassword = 3009,

        /// <summary>
        /// 3010 = Current Password and New Password are same
        /// </summary>
        [Description("Current Password and New Password are same")]
        oldNewPasswordSame = 3010,

        /// <summary>
        /// 3014 = Confirm New Password and New Password are not same
        /// </summary>
        [Description("Confirm New Password and New Password are not same")]
        notSameNewPassword = 3014,

        /// <summary>
        /// 3015 = Confirm New Password and New Password are not same
        /// </summary>
        [Description("Confirm New Password and New Password are not same")]
        enterConfirmNewPassword = 3015,

        /// <summary>
        /// 3016 = Confirm New Password and Current Password are same
        /// </summary>
        [Description("Confirm New Password and Current Password are same")]
        oldAndConfrimPasswordNotSame = 3016,
    }
    #endregion
    //Tony Liew 20190308 RDT-69 /\


    #region ProfileController [AddContactNumber] Enumeration
    /// <summary>
    /// ProfileController [AddContactNumber] Enumeration
    /// </summary>
    public enum ProfileController_AddContactNumber
    {
    }
    #endregion

    #region ProfileController [ChangeContactNumber] Enumeration
    /// <summary>
    /// ProfileController [ChangeContactNumber] Enumeration
    /// </summary>
    public enum ProfileController_ChangeContactNumber
    {
        /// <summary>
        /// 3018 = No Contact Id
        /// </summary>
        [Description("No Contact Id")]
        NoContactNumberId = 3019,

        /// <summary>
        /// 3024 = The Contact Number Enter Same As Previous
        /// </summary>
        [Description("The Contact Number Entered Same As Previous")]
        SameContactNumber = 3024,
    }
    #endregion

    #region ProfileController [UpdateLanguage] Enumeration
    /// <summary>
    /// ProfileController [UpdateLanguage] Enumeration
    /// </summary>
    public enum ProfileController_UpdateLanguage
    {
        /// <summary>
        /// 3012 = Invalid language code
        /// </summary>
        [Description("Invalid language code")]
        InvalidLanguageCode = 3012,
    }
    #endregion

    #region ProfileController [UpdateIdentityNumber] Enumeration
    /// <summary>
    /// ProfileController [UpdateIdentityNumber] Enumeration
    /// </summary>
    public enum ProfileController_UpdateIdentityNumber
    {
        /// <summary>
        /// 3020 = Identity Number already exist
        /// </summary>
        [Value("Identity Number already exist")]
        [Description("Identity Number already exist")]
        DuplicateIdentityNumber = 3020,

        /// <summary>
        /// 3021 = Identity Number does not exist
        /// </summary>
        [Value("Identity Number does not exist")]
        [Description("Identity Number does not exist")]
        IdentityNumberNotExist = 3021,
    }
    #endregion
}