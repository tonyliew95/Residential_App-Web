using System;

namespace Nop.Core.Domain.Msp.Custom
{
    public class DepositRewardHistoryCustom //Atiqah 20180926 MSP-150
    {
        /// <summary>
        /// Gets or sets the Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the Contributed By
        /// </summary>
        //public int ContributedBy { get; set; } //Jerry 20181009 MSP-209
        //public string ContributedBy { get; set; } //Jerry 20181009 MSP-209 //20190221 MDT-257

        /// <summary>
        /// Gets or sets the Total Deposit Amount
        /// </summary>
        public decimal Deposit_Amt { get; set; }

        /// <summary>
        /// Gets or sets the Total Deposit Returned
        /// </summary>
        public decimal Deposit_Returned { get; set; }

        /// <summary>
        /// Gets or sets the Total Deposit Returned
        /// </summary>
        public decimal Deposit_Returned_Reward { get; set; } //Atiqah 20190221 MDT-257

        /// <summary>
        /// Gets or sets the Total Deposit Returned
        /// </summary>
        public decimal Deposit_Overflow_Reward { get; set; } //Atiqah 20190221 MDT-257

        /// <summary>
        /// Gets or sets the Total Deposit Reward
        /// </summary>
        //public decimal Deposit_Reward { get; set; } //Atiqah 20190221 MDT-257

        /// <summary>
        /// Gets or sets the Total Extra Reward
        /// </summary>
        public decimal Extra_Reward { get; set; }

        /// <summary>
        /// Gets or sets the Total Reward
        /// </summary>
        public decimal Total_Reward { get; set; }

        //Atiqah 20190221 MDT-257 \/
        /// <summary>
        /// Gets or sets the Total Score Self Percentage
        /// </summary>
        //public decimal SelfScore_Pct { get; set; } 

        /// <summary>
        /// Gets or sets the Total Score Self 
        /// </summary>
        //public decimal SelfScore { get; set; } 

        /// <summary>
        /// Gets or sets the Total Team Self Percentage
        /// </summary>
        //public decimal TeamScore_Pct { get; set; } 

        /// <summary>
        /// Gets or sets the Total Team Self
        /// </summary>
        //public decimal TeamScore { get; set; } 
        //Atiqah 20190221 MDT-257 /\
    }
}
