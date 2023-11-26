using System.Collections.Generic;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Security;

namespace Nop.Services.Security
{
    /// <summary>
    /// Standard permission provider
    /// </summary>
    public partial class StandardPermissionProvider : IPermissionProvider
    {
        //admin area permissions
        public static readonly PermissionRecord AccessAdminPanel = new PermissionRecord { Name = "Access admin area", SystemName = "AccessAdminPanel", Category = "Standard" };
        public static readonly PermissionRecord AllowCustomerImpersonation = new PermissionRecord { Name = "Admin area. Allow Customer Impersonation", SystemName = "AllowCustomerImpersonation", Category = "Customers" };
        public static readonly PermissionRecord ManageProducts = new PermissionRecord { Name = "Admin area. Manage Products", SystemName = "ManageProducts", Category = "Catalog" };
        public static readonly PermissionRecord ManageCategories = new PermissionRecord { Name = "Admin area. Manage Categories", SystemName = "ManageCategories", Category = "Catalog" };
        public static readonly PermissionRecord ManageManufacturers = new PermissionRecord { Name = "Admin area. Manage Manufacturers", SystemName = "ManageManufacturers", Category = "Catalog" };
        public static readonly PermissionRecord ManageProductReviews = new PermissionRecord { Name = "Admin area. Manage Product Reviews", SystemName = "ManageProductReviews", Category = "Catalog" };
        public static readonly PermissionRecord ManageProductTags = new PermissionRecord { Name = "Admin area. Manage Product Tags", SystemName = "ManageProductTags", Category = "Catalog" };
        public static readonly PermissionRecord ManageAttributes = new PermissionRecord { Name = "Admin area. Manage Attributes", SystemName = "ManageAttributes", Category = "Catalog" };
        // Tony Liew 20190115 MDT-200\/
        public static readonly PermissionRecord ManageCustomersRole = new PermissionRecord { Name = "Admin area. Manage Customer Roles", SystemName = "ManageCustomerRoles", Category = "Customers" };
        public static readonly PermissionRecord ManageCustomers = new PermissionRecord { Name = "Admin area. Manage Customers", SystemName = "ManageCustomers", Category = "Customers" };
        // Tony Liew 20190115 MDT-200 /\
        public static readonly PermissionRecord ManageVendors = new PermissionRecord { Name = "Admin area. Manage Vendors", SystemName = "ManageVendors", Category = "Customers" };
        public static readonly PermissionRecord ManageCurrentCarts = new PermissionRecord { Name = "Admin area. Manage Current Carts", SystemName = "ManageCurrentCarts", Category = "Orders" };
        public static readonly PermissionRecord ManageOrders = new PermissionRecord { Name = "Admin area. Manage Orders", SystemName = "ManageOrders", Category = "Orders" };
        public static readonly PermissionRecord ManageRecurringPayments = new PermissionRecord { Name = "Admin area. Manage Recurring Payments", SystemName = "ManageRecurringPayments", Category = "Orders" };
        public static readonly PermissionRecord ManageGiftCards = new PermissionRecord { Name = "Admin area. Manage Gift Cards", SystemName = "ManageGiftCards", Category = "Orders" };
        public static readonly PermissionRecord ManageReturnRequests = new PermissionRecord { Name = "Admin area. Manage Return Requests", SystemName = "ManageReturnRequests", Category = "Orders" };
        public static readonly PermissionRecord OrderCountryReport = new PermissionRecord { Name = "Admin area. Access order country report", SystemName = "OrderCountryReport", Category = "Orders" };
        public static readonly PermissionRecord ManageAffiliates = new PermissionRecord { Name = "Admin area. Manage Affiliates", SystemName = "ManageAffiliates", Category = "Promo" };
        public static readonly PermissionRecord ManageCampaigns = new PermissionRecord { Name = "Admin area. Manage Campaigns", SystemName = "ManageCampaigns", Category = "Promo" };
        public static readonly PermissionRecord ManageDiscounts = new PermissionRecord { Name = "Admin area. Manage Discounts", SystemName = "ManageDiscounts", Category = "Promo" };
        public static readonly PermissionRecord ManageNewsletterSubscribers = new PermissionRecord { Name = "Admin area. Manage Newsletter Subscribers", SystemName = "ManageNewsletterSubscribers", Category = "Promo" };
        public static readonly PermissionRecord ManagePolls = new PermissionRecord { Name = "Admin area. Manage Polls", SystemName = "ManagePolls", Category = "Content Management" };
        public static readonly PermissionRecord ManageNews = new PermissionRecord { Name = "Admin area. Manage News", SystemName = "ManageNews", Category = "Content Management" };
        public static readonly PermissionRecord ManageBlog = new PermissionRecord { Name = "Admin area. Manage Blog", SystemName = "ManageBlog", Category = "Content Management" };
        public static readonly PermissionRecord ManageWidgets = new PermissionRecord { Name = "Admin area. Manage Widgets", SystemName = "ManageWidgets", Category = "Content Management" };
        public static readonly PermissionRecord ManageTopics = new PermissionRecord { Name = "Admin area. Manage Topics", SystemName = "ManageTopics", Category = "Content Management" };
        public static readonly PermissionRecord ManageForums = new PermissionRecord { Name = "Admin area. Manage Forums", SystemName = "ManageForums", Category = "Content Management" };
        public static readonly PermissionRecord ManageMessageTemplates = new PermissionRecord { Name = "Admin area. Manage Message Templates", SystemName = "ManageMessageTemplates", Category = "Content Management" };

