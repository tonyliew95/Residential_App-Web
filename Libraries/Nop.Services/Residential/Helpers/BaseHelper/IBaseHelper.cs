using Nop.Core.Domain.Residential.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nop.Services.Residential.Helpers.BaseHelper
{
    public interface IBaseHelper
    {
        /// <summary>
        /// Get Setting Value By Key
        /// </summary>
        /// <param name="SettingKey"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        string getSettingValueByKey(string SettingKey, string DefaultValue); //Tony Liew 20190321

        ///// <summary>
        ///// Get Localization By CustomerId
        ///// </summary>
        ///// <param name="CustomerID"></param>
        ///// <param name="ResourceName"></param>
        ///// <returns></returns>
        //string GetLocalizationByCustomerId(int CustomerID, string ResourceName);

        /// <summary>
        /// Get URL from from S3 server
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="customerId"></param>
        /// <param name="container"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        string returnURL(string base64String, int customerId, string file, string fileType);//Tony Liew 20190328

        /// <summary>
        /// Get Localization By CustomerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="returnCode"></param>
        /// <returns></returns>
        string getMessageLocalizationByLocaleId(int customerId, int returnCode);//Tony Liew 20190329

        /// <summary>
        /// Get Standard Code and Description by Key & Code
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        Adm_StandardCodeCustom GetStandardCodeByKeyCode(string Key, string Code);

        /// <summary>
        /// Get Standard Code and Description list by Key
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        IList<Adm_StandardCodeCustom> GetStandardCodeListByKey(string Key);

        // WKK 20190418
        /// <summary>
        /// Get time zone utc offset value by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int getUtcOffsetByTimezoneId(int id, int defaultOffset = 8);

        // WKK 20190418 Get time zone utc offset value by org id
        /// <summary>
        /// Get time zone utc offset value by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int getUtcOffsetByOrgId(int orgId, int defaultOffset = 8);

    }
}