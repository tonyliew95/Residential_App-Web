using Nop.Core.Data;
using Nop.Core.Domain.Msp.Setting;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Nop.Core.Extensions;
using System.Diagnostics;

namespace Nop.Services.Helpers
{
    public class MspHelper : IMspHelper
    {
        private readonly IRepository<MSP_Setting> _settingRepository;
        private static string LanguageCode = null; //wailiang 20190114 MDT-206
        private static string BlockchainTxId = null; //wailiang 20190116 MDT-193
        private static int precision = 0; //wailiang 20190201 MDT-220
        private static string dateformat = null; //wailiang 20190201 MDT-220
        private static readonly string PrefixBlockChainUrl = "https://www.blockchain.com/btc/tx/";
        private static string depoCount = null;

        public MspHelper(
            IRepository<MSP_Setting> settingRepository
        )
        {
            this._settingRepository = settingRepository;
        }

        public string GetSettingValueByKey(string SettingKey, string DefaultValue)
        {
            string settingValue = "";
            try
            {
                settingValue =
                    (from setting in _settingRepository.Table
                     where setting.SettingKey == SettingKey && setting.Status == "A"
                     select new { settingValue = setting.SettingValue }
                    ).FirstOrDefault().settingValue.ToString();
            }
            catch (Exception)
            {
                settingValue = DefaultValue;
            }

            return settingValue;
        }

        #region GetGlobalDateFormat
        public static string GetGlobalDateFormat(string SettingKey, string DefaultValue)
        {
            string settingValue = "";

            try
            {
                var connectionString = new DataSettingsManager().LoadSettings().DataConnectionString;

                var dbContext = new DbContext(connectionString);
                var results = (dbContext.Database.SqlQuery<MSP_Setting>(
                                "Select * From MSP_Setting Where SettingKey = @SettingKey And Status = @Status",
                                new SqlParameter("@SettingKey", SettingKey),
                                new SqlParameter("@Status", "A"))
                                ).FirstOrDefault();
                if (results != null)
                {
                    settingValue = results.SettingValue;
                }
                else
                    return DefaultValue;
            }
            catch (Exception ex)
            {
                settingValue = DefaultValue;
            }

            return settingValue;
        }
        #endregion

        #region TruncateDecimal

        //RW 20180712 MDT-128
        public static string GetGlobalMBTCDecimalPlace()
        {
            string SettingKey = "SystemDecimals";
            string DefaultValue = "6";
            string settingValue = "";

            try
            {
                var connectionString = new DataSettingsManager().LoadSettings().DataConnectionString;

                var dbContext = new DbContext(connectionString);
                var results = (dbContext.Database.SqlQuery<MSP_Setting>(
                                "Select * From MSP_Setting Where SettingKey = @SettingKey And Status = @Status",
                                new SqlParameter("@SettingKey", SettingKey),
                                new SqlParameter("@Status", "A"))
                                ).FirstOrDefault();
                if (results != null)
                {
                    settingValue = results.SettingValue;
                }
                else
                    return DefaultValue;
            }
            catch (Exception ex)
            {
                settingValue = DefaultValue;
            }

            return settingValue;
        }
        //RW 20180712 MDT-128

        public decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            decimal tmp = Math.Truncate(step * value);
            return tmp / step;
        }

        public decimal TruncateDecimal_BTC(decimal value)
        {
            //int precision = Convert.ToInt32(GetSettingValueByKey("BtcDecimals", "8")); //RW 20181023
            precision = precision == 0 ? Convert.ToInt32(GetSettingValueByKey("BtcDecimals", "8")) : precision; //wailiang 20190201 MDT-220
            decimal returnValue = TruncateDecimal(value, precision);
            return returnValue;
        }

        public decimal TruncateDecimal_MBTC(decimal value)
        {
            //int precision = Convert.ToInt32(GetSettingValueByKey("SystemDecimals", "6")); //RW 20181023 //wailiang 20190201
            precision = precision == 0 ? Convert.ToInt32(GetSettingValueByKey("SystemDecimals", "6")) : precision; //wailiang 20190201
            decimal returnValue = TruncateDecimal(value, precision);
            return returnValue;
        }