		// WKK 20181227 MDT-183
		public static readonly PermissionRecord ManageGrcAnnouncements = new PermissionRecord { Name = "Admin area. Manage Grc Announcements", SystemName = "ManageGrcAnnouncements", Category = "Content Management" };

		public static readonly PermissionRecord ManageCountries = new PermissionRecord { Name = "Admin area. Manage Countries", SystemName = "ManageCountries", Category = "Configuration" };
        public static readonly PermissionRecord ManageLanguages = new PermissionRecord { Name = "Admin area. Manage Languages", SystemName = "ManageLanguages", Category = "Configuration" };
        public static readonly PermissionRecord ManageSettings = new PermissionRecord { Name = "Admin area. Manage Settings", SystemName = "ManageSettings", Category = "Configuration" };
        public static readonly PermissionRecord ManagePaymentMethods = new PermissionRecord { Name = "Admin area. Manage Payment Methods", SystemName = "ManagePaymentMethods", Category = "Configuration" };
        public static readonly PermissionRecord ManageExternalAuthenticationMethods = new PermissionRecord { Name = "Admin area. Manage External Authentication Methods", SystemName = "ManageExternalAuthenticationMethods", Category = "Configuration" };
        public static readonly PermissionRecord ManageTaxSettings = new PermissionRecord { Name = "Admin area. Manage Tax Settings", SystemName = "ManageTaxSettings", Category = "Configuration" };
        public static readonly PermissionRecord ManageShippingSettings = new PermissionRecord { Name = "Admin area. Manage Shipping Settings", SystemName = "ManageShippingSettings", Category = "Configuration" };
        public static readonly PermissionRecord ManageCurrencies = new PermissionRecord { Name = "Admin area. Manage Currencies", SystemName = "ManageCurrencies", Category = "Configuration" };
        public static readonly PermissionRecord ManageActivityLog = new PermissionRecord { Name = "Admin area. Manage Activity Log", SystemName = "ManageActivityLog", Category = "Configuration" };
        public static readonly PermissionRecord ManageAcl = new PermissionRecord { Name = "Admin area. Manage ACL", SystemName = "ManageACL", Category = "Configuration" };
        public static readonly PermissionRecord ManageEmailAccounts = new PermissionRecord { Name = "Admin area. Manage Email Accounts", SystemName = "ManageEmailAccounts", Category = "Configuration" };
        public static readonly PermissionRecord ManageStores = new PermissionRecord { Name = "Admin area. Manage Stores", SystemName = "ManageStores", Category = "Configuration" };
        public static readonly PermissionRecord ManagePlugins = new PermissionRecord { Name = "Admin area. Manage Plugins", SystemName = "ManagePlugins", Category = "Configuration" };
        public static readonly PermissionRecord ManageSystemLog = new PermissionRecord { Name = "Admin area. Manage System Log", SystemName = "ManageSystemLog", Category = "Configuration" };
        public static readonly PermissionRecord ManageMessageQueue = new PermissionRecord { Name = "Admin area. Manage Message Queue", SystemName = "ManageMessageQueue", Category = "Configuration" };
        public static readonly PermissionRecord ManageMaintenance = new PermissionRecord { Name = "Admin area. Manage Maintenance", SystemName = "ManageMaintenance", Category = "Configuration" };
        public static readonly PermissionRecord HtmlEditorManagePictures = new PermissionRecord { Name = "Admin area. HTML Editor. Manage pictures", SystemName = "HtmlEditor.ManagePictures", Category = "Configuration" };
        public static readonly PermissionRecord ManageScheduleTasks = new PermissionRecord { Name = "Admin area. Manage Schedule Tasks", SystemName = "ManageScheduleTasks", Category = "Configuration" };
        //Atiqah 20180829 MSP-94 \/
        public static readonly PermissionRecord ManageTransaction = new PermissionRecord { Name = "Admin area. Manage Transaction", SystemName = "ManageTransaction", Category = "TransactionList" };
        public static readonly PermissionRecord ManageTopUp = new PermissionRecord { Name = "Admin area. Manage Top Up", SystemName = "ManageTopUp", Category = "TransactionList" };
        public static readonly PermissionRecord ManageDeposit = new PermissionRecord { Name = "Admin area. Manage Deposit", SystemName = "ManageDeposit", Category = "TransactionList" };
        public static readonly PermissionRecord ManageWithdrawal = new PermissionRecord { Name = "Admin area. Manage Withdrawal", SystemName = "ManageWithdrawal", Category = "TransactionList" };
        public static readonly PermissionRecord ManageRedeem = new PermissionRecord { Name = "Admin area. Manage Redeem", SystemName = "ManageRedeem", Category = "TransactionList" };
        public static readonly PermissionRecord ManageConsumptionRewardSelf = new PermissionRecord { Name = "Admin area. Manage Consumption Reward Self", SystemName = "ManageConsumptionRewardSelf", Category = "TransactionList" }; //JK 20180926 MSP-97
        public static readonly PermissionRecord ManageConsumptionRewardHistory = new PermissionRecord { Name = "Admin area. Manage Consumption Reward History", SystemName = "ManageConsumptionRewardHistory", Category = "TransactionList" }; //wailiang 20180921 MSP-98
        public static readonly PermissionRecord ManageDepositRewardHistory = new PermissionRecord { Name = "Admin area. Manage Deposit Reward History", SystemName = "ManageDepositRewardHistory", Category = "TransactionList" }; //Atiqah 20181001 MSP-150
        //Atiqah 20180829 MSP-94 /\

