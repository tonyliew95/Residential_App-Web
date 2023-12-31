using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Msp.Calculation;
using Nop.Core.Domain.Msp.Interface;
using Nop.Core.Domain.Msp.Member;
//using Nop.Core.Domain.Msp.Security; //Atiqah 20190131 MDT-205
using Nop.Core.Domain.Msp.Transaction;
using Nop.Core.Domain.Msp.Wallet;
using Nop.Core.Domain.News;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Polls;
using Nop.Core.Domain.Shipping;
using Nop.Core.Extensions;
using Nop.Data;
using Nop.Services.Common;
using Nop.Services.Enumeration;
using Nop.Services.Events;
using Nop.Services.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Nop.Services.Customers
{
    /// <summary>
    /// Customer service
    /// </summary>
    public partial class CustomerService : ICustomerService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string CUSTOMERROLES_ALL_KEY = "Nop.customerrole.all-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : system name
        /// </remarks>
        private const string CUSTOMERROLES_BY_SYSTEMNAME_KEY = "Nop.customerrole.systemname-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CUSTOMERROLES_PATTERN_KEY = "Nop.customerrole.";

        #endregion

        #region Fields

        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerPassword> _customerPasswordRepository;
        private readonly IRepository<CustomerRole> _customerRoleRepository;
        private readonly IRepository<GenericAttribute> _gaRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ForumPost> _forumPostRepository;
        private readonly IRepository<ForumTopic> _forumTopicRepository;
        private readonly IRepository<BlogComment> _blogCommentRepository;
        private readonly IRepository<NewsComment> _newsCommentRepository;
        private readonly IRepository<PollVotingRecord> _pollVotingRecordRepository;
        private readonly IRepository<ProductReview> _productReviewRepository;
        private readonly IRepository<ProductReviewHelpfulness> _productReviewHelpfulnessRepository;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly ICacheManager _cacheManager;
        private readonly IEventPublisher _eventPublisher;
        private readonly CustomerSettings _customerSettings;
        private readonly CommonSettings _commonSettings;
        private readonly IRepository<CustomerPassword_History> _customerPasswordHistoryRepository;//Tony Liew 20190311 RDT-69

        //Jerry 20180814 MSP-45 \/
        private readonly IRepository<CustomerTransactionPassword> _customerTransactionPasswordRepository;
        private readonly IRepository<MSP_MemberTree> _memberTreeRepository;
        // private readonly IRepository<MSP_SecurityQuestion> _securityQuestionRepository; //Atiqah 20190131 MDT-205
        //private readonly IRepository<MSP_SecurityQuestionCustom> _securityQuestionCustomRepository; //Atiqah 20190131 MDT-205
        //private readonly IRepository<MSP_SecurityAnswer> _securityAnswerRepository; //Atiqah 20190130 MDT-205
        private readonly IRepository<MSP_Wallet> _walletRepository;
        private readonly IRepository<MSP_Deposit> _depositRepository;
        private readonly IRepository<MSP_Deposit_Offset> _depositOffsetRepository;
        // private readonly IRepository<MSP_Interface_Consumer> _interfaceRepository; //Chew 20190129 MDT-205
        private readonly IMspHelper _mspHelper;
        //Jerry 20180814 MSP-45 /\

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="customerRepository">Customer repository</param>
        /// <param name="customerPasswordRepository">Customer password repository</param>
        /// <param name="customerRoleRepository">Customer role repository</param>
        /// <param name="gaRepository">Generic attribute repository</param>
        /// <param name="orderRepository">Order repository</param>
        /// <param name="forumPostRepository">Forum post repository</param>
        /// <param name="forumTopicRepository">Forum topic repository</param>
        /// <param name="blogCommentRepository">Blog comment repository</param>
        /// <param name="newsCommentRepository">News comment repository</param>
        /// <param name="pollVotingRecordRepository">Poll voting record repository</param>
        /// <param name="productReviewRepository">Product review repository</param>
        /// <param name="productReviewHelpfulnessRepository">Product review helpfulness repository</param>
        /// <param name="genericAttributeService">Generic attribute service</param>
        /// <param name="dataProvider">Data provider</param>
        /// <param name="dbContext">DB context</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="customerSettings">Customer settings</param>
        /// <param name="commonSettings">Common settings</param>
        public CustomerService(ICacheManager cacheManager,
            IRepository<Customer> customerRepository,
            IRepository<CustomerPassword> customerPasswordRepository,
            IRepository<CustomerRole> customerRoleRepository,
            IRepository<GenericAttribute> gaRepository,
            IRepository<Order> orderRepository,
            IRepository<ForumPost> forumPostRepository,
            IRepository<ForumTopic> forumTopicRepository,
            IRepository<BlogComment> blogCommentRepository,
            IRepository<NewsComment> newsCommentRepository,
            IRepository<PollVotingRecord> pollVotingRecordRepository,
            IRepository<ProductReview> productReviewRepository,
            IRepository<ProductReviewHelpfulness> productReviewHelpfulnessRepository,
            IGenericAttributeService genericAttributeService,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IEventPublisher eventPublisher,
            CustomerSettings customerSettings,
            CommonSettings commonSettings
            //Jerry 20180814 MSP-45 \/
            , IRepository<CustomerTransactionPassword> customerTransactionPasswordRepository
            , IRepository<MSP_MemberTree> memberTreeRepository
            //, IRepository<MSP_SecurityQuestion> securityQuestionRepository //Atiqah 20190131 MDT-205
            //, IRepository<MSP_SecurityQuestionCustom> securityQuestionCustomRepository //Atiqah 20190131 MDT-205
            //, IRepository<MSP_SecurityAnswer> securityAnswerRepository //Atiqah 20190130 MDT-205
            , IRepository<MSP_Wallet> walletRepository
            , IRepository<MSP_Deposit> depositRepository
            , IRepository<MSP_Deposit_Offset> depositOffsetRepository
            // , IRepository<MSP_Interface_Consumer> interfaceRepository //Chew 20190129 MDT-205
            , IMspHelper mspHelper
            , IRepository<CustomerPassword_History> customerPasswordHistoryRepository//Tony Liew 20190311 RDT-69
                                                                                    
            )
        {
            this._cacheManager = cacheManager;
            this._customerRepository = customerRepository;
            this._customerPasswordRepository = customerPasswordRepository;
            this._customerRoleRepository = customerRoleRepository;
            this._gaRepository = gaRepository;
            this._orderRepository = orderRepository;
            this._forumPostRepository = forumPostRepository;
            this._forumTopicRepository = forumTopicRepository;
            this._blogCommentRepository = blogCommentRepository;
            this._newsCommentRepository = newsCommentRepository;
            this._pollVotingRecordRepository = pollVotingRecordRepository;
            this._productReviewRepository = productReviewRepository;
            this._productReviewHelpfulnessRepository = productReviewHelpfulnessRepository;
            this._genericAttributeService = genericAttributeService;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._eventPublisher = eventPublisher;
            this._customerSettings = customerSettings;
            this._commonSettings = commonSettings;
            //Jerry 20180814 MSP-45 \/
            this._customerTransactionPasswordRepository = customerTransactionPasswordRepository;
            this._memberTreeRepository = memberTreeRepository;
            //this._securityQuestionRepository = securityQuestionRepository; //Atiqah 20190131 MDT-205
            //this._securityQuestionCustomRepository = securityQuestionCustomRepository; //Atiqah 20190131 MDT-205
            // this._securityAnswerRepository = securityAnswerRepository; //Atiqah 20190130 MDT-205
            this._walletRepository = walletRepository;
            this._depositRepository = depositRepository;
            this._depositOffsetRepository = depositOffsetRepository;
            // this._interfaceRepository = interfaceRepository; //Chew 20190129 MDT-205
            this._mspHelper = mspHelper;
            this._customerPasswordHistoryRepository = customerPasswordHistoryRepository;//Tony Liew 20190311 RDT-69
            //Jerry 20180814 MSP-45 /\
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Delete guest customers using LINQ
        /// </summary>
        /// <param name="createdFromUtc">Created from</param>
        /// <param name="createdToUtc">Created to</param>
        /// <param name="onlyWithoutShoppingCart">Delete only without shopping cart</param>
        /// <returns>Number of delete customers</returns>
        protected virtual int DeleteGuestCustomersUseLinq(DateTime? createdFromUtc, DateTime? createdToUtc, bool onlyWithoutShoppingCart)
        {
            var guestRole = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Guests);
            if (guestRole == null)
                throw new NopException("'Guests' role could not be loaded");

            var query = _customerRepository.Table;
            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
            query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).Contains(guestRole.Id));
            if (onlyWithoutShoppingCart)
                query = query.Where(c => !c.ShoppingCartItems.Any());
            //no orders
            query = from c in query
                    join o in _orderRepository.Table on c.Id equals o.CustomerId into c_o
                    from o in c_o.DefaultIfEmpty()
                    where !c_o.Any()
                    select c;
            //no blog comments
            query = from c in query
                    join bc in _blogCommentRepository.Table on c.Id equals bc.CustomerId into c_bc
                    from bc in c_bc.DefaultIfEmpty()
                    where !c_bc.Any()
                    select c;
            //no news comments
            query = from c in query
                    join nc in _newsCommentRepository.Table on c.Id equals nc.CustomerId into c_nc
                    from nc in c_nc.DefaultIfEmpty()
                    where !c_nc.Any()
                    select c;
            //no product reviews
            query = from c in query
                    join pr in _productReviewRepository.Table on c.Id equals pr.CustomerId into c_pr
                    from pr in c_pr.DefaultIfEmpty()
                    where !c_pr.Any()
                    select c;
            //no product reviews helpfulness
            query = from c in query
                    join prh in _productReviewHelpfulnessRepository.Table on c.Id equals prh.CustomerId into c_prh
                    from prh in c_prh.DefaultIfEmpty()
                    where !c_prh.Any()
                    select c;
            //no poll voting
            query = from c in query
                    join pvr in _pollVotingRecordRepository.Table on c.Id equals pvr.CustomerId into c_pvr
                    from pvr in c_pvr.DefaultIfEmpty()
                    where !c_pvr.Any()
                    select c;
            //no forum posts 
            query = from c in query
                    join fp in _forumPostRepository.Table on c.Id equals fp.CustomerId into c_fp
                    from fp in c_fp.DefaultIfEmpty()
                    where !c_fp.Any()
                    select c;
            //no forum topics
            query = from c in query
                    join ft in _forumTopicRepository.Table on c.Id equals ft.CustomerId into c_ft
                    from ft in c_ft.DefaultIfEmpty()
                    where !c_ft.Any()
                    select c;
            //don't delete system accounts
            query = query.Where(c => !c.IsSystemAccount);

            //only distinct customers (group by ID)
            query = from c in query
                    group c by c.Id
                into cGroup
                    orderby cGroup.Key
                    select cGroup.FirstOrDefault();
            query = query.OrderBy(c => c.Id);
            var customers = query.ToList();

            var totalRecordsDeleted = 0;
            foreach (var c in customers)
            {
                try
                {
                    //delete attributes
                    var attributes = _genericAttributeService.GetAttributesForEntity(c.Id, "Customer");
                    _genericAttributeService.DeleteAttributes(attributes);

                    //delete from database
                    _customerRepository.Delete(c);
                    totalRecordsDeleted++;
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                }
            }
            return totalRecordsDeleted;
        }

        /// <summary>
        /// Delete guest customers using a stored procedure
        /// </summary>
        /// <param name="createdFromUtc">Created from</param>
        /// <param name="createdToUtc">Created to</param>
        /// <param name="onlyWithoutShoppingCart">Delete only without shopping cart</param>
        /// <returns>Number of delete customers</returns>
        protected virtual int DeleteGuestCustomersUseStoredProcedure(DateTime? createdFromUtc, DateTime? createdToUtc, bool onlyWithoutShoppingCart)
        {
            //prepare parameters
            var pOnlyWithoutShoppingCart = _dataProvider.GetBooleanParameter("OnlyWithoutShoppingCart", onlyWithoutShoppingCart);
            var pCreatedFromUtc = _dataProvider.GetDateTimeParameter("CreatedFromUtc", createdFromUtc);
            var pCreatedToUtc = _dataProvider.GetDateTimeParameter("CreatedToUtc", createdToUtc);
            var pTotalRecordsDeleted = _dataProvider.GetOutputInt32Parameter("TotalRecordsDeleted");

            //invoke stored procedure
            _dbContext.ExecuteSqlCommand(
                "EXEC [DeleteGuests] @OnlyWithoutShoppingCart, @CreatedFromUtc, @CreatedToUtc, @TotalRecordsDeleted OUTPUT",
                false, null,
                pOnlyWithoutShoppingCart,
                pCreatedFromUtc,
                pCreatedToUtc,
                pTotalRecordsDeleted);

            var totalRecordsDeleted = pTotalRecordsDeleted.Value != DBNull.Value ? Convert.ToInt32(pTotalRecordsDeleted.Value) : 0;
            return totalRecordsDeleted;
        }

        #endregion

        #region Methods

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
        public virtual IPagedList<Customer> GetAllCustomers(bool isCS = false, bool isCSAdmin = false, bool isAdmin = false, bool isAOD = false, bool isFinance = false, int currentCustomerID = 0, 
            DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int affiliateId = 0, int vendorId = 0,
            int[] customerRoleIds = null, string email = null, string username = null,
            string firstName = null, string lastName = null,
            int dayOfBirth = 0, int monthOfBirth = 0,
            string company = null, string phone = null, string zipPostalCode = null,
            string ipAddress = null, bool loadOnlyWithShoppingCart = false, ShoppingCartType? sct = null,
            int pageIndex = 0, int pageSize = int.MaxValue) //wailiang 20181213 MDT-133
        {
            var query = _customerRepository.Table;

            // Tony Liew 20181030 MSP-411 \/
            if (isCS)
                query = query.Where(c => c.IsMember == true || c.Id == currentCustomerID);
            //wailiang 20181213 MDT-133 \/
            else if (isCSAdmin)
            {
                var adminRoleId = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Administrators).Id;
                //query = query.Where(c => c.CustomerRoles.Where(cr => cr.Id == adminRoleId).Select(cr => cr.Id).FirstOrDefault() != adminRoleId || c.Id == currentCustomerID);
                query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).FirstOrDefault() != adminRoleId || c.Id == currentCustomerID);
            }
            else if (isAOD)
            {
                var aodRoleId = GetCustomerRoleBySystemName(SystemCustomerRoleNames.AOD).Id;
                //query = query.Where(c => c.CustomerRoles.Where(cr => cr.Id == aodRoleId).Select(cr => cr.Id).FirstOrDefault() != aodRoleId || c.Id == currentCustomerID);
                query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).FirstOrDefault() != aodRoleId || c.Id == currentCustomerID);
            }
            else if (isFinance)
            {
                var financeRoleId = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Finance).Id;
                //query = query.Where(c => c.CustomerRoles.Where(cr => cr.Id == financeRoleId).Select(cr => cr.Id).FirstOrDefault() != financeRoleId || c.Id == currentCustomerID);
                query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).FirstOrDefault() != financeRoleId || c.Id == currentCustomerID);
            }
            #region Should show all role in customer list
            //wailiang 20181213 MDT-133 \/
            //else if (isCSAdmin)
            //{
            //    var adminRoleId = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Administrators).Id;
            //    //query = query.Where(c => c.CustomerRoles.Where(cr => cr.Id == adminRoleId).Select(cr => cr.Id).FirstOrDefault() != adminRoleId || c.Id == currentCustomerID);
            //    query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).FirstOrDefault() != adminRoleId || c.Id == currentCustomerID);
            //}
            //else if (isAOD)
            //{
            //    var aodRoleId = GetCustomerRoleBySystemName(SystemCustomerRoleNames.AOD).Id;
            //    //query = query.Where(c => c.CustomerRoles.Where(cr => cr.Id == aodRoleId).Select(cr => cr.Id).FirstOrDefault() != aodRoleId || c.Id == currentCustomerID);
            //    query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).FirstOrDefault() != aodRoleId || c.Id == currentCustomerID);
            //}
            //else if (isFinance)
            //{
            //    var financeRoleId = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Finance).Id;
            //    //query = query.Where(c => c.CustomerRoles.Where(cr => cr.Id == financeRoleId).Select(cr => cr.Id).FirstOrDefault() != financeRoleId || c.Id == currentCustomerID);
            //    query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).FirstOrDefault() != financeRoleId || c.Id == currentCustomerID);
            //}
            //wailiang 20181213 MDT-133 /\
            // Tony Liew 20181030 MSP-411 /\
            #endregion

            if (createdFromUtc.HasValue)
                query = query.Where(c => createdFromUtc.Value <= c.CreatedOnUtc);
            if (createdToUtc.HasValue)
                query = query.Where(c => createdToUtc.Value >= c.CreatedOnUtc);
            if (affiliateId > 0)
                query = query.Where(c => affiliateId == c.AffiliateId);
            if (vendorId > 0)
                query = query.Where(c => vendorId == c.VendorId);
            query = query.Where(c => !c.Deleted);
            if (customerRoleIds != null && customerRoleIds.Length > 0)
                query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).Intersect(customerRoleIds).Any());
            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(c => c.Email.Contains(email));
            if (!string.IsNullOrWhiteSpace(username))
                query = query.Where(c => c.Username.Contains(username));

           

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.FirstName &&
                        z.Attribute.Value.Contains(firstName)))
                    .Select(z => z.Customer);
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.LastName &&
                        z.Attribute.Value.Contains(lastName)))
                    .Select(z => z.Customer);
            }
            //date of birth is stored as a string into database.
            //we also know that date of birth is stored in the following format YYYY-MM-DD (for example, 1983-02-18).
            //so let's search it as a string
            if (dayOfBirth > 0 && monthOfBirth > 0)
            {
                //both are specified
                var dateOfBirthStr = monthOfBirth.ToString("00", CultureInfo.InvariantCulture) + "-" + dayOfBirth.ToString("00", CultureInfo.InvariantCulture);
                //EndsWith is not supported by SQL Server Compact
                //so let's use the following workaround http://social.msdn.microsoft.com/Forums/is/sqlce/thread/0f810be1-2132-4c59-b9ae-8f7013c0cc00

                //we also cannot use Length function in SQL Server Compact (not supported in this context)
                //z.Attribute.Value.Length - dateOfBirthStr.Length = 5
                //dateOfBirthStr.Length = 5
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.DateOfBirth &&
                        z.Attribute.Value.Substring(5, 5) == dateOfBirthStr))
                    .Select(z => z.Customer);
            }
            else if (dayOfBirth > 0)
            {
                //only day is specified
                var dateOfBirthStr = dayOfBirth.ToString("00", CultureInfo.InvariantCulture);
                //EndsWith is not supported by SQL Server Compact
                //so let's use the following workaround http://social.msdn.microsoft.com/Forums/is/sqlce/thread/0f810be1-2132-4c59-b9ae-8f7013c0cc00

                //we also cannot use Length function in SQL Server Compact (not supported in this context)
                //z.Attribute.Value.Length - dateOfBirthStr.Length = 8
                //dateOfBirthStr.Length = 2
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.DateOfBirth &&
                        z.Attribute.Value.Substring(8, 2) == dateOfBirthStr))
                    .Select(z => z.Customer);
            }
            else if (monthOfBirth > 0)
            {
                //only month is specified
                var dateOfBirthStr = "-" + monthOfBirth.ToString("00", CultureInfo.InvariantCulture) + "-";
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.DateOfBirth &&
                        z.Attribute.Value.Contains(dateOfBirthStr)))
                    .Select(z => z.Customer);
            }
            //search by company
            if (!string.IsNullOrWhiteSpace(company))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.Company &&
                        z.Attribute.Value.Contains(company)))
                    .Select(z => z.Customer);
            }
            //search by phone
            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.Phone &&
                        z.Attribute.Value.Contains(phone)))
                    .Select(z => z.Customer);
            }
            //search by zip
            if (!string.IsNullOrWhiteSpace(zipPostalCode))
            {
                query = query
                    .Join(_gaRepository.Table, x => x.Id, y => y.EntityId, (x, y) => new { Customer = x, Attribute = y })
                    .Where((z => z.Attribute.KeyGroup == "Customer" &&
                        z.Attribute.Key == SystemCustomerAttributeNames.ZipPostalCode &&
                        z.Attribute.Value.Contains(zipPostalCode)))
                    .Select(z => z.Customer);
            }

            //search by IpAddress
            if (!string.IsNullOrWhiteSpace(ipAddress) && CommonHelper.IsValidIpAddress(ipAddress))
            {
                query = query.Where(w => w.LastIpAddress == ipAddress);
            }

            if (loadOnlyWithShoppingCart)
            {
                int? sctId = null;
                if (sct.HasValue)
                    sctId = (int)sct.Value;

                query = sct.HasValue ?
                    query.Where(c => c.ShoppingCartItems.Any(x => x.ShoppingCartTypeId == sctId)) :
                    query.Where(c => c.ShoppingCartItems.Any());
            }

            query = query.OrderByDescending(c => c.CreatedOnUtc);

            var customers = new PagedList<Customer>(query, pageIndex, pageSize);
            return customers;
        }

        /// <summary>
        /// Gets online customers
        /// </summary>
        /// <param name="lastActivityFromUtc">Customer last activity date (from)</param>
        /// <param name="customerRoleIds">A list of customer role identifiers to filter by (at least one match); pass null or empty list in order to load all customers; </param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Customers</returns>
        public virtual IPagedList<Customer> GetOnlineCustomers(DateTime lastActivityFromUtc,
            int[] customerRoleIds, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _customerRepository.Table;
            query = query.Where(c => lastActivityFromUtc <= c.LastActivityDateUtc);
            query = query.Where(c => !c.Deleted);
            if (customerRoleIds != null && customerRoleIds.Length > 0)
                query = query.Where(c => c.CustomerRoles.Select(cr => cr.Id).Intersect(customerRoleIds).Any());

            query = query.OrderByDescending(c => c.LastActivityDateUtc);
            var customers = new PagedList<Customer>(query, pageIndex, pageSize);
            return customers;
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void DeleteCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (customer.IsSystemAccount)
                throw new NopException($"System customer account ({customer.SystemName}) could not be deleted");

            customer.Deleted = true;

            if (_customerSettings.SuffixDeletedCustomers)
            {
                if (!string.IsNullOrEmpty(customer.Email))
                    customer.Email += "-DELETED";
                if (!string.IsNullOrEmpty(customer.Username))
                    customer.Username += "-DELETED";
            }

            UpdateCustomer(customer);

            //event notification
            _eventPublisher.EntityDeleted(customer);
        }

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A customer</returns>
        public virtual Customer GetCustomerById(int customerId)
        {
            if (customerId == 0)
                return null;

            return _customerRepository.GetById(customerId);
        }

        /// <summary>
        /// Get customers by identifiers
        /// </summary>
        /// <param name="customerIds">Customer identifiers</param>
        /// <returns>Customers</returns>
        public virtual IList<Customer> GetCustomersByIds(int[] customerIds)
        {
            if (customerIds == null || customerIds.Length == 0)
                return new List<Customer>();

            var query = from c in _customerRepository.Table
                        where customerIds.Contains(c.Id) && !c.Deleted
                        select c;
            var customers = query.ToList();
            //sort by passed identifiers
            var sortedCustomers = new List<Customer>();
            foreach (var id in customerIds)
            {
                var customer = customers.Find(x => x.Id == id);
                if (customer != null)
                    sortedCustomers.Add(customer);
            }
            return sortedCustomers;
        }

        /// <summary>
        /// Gets a customer by GUID
        /// </summary>
        /// <param name="customerGuid">Customer GUID</param>
        /// <returns>A customer</returns>
        public virtual Customer GetCustomerByGuid(Guid customerGuid)
        {
            if (customerGuid == Guid.Empty)
                return null;

            var query = from c in _customerRepository.Table
                        where c.CustomerGuid == customerGuid
                        orderby c.Id
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Get customer by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Customer</returns>
        public virtual Customer GetCustomerByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.Email == email
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Get customer by system name
        /// </summary>
        /// <param name="systemName">System name</param>
        /// <returns>Customer</returns>
        public virtual Customer GetCustomerBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.SystemName == systemName
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Get customer by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>Customer</returns>
        public virtual Customer GetCustomerByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            var query = from c in _customerRepository.Table
                        orderby c.Id
                        where c.Username == username
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        /// <summary>
        /// Insert a guest customer
        /// </summary>
        /// <returns>Customer</returns>
        public virtual Customer InsertGuestCustomer()
        {
            var customer = new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                Active = true,
                CreatedOnUtc = DateTime.UtcNow,
                LastActivityDateUtc = DateTime.UtcNow,
            };

            //add to 'Guests' role
            var guestRole = GetCustomerRoleBySystemName(SystemCustomerRoleNames.Guests);
            if (guestRole == null)
                throw new NopException("'Guests' role could not be loaded");
            customer.CustomerRoles.Add(guestRole);

            _customerRepository.Insert(customer);

            return customer;
        }

        /// <summary>
        /// Insert a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void InsertCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            _customerRepository.Insert(customer);

            //event notification
            _eventPublisher.EntityInserted(customer);
        }

        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        public virtual void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            _customerRepository.Update(customer);

            //event notification
            _eventPublisher.EntityUpdated(customer);
        }

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
        public virtual void ResetCheckoutData(Customer customer, int storeId,
            bool clearCouponCodes = false, bool clearCheckoutAttributes = false,
            bool clearRewardPoints = true, bool clearShippingMethod = true,
            bool clearPaymentMethod = true)
        {
            if (customer == null)
                throw new ArgumentNullException();

            //clear entered coupon codes
            if (clearCouponCodes)
            {
                _genericAttributeService.SaveAttribute<string>(customer, SystemCustomerAttributeNames.DiscountCouponCode, null);
                _genericAttributeService.SaveAttribute<string>(customer, SystemCustomerAttributeNames.GiftCardCouponCodes, null);
            }

            //clear checkout attributes
            if (clearCheckoutAttributes)
            {
                _genericAttributeService.SaveAttribute<string>(customer, SystemCustomerAttributeNames.CheckoutAttributes, null, storeId);
            }

            //clear reward points flag
            if (clearRewardPoints)
            {
                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.UseRewardPointsDuringCheckout, false, storeId);
            }

            //clear selected shipping method
            if (clearShippingMethod)
            {
                _genericAttributeService.SaveAttribute<ShippingOption>(customer, SystemCustomerAttributeNames.SelectedShippingOption, null, storeId);
                _genericAttributeService.SaveAttribute<ShippingOption>(customer, SystemCustomerAttributeNames.OfferedShippingOptions, null, storeId);
                _genericAttributeService.SaveAttribute<PickupPoint>(customer, SystemCustomerAttributeNames.SelectedPickupPoint, null, storeId);
            }

            //clear selected payment method
            if (clearPaymentMethod)
            {
                _genericAttributeService.SaveAttribute<string>(customer, SystemCustomerAttributeNames.SelectedPaymentMethod, null, storeId);
            }

            UpdateCustomer(customer);
        }

        /// <summary>
        /// Delete guest customer records
        /// </summary>
        /// <param name="createdFromUtc">Created date from (UTC); null to load all records</param>
        /// <param name="createdToUtc">Created date to (UTC); null to load all records</param>
        /// <param name="onlyWithoutShoppingCart">A value indicating whether to delete customers only without shopping cart</param>
        /// <returns>Number of deleted customers</returns>
        public virtual int DeleteGuestCustomers(DateTime? createdFromUtc, DateTime? createdToUtc, bool onlyWithoutShoppingCart)
        {
            if (_commonSettings.UseStoredProceduresIfSupported && _dataProvider.StoredProceduredSupported)
            {
                //stored procedures are enabled and supported by the database. 
                //It's much faster than the LINQ implementation below 
                return DeleteGuestCustomersUseStoredProcedure(createdFromUtc, createdToUtc, onlyWithoutShoppingCart);
            }

            //stored procedures aren't supported. Use LINQ
            return DeleteGuestCustomersUseLinq(createdFromUtc, createdToUtc, onlyWithoutShoppingCart);
        }

        /// <summary>
        /// Get Customer Username by Id  //RW MSP-46 20180827
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public string GetCustomerUsernameById(int CustomerId)
        {
            var username = _customerRepository.Table.Where(o => o.Id == CustomerId).Select(o => o.Username).FirstOrDefault();

            return username;
        }

        #endregion

        #region Customer roles

        /// <summary>
        /// Delete a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void DeleteCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException(nameof(customerRole));

            if (customerRole.IsSystemRole)
                throw new NopException("System role could not be deleted");

            _customerRoleRepository.Delete(customerRole);

            _cacheManager.RemoveByPattern(CUSTOMERROLES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityDeleted(customerRole);
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="customerRoleId">Customer role identifier</param>
        /// <returns>Customer role</returns>
        public virtual CustomerRole GetCustomerRoleById(int customerRoleId)
        {
            if (customerRoleId == 0)
                return null;

            return _customerRoleRepository.GetById(customerRoleId);
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        public virtual CustomerRole GetCustomerRoleBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var key = string.Format(CUSTOMERROLES_BY_SYSTEMNAME_KEY, systemName);
            return _cacheManager.Get(key, () =>
            {
                var query = from cr in _customerRoleRepository.Table
                            orderby cr.Id
                            where cr.SystemName == systemName
                            select cr;
                var customerRole = query.FirstOrDefault();
                return customerRole;
            });
        }

        /// <summary>
        /// Gets all customer roles
        /// </summary>
        /// <param name="showHidden">A value indicating whether to show hidden records</param>
        /// <returns>Customer roles</returns>
        public virtual IList<CustomerRole> GetAllCustomerRoles(bool showHidden = false)
        {
            var key = string.Format(CUSTOMERROLES_ALL_KEY, showHidden);
            return _cacheManager.Get(key, () =>
            {
                var query = from cr in _customerRoleRepository.Table
                            orderby cr.Name
                            where showHidden || cr.Active
                            select cr;
                var customerRoles = query.ToList();
                return customerRoles;
            });
        }

        /// <summary>
        /// Inserts a customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void InsertCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException(nameof(customerRole));

            _customerRoleRepository.Insert(customerRole);

            _cacheManager.RemoveByPattern(CUSTOMERROLES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityInserted(customerRole);
        }

        /// <summary>
        /// Updates the customer role
        /// </summary>
        /// <param name="customerRole">Customer role</param>
        public virtual void UpdateCustomerRole(CustomerRole customerRole)
        {
            if (customerRole == null)
                throw new ArgumentNullException(nameof(customerRole));

            _customerRoleRepository.Update(customerRole);

            _cacheManager.RemoveByPattern(CUSTOMERROLES_PATTERN_KEY);

            //event notification
            _eventPublisher.EntityUpdated(customerRole);
        }

        #endregion

        #region Customer passwords

        /// <summary>
        /// Gets customer passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of customer passwords</returns>
        public virtual IList<CustomerPassword> GetCustomerPasswords(int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null)
        {
            var query = _customerPasswordRepository.Table;

            //filter by customer
            if (customerId.HasValue)
                query = query.Where(password => password.CustomerId == customerId.Value);

            //filter by password format
            if (passwordFormat.HasValue)
                query = query.Where(password => password.PasswordFormatId == (int)(passwordFormat.Value));

            //get the latest passwords
            if (passwordsToReturn.HasValue)
                query = query.OrderByDescending(password => password.CreatedOnUtc).Take(passwordsToReturn.Value);

            return query.ToList();
        }

        //Tony Liew 20190308 RDT-69 \/

        /// <summary>
        /// Gets customer passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning passwords; pass null to load all records</param>
        /// <returns>List of customer passwords</returns>
        public virtual IList<CustomerPassword_History> GetPreviousCustomerPasswords(int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null)
        {
            var query = _customerPasswordHistoryRepository.Table;

            //filter by customer
            if (customerId.HasValue)
                query = query.Where(password => password.CustomerId == customerId.Value);

            //filter by password format
            if (passwordFormat.HasValue)
                query = query.Where(password => password.PasswordFormatId == (int)(passwordFormat.Value));

            //get the latest passwords
            if (passwordsToReturn.HasValue)
                query = query.OrderByDescending(password => password.CreatedOnUtc).Take(passwordsToReturn.Value);

            return query.ToList();
        }

        //Tony Liew 20190308 RDT-69 /\

        /// <summary>
        /// Get current customer password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer password</returns>
        public virtual CustomerPassword GetCurrentPassword(int customerId)
        {
            if (customerId == 0)
                return null;

            //return the latest password
            return GetCustomerPasswords(customerId, passwordsToReturn: 1).FirstOrDefault();
        }

        //Tony Liew 20190308 RDT-69 \/
        /// <summary>
        /// Get current customer password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer password</returns>
        public virtual CustomerPassword_History GetPreviousPassword(int customerId)
        {
            if (customerId == 0)
                return null;

            //return the latest password
            return GetPreviousCustomerPasswords(customerId, passwordsToReturn: 1).FirstOrDefault();
        }
        //Tony Liew 20190308 RDT-69/\

        /// <summary>
        /// Insert a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        public virtual void InsertCustomerPassword(CustomerPassword customerPassword)
        {
            if (customerPassword == null)
                throw new ArgumentNullException(nameof(customerPassword));

            _customerPasswordRepository.Insert(customerPassword);

            //event notification
            _eventPublisher.EntityInserted(customerPassword);
        }

        /// <summary>
        /// Update a customer password
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        public virtual void UpdateCustomerPassword(CustomerPassword customerPassword)
        {
            if (customerPassword == null)
                throw new ArgumentNullException(nameof(customerPassword));

            _customerPasswordRepository.Update(customerPassword);

            //event notification
            _eventPublisher.EntityUpdated(customerPassword);
        }

        #endregion

        #region Customer transaction passwords

        /// <summary>
        /// Gets customer transaction passwords
        /// </summary>
        /// <param name="customerId">Customer identifier; pass null to load all records</param>
        /// <param name="passwordFormat">Password format; pass null to load all records</param>
        /// <param name="passwordsToReturn">Number of returning transaction passwords; pass null to load all records</param>
        /// <returns>List of customer transaction passwords</returns>
        public virtual IList<CustomerTransactionPassword> GetCustomerTransactionPasswords(int? customerId = null,
            PasswordFormat? passwordFormat = null, int? passwordsToReturn = null) //Jerry 20180814 MSP-45
        {
            var query = _customerTransactionPasswordRepository.Table;

            //filter by customer
            if (customerId.HasValue)
                query = query.Where(password => password.CustomerId == customerId.Value);

            //filter by password format
            if (passwordFormat.HasValue)
                query = query.Where(password => password.PasswordFormatId == (int)(passwordFormat.Value));

            //get the latest passwords
            if (passwordsToReturn.HasValue)
                query = query.OrderByDescending(password => password.CreatedOnUtc).Take(passwordsToReturn.Value);

            return query.ToList();
        }

        /// <summary>
        /// Get current customer transaction password
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer transaction password</returns>
        public virtual CustomerTransactionPassword GetCurrentTransactionPassword(int customerId) //Jerry 20180814 MSP-45
        {
            if (customerId == 0)
                return null;

            //return the latest password
            return GetCustomerTransactionPasswords(customerId, passwordsToReturn: 1).FirstOrDefault();
        }

        /// <summary>
        /// Insert a customer transaction password
        /// </summary>
        /// <param name="customerTransactionPassword">Customer transaction password</param>
        public virtual void InsertCustomerTransactionPassword(CustomerTransactionPassword customerTransactionPassword) //Jerry 20180814 MSP-45
        {
            if (customerTransactionPassword == null)
                throw new ArgumentNullException(nameof(customerTransactionPassword));

            _customerTransactionPasswordRepository.Insert(customerTransactionPassword);

            //event notification
            _eventPublisher.EntityInserted(customerTransactionPassword);
        }

        /// <summary>
        /// Update a customer transaction password
        /// </summary>
        /// <param name="customerTransactionPassword">Customer transaction password</param>
        public virtual void UpdateCustomerTransactionPassword(CustomerTransactionPassword customerTransactionPassword) //Jerry 20180814 MSP-45
        {
            if (customerTransactionPassword == null)
                throw new ArgumentNullException(nameof(customerTransactionPassword));

            _customerTransactionPasswordRepository.Update(customerTransactionPassword);

            //event notification
            _eventPublisher.EntityUpdated(customerTransactionPassword);
        }

        #endregion

        #region Membership

        #region Member Tree

        /// <summary>
        /// Gets a customer's member tree
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A member tree</returns>
        public virtual MSP_MemberTree GetMemberTree(int customerId) //Jerry 20180814 MSP-45
        {
            if (customerId == 0)
                return null;

            var returnResult =
                (from memberTree in _memberTreeRepository.Table
                 where memberTree.CustomerID == customerId
                 select memberTree
                ).FirstOrDefault();

            return returnResult;
        }

        #endregion

        #region Security Question And Answer
        //Atiqah 20190130 MDT-205 \/
        /// <summary>
        /// Gets security question and answer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>List of Security question and answer</returns>
        //public virtual IList GetSecurityQuestionAndAnswer(int customerId) //Jerry 20180814 MSP-45
        //{
        //    var returnResult =
        //        (from ans in _securityAnswerRepository.Table
        //         join que in _securityQuestionRepository.Table
        //         on new { ans.SecurityQuestionID, ans.SecurityQuestionType }
        //         equals new { SecurityQuestionID = que.Id, SecurityQuestionType = "S" } into que_join
        //         from que in que_join.DefaultIfEmpty()
        //         join cus in _securityQuestionCustomRepository.Table
        //         on new { ans.SecurityQuestionID, ans.SecurityQuestionType }
        //         equals new { SecurityQuestionID = cus.Id, SecurityQuestionType = "C" } into cus_join
        //         from cus in cus_join.DefaultIfEmpty()
        //         where ans.CustomerId == customerId
        //         orderby ans.SecurityQuestionType descending, ans.Id
        //         select new
        //         {
        //             Question = (cus.Question ?? que.Question),
        //             ans.Answer
        //         }
        //        ).ToList();

        //    return returnResult;
        //}
        //Atiqah 20190130 MDT-205 /\

        #endregion

        #region Wallet

        /// <summary>
        /// Gets a customer's wallet
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>A wallet</returns>
        public virtual MSP_Wallet GetMemberWallet(int customerId) //Jerry 20180814 MSP-45
        {
            if (customerId == 0)
                return null;

            var returnResult =
                (from wallet in _walletRepository.Table
                 where wallet.CustomerID == customerId
                 select wallet
                ).FirstOrDefault();

            return returnResult;
        }

        #endregion

        #region Deposit

        /// <summary>
        /// Gets Total Deposit Return
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Total Deposit Return</returns>
        public virtual decimal GetTotalDepositReturn(int customerId) //Jerry 20180814 MSP-45
        {
            if (customerId == 0)
                return 0m;

            decimal returnResult =
                (from dps in _depositRepository.Table
                 where dps.CustomerID == customerId
                 group dps by new
                 {
                     dps.CustomerID
                 } into g
                 select new
                 {
                     TotalDepositAmt = g.Sum(p => p.DepositAmt)
                 }
                ).FirstOrDefault().TotalDepositAmt;

            return returnResult;
        }

        /// <summary>
        /// Gets Deposit Returned Amount And Percentage
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Deposit Returned Amount And Percentage</returns>
        public virtual string GetDepositReturnedAmountAndPercentage(int customerId) //Jerry 20180814 MSP-45
        {
            string returnResult = "";

            if (customerId == 0)
                return returnResult;

            var data =
                (from wallet in _walletRepository.Table
                 where wallet.CustomerID == customerId
                 select wallet
                ).FirstOrDefault();

            if (data != null)
            {
                returnResult = _mspHelper.TruncateDecimalToString_MBTC(0) + "mBTC (0%)";
                decimal Deposit_Total = data.Deposit_Total;
                decimal Deposit_Return_Total = data.Deposit_Return_Total;

                if (Deposit_Total <= 0 || Deposit_Return_Total <= 0)
                {
                    return returnResult;
                }

                decimal percentage = Math.Round((Deposit_Return_Total / Deposit_Total) * 100, 2);

                returnResult = _mspHelper.TruncateDecimalToString_MBTC(Deposit_Return_Total) + "mBTC (" + percentage + "%)";
            }

            return returnResult;
        }

        /// <summary>
        /// Gets Deposit Not Returned Amount And Percentage
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Deposit Not Returned Amount And Percentage</returns>
        public virtual string GetDepositNotReturnedAmountAndPercentage(int customerId) //Jerry 20180814 MSP-45
        {
            string returnResult = "";

            if (customerId == 0)
                return returnResult;

            var data =
                (from wallet in _walletRepository.Table
                 where wallet.CustomerID == customerId
                 select wallet
                ).FirstOrDefault();

            if (data != null)
            {
                returnResult = _mspHelper.TruncateDecimalToString_MBTC(0) + "mBTC (0%)";
                decimal Deposit_Total = data.Deposit_Total;
                decimal Deposit_Return_Total = data.Deposit_Return_Total;

                if (Deposit_Total <= 0 || Deposit_Return_Total <= 0)
                {
                    return returnResult;
                }

                decimal percentage = Math.Round(((Deposit_Total - Deposit_Return_Total) / Deposit_Total) * 100, 2);

                returnResult = _mspHelper.TruncateDecimalToString_MBTC(Deposit_Total - Deposit_Return_Total) + "mBTC (" + percentage + "%)";
            }

            return returnResult;
        }

        #endregion

        #region Consumption

        public string GetTotalSelfConsumptionRewardEarned(int CustomerId) //Jerry 20181008 MSP-252
        {
            string totalAmount = "";

            StringBuilder sp = new StringBuilder();
            //sp.Append("EXEC [usp_MSP_API_TotalSelfConsumptionRewardEarned] "); //Jerry 20181016 MSP-252
            sp.Append("EXEC [usp_MSP_API_TotalConsumptionRewardEarnedSelf] "); //Jerry 20181016 MSP-252
            sp.Append("@CustomerID");
            sp.Append(",@outReturnCode OUTPUT");
            sp.Append(",@outReturnMessage OUTPUT");
            sp.Append(",@outTotalRewardEarned OUTPUT");

            #region Output Parameter

            SqlParameter pReturnCode = new SqlParameter();
            pReturnCode.ParameterName = "@outReturnCode";
            pReturnCode.DbType = DbType.Int32;
            pReturnCode.Direction = ParameterDirection.Output;

            SqlParameter pReturnMessage = new SqlParameter();
            pReturnMessage.ParameterName = "@outReturnMessage";
            pReturnMessage.DbType = DbType.String;
            pReturnMessage.Size = 200;
            pReturnMessage.Direction = ParameterDirection.Output;

            SqlParameter pTotalRewardEarned = new SqlParameter();
            pTotalRewardEarned.ParameterName = "@outTotalRewardEarned";
            pTotalRewardEarned.DbType = DbType.Decimal;
            pTotalRewardEarned.Direction = ParameterDirection.Output;
            pTotalRewardEarned.Scale = 6;
            pTotalRewardEarned.Precision = 14;

            #endregion Output Parameter

            #region Execute Stored Procedure
            var result = _dbContext.SqlQuery<string>(
                sp.ToString()
                , new SqlParameter("@CustomerID", CustomerId)
                , pReturnCode
                , pReturnMessage
                , pTotalRewardEarned
                ).FirstOrDefault();

            #endregion Execute Stored Procedure

            #region Response Result
            totalAmount = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(pTotalRewardEarned.SqlValue.ToString()));
            #endregion

            return totalAmount;
        }

        public string GetTotalTeamConsumptionRewardEarned(int CustomerId) //Jerry 20181008 MSP-252
        {
            string totalAmount = "";

            StringBuilder sp = new StringBuilder();
            //sp.Append("EXEC [usp_MSP_API_TotalTeamConsumptionRewardEarned] "); //Jerry 20181016 MSP-252
            sp.Append("EXEC [usp_MSP_API_TotalConsumptionRewardEarnedTeam] "); //Jerry 20181016 MSP-252
            sp.Append("@CustomerID");
            sp.Append(",@outReturnCode OUTPUT");
            sp.Append(",@outReturnMessage OUTPUT");
            sp.Append(",@outTotalRewardEarned OUTPUT");

            #region Output Parameter

            SqlParameter pReturnCode = new SqlParameter();
            pReturnCode.ParameterName = "@outReturnCode";
            pReturnCode.DbType = DbType.Int32;
            pReturnCode.Direction = ParameterDirection.Output;

            SqlParameter pReturnMessage = new SqlParameter();
            pReturnMessage.ParameterName = "@outReturnMessage";
            pReturnMessage.DbType = DbType.String;
            pReturnMessage.Size = 200;
            pReturnMessage.Direction = ParameterDirection.Output;

            SqlParameter pTotalRewardEarned = new SqlParameter();
            pTotalRewardEarned.ParameterName = "@outTotalRewardEarned";
            pTotalRewardEarned.DbType = DbType.Decimal;
            pTotalRewardEarned.Direction = ParameterDirection.Output;
            pTotalRewardEarned.Scale = 6;
            pTotalRewardEarned.Precision = 14;

            #endregion Output Parameter

            #region Execute Stored Procedure

            var result = _dbContext.SqlQuery<string>(
                sp.ToString()
                , new SqlParameter("@CustomerID", CustomerId)
                , pReturnCode
                , pReturnMessage
                , pTotalRewardEarned
                ).FirstOrDefault();

            #endregion Execute Stored Procedure

            #region Response Result
            totalAmount = _mspHelper.TruncateDecimalToString_MBTC(Convert.ToDecimal(pTotalRewardEarned.SqlValue.ToString()));
            #endregion

            return totalAmount;
        }

        #endregion Consumption

        #region Interface //Chew 20190129 MDT-205

        ///// <summary>
        ///// Gets a customer's interface
        ///// </summary>
        ///// <param name="customerId">Customer identifier</param>
        ///// <returns>A consumer interface</returns>
        //public virtual MSP_Interface_Consumer GetConsumerInterface(int customerId) //Jerry 20180912 MSP-45
        //{
        //    if (customerId == 0)
        //        return null;

        //    var returnResult =
        //        (from data in _interfaceRepository.Table
        //         where data.CustomerID == customerId
        //         select data
        //        ).FirstOrDefault();

        //    return returnResult;
        //}


        #endregion

        #region Update MemberTree
        /// <summary>
        /// Updates the MSP Member
        /// </summary>
        /// <param name="member">Customer</param>
        public virtual void UpdateMemberWithdrawalLimit(MSP_MemberTree member) //Tony Liew 20181121 MDT-8  
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            _memberTreeRepository.Update(member);

            //event notification
            _eventPublisher.EntityUpdated(member);
        }
        #endregion
        #region Prepare Customer With isSync, UpdatedBy , UpdatedOnUtc

        public Customer PrepareCustomerSyncUpdatedInfo(Customer AssignedCustomer , Customer LoggedInCustomer = null, string SyncType = null)  //LeeChurn 20181031 MSP-410
        {

            if (LoggedInCustomer!=null)   //from front End / Back Office
            {
                AssignedCustomer.UpdatedOnUtc = DateTime.UtcNow;
                AssignedCustomer.UpdatedBy = LoggedInCustomer.Id;
                return AssignedCustomer;
            }
            else    // from API
            {
                if (SyncType == GlobalSettingEnum.MSPPrepareCustomerSyncTypeFirstTimeCreate)
                {
                    return AssignedCustomer;
                }
                else
                {
                    AssignedCustomer.UpdatedOnUtc = DateTime.UtcNow;
                    AssignedCustomer.UpdatedBy = 0;
                    return AssignedCustomer;
                }
                    
            }    
        }
        #endregion

        #endregion

        //Tony Liew 20190308 RDT-69 \/

        /// <summary>
        /// Insert old customer password 
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        public void InsertCustomerPasswordHistory(CustomerPassword_History customerPassword)
        {
            if (customerPassword == null)
                throw new ArgumentNullException(nameof(customerPassword));

            _customerPasswordHistoryRepository.Insert(customerPassword);

            //event notification
            _eventPublisher.EntityInserted(customerPassword);
        }
        //Tony Liew 20190308 RDT-69 /\
        #endregion

        /// <summary>
        /// Get CustomerId by email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Customer</returns>
        public int GetCustomerIdByEmail(string email)
        {
            var query = from c in _customerRepository.Table
                        where c.Email == email
                        select c.Id;
            return query != null ? query.FirstOrDefault() : 0;
        }

        /// <summary>
        /// Get CustomerGuid by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Guid? GetCustomerGuidByUsername(string username)
        {
            var query = from c in _customerRepository.TableNoTracking
                        where c.Username == username
                        select c.CustomerGuid;
            return query.FirstOrDefault();
        }
    }
}