        public decimal TruncateDecimalTo5Decimal_MBTC(decimal value) //WilliamHo 20180922 MSP-159
        {
            int precision = 5;
            decimal returnValue = TruncateDecimal(value, precision);
            return returnValue;
        }

        public string TruncateDecimalToString_BTC(decimal value)
        {
            precision = precision == 0 ? Convert.ToInt32(GetSettingValueByKey("BtcDecimals", "8")) : precision; //wailiang 20190201 MDT-220
            //int precision = Convert.ToInt32(GetSettingValueByKey("BtcDecimals", "8")); //RW 20181023 //wailiang 20190201
            string returnValue = FormatDecimalValue(TruncateDecimal(value, precision), precision);
            return returnValue;
        }

        public string TruncateDecimalToString_MBTC(decimal value)
        {
            precision = precision == 0 ? Convert.ToInt32(GetSettingValueByKey("SystemDecimals", "6")) : precision; //wailiang 20190201 MDT-220
            //int precision = Convert.ToInt32(GetSettingValueByKey("SystemDecimals", "6")); //RW 20181023
            string returnValue = FormatDecimalValue(TruncateDecimal(value, precision), precision);
            return returnValue;
        }

        public string FormatDecimalValue(decimal value, int precision)
        {
            //string decimalFormat = "{0:N" + precision + "}";   //LeeChurn 20181101 MSP-435 
            string decimalFormat = "{0:F" + precision + "}";     //LeeChurn 20181101 MSP-435 
            string returnValue = String.Format(decimalFormat, value);
            return returnValue;
        }

        //RW 20181023
        public string TruncateDecimal_Pct(decimal value)
        {
            //int precision = Convert.ToInt32(GetSettingValueByKey("MSP_PercentageDecimal", "2")); //wailiang 20190201 MDT-220
            precision = precision == 0 ? Convert.ToInt32(GetSettingValueByKey("MSP_PercentageDecimal", "2")) : precision; //wailiang 20190201 MDT-220
            string returnValue = FormatDecimalValue(TruncateDecimal(value * 100, precision), precision);
            return returnValue;
        }
        //RW 20181023

        #region BackEnd
        //RW 20181221 MDT-139
        public string FormatBEDecimalValue(decimal value, int precision)
        {
            string decimalFormat = "{0:N" + precision + "}"; //Thousand Separator String format
                                                             //string decimalFormat = "{0:F" + precision + "}";
            string returnValue = String.Format(decimalFormat, value);
            return returnValue;
        }
        //RW 20181221 MDT-139

        //RW 20181221 MDT-139
        public string TruncateBEDecimal_Score(decimal value)
        {
            //int precision = Convert.ToInt32(GetSettingValueByKey("SystemDecimals", "6")); //wailiang 20190201 MDT-220
            precision = precision == 0 ? Convert.ToInt32(GetSettingValueByKey("SystemDecimals", "6")) : precision; //wailiang 20190201 MDT-220
            string returnValue = FormatBEDecimalValue(TruncateDecimal(value, precision), precision);
            return returnValue;
        }
        //RW 20181221 MDT-139
        #endregion

        #endregion TruncateDecimal

        #region Password

        public bool PasswordLengthCheckEnabled(string flag)
        {
            return bool.Parse(GetSettingValueByKey(flag, false.ToString()).ToString()); ;
        }

        public int PasswordMinimumLength(string length)
        {
            return int.Parse(GetSettingValueByKey(length, "50").ToString()); ;
        }

        public int PasswordMaximumLength(string length)
        {
            return int.Parse(GetSettingValueByKey(length, "100").ToString()); ;
        }

        #endregion

        #region Username

        public bool UsernameLengthCheckEnabled(string flag)
        {
            return bool.Parse(GetSettingValueByKey(flag, false.ToString()).ToString());
        }

        public int UsernameMinimumLength(string length)
        {
            return int.Parse(GetSettingValueByKey(length, "8").ToString());
        }

        public int UsernameMaximumLength(string length)
        {
            return int.Parse(GetSettingValueByKey(length, "100").ToString());
        }

        #endregion

        #region Date Time
        ///// <summary>
        /////  RW 20180912 MSP-46
        ///// </summary>
        ///// <param name="date"></param>
        ///// <returns></returns>
        //public DateTime FromDate(DateTime date)
        //{
        //    return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        //}