        //Tony Liew 20180904 MSP-47 \/
        public static readonly PermissionRecord ManageSecurityQuestions = new PermissionRecord { Name = "Admin area. Manage Security Questions", SystemName = "ManageSecurityQuestions", Category = "Configuration" };
        //Tony Liew 20180904 MSP-47 /\

        //Tony Liew 20180910 MSP-92 \/
        public static readonly PermissionRecord ManageDepositDepositReturnedConsumptionReport = new PermissionRecord { Name = "Admin area. Manage Deposit DepositReturned Consumption Report", SystemName = "ManageDepositDepositReturnedConsumptionReport", Category = "Report" };
        public static readonly PermissionRecord ManageTopUpDepositWithdrawalReport = new PermissionRecord { Name = "Manage TopUp Deposit Withdrawal Report", SystemName = "ManageTopUpDepositWithdrawalReport", Category = "Report" };
        //Tony Liew 20180910 MSP-92 /\

        //RW 20181214 MDT-139
        public static readonly PermissionRecord ManageMemberTree = new PermissionRecord { Name = "Admin area. Manage Member Tree", SystemName = "ManageMemberTree", Category = "MemberTreeList" };
        //RW 20181214 MDT-139

        //public store permissions
        public static readonly PermissionRecord DisplayPrices = new PermissionRecord { Name = "Public store. Display Prices", SystemName = "DisplayPrices", Category = "PublicStore" };
        public static readonly PermissionRecord EnableShoppingCart = new PermissionRecord { Name = "Public store. Enable shopping cart", SystemName = "EnableShoppingCart", Category = "PublicStore" };
        public static readonly PermissionRecord EnableWishlist = new PermissionRecord { Name = "Public store. Enable wishlist", SystemName = "EnableWishlist", Category = "PublicStore" };
        public static readonly PermissionRecord PublicStoreAllowNavigation = new PermissionRecord { Name = "Public store. Allow navigation", SystemName = "PublicStoreAllowNavigation", Category = "PublicStore" };
        public static readonly PermissionRecord AccessClosedStore = new PermissionRecord { Name = "Public store. Access a closed store", SystemName = "AccessClosedStore", Category = "PublicStore" };

