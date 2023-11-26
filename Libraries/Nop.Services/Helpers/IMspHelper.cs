using System;

namespace Nop.Services.Helpers
{
    public partial interface IMspHelper
    {
        string GetSettingValueByKey(string SettingKey, string DefaultValue);

        decimal TruncateDecimal(decimal value, int precision);

        decimal TruncateDecimal_BTC(decimal value);

        decimal TruncateDecimal_MBTC(decimal value);

        string TruncateDecimalToString_BTC(decimal value);

        string TruncateDecimalToString_MBTC(decimal value);

        string FormatDecimalValue(decimal value, int precision);

        string FormatBEDecimalValue(decimal value, int precision); //RW 20181221 MDT-139

        string TruncateBEDecimal_Score(decimal value); //RW 20181221 MDT-139

        bool PasswordLengthCheckEnabled(string flag);

        int PasswordMinimumLength(string length);

        int PasswordMaximumLength(string length);

        bool UsernameLengthCheckEnabled(string flag);

        int UsernameMinimumLength(string length);

        int UsernameMaximumLength(string length);

        decimal TruncateDecimalTo5Decimal_MBTC(decimal value);

        string TruncateDecimal_Pct(decimal value); //RW 20181023

        ///// <summary>
        ///// To Date RW 20180912 MSP-46
        ///// </summary>
        ///// <param name="date"></param>
        ///// <returns></returns>
        //DateTime FromDate(DateTime date);

        /// <summary>
        /// From Date RW 20180912 MSP-46
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        DateTime ToDate(DateTime date);

        decimal? ConvertStringToDecimal(string number);

        bool ValidateArray<T>(T[] array, bool isCheckNullValue = false, bool isCheckDuplicateValue = false) where T : class; //Jerry 20181114 MSP-494

        string GetLanguage(); //wailiang 20190114 MDT-206

        string GetBlockchainTxIdPrefixLink(); //wailiang 20190116 MDT-193

        string URLFormatEndWithSlash(string url); //wailiang 20190116 MDT-193

        string GetDateFormatISO(DateTime? date); //wailiang 20190201 MDT-220

        int GetDepositUpgradeCount(); //wailiang 20190228 MDT-286
    }
}