        /// <summary>
        /// RW 20180912 MSP-46
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime ToDate(DateTime date)
        {
            return date.AddDays(1).AddMilliseconds(-1);
            //return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
        #endregion

        public decimal? ConvertStringToDecimal(string number)
        {
            decimal? newNumber = null;
            if (!string.IsNullOrEmpty(number))
            {
                decimal.TryParse(number, out decimal sNewNumber);
                newNumber = sNewNumber;
            }

            return newNumber;
        }

        public bool ValidateArray<T>(T[] array, bool isCheckNullValue = false, bool isCheckDuplicateValue = false) where T : class //Jerry 20181114 MSP-494
        {
            bool ret = true;

            if (array == null || array.Length == 0)
            {
                ret = false;
            }
            else
            {
                if (array.All(item => item == null))
                {
                    ret = false;
                }
                else
                {
                    if (isCheckNullValue)
                    {
                        var NullString = (from item in array where item == null select item);

                        if (NullString.Count() > 0)
                        {
                            ret = false;
                        }
                    }
                    else if (isCheckDuplicateValue)
                    {
                        if (array.Distinct().Count() != array.Count())
                        {
                            ret = false;
                        }
                    }
                }
            }

            return ret;
        }

        #region Get Language
        public string GetLanguage() //wailiang 20190114 MDT-206
        {
            if (string.IsNullOrEmpty(LanguageCode))
                LanguageCode = GetSettingValueByKey("MSP_DefaultLanguage", "zh-CN");

            return LanguageCode;
        }
        #endregion

        #region Get Blockchain Link
        public string GetBlockchainTxIdPrefixLink() //wailiang 20190116 MDT-193
        {
            if (string.IsNullOrEmpty(BlockchainTxId))
            {
                BlockchainTxId = GetSettingValueByKey("MSP_BlockchainTxIDPrefixLink", PrefixBlockChainUrl);
                BlockchainTxId = URLFormatEndWithSlash(BlockchainTxId);
            }
            
            return BlockchainTxId;
        }

        public static string GetBlockchainUrlPrefix()
        {
            try
            {
                if (string.IsNullOrEmpty(BlockchainTxId))
                {
                    var connectionString = new DataSettingsManager().LoadSettings().DataConnectionString;

                    var dbContext = new DbContext(connectionString);
                    var results = (dbContext.Database.SqlQuery<MSP_Setting>(
                                    "Select * From MSP_Setting Where SettingKey = @SettingKey And Status = @Status",
                                    new SqlParameter("@SettingKey", "MSP_BlockchainTxIDPrefixLink"),
                                    new SqlParameter("@Status", "A"))
                                    ).FirstOrDefault();
                    if (results != null)
                    {
                        BlockchainTxId = results.SettingValue;
                        BlockchainTxId = (!BlockchainTxId.EndsWith("/")) ? BlockchainTxId.Trim() + "/" : BlockchainTxId;
                    }
                    else
                        return PrefixBlockChainUrl;
                }
            }
            catch (Exception ex)
            {
                BlockchainTxId = PrefixBlockChainUrl;
            }

            return BlockchainTxId;
        }
        #endregion

        #region URL Format End Slash
        public string URLFormatEndWithSlash(string url) //wailiang 20190116 MDT-193
        {
            if (!url.EndsWith("/"))
                url += "/";

            return url;
        }
        #endregion

        #region Date Format ISO
        public string GetDateFormatISO(DateTime? date) //wailiang 20190201 MDT-220
        {
            if (date == null) return string.Empty;
            dateformat = string.IsNullOrEmpty(dateformat) ? GetSettingValueByKey("MSP_GlobalDateTimeFormat", "yyyy-MM-ddTHH:mm:ss.fffZ") : dateformat;
            
            return date.Value.ToString(dateformat);
        }
        #endregion

        #region Deposit Upgrade Count
        public int GetDepositUpgradeCount() //wailiang 20190228 MDT-286
        {
            depoCount = GetSettingValueByKey("MSP_DepositUpgradeAllowCount", "1");

            return Convert.ToInt32(depoCount);
        }
        #endregion
    }
}
