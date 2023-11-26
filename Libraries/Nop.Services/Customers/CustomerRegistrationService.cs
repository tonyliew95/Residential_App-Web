using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Stores;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Nop.Services.Customers
{
    /// <summary>
    /// Customer registration service
    /// </summary>
    public partial class CustomerRegistrationService : ICustomerRegistrationService
    {
        #region Fields

        private const int SALT_KEY_SIZE = 5;

        private readonly ICustomerService _customerService;
        private readonly IEncryptionService _encryptionService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreService _storeService;
        private readonly IRewardPointService _rewardPointService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IWorkContext _workContext;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly IEventPublisher _eventPublisher;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly IRepository<CustomerPassword> _customerPassword;
       

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="customerService">Customer service</param>
        /// <param name="encryptionService">Encryption service</param>
        /// <param name="newsLetterSubscriptionService">Newsletter subscription service</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="storeService">Store service</param>
        /// <param name="rewardPointService">Reward points service</param>
        /// <param name="genericAttributeService">Generic attribute service</param>
        /// <param name="workContext">Work context</param>
        /// <param name="workflowMessageService">Workflow message service</param>
        /// <param name="eventPublisher">Event publisher</param>
        /// <param name="rewardPointsSettings">Reward points settings</param>
        /// <param name="customerSettings">Customer settings</param>
        public CustomerRegistrationService(ICustomerService customerService,
            IEncryptionService encryptionService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            ILocalizationService localizationService,
            IStoreService storeService,
            IRewardPointService rewardPointService,
            IWorkContext workContext,
            IGenericAttributeService genericAttributeService,
            IWorkflowMessageService workflowMessageService,
            IEventPublisher eventPublisher,
            RewardPointsSettings rewardPointsSettings,
            CustomerSettings customerSettings,
            IRepository<CustomerPassword> customerPassword
            )
        {
            this._customerService = customerService;
            this._encryptionService = encryptionService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._localizationService = localizationService;
            this._storeService = storeService;
            this._rewardPointService = rewardPointService;
            this._genericAttributeService = genericAttributeService;
            this._workContext = workContext;
            this._workflowMessageService = workflowMessageService;
            this._eventPublisher = eventPublisher;
            this._rewardPointsSettings = rewardPointsSettings;
            this._customerSettings = customerSettings;
            this._customerPassword = customerPassword;
        }

        #endregion

        #region Utilities

        #region Password Match
        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        public virtual bool PasswordsMatch(CustomerPassword customerPassword, string enteredPassword , string deviceUuid = "")
        {
            if (customerPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch (customerPassword.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    savedPassword = enteredPassword;
                    break;
                case PasswordFormat.Encrypted:
                    savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.Hashed:
                    if (string.IsNullOrEmpty(deviceUuid))
                    {
                        HashAlgorithm hash = new SHA1Managed();
                        string sha1Hasded = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));
                        savedPassword = _encryptionService.CreatePasswordHash(sha1Hasded, customerPassword.PasswordSalt, _customerSettings.HashedPasswordFormat);// Generate SHA512 hashing algorithm for web//Tony Liew 20190311 RDT-69 
                    }
                    else
                        savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, customerPassword.PasswordSalt, _customerSettings.HashedPasswordFormat);// Generate SHA512 hashing algorithm for web//Tony Liew 20190311 RDT-69 

                    break;

            }

            if (customerPassword.Password == null)
                return false;

            return customerPassword.Password.Equals(savedPassword); // If entered password with hashing and saved password match, return true //Tony Liew 20190311 RDT-69
        }
        #endregion

        #region Check previous password in CustomerPassword_History table
        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        public virtual bool PasswordsMatch(CustomerPassword_History customerPassword, string enteredPassword, string deviceUuid = "") //Tony Liew 20190311 RDT-69
        {
            if (customerPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch (customerPassword.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    savedPassword = enteredPassword;
                    break;
                case PasswordFormat.Encrypted:
                    savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.Hashed:
                    if (string.IsNullOrEmpty(deviceUuid))
                    {
                        HashAlgorithm hash = new SHA1Managed();
                        string sha1Hasded = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));// Generate SHA1 hashing algorithm for web//Tony Liew 20190311 RDT-69
                        savedPassword = _encryptionService.CreatePasswordHash(sha1Hasded, customerPassword.PasswordSalt, _customerSettings.HashedPasswordFormat);// Generate SHA512 hashing algorithm for web//Tony Liew 20190311 RDT-69 
                    }
                    else
                        savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, customerPassword.PasswordSalt, _customerSettings.HashedPasswordFormat);// Generate SHA512 hashing algorithm for web//Tony Liew 20190311 RDT-69 

                    break;
            }

            if (customerPassword.Password == null)
                return false;

            return customerPassword.Password.Equals(savedPassword); // If entered password with hashing and saved password match, return true //Tony Liew 20190311 RDT-69
        }

        #endregion

        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="customerPassword">Customer password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        public virtual bool TransactionPasswordsMatch(CustomerTransactionPassword customerTransactionPassword, string enteredPassword) //Jerry 20180814 MSP-45
        {
            if (customerTransactionPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;
            switch (customerTransactionPassword.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    savedPassword = enteredPassword;
                    break;
                case PasswordFormat.Encrypted:
                    savedPassword = _encryptionService.EncryptText(enteredPassword);
                    break;
                case PasswordFormat.Hashed:
                    savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, customerTransactionPassword.PasswordSalt, _customerSettings.HashedPasswordFormat);
                    break;
            }

            if (customerTransactionPassword.Password == null)
                return false;

            return customerTransactionPassword.Password.Equals(savedPassword);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual CustomerLoginResults ValidateCustomer(string usernameOrEmail, string password , string deviceUuid = "") //Tony Liew 20190308 RDT-69
        {
            var customer = _customerSettings.UsernamesEnabled ?
                _customerService.GetCustomerByUsername(usernameOrEmail) :
                _customerService.GetCustomerByEmail(usernameOrEmail);
            
            if (customer == null)
                return CustomerLoginResults.CustomerNotExist;
            if (customer.Deleted)
                return CustomerLoginResults.Deleted;
            if (!customer.Active)
                return CustomerLoginResults.NotActive;
            if (customer.IsMember)
                return CustomerLoginResults.IsMember;
            //only registered can login
            if (!customer.IsRegistered())
                return CustomerLoginResults.NotRegistered;

            #region Commend Code wailiang 20181213 MDT-133 (Will Cause Issue in login)
            //Atiqah 20181002 MSP-186 \/
            //if (!customer.IsAdmin() && !customer.IsCSAdmin() && !customer.IsCS() && !customer.IsFinance() && !customer.IsAOD()) //Tony 20181030 MSP-411  //wailiang 20181213 MDT-133
            //    return CustomerLoginResults.NonAdmin;
            //Atiqah 20181002 MSP-186 /\
            #endregion

            //check whether a customer is locked out
            if (customer.CannotLoginUntilDateUtc.HasValue && customer.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
                return CustomerLoginResults.LockedOut;

            //Tony Liew 20190308 RDT-69 \/
          
                if (!PasswordsMatch(_customerService.GetCurrentPassword(customer.Id), password , deviceUuid)) 
                {
                    //wrong password
                    customer.FailedLoginAttempts++;
                    if (_customerSettings.FailedPasswordAllowedAttempts > 0 &&
                        customer.FailedLoginAttempts >= _customerSettings.FailedPasswordAllowedAttempts)
                    {
                        //lock out
                        customer.CannotLoginUntilDateUtc = DateTime.UtcNow.AddMinutes(_customerSettings.FailedPasswordLockoutMinutes);
                        //reset the counter
                        customer.FailedLoginAttempts = 0;
                    }
                    _customerService.UpdateCustomer(customer);

                    return CustomerLoginResults.WrongPassword;
                }

            //Tony Liew 20190308 RDT-69 /\

            //update login details
            customer.FailedLoginAttempts = 0;
            customer.CannotLoginUntilDateUtc = null;
            customer.RequireReLogin = false;
            customer.LastLoginDateUtc = DateTime.UtcNow;
            _customerService.UpdateCustomer(customer);

            return CustomerLoginResults.Successful;
        }

        /// <summary>
        /// Register customer
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual CustomerRegistrationResult RegisterCustomer(CustomerRegistrationRequest request , string deviceUuid = "" )
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Customer == null)
                throw new ArgumentException("Can't load current customer");

            var result = new CustomerRegistrationResult();
            if (request.Customer.IsSearchEngineAccount())
            {
                result.AddError("Search engine can't be registered");
                return result;
            }
            if (request.Customer.IsBackgroundTaskAccount())
            {
                result.AddError("Background task account can't be registered");
                return result;
            }
            if (request.Customer.IsRegistered())
            {
                result.AddError("Current customer is already registered");
                return result;
            }
            if (string.IsNullOrEmpty(request.Email))
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.EmailIsNotProvided"));
                return result;
            }
            if (!CommonHelper.IsValidEmail(request.Email))
            {
                result.AddError(_localizationService.GetResource("Common.WrongEmail"));
                return result;
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.PasswordIsNotProvided"));
                return result;
            }
            if (_customerSettings.UsernamesEnabled)
            {
                if (string.IsNullOrEmpty(request.Username))
                {
                    result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameIsNotProvided"));
                    return result;
                }
            }

            //Tony Liew 20190308 RDT-69 \/
            var customer = _customerService.GetCustomerByEmail(request.Email);
            var customerPasswordExist = (from pass in _customerPassword.Table where pass.CustomerId == customer.Id select pass).FirstOrDefault(); //Check whether the user password is in the customer password table

            //validate unique user
            if (customer != null && customerPasswordExist != null)
            {
                result.AddError(_localizationService.GetResource("Account.Register.Errors.EmailAlreadyExists"));
                return result;
            }
            if (_customerSettings.UsernamesEnabled)
            {
                if (_customerService.GetCustomerByUsername(request.Username) != null )
                {
                    result.AddError(_localizationService.GetResource("Account.Register.Errors.UsernameAlreadyExists"));
                    return result;
                }
            }
            //Tony Liew 20190308 RDT-69 /\

            //at this point request is valid
            request.Customer.Username = request.Username;
            request.Customer.Email = request.Email;

            var customerPassword = new CustomerPassword
            {
                Customer = request.Customer,
                PasswordFormat = request.PasswordFormat,
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow
            };
            switch (request.PasswordFormat)
            {
                case PasswordFormat.Clear:
                    customerPassword.Password = request.Password;
                    break;
                case PasswordFormat.Encrypted:
                    customerPassword.Password = _encryptionService.EncryptText(request.Password);
                    break;
                case PasswordFormat.Hashed:
                    {
                        var saltKey = _encryptionService.CreateSaltKey(SALT_KEY_SIZE);
                        customerPassword.PasswordSalt = saltKey;
                        if (string.IsNullOrEmpty(deviceUuid)) //if the user using web for register
                        {
                            HashAlgorithm hash = new SHA1Managed();
                            string sha1Hasded = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));  //Tony Liew 20190308 RDT-69
                            customerPassword.Password = _encryptionService.CreatePasswordHash(sha1Hasded, saltKey, _customerSettings.HashedPasswordFormat);
                        }
                        else //else the user using mobile for register
                            customerPassword.Password = _encryptionService.CreatePasswordHash(request.Password, saltKey, _customerSettings.HashedPasswordFormat);

                    }
                    break;
            }
            _customerService.InsertCustomerPassword(customerPassword);

            request.Customer.Active = request.IsApproved;

            //add to 'Registered' role
            var registeredRole = _customerService.GetCustomerRoleBySystemName(SystemCustomerRoleNames.Registered);
            if (registeredRole == null)
                throw new NopException("'Registered' role could not be loaded");
            request.Customer.CustomerRoles.Add(registeredRole);
            //remove from 'Guests' role
            var guestRole = request.Customer.CustomerRoles.FirstOrDefault(cr => cr.SystemName == SystemCustomerRoleNames.Guests);
            if (guestRole != null)
                request.Customer.CustomerRoles.Remove(guestRole);

            //Add reward points for customer registration (if enabled)
            if (_rewardPointsSettings.Enabled &&
                _rewardPointsSettings.PointsForRegistration > 0)
            {
                _rewardPointService.AddRewardPointsHistoryEntry(request.Customer,
                    _rewardPointsSettings.PointsForRegistration,
                    request.StoreId,
                    _localizationService.GetResource("RewardPoints.Message.EarnedForRegistration"));
            }

            _customerService.UpdateCustomer(request.Customer);

            //publish event
            _eventPublisher.Publish(new CustomerPasswordChangedEvent(customerPassword));

            return result;
        }

        /// <summary>
        /// Change password for Web
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual ChangePasswordResult ChangePassword(ChangePasswordRequest request, string deviceUuid = "") 
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new ChangePasswordResult();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.EmailIsNotProvided"));
                return result;
            }
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.PasswordIsNotProvided"));
                return result;
            }

            var customer = _customerService.GetCustomerByEmail(request.Email);
            if (customer == null)
            {
                result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.EmailNotFound"));
                return result;
            }

            if (request.ValidateRequest)
            {
                //request isn't valid
                if (!PasswordsMatch(_customerService.GetCurrentPassword(customer.Id), request.OldPassword , deviceUuid))
                {
                    result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.OldPasswordDoesntMatch"));
                    return result;
                }
            }

            //check for duplicates
            if (_customerSettings.UnduplicatedPasswordsNumber > 0)
            {
                //get some of previous passwords
                var previousPasswords = _customerService.GetPreviousCustomerPasswords(customer.Id, passwordsToReturn: _customerSettings.UnduplicatedPasswordsNumber);

                var newPasswordMatchesWithPrevious = previousPasswords.Any(password => PasswordsMatch(password, request.NewPassword, deviceUuid));
                if (newPasswordMatchesWithPrevious)
                {
                    result.AddError(_localizationService.GetResource("Account.ChangePassword.Errors.PasswordMatchesWithPrevious"));
                    return result;
                }
            }

            //at this point request is valid
            var customerPassword = new CustomerPassword
            {
                Customer = customer,
                PasswordFormat = request.NewPasswordFormat,
                UpdatedOnUtc = DateTime.UtcNow,
            };
            switch (request.NewPasswordFormat)
            {
                case PasswordFormat.Clear:
                    customerPassword.Password = request.NewPassword;
                    break;
                case PasswordFormat.Encrypted:
                    customerPassword.Password = _encryptionService.EncryptText(request.NewPassword);
                    break;
                case PasswordFormat.Hashed:
                    {
                        var saltKey = _encryptionService.CreateSaltKey(SALT_KEY_SIZE);
                        customerPassword.PasswordSalt = saltKey;
                        if (string.IsNullOrEmpty(deviceUuid))
                        {
                            HashAlgorithm hash = new SHA1Managed();
                            string sha1Hasded = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword)));  //Tony Liew 20190308 RDT-69
                            customerPassword.Password = _encryptionService.CreatePasswordHash(sha1Hasded, saltKey, _customerSettings.HashedPasswordFormat);  //Tony Liew 20190308 RDT-69
                        }
                        else
                            customerPassword.Password = _encryptionService.CreatePasswordHash(request.NewPassword, saltKey, _customerSettings.HashedPasswordFormat);  //Tony Liew 20190308 RDT-69
                    }
                    break;
            }
            //Tony Liew 20190308 RDT-69 \/
            var currentCustomerPassword = (from password in _customerPassword.Table
                                           where password.CustomerId == customer.Id
                                           select password).FirstOrDefault(); //get current customer password

            var oldPassword = new CustomerPassword_History();
            if (currentCustomerPassword != null)
            {
                #region old Password assignment
                oldPassword.CustomerId = currentCustomerPassword.CustomerId;
                oldPassword.Password = currentCustomerPassword.Password;
                oldPassword.PasswordFormatId = currentCustomerPassword.PasswordFormatId;
                oldPassword.PasswordSalt = currentCustomerPassword.PasswordSalt;
                oldPassword.CreatedOnUtc = DateTime.UtcNow;
                #endregion

                _customerService.InsertCustomerPasswordHistory(oldPassword); //Copy old password to the customer password history table

                #region new Password assignment
                currentCustomerPassword.Password = customerPassword.Password;
                currentCustomerPassword.PasswordSalt = customerPassword.PasswordSalt;
                currentCustomerPassword.UpdatedOnUtc = DateTime.UtcNow;
                currentCustomerPassword.UpdatedBy = customer.Id;
                #endregion

                _customerService.UpdateCustomerPassword(currentCustomerPassword); // update new password
                //Tony Liew 20190308 RDT-69 /\
            }
            else //customer password not found in customer password table
            {
                customerPassword.CreatedOnUtc = DateTime.UtcNow;
                customerPassword.Customer = customer;
                _customerService.InsertCustomerPassword(customerPassword);
            }
            //publish event
            _eventPublisher.Publish(new CustomerPasswordChangedEvent(customerPassword));

            return result;
        }

        /// <summary>
        /// Change transaction password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual ChangePasswordResult ChangeTransactionPassword(ChangePasswordRequest request) //Jerry 20180814 MSP-45
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new ChangePasswordResult();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                result.AddError(_localizationService.GetResource("Account.ChangeTransactionPassword.Errors.EmailIsNotProvided"));
                return result;
            }
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                result.AddError(_localizationService.GetResource("Account.ChangeTransactionPassword.Errors.PasswordIsNotProvided"));
                return result;
            }

            var customer = _customerService.GetCustomerByEmail(request.Email);
            if (customer == null)
            {
                result.AddError(_localizationService.GetResource("Account.ChangeTransactionPassword.Errors.EmailNotFound"));
                return result;
            }

            if (request.ValidateRequest)
            {
                //request isn't valid
                if (!PasswordsMatch(_customerService.GetCurrentPassword(customer.Id), request.OldPassword))
                {
                    result.AddError(_localizationService.GetResource("Account.ChangeTransactionPassword.Errors.OldPasswordDoesntMatch"));
                    return result;
                }
            }

            //check for duplicates
            if (_customerSettings.UnduplicatedPasswordsNumber > 0)
            {
                //get some of previous passwords
                var previousPasswords = _customerService.GetCustomerTransactionPasswords(customer.Id, passwordsToReturn: _customerSettings.UnduplicatedPasswordsNumber);

                var newPasswordMatchesWithPrevious = previousPasswords.Any(password => TransactionPasswordsMatch(password, request.NewPassword));
                if (newPasswordMatchesWithPrevious)
                {
                    result.AddError(_localizationService.GetResource("Account.ChangeTransactionPassword.Errors.PasswordMatchesWithPrevious"));
                    return result;
                }
            }

            //at this point request is valid
            var customerPassword = new CustomerTransactionPassword
            {
                Customer = customer,
                PasswordFormat = request.NewPasswordFormat,
                CreatedOnUtc = DateTime.UtcNow
            };
            switch (request.NewPasswordFormat)
            {
                case PasswordFormat.Clear:
                    customerPassword.Password = request.NewPassword;
                    break;
                case PasswordFormat.Encrypted:
                    customerPassword.Password = _encryptionService.EncryptText(request.NewPassword);
                    break;
                case PasswordFormat.Hashed:
                    {
                        var saltKey = _encryptionService.CreateSaltKey(SALT_KEY_SIZE);
                        customerPassword.PasswordSalt = saltKey;
                        customerPassword.Password = _encryptionService.CreatePasswordHash(request.NewPassword, saltKey, _customerSettings.HashedPasswordFormat);
                    }
                    break;
            }
            _customerService.InsertCustomerTransactionPassword(customerPassword);

            //publish event
            _eventPublisher.Publish(new CustomerTransactionPasswordChangedEvent(customerPassword));

            return result;
        }

        /// <summary>
        /// Sets a user email
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="newEmail">New email</param>
        /// <param name="requireValidation">Require validation of new email address</param>
        public virtual void SetEmail(Customer customer, string newEmail, bool requireValidation)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (newEmail == null)
                throw new NopException("Email cannot be null");

            newEmail = newEmail.Trim();
            var oldEmail = customer.Email;

            if (!CommonHelper.IsValidEmail(newEmail))
                throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.NewEmailIsNotValid"));

            if (newEmail.Length > 100)
                throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.EmailTooLong"));

            var customer2 = _customerService.GetCustomerByEmail(newEmail);
            if (customer2 != null && customer.Id != customer2.Id)
                throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.EmailAlreadyExists"));

            if (requireValidation)
            {
                //re-validate email
                customer.EmailToRevalidate = newEmail;
                _customerService.UpdateCustomer(customer);

                //email re-validation message
                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.EmailRevalidationToken, Guid.NewGuid().ToString());
                _workflowMessageService.SendCustomerEmailRevalidationMessage(customer, _workContext.WorkingLanguage.Id);
            }
            else
            {
                customer.Email = newEmail;
                _customerService.UpdateCustomer(customer);

                //update newsletter subscription (if required)
                if (!string.IsNullOrEmpty(oldEmail) && !oldEmail.Equals(newEmail, StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var store in _storeService.GetAllStores())
                    {
                        var subscriptionOld = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(oldEmail, store.Id);
                        if (subscriptionOld != null)
                        {
                            subscriptionOld.Email = newEmail;
                            _newsLetterSubscriptionService.UpdateNewsLetterSubscription(subscriptionOld);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sets a customer username
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="newUsername">New Username</param>
        public virtual void SetUsername(Customer customer, string newUsername)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            if (!_customerSettings.UsernamesEnabled)
                throw new NopException("Usernames are disabled");

            newUsername = newUsername.Trim();

            if (newUsername.Length > 100)
                throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.UsernameTooLong"));

            var user2 = _customerService.GetCustomerByUsername(newUsername);
            if (user2 != null && customer.Id != user2.Id)
                throw new NopException(_localizationService.GetResource("Account.EmailUsernameErrors.UsernameAlreadyExists"));

            customer.Username = newUsername;
            _customerService.UpdateCustomer(customer);
        }

        #endregion
    }
}