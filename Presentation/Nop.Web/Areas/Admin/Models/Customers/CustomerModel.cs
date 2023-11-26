using FluentValidation.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Web.Areas.Admin.Validators.Customers;
using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Web.Areas.Admin.Models.Customers
{
    [Validator(typeof(CustomerValidator))]
    public partial class CustomerModel : BaseNopEntityModel
    {
        public CustomerModel()
        {
            this.AvailableTimeZones = new List<SelectListItem>();
            this.SendEmail = new SendEmailModel() { SendImmediately = true };
            this.SendPm = new SendPmModel();

            this.SelectedCustomerRoleIds = new List<int>();
            this.AvailableCustomerRoles = new List<SelectListItem>();

            this.AssociatedExternalAuthRecords = new List<AssociatedExternalAuthModel>();
            this.AvailableCountries = new List<SelectListItem>();
            this.AvailableStates = new List<SelectListItem>();
            this.AvailableVendors = new List<SelectListItem>();
            this.CustomerAttributes = new List<CustomerAttributeModel>();
            this.AvailableNewsletterSubscriptionStores = new List<StoreModel>();
            this.RewardPointsAvailableStores = new List<SelectListItem>();

            this.SecurityQuestionAndAnswer = new List<SecurityQuestionAndAnswerModel>(); //Jerry 20180814 MSP-45

    }

        //MVC is suppressing further validation if the IFormCollection is passed to a controller method. That's why we add to the model
        public IFormCollection Form { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.ReferralName")]
        public string ReferralName { get; set; } //Jerry 20180824 MSP-45

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.UserId")]
        public int ReferralId { get; set; } //Jerry 20180824 MSP-45

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.SSO_GUID")]
        public string SSO_GUID { get; set; } //Jerry 20180824 MSP-45

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.UserId")]
        public int UserId { get; set; } //Jerry 20180814 MSP-45

        public bool UsernamesEnabled { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Username")]
        public string Username { get; set; }

        [DataType(DataType.EmailAddress)]
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Email")]
        public string Email { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Password")]
        [DataType(DataType.Password)]
        [NoTrim]
        public string Password { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.TransactionPassword")]
        [DataType(DataType.Password)]
        [NoTrim]
        public string TransactionPassword { get; set; } //Jerry 20180814 MSP-45

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Vendor")]
        public int VendorId { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }

        //form fields & properties
        public bool GenderEnabled { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Gender")]
        public string Gender { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.FirstName")]
        public string FirstName { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.LastName")]
        public string LastName { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.FullName")]
        public string FullName { get; set; }

        public bool DateOfBirthEnabled { get; set; }
        [UIHint("DateNullable")]
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        public bool CompanyEnabled { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Company")]
        public string Company { get; set; }

        public bool StreetAddressEnabled { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.StreetAddress")]
        public string StreetAddress { get; set; }

        public bool StreetAddress2Enabled { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.StreetAddress2")]
        public string StreetAddress2 { get; set; }

        public bool ZipPostalCodeEnabled { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.ZipPostalCode")]
        public string ZipPostalCode { get; set; }

        public bool CityEnabled { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.City")]
        public string City { get; set; }

        public bool CountryEnabled { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Country")]
        public int CountryId { get; set; }
        public IList<SelectListItem> AvailableCountries { get; set; }

        public bool StateProvinceEnabled { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.StateProvince")]
        public int StateProvinceId { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }

        public bool PhoneEnabled { get; set; }
        [DataType(DataType.PhoneNumber)]
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Phone")]
        public string Phone { get; set; }

        public bool FaxEnabled { get; set; }
        [DataType(DataType.PhoneNumber)]
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Fax")]
        public string Fax { get; set; }

        public List<CustomerAttributeModel> CustomerAttributes { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.RegisteredInStore")]
        public string RegisteredInStore { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.AdminComment")]
        public string AdminComment { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Active")]
        public bool Active { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Affiliate")]
        public int AffiliateId { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Affiliate")]
        public string AffiliateName { get; set; }

        //time zone
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.TimeZoneId")]
        public string TimeZoneId { get; set; }

        public bool AllowCustomersToSetTimeZone { get; set; }

        public IList<SelectListItem> AvailableTimeZones { get; set; }

        //EU VAT
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.VatNumber")]
        public string VatNumber { get; set; }

        public string VatNumberStatusNote { get; set; }

        public bool DisplayVatNumber { get; set; }

        //registration date
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.LastActivityDate")]
        public DateTime LastActivityDate { get; set; }

        //IP address
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.IPAddress")]
        public string LastIpAddress { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Fields.LastVisitedPage")]
        public string LastVisitedPage { get; set; }

        //Membership
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.IPAddress_Finsys")]
        public string IPAddress_Finsys { get; set; } //Jerry 20180814 MSP-45
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.IPAddress_Game")]
        public string IPAddress_Game { get; set; } //Jerry 20180814 MSP-45

        //SecurityQuestionAndAnswer
        public List<SecurityQuestionAndAnswerModel> SecurityQuestionAndAnswer { get; set; } //Jerry 20180814 MSP-45

        #region Revenue

        //Jerry 20180814 MSP-45 \/
        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Deposit_BTC_Address")]
        public string Deposit_BTC_Address { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Withdrawal_BTC_Address")]
        public string Withdrawal_BTC_Address { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Available_Balance")]
        public string Available_Balance { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Deposit_Wallet")]
        public string Deposit_Wallet { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Total_Reward_Earned")]
        public string Total_Reward_Earned { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Deposit_Returned_Amount")]
        public string Deposit_Returned_Amount { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Deposit_Not_Returned_Amount")]
        public string Deposit_Not_Returned_Amount { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Current_Score_Y")]
        public string Current_Score_Y { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Current_Score_Y_Percentage")]
        public string Current_Score_Y_Percentage { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Current_Score_Z")]
        public string Current_Score_Z { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Current_Score_Z_Percentage")]
        public string Current_Score_Z_Percentage { get; set; }
        //Jerry 20180814 MSP-45 /\

        //Jerry 20180824 MSP-45 \/
        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Total_Self_Reward_Earned")]
        public string Total_Self_Reward_Earned { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Total_Team_Reward_Earned")]
        public string Total_Team_Reward_Earned { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.Revenue.Redeem_Wallet")]
        public string Redeem_Wallet { get; set; }
        //Jerry 20180824 MSP-45 /\

        #endregion

        //customer roles
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.CustomerRoles")]
        public string CustomerRoleNames { get; set; }
        public List<SelectListItem> AvailableCustomerRoles { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.CustomerRoles")]
        public IList<int> SelectedCustomerRoleIds { get; set; }

        //newsletter subscriptions (per store)
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Newsletter")]
        public List<StoreModel> AvailableNewsletterSubscriptionStores { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.Newsletter")]
        public int[] SelectedNewsletterSubscriptionStoreIds { get; set; }

        //reward points history
        public bool DisplayRewardPointsHistory { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsValue")]
        public int AddRewardPointsValue { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsMessage")]
        public string AddRewardPointsMessage { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsStore")]
        public int AddRewardPointsStoreId { get; set; }
        [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsStore")]
        public IList<SelectListItem> RewardPointsAvailableStores { get; set; }

        //send email model
        public SendEmailModel SendEmail { get; set; }
        //send PM model
        public SendPmModel SendPm { get; set; }
        //send the welcome message
        public bool AllowSendingOfWelcomeMessage { get; set; }
        //re-send the activation message
        public bool AllowReSendingOfActivationMessage { get; set; }

        [NopResourceDisplayName("Admin.Customers.Customers.AssociatedExternalAuth")]
        public IList<AssociatedExternalAuthModel> AssociatedExternalAuthRecords { get; set; }

        //Check whether the current customer is Customer Service
        public bool IsCS { get; set; }        // Tony Liew 20181030 MSP-411 

        //Check whether the current customer is Customer Service Admin
        public bool IsCSAdmin { get; set; }    // Tony Liew 20181030 MSP-411  

        //Check whether the current customer is Admin
        public bool IsAdmin { get; set; } // Tony Liew 20181030 MSP-411

        //wailiang 20181213 MDT-133 \/
        //Check whether the current customer is Admin
        public bool IsAOD { get; set; } 

        //Check whether the current customer is Admin
        public bool IsFinance { get; set; }
        //wailiang 20181213 MDT-133 /\

        //Check whether the current user is Status Editable
        public bool IsStatusEditable { get; set; } // Lee Churn 20181102 MSP-439

        //Check whether the customer in a particular row is member
        public bool CustomerIsMember { get; set; }

        //Check whether the customer in a particular row is Us Citizen
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.IsUsCitizen")]
        public bool IsUsCitizen { get; set; }

        //Get UserRole
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.UserRole")]
        public string UserRole { get; set; }

        //Get CurrentCustomerID
        public int CurrentCustomerID { get; set; } // Tony Liew 20181030 MSP-411 

        //Get Is Able To Create
        public bool IsAbleToCreate { get; set; } // Tony Liew 20181030 MSP-411 

        //Get ChangedPasswordEnabled
        public bool ChangedPasswordEnabled { get; set; } // Tony Liew 20181030 MSP-411 

        //Check whether the current user is Editable
        public bool IsEditable { get; set; } // Tony Liew 20181030 MSP-411

        //Get Withdrawal Limit
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.WithdrawalLimit")]
        public decimal WithdrawalLimit { get; set; } //Tony Liew 20181121 MDT-8 

        //Get Withdrawal Limit VIP 
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.WithdrawalLimitVIP")]
        public decimal WithdrawalLimitVIP { get; set; } //Tony Liew 20181121 MDT-8 

        //Get Withdrawal Enabled
        [NopResourceDisplayName("Admin.Customers.Customers.Fields.WithdrawalEnabled")]
        public bool WithdrawalEnabled { get; set; } //Tony Liew 20181121 MDT-8 


        #region Nested classes

        public partial class StoreModel : BaseNopEntityModel
        {
            public string Name { get; set; }
        }

        public partial class AssociatedExternalAuthModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("Admin.Customers.Customers.AssociatedExternalAuth.Fields.Email")]
            public string Email { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.AssociatedExternalAuth.Fields.ExternalIdentifier")]
            public string ExternalIdentifier { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.AssociatedExternalAuth.Fields.AuthMethodName")]
            public string AuthMethodName { get; set; }
        }

        public partial class RewardPointsHistoryModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.Store")]
            public string StoreName { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.Points")]
            public int Points { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.PointsBalance")]
            public string PointsBalance { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.Message")]
            public string Message { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.RewardPoints.Fields.Date")]
            public DateTime CreatedOn { get; set; }
        }

        public partial class SendEmailModel : BaseNopModel
        {
            [NopResourceDisplayName("Admin.Customers.Customers.SendEmail.Subject")]
            public string Subject { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.SendEmail.Body")]
            public string Body { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.SendEmail.SendImmediately")]
            public bool SendImmediately { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.SendEmail.DontSendBeforeDate")]
            [UIHint("DateTimeNullable")]
            public DateTime? DontSendBeforeDate { get; set; }
        }

        public partial class SendPmModel : BaseNopModel
        {
            [NopResourceDisplayName("Admin.Customers.Customers.SendPM.Subject")]
            public string Subject { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.SendPM.Message")]
            public string Message { get; set; }
        }

        public partial class OrderModel : BaseNopEntityModel
        {
            public override int Id { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Orders.CustomOrderNumber")]
            public string CustomOrderNumber { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.Orders.OrderStatus")]
            public string OrderStatus { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.Orders.OrderStatus")]
            public int OrderStatusId { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.Orders.PaymentStatus")]
            public string PaymentStatus { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.Orders.ShippingStatus")]
            public string ShippingStatus { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.Orders.OrderTotal")]
            public string OrderTotal { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.Orders.Store")]
            public string StoreName { get; set; }

            [NopResourceDisplayName("Admin.Customers.Customers.Orders.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }

        public partial class ActivityLogModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("Admin.Customers.Customers.ActivityLog.ActivityLogType")]
            public string ActivityLogTypeName { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.ActivityLog.Comment")]
            public string Comment { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.ActivityLog.CreatedOn")]
            public DateTime CreatedOn { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.ActivityLog.IpAddress")]
            public string IpAddress { get; set; }
        }

        public partial class BackInStockSubscriptionModel : BaseNopEntityModel
        {
            [NopResourceDisplayName("Admin.Customers.Customers.BackInStockSubscriptions.Store")]
            public string StoreName { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.BackInStockSubscriptions.Product")]
            public int ProductId { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.BackInStockSubscriptions.Product")]
            public string ProductName { get; set; }
            [NopResourceDisplayName("Admin.Customers.Customers.BackInStockSubscriptions.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }

        public partial class CustomerAttributeModel : BaseNopEntityModel
        {
            public CustomerAttributeModel()
            {
                Values = new List<CustomerAttributeValueModel>();
            }

            public string Name { get; set; }

            public bool IsRequired { get; set; }

            /// <summary>
            /// Default value for textboxes
            /// </summary>
            public string DefaultValue { get; set; }

            public AttributeControlType AttributeControlType { get; set; }

            public IList<CustomerAttributeValueModel> Values { get; set; }
        }

        public partial class CustomerAttributeValueModel : BaseNopEntityModel
        {
            public string Name { get; set; }

            public bool IsPreSelected { get; set; }
        }

        //Jerry 20180814 MSP-45 \/
        public partial class SecurityQuestionAndAnswerModel : BaseNopEntityModel
        {
            public SecurityQuestionAndAnswerModel()
            {
            }

            public string SecurityQuestionLabel { get; set; }

            public string SecurityQuestion { get; set; }

            public string SecurityAnswerLabel { get; set; }

            public string SecurityAnswer { get; set; }
        }
        //Jerry 20180814 MSP-45 /\

        #endregion
    }
}