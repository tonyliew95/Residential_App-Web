using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Plugin.Api.DTOs.Products;
using Nop.Services.Catalog;
using Nop.Services.Seo;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Plugin.Api.DTOs.Images;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Stores;
using Nop.Plugin.Api.MappingExtensions;
using Nop.Plugin.Api.DTOs.Categories;
using Nop.Plugin.Api.DTOs.Customers;
using Nop.Plugin.Api.DTOs.Languages;
using Nop.Plugin.Api.DTOs.Orders;
using Nop.Plugin.Api.Services;
using Nop.Services.Media;
using Nop.Plugin.Api.DTOs.ShoppingCarts;
using Nop.Plugin.Api.DTOs.Stores;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Plugin.Api.DTOs.ProductAttributes;
using Nop.Plugin.Api.DTOs.OrderItems;
using Nop.Plugin.Api.Services.Interface.Deposit;
using Nop.Plugin.Api.Services.Interface;
using Nop.Plugin.Api.Models.Base;
using Nop.Services.Configuration;
using GlobalSetting =  Nop.Plugin.Api.Enumeration.GlobalSettingEnum;

namespace Nop.Plugin.Api.Helpers
{
    public class SessionHelper : ISessionHelper
    {
        private ISessionService _sessionServices;
        private IUtilityHelper _utilityHelper;

        public SessionHelper(ISessionService sessionService,IUtilityHelper utilityHelper)
        {
            _sessionServices = sessionService;
            _utilityHelper = utilityHelper;
        }

        public int GetCustomerIdBySession(string SessionId, ResponseResult result)
        {
            int returnCode = 0;
            string returnMessage = ""; 
            int CustomerID = GetCustomerIdBySession(SessionId, out returnCode, out returnMessage);
            result.ReturnCode = returnCode;
            result.ReturnMessage = returnMessage;
            return CustomerID;
        }

        public int GetCustomerIdBySession(string SessionId,out int ReturnCode, out string ReturnMessage)
        {

            int customerId = 0;
            ReturnCode = 0;  // Success
            ReturnMessage = "Success";
            //int timeOut = 60;  ///LeeChurn 20180730 MSP-4
            int timeOut = int.Parse(_utilityHelper.GetSettingValueByKey(GlobalSetting.MSPGlobalSettingTimeout.ToString(), "60").ToString()); //LeeChurn 20180730 MSP-4

            try
            {
                var sessionDto = _sessionServices.GetCustomerIDBySessionID(SessionId);
                if (sessionDto == null)
                {
                    ReturnCode = 1010;
                    ReturnMessage = "invalid session id";
                }
                else if (sessionDto.LoginDateUtc.AddMinutes(timeOut) < DateTime.UtcNow)   // Get the timeout minutes from setting
                {
                    ReturnCode = 1011;
                    ReturnMessage = "session timeout";
                }
                else if (sessionDto.CustomerID <= 0)
                {
                    ReturnCode = 1012;
                    ReturnMessage = "session id not match";
                }
                else
                {
                    customerId = sessionDto.CustomerID; 
                }
            }
            catch (Exception ex)
            {
                ReturnCode = ex.HResult; 
                ReturnMessage = ex.Message;
            }
            return customerId;
        }

    }
}
