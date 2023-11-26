using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Enumeration
{
    #region MSP_MemberScorePct
    public enum MSP_MemberScorePct_ScoreType
    {
        [ValueAttribute("SY")]
        [DescriptionAttribute("Score Y")]
        ScoreY,
        [ValueAttribute("SZ")]
        [DescriptionAttribute("Score Z")]
        ScoreZ
    }
    #endregion 

    #region MSP_WT_Score 
    public enum MSP_WT_Score_RefType
    {
        [ValueAttribute("DS")]
        [DescriptionAttribute("Deposit Score")]
        DepositScore,
        [ValueAttribute("CS")]
        [DescriptionAttribute("Consumption Score")]
        ConsumptionScore,
        [ValueAttribute("DSU")]
        [DescriptionAttribute("Deposit Score Upline")]
        DepositScoreUpline,
        [ValueAttribute("CSU")]
        [DescriptionAttribute("Consumption Score Upline")]
        ConsumptionScoreUpline,
    }

    public enum MSP_WT_Score_AmountType
    {
        [ValueAttribute("SY")]
        [DescriptionAttribute("Score Y")]
        ScoreY,
        [ValueAttribute("SZ")]
        [DescriptionAttribute("Score Z")]
        ScoreZ,
    }
    #endregion

    #region MSP_WT_Mbtc_Float
    public enum MSP_WT_Mbtc_Float_RefType
    {
        [ValueAttribute("DO")]
        [DescriptionAttribute("Deposit Offset")]
        DepositOffset,
        [ValueAttribute("DP")]
        [DescriptionAttribute("Deposit Profit")]
        DepositProfit,
        [ValueAttribute("CO")]
        [DescriptionAttribute("Consumption Offset")]
        ConsumptioOffset,
        [ValueAttribute("CP")]
        [DescriptionAttribute("Consumption Profit")]
        ConsumptionProfit,
        [ValueAttribute("S")]
        [DescriptionAttribute("Settlement")]
        Settlement,
    }
    #endregion  

    #region MSP_WT_Mbtc
    public enum MSP_WT_Mbtc_RefType
    {
        [ValueAttribute("DM")]
        [DescriptionAttribute("Deposit Mbtc")]
        DepositMbtc,
        [ValueAttribute("D")]
        [DescriptionAttribute("Deposit")]
        Deposit,
        [ValueAttribute("WM")]
        [DescriptionAttribute("Withdrawal Mbtc")]
        WithdrawalMbtc,
        [ValueAttribute("S")]
        [DescriptionAttribute("Settlement")]
        Settlement,
    }
    #endregion

    #region MSP_WT_Profit
    public enum MSP_WT_Profit_RefType
    {
        [ValueAttribute("DP")]
        [DescriptionAttribute("Deposit Profit")]
        DepositProfit,
        [ValueAttribute("CP")]
        [DescriptionAttribute("Consumption Profit")]
        ConsumptionProfit
    }

    public enum MSP_WT_Profit_AmountType
    {
        [ValueAttribute("DP")]
        [DescriptionAttribute("Deposit Profit")]
        DepositProfit,
        [ValueAttribute("CP")]
        [DescriptionAttribute("Consumption Profit")]
        ConsumptionProfit
    }
    #endregion

    #region MSP_WT_Deposit
    public enum MSP_WT_Deposit_RefType
    {
        [ValueAttribute("D")]
        [DescriptionAttribute("Deposit")]
        Deposit,
        [ValueAttribute("DO")]
        [DescriptionAttribute("Deposit Offset")]
        DepositOffset,
        [ValueAttribute("CO")]
        [DescriptionAttribute("Consumption Offset")]
        ConsumptionOffsset
    }
    #endregion

    #region MSP_Settlement
    public enum MSP_Settlement_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed,
    }
    #endregion

    #region MSP_Mbtc_Deposit
    public enum MSP_Mbtc_Deposit_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }
    #endregion

    #region MSP_Deposit
    public enum MSP_Deposit_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("API_MSP_Deposit_Status.New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("API_MSP_Deposit_Status.Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("API_MSP_Deposit_Status.Failed")]
        Failed
    }
    #endregion

    #region MSP_Deposit_Profit
    public enum MSP_Deposit_Profit_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }
    #endregion

    #region MSP_Deposit_Offset
    public enum MSP_Deposit_Offset_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }
    #endregion

    #region MSP_Deposit_Score
    public enum MSP_Deposit_Score_TransactionType
    {
        [ValueAttribute("MD")]
        [DescriptionAttribute("Member Deposit")]
        MemberDeposit
    }
    public enum MSP_Deposit_Score_AmountType
    {
        [ValueAttribute("SY")]
        [DescriptionAttribute("Score Y")]
        ScoreY,
        [ValueAttribute("SZ")]
        [DescriptionAttribute("Score X")]
        ScoreZ
    }
    public enum MSP_Deposit_Score_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }
    #endregion

    #region MSP_Deposit_Rule
    public enum MSP_Deposit_Rule_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }
    #endregion 

    #region MSP_Deposit_Score_Upline
    public enum MSP_Deposit_Score_Upline_TransactionType
    {
        [ValueAttribute("DD")]
        [DescriptionAttribute("Direct Downline")]
        DirectDownline,
        [ValueAttribute("SD")]
        [DescriptionAttribute("Subsequent Downline")]
        SubsequentDownline
    }

    public enum MSP_Deposit_Score_Upline_AmountType
    {
        [ValueAttribute("SY")]
        [DescriptionAttribute("Score Y")]
        ScoreY,
        [ValueAttribute("SZ")]
        [DescriptionAttribute("Score Z")]
        ScoreZ
    }

    public enum MSP_Deposit_Score_Upline_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }

    #endregion 

    #region MSP_Consumption
    public enum MSP_Consumption_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }
    #endregion 

    #region MSP_Consumption_Offset
    public enum MSP_Consumption_Offset_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }
    #endregion 

    #region MSP_Consumption_Score
    public enum MSP_Consumption_Score_TransactionType
    {
        [ValueAttribute("MC")]
        [DescriptionAttribute("Member Consumption")]
        MemberConsumption,
        [ValueAttribute("DC")]
        [DescriptionAttribute("Direct Downline Consumption")]
        DirectDownlineConsumption,
        [ValueAttribute("SC")]
        [DescriptionAttribute("Subsequent Downline Consumption")]
        SubsequentDownlineConsumption
    }
    public enum MSP_Consumption_Score_AmountType
    {
        [ValueAttribute("SY")]
        [DescriptionAttribute("Score Y")]
        ScoreY,
        [ValueAttribute("SZ")]
        [DescriptionAttribute("Score Z")]
        ScoreZ
    }
    public enum MSP_Consumption_Score_Status
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed
    }
    #endregion 

    #region MS_Setting
    public enum MSP_Setting_Status
    {
        [ValueAttribute("A")]
        [DescriptionAttribute("Active")]
        Active,
        [ValueAttribute("I")]
        [DescriptionAttribute("Inactive")]
        Inactive
    }
    #endregion

    #region MSP_MemberWalletAddress
    //public enum MSP_MemberWalletAddress_AddressSource
    //{
    //    [ValueAttribute("F")]
    //    [DescriptionAttribute("Finsys")]
    //    Finsys,
    //    [ValueAttribute("G")]
    //    [DescriptionAttribute("Game")]
    //    Game
    //}
    //public enum MSP_MemberWalletAddress_AddressType
    //{
    //    [ValueAttribute("D")]
    //    [DescriptionAttribute("Deposit")]
    //    Finsys,
    //    [ValueAttribute("W")]
    //    [DescriptionAttribute("Withdrawal")]
    //    Game
    //}
    #endregion

    #region MSP_WT_Bank
    public enum MSP_WT_Bank_RefType
    {
        [ValueAttribute("DS")]
        [DescriptionAttribute("Deposit Score")]
        DepositScore,
        [ValueAttribute("CS")]
        [DescriptionAttribute("Consumption Score")]
        ConsumptionScore
    }
    public enum MSP_WT_Bank_AmountType
    {
        [ValueAttribute("DS")]
        [DescriptionAttribute("Deposit Score")]
        DepositScore,
        [ValueAttribute("CS")]
        [DescriptionAttribute("Consumption Score")]
        ConsumptionScore
    }
    #endregion

    #region MSP_SecurityQuestion
    public enum MSP_SecurityQuestion_Status
    {
        [ValueAttribute("A")]
        [DescriptionAttribute("Active")]
        Active,
        [ValueAttribute("I")]
        [DescriptionAttribute("Inactive")]
        Inactive
    }
    #endregion 

    #region MSP_SecurityAnswer
    public enum MSP_SecurityAnswer_SecurityQuestionType
    {
        [ValueAttribute("S")]
        [DescriptionAttribute("System")]
        System,
        [ValueAttribute("C")]
        [DescriptionAttribute("Custom")]
        Custom
    }
    #endregion 

    #region MSP_Security_Token
    public enum MSP_Security_Token_TokenType
    {
        [ValueAttribute("RP")]
        [DescriptionAttribute("Reset Password")]
        ResetPassword,
        [ValueAttribute("CP")]
        [DescriptionAttribute("Change Password")]
        ChangePassword,
        [ValueAttribute("CTP")]
        [DescriptionAttribute("Change Transaction Password")]
        ChangeTransactionPassword,
        [ValueAttribute("CSQA")]
        [DescriptionAttribute("Change Security Question Answer")]
        ChangeSecurityQuestionAnswer
    }
    #endregion

    #region MSP_Announce_Content
    public enum MSP_Announce_Content_ContentType
    {
        [ValueAttribute("V")]
        [DescriptionAttribute("API_MSP_Announce_Content_ContentType.Video")]
        Video,
        [ValueAttribute("U")]
        [DescriptionAttribute("API_MSP_Announce_Content_ContentType.UserGuide")]
        UserGuide,
        [ValueAttribute("A")]
        [DescriptionAttribute("API_MSP_Announce_Content_ContentType.Announcement")]
        Announcement,
        [ValueAttribute("P")]
        [DescriptionAttribute("API_MSP_Announce_Content_ContentType.Promotion")]
        Promotion
    }
    public enum MSP_Announce_Content_Status
    {
        [ValueAttribute("A")]
        [DescriptionAttribute("Active")]
        Active,
        [ValueAttribute("I")]
        [DescriptionAttribute("Inactive")]
        Inactive
    }
    #endregion

    #region MSP_Transaction_Fees
    public enum MSP_Transaction_Fees_Status
    {
        [ValueAttribute("A")]
        [DescriptionAttribute("Active")]
        Active,
        [ValueAttribute("I")]
        [DescriptionAttribute("Inactive")]
        Inactive,
    }
    public enum MSP_Transaction_Fees_FeesType //WilliamHo 20180914 MSP-135
    {
        [ValueAttribute("F")]
        [DescriptionAttribute("Fixed")]
        Fixed,
        [ValueAttribute("P")]
        [DescriptionAttribute("Percentage")]
        Percentage,
    }
    public enum MSP_Transaction_Fees_TransactionType //WilliamHo 20180914 MSP-135
    {
        [ValueAttribute("MWF")]
        [DescriptionAttribute("mBTC_WithdrawalFees")]
        mBTC_WithdrawalFees,
    }
    #endregion

    #region Customer Type 
    //Tony 20181101 MSP-411 \/  
    public enum Customer_Type
    {
        [ValueAttribute("M")]
        [DescriptionAttribute("Merchant")]
        Merchant,

        [ValueAttribute("C")]
        [DescriptionAttribute("Consumer")]
        Consumer,

        [ValueAttribute("B")]
        [DescriptionAttribute("Bank")]
        Bank, // Tony Liew 20190111 MSP-670
    }
    //Tony 20181101 MSP-411 /\  
    #endregion

    #region Status type (Withdrawal Admin Panel Use)
    public enum WithdrawalStatusEnum
    {
        [ValueAttribute("N")]
        [DescriptionAttribute("New")]
        New,
        [ValueAttribute("P")]
        [DescriptionAttribute("Pending")]
        Pending,
        [ValueAttribute("F")]
        [DescriptionAttribute("Failed")]
        Failed,
        [ValueAttribute("S")]
        [DescriptionAttribute("Success")]
        Success
    }
    #endregion

    //Tony Liew 20190215 MDT-237 \/
    #region Platform Name
    public enum PlatformNameEnum
    {
        [ValueAttribute("Jemjar")]
        [DescriptionAttribute("Crypto-Rewards From Your Favourite Local Places")]
        Jemjar,
        [ValueAttribute("FSB")]
        [DescriptionAttribute("Free Shopping Bus")]
        FSB,
        [ValueAttribute("Powerkingdoms")]
        [DescriptionAttribute("Global Entertainment Club")]
        Powerkingdoms,
        [ValueAttribute("BTC Boosters")]
        [DescriptionAttribute("BTC Boosters")]
        BTCBoosters,
        [ValueAttribute("Wenda")]
        [DescriptionAttribute("Wenda")]
        Wenda
    }
    #endregion
    //Tony Liew 20190215 MDT-237 /\
}