        //RES Permission Records \/
        public static readonly PermissionRecord ManageAnnouncementsItems = new PermissionRecord { Name = "Admin area. Manage Announcement", SystemName = "ManageAnnouncementsItems", Category = "Content Management" };       //WilliamHo 20180830 MSP-99
        public static readonly PermissionRecord ManagePromotionsItems = new PermissionRecord { Name = "Admin area. Manage Promotion", SystemName = "ManagePromotionsItems", Category = "Content Management" };                //WilliamHo 20180830 MSP-100
        public static readonly PermissionRecord ManageUserGuides = new PermissionRecord { Name = "Admin area. Manage User Guide", SystemName = "ManageUserGuides", Category = "Content Management" };                         //WilliamHo 20180830 MSP-101
        public static readonly PermissionRecord ManageVideoGuides = new PermissionRecord { Name = "Admin area. Manage Video Guide", SystemName = "ManageVideoGuides", Category = "Content Management" };
        public static readonly PermissionRecord ManageIncidents = new PermissionRecord { Name = "Admin area. Manage Incident", SystemName = "ManageIncident", Category = "Incidents" };                      //wailiang 20190319 RDT-127
        //RES Permission Records /\

        /// <summary>
        /// Get permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[] 
            {
                AccessAdminPanel,
                AllowCustomerImpersonation,
                ManageProducts,
                ManageCategories,
                ManageManufacturers,
                ManageProductReviews,
                ManageProductTags,
                ManageAttributes,
                ManageCustomers,
                ManageCustomersRole, // Tony Liew 20190115 MDT-200
                ManageVendors,
                ManageCurrentCarts,
                ManageOrders,
                ManageRecurringPayments,
                ManageGiftCards,
                ManageReturnRequests,
                OrderCountryReport,
                ManageAffiliates,
                ManageCampaigns,
                ManageDiscounts,
                ManageNewsletterSubscribers,
                ManagePolls,
                ManageNews,
                ManageBlog,
                ManageWidgets,
                ManageTopics,
                ManageForums,
                ManageMessageTemplates,
                ManageCountries,
                ManageLanguages,
                ManageSettings,
                ManagePaymentMethods,
                ManageExternalAuthenticationMethods,
                ManageTaxSettings,
                ManageShippingSettings,
                ManageCurrencies,
                ManageActivityLog,
                ManageAcl,
                ManageEmailAccounts,
                ManageStores,
                ManagePlugins,
                ManageSystemLog,
                ManageMessageQueue,
                ManageMaintenance,
                HtmlEditorManagePictures,
                ManageScheduleTasks,
                DisplayPrices,
                EnableShoppingCart,
                EnableWishlist,
                PublicStoreAllowNavigation,
                AccessClosedStore
                //MSP Permission Records \/
                ,ManageAnnouncementsItems //WilliamHo 20180830 MSP-99
                ,ManagePromotionsItems    //WilliamHo 20180830 MSP-100
                ,ManageUserGuides         //WilliamHo 20180830 MSP-101
                ,ManageVideoGuides        //WilliamHo 20180830 MSP-102
                //MSP Permission Records /\
                ,ManageMemberTree //RW 20181214 MDT-139
                ,ManageIncidents
            };
        }

