using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Msp.Interface;
using Nop.Core.Domain.Msp.Member;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Core.Domain.Orders;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Nop.Services.Customers
{
    /// <summary>
    /// Customer service interface
    /// </summary>
    public partial interface ICustomerService
    {
        #region Customers

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="affiliateId">Affiliate identifier</param>
        /// <param name="vendorId">Vendor identifier</param>
        /// <param name="customerRoleIds">A list of customer role identifiers to filter by (at least one match); pass null or empty list in order to load all customers; </param>
        /// <param name="email">Email; null to load all customers</param>
        /// <param name="username">Username; null to load all customers</param>
        /// <param name="firstName">First name; null to load all customers</param>
        /// <param name="lastName">Last name; null to load all customers</param>
        /// <param name="dayOfBirth">Day of birth; 0 to load all customers</param>
        /// <param name="monthOfBirth">Month of birth; 0 to load all customers</param>
        /// <param name="company">Company; null to load all customers</param>
        /// <param name="phone">Phone; null to load all customers</param>
        /// <param name="zipPostalCode">Phone; null to load all customers</param>
        /// <param name="ipAddress">IP address; null to load all customers</param>
        /// <param name="loadOnlyWithShoppingCart">Value indicating whether to load customers only with shopping cart</param>
        /// <param name="sct">Value indicating what shopping cart type to filter; userd when 'loadOnlyWithShoppingCart' param is 'true'</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customers</returns>
         
        // wailiang 20181213 MDT-133 \/
        // Tony Liew 20181030 MSP-411 \/
        IPagedList<Customer> GetAllCustomers(bool isCS = false, bool isCSAdmin = false, bool isAdmin = false, bool isAOD = false, bool isFinance = false, int currentCustomerID = 0,
            DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int affiliateId = 0, int vendorId = 0,
            int[] customerRoleIds = null, string email = null, string username = null,
            string firstName = null, string lastName = null,
            int dayOfBirth = 0, int monthOfBirth = 0,
            string company = null, string phone = null, string zipPostalCode = null,
            string ipAddress = null, bool loadOnlyWithShoppingCart = false, ShoppingCartType? sct = null,
            int pageIndex = 0, int pageSize = int.MaxValue);
        // Tony Liew 20181030 MSP-411 /\
        // wailiang 20181213 MDT-133 /\

        /// <summary>
        /// Gets online customers
        /// </summary>
        /// <param name="lastActivityFromUtc">Customer last activity date (from)</param>
        /// <param name="customerRoleIds">A list of customer role identifiers to filter by (at least one match); pass null or empty list in order to load all customers; </param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customers</returns>
        IPagedList<Customer> GetOnlineCustomers(DateTime lastActivityFromUtc,
            int[] customerRoleIds, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        void DeleteCustomer(Customer customer);

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A customer</returns>
        Customer GetCustomerById(int customerId);

        /// <summary>
        /// Get customers by identifiers
        /// </summary>
        /// <param name="customerIds">Customer identifiers</param>
        /// <returns>Customers</returns>
        IList<Customer> GetCustomersByIds(int[] customerIds);

        /// <summary>
        /// Gets a customer by GUID
        /// </summary>
        /// <param name="customerGuid">Customer GUID</param>
        /// <returns>A customer</returns>
        Customer GetCustomerByGuid(Guid customerGuid);

        /// <summary>
        /// Get customer by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Customer</returns>
        Customer GetCustomerByEmail(string email);

        /// <summary>
        /// Get customer by system role
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Customer</returns>
        Customer GetCustomerBySystemName(string systemName);

        /// <summary>
        /// Get customer by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Customer</returns>
        Customer GetCustomerByUsername(string username);

        /// <summary>
        /// Insert a guest customer
        /// </summary>
        /// <returns>Customer</returns>
        Customer InsertGuestCustomer();

        /// <summary>
        /// Insert a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        void InsertCustomer(Customer customer);

        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        void UpdateCustomer(Customer customer);

        /// <summary>
        /// Reset data required for checkout
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="clearCouponCodes">A value indicating whether to clear coupon code</param>
        /// <param name="clearCheckoutAttributes">A value indicating whether to clear selected checkout attributes</param>
        /// <param name="clearRewardPoints">A value indicating whether to clear "Use reward points" flag</param>
        /// <param name="clearShippingMethod">A value indicating whether to clear selected shipping method</param>
        /// <param name="clearPaymentMethod">A value indicating whether to clear selected payment method</param>
        void ResetCheckoutData(Customer customer, int storeId,
            bool clearCouponCodes = false, bool clearCheckoutAttributes = false,
            bool clearRewardPoints = true, bool clearShippingMethod = true,
            bool clearPaymentMethod = true);

        /// <summary>
        /// Delete guest customer records
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="onlyWithoutShoppingCart">A value indicating whether to delete customers only without shopping cart</param>
        /// <returns>Number of deleted customers</returns>
        int DeleteGuestCustomers(DateTime? createdFromUtc, DateTime? createdToUtc, bool onlyWithoutShoppingCart);

        /// <summary>
        /// Get Customer Username by Id
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        string GetCustomerUsernameById(int CustomerId);

        #endregion

        #region Customer roles

        /// <summary>
        /// Delete a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        void DeleteCustomerRole(CustomerRole customerRole);

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="customerRoleId">Customer role identifier</param>
        /// <returns>Customer role</returns>
        CustomerRole GetCustomerRoleById(int customerRoleId);

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        CustomerRole GetCustomerRoleBySystemName(string systemName);

        /// <summary>
        /// Gets all customer roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Customer roles</returns>
        IList<CustomerRole> GetAllCustomerRoles(bool showHidden = false);

        /// <summary>
        /// Inserts a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        void InsertCustomerRole(CustomerRole customerRole);

        /// <summary>
        /// Updates the customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        void UpdateCustomerRole(CustomerRole customerRole);

        #endregion

        #region Customer passwords

        /// <summary>
        /// Gets customer passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of customer passwords</returns>
        IList<CustomerPassword> GetCustomerPasswords(int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null);

        /// <summary>
        /// Gets customer passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of customer passwords</returns>
        IList<CustomerPassword_History> GetPreviousCustomerPasswords(int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null); //Tony Liew 20190308 RDT-69

        /// <summary>
        /// Get current customer password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer password</returns>
        CustomerPassword GetCurrentPassword(int customerId);

        /// <summary>
        /// Get current customer password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer password</returns>
        CustomerPassword_History GetPreviousPassword(int customerId); //Tony Liew 20190308 RDT-69

        /// <summary>
        /// Insert a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        void InsertCustomerPassword(CustomerPassword customerPassword);

        /// <summary>
        /// Update a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        void UpdateCustomerPassword(CustomerPassword customerPassword);

        #endregion

        #region Customer transaction passwords

        /// <summary>
        /// Gets customer transaction passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning transaction passwords; pass null to load all records</param>
        /// <returns>List of customer transaction passwords</returns>
        IList<CustomerTransactionPassword> GetCustomerTransactionPasswords(int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null); //Jerry 20180814 MSP-45

        /// <summary>
        /// Get current customer transaction password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer transaction password</returns>
        CustomerTransactionPassword GetCurrentTransactionPassword(int customerId); //Jerry 20180814 MSP-45

        /// <summary>
        /// Insert a customer transaction password
        /// </summary>
        /// <param name="customerTransactionPassword">Customer transaction password</param>
        void InsertCustomerTransactionPassword(CustomerTransactionPassword customerTransactionPassword); //Jerry 20180814 MSP-45

        /// <summary>
        /// Update a customer transaction password
        /// </summary>
        /// <param name="customerTransactionPassword">Customer transaction password</param>
        void UpdateCustomerTransactionPassword(CustomerTransactionPassword customerTransactionPassword); //Jerry 20180814 MSP-45

        #endregion

        #region Membership

        #region Member Tree

        /// <summary>
        /// Gets a customer's member tree
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A member tree</returns>
        MSP_MemberTree GetMemberTree(int customerId); //Jerry 20180814 MSP-45

        /// <summary>
        /// Update Member's withdrawal limits 
        /// </summary>
        /// <param name="member"></param>
        void UpdateMemberWithdrawalLimit(MSP_MemberTree member); //Tony Liew 20181121 MDT-8  

        #endregion


        #region Security Question And Answer

        /// <summary>
        /// Gets security question and answer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>List of Security question and answer</returns>
        //IList GetSecurityQuestionAndAnswer(int customerId); //Jerry 20180814 MSP-45 //Atiqah 20190130 MDT-205 

        #endregion

        #region Wallet

        /// <summary>
        /// Gets a customer's wallet
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A wallet</returns>
        MSP_Wallet GetMemberWallet(int customerId); //Jerry 20180814 MSP-45

        #endregion

        #region Deposit

        /// <summary>
        /// Gets a customer's wallet
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A wallet</returns>
        decimal GetTotalDepositReturn(int customerId); //Jerry 20180814 MSP-45

        /// <summary>
        /// Gets Deposit Returned Amount And Percentage
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Deposit Returned Amount And Percentage</returns>
        string GetDepositReturnedAmountAndPercentage(int customerId); //Jerry 20180814 MSP-45

        /// <summary>
        /// Gets Deposit Not Returned Amount And Percentage
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Deposit Not Returned Amount And Percentage</returns>
        string GetDepositNotReturnedAmountAndPercentage(int customerId); //Jerry 20180814 MSP-45

        #endregion

        #region Consumption

        string GetTotalSelfConsumptionRewardEarned(int CustomerId); //Jerry 20181008 MSP-252

        string GetTotalTeamConsumptionRewardEarned(int CustomerId); //Jerry 20181008 MSP-252

        #endregion Consumption

        #region Interface //Chew 20190129 MDT-205

        ///// <summary>
        ///// Gets a customer's interface
        ///// </summary>
        ///// <param name="customerId">Customer identifier</param>
        ///// <returns>A consumer interface</returns>
        //MSP_Interface_Consumer GetConsumerInterface(int customerId); //Jerry 20180912 MSP-45

        #endregion
        #region Prepare Customer With isSync, UpdatedBy , UpdatedOnUtc
        Customer PrepareCustomerSyncUpdatedInfo(Customer Currentcustomer, Customer AssignedCustomer, string SyncType = null); //LeeChurn 20181031 MSP-409
        #endregion

        #endregion

        /// <summary>
        /// Insert old customer password 
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        void InsertCustomerPasswordHistory(CustomerPassword_History customerPassword);//Tony Liew 20190308 RDT-69

        /// <summary>
        /// Get CustomerId by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        int GetCustomerIdByEmail(string email);

        /// <summary>
        /// Get CustomerGuid by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Guid? GetCustomerGuidByUsername(string username);
    }
}