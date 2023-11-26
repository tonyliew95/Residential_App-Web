using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Messages;
using Nop.Data;
using Nop.Plugin.Api.Services.Interface;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Core.Domain.Directory;

namespace Nop.Plugin.Api.Services
{
    /// <summary>
    /// Account API Service
    /// </summary>
    public class AccountApiService : IAccountApiService
    {
        //private readonly CommonSettings _commonSettings;
        //private readonly CustomerSettings _customerSettings;
        //private readonly ICustomerRegistrationService _customerRegistrationService;
        //private readonly ICustomerService _customerService;
        //private readonly IDataProvider _dataProvider;
        //private readonly IDbContext _dbContext;
        //private readonly IEncryptionService _encryptionService;
        //private readonly IEventPublisher _eventPublisher;
        //private readonly IMspHelper _mspHelper;
        //private readonly IQueuedEmailService _queuedEmailService;
        //private readonly IRepository<Customer> _customerRepository;
        //private readonly IRepository<CustomerPassword> _customerPasswordRepository;
        //private readonly IRepository<QueuedEmail> _queuedEmailRepository;
        //private readonly IStoreContext _storeContext;
        //private readonly IWorkflowMessageService _workflowMessageService;
        //private readonly IRepository<Country> _countryRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        public AccountApiService
            (
            //  IRepository<QueuedEmail> QueuedEmailRepositoty
            //, CommonSettings commonSettings
            //, CustomerSettings customerSettings
            //, ICustomerRegistrationService CustomerRegistrationService
            //, ICustomerService customerService
            //, IDataProvider dataProvider
            //, IDbContext dbContext
            //, IEncryptionService encryptionService
            //, IEventPublisher eventPublisher
            //, IMspHelper mspHelper
            //, IQueuedEmailService queuedEmailService
            //, IRepository<Customer> CustomerRepository
            //, IRepository<CustomerPassword> CustomerPasswordRepository
            //, IStoreContext storeContext
            //, IWorkflowMessageService workflowMessageService
            //, IRepository<Country> countryRepository
            )
        {
            //_commonSettings = commonSettings;
            //_customerPasswordRepository = CustomerPasswordRepository;
            //_customerRegistrationService = CustomerRegistrationService;
            //_customerRepository = CustomerRepository;
            //_customerService = customerService;
            //_customerSettings = customerSettings;
            //_dataProvider = dataProvider;
            //_dbContext = dbContext;
            //_encryptionService = encryptionService;
            //_eventPublisher = eventPublisher;
            //_mspHelper = mspHelper;
            //_queuedEmailRepository = QueuedEmailRepositoty;
            //_queuedEmailService = queuedEmailService;
            //_storeContext = storeContext;
            //_workflowMessageService = workflowMessageService;
            //_countryRepository = countryRepository;
        }

    }
}
