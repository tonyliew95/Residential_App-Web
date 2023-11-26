using Nop.Core;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.Mobile;
using Nop.Core.Domain.Residential.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Residential.Profile
{
    public interface IProfileServices
    {
        /// <summary>
        /// Get a list of Properties
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IQueryable<PropertyCustom> listProperties(int customerId, int PageIndex = 1, int PageSize = int.MaxValue);// Tony Liew 20190315 RDT-65 

        /// <summary>
        /// Search Customer Msisdn
        /// </summary>
        /// <param name="customerId"></param>
        IQueryable<Mnt_UserMsisdn> SearchCustomerMsisdn(int customerId); //Tony Liew 20190412 RDT-68 

        /// <summary>
        /// Insert customer Msisdn
        /// </summary>
        /// <param name="customerMsisdn"></param>
        void insertCustomerMsisdn(Mnt_UserMsisdn customerMsisdn); //Tony Liew 20190319 RDT-67 

        /// <summary>
        /// Update customer Msisdn
        /// </summary>
        /// <param name="customerMsisdn"></param>
        void updateCustomerMsisdn(Mnt_UserMsisdn customerMsisdn); //Tony Liew 20190319 RDT-67

        /// <summary>
        /// Delete customer Msisdn
        /// </summary>
        /// <param name="customerMsisdn"></param>
        void deleteCustomerMsisdn(Mnt_UserMsisdn customerMsisdn); //Tony Liew 20190319 RDT-200

        /// <summary>
        /// Get User Msisdn By User Id
        /// </summary>
        /// <param name="customerMsisdn"></param>
        IQueryable<Mnt_UserMsisdn> getUserMsisdnByUserId(int userId); //Tony Liew 20190319 RDT-67 

        /// <summary>
        /// Validate LanguageCode and if exist return LanguageId
        /// </summary>
        /// <param name="LanguageCode"></param>
        /// <param name="LanguageId"></param>
        /// <returns></returns>
        bool ValidateLanguageCode(string LanguageCode, out int LanguageId); //JK 20190322 RDT-71

        /// <summary>
        /// Update customer language
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="LanguageId"></param>
        void UpdateCustomerLanguage(int CustomerID, int LanguageId); //JK 20190322 RDT-71
        
        /// <summary>
        /// Get a list of user properties
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        List<Mnt_UserPropertyCustom> GetProperties(int customerId); // WKK 20190322 RDT-163 [API] Login - profile dto

        //WKK 20190430 RDT-168 [API] Property Unit - Bind new property
        /// <summary>
        /// Get Properties count
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        int countProperties(int customerId);
    }
}