        /// <summary>
        /// Get default permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[] 
            {
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Administrators,
                    PermissionRecords = new[] 
                    {
                        AccessAdminPanel,
                        AllowCustomerImpersonation,
                        ManageProducts,
                        ManageCategories,
                        ManageManufacturers,
                        ManageProductReviews,
                        ManageProductTags,
                        ManageAttributes,
                        ManageCustomersRole, // Tony Liew 20190115 MDT-200
                        ManageCustomers,
                        ManageVendors,
                        ManageCurrentCarts,
                        ManageOrders,
                        ManageRecurringPayments,
                        ManageGiftCards,
                        ManageReturnRequests,
                        OrderCountryReport,
                        ManageAffiliates,
                        ManageCampaigns,
                        ManageDiscounts,
                        ManageNewsletterSubscribers,
                        ManagePolls,
                        ManageNews,
                        ManageBlog,
                        ManageWidgets,
                        ManageTopics,
                        ManageForums,
                        ManageMessageTemplates,
                        ManageCountries,
                        ManageLanguages,
                        ManageSettings,
                        ManagePaymentMethods,
                        ManageExternalAuthenticationMethods,
                        ManageTaxSettings,
                        ManageShippingSettings,
                        ManageCurrencies,
                        ManageActivityLog,
                        ManageAcl,
                        ManageEmailAccounts,
                        ManageStores,
                        ManagePlugins,
                        ManageSystemLog,
                        ManageMessageQueue,
                        ManageMaintenance,
                        HtmlEditorManagePictures,
                        ManageScheduleTasks,
                        DisplayPrices,
                        EnableShoppingCart,
                        EnableWishlist,
                        PublicStoreAllowNavigation,
                        AccessClosedStore
                        //MSP Permission Records \/
                        ,ManageAnnouncementsItems   //WilliamHo 20180830 MSP-99
                        ,ManagePromotionsItems      //WilliamHo 20180830 MSP-100
                        ,ManageUserGuides           //WilliamHo 20180830 MSP-101
                        ,ManageVideoGuides          //WilliamHo 20180830 MSP-102
                        //MSP Permission Records /\
                        ,ManageMemberTree   //RW 20181214 MDT-139
                        ,ManageIncidents
                    }
                },
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.ForumModerators,
                    PermissionRecords = new[] 
                    {
                        DisplayPrices,
                        EnableShoppingCart,
                        EnableWishlist,
                        PublicStoreAllowNavigation
                    }
                },
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Guests,
                    PermissionRecords = new[] 
                    {
                        DisplayPrices,
                        EnableShoppingCart,
                        EnableWishlist,
                        PublicStoreAllowNavigation
                    }
                },
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Registered,
                    PermissionRecords = new[] 
                    {
                        DisplayPrices,
                        EnableShoppingCart,
                        EnableWishlist,
                        PublicStoreAllowNavigation
                    }
                },
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Vendors,
                    PermissionRecords = new[] 
                    {
                        AccessAdminPanel,
                        ManageProducts,
                        ManageProductReviews,
                        ManageOrders
                    }
                }
            };
        }
    }
}