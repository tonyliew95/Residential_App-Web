using Nop.Core.Domain.Customers;

namespace Nop.Services.Customers
{
    /// <summary>
    /// Customer registration interface
    /// </summary>
    public partial interface ICustomerRegistrationService
    {
        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        CustomerLoginResults ValidateCustomer(string usernameOrEmail, string password , string deviceUuid = "");//Tony Liew 20190311 RDT-69

        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        CustomerRegistrationResult RegisterCustomer(CustomerRegistrationRequest request, string deviceUuid = "");//Tony Liew 20190311 RDT-69

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        ChangePasswordResult ChangePassword(ChangePasswordRequest request , string deviceUuid = ""); //Tony Liew 20190308 RDT-69

        /// <summary>
        /// Change transaction password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        ChangePasswordResult ChangeTransactionPassword(ChangePasswordRequest request); //Jerry 20180814 MSP-45

        /// <summary>
        /// Sets a user email
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="newEmail">New email</param>
        /// <param name="requireValidation">Require validation of new email address</param>
        void SetEmail(Customer customer, string newEmail, bool requireValidation);

        /// <summary>
        /// Sets a customer username
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="newUsername">New Username</param>
        void SetUsername(Customer customer, string newUsername);

        /// <summary>
        /// Password match
        /// </summary>
        /// <param name="customerPassword"></param>
        /// <param name="enteredPassword"></param>
        /// <returns></returns>
        /// 20180806 WilliamHo
        bool PasswordsMatch(CustomerPassword customerPassword, string enteredPassword , string deviceUuid = "");//Tony Liew 20190311 RDT-69

        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        bool PasswordsMatch(CustomerPassword_History customerPassword, string enteredPassword, string deviceUuid = ""); //Tony Liew 20190311 RDT-69

        /// <summary>
        /// Transaction Passwords Match
        /// </summary>
        /// <param name="customerTransactionPassword"></param>
        /// <param name="enteredPassword"></param>
        /// <returns></returns>
        bool TransactionPasswordsMatch(CustomerTransactionPassword customerTransactionPassword, string enteredPassword);
    }
}