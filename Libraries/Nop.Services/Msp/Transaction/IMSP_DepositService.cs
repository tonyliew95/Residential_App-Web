using System;
using System.Collections.Generic;
using Nop.Core;
using Nop.Core.Domain.Msp.Transaction;

namespace Nop.Services.Msp.Transaction
{
    /// <summary>
    /// Customer service interface
    /// </summary>
    public partial interface IMSP_DepositService
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
        IPagedList<MSP_Deposit> GetAllDeposits(DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int CustomerID = 0, int WalletID = 0, int ParentID = 0,
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="customer">Deposit</param>
        void DeleteDeposit(MSP_Deposit deposit);

        /// <summary>
        /// Gets a deposit
        /// </summary>
        /// <param name="Id">Deposit identifier</param>
        /// <returns>A deposit</returns>
        MSP_Deposit GetDepositById(int Id);


        /// <summary>
        /// Get Deposit by identifiers
        /// </summary>
        /// <param name="customerId">Customer id</param>
        /// <returns>Deposits</returns>
        IList<MSP_Deposit> GetDepositByCustomerID(int customerId);

        ///// <summary>
        ///// Get Deposit by identifiers
        ///// </summary>
        ///// <param name="batchId">Deposit batch identifiers</param>
        ///// <returns>Deposits</returns>
        //IList<MSP_Deposit> GetDepositByBatchID(Guid batchId);

        /// <summary>
        /// Get deposit by parent id
        /// </summary>
        /// <param name="parentId">parent id</param>
        /// <returns>Deposits</returns>
        IList<MSP_Deposit> GetDepositByParentID(int parentId);

        /// <summary>
        /// Get deposit by wallet id
        /// </summary>
        /// <param name="walletId">wallet id</param>
        /// <returns>Deposits</returns>
        IList<MSP_Deposit> GetDepositByWalletID(int walletId);

        /// <summary>
        /// Insert a deposit
        /// </summary>
        /// <param name="deposit">deposit</param>
        void InsertDeposit(MSP_Deposit deposit);

        /// <summary>
        /// Updates the deposit
        /// </summary>
        /// <param name="deposit">Deposit</param>
        void UpdateDeposit(MSP_Deposit deposit);


        #endregion
    }
}