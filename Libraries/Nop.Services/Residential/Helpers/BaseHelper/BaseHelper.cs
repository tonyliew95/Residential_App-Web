using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Residential.Custom;
using Nop.Core.Domain.Residential.User;
using Nop.Core.Domain.Residential.Setting;
using Nop.Core.Enumeration;
using Nop.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core.Domain.Residential.Organization;

namespace Nop.Services.Residential.Helpers.BaseHelper
{

    public class BaseHelper : IBaseHelper
    {
        private readonly IRepository<Adm_SystemControl>  _admSystemControlRepository;
        private readonly IRepository<Adm_MsgLocalization> _admMsgLocalizationRepository;
        private readonly IRepository<Mnt_UserProfile> _mntUserProfileRepository;
        private readonly IRepository<Mnt_Organization> _mntOrganizationRepository;
        private readonly IRepository<Language> _languageRepository;
        private readonly IRepository<Adm_StandardCode> _admStandardCodeRepository;
        private readonly IRepository<Adm_TimeZone> _admTimeZoneRepository;

        public BaseHelper(
            IRepository<Adm_SystemControl> admSystemControlRepository
            , IRepository<Adm_MsgLocalization> admMsgLocalizationRepository
            , IRepository<Mnt_UserProfile> mntUserProfileRepository
            , IRepository<Mnt_Organization> mntOrganizationRepository
            , IRepository<Language> languageRepository
            , IRepository<Adm_StandardCode> admStandardCodeRepository
            , IRepository<Adm_TimeZone> admTimeZoneRepository
        )
        {
            this._admSystemControlRepository = admSystemControlRepository;
            this._admMsgLocalizationRepository = admMsgLocalizationRepository;
            this._mntUserProfileRepository = mntUserProfileRepository;
            this._mntOrganizationRepository = mntOrganizationRepository;
            this._languageRepository = languageRepository;
            this._admStandardCodeRepository = admStandardCodeRepository;
            this._admTimeZoneRepository = admTimeZoneRepository;
        }

        // Tony Liew 20190321 \/
        #region Get Setting Value By Key
        /// <summary>
        /// Get Setting Value By Key
        /// </summary>
        /// <param name="SettingKey"></param>
        /// <param name="DefaultValue"></param>
        /// <returns></returns>
        public string getSettingValueByKey(string SettingKey, string DefaultValue) // Tony Liew 20190321
        {
            string settingValue = "";
            try
            {
                settingValue =
                    (from setting in _admSystemControlRepository.Table
                     where setting.Name == SettingKey && setting.Active == true
                     select new { settingValue = setting.Value }
                    ).FirstOrDefault().settingValue.ToString();
            }
            catch (Exception)
            {
                settingValue = DefaultValue;
            }

            return settingValue;
        }
        #endregion
        // Tony Liew 20190321 /\

        #region Get Localization By CustomerId
        //Commented By Tony 20190329 \/
        ///// <summary>
        ///// Get Localization By CustomerId
        ///// </summary>
        ///// <param name="CustomerId"></param>
        ///// <param name="ResourceName"></param>
        ///// <returns></returns>
        //public string GetLocalizationByCustomerId(int CustomerID, string ResourceName)
        //{
        //    //string ResourceValue = "";
        //    //int CustomerLanguageID = (from data in _mntUserProfileRepository.Table
        //    //                          where data.CustomerId == CustomerID
        //    //                          select data.Locale_Id.Value).FirstOrDefault();

        //    if (CustomerLanguageID > 0)
        //    {
        //        try
        //        {
        //            //ResourceValue = (from data in _mntLocalizationRepository.Table
        //            //                 where data. == CustomerLanguageID && data.ResourceName == ResourceName
        //            //                 select data.ResourceValue).FirstOrDefault().ToString();
        //        }
        //        catch (Exception)
        //        {
        //            ResourceValue = ResourceName;
        //        }
        //    }
        //    return ResourceValue;
        //}
        //Commented By Tony 20190329 /\
        #endregion

        #region Get Localization By CustomerId
        // Tony Liew 20190329 \/
        /// <summary>
        /// Get Localization By CustomerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="returnCode"></param>
        /// <returns></returns>
        public string getMessageLocalizationByLocaleId(int customerId , int returnCode) //Temporary Solution
        {
            var getUserProfile = (from profile in _mntUserProfileRepository.Table where profile.CustomerId == customerId select profile).FirstOrDefault(); // Get user profile
            if (getUserProfile == null)
                return string.Empty;

            var getLocalizeReturnMessage = (from message in _admMsgLocalizationRepository.Table where message.Code == returnCode  select message).FirstOrDefault(); //Get the message supposed to be return
            if (getLocalizeReturnMessage == null)
                return string.Empty;
            else
            {
                switch (getUserProfile.Locale_Id)
                {
                    case 1:
                        return getLocalizeReturnMessage.Message_EN;
                    case 2:
                        return getLocalizeReturnMessage.Message_CN;
                    case 3:
                        return getLocalizeReturnMessage.Message_BM;
                    default:
                        return getLocalizeReturnMessage.Message_EN;
                }
            }
        }
        // Tony Liew 20190329 /\
        #endregion

