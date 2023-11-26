using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Extensions;
using Nop.Data;
using Nop.Services.Common;
using Nop.Services.Events;
using Nop.Services.Msp.Transaction;

namespace Nop.Services.Msp.Transaction
{
    /// <summary>
    /// Customer service
    /// </summary>
    public partial class MSP_DepositService : IMSP_DepositService
    {

        #region Fields

        private readonly IRepository<MSP_Deposit> _MSP_DepositRepository;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="MSP_DepositRepository">Deposit repository</param>
        /// <param name="genericAttributeService">Generic attribute service</param>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="dbContext">DB context</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="customerSettings">Customer settings</param>
        /// <param name="commonSettings">Common settings</param>
        public MSP_DepositService(ICacheManager cacheManager,
            IRepository<MSP_Deposit> MSP_DepositRepository,
            IGenericAttributeService genericAttributeService,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IEventPublisher eventPublisher)
        {
            this._cacheManager = cacheManager;
            this._MSP_DepositRepository = MSP_DepositRepository;
            this._genericAttributeService = genericAttributeService;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        #region Methods

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
        public virtual IPagedList<MSP_Deposit> GetAllDeposits(DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int CustomerID = 0, int WalletID = 0, int ParentID = 0,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _MSP_DepositRepository.Table;
            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
            if (CustomerID > 0)
                query = query.Where(c => CustomerID == c.CustomerID);
            if (WalletID > 0)
                query = query.Where(c => WalletID == c.WalletID);
            if (ParentID > 0)
                query = query.Where(c => ParentID == c.ParentID);

            //query = query.Where(c => !c.Deleted);
            //if (customerRoleIds != null && customerRoleIds.Length > 0)
            //    query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).Intersect(customerRoleIds).Any());
            //if (!string.IsNullOrWhiteSpace(email))
            //    query = query.Where(c => c.Email.Contains(email));
            //if (!string.IsNullOrWhiteSpace(username))
            //    query = query.Where(c => c.Username.Contains(username));
            //if (!string.IsNullOrWhiteSpace(firstName))
            //{
            //    query = query
            //        .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
            //        .Where((z => z.Attribute.KeyGroup == "Customer" &&
            //            z.Attribute.Key == SystemCustomerAttributeNames.FirstName &&
            //            z.Attribute.Value.Contains(firstName)))
            //        .Select(z => z.Customer);
            //}
           
           
            query = query.OrderByDescending(c => c.CreatedOnUtc);

            var deposits = new PagedList<MSP_Deposit>(query, pageIndex, pageSize);
            return deposits;
        }

        /// <summary>
        /// Delete a deposit
        /// </summary>
        /// <param name="deposit">Deposit</param>
        public virtual void DeleteDeposit(MSP_Deposit deposit)
        {
            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));
      
            //customer.Deleted = true;

            //if (_customerSettings.SuffixDeletedCustomers)
            //{
            //    if (!string.IsNullOrEmpty(customer.Email))
            //        customer.Email += "-DELETED";
            //    if (!string.IsNullOrEmpty(customer.Username))
            //        customer.Username += "-DELETED";
            //}

            //UpdateCustomer(customer);

            //event notification
            _eventPublisher.EntityDeleted(deposit);
        }

        /// <summary>
        /// Gets a deposit
        /// </summary>
        /// <param name="id">deposit identifier</param>
        /// <returns>A deposit</returns>
        public virtual MSP_Deposit GetDepositById(int Id)
        {
            if (Id == 0)
                return null;
            
            return _MSP_DepositRepository.GetById(Id);
        }

        /// <summary>
        /// Get deposits by identifiers
        /// </summary>
        /// <param name="customerid">Deposit identifiers</param>
        /// <returns>Deposits</returns>
        public virtual IList<MSP_Deposit> GetDepositByCustomerID(int customerId)
        {
            if (customerId == 0)
                return null;
            
            var query = from c in _MSP_DepositRepository.Table
                        where c.CustomerID == customerId
                        select c;
            var deposits = query.ToList();
           
            return deposits;
        }

        /// <summary>
        /// Get deposit by identifiers
        /// </summary>
        /// <param name="batchi">Deposit batchd id</param>
        /// <returns>Deposits</returns>
        //public virtual IList<MSP_Deposit> GetDepositByBatchID(Guid batchid)
        //{
        //    if (batchid == null)
        //        return null;

        //    var query = from c in _MSP_DepositRepository.Table
        //                where c.BatchID == batchid
        //                select c;
        //    var deposits = query.ToList();

        //    return deposits;
        //}


        /// <summary>
        /// Get deposit by parent id
        /// </summary>
        /// <param name="parentId">parent id</param>
        /// <returns>Deposits</returns>
        public virtual IList<MSP_Deposit> GetDepositByParentID(int parentId)
        {
            if (parentId == 0)
                return null;

            var query = from c in _MSP_DepositRepository.Table
                        where c.ParentID == parentId
                        select c;
            var deposits = query.ToList();

            return deposits;
        }


        /// <summary>
        /// Get deposit by wallet id
        /// </summary>
        /// <param name="walletId">wallet id</param>
        /// <returns>Deposits</returns>
        public virtual IList<MSP_Deposit> GetDepositByWalletID(int walletId)
        {
            if (walletId == 0)
                return null;

            var query = from c in _MSP_DepositRepository.Table
                        where c.WalletID == walletId
                        select c;
            var deposits = query.ToList();

            return deposits;
        }

        /// <summary>
        /// Insert a deposit
        /// </summary>
        /// <param name="deposit">deposit</param>
        public virtual void InsertDeposit(MSP_Deposit deposit)
        {
            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            _MSP_DepositRepository.Insert(deposit);

            //event notification
            _eventPublisher.EntityInserted(deposit);
        }

        /// <summary>
        /// Updates the deposit
        /// </summary>
        /// <param name="deposit">Deposit</param>
        public virtual void UpdateDeposit(MSP_Deposit deposit)
        {
            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            _MSP_DepositRepository.Update(deposit);

            //event notification
            _eventPublisher.EntityUpdated(deposit);
        }

        #endregion 
    }
}