        //Tony Liew 20190328 \/
        #region Get URL from S3 server
        /// <summary>
        /// Get URL from from S3 server
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="customerId"></param>
        /// <param name="container"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public string returnURL(string base64String, int customerId , string file , string fileType ) //Tony Liew 20190328
        {
            byte[] bytesArray = Convert.FromBase64String(base64String);
          
            var fileExtension = getImageType(bytesArray , fileType);
           
            var fileName = string.Empty;
            string container = RES_FileEnum.residentialImage.ToValue<RES_FileEnum>();

            fileName = Guid.NewGuid().ToString() + fileExtension;
            AWSS3PictureService.SaveThumbTos3(container + "/" + file + "/" + customerId, fileName, bytesArray);

            return AWSS3PictureService.GetThumbUrlFroms3(container + "/" + file + "/" + customerId, fileName);

        }

        private bool isMatch(byte[] pattern, byte[] data)
        {
            if (pattern.Length <= data.Length)
            {
                for (int idx = 0; idx < pattern.Length; ++idx)
                {
                    if (pattern[idx] != data[idx])
                        return false;
                }
                return true;
            }
            return false;
        }

        private string getImageType(byte[] data , string fileType)
        {
            //        filetype    magic number(hex)
            //        jpg         FF D8 FF
            //        gif         47 49 46 38
            //        png         89 50 4E 47 0D 0A 1A 0A
            //        bmp         42 4D
            //        tiff(LE)    49 49 2A 00
            //        tiff(BE)    4D 4D 00 2A

            byte[] pngPattern = new byte[] { (byte)0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
            byte[] jpgPattern = new byte[] { (byte)0xFF, (byte)0xD8, (byte)0xFF };
            byte[] gifPattern = new byte[] { 0x47, 0x49, 0x46, 0x38 };
            byte[] bmpPattern = new byte[] { 0x42, 0x4D };
            byte[] tiffLEPattern = new byte[] { 0x49, 0x49, 0x2A, 0x00 };
            byte[] tiffBEPattern = new byte[] { 0x4D, 0x4D, 0x00, 0x2A };
            if (isMatch(pngPattern, data))
                return ".png";

            else if (isMatch(jpgPattern, data))
                return ".jpg";

            else if (isMatch(gifPattern, data))
                return ".gif";

            else if (isMatch(bmpPattern, data))
                return ".bmp";

            else if (isMatch(tiffLEPattern, data))
                return ".tif";

            else if (isMatch(tiffBEPattern, data))
                return ".tif";
            else
                return fileType;
        }


        #endregion
        //Tony Liew 20190328 /\

        #region Get Standard Code and Description by Key
        /// <summary>
        /// Get Standard Code and Description As Queryable
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        private IQueryable<Adm_StandardCodeCustom> GetStandardCode(string Key, string Code = "") 
        {
            var query = (from standard in _admStandardCodeRepository.Table
                        where standard.Key == Key
                        select new Adm_StandardCodeCustom
                        {
                            code = standard.Code,
                            name = standard.Description
                        }).AsQueryable();
            if (!string.IsNullOrEmpty(Code))
                query = query.Where(x => x.code == Code);
                
            return query;
        }

        /// <summary>
        /// Get Standard Code and Description by Key & Code
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public Adm_StandardCodeCustom GetStandardCodeByKeyCode(string Key, string Code)
        {
            return GetStandardCode(Key, Code).FirstOrDefault();
        }

        /// <summary>
        /// Get Standard Code and Description list by Key
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public IList<Adm_StandardCodeCustom> GetStandardCodeListByKey(string Key)
        {
            return GetStandardCode(Key).ToList();
        }
        #endregion

        // WKK 20190418 Get time zone utc offset value by id
        /// <summary>
        /// Get time zone utc offset value by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int getUtcOffsetByTimezoneId(int id, int defaultOffset = 8)
        {
            try
            {
                var offset = _admTimeZoneRepository.Table.Where(tz => tz.Id == id).FirstOrDefault().UtcOffset;

                return Convert.ToInt16(offset);
            }
            catch (Exception)
            {
                return defaultOffset;
            }
        }

        // WKK 20190418 Get time zone utc offset value by org id
        /// <summary>
        /// Get time zone utc offset value by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int getUtcOffsetByOrgId(int orgId, int defaultOffset = 8)
        {
            try
            {
                var offset =
                    (
                        from tz in _admTimeZoneRepository.Table
                        join org in _mntOrganizationRepository.Table
                        on tz.Id equals org.TimeZoneId
                        where tz.Id == orgId
                        select tz
                    );

                return Convert.ToInt16(offset.FirstOrDefault().UtcOffset);
            }
            catch (Exception)
            {
                return defaultOffset;
            }
        }
    }
